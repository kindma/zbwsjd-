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
using System.Data.OleDb;
using System.IO;


namespace EasyExam.UserManag
{
	/// <summary>
	/// NewMoreUser 的摘要说明。
	/// </summary>
	public partial class NewMoreUser : System.Web.UI.Page
	{
		PublicFunction ObjFun=new PublicFunction();

		string myUserID="";
		string myLoginID="";

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
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=2)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ShowDeptInfo();//显示部门信息
					ShowJobInfo();//显示职务信息
				}
			}
		}

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

		}
		#endregion

		#region//*********显示部门信息**********
		private void ShowDeptInfo()
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
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLDept.Items.Add(strTmp);
			DDLDept.Items.FindByText("--请选择--").Selected=true;
		}
		#endregion

		#region//*********显示职务信息**********
		private void ShowJobInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from JobInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"JobInfo");
			DDLJob.DataSource=SqlDS.Tables["JobInfo"].DefaultView;
			DDLJob.DataTextField="JobName";
			DDLJob.DataValueField="JobID";
			DDLJob.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLJob.Items.Add(strTmp);
			DDLJob.Items.FindByText("--请选择--").Selected=true;

		}
		#endregion

		#region//*********批量新建帐户信息**********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			long SLoginID=0;
			long ELoginID=0;
			string strTmp="";

			if (txtSLoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('起始帐号不能为空！')</script>");
				return;
			}
			if (txtELoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('终止帐号不能为空！')</script>");
				return;
			}
			if(DDLUserType.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择帐户类型！')</script>");
				return;
			}
			if ((myLoginID.Trim().ToUpper()!="ADMIN")&&(DDLUserType.SelectedItem.Value=="1"))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('管理帐户只能创建普通帐户！')</script>");
				return;
			}
			if(DDLUserState.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择帐户状态！')</script>");
				return;
			}
			try
			{
				SLoginID=Convert.ToInt64(txtSLoginID.Text.Trim());
				ELoginID=Convert.ToInt64(txtELoginID.Text.Trim());
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('起止帐号必须为合法的数字！')</script>");
				return;
			}
			if (SLoginID>ELoginID)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('起始帐号大于终止帐号，请重新输入！')</script>");
				return;
			}
			for (long i=SLoginID;i<=ELoginID;i++)
			{
				strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+i.ToString()+"'","LoginID");
				if (strTmp.Trim()!="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('"+strTmp+"帐号已经存在，不能注册！')</script>");
					return;
				}
			}
			string strUserName="";
			string strUserPwd="";
			string strDeptID=DDLDept.SelectedItem.Value;
			string strJobID=DDLJob.SelectedItem.Value;
			int intUserType=Convert.ToInt32(DDLUserType.SelectedItem.Value);
			int intUserState=Convert.ToInt32(DDLUserState.SelectedItem.Value);
			int intJudgeUser=0;
			int intJudgeTestType=0;
			int intRoleMenu=0;
			if (intUserType==1)
			{
				intJudgeUser=1;
				intJudgeTestType=1;
				intRoleMenu=1;
			}
			int intCreateUserID=Convert.ToInt32(myUserID);
			DateTime dtmCreateDate=Convert.ToDateTime(System.DateTime.Now.ToString("d"));

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Connection=ObjConn;
			ObjCmd.Transaction=ObjTran;
			try
			{
				for (long i=SLoginID;i<=ELoginID;i++)
				{
					ObjCmd.CommandText="insert into UserInfo(LoginID,UserName,UserPwd,UserSex,Birthday,DeptID,JobID,CertType,CertNum,Telephone,LoginIP,UserType,UserState,JudgeUser,JudgeTestType,RoleMenu,CreateUserID,CreateDate) values('"+i.ToString()+"','"+strUserName+"','"+strUserPwd+"','男',null,'"+strDeptID+"','"+strJobID+"','','','','',"+intUserType+","+intUserState+","+intJudgeUser+","+intJudgeTestType+","+intRoleMenu+",'"+intCreateUserID.ToString()+"','"+dtmCreateDate+"')";
					ObjCmd.ExecuteNonQuery();
				}
				ObjTran.Commit();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('批量新建帐户成功！')</script>");
			}
			catch
			{
				ObjTran.Rollback();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('批量新建帐户失败！')</script>");
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
			txtSLoginID.Text="";
			txtELoginID.Text="";

			return;

		}
		#endregion

	}
}
