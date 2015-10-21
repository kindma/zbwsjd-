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
	/// ManagLoreList 的摘要说明。
	/// </summary>
	public partial class ManagLoreList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSubjectID=0;
		string strSubjectName="";
	
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
			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			strSubjectName=Convert.ToString(Request["SubjectName"]);
			labSubject.Text=Convert.ToString(Request["SubjectName"]);
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
					if (intSubjectID!=0)
					{
						ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确实要删除选择知识点吗？')==false) return false;}");
						strSql="select * from LoreInfo where SubjectID="+intSubjectID+" order by LoreID desc";
						if (DataGridLore.Attributes["SortExpression"] == null)
						{
							DataGridLore.Attributes["SortExpression"] = "LoreID";
							DataGridLore.Attributes["SortDirection"] = "DESC";
						}
						ShowData(strSql);
						LabCondition.Text=strSql;
					}
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
			SqlCmd.Fill(SqlDS,"LoreInfo");
			RowNum=DataGridLore.CurrentPageIndex*DataGridLore.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridLore.Attributes["SortExpression"];
			string SortDirection = DataGridLore.Attributes["SortDirection"];
			SqlDS.Tables["LoreInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridLore.DataSource=SqlDS.Tables["LoreInfo"].DefaultView;
			DataGridLore.DataBind();
			for(int i=0;i<DataGridLore.Items.Count;i++)
			{				
				LinkButton LBDel=(LinkButton)DataGridLore.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择知识点吗？')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["LoreInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridLore.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridLore.CurrentPageIndex+1);
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
			this.DataGridLore.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridLore_PageIndexChanged);
			this.DataGridLore.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridLore_CancelCommand);
			this.DataGridLore.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridLore_EditCommand);
			this.DataGridLore.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridLore.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridLore_UpdateCommand);
			this.DataGridLore.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridLore_DeleteCommand);
			this.DataGridLore.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridLore_ItemDataBound);

		}
		#endregion

		#region//*******添加知识点名称*******	
		protected void ButNewDept_Click(object sender, System.EventArgs e)
		{
			if (intSubjectID!=0)
			{
				if (txtLore.Text.Trim()=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('知识点名称不能为空！')</script>");
					return;
				}
				string strTmp=ObjFun.GetValues("select LoreName from LoreInfo where LoreName='"+ObjFun.getStr(ObjFun.CheckString(txtLore.Text.Trim()),20)+"' and SubjectID='"+intSubjectID+"'","LoreName");
				if (strTmp=="")
				{
					strSql="Insert into LoreInfo(LoreName,SubjectID) values('"+ObjFun.getStr(ObjFun.CheckString(txtLore.Text.Trim()),20)+"','"+intSubjectID+"')";
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand SqlCmd=new SqlCommand(strSql,SqlConn);
					SqlConn.Open();
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
					SqlConn.Dispose();
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('添加知识点名称成功！')</script>");
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此知识点名称已经存在！')</script>");
					return;
				}
				txtLore.Text="";
				strSql=LabCondition.Text;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******更新知识点名称*******
		private void DataGridLore_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intLoreID=Convert.ToInt32(e.Item.Cells[0].Text.Trim());
			string strName=((TextBox)e.Item.Cells[3].Controls[0]).Text;
			string strTmp=ObjFun.GetValues("select LoreName from LoreInfo where LoreName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"'and LoreID<>"+intLoreID+"","LoreName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("update LoreInfo set LoreName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"' where LoreID="+intLoreID,SqlConn);
				SqlCmd.ExecuteNonQuery();
				DataGridLore.EditItemIndex=-1;
				ShowData(strSql);
				SqlConn.Close();
				SqlConn.Dispose();
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此知识点名称已经存在！')</script>");
				return;
			}
		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridLore_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridLore.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridLore_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridLore').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridLore').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridLore.Attributes.Add("oldValue", "#F7F7F7");
				DataGridLore.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridLore_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridLore.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******删除选中知识点*******
		private void DataGridLore_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intLoreID=Convert.ToInt32(e.Item.Cells[0].Text);
			if (ObjFun.GetValues("select RubricID from RubricInfo where LoreID="+intLoreID+"","RubricID")=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("delete from LoreInfo where LoreID="+intLoreID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//自动翻页
				if (DataGridLore.Items.Count==1&&DataGridLore.CurrentPageIndex>0)
				{
					DataGridLore.CurrentPageIndex--;
				}   

				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此知识点正在使用，请先删除题库中相应试题后再进行此操作！')</script>");
				return;
			}
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridLore_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridLore.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
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
					if (ObjFun.GetValues("select RubricID from RubricInfo where LoreID="+DataGridLore.Items[i].Cells[0].Text.Trim()+"","RubricID")=="")
					{
						SqlCmd=new SqlCommand("delete from LoreInfo where LoreID="+DataGridLore.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//自动翻页
				if(textArray.Length==DataGridLore.Items.Count&&DataGridLore.CurrentPageIndex>0)
				{
					DataGridLore.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******批量新建知识点*******
		private void ImgButAllAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("NewMoreLore.aspx?SubjectID="+intSubjectID+"&SubjectName="+strSubjectName);
		}

		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//显示以知识点为条件的数据
			if (txtLore.Text.Trim()=="")
			{
				strSql="select * from LoreInfo where SubjectID="+intSubjectID+" order by LoreID desc";
			}
			else
			{
				strSql="select * from LoreInfo where SubjectID="+intSubjectID+" and LoreName like '%"+ObjFun.CheckString(txtLore.Text.Trim())+"%' order by LoreID desc";
			}

			DataGridLore.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridLore.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridLore.CurrentPageIndex>0)
			{
				DataGridLore.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridLore.CurrentPageIndex<(DataGridLore.PageCount-1))
			{
				DataGridLore.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridLore.CurrentPageIndex=(DataGridLore.PageCount-1);
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
			for (int i = 0; i < DataGridLore.Columns.Count; i++)
			{
				DataGridLore.Columns[i].HeaderText = (DataGridLore.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridLore.Columns[i].HeaderText = (DataGridLore.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridLore.Columns.Count; i++)
			{
				if (DataGridLore.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridLore.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridLore.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridLore.Attributes["SortExpression"] = SortExpression;
			DataGridLore.Attributes["SortDirection"] = SortDirection;
			if (DataGridLore.Attributes["SortDirection"] == "ASC")
			{
				DataGridLore.Columns[colindex].HeaderText = DataGridLore.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridLore.Columns[colindex].HeaderText = DataGridLore.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
