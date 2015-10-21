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
	/// CountTest ��ժҪ˵����
	/// </summary>
	public partial class CountTest : System.Web.UI.Page
	{
		protected int RowNum=0;

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

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
			strSql="select a.SubjectID,b.SubjectName,Count(*) as TestCount from RubricInfo a,SubjectInfo b where a.SubjectID=b.SubjectID group by a.SubjectID,b.SubjectName order by a.SubjectID desc";
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					if (DataGridCount.Attributes["SortExpression"] == null)
					{
						DataGridCount.Attributes["SortExpression"] = "SubjectID";
						DataGridCount.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
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
			this.DataGridCount.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridCount_PageIndexChanged);
			this.DataGridCount.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridCount_SortCommand);
			this.DataGridCount.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridCount_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridCount_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridCount.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridCount_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridCount').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridCount').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridCount.Attributes.Add("oldValue", "#F7F7F7");
				DataGridCount.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"RubricInfo");
			RowNum=DataGridCount.CurrentPageIndex*DataGridCount.PageSize+1;

			string SortExpression = DataGridCount.Attributes["SortExpression"];
			string SortDirection = DataGridCount.Attributes["SortDirection"];
			SqlDS.Tables["RubricInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridCount.DataSource=SqlDS.Tables["RubricInfo"].DefaultView;
			DataGridCount.DataBind();
			for(int i=0;i<DataGridCount.Items.Count;i++)
			{
				LinkButton LBCountTestDist=(LinkButton)DataGridCount.Items[i].FindControl("LinkButCountTestDist");
				LBCountTestDist.Attributes.Add("onclick", "jscomNewOpenBySize('CountTestDist.aspx?SubjectID="+DataGridCount.Items[i].Cells[0].Text.Trim()+"&SubjectName="+DataGridCount.Items[i].Cells[2].Text.Trim()+"','CountTestDist',570,375); return false;");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["RubricInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridCount.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridCount.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridCount.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridCount.CurrentPageIndex>0)
			{
				DataGridCount.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridCount.CurrentPageIndex<(DataGridCount.PageCount-1))
			{
				DataGridCount.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridCount.CurrentPageIndex=(DataGridCount.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridCount_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridCount.Columns.Count; i++)
			{
				DataGridCount.Columns[i].HeaderText = (DataGridCount.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridCount.Columns[i].HeaderText = (DataGridCount.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridCount.Columns.Count; i++)
			{
				if (DataGridCount.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridCount.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridCount.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridCount.Attributes["SortExpression"] = SortExpression;
			DataGridCount.Attributes["SortDirection"] = SortDirection;
			if (DataGridCount.Attributes["SortDirection"] == "ASC")
			{
				DataGridCount.Columns[colindex].HeaderText = DataGridCount.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridCount.Columns[colindex].HeaderText = DataGridCount.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
