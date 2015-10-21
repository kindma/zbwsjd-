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
using System.IO;

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// JoinStudyList 的摘要说明。
	/// </summary>
	public partial class JoinStudyList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0,intSubjectID=0,intChapterID=0,intSectionID=0;
	
		#region//*******初始化信息********
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
			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			intChapterID=Convert.ToInt32(Request["ChapterID"]);
			intSectionID=Convert.ToInt32(Request["SectionID"]);

			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				ShowSubjectInfo();//显示科目信息
				DDLSubjectName.Items.FindByText("--全部--").Selected=true;

				bWhere=true;
				strSql="select a.SectionID,c.SubjectName,b.ChapterName,a.SectionName,a.BrowNumber,d.LoginID as CreateLoginID from SectionInfo a LEFT OUTER JOIN ChapterInfo b ON b.ChapterID=a.ChapterID LEFT OUTER JOIN SubjectInfo c ON c.SubjectID=a.SubjectID LEFT OUTER JOIN UserInfo d ON d.UserID=b.CreateUserID where (c.BrowAccount=1 or (c.BrowAccount=2 and Exists(select * from SubjectUser d where d.SubjectID=c.SubjectID and d.UserID="+intUserID+")) or Exists(select * from UserInfo e,DeptInfo f,SubjectUser g where e.UserID="+intUserID+" and e.DeptID=f.DeptID and f.DeptID=g.DeptID and g.SubjectID=c.SubjectID))";
				//显示以科目为条件的数据
				if (intSubjectID!=0)
				{
					if (bWhere)
					{
						strSql=strSql+" and a.SubjectID='"+intSubjectID.ToString()+"'";
					}
					else
					{
						strSql=strSql+" where a.SubjectID='"+intSubjectID.ToString()+"'";
						bWhere=true;
					}
				}
				//显示以章名为条件的数据
				if (intChapterID!=0)
				{
					if (bWhere)
					{
						strSql=strSql+" and a.ChapterID='"+intChapterID.ToString()+"'";
					}
					else
					{
						strSql=strSql+" where a.ChapterID='"+intChapterID.ToString()+"'";
						bWhere=true;
					}
				}
				//显示以节名为条件的数据
				if (intSectionID!=0)
				{
					if (bWhere)
					{
						strSql=strSql+" and a.SectionID='"+intSectionID.ToString()+"'";
					}
					else
					{
						strSql=strSql+" where a.SectionID='"+intSectionID.ToString()+"'";
						bWhere=true;
					}
				}
				strSql=strSql+" order by c.SubjectName,b.ChapterID,a.SectionID";
				if (DataGridBook.Attributes["SortExpression"] == null)
				{
					DataGridBook.Attributes["SortExpression"] = "SubjectName";
					DataGridBook.Attributes["SortDirection"] = "ASC";
				}
				ShowData(strSql);
				LabCondition.Text=strSql;
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*********显示科目信息**********
		private void ShowSubjectInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.* from SubjectInfo a where (a.BrowAccount=1 or (a.BrowAccount=2 and Exists(select * from SubjectUser b where b.SubjectID=a.SubjectID and b.UserID="+intUserID+")) or Exists(select * from UserInfo c,DeptInfo d,SubjectUser e where c.UserID="+intUserID+" and c.DeptID=d.DeptID and d.DeptID=e.DeptID and e.SubjectID=a.SubjectID)) order by a.SubjectName asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SubjectInfo");
			DDLSubjectName.Items.Clear();
			DDLSubjectName.DataSource=SqlDS.Tables["SubjectInfo"].DefaultView;
			DDLSubjectName.DataTextField="SubjectName";
			DDLSubjectName.DataValueField="SubjectID";
			DDLSubjectName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--全部--","0");
			DDLSubjectName.Items.Add(strTmp);
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
			this.DataGridBook.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridBook_PageIndexChanged);
			this.DataGridBook.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridBook_SortCommand);
			this.DataGridBook.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridBook_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridBook_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridBook.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridBook_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridBook').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridBook').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridBook.Attributes.Add("oldValue", "#F7F7F7");
				DataGridBook.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******显示数据列表*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SectionInfo");
			RowNum=DataGridBook.CurrentPageIndex*DataGridBook.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridBook.Attributes["SortExpression"];
			string SortDirection = DataGridBook.Attributes["SortDirection"];
			SqlDS.Tables["SectionInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridBook.DataSource=SqlDS.Tables["SectionInfo"].DefaultView;
			DataGridBook.DataBind();
			for(int i=0;i<DataGridBook.Items.Count;i++)
			{		
				LinkButton LBSectionName=(LinkButton)DataGridBook.Items[i].FindControl("LinkButSectionName");
				LinkButton LBBrowBook=(LinkButton)DataGridBook.Items[i].FindControl("LinkButBrowBook");

				LBSectionName.Attributes.Add("onclick", "javascript:NewWin=window.open('BrowBook.aspx?SectionID="+DataGridBook.Items[i].Cells[0].Text.Trim()+"','BrowBook','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
				LBBrowBook.Attributes.Add("onclick", "javascript:NewWin=window.open('BrowBook.aspx?SectionID="+DataGridBook.Items[i].Cells[0].Text.Trim()+"','BrowBook','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["SectionInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridBook.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridBook.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridBook_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridBook.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridBook_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridBook.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.SectionID,c.SubjectName,b.ChapterName,a.SectionName,a.BrowNumber,d.LoginID as CreateLoginID from SectionInfo a LEFT OUTER JOIN ChapterInfo b ON b.ChapterID=a.ChapterID LEFT OUTER JOIN SubjectInfo c ON c.SubjectID=a.SubjectID LEFT OUTER JOIN UserInfo d ON d.UserID=b.CreateUserID where (c.BrowAccount=1 or (c.BrowAccount=2 and Exists(select * from SubjectUser d where d.SubjectID=c.SubjectID and d.UserID="+intUserID+")) or Exists(select * from UserInfo e,DeptInfo f,SubjectUser g where e.UserID="+intUserID+" and e.DeptID=f.DeptID and f.DeptID=g.DeptID and g.SubjectID=c.SubjectID))";
			//显示以科目为条件的数据
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以章名为条件的数据
			if (txtChapterName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.ChapterName='"+txtChapterName.Text.Trim()+"'";
				}
				else
				{
					strSql=strSql+" where b.ChapterName='"+txtChapterName.Text.Trim()+"'";
					bWhere=true;
				}
			}
			//显示以节名为条件的数据
			if (txtSectionName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SectionName like '%"+txtSectionName.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.SectionName like '%"+txtSectionName.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by c.SubjectName,b.ChapterID,a.SectionID";

			DataGridBook.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridBook.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridBook.CurrentPageIndex>0)
			{
				DataGridBook.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridBook.CurrentPageIndex<(DataGridBook.PageCount-1))
			{
				DataGridBook.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridBook.CurrentPageIndex=(DataGridBook.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridBook_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridBook.Columns.Count; i++)
			{
				DataGridBook.Columns[i].HeaderText = (DataGridBook.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridBook.Columns[i].HeaderText = (DataGridBook.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridBook.Columns.Count; i++)
			{
				if (DataGridBook.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridBook.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridBook.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridBook.Attributes["SortExpression"] = SortExpression;
			DataGridBook.Attributes["SortDirection"] = SortDirection;
			if (DataGridBook.Attributes["SortDirection"] == "ASC")
			{
				DataGridBook.Columns[colindex].HeaderText = DataGridBook.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridBook.Columns[colindex].HeaderText = DataGridBook.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
