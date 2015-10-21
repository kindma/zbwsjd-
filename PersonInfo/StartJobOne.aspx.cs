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
	/// StartJobOne ��ժҪ˵����
	/// </summary>
	public partial class StartJobOne : System.Web.UI.Page
	{
		protected string strPaperName="";
		protected string strTestCount="";
		protected string strPaperMark="";
		protected string strPlan="";
		protected string strTestTypeMark="";
		protected string strTestContent="";
		protected string strSelectOption="";
		protected string strPaperContent="";
		protected string strJavaScript="";
		protected string strSubmitExam="";
		protected string strCheckAnswer="";

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

		string myUserID="";
		string myLoginID="";
		string myUserName="";
		PublicFunction ObjFun=new PublicFunction();
		int i=0,k=0,intOptionNum=0;
		double dblTotalMark=0;
		string[] strArrOptionContent,strArrTypeUserAnswer;

		string strRubricID="",strBaseTestType="",strUserAnswer="";
		int intProduceWay=0,intTestAmount=1;
		double dblUserScore=0,dblImpScore=0;

		#region//*********��ʼ��Ϣ*******
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
			//�������
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
					//�����Ծ�
					SqlConn.Open();
					ObjCmd=new SqlCommand("CreatePaper",SqlConn);
					ObjCmd.CommandType=CommandType.StoredProcedure;//ָʾCreatePaperΪ�洢���� 
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
					ObjCmd.ExecuteNonQuery();//ִ�д洢���� 
					intUserScoreID=Convert.ToInt32(ObjCmd.Parameters["@UserScoreID"].Value);
					intRemTime=Convert.ToInt32(ObjCmd.Parameters["@RemTime"].Value);
					
					if (intUserScoreID==0)
					{
						ObjFun.Alert("���Ѿ��μ��˴��Ծ����ҵ��");
					}
					if (intUserScoreID==-1)
					{
						ObjFun.Alert("���ɴ������������Ժ����ԣ�");
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

					if (Request["Start"]!=null)
					{
						intRemMinute=intRemTime/60;
						intRemSecond=intRemTime%60;
						intTestNum=1;
						i=intTestNum-1;
					}
					else
					{
						intRemMinute=Convert.ToInt32(Request["timeminute"]);
						intRemSecond=Convert.ToInt32(Request["timesecond"]);
						intRemTime=intRemMinute*60+intRemSecond;
						intTestNum=Convert.ToInt32(Request["TestNum"]);

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
						//���µ�ǰ����𰸡��÷�
						//ObjCmd.CommandText="Update UserAnswer set UserAnswer='"+ObjFun.getStr(strUserAnswer,2000)+"',UserScore="+dblUserScore+" where UserScoreID="+intUserScoreID+" and TestOrder="+intTestOrder+"";
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

						//���±���ʱ�䡢��ҵʣ��ʱ��
						//ObjCmd.CommandText="Update UserScore set EndTime=Getdate(),RemTime="+intRemTime+" where UserScoreID="+intUserScoreID+"";
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

						if (Request["SelectTest"]!=null)//ѡ�����
						{
							if ((Convert.ToInt32(Request["SelTestNum"])>=1)&&(Convert.ToInt32(Request["SelTestNum"])<=Convert.ToInt32(strTestCount)))
							{
								intTestNum=Convert.ToInt32(Request["SelTestNum"]);
								i=intTestNum-1;
							}
						}
						if (Request["PriorTest"]!=null)//ѡ����һ��
						{
							if ((Convert.ToInt32(Request["TestNum"])-1)>=1)
							{
								intTestNum=Convert.ToInt32(Request["TestNum"])-1;
								i=intTestNum-1;
							}
						}
						if (Request["NextTest"]!=null)//ѡ����һ��
						{
							if ((Convert.ToInt32(Request["TestNum"])+1)<=Convert.ToInt32(strTestCount))
							{
								intTestNum=Convert.ToInt32(Request["TestNum"])+1;
								i=intTestNum-1;
							}
						}
						if (Request["TrySubmit"]!=null)//�����ύ
						{
							if ((Convert.ToInt32(Request["TestNum"])>=1)&&(Convert.ToInt32(Request["TestNum"])<=Convert.ToInt32(strTestCount)))
							{
								intTestNum=Convert.ToInt32(Request["TestNum"]);
								i=intTestNum-1;
							}
						}
					}

					SqlCmdTest=new SqlDataAdapter("select a.PaperTestTypeID,a.TestTypeID,b.BaseTestType,a.TestTypeTitle,a.TestAmount,a.TestTotalMark,a.TestTypeOrder,c.TestOrder,c.RubricID,d.OptionNum,c.TestMark,d.TestContent,c.TestFileName,d.OptionContent,c.UserAnswer from PaperTestType a,TestTypeInfo b,UserAnswer c,RubricInfo d where a.TestTypeID=b.TestTypeID and c.RubricID=d.RubricID and d.TestTypeID=a.TestTypeID and c.UserScoreID="+intUserScoreID+" and a.PaperID="+intPaperID+" and c.TestOrder="+intTestNum.ToString()+" order by a.PaperTestTypeID asc,c.TestOrder asc",SqlConn);
					SqlDSTest=new DataSet();
					SqlCmdTest.Fill(SqlDSTest,"UserAnswer");

					//SqlCmdTestType=new SqlDataAdapter("select TestTypeID from PaperTestType where PaperID="+intPaperID+" order by PaperTestTypeID asc",SqlConn);
					//SqlDSTestType=new DataSet();
					//SqlCmdTestType.Fill(SqlDSTestType,"PaperTestType");
					//for(int j=0;j<SqlDSTestType.Tables["PaperTestType"].Rows.Count;j++)
					//{
					//	if (Convert.ToInt32(SqlDSTestType.Tables["PaperTestType"].Rows[j]["TestTypeID"])==Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeID"]))
					//	{
					//		dblTotalMark=System.Math.Round(Convert.ToDouble(ObjFun.GetValues("select sum(a.TestMark) as TotalMark from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+"","TotalMark")),2);
					//		strTestTypeMark=ObjFun.convertint(Convert.ToString(j+1))+"."+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeTitle"].ToString()+"����"+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestAmount"].ToString()+"��,��"+dblTotalMark.ToString()+"�֣�";
					//		strPlan=intTestNum.ToString()+"/"+strTestCount;
					//		break;
					//	}
					//}

					if (intProduceWay==3)//�������
					{
						dblTotalMark=System.Math.Round(Convert.ToDouble(ObjFun.GetValues("select sum(a.TestMark) as TotalMark from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+"","TotalMark")),2);
					}
					else//����̶����������
					{
						dblTotalMark=System.Math.Round(Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTotalMark"].ToString()),2);
					}
					strTestTypeMark=ObjFun.convertint(SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeOrder"].ToString())+"."+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeTitle"].ToString()+"����"+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestAmount"].ToString()+"��,��"+dblTotalMark.ToString()+"�֣�";
					strPlan=intTestNum.ToString()+"/"+strTestCount;

					strPaperContent=strPaperContent+"<table cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent"+Convert.ToString(i+1)+"'>";
					strTestContent=SqlDSTest.Tables["UserAnswer"].Rows[0]["TestContent"].ToString();
					strPaperContent=strPaperContent+"<tr>";
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="�����")
					{
						intOptionNum=0;
						Regex Reg=new Regex("___",RegexOptions.IgnoreCase);
						while (strTestContent.IndexOf("___")>=0)
						{
							strTestContent=Reg.Replace(strTestContent,"",1);
							intOptionNum=intOptionNum+1;
						}
						strTestContent=SqlDSTest.Tables["UserAnswer"].Rows[0]["TestContent"].ToString();
						strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().Split(',');
						for(k=1;k<=intOptionNum;k++)
						{
							if (k<=strArrOptionContent.Length)
							{
								strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='"+strArrOptionContent[k-1]+"' onBlur='textcheck()' title='������в��ܰ�����Ƕ��š�,��'>",1);
							}
							else
							{
								strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='' onBlur='textcheck()' title='������в��ܰ�����Ƕ��š�,��'>",1);
							}
						}
					}
					strPaperContent=strPaperContent+"<td colspan='2' width='100%'><input type='hidden' id='TestTypeTitle"+intTestNum.ToString()+"' name='TestTypeTitle"+intTestNum.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestTypeTitle"].ToString()+"'><input type='hidden' id='RubricID"+intTestNum.ToString()+"' name='RubricID"+intTestNum.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[0]["RubricID"].ToString()+"'><input type='hidden' id='BaseTestType"+intTestNum.ToString()+"' name='BaseTestType"+intTestNum.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()+"'><a id='l"+intTestNum.ToString()+"' style='color:black'>"+intTestNum.ToString()+"</a>."+strTestContent+"<font color='red'>��"+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestMark"].ToString()+"�֣�</font></td>";
					strPaperContent=strPaperContent+"</tr>";
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="��ѡ��")
					{
						intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[0]["OptionNum"]);
						strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[0]["OptionContent"].ToString().Split('|');
						for(k=1;k<=intOptionNum;k++)
						{
							strPaperContent=strPaperContent+"<tr>";
							if (SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
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
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="��ѡ��")
					{
						intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[0]["OptionNum"]);
						strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[0]["OptionContent"].ToString().Split('|');
						for(k=1;k<=intOptionNum;k++)
						{
							strPaperContent=strPaperContent+"<tr>";
							if (SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
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
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="�ж���")
					{
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10' nowrap>";
						if (SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().IndexOf("��ȷ")>=0)
						{
							strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='��ȷ' checked>��ȷ</td>";
						}
						else
						{
							strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='��ȷ'>��ȷ</td>";
						}
						strPaperContent=strPaperContent+"</tr>";
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10' nowrap>";
						if (SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().IndexOf("����")>=0)
						{
							strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='����' checked>����</td>";
						}
						else
						{
							strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='����'>����</td>";
						}
						strPaperContent=strPaperContent+"</tr>";
					}
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="�����")
					{
					}
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="�ʴ���")
					{
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text>"+SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString()+"</TEXTAREA></td>";
						strPaperContent=strPaperContent+"</tr>";
					}
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="������")
					{
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text>"+SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString()+"</TEXTAREA></td>";
						strPaperContent=strPaperContent+"</tr>";
					}
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="������")
					{
						if (SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().IndexOf(",")>=0)
						{
							strArrTypeUserAnswer=SqlDSTest.Tables["UserAnswer"].Rows[0]["UserAnswer"].ToString().Split(',');
						}
						else
						{
							strArrTypeUserAnswer="0,0".Split(',');
						}
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('TypeWord.aspx?TestNum="+intTestNum+"&UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[0]["RubricID"].ToString()+"',window,'dialogHeight:430px;dialogWidth:570px;edge:Raised;center:Yes;help:Yes;resizable:No;status:No;'); title='�����ʼ����'>��ʼ����</a>&nbsp;�����ٶȣ�<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[0]+"'>����/����&nbsp;��ȷ�ʣ�<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[1]+"'>%</td>";
						strPaperContent=strPaperContent+"</tr>";
					}
					if (SqlDSTest.Tables["UserAnswer"].Rows[0]["BaseTestType"].ToString()=="������")
					{
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='DownLoadFile.aspx?UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[0]["RubricID"].ToString()+"' title='�������'>�����ļ���</a><b>"+SqlDSTest.Tables["UserAnswer"].Rows[0]["TestFileName"].ToString()+"</b></td>";
						strPaperContent=strPaperContent+"</tr>";
						strPaperContent=strPaperContent+"<tr>";
						strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('UpLoadFile.aspx?TestNum="+intTestNum+"&UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[0]["RubricID"].ToString()+"',window,'dialogHeight:150px;dialogWidth:330px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='����ϴ�'>�ϴ��ļ���</a><input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='16' ReadOnly='true' value=''></td>";
						strPaperContent=strPaperContent+"</tr>";
					}
					strPaperContent=strPaperContent+"<tr>";
					strPaperContent=strPaperContent+"<td colspan='2' height='10'></td>";
					strPaperContent=strPaperContent+"</tr>";
					strPaperContent=strPaperContent+"</table>";

					strSelectOption=strSelectOption+"<select name='select1' onChange='seltestnum();'>";
					for(k=0;k<Convert.ToInt32(strTestCount);k++)
					{
						if ((k+1)==intTestNum)
						{
							strSelectOption=strSelectOption+"<option value='"+Convert.ToString(k+1)+"' selected>"+Convert.ToString(k+1)+"</option>";
						}
						else
						{
							strSelectOption=strSelectOption+"<option value='"+Convert.ToString(k+1)+"'>"+Convert.ToString(k+1)+"</option>";
						}
					}
					strSelectOption=strSelectOption+"</select>";

					if (intTestNum==1)
					{
						strJavaScript="<script language='javascript'>document.form1.priobtn.disabled=true;</script>";
					}
					if (intTestNum==Convert.ToInt32(strTestCount))
					{
						strJavaScript="<script language='javascript'>document.form1.nextbtn.disabled=true;</script>";
					}

					if (Request["TrySubmit"]!=null)//�����ύ
					{
						SqlCmdTest=new SqlDataAdapter("select a.PaperTestTypeID,b.BaseTestType,a.TestTypeTitle,c.TestOrder,c.UserAnswer from PaperTestType a,TestTypeInfo b,UserAnswer c,RubricInfo d where a.TestTypeID=b.TestTypeID and c.RubricID=d.RubricID and d.TestTypeID=a.TestTypeID and c.UserScoreID="+intUserScoreID+" and a.PaperID="+intPaperID+" order by a.PaperTestTypeID,c.TestOrder asc",SqlConn);
						SqlDSTest=new DataSet();
						SqlCmdTest.Fill(SqlDSTest,"UserAnswer");

						int intNotFinished=0;
						string strTestTypeTitle="";
						for(i=0;i<SqlDSTest.Tables["UserAnswer"].Rows.Count;i++)
						{
							strTestTypeTitle=SqlDSTest.Tables["UserAnswer"].Rows[i]["TestTypeTitle"].ToString();
							strBaseTestType=SqlDSTest.Tables["UserAnswer"].Rows[i]["BaseTestType"].ToString();
							strUserAnswer=SqlDSTest.Tables["UserAnswer"].Rows[i]["UserAnswer"].ToString();
							switch(strBaseTestType)
							{
								case "��ѡ��":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "��ѡ��":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "�ж���":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "�����":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "�ʴ���":
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "������":
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=i+1;
									}
									break;
								case "������":
									break;
							}
							if (intNotFinished!=0)
							{ 
								break;
							}
						}
						if (intNotFinished!=0)
						{
							strSubmitExam=strSubmitExam+"<div id='submitexam3' style='LEFT:expression((document.body.clientWidth-320)/2);POSITION:absolute;top:expression(document.body.scrollTop+(window.screen.availHeight-90)/2-20);z-index:120;visibility:visible'>";
							strSubmitExam=strSubmitExam+"<table width='320' border=1 cellSpacing=0 borderColorLight=#000000 borderColorDark=#ffffff bgColor=#FFFFFF style='cursor:default'>";
							strSubmitExam=strSubmitExam+"<tr bgcolor='#336699'>";
							strSubmitExam=strSubmitExam+"<td width='320'><span ><font color='#FFFFFF'>��ʾ��Ϣ</font></span></td>";
							strSubmitExam=strSubmitExam+"<td width='10' align='right'><img src='../images/close.gif' border='0' alt='�ر�' onClick='selecttest("+intNotFinished+");' style='cursor:hand' ></td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"<tr>";
							strSubmitExam=strSubmitExam+"<td colspan='2' width='320' height='90' align='center' valign='middle' bgcolor='#CCCCCC'>";
							strSubmitExam=strSubmitExam+"<img src='../images/warning.gif' hspace='5' align='absmiddle'>";
							strSubmitExam=strSubmitExam+"<font style='color:black' >"+strTestTypeTitle+"��"+intNotFinished+"������û�������Ƿ�ȷ������</font>";
							strSubmitExam=strSubmitExam+"</td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"<tr>";
							strSubmitExam=strSubmitExam+"<td colspan='2' height='35' align='center' bgcolor='#CCCCCC'>";
							strSubmitExam=strSubmitExam+"<input class='button' name='yes' type=button value=' ȷ �� ' id='yes' onClick='submit();'>&nbsp;&nbsp;&nbsp;&nbsp;";
							strSubmitExam=strSubmitExam+"<input class='button' name='no' type=button value=' ȡ �� ' id='no'  onClick='selecttest("+intNotFinished+");'>";
							strSubmitExam=strSubmitExam+"</td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"</table>";
							strSubmitExam=strSubmitExam+"</div>";
						}
						else
						{
							strSubmitExam=strSubmitExam+"<div id='submitexam3' style='LEFT:expression((document.body.clientWidth-320)/2);POSITION:absolute;top:expression(document.body.scrollTop+(window.screen.availHeight-90)/2-20);z-index:120;visibility:visible'>";
							strSubmitExam=strSubmitExam+"<table width='320' border=1 cellSpacing=0 borderColorLight=#000000 borderColorDark=#ffffff bgColor=#FFFFFF style='cursor: default' height='142'>";
							strSubmitExam=strSubmitExam+"<tr bgcolor='#336699'>";
							strSubmitExam=strSubmitExam+"<td width='320'><span ><font color='#FFFFFF'>��ʾ��Ϣ</font></span></td>";
							strSubmitExam=strSubmitExam+"<td width='10' align='right' height='14'><a href='#' onClick='cancel();'><img src='../images/close.gif' border='0' alt='�ر�'></a></td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"<tr>";
							strSubmitExam=strSubmitExam+"<td colspan='2' width='320' height='90' align='center' valign='middle' bgcolor='#CCCCCC'>";
							strSubmitExam=strSubmitExam+"<img src='../images/warning.gif' hspace='5' align='absmiddle'>";
							strSubmitExam=strSubmitExam+"<font style='color:black' >������Ŀ�������Ƿ�ȷ������</font>";
							strSubmitExam=strSubmitExam+"</td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"<tr>";
							strSubmitExam=strSubmitExam+"<td colspan='2' height='35' align='center' bgcolor='#CCCCCC'>";
							strSubmitExam=strSubmitExam+"<input class='button' name='yes' type=button value=' ȷ �� ' id='yes' onClick='submit();'>&nbsp;&nbsp;&nbsp;&nbsp;";
							strSubmitExam=strSubmitExam+"<input class='button' name='no' type=button value=' ȡ �� ' id='no'  onClick='cancel();'>";
							strSubmitExam=strSubmitExam+"</td>";
							strSubmitExam=strSubmitExam+"</tr>";
							strSubmitExam=strSubmitExam+"</table>";
							strSubmitExam=strSubmitExam+"</div>";
						}
					}
					else
					{
						strSubmitExam=strSubmitExam+"<div id='submitexam3' style='LEFT:expression((document.body.clientWidth-320)/2);POSITION:absolute;top:expression(document.body.scrollTop+(window.screen.availHeight-90)/2-20);z-index:120;visibility:hidden'>";
						strSubmitExam=strSubmitExam+"<table width='320' border=1 cellSpacing=0 borderColorLight=#000000 borderColorDark=#ffffff bgColor=#FFFFFF style='cursor: default' height='142'>";
						strSubmitExam=strSubmitExam+"<tr bgcolor='#336699'>";
						strSubmitExam=strSubmitExam+"<td width='320'><span ><font color='#FFFFFF'>��ʾ��Ϣ</font></span></td>";
						strSubmitExam=strSubmitExam+"<td width='10' align='right' height='14'><a href='#' onClick='cancel();'><img src='../images/close.gif' border='0' alt='�ر�'></a></td>";
						strSubmitExam=strSubmitExam+"</tr>";
						strSubmitExam=strSubmitExam+"<tr>";
						strSubmitExam=strSubmitExam+"<td colspan='2' width='320' height='90' align='center' valign='middle' bgcolor='#CCCCCC'>";
						strSubmitExam=strSubmitExam+"<img src='../images/warning.gif' hspace='5' align='absmiddle'>";
						strSubmitExam=strSubmitExam+"<font style='color:black' >������Ŀ�������Ƿ�ȷ������</font>";
						strSubmitExam=strSubmitExam+"</td>";
						strSubmitExam=strSubmitExam+"</tr>";
						strSubmitExam=strSubmitExam+"<tr>";
						strSubmitExam=strSubmitExam+"<td colspan='2' height='35' align='center' bgcolor='#CCCCCC'>";
						strSubmitExam=strSubmitExam+"<input class='button' name='yes' type=button value=' ȷ �� ' id='yes' onClick='submit();'>&nbsp;&nbsp;&nbsp;&nbsp;";
						strSubmitExam=strSubmitExam+"<input class='button' name='no' type=button value=' ȡ �� ' id='no'  onClick='cancel();'>";
						strSubmitExam=strSubmitExam+"</td>";
						strSubmitExam=strSubmitExam+"</tr>";
						strSubmitExam=strSubmitExam+"</table>";
						strSubmitExam=strSubmitExam+"</div>";
					}
					if (Request["CheckAnswer"]!=null)//�����
					{
						strCheckAnswer=strCheckAnswer+"<div id='submitexam4' style='Z-INDEX: 120;                  ; LEFT: expression((document.body.clientWidth-435)/2);                  VISIBILITY: visible;                  POSITION: absolute;                  ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-190)/2-20)'>";
						strCheckAnswer=strCheckAnswer+"<table style='CURSOR: default' cellSpacing='0' borderColorDark='#ffffff' width='435' bgColor='#ffffff' borderColorLight='#000000' border='1'>";
						strCheckAnswer=strCheckAnswer+"<tr bgColor='#336699'>";
						strCheckAnswer=strCheckAnswer+"<td width='435'><span><font color='#ffffff'>��ʾ��Ϣ �Ѵ���:<img src='../images/finished.gif' align='absMiddle'>δ����:<img src='../images/nofinished.gif' align='absMiddle'></font></span></td>";
						strCheckAnswer=strCheckAnswer+"<td align='right' width='10'><IMG style='CURSOR: hand' onclick='cancel1();' alt='�ر�' src='../images/close.gif' border='0'></td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"<tr>";
						strCheckAnswer=strCheckAnswer+"<td vAlign='middle' align='center' width='435' colSpan='2' bgColor='#cccccc' height='190'>";

						strCheckAnswer=strCheckAnswer+"<table height='100%' width='100%'>";
						strCheckAnswer=strCheckAnswer+"<tr>";
						strCheckAnswer=strCheckAnswer+"<td>";
						strCheckAnswer=strCheckAnswer+"<div style='FONT-SIZE: 9pt; OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%'>";
						strCheckAnswer=strCheckAnswer+"<table style='FONT-SIZE: 10pt' cellSpacing='0' borderColorDark='#cccccc' cellPadding='0' border='1'>";
						int n=1;

						SqlCmdTest=new SqlDataAdapter("select a.PaperTestTypeID,b.BaseTestType,a.TestTypeTitle,c.TestOrder,c.UserAnswer from PaperTestType a,TestTypeInfo b,UserAnswer c,RubricInfo d where a.TestTypeID=b.TestTypeID and c.RubricID=d.RubricID and d.TestTypeID=a.TestTypeID and c.UserScoreID="+intUserScoreID+" and a.PaperID="+intPaperID+" order by a.PaperTestTypeID,c.TestOrder asc",SqlConn);
						SqlDSTest=new DataSet();
						SqlCmdTest.Fill(SqlDSTest,"UserAnswer");

						int intNotFinished=0;
						string strTestTypeTitle="";
						for(i=0;i<SqlDSTest.Tables["UserAnswer"].Rows.Count;i++)
						{
							strTestTypeTitle=SqlDSTest.Tables["UserAnswer"].Rows[i]["TestTypeTitle"].ToString();
							strBaseTestType=SqlDSTest.Tables["UserAnswer"].Rows[i]["BaseTestType"].ToString();
							strUserAnswer=SqlDSTest.Tables["UserAnswer"].Rows[i]["UserAnswer"].ToString();
							intNotFinished=0;
							switch(strBaseTestType)
							{
								case "��ѡ��":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "��ѡ��":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "�ж���":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer.ToUpper()=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "�����":
									strUserAnswer=strUserAnswer.Trim().Replace(",","");
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "�ʴ���":
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "������":
									strUserAnswer=strUserAnswer.Trim().Replace(" ","");
									if (strUserAnswer=="")
									{
										intNotFinished=intNotFinished+1;
									}
									break;
								case "������":
									break;
							}
							if (intNotFinished!=0)//δ����
							{ 
								if (n==1)
								{
									strCheckAnswer=strCheckAnswer+"<tr>";
									strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"1'>"+(i+1).ToString()+":<img src='../images/nofinished.gif' border='0'></span></td>";
									n=n+1;
								}
								else
								{
									if (n==10)
									{
										strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"1'>"+(i+1).ToString()+":<img src='../images/nofinished.gif' border='0'></span></td>";
										strCheckAnswer=strCheckAnswer+"</tr>";
										n=1;
									}
									else
									{
										strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"1'>"+(i+1).ToString()+":<img src='../images/nofinished.gif' border='0'></span></td>";
										n=n+1;
									}
								}
							}
							else//������
							{
								if (n==1)
								{
									strCheckAnswer=strCheckAnswer+"<tr>";
									strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"2'>"+(i+1).ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
									n=n+1;
								}
								else
								{
									if (n==10)
									{
										strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"2'>"+(i+1).ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
										strCheckAnswer=strCheckAnswer+"</tr>";
										n=1;
									}
									else
									{
										strCheckAnswer=strCheckAnswer+"<td align='right' width='39' height='18'><span id='icon"+(i+1).ToString()+"2'>"+(i+1).ToString()+":<img src='../images/finished.gif' border='0'></span></td>";
										n=n+1;
									}
								}
							}
						}
						strCheckAnswer=strCheckAnswer+"</table>";
						strCheckAnswer=strCheckAnswer+"</div>";
						strCheckAnswer=strCheckAnswer+"</td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"</table>";

						strCheckAnswer=strCheckAnswer+"</td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"<tr>";
						strCheckAnswer=strCheckAnswer+"<td align='center' bgColor='#cccccc' colSpan='2' height='35'><input class='button' id='close' onclick='cancel1();' type='button' value=' �� �� ' name='no'>";
						strCheckAnswer=strCheckAnswer+"</td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"</table>";
						strCheckAnswer=strCheckAnswer+"</div>";
					}
					else
					{
						strCheckAnswer=strCheckAnswer+"<div id='submitexam4' style='Z-INDEX: 120;                  ; LEFT: expression((document.body.clientWidth-435)/2);                  VISIBILITY: hidden;                  POSITION: absolute;                  ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-190)/2-20)'>";
						strCheckAnswer=strCheckAnswer+"<table style='CURSOR: default' cellSpacing='0' borderColorDark='#ffffff' width='435' bgColor='#ffffff' borderColorLight='#000000' border='1'>";
						strCheckAnswer=strCheckAnswer+"<tr bgColor='#336699'>";
						strCheckAnswer=strCheckAnswer+"<td width='435'><span><font color='#ffffff'>��ʾ��Ϣ �Ѵ���:<img src='../images/finished.gif' align='absMiddle'>δ����:<img src='../images/nofinished.gif' align='absMiddle'></font></span></td>";
						strCheckAnswer=strCheckAnswer+"<td align='right' width='10'><IMG style='CURSOR: hand' onclick='cancel1();' alt='�ر�' src='../images/close.gif' border='0'></td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"<tr>";
						strCheckAnswer=strCheckAnswer+"<td vAlign='middle' align='center' width='435' colSpan='2' bgColor='#cccccc' height='190'> </td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"<tr>";
						strCheckAnswer=strCheckAnswer+"<td align='center' bgColor='#cccccc' colSpan='2' height='35'><input class='button' id='close' onclick='cancel1();' type='button' value=' �� �� ' name='no'>";
						strCheckAnswer=strCheckAnswer+"</td>";
						strCheckAnswer=strCheckAnswer+"</tr>";
						strCheckAnswer=strCheckAnswer+"</table>";
						strCheckAnswer=strCheckAnswer+"</div>";
					}

					SqlConn.Close();
					SqlConn.Dispose();
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
