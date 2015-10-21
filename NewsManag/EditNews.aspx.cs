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

namespace EasyExam.NewsManag
{
	/// <summary>
	/// EditNews 的摘要说明。
	/// </summary>
	public partial class EditNews : System.Web.UI.Page
	{

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intNewsID=0,intCreateUserID=0;
	
		#region//************初始化信息*********
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
			intNewsID=Convert.ToInt32(Request["NewsID"]);
			rbSelectAccount.Text="";
			txtSelectAccount.Attributes["readonly"]="true";
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=1)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intNewsID!=0)
					{
						btnSelectAccount.Attributes.Add("onclick", "javascript:var obj=new Object();obj.name=document.all('txtSelectAccount').value;var str=window.showModalDialog('SelectBrower.aspx?NewsID="+intNewsID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectAccount').value=str; };return false;");
						LoadNewsData();
					}
				}
			}
		}
		#endregion

		#region//*********删除关联数据**********
		private void DelRelationData()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Transaction=ObjTran;
			ObjCmd.Connection=ObjConn;
			try
			{
				ObjCmd.CommandText="delete from NewsUser where NewsID="+intNewsID+"";
				ObjCmd.ExecuteNonQuery();
				
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
		}
		#endregion

		#region//**********加载要修改的新闻数据*********
		private void LoadNewsData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlConnection SqlConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select * from NewsInfo where NewsID="+intNewsID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				txtNewsTitle.Text=ObjDR["NewsTitle"].ToString();
				txtNewsContent.Text=ObjDR["NewsContent"].ToString();
				rbAllAccount.Checked=false;
				rbSelectAccount.Checked=false;
				if (ObjDR["BrowAccount"].ToString()=="1")
				{
					rbAllAccount.Checked=true;
				}
				SqlDataAdapter SqlCmd=null;
				DataSet SqlDS=null;
				int i=0;
				if (ObjDR["BrowAccount"].ToString()=="2")
				{
					rbSelectAccount.Checked=true;
					txtSelectAccount.Text="";

					SqlCmd=new SqlDataAdapter("select b.DeptID,b.DeptName from NewsUser a,DeptInfo b where a.DeptID=b.DeptID and a.NewsID="+intNewsID+" order by a.DeptID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"DeptInfo");
					for(i=0;i<SqlDS.Tables["DeptInfo"].Rows.Count;i++)
					{
						if (txtSelectAccount.Text.Trim()=="")
						{
							txtSelectAccount.Text=txtSelectAccount.Text+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
						else
						{
							txtSelectAccount.Text=txtSelectAccount.Text+";"+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
					}

					SqlCmd=new SqlDataAdapter("select b.UserID,b.LoginID from NewsUser a,UserInfo b where a.UserID=b.UserID and a.NewsID="+intNewsID+" order by a.UserID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"UserInfo");
					for(i=0;i<SqlDS.Tables["UserInfo"].Rows.Count;i++)
					{
						if (txtSelectAccount.Text.Trim()=="")
						{
							txtSelectAccount.Text=txtSelectAccount.Text+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
						else
						{
							txtSelectAccount.Text=txtSelectAccount.Text+";"+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
					}
				}
				txtCreateDate.Text=Convert.ToDateTime(ObjDR["CreateDate"].ToString()).ToString("d");
				intCreateUserID=Convert.ToInt32(ObjDR["CreateUserID"].ToString());
			}
			SqlConn.Dispose();
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

		#region//*********选择浏览人员*******
		protected void btnSelectAccount_Click(object sender, System.EventArgs e)
		{
			this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=new Object();obj.name=document.all('txtSelectAccount').value;var str=window.showModalDialog('SelectBrower.aspx?NewsID="+intNewsID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectAccount').value=str; }</script>");
		}
		#endregion

		#region//*********提交新闻信息***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (txtNewsTitle.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('新闻标题不能为空！')</script>");
				return;
			}
			if (txtNewsContent.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('新闻内容不能为空！')</script>");
				return;
			}
			if ((rbSelectAccount.Checked==true)&&(txtSelectAccount.Text.Trim()==""))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择浏览人员！')</script>");
				return;
			}

			string strTmp=ObjFun.GetValues("select NewsID from NewsInfo where NewsTitle='"+ObjFun.getStr(ObjFun.CheckString(txtNewsTitle.Text.Trim()),100)+"' and NewsID<>"+intNewsID+"","NewsID");
			if (strTmp.Trim()!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此新闻标题已经存在，请重新输入！')</script>");
				return;
			}

			string strNewsTitle=ObjFun.getStr(ObjFun.CheckString(txtNewsTitle.Text.Trim()),100);
			string strNewsContent=ObjFun.getStr(ObjFun.CheckString(txtNewsContent.Text.Trim()),800);
			int intBrowAccount=0;
			if (rbAllAccount.Checked==true)
			{
				intBrowAccount=1;
			}
			if (rbSelectAccount.Checked==true)
			{
				intBrowAccount=2;
			}
			int intCreateUserID=Convert.ToInt32(myUserID);
			DateTime dtmCreateDate=Convert.ToDateTime(txtCreateDate.Text);

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Transaction=ObjTran;
			ObjCmd.Connection=ObjConn;
			try
			{
				//更新新闻
				if (rbAllAccount.Checked==true)
				{
					ObjCmd.CommandText="delete from NewsUser where NewsID="+intNewsID+"";
					ObjCmd.ExecuteNonQuery();
				}
				ObjCmd.CommandText="Update NewsInfo set NewsTitle='"+strNewsTitle+"',NewsContent='"+strNewsContent+"',BrowAccount='"+intBrowAccount+"' where NewsID="+intNewsID+"";
				ObjCmd.ExecuteNonQuery();
				
				ObjTran.Commit();

				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改新闻成功！');try{ window.opener.RefreshForm() }catch(e){};window.close();</script>");
			}
			catch
			{
				ObjTran.Rollback();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改新闻失败！')</script>");
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
		}
		#endregion

	}
}
