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
using System.IO;

namespace EasyExam.GradeManag
{
	/// <summary>
	/// StatisTest ��ժҪ˵����
	/// </summary>
	public partial class StatisTest : System.Web.UI.Page
	{
		protected int RowNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intCount=0;
		int j=0,intSelA=0,intSelB=0,intSelC=0,intSelD=0,intSelE=0,intSelF=0;
		bool bJoySoftware=false;
	
		#region//*******��ʼ����Ϣ********
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
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intCount=Convert.ToInt32(ObjFun.GetValues("select count(*) as count from UserScore where PaperID="+intPaperID+" and ExamState=1","count"));//����¼��
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
			bJoySoftware=ObjFun.JoySoftware();
//			if (bJoySoftware==false)
//			{
//				ObjFun.Alert("�Բ���δע���û����ܽ����Ծ�ͳ�ƣ�");
//			}
			strSql="select e.PaperTestTypeID,a.RubricID,d.TestTypeName,d.BaseTestType,count(a.RubricID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,PaperTest c,TestTypeInfo d,PaperTestType e,UserScore f where a.RubricID=b.RubricID and b.RubricID=c.RubricID and b.TestTypeID=d.TestTypeID and b.TestTypeID=e.TestTypeID and c.PaperID=e.PaperID and f.PaperID=e.PaperID and a.UserScoreID=f.UserScoreID and e.PaperID="+intPaperID+" and f.ExamState=1 group by a.RubricID,d.TestTypeName,d.BaseTestType,e.PaperTestTypeID,c.PaperTestID order by e.PaperTestTypeID,c.PaperTestID asc";
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=6)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					if (intPaperID!=0)
					{
						if (DataGridTest.Attributes["SortExpression"] == null)
						{
							DataGridTest.Attributes["SortExpression"] = "PaperTestTypeID";
							DataGridTest.Attributes["SortDirection"] = "ASC";
						}
						ShowData(strSql);
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
			this.DataGridTest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridTest_PageIndexChanged);
			this.DataGridTest.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridTest_SortCommand);
			this.DataGridTest.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridTest_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridTest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridTest.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridTest_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTest').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTest').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridTest.Attributes.Add("oldValue", "#F7F7F7");
				DataGridTest.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******��ʾ�����б�*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserAnswer");
			RowNum=DataGridTest.CurrentPageIndex*DataGridTest.PageSize+1;

			string SortExpression = DataGridTest.Attributes["SortExpression"];
			string SortDirection = DataGridTest.Attributes["SortDirection"];
			SqlDS.Tables["UserAnswer"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTest.DataSource=SqlDS.Tables["UserAnswer"].DefaultView;
			DataGridTest.DataBind();

			SqlDataAdapter SqlCmdTmp=null;
			DataSet SqlDSTmp=null;
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{
				System.Web.UI.WebControls.Image imgRate=(System.Web.UI.WebControls.Image)DataGridTest.Items[i].FindControl("imgRate");
				imgRate.Width=Convert.ToInt32(200*Convert.ToDouble(DataGridTest.Items[i].Cells[6].Text.Trim())/Convert.ToDouble(DataGridTest.Items[i].Cells[5].Text.Trim()));
				imgRate.ToolTip=Convert.ToString(System.Math.Round(Convert.ToDouble(DataGridTest.Items[i].Cells[6].Text.Trim())/Convert.ToDouble(DataGridTest.Items[i].Cells[5].Text.Trim())*100,1))+"%";

				if (DataGridTest.Items[i].Cells[3].Text.Trim()=="��ѡ��")
				{
					intSelA=0;intSelB=0;intSelC=0;intSelD=0;intSelE=0;intSelF=0;
					SqlCmdTmp=new SqlDataAdapter("select a.RubricID,a.UserAnswer from UserAnswer a,UserScore b where a.UserScoreID=b.UserScoreID and a.RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim()+" and b.PaperID="+intPaperID+" and b.ExamState=1",SqlConn);
					SqlDSTmp=new DataSet();
					SqlCmdTmp.Fill(SqlDSTmp,"TestAnswer");
					for(j=0;j<SqlDSTmp.Tables["TestAnswer"].Rows.Count;j++)
					{
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="A")
						{
							intSelA=intSelA+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="B")
						{
							intSelB=intSelB+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="C")
						{
							intSelC=intSelC+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="D")
						{
							intSelD=intSelD+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="E")
						{
							intSelE=intSelE+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper()=="F")
						{
							intSelF=intSelF+1;
						}
					}
					Label labSelA=(Label)DataGridTest.Items[i].FindControl("labSelA");
					labSelA.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelA)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelB=(Label)DataGridTest.Items[i].FindControl("labSelB");
					labSelB.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelB)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelC=(Label)DataGridTest.Items[i].FindControl("labSelC");
					labSelC.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelC)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelD=(Label)DataGridTest.Items[i].FindControl("labSelD");
					labSelD.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelD)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelE=(Label)DataGridTest.Items[i].FindControl("labSelE");
					labSelE.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelE)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelF=(Label)DataGridTest.Items[i].FindControl("labSelF");
					labSelF.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelF)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";
				}
				if (DataGridTest.Items[i].Cells[3].Text.Trim()=="��ѡ��")
				{
					intSelA=0;intSelB=0;intSelC=0;intSelD=0;intSelE=0;intSelF=0;
					SqlCmdTmp=new SqlDataAdapter("select a.RubricID,a.UserAnswer from UserAnswer a,UserScore b where a.UserScoreID=b.UserScoreID and a.RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim()+" and b.PaperID="+intPaperID+" and b.ExamState=1",SqlConn);
					SqlDSTmp=new DataSet();
					SqlCmdTmp.Fill(SqlDSTmp,"TestAnswer");
					for(j=0;j<SqlDSTmp.Tables["TestAnswer"].Rows.Count;j++)
					{
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("A")>=0)
						{
							intSelA=intSelA+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("B")>=0)
						{
							intSelB=intSelB+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("C")>=0)
						{
							intSelC=intSelC+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("D")>=0)
						{
							intSelD=intSelD+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("E")>=0)
						{
							intSelE=intSelE+1;
						}
						if (SqlDSTmp.Tables["TestAnswer"].Rows[j]["UserAnswer"].ToString().ToUpper().IndexOf("F")>=0)
						{
							intSelF=intSelF+1;
						}
					}
					Label labSelA=(Label)DataGridTest.Items[i].FindControl("labSelA");
					labSelA.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelA)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelB=(Label)DataGridTest.Items[i].FindControl("labSelB");
					labSelB.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelB)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelC=(Label)DataGridTest.Items[i].FindControl("labSelC");
					labSelC.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelC)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelD=(Label)DataGridTest.Items[i].FindControl("labSelD");
					labSelD.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelD)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelE=(Label)DataGridTest.Items[i].FindControl("labSelE");
					labSelE.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelE)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";

					Label labSelF=(Label)DataGridTest.Items[i].FindControl("labSelF");
					labSelF.Text=Convert.ToString(System.Math.Round((Convert.ToDouble(intSelF)/SqlDSTmp.Tables["TestAnswer"].Rows.Count)*100,1))+"%";
				}
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridTest_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex>0)
			{
				DataGridTest.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex<(DataGridTest.PageCount-1))
			{
				DataGridTest.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		private void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=(DataGridTest.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******����ͳ������*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			strSql="select d.TestTypeName,d.BaseTestType,count(a.RubricID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,PaperTest c,TestTypeInfo d,PaperTestType e,UserScore f where a.RubricID=b.RubricID and b.RubricID=c.RubricID and b.TestTypeID=d.TestTypeID and b.TestTypeID=e.TestTypeID and c.PaperID=e.PaperID and f.PaperID=e.PaperID and a.UserScoreID=f.UserScoreID and e.PaperID="+intPaperID+" and f.ExamState=1 group by a.RubricID,d.TestTypeName,d.BaseTestType,e.PaperTestTypeID,c.PaperTestID order by e.PaperTestTypeID,c.PaperTestID asc";

			//������Excel�ļ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//׼���ļ�
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"StatisTest.xls",Server.MapPath("..\\UpLoadFiles\\")+"StatisTest.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"StatisTest.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��¼Ϊ�գ����ܵ�����')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//�����ļ�
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=StatisTest.xls");
		}
		#endregion

		#region//*******��������*******
		private void DataGridTest_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridTest.Columns.Count; i++)
			{
				DataGridTest.Columns[i].HeaderText = (DataGridTest.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridTest.Columns[i].HeaderText = (DataGridTest.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridTest.Columns.Count; i++)
			{
				if (DataGridTest.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridTest.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridTest.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridTest.Attributes["SortExpression"] = SortExpression;
			DataGridTest.Attributes["SortDirection"] = SortDirection;
			if (DataGridTest.Attributes["SortDirection"] == "ASC")
			{
				DataGridTest.Columns[colindex].HeaderText = DataGridTest.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridTest.Columns[colindex].HeaderText = DataGridTest.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
