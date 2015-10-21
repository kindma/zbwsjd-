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

namespace EasyExam.PersonalInfo
{
	/// <summary>
	/// SubmExamAll 的摘要说明。
	/// </summary>
	public partial class SubmExamAll : System.Web.UI.Page
	{
		protected string strMessage="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intUserScoreID=0,intRemTime=0,intRemMinute=0,intRemSecond=0,intTestAmount=0;
		int intPassMark=0,intFillAutoGrade=0,intSeeResult=0,intPassState=0,intAutoJudge=0,intJudgeState=0,intJudgeUserID=0;
		string strRubricID="",strBaseTestType="",strUserAnswer="",strMsg="";
		double dblUserScore=0,dblImpScore=0,dblSubScore=0;
		string[] strArrTypeStandardAnswer,strArrTypeUserAnswer;
		string[] strArrUserAnswer,strArrStandardAnswer;

		#region//*********初始信息*******
		protected void Page_Load(object sender, System.EventArgs e)
		{
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			intRemMinute=Convert.ToInt32(Request["timeminute"]);
			intRemSecond=Convert.ToInt32(Request["timesecond"]);
			intRemTime=intRemMinute*60+intRemSecond;//考试剩余时间
			intTestAmount=Convert.ToInt32(Request["irow"]);//试题总数

			intPassMark=Convert.ToInt32(Request["PassMark"]);
			intFillAutoGrade=Convert.ToInt32(Request["FillAutoGrade"]);
			intSeeResult=Convert.ToInt32(Request["SeeResult"]);
			intAutoJudge=Convert.ToInt32(Request["AutoJudge"]);
			if ((intPaperID!=0)&&(intUserScoreID!=0))
			{
				try
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand ObjCmd=new SqlCommand();
					SqlConn.Open();

					SqlDataAdapter SqlCmdTest=null;
					DataSet SqlDSTest=null;

					//SqlCmd=new SqlDataAdapter("select * from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
					//SqlDS=new DataSet();
					//SqlCmd.Fill(SqlDS,"PaperInfo");
					//if (SqlDS.Tables["PaperInfo"].Rows.Count==0)
					//{
					//	Response.Write("<script>alert('考试试卷记录不存在，提交失败！')</script>");
					//	return;
					//}
					//else
					//{
					//	intPassMark=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"]);
					//	intFillAutoGrade=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["FillAutoGrade"]);
					//	intSeeResult=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
					//	intAutoJudge=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["AutoJudge"]);
					//}
					//
					//SqlCmd=new SqlDataAdapter("select * from UserScore where UserScoreID="+intUserScoreID+" order by UserScoreID asc",SqlConn);
					//SqlDS=new DataSet();
					//SqlCmd.Fill(SqlDS,"UserScore");
					//if (SqlDS.Tables["UserScore"].Rows.Count==0)
					//{
					//	Response.Write("<script>alert('答卷记录不存在，提交失败！')</script>");
					//	return;
					//}

					StringBuilder BuilderRubricID=new StringBuilder();    //试题ID
					StringBuilder BuilderUserAnswer=new StringBuilder();  //答案
					StringBuilder BuilderUserScore=new StringBuilder();   //得分

					SqlCmdTest=new SqlDataAdapter("select a.TestMark,b.StandardAnswer from UserAnswer a,RubricInfo b,PaperTestType c where a.RubricID=b.RubricID and b.TestTypeID=c.TestTypeID and a.UserScoreID="+intUserScoreID+" and c.PaperID="+intPaperID+" order by c.PaperTestTypeID,a.TestOrder",SqlConn);
					SqlDSTest=new DataSet();
					SqlCmdTest.Fill(SqlDSTest,"UserAnswer");
					dblImpScore=0;dblSubScore=0;
					for(int i=0;i<SqlDSTest.Tables["UserAnswer"].Rows.Count;i++)
					{
						dblUserScore=0;
						strRubricID=Convert.ToString(Request["RubricID"+Convert.ToString(i+1)]);
						strBaseTestType=Convert.ToString(Request["BaseTestType"+Convert.ToString(i+1)]);
						strUserAnswer=Convert.ToString(Request["Answer"+Convert.ToString(i+1)]);
						if (strUserAnswer==null)
						{
							strUserAnswer="";
						}
						if (strUserAnswer.Length>2000)
						{
							strUserAnswer=strUserAnswer.Substring(0,2000);
						}
						strUserAnswer=strUserAnswer.Replace("|","︱");
						switch(strBaseTestType)
						{
							case "单选类":
								strUserAnswer=strUserAnswer.Trim().Replace(",","");
								strUserAnswer=strUserAnswer.Trim().Replace(" ","");
								if (strUserAnswer.ToUpper()==SqlDSTest.Tables["UserAnswer"].Rows[i]["StandardAnswer"].ToString().Replace(" ","").ToUpper())
								{
									dblUserScore=Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[i]["TestMark"]);
									dblImpScore=dblImpScore+dblUserScore;
								}
								break;
							case "多选类":
								strUserAnswer=strUserAnswer.Trim().Replace(",","");
								strUserAnswer=strUserAnswer.Trim().Replace(" ","");
								if (strUserAnswer.ToUpper()==SqlDSTest.Tables["UserAnswer"].Rows[i]["StandardAnswer"].ToString().Replace(" ","").ToUpper())
								{
									dblUserScore=Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[i]["TestMark"]);
									dblImpScore=dblImpScore+dblUserScore;
								}
								break;
							case "判断类":
								strUserAnswer=strUserAnswer.Trim().Replace(",","");
								strUserAnswer=strUserAnswer.Trim().Replace(" ","");
								if (strUserAnswer.ToUpper()==SqlDSTest.Tables["UserAnswer"].Rows[i]["StandardAnswer"].ToString().Replace(" ","").ToUpper())
								{
									dblUserScore=Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[i]["TestMark"]);
									dblImpScore=dblImpScore+dblUserScore;
								}
								break;
							case "填空类":
								strUserAnswer=strUserAnswer.Trim().Replace(" ","");
								if (intFillAutoGrade==1)
								{
									strArrUserAnswer=strUserAnswer.ToUpper().Split(',');
									strArrStandardAnswer=SqlDSTest.Tables["UserAnswer"].Rows[i]["StandardAnswer"].ToString().Replace(" ","").ToUpper().Split(',');
									for(int j=1;j<=Math.Min(strArrUserAnswer.Length,strArrStandardAnswer.Length);j++)
									{
										if (strArrUserAnswer[j-1]==strArrStandardAnswer[j-1])
										{
											dblUserScore=dblUserScore+Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[i]["TestMark"])*(1/Convert.ToDouble(Math.Min(strArrUserAnswer.Length,strArrStandardAnswer.Length)));
										}
									}
									dblSubScore=dblSubScore+dblUserScore;
								}
								break;
							case "问答类":
								break;
							case "作文类":
								break;
							case "打字类":
								strArrTypeStandardAnswer=SqlDSTest.Tables["UserAnswer"].Rows[i]["StandardAnswer"].ToString().Split(',');
								if (strUserAnswer.ToString().IndexOf(",")>=0)
								{
									strArrTypeUserAnswer=strUserAnswer.Trim().Split(',');
								}
								else
								{
									strArrTypeUserAnswer="0,0".Split(',');
								}
								dblUserScore=System.Math.Round(Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[i]["TestMark"])*System.Math.Min(1,Convert.ToDouble(strArrTypeUserAnswer[0])/Convert.ToDouble(strArrTypeStandardAnswer[1]))*(Convert.ToDouble(strArrTypeUserAnswer[1])/100),1);
								dblImpScore=dblImpScore+dblUserScore;
								break;
							case "操作类":
								break;
						}
						//保存答案、得分
						//ObjCmd.CommandText="Update UserAnswer set UserAnswer='"+ObjFun.getStr(strUserAnswer,2000)+"',UserScore="+dblUserScore+" where UserScoreID="+intUserScoreID+" and TestOrder="+SqlDSTest.Tables["UserAnswer"].Rows[i]["TestOrder"]+"";
						//ObjCmd.ExecuteNonQuery();
						BuilderRubricID.Append(strRubricID+"|");
						BuilderUserAnswer.Append(strUserAnswer+"|");
						BuilderUserScore.Append(dblUserScore.ToString()+"|");
					}
					//更新答案、得分
					ObjCmd=new SqlCommand("UpdateUserAnswer",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//指示为存储过程
					ObjCmd.Parameters.Add("@RecordNumber",SqlDbType.Int,4);
					ObjCmd.Parameters["@RecordNumber"].Value=intTestAmount;
					ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4);
					ObjCmd.Parameters["@UserScoreID"].Value=intUserScoreID;
					ObjCmd.Parameters.Add("@RubricID",SqlDbType.VarChar,8000);
					ObjCmd.Parameters["@RubricID"].Value=BuilderRubricID.ToString();
					ObjCmd.Parameters.Add("@UserAnswer",SqlDbType.VarChar,8000);
					ObjCmd.Parameters["@UserAnswer"].Value=BuilderUserAnswer.ToString();
					ObjCmd.Parameters.Add("@UserScore",SqlDbType.VarChar,8000);
					ObjCmd.Parameters["@UserScore"].Value=BuilderUserScore.ToString();
					ObjCmd.ExecuteNonQuery();//执行存储过程

					//总分
					if ((dblImpScore+dblSubScore)>=intPassMark)
					{
						intPassState=1;
					}
					//自动阅卷
					if (intAutoJudge==1)
					{
						intJudgeState=1;
						intJudgeUserID=Convert.ToInt32(ObjFun.GetValues("select UserID from UserInfo where LoginID='Admin'","UserID"));
					}
					//更新保存时间、考试剩余时间、IP地址
					//ObjCmd.CommandText="Update UserScore set ExamState=1,EndTime=Getdate(),RemTime="+intRemTime+",LoginIP='"+Convert.ToString(Request.ServerVariables["Remote_Addr"])+"',JudgeState="+intJudgeState+",JudgeUserID="+intJudgeUserID+",ImpScore="+dblImpScore+",SubScore="+dblSubScore+",PassState="+intPassState+",TotalMark="+(dblImpScore+dblSubScore)+" where UserScoreID="+intUserScoreID+"";
					//ObjCmd.ExecuteNonQuery();
					ObjCmd=new SqlCommand("SubmitUserScore",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//指示为存储过程
					ObjCmd.Parameters.Add("@ExamState",SqlDbType.Int,4);
					ObjCmd.Parameters["@ExamState"].Value=1;
					ObjCmd.Parameters.Add("@RemTime",SqlDbType.Int,4);
					ObjCmd.Parameters["@RemTime"].Value=intRemTime;
					ObjCmd.Parameters.Add("@LoginIP",SqlDbType.VarChar,20);
					ObjCmd.Parameters["@LoginIP"].Value=Convert.ToString(Request.ServerVariables["Remote_Addr"]);
					ObjCmd.Parameters.Add("@JudgeState",SqlDbType.Int,4);
					ObjCmd.Parameters["@JudgeState"].Value=intJudgeState;
					ObjCmd.Parameters.Add("@JudgeUserID",SqlDbType.Int,4);
					ObjCmd.Parameters["@JudgeUserID"].Value=intJudgeUserID;
					ObjCmd.Parameters.Add("@ImpScore",SqlDbType.Float);
					ObjCmd.Parameters["@ImpScore"].Value=dblImpScore;
					ObjCmd.Parameters.Add("@SubScore",SqlDbType.Float);
					ObjCmd.Parameters["@SubScore"].Value=dblSubScore;
					ObjCmd.Parameters.Add("@PassState",SqlDbType.Int,4);
					ObjCmd.Parameters["@PassState"].Value=intPassState;
					ObjCmd.Parameters.Add("@TotalMark",SqlDbType.Float);
					ObjCmd.Parameters["@TotalMark"].Value=System.Math.Round(dblImpScore+dblSubScore,1);
					ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4);
					ObjCmd.Parameters["@UserScoreID"].Value=intUserScoreID;
					ObjCmd.ExecuteNonQuery();//执行存储过程 

					SqlConn.Close();
					SqlConn.Dispose();

					if (intSeeResult==1)
					{
						strMsg="您自动评卷得分："+Convert.ToString(System.Math.Round((dblImpScore+dblSubScore),1));
					}
					else
					{
						strMsg="您已经成功提交答卷！";
					}
					//显示信息
					strMessage=strMessage+"<form id='form1' method='post'>";
					strMessage=strMessage+"<table width='100%' border='0' height='100%'>";
					strMessage=strMessage+"<tr align='center'><td>";
					strMessage=strMessage+"<table border='1' bordercolorlight='#000000' bordercolordark='#FFFFFF' cellspacing='0' bgcolor='#99CCFF'>";
					strMessage=strMessage+"<tr><td><table border='0' bgcolor='#0033CC' cellspacing='0' cellpadding='2' width='100%'>";
					strMessage=strMessage+"<tr><td style='font-size: 12pt; color: #FFFFFF' width='423'> 考试结果</td>";
					strMessage=strMessage+"</tr></table>";
					strMessage=strMessage+"<table border='0' width='100%' cellpadding='4'>";
					strMessage=strMessage+"<tr>";
					strMessage=strMessage+"<td style='font-size: 36pt; color: black' align=center  height=300><p style='line-height: 100%'><B>"+strMsg+"</B></td></tr><tr  style=''><td colspan='2' align='center' valign='top'>";
					if (intSeeResult==1)
					{
						strMessage=strMessage+"<input class='button' type='button' name='cancel'  value='查看答卷' onclick=javascript:{location.href='ShowAnswer.aspx?UserScoreID="+intUserScoreID+"';}><br>";
						strMessage=strMessage+"<input style='display:none;' class='button' type='button' name='cancel'  value='答卷统计' onclick=javascript:{NewWin=window.open('ShowStatis.aspx?UserScoreID="+intUserScoreID+"','ShowStatis','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);}>";
					}
					strMessage=strMessage+"<input class='button' type='button' name='cancel'  value='结束考试' onclick='location.replace(\"/MainLeftMenu.aspx\")'>";
					strMessage=strMessage+"</td></tr></table></td></tr></table>";
					strMessage=strMessage+"</td></tr></table>";
					strMessage=strMessage+"</form>";
				}
				catch
				{
					//显示信息
					strMessage=strMessage+"<form id='form1' method='post' action='SubmExamAll.aspx'>";
					foreach(string item in Request.Form)
					{
						strMessage=strMessage+"<input type='hidden' name='"+item+"' value='"+Request.Form[item].ToString()+"'>";
					}
					strMessage=strMessage+"<table width='100%' border='0' height='100%'>";
					strMessage=strMessage+"<tr align='center'><td align='center'>";
					strMessage=strMessage+"<table border='1' bordercolorlight='#000000' bordercolordark='#FFFFFF' cellspacing='0' bgcolor='#99CCFF'>";
					strMessage=strMessage+"<tr><td><table border='0' bgcolor='#0033CC' cellspacing='0' cellpadding='2' width='350'>";
					strMessage=strMessage+"<tr><td style='font-size:12pt; color:#FFFFFF' width='360'>□提示信息</td>";
					strMessage=strMessage+"</tr></table>";
					strMessage=strMessage+"<table border='0' width='350' cellpadding='4'>";
					strMessage=strMessage+"<tr><td width='59' align='center' valign='top'><img border='0' src='../images/Information.gif'></td>";
					strMessage=strMessage+"<td style='font-size: 12pt; color: black' width='269'>提交答卷失败，请管理员检查数据库服务器或网络是否正常，正常后再试着提交一次。</td></tr><tr><td colspan='2' align='center' valign='top'>";
					strMessage=strMessage+"<input class='button' type='submit' name='ok' id='ok' value='重新提交'>";
					strMessage=strMessage+"<input class='button' type='button' name='cancel'  value='关闭窗口' onclick='window.close()'>";
					strMessage=strMessage+"</td></tr></table></td></tr></table>";
					strMessage=strMessage+"</td></tr></table>";
					strMessage=strMessage+"</form>";
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
