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

namespace EasyExam.Help
{
	/// <summary>
	/// About 的摘要说明。
	/// </summary>
	public partial class About : System.Web.UI.Page
	{
		protected string strAboutInfo="";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			strAboutInfo=strAboutInfo+"<HTML>";
			strAboutInfo=strAboutInfo+"<HEAD>";
			strAboutInfo=strAboutInfo+"<title>关于</title>";
			strAboutInfo=strAboutInfo+"<LINK href='../css/style.css' rel='stylesheet' type='text/css'>";
			strAboutInfo=strAboutInfo+"<meta http-equiv='Content-Type' content='text/html; charset=gb2312'>";
			strAboutInfo=strAboutInfo+"<link href='../css/style.css' rel='stylesheet' type='text/css'>";
			strAboutInfo=strAboutInfo+"</HEAD>";
			strAboutInfo=strAboutInfo+"<body bgcolor='#cccccc' leftmargin='0' topmargin='0' onload='softinfo.focus();' style='BACKGROUND-COLOR: #c0c0c0'>";
			strAboutInfo=strAboutInfo+"<form id='Form1' method='post'>";
			strAboutInfo=strAboutInfo+"<table id='softinfo' width='234' border='0' cellspacing='0' cellpadding='0' height='137'";
			strAboutInfo=strAboutInfo+"align='left'>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td width='530' height='11' colspan='2'>";
			//strAboutInfo=strAboutInfo+"<img border='0' src='../images/about.gif' width='300' height='51'></td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td width='17' height='20'>";
			strAboutInfo=strAboutInfo+"</td>";
			strAboutInfo=strAboutInfo+"<td width='513' height='20'>网络考试系统&nbsp; 版本：V1.0（ExamV1.0）</td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td width='17' height='20'>";
			strAboutInfo=strAboutInfo+"</td>";
			strAboutInfo=strAboutInfo+"<td width='513' height='20'>作者：孜创信息技术有限公司</a>";
			strAboutInfo=strAboutInfo+" </td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td width='17' height='20'>";
			strAboutInfo=strAboutInfo+"</td>";
			strAboutInfo=strAboutInfo+"<td width='513' height='20'>QQ：750252033  </td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td width='12' height='20'></td>";
			strAboutInfo=strAboutInfo+"<td width='225' height='20'> 版权所有 &copy;&nbsp;2015-2018</td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td colspan='2' width='532' height='20'>";
			strAboutInfo=strAboutInfo+"<hr>";
			strAboutInfo=strAboutInfo+"</td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"<tr>";
			strAboutInfo=strAboutInfo+"<td colspan='2' width='532' height='20'><div align='center'>";
			strAboutInfo=strAboutInfo+"<p align='center'>";
			strAboutInfo=strAboutInfo+"<input name='Button' type='button' class='button' value='确 定' onClick='window.close();'></p>";
			strAboutInfo=strAboutInfo+"</div>";
			strAboutInfo=strAboutInfo+"</td>";
			strAboutInfo=strAboutInfo+"</tr>";
			strAboutInfo=strAboutInfo+"</table>";
			strAboutInfo=strAboutInfo+"</form>";
			strAboutInfo=strAboutInfo+"</body>";
			strAboutInfo=strAboutInfo+"</HTML>";

			Response.Write(strAboutInfo);
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
