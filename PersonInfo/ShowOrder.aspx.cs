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

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// ShowOrder 的摘要说明。
	/// </summary>
	public partial class ShowOrder : System.Web.UI.Page
	{

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

		int intPaperID=0,intOrder=0;
		string strPaperType="";
		double dblCurTotalMark=0;
	
		#region//************初始化信息*********
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
			//清除缓存
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();

			intPaperID=Convert.ToInt32(Request["PaperID"]);
			strPaperType=Convert.ToString(Request["PaperType"]);
			dblCurTotalMark=Convert.ToDouble(Request["CurTotalMark"]);
			if (!IsPostBack)
			{
				if (intPaperID!=0)
				{
					intOrder=Convert.ToInt32(ObjFun.GetValues("select count(*) as count from UserScore where PaperID="+intPaperID+" and ExamState=1 and TotalMark>"+dblCurTotalMark+"","count"))+1;
					labOrder.Text="您在本次"+strPaperType+"中排名第"+intOrder.ToString()+"名！";
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
