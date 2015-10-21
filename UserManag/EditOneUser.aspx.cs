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
	/// EditOneUser 的摘要说明。
	/// </summary>
	public partial class EditOneUser : System.Web.UI.Page
	{

		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0,intCreateUserID=0;
	
		#region//************初始化信息*********
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
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intUserID!=0)
					{
						ShowDeptInfo();//显示部门信息
						ShowJobInfo();//显示职务信息
						LoadUserData();
						LoadUserPhoto();
					}
				}
			}
		}
		#endregion
		
		#region//*********显示部门信息**********
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
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLDept.Items.Add(strTmp);
		}
		#endregion

		#region//*********显示职务信息**********
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
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLJob.Items.Add(strTmp);

		}
		#endregion

		#region//**********加载要修改的帐户数据*********
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

		#region//**********加载帐帐户照片*********
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

		#region//*********修改单个帐户信息***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (txtLoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('帐号不能为空！')</script>");
				return;
			}
			if (txtNewUserPwd.Text.Trim()!=txtSureUserPwd.Text.Trim())
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('两次密码不一致！')</script>");
				return;
			}
			if (RBLUserSex.SelectedIndex<0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择帐户性别！')</script>");
				return;
			}
			if (DDLUserType.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择帐户类型！')</script>");
				return;
			}
			if ((myLoginID.Trim().ToUpper()!="ADMIN")&&(DDLUserType.SelectedItem.Value=="1"))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('管理帐户只能创建普通帐户！')</script>");
				return;
			}
			if (DDLUserState.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择帐户状态！')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+"' and UserID<>"+intUserID+"","LoginID");
			if (strTmp.Trim()!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此"+txtLoginID.Text.Trim()+"帐号已经存在，无法注册！')</script>");
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
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('照片格式不正确！')</script>");
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
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改帐户成功！');try{ window.opener.RefreshForm() }catch(e){};window.close();</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改帐户失败！')</script>");
			}
		}
		#endregion

		#region//*********将数据更到数据库中*********
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
			//帐号
			SqlParameter ParamLoginID=new SqlParameter("@TmpLoginID",SqlDbType.VarChar,20);
			ParamLoginID.Value = strLoginID;
			ObjCmd.Parameters.Add(ParamLoginID); 
			//姓名
			SqlParameter ParamUserName=new SqlParameter("@TmpUserName",SqlDbType.VarChar,20);
			ParamUserName.Value = strUserName;
			ObjCmd.Parameters.Add(ParamUserName);   
			//密码
			SqlParameter ParamUserPwd=new SqlParameter("@TmpUserPwd",SqlDbType.VarChar,20);
			ParamUserPwd.Value = strUserPwd;
			ObjCmd.Parameters.Add(ParamUserPwd); 
			//性别
			SqlParameter ParamUserSex=new SqlParameter("@TmpUserSex",SqlDbType.VarChar,2);
			ParamUserSex.Value = strUserSex;
			ObjCmd.Parameters.Add(ParamUserSex); 
			//出生年月
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
			//所属部门
			SqlParameter ParamDeptID=new SqlParameter("@TmpDeptID",SqlDbType.Int);
			ParamDeptID.Value = Convert.ToInt32(strDeptID);   
			ObjCmd.Parameters.Add(ParamDeptID);
			//职务
			SqlParameter ParamJobID=new SqlParameter("@TmpJobID",SqlDbType.Int);
			ParamJobID.Value = Convert.ToInt32(strJobID);
			ObjCmd.Parameters.Add(ParamJobID);  
			//电话
			SqlParameter ParamTelephone=new SqlParameter("@TmpTelephone",SqlDbType.VarChar,20);
			ParamTelephone.Value = strTelephone;
			ObjCmd.Parameters.Add(ParamTelephone); 
			//证件类型	
			SqlParameter ParamCertType=new SqlParameter("@TmpCertType",SqlDbType.VarChar,20);
			ParamCertType.Value = strCertType;
			ObjCmd.Parameters.Add(ParamCertType); 
			//证件号码
			SqlParameter ParamCertNum=new SqlParameter("@TmpCertNum",SqlDbType.VarChar,20);
			ParamCertNum.Value = strCertNum;
			ObjCmd.Parameters.Add(ParamCertNum);
			//登录IP
			SqlParameter ParamLoginIP=new SqlParameter("@TmpLoginIP",SqlDbType.VarChar,20);
			ParamLoginIP.Value = strLoginIP;
			ObjCmd.Parameters.Add(ParamLoginIP); 
			//类型
			SqlParameter ParamUserType=new SqlParameter("@TmpUserType",SqlDbType.Int);
			ParamUserType.Value = Convert.ToInt32(intUserType);
			ObjCmd.Parameters.Add(ParamUserType); 
			//状态
			SqlParameter ParamUserState=new SqlParameter("@TmpUserState",SqlDbType.Int);
			ParamUserState.Value = Convert.ToInt32(intUserState);
			ObjCmd.Parameters.Add(ParamUserState); 
			//评卷帐号
			SqlParameter ParamJudgeUser=new SqlParameter("@TmpJudgeUser",SqlDbType.Int);
			ParamJudgeUser.Value = Convert.ToInt32(intJudgeUser);
			ObjCmd.Parameters.Add(ParamJudgeUser); 
			//评卷题型
			SqlParameter ParamJudgeTestType=new SqlParameter("@TmpJudgeTestType",SqlDbType.Int);
			ParamJudgeTestType.Value = Convert.ToInt32(intJudgeTestType);
			ObjCmd.Parameters.Add(ParamJudgeTestType); 
			//角色菜单
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

		#region//*********检测帐号是否存在*********
		protected void ButCheck_Click(object sender, System.EventArgs e)
		{
			string strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.getStr(ObjFun.CheckString(txtLoginID.Text.Trim()),20)+"' and UserID<>"+intUserID+"","LoginID");
			if (strTmp.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此"+txtLoginID.Text.Trim()+"帐号不存在，可以注册！')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此"+txtLoginID.Text.Trim()+"帐号已经存在，不能注册！')</script>");
			}
		}
		#endregion

		#region//*********删除帐户照片*********
		private void lbtDeleFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				ObjFun.ExecuteSql("Update UserInfo set UserPhoto=null where UserID="+intUserID+"");
				lbtDelePhoto.Visible=false;
				ImageUser.ImageUrl="../Images/UserImage.gif";
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('删除帐户照片成功！')</script>");
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('删除帐户照片失败！')</script>");
			}
		}
		#endregion

	}
}
