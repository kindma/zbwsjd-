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
	/// BrowNews ��ժҪ˵����
	/// </summary>
	public partial class BrowNews : System.Web.UI.Page
	{
		protected string strNewsContent="";

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intNewsID=0;
	
		#region//************��ʼ����Ϣ*********
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
			//�������
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

		#region//**********����Ҫ�޸ĵ���������*********
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
