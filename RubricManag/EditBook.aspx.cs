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
	/// EditBook 的摘要说明。
	/// </summary>
	public partial class EditBook : System.Web.UI.Page
	{
		protected string strSectionID;

		PublicFunction ObjFun=new PublicFunction();

		#region//************初始化信息*********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			strSectionID=Convert.ToString(Request["SectionID"]);
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

		#region//*********提交章节信息***********
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
