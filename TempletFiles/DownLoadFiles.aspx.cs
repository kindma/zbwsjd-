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
	/// DownLoadFiles ��ժҪ˵����
	/// </summary>
	public partial class DownLoadFiles : System.Web.UI.Page
	{
		string myLoginID="";

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
			if (!IsPostBack)
			{
				if (Request["FileName"]!=null)
				{
					//�����ļ�
					FileInfo fileInfo=new FileInfo(Server.MapPath("..\\UpLoadFiles\\")+Request["FileName"].ToString());
					Response.Clear();
					Response.ClearContent();
					Response.ClearHeaders();
					Response.AddHeader("Content-Disposition", "online;filename="+Request["FileName"].ToString());//attachment ������ʾ��Ϊ�������� online ���ߴ�
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
