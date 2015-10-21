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
	/// BrowBook 的摘要说明。
	/// </summary>
	public partial class BrowBook : System.Web.UI.Page
	{
		protected string strSectionContent="";

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSectionID=0;
	
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
			//清除缓存
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();

			intSectionID=Convert.ToInt32(Request["SectionID"]);
			if (!IsPostBack)
			{
				if (intSectionID!=0)
				{
					LoadSectionData();
				}
			}
		}
		#endregion

		#region//**********加载要修改的新闻数据*********
		private void LoadSectionData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlConnection SqlConn = new SqlConnection(strConn);

			SqlCommand SqlCmd=null;
			SqlConn.Open();

			SqlCmd=new SqlCommand("update SectionInfo set BrowNumber=BrowNumber+1 where SectionID="+intSectionID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Dispose();

			SqlCommand ObjCmd=new SqlCommand("select a.SectionName,a.SectionContent,a.BrowNumber,a.CreateDate,c.LoginID as CreateLoginID from SectionInfo a LEFT OUTER JOIN ChapterInfo b ON b.ChapterID=a.ChapterID LEFT OUTER JOIN UserInfo c ON c.UserID=b.CreateUserID where SectionID="+intSectionID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				labSectionName.Text=ObjDR["SectionName"].ToString();
				labCreateLoginID.Text=ObjDR["CreateLoginID"].ToString();
				labBrowNumber.Text=ObjDR["BrowNumber"].ToString();
				labCreateDate.Text=Convert.ToDateTime(ObjDR["CreateDate"].ToString()).ToString("d");
				strSectionContent=ObjDR["SectionContent"].ToString();
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
	}
}
