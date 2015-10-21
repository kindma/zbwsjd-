using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// PassWord 的摘要说明。
	/// </summary>
	public partial class PassWord : System.Web.UI.Page
	{

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
	
		#region//************初始化信息*********
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
			if (!IsPostBack)
			{
				if (intUserID!=0)
				{
					LoadUserData();
				}
			}
		}
		#endregion
		
		#region//**********加载要修改的帐户数据*********
		private void LoadUserData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select a.UserID,a.LoginID,a.UserName,a.UserSex,a.Birthday,b.DeptName,a.Telephone,a.CertType,a.CertNum,a.LoginIP,c.JobName,case a.UserType when 1 then '管理帐户' when 0 then '普通帐户' end as UserType,case a.UserState when 1 then '正常' when 0 then '禁止' end as UserState from UserInfo a LEFT OUTER JOIN DeptInfo b ON a.DeptID = b.DeptID LEFT OUTER JOIN JobInfo c ON a.JobID = c.JobID where UserID="+intUserID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				txtLoginID.Text=ObjDR["LoginID"].ToString();
				txtOldPwd.Text="";
				txtNewPwd.Text="";
				txtSureNewPwd.Text="";
			}
			ObjConn.Dispose();
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

		}
		#endregion

		#region//*********修改单个帐户信息***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			string strTmp=ObjFun.GetValues("select UserPwd from UserInfo where LoginID='"+ObjFun.CheckString(txtLoginID.Text)+"'","UserPwd");
			if (txtOldPwd.Text.Trim()!=strTmp)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('旧密码不正确！')</script>");
				return;
			}
			else
			{
				if (txtNewPwd.Text.Trim()!=txtSureNewPwd.Text.Trim())
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('两次新密码不一致！')</script>");
					return;
				}
				else
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand SqlCmd=new SqlCommand("update UserInfo set UserPwd='"+ObjFun.CheckString(txtNewPwd.Text.Trim())+"' where LoginID='"+ObjFun.CheckString(txtLoginID.Text)+"'",SqlConn);
					SqlConn.Open();
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
					SqlConn.Dispose();
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('密码修改成功！')</script>");
				}
			}
		}
		#endregion

	}
}
