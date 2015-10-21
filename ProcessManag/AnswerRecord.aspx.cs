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
	/// AnswerRecord ��ժҪ˵����
	/// </summary>
	public partial class AnswerRecord : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImgButSubject;
		protected System.Web.UI.WebControls.TextBox txtSubjectName;
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intUserID=0,intPaperType=0;
	
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
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intPaperType=Convert.ToInt32(Request["PaperType"]);
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
			labPaperMark.Text=Convert.ToString(Request["PaperMark"]);
			labPassMark.Text=Convert.ToString(Request["PassMark"]);
			if (!IsPostBack)
			{
				LoadDeptInfo();//��ʾ������Ϣ
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
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ������')==false) return false;}");
					ButRefresh.Attributes.Add("onclick","javascript:{ RefreshForm();return false;}");
					ButAbsentUser.Attributes.Add("onclick","javascript:NewWin=window.open('AbsentUser.aspx?PaperID="+intPaperID+"&PaperName="+labPaperName.Text+"','AbsentUser','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
					strSql="select a.UserScoreID,b.UserID,b.LoginID,b.UserName,c.DeptName,round(a.TotalMark,1) as TotalMark,a.StartTime,a.EndTime,a.LoginIP,case a.ExamState when 0 then '�����' when 1 then '�ѽ���' end as ExamState,d.LoginID as JudgeLoginID,a.ImpScore from UserScore a LEFT OUTER JOIN UserInfo b ON a.UserID=b.UserID LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN UserInfo d ON a.JudgeUserID=d.UserID,UserInfo e where a.PaperID="+intPaperID+" and e.UserID="+intUserID+" and (e.JudgeUser=1 or (e.JudgeUser=2 and Exists(select * from UserPower where UserPower.UserID="+intUserID+" and UserPower.PowerID=1 and UserPower.OptionID=a.UserID)) or Exists(select * from UserInfo f,DeptInfo g,UserPower h where f.UserID=a.UserID and f.DeptID=g.DeptID and g.DeptID=h.DeptID and h.UserID="+intUserID+" and h.PowerID=1)) order by b.UserID desc";
					if (DataGridScore.Attributes["SortExpression"] == null)
					{
						DataGridScore.Attributes["SortExpression"] = "UserID";
						DataGridScore.Attributes["SortDirection"] = "DESC";
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

		#region//******��ʾ������Ϣ******
		private void LoadDeptInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from DeptInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"DeptInfo");
			DDLDept.DataSource=SqlDS.Tables["DeptInfo"].DefaultView;
			DDLDept.DataTextField="DeptName";
			DDLDept.DataValueField="DeptID";
			DDLDept.DataBind();
			SqlConn.Dispose();
			ListItem objTmp=new ListItem("--ȫ��--","0");
			DDLDept.Items.Add(objTmp);
			DDLDept.Items.FindByText("--ȫ��--").Selected=true;
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
			RowNum=DataGridScore.CurrentPageIndex*DataGridScore.PageSize+1;

			string SortExpression = DataGridScore.Attributes["SortExpression"];
			string SortDirection = DataGridScore.Attributes["SortDirection"];
			SqlDS.Tables["UserScore"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridScore.DataSource=SqlDS.Tables["UserScore"].DefaultView;
			DataGridScore.DataBind();
			for(int i=0;i<DataGridScore.Items.Count;i++)
			{				
				Label labExamTime=(Label)DataGridScore.Items[i].FindControl("labExamTime");
				labExamTime.Text=DataGridScore.Items[i].Cells[11].Text.Trim()+"/<br>"+DataGridScore.Items[i].Cells[12].Text.Trim();

				LinkButton LBAnswer=(LinkButton)DataGridScore.Items[i].FindControl("LinkButAnswer");
				LBAnswer.Attributes.Add("onclick","javascript:NewWin=window.open('../PersonInfo/ShowAnswer.aspx?UserScoreID="+DataGridScore.Items[i].Cells[0].Text.Trim()+"&ManageUser=1','ShowAnswer','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");

				LinkButton LBStatis=(LinkButton)DataGridScore.Items[i].FindControl("LinkButStatis");
				LBStatis.Attributes.Add("onclick","javascript:NewWin=window.open('../PersonInfo/ShowStatis.aspx?UserScoreID="+DataGridScore.Items[i].Cells[0].Text.Trim()+"&ManageUser=1','ShowStatis','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");

				LinkButton LBJudge=(LinkButton)DataGridScore.Items[i].FindControl("LinkButJudge");
				LBJudge.Attributes.Add("onclick","javascript:NewWin=window.open('JudgeAnswer.aspx?UserScoreID="+DataGridScore.Items[i].Cells[0].Text.Trim()+"&ImpScore="+DataGridScore.Items[i].Cells[13].Text.Trim()+"&PassMark="+labPassMark.Text+"&ManageUser=1','JudgeAnswer','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");

				LinkButton LBDel=(LinkButton)DataGridScore.Items[i].FindControl("LinkButDelete");
				LBDel.Attributes.Add("onclick", "javascript:{if(confirm('ȷ��Ҫɾ��ѡ������')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserScore"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridScore.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridScore.CurrentPageIndex+1);
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
			this.DataGridScore.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridScore_PageIndexChanged);
			this.DataGridScore.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridScore.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridScore_DeleteCommand);
			this.DataGridScore.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridScore_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridScore_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridScore.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridScore_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridScore').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridScore').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridScore.Attributes.Add("oldValue", "#F7F7F7");
				DataGridScore.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridScore_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridScore.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
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

					SqlCmd=new SqlCommand("delete from UserAnswer where UserScoreID="+DataGridScore.Items[i].Cells[0].Text.Trim(),SqlConn);
					SqlCmd.ExecuteNonQuery();

					SqlCmd=new SqlCommand("delete from UserScore where UserScoreID="+DataGridScore.Items[i].Cells[0].Text.Trim(),SqlConn);
					SqlCmd.ExecuteNonQuery();
				}

				//�Զ���ҳ
				if(textArray.Length==DataGridScore.Items.Count&&DataGridScore.CurrentPageIndex>0)
				{
					DataGridScore.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*******ɾ��ѡ�д��*******
		private void DataGridScore_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intUserScoreID=Convert.ToInt32(e.Item.Cells[0].Text);

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlConn.Open();

			SqlCmd=new SqlCommand("delete from UserAnswer where UserScoreID="+intUserScoreID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlCmd=new SqlCommand("delete from UserScore where UserScoreID="+intUserScoreID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Close();
			SqlConn.Dispose();

			//�Զ���ҳ
			if (DataGridScore.Items.Count==1&&DataGridScore.CurrentPageIndex>0)
			{
				DataGridScore.CurrentPageIndex--;
			}        

			ShowData(strSql);
		}
		#endregion
		
		#region//*******ȡ���༭��Ϣ*******
		private void DataGridScore_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridScore.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.UserScoreID,b.UserID,b.LoginID,b.UserName,c.DeptName,round(a.TotalMark,1) as TotalMark,a.StartTime,a.EndTime,a.LoginIP,case a.ExamState when 0 then '�����' when 1 then '�ѽ���' end as ExamState,d.LoginID as JudgeLoginID,a.ImpScore from UserScore a LEFT OUTER JOIN UserInfo b ON a.UserID=b.UserID LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN UserInfo d ON a.JudgeUserID=d.UserID,UserInfo e where a.PaperID="+intPaperID+" and e.UserID="+intUserID+" and (e.JudgeUser=1 or (e.JudgeUser=2 and Exists(select * from UserPower where UserPower.UserID="+intUserID+" and UserPower.PowerID=1 and UserPower.OptionID=a.UserID)) or Exists(select * from UserInfo f,DeptInfo g,UserPower h where f.UserID=a.UserID and f.DeptID=g.DeptID and g.DeptID=h.DeptID and h.UserID="+intUserID+" and h.PowerID=1))";
			//��ʾ�Բ���Ϊ����������
			if (DDLDept.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and c.DeptID='"+DDLDept.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where c.DeptID='"+DDLDept.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ���ʺ�Ϊ����������
			if (txtLoginID.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
				}
				else
				{
					strSql=strSql+" where b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (txtUserName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//��ʾ�Դ��״̬Ϊ����������
			if (DDLExamState.SelectedItem.Value!="-1")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.ExamState='"+DDLExamState.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.ExamState='"+DDLExamState.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ������״̬Ϊ����������
			if (DDLJudgeState.SelectedItem.Value!="-1")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.JudgeState='"+DDLJudgeState.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.JudgeState='"+DDLJudgeState.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by b.UserID desc";

			DataGridScore.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******�����¼�*******
		private void ImgButReturn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("ManagProcess.aspx?PaperType="+intPaperType+"");
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridScore.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridScore.CurrentPageIndex>0)
			{
				DataGridScore.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridScore.CurrentPageIndex<(DataGridScore.PageCount-1))
			{
				DataGridScore.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridScore.CurrentPageIndex=(DataGridScore.PageCount-1);
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
			for (int i = 0; i < DataGridScore.Columns.Count; i++)
			{
				DataGridScore.Columns[i].HeaderText = (DataGridScore.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridScore.Columns[i].HeaderText = (DataGridScore.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridScore.Columns.Count; i++)
			{
				if (DataGridScore.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridScore.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridScore.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridScore.Attributes["SortExpression"] = SortExpression;
			DataGridScore.Attributes["SortDirection"] = SortDirection;
			if (DataGridScore.Attributes["SortDirection"] == "ASC")
			{
				DataGridScore.Columns[colindex].HeaderText = DataGridScore.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridScore.Columns[colindex].HeaderText = DataGridScore.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
