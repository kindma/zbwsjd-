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


namespace EasyExam.UserManag
{
	/// <summary>
	/// ManagUserList 的摘要说明。
	/// </summary>
	public partial class ManagUserList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

		#region//*******初始信息*********
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
			if (!IsPostBack)
			{
				LoadDeptInfo();//显示部门信息
			}
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=2)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ButNewOneUser.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('NewOneUser.aspx','NewOneUser',550,445); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择帐户吗？')==false) return false;}");
					ButDelAnswer.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择帐户答卷吗？')==false) return false;}");
					
					strSql="select a.UserID,a.LoginID,a.UserName,a.UserSex,b.DeptName,c.JobName,case a.UserType when 0 then '普通帐户' when 1 then '管理帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState,d.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID LEFT OUTER JOIN UserInfo d ON a.CreateUserID=d.UserID order by a.UserID desc";
					if (DataGridUser.Attributes["SortExpression"] == null)
					{
						DataGridUser.Attributes["SortExpression"] = "UserID";
						DataGridUser.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//显示数据
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
			LinNum=0;

			string SortExpression = DataGridUser.Attributes["SortExpression"];
			string SortDirection = DataGridUser.Attributes["SortDirection"];
			SqlDS.Tables["UserInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridUser.DataSource=SqlDS.Tables["UserInfo"].DefaultView;
			DataGridUser.DataBind();
			for(int i=0;i<DataGridUser.Items.Count;i++)
			{				
				LinkButton LBEditUser=(LinkButton)DataGridUser.Items[i].FindControl("LinkButEditUser");
				LinkButton LBDel=(LinkButton)DataGridUser.Items[i].FindControl("LinkButDel");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
				{
					LBEditUser.Attributes.Add("onclick", "javascript:jscomNewOpenByFixSize('EditOneUser.aspx?UserID="+DataGridUser.Items[i].Cells[0].Text+"','EditOneUser',550,445); return false;");
					if ((DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()=="ADMIN")||(DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()=="GUEST"))
					{
						LBDel.Attributes.Add("onclick", "javascript:alert('系统内置帐号，不能删除！');return false;");
					}
					else
					{
						LBDel.Attributes.Add("onclick", "javascript:{if(confirm('确定要删除选择帐户吗？')==false) return false;}");
					}
				}
				else
				{
					LBEditUser.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
				}
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
			SqlConn.Open();
			SqlCommand SqlCmd=null;
			if ((e.Item.Cells[3].Text.Trim().ToUpper()!="ADMIN")&&(e.Item.Cells[3].Text.Trim().ToUpper()!="GUEST"))
			{
				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==e.Item.Cells[10].Text.Trim().ToUpper()))
				{
					//数据表NewsUser
					SqlCmd=new SqlCommand("delete from NewsUser where UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					//数据表PaperUser
					SqlCmd=new SqlCommand("delete from PaperUser where UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					//数据表UserAnswer
					SqlCmd=new SqlCommand("delete UserAnswer from UserAnswer a LEFT OUTER JOIN UserScore b ON a.UserScoreID=b.UserScoreID where b.UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					//数据表UserScore
					SqlCmd=new SqlCommand("delete from UserScore where UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					//数据表UserPower
					SqlCmd=new SqlCommand("delete from UserPower where UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					SqlCmd=new SqlCommand("delete from UserPower where PowerID=1 and OptionID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
					//数据表UserInfo
					SqlCmd=new SqlCommand("delete from UserInfo where UserID="+intUserID+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
				}
			}
			SqlConn.Close();
			SqlConn.Dispose();

			//自动翻页
			if (DataGridUser.Items.Count==1&&DataGridUser.CurrentPageIndex>0)
			{
				DataGridUser.CurrentPageIndex--;
			}        

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

		#region//*******删除选择帐户*******
		protected void butDelete_Click(object sender, System.EventArgs e)
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

					if ((DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()!="ADMIN")&&(DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()!="GUEST"))
					{
						if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
						{
							//数据表NewsUser
							SqlCmd=new SqlCommand("delete from NewsUser where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							//数据表PaperUser
							SqlCmd=new SqlCommand("delete from PaperUser where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							//数据表UserAnswer
							SqlCmd=new SqlCommand("delete UserAnswer from UserAnswer a LEFT OUTER JOIN UserScore b ON a.UserScoreID=b.UserScoreID where b.UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							//数据表UserScore
							SqlCmd=new SqlCommand("delete from UserScore where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							//数据表UserPower
							SqlCmd=new SqlCommand("delete from UserPower where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							SqlCmd=new SqlCommand("delete from UserPower where PowerID=1 and OptionID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
							//数据表UserInfo
							SqlCmd=new SqlCommand("delete from UserInfo where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
							SqlCmd.ExecuteNonQuery();
						}
					}
				}

				//自动翻页
				if(textArray.Length==DataGridUser.Items.Count&&DataGridUser.CurrentPageIndex>0)
				{
					DataGridUser.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******查询人员信息*******
		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=false;
			strSql="select a.UserID,a.LoginID,a.UserName,a.UserSex,b.DeptName,c.JobName,case a.UserType when 0 then '普通帐户' when 1 then '管理帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState,d.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID LEFT OUTER JOIN UserInfo d ON a.CreateUserID=d.UserID";
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
			strSql=strSql+" order by a.UserID desc";

			DataGridUser.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******禁用选择帐户*******
		protected void butLock_Click(object sender, System.EventArgs e)
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

					if ((DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()!="ADMIN")&&(DataGridUser.Items[i].Cells[3].Text.Trim().ToUpper()!="GUEST"))
					{
						if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
						{
							SqlCmd=new SqlCommand("update UserInfo set UserState=0 where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim(),SqlConn);
							SqlCmd.ExecuteNonQuery();
						}
					}
				}
				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******启用选择帐户*******
		protected void ButUnLock_Click(object sender, System.EventArgs e)
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

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
					{
						SqlCmd=new SqlCommand("update UserInfo set UserState=1 where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}
				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******帐户密码置空*******
		protected void ButClearPwd_Click(object sender, System.EventArgs e)
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

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
					{
						SqlCmd=new SqlCommand("update UserInfo set UserPwd='' where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}
				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******删除答卷*******
		protected void ButDelAnswer_Click(object sender, System.EventArgs e)
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

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridUser.Items[i].Cells[10].Text.Trim().ToUpper()))
					{
						//数据表UserAnswer
						SqlCmd=new SqlCommand("delete UserAnswer from UserAnswer a LEFT OUTER JOIN UserScore b ON a.UserScoreID=b.UserScoreID where b.UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//数据表UserScore
						SqlCmd=new SqlCommand("delete from UserScore where UserID="+DataGridUser.Items[i].Cells[0].Text.Trim()+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}
				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
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
			if (myLoginID.Trim().ToUpper()=="ADMIN")
			{
				bWhere=false;
				strSql="select a.LoginID,a.UserName,a.UserPwd,a.UserSex,convert(varchar(10),a.Birthday,2) as Birthday,b.DeptName,c.JobName,a.Telephone,a.CertType,a.CertNum,a.LoginIP,case a.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID";
			}
			else
			{
				bWhere=true;
				strSql="select a.LoginID,a.UserName,a.UserPwd,a.UserSex,convert(varchar(10),a.Birthday,2) as Birthday,b.DeptName,c.JobName,a.Telephone,a.CertType,a.CertNum,a.LoginIP,case a.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID=b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID=c.JobID where a.CreateUserID="+Convert.ToInt32(myUserID)+"";
			}
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
			strSql=strSql+" order by a.UserID desc";

			//导出到Excel文件
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//准备文件
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"ExportUser.xls",Server.MapPath("..\\UpLoadFiles\\")+"ExportUser.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"ExportUser.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('记录为空，不能导出！')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//下载文件
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=ExportUser.xls");
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
