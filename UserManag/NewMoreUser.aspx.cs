using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.OleDb;
using System.IO;


namespace EasyExam.UserManag
{
	/// <summary>
	/// NewMoreUser ��ժҪ˵����
	/// </summary>
	public partial class NewMoreUser : System.Web.UI.Page
	{
		PublicFunction ObjFun=new PublicFunction();

		string myUserID="";
		string myLoginID="";

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
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=2)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ShowDeptInfo();//��ʾ������Ϣ
					ShowJobInfo();//��ʾְ����Ϣ
				}
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
			DDLDept.Items.FindByText("--��ѡ��--").Selected=true;
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
			DDLJob.Items.FindByText("--��ѡ��--").Selected=true;

		}
		#endregion

		#region//*********�����½��ʻ���Ϣ**********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			long SLoginID=0;
			long ELoginID=0;
			string strTmp="";

			if (txtSLoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ʼ�ʺŲ���Ϊ�գ�')</script>");
				return;
			}
			if (txtELoginID.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ֹ�ʺŲ���Ϊ�գ�')</script>");
				return;
			}
			if(DDLUserType.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ����ͣ�')</script>");
				return;
			}
			if ((myLoginID.Trim().ToUpper()!="ADMIN")&&(DDLUserType.SelectedItem.Value=="1"))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����ʻ�ֻ�ܴ�����ͨ�ʻ���')</script>");
				return;
			}
			if(DDLUserState.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ʻ�״̬��')</script>");
				return;
			}
			try
			{
				SLoginID=Convert.ToInt64(txtSLoginID.Text.Trim());
				ELoginID=Convert.ToInt64(txtELoginID.Text.Trim());
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ֹ�ʺű���Ϊ�Ϸ������֣�')</script>");
				return;
			}
			if (SLoginID>ELoginID)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ʼ�ʺŴ�����ֹ�ʺţ����������룡')</script>");
				return;
			}
			for (long i=SLoginID;i<=ELoginID;i++)
			{
				strTmp=ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+i.ToString()+"'","LoginID");
				if (strTmp.Trim()!="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('"+strTmp+"�ʺ��Ѿ����ڣ�����ע�ᣡ')</script>");
					return;
				}
			}
			string strUserName="";
			string strUserPwd="";
			string strDeptID=DDLDept.SelectedItem.Value;
			string strJobID=DDLJob.SelectedItem.Value;
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
			int intCreateUserID=Convert.ToInt32(myUserID);
			DateTime dtmCreateDate=Convert.ToDateTime(System.DateTime.Now.ToString("d"));

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Connection=ObjConn;
			ObjCmd.Transaction=ObjTran;
			try
			{
				for (long i=SLoginID;i<=ELoginID;i++)
				{
					ObjCmd.CommandText="insert into UserInfo(LoginID,UserName,UserPwd,UserSex,Birthday,DeptID,JobID,CertType,CertNum,Telephone,LoginIP,UserType,UserState,JudgeUser,JudgeTestType,RoleMenu,CreateUserID,CreateDate) values('"+i.ToString()+"','"+strUserName+"','"+strUserPwd+"','��',null,'"+strDeptID+"','"+strJobID+"','','','','',"+intUserType+","+intUserState+","+intJudgeUser+","+intJudgeTestType+","+intRoleMenu+",'"+intCreateUserID.ToString()+"','"+dtmCreateDate+"')";
					ObjCmd.ExecuteNonQuery();
				}
				ObjTran.Commit();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����½��ʻ��ɹ���')</script>");
			}
			catch
			{
				ObjTran.Rollback();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����½��ʻ�ʧ�ܣ�')</script>");
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
			txtSLoginID.Text="";
			txtELoginID.Text="";

			return;

		}
		#endregion

	}
}
