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
	/// EditRandPaper 的摘要说明。
	/// </summary>
	public partial class EditRandPaper : System.Web.UI.Page
	{

		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intUserID=0;
		int intPaperTypeID=0,intCreateUserID=0;
		bool bJoySoftware=false;
	
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
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intPaperTypeID=Convert.ToInt32(Request["PaperType"]);
			bJoySoftware=ObjFun.JoySoftware();

			rbSelectAccount.Text="";
			rbSelectManagerAccount.Text="";
			txtStartTime.Attributes["readonly"]="true";
			txtEndTime.Attributes["readonly"]="true";
			txtSelectExam.Attributes["readonly"]="true";
			txtSelectManager.Attributes["readonly"]="true";
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=4)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if ((intPaperID!=0)&&(intPaperTypeID!=0))
					{
//						if (bJoySoftware==false)
//						{
//							ButInput.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//							ButAddPolicy.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//							ButSelectExam.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//							ButSelectManager.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//						}
//						else
//						{
							ButInput.Attributes.Add("onclick", "javascript:submitexam1.style.visibility='visible';return true;");
							ButAddPolicy.Attributes.Add("onclick", "javascript:var str=window.showModalDialog('AddRandPolicy.aspx?PaperID="+intPaperID+"','','dialogHeight:190px;dialogWidth:500px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');");
							ButSelectExam.Attributes.Add("onclick", "javascript:var obj=new Object();obj.name=document.all('txtSelectExam').value;var str=window.showModalDialog('SelectJoinExam.aspx?PaperID="+intPaperID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectExam').value=str; };return false;");
							ButSelectManager.Attributes.Add("onclick", "javascript:var obj=new Object();obj.name=document.all('txtSelectManager').value;var str=window.showModalDialog('SelectJoinManager.aspx?PaperID="+intPaperID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectManager').value=str; };return false;");
//						}
						ShowPaperPolicy();//显示试题策略
						ShowPaperTestType();//显示试题标题
						LoadPaperData();//加载试卷数据
					}
				}
			}
		}
		#endregion

		#region//*********删除关联数据**********
		private void DelRelationData()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlTransaction ObjTran=ObjConn.BeginTransaction();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Transaction=ObjTran;
			ObjCmd.Connection=ObjConn;
			try
			{
				ObjCmd.CommandText="delete from PaperPolicy where PaperID="+intPaperID+"";
				ObjCmd.ExecuteNonQuery();

				ObjCmd.CommandText="delete from PaperTestType where PaperID="+intPaperID+"";
				ObjCmd.ExecuteNonQuery();

				ObjCmd.CommandText="delete from PaperUser where PaperID="+intPaperID+"";
				ObjCmd.ExecuteNonQuery();
				
				ObjCmd.CommandText="delete from PaperTest where PaperID="+intPaperID+"";
				ObjCmd.ExecuteNonQuery();
				ObjTran.Commit();
			}
			catch
			{
				ObjTran.Rollback();
			}
			finally
			{
				ObjConn.Close();
				ObjConn.Dispose();
			}
		}
		#endregion

		#region//******显示试题策略列表******
		private void ShowPaperPolicy()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.PaperPolicyID,a.SubjectID,b.SubjectName,a.LoreID,c.LoreName,a.TestTypeID,d.TestTypeName,a.TestDiff1,a.TestDiff2,a.TestDiff3,a.TestDiff4,a.TestDiff5 from PaperPolicy a,SubjectInfo b,LoreInfo c,TestTypeInfo d where a.SubjectID=b.SubjectID and a.LoreID=c.LoreID and a.TestTypeID=d.TestTypeID and a.PaperID="+intPaperID+" order by a.PaperPolicyID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"PaperPolicy");
			DataGridPolicy.DataSource=SqlDS.Tables["PaperPolicy"].DefaultView;
			DataGridPolicy.DataBind();

			for(int i=0;i<DataGridPolicy.Items.Count;i++)
			{
				TextBox strTestDiff1=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff1");
				strTestDiff1.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff1"].ToString();

				TextBox strTestDiff2=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff2");
				strTestDiff2.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff2"].ToString();

				TextBox strTestDiff3=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff3");
				strTestDiff3.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff3"].ToString();

				TextBox strTestDiff4=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff4");
				strTestDiff4.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff4"].ToString();

				TextBox strTestDiff5=(TextBox)DataGridPolicy.Items[i].FindControl("txtTestDiff5");
				strTestDiff5.Text=SqlDS.Tables["PaperPolicy"].Rows[i]["TestDiff5"].ToString();

				LinkButton LBEdit=(LinkButton)DataGridPolicy.Items[i].FindControl("LinkButEdit");
				LinkButton LBDel=(LinkButton)DataGridPolicy.Items[i].FindControl("LinkButDel");
//				if (bJoySoftware==false)
//				{
//					LBEdit.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//					LBDel.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//				}
//				else
//				{
					LBEdit.Attributes.Add("onclick", "javascript:var str=window.showModalDialog('EditRandPolicy.aspx?PaperID="+intPaperID+"&SubjectID="+DataGridPolicy.Items[i].Cells[10].Text+"&LoreID="+DataGridPolicy.Items[i].Cells[11].Text+"&TestTypeID="+DataGridPolicy.Items[i].Cells[12].Text+"','','dialogHeight:190px;dialogWidth:500px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');");
					LBDel.Attributes.Add("onclick", "javascript:{if(confirm('确定要删除选择策略吗？')==false) return false;}");
//				}
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//******显示试题标题列表******
		private void ShowPaperTestType()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.PaperTestTypeID,a.TestTypeID,b.TestTypeName,b.BaseTestType,a.TestTypeTitle,a.TestTypeMark,a.TestAmount from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.PaperID="+intPaperID+" order by a.PaperTestTypeID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"PaperTestType");
			DataGridTestType.DataSource=SqlDS.Tables["PaperTestType"].DefaultView;
			DataGridTestType.DataBind();
			for(int i=0;i<DataGridTestType.Items.Count;i++)
			{
				TextBox strTestTypeTitle=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeTitle");
				strTestTypeTitle.Text=SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString();

				TextBox strTestTypeMark=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeMark");
				strTestTypeMark.Text=SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeMark"].ToString();

				LinkButton LBMoveUp=(LinkButton)DataGridTestType.Items[i].FindControl("LinkButMoveUp");
				LinkButton LBMoveDown=(LinkButton)DataGridTestType.Items[i].FindControl("LinkButMoveDown");
//				if (bJoySoftware==false)
//				{
//					LBMoveUp.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//					LBMoveDown.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能修改试卷！');return false;");
//				}
			}
			SqlConn.Dispose();

		}
		#endregion

		#region//**********加载要修改的试卷数据*********
		private void LoadPaperData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlConnection SqlConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select * from PaperInfo where PaperID="+intPaperID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				txtPaperName.Text=ObjDR["PaperName"].ToString();
				DDLPaperType.Items.FindByValue(ObjDR["PaperType"].ToString()).Selected=true;
				DDLProduceWay.Items.FindByValue(ObjDR["ProduceWay"].ToString()).Selected=true;
				DDLShowModal.Items.FindByValue(ObjDR["ShowModal"].ToString()).Selected=true;
				txtExamTime.Text=ObjDR["ExamTime"].ToString();
				txtStartTime.Text=ObjDR["StartTime"].ToString();
				txtEndTime.Text=ObjDR["EndTime"].ToString();
				txtPaperMark.Text=ObjDR["PaperMark"].ToString();
				txtPassMark.Text=ObjDR["PassMark"].ToString();
				rbMarkDefine1.Checked=false;
				rbMarkDefine2.Checked=false;
				if (ObjDR["MarkDefine"].ToString()=="1")
				{
					rbMarkDefine1.Checked=true;
				}
				if (ObjDR["MarkDefine"].ToString()=="2")
				{
					rbMarkDefine2.Checked=true;
				}
				if (ObjDR["RepeatExam"].ToString()=="1")
				{
					chkRepeatExam.Checked=true;
				}
				else
				{
					chkRepeatExam.Checked=false;
				}
				if (ObjDR["FillAutoGrade"].ToString()=="1")
				{
					chkFillAutoGrade.Checked=true;
				}
				else
				{
					chkFillAutoGrade.Checked=false;
				}
				if (ObjDR["SeeResult"].ToString()=="1")
				{
					chkSeeResult.Checked=true;
				}
				else
				{
					chkSeeResult.Checked=false;
				}
				if (ObjDR["AutoSave"].ToString()!="0")
				{
					chkAutoSave.Checked=true;
					txtSaveTime.Text=ObjDR["AutoSave"].ToString();
				}
				else
				{
					chkAutoSave.Checked=false;
					txtSaveTime.Text="5";
				}
				rbAllAccount.Checked=false;
				rbSelectAccount.Checked=false;
				if (ObjDR["ExamAccount"].ToString()=="1")
				{
					rbAllAccount.Checked=true;
				}
				SqlDataAdapter SqlCmd=null;
				DataSet SqlDS=null;
				int i=0;
				if (ObjDR["ExamAccount"].ToString()=="2")
				{
					rbSelectAccount.Checked=true;
					txtSelectExam.Text="";

					SqlCmd=new SqlDataAdapter("select b.DeptID,b.DeptName from PaperUser a,DeptInfo b where a.DeptID=b.DeptID and a.UserType=0 and a.PaperID="+intPaperID+" order by a.DeptID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"DeptInfo");
					for(i=0;i<SqlDS.Tables["DeptInfo"].Rows.Count;i++)
					{
						if (txtSelectExam.Text.Trim()=="")
						{
							txtSelectExam.Text=txtSelectExam.Text+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
						else
						{
							txtSelectExam.Text=txtSelectExam.Text+";"+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
					}

					SqlCmd=new SqlDataAdapter("select b.UserID,b.LoginID from PaperUser a,UserInfo b where a.UserID=b.UserID and a.UserType=0 and a.PaperID="+intPaperID+" order by a.UserID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"UserInfo");
					for(i=0;i<SqlDS.Tables["UserInfo"].Rows.Count;i++)
					{
						if (txtSelectExam.Text.Trim()=="")
						{
							txtSelectExam.Text=txtSelectExam.Text+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
						else
						{
							txtSelectExam.Text=txtSelectExam.Text+";"+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
					}
				}
				rbAllManagerAccount.Checked=false;
				rbSelectManagerAccount.Checked=false;
				if (ObjDR["ManagerAccount"].ToString()=="1")
				{
					rbAllManagerAccount.Checked=true;
				}
				if (ObjDR["ManagerAccount"].ToString()=="2")
				{
					rbSelectManagerAccount.Checked=true;
					txtSelectManager.Text="";

					SqlCmd=new SqlDataAdapter("select b.DeptID,b.DeptName from PaperUser a,DeptInfo b where a.DeptID=b.DeptID and a.UserType=1 and a.PaperID="+intPaperID+" order by a.DeptID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"DeptInfo");
					for(i=0;i<SqlDS.Tables["DeptInfo"].Rows.Count;i++)
					{
						if (txtSelectManager.Text.Trim()=="")
						{
							txtSelectManager.Text=txtSelectManager.Text+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
						else
						{
							txtSelectManager.Text=txtSelectManager.Text+";"+SqlDS.Tables["DeptInfo"].Rows[i]["DeptName"].ToString();
						}
					}

					SqlCmd=new SqlDataAdapter("select b.UserID,b.LoginID from PaperUser a,UserInfo b where a.UserID=b.UserID and a.UserType=1 and a.PaperID="+intPaperID+" order by a.UserID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"UserInfo");
					for(i=0;i<SqlDS.Tables["UserInfo"].Rows.Count;i++)
					{
						if (txtSelectManager.Text.Trim()=="")
						{
							txtSelectManager.Text=txtSelectManager.Text+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
						else
						{
							txtSelectManager.Text=txtSelectManager.Text+";"+SqlDS.Tables["UserInfo"].Rows[i]["LoginID"].ToString();
						}
					}
				}
				intCreateUserID=Convert.ToInt32(ObjDR["CreateUserID"].ToString());
			}
			SqlConn.Dispose();
			ObjConn.Dispose();
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
			this.DataGridPolicy.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridPolicy_ItemCommand);
			this.DataGridPolicy.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridPolicy_DeleteCommand);
			this.DataGridPolicy.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPolicy_ItemDataBound);
			this.DataGridTestType.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTestType_ItemCommand);
			this.DataGridTestType.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridTestType_ItemDataBound);

		}
		#endregion

		#region//*********选择参考人员*******
		protected void ButSelectExam_Click(object sender, System.EventArgs e)
		{
			this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=new Object();obj.name=document.all('txtSelectExam').value;var str=window.showModalDialog('SelectJoinExam.aspx?PaperID="+intPaperID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectExam').value=str; }</script>");
		}
		#endregion

		#region//*********选择评卷人员*******
		protected void ButSelectManager_Click(object sender, System.EventArgs e)
		{
			this.RegisterStartupScript("newWindow","<script language='javascript'>var obj=new Object();obj.name=document.all('txtSelectManager').value;var str=window.showModalDialog('SelectJoinManager.aspx?PaperID="+intPaperID+"',obj,'dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');if (str!=null) { document.all('txtSelectManager').value=str; }</script>");
		}
		#endregion

		#region//*******DataGridPolicy行列颜色变换*******
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

		#region//*******DataGridPolicy删除选中策略*******
		private void DataGridPolicy_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intPaperPolicyID=Convert.ToInt32(e.Item.Cells[0].Text);
			int intSubjectID=Convert.ToInt32(e.Item.Cells[10].Text);
			int intLoreID=Convert.ToInt32(e.Item.Cells[11].Text);
			int intTestTypeID=Convert.ToInt32(e.Item.Cells[12].Text);

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlConn.Open();
			SqlCommand SqlCmd=null;
			SqlCmd=new SqlCommand("delete from PaperPolicy where PaperPolicyID="+intPaperPolicyID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();
			if (ObjFun.GetValues("select PaperPolicyID from PaperPolicy where PaperID='"+intPaperID+"' and TestTypeID='"+intTestTypeID+"'","PaperPolicyID")=="")
			{
				SqlCmd=new SqlCommand("delete from PaperTestType where PaperID='"+intPaperID+"' and TestTypeID='"+intTestTypeID+"'",SqlConn);
				SqlCmd.ExecuteNonQuery();
			}

			SqlDataAdapter SqlCmdTmp=null;
			DataSet SqlDSTmp=null;
			SqlCmdTmp=new SqlDataAdapter("select a.PaperTestID,a.RubricID,b.SubjectID,b.LoreID,b.TestTypeID from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.SubjectID="+intSubjectID+"and b.LoreID="+intLoreID+" and b.TestTypeID="+intTestTypeID+" and a.PaperID="+intPaperID+" order by a.PaperTestID asc",SqlConn);
			SqlDSTmp=new DataSet();
			SqlCmdTmp.Fill(SqlDSTmp,"PaperTest");
			for(int i=0;i<SqlDSTmp.Tables["PaperTest"].Rows.Count;i++)
			{
				SqlCmd=new SqlCommand("delete from PaperTest where PaperTestID="+Convert.ToInt32(SqlDSTmp.Tables["PaperTest"].Rows[i]["PaperTestID"])+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
			}
			//更新题型题量
			SqlCmd=new SqlCommand("Update PaperTestType set TestAmount="+Convert.ToInt32(ObjFun.GetValues("select Count(*) as TestCount from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+intTestTypeID+" and PaperID="+intPaperID+"","TestCount"))+" where TestTypeID="+intTestTypeID+" and PaperID="+intPaperID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Close();
			SqlConn.Dispose();

			ShowPaperPolicy();
			ShowPaperTestType();
		}
		#endregion

		#region//*******DataGridPolicy按钮事件*******
		private void DataGridPolicy_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName=="Edit")
			{
				ShowPaperPolicy();//显示试题策略
				ShowPaperTestType();//显示试题标题
			}
		}
		#endregion

		#region//*******DataGridTestType行列颜色变换*******
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
		}
		#endregion

		#region//*******DataGridTestType按钮事件*******
		private void DataGridTestType_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intPriorPaperTestTypeID=0,intPriorTestTypeID=0,intPriorTestAmount=0,intNextPaperTestTypeID=0,intNextTestTypeID=0,intNextTestAmount=0;
			string strPriorTestTypeTitle="",strNextTestTypeTitle="";
			double dblPriorTestTypeMark=0,dblNextTestTypeMark=0;

			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlDataReader ObjDR=null;
			if (e.CommandName=="MoveUp")
			{
				if (e.Item.ItemIndex>0)
				{
					SqlConn.Open();
					intPriorPaperTestTypeID=Convert.ToInt32(DataGridTestType.Items[e.Item.ItemIndex-1].Cells[0].Text);
					SqlCmd=new SqlCommand("select TestTypeID,TestTypeTitle,TestTypeMark,TestAmount from PaperTestType where PaperTestTypeID='"+intPriorPaperTestTypeID+"'",SqlConn);
					ObjDR=SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
					if (ObjDR.Read())
					{
						intPriorTestTypeID=Convert.ToInt32(ObjDR["TestTypeID"].ToString());
						strPriorTestTypeTitle=ObjDR["TestTypeTitle"].ToString();
						dblPriorTestTypeMark=Convert.ToDouble(ObjDR["TestTypeMark"].ToString());
						intPriorTestAmount=Convert.ToInt32(ObjDR["TestAmount"].ToString());
					}
					SqlConn.Close();

					SqlConn.Open();
					intNextPaperTestTypeID=Convert.ToInt32(DataGridTestType.Items[e.Item.ItemIndex].Cells[0].Text);
					SqlCmd=new SqlCommand("select TestTypeID,TestTypeTitle,TestTypeMark,TestAmount from PaperTestType where PaperTestTypeID='"+intNextPaperTestTypeID+"'",SqlConn);
					ObjDR=SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
					if (ObjDR.Read())
					{
						intNextTestTypeID=Convert.ToInt32(ObjDR["TestTypeID"].ToString());
						strNextTestTypeTitle=ObjDR["TestTypeTitle"].ToString();
						dblNextTestTypeMark=Convert.ToDouble(ObjDR["TestTypeMark"].ToString());
						intNextTestAmount=Convert.ToInt32(ObjDR["TestAmount"].ToString());
					}
					SqlConn.Close();

					SqlConn.Open();
					SqlCmd=new SqlCommand("Update PaperTestType set TestTypeID='"+intNextTestTypeID+"',TestTypeTitle='"+strNextTestTypeTitle+"',TestTypeMark='"+dblNextTestTypeMark+"',TestAmount='"+intNextTestAmount+"' where PaperTestTypeID='"+intPriorPaperTestTypeID+"'",SqlConn);
					SqlCmd.ExecuteNonQuery();

					SqlCmd=new SqlCommand("Update PaperTestType set TestTypeID='"+intPriorTestTypeID+"',TestTypeTitle='"+strPriorTestTypeTitle+"',TestTypeMark='"+dblPriorTestTypeMark+"',TestAmount='"+intPriorTestAmount+"' where PaperTestTypeID='"+intNextPaperTestTypeID+"'",SqlConn);
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
				}
			}
			if (e.CommandName=="MoveDown")
			{
				if (e.Item.ItemIndex<DataGridTestType.Items.Count-1)
				{
					SqlConn.Open();
					intPriorPaperTestTypeID=Convert.ToInt32(DataGridTestType.Items[e.Item.ItemIndex].Cells[0].Text);
					SqlCmd=new SqlCommand("select TestTypeID,TestTypeTitle,TestTypeMark,TestAmount from PaperTestType where PaperTestTypeID='"+intPriorPaperTestTypeID+"'",SqlConn);
					ObjDR=SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
					if (ObjDR.Read())
					{
						intPriorTestTypeID=Convert.ToInt32(ObjDR["TestTypeID"].ToString());
						strPriorTestTypeTitle=ObjDR["TestTypeTitle"].ToString();
						dblPriorTestTypeMark=Convert.ToDouble(ObjDR["TestTypeMark"].ToString());
						intPriorTestAmount=Convert.ToInt32(ObjDR["TestAmount"].ToString());
					}
					SqlConn.Close();

					SqlConn.Open();
					intNextPaperTestTypeID=Convert.ToInt32(DataGridTestType.Items[e.Item.ItemIndex+1].Cells[0].Text);
					SqlCmd=new SqlCommand("select TestTypeID,TestTypeTitle,TestTypeMark,TestAmount from PaperTestType where PaperTestTypeID='"+intNextPaperTestTypeID+"'",SqlConn);
					ObjDR=SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
					if (ObjDR.Read())
					{
						intNextTestTypeID=Convert.ToInt32(ObjDR["TestTypeID"].ToString());
						strNextTestTypeTitle=ObjDR["TestTypeTitle"].ToString();
						dblNextTestTypeMark=Convert.ToDouble(ObjDR["TestTypeMark"].ToString());
						intNextTestAmount=Convert.ToInt32(ObjDR["TestAmount"].ToString());
					}
					SqlConn.Close();

					SqlConn.Open();
					SqlCmd=new SqlCommand("Update PaperTestType set TestTypeID='"+intNextTestTypeID+"',TestTypeTitle='"+strNextTestTypeTitle+"',TestTypeMark='"+dblNextTestTypeMark+"',TestAmount='"+intNextTestAmount+"' where PaperTestTypeID='"+intPriorPaperTestTypeID+"'",SqlConn);
					SqlCmd.ExecuteNonQuery();

					SqlCmd=new SqlCommand("Update PaperTestType set TestTypeID='"+intPriorTestTypeID+"',TestTypeTitle='"+strPriorTestTypeTitle+"',TestTypeMark='"+dblPriorTestTypeMark+"',TestAmount='"+intPriorTestAmount+"' where PaperTestTypeID='"+intNextPaperTestTypeID+"'",SqlConn);
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
				}
			}
			SqlConn.Dispose();

			ShowPaperTestType();
		}
		#endregion
		
		#region//*******提交按钮事件*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			int i=0,j=0,intExamTime=0,intPaperMark=0,intPassMark=0,intSaveTime=0,intTestCount=0,intAutoJudge=1,intTmp=0;
			double dblTestTypeMark=0,dblTotalMark=0,dblRate=0;
//			if (ObjFun.JoySoftware()==false)
//			{
//				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('对不起，未注册用户不能修改试卷！')</script>");
//				return;
//			}
			if (txtPaperName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试卷名称！')</script>");
				return;
			}
			if (DDLPaperType.SelectedItem.Text=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择试卷类型！')</script>");
				return;
			}
			if (DDLProduceWay.SelectedItem.Text=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择出题方式！')</script>");
				return;
			}
			if (DDLShowModal.SelectedItem.Text=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择显示模式！')</script>");
				return;
			}
			if (txtExamTime.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入答题时间！')</script>");
				return;
			}
			else
			{
				try
				{
					intExamTime=Convert.ToInt32(txtExamTime.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的答题时间！')</script>");
					return;
				}
			}
			if (txtStartTime.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入开始时间！')</script>");
				return;
			}
			if (txtEndTime.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入结束时间！')</script>");
				return;
			}
			if (txtPaperMark.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试卷总分！')</script>");
				return;
			}
			else
			{
				try
				{
					intPaperMark=Convert.ToInt32(txtPaperMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的试卷总分！')</script>");
					return;
				}
			}
			if (txtPassMark.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入通过分数！')</script>");
				return;
			}
			else
			{
				try
				{
					intPassMark=Convert.ToInt32(txtPassMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的通过分数！')</script>");
					return;
				}
			}
			if ((rbMarkDefine1.Checked==false)&&(rbMarkDefine2.Checked==false))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择分数定义方式！')</script>");
				return;
			}
			int intFillAutoGrade=0;
			if (chkFillAutoGrade.Checked==true)
			{
				intFillAutoGrade=1;
			}
			if (chkAutoSave.Checked==true)
			{
				if (txtSaveTime.Text.Trim()=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入保存间隔时间！')</script>");
					return;
				}
				else
				{
					try
					{
						intSaveTime=Convert.ToInt32(txtSaveTime.Text.Trim());
					}
					catch
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的保存间隔时间！')</script>");
						return;
					}
					if (intSaveTime<3)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('保存间隔时间应大于或等于3分钟！')</script>");
						return;
					}
				}
			}
			if (DataGridPolicy.Items.Count==0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请添加试题策略！')</script>");
				return;
			}
			TextBox strTestDiff1=null,strTestDiff2=null,strTestDiff3=null,strTestDiff4=null,strTestDiff5=null;
			for(i=0;i<DataGridPolicy.Items.Count;i++)
			{
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
			TextBox strTestTypeTitle=null;
			TextBox strTestTypeMark=null;
			for(i=0;i<DataGridTestType.Items.Count;i++)
			{
				strTestTypeTitle=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeTitle");
				strTestTypeMark=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeMark");
				if (strTestTypeTitle.Text.Trim()=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请在题型标题"+Convert.ToString(i+1)+"行2列中输入题型标题！')</script>");
					return;
				}
				if (ObjFun.GetValues("select TestTypeTitle from PaperTestType where TestTypeTitle='"+ObjFun.getStr(ObjFun.CheckString(strTestTypeTitle.Text.Trim()),20)+"' and PaperTestTypeID<>'"+DataGridTestType.Items[i].Cells[0].Text.Trim()+"' and PaperID="+intPaperID+"","TestTypeTitle")!="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('在题型标题"+Convert.ToString(i+1)+"行2列中输入的题型标题已经存在！')</script>");
					return;
				}
				if (rbMarkDefine2.Checked==true)
				{
					try
					{
						dblTestTypeMark=Convert.ToDouble(strTestTypeMark.Text.Trim());
					}
					catch
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请在题型标题"+Convert.ToString(i+1)+"行3列中输入正确的每题分数！')</script>");
						return;
					}
					if (dblTestTypeMark<=0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('在题型标题"+Convert.ToString(i+1)+"行3列中的每题分数应大于0！')</script>");
						return;
					}
				}
				strTestTypeTitle=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeTitle");
				strTestTypeMark=(TextBox)DataGridTestType.Items[i].FindControl("txtTestTypeMark");

				strSql="Update PaperTestType set TestTypeTitle='"+ObjFun.getStr(ObjFun.CheckString(strTestTypeTitle.Text.Trim()),20)+"',TestTypeMark='"+strTestTypeMark.Text.Trim()+"' where PaperTestTypeID='"+DataGridTestType.Items[i].Cells[0].Text.Trim()+"'";
				intTmp=ObjFun.ExecuteSql(strSql);

				if (intAutoJudge==1)
				{
					if ((DataGridTestType.Items[i].Cells[6].Text.Trim()=="填空类")&&(intFillAutoGrade==0))
					{
						intAutoJudge=0;
					}
					if ((DataGridTestType.Items[i].Cells[6].Text.Trim()=="问答类")||(DataGridTestType.Items[i].Cells[6].Text.Trim()=="作文类")||(DataGridTestType.Items[i].Cells[6].Text.Trim()=="操作类"))
					{
						intAutoJudge=0;
					}
				}
			}
			if ((rbSelectAccount.Checked==true)&&(txtSelectExam.Text.Trim()==""))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择参考人员！')</script>");
				return;
			}
			if ((rbSelectManagerAccount.Checked==true)&&(txtSelectManager.Text.Trim()==""))
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择评卷人员！')</script>");
				return;
			}

			string strPaperName=ObjFun.getStr(ObjFun.CheckString(txtPaperName.Text.Trim()),50);
			int intPaperType=Convert.ToInt32(DDLPaperType.SelectedItem.Value);
			int intProduceWay=Convert.ToInt32(DDLProduceWay.SelectedItem.Value);
			int intShowModal=Convert.ToInt32(DDLShowModal.SelectedItem.Value);
			intExamTime=Convert.ToInt32(txtExamTime.Text.Trim());
			DateTime dtmStartTime=Convert.ToDateTime(txtStartTime.Text.Trim());
			DateTime dtmEndTime=Convert.ToDateTime(txtEndTime.Text.Trim());
			intPaperMark=Convert.ToInt32(txtPaperMark.Text.Trim());
			intPassMark=Convert.ToInt32(txtPassMark.Text.Trim());
			int intMarkDefine=0;
			if (rbMarkDefine1.Checked==true)
			{
				intMarkDefine=1;
			}
			if (rbMarkDefine2.Checked==true)
			{
				intMarkDefine=2;
			}
			int intRepeatExam=0;
			if (chkRepeatExam.Checked==true)
			{
				intRepeatExam=1;
			}
			int intSeeResult=0;
			if (chkSeeResult.Checked==true)
			{
				intSeeResult=1;
			}
			int intAutoSave=0;
			if (chkAutoSave.Checked==true)
			{
				intAutoSave=Convert.ToInt32(txtSaveTime.Text.Trim());
			}
			int intExamAccount=0;
			if (rbAllAccount.Checked==true)
			{
				intExamAccount=1;
			}
			if (rbSelectAccount.Checked==true)
			{
				intExamAccount=2;
			}
			int intManagerAccount=0;
			if (rbAllManagerAccount.Checked==true)
			{
				intManagerAccount=1;
			}
			if (rbSelectManagerAccount.Checked==true)
			{
				intManagerAccount=2;
			}
			int intCreateUserID=Convert.ToInt32(myUserID);
			DateTime currentTime=new System.DateTime();
			currentTime=System.DateTime.Now;
			DateTime dtmCreateDate=Convert.ToDateTime(currentTime.ToString("d"));

			string strTmp=ObjFun.GetValues("select PaperName from PaperInfo where PaperName='"+strPaperName+"' and PaperID<>"+intPaperID+"","PaperName");
			if (strTmp!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此试卷名称已经存在！')</script>");
				return;
			}
			else
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn = new SqlConnection(strConn);
				SqlConnection ObjConn = new SqlConnection(strConn);
				SqlConn.Open();
				ObjConn.Open();
				SqlTransaction ObjTran=ObjConn.BeginTransaction();
				SqlCommand ObjCmd=new SqlCommand();
				ObjCmd.Transaction=ObjTran;
				ObjCmd.Connection=ObjConn;
				try
				{
					SqlCommand ObjectCmd=new SqlCommand();
					ObjectCmd.Connection=SqlConn;
					SqlDataAdapter SqlCmd=null,SqlCmdTmp=null;
					DataSet SqlDS=null,SqlDSTmp=null;
					//生成试题
					ObjectCmd.CommandText="Update PaperTest set PaperTest.TestMark=RubricInfo.TestMark from PaperTest,RubricInfo where PaperTest.RubricID=RubricInfo.RubricID and PaperID="+intPaperID+"";
					ObjectCmd.ExecuteNonQuery();
					//SqlCmd=new SqlDataAdapter("select a.RubricID,b.TestMark from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and PaperID="+intPaperID+"",SqlConn);
					//SqlDS=new DataSet();
					//SqlCmd.Fill(SqlDS,"PaperTest");
					//for(i=0;i<SqlDS.Tables["PaperTest"].Rows.Count;i++)
					//{
					//	ObjectCmd.CommandText="Update PaperTest set TestMark="+SqlDS.Tables["PaperTest"].Rows[i]["TestMark"]+" where RubricID="+SqlDS.Tables["PaperTest"].Rows[i]["RubricID"]+" and PaperID="+intPaperID+"";
					//	ObjectCmd.ExecuteNonQuery();
					//}

					//更新试题分数
					if (rbMarkDefine1.Checked==true)
					{
						SqlCmd=new SqlDataAdapter("select sum(TestMark) as TotalMark from PaperTest where PaperID="+intPaperID+"",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperTest");
						dblTotalMark=Convert.ToDouble(SqlDS.Tables["PaperTest"].Rows[0]["TotalMark"]);
						dblRate=System.Math.Round(intPaperMark/dblTotalMark,4);
						ObjectCmd.CommandText="Update PaperTest set TestMark=Round(TestMark*"+dblRate+",2) where PaperID="+intPaperID+"";
						ObjectCmd.ExecuteNonQuery();
					}
					if (rbMarkDefine2.Checked==true)
					{
						SqlCmd=new SqlDataAdapter("select * from PaperTestType where PaperID="+intPaperID+"",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperTestType");
						for(i=0;i<SqlDS.Tables["PaperTestType"].Rows.Count;i++)
						{
							//SqlCmdTmp=new SqlDataAdapter("select a.PaperTestID,b.TestTypeID from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.PaperID="+intPaperID+"",SqlConn);
							//SqlDSTmp=new DataSet();
							//SqlCmdTmp.Fill(SqlDSTmp,"PaperTest");
							//for(j=0;j<SqlDSTmp.Tables["PaperTest"].Rows.Count;j++)
							//{
							//	ObjectCmd.CommandText="Update PaperTest set TestMark="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeMark"]+" where PaperTestID='"+SqlDSTmp.Tables["PaperTest"].Rows[j]["PaperTestID"]+"'";
							//	ObjectCmd.ExecuteNonQuery();
							//}

							ObjectCmd.CommandText="Update PaperTest set TestMark="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeMark"]+" from PaperTest,RubricInfo where PaperTest.RubricID=RubricInfo.RubricID and RubricInfo.TestTypeID="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and PaperTest.PaperID="+intPaperID+"";
							ObjectCmd.ExecuteNonQuery();
						}

						SqlCmd=new SqlDataAdapter("select sum(TestMark) as TotalMark from PaperTest where PaperID="+intPaperID+"",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperTest");
						dblTotalMark=Convert.ToDouble(SqlDS.Tables["PaperTest"].Rows[0]["TotalMark"]);
						dblRate=System.Math.Round(intPaperMark/dblTotalMark,4);
						ObjectCmd.CommandText="Update PaperTest set TestMark=Round(TestMark*"+dblRate+",2) where PaperID="+intPaperID+"";
						ObjectCmd.ExecuteNonQuery();
					}
					//更新题型分数、顺序
					SqlCmd=new SqlDataAdapter("select TestTypeID from PaperTestType where PaperID="+intPaperID+" order by PaperTestTypeID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"PaperTestType");
					for(i=0;i<SqlDS.Tables["PaperTestType"].Rows.Count;i++)
					{
						SqlCmdTmp=new SqlDataAdapter("select sum(a.TestMark) as TotalMark from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and PaperID="+intPaperID+"",SqlConn);
						SqlDSTmp=new DataSet();
						SqlCmdTmp.Fill(SqlDSTmp,"PaperTest");
						ObjectCmd.CommandText="Update PaperTestType set TestTotalMark="+System.Math.Round(Convert.ToDouble(SqlDSTmp.Tables["PaperTest"].Rows[0]["TotalMark"]),2)+",TestTypeOrder="+Convert.ToString(i+1)+" where TestTypeID="+SqlDS.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and PaperID="+intPaperID+"";
						ObjectCmd.ExecuteNonQuery();
					}
					//统计试题数量
					SqlCmd=new SqlDataAdapter("select Count(*) as TestCount from PaperTest where PaperID="+intPaperID+"",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"PaperTest");
					intTestCount=Convert.ToInt32(SqlDS.Tables["PaperTest"].Rows[0]["TestCount"]);
					//更新人员设置
					if (rbAllAccount.Checked==true)
					{
						ObjectCmd.CommandText="delete from PaperUser where UserType=0 and PaperID="+intPaperID+"";
						ObjectCmd.ExecuteNonQuery();
					}
					if (rbAllManagerAccount.Checked==true)
					{
						ObjectCmd.CommandText="delete from PaperUser where UserType=1 and PaperID="+intPaperID+"";
						ObjectCmd.ExecuteNonQuery();
					}
					//更新试卷
					ObjCmd.CommandText="Update PaperInfo set PaperName='"+strPaperName+"',PaperType='"+intPaperType+"',ProduceWay='"+intProduceWay+"',ShowModal='"+intShowModal+"',ExamTime='"+intExamTime+"',StartTime='"+dtmStartTime+"',EndTime='"+dtmEndTime+"',PaperMark='"+intPaperMark+"',PassMark='"+intPassMark+"',MarkDefine='"+intMarkDefine+"',RepeatExam='"+intRepeatExam+"',FillAutoGrade='"+intFillAutoGrade+"',SeeResult='"+intSeeResult+"',AutoSave='"+intAutoSave+"',ExamAccount='"+intExamAccount+"',ManagerAccount='"+intManagerAccount+"',TestCount='"+intTestCount+"',AutoJudge='"+intAutoJudge+"',CreateWay=1 where PaperID="+intPaperID+"";
					ObjCmd.ExecuteNonQuery();

					ObjTran.Commit();
					//删除答卷
					ObjectCmd.CommandText="delete UserAnswer from UserAnswer a LEFT OUTER JOIN UserScore b ON a.UserScoreID=b.UserScoreID where b.PaperID="+intPaperID+" and b.ExamState<=0";
					ObjectCmd.ExecuteNonQuery();

					ObjectCmd.CommandText="delete from UserScore where PaperID="+intPaperID+" and ExamState<=0";
					ObjectCmd.ExecuteNonQuery();
					//生成答卷
					if (rbAllAccount.Checked==true)
					{
						SqlCmd=new SqlDataAdapter("select UserID from UserInfo order by UserID asc",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperUser");
					}
					else
					{
						SqlCmd=new SqlDataAdapter("select UserID from UserInfo a where Exists(select b.UserID from PaperUser b where b.PaperID="+intPaperID+" and b.UserID=a.UserID and b.UserID<>0 and b.UserType=0) or Exists(select c.UserID from UserInfo c, PaperUser d where d.PaperID="+intPaperID+" and c.DeptID=d.DeptID and d.DeptID<>0 and c.UserID=a.UserID and d.UserType=0) order by UserID asc",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperUser");
					}
					for(i=0;i<SqlDS.Tables["PaperUser"].Rows.Count;i++)
					{
						intUserID=Convert.ToInt32(SqlDS.Tables["PaperUser"].Rows[i]["UserID"]);

						ObjCmd=new SqlCommand("CreatePaper",SqlConn);
						ObjCmd.CommandType=CommandType.StoredProcedure;//指示CreatePaper为存储过程
						ObjCmd.Parameters.Add("@UserID",SqlDbType.Int,4);
						ObjCmd.Parameters["@UserID"].Value=intUserID;
						ObjCmd.Parameters.Add("@PaperID",SqlDbType.Int,4);
						ObjCmd.Parameters["@PaperID"].Value=intPaperID;
						ObjCmd.Parameters.Add("@ExamState",SqlDbType.Int,4);
						ObjCmd.Parameters["@ExamState"].Value=-1;
						ObjCmd.Parameters.Add("@LoginIP",SqlDbType.VarChar,20);
						ObjCmd.Parameters["@LoginIP"].Value=Convert.ToString(Request.ServerVariables["Remote_Addr"]);
						ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4);
						ObjCmd.Parameters["@UserScoreID"].Direction=ParameterDirection.Output;
						ObjCmd.Parameters.Add("@RemTime",SqlDbType.Int,4);
						ObjCmd.Parameters["@RemTime"].Direction=ParameterDirection.Output;
						ObjCmd.ExecuteNonQuery();//执行存储过程
					}

					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改随机组卷成功！');try{ window.opener.RefreshForm() }catch(e){};window.close();</script>");
				}
				catch
				{
					ObjTran.Rollback();
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改随机组卷失败！')</script>");
				}
				finally
				{
					SqlConn.Close();
					SqlConn.Dispose();

					ObjConn.Close();
					ObjConn.Dispose();
				}
			}
		}
		#endregion

		#region//*******取消按钮事件*******
		private void ButCancel_Click(object sender, System.EventArgs e)
		{
			if (intPaperTypeID==1)
			{
				Response.Redirect("ManagExamPaper.aspx");
			}
			else
			{
				Response.Redirect("ManagJobPaper.aspx");
			}
		}
		#endregion

		#region//*******添加策略事件*******
		protected void ButAddPolicy_Click(object sender, System.EventArgs e)
		{
			ShowPaperPolicy();//显示试题策略
			ShowPaperTestType();//显示试题标题
		}
		#endregion

	}
}
