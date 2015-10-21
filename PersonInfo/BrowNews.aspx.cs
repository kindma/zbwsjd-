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
	/// BrowNews 的摘要说明。
	/// </summary>
	public partial class BrowNews : System.Web.UI.Page
	{
		protected string strNewsContent="";

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intNewsID=0;
	
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

			intNewsID=Convert.ToInt32(Request["NewsID"]);
			if (!IsPostBack)
			{
				if (intNewsID!=0)
				{
					LoadNewsData();
				}
			}
		}
		#endregion

		#region//**********加载要修改的新闻数据*********
		private void LoadNewsData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlConnection SqlConn = new SqlConnection(strConn);

			SqlCommand SqlCmd=null;
			SqlConn.Open();

			SqlCmd=new SqlCommand("update NewsInfo set BrowNumber=BrowNumber+1 where NewsID="+intNewsID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Dispose();

			SqlCommand ObjCmd=new SqlCommand("select a.NewsTitle,a.NewsContent,a.BrowNumber,a.CreateDate,b.LoginID as CreateLoginID from NewsInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where NewsID="+intNewsID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				labNewsTitle.Text=ObjDR["NewsTitle"].ToString();
				labCreateLoginID.Text=ObjDR["CreateLoginID"].ToString();
				labBrowNumber.Text=ObjDR["BrowNumber"].ToString();
				labCreateDate.Text=Convert.ToDateTime(ObjDR["CreateDate"].ToString()).ToString("d");
				strNewsContent=ObjDR["NewsContent"].ToString();
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
