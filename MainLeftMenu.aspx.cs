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
	/// MainLeftMenu ��ժҪ˵����
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
			if (intUserType==0)//��ͨ�ʻ�
			{
				 
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu1' style='DISPLAY: block;  color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' >";
				strMainMenu=strMainMenu+"<table cellspacing='0' cellpadding='0' width='100%'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/BrowNewsList.aspx'><font>�鿴֪ͨ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinExam.aspx'><font>���߿���</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinJob.aspx'><font>ģ�⿼��/��ҵ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinStudyFrame.aspx'><font>����ѧϰ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/UserInfo.aspx'><font>�ʻ���Ϣ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/PassWord.aspx'><font>�޸�����</font></a></td>";
				strMainMenu=strMainMenu+"</tr>"; 
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=1'><font>���Գɼ�</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				//strMainMenu=strMainMenu+"<tr>";
//				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=2'><font>��ҵ�ɼ�</font></a></td>";
//				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='/login.aspx'><font>�˳���¼</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
			
	 
		 
			}
			else//�����ʻ�
			{
				intRoleMenu=Convert.ToInt32(ObjFun.GetValues("select RoleMenu from UserInfo where LoginID='"+myLoginID+"'","RoleMenu"));

				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu1' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(1)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>��������</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu1' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/BrowNewsList.aspx'><font>�鿴֪ͨ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinExam.aspx'><font>�μӿ���</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+ "<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinJob.aspx'><font>�μ���ҵ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+ "<tr style='display:none;'>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/JoinStudyFrame.aspx'><font>�μ�ѧϰ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/UserInfo.aspx'><font>�ʻ���Ϣ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a  href='PersonInfo/PassWord.aspx'><font>�޸�����</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu = strMainMenu + "<tr>";
                strMainMenu = strMainMenu + "<td  ><a  href='PersonInfo/ErrBook.aspx'><font>���⿨</font></a></td>";
                strMainMenu = strMainMenu + "</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu2' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(2)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>�ɼ���ѯ</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu2' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
				strMainMenu=strMainMenu+"align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=1'><font>���Գɼ�</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td ><a  href='PersonInfo/QueryGrade.aspx?PaperType=2'><font>��ҵ�ɼ�</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";

				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=1","OptionID")!=""))//���Ź���
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu3' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(3)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>֪ͨ����</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu3' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='NewsManag/ManagNewsList.aspx'><font>֪ͨ����</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=2","OptionID")!=""))//�ʻ�����
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu4' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(4)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>�ʻ�����</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu4' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/NewMoreUser.aspx'><font>�����½�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/ImportUser.aspx'><font>�����ʻ�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='UserManag/ManagUserList.aspx'><font>�ʻ�����</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=3","OptionID")!=""))//������
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu5' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(5)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>������</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu5' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ImportTest.aspx'><font>��������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ManagTestList.aspx'><font>������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/CountTest.aspx'><font>���ͳ��</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='RubricManag/ManagBookFrame.aspx'><font>��������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=4","OptionID")!=""))//�Ծ����
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu6' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(6)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>�Ծ����</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu6' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='PaperManag/ManagExamPaper.aspx'><font>�����Ծ�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='PaperManag/ManagJobPaper.aspx'><font>��ҵ�Ծ�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=5","OptionID")!=""))//���̹���
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu7' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(7)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>���̹���</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu7' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  style='border-collapse: collapse' bordercolor='#111111'";
					strMainMenu=strMainMenu+"align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='ProcessManag/ManagProcess.aspx?PaperType=1'><font>���Թ���</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td ><a  href='ProcessManag/ManagProcess.aspx?PaperType=2'><font>��ҵ����</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
				if ((intRoleMenu==1)||(ObjFun.GetValues("select OptionID from UserPower where UserID="+intUserID+" and PowerID=3 and OptionID=6","OptionID")!=""))//�ɼ�����
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu8' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(8)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>�ɼ�����</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu8' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='GradeManag/ManagGrade.aspx?PaperType=1'><font>���Գɼ�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='GradeManag/ManagGrade.aspx?PaperType=2'><font>��ҵ�ɼ�</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}

				if (myLoginID.Trim().ToUpper()=="ADMIN")//ϵͳ����Ա
				{
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='imgmenu9' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(9)'";
					strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
					strMainMenu=strMainMenu+"height='25'>";
					strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>ϵͳ����</span></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td id='submenu9' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
					strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
					strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagDeptList.aspx'><font>��������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagJobList.aspx'><font>ְ������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagSubjectList.aspx'><font>��Ŀ����</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagTestTypeList.aspx'><font>��������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/ManagPowerList.aspx'><font>Ȩ������</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"<tr>";
					strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/SetOther.aspx'><font>�ۺ�����</font></a></td>";
					strMainMenu=strMainMenu+"</tr>";
                    //strMainMenu=strMainMenu+"<tr>";
                    //strMainMenu=strMainMenu+"<td  ><a  href='SystemSet/RegistSoft.aspx'><font>���ע��</font></a></td>";
                    //strMainMenu=strMainMenu+"</tr>";
					strMainMenu=strMainMenu+"</table>";
					strMainMenu=strMainMenu+"</div>";
					strMainMenu=strMainMenu+"</td>";
					strMainMenu=strMainMenu+"</tr>";
				}
			}

			if (intUserType==0)//��ͨ�ʻ�
			{
				 	
			}
			else//�����ʻ�
			{
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='imgmenu10' class='menu_title' onmouseover=this.className='menu_title2'; onclick='showsubmenu(10)'";
				strMainMenu=strMainMenu+"onmouseout=this.className='menu_title'; style='cursor:hand' background='images/menudown.gif'";
				strMainMenu=strMainMenu+"height='25'>";
				strMainMenu=strMainMenu+"<span style='font-weight: bold; left: 36px; color: #215dc6; position: relative; top: 2px'>ϵͳ����</span></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td id='submenu10' style='DISPLAY: none; font-size:9pt; color:#333333; line-height:150%'>";
				strMainMenu=strMainMenu+"<div class='sec_menu' style='WIDTH: 135px'>";
				strMainMenu=strMainMenu+"<table cellspacing='3' cellpadding='0'  align='center'>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a href='#' onclick={NewWin=window.open('Help/ManagHelp.htm','ManagHelp','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);}><font>���߰���</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"<tr>";
				strMainMenu=strMainMenu+"<td  ><a href='#' onclick=showModalDialog('Help/About.aspx',0,'dialogWidth:306px;dialogHeight:224px;help:no;center:yes;resizable:no;status:no;scroll:no');><font>����ϵͳ</font></a></td>";
				strMainMenu=strMainMenu+"</tr>";
				strMainMenu=strMainMenu+"</table>";
				strMainMenu=strMainMenu+"</div>";
				strMainMenu=strMainMenu+"</td>";
				strMainMenu=strMainMenu+"</tr>";
			}
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
	}
}
