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


namespace EasyExam.ProcessManag
{
	/// <summary>
	/// JudgeAnswer ��ժҪ˵����
	/// </summary>
	public partial class JudgeAnswer : System.Web.UI.Page
	{

		protected string strPaperName="";
		protected string strTestCount="";
		protected string strPaperMark="";
		protected string strPaperContent="";

		protected string strLoginID="";
		protected string strUserName="";
		protected string strExamTime="";
		protected string strPassMark="";
		protected string strTotalMark="";

		protected int intPaperID=0;
		protected int intUserID=0;
		protected int intUserScoreID=0;
		protected int intTestNum=0,intTestNo=0;

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int k=0,intOptionNum=0,intSeeResult=0;
		double dblTotalMark=0;
		string strTestContent="";
		string strManageUser="";
		string[] strArrOptionContent;
        double dblImpScore=0,dblSubScore=0,dblUserScore=0;
		int intPassMark=0;
		string strJudgeTestType="";

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

			intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
			strManageUser=Convert.ToString(Request["ManageUser"]);
			dblImpScore=Convert.ToDouble(Request["ImpScore"]);
			intPassMark=Convert.ToInt32(Request["PassMark"]);
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=5)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				if (intUserScoreID!=0)
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand ObjCmd=null; 
					SqlDataAdapter SqlCmd=null,SqlCmdTestType=null,SqlCmdTest=null;
					DataSet SqlDS=null,SqlDSTestType=null,SqlDSTest=null;

