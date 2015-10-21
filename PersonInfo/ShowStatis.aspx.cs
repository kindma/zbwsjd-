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
using System.Text;
using System.Text.RegularExpressions;


namespace EasyExam.PersonalInfo
{
	/// <summary>
	/// ShowStatis 的摘要说明。
	/// </summary>
	public partial class ShowStatis : System.Web.UI.Page
	{

		protected string strPaperName="";
		protected string strTestCount="";
		protected string strPaperMark="";
		protected string strPaperContent="";

		protected string strLoginID="";
		protected string strUserName="";
		protected string strExamTime="";
		protected string strPassMark="";
		protected string strTotalMark="";

		protected int intPaperID=0;
		protected int intUserID=0;
		protected int intUserScoreID=0;
		protected int RowNum=0,ImgWidth=0;
		protected string strScoreRate="";

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSeeResult=0;
		string strManageUser="";

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

			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			strManageUser=Convert.ToString(Request["ManageUser"]);

			if (intUserScoreID!=0)
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand ObjCmd=null; 
				SqlDataAdapter SqlCmd=null,SqlCmdTestType=null,SqlCmdTest=null;
				DataSet SqlDS=null,SqlDSTestType=null,SqlDSTest=null;

				SqlCmd=new SqlDataAdapter("select * from UserScore where UserScoreID="+intUserScoreID+" order by UserScoreID asc",SqlConn);
				SqlDS=new DataSet();
				SqlCmd.Fill(SqlDS,"UserScore");
				intPaperID=Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["PaperID"]);
				intUserID=Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["UserID"]);
				strExamTime=SqlDS.Tables["UserScore"].Rows[0]["StartTime"].ToString()+"/"+SqlDS.Tables["UserScore"].Rows[0]["EndTime"].ToString();
				strTotalMark=SqlDS.Tables["UserScore"].Rows[0]["TotalMark"].ToString();

				SqlCmd=new SqlDataAdapter("select * from UserInfo where UserID="+intUserID+" order by UserID asc",SqlConn);
				SqlDS=new DataSet();
				SqlCmd.Fill(SqlDS,"UserInfo");

				strLoginID=SqlDS.Tables["UserInfo"].Rows[0]["LoginID"].ToString();
				strUserName=SqlDS.Tables["UserInfo"].Rows[0]["UserName"].ToString();
				
				SqlCmd=new SqlDataAdapter("select * from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
				SqlDS=new DataSet();
				SqlCmd.Fill(SqlDS,"PaperInfo");
				strPaperName=SqlDS.Tables["PaperInfo"].Rows[0]["PaperName"].ToString();
				strTestCount=SqlDS.Tables["PaperInfo"].Rows[0]["TestCount"].ToString();
				strPaperMark=SqlDS.Tables["PaperInfo"].Rows[0]["PaperMark"].ToString();
				strPassMark=SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"].ToString();
				intSeeResult=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
				if ((intSeeResult==0)&&(strManageUser!="1"))
				{
					ObjFun.Alert("该试卷不允许查看结果！");
				}

				SqlConn.Close();
				SqlConn.Dispose();

				strSql="select c.SubjectName,d.LoreName,count(d.LoreID) as TestCount,round(sum(a.TestMark),1) as TestMark,round(sum(a.UserScore),1) as TotalMark,round(round(sum(a.UserScore),1)/round(sum(a.TestMark),1)*100,1) as Rate from UserAnswer a,RubricInfo b,SubjectInfo c,LoreInfo d where a.RubricID=b.RubricID and b.SubjectID=c.SubjectID and b.LoreID=d.LoreID and a.UserScoreID="+intUserScoreID+" group by c.SubjectName,d.LoreName order by c.SubjectName asc";
				if (!IsPostBack)
				{
					if (DataGridStatis.Attributes["SortExpression"] == null)
					{
						DataGridStatis.Attributes["SortExpression"] = "SubjectName";
						DataGridStatis.Attributes["SortDirection"] = "ASC";
					}
					ShowData(strSql);
				}
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
			SqlCmd.Fill(SqlDS,"UserAnswer");
			RowNum=DataGridStatis.CurrentPageIndex*DataGridStatis.PageSize+1;

			string SortExpression = DataGridStatis.Attributes["SortExpression"];
			string SortDirection = DataGridStatis.Attributes["SortDirection"];
			SqlDS.Tables["UserAnswer"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridStatis.DataSource=SqlDS.Tables["UserAnswer"].DefaultView;
			DataGridStatis.DataBind();
			for(int i=0;i<DataGridStatis.Items.Count;i++)
			{
				System.Web.UI.WebControls.Image imgRate=(System.Web.UI.WebControls.Image)DataGridStatis.Items[i].FindControl("imgRate");
				imgRate.Width=Convert.ToInt32(200*Convert.ToDouble(DataGridStatis.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridStatis.Items[i].Cells[4].Text.Trim()));
				imgRate.ToolTip=Convert.ToString(System.Math.Round(Convert.ToDouble(DataGridStatis.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridStatis.Items[i].Cells[4].Text.Trim())*100,1))+"%";
			}
			SqlConn.Dispose();
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
			this.DataGridStatis.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridStatis_PageIndexChanged);
			this.DataGridStatis.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridStatis_SortCommand);
			this.DataGridStatis.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridStatis_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridStatis_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridStatis.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridStatis_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridStatis').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridStatis').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridStatis.Attributes.Add("oldValue", "#F7F7F7");
				DataGridStatis.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******转到第一页*******
		private void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridStatis.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		private void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridStatis.CurrentPageIndex>0)
			{
				DataGridStatis.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		private void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridStatis.CurrentPageIndex<(DataGridStatis.PageCount-1))
			{
				DataGridStatis.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		private void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridStatis.CurrentPageIndex=(DataGridStatis.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridStatis_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridStatis.Columns.Count; i++)
			{
				DataGridStatis.Columns[i].HeaderText = (DataGridStatis.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridStatis.Columns[i].HeaderText = (DataGridStatis.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridStatis.Columns.Count; i++)
			{
				if (DataGridStatis.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridStatis.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridStatis.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridStatis.Attributes["SortExpression"] = SortExpression;
			DataGridStatis.Attributes["SortDirection"] = SortDirection;
			if (DataGridStatis.Attributes["SortDirection"] == "ASC")
			{
				DataGridStatis.Columns[colindex].HeaderText = DataGridStatis.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridStatis.Columns[colindex].HeaderText = DataGridStatis.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
