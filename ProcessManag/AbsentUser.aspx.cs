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

namespace EasyExam.ProcessManag
{
	/// <summary>
	/// AbsentUser 的摘要说明。
	/// </summary>
	public partial class AbsentUser : System.Web.UI.Page
	{

		PublicFunction ObjFun=new PublicFunction();
		protected int RowNum=0;

		string strSql="";
		string myLoginID="";
		int intPaperID=0;
	
		#region//*******初始化信息********
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
			//清除缓存
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			labPaperName.Text=Convert.ToString(Request["PaperName"]);

			strSql="select distinct a.UserID,a.LoginID,a.UserName,a.UserSex,convert(varchar(10),a.Birthday,2) as Birthday,b.DeptName,c.JobName,case a.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID,PaperInfo d where ((d.ExamAccount=1) or (d.ExamAccount=2 and exists(select * from PaperUser where PaperUser.PaperID=d.PaperID and PaperUser.UserID=a.UserID and PaperUser.UserType=0) or Exists(select * from UserInfo e,DeptInfo f,PaperUser g where e.UserID=a.UserID and e.DeptID=f.DeptID and f.DeptID=g.DeptID and g.PaperID=d.PaperID))) and not exists(select * from UserScore where UserScore.PaperID=d.PaperID and UserScore.UserID=a.UserID and UserScore.ExamState>=0) and d.PaperID="+intPaperID+" order by a.LoginID asc";
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=5)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intPaperID!=0)
					{
						if (DataGridUser.Attributes["SortExpression"] == null)
						{
							DataGridUser.Attributes["SortExpression"] = "LoginID";
							DataGridUser.Attributes["SortDirection"] = "ASC";
						}
						ShowData(strSql);
					}
				}
			}
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
			this.DataGridUser.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridUser_PageIndexChanged);
			this.DataGridUser.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridUser.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridUser_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridUser_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridUser.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridUser_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridUser').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridUser').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridUser.Attributes.Add("oldValue", "#F7F7F7");
				DataGridUser.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"UserInfo");
			RowNum=DataGridUser.CurrentPageIndex*DataGridUser.PageSize+1;

			string SortExpression = DataGridUser.Attributes["SortExpression"];
			string SortDirection = DataGridUser.Attributes["SortDirection"];
			SqlDS.Tables["UserInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridUser.DataSource=SqlDS.Tables["UserInfo"].DefaultView;
			DataGridUser.DataBind();
			for(int i=0;i<DataGridUser.Items.Count;i++)
			{	

			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridUser.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridUser.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridUser_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridUser.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridUser.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridUser.CurrentPageIndex>0)
			{
				DataGridUser.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridUser.CurrentPageIndex<(DataGridUser.PageCount-1))
			{
				DataGridUser.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridUser.CurrentPageIndex=(DataGridUser.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******导出帐户数据*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			strSql="select distinct a.LoginID,a.UserName,a.UserSex,convert(varchar(10),a.Birthday,2) as Birthday,b.DeptName,c.JobName,case a.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID,PaperInfo d where ((d.ExamAccount=1) or (d.ExamAccount=2 and exists(select * from PaperUser where PaperUser.PaperID=d.PaperID and PaperUser.UserID=a.UserID and PaperUser.UserType=0) or Exists(select * from UserInfo e,DeptInfo f,PaperUser g where e.UserID=a.UserID and e.DeptID=f.DeptID and f.DeptID=g.DeptID and g.PaperID=d.PaperID))) and not exists(select * from UserScore where UserScore.PaperID=d.PaperID and UserScore.UserID=a.UserID and UserScore.ExamState>=0) and d.PaperID="+intPaperID+" order by a.LoginID asc";

			//导出到Excel文件
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//准备文件
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"AbsentUser.xls",Server.MapPath("..\\UpLoadFiles\\")+"AbsentUser.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"AbsentUser.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('记录为空，不能导出！')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//下载文件
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=AbsentUser.xls");
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridUser_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridUser.Columns.Count; i++)
			{
				DataGridUser.Columns[i].HeaderText = (DataGridUser.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridUser.Columns[i].HeaderText = (DataGridUser.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridUser.Columns.Count; i++)
			{
				if (DataGridUser.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridUser.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridUser.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridUser.Attributes["SortExpression"] = SortExpression;
			DataGridUser.Attributes["SortDirection"] = SortDirection;
			if (DataGridUser.Attributes["SortDirection"] == "ASC")
			{
				DataGridUser.Columns[colindex].HeaderText = DataGridUser.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridUser.Columns[colindex].HeaderText = DataGridUser.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
