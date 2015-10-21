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

namespace EasyExam
{
	/// <summary>
	/// RegistUser ��ժҪ˵����
	/// </summary>
	public partial class RegistUser : System.Web.UI.Page
	{

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserState=0;
	
		#region//************��ʼ����Ϣ*********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//�������
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();
			//�ж��Ƿ������ʻ�ע��
			if (ObjFun.GetValues("select StartValue from SystemSet where SetName='OnLineRegist'","StartValue")=="1")
			{
				intUserState=Convert.ToInt32(ObjFun.GetValues("select StartValue from SystemSet where SetName='RegistWay'","StartValue"));
			}
			else
			{
				ObjFun.Alert("ϵͳ�������ʻ�����ע�ᣡ");
			}
			txtBirthday.Attributes["readonly"]="true";
			if (!IsPostBack)
			{
				ShowDeptInfo();//��ʾ������Ϣ
				ShowJobInfo();//��ʾְ����Ϣ
				ShowUserType();//��ʾ�ʻ�����
				DDLDept.Items.FindByText("--��ѡ��--").Selected=true;
				DDLJob.Items.FindByText("--��ѡ��--").Selected=true;

                //����Զ�����룺 ��⵽�û�ͷһ�ε�¼����ת�����ע����棬ִ���Զ�ע��
                if (Request.QueryString["u"] != null)
                {
                    txtLoginID.Text = Request.QueryString["u"].Trim();
                    txtNewUserPwd.Text = Request.QueryString["n"].Trim();
                    this.RegisterStartupScript("newWindow", "<script language='javascript'>document.getElementById('ButInput').click();</script>");
                }
                else
                {
                    this.RegisterStartupScript("newWindow", "<script language='javascript'>document.getElementById('regtable').style.display='';</script>");

                }

                }
        }
		#endregion

