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

namespace EasyExam.RubricManag
{
	/// <summary>
	/// ManagBookList ��ժҪ˵����
	/// </summary>
	public partial class ManagBookList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intSubjectID=0,intChapterID=0,intSectionID=0;
	
		#region//*******��ʼ����Ϣ********
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
			intSubjectID=Convert.ToInt32(Request["SubjectID"]);
			intChapterID=Convert.ToInt32(Request["ChapterID"]);
			intSectionID=Convert.ToInt32(Request["SectionID"]);

			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ShowSubjectInfo();//��ʾ��Ŀ��Ϣ
					DDLSubjectName.Items.FindByText("--ȫ��--").Selected=true;

					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ���½���')==false) return false;}");
					bWhere=false;
					strSql="select a.SectionID,c.SubjectName,b.ChapterName,a.SectionName,a.BrowNumber,d.LoginID as CreateLoginID from SectionInfo a LEFT OUTER JOIN ChapterInfo b ON b.ChapterID=a.ChapterID LEFT OUTER JOIN SubjectInfo c ON c.SubjectID=a.SubjectID LEFT OUTER JOIN UserInfo d ON d.UserID=b.CreateUserID";
					//��ʾ�Կ�ĿΪ����������
					if (intSubjectID!=0)
					{
						if (bWhere)
						{
							strSql=strSql+" and a.SubjectID='"+intSubjectID.ToString()+"'";
						}
						else
						{
							strSql=strSql+" where a.SubjectID='"+intSubjectID.ToString()+"'";
							bWhere=true;
						}
					}
					//��ʾ������Ϊ����������
					if (intChapterID!=0)
					{
						if (bWhere)
						{
							strSql=strSql+" and a.ChapterID='"+intChapterID.ToString()+"'";
						}
						else
						{
							strSql=strSql+" where a.ChapterID='"+intChapterID.ToString()+"'";
							bWhere=true;
						}
					}
					//��ʾ�Խ���Ϊ����������
					if (intSectionID!=0)
					{
						if (bWhere)
						{
							strSql=strSql+" and a.SectionID='"+intSectionID.ToString()+"'";
						}
						else
						{
							strSql=strSql+" where a.SectionID='"+intSectionID.ToString()+"'";
							bWhere=true;
						}
					}
					strSql=strSql+" order by c.SubjectName,b.ChapterID,a.SectionID";
					if (DataGridBook.Attributes["SortExpression"] == null)
					{
						DataGridBook.Attributes["SortExpression"] = "SubjectName";
						DataGridBook.Attributes["SortDirection"] = "ASC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*********��ʾ��Ŀ��Ϣ**********
		private void ShowSubjectInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from SubjectInfo order by SubjectID",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SubjectInfo");
			DDLSubjectName.Items.Clear();
			DDLSubjectName.DataSource=SqlDS.Tables["SubjectInfo"].DefaultView;
			DDLSubjectName.DataTextField="SubjectName";
			DDLSubjectName.DataValueField="SubjectID";
			DDLSubjectName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--ȫ��--","0");
			DDLSubjectName.Items.Add(strTmp);
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
			this.DataGridBook.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridBook_PageIndexChanged);
			this.DataGridBook.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridBook_SortCommand);
			this.DataGridBook.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridBook_DeleteCommand);
			this.DataGridBook.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridBook_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridBook_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridBook.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ɫ�任*******
		private void DataGridBook_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridBook').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridBook').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridBook.Attributes.Add("oldValue", "#F7F7F7");
				DataGridBook.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******ɾ��ѡ���½�*******
		private void DataGridBook_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intSectionID=Convert.ToInt32(e.Item.Cells[0].Text);
			if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==e.Item.Cells[7].Text.Trim().ToUpper()))
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=new SqlCommand("delete from SectionInfo where SectionID="+intSectionID+"",SqlConn);
				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//�Զ���ҳ
				if (DataGridBook.Items.Count==1&&DataGridBook.CurrentPageIndex>0)
				{
					DataGridBook.CurrentPageIndex--;
				}        

				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
				return;
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
			SqlCmd.Fill(SqlDS,"SectionInfo");
			RowNum=DataGridBook.CurrentPageIndex*DataGridBook.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridBook.Attributes["SortExpression"];
			string SortDirection = DataGridBook.Attributes["SortDirection"];
			SqlDS.Tables["SectionInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridBook.DataSource=SqlDS.Tables["SectionInfo"].DefaultView;
			DataGridBook.DataBind();
			for(int i=0;i<DataGridBook.Items.Count;i++)
			{				
				LinkButton LBEditBook=(LinkButton)DataGridBook.Items[i].FindControl("LinkButEditBook");
				LinkButton LBDel=(LinkButton)DataGridBook.Items[i].FindControl("LinkButDel");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridBook.Items[i].Cells[7].Text.Trim().ToUpper()))
				{
					LBEditBook.Attributes.Add("onclick", "javascript:jscomNewOpenByFixSize('EditBook.aspx?SectionID="+DataGridBook.Items[i].Cells[0].Text+"&SectionName="+DataGridBook.Items[i].Cells[5].Text+"','EditBook',688,496); return false;");
					LBDel.Attributes.Add("onclick", "javascript:{if(confirm('ȷ��Ҫɾ��ѡ���½���')==false) return false;}");
				}
				else
				{
					LBEditBook.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["SectionInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridBook.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridBook.CurrentPageIndex+1);
			SqlConn.Dispose();
		}
		#endregion

		#region//*******ɾ��������Ϣ*******
		protected void ButDelete_Click(object sender, System.EventArgs e)
		{
			if (Request["chkSelect"]!=null)
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=null;

				int i=0;
				string[] textArray=Request["chkSelect"].ToString().Split(',');
				for (int j=0;j<textArray.Length;j++)
				{
					i=Convert.ToInt32(textArray[j]);

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridBook.Items[i].Cells[7].Text.Trim().ToUpper()))
					{
						SqlCmd=new SqlCommand("delete from SectionInfo where SectionID="+DataGridBook.Items[i].Cells[0].Text.Trim(),SqlConn);
						SqlCmd.ExecuteNonQuery();
					}

				}
				//�Զ���ҳ
				if(textArray.Length==DataGridBook.Items.Count&&DataGridBook.CurrentPageIndex>0)
				{
					DataGridBook.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*******ȡ���༭��Ϣ*******
		private void DataGridBook_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridBook.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridBook_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridBook.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=false;
			strSql="select a.SectionID,c.SubjectName,b.ChapterName,a.SectionName,a.BrowNumber,d.LoginID as CreateLoginID from SectionInfo a LEFT OUTER JOIN ChapterInfo b ON b.ChapterID=a.ChapterID LEFT OUTER JOIN SubjectInfo c ON c.SubjectID=a.SubjectID LEFT OUTER JOIN UserInfo d ON d.UserID=b.CreateUserID";
			//��ʾ�Կ�ĿΪ����������
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (txtChapterName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and b.ChapterName='"+txtChapterName.Text.Trim()+"'";
				}
				else
				{
					strSql=strSql+" where b.ChapterName='"+txtChapterName.Text.Trim()+"'";
					bWhere=true;
				}
			}
			//��ʾ�Խ���Ϊ����������
			if (txtSectionName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SectionName like '%"+txtSectionName.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.SectionName like '%"+txtSectionName.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by c.SubjectName,b.ChapterID,a.SectionID";

			DataGridBook.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridBook.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridBook.CurrentPageIndex>0)
			{
				DataGridBook.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridBook.CurrentPageIndex<(DataGridBook.PageCount-1))
			{
				DataGridBook.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridBook.CurrentPageIndex=(DataGridBook.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridBook_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridBook.Columns.Count; i++)
			{
				DataGridBook.Columns[i].HeaderText = (DataGridBook.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridBook.Columns[i].HeaderText = (DataGridBook.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridBook.Columns.Count; i++)
			{
				if (DataGridBook.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridBook.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridBook.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridBook.Attributes["SortExpression"] = SortExpression;
			DataGridBook.Attributes["SortDirection"] = SortDirection;
			if (DataGridBook.Attributes["SortDirection"] == "ASC")
			{
				DataGridBook.Columns[colindex].HeaderText = DataGridBook.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridBook.Columns[colindex].HeaderText = DataGridBook.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion

	}
}
