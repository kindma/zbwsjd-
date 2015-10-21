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
	/// BrowNewsList 的摘要说明。
	/// </summary>
	public partial class BrowNewsList : System.Web.UI.Page
	{
		protected int RowNum=0;

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intUserID=0;
	
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

			strSql="select a.NewsID,a.NewsTitle,a.NewsContent,a.BrowNumber,a.CreateDate,b.LoginID as CreateLoginID from NewsInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where (a.BrowAccount=1 or (a.BrowAccount=2 and Exists(select * from NewsUser c where c.NewsID=a.NewsID and c.UserID="+intUserID+")) or Exists(select * from UserInfo d,DeptInfo e,NewsUser f where d.UserID="+intUserID+" and d.DeptID=e.DeptID and e.DeptID=f.DeptID and f.NewsID=a.NewsID)) order by a.NewsID desc";
			if (!IsPostBack)
			{
				if (DataGridNews.Attributes["SortExpression"] == null)
				{
					DataGridNews.Attributes["SortExpression"] = "NewsID";
					DataGridNews.Attributes["SortDirection"] = "DESC";
				}
				ShowData(strSql);
			}
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
			this.DataGridNews.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridNews_PageIndexChanged);
			this.DataGridNews.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridNews_SortCommand);
			this.DataGridNews.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridNews_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridNews_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridNews.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridNews_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridNews').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridNews').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridNews.Attributes.Add("oldValue", "#F7F7F7");
				DataGridNews.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"NewsInfo");
			RowNum=DataGridNews.CurrentPageIndex*DataGridNews.PageSize+1;

			string SortExpression = DataGridNews.Attributes["SortExpression"];
			string SortDirection = DataGridNews.Attributes["SortDirection"];
			SqlDS.Tables["NewsInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridNews.DataSource=SqlDS.Tables["NewsInfo"].DefaultView;
			DataGridNews.DataBind();
			for(int i=0;i<DataGridNews.Items.Count;i++)
			{				
				LinkButton LBNewsTitle=(LinkButton)DataGridNews.Items[i].FindControl("LinkButNewsTitle");
				LBNewsTitle.Text=Server.HtmlEncode(LBNewsTitle.Text);
				if (LBNewsTitle.Text.Trim().Length>30)
				{
					LBNewsTitle.Text=LBNewsTitle.Text.Trim().Substring(0,30)+"...";
				}

				LBNewsTitle.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('BrowNews.aspx?NewsID="+DataGridNews.Items[i].Cells[0].Text+"','BrowNews',458,323); return false;");

				Label labCreateDate=(Label)DataGridNews.Items[i].FindControl("labCreateDate");
				labCreateDate.Text=Convert.ToDateTime(labCreateDate.Text).ToString("d");

				LinkButton LBBrowNews=(LinkButton)DataGridNews.Items[i].FindControl("LinkButBrowNews");
				LBBrowNews.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('BrowNews.aspx?NewsID="+DataGridNews.Items[i].Cells[0].Text+"','BrowNews',458,323); return false;");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["NewsInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridNews.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridNews.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridNews_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridNews.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridNews_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridNews.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridNews.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridNews.CurrentPageIndex>0)
			{
				DataGridNews.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridNews.CurrentPageIndex<(DataGridNews.PageCount-1))
			{
				DataGridNews.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridNews.CurrentPageIndex=(DataGridNews.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridNews_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridNews.Columns.Count; i++)
			{
				DataGridNews.Columns[i].HeaderText = (DataGridNews.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridNews.Columns[i].HeaderText = (DataGridNews.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridNews.Columns.Count; i++)
			{
				if (DataGridNews.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridNews.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridNews.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridNews.Attributes["SortExpression"] = SortExpression;
			DataGridNews.Attributes["SortDirection"] = SortDirection;
			if (DataGridNews.Attributes["SortDirection"] == "ASC")
			{
				DataGridNews.Columns[colindex].HeaderText = DataGridNews.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridNews.Columns[colindex].HeaderText = DataGridNews.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
