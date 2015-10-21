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
	/// EditRandPolicy 的摘要说明。
	/// </summary>
	public partial class EditRandPolicy : System.Web.UI.Page
	{

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
		int intSubjectID=0;
		int intLoreID=0;
		int intTestTypeID=0;
	
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
			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			intLoreID=Convert.ToInt32(Request["LoreID"]);
			intTestTypeID=Convert.ToInt32(Request["TestTypeID"]);
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
					DDLSubjectName.Items.FindByValue(intSubjectID.ToString()).Selected=true;
					ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
					DDLLoreName.Items.FindByValue(intLoreID.ToString()).Selected=true;
					ShowTestTypeInfo();//显示题型名称
					DDLTestTypeName.Items.FindByValue(intTestTypeID.ToString()).Selected=true;
					DDLSubjectName.Enabled=false;
					DDLLoreName.Enabled=false;
					DDLTestTypeName.Enabled=false;
					strSql="select (select count(*) from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+") as TestCount,a.PaperPolicyID,a.SubjectID,b.SubjectName,a.LoreID,c.LoreName,a.TestTypeID,d.TestTypeName,a.TestDiff1,a.TestDiff2,a.TestDiff3,a.TestDiff4,a.TestDiff5 from PaperPolicy a,SubjectInfo b,LoreInfo c,TestTypeInfo d where a.SubjectID=b.SubjectID and a.LoreID=c.LoreID and a.TestTypeID=d.TestTypeID and a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and a.PaperID="+intPaperID+" order by a.PaperPolicyID asc";
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

			strSql="select (select count(*) from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+") as TestCount,a.PaperPolicyID,a.SubjectID,b.SubjectName,a.LoreID,c.LoreName,a.TestTypeID,d.TestTypeName,a.TestDiff1,a.TestDiff2,a.TestDiff3,a.TestDiff4,a.TestDiff5 from PaperPolicy a,SubjectInfo b,LoreInfo c,TestTypeInfo d where a.SubjectID=b.SubjectID and a.LoreID=c.LoreID and a.TestTypeID=d.TestTypeID and a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and a.PaperID="+intPaperID+" order by a.PaperPolicyID asc";
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******知识点选择发生改变*******
		protected void DDLLoreName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowTestTypeInfo();
			DDLTestTypeName.Items.FindByText("--请选择--").Selected=true;

			strSql="select (select count(*) from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+") as TestCount,a.PaperPolicyID,a.SubjectID,b.SubjectName,a.LoreID,c.LoreName,a.TestTypeID,d.TestTypeName,a.TestDiff1,a.TestDiff2,a.TestDiff3,a.TestDiff4,a.TestDiff5 from PaperPolicy a,SubjectInfo b,LoreInfo c,TestTypeInfo d where a.SubjectID=b.SubjectID and a.LoreID=c.LoreID and a.TestTypeID=d.TestTypeID and a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and a.PaperID="+intPaperID+" order by a.PaperPolicyID asc";
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******题型选择发生改变*******
		protected void DDLTestTypeName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			strSql="select (select count(*) from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+") as TestCount,a.PaperPolicyID,a.SubjectID,b.SubjectName,a.LoreID,c.LoreName,a.TestTypeID,d.TestTypeName,a.TestDiff1,a.TestDiff2,a.TestDiff3,a.TestDiff4,a.TestDiff5 from PaperPolicy a,SubjectInfo b,LoreInfo c,TestTypeInfo d where a.SubjectID=b.SubjectID and a.LoreID=c.LoreID and a.TestTypeID=d.TestTypeID and a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and a.PaperID="+intPaperID+" order by a.PaperPolicyID asc";
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
			this.DataGridPolicy.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridPolicy_PageIndexChanged);
			this.DataGridPolicy.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPolicy_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridPolicy_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridPolicy.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridPolicy_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPolicy').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPolicy').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridPolicy.Attributes.Add("oldValue", "#F7F7F7");
				DataGridPolicy.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"PaperPolicy");
			DataGridPolicy.DataSource=SqlDS.Tables["PaperPolicy"].DefaultView;
			DataGridPolicy.DataBind();

			SqlDataAdapter SqlCmdTmp=null;
			DataSet SqlDSTmp=null;
			for(int i=0;i<DataGridPolicy.Items.Count;i++)
			{				
				TextBox strTestDiff1=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff1");
				strTestDiff1.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff1"].ToString();
				SqlCmdTmp=new SqlDataAdapter("select * from RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+"and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='易'",SqlConn);
				SqlDSTmp=new DataSet();
				SqlCmdTmp.Fill(SqlDSTmp,"RubricInfo");
				Label labTestDiff1=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff1");
				labTestDiff1.Text=Convert.ToString(SqlDSTmp.Tables["RubricInfo"].Rows.Count);

				TextBox strTestDiff2=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff2");
				strTestDiff2.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff2"].ToString();
				SqlCmdTmp=new SqlDataAdapter("select * from RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+"and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='较易'",SqlConn);
				SqlDSTmp=new DataSet();
				SqlCmdTmp.Fill(SqlDSTmp,"RubricInfo");
				Label labTestDiff2=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff2");
				labTestDiff2.Text=Convert.ToString(SqlDSTmp.Tables["RubricInfo"].Rows.Count);

				TextBox strTestDiff3=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff3");
				strTestDiff3.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff3"].ToString();
				SqlCmdTmp=new SqlDataAdapter("select * from RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+"and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='中等'",SqlConn);
				SqlDSTmp=new DataSet();
				SqlCmdTmp.Fill(SqlDSTmp,"RubricInfo");
				Label labTestDiff3=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff3");
				labTestDiff3.Text=Convert.ToString(SqlDSTmp.Tables["RubricInfo"].Rows.Count);

				TextBox strTestDiff4=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff4");
				strTestDiff4.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff4"].ToString();
				SqlCmdTmp=new SqlDataAdapter("select * from RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+"and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='较难'",SqlConn);
				SqlDSTmp=new DataSet();
				SqlCmdTmp.Fill(SqlDSTmp,"RubricInfo");
				Label labTestDiff4=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff4");
				labTestDiff4.Text=Convert.ToString(SqlDSTmp.Tables["RubricInfo"].Rows.Count);

				TextBox strTestDiff5=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff5");
				strTestDiff5.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff5"].ToString();
				SqlCmdTmp=new SqlDataAdapter("select * from RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+"and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='难'",SqlConn);
				SqlDSTmp=new DataSet();
				SqlCmdTmp.Fill(SqlDSTmp,"RubricInfo");
				Label labTestDiff5=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff5");
				labTestDiff5.Text=Convert.ToString(SqlDSTmp.Tables["RubricInfo"].Rows.Count);
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridPolicy_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPolicy.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到第一页*******
		private void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridPolicy.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		private void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPolicy.CurrentPageIndex>0)
			{
				DataGridPolicy.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		private void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPolicy.CurrentPageIndex<(DataGridPolicy.PageCount-1))
			{
				DataGridPolicy.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		private void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridPolicy.CurrentPageIndex=(DataGridPolicy.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******选定试题*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
//			if (ObjFun.JoySoftware()==false)
//			{
//				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('对不起，未注册用户不能修改策略！')</script>");
//				return;
//			}
			int intTestDiff=0,intSum=0,intRowID=0,intRubricID=0;
			double dblTestMark=0;
			string str="";
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
			TextBox strTestDiff=null,strTestDiff1=null,strTestDiff2=null,strTestDiff3=null,strTestDiff4=null,strTestDiff5=null;
			Label labTestDiff=null;
			for(int i=0;i<DataGridPolicy.Items.Count;i++)
			{
				for(int j=0;j<5;j++)
				{
					strTestDiff=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff"+Convert.ToString(j+1));
					labTestDiff=(Label)DataGridPolicy.Items[i].FindControl("labTestDiff"+Convert.ToString(j+1));
					try
					{
						intTestDiff=Convert.ToInt32(strTestDiff.Text.Trim());
					}
					catch
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请在试题策略"+Convert.ToString(j+2)+"列中输入正确的题量！')</script>");
						return;
					}
					if ((intTestDiff<0)||(intTestDiff>Convert.ToInt32(labTestDiff.Text)))
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('在试题策略"+Convert.ToString(j+2)+"列中的数字应大于等于0并且小于等于"+labTestDiff.Text+"！')</script>");
						return;
					}
				}
				strTestDiff1=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff1");
				strTestDiff2=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff2");
				strTestDiff3=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff3");
				strTestDiff4=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff4");
				strTestDiff5=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff5");
				if (Convert.ToInt32(strTestDiff1.Text.Trim())+Convert.ToInt32(strTestDiff2.Text.Trim())+Convert.ToInt32(strTestDiff3.Text.Trim())+Convert.ToInt32(strTestDiff4.Text.Trim())+Convert.ToInt32(strTestDiff5.Text.Trim())==0)
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('在试题策略"+Convert.ToString(i+1)+"行中输入的题量应大于0！')</script>");
					return;
				}
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
				ObjCmd.CommandText="Insert into PaperPolicy(PaperID,SubjectID,LoreID,TestTypeID,TestDiff1,TestDiff2,TestDiff3,TestDiff4,TestDiff5) values('"+intPaperID+"','"+DDLSubjectName.SelectedItem.Value+"','"+DDLLoreName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Value+"','"+strTestDiff1.Text.Trim()+"','"+strTestDiff2.Text.Trim()+"','"+strTestDiff3.Text.Trim()+"','"+strTestDiff4.Text.Trim()+"','"+strTestDiff5.Text.Trim()+"')";
				ObjCmd.ExecuteNonQuery();
			}
			else
			{
				ObjCmd.CommandText="Update PaperPolicy set TestDiff1='"+strTestDiff1.Text.Trim()+"',TestDiff2='"+strTestDiff2.Text.Trim()+"',TestDiff3='"+strTestDiff3.Text.Trim()+"',TestDiff4='"+strTestDiff4.Text.Trim()+"',TestDiff5='"+strTestDiff5.Text.Trim()+"' where SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and LoreID='"+DDLLoreName.SelectedItem.Value+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"' and PaperID='"+intPaperID+"'";
				ObjCmd.ExecuteNonQuery();
			}
			if (ObjFun.GetValues("select PaperTestTypeID from PaperTestType where PaperID='"+intPaperID+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'","PaperTestTypeID")=="")
			{
				ObjCmd.CommandText="Insert into PaperTestType(PaperID,TestTypeID,TestTypeTitle,TestTypeMark,TestAmount) values('"+intPaperID+"','"+DDLTestTypeName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Text+"',0,0)";
				ObjCmd.ExecuteNonQuery();
			}
			ObjConn.Close();
			ObjConn.Dispose();

			SqlConn.Open();
			SqlCommand ObjectCmd=new SqlCommand();
			ObjectCmd.Connection=SqlConn;
			SqlDataAdapter SqlCmd=null,SqlCmdTmp=null;
			DataSet SqlDS=null,SqlDSTmp=null;
			//生成试题
			ObjectCmd.CommandText="delete PaperTest from PaperTest a LEFT OUTER JOIN RubricInfo b ON a.RubricID=b.RubricID where b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and b.LoreID='"+DDLLoreName.SelectedItem.Value+"' and b.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"' and a.PaperID="+intPaperID+"";
			ObjectCmd.ExecuteNonQuery();

			SqlCmd=new SqlDataAdapter("select PaperPolicyID,SubjectID,LoreID,TestTypeID,TestDiff1,TestDiff2,TestDiff3,TestDiff4,TestDiff5 from PaperPolicy where SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and LoreID='"+DDLLoreName.SelectedItem.Value+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"' and PaperID="+intPaperID+" order by PaperPolicyID asc",SqlConn);
			SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"PaperPolicy");
			for(int i=0;i<SqlDS.Tables["PaperPolicy"].Rows.Count;i++)
			{
				intSum=Convert.ToInt32(SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff1"]);
				if (intSum>0)
				{
					ObjectCmd.CommandText="INSERT INTO PaperTest(PaperID,RubricID,TestMark) SELECT top "+intSum.ToString()+" "+intPaperID.ToString()+" AS PaperID,RubricID,TestMark FROM RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+" and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='易' ORDER BY NEWID()";
					ObjectCmd.ExecuteNonQuery();
				}

				intSum=Convert.ToInt32(SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff2"]);
				if (intSum>0)
				{
					ObjectCmd.CommandText="INSERT INTO PaperTest(PaperID,RubricID,TestMark) SELECT top "+intSum.ToString()+" "+intPaperID.ToString()+" AS PaperID,RubricID,TestMark FROM RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+" and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='较易' ORDER BY NEWID()";
					ObjectCmd.ExecuteNonQuery();
				}

				intSum=Convert.ToInt32(SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff3"]);
				if (intSum>0)
				{
					ObjectCmd.CommandText="INSERT INTO PaperTest(PaperID,RubricID,TestMark) SELECT top "+intSum.ToString()+" "+intPaperID.ToString()+" AS PaperID,RubricID,TestMark FROM RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+" and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='中等' ORDER BY NEWID()";
					ObjectCmd.ExecuteNonQuery();
				}

				intSum=Convert.ToInt32(SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff4"]);
				if (intSum>0)
				{
					ObjectCmd.CommandText="INSERT INTO PaperTest(PaperID,RubricID,TestMark) SELECT top "+intSum.ToString()+" "+intPaperID.ToString()+" AS PaperID,RubricID,TestMark FROM RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+" and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='较难' ORDER BY NEWID()";
					ObjectCmd.ExecuteNonQuery();
				}

				intSum=Convert.ToInt32(SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff5"]);
				if (intSum>0)
				{
					ObjectCmd.CommandText="INSERT INTO PaperTest(PaperID,RubricID,TestMark) SELECT top "+intSum.ToString()+" "+intPaperID.ToString()+" AS PaperID,RubricID,TestMark FROM RubricInfo where SubjectID="+SqlDS.Tables["PaperPolicy"].Rows[i]["SubjectID"]+" and LoreID="+SqlDS.Tables["PaperPolicy"].Rows[i]["LoreID"]+" and TestTypeID="+SqlDS.Tables["PaperPolicy"].Rows[i]["TestTypeID"]+" and TestDiff='难' ORDER BY NEWID()";
					ObjectCmd.ExecuteNonQuery();
				}
			}
			//更新题型题量
			ObjectCmd.CommandText="Update PaperTestType set TestAmount="+Convert.ToInt32(ObjFun.GetValues("select Count(*) as TestCount from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"","TestCount"))+" where TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"";
			ObjectCmd.ExecuteNonQuery();

			SqlConn.Close();
			SqlConn.Dispose();

			this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改试题策略成功！');window.close();</script>");
		}
		#endregion

	}
}
