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

namespace EasyExam.PersonalInfo
{
	/// <summary>
	/// JoinExam 的摘要说明。
	/// </summary>
	public partial class JoinExam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImgButSubject;
		protected System.Web.UI.WebControls.TextBox txtSubjectName;
		protected int RowNum=0;

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
	
		#region//*******初始化信息********
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
			strSql="select a.PaperID,a.PaperName,a.PaperType,case a.ProduceWay when 1 then '题序固定' when 2 then '题序随机' when 3 then '试题随机' end as ProduceWay,a.ShowModal,a.ExamTime,a.StartTime,a.EndTime,a.TestCount,a.PaperMark,a.CreateWay,b.LoginID as CreateLoginID from PaperInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where a.PaperType=1 and a.StartTime<Getdate() and a.EndTime>Getdate() and (a.ExamAccount=1 or (a.ExamAccount=2 and Exists(select * from PaperUser where PaperUser.PaperID=a.PaperID and PaperUser.UserType=0 and PaperUser.UserID="+intUserID+")) or Exists(select * from UserInfo d,DeptInfo e,PaperUser f where d.UserID="+intUserID+" and d.DeptID=e.DeptID and e.DeptID=f.DeptID and f.PaperID=a.PaperID and f.UserType=0)) and (a.RepeatExam=1 or ((a.RepeatExam=0) and (not Exists (select * from UserScore where UserScore.PaperID=a.PaperID and UserScore.UserID="+intUserID+" and UserScore.ExamState=1)))) order by a.PaperID desc";
			if (!IsPostBack)
			{
				if (DataGridPaper.Attributes["SortExpression"] == null)
				{
					DataGridPaper.Attributes["SortExpression"] = "PaperID";
					DataGridPaper.Attributes["SortDirection"] = "DESC";
				}
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******显示数据列表*******
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
				labAvaiTime.Text=DataGridPaper.Items[i].Cells[11].Text.Trim()+"/<br>"+DataGridPaper.Items[i].Cells[12].Text.Trim();

				Label LBStartExam=(Label)DataGridPaper.Items[i].FindControl("LinkButStartExam");
				if (DataGridPaper.Items[i].Cells[14].Text.Trim()=="1")
				{
					LBStartExam.Attributes.Add("onclick","location.replace('StartExamAll.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"&UserID="+intUserID+"&Start=yes')");
				}
				if (DataGridPaper.Items[i].Cells[14].Text.Trim()=="2")
				{
					LBStartExam.Attributes.Add("onclick","javascript:{if (confirm('您确定要开始考试吗？')==true) {NewExam=window.open('StartExamOne.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"&UserID="+intUserID+"&Start=yes')}else{return false;}}");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["PaperInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridPaper.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridPaper.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGridPaper.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridPaper_PageIndexChanged);
			this.DataGridPaper.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridPaper_SortCommand);
			this.DataGridPaper.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPaper_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridPaper_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridPaper.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
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

		#region//*******进行编辑信息*******
		private void DataGridPaper_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridPaper_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex>0)
			{
				DataGridPaper.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex<(DataGridPaper.PageCount-1))
			{
				DataGridPaper.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=(DataGridPaper.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridPaper_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridPaper.Columns.Count; i++)
			{
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
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
