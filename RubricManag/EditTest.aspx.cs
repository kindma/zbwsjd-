using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyExam.RubricManag
{
	/// <summary>
	/// EditTest 的摘要说明。
	/// </summary>
	public partial class EditTest : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable JudgeTable;
		protected System.Web.UI.HtmlControls.HtmlTable OtherTable;
		protected System.Web.UI.WebControls.ImageButton ImgButAdd;
		protected string strActive;
		protected string strRubricID;

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intRubricID=0,intCreateUserID=0;
	
		#region//************初始化信息*********
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
			intRubricID=Convert.ToInt32(Request["RubricID"]);
			strActive="Edit";
			strRubricID=Convert.ToString(intRubricID);

			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intRubricID!=0)
					{
						ShowSubjectInfo();//显示科目信息
						ShowTestTypeInfo();//显示题型名称
						LoadTestData();
					}

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
			for(int i=0;i<SqlDS.Tables["TestTypeInfo"].Rows.Count;i++)
			{
				DDLTestTypeName.Items.Add(new ListItem(SqlDS.Tables["TestTypeInfo"].Rows[i][1].ToString(),SqlDS.Tables["TestTypeInfo"].Rows[i][0].ToString()+","+SqlDS.Tables["TestTypeInfo"].Rows[i][2].ToString()));
			}
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--请选择--","0");
			DDLTestTypeName.Items.Add(strTmp);
		}
		#endregion

		#region//**********加载要修改的试题数据*********
		private void LoadTestData()
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select a.RubricID,a.SubjectID,a.LoreID,a.TestTypeID,b.BaseTestType,a.TestDiff,a.OptionNum,a.TestMark,a.TestContent,a.TestFile,a.TestFileName,a.OptionContent,a.StandardAnswer,a.TestParse,a.CreateDate,a.CreateUserID from RubricInfo a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and RubricID="+intRubricID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				DDLSubjectName.Items.FindByValue(ObjDR["SubjectID"].ToString()).Selected=true;
				ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
				DDLLoreName.Items.FindByValue(ObjDR["LoreID"].ToString()).Selected=true;
				ShowTestTypeInfo();
				DDLTestTypeName.Items.FindByValue(ObjDR["TestTypeID"].ToString()+","+ObjDR["BaseTestType"].ToString()).Selected=true;
				DDLTestDiff.SelectedIndex=-1;
				DDLTestDiff.Items.FindByValue(ObjDR["TestDiff"].ToString()).Selected=true;
				DDLOptionNum.SelectedIndex=-1;
				DDLOptionNum.Items.FindByValue(ObjDR["OptionNum"].ToString()).Selected=true;
				txtTestMark.Text=ObjDR["TestMark"].ToString();
				txtTestContent0.Text=ObjDR["TestContent"].ToString();
				int i=0;
				int intOptionNum=0;
				string strStandardAnswer="";
				string[] strArrOptionContent,strArrTypeStandardAnswer;
				TextBox txtStr=null;
				strStandardAnswer=ObjDR["StandardAnswer"].ToString();
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("单选类")>=0)
				{
					intOptionNum=Convert.ToInt32(DDLOptionNum.SelectedItem.Value);
					strArrOptionContent=ObjDR["OptionContent"].ToString().Split('|');
					for (i=1;i<=intOptionNum;i++)
					{
						txtStr=(TextBox)Page.FindControl("txtTestContent"+i.ToString());
						txtStr.Text=strArrOptionContent[i-1];
					}
					RadioButton rbStr=null;
					for (i=1;i<=intOptionNum;i++)
					{
						rbStr=(RadioButton)Page.FindControl("rbOneSelect"+i.ToString());
						if (rbStr.Text.Trim()==strStandardAnswer.ToUpper())
						{
							rbStr.Checked=true;
						}
					}
				}
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("多选类")>=0)
				{
					intOptionNum=Convert.ToInt32(DDLOptionNum.SelectedItem.Value);
					strArrOptionContent=ObjDR["OptionContent"].ToString().Split('|');
					for (i=1;i<=intOptionNum;i++)
					{
						txtStr=(TextBox)Page.FindControl("txtTestContent"+i.ToString());
						txtStr.Text=strArrOptionContent[i-1];
					}
					CheckBox chkStr=null;
					for (i=1;i<=intOptionNum;i++)
					{
						chkStr=(CheckBox)Page.FindControl("chkMultiSelect"+i.ToString());
						if (strStandardAnswer.ToUpper().IndexOf(chkStr.Text.Trim())>=0)
						{
							chkStr.Checked=true;
						}
					}
				}
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("判断类")>=0)
				{
					if (strStandardAnswer.IndexOf(rbJudgeRight.Text.Trim())>=0)
					{
						rbJudgeRight.Checked=true;
					}
					if (strStandardAnswer.IndexOf(rbJudgeWrong.Text.Trim())>=0)
					{
						rbJudgeWrong.Checked=true;
					}
				}
				if ((DDLTestTypeName.SelectedItem.Value.IndexOf("填空类")>=0)||(DDLTestTypeName.SelectedItem.Value.IndexOf("问答类")>=0)||(DDLTestTypeName.SelectedItem.Value.IndexOf("作文类")>=0))
				{
					txtTestContent7.Text=strStandardAnswer;
				}
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("打字类")>=0)
				{
					strArrTypeStandardAnswer=ObjDR["StandardAnswer"].ToString().Split(',');
					txtTypeTime.Text=strArrTypeStandardAnswer[0];
					txtStandardSpeed.Text=strArrTypeStandardAnswer[1];
				}
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("操作类")>=0)
				{
					lbtTestFile.Text=ObjDR["TestFileName"].ToString();
					if (lbtTestFile.Text.Trim()!="")
					{
						lbtTestFile.Visible=true;
						lbtDeleFile.Visible=true;
					}
					else
					{
						lbtTestFile.Visible=false;
						lbtDeleFile.Visible=false;
					}
				}
				txtTestContent8.Text=ObjDR["TestParse"].ToString();
				txtCreateDate.Text=Convert.ToDateTime(ObjDR["CreateDate"].ToString()).ToString("d");
				intCreateUserID=Convert.ToInt32(ObjDR["CreateUserID"].ToString());
			}
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

		}
		#endregion

		#region//*******选择发生改变*******
		protected void DDLSubjectName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
			DDLLoreName.Items.FindByText("--请选择--").Selected=true;
		}
		#endregion

		#region//*********新建试题信息***********
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			int intSubjectID,intLoreID,intTestTypeID,intOptionNum,intTypeTime,intStandardSpeed,intCreateUserID;
			string strTestDiff,strTestContent,strTestFileName,strOptionContent,strStandardAnswer,strTestParse;
			double dblTestMark;
			DateTime dtmCreateDate;
	
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
			if (DDLTestDiff.SelectedItem.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择试题难度！')</script>");
				return;
			}
			if (txtTestMark.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试题分数！')</script>");
				return;
			}
			try
			{
				dblTestMark=Convert.ToDouble(txtTestMark.Text.Trim());
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的试题分数！')</script>");
				return;
			}
			if (dblTestMark<=0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('试题分数应大于0！')</script>");
				return;
			}
			strTestContent=ObjFun.getStr(ObjFun.CheckTestStr(txtTestContent0.Text.Trim()),4000);
			if (strTestContent.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试题内容！')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select RubricID from RubricInfo where SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and LoreID='"+DDLLoreName.SelectedItem.Value+"' and TestContent='"+strTestContent+"' and RubricID<>'"+intRubricID+"'","RubricID");
			if (strTmp.Trim()!="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('当前科目知识点中已经存在此试题，请重新输入！')</script>");
				return;
			}

			bool bSelected=false;
			int i=0;
			intOptionNum=0;
			strOptionContent="";
			strStandardAnswer="";
			if (DDLTestTypeName.SelectedItem.Value.IndexOf("单选类")>=0)
			{
				intOptionNum=Convert.ToInt32(DDLOptionNum.SelectedItem.Value);
				for (i=1;i<=intOptionNum;i++)
				{
					if (Request["txtTestContent"+i.ToString()]=="")
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试题选项内容！')</script>");
						return;
					}
				}
				RadioButton rbStr=null;
				for (i=1;i<=intOptionNum;i++)
				{
					rbStr=(RadioButton)Page.FindControl("rbOneSelect"+i.ToString());
					if (rbStr.Checked==true)
					{
						bSelected=true;
						strStandardAnswer=strStandardAnswer+rbStr.Text;
					}
					if (strOptionContent=="")
					{
						strOptionContent=ObjFun.getStr(Request["txtTestContent"+i.ToString()],300);
					}
					else
					{
						strOptionContent=strOptionContent+"|"+ObjFun.getStr(Request["txtTestContent"+i.ToString()],300);
					}
				}
				if (bSelected==false)
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择试题答案！')</script>");
					return;
				}
			}
			if (DDLTestTypeName.SelectedItem.Value.IndexOf("多选类")>=0)
			{
				intOptionNum=Convert.ToInt32(DDLOptionNum.SelectedItem.Value);
				for (i=1;i<=intOptionNum;i++)
				{
					if (Request["txtTestContent"+i.ToString()]=="")
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入试题选项内容！')</script>");
						return;
					}
				}
				CheckBox chkStr=null;
				for (i=1;i<=intOptionNum;i++)
				{
					chkStr=(CheckBox)Page.FindControl("chkMultiSelect"+i.ToString());
					if (chkStr.Checked==true)
					{
						bSelected=true;
						strStandardAnswer=strStandardAnswer+chkStr.Text;
					}
					if (strOptionContent=="")
					{
						strOptionContent=ObjFun.getStr(Request["txtTestContent"+i.ToString()],300);
					}
					else
					{
						strOptionContent=strOptionContent+"|"+ObjFun.getStr(Request["txtTestContent"+i.ToString()],300);
					}
				}
				if (bSelected==false)
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择试题答案！')</script>");
					return;
				}
			}
			if (DDLTestTypeName.SelectedItem.Value.IndexOf("判断类")>=0)
			{
				if ((rbJudgeRight.Checked==false)&&(rbJudgeWrong.Checked==false))
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择试题答案！')</script>");
					return;
				}
				if (rbJudgeRight.Checked==true)
				{
					strStandardAnswer=rbJudgeRight.Text.Trim();
				}
				if (rbJudgeWrong.Checked==true)
				{
					strStandardAnswer=rbJudgeWrong.Text.Trim();
				}
			}
			if ((DDLTestTypeName.SelectedItem.Value.IndexOf("填空类")>=0)||(DDLTestTypeName.SelectedItem.Value.IndexOf("问答类")>=0)||(DDLTestTypeName.SelectedItem.Value.IndexOf("作文类")>=0))
			{
				strStandardAnswer=txtTestContent7.Text.Trim();
				if (DDLTestTypeName.SelectedItem.Value.IndexOf("填空类")>=0)
				{
					if (strTestContent.IndexOf("___")<0)
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('填空类试题内容中用3个下划线“_”表示填空位置！')</script>");
						return;
					}
					Regex regex1=new Regex("___");
					Regex regex2=new Regex(",");
					if (regex1.Matches(strTestContent,0).Count!=(regex2.Matches(strStandardAnswer,0).Count+1))
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('填空位置与答案数量不一致，各空答案间用半角逗号“,”隔开！')</script>");
						return;
					}
				}
			}
			if (DDLTestTypeName.SelectedItem.Value.IndexOf("打字类")>=0)
			{
				try
				{
					intTypeTime=Convert.ToInt32(txtTypeTime.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的打字时间！')</script>");
					return;
				}
				try
				{
					intStandardSpeed=Convert.ToInt32(txtStandardSpeed.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请输入正确的标准速度！')</script>");
					return;
				}
				strStandardAnswer=txtTypeTime.Text.Trim()+","+txtStandardSpeed.Text.Trim();
			}
			strTestFileName="";
			byte[] fileBinaryData;
			fileBinaryData=new byte[0];
			if (DDLTestTypeName.SelectedItem.Value.IndexOf("操作类")>=0)
			{
				strTestFileName=TestFile.PostedFile.FileName.Substring(TestFile.PostedFile.FileName.LastIndexOf("\\")+1);
				if (strTestFileName.Trim()!="")
				{
					Stream fileStream;
					int fileLen;
					fileStream=TestFile.PostedFile.InputStream;
					fileLen=TestFile.PostedFile.ContentLength;
					fileBinaryData=new byte[fileLen];
					int n=fileStream.Read(fileBinaryData,0,fileLen);
				}
				else
				{
					fileBinaryData=new byte[0];
					if (lbtTestFile.Text.Trim()=="")
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择下载文件！')</script>");
						return;
					}
				}
				strStandardAnswer="";
			}

			string[] strArrTestType=DDLTestTypeName.SelectedItem.Value.Split(',');
			intSubjectID=Convert.ToInt32(DDLSubjectName.SelectedItem.Value);
			intLoreID=Convert.ToInt32(DDLLoreName.SelectedItem.Value);
			intTestTypeID=Convert.ToInt32(strArrTestType[0]);
			strTestDiff=DDLTestDiff.SelectedItem.Value;
			intOptionNum=Convert.ToInt32(DDLOptionNum.SelectedItem.Value);
			dblTestMark=System.Math.Round(Convert.ToDouble(txtTestMark.Text.Trim()),1);
			strOptionContent=ObjFun.getStr(ObjFun.CheckTestStr(strOptionContent),1800);
			strStandardAnswer=ObjFun.getStr(ObjFun.CheckTestStr(strStandardAnswer),2000);
			strTestParse=ObjFun.getStr(ObjFun.CheckTestStr(txtTestContent8.Text.Trim()),500);
            intCreateUserID=Convert.ToInt32(myUserID);
			dtmCreateDate=Convert.ToDateTime(txtCreateDate.Text);

			int NumRowsAffected=MyDatabaseMethod(intSubjectID,intLoreID,intTestTypeID,strTestDiff,intOptionNum,dblTestMark,strTestContent,fileBinaryData,strTestFileName,strOptionContent,strStandardAnswer,strTestParse,intCreateUserID,dtmCreateDate);
			if (NumRowsAffected>0)
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改试题成功！');try{ window.opener.RefreshForm() }catch(e){};window.close();</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('修改试题失败！')</script>");
			}
		}
		#endregion

		#region//*********将数据更到数据库中*********
		public int MyDatabaseMethod(int intSubjectID,int intLoreID,int intTestTypeID,string strTestDiff,int intOptionNum,double dblTestMark,string strTestContent,byte[] fileBinaryData,string strTestFileName,string strOptionContent,string strStandardAnswer,string strTestParse,int intCreateUserID,DateTime dtmCreateDate)
		{
			string strConn="";
			string strSql="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			if (fileBinaryData.Length>0)
			{
				strSql="Update RubricInfo set SubjectID=@TmpSubjectID,LoreID=@TmpLoreID,TestTypeID=@TmpTestTypeID,TestDiff=@TmpTestDiff,OptionNum=@TmpOptionNum,TestMark=@TmpTestMark,TestContent=@TmpTestContent,TestFile=@TmpTestFile,TestFileName=@TmpTestFileName,OptionContent=@TmpOptionContent,StandardAnswer=@TmpStandardAnswer,TestParse=@TmpTestParse,CreateUserID=@TmpCreateUserID,CreateDate=@TmpCreateDate where RubricID="+intRubricID+"";
			}
			else
			{
				strSql="Update RubricInfo set SubjectID=@TmpSubjectID,LoreID=@TmpLoreID,TestTypeID=@TmpTestTypeID,TestDiff=@TmpTestDiff,OptionNum=@TmpOptionNum,TestMark=@TmpTestMark,TestContent=@TmpTestContent,OptionContent=@TmpOptionContent,StandardAnswer=@TmpStandardAnswer,TestParse=@TmpTestParse,CreateUserID=@TmpCreateUserID,CreateDate=@TmpCreateDate where RubricID="+intRubricID+"";
			}
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);

			SqlParameter ParamSubjectID=new SqlParameter("@TmpSubjectID",SqlDbType.Int);
			ParamSubjectID.Value = intSubjectID;
			ObjCmd.Parameters.Add(ParamSubjectID); 

			SqlParameter ParamLoreID=new SqlParameter("@TmpLoreID",SqlDbType.Int);
			ParamLoreID.Value = intLoreID;
			ObjCmd.Parameters.Add(ParamLoreID); 

			SqlParameter ParamTestTypeID=new SqlParameter("@TmpTestTypeID",SqlDbType.Int);
			ParamTestTypeID.Value = intTestTypeID;
			ObjCmd.Parameters.Add(ParamTestTypeID); 

			SqlParameter ParamTestDiff=new SqlParameter("@TmpTestDiff",SqlDbType.VarChar,6);
			ParamTestDiff.Value = strTestDiff;
			ObjCmd.Parameters.Add(ParamTestDiff); 

			SqlParameter ParamOptionNum=new SqlParameter("@TmpOptionNum",SqlDbType.Int);
			ParamOptionNum.Value = intOptionNum;
			ObjCmd.Parameters.Add(ParamOptionNum); 

			SqlParameter ParamTestMark=new SqlParameter("@TmpTestMark",SqlDbType.Float);
			ParamTestMark.Value = dblTestMark;
			ObjCmd.Parameters.Add(ParamTestMark); 

			SqlParameter ParamTestContent=new SqlParameter("@TmpTestContent",SqlDbType.VarChar,4000);
			ParamTestContent.Value = strTestContent;
			ObjCmd.Parameters.Add(ParamTestContent);

			if (fileBinaryData.Length>0)
			{
				SqlParameter ParamTestFile=new SqlParameter("@TmpTestFile",SqlDbType.Image);
				ParamTestFile.Value = fileBinaryData;
				ObjCmd.Parameters.Add(ParamTestFile);

				SqlParameter ParamTestFileName=new SqlParameter("@TmpTestFileName",SqlDbType.VarChar,255);
				ParamTestFileName.Value = strTestFileName.Trim();
				ObjCmd.Parameters.Add(ParamTestFileName);
			}
			else
			{
				//SqlParameter ParamTestFile=new SqlParameter("@TmpTestFile",SqlDbType.Image);
				//ParamTestFile.Value = System.DBNull.Value;
				//ObjCmd.Parameters.Add(ParamTestFile);

				//SqlParameter ParamTestFileName=new SqlParameter("@TmpTestFileName",SqlDbType.VarChar,255);
				//ParamTestFileName.Value = strTestFileName.Trim();
				//ObjCmd.Parameters.Add(ParamTestFileName);
			}

			SqlParameter ParamOptionContent=new SqlParameter("@TmpOptionContent",SqlDbType.VarChar,1800);
			ParamOptionContent.Value = strOptionContent;
			ObjCmd.Parameters.Add(ParamOptionContent);

			SqlParameter ParamStandardAnswer=new SqlParameter("@TmpStandardAnswer",SqlDbType.VarChar,2000);
			ParamStandardAnswer.Value = strStandardAnswer;
			ObjCmd.Parameters.Add(ParamStandardAnswer);

			SqlParameter ParamTestParse=new SqlParameter("@TmpTestParse",SqlDbType.VarChar,500);
			ParamTestParse.Value = strTestParse;
			ObjCmd.Parameters.Add(ParamTestParse);

			SqlParameter ParamCreateUserID=new SqlParameter("@TmpCreateUserID",SqlDbType.Int);
			ParamCreateUserID.Value = intCreateUserID;
			ObjCmd.Parameters.Add(ParamCreateUserID);
 
			SqlParameter ParamCreateDate=new SqlParameter("@TmpCreateDate",SqlDbType.DateTime);
			ParamCreateDate.Value = dtmCreateDate;
			ObjCmd.Parameters.Add(ParamCreateDate);

			ObjConn.Open();
			int numRowsAffected=ObjCmd.ExecuteNonQuery();
			ObjConn.Close();
			ObjConn.Dispose();
			return numRowsAffected;
		}
		#endregion

		#region//*********删除试题附件*********
		protected void lbtDeleFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				ObjFun.ExecuteSql("Update RubricInfo set TestFileName='',TestFile=null where RubricID="+intRubricID+"");
				lbtTestFile.Visible=false;
				lbtDeleFile.Visible=false;
				lbtTestFile.Text="";
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('删除试题附件成功！')</script>");
			}
			catch
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('删除试题附件失败！')</script>");
			}
		}
		#endregion

		#region//*********下载试题附件*********
		protected void lbtTestFile_Click(object sender, System.EventArgs e)
		{
			string strConn="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			SqlCommand ObjCmd=new SqlCommand("select * from RubricInfo where RubricID="+intRubricID+"",ObjConn);
			ObjConn.Open();
			SqlDataReader ObjDR= ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				Response.ContentType="application/octet-stream";
				Response.AddHeader("Content-Disposition", "attachment;FileName="+ObjDR["TestFileName"].ToString());
				Response.BinaryWrite((byte[])ObjDR["TestFile"]);
				Response.End();
			}
			ObjConn.Dispose();
		}
		#endregion

	}
}
