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
using System.Text;
using System.Text.RegularExpressions;


namespace EasyExam.PersonalInfo
{
	/// <summary>
	/// StartExamAll 的摘要说明。
	/// </summary>
	public partial class StartExamAll : System.Web.UI.Page
	{
		protected string strPaperName="";
		protected string strTestCount="";
		protected string strPaperMark="";
		protected string strPaperContent="";

		protected int intRemTime=0;
		protected int intRemMinute=0;
		protected int intRemSecond=0;
		protected int intAutoSaveTime=0;

		protected string strLoginID="";
		protected string strUserName="";
		protected int intExamTime=0;

		protected int intPaperID=0;
		protected int intPassMark=0;
		protected int intFillAutoGrade=0;
		protected int intSeeResult=0; 
		protected int intAutoJudge=0; 
		protected int intUserID=0;
		protected int intUserScoreID=0;
		protected int intTestNum=0;
		protected string strtable="";

		string myUserID="";
		string myLoginID="";
		string myUserName="";
		PublicFunction ObjFun=new PublicFunction();
		int k=0,intProduceWay=0,intOptionNum=0;
		double dblTotalMark=0;
		string strTestContent="";
		string[] strArrOptionContent,strArrTypeUserAnswer;

		#region//*********初始信息*******
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				myUserName=Session["UserName"].ToString();
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
			intUserID=Convert.ToInt32(myUserID);

