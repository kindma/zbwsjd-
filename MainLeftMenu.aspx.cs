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

namespace EasyExam
{
	/// <summary>
	/// MainLeftMenu 的摘要说明。
	/// </summary>
	public partial class MainLeftMenu : System.Web.UI.Page
	{
		protected string strMainMenu="";

		string myUserID="";
		string myLoginID="";
		string myUserName="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0,intUserType=0,intRoleMenu=0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				myUserName=Session["UserName"].ToString();
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("Login.aspx");
			}
			intUserID=Convert.ToInt32(myUserID);
			intUserType=Convert.ToInt32(ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"'","UserType"));
			if (intUserType==0)//普通帐户
			{
				 
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu1' style='DISPLAY: block;  color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' >";
				strMainMenu=strMainMenu+"<table cellspacing='0' cellpadding='0' width='100%'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/BrowNewsList.aspx'><font>查看通知</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinExam.aspx'><font>在线考试</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinJob.aspx'><font>模拟考试/作业</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinStudyFrame.aspx'><font>在线学习</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/UserInfo.aspx'><font>帐户信息</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/PassWord.aspx'><font>修改密码</font></a></td>";
				strMainMenu=strMainMenu+"</tr>"; 
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=1'><font>考试成绩</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				//strMainMenu=strMainMenu+"<tr>";
//				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=2'><font>作业成绩</font></a></td>";
//				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='/login.aspx'><font>退出登录</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
			
	 
		 
			}
			else//管理帐户
			{
				intRoleMenu=Convert.ToInt32(ObjFun.GetValues("select RoleMenu from UserInfo where LoginID='"+myLoginID+"'","RoleMenu"));

				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu1' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(1)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>个人事务</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu1' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/BrowNewsList.aspx'><font>查看通知</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinExam.aspx'><font>参加考试</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+ "<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinJob.aspx'><font>参加作业</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+ "<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinStudyFrame.aspx'><font>参加学习</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/UserInfo.aspx'><font>帐户信息</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/PassWord.aspx'><font>修改密码</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu = strMainMenu + "<tr>";
                strMainMenu = strMainMenu + "<td  ><a  href='PersonInfo/ErrBook.aspx'><font>错题卡</font></a></td>";
                strMainMenu = strMainMenu + "</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu2' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(2)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>成绩查询</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu2' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
				strMainMenu=strMainMenu+"align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=1'><font>考试成绩</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=2'><font>作业成绩</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";

				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=1","OptionID")!=""))//新闻管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu3' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(3)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>通知管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu3' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='NewsManag/ManagNewsList.aspx'><font>通知管理</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=2","OptionID")!=""))//帐户管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu4' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(4)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>帐户管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu4' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/NewMoreUser.aspx'><font>批量新建</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/ImportUser.aspx'><font>导入帐户</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/ManagUserList.aspx'><font>帐户管理</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=3","OptionID")!=""))//题库管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu5' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(5)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>题库管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu5' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ImportTest.aspx'><font>导入试题</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ManagTestList.aspx'><font>题库管理</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/CountTest.aspx'><font>题库统计</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ManagBookFrame.aspx'><font>在线资料</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=4","OptionID")!=""))//试卷管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu6' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(6)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>试卷管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu6' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='PaperManag/ManagExamPaper.aspx'><font>考试试卷</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='PaperManag/ManagJobPaper.aspx'><font>作业试卷</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=5","OptionID")!=""))//过程管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu7' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(7)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>过程管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu7' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='ProcessManag/ManagProcess.aspx?PaperType=1'><font>考试管理</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='ProcessManag/ManagProcess.aspx?PaperType=2'><font>作业管理</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=6","OptionID")!=""))//成绩管理
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu8' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(8)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>成绩管理</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu8' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='GradeManag/ManagGrade.aspx?PaperType=1'><font>考试成绩</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='GradeManag/ManagGrade.aspx?PaperType=2'><font>作业成绩</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}

				if (myLoginID.Trim().ToUpper()=="ADMIN")//系统管理员
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu9' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(9)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>系统设置</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu9' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagDeptList.aspx'><font>部门设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagJobList.aspx'><font>职务设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagSubjectList.aspx'><font>科目设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagTestTypeList.aspx'><font>题型设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagPowerList.aspx'><font>权限设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/SetOther.aspx'><font>综合设置</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
                    //strMainMenu=strMainMenu+"<tr>";
                    //strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/RegistSoft.aspx'><font>软件注册</font></a></td>";
                    //strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
			}

			if (intUserType==0)//普通帐户
			{
				 	
			}
			else//管理帐户
			{
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu10' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(10)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>系统帮助</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu10' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a href='#' onclick={NewWin=window.open('Help/ManagHelp.htm','ManagHelp','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);}><font>在线帮助</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a href='#' onclick=showModalDialog('Help/About.aspx',0,'dialogWidth:306px;dialogHeight:224px;help:no;center:yes;resizable:no;status:no;scroll:no');><font>关于系统</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
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
