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
	/// Upload_Dialog ��ժҪ˵����
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

		protected void btnUpLoad_Click(object sender, System.EventArgs e)
		{
			string strFileName=UpLoadFile.PostedFile.FileName;
			if (strFileName.Trim()!="")
			{
				string strName=strFileName.Substring(strFileName.LastIndexOf("."));//strFileName.Substring(strFileName.Length-4);��ȡ��׺��
				string strTmp;
				if (strFileType=="pic")
				{
					strTmp=".JPG.GIF.BMP.PNG";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('ͼƬ�ļ���ʽ����ȷ��')</script>");
						return;
					}
				}
				if (strFileType=="flash")
				{
					strTmp=".SWF";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('Flash�ļ���ʽ����ȷ��')</script>");
						return;
					}
				}
				if (strFileType=="media")
				{
					strTmp=".WAV.MP3.MID.WMV.ASF.AVI.MPG";
					if (strTmp.IndexOf(strName.ToUpper())<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('ý���ļ���ʽ����ȷ��')</script>");
						return;
					}
				}
				if (strFileType=="file")
				{
					strTmp=".ASP.ASA.INC.ASPX.ASCX.BAT.EXE.COM.DLL.VBS.REG.CGI.HTACCESS.CER.CDX";
					if (strTmp.IndexOf(strName.ToUpper())>=0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����ϴ��Ƿ��ļ���')</script>");
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