			if (!IsPostBack)
			{
				if (intPaperID!=0)
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand ObjCmd=null; 
					SqlDataAdapter SqlCmd=null,SqlCmdTestType=null,SqlCmdTest=null;
					DataSet SqlDS=null,SqlDSTestType=null,SqlDSTest=null;
					//生成试卷
					SqlConn.Open();
					ObjCmd=new SqlCommand("CreatePaper",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//指示CreatePaper为存储过程 
					ObjCmd.Parameters.Add("@UserID",SqlDbType.Int,4); 
					ObjCmd.Parameters["@UserID"].Value=intUserID;
					ObjCmd.Parameters.Add("@PaperID",SqlDbType.Int,4); 
					ObjCmd.Parameters["@PaperID"].Value=intPaperID;
					ObjCmd.Parameters.Add("@ExamState",SqlDbType.Int,4);
					ObjCmd.Parameters["@ExamState"].Value=0;
					ObjCmd.Parameters.Add("@LoginIP",SqlDbType.VarChar,20); 
					ObjCmd.Parameters["@LoginIP"].Value=Convert.ToString(Request.ServerVariables["Remote_Addr"]);
					ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4); 
					ObjCmd.Parameters["@UserScoreID"].Direction=ParameterDirection.Output; 
					ObjCmd.Parameters.Add("@RemTime",SqlDbType.Int,4); 
					ObjCmd.Parameters["@RemTime"].Direction=ParameterDirection.Output; 
					ObjCmd.ExecuteNonQuery();//执行存储过程 
					intUserScoreID=Convert.ToInt32(ObjCmd.Parameters["@UserScoreID"].Value);
					intRemTime=Convert.ToInt32(ObjCmd.Parameters["@RemTime"].Value);
					
					if (intUserScoreID==0)
					{
						ObjFun.Alert("您已经参加了此试卷的考试！");
					}
					if (intUserScoreID==-1)
					{
						ObjFun.Alert("生成答卷意外错误，请稍候再试！");
					}
					strLoginID=Convert.ToString(myLoginID);
					strUserName=Convert.ToString(myUserName);
					
					SqlCmd=new SqlDataAdapter("select PaperName,ProduceWay,TestCount,PaperMark,ExamTime,AutoSave,PassMark,FillAutoGrade,SeeResult,AutoJudge from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"PaperInfo");
					strPaperName=SqlDS.Tables["PaperInfo"].Rows[0]["PaperName"].ToString();
					intProduceWay=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["ProduceWay"]);
					strTestCount=SqlDS.Tables["PaperInfo"].Rows[0]["TestCount"].ToString();
					strPaperMark=SqlDS.Tables["PaperInfo"].Rows[0]["PaperMark"].ToString();
					intExamTime=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["ExamTime"]);
					intAutoSaveTime=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["AutoSave"])*60;
					intPassMark=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"]);
					intFillAutoGrade=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["FillAutoGrade"]);
					intSeeResult=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
					intAutoJudge=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["AutoJudge"]);

					intRemMinute=intRemTime/60;
					intRemSecond=intRemTime%60;
					
					intTestNum=0;
					SqlCmdTestType=new SqlDataAdapter("select a.TestTypeID,b.BaseTestType,a.TestTypeTitle,a.TestAmount,a.TestTotalMark from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.PaperID="+intPaperID+" order by a.PaperTestTypeID asc",SqlConn);
					SqlDSTestType=new DataSet();
					SqlCmdTestType.Fill(SqlDSTestType,"PaperTestType");
					for(int i=0;i<SqlDSTestType.Tables["PaperTestType"].Rows.Count;i++)
					{
						if (intProduceWay==3)//试题随机
						{
							dblTotalMark=System.Math.Round(Convert.ToDouble(ObjFun.GetValues("select sum(a.TestMark) as TotalMark from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+"","TotalMark")),2);
						}
						else//试题固定、题序随机
						{
							dblTotalMark=System.Math.Round(Convert.ToDouble(SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTotalMark"].ToString()),2);
						}
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td colspan='2' bgcolor='#CEE7FF' height='20' style='CURSOR:hand' onclick='jscomFlexObject(trTestTypeContent"+Convert.ToString(i+1)+")' title='点击伸缩该大题'>"+ObjFun.convertint(Convert.ToString(i+1))+"."+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString()+"（共"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestAmount"].ToString()+"题,共"+dblTotalMark.ToString()+"分）</td>";
						strPaperContent=strPaperContent+"</tr>";
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10' nowrap></td>";
						strPaperContent=strPaperContent+"<td width='100%'>";
						strPaperContent=strPaperContent+"<table cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent"+Convert.ToString(i+1)+"'>";

						SqlCmdTest=new SqlDataAdapter("select a.TestOrder,a.RubricID,b.TestTypeID,b.OptionNum,a.TestMark,b.TestContent,a.TestFileName,b.OptionContent,a.UserAnswer from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+" order by a.TestOrder asc",SqlConn);
						SqlDSTest=new DataSet();
						SqlCmdTest.Fill(SqlDSTest,"UserAnswer");
						for(int j=0;j<SqlDSTest.Tables["UserAnswer"].Rows.Count;j++)
						{
							intTestNum=intTestNum+1;
							strTestContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
							strPaperContent=strPaperContent+"<tr>";
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="填空类")
							{
								intOptionNum=0;
								Regex Reg=new Regex("___",RegexOptions.IgnoreCase);
								while (strTestContent.IndexOf("___")>=0)
								{
									strTestContent=Reg.Replace(strTestContent,"",1);
									intOptionNum=intOptionNum+1;
								}
								strTestContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
								strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');
								for(k=1;k<=intOptionNum;k++)
								{
									if (k<=strArrOptionContent.Length)
									{
										strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='"+strArrOptionContent[k-1]+"' onBlur='textcheck()' title='试题答案中不能包含半角逗号“,”'>",1);
									}
									else
									{
										strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='' onBlur='textcheck()' title='试题答案中不能包含半角逗号“,”'>",1);
									}
								}
							}
							strPaperContent=strPaperContent+"<td colspan='2' width='100%'><input type='hidden' id='TestTypeTitle"+intTestNum.ToString()+"' name='TestTypeTitle"+intTestNum.ToString()+"' value='"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString()+"'><input type='hidden' id='RubricID"+intTestNum.ToString()+"' name='RubricID"+intTestNum.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"'><input type='hidden' id='BaseTestType"+intTestNum.ToString()+"' name='BaseTestType"+intTestNum.ToString()+"' value='"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()+"'><a id='l"+intTestNum.ToString()+"' style='color:black'>"+intTestNum.ToString()+"</a>."+strTestContent+"<font color='red'>（"+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestMark"].ToString()+"分）</font></td>";
							strPaperContent=strPaperContent+"</tr>";
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="单选类")
							{
								intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
								strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
								for(k=1;k<=intOptionNum;k++)
								{
									strPaperContent=strPaperContent+"<tr>";
									if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
									{
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' checked>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
									}
									else
									{
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"'>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
									}
									strPaperContent=strPaperContent+"</tr>";
								}
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="多选类")
							{
								intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
								strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
								for(k=1;k<=intOptionNum;k++)
								{
									strPaperContent=strPaperContent+"<tr>";
									if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
									{
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' checked>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
									}
									else
									{
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"'>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
									}
									strPaperContent=strPaperContent+"</tr>";
								}
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="判断类")
							{
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10' nowrap>";
								if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("正确")>=0)
								{
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='正确' checked>正确</td>";
								}
								else
								{
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='正确'>正确</td>";
								}
								strPaperContent=strPaperContent+"</tr>";
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10' nowrap>";
								if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("错误")>=0)
								{
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='错误' checked>错误</td>";
								}
								else
								{
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='错误'>错误</td>";
								}
								strPaperContent=strPaperContent+"</tr>";
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="填空类")
							{
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="问答类")
							{
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString()+"</TEXTAREA></td>";
								strPaperContent=strPaperContent+"</tr>";
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="作文类")
							{
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString()+"</TEXTAREA></td>";
								strPaperContent=strPaperContent+"</tr>";
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="打字类")
							{
								if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(",")>=0)
								{
									strArrTypeUserAnswer=SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');
								}
								else
								{
									strArrTypeUserAnswer="0,0".Split(',');
								}
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('TypeWord.aspx?TestNum="+intTestNum+"&UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"',window,'dialogHeight:430px;dialogWidth:570px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='点击开始打字'>开始打字</a>&nbsp;打字速度：<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[0]+"'>个字/分钟&nbsp;正确率：<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[1]+"'>%</td>";
								strPaperContent=strPaperContent+"</tr>";
							}
							if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="操作类")
							{
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='DownLoadFile.aspx?UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"' title='点击下载'>下载文件：</a><b>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestFileName"].ToString()+"</b></td>";
								strPaperContent=strPaperContent+"</tr>";
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('UpLoadFile.aspx?TestNum="+intTestNum+"&UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"',window,'dialogHeight:150px;dialogWidth:330px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='点击上传'>上传文件：</a><input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='16' ReadOnly='true' value=''></td>";
								strPaperContent=strPaperContent+"</tr>";
							}
							strPaperContent=strPaperContent+"<tr>";
							strPaperContent=strPaperContent+"<td colspan='2' height='10'></td>";
							strPaperContent=strPaperContent+"</tr>";
						}
						strPaperContent=strPaperContent+"</table>";
						strPaperContent=strPaperContent+"</td>";
						strPaperContent=strPaperContent+"</tr>";
					}

					SqlConn.Close();
					SqlConn.Dispose();

					//检查试卷表格
					strtable=strtable+"<table height='100%' width='100%'>";
					strtable=strtable+"<tr>";
					strtable=strtable+"<td>";
					strtable=strtable+"<div style='FONT-SIZE: 9pt; OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%'>";
					strtable=strtable+"<table style='FONT-SIZE: 10pt' cellSpacing='0' borderColorDark='#cccccc' cellPadding='0' border='1'>";
					int n=1;
					for (int i=1;i<=intTestNum;i++)
					{
						if (n==1)
						{
							strtable=strtable+"<tr>";
							strtable=strtable+"<td align='right' width='39' height='18'><span id='icon"+i.ToString()+"1'>"+i.ToString()+":<img src='../images/nofinished.gif' border='0'></span><span id='icon"+i.ToString()+"2' style='DISPLAY: none'>"+i.ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
							n=n+1;
						}
						else
						{
							if (n==10)
							{
								strtable=strtable+"<td align='right' width='39' height='18'><span id='icon"+i.ToString()+"1'>"+i.ToString()+":<img src='../images/nofinished.gif' border='0'></span><span id='icon"+i.ToString()+"2' style='DISPLAY: none'>"+i.ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
								strtable=strtable+"</tr>";
								n=1;
							}
							else
							{
								strtable=strtable+"<td align='right' width='39' height='18'><span id='icon"+i.ToString()+"1'>"+i.ToString()+":<img src='../images/nofinished.gif' border='0'></span><span id='icon"+i.ToString()+"2' style='DISPLAY: none'>"+i.ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
								n=n+1;
							}
						}
					}
					strtable=strtable+"</table>";
					strtable=strtable+"</div>";
					strtable=strtable+"</td>";
					strtable=strtable+"</tr>";
					strtable=strtable+"</table>";
				}
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

		}
		#endregion
	}
}
