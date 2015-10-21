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
using System.IO;

namespace EasyExam.GradeManag
{
	/// <summary>
	/// StatisLore ��ժҪ˵����
	/// </summary>
	public partial class StatisLore : System.Web.UI.Page
	{
		protected int RowNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intCount=0;
		bool bJoySoftware=false;
	
		#region//*******��ʼ����Ϣ********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myLoginID=Session["LoginID"].ToString();
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intCount=Convert.ToInt32(ObjFun.GetValues("select count(*) as count from UserScore where PaperID="+intPaperID+" and ExamState=1","count"));//����¼��
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
			bJoySoftware=ObjFun.JoySoftware();
//			if (bJoySoftware==false)
//			{
//				ObjFun.Alert("�Բ���δע���û����ܽ����Ծ�ͳ�ƣ�");
//			}

			strSql="select c.SubjectName,d.LoreName,count(d.LoreID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,SubjectInfo c,LoreInfo d,UserScore e where a.RubricID=b.RubricID and b.SubjectID=c.SubjectID and b.LoreID=d.LoreID and a.UserScoreID=e.UserScoreID and e.PaperID="+intPaperID+" and e.ExamState=1 group by c.SubjectName,d.LoreName order by c.SubjectName asc";
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=6)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					if (intPaperID!=0)
					{
						if (DataGridLore.Attributes["SortExpression"] == null)
						{
							DataGridLore.Attributes["SortExpression"] = "SubjectName";
							DataGridLore.Attributes["SortDirection"] = "ASC";
						}
						ShowData(strSql);
					}
				}
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
			this.DataGridLore.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridLore_PageIndexChanged);
			this.DataGridLore.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridLore_SortCommand);
			this.DataGridLore.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridLore_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridLore_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridLore.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridLore_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridLore').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridLore').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridLore.Attributes.Add("oldValue", "#F7F7F7");
				DataGridLore.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"UserAnswer");
			RowNum=DataGridLore.CurrentPageIndex*DataGridLore.PageSize+1;

			string SortExpression = DataGridLore.Attributes["SortExpression"];
			string SortDirection = DataGridLore.Attributes["SortDirection"];
			SqlDS.Tables["UserAnswer"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridLore.DataSource=SqlDS.Tables["UserAnswer"].DefaultView;
			DataGridLore.DataBind();
			for(int i=0;i<DataGridLore.Items.Count;i++)
			{
				System.Web.UI.WebControls.Image imgRate=(System.Web.UI.WebControls.Image)DataGridLore.Items[i].FindControl("imgRate");
				imgRate.Width=Convert.ToInt32(200*Convert.ToDouble(DataGridLore.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridLore.Items[i].Cells[4].Text.Trim()));
				imgRate.ToolTip=Convert.ToString(System.Math.Round(Convert.ToDouble(DataGridLore.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridLore.Items[i].Cells[4].Text.Trim())*100,1))+"%";
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridLore_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridLore.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridLore.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridLore.CurrentPageIndex>0)
			{
				DataGridLore.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridLore.CurrentPageIndex<(DataGridLore.PageCount-1))
			{
				DataGridLore.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		private void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridLore.CurrentPageIndex=(DataGridLore.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******����ͳ������*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			strSql="select c.SubjectName,d.LoreName,count(d.LoreID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,SubjectInfo c,LoreInfo d,UserScore e where a.RubricID=b.RubricID and b.SubjectID=c.SubjectID and b.LoreID=d.LoreID and a.UserScoreID=e.UserScoreID and e.PaperID="+intPaperID+" and e.ExamState=1 group by c.SubjectName,d.LoreName order by c.SubjectName asc";

			//������Excel�ļ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//׼���ļ�
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"StatisLore.xls",Server.MapPath("..\\UpLoadFiles\\")+"StatisLore.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"StatisLore.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��¼Ϊ�գ����ܵ�����')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//�����ļ�
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=StatisLore.xls");
		}
		#endregion

		#region//*******��������*******
		private void DataGridLore_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridLore.Columns.Count; i++)
			{
				DataGridLore.Columns[i].HeaderText = (DataGridLore.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridLore.Columns[i].HeaderText = (DataGridLore.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridLore.Columns.Count; i++)
			{
				if (DataGridLore.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridLore.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridLore.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridLore.Attributes["SortExpression"] = SortExpression;
			DataGridLore.Attributes["SortDirection"] = SortDirection;
			if (DataGridLore.Attributes["SortDirection"] == "ASC")
			{
				DataGridLore.Columns[colindex].HeaderText = DataGridLore.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridLore.Columns[colindex].HeaderText = DataGridLore.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}

}
