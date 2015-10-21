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

namespace EasyExam.Editor
{
	/// <summary>
	/// Content ��ժҪ˵����
	/// </summary>
	public partial class Content : System.Web.UI.Page
	{
		int intSectionID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			intSectionID=Convert.ToInt32(Request["SectionID"]);

			if (intSectionID!=0)
			{
				string strConn="";
				strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection ObjConn = new SqlConnection(strConn);
				SqlCommand ObjCmd=new SqlCommand("select * from SectionInfo where SectionID="+intSectionID+"",ObjConn);
				ObjConn.Open();
				SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
				if (ObjDR.Read())
				{
					Response.Write(ObjDR["SectionContent"].ToString());
				}
				ObjConn.Dispose();
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
