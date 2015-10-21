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
	/// ManagTestList ��ժҪ˵����
	/// </summary>
	public partial class ManagTestList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.DropDownList DropDownList3;
		protected System.Web.UI.WebControls.DropDownList DropDownList4;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.DropDownList DropDownList5;
		protected System.Web.UI.WebControls.DropDownList DropDownList6;
		protected System.Web.UI.WebControls.DropDownList DropDownList7;
		protected System.Web.UI.WebControls.DropDownList DropDownList8;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
	
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
					ShowTestTypeInfo();//��ʾ��������
					DDLTestTypeName.Items.FindByText("--ȫ��--").Selected=true;

					ButNewTest.Attributes.Add("onclick","javascript:jscomNewOpenBySize('NewTest.aspx','NewTest',688,496); return false;");
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('ȷʵҪɾ��ѡ��������')==false) return false;}");
					strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID order by a.RubricID desc";
					if (DataGridTest.Attributes["SortExpression"] == null)
					{
						DataGridTest.Attributes["SortExpression"] = "RubricID";
						DataGridTest.Attributes["SortDirection"] = "DESC";
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

		#region//*********��ʾ֪ʶ����Ϣ**********
		private void ShowLoreInfo(int SubjectID)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from LoreInfo where SubjectID="+SubjectID+" order by LoreID",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"LoreInfo");
			DDLLoreName.Items.Clear();
			DDLLoreName.DataSource=SqlDS.Tables["LoreInfo"].DefaultView;
			DDLLoreName.DataTextField="LoreName";
			DDLLoreName.DataValueField="LoreID";
			DDLLoreName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--ȫ��--","0");
			DDLLoreName.Items.Add(strTmp);
		}
		#endregion

		#region//*********��ʾ��������**********
		private void ShowTestTypeInfo()
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter("select * from TestTypeInfo order by TestTypeID",SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"TestTypeInfo");
			DDLTestTypeName.Items.Clear();
			DDLTestTypeName.DataSource=SqlDS.Tables["TestTypeInfo"].DefaultView;
			DDLTestTypeName.DataTextField="TestTypeName";
			DDLTestTypeName.DataValueField="TestTypeID";
			DDLTestTypeName.DataBind();
			SqlConn.Dispose();
			ListItem strTmp=new ListItem("--ȫ��--","0");
			DDLTestTypeName.Items.Add(strTmp);
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
			this.DataGridTest.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTest_DeleteCommand);
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

		#region//*******ɾ��ѡ������*******
		private void DataGridTest_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intRubricID=Convert.ToInt32(e.Item.Cells[0].Text);
			if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==e.Item.Cells[7].Text.Trim().ToUpper()))
			{
				if (ObjFun.GetValues("select PaperTestID from PaperTest where RubricID="+intRubricID+"","PaperTestID")=="")
				{
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlCommand SqlCmd=new SqlCommand("delete from RubricInfo where RubricID="+intRubricID+"",SqlConn);
					SqlConn.Open();
					SqlCmd.ExecuteNonQuery();
					SqlConn.Close();
					SqlConn.Dispose();

					//�Զ���ҳ
					if (DataGridTest.Items.Count==1&&DataGridTest.CurrentPageIndex>0)
					{
						DataGridTest.CurrentPageIndex--;
					}        

					ShowData(strSql);
				}
				else
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('����������ʹ�ã�����ɾ��ʹ�ô�������Ծ���ٽ��д˲�����')</script>");
					return;
				}
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
			SqlCmd.Fill(SqlDS,"RubricInfo");
			RowNum=DataGridTest.CurrentPageIndex*DataGridTest.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridTest.Attributes["SortExpression"];
			string SortDirection = DataGridTest.Attributes["SortDirection"];
			SqlDS.Tables["RubricInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTest.DataSource=SqlDS.Tables["RubricInfo"].DefaultView;
			DataGridTest.DataBind();
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				Label labTestContent=(Label)DataGridTest.Items[i].FindControl("labTestContent");
				DataGridTest.Items[i].Cells[6].ToolTip=labTestContent.Text;
				labTestContent.Text=Server.HtmlEncode(labTestContent.Text);
				if (labTestContent.Text.Trim().Length>20)
				{
					labTestContent.Text=labTestContent.Text.Trim().Substring(0,20)+"...";
				}
				//labTestContent.Text=ObjFun.getStr(labTestContent.Text.Trim(),20)+"...";

				LinkButton LBEditTest=(LinkButton)DataGridTest.Items[i].FindControl("LinkButEditTest");
				LinkButton LBDel=(LinkButton)DataGridTest.Items[i].FindControl("LinkButDel");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridTest.Items[i].Cells[7].Text.Trim().ToUpper()))
				{
					LBEditTest.Attributes.Add("onclick", "javascript:jscomNewOpenBySize('EditTest.aspx?RubricID="+DataGridTest.Items[i].Cells[0].Text+"','EditTest',688,496); return false;");
					LBDel.Attributes.Add("onclick", "javascript:{if(confirm('ȷ��Ҫɾ��ѡ��������')==false) return false;}");
				}
				else
				{
					LBEditTest.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('�Բ�����û�д˲���Ȩ�ޣ�');return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["RubricInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridTest.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridTest.CurrentPageIndex+1);
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

					if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridTest.Items[i].Cells[7].Text.Trim().ToUpper()))
					{
						if (ObjFun.GetValues("select PaperTestID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim()+"","PaperTestID")=="")
						{
							SqlCmd=new SqlCommand("delete from RubricInfo where RubricID="+DataGridTest.Items[i].Cells[0].Text.Trim(),SqlConn);
							SqlCmd.ExecuteNonQuery();
						}
					}

				}
				//�Զ���ҳ
				if(textArray.Length==DataGridTest.Items.Count&&DataGridTest.CurrentPageIndex>0)
				{
					DataGridTest.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//��ʾ����
			}
		}
		#endregion

		#region//*******ȡ���༭��Ϣ*******
		private void DataGridTest_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******���б༭��Ϣ*******
		private void DataGridTest_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridTest.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******������ѯ��Ϣ*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=false;
			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID";
			//��ʾ�Կ�ĿΪ����������
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ��֪ʶ��Ϊ����������
			if (DDLLoreName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (DDLTestTypeName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ���Ѷ�Ϊ����������
			if (DDLTestDiff.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ����������Ϊ����������
			if (txtTestContent.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.RubricID desc";

			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******ѡ�����ı�*******
		protected void DDLSubjectName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
			DDLLoreName.Items.FindByText("--ȫ��--").Selected=true;
		}
		#endregion

		#region//*******������Ϣ*******
		protected void ButExport_Click(object sender, System.EventArgs e)
		{
			if (myLoginID.Trim().ToUpper()=="ADMIN")
			{
				bWhere=false;
				strSql="select b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,a.OptionContent,a.StandardAnswer,a.TestParse from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID";
			}
			else
			{
				bWhere=true;
				strSql="select b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,a.OptionContent,a.StandardAnswer,a.TestParse from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID where a.CreateUserID="+Convert.ToInt32(myUserID)+"";
			}
			//��ʾ�Կ�ĿΪ����������
			if (DDLSubjectName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.SubjectID=b.SubjectID and b.SubjectID='"+DDLSubjectName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ��֪ʶ��Ϊ����������
			if (DDLLoreName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.LoreID=c.LoreID and c.LoreID='"+DDLLoreName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ������Ϊ����������
			if (DDLTestTypeName.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestTypeID=d.TestTypeID and d.TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ���Ѷ�Ϊ����������
			if (DDLTestDiff.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.TestDiff='"+DDLTestDiff.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			//��ʾ����������Ϊ����������
			if (txtTestContent.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
				}
				else
				{
					strSql=strSql+" where a.TestContent like '%"+txtTestContent.Text.Trim()+"%'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.RubricID desc";

			//������Excel�ļ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"RubricInfo");
			if (SqlDS.Tables["RubricInfo"].Rows.Count!=0)
			{
				//׼���ļ�
				File.Copy(Server.MapPath("..\\TempletFiles\\")+"ExportTest.xls",Server.MapPath("..\\UpLoadFiles\\")+"ExportTest.xls",true);
				ObjFun.DataTableToExcel(SqlDS.Tables["RubricInfo"],Server.MapPath("..\\UpLoadFiles\\")+"ExportTest.xls");
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��¼Ϊ�գ����ܵ�����')</script>");
				return;
			}
			SqlConn.Close();
			SqlConn.Dispose();
			//�����ļ�
			Response.Redirect("../TempletFiles/DownLoadFiles.aspx?FileName=ExportTest.xls");
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex>0)
			{
				DataGridTest.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת����һҳ*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridTest.CurrentPageIndex<(DataGridTest.PageCount-1))
			{
				DataGridTest.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******ת�����ҳ*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridTest.CurrentPageIndex=(DataGridTest.PageCount-1);
			ShowData(strSql);
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
