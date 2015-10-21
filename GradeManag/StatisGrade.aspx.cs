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
	/// StatisGrade ��ժҪ˵����
	/// </summary>
	public partial class StatisGrade : System.Web.UI.Page
	{
		protected int RowNum=0;

		bool bWhere;
		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
		double dblTotalMark=0;
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
			labPaperName.Text=Convert.ToString(Request["PaperName"]);
//			bJoySoftware=ObjFun.JoySoftware();
//			if (bJoySoftware==false)
//			{
//				ObjFun.Alert("�Բ���δע���û����ܽ����Ծ�ͳ�ƣ�");
//			}
			if (!IsPostBack)
			{
				LoadDeptInfo();//��ʾ������Ϣ
			}
			strSql=LabCondition.Text;
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
						strSql="select a.UserScoreID,b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '�����ʻ�' when 0 then '��ͨ�ʻ�' end as UserType,case b.UserState when 1 then '����' when 0 then '��ֹ' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1 order by a.TotalMark desc";
						if (DataGridGrade.Attributes["SortExpression"] == null)
						{
							DataGridGrade.Attributes["SortExpression"] = "TotalMark";
							DataGridGrade.Attributes["SortDirection"] = "DESC";
						}
						ShowData(strSql);
						LabCondition.Text=strSql;
					}
				}
			}
		}
		#endregion

		#region//******��ʾ������Ϣ******
		private void LoadDeptInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from DeptInfo",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"DeptInfo");
			DDLDept.DataSource=SqlDS.Tables["DeptInfo"].DefaultView;
			DDLDept.DataTextField="DeptName";
			DDLDept.DataValueField="DeptID";
			DDLDept.DataBind();
			SqlConn.Dispose();
			ListItem objTmp=new ListItem("--ȫ��--","0");
			DDLDept.Items.Add(objTmp);
			DDLDept.Items.FindByText("--ȫ��--").Selected=true;
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
			this.DataGridGrade.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridGrade_PageIndexChanged);
			this.DataGridGrade.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridGrade_SortCommand);
			this.DataGridGrade.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridGrade_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridGrade_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridGrade.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridGrade_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridGrade').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridGrade').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridGrade.Attributes.Add("oldValue", "#F7F7F7");
				DataGridGrade.Attributes.Add("singleValue", "#FFFFFF");
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
			SqlCmd.Fill(SqlDS,"UserScore");
			RowNum=DataGridGrade.CurrentPageIndex*DataGridGrade.PageSize+1;

			string SortExpression = DataGridGrade.Attributes["SortExpression"];
			string SortDirection = DataGridGrade.Attributes["SortDirection"];
			SqlDS.Tables["UserScore"].DefaultView.Sort = SortExpression + " " + SortDirection;
			
			DataGridGrade.DataSource=SqlDS.Tables["UserScore"].DefaultView;
			DataGridGrade.DataBind();
			for(int i=0;i<DataGridGrade.Items.Count;i++)
			{	

			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["UserScore"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridGrade.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridGrade.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridGrade_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridGrade.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{		
			bWhere=true;
			strSql="select a.UserScoreID,b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '�����ʻ�' when 0 then '��ͨ�ʻ�' end as UserType,case b.UserState when 1 then '����' when 0 then '��ֹ' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1";
			//��ʾ�Բ���Ϊ����������
			if (DDLDept.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and c.DeptID='"+DDLDept.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where c.DeptID='"+DDLDept.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ���ʺ�Ϊ����������
			if (txtLoginID.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
				}
				else
				{
					strSql=strSql+" where b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (txtUserName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//��ʾ�Գɼ�Ϊ����������
			if ((DDLCondition.SelectedItem.Value!="-1")&&(txtTotalMark.Text.Trim()!=""))
			{
				try
				{
					dblTotalMark=Convert.ToDouble(txtTotalMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��������ȷ�ķ�����')</script>");
					return;
				}
				if (bWhere)
				{
					strSql=strSql+" and round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
				}
				else
				{
					strSql=strSql+" where round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.TotalMark desc";

			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex>0)
			{
				DataGridGrade.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridGrade.CurrentPageIndex<(DataGridGrade.PageCount-1))
			{
				DataGridGrade.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridGrade.CurrentPageIndex=(DataGridGrade.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******����ͳ������*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select b.LoginID,b.UserName,b.UserSex,c.DeptName,d.JobName,case b.UserType when 1 then '�����ʻ�' when 0 then '��ͨ�ʻ�' end as UserType,case b.UserState when 1 then '����' when 0 then '��ֹ' end as UserState,round(a.TotalMark,1) as TotalMark from UserScore a,UserInfo b LEFT OUTER JOIN DeptInfo c ON b.DeptID=c.DeptID LEFT OUTER JOIN JobInfo d ON b.JobID=d.JobID where a.PaperID="+intPaperID+" and a.UserID=b.UserID and a.ExamState=1";
			//��ʾ�Բ���Ϊ����������
			if (DDLDept.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and c.DeptID='"+DDLDept.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where c.DeptID='"+DDLDept.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ���ʺ�Ϊ����������
			if (txtLoginID.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
				}
				else
				{
					strSql=strSql+" where b.LoginID='"+ObjFun.CheckString(txtLoginID.Text.Trim())+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (txtUserName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where b.UserName like '%"+ObjFun.CheckString(txtUserName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//��ʾ�Գɼ�Ϊ����������
			if ((DDLCondition.SelectedItem.Value!="-1")&&(txtTotalMark.Text.Trim()!=""))
			{
				try
				{
					dblTotalMark=Convert.ToDouble(txtTotalMark.Text.Trim());
				}
				catch
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��������ȷ�ķ�����')</script>");
					return;
				}
				if (bWhere)
				{
					strSql=strSql+" and round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
				}
				else
				{
					strSql=strSql+" where round(a.TotalMark,1)"+DDLCondition.SelectedItem.Value+txtTotalMark.Text.Trim()+"";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.TotalMark desc";

			//������Excel�ļ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"UserInfo");
			if (SqlDS.Tables["UserInfo"].Rows.Count!=0)
			{
				//׼���ļ�
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"StatisGrade.xls",Server.MapPath("..\\UpLoadFiles\\")+"StatisGrade.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["UserInfo"],Server.MapPath("..\\UpLoadFiles\\")+"StatisGrade.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��¼Ϊ�գ����ܵ�����')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//�����ļ�
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=StatisGrade.xls");
		}
		#endregion

		#region//*******��������*******
		private void DataGridGrade_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridGrade.Columns.Count; i++)
			{
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridGrade.Columns[i].HeaderText = (DataGridGrade.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridGrade.Columns.Count; i++)
			{
				if (DataGridGrade.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridGrade.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridGrade.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridGrade.Attributes["SortExpression"] = SortExpression;
			DataGridGrade.Attributes["SortDirection"] = SortDirection;
			if (DataGridGrade.Attributes["SortDirection"] == "ASC")
			{
				DataGridGrade.Columns[colindex].HeaderText = DataGridGrade.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridGrade.Columns[colindex].HeaderText = DataGridGrade.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
