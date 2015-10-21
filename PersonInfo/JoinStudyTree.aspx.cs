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


namespace EasyExam.PersonInfo
{
	/// <summary>
	/// JoinStudyTree 的摘要说明。
	/// </summary>
	public partial class JoinStudyTree : System.Web.UI.Page
	{

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				intUserID=Convert.ToInt32(myUserID);
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			strSql="select a.* from SubjectInfo a where (a.BrowAccount=1 or (a.BrowAccount=2 and Exists(select * from SubjectUser b where b.SubjectID=a.SubjectID and b.UserID="+intUserID+")) or Exists(select * from UserInfo c,DeptInfo d,SubjectUser e where c.UserID="+intUserID+" and c.DeptID=d.DeptID and d.DeptID=e.DeptID and e.SubjectID=a.SubjectID)) order by a.SubjectName asc";

			if(!Page.IsPostBack)
			{
				ShowSubjectNode(strSql);
			}
		}

		private void ShowSubjectNode(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SubjectInfo");

			//添加根节点
			TreeViewBook.Nodes.Clear();
			for(int i=0;i<SqlDS.Tables["SubjectInfo"].Rows.Count;i++)
			{	
				TreeNode node=new TreeNode();
                node.ImageUrl = "../images/folder.gif";
				node.Text=SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectName"].ToString();
				node.Target="studymain";
				node.Value="JoinStudyList.aspx?SubjectID="+SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+Convert.ToString(0)+"&SectionID="+Convert.ToString(0)+"";
				//node.Value=SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"].ToString()+".0.0.0";
				node.Expanded=true;
				TreeViewBook.Nodes.Add(node);   
				ShowChapterNode(Convert.ToInt32(SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"]),node);
			}
			TreeViewBook.DataBind();

			SqlConn.Dispose();
		}

		private void ShowChapterNode(int intSubjectID,TreeNode treenode)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from ChapterInfo where SubjectID="+intSubjectID+" order by ChapterID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"ChapterInfo");

			for(int i=0;i<SqlDS.Tables["ChapterInfo"].Rows.Count;i++)
			{	
			   TreeNode node=new TreeNode();
				node.Target="studymain";
                node.ImageUrl = "../images/folder.gif";
				node.Text=SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterName"].ToString();
				node.Value="JoinStudyList.aspx?SubjectID="+SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"].ToString()+"&SectionID="+Convert.ToString(0)+"";
				//node.Value=SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"].ToString()+"."+SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"].ToString()+".0."+SqlDS.Tables["ChapterInfo"].Rows[i]["CreateUserID"].ToString();
				node.Expanded=true;
				treenode.ChildNodes.Add(node);
				ShowSectionNode(Convert.ToInt32(SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"]),Convert.ToInt32(SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"]),node);
			}
			TreeViewBook.DataBind();

			SqlConn.Dispose();
		}

		private void ShowSectionNode(int intSubjectID,int intChapterID,TreeNode treenode)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.*,b.CreateUserID from SectionInfo a,ChapterInfo b where a.SubjectID="+intSubjectID+" and a.ChapterID="+intChapterID+" and b.SubjectID=a.SubjectID and b.ChapterID=a.ChapterID order by a.SectionID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SectionInfo");

			for(int i=0;i<SqlDS.Tables["SectionInfo"].Rows.Count;i++)
			{	
				TreeNode node=new TreeNode();
				node.Target="studymain";
                node.ImageUrl = "../images/folder.gif";
				node.Text=SqlDS.Tables["SectionInfo"].Rows[i]["SectionName"].ToString();
				node.Value="JoinStudyList.aspx?SubjectID="+SqlDS.Tables["SectionInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+SqlDS.Tables["SectionInfo"].Rows[i]["ChapterID"].ToString()+"&SectionID="+SqlDS.Tables["SectionInfo"].Rows[i]["SectionID"].ToString()+"";
				//node.Value=SqlDS.Tables["SectionInfo"].Rows[i]["SubjectID"].ToString()+"."+SqlDS.Tables["SectionInfo"].Rows[i]["ChapterID"].ToString()+"."+SqlDS.Tables["SectionInfo"].Rows[i]["SectionID"].ToString()+"."+SqlDS.Tables["SectionInfo"].Rows[i]["CreateUserID"].ToString();
				node.Expanded=true;
				treenode.ChildNodes.Add(node);
			}
			TreeViewBook.DataBind();

			SqlConn.Dispose();
		}

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

        protected void TreeViewBook_SelectedNodeChanged(object sender, EventArgs e)
        {
            string url = TreeViewBook.SelectedNode.Value.ToString();
            Response.Write("<script>window.open('" + url + "','studymain')</script>");
        }
}
}
