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

namespace EasyExam.SystemSet
{
	/// <summary>
	/// ManagSubjectList ��ժҪ˵����
	/// </summary>
	public partial class ManagSubjectList : System.Web.UI.Page
	{
		protected int RowNum=0,LinNum=0;

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
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and LoginID='Admin'","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ButNewMoreSubject.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('NewMoreSubject.aspx','NewMoreSubject',502,280); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ���Ŀ��')==false) return false;}");
					strSql="select * from SubjectInfo order by SubjectID desc";
					if (DataGridSubject.Attributes["SortExpression"] == null)
					{
						DataGridSubject.Attributes["SortExpression"] = "SubjectID";
						DataGridSubject.Attributes["SortDirection"] = "DESC";
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

		#region//*******��ʾ�����б�*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"SubjectInfo");
			RowNum=DataGridSubject.CurrentPageIndex*DataGridSubject.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridSubject.Attributes["SortExpression"];
			string SortDirection = DataGridSubject.Attributes["SortDirection"];
			SqlDS.Tables["SubjectInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridSubject.DataSource=SqlDS.Tables["SubjectInfo"].DefaultView;
			DataGridSubject.DataBind();
			for(int i=0;i<DataGridSubject.Items.Count;i++)
			{	
				LinkButton LBLore=(LinkButton)DataGridSubject.Items[i].FindControl("LinkButEditLore");
				LBLore.Attributes.Add("onclick","javascript:jscomNewOpenByFixSize('ManagLoreList.aspx?SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"&SubjectName="+DataGridSubject.Items[i].Cells[3].Text.Trim()+"','ManagLoreList',560,501); return false;");

				LinkButton LBUser=(LinkButton)DataGridSubject.Items[i].FindControl("LinkButUser");
				LBUser.Attributes.Add("onclick", "var str=window.showModalDialog('SelectSubjectUser.aspx?SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"','','dialogHeight:415px;dialogWidth:490px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;');return false;");

				LinkButton LBDel=(LinkButton)DataGridSubject.Items[i].FindControl("LinkButDel");
				LBDel.Attributes.Add("onclick","javascript:{if(confirm('ȷ��Ҫɾ��ѡ���Ŀ��')==false) return false;}");
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["SubjectInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridSubject.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridSubject.CurrentPageIndex+1);
			SqlConn.Dispose();
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
			this.DataGridSubject.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridSubject_PageIndexChanged);
			this.DataGridSubject.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridSubject_CancelCommand);
			this.DataGridSubject.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridSubject_EditCommand);
			this.DataGridSubject.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridUser_SortCommand);
			this.DataGridSubject.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridSubject_UpdateCommand);
			this.DataGridSubject.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridSubject_DeleteCommand);
			this.DataGridSubject.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridSubject_ItemDataBound);

		}
		#endregion

		#region//*******���ҳ�¼�*******
		private void DataGridSubject_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridSubject.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******���¿�Ŀ����*******
		private void DataGridSubject_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intSubjectID=Convert.ToInt32(e.Item.Cells[0].Text.Trim());
			string strName=((TextBox)e.Item.Cells[3].Controls[0]).Text;
			string strOldName=e.Item.Cells[6].Text.Trim();
			string strTmp=ObjFun.GetValues("select SubjectName from SubjectInfo where SubjectName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"'and SubjectID<>"+intSubjectID+"","SubjectName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=new SqlCommand("update SubjectInfo set SubjectName='"+ObjFun.getStr(ObjFun.CheckString(strName.Trim()),20)+"' where SubjectID="+intSubjectID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				DataGridSubject.EditItemIndex=-1;
				SqlConn.Close();
				SqlConn.Dispose();
				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�˿�Ŀ�����Ѿ����ڣ�')</script>");
				return;
			}
		}
		#endregion	
			
		#region//*******������ɫ�任*******
		private void DataGridSubject_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridSubject').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridSubject').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridSubject.Attributes.Add("oldValue", "#F7F7F7");
				DataGridSubject.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridSubject_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridSubject.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******ɾ��ѡ�п�Ŀ*******
		private void DataGridSubject_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intSubjectID=Convert.ToInt32(e.Item.Cells[0].Text);
			if ((ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+intSubjectID+"","LoreID")=="")&&(ObjFun.GetValues("select RubricID from RubricInfo where SubjectID="+intSubjectID+"","RubricID")=="")&&(ObjFun.GetValues("select ChapterID from ChapterInfo where SubjectID="+intSubjectID+"","ChapterID")=="")&&(ObjFun.GetValues("select SectionID from SectionInfo where SubjectID="+intSubjectID+"","SectionID")==""))
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=null;
				SqlConn.Open();
				//ɾ�����ݱ�SubjectUser
				SqlCmd=new SqlCommand("delete from SubjectUser where SubjectID="+intSubjectID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				//ɾ�����ݱ�SubjectInfo
				SqlCmd=new SqlCommand("delete from SubjectInfo where SubjectID="+intSubjectID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				
				SqlConn.Close();
				SqlConn.Dispose();
				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�˿�Ŀ����ʹ�ã�����ɾ����Ӧ֪ʶ�㡢������½����ݺ��ٽ��д˲�����')</script>");
				return;
			}
		}
		#endregion
		
		#region//*******ȡ���༭��Ϣ*******
		private void DataGridSubject_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridSubject.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******��ӿ�Ŀ����*******	
		protected void ButNewDept_Click(object sender, System.EventArgs e)
		{
			if (txtSubjectName.Text.Trim()=="")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��Ŀ���Ʋ���Ϊ�գ�')</script>");
				return;
			}
			string strTmp=ObjFun.GetValues("select SubjectName from SubjectInfo where SubjectName='"+ObjFun.getStr(ObjFun.CheckString(txtSubjectName.Text.Trim()),20)+"'","SubjectName");
			if (strTmp=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=new SqlCommand("Insert into SubjectInfo(SubjectName,BrowAccount) values('"+ObjFun.getStr(ObjFun.CheckString(txtSubjectName.Text.Trim()),20)+"',1)",SqlConn);
				SqlConn.Open();
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ӿ�Ŀ���Ƴɹ���')</script>");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�˿�Ŀ�����Ѿ����ڣ�')</script>");
				return;
			}
			txtSubjectName.Text="";
			ShowData(strSql);
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

					if ((ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"","LoreID")=="")&&(ObjFun.GetValues("select RubricID from RubricInfo where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"","RubricID")=="")&&(ObjFun.GetValues("select ChapterID from ChapterInfo where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"","ChapterID")=="")&&(ObjFun.GetValues("select SectionID from SectionInfo where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"","SectionID")==""))
					{
						//ɾ�����ݱ�SubjectUser
						SqlCmd=new SqlCommand("delete from SubjectUser where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//ɾ�����ݱ�SubjectInfo
						SqlCmd=new SqlCommand("delete from SubjectInfo where SubjectID="+DataGridSubject.Items[i].Cells[0].Text.Trim()+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//�Զ���ҳ
				if(textArray.Length==DataGridSubject.Items.Count&&DataGridSubject.CurrentPageIndex>0)
				{
					DataGridSubject.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			//��ʾ������Ϊ����������
			if (txtSubjectName.Text.Trim()=="")
			{
				strSql="select * from SubjectInfo order by SubjectID desc";
			}
			else
			{
				strSql="select * from SubjectInfo where SubjectName like '%"+ObjFun.CheckString(txtSubjectName.Text.Trim())+"%' order by SubjectID desc";
			}

			DataGridSubject.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridSubject.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridSubject.CurrentPageIndex>0)
			{
				DataGridSubject.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridSubject.CurrentPageIndex<(DataGridSubject.PageCount-1))
			{
				DataGridSubject.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridSubject.CurrentPageIndex=(DataGridSubject.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******��������*******
		private void DataGridUser_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//�������
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//���֮ǰ��ͼ��
			for (int i = 0; i < DataGridSubject.Columns.Count; i++)
			{
				DataGridSubject.Columns[i].HeaderText = (DataGridSubject.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridSubject.Columns[i].HeaderText = (DataGridSubject.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//�ҵ��������HeaderText��������
			for (int i = 0; i < DataGridSubject.Columns.Count; i++)
			{
				if (DataGridSubject.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridSubject.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridSubject.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridSubject.Attributes["SortExpression"] = SortExpression;
			DataGridSubject.Attributes["SortDirection"] = SortDirection;
			if (DataGridSubject.Attributes["SortDirection"] == "ASC")
			{
				DataGridSubject.Columns[colindex].HeaderText = DataGridSubject.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridSubject.Columns[colindex].HeaderText = DataGridSubject.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
