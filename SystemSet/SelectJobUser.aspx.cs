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
	/// SelectJobUser 的摘要说明。
	/// </summary>
	public partial class SelectJobUser : System.Web.UI.Page
	{
		
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intJobID=0;

		#region//*********初始信息*******
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

			intJobID=Convert.ToInt32(Request["JobID"]);
			if (!IsPostBack)
			{
				ShowSelectedData();//显示选择数据
				//this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=window.dialogArguments;document.all('txtQuery').value=obj.name;</script>");
			}
			strSql="select a.UserID,a.LoginID from UserInfo a";
			if (txtQuery.Text.Trim()!="")
			{
				strSql=strSql+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
			}
			strSql=strSql+" order by a.LoginID asc";

			if (!IsPostBack)
			{
				ShowData(strSql);
			}
		}
		#endregion

		#region//******显示数据列表******
		private void ShowData(string strSql)
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection objConn=new SqlConnection(strConn);
			SqlDataAdapter objCmd=null;
			DataSet objDS=null;

			LBSelect.Items.Clear();
			objCmd=new SqlDataAdapter(strSql,objConn);
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

		#region//******显示已选择数据列表******
		private void ShowSelectedData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection objConn=new SqlConnection(strConn);
			SqlDataAdapter objCmd=null;
			DataSet objDS=null;
			objCmd=new SqlDataAdapter("select a.UserID,a.LoginID from UserInfo a where a.JobID="+intJobID+" order by a.LoginID asc",objConn);
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

		#region//******选择部门显示人员******
		private void DDLDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			strSql="select a.UserID,a.LoginID from UserInfo a";
			if (txtQuery.Text.Trim()!="")
			{
				strSql=strSql+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
			}
			strSql=strSql+" order by a.LoginID asc";

			ShowData(strSql);	
		}
		#endregion

		#region//****查询人员按钮事件****

		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			strSql="select a.UserID,a.LoginID from UserInfo a";
			if (txtQuery.Text.Trim()!="")
			{
				strSql=strSql+" where (a.LoginID like '%"+txtQuery.Text.Trim()+"%' or a.UserName like '%"+txtQuery.Text.Trim()+"%')";
			}
			strSql=strSql+" order by a.LoginID asc";

			ShowData(strSql);	
		}
		#endregion

		#region//****选择人员按钮事件****
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

		#region//********选定帐号人员*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			//保存到数据库
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
				ObjCmd.CommandText="Update UserInfo set JobID=0 where JobID="+intJobID+"";
				ObjCmd.ExecuteNonQuery();

				for(i=0;i<LBSelected.Items.Count;i++)
				{
					ObjCmd.CommandText="Update UserInfo set JobID="+intJobID+" where UserID="+LBSelected.Items[i].Value+"";
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
