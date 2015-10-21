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

namespace EasyExam.RubricManag
{
	/// <summary>
	/// CountTestDist 的摘要说明。
	/// </summary>
	public partial class CountTestDist : System.Web.UI.Page
	{
		protected string strTestType="";
		protected string strTestLore="";
		protected string strTestDiff="";

		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSubjectID=0;
		string strSubjectName="";
	
		#region//*******初始化信息********
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myLoginID=Session["LoginID"].ToString();
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			strSubjectName=Convert.ToString(Request["SubjectName"]);
			labSubject.Text=Convert.ToString(Request["SubjectName"]);
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					if (intSubjectID!=0)
					{
						CountTestType(intSubjectID);//以题型名称统计
						CountTestLore(intSubjectID);//以试题知识点统计
						CountTestDiff(intSubjectID);//以试题难度统计
					}
				}
			}

			
		}
		#endregion

		#region//******以题型名称统计*******
		private void CountTestType(int SubjectID)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.TestTypeID,b.TestTypeName,Count(*) as TestCount from RubricInfo a,TestTypeInfo b where a.SubjectID="+SubjectID+" and a.TestTypeID=b.TestTypeID group by a.TestTypeID,b.TestTypeName order by a.TestTypeID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");
			for(int i=0;i<SqlDS.Tables["RubricInfo"].Rows.Count;i++)
			{
				strTestType=strTestType+"<tr>";
				strTestType=strTestType+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i]["TestTypeName"].ToString()+"：</STRONG></td>";
				strTestType=strTestType+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i]["TestCount"].ToString()+"道</FONT></td>";
				strTestType=strTestType+"</tr>";
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//******以试题知识点统计*******
		private void CountTestLore(int SubjectID)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select a.LoreID,b.LoreName,Count(*) as TestCount from RubricInfo a,LoreInfo b where a.SubjectID="+SubjectID+" and a.LoreID=b.LoreID group by a.LoreID,b.LoreName order by a.LoreID asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");
			for(int i=0;i<SqlDS.Tables["RubricInfo"].Rows.Count;i++)
			{
				strTestLore=strTestLore+"<tr>";
				strTestLore=strTestLore+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i]["LoreName"].ToString()+"：</STRONG></td>";
				strTestLore=strTestLore+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i]["TestCount"].ToString()+"道</FONT></td>";
				strTestLore=strTestLore+"</tr>";
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//******以试题难度统计*******
		private void CountTestDiff(int SubjectID)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select TestDiff,Count(*) as TestCount from RubricInfo where SubjectID="+SubjectID+" group by TestDiff order by TestDiff asc",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");
			for(int i1=0;i1<SqlDS.Tables["RubricInfo"].Rows.Count;i1++)
			{
				if (SqlDS.Tables["RubricInfo"].Rows[i1]["TestDiff"].ToString()=="易")
				{
					strTestDiff=strTestDiff+"<tr>";
					strTestDiff=strTestDiff+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i1]["TestDiff"].ToString()+"：</STRONG></td>";
					strTestDiff=strTestDiff+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i1]["TestCount"].ToString()+"道</FONT></td>";
					strTestDiff=strTestDiff+"</tr>";
				}
			}
			for(int i2=0;i2<SqlDS.Tables["RubricInfo"].Rows.Count;i2++)
			{
				if (SqlDS.Tables["RubricInfo"].Rows[i2]["TestDiff"].ToString()=="较易")
				{
					strTestDiff=strTestDiff+"<tr>";
					strTestDiff=strTestDiff+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i2]["TestDiff"].ToString()+"：</STRONG></td>";
					strTestDiff=strTestDiff+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i2]["TestCount"].ToString()+"道</FONT></td>";
					strTestDiff=strTestDiff+"</tr>";
				}
			}
			for(int i3=0;i3<SqlDS.Tables["RubricInfo"].Rows.Count;i3++)
			{
				if (SqlDS.Tables["RubricInfo"].Rows[i3]["TestDiff"].ToString()=="中等")
				{
					strTestDiff=strTestDiff+"<tr>";
					strTestDiff=strTestDiff+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i3]["TestDiff"].ToString()+"：</STRONG></td>";
					strTestDiff=strTestDiff+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i3]["TestCount"].ToString()+"道</FONT></td>";
					strTestDiff=strTestDiff+"</tr>";
				}
			}
			for(int i4=0;i4<SqlDS.Tables["RubricInfo"].Rows.Count;i4++)
			{
				if (SqlDS.Tables["RubricInfo"].Rows[i4]["TestDiff"].ToString()=="较难")
				{
					strTestDiff=strTestDiff+"<tr>";
					strTestDiff=strTestDiff+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i4]["TestDiff"].ToString()+"：</STRONG></td>";
					strTestDiff=strTestDiff+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i4]["TestCount"].ToString()+"道</FONT></td>";
					strTestDiff=strTestDiff+"</tr>";
				}
			}
			for(int i5=0;i5<SqlDS.Tables["RubricInfo"].Rows.Count;i5++)
			{
				if (SqlDS.Tables["RubricInfo"].Rows[i5]["TestDiff"].ToString()=="难")
				{
					strTestDiff=strTestDiff+"<tr>";
					strTestDiff=strTestDiff+"<td style='WIDTH: 150px' align='right' height='14'><STRONG>"+SqlDS.Tables["RubricInfo"].Rows[i5]["TestDiff"].ToString()+"：</STRONG></td>";
					strTestDiff=strTestDiff+"<td height='14'><FONT color='red'>"+SqlDS.Tables["RubricInfo"].Rows[i5]["TestCount"].ToString()+"道</FONT></td>";
					strTestDiff=strTestDiff+"</tr>";
				}
			}
			SqlConn.Dispose();
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
