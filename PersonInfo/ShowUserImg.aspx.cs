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

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// ShowUserImg ��ժҪ˵����
	/// </summary>
	public partial class ShowUserImg : System.Web.UI.Page
	{
		string myLoginID="";
		int intUserID=0;

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
			if (!IsPostBack)
			{
				string strConn="";
				string strSql="";
				strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection ObjConn = new SqlConnection(strConn);
				intUserID=Convert.ToInt32(Request["UserID"]);
				if (intUserID!=0)
				{
					strSql = "select UserPhoto from UserInfo where UserID='"+intUserID+"' and  userphoto is not null";
					SqlCommand ObjCmd =null;
					ObjCmd=new SqlCommand (strSql, ObjConn);

					ObjConn.Open();
					SqlDataReader ObjDR=ObjCmd.ExecuteReader();
					if (ObjDR.Read())
					{
						Response.BinaryWrite((byte[])ObjDR["UserPhoto"]);
					} 
					ObjConn.Close();
					ObjConn.Dispose();
				}
			}
		}

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
