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

namespace EasyExam.Editor.Dialog
{
	/// <summary>
	/// Upload_Dialog 的摘要说明。
	/// </summary>
	public partial class Upload_Dialog : System.Web.UI.Page
	{
		protected string strFileType="";

		string myLoginID="";
	
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
			strFileType=Convert.ToString(Request["DialogType"]);
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

		protected void btnUpLoad_Click(object sender, System.EventArgs e)
		{
			string strFileName=UpLoadFile.PostedFile.FileName;
			if (strFileName.Trim()!="")
			{
				string strName=strFileName.Substring(strFileName.LastIndexOf("."));//strFileName.Substring(strFileName.Length-4);获取后缀名
				string strTmp;
				if (strFileType=="pic")
				{
					strTmp=".JPG.GIF.BMP.PNG";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('图片文件格式不正确！')</script>");
						return;
					}
				}
				if (strFileType=="flash")
				{
					strTmp=".SWF";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('Flash文件格式不正确！')</script>");
						return;
					}
				}
				if (strFileType=="media")
				{
					strTmp=".WAV.MP3.MID.WMV.ASF.AVI.MPG";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('媒体文件格式不正确！')</script>");
						return;
					}
				}
				if (strFileType=="file")
				{
					strTmp=".ASP.ASA.INC.ASPX.ASCX.BAT.EXE.COM.DLL.VBS.REG.CGI.HTACCESS.CER.CDX";
					if (strTmp.IndexOf(strName.ToUpper())>=0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('不能上传非法文件！')</script>");
						return;
					}
				}
				if ((strFileType=="pic")||(strFileType=="flash")||(strFileType=="media")||(strFileType=="file"))
				{
					strFileName=UpLoadFile.PostedFile.FileName.Substring(UpLoadFile.PostedFile.FileName.LastIndexOf("\\")+1);
					UpLoadFile.PostedFile.SaveAs(Server.MapPath("~\\UpLoadFiles\\"+strFileName));
					this.RegisterStartupScript("newWindow","<script language='javascript'>parent.document.all('url').value='"+"..\\\\UpLoadFiles\\\\"+strFileName+"';parent.document.all('UpFileName').value='"+strFileName+"';</script>");
				}
			}
		}
	}
}
