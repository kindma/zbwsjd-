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

namespace EasyExam.RubricManag
{
	/// <summary>
	/// ManagTestList 的摘要说明。
	/// </summary>
	public partial class ManagTestList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.DropDownList DropDownList3;
		protected System.Web.UI.WebControls.DropDownList DropDownList4;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.DropDownList DropDownList5;
		protected System.Web.UI.WebControls.DropDownList DropDownList6;
		protected System.Web.UI.WebControls.DropDownList DropDownList7;
		protected System.Web.UI.WebControls.DropDownList DropDownList8;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
	
		#region//*******初始化信息********
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
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ShowSubjectInfo();//显示科目信息
					DDLSubjectName.Items.FindByText("--全部--").Selected=true;
					ShowTestTypeInfo();//显示题型名称
					DDLTestTypeName.Items.FindByText("--全部--").Selected=true;

					ButNewTest.Attributes.Add("onclick","javascript:jscomNewOpenBySize('NewTest.aspx','NewTest',688,496); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确实要删除选择试题吗？')==false) return false;}");
					strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID order by a.RubricID desc";
					if (DataGridTest.Attributes["SortExpression"] == null)
					{
						DataGridTest.Attributes["SortExpression"] = "RubricID";
						DataGridTest.Attributes["SortDirection"] = "DESC";
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

		#region//*********显示科目信息**********
		private void ShowSubjectInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from SubjectInfo order by SubjectID",SqlConn);
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

		#region//*********显示知识点信息**********
		private void ShowLoreInfo(int SubjectID)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from LoreInfo where SubjectID="+SubjectID+" order by LoreID",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"LoreInfo");
			DDLLoreName.Items.Clear();
			DDLLoreName.DataSource=SqlDS.Tables["LoreInfo"].DefaultView;
			DDLLoreName.DataTextField="LoreName";
			DDLLoreName.DataValueField="LoreID";
			DDLLoreName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--全部--","0");
			DDLLoreName.Items.Add(strTmp);
		}
		#endregion

