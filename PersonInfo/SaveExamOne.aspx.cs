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
	/// SaveExamOne 的摘要说明。
	/// </summary>
	public partial class SaveExamOne : System.Web.UI.Page
	{
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intUserScoreID=0,intRemTime=0,intRemMinute=0,intRemSecond=0,intTestAmount=0;
		int intPassMark=0,intFillAutoGrade=0,intSeeResult=0,intPassState=0;
		string strRubricID="",strBaseTestType="",strUserAnswer="";
		double dblUserScore=0,dblImpScore=0;
		int i=0,intTestNum=0;

		#region//*********初始信息*******
		protected void Page_Load(object sender, System.EventArgs e)
		{
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			intRemMinute=Convert.ToInt32(Request["timeminute"]);
			intRemSecond=Convert.ToInt32(Request["timesecond"]);
			intRemTime=intRemMinute*60+intRemSecond;//考试剩余时间
			intTestAmount=1;//试题总数
			intTestNum=Convert.ToInt32(Request["irow"]);//当前试题
			if ((intPaperID!=0)&&(intUserScoreID!=0))
			{
				try
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand ObjCmd=new SqlCommand();
					SqlConn.Open();

					//SqlDataAdapter SqlCmd=null;
					//DataSet SqlDS=null;
					//
					//SqlCmd=new SqlDataAdapter("select * from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
					//SqlDS=new DataSet();
					//SqlCmd.Fill(SqlDS,"PaperInfo");
					//if (SqlDS.Tables["PaperInfo"].Rows.Count==0)
					//{
					//	Response.Write("<script>alert('考试试卷记录不存在，保存失败！')</script>");
					//	return;
					//}
					//else
					//{
					//	intPassMark=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"]);
					//	intFillAutoGrade=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["FillAutoGrade"]);
					//	intSeeResult=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
					//}
					//
					//SqlCmd=new SqlDataAdapter("select * from UserScore where UserScoreID="+intUserScoreID+" order by UserScoreID asc",SqlConn);
					//SqlDS=new DataSet();
					//SqlCmd.Fill(SqlDS,"UserScore");
					//if (SqlDS.Tables["UserScore"].Rows.Count==0)
					//{
					//	Response.Write("<script>alert('答卷记录不存在，保存失败！')</script>");
					//	return;
					//}

					StringBuilder BuilderRubricID=new StringBuilder();    //试题ID
					StringBuilder BuilderUserAnswer=new StringBuilder();  //答案
					StringBuilder BuilderUserScore=new StringBuilder();   //得分

					i=intTestNum-1;

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
							break;
						case "多选类":
							strUserAnswer=strUserAnswer.Trim().Replace(",","");
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "判断类":
							strUserAnswer=strUserAnswer.Trim().Replace(",","");
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "填空类":
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "问答类":
							break;
						case "作文类":
							break;
						case "打字类":
							break;
						case "操作类":
							break;
					}
					//更新答案、得分
					//ObjCmd.CommandText="Update UserAnswer set UserAnswer='"+ObjFun.getStr(strUserAnswer,2000)+"',UserScore="+dblUserScore+" where UserScoreID="+intUserScoreID+" and TestOrder="+SqlDSTest.Tables["UserAnswer"].Rows[i]["TestOrder"]+"";
					//ObjCmd.ExecuteNonQuery();
					BuilderRubricID.Append(strRubricID+"|");
					BuilderUserAnswer.Append(strUserAnswer+"|");
					BuilderUserScore.Append(dblUserScore.ToString()+"|");
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
					
					//更新时间、考试剩余时间、IP地址
					//ObjCmd.CommandText="Update UserScore set EndTime=Getdate(),RemTime="+intRemTime+",LoginIP='"+Convert.ToString(Request.ServerVariables["Remote_Addr"])+"' where UserScoreID="+intUserScoreID+"";
					//ObjCmd.ExecuteNonQuery();
					ObjCmd=new SqlCommand("UpdateUserScore",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//指示为存储过程
					ObjCmd.Parameters.Add("@RemTime",SqlDbType.Int,4);
					ObjCmd.Parameters["@RemTime"].Value=intRemTime;
					ObjCmd.Parameters.Add("@LoginIP",SqlDbType.VarChar,20);
					ObjCmd.Parameters["@LoginIP"].Value=Convert.ToString(Request.ServerVariables["Remote_Addr"]);
					ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4);
					ObjCmd.Parameters["@UserScoreID"].Value=intUserScoreID;
					ObjCmd.ExecuteNonQuery();//执行存储过程 

					SqlConn.Close();
					SqlConn.Dispose();
				}
				catch
				{
					Response.Write("<script>alert('保存考生答案失败，请检查数据库服务器或网络后重试！')</script>");
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
