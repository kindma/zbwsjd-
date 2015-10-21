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

namespace EasyExam.UserManag
{
	/// <summary>
	/// EditOneUser ��ժҪ˵����
	/// </summary>
	public partial class EditOneUser : System.Web.UI.Page
	{

		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0,intCreateUserID=0;
	
		#region//************��ʼ����Ϣ*********
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
			intUserID=Convert.ToInt32(Request["UserID"]);
			txtBirthday.Attributes["readonly"]="true";
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=2)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					if (intUserID!=0)
					{
						ShowDeptInfo();//��ʾ������Ϣ
						ShowJobInfo();//��ʾְ����Ϣ
						LoadUserData();
						LoadUserPhoto();
					}
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

		#region//**********����Ҫ�޸ĵ��ʻ�����*********
		private void LoadUserData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select * from UserInfo where UserID="+intUserID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				txtLoginID.Text=ObjDR["LoginID"].ToString();
				txtUserName.Text=ObjDR["UserPwd"].ToString();
				 
				txtNewUserPwd.Text=ObjDR["UserPwd"].ToString();
				txtSureUserPwd.Text=ObjDR["UserPwd"].ToString();
				RBLUserSex.Items.FindByText(ObjDR["UserSex"].ToString()).Selected=true;
				if (ObjDR["Birthday"].ToString()!="")
				{
					txtBirthday.Text=Convert.ToDateTime(ObjDR["Birthday"].ToString()).ToString("d");
				} 
				else
				{
					txtBirthday.Text="";
				}
				DDLDept.Items.FindByValue(ObjDR["DeptID"].ToString()).Selected=true;
				DDLJob.Items.FindByValue(ObjDR["JobID"].ToString()).Selected=true;
				txtTelephone.Text=ObjDR["Telephone"].ToString();
				txtCertType.Text=ObjDR["CertType"].ToString();
				txtCertNum.Text=ObjDR["CertNum"].ToString();
				txtLoginIP.Text=ObjDR["LoginIP"].ToString();
				DDLUserType.Items.FindByValue(ObjDR["UserType"].ToString()).Selected=true;
				DDLUserState.Items.FindByValue(ObjDR["UserState"].ToString()).Selected=true;
				intCreateUserID=Convert.ToInt32(ObjDR["CreateUserID"].ToString());
				if (txtLoginID.Text.Trim().ToUpper()=="ADMIN")
				{
					txtLoginID.Enabled=false;
					DDLUserType.Enabled=false;
					DDLUserState.Enabled=false;
				}
				if (txtLoginID.Text.Trim().ToUpper()=="GUEST")
				{
					txtLoginID.Enabled=false;
					DDLUserType.Enabled=false;
				}
			}
			ObjConn.Dispose();
		}
		#endregion

		#region//**********�������ʻ���Ƭ*********
		private void LoadUserPhoto()
		{
			string strConn="";
			string strSql="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			strSql="select UserPhoto from UserInfo where UserID='"+intUserID+"' and  userphoto is not null";
			SqlCommand ObjCmd =null;
			ObjCmd=new SqlCommand (strSql, ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR=ObjCmd.ExecuteReader();
			if (ObjDR.Read())
			{
				ImageUser.ImageUrl="../PersonInfo/ShowUserImg.aspx?UserID="+intUserID+"";
				lbtDelePhoto.Visible=true;
			}  
			else
			{
				ImageUser.ImageUrl="../Images/UserImage.gif";
				lbtDelePhoto.Visible=false;
			}
			ObjConn.Close();
			ObjConn.Dispose();
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

		#region//*********�޸ĵ����ʻ���Ϣ***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (txtLoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ʺŲ���Ϊ�գ�')</script>");
				return;
			}
			if (txtNewUserPwd.Text.Trim()!=txtSureUserPwd.Text.Trim())
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�������벻һ�£�')</script>");
				return;
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
			if ((myLoginID.Trim().ToUpper()!="ADMIN")&&(DDLUserType.SelectedItem.Value=="1"))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����ʻ�ֻ�ܴ�����ͨ�ʻ���')</script>");
				return;
			}
			if (DDLUserState.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ�״̬��')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+"' and UserID<>"+intUserID+"","LoginID");
			if (strTmp.Trim()!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��"+txtLoginID.Text.Trim()+"�ʺ��Ѿ����ڣ��޷�ע�ᣡ')</script>");
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
			int intUserState=Convert.ToInt32(DDLUserState.SelectedItem.Value);
			int intJudgeUser=0;
			int intJudgeTestType=0;
			int intRoleMenu=0;
			if (intUserType==1)
			{
				intJudgeUser=1;
				intJudgeTestType=1;
				intRoleMenu=1;
			}

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

			int NumRowsAffected=MyDatabaseMethod(strLoginID,strUserName,strUserPwd,strUserSex,strBirthday,strDeptID,strJobID,strTelephone,strCertType,strCertNum,strLoginIP,intUserType,intUserState,intJudgeUser,intJudgeTestType,intRoleMenu,imgBinaryData);
			if (NumRowsAffected>0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�޸��ʻ��ɹ���');try{ window.opener.RefreshForm() }catch(e){};window.close();</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�޸��ʻ�ʧ�ܣ�')</script>");
			}
		}
		#endregion

		#region//*********�����ݸ������ݿ���*********
		public int MyDatabaseMethod(string strLoginID,string strUserName,string strUserPwd,string strUserSex,string strBirthday,string strDeptID,string strJobID,string strTelephone,string strCertType,string strCertNum,string strLoginIP,int intUserType,int intUserState,int intJudgeUser,int intJudgeTestType,int intRoleMenu,byte[] imgbin)
		{
			string strConn="";
			string strSql="";
			PublicFunction ObjFun=new PublicFunction();
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			if (imgbin.Length>0)
			{
				strSql="Update UserInfo set LoginID=@TmpLoginID,UserName=@TmpUserName,UserPwd=@TmpUserPwd,UserSex=@TmpUserSex,Birthday=@TmpBirthday,DeptID=@TmpDeptID,JobID=@TmpJobID,Telephone=@TmpTelephone,CertType=@TmpCertType,CertNum=@TmpCertNum,LoginIP=@TmpLoginIP,UserType=@TmpUserType,UserState=@TmpUserState,JudgeUser=@TmpJudgeUser,JudgeTestType=@TmpJudgeTestType,RoleMenu=@TmpRoleMenu,UserPhoto=@TmpUserPhoto where UserID="+intUserID+"";
			}
			else
			{
				strSql="Update UserInfo set LoginID=@TmpLoginID,UserName=@TmpUserName,UserPwd=@TmpUserPwd,UserSex=@TmpUserSex,Birthday=@TmpBirthday,DeptID=@TmpDeptID,JobID=@TmpJobID,Telephone=@TmpTelephone,CertType=@TmpCertType,CertNum=@TmpCertNum,LoginIP=@TmpLoginIP,UserType=@TmpUserType,UserState=@TmpUserState,JudgeUser=@TmpJudgeUser,JudgeTestType=@TmpJudgeTestType,RoleMenu=@TmpRoleMenu where UserID="+intUserID+"";
			}
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
			ParamCertType.Value = strCertType;
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

			if (imgbin.Length>0)
			{
				SqlParameter ParamUserPhoto=new SqlParameter("@TmpUserPhoto",SqlDbType.Image);   
				ParamUserPhoto.Value = imgbin;
				ObjCmd.Parameters.Add(ParamUserPhoto);
			}
			else
			{
				//SqlParameter ParamUserPhoto=new SqlParameter("@TmpUserPhoto",SqlDbType.Image);   
				//ParamUserPhoto.Value = System.DBNull.Value;
				//ObjCmd.Parameters.Add(ParamUserPhoto);
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
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+"' and UserID<>"+intUserID+"","LoginID");
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

		#region//*********ɾ���ʻ���Ƭ*********
		private void lbtDeleFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				ObjFun.ExecuteSql("Update UserInfo set UserPhoto=null where UserID="+intUserID+"");
				lbtDelePhoto.Visible=false;
				ImageUser.ImageUrl="../Images/UserImage.gif";
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('ɾ���ʻ���Ƭ�ɹ���')</script>");
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('ɾ���ʻ���Ƭʧ�ܣ�')</script>");
			}
		}
		#endregion

	}
}
