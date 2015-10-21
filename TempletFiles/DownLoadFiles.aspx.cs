using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace EasyExam.TempletFiles
{
	/// <summary>
	/// DownLoadFiles 的摘要说明。
	/// </summary>
	public partial class DownLoadFiles : System.Web.UI.Page
	{
		string myLoginID="";

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
			if (!IsPostBack)
			{
				if (Request["FileName"]!=null)
				{
					//下载文件
					FileInfo fileInfo=new FileInfo(Server.MapPath("..\\UpLoadFiles\\")+Request["FileName"].ToString());
					Response.Clear();
					Response.ClearContent();
					Response.ClearHeaders();
					Response.AddHeader("Content-Disposition", "online;filename="+Request["FileName"].ToString());//attachment 参数表示作为附件下载 online 在线打开
					Response.AddHeader("Content-Length", fileInfo.Length.ToString());
					Response.AddHeader("Content-Transfer-Encoding", "binary");
					Response.ContentType = "application/octet-stream";
					Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
					Response.WriteFile(Server.MapPath("..\\UpLoadFiles\\")+Request["FileName"].ToString());
					Response.Flush();
					Response.End();
				}
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
