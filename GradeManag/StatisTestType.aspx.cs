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
	/// StatisTestType ��ժҪ˵����
	/// </summary>
	public partial class StatisTestType : System.Web.UI.Page
	{
		protected int RowNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0,intCount=0;
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
			//�������
			Response.Expires=0;
			Response.Buffer=true;
			Response.Clear();
			intPaperID=Convert.ToInt32(Request["PaperID"]);
			intCount=Convert.ToInt32(ObjFun.GetValues("select count(*) as count from UserScore where PaperID="+intPaperID+"and ExamState=1","count"));//����¼��
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
			bJoySoftware=ObjFun.JoySoftware();
//			if (bJoySoftware==false)
//			{
//				ObjFun.Alert("�Բ���δע���û����ܽ����Ծ�ͳ�ƣ�");
//			}

			strSql="select d.PaperTestTypeID,c.TestTypeName,d.TestTypeTitle,count(c.TestTypeID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,TestTypeInfo c,PaperTestType d,UserScore e where a.RubricID=b.RubricID and b.TestTypeID=c.TestTypeID and b.TestTypeID=d.TestTypeID and e.PaperID=d.PaperID and a.UserScoreID=e.UserScoreID and d.PaperID="+intPaperID+" and e.ExamState=1 group by c.TestTypeName,d.TestTypeTitle,d.PaperTestTypeID order by d.PaperTestTypeID asc";
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
						if (DataGridTestType.Attributes["SortExpression"] == null)
						{
							DataGridTestType.Attributes["SortExpression"] = "PaperTestTypeID";
							DataGridTestType.Attributes["SortDirection"] = "ASC";
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
			this.DataGridTestType.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridTestType_PageIndexChanged);
			this.DataGridTestType.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridTest_SortCommand);
			this.DataGridTestType.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridTestType_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridTestType_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridTestType.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridTestType_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTestType').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridTestType').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridTestType.Attributes.Add("oldValue", "#F7F7F7");
				DataGridTestType.Attributes.Add("singleValue", "#FFFFFF");
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
			RowNum=DataGridTestType.CurrentPageIndex*DataGridTestType.PageSize+1;

			string SortExpression = DataGridTestType.Attributes["SortExpression"];
			string SortDirection = DataGridTestType.Attributes["SortDirection"];
			SqlDS.Tables["UserAnswer"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTestType.DataSource=SqlDS.Tables["UserAnswer"].DefaultView;
			DataGridTestType.DataBind();
			for(int i=0;i<DataGridTestType.Items.Count;i++)
			{
				System.Web.UI.WebControls.Image imgRate=(System.Web.UI.WebControls.Image)DataGridTestType.Items[i].FindControl("imgRate");
				imgRate.Width=Convert.ToInt32(200*Convert.ToDouble(DataGridTestType.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridTestType.Items[i].Cells[4].Text.Trim()));
				imgRate.ToolTip=Convert.ToString(System.Math.Round(Convert.ToDouble(DataGridTestType.Items[i].Cells[5].Text.Trim())/Convert.ToDouble(DataGridTestType.Items[i].Cells[4].Text.Trim())*100,1))+"%";
			}
			SqlConn.Dispose();
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridTestType_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTestType.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridTestType.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTestType.CurrentPageIndex>0)
			{
				DataGridTestType.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		private void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTestType.CurrentPageIndex<(DataGridTestType.PageCount-1))
			{
				DataGridTestType.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		private void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridTestType.CurrentPageIndex=(DataGridTestType.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******����ͳ������*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			strSql="select c.TestTypeName,d.TestTypeTitle,count(c.TestTypeID)/"+intCount+" as TestCount,round(sum(a.TestMark)/"+intCount+",1) as TestMark,round(sum(a.UserScore)/"+intCount+",1) as TotalMark,round(round(sum(a.UserScore)/"+intCount+",1)/round(sum(a.TestMark)/"+intCount+",1)*100,1) as Rate from UserAnswer a,RubricInfo b,TestTypeInfo c,PaperTestType d,UserScore e where a.RubricID=b.RubricID and b.TestTypeID=c.TestTypeID and b.TestTypeID=d.TestTypeID and e.PaperID=d.PaperID and a.UserScoreID=e.UserScoreID and d.PaperID="+intPaperID+" and e.ExamState=1 group by c.TestTypeName,d.TestTypeTitle,d.PaperTestTypeID order by d.PaperTestTypeID asc";

			//������Excel�ļ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//׼���ļ�
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"StatisTestType.xls",Server.MapPath("..\\UpLoadFiles\\")+"StatisTestType.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"StatisTestType.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��¼Ϊ�գ����ܵ�����')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//�����ļ�
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=StatisTestType.xls");
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
			for (int i = 0; i < DataGridTestType.Columns.Count; i++)
			{
				DataGridTestType.Columns[i].HeaderText = (DataGridTestType.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridTestType.Columns[i].HeaderText = (DataGridTestType.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridTestType.Columns.Count; i++)
			{
				if (DataGridTestType.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridTestType.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridTestType.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridTestType.Attributes["SortExpression"] = SortExpression;
			DataGridTestType.Attributes["SortDirection"] = SortDirection;
			if (DataGridTestType.Attributes["SortDirection"] == "ASC")
			{
				DataGridTestType.Columns[colindex].HeaderText = DataGridTestType.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridTestType.Columns[colindex].HeaderText = DataGridTestType.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
