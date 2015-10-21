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
	/// Content 的摘要说明。
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
