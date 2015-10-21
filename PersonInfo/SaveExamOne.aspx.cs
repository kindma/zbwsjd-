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
	/// SaveExamOne ��ժҪ˵����
	/// </summary>
	public partial class SaveExamOne : System.Web.UI.Page
	{
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intUserScoreID=0,intRemTime=0,intRemMinute=0,intRemSecond=0,intTestAmount=0;
		int intPassMark=0,intFillAutoGrade=0,intSeeResult=0,intPassState=0;
		string strRubricID="",strBaseTestType="",strUserAnswer="";
		double dblUserScore=0,dblImpScore=0;
		int i=0,intTestNum=0;

		#region//*********��ʼ��Ϣ*******
		protected void Page_Load(object sender, System.EventArgs e)
		{
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			intRemMinute=Convert.ToInt32(Request["timeminute"]);
			intRemSecond=Convert.ToInt32(Request["timesecond"]);
			intRemTime=intRemMinute*60+intRemSecond;//����ʣ��ʱ��
			intTestAmount=1;//��������
			intTestNum=Convert.ToInt32(Request["irow"]);//��ǰ����
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
					//	Response.Write("<script>alert('�����Ծ��¼�����ڣ�����ʧ�ܣ�')</script>");
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
					//	Response.Write("<script>alert('����¼�����ڣ�����ʧ�ܣ�')</script>");
					//	return;
					//}

					StringBuilder BuilderRubricID=new StringBuilder();    //����ID
					StringBuilder BuilderUserAnswer=new StringBuilder();  //��
					StringBuilder BuilderUserScore=new StringBuilder();   //�÷�

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
					strUserAnswer=strUserAnswer.Replace("|","��");
					switch(strBaseTestType)
					{
						case "��ѡ��":
							strUserAnswer=strUserAnswer.Trim().Replace(",","");
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "��ѡ��":
							strUserAnswer=strUserAnswer.Trim().Replace(",","");
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "�ж���":
							strUserAnswer=strUserAnswer.Trim().Replace(",","");
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "�����":
							strUserAnswer=strUserAnswer.Trim().Replace(" ","");
							break;
						case "�ʴ���":
							break;
						case "������":
							break;
						case "������":
							break;
						case "������":
							break;
					}
					//���´𰸡��÷�
					//ObjCmd.CommandText="Update UserAnswer set UserAnswer='"+ObjFun.getStr(strUserAnswer,2000)+"',UserScore="+dblUserScore+" where UserScoreID="+intUserScoreID+" and TestOrder="+SqlDSTest.Tables["UserAnswer"].Rows[i]["TestOrder"]+"";
					//ObjCmd.ExecuteNonQuery();
					BuilderRubricID.Append(strRubricID+"|");
					BuilderUserAnswer.Append(strUserAnswer+"|");
					BuilderUserScore.Append(dblUserScore.ToString()+"|");
					//���´𰸡��÷�
					ObjCmd=new SqlCommand("UpdateUserAnswer",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//ָʾΪ�洢����
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
					ObjCmd.ExecuteNonQuery();//ִ�д洢����
					
					//����ʱ�䡢����ʣ��ʱ�䡢IP��ַ
					//ObjCmd.CommandText="Update UserScore set EndTime=Getdate(),RemTime="+intRemTime+",LoginIP='"+Convert.ToString(Request.ServerVariables["Remote_Addr"])+"' where UserScoreID="+intUserScoreID+"";
					//ObjCmd.ExecuteNonQuery();
					ObjCmd=new SqlCommand("UpdateUserScore",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//ָʾΪ�洢����
					ObjCmd.Parameters.Add("@RemTime",SqlDbType.Int,4);
					ObjCmd.Parameters["@RemTime"].Value=intRemTime;
					ObjCmd.Parameters.Add("@LoginIP",SqlDbType.VarChar,20);
					ObjCmd.Parameters["@LoginIP"].Value=Convert.ToString(Request.ServerVariables["Remote_Addr"]);
					ObjCmd.Parameters.Add("@UserScoreID",SqlDbType.Int,4);
					ObjCmd.Parameters["@UserScoreID"].Value=intUserScoreID;
					ObjCmd.ExecuteNonQuery();//ִ�д洢���� 

					SqlConn.Close();
					SqlConn.Dispose();
				}
				catch
				{
					Response.Write("<script>alert('���濼����ʧ�ܣ��������ݿ����������������ԣ�')</script>");
				}
			}
		}
		#endregion

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
