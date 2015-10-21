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

namespace EasyExam.PersonalInfo
{
	/// <summary>
	/// DownLoadFile 的摘要说明。
	/// </summary>
	public partial class DownLoadFile : System.Web.UI.Page
	{
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserScoreID=0,intRubricID=0;

		#region//*********初始信息*******
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
			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			intRubricID=Convert.ToInt32(Request["RubricID"]);

			if (!IsPostBack)
			{
				string strConn="";
				strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection ObjConn = new SqlConnection(strConn);
				SqlCommand ObjCmd=new SqlCommand("select * from UserAnswer where UserScoreID="+intUserScoreID+" and RubricID="+intRubricID+"",ObjConn);
				ObjConn.Open();
				SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
				if (ObjDR.Read())
				{
					Response.ContentType="application/octet-stream";
					Response.AddHeader("Content-Disposition", "attachment;FileName="+ObjDR["TestFileName"].ToString());
					Response.BinaryWrite((byte[])ObjDR["TestFile"]);
					Response.End();
				}
				ObjConn.Dispose();
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
		}
		#endregion
	}
}
