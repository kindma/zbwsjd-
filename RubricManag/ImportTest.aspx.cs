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
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace EasyExam.RubricManag
{
	/// <summary>
	/// ImportTest 的摘要说明。
	/// </summary>
	public partial class ImportTest : System.Web.UI.Page
	{

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

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
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ButInput.Attributes.Add("onclick","javascript:document.all('LabelMessage').innerHTML='正在导入，请稍候......';");
				}
			}
		}

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

		#region//*******导入试题数据*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (UpLoadFile.PostedFile!=null)//检查上传文件不为空
			{
				if (UpLoadFile.PostedFile.FileName=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择正确的Excel文件！')</script>");
					return;
				}
				string strName=UpLoadFile.PostedFile.FileName.Substring(UpLoadFile.PostedFile.FileName.LastIndexOf("."));//获取后缀名
				if (strName.ToUpper()!=".XLS")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择正确的Excel文件！')</script>");
					return;
				}

				UpLoadFile.PostedFile.SaveAs(Server.MapPath("..\\UpLoadFiles\\ImportTest.xls")); 

				string FilePath=Server.MapPath("..\\UpLoadFiles\\ImportTest.xls");
				int intTmp=0;
				string strTmp="";
				string SheetName="";
				bool canOpen=false;

				System.Data.DataTable rs=new System.Data.DataTable();
   				OleDbConnection conn=new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;\"");
    
				try//尝试数据连接是否可用
				{
					conn.Open();
					conn.Close();
					canOpen=true;
				}
				catch//不是Excel文件格式
				{
					canOpen=false;
					LabelMessage.Text="不是Excel文件格式，导入试题失败！";
				}
				if (canOpen)
				{
					try//如果数据连接可以打开则尝试读入数据
					{
						SheetName="Sheet1";
						OleDbCommand myOleDbCommand=new OleDbCommand("SELECT * FROM ["+SheetName+"$]",conn);
						OleDbDataAdapter myData=new OleDbDataAdapter(myOleDbCommand);
						myData.Fill(rs);
						conn.Close();
					}
					catch//如果数据连接可以打开但是读入数据失败，则从文件中提取出工作表的名称，再读入数据
					{
						SheetName=ObjFun.GetSheetName(FilePath);
						if (SheetName.Length>0)
						{
							OleDbCommand myOleDbCommand= new OleDbCommand("SELECT * FROM ["+SheetName+"$]",conn);
							OleDbDataAdapter myData= new OleDbDataAdapter(myOleDbCommand);
							myData.Fill(rs);
							conn.Close();
						}
					}
					intTmp=ObjFun.ExecuteSqlCmd("delete from ImportErr");//清空ImportErr数据表
					int intSubjectID,intLoreID,intTestTypeID,intOptionNum,intTypeTime,intStandardSpeed,intCreateUserID;
					string strSubjectName,strLoreName,strTestTypeName,strBaseTestType,strTestDiff,strTestMark,strTestContent,strOptionContent,strStandardAnswer,strTestParse;
					double dblTestMark;
					string[] strArrOptionContent,strArrTypeStandardAnswer;
					
					intCreateUserID=Convert.ToInt32(myUserID);
					DateTime dtmCreateDate;
					System.DateTime currentTime=new System.DateTime();
					currentTime=System.DateTime.Now;
					dtmCreateDate=Convert.ToDateTime(Convert.ToString(currentTime.ToString("d")));

					int i=0,index=0;
					for(i=0;i<rs.Rows.Count;i++)
					{
						strSubjectName=ObjFun.CheckString(rs.Rows[i][0].ToString().Trim());//科目名称
						strLoreName=ObjFun.CheckString(rs.Rows[i][1].ToString().Trim());//知识点
						strTestTypeName=ObjFun.CheckString(rs.Rows[i][2].ToString().Trim());//题型名称
						strTestDiff=ObjFun.CheckString(rs.Rows[i][3].ToString().Trim());//试题难度
						strTestMark=rs.Rows[i][4].ToString().Trim();//试题分数
						strTestContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][5].ToString().Trim()),4000);//试题内容
						strOptionContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][6].ToString().Trim()),1800);//试题选项
						strStandardAnswer=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][7].ToString().Trim()),2000);//标准答案
						strTestParse=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][8].ToString().Trim()),500);//试题解析

						intSubjectID=0;
						intLoreID=0;
						intTestTypeID=0;
						//科目名称
						if (strSubjectName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'科目名称为空')");
						}
						else
						{
							strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
							if (strTmp=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'科目名称不存在')");
							}
							else
							{
								intSubjectID=Convert.ToInt32(strTmp);
							}
						}
						//知识点
						if (strLoreName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'知识点为空')");
						}
						else
						{
							strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
							if (strTmp!="")
							{
								if (ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+Convert.ToInt32(strTmp)+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID")=="")
								{
									index=index+1;
									intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'当前科目知识点不存在')");
								}
								else
								{
									intLoreID=Convert.ToInt32(ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+Convert.ToInt32(strTmp)+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID"));
								}
							}
						}
						//题型名称
						if (strTestTypeName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'题型名称为空')");
						}
						else
						{
							if (ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID")=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'题型名称不存在')");
							}
							else
							{
								intTestTypeID=Convert.ToInt32(ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID"));
							}
						}
						//试题难度
						if ((strTestDiff!="易")&&(strTestDiff!="较易")&&(strTestDiff!="中等")&&(strTestDiff!="较难")&&(strTestDiff!="难"))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题难度不正确')");
						}
						//试题分数
						try
						{
							dblTestMark=Convert.ToDouble(strTestMark);
						}
						catch
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题分数不正确')");
						}
						if ((intSubjectID!=0)&&(intLoreID!=0)&&(intTestTypeID!=0))
						{
							//试题内容
							if (strTestContent=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题内容为空')");
							}
							//题型检查
							strTmp=ObjFun.GetValues("select RubricID from RubricInfo where SubjectID='"+intSubjectID+"' and LoreID='"+intLoreID+"' and TestContent='"+strTestContent+"'","RubricID");
							if (strTmp.Trim()!="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'当前科目知识点中已经存在此试题')");
							}
							else
							{
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="单选类")
								{
									if (strOptionContent.IndexOf("|")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题选项内容未用“|”隔开')");
									}
									if ((strStandardAnswer.ToUpper()!="A")&&(strStandardAnswer.ToUpper()!="B")&&(strStandardAnswer.ToUpper()!="C")&&(strStandardAnswer.ToUpper()!="D")&&(strStandardAnswer.ToUpper()!="E")&&(strStandardAnswer.ToUpper()!="F"))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题答案不正确')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="多选类")
								{
									if (strOptionContent.IndexOf("|")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题选项内容未用“|”隔开')");
									}
									if ((strStandardAnswer.ToUpper().IndexOf("A")<0)&&(strStandardAnswer.ToUpper().IndexOf("B")<0)&&(strStandardAnswer.ToUpper().IndexOf("C")<0)&&(strStandardAnswer.ToUpper().IndexOf("D")<0)&&(strStandardAnswer.ToUpper().IndexOf("E")<0)&&(strStandardAnswer.ToUpper().IndexOf("F")<0))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题答案不正确')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="判断类")
								{
									if ((strStandardAnswer.ToUpper()!="正确")&&(strStandardAnswer.ToUpper()!="错误"))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题答案不正确')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="填空类")
								{
									if (strTestContent.IndexOf("___")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题内容中未用3个下划线“_”表示填空位置')");
									}
									Regex regex1=new Regex("___");
									Regex regex2=new Regex(",");
									if (regex1.Matches(strTestContent,0).Count!=(regex2.Matches(strStandardAnswer,0).Count+1))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'填空位置与答案数量不一致，答案间用半角逗号“,”隔开')");
									}
								}
								if ((ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="问答类")||(ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="作文类"))
								{
									if (strStandardAnswer=="")
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题答案不能为空')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="打字类")
								{
									if (strStandardAnswer.IndexOf(",")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题参数未用“,”隔开')");
									}
									strArrTypeStandardAnswer=strStandardAnswer.Split(',');
									try
									{
										intTypeTime=Convert.ToInt32(strArrTypeStandardAnswer[0]);
									}
									catch
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题参数的打字时间不正确')");
									}
									try
									{
										intStandardSpeed=Convert.ToInt32(strArrTypeStandardAnswer[1]);
									}
									catch
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'试题参数的标准速度不正确')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="操作类")
								{
									
								}
							}
						}
					}
					string strSql="select a.ErrID,a.RowNum,a.ErrInfo from ImportErr a order by a.ErrID asc";
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
					DataSet SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"ImportErr");
					if (SqlDS.Tables["ImportErr"].Rows.Count>0)//如果导入有错误，则绑定DataGridErr
					{
						DataGridErr.DataSource=SqlDS;
						DataGridErr.AllowPaging=false;
						DataGridErr.DataBind();
						DataGridErr.Visible=true;
						LabelMessage.Text="导入试题失败！";
					}
					else//试题导入UserInfo数据表
					{
						DataGridErr.DataSource=null;
						DataGridErr.Visible=false;

						strConn=ConfigurationSettings.AppSettings["strConn"];
						SqlConnection ObjConn = new SqlConnection(strConn);
						ObjConn.Open();
						SqlTransaction ObjTran=ObjConn.BeginTransaction();
						SqlCommand ObjCmd=new SqlCommand();
						ObjCmd.Connection=ObjConn;
						ObjCmd.Transaction=ObjTran;
						try
						{
							for(i=0;i<rs.Rows.Count;i++)
							{
								strSubjectName=ObjFun.CheckString(rs.Rows[i][0].ToString().Trim());//科目名称
								strLoreName=ObjFun.CheckString(rs.Rows[i][1].ToString().Trim());//知识点
								strTestTypeName=ObjFun.CheckString(rs.Rows[i][2].ToString().Trim());//题型名称
								strTestDiff=ObjFun.CheckString(rs.Rows[i][3].ToString().Trim());//试题难度
								strTestMark=rs.Rows[i][4].ToString().Trim();//试题分数
								strTestContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][5].ToString().Trim()),4000);//试题内容
								strOptionContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][6].ToString().Trim()),1800);//试题选项
								strStandardAnswer=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][7].ToString().Trim()),2000);//标准答案
								strTestParse=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][8].ToString().Trim()),500);//试题解析

								intSubjectID=0;
								intLoreID=0;
								intTestTypeID=0;
								strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
								if (strTmp!="") 
								{
									intSubjectID=Convert.ToInt32(strTmp);
								}
								strTmp=ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+intSubjectID+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID");
								if (strTmp!="")
								{
									intLoreID=Convert.ToInt32(strTmp);
								}
								strTmp=ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID");
								if (strTmp!="")
								{
									intTestTypeID=Convert.ToInt32(strTmp);
								}
								if ((intSubjectID!=0)&&(intLoreID!=0)&&(intTestTypeID!=0))
								{
									strTmp=ObjFun.GetValues("select RubricID from RubricInfo where SubjectID='"+intSubjectID+"' and LoreID='"+intLoreID+"' and TestContent='"+strTestContent.Trim()+"'","RubricID");
									if (strTmp.Trim()=="")
									{
										dblTestMark=Convert.ToDouble(strTestMark);
										strBaseTestType="";
										intOptionNum=4;
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="单选类")
										{
											strBaseTestType="单选类";
											strArrOptionContent=strOptionContent.Split('|');
											intOptionNum=strArrOptionContent.Length;
											strStandardAnswer=strStandardAnswer.ToUpper();
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="多选类")
										{
											strBaseTestType="多选类";
											strArrOptionContent=strOptionContent.Split('|');
											intOptionNum=strArrOptionContent.Length;
											strStandardAnswer=strStandardAnswer.ToUpper();
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="判断类")
										{
											strBaseTestType="判断类";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="填空类")
										{
											strBaseTestType="填空类";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="问答类")
										{
											strBaseTestType="问答类";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="作文类")
										{
											strBaseTestType="作文类";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="打字类")
										{
											strBaseTestType="打字类";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="操作类")
										{
											strBaseTestType="操作类";
											strStandardAnswer="";
										}
										intTmp=ObjFun.ExecuteSqlCmd("Insert into RubricInfo(SubjectID,LoreID,TestTypeID,TestDiff,OptionNum,TestMark,TestContent,OptionContent,StandardAnswer,TestParse,CreateUserID,CreateDate) Values ("+intSubjectID+","+intLoreID+","+intTestTypeID+",'"+strTestDiff+"',"+intOptionNum+","+dblTestMark+",'"+strTestContent+"','"+strOptionContent+"','"+strStandardAnswer+"','"+strTestParse+"',"+intCreateUserID+",'"+dtmCreateDate+"')");
									}
								}
							}
							ObjTran.Commit();
						}
						catch
						{
							ObjTran.Rollback();
							LabelMessage.Text="导入试题失败！";
						}
						finally
						{
							ObjConn.Close();
							ObjConn.Dispose();
							LabelMessage.Text="导入试题成功！";
						}
					}
				}
			}
		}
		#endregion

	}
}
