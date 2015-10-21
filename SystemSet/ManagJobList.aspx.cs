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
	/// ManagJobList 的摘要说明。
	/// </summary>
	public partial class ManagJobList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

		#region//*******初始化信息********
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
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and LoginID='Admin'","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ButNewMoreJob.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('NewMoreJob.aspx','NewMoreJob',502,280); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确实要删除选择职务吗？')==false) return false;}");
					strSql="select * from JobInfo order by JobID desc";
					if (DataGridJob.Attributes["SortExpression"] == null)
					{
						DataGridJob.Attributes["SortExpression"] = "JobID";
						DataGridJob.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//显示数据
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
			this.DataGridJob.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridJob_PageIndexChanged);
			this.DataGridJob.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridJob_CancelCommand);
			this.DataGridJob.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridJob_EditCommand);
			this.DataGridJob.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridJob.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridJob_UpdateCommand);
			this.DataGridJob.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridJob_DeleteCommand);
			this.DataGridJob.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridJob_ItemDataBound);

		}
		#endregion

		#region//*******添加职务名称*******
		protected void ButNewDept_Click(object sender, System.EventArgs e)
		{
			if (txtJobName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('职务名称不能为空！')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select JobName from JobInfo where JobName='"+ObjFun.getStr(ObjFun.CheckString(txtJobName.Text.Trim()),20)+"'","JobName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=new SqlCommand("Insert into JobInfo(JobName) values('"+ObjFun.getStr(ObjFun.CheckString(txtJobName.Text.Trim()),20)+"')",SqlConn);
				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('新建职务名称成功！')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此职务名称已经存在！')</script>");
				return;
			}
			txtJobName.Text="";
			ShowData(strSql);
		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridJob_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridJob.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridJob_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridJob').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridJob').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridJob.Attributes.Add("oldValue", "#F7F7F7");
				DataGridJob.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******删除选中职务*******
		private void DataGridJob_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intJobID=Convert.ToInt32(e.Item.Cells[0].Text);
			if (ObjFun.GetValues("select UserID from UserInfo where JobID="+intJobID+"","UserID")=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("delete from JobInfo where JobID="+intJobID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//自动翻页
				if (DataGridJob.Items.Count==1&&DataGridJob.CurrentPageIndex>0)
				{
					DataGridJob.CurrentPageIndex--;
				}        
				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此职务正在使用，请先删除帐户中相应帐户后再进行此操作！')</script>");
				return;
			}
		}
		#endregion

		#region//*******更新职务名称*******
		private void DataGridJob_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intJobID=Convert.ToInt32(e.Item.Cells[0].Text.Trim());
			string strName=((TextBox)e.Item.Cells[3].Controls[0]).Text;
			string strTmp=ObjFun.GetValues("select JobName from JobInfo where JobName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"'and JobID<>"+intJobID+"","JobName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("update JobInfo set JobName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"' where JobID="+intJobID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				DataGridJob.EditItemIndex=-1;
				ShowData(strSql);
				SqlConn.Close();
				SqlConn.Dispose();
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此职务名称已经存在！')</script>");
				return;
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
			SqlCmd.Fill(SqlDS,"JobInfo");
			RowNum=DataGridJob.CurrentPageIndex*DataGridJob.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridJob.Attributes["SortExpression"];
			string SortDirection = DataGridJob.Attributes["SortDirection"];
			SqlDS.Tables["JobInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridJob.DataSource=SqlDS.Tables["JobInfo"].DefaultView;
			DataGridJob.DataBind();
			for(int i=0;i<DataGridJob.Items.Count;i++)
			{	
				LinkButton LBUser=(LinkButton)DataGridJob.Items[i].FindControl("LinkButUser");
				LBUser.Attributes.Add("onclick", "var str=window.showModalDialog('SelectJobUser.aspx?JobID="+DataGridJob.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");

				LinkButton LBDel=(LinkButton)DataGridJob.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick", "javascript:{if(confirm('确定要删除选择职务吗？')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["JobInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridJob.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridJob.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******删除所有信息*******
		protected void ButDelete_Click(object sender, System.EventArgs e)
		{
			if (Request["chkSelect"]!=null)
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=null;
				int i=0;
				string[] textArray=Request["chkSelect"].ToString().Split(',');
				for (int j=0;j<textArray.Length;j++)
				{
					i=Convert.ToInt32(textArray[j]);
					if (ObjFun.GetValues("select UserID from UserInfo where JobID='"+DataGridJob.Items[i].Cells[0].Text.Trim()+"'","UserID")=="")
					{
						SqlCmd=new SqlCommand("delete from JobInfo where JobID="+DataGridJob.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//自动翻页
				if(textArray.Length==DataGridJob.Items.Count&&DataGridJob.CurrentPageIndex>0)
				{
					DataGridJob.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridJob_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridJob.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridJob_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridJob.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//显示以名称为条件的数据
			if (txtJobName.Text.Trim()=="")
			{
				strSql="select * from JobInfo order by JobID desc";
			}
			else
			{
				strSql="select * from JobInfo where JobName like '%"+ObjFun.CheckString(txtJobName.Text.Trim())+"%' order by JobID desc";
			}

			DataGridJob.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridJob.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridJob.CurrentPageIndex>0)
			{
				DataGridJob.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridJob.CurrentPageIndex<(DataGridJob.PageCount-1))
			{
				DataGridJob.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridJob.CurrentPageIndex=(DataGridJob.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridUser_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridJob.Columns.Count; i++)
			{
				DataGridJob.Columns[i].HeaderText = (DataGridJob.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridJob.Columns[i].HeaderText = (DataGridJob.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridJob.Columns.Count; i++)
			{
				if (DataGridJob.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridJob.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridJob.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridJob.Attributes["SortExpression"] = SortExpression;
			DataGridJob.Attributes["SortDirection"] = SortDirection;
			if (DataGridJob.Attributes["SortDirection"] == "ASC")
			{
				DataGridJob.Columns[colindex].HeaderText = DataGridJob.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridJob.Columns[colindex].HeaderText = DataGridJob.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
