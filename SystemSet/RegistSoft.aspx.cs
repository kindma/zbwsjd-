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
using System.Management;
using System.Xml;
using System.Web.Security;

namespace EasyExam.SystemSet
{
	/// <summary>
	/// RegistSoft 的摘要说明。
	/// </summary>
	public partial class RegistSoft : System.Web.UI.Page
	{

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intRegistUser=0,intUserState=0;
		string strMAC="";
	
		#region//************初始化信息*********
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
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and LoginID='Admin'","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					LoadRegistData();
				}
			}
		}
		#endregion

		#region//**********加载要修改的数据*********
		private void LoadRegistData()
		{
			txtMachineCode.Text=FormsAuthentication.HashPasswordForStoringInConfigFile(ObjFun.GetMainBoardInfo()+ObjFun.GetCpuID()+ObjFun.GetMacAddress()+"dengjoy","SHA1").Substring(10,12).ToUpper();

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from RegistInfo where RegistName='RegistInfo'",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RegistInfo");
			if (SqlDS.Tables["RegistInfo"].Rows.Count>0)
			{
				txtRegistUnit.Text=SqlDS.Tables["RegistInfo"].Rows[0]["RegistUnit"].ToString();
				txtRegistCode.Text=SqlDS.Tables["RegistInfo"].Rows[0]["RegistCode"].ToString();
			}
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

		}
		#endregion

		#region//*********提交新闻信息***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlConn.Open();
			
			string strTmp="";
			//软件注册
			strTmp=ObjFun.GetValues("select RegistName from RegistInfo where RegistName='RegistInfo'","RegistName");
			if (strTmp=="")
			{
				SqlCmd=new SqlCommand("insert into RegistInfo(RegistName,RegistUnit,RegistCode) values('RegistInfo','"+ObjFun.getStr(txtRegistUnit.Text.Trim(),50)+"','"+ObjFun.getStr(txtRegistCode.Text.Trim(),20)+"')",SqlConn);
				SqlCmd.ExecuteNonQuery();
			}
			else
			{
				SqlCmd=new SqlCommand("update RegistInfo set RegistUnit='"+ObjFun.getStr(txtRegistUnit.Text.Trim(),50)+"',RegistCode='"+ObjFun.getStr(txtRegistCode.Text.Trim(),50)+"' where RegistName='RegistInfo'",SqlConn);
				SqlCmd.ExecuteNonQuery();
			}

			SqlConn.Close();
			SqlConn.Dispose();
			
			Response.Write("<script>alert('注册码已经生效，感谢使用！')</script>");
		}	
		#endregion

	}
}
