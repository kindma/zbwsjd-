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


namespace EasyExam.RubricManag
{
	/// <summary>
	/// ManagBookTree ��ժҪ˵����
	/// </summary>
	public partial class ManagBookTree : System.Web.UI.Page
	{

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
	
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

			strSql="select * from SubjectInfo order by SubjectName asc";
			if(!Page.IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ButDel.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ���½���')==false) return false;}");
					ShowSubjectNode(strSql);
				}
			}
		}

		private void ShowSubjectNode(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SubjectInfo");

			//��Ӹ��ڵ�
			TreeViewBook.Nodes.Clear();
			for(int i=0;i<SqlDS.Tables["SubjectInfo"].Rows.Count;i++)
			{	
				TreeNode node=new TreeNode();
                node.ImageUrl = "../Images/folder.gif";
				node.Text=SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectName"].ToString();
				node.Target="bookmain";
				//node.NavigateUrl="ManagBookList.aspx?SubjectID="+SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+Convert.ToString(0)+"&SectionID="+Convert.ToString(0)+"";
                node.Value = SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"].ToString() + ".0.0.0." + "ManagBookList.aspx?SubjectID=" + SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"].ToString() + "&ChapterID=" + Convert.ToString(0) + "&SectionID=" + Convert.ToString(0) + ".0";
				node.Expanded=true;
				TreeViewBook.Nodes.Add(node);   
				ShowChapterNode(Convert.ToInt32(SqlDS.Tables["SubjectInfo"].Rows[i]["SubjectID"]),node);
			}
            //TreeViewBook.DataBind();

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
				node.Target="bookmain";
                node.ImageUrl = "../Images/folder.gif";
				node.Text=SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterName"].ToString();
				//node.NavigateUrl="ManagBookList.aspx?SubjectID="+SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"].ToString()+"&SectionID="+Convert.ToString(0)+"";
                node.Value = SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"].ToString() + "." + SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"].ToString() + ".0." + SqlDS.Tables["ChapterInfo"].Rows[i]["CreateUserID"].ToString() + "." + "ManagBookList.aspx?SubjectID=" + SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"].ToString() + "&ChapterID=" + SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"].ToString() + "&SectionID=" + Convert.ToString(0) + ".1";
				node.Expanded=true;
				treenode.ChildNodes.Add(node);
				ShowSectionNode(Convert.ToInt32(SqlDS.Tables["ChapterInfo"].Rows[i]["SubjectID"]),Convert.ToInt32(SqlDS.Tables["ChapterInfo"].Rows[i]["ChapterID"]),node);
			}
            //TreeViewBook.DataBind();

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
				node.Target="bookmain";
                node.ImageUrl = "../Images/folder.gif";
				node.Text=SqlDS.Tables["SectionInfo"].Rows[i]["SectionName"].ToString();
				//node.NavigateUrl="ManagBookList.aspx?SubjectID="+SqlDS.Tables["SectionInfo"].Rows[i]["SubjectID"].ToString()+"&ChapterID="+SqlDS.Tables["SectionInfo"].Rows[i]["ChapterID"].ToString()+"&SectionID="+SqlDS.Tables["SectionInfo"].Rows[i]["SectionID"].ToString()+"";
                node.Value = SqlDS.Tables["SectionInfo"].Rows[i]["SubjectID"].ToString() + "." + SqlDS.Tables["SectionInfo"].Rows[i]["ChapterID"].ToString() + "." + SqlDS.Tables["SectionInfo"].Rows[i]["SectionID"].ToString() + "." + SqlDS.Tables["SectionInfo"].Rows[i]["CreateUserID"].ToString() + "." + "ManagBookList.aspx?SubjectID=" + SqlDS.Tables["SectionInfo"].Rows[i]["SubjectID"].ToString() + "&ChapterID=" + SqlDS.Tables["SectionInfo"].Rows[i]["ChapterID"].ToString() + "&SectionID=" + SqlDS.Tables["SectionInfo"].Rows[i]["SectionID"].ToString() + ".2";
				treenode.ChildNodes.Add(node);
			}
            //TreeViewBook.DataBind();

			SqlConn.Dispose();
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

		protected void ButAdd_Click(object sender, System.EventArgs e)
		{
			//��ӽڵ�
			if (txtName.Text=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('0�������½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

            TreeNode treenode = TreeViewBook.SelectedNode;

			if (treenode==null)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���Ŀ�������ƺ��ٽ��д˲�����')</script>");
				return;
			}

            string[] arrnode = treenode.Value.ToString().Split('.');
			int subjectid=Int32.Parse(arrnode[0]);//��ÿ�ĿID
			int chapterid=Int32.Parse(arrnode[1]);//��ȡ��ID
			int sectionid=Int32.Parse(arrnode[2]);//��ȡ��ID
			string CreateUserID=Convert.ToString(arrnode[3]);//�����ʻ�

            int depth =int.Parse(arrnode[6].ToString());//���

			if (depth==2)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('2��ѡ���Ŀ�������ƺ��ٽ��д˲�����')</script>");
				return;
			}

			string strTmp="";
			if (depth==0)
			{
				strTmp=ObjFun.GetValues("select ChapterName from ChapterInfo where ChapterName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' and SubjectID="+subjectid+"","ChapterName");
			}
			if (depth==1)
			{
				strTmp=ObjFun.GetValues("select SectionName from SectionInfo where SectionName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' and SubjectID="+subjectid+" and ChapterID="+chapterid+"","SectionName");
			}

			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=null;

				if (depth==0)
				{
					SqlCmd=new SqlCommand("insert into ChapterInfo(SubjectID,ChapterName,CreateUserID) values("+subjectid+",'"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"','"+myUserID+"')",SqlConn);
				}
				if (depth==1)
				{
					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(CreateUserID==myUserID.Trim()))
					{
						SqlCmd=new SqlCommand("insert into SectionInfo(SubjectID,ChapterID,SectionName,BrowNumber,CreateDate) values("+subjectid+","+chapterid+",'"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"',0,'"+System.DateTime.Now.ToString("d")+"')",SqlConn);
					}
					else
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
						return;
					}
				}

				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				//this.RegisterStartupScript("newWindow","<script language='javascript'>alert('����½����Ƴɹ���')</script>");

				txtName.Text="";
				ShowSubjectNode(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('���½������Ѿ����ڣ�')</script>");
				return;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public int getDepth(TreeNode treeNode)
        {
            int max = 0;
            if (treeNode.ChildNodes.Count == 0)
                return 0;
            else
            {
                foreach (TreeNode node in treeNode.ChildNodes)
                {
                    if (getDepth(node) > max)
                        max = getDepth(node);
                }
                return max + 1;
            }
        }



		protected void ButEdit_Click(object sender, System.EventArgs e)
		{
			//�޸Ľڵ�
			if (txtName.Text=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�������½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

           TreeNode treenode = TreeViewBook.SelectedNode;

			if (treenode==null)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

			string[] arrnode=treenode.Value.ToString().Split('.');
			int subjectid=Int32.Parse(arrnode[0]);//��ÿ�ĿID
			int chapterid=Int32.Parse(arrnode[1]);//��ȡ��ID
			int sectionid=Int32.Parse(arrnode[2]);//��ȡ��ID
			string CreateUserID=Convert.ToString(arrnode[3]);//�����ʻ�

			int depth=int.Parse(arrnode[6].ToString());//������

			if (depth==0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

			string strTmp="";
			if (depth==1)
			{   
				strTmp=ObjFun.GetValues("select ChapterName from ChapterInfo where ChapterName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' and SubjectID="+subjectid+" and ChapterID<>"+chapterid+"","ChapterName");
			}
			if (depth==2)
			{
				strTmp=ObjFun.GetValues("select SectionName from SectionInfo where SectionName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' and SubjectID="+subjectid+" and ChapterID="+chapterid+" and SectionID<>"+sectionid+"","SectionID");
			}

			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=null;

				if (depth==1)
				{
					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(CreateUserID==myUserID.Trim()))
					{
						SqlCmd=new SqlCommand("update ChapterInfo set ChapterName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' where SubjectID="+subjectid+" and ChapterID="+chapterid+"",SqlConn);
					}
					else
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
						return;
					}
				}
				if (depth==2)
				{
					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(CreateUserID==myUserID.Trim()))
					{
						SqlCmd=new SqlCommand("update SectionInfo set SectionName='"+ObjFun.getStr(ObjFun.CheckString(txtName.Text.Trim()),50)+"' where SubjectID="+subjectid+" and ChapterID="+chapterid+" and SectionID="+sectionid+"",SqlConn);
					}
					else
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
						return;
					}
				}

				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�޸��½����Ƴɹ���')</script>");

				txtName.Text="";
				ShowSubjectNode(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('���½������Ѿ����ڣ�')</script>");
				return;
			}
		}

		protected void ButDel_Click(object sender, System.EventArgs e)
		{
			//ɾ���ڵ�
			TreeNode treenode=TreeViewBook.SelectedNode;

			if (treenode==null)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

			string[] arrnode=treenode.Value.Split('.');
			int subjectid=Int32.Parse(arrnode[0]);//��ÿ�ĿID
			int chapterid=Int32.Parse(arrnode[1]);//��ȡ��ID
			int sectionid=Int32.Parse(arrnode[2]);//��ȡ��ID
			string CreateUserID=Convert.ToString(arrnode[3]);//�����ʻ�

			int depth=int.Parse(arrnode[6].ToString());//������

			if (depth==0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���½����ƺ��ٽ��д˲�����')</script>");
				return;
			}

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlConn.Open();

			if (depth==1)
			{
				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(CreateUserID==myUserID.Trim()))
				{
					//ɾ�����ݱ�SectionInfo
					SqlCmd=new SqlCommand("delete SectionInfo from ChapterInfo a,SectionInfo b where a.SubjectID="+subjectid+" and a.ChapterID="+chapterid+" and b.SubjectID=a.SubjectID and b.ChapterID=a.ChapterID",SqlConn);
					SqlCmd.ExecuteNonQuery();

					SqlCmd=new SqlCommand("delete ChapterInfo where SubjectID="+subjectid+" and ChapterID="+chapterid+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					return;
				}

			}
			if (depth==2)
			{
				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(CreateUserID==myUserID.Trim()))
				{

					SqlCmd=new SqlCommand("delete SectionInfo where SubjectID="+subjectid+" and ChapterID="+chapterid+" and SectionID="+sectionid+"",SqlConn);
					SqlCmd.ExecuteNonQuery();
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					return;
				}
			}

			SqlConn.Close();
			SqlConn.Dispose();
			//this.RegisterStartupScript("newWindow","<script language='javascript'>alert('ɾ���½����Ƴɹ���')</script>");

			txtName.Text="";
			ShowSubjectNode(strSql);
		}
        protected void TreeViewBook_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode treenode = TreeViewBook.SelectedNode;
            string[] arrnode = treenode.Value.Split('.');
            this.RegisterStartupScript("newWindow", "<script language='javascript'>window.open('" + arrnode[4]+"."+arrnode[5] + "','bookmain')</script>");
        }
}
}
