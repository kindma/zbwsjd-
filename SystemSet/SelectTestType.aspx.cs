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
	/// SelectTestType ��ժҪ˵����
	/// </summary>
	public partial class SelectTestType : System.Web.UI.Page
	{
		
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
		bool bJoySoftware=false;

		#region//*********��ʼ��Ϣ*******
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
			//�������
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();

			intUserID=Convert.ToInt32(Request["UserID"]);
			bJoySoftware=ObjFun.JoySoftware();
			if (!IsPostBack)
			{
				if (Convert.ToInt32(ObjFun.GetValues("select JudgeTestType from UserInfo where UserID="+intUserID+"","JudgeTestType"))==1)
				{
					rbAllTestType.Checked=true;
					rbSelectTestType.Checked=false;
				}
				else
				{
					rbSelectTestType.Checked=true;
					rbAllTestType.Checked=false;
				}
				ShowSelectedData();//��ʾѡ������
				//this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=window.dialogArguments;document.all('txtQuery').value=obj.name;</script>");
			}
			if (!IsPostBack)
			{
//				if (bJoySoftware==false)
//				{
//					ButInput.Attributes.Add("onclick", "javascript:alert('�Բ���δע���û����������������ͣ�');return false;");
//				}
				strSql="select TestTypeID,TestTypeName from TestTypeInfo where BaseTestType='�����' or BaseTestType='�ʴ���' or BaseTestType='������' or BaseTestType='������' order by TestTypeID asc";
				ShowData(strSql);
			}
		}
		#endregion

		#region//******��ʾ�����б�******
		private void ShowData(string strSql)
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection objConn=new SqlConnection(strConn);
			SqlDataAdapter objCmd=new SqlDataAdapter(strSql,objConn);
			DataSet objDS=new DataSet();
			objCmd.Fill(objDS,"TestTypeInfo");
			LBSelect.DataTextField="TestTypeName";
			LBSelect.DataValueField="TestTypeID";
			LBSelect.DataSource=objDS.Tables["TestTypeInfo"].DefaultView;
			LBSelect.DataBind();
			objCmd.Dispose();
			objDS.Dispose();
			objConn.Dispose();
		}
		#endregion

		#region//******��ʾ��ѡ�������б�******
		private void ShowSelectedData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection objConn=new SqlConnection(strConn);
			SqlDataAdapter objCmd=new SqlDataAdapter("select b.TestTypeID,b.TestTypeName from UserPower a,TestTypeInfo b where a.OptionID=b.TestTypeID and a.UserID="+intUserID+" and a.PowerID=2 order by a.OptionID asc",objConn);
			DataSet objDS=new DataSet();
			objCmd.Fill(objDS,"UserPower");
			LBSelected.DataTextField="TestTypeName";
			LBSelected.DataValueField="TestTypeID";
			LBSelected.DataSource=objDS.Tables["UserPower"].DefaultView;
			LBSelected.DataBind();
			objCmd.Dispose();
			objDS.Dispose();
			objConn.Dispose();
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

		#region//****ѡ�����Ͱ�ť�¼�****
		protected void butAllSelect_Click(object sender, System.EventArgs e)
		{
			ListItem LITmp=null;
			for(int i=0;i<LBSelect.Items.Count;i++)
			{
				LITmp=new ListItem(LBSelect.Items[i].Text,LBSelect.Items[i].Value);
				if(LBSelected.Items.IndexOf(LITmp)==-1)
				{
					LBSelected.Items.Add(LITmp);
				}
			}
			LBSelect.Items.Clear();
		}

		protected void butOneSelect_Click(object sender, System.EventArgs e)
		{
			ArrayList arrList=new ArrayList();
			foreach(ListItem item in LBSelect.Items)
			{
				if (item.Selected)
				{
					arrList.Add(item);
				}
			}  
			foreach(ListItem item in arrList)
			{
				if (LBSelected.Items.IndexOf(item)==-1)
				{
					LBSelected.Items.Add(item);
				}
				LBSelect.Items.Remove(item);
			}
			LBSelected.SelectedIndex=-1;
		}

		protected void butOneDel_Click(object sender, System.EventArgs e)
		{
			ArrayList arrList=new ArrayList();
			foreach(ListItem item in LBSelected.Items)
			{
				if (item.Selected)
				{
					arrList.Add(item);
				}
			}  
			foreach(ListItem item in arrList)
			{
				if (LBSelect.Items.IndexOf(item)==-1)
				{
					LBSelect.Items.Add(item);
				}
				LBSelected.Items.Remove(item);
			}
			LBSelect.SelectedIndex=-1;
		}

		protected void butAllDel_Click(object sender, System.EventArgs e)
		{
			ListItem LITmp=null;
			for(int i=0;i<LBSelected.Items.Count;i++)
			{
				LITmp=new ListItem(LBSelected.Items[i].Text,LBSelected.Items[i].Value);
				if(LBSelect.Items.IndexOf(LITmp)==-1)
				{
					LBSelect.Items.Add(LITmp);
				}
			}
			LBSelected.Items.Clear();
		}
		#endregion

		#region//********ѡ���ʺ�����*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
//			if (ObjFun.JoySoftware()==false)
//			{
//				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ���δע���û����������������ͣ�')</script>");
//				return;
//			}
			//���浽���ݿ�
			int i=0;
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn =new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Transaction=ObjTran;
			ObjCmd.Connection=ObjConn;
			try
			{
				ObjCmd.CommandText="delete from UserPower where UserID="+intUserID+" and PowerID=2";
				ObjCmd.ExecuteNonQuery();
				
				if (rbAllTestType.Checked==true)
				{
					ObjCmd.CommandText="Update UserInfo set JudgeTestType=1 where UserID="+intUserID+"";
					ObjCmd.ExecuteNonQuery();
				}
				else
				{
					for(i=0;i<LBSelected.Items.Count;i++)
					{
						ObjCmd.CommandText="insert into UserPower(UserID,PowerID,OptionID) values("+intUserID+",2,"+LBSelected.Items[i].Value+")";
						ObjCmd.ExecuteNonQuery();
					}
					ObjCmd.CommandText="Update UserInfo set JudgeTestType=2 where UserID="+intUserID+"";
					ObjCmd.ExecuteNonQuery();
				}

				ObjTran.Commit();
			}
			catch
			{
				ObjTran.Rollback();
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
			this.RegisterStartupScript("newWindow","<script language='javascript'>window.close();</script>");
		}
		#endregion

	}
}
