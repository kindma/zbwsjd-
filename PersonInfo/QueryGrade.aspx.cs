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

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// QueryGrade ��ժҪ˵����
	/// </summary>
	public partial class QueryGrade : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImgButSubject;
		protected System.Web.UI.WebControls.TextBox txtSubjectName;
		protected int RowNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
		int intSeeResult=0;
		int intPaperType=0;
		int intOrder=0;
	
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
			txtStartTime.Attributes["readonly"]="true";
			txtEndTime.Attributes["readonly"]="true";
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
				strSql="select a.UserScoreID,a.PaperID,b.PaperName,b.SeeResult,a.ExamState,a.StartTime,a.EndTime,case a.JudgeState when 0 then 'δ��' when 1 then '����' end as JudgeState,c.LoginID as JudgeLoginID,case a.PassState when 0 then 'δͨ��' when 1 then 'ͨ��' end as PassState,a.TotalMark from UserScore a LEFT OUTER JOIN PaperInfo b ON a.PaperID=b.PaperID LEFT OUTER JOIN UserInfo c ON a.JudgeUserID=c.UserID where a.ExamState=1 and b.PaperType="+intPaperType+" and b.SeeResult=1 and a.UserID="+intUserID+" order by UserScoreID desc";
				if (DataGridGrade.Attributes["SortExpression"] == null)
				{
					DataGridGrade.Attributes["SortExpression"] = "UserScoreID";
					DataGridGrade.Attributes["SortDirection"] = "DESC";
				}
				ShowData(strSql);
				LabCondition.Text=strSql;
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
			SqlCmd.Fill(SqlDS,"UserScore");
			RowNum=DataGridGrade.CurrentPageIndex*DataGridGrade.PageSize+1;

			string SortExpression = DataGridGrade.Attributes["SortExpression"];
			string SortDirection = DataGridGrade.Attributes["SortDirection"];
			SqlDS.Tables["UserScore"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridGrade.DataSource=SqlDS.Tables["UserScore"].DefaultView;
			DataGridGrade.DataBind();

			SqlDataAdapter SqlCmdTmp=null;
			DataSet SqlDSTmp=null;
			for(int i=0;i<DataGridGrade.Items.Count;i++)
			{
				string m=DataGridGrade.Items[i].Cells[3].Text.Trim();
				Label labExamTime=(Label)DataGridGrade.Items[i].FindControl("labExamTime");
				labExamTime.Text=DataGridGrade.Items[i].Cells[10].Text.Trim()+"/<br>"+DataGridGrade.Items[i].Cells[11].Text.Trim();

				Label labTotalMark=(Label)DataGridGrade.Items[i].FindControl("labTotalMark");
				Label labPassState=(Label)DataGridGrade.Items[i].FindControl("labPassState");

				LinkButton LBOrder=(LinkButton)DataGridGrade.Items[i].FindControl("LinkButOrder");
				LinkButton LBAnswer=(LinkButton)DataGridGrade.Items[i].FindControl("LinkButAnswer");
				LinkButton LBStatis=(LinkButton)DataGridGrade.Items[i].FindControl("LinkButStatis");

				if (DataGridGrade.Items[i].Cells[12].Text.Trim()=="0")
				{
					labTotalMark.Text="****";
					labPassState.Text="****";
				
					LBOrder.Attributes.Add("onclick","javascript:alert('���Ծ�����鿴�����');return false;");
					LBAnswer.Attributes.Add("onclick","javascript:alert('���Ծ�����鿴�����');return false;");
					LBStatis.Attributes.Add("onclick","javascript:alert('���Ծ�����鿴�����');return false;");
				}
				else
				{
					//labTotalMark.Text=Convert.ToString(System.Math.Round(Convert.ToDouble(labTotalMark.Text),1));
					//labPassState.Text=labPassState.Text;
				
					//SqlCmdTmp=new SqlDataAdapter("select TotalMark from UserScore where PaperID="+DataGridGrade.Items[i].Cells[1].Text.Trim()+" and ExamState=1 order by TotalMark desc",SqlConn);
					//SqlDSTmp=new DataSet();
					//SqlCmdTmp.Fill(SqlDSTmp,"UserScore");
					//for(int j=0;j<SqlDSTmp.Tables["UserScore"].Rows.Count;j++)
					//{
					//	if (System.Math.Round(Convert.ToDouble(SqlDSTmp.Tables["UserScore"].Rows[j]["TotalMark"]),1)==System.Math.Round(Convert.ToDouble(labTotalMark.Text),1))
					//	{
					//		intOrder=j+1;
					//		break;
					//	}
					//}
					intOrder=Convert.ToInt32(ObjFun.GetValues("select count(*) as count from UserScore where PaperID="+DataGridGrade.Items[i].Cells[1].Text.Trim()+" and ExamState=1 and TotalMark>"+labTotalMark.Text+"","count"))+1;
					LBOrder.Attributes.Add("onclick","javascript:alert('���ڱ���"+labPaperType.Text+"��������"+intOrder.ToString()+"����');return false;");
					LBAnswer.Attributes.Add("onclick","javascript:NewWin=window.open('ShowAnswer.aspx?UserScoreID="+DataGridGrade.Items[i].Cells[0].Text.Trim()+"','ShowAnswer','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
					LBStatis.Attributes.Add("onclick","javascript:NewWin=window.open('ShowStatis.aspx?UserScoreID="+DataGridGrade.Items[i].Cells[0].Text.Trim()+"','ShowStatis','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserScore"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridGrade.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridGrade.CurrentPageIndex+1);
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
			this.DataGridGrade.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridGrade_PageIndexChanged);
			this.DataGridGrade.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridGrade_SortCommand);
			this.DataGridGrade.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridGrade_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridGrade_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridGrade.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridGrade_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridGrade').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridGrade').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridGrade.Attributes.Add("oldValue", "#F7F7F7");
				DataGridGrade.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridGrade_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridGrade.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ȡ���༭��Ϣ*******
		private void DataGridGrade_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridGrade.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.UserScoreID,a.PaperID,b.PaperName,b.SeeResult,a.ExamState,a.StartTime,a.EndTime,case a.JudgeState when 0 then 'δ��' when 1 then '����' end as JudgeState,c.LoginID as JudgeLoginID,case a.PassState when 0 then 'δͨ��' when 1 then 'ͨ��' end as PassState,a.TotalMark from UserScore a LEFT OUTER JOIN PaperInfo b ON a.PaperID=b.PaperID LEFT OUTER JOIN UserInfo c ON a.JudgeUserID=c.UserID where a.ExamState=1 and b.PaperType="+intPaperType+" and b.SeeResult=1 and a.UserID="+intUserID+"";
			//��ʾ������Ϊ����������
			if (txtPaperName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where b.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//��ʾ�Կ�ʼ����Ϊ����������
			if (txtStartTime.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.StartTime>='"+txtStartTime.Text.Trim()+"'";
				}
				else
				{
					strSql=strSql+" where a.StartTime>='"+txtStartTime.Text.Trim()+"'";
					bWhere=true;
				}
			}
			//��ʾ�Կ�ʼ����Ϊ����������
			if (txtEndTime.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.EndTime<='"+txtEndTime.Text.Trim()+"'";
				}
				else
				{
					strSql=strSql+" where a.EndTime<='"+txtEndTime.Text.Trim()+"'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.UserScoreID desc";

			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex>0)
			{
				DataGridGrade.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex<(DataGridGrade.PageCount-1))
			{
				DataGridGrade.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=(DataGridGrade.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridGrade_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridGrade.Columns.Count; i++)
			{
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridGrade.Columns.Count; i++)
			{
				if (DataGridGrade.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridGrade.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridGrade.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridGrade.Attributes["SortExpression"] = SortExpression;
			DataGridGrade.Attributes["SortDirection"] = SortDirection;
			if (DataGridGrade.Attributes["SortDirection"] == "ASC")
			{
				DataGridGrade.Columns[colindex].HeaderText = DataGridGrade.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridGrade.Columns[colindex].HeaderText = DataGridGrade.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
