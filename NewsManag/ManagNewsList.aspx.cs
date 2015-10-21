using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace EasyExam.NewsManag
{
	/// <summary>
	/// ManagNewsList ��ժҪ˵����
	/// </summary>
	public partial class ManagNewsList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
	
		#region//*******��ʼ����Ϣ********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=1)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ButIssuNews.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('IssuNews.aspx','IssuNews',500,375); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ��������')==false) return false;}");
					strSql="select a.NewsID,a.NewsTitle,a.NewsContent,a.BrowNumber,b.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from NewsInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID order by a.NewsID desc";
					if (DataGridNews.Attributes["SortExpression"] == null)
					{
						DataGridNews.Attributes["SortExpression"] = "NewsID";
						DataGridNews.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGridNews.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridNews_PageIndexChanged);
			this.DataGridNews.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridNews_SortCommand);
			this.DataGridNews.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridNews_DeleteCommand);
			this.DataGridNews.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridNews_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridNews_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridNews.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridNews_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridNews').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridNews').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridNews.Attributes.Add("oldValue", "#F7F7F7");
				DataGridNews.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******ɾ��ѡ������*******
		private void DataGridNews_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intNewsID=Convert.ToInt32(e.Item.Cells[0].Text);
			if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==e.Item.Cells[5].Text.Trim().ToUpper()))
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=null;

				SqlCmd=new SqlCommand("delete from NewsInfo where NewsID="+intNewsID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();

				SqlCmd=new SqlCommand("delete from NewsUser where NewsID="+intNewsID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();

				SqlConn.Close();
				SqlConn.Dispose();
				
				//�Զ���ҳ
				if (DataGridNews.Items.Count==1&&DataGridNews.CurrentPageIndex>0)
				{
					DataGridNews.CurrentPageIndex--;
				}        

				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
				return;
			}
		}
		#endregion

		#region//*******��ʾ�����б�*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"NewsInfo");
			RowNum=DataGridNews.CurrentPageIndex*DataGridNews.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridNews.Attributes["SortExpression"];
			string SortDirection = DataGridNews.Attributes["SortDirection"];
			SqlDS.Tables["NewsInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridNews.DataSource=SqlDS.Tables["NewsInfo"].DefaultView;
			DataGridNews.DataBind();
			for(int i=0;i<DataGridNews.Items.Count;i++)
			{				
				Label labNewsTitle=(Label)DataGridNews.Items[i].FindControl("labNewsTitle");
				labNewsTitle.Text=Server.HtmlEncode(labNewsTitle.Text);
				if (labNewsTitle.Text.Trim().Length>30)
				{
					labNewsTitle.Text=labNewsTitle.Text.Trim().Substring(0,30)+"...";
				}
				//labNewsTitle.Text=ObjFun.getStr(labNewsTitle.Text.Trim(),30)+"...";

				LinkButton LBEditNews=(LinkButton)DataGridNews.Items[i].FindControl("LinkButEditNews");
				LinkButton LBDel=(LinkButton)DataGridNews.Items[i].FindControl("LinkButDel");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridNews.Items[i].Cells[5].Text.Trim().ToUpper()))
				{
					LBEditNews.Attributes.Add("onclick", "javascript:jscomNewOpenByFixSize('EditNews.aspx?NewsID="+DataGridNews.Items[i].Cells[0].Text+"','IssuNews',500,375); return false;");
					LBDel.Attributes.Add("onclick", "javascript:{if(confirm('ȷ��Ҫɾ��ѡ��������')==false) return false;}");
				}
				else
				{
					LBEditNews.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["NewsInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridNews.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridNews.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******ɾ��������Ϣ*******
		protected void ButDelete_Click(object sender, System.EventArgs e)
		{
			if (Request["chkSelect"]!=null)
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=null;

				int i=0;
				string[] textArray=Request["chkSelect"].ToString().Split(',');
				for (int j=0;j<textArray.Length;j++)
			    {
					i=Convert.ToInt32(textArray[j]);

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridNews.Items[i].Cells[5].Text.Trim().ToUpper()))
					{
						SqlCmd=new SqlCommand("delete from NewsInfo where NewsID="+DataGridNews.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();

						SqlCmd=new SqlCommand("delete from NewsUser where NewsID="+DataGridNews.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}
				SqlConn.Close();
				
				//�Զ���ҳ
				if(textArray.Length==DataGridNews.Items.Count&&DataGridNews.CurrentPageIndex>0)
				{
					DataGridNews.CurrentPageIndex--;
				}

				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*******ȡ���༭��Ϣ*******
		private void DataGridNews_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridNews.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridNews_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridNews.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=false;
			strSql="select a.NewsID,a.NewsTitle,a.NewsContent,a.BrowNumber,b.LoginID as CreateLoginID,a.CreateUserID,convert(varchar(10),a.CreateDate,120) as CreateDate from NewsInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID";
			//��ʾ�����ű���Ϊ����������
			if (txtNewsTitle.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.NewsTitle like '%"+txtNewsTitle.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.NewsTitle like '%"+txtNewsTitle.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.NewsID desc";

			DataGridNews.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridNews.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridNews.CurrentPageIndex>0)
			{
				DataGridNews.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridNews.CurrentPageIndex<(DataGridNews.PageCount-1))
			{
				DataGridNews.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridNews.CurrentPageIndex=(DataGridNews.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridNews_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridNews.Columns.Count; i++)
			{
				DataGridNews.Columns[i].HeaderText = (DataGridNews.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridNews.Columns[i].HeaderText = (DataGridNews.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridNews.Columns.Count; i++)
			{
				if (DataGridNews.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridNews.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridNews.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridNews.Attributes["SortExpression"] = SortExpression;
			DataGridNews.Attributes["SortDirection"] = SortDirection;
			if (DataGridNews.Attributes["SortDirection"] == "ASC")
			{
				DataGridNews.Columns[colindex].HeaderText = DataGridNews.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridNews.Columns[colindex].HeaderText = DataGridNews.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
