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

namespace EasyExam.SystemSet
{
	/// <summary>
	/// ManagPowerList 的摘要说明。
	/// </summary>
	public partial class ManagPowerList : System.Web.UI.Page
	{
		protected int RowNum=0;

		bool bWhere;
		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		bool bJoySoftware=false;

		#region//*******初始信息*********
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
			bJoySoftware=ObjFun.JoySoftware();
			if (!IsPostBack)
			{
				LoadDeptInfo();//显示部门信息
			}
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and LoginID='Admin'","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					strSql="select a.UserID,a.LoginID,a.UserName,a.UserSex,b.DeptName,c.JobName,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID = b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID = c.JobID where a.UserType=1 and LoginID<>'Admin' order by a.LoginID desc";
					if (DataGridUser.Attributes["SortExpression"] == null)
					{
						DataGridUser.Attributes["SortExpression"] = "LoginID";
						DataGridUser.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
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

		#region//******显示数据列表******
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
				LinkButton LBUser=(LinkButton)DataGridUser.Items[i].FindControl("LinkButUser");
				LinkButton LBTestType=(LinkButton)DataGridUser.Items[i].FindControl("LinkButTestType");
				LinkButton LBMenu=(LinkButton)DataGridUser.Items[i].FindControl("LinkButMenu");
//				if (bJoySoftware==false)
//				{
//					LBUser.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能设置评卷帐号！');return false;");
//					LBTestType.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能设置评卷题型！');return false;");
//					LBMenu.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能设置角色菜单！');return false;");
//				}
//				else
//				{
					LBUser.Attributes.Add("onclick", "var str=window.showModalDialog('SelectUser.aspx?UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");
					LBTestType.Attributes.Add("onclick", "var str=window.showModalDialog('SelectTestType.aspx?UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:373px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");
					LBMenu.Attributes.Add("onclick", "var str=window.showModalDialog('SelectMenu.aspx?UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:373px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");
//				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridUser.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridUser.CurrentPageIndex+1);
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
			this.DataGridUser.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridUser_PageIndexChanged);
			this.DataGridUser.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridUser.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridUser_DeleteCommand);
			this.DataGridUser.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridUser_ItemDataBound);

		}
		#endregion

		#region//*******表格分页信息*********
		private void DataGridUser_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridUser.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);		
		}
		#endregion

		#region//*******删除选定帐户*******
		private void DataGridUser_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intUserID=Convert.ToInt32(e.Item.Cells[0].Text);
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=new SqlCommand("delete from UserInfo where UserID="+ intUserID,SqlConn);
			SqlConn.Open();
			SqlCmd.ExecuteNonQuery();
			SqlConn.Close();
			SqlConn.Dispose();
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

		#region//*******查询人员信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.UserID,a.LoginID,a.UserName,a.UserSex,b.DeptName,c.JobName,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID = b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID = c.JobID where a.UserType=1 and LoginID<>'Admin'";
			//显示以部门为条件的数据
			if (DDLDept.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.DeptID=b.DeptID and b.DeptID='"+DDLDept.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.DeptID=b.DeptID and b.DeptID='"+DDLDept.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以帐号为条件的数据
			if (txtLoginID.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
				}
				else
				{
					strSql=strSql+" where a.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
					bWhere=true;
				}
			}
			//显示以姓名为条件的数据
			if (txtUserName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.UserName like '%"+ObjFun.CheckString(txtUserName.Text)+"%'";
				}
				else
				{
					strSql=strSql+" where a.UserName like '%"+ObjFun.CheckString(txtUserName.Text)+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.LoginID desc";

			DataGridUser.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
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
