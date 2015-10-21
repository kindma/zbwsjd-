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

namespace EasyExam.SystemSet
{
	/// <summary>
	/// NewMoreDept ��ժҪ˵����
	/// </summary>
	public partial class NewMoreDept : System.Web.UI.Page
	{

		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

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
			if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and LoginID='Admin'","UserType")!="1")
			{
				Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
				Response.End();
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

		#region//*******�������Ӳ���*********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if(txtDeptName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�������Ʋ���Ϊ�գ�')</script>");
				return;
			}
			string strTmpDept=txtDeptName.Text;

			string[] strArrDept= strTmpDept.Split(',');

			for(long i=0;i<strArrDept.Length;i++)
			{
				if (strArrDept[i].Trim()!="")
				{
					string strTmp=ObjFun.GetValues("select DeptName from DeptInfo where DeptName='"+ObjFun.getStr(ObjFun.CheckString(strArrDept[i].Trim()),20)+"'","DeptName");
					if (strTmp.Trim()!="")
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('"+strTmp+"�����Ѿ����ڣ�')</script>");
						return;
					}
				}
			}
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Transaction=ObjTran;
			ObjCmd.Connection=ObjConn;
			try
			{
				for(long i=0;i<strArrDept.Length;i++)
				{
					if (strArrDept[i].Trim()!="")
					{
						ObjCmd.CommandText="insert into DeptInfo(DeptName) values('"+ObjFun.getStr(ObjFun.CheckString(strArrDept[i]).Trim(),20)+"')";
						ObjCmd.ExecuteNonQuery();
					}
				}
				ObjTran.Commit();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����½����ųɹ���');try{ window.opener.RefreshForm() }catch(e){};</script>");
			}
			catch
			{
				ObjTran.Rollback();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�����½�����ʧ�ܣ�')</script>");
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
			txtDeptName.Text="";

			return;
		}
		#endregion

	}
}
