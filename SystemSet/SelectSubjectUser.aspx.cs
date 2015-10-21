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
	/// SelectSubjectUser ��ժҪ˵����
	/// </summary>
	public partial class SelectSubjectUser : System.Web.UI.Page
	{
		
		string strSql1="",strSql2="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSubjectID=0;

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

			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			if (!IsPostBack)
			{
				if (Convert.ToInt32(ObjFun.GetValues("select BrowAccount from SubjectInfo where SubjectID="+intSubjectID+"","BrowAccount"))==1)
				{
					rbAllAccount.Checked=true;
					rbSelectAccount.Checked=false;
				}
				else
				{
					rbSelectAccount.Checked=true;
					rbAllAccount.Checked=false;
				}
				ShowDeptInfo();//��ʾ������Ϣ
				ShowSelectedData();//��ʾѡ������
				//this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=window.dialogArguments;document.all('txtQuery').value=obj.name;</script>");
			}
			//��ʾȫ��
			if (DDLDeptName.SelectedItem.Value=="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			//��ʾ�Բ���Ϊ����������
			if(DDLDeptName.SelectedItem.Value!="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo where DeptID='"+DDLDeptName.SelectedItem.Value+"' order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a,DeptInfo b where a.DeptID=b.DeptID and b.DeptID='"+DDLDeptName.SelectedItem.Value+"'";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" and (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			strSql2=strSql2+" order by a.LoginID asc";

			if (!IsPostBack)
			{
				ShowData(strSql1,strSql2);
			}
		}
		#endregion

		#region//******��ʾ������Ϣ******
		private void ShowDeptInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from DeptInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"DeptInfo");
			DDLDeptName.DataSource=SqlDS.Tables["DeptInfo"].DefaultView;
			DDLDeptName.DataTextField="DeptName";
			DDLDeptName.DataValueField="DeptID";
			DDLDeptName.DataBind();
			SqlConn.Dispose();
			ListItem objTmp=new ListItem("--ȫ��--","0");
			DDLDeptName.Items.Add(objTmp);
			DDLDeptName.Items.FindByText("--ȫ��--").Selected=true;
		}
		#endregion

		#region//******��ʾ�����б�******
		private void ShowData(string strSql1,string strSql2)
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection objConn=new SqlConnection(strConn);
			SqlDataAdapter objCmd=null;
			DataSet objDS=null;

			LBSelect.Items.Clear();
			objCmd=new SqlDataAdapter(strSql1,objConn);
			objDS=new DataSet();
			objCmd.Fill(objDS,"DeptInfo");
			for(int i=0;i<objDS.Tables["DeptInfo"].Rows.Count;i++)
			{
				LBSelect.Items.Add(new ListItem(objDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString().Trim(),"#"+objDS.Tables["DeptInfo"].Rows[i]["DeptID"].ToString().Trim()));
			}

			objCmd=new SqlDataAdapter(strSql2,objConn);
			objDS=new DataSet();
			objCmd.Fill(objDS,"UserInfo");
			for(int i=0;i<objDS.Tables["UserInfo"].Rows.Count;i++)
			{
				LBSelect.Items.Add(new ListItem(objDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString().Trim(),objDS.Tables["UserInfo"].Rows[i]["UserID"].ToString().Trim()));
			}

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
			SqlDataAdapter objCmd=null;
			DataSet objDS=null;

			LBSelected.Items.Clear();
			objCmd=new SqlDataAdapter("select b.DeptID,b.DeptName from SubjectUser a,DeptInfo b where a.DeptID=b.DeptID and a.SubjectID="+intSubjectID+" order by b.DeptName asc",objConn);
			objDS=new DataSet();
			objCmd.Fill(objDS,"DeptInfo");
			for(int i=0;i<objDS.Tables["DeptInfo"].Rows.Count;i++)
			{
				LBSelected.Items.Add(new ListItem(objDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString().Trim(),"#"+objDS.Tables["DeptInfo"].Rows[i]["DeptID"].ToString().Trim()));
			}

			objCmd=new SqlDataAdapter("select b.UserID,b.LoginID from SubjectUser a,UserInfo b where a.UserID=b.UserID and a.SubjectID="+intSubjectID+" order by b.LoginID asc",objConn);
			objDS=new DataSet();
			objCmd.Fill(objDS,"UserInfo");
			for(int i=0;i<objDS.Tables["UserInfo"].Rows.Count;i++)
			{
				LBSelected.Items.Add(new ListItem(objDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString().Trim(),objDS.Tables["UserInfo"].Rows[i]["UserID"].ToString().Trim()));
			}

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

		#region//******ѡ������ʾ��Ա******
		protected void DDLDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//��ʾȫ��
			if (DDLDeptName.SelectedItem.Value=="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			//��ʾ�Բ���Ϊ����������
			if(DDLDeptName.SelectedItem.Value!="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo where DeptID='"+DDLDeptName.SelectedItem.Value+"' order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a,DeptInfo b where a.DeptID=b.DeptID and b.DeptID='"+DDLDeptName.SelectedItem.Value+"'";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" and (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			strSql2=strSql2+" order by a.LoginID asc";

			ShowData(strSql1,strSql2);	
		}
		#endregion

		#region//****��ѯ��Ա��ť�¼�****

		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//��ʾȫ��
			if (DDLDeptName.SelectedItem.Value=="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			//��ʾ�Բ���Ϊ����������
			if(DDLDeptName.SelectedItem.Value!="0")
			{
				strSql1="select DeptID,DeptName from DeptInfo where DeptID='"+DDLDeptName.SelectedItem.Value+"' order by DeptName";
				strSql2="select a.UserID,a.LoginID from UserInfo a,DeptInfo b where a.DeptID=b.DeptID and b.DeptID='"+DDLDeptName.SelectedItem.Value+"'";
				if (txtQuery.Text.Trim()!="")
				{
					strSql2=strSql2+" and (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
				}
			}
			strSql2=strSql2+" order by a.LoginID asc";

			ShowData(strSql1,strSql2);	
		}
		#endregion

		#region//****ѡ����Ա��ť�¼�****
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

		#region//********ѡ���ʺ���Ա*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
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
				ObjCmd.CommandText="delete from SubjectUser where SubjectID="+intSubjectID+"";
				ObjCmd.ExecuteNonQuery();

				if (rbAllAccount.Checked==true)
				{
					ObjCmd.CommandText="Update SubjectInfo set BrowAccount=1 where SubjectID="+intSubjectID+"";
					ObjCmd.ExecuteNonQuery();
				}
				else
				{
					for(i=0;i<LBSelected.Items.Count;i++)
					{
						if (LBSelected.Items[i].Value.ToString().IndexOf("#")>=0)
						{
							ObjCmd.CommandText="insert into SubjectUser(SubjectID,UserID,DeptID) values("+intSubjectID+",0,"+LBSelected.Items[i].Value.Replace("#","")+")";
							ObjCmd.ExecuteNonQuery();
						}
						else
						{
							ObjCmd.CommandText="insert into SubjectUser(SubjectID,UserID,DeptID) values("+intSubjectID+","+LBSelected.Items[i].Value+",0)";
							ObjCmd.ExecuteNonQuery();
						}
					}
					ObjCmd.CommandText="Update SubjectInfo set BrowAccount=2 where SubjectID="+intSubjectID+"";
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
