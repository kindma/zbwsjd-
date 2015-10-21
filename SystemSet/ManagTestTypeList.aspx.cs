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
	/// ManagTestTypeList 的摘要说明。
	/// </summary>
	public partial class ManagTestTypeList : System.Web.UI.Page
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
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确实要删除选择题型吗？')==false) return false;}");
					strSql="select * from TestTypeInfo order by TestTypeID desc";
					if (DataGridTestType.Attributes["SortExpression"] == null)
					{
						DataGridTestType.Attributes["SortExpression"] = "TestTypeID";
						DataGridTestType.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
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
			SqlCmd.Fill(SqlDS,"TestTypeInfo");
			RowNum=DataGridTestType.CurrentPageIndex*DataGridTestType.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridTestType.Attributes["SortExpression"];
			string SortDirection = DataGridTestType.Attributes["SortDirection"];
			SqlDS.Tables["TestTypeInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTestType.DataSource=SqlDS.Tables["TestTypeInfo"].DefaultView;
			DataGridTestType.DataBind();
			for(int i=0;i<DataGridTestType.Items.Count;i++)
			{
				Label labUseState=(Label)DataGridTestType.Items[i].FindControl("labUseState");
				if (ObjFun.GetValues("select TestTypeID from RubricInfo where TestTypeID="+DataGridTestType.Items[i].Cells[0].Text.Trim()+"","TestTypeID")!="")
				{
					labUseState.Text="已使用";
				}
				else
				{
					labUseState.Text="未使用";
				}

				LinkButton LBDel=(LinkButton)DataGridTestType.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择题型吗？')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["TestTypeInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridTestType.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridTestType.CurrentPageIndex+1);
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
			this.DataGridTestType.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridTestType_PageIndexChanged);
			this.DataGridTestType.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTestType_CancelCommand);
			this.DataGridTestType.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTestType_EditCommand);
			this.DataGridTestType.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridTestType.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTestType_UpdateCommand);
			this.DataGridTestType.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTestType_DeleteCommand);
			this.DataGridTestType.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridTestType_ItemDataBound);

		}
		#endregion

		#region//*******添加题型名称*******	
		protected void ButNewDept_Click(object sender, System.EventArgs e)
		{
			if (txtTestTypeName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('题型名称不能为空！')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select TestTypeName from TestTypeInfo where TestTypeName='"+ObjFun.getStr(ObjFun.CheckString(txtTestTypeName.Text.Trim()),20)+"'","TestTypeName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=new SqlCommand("Insert into TestTypeInfo(TestTypeName,BaseTestType) values('"+ObjFun.getStr(ObjFun.CheckString(txtTestTypeName.Text.Trim()),20)+"','"+DDLBaseTestType.SelectedItem.Value+"')",SqlConn);
				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('添加题型名称成功！')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此题型名称已经存在！')</script>");
				return;
			}
			txtTestTypeName.Text="";
			ShowData(strSql);
		}
		#endregion

		#region//*******更新题型名称*******
		private void DataGridTestType_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intTestTypeID=Convert.ToInt32(e.Item.Cells[0].Text.Trim());
			string strName=((TextBox)e.Item.Cells[3].Controls[0]).Text;
			DropDownList DDLTestType=(DropDownList)e.Item.FindControl("DDLTestType");
			string strBaseTestType=DDLTestType.SelectedItem.Value;
			string strTmp=ObjFun.GetValues("select TestTypeName from TestTypeInfo where TestTypeName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"'and TestTypeID<>"+intTestTypeID+"","TestTypeName");
			if (strTmp=="")
			{
				if ((strBaseTestType!=e.Item.Cells[8].Text.Trim())&&(ObjFun.GetValues("select TestTypeID from RubricInfo where TestTypeID="+intTestTypeID+"","TestTypeID")!=""))
				{
					strBaseTestType=e.Item.Cells[8].Text.Trim();
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此题型正在使用，请先删除题库中相应试题后再进行此操作！')</script>");
				}
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("update TestTypeInfo set TestTypeName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"',BaseTestType='"+strBaseTestType+"' where TestTypeID="+intTestTypeID,SqlConn);
				SqlCmd.ExecuteNonQuery();
				DataGridTestType.EditItemIndex=-1;
				ShowData(strSql);
				SqlConn.Close();
				SqlConn.Dispose();
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此题型名称已经存在！')</script>");
				return;
			}
		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridTestType_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridTestType.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridTestType_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTestType').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTestType').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridTestType.Attributes.Add("oldValue", "#F7F7F7");
				DataGridTestType.Attributes.Add("singleValue", "#FFFFFF");
			}

			if (e.Item.ItemType == ListItemType.EditItem)
			{
				DropDownList DDLTestType=(DropDownList)e.Item.FindControl("DDLTestType");
				DDLTestType.Items.Clear();
				DDLTestType.Items.Add(new ListItem("单选类","单选类"));
				DDLTestType.Items.Add(new ListItem("多选类","多选类"));
				DDLTestType.Items.Add(new ListItem("判断类","判断类"));
				DDLTestType.Items.Add(new ListItem("填空类","填空类"));
				DDLTestType.Items.Add(new ListItem("问答类","问答类"));
				DDLTestType.Items.Add(new ListItem("作文类","作文类"));
				DDLTestType.Items.Add(new ListItem("打字类","打字类"));
				DDLTestType.Items.Add(new ListItem("操作类","操作类"));
				DDLTestType.SelectedIndex=DDLTestType.Items.IndexOf(DDLTestType.Items.FindByValue(e.Item.Cells[8].Text));
			}

		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridTestType_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTestType.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******删除选中题型*******
		private void DataGridTestType_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intTestTypeID=Convert.ToInt32(e.Item.Cells[0].Text);
			if (ObjFun.GetValues("select TestTypeID from RubricInfo where TestTypeID="+intTestTypeID+"","TestTypeID")!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此题型正在使用，请先删除题库中相应试题后再进行此操作！')</script>");
				return;
			}
			else
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("delete from TestTypeInfo where TestTypeID="+intTestTypeID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//自动翻页
				if (DataGridTestType.Items.Count==1&&DataGridTestType.CurrentPageIndex>0)
				{
					DataGridTestType.CurrentPageIndex--;
				}        

				ShowData(strSql);
			}
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridTestType_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTestType.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//显示以名称为条件的数据
			if (txtTestTypeName.Text.Trim()=="")
			{
				strSql="select * from TestTypeInfo order by TestTypeID desc";
			}
			else
			{
				strSql="select * from TestTypeInfo where TestTypeName like '%"+ObjFun.CheckString(txtTestTypeName.Text.Trim())+"%' order by TestTypeID desc";
			}

			DataGridTestType.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
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

					if (ObjFun.GetValues("select TestTypeID from RubricInfo where TestTypeID="+DataGridTestType.Items[i].Cells[0].Text.Trim()+"","TestTypeID")=="")
					{
						SqlCmd=new SqlCommand("delete from TestTypeInfo where TestTypeID="+DataGridTestType.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//自动翻页
				if(textArray.Length==DataGridTestType.Items.Count&&DataGridTestType.CurrentPageIndex>0)
				{
					DataGridTestType.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridTestType.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTestType.CurrentPageIndex>0)
			{
				DataGridTestType.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTestType.CurrentPageIndex<(DataGridTestType.PageCount-1))
			{
				DataGridTestType.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridTestType.CurrentPageIndex=(DataGridTestType.PageCount-1);
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
			for (int i = 0; i < DataGridTestType.Columns.Count; i++)
			{
				DataGridTestType.Columns[i].HeaderText = (DataGridTestType.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridTestType.Columns[i].HeaderText = (DataGridTestType.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridTestType.Columns.Count; i++)
			{
				if (DataGridTestType.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridTestType.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridTestType.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridTestType.Attributes["SortExpression"] = SortExpression;
			DataGridTestType.Attributes["SortDirection"] = SortDirection;
			if (DataGridTestType.Attributes["SortDirection"] == "ASC")
			{
				DataGridTestType.Columns[colindex].HeaderText = DataGridTestType.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridTestType.Columns[colindex].HeaderText = DataGridTestType.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
