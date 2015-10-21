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

namespace EasyExam.PaperManag
{
	/// <summary>
	/// AddCustomPolicy 的摘要说明。
	/// </summary>
	public partial class AddCustomPolicy : System.Web.UI.Page
	{

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
	
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
			//清除缓存
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=4)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ShowSubjectInfo();//显示科目信息
					DDLSubjectName.Items.FindByText("--请选择--").Selected=true;
					ShowTestTypeInfo();//显示题型名称
					DDLTestTypeName.Items.FindByText("--请选择--").Selected=true;
					strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
					if (DataGridTest.Attributes["SortExpression"] == null)
					{
						DataGridTest.Attributes["SortExpression"] = "RubricID";
						DataGridTest.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
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
			ListItem strTmp=new ListItem("--请选择--","0");
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
			ListItem strTmp=new ListItem("--请选择--","0");
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
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLTestTypeName.Items.Add(strTmp);

		}
		#endregion

		#region//*******科目选择发生改变*******
		protected void DDLSubjectName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
			DDLLoreName.Items.FindByText("--请选择--").Selected=true;
			ShowTestTypeInfo();
			DDLTestTypeName.Items.FindByText("--请选择--").Selected=true;

			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******知识点选择发生改变*******
		protected void DDLLoreName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowTestTypeInfo();
			DDLTestTypeName.Items.FindByText("--请选择--").Selected=true;

			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******题型选择发生改变*******
		protected void DDLTestTypeName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
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

		#region//*******显示数据列表*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");

			string SortExpression = DataGridTest.Attributes["SortExpression"];
			string SortDirection = DataGridTest.Attributes["SortDirection"];
			SqlDS.Tables["RubricInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTest.DataSource=SqlDS.Tables["RubricInfo"].DefaultView;
			DataGridTest.DataBind();
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				CheckBox chkCheckBoxSel=(CheckBox)DataGridTest.Items[i].FindControl("CheckBoxSel");
			    string strTmp=ObjFun.GetValues("select RubricID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"","RubricID");
				if (strTmp!="")
				{
					chkCheckBoxSel.Checked=true;
				}

				Label labTestContent=(Label)DataGridTest.Items[i].FindControl("labTestContent");
				DataGridTest.Items[i].Cells[7].ToolTip=labTestContent.Text;
				labTestContent.Text=Server.HtmlEncode(labTestContent.Text);
				if (labTestContent.Text.Trim().Length>20)
				{
					labTestContent.Text=labTestContent.Text.Trim().Substring(0,20)+"...";
				}
				//labTestContent.Text=ObjFun.getStr(labTestContent.Text.Trim(),20)+"...";
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["RubricInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridTest.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridTest.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridTest_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
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

		#region//*******选定试题*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
//			if (ObjFun.JoySoftware()==false)
//			{
//				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('对不起，未注册用户不能添加策略！')</script>");
//				return;
//			}
			if (DDLSubjectName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择科目名称！')</script>");
				return;
			}
			if (DDLLoreName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择知识点！')</script>");
				return;
			}
			if (DDLTestTypeName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择题型名称！')</script>");
				return;
			}
			//保存到数据库
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlConnection ObjConn=new SqlConnection(strConn);
			ObjConn.Open();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Connection=ObjConn;
			//保存试题策略
			if (ObjFun.GetValues("select PaperPolicyID from PaperPolicy where SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and LoreID='"+DDLLoreName.SelectedItem.Value+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"' and PaperID='"+intPaperID+"'","PaperPolicyID")=="")
			{
				ObjCmd.CommandText="Insert into PaperPolicy(PaperID,SubjectID,LoreID,TestTypeID,TestDiff1,TestDiff2,TestDiff3,TestDiff4,TestDiff5) values('"+intPaperID+"','"+DDLSubjectName.SelectedItem.Value+"','"+DDLLoreName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Value+"',0,0,0,0,0)";
				ObjCmd.ExecuteNonQuery();
			}
			if (ObjFun.GetValues("select PaperTestTypeID from PaperTestType where PaperID='"+intPaperID+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'","PaperTestTypeID")=="")
			{
				ObjCmd.CommandText="Insert into PaperTestType(PaperID,TestTypeID,TestTypeTitle,TestTypeMark,TestAmount) values('"+intPaperID+"','"+DDLTestTypeName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Text+"',0,0)";
				ObjCmd.ExecuteNonQuery();
			}
			//生成试题
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				CheckBox chkCheckBoxSel=(CheckBox)DataGridTest.Items[i].FindControl("CheckBoxSel");
				if (chkCheckBoxSel.Checked==true)
				{
					string strTmp=ObjFun.GetValues("select RubricID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"","RubricID");
					if (strTmp=="")
					{
						ObjCmd.CommandText="Insert into PaperTest(PaperID,RubricID,TestMark) values('"+intPaperID+"','"+DataGridTest.Items[i].Cells[1].Text+"','"+DataGridTest.Items[i].Cells[6].Text+"')";
						ObjCmd.ExecuteNonQuery();
					}
				}
				else
				{
					ObjCmd.CommandText="delete from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"";
					ObjCmd.ExecuteNonQuery();
				}
			}
			//更新题型题量
			ObjCmd.CommandText="Update PaperTestType set TestAmount="+Convert.ToInt32(ObjFun.GetValues("select Count(*) as TestCount from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"","TestCount"))+" where TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"";
			ObjCmd.ExecuteNonQuery();

			ObjConn.Close();
			ObjConn.Dispose();

			this.RegisterStartupScript("newWindow","<script language='javascript'>alert('添加试题策略成功！');</script>");
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
