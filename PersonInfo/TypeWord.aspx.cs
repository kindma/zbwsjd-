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
	/// TypeWord ��ժҪ˵����
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

		#region//************��ʼ����Ϣ*********
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
			//�������
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

		#region//**********����������������*********
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
