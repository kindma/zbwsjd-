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
	/// PreviewPaper 的摘要说明。
	/// </summary>
	public partial class PreviewPaper : System.Web.UI.Page
	{

		protected string strTestCount="";
		protected string strPaperMark="";
		protected string strPaperName="";
		protected string strPaperContent="";

		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
		int intTestNum=0,k=0,intOptionNum=0;
		string[] strArrOptionContent,strArrTypeStandardAnswer;

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
			strPaperContent="";
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			//if (!IsPostBack)
			//{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=4)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intPaperID!=0)
					{
						string strConn=ConfigurationSettings.AppSettings["strConn"];
						SqlConnection SqlConn=new SqlConnection(strConn);
						SqlDataAdapter SqlCmd=null,SqlCmdTestType=null,SqlCmdTest=null;
						DataSet SqlDS=null,SqlDSTestType=null,SqlDSTest=null;

						SqlCmd=new SqlDataAdapter("select * from PaperInfo where PaperID="+intPaperID+" order by PaperID asc",SqlConn);
						SqlDS=new DataSet();
						SqlCmd.Fill(SqlDS,"PaperInfo");
						strPaperName=SqlDS.Tables["PaperInfo"].Rows[0]["PaperName"].ToString();
						strTestCount=SqlDS.Tables["PaperInfo"].Rows[0]["TestCount"].ToString();
						strPaperMark=SqlDS.Tables["PaperInfo"].Rows[0]["PaperMark"].ToString();
					
						intTestNum=0;
						SqlCmdTestType=new SqlDataAdapter("select a.TestTypeID,b.TestTypeName,b.BaseTestType,a.TestTypeTitle,a.TestTypeMark,a.TestAmount,a.TestTotalMark from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.PaperID="+intPaperID+" order by a.PaperTestTypeID asc",SqlConn);
						SqlDSTestType=new DataSet();
						SqlCmdTestType.Fill(SqlDSTestType,"PaperTestType");
						for(int i=0;i<SqlDSTestType.Tables["PaperTestType"].Rows.Count;i++)
						{
							strPaperContent=strPaperContent+"<tr>";
							strPaperContent=strPaperContent+"<td colspan='2' bgcolor='#CEE7FF' height='20' style='CURSOR:hand' onclick='jscomFlexObject(trTestTypeContent"+Convert.ToString(i+1)+")' title='点击伸缩该大题'>"+ObjFun.convertint(Convert.ToString(i+1))+"."+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeTitle"].ToString()+"（共"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestAmount"].ToString()+"题,共"+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTotalMark"].ToString()+"分）</td>";
							strPaperContent=strPaperContent+"</tr>";
							strPaperContent=strPaperContent+"<tr>";
							strPaperContent=strPaperContent+"<td width='10' nowrap></td>";
							strPaperContent=strPaperContent+"<td width='100%'>";
							strPaperContent=strPaperContent+"<table cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent"+Convert.ToString(i+1)+"'>";

							SqlCmdTest=new SqlDataAdapter("select a.RubricID,b.TestTypeID,b.TestDiff,b.OptionNum,a.TestMark,b.TestContent,b.TestFile,b.TestFileName,b.OptionContent,b.StandardAnswer,b.TestParse from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+SqlDSTestType.Tables["PaperTestType"].Rows[i]["TestTypeID"]+" and a.PaperID="+intPaperID+" order by a.PaperTestID asc",SqlConn);
							SqlDSTest=new DataSet();
							SqlCmdTest.Fill(SqlDSTest,"PaperTest");
							for(int j=0;j<SqlDSTest.Tables["PaperTest"].Rows.Count;j++)
							{
								intTestNum=intTestNum+1;
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2' width='100%'>"+intTestNum.ToString()+"."+SqlDSTest.Tables["PaperTest"].Rows[j]["TestContent"].ToString()+"<font color='red'>（"+SqlDSTest.Tables["PaperTest"].Rows[j]["TestMark"].ToString()+"分）</font></td>";
								strPaperContent=strPaperContent+"</tr>";
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="单选类")
								{
									intOptionNum=Convert.ToInt32(SqlDSTest.Tables["PaperTest"].Rows[j]["OptionNum"]);
									strArrOptionContent=SqlDSTest.Tables["PaperTest"].Rows[j]["OptionContent"].ToString().Split('|');
									for(k=1;k<=intOptionNum;k++)
									{
										strPaperContent=strPaperContent+"<tr>";
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+k.ToString()+"'>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
										strPaperContent=strPaperContent+"</tr>";
									}
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="多选类")
								{
									intOptionNum=Convert.ToInt32(SqlDSTest.Tables["PaperTest"].Rows[j]["OptionNum"]);
									strArrOptionContent=SqlDSTest.Tables["PaperTest"].Rows[j]["OptionContent"].ToString().Split('|');
									for(k=1;k<=intOptionNum;k++)
									{
										strPaperContent=strPaperContent+"<tr>";
										strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='"+k.ToString()+"'>"+Convert.ToChar(64+k)+"."+strArrOptionContent[k-1]+"</td>";
										strPaperContent=strPaperContent+"</tr>";
									}
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="判断类")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10' nowrap>";
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='1'>正确</td>";
									strPaperContent=strPaperContent+"</tr>";
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10' nowrap>";
									strPaperContent=strPaperContent+"<td width='100%'><input type='radio' id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' value='2'>错误</td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="填空类")
								{
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="问答类")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text></TEXTAREA></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="作文类")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer"+intTestNum.ToString()+"' name='Answer"+intTestNum.ToString()+"' style='width:100%' class=Text></TEXTAREA></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="打字类")
								{
								}
								if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="操作类")
								{
									strPaperContent=strPaperContent+"<tr>";
									strPaperContent=strPaperContent+"<td width='10px' nowrap><td width='100%'>下载文件：<b>"+SqlDSTest.Tables["PaperTest"].Rows[j]["TestFileName"].ToString()+"</b></td>";
									strPaperContent=strPaperContent+"</tr>";
								}
								if (RadioButAll.Checked)
								{
									if (SqlDSTestType.Tables["PaperTestType"].Rows[i]["BaseTestType"].ToString()=="打字类")
									{
										strArrTypeStandardAnswer=SqlDSTest.Tables["PaperTest"].Rows[j]["StandardAnswer"].ToString().Split(',');
										strPaperContent=strPaperContent+"<tr>";
										strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>标准答案：标准速度："+strArrTypeStandardAnswer[1]+"个字/分钟&nbsp;正确率：100%</font></td>";
										strPaperContent=strPaperContent+"</tr>";
									}
									else
									{
										strPaperContent=strPaperContent+"<tr>";
										strPaperContent=strPaperContent+"<td colspan='2'><font color='blue'>标准答案："+SqlDSTest.Tables["PaperTest"].Rows[j]["StandardAnswer"].ToString()+"</font></td>";
										strPaperContent=strPaperContent+"</tr>";
									}
								}
								strPaperContent=strPaperContent+"<tr>";
								strPaperContent=strPaperContent+"<td colspan='2' height='10'></td>";
								strPaperContent=strPaperContent+"</tr>";
							}
							strPaperContent=strPaperContent+"</table>";
							strPaperContent=strPaperContent+"</td>";
							strPaperContent=strPaperContent+"</tr>";
						}
					}
				}
			//}
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

		#region
		protected void RadioButAll_CheckedChanged(object sender, System.EventArgs e)
		{
			Page_Load(sender,e);
		}
		#endregion

		#region
		protected void RadioButHiddenAnswer_CheckedChanged(object sender, System.EventArgs e)
		{
			Page_Load(sender,e);
		}
		#endregion
	}
}
