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
	/// StatisGrade 的摘要说明。
	/// </summary>
	public partial class StatisGrade : System.Web.UI.Page
	{
		protected int RowNum=0;

		bool bWhere;
		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
		double dblTotalMark=0;
		bool bJoySoftware=false;
	
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
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
//			bJoySoftware=ObjFun.JoySoftware();
//			if (bJoySoftware==false)
//			{
//				ObjFun.Alert("对不起，未注册用户不能进行试卷统计！");
//			}
			if (!IsPostBack)
			{
				LoadDeptInfo();//显示部门信息
			}
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=6)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intPaperID!=0)
					{
						strSql="select a.UserScoreID,b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case b.UserState when 1 then '正常' when 0 then '禁止' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1 order by a.TotalMark desc";
						if (DataGridGrade.Attributes["SortExpression"] == null)
						{
							DataGridGrade.Attributes["SortExpression"] = "TotalMark";
							DataGridGrade.Attributes["SortDirection"] = "DESC";
						}
						ShowData(strSql);
						LabCondition.Text=strSql;
					}
				}
			}
		}
		#endregion

		#region//******显示部门信息******
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
			ListItem objTmp=new ListItem("--全部--","0");
			DDLDept.Items.Add(objTmp);
			DDLDept.Items.FindByText("--全部--").Selected=true;
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
			this.DataGridGrade.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridGrade_PageIndexChanged);
			this.DataGridGrade.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridGrade_SortCommand);
			this.DataGridGrade.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridGrade_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridGrade_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridGrade.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
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

		#region//*******显示数据列表*******
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
			for(int i=0;i<DataGridGrade.Items.Count;i++)
			{	

			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserScore"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridGrade.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridGrade.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridGrade_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridGrade.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{		
			bWhere=true;
			strSql="select a.UserScoreID,b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case b.UserState when 1 then '正常' when 0 then '禁止' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1";
			//显示以部门为条件的数据
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
			//显示以帐号为条件的数据
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
			//显示以姓名为条件的数据
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
			//显示以成绩为条件的数据
			if ((DDLCondition.SelectedItem.Value!="-1")&&(txtTotalMark.Text.Trim()!=""))
			{
				try
				{
					dblTotalMark=Convert.ToDouble(txtTotalMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的分数！')</script>");
					return;
				}
				if (bWhere)
				{
					strSql=strSql+" and round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
				}
				else
				{
					strSql=strSql+" where round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.TotalMark desc";

			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex>0)
			{
				DataGridGrade.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex<(DataGridGrade.PageCount-1))
			{
				DataGridGrade.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=(DataGridGrade.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******导出统计数据*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case b.UserState when 1 then '正常' when 0 then '禁止' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1";
			//显示以部门为条件的数据
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
			//显示以帐号为条件的数据
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
			//显示以姓名为条件的数据
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
			//显示以成绩为条件的数据
			if ((DDLCondition.SelectedItem.Value!="-1")&&(txtTotalMark.Text.Trim()!=""))
			{
				try
				{
					dblTotalMark=Convert.ToDouble(txtTotalMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的分数！')</script>");
					return;
				}
				if (bWhere)
				{
					strSql=strSql+" and round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
				}
				else
				{
					strSql=strSql+" where round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.TotalMark desc";

			//导出到Excel文件
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//准备文件
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"StatisGrade.xls",Server.MapPath("..\\UpLoadFiles\\")+"StatisGrade.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"StatisGrade.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('记录为空，不能导出！')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//下载文件
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=StatisGrade.xls");
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridGrade_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridGrade.Columns.Count; i++)
			{
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
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
