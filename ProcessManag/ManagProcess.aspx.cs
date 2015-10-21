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

namespace EasyExam.ProcessManag
{
	/// <summary>
	/// ManagProcess ��ժҪ˵����
	/// </summary>
	public partial class ManagProcess : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImgButSubject;
		protected System.Web.UI.WebControls.TextBox txtSubjectName;
		protected int RowNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0,intPaperType=0;
	
		#region//*******��ʼ����Ϣ********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				intUserID=Convert.ToInt32(myUserID);
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			intPaperType=Convert.ToInt32(Request["PaperType"]);
			if (intPaperType==1)
			{
				labPaperType.Text="����";
			}
			else
			{
				labPaperType.Text="��ҵ";
			}
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=5)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					strSql="select a.PaperID,a.PaperName,a.PaperType,case a.ProduceWay when 1 then '����̶�' when 2 then '�������' when 3 then '�������' end as ProduceWay,a.StartTime,a.EndTime,a.PaperMark,a.PassMark,b.LoginID as CreateLoginID,(select count(*) from UserScore c where a.PaperID=c.PaperID) as intCount from PaperInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where a.PaperType="+intPaperType+" and (a.ManagerAccount=1 or (a.ManagerAccount=2 and Exists(select * from PaperUser where PaperUser.PaperID=a.PaperID and PaperUser.UserType=1 and PaperUser.UserID="+intUserID+")) or Exists(select * from UserInfo d,DeptInfo e,PaperUser f where d.UserID="+intUserID+" and d.DeptID=e.DeptID and e.DeptID=f.DeptID and f.PaperID=a.PaperID and f.UserType=1)) order by a.PaperID desc";
					if (DataGridPaper.Attributes["SortExpression"] == null)
					{
						DataGridPaper.Attributes["SortExpression"] = "PaperID";
						DataGridPaper.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
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
			SqlCmd.Fill(SqlDS,"PaperInfo");
			RowNum=DataGridPaper.CurrentPageIndex*DataGridPaper.PageSize+1;

			string SortExpression = DataGridPaper.Attributes["SortExpression"];
			string SortDirection = DataGridPaper.Attributes["SortDirection"];
			SqlDS.Tables["PaperInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridPaper.DataSource=SqlDS.Tables["PaperInfo"].DefaultView;
			DataGridPaper.DataBind();
			for(int i=0;i<DataGridPaper.Items.Count;i++)
			{				
				Label labAvaiTime=(Label)DataGridPaper.Items[i].FindControl("labAvaiTime");
				labAvaiTime.Text=DataGridPaper.Items[i].Cells[8].Text.Trim()+"/<br>"+DataGridPaper.Items[i].Cells[9].Text.Trim();

				LinkButton LBAnswerRecord=(LinkButton)DataGridPaper.Items[i].FindControl("LinkButAnswerRecord");
				LBAnswerRecord.Attributes.Add("onclick", "javascript:NewWin=window.open('AnswerRecord.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"&PaperName="+DataGridPaper.Items[i].Cells[2].Text.Trim()+"&PaperMark="+DataGridPaper.Items[i].Cells[10].Text.Trim()+"&PassMark="+DataGridPaper.Items[i].Cells[11].Text.Trim()+"&PaperType="+intPaperType+"','AnswerRecord','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
				
				LinkButton LBDel=(LinkButton)DataGridPaper.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick","javascript:{if(confirm('ȷ��Ҫɾ��ѡ������')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["PaperInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridPaper.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridPaper.CurrentPageIndex+1);
			SqlConn.Dispose();
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
			this.DataGridPaper.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridPaper_PageIndexChanged);
			this.DataGridPaper.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridPaper.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridPaper_DeleteCommand);
			this.DataGridPaper.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPaper_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridPaper_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridPaper.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridPaper_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPaper').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPaper').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridPaper.Attributes.Add("oldValue", "#F7F7F7");
				DataGridPaper.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridPaper_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ɾ��ѡ�д��*******
		private void DataGridPaper_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intPaperID=Convert.ToInt32(e.Item.Cells[0].Text);

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlConn.Open();

			//ɾ������¼
			SqlCmd=new SqlCommand("delete UserAnswer from UserAnswer a LEFT OUTER JOIN UserScore b ON a.UserScoreID=b.UserScoreID where b.PaperID="+intPaperID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();
			//ɾ���ο���¼
			SqlCmd=new SqlCommand("delete from UserScore where PaperID="+intPaperID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Close();
			SqlConn.Dispose();

			//�Զ���ҳ
			if (DataGridPaper.Items.Count==1&&DataGridPaper.CurrentPageIndex>0)
			{
				DataGridPaper.CurrentPageIndex--;
			}        

			ShowData(strSql);
		}
		#endregion
		
		#region//*******ȡ���༭��Ϣ*******
		private void DataGridPaper_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.PaperID,a.PaperName,a.PaperType,case a.ProduceWay when 1 then '����̶�' when 2 then '�������' when 3 then '�������' end as ProduceWay,a.StartTime,a.EndTime,a.PaperMark,a.PassMark,b.LoginID as CreateLoginID,(select count(*) from UserScore c where a.PaperID=c.PaperID) as intCount from PaperInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where a.PaperType="+intPaperType+" and (a.ManagerAccount=1 or (a.ManagerAccount=2 and Exists(select * from PaperUser where PaperUser.PaperID=a.PaperID and PaperUser.UserType=1 and PaperUser.UserID="+intUserID+")) or Exists(select * from UserInfo d,DeptInfo e,PaperUser f where d.UserID="+intUserID+" and d.DeptID=e.DeptID and e.DeptID=f.DeptID and f.PaperID=a.PaperID and f.UserType=1))";
			//��ʾ������Ϊ����������
			if (txtPaperName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where a.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//��ʾ�Դ�����ʽΪ����������
			if (DDLCreateWay.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.CreateWay='"+DDLCreateWay.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.CreateWay='"+DDLCreateWay.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.PaperID desc";

			DataGridPaper.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex>0)
			{
				DataGridPaper.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex<(DataGridPaper.PageCount-1))
			{
				DataGridPaper.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=(DataGridPaper.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridUser_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridPaper.Columns.Count; i++)
			{
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridPaper.Columns.Count; i++)
			{
				if (DataGridPaper.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridPaper.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridPaper.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridPaper.Attributes["SortExpression"] = SortExpression;
			DataGridPaper.Attributes["SortDirection"] = SortDirection;
			if (DataGridPaper.Attributes["SortDirection"] == "ASC")
			{
				DataGridPaper.Columns[colindex].HeaderText = DataGridPaper.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridPaper.Columns[colindex].HeaderText = DataGridPaper.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