		#region//*********显示题型名称**********
		private void ShowTestTypeInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from TestTypeInfo order by TestTypeID",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"TestTypeInfo");
			DDLTestTypeName.Items.Clear();
			DDLTestTypeName.DataSource=SqlDS.Tables["TestTypeInfo"].DefaultView;
			DDLTestTypeName.DataTextField="TestTypeName";
			DDLTestTypeName.DataValueField="TestTypeID";
			DDLTestTypeName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--全部--","0");
			DDLTestTypeName.Items.Add(strTmp);
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
			this.DataGridTest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridTest_PageIndexChanged);
			this.DataGridTest.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridTest_SortCommand);
			this.DataGridTest.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTest_DeleteCommand);
			this.DataGridTest.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridTest_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridTest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridTest.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridTest_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTest').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTest').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridTest.Attributes.Add("oldValue", "#F7F7F7");
				DataGridTest.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******删除选中试题*******
		private void DataGridTest_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intRubricID=Convert.ToInt32(e.Item.Cells[0].Text);
			if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==e.Item.Cells[7].Text.Trim().ToUpper()))
			{
				if (ObjFun.GetValues("select PaperTestID from PaperTest where RubricID="+intRubricID+"","PaperTestID")=="")
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand SqlCmd=new SqlCommand("delete from RubricInfo where RubricID="+intRubricID+"",SqlConn);
					SqlConn.Open();
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
					SqlConn.Dispose();

					//自动翻页
					if (DataGridTest.Items.Count==1&&DataGridTest.CurrentPageIndex>0)
					{
						DataGridTest.CurrentPageIndex--;
					}        

					ShowData(strSql);
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此试题正在使用，请先删除使用此试题的试卷后再进行此操作！')</script>");
					return;
				}
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('对不起，您没有此操作权限！')</script>");
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
			SqlCmd.Fill(SqlDS,"RubricInfo");
			RowNum=DataGridTest.CurrentPageIndex*DataGridTest.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridTest.Attributes["SortExpression"];
			string SortDirection = DataGridTest.Attributes["SortDirection"];
			SqlDS.Tables["RubricInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTest.DataSource=SqlDS.Tables["RubricInfo"].DefaultView;
			DataGridTest.DataBind();
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				Label labTestContent=(Label)DataGridTest.Items[i].FindControl("labTestContent");
				DataGridTest.Items[i].Cells[6].ToolTip=labTestContent.Text;
				labTestContent.Text=Server.HtmlEncode(labTestContent.Text);
				if (labTestContent.Text.Trim().Length>20)
				{
					labTestContent.Text=labTestContent.Text.Trim().Substring(0,20)+"...";
				}
				//labTestContent.Text=ObjFun.getStr(labTestContent.Text.Trim(),20)+"...";

				LinkButton LBEditTest=(LinkButton)DataGridTest.Items[i].FindControl("LinkButEditTest");
				LinkButton LBDel=(LinkButton)DataGridTest.Items[i].FindControl("LinkButDel");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridTest.Items[i].Cells[7].Text.Trim().ToUpper()))
				{
					LBEditTest.Attributes.Add("onclick", "javascript:jscomNewOpenBySize('EditTest.aspx?RubricID="+DataGridTest.Items[i].Cells[0].Text+"','EditTest',688,496); return false;");
					LBDel.Attributes.Add("onclick", "javascript:{if(confirm('确定要删除选择试题吗？')==false) return false;}");
				}
				else
				{
					LBEditTest.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["RubricInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridTest.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridTest.CurrentPageIndex+1);
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

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridTest.Items[i].Cells[7].Text.Trim().ToUpper()))
					{
						if (ObjFun.GetValues("select PaperTestID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim()+"","PaperTestID")=="")
						{
							SqlCmd=new SqlCommand("delete from RubricInfo where RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim(),SqlConn);
							SqlCmd.ExecuteNonQuery();
						}
					}

				}
				//自动翻页
				if(textArray.Length==DataGridTest.Items.Count&&DataGridTest.CurrentPageIndex>0)
				{
					DataGridTest.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******取消编辑信息*******
		private void DataGridTest_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridTest_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=false;
			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID";
			//显示以科目为条件的数据
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以知识点为条件的数据
			if (DDLLoreName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以题型为条件的数据
			if (DDLTestTypeName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以难度为条件的数据
			if (DDLTestDiff.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以试题内容为条件的数据
			if (txtTestContent.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.RubricID desc";

			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******选择发生改变*******
		protected void DDLSubjectName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
			DDLLoreName.Items.FindByText("--全部--").Selected=true;
		}
		#endregion

		#region//*******导出信息*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			if (myLoginID.Trim().ToUpper()=="ADMIN")
			{
				bWhere=false;
				strSql="select b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,a.OptionContent,a.StandardAnswer,a.TestParse from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID";
			}
			else
			{
				bWhere=true;
				strSql="select b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,a.OptionContent,a.StandardAnswer,a.TestParse from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID where a.CreateUserID="+Convert.ToInt32(myUserID)+"";
			}
			//显示以科目为条件的数据
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以知识点为条件的数据
			if (DDLLoreName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以题型为条件的数据
			if (DDLTestTypeName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以难度为条件的数据
			if (DDLTestDiff.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//显示以试题内容为条件的数据
			if (txtTestContent.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.RubricID desc";

			//导出到Excel文件
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");
			if (SqlDS.Tables["RubricInfo"].Rows.Count!=0)
			{
				//准备文件
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"ExportTest.xls",Server.MapPath("..\\UpLoadFiles\\")+"ExportTest.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["RubricInfo"],Server.MapPath("..\\UpLoadFiles\\")+"ExportTest.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('记录为空，不能导出！')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//下载文件
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=ExportTest.xls");
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex>0)
			{
				DataGridTest.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex<(DataGridTest.PageCount-1))
			{
				DataGridTest.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=(DataGridTest.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridTest_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridTest.Columns.Count; i++)
			{
				DataGridTest.Columns[i].HeaderText = (DataGridTest.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridTest.Columns[i].HeaderText = (DataGridTest.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridTest.Columns.Count; i++)
			{
				if (DataGridTest.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridTest.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridTest.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridTest.Attributes["SortExpression"] = SortExpression;
			DataGridTest.Attributes["SortDirection"] = SortDirection;
			if (DataGridTest.Attributes["SortDirection"] == "ASC")
			{
				DataGridTest.Columns[colindex].HeaderText = DataGridTest.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridTest.Columns[colindex].HeaderText = DataGridTest.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
