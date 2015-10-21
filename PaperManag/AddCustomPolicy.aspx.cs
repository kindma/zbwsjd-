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
	/// AddCustomPolicy ��ժҪ˵����
	/// </summary>
	public partial class AddCustomPolicy : System.Web.UI.Page
	{

		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		int intPaperID=0;
	
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
			strSql=LabCondition.Text;
			if (!IsPostBack)
			{	
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=4)))","UserType")!="1")
				{
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ShowSubjectInfo();//��ʾ��Ŀ��Ϣ
					DDLSubjectName.Items.FindByText("--��ѡ��--").Selected=true;
					ShowTestTypeInfo();//��ʾ��������
					DDLTestTypeName.Items.FindByText("--��ѡ��--").Selected=true;
					strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
					if (DataGridTest.Attributes["SortExpression"] == null)
					{
						DataGridTest.Attributes["SortExpression"] = "RubricID";
						DataGridTest.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
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
			ListItem strTmp=new ListItem("--��ѡ��--","0");
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
			ListItem strTmp=new ListItem("--��ѡ��--","0");
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
			ListItem strTmp=new ListItem("--��ѡ��--","0");
			DDLTestTypeName.Items.Add(strTmp);

		}
		#endregion

		#region//*******��Ŀѡ�����ı�*******
		protected void DDLSubjectName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowLoreInfo(Convert.ToInt32(DDLSubjectName.SelectedValue));
			DDLLoreName.Items.FindByText("--��ѡ��--").Selected=true;
			ShowTestTypeInfo();
			DDLTestTypeName.Items.FindByText("--��ѡ��--").Selected=true;

			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******֪ʶ��ѡ�����ı�*******
		protected void DDLLoreName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowTestTypeInfo();
			DDLTestTypeName.Items.FindByText("--��ѡ��--").Selected=true;

			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******����ѡ�����ı�*******
		protected void DDLTestTypeName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			strSql="select a.RubricID,b.SubjectName,c.LoreName,d.TestTypeName,a.TestDiff,a.TestMark,a.TestContent,e.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from RubricInfo a LEFT OUTER JOIN SubjectInfo b ON a.SubjectID=b.SubjectID LEFT OUTER JOIN LoreInfo c ON a.LoreID=c.LoreID LEFT OUTER JOIN TestTypeInfo d ON a.TestTypeID=d.TestTypeID LEFT OUTER JOIN UserInfo e ON a.CreateUserID=e.UserID where a.SubjectID="+DDLSubjectName.SelectedItem.Value+" and a.LoreID="+DDLLoreName.SelectedItem.Value+" and a.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" order by a.RubricID desc";
			
			DataGridTest.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
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
			SqlCmd.Fill(SqlDS,"RubricInfo");

			string SortExpression = DataGridTest.Attributes["SortExpression"];
			string SortDirection = DataGridTest.Attributes["SortDirection"];
			SqlDS.Tables["RubricInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridTest.DataSource=SqlDS.Tables["RubricInfo"].DefaultView;
			DataGridTest.DataBind();
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				CheckBox chkCheckBoxSel=(CheckBox)DataGridTest.Items[i].FindControl("CheckBoxSel");
			    string strTmp=ObjFun.GetValues("select RubricID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"","RubricID");
				if (strTmp!="")
				{
					chkCheckBoxSel.Checked=true;
				}

				Label labTestContent=(Label)DataGridTest.Items[i].FindControl("labTestContent");
				DataGridTest.Items[i].Cells[7].ToolTip=labTestContent.Text;
				labTestContent.Text=Server.HtmlEncode(labTestContent.Text);
				if (labTestContent.Text.Trim().Length>20)
				{
					labTestContent.Text=labTestContent.Text.Trim().Substring(0,20)+"...";
				}
				//labTestContent.Text=ObjFun.getStr(labTestContent.Text.Trim(),20)+"...";
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["RubricInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridTest.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridTest.CurrentPageIndex+1);
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

		#region//*******ѡ������*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
//			if (ObjFun.JoySoftware()==false)
//			{
//				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ���δע���û�������Ӳ��ԣ�')</script>");
//				return;
//			}
			if (DDLSubjectName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���Ŀ���ƣ�')</script>");
				return;
			}
			if (DDLLoreName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ��֪ʶ�㣡')</script>");
				return;
			}
			if (DDLTestTypeName.SelectedItem.Value=="0")
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ���������ƣ�')</script>");
				return;
			}
			//���浽���ݿ�
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlConnection ObjConn=new SqlConnection(strConn);
			ObjConn.Open();
			SqlCommand ObjCmd=new SqlCommand();
			ObjCmd.Connection=ObjConn;
			//�����������
			if (ObjFun.GetValues("select PaperPolicyID from PaperPolicy where SubjectID='"+DDLSubjectName.SelectedItem.Value+"' and LoreID='"+DDLLoreName.SelectedItem.Value+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"' and PaperID='"+intPaperID+"'","PaperPolicyID")=="")
			{
				ObjCmd.CommandText="Insert into PaperPolicy(PaperID,SubjectID,LoreID,TestTypeID,TestDiff1,TestDiff2,TestDiff3,TestDiff4,TestDiff5) values('"+intPaperID+"','"+DDLSubjectName.SelectedItem.Value+"','"+DDLLoreName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Value+"',0,0,0,0,0)";
				ObjCmd.ExecuteNonQuery();
			}
			if (ObjFun.GetValues("select PaperTestTypeID from PaperTestType where PaperID='"+intPaperID+"' and TestTypeID='"+DDLTestTypeName.SelectedItem.Value+"'","PaperTestTypeID")=="")
			{
				ObjCmd.CommandText="Insert into PaperTestType(PaperID,TestTypeID,TestTypeTitle,TestTypeMark,TestAmount) values('"+intPaperID+"','"+DDLTestTypeName.SelectedItem.Value+"','"+DDLTestTypeName.SelectedItem.Text+"',0,0)";
				ObjCmd.ExecuteNonQuery();
			}
			//��������
			for(int i=0;i<DataGridTest.Items.Count;i++)
			{				
				CheckBox chkCheckBoxSel=(CheckBox)DataGridTest.Items[i].FindControl("CheckBoxSel");
				if (chkCheckBoxSel.Checked==true)
				{
					string strTmp=ObjFun.GetValues("select RubricID from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"","RubricID");
					if (strTmp=="")
					{
						ObjCmd.CommandText="Insert into PaperTest(PaperID,RubricID,TestMark) values('"+intPaperID+"','"+DataGridTest.Items[i].Cells[1].Text+"','"+DataGridTest.Items[i].Cells[6].Text+"')";
						ObjCmd.ExecuteNonQuery();
					}
				}
				else
				{
					ObjCmd.CommandText="delete from PaperTest where RubricID="+DataGridTest.Items[i].Cells[1].Text+" and PaperID="+intPaperID+"";
					ObjCmd.ExecuteNonQuery();
				}
			}
			//������������
			ObjCmd.CommandText="Update PaperTestType set TestAmount="+Convert.ToInt32(ObjFun.GetValues("select Count(*) as TestCount from PaperTest a,RubricInfo b where a.RubricID=b.RubricID and b.TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"","TestCount"))+" where TestTypeID="+DDLTestTypeName.SelectedItem.Value+" and PaperID="+intPaperID+"";
			ObjCmd.ExecuteNonQuery();

			ObjConn.Close();
			ObjConn.Dispose();

			this.RegisterStartupScript("newWindow","<script language='javascript'>alert('���������Գɹ���');</script>");
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
