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

namespace EasyExam.RubricManag
{
	/// <summary>
	/// EditBook ��ժҪ˵����
	/// </summary>
	public partial class EditBook : System.Web.UI.Page
	{
		protected string strSectionID;

		PublicFunction ObjFun=new PublicFunction();

		#region//************��ʼ����Ϣ*********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			strSectionID=Convert.ToString(Request["SectionID"]);
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

		#region//*********�ύ�½���Ϣ***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlConn.Open();
			SqlCommand SqlCmd=new SqlCommand("update SectionInfo set SectionContent='"+ObjFun.getStr(ObjFun.CheckTestStr(Request["sectioncontent"].ToString()),80000)+"' where SectionID="+strSectionID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();
			SqlConn.Close();
			SqlConn.Dispose();

			this.RegisterStartupScript("newWindow","<script language='javascript'>window.close();</script>");
		}
		#endregion
	}
}
