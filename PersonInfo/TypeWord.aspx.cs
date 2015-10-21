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
	/// TypeWord 的摘要说明。
	/// </summary>
	public partial class TypeWord : System.Web.UI.Page
	{
		protected int intTestNum=0,intTypeTime=0;
		protected string strTestContent="";

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserScoreID=0,intRubricID=0;
		string[] strArrTypeStandardAnswer;

		#region//************初始化信息*********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				intTestNum=Convert.ToInt32(Request["TestNum"]);
				intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
				intRubricID=Convert.ToInt32(Request["RubricID"]);
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

			if (!IsPostBack)
			{
				if (intTestNum!=0)
				{
					LoadTestData();
				}
			}
		}
		#endregion

		#region//**********加载试题内容数据*********
		private void LoadTestData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select TestContent,StandardAnswer from RubricInfo where RubricID="+intRubricID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				strArrTypeStandardAnswer=ObjDR["StandardAnswer"].ToString().Split(',');
				intTypeTime=Convert.ToInt32(strArrTypeStandardAnswer[0]);
				strTestContent=ObjDR["TestContent"].ToString();
				strTestContent=strTestContent.Replace("<BR>","<br>");
				strTestContent=strTestContent.Replace("<Br>","<br>");
				strTestContent=strTestContent.Replace("<bR>","<br>");
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
