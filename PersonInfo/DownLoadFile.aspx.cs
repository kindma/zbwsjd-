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
	/// DownLoadFile ��ժҪ˵����
	/// </summary>
	public partial class DownLoadFile : System.Web.UI.Page
	{
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserScoreID=0,intRubricID=0;

		#region//*********��ʼ��Ϣ*******
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

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
