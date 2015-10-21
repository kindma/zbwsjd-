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
	/// ManagDeptList 的摘要说明。
	/// </summary>
	public partial class ManagDeptList : System.Web.UI.Page
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
					ButNewMoreDept.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('NewMoreDept.aspx','NewMoreDept',502,280); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确实要删除选择部门吗？')==false) return false;}");
					strSql="select * from DeptInfo order by DeptID desc";
					if (DataGridDept.Attributes["SortExpression"] == null)
					{
						DataGridDept.Attributes["SortExpression"] = "DeptID";
						DataGridDept.Attributes["SortDirection"] = "DESC";
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
			this.DataGridDept.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridDept_PageIndexChanged);
			this.DataGridDept.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridDept_CancelCommand);
			this.DataGridDept.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridDept_EditCommand);
			this.DataGridDept.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridDept.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridDept_UpdateCommand);
			this.DataGridDept.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridDept_DeleteCommand);
			this.DataGridDept.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridDept_ItemDataBound);

		}
		#endregion

		#region//*******添加部门名称*******
		protected void ButNewDept_Click(object sender, System.EventArgs e)
		{
			if (txtDeptName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('部门名称不能为空！')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select DeptName from DeptInfo where DeptName='"+ObjFun.getStr(ObjFun.CheckString(txtDeptName.Text.Trim()),20)+"'","DeptName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=new SqlCommand("Insert into DeptInfo(DeptName) values('"+ObjFun.getStr(ObjFun.CheckString(txtDeptName.Text.Trim()),20)+"')",SqlConn);
				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('新建部门名称成功！')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此部门名称已经存在！')</script>");
				return;
			}
			txtDeptName.Text="";
			ShowData(strSql);
		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridDept_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridDept.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridDept_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridDept').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridDept').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridDept.Attributes.Add("oldValue", "#F7F7F7");
				DataGridDept.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******删除选中部门*******
		private void DataGridDept_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intDeptID=Convert.ToInt32(e.Item.Cells[0].Text);
			if (ObjFun.GetValues("select UserID from UserInfo where DeptID="+intDeptID+"","UserID")=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("delete from DeptInfo where DeptID="+intDeptID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//自动翻页
				if (DataGridDept.Items.Count==1&&DataGridDept.CurrentPageIndex>0)
				{
					DataGridDept.CurrentPageIndex--;
				}   
     
				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此部门正在使用，请先删除帐户中相应帐户后再进行此操作！')</script>");
				return;
			}
		}
		#endregion

		#region//*******更新部门名称*******
		private void DataGridDept_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intDeptID=Convert.ToInt32(e.Item.Cells[0].Text.Trim());
			string strName=((TextBox)e.Item.Cells[3].Controls[0]).Text;
			string strTmp=ObjFun.GetValues("select DeptName from DeptInfo where DeptName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"'and DeptID<>"+intDeptID+"","DeptName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("update DeptInfo set DeptName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"' where DeptID="+intDeptID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				DataGridDept.EditItemIndex=-1;
				ShowData(strSql);
				SqlConn.Close();
				SqlConn.Dispose();
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此部门名称已经存在！')</script>");
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
			SqlCmd.Fill(SqlDS,"DeptInfo");
			RowNum=DataGridDept.CurrentPageIndex*DataGridDept.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridDept.Attributes["SortExpression"];
			string SortDirection = DataGridDept.Attributes["SortDirection"];
			SqlDS.Tables["DeptInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridDept.DataSource=SqlDS.Tables["DeptInfo"].DefaultView;
			DataGridDept.DataBind();
			for(int i=0;i<DataGridDept.Items.Count;i++)
			{	
				LinkButton LBUser=(LinkButton)DataGridDept.Items[i].FindControl("LinkButUser");
				LBUser.Attributes.Add("onclick", "var str=window.showModalDialog('SelectDeptUser.aspx?DeptID="+DataGridDept.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");

				LinkButton LBDel=(LinkButton)DataGridDept.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick", "javascript:{if(confirm('确定要删除选择部门吗？')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["DeptInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridDept.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridDept.CurrentPageIndex+1);
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

					if (ObjFun.GetValues("select UserID from UserInfo where DeptID='"+DataGridDept.Items[i].Cells[0].Text.Trim()+"'","UserID")=="")
					{
						SqlCmd=new SqlCommand("delete from DeptInfo where DeptID="+DataGridDept.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//自动翻页
				if(textArray.Length==DataGridDept.Items.Count&&DataGridDept.CurrentPageIndex>0)
				{
					DataGridDept.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridDept_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridDept.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridDept_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridDept.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//显示以名称为条件的数据
			if (txtDeptName.Text.Trim()=="")
			{
				strSql="select * from DeptInfo order by DeptID desc";
			}
			else
			{
				strSql="select * from DeptInfo where DeptName like '%"+ObjFun.CheckString(txtDeptName.Text.Trim())+"%' order by DeptID desc";
			}

			DataGridDept.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridDept.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridDept.CurrentPageIndex>0)
			{
				DataGridDept.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridDept.CurrentPageIndex<(DataGridDept.PageCount-1))
			{
				DataGridDept.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridDept.CurrentPageIndex=(DataGridDept.PageCount-1);
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
			for (int i = 0; i < DataGridDept.Columns.Count; i++)
			{
				DataGridDept.Columns[i].HeaderText = (DataGridDept.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridDept.Columns[i].HeaderText = (DataGridDept.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridDept.Columns.Count; i++)
			{
				if (DataGridDept.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridDept.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridDept.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridDept.Attributes["SortExpression"] = SortExpression;
			DataGridDept.Attributes["SortDirection"] = SortDirection;
			if (DataGridDept.Attributes["SortDirection"] == "ASC")
			{
				DataGridDept.Columns[colindex].HeaderText = DataGridDept.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridDept.Columns[colindex].HeaderText = DataGridDept.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