					SqlCmd=new SqlDataAdapter("select * from UserScore where UserScoreID="+intUserScoreID+" order by UserScoreID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"UserScore");
					intPaperID=Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["PaperID"]);
					intUserID=Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["UserID"]);
					strExamTime=SqlDS.Tables["UserScore"].Rows[0]["StartTime"].ToString()+"/"+SqlDS.Tables["UserScore"].Rows[0]["EndTime"].ToString();
					strTotalMark=SqlDS.Tables["UserScore"].Rows[0]["TotalMark"].ToString();

					SqlCmd=new SqlDataAdapter("select * from UserInfo where UserID="+intUserID+" order by UserID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"UserInfo");

					strLoginID=SqlDS.Tables["UserInfo"].Rows[0]["LoginID"].ToString();
					strUserName=SqlDS.Tables["UserInfo"].Rows[0]["UserName"].ToString();
					
					SqlCmd=new SqlDataAdapter("select * from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
					SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"PaperInfo");
					strPaperName=SqlDS.Tables["PaperInfo"].Rows[0]["PaperName"].ToString();
					strTestCount=SqlDS.Tables["PaperInfo"].Rows[0]["TestCount"].ToString();
					strPaperMark=SqlDS.Tables["PaperInfo"].Rows[0]["PaperMark"].ToString();
					strPassMark=SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"].ToString();
					intSeeResult=Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
					if ((intSeeResult==0)&&(strManageUser!="1"))
					{
						ObjFun.Alert("���Ծ�����鿴�����");
					}

					intTestNum=0;
					intTestNo=0;
					SqlCmdTestType=new SqlDataAdapter("select a.TestTypeID,b.BaseTestType,a.TestTypeTitle,a.TestTypeMark,a.TestAmount,a.TestTotalMark from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.PaperID="+intPaperID+" order by a.PaperTestTypeID asc",SqlConn);
					SqlDSTestType=new DataSet();
					SqlCmdTestType.Fill(SqlDSTestType,"PaperTestType");
					for(int i=0;i<SqlDSTestType.Tables["PaperTestType"].Rows.Count;i++)
					{
						strJudgeTestType=ObjFun.GetValues("select a.UserID from UserInfo a where a.UserID="+Convert.ToInt32(myUserID)+" and (a.JudgeTestType=1 or (a.JudgeTestType=2 and Exists(select UserPowerID from UserPower where UserPower.UserID="+Convert.ToInt32(myUserID)+" and UserPower.PowerID=2 and UserPower.OptionID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"].ToString()+")))","UserID");
						if (((SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�����")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�ʴ���")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������"))&&(strJudgeTestType!=""))
						{
							dblTotalMark=System.Math.Round(Convert.ToDouble(ObjFun.GetValues("select sum(a.TestMark) as TotalMark from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+"","TotalMark")),1);
							strPaperContent=strPaperContent+"<tr>";
							strPaperContent=strPaperContent+"<td colspan='2' bgcolor='#CEE7FF' height='20' style='CURSOR:hand' onclick='jscomFlexObject(trTestTypeContent"+Convert.ToString(i+1)+")' title='��������ô���'>"+ObjFun.convertint(Convert.ToString(i+1))+"."+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString()+"����"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestAmount"].ToString()+"��,��"+dblTotalMark.ToString()+"�֣�</td>";
							strPaperContent=strPaperContent+"</tr>";
							strPaperContent=strPaperContent+"<tr>";
							strPaperContent=strPaperContent+"<td width='10' nowrap></td>";
							strPaperContent=strPaperContent+"<td width='100%'>";
							strPaperContent=strPaperContent+"<table cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent"+Convert.ToString(i+1)+"'>";
						}

						SqlCmdTest=new SqlDataAdapter("select a.TestOrder,a.RubricID,b.TestTypeID,b.TestDiff,b.OptionNum,a.TestMark,b.TestContent,a.TestFile,a.TestFileName,b.OptionContent,b.StandardAnswer,b.TestParse,a.UserAnswer,a.UserScore from UserAnswer a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.UserScoreID="+intUserScoreID+" order by a.TestOrder asc",SqlConn);
						SqlDSTest=new DataSet();
						SqlCmdTest.Fill(SqlDSTest,"UserAnswer");
						for(int j=0;j<SqlDSTest.Tables["UserAnswer"].Rows.Count;j++)
						{
							intTestNum=intTestNum+1;
							if (((SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�����")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�ʴ���")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������"))&&(strJudgeTestType!=""))
							{
								intTestNo=intTestNo+1;
								strTestContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
								strPaperContent=strPaperContent+"<tr>";
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�����")
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
											strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='"+strArrOptionContent[k-1]+"' onBlur='textcheck()' readonly title='������в��ܰ�����Ƕ��š�,��'>",1);
										}
										else
										{
											strTestContent=Reg.Replace(strTestContent,"<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' size='16' class=filltext value='' onBlur='textcheck()' readonly title='������в��ܰ�����Ƕ��š�,��'>",1);
										}
									}
								}
								strPaperContent=strPaperContent+"<td colspan='2' width='100%'><input type='hidden' id='TestTypeTitle"+intTestNum.ToString()+"' name='TestTypeTitle"+intTestNum.ToString()+"' value='"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString()+"'><input type='hidden' id='RubricID"+intTestNum.ToString()+"' name='RubricID"+intTestNum.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"'><input type='hidden' id='BaseTestType"+intTestNum.ToString()+"' name='BaseTestType"+intTestNum.ToString()+"' value='"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()+"'><a id='l"+intTestNum.ToString()+"' style='color:black'>"+intTestNum.ToString()+"</a>."+strTestContent+"<font color='red'>��"+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestMark"].ToString()+"�֣�</font></td>";
								strPaperContent=strPaperContent+"</tr>";
								//if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="��ѡ��")
								//{
								//	intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
								//	strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
								//	for(k=1;k<=intOptionNum;k++)
								//	{
								//		strPaperContent=strPaperContent+"<tr>";
								//		if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
								//		{
								//			strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' checked disabled>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
								//		}
								//		else
								//		{
								//			strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' disabled>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
								//		}
								//		strPaperContent=strPaperContent+"</tr>";
								//	}
								//}
								//if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="��ѡ��")
								//{
								//	intOptionNum=Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
								//	strArrOptionContent=SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
								//	for(k=1;k<=intOptionNum;k++)
								//	{
								//		strPaperContent=strPaperContent+"<tr>";
								//		if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64+k))>=0)
								//		{
								//			strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' checked disabled>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
								//		}
								//		else
								//		{
								//			strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+Convert.ToChar(64+k)+"' disabled>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
								//		}
								//		strPaperContent=strPaperContent+"</tr>";
								//	}
								//}
								//if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�ж���")
								//{
								//	strPaperContent=strPaperContent+"<tr>";
								//	strPaperContent=strPaperContent+"<td width='10' nowrap>";
								//	if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("��ȷ")>=0)
								//	{
								//		strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='��ȷ' checked disabled>��ȷ</td>";
								//	}
								//	else
								//	{
								//		strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='��ȷ' disabled>��ȷ</td>";
								//	}
								//	strPaperContent=strPaperContent+"</tr>";
								//	strPaperContent=strPaperContent+"<tr>";
								//	strPaperContent=strPaperContent+"<td width='10' nowrap>";
								//	if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("����")>=0)
								//	{
								//		strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='����' checked disabled>����</td>";
								//	}
								//	else
								//	{
								//		strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='����' disabled>����</td>";
								//	}
								//	strPaperContent=strPaperContent+"</tr>";
								//}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�����")
								{
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�ʴ���")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text readonly>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString()+"</TEXTAREA></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text readonly>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString()+"</TEXTAREA></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								//if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")
								//{
								//	if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(",")>=0)
								//	{
								//		strArrTypeUserAnswer=SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');
								//	}
								//	else
								//	{
								//		strArrTypeUserAnswer="0,0".Split(',');
								//	}
								//	strPaperContent=strPaperContent+"<tr>";
								//	strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('TypeWord.aspx?TestNum="+intTestNum+"&UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"',window,'dialogHeight:430px;dialogWidth:570px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='�����ʼ����'>��ʼ����</a>&nbsp;�����ٶȣ�<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[0]+"'>����/����&nbsp;��ȷ�ʣ�<input type='text' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' class=Text size='3' ReadOnly='true' value='"+strArrTypeUserAnswer[1]+"'>%</td>";
								//	strPaperContent=strPaperContent+"</tr>";
								//}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><a href='../PersonInfo/DownLoadFile.aspx?UserScoreID="+intUserScoreID+"&RubricID="+SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString()+"' title='�������'>�����ļ���</a><b>"+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestFileName"].ToString()+"</b></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>��׼�𰸣�"+SqlDSTest.Tables["UserAnswer"].Rows[j]["StandardAnswer"].ToString()+"</font></td>";
								strPaperContent=strPaperContent+"</tr>";

								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>�����𰸣�"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString()+"</font></td>";
								strPaperContent=strPaperContent+"</tr>";

								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>����÷֣�<INPUT type='text' size=4 name='UserScore"+intTestNo.ToString()+"' value='"+SqlDSTest.Tables["UserAnswer"].Rows[j]["UserScore"].ToString()+"' class='scoretext' onblur='checkscore("+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestMark"].ToString()+")'><INPUT type='hidden' size=4 name='TestNum"+intTestNo.ToString()+"' value='"+intTestNum+"'></font></td>";
								strPaperContent=strPaperContent+"</tr>";

								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>���������"+SqlDSTest.Tables["UserAnswer"].Rows[j]["TestParse"].ToString()+"</font></td>";
								strPaperContent=strPaperContent+"</tr>";

								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2' height='10'></td>";
								strPaperContent=strPaperContent+"</tr>";
							}
						}
						if (((SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�����")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="�ʴ���")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������")||(SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="������"))&&(strJudgeTestType!=""))
						{
							strPaperContent=strPaperContent+"</table>";
							strPaperContent=strPaperContent+"</td>";
							strPaperContent=strPaperContent+"</tr>";
						}
					}

					SqlConn.Close();
					SqlConn.Dispose();

					if (intTestNo==0)
					{
						ObjFun.Alert("���Ծ�û����Ҫ�ֶ��������⣡");
					}
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

		#region//*********�ύ����*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlCommand SqlCmd=null;
			SqlConn.Open();

			int intRubricID=0,intPassState=0;
			for(int i=0;i<Convert.ToInt32(Request["irow"]);i++)
			{
				k=Convert.ToInt32(Request["TestNum"+Convert.ToString(i+1)]);
				dblUserScore=Convert.ToDouble(Request["UserScore"+Convert.ToString(i+1)]);
				intRubricID=Convert.ToInt32(Request["RubricID"+k.ToString()]);

				SqlCmd=new SqlCommand("update UserAnswer set UserScore="+dblUserScore+" where UserScoreID="+intUserScoreID+" and RubricID="+intRubricID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();

				dblSubScore=dblSubScore+dblUserScore;
			}
			if ((dblImpScore+dblSubScore)>=intPassMark)
			{
				intPassState=1;
			}
			SqlCmd=new SqlCommand("update UserScore set SubScore="+dblSubScore+",TotalMark="+Convert.ToString(System.Math.Round(dblImpScore+dblSubScore,1))+",JudgeState=1,JudgeUserID="+Convert.ToInt32(myUserID)+",PassState="+intPassState+" where UserScoreID="+intUserScoreID+"",SqlConn);
			SqlCmd.ExecuteNonQuery();

			SqlConn.Close();
			SqlConn.Dispose();

			ObjFun.Alert("���ʺ��ֶ�����÷֣�"+Convert.ToString(System.Math.Round(dblSubScore,1)));
		}
		#endregion

	}
}