		#region//*********��ʾ������Ϣ**********
		private void ShowDeptInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from DeptInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"DeptInfo");
			DDLDept.DataSource=SqlDS.Tables["DeptInfo"].DefaultView;
			DDLDept.DataTextField="DeptName";
			DDLDept.DataValueField="DeptID";
			DDLDept.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--��ѡ��--","0");
			DDLDept.Items.Add(strTmp);
		}
		#endregion

		#region//*********��ʾְ����Ϣ**********
		private void ShowJobInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from JobInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"JobInfo");
			DDLJob.DataSource=SqlDS.Tables["JobInfo"].DefaultView;
			DDLJob.DataTextField="JobName";
			DDLJob.DataValueField="JobID";
			DDLJob.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--��ѡ��--","0");
			DDLJob.Items.Add(strTmp);
		}
		#endregion

		#region//*********��ʾ�ʻ�����**********
		private void ShowUserType()
		{
			DDLUserType.Items.Clear();
			DDLUserType.Items.Add(new ListItem("��ͨ�ʻ�","0"));
			if (ObjFun.GetValues("select StartValue from SystemSet where SetName='RegistManag'","StartValue")=="1")
			{
				DDLUserType.Items.Add(new ListItem("�����ʻ�","1"));
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

		#region//*********�½������ʻ���Ϣ***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (txtLoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ʺŲ���Ϊ�գ�')</script>");
				return;
			}
			if (txtNewUserPwd.Text.Trim()!=txtSureUserPwd.Text.Trim())
			{
				//this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�������벻һ�£�')</script>");
				//return;
			}
			if (RBLUserSex.SelectedIndex<0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ��Ա�')</script>");
				return;
			}
			if (DDLUserType.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ����ͣ�')</script>");
				return;
			}
			//if (DDLUserState.SelectedItem.Text.Trim()=="")
			//{
			//	this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ�״̬��')</script>");
			//	return;
			//}
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+ "' AND UserPwd='"+ ObjFun.getStr(ObjFun.CheckString(txtNewUserPwd.Text.Trim()), 20) + "'", "LoginID");
			if (strTmp.Trim()!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��"+txtLoginID.Text.Trim()+"�ʺ��Ѿ����ڣ���ֱ�ӵ�¼��');location.replace='/login.aspx';</script>");
				return;
			}

			string strLoginID=ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20);
			string strUserName=ObjFun.getStr(ObjFun.CheckString(txtUserName.Text.Trim()),20);
			string strUserPwd=ObjFun.getStr(ObjFun.CheckString(txtNewUserPwd.Text.Trim()),20);
			string strUserSex=RBLUserSex.SelectedItem.Text.Trim();
			string strBirthday=txtBirthday.Text.Trim();
			string strUserImg=UpUserPhoto.PostedFile.FileName;
			string strDeptID=DDLDept.SelectedItem.Value;
			string strJobID=DDLJob.SelectedItem.Value;
			string strTelephone=ObjFun.getStr(ObjFun.CheckString(txtTelephone.Text.Trim()),20);
			string strCertType=ObjFun.getStr(ObjFun.CheckString(txtCertType.Text.Trim()),20);
			string strCertNum=ObjFun.getStr(ObjFun.CheckString(txtCertNum.Text.Trim()),20);
			string strLoginIP=ObjFun.getStr(txtLoginIP.Text.Trim(),20);
			int intUserType=Convert.ToInt32(DDLUserType.SelectedItem.Value);
			//int intUserState=Convert.ToInt32(DDLUserState.SelectedItem.Value);
			int intJudgeUser=0;
			int intJudgeTestType=0;
			int intRoleMenu=0;
			if (intUserType==1)
			{
				intJudgeUser=1;
				intJudgeTestType=1;
				intRoleMenu=1;
			}
			int intCreateUserID=Convert.ToInt32(ObjFun.GetValues("select UserID from UserInfo where LoginID='Admin'","UserID"));
			DateTime dtmCreateDate=Convert.ToDateTime(System.DateTime.Now.ToString("d"));

			byte[] imgBinaryData;
			if (strUserImg.Trim()!="")
			{
				string strName=strUserImg.Substring(strUserImg.Length-4);
				strTmp=".JPG.GIF";
				if (strTmp.IndexOf(strName.ToUpper())<0)
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��Ƭ��ʽ����ȷ��')</script>");
					return;
				}
				Stream imgStream;
				int imgLen;
				imgStream=UpUserPhoto.PostedFile.InputStream;
				imgLen=UpUserPhoto.PostedFile.ContentLength;
				imgBinaryData=new byte[imgLen];
				int n=imgStream.Read(imgBinaryData,0,imgLen);
			}
			else
			{
				imgBinaryData=new byte[0];
			}

			int NumRowsAffected=MyDatabaseMethod(strLoginID,strUserName,strUserPwd,strUserSex,strBirthday,strDeptID,strJobID,strTelephone,strCertType,strCertNum,strLoginIP,intUserType,intUserState,intJudgeUser,intJudgeTestType,intRoleMenu,intCreateUserID,dtmCreateDate,imgBinaryData);
			if (NumRowsAffected>0)
			{
				if (intUserState==0)
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ʻ�ע��ɹ����ʺ���˺���Ч��');window.close();</script>");
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ʻ�ע��ɹ���');location.replace('/login.aspx?"+Request.QueryString.ToString()+"');//window.close();</script>");
				}
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ʻ�ע��ʧ�ܣ�')</script>");
			}
		}
		#endregion

		#region//*********�����ݸ������ݿ���*********
		public int MyDatabaseMethod(string strLoginID,string strUserName,string strUserPwd,string strUserSex,string strBirthday,string strDeptID,string strJobID,string strTelephone,string strCertType,string strCertNum,string strLoginIP,int intUserType,int intUserState,int intJudgeUser,int intJudgeTestType,int intRoleMenu,int intCreateUserID,DateTime dtmCreateDate,byte[] imgbin)
		{
			string strConn="";
			string strSql="";
			PublicFunction ObjFun=new PublicFunction();
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			strSql="Insert into UserInfo(LoginID,UserName,UserPwd,UserSex,Birthday,DeptID,JobID,Telephone,CertType,CertNum,LoginIP,UserType,UserState,JudgeUser,JudgeTestType,RoleMenu,CreateUserID,CreateDate,UserPhoto) Values (@TmpLoginID,@TmpUserName,@TmpUserPwd,@TmpUserSex,@TmpBirthday,@TmpDeptID,@TmpJobID,@TmpTelephone,@TmpCertType,@TmpCertNum,@TmpLoginIP,@TmpUserType,@TmpUserState,@TmpJudgeUser,@TmpJudgeTestType,@TmpRoleMenu,@TmpCreateUserID,@TmpCreateDate,@TmpUserPhoto)";
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);
			//�ʺ�
			SqlParameter ParamLoginID=new SqlParameter("@TmpLoginID",SqlDbType.VarChar,20);
			ParamLoginID.Value = strLoginID;
			ObjCmd.Parameters.Add(ParamLoginID); 
			//����
			SqlParameter ParamUserName=new SqlParameter("@TmpUserName",SqlDbType.VarChar,20);
			ParamUserName.Value = strUserName;   
			ObjCmd.Parameters.Add(ParamUserName);   
			//����
			SqlParameter ParamUserPwd=new SqlParameter("@TmpUserPwd",SqlDbType.VarChar,20);
			ParamUserPwd.Value = strUserPwd;   
			ObjCmd.Parameters.Add(ParamUserPwd); 
			//�Ա�
			SqlParameter ParamUserSex=new SqlParameter("@TmpUserSex",SqlDbType.VarChar,2);
			ParamUserSex.Value = strUserSex;
			ObjCmd.Parameters.Add(ParamUserSex); 
			//��������
			if (strBirthday!="")
			{
				SqlParameter ParamBirthday=new SqlParameter("@TmpBirthday",SqlDbType.DateTime);   
				ParamBirthday.Value = Convert.ToDateTime(strBirthday);
				ObjCmd.Parameters.Add(ParamBirthday);
			}
			else
			{
				SqlParameter ParamBirthday=new SqlParameter("@TmpBirthday",SqlDbType.DateTime);   
				ParamBirthday.Value = System.DBNull.Value;
				ObjCmd.Parameters.Add(ParamBirthday);
			}
			//��������
			SqlParameter ParamDeptID=new SqlParameter("@TmpDeptID",SqlDbType.Int);
			ParamDeptID.Value = Convert.ToInt32(strDeptID);   
			ObjCmd.Parameters.Add(ParamDeptID);
			//ְ��
			SqlParameter ParamJobID=new SqlParameter("@TmpJobID",SqlDbType.Int);
			ParamJobID.Value = Convert.ToInt32(strJobID);
			ObjCmd.Parameters.Add(ParamJobID);  
			//�绰
			SqlParameter ParamTelephone=new SqlParameter("@TmpTelephone",SqlDbType.VarChar,20);
			ParamTelephone.Value = strTelephone;
			ObjCmd.Parameters.Add(ParamTelephone); 
			//֤������	
			SqlParameter ParamCertType=new SqlParameter("@TmpCertType",SqlDbType.VarChar,20);
			ParamCertType.Value = ObjFun.CheckString(strCertType);
			ObjCmd.Parameters.Add(ParamCertType); 
			//֤������
			SqlParameter ParamCertNum=new SqlParameter("@TmpCertNum",SqlDbType.VarChar,20);
			ParamCertNum.Value = strCertNum;
			ObjCmd.Parameters.Add(ParamCertNum);
			//��¼IP
			SqlParameter ParamLoginIP=new SqlParameter("@TmpLoginIP",SqlDbType.VarChar,20);
			ParamLoginIP.Value = strLoginIP;
			ObjCmd.Parameters.Add(ParamLoginIP); 
			//����
			SqlParameter ParamUserType=new SqlParameter("@TmpUserType",SqlDbType.Int);
			ParamUserType.Value = Convert.ToInt32(intUserType);
			ObjCmd.Parameters.Add(ParamUserType); 
			//״̬
			SqlParameter ParamUserState=new SqlParameter("@TmpUserState",SqlDbType.Int);
			ParamUserState.Value = Convert.ToInt32(intUserState);
			ObjCmd.Parameters.Add(ParamUserState); 
			//�����ʺ�
			SqlParameter ParamJudgeUser=new SqlParameter("@TmpJudgeUser",SqlDbType.Int);
			ParamJudgeUser.Value = Convert.ToInt32(intJudgeUser);
			ObjCmd.Parameters.Add(ParamJudgeUser); 
			//��������
			SqlParameter ParamJudgeTestType=new SqlParameter("@TmpJudgeTestType",SqlDbType.Int);
			ParamJudgeTestType.Value = Convert.ToInt32(intJudgeTestType);
			ObjCmd.Parameters.Add(ParamJudgeTestType); 
			//��ɫ�˵�
			SqlParameter ParamRoleMenu=new SqlParameter("@TmpRoleMenu",SqlDbType.Int);
			ParamRoleMenu.Value = Convert.ToInt32(intRoleMenu);
			ObjCmd.Parameters.Add(ParamRoleMenu); 
			//�����ʺ�
			SqlParameter ParamCreateUserID=new SqlParameter("@TmpCreateUserID",SqlDbType.Int);
			ParamCreateUserID.Value = Convert.ToInt32(intCreateUserID);
			ObjCmd.Parameters.Add(ParamCreateUserID); 
			//����ʱ��
			SqlParameter ParamCreateDate=new SqlParameter("@TmpCreateDate",SqlDbType.DateTime);
			ParamCreateDate.Value = dtmCreateDate;
			ObjCmd.Parameters.Add(ParamCreateDate);

			if (imgbin.Length>0)
			{
				SqlParameter ParamUserPhoto=new SqlParameter("@TmpUserPhoto",SqlDbType.Image);   
				ParamUserPhoto.Value = imgbin;
				ObjCmd.Parameters.Add(ParamUserPhoto);
			}
			else
			{
				SqlParameter ParamUserPhoto=new SqlParameter("@TmpUserPhoto",SqlDbType.Image);   
				ParamUserPhoto.Value = System.DBNull.Value;
				ObjCmd.Parameters.Add(ParamUserPhoto);
			}
   
			ObjConn.Open();
			int numRowsAffected=ObjCmd.ExecuteNonQuery();
			ObjConn.Close();
			ObjConn.Dispose();
			return numRowsAffected;
		}
		#endregion

		#region//*********����ʺ��Ƿ����*********
		protected void ButCheck_Click(object sender, System.EventArgs e)
		{
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+"'","LoginID");
			if (strTmp.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��"+txtLoginID.Text.Trim()+"�ʺŲ����ڣ�����ע�ᣡ')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��"+txtLoginID.Text.Trim()+"�ʺ��Ѿ����ڣ�����ע�ᣡ')</script>");
			}
		}
		#endregion

	}
}
