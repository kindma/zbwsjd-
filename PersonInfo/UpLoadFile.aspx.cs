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

namespace EasyExam.PersonInfo
{
	/// <summary>
	/// UpLoadFile ��ժҪ˵����
	/// </summary>
	public partial class UpLoadFile : System.Web.UI.Page
	{
		
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intTestNum=0,intUserScoreID=0,intRubricID=0;

		#region//*********��ʼ��Ϣ*******
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
				intTestNum=Convert.ToInt32(Request["TestNum"]);
				intUserScoreID=Convert.ToInt32(Request["UserScoreID"]);
				intRubricID=Convert.ToInt32(Request["RubricID"]);
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

		#region//********�ύ��ť�¼�*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			string str="";
			string strTestFileName="";
			byte[] fileBinaryData;
			fileBinaryData=new byte[0];
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
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���ϴ��ļ���')</script>");
				return;
			}
			//���浽���ݿ�
			string strConn="";
			string strSql="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			strSql="Update UserAnswer set TestFile=@TmpTestFile,TestFileName=@TmpTestFileName where UserScoreID="+intUserScoreID+" and RubricID="+intRubricID+"";
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);

			if (fileBinaryData.Length>0)
			{
				SqlParameter ParamTestFile=new SqlParameter("@TmpTestFile",SqlDbType.Image);
				ParamTestFile.Value = fileBinaryData;
				ObjCmd.Parameters.Add(ParamTestFile);
			}
			else
			{
				SqlParameter ParamTestFile=new SqlParameter("@TmpTestFile",SqlDbType.Image);
				ParamTestFile.Value = System.DBNull.Value;
				ObjCmd.Parameters.Add(ParamTestFile);
			}

			SqlParameter ParamTestFileName=new SqlParameter("@TmpTestFileName",SqlDbType.VarChar,255);
			ParamTestFileName.Value = strTestFileName;
			ObjCmd.Parameters.Add(ParamTestFileName);

			ObjConn.Open();
			ObjCmd.ExecuteNonQuery();
			ObjConn.Close();
			ObjConn.Dispose();

			this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�ļ��ϴ��ɹ���');var obj=window.dialogArguments;obj.document.all('Answer'+"+intTestNum+").value='"+strTestFileName+"';window.close();</script>");
		}
		#endregion

	}
}
