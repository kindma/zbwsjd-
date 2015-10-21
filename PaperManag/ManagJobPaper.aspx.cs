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
	/// ManagJobPaper 的摘要说明。
	/// </summary>
	public partial class ManagJobPaper : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImgButSubject;
		protected System.Web.UI.WebControls.TextBox txtSubjectName;
		protected int RowNum=0,LinNum=0;

		bool bWhere;
		string strSql="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();
		bool bJoySoftware=false;
	
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
			strSql=LabCondition.Text;
			bJoySoftware=ObjFun.JoySoftware();
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=4)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
//					if (bJoySoftware==false)
//					{
//						ButRandPaper.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能新建试卷！');return false;");
//						ButCustomPaper.Attributes.Add("onclick", "javascript:alert('对不起，未注册用户不能新建试卷！');return false;");
//					}
//					else
//					{
						ButRandPaper.Attributes.Add("onclick","javascript:jscomNewOpenBySize('NewRandPaper.aspx?PaperType=2','NewRandPaper',700,600); return false;");
						ButCustomPaper.Attributes.Add("onclick","javascript:jscomNewOpenBySize('NewCustomPaper.aspx?PaperType=2','NewCustomPaper',700,600); return false;");
//					}
					ButDelete.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择试卷吗？')==false) return false;}");
					strSql="select a.PaperID,a.PaperName,a.PaperType,case a.ProduceWay when 1 then '题序固定' when 2 then '题序随机' when 3 then '试题随机' end as ProduceWay,case a.ShowModal when 1 then '整卷模式' when 2 then '逐题模式' end as ShowModal,a.StartTime,a.EndTime,a.TestCount,a.PaperMark,a.CreateWay,b.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from PaperInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where a.PaperType=2 order by a.PaperID desc";
					if (DataGridPaper.Attributes["SortExpression"] == null)
					{
						DataGridPaper.Attributes["SortExpression"] = "PaperID";
						DataGridPaper.Attributes["SortDirection"] = "DESC";
					}
					ShowData(strSql);
					LabCondition.Text=strSql;
				}
			}
			if (Request["hidcommand"]=="RefreshForm")
			{
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******显示数据列表*******
		private void ShowData(string strSql)
		{
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
			DataSet SqlDS=new DataSet();
			SqlCmd.Fill(SqlDS,"PaperInfo");
			RowNum=DataGridPaper.CurrentPageIndex*DataGridPaper.PageSize+1;
			LinNum=0;

			string SortExpression = DataGridPaper.Attributes["SortExpression"];
			string SortDirection = DataGridPaper.Attributes["SortDirection"];
			SqlDS.Tables["PaperInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

			DataGridPaper.DataSource=SqlDS.Tables["PaperInfo"].DefaultView;
			DataGridPaper.DataBind();
			for(int i=0;i<DataGridPaper.Items.Count;i++)
			{				
				Label labAvaiTime=(Label)DataGridPaper.Items[i].FindControl("labAvaiTime");
				labAvaiTime.Text=DataGridPaper.Items[i].Cells[12].Text.Trim()+"/<br>"+DataGridPaper.Items[i].Cells[13].Text.Trim();

				LinkButton LBEditPaper=(LinkButton)DataGridPaper.Items[i].FindControl("LinkButEditPaper");
				LinkButton LBDel=(LinkButton)DataGridPaper.Items[i].FindControl("LinkButDel");
				LinkButton LBPreviewPaper=(LinkButton)DataGridPaper.Items[i].FindControl("LinkButPreviewPaper");

				if ((myLoginID.Trim().ToUpper()=="ADMIN")||(myLoginID.Trim().ToUpper()==DataGridPaper.Items[i].Cells[8].Text.Trim().ToUpper()))
				{
					if (DataGridPaper.Items[i].Cells[14].Text.Trim()=="1")
					{
						LBEditPaper.Attributes.Add("onclick", "jscomNewOpenBySize('EditRandPaper.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"&PaperType="+DataGridPaper.Items[i].Cells[11].Text.Trim()+"','EditRandPaper',700,600);return false;");
					}
					else
					{
						LBEditPaper.Attributes.Add("onclick", "jscomNewOpenBySize('EditCustomPaper.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"&PaperType="+DataGridPaper.Items[i].Cells[11].Text.Trim()+"','EditCustomPaper',700,600);return false;");
					}
					LBDel.Attributes.Add("onclick","javascript:{if(confirm('确定要删除选择试卷吗？')==false) return false;}");
					LBPreviewPaper.Attributes.Add("onclick","javascript:NewWin=window.open('PreviewPaper.aspx?PaperID="+DataGridPaper.Items[i].Cells[0].Text.Trim()+"','PreviewPaper','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewWin.moveTo(0,0);NewWin.resizeTo(screen.availWidth,screen.availHeight);return false;");
				}
				else
				{
					LBEditPaper.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
					LBDel.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
					LBPreviewPaper.Attributes.Add("onclick", "javascript:alert('对不起，您没有此操作权限！');return false;");
				}
			}
			LabelRecord.Text=Convert.ToString(SqlDS.Tables["PaperInfo"].Rows.Count);
			LabelCountPage.Text=Convert.ToString(DataGridPaper.PageCount);
			LabelCurrentPage.Text=Convert.ToString(DataGridPaper.CurrentPageIndex+1);
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
			this.DataGridPaper.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGridPaper_PageIndexChanged);
			this.DataGridPaper.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DataGridPaper_SortCommand);
			this.DataGridPaper.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridPaper_DeleteCommand);
			this.DataGridPaper.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPaper_ItemDataBound);

		}
		#endregion

		#region//*******表格翻页事件*******
		private void DataGridPaper_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGridPaper.CurrentPageIndex=e.NewPageIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******行列颜色变换*******
		private void DataGridPaper_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.ItemIndex != -1 )
			{
				e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

				if (e.Item.ItemIndex % 2 == 0 )
				{
					e.Item.Attributes.Add("bgcolor", "#FFFFFF");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPaper').getAttribute('singleValue')");
				}
				else
				{
					e.Item.Attributes.Add("bgcolor", "#F7F7F7");
					e.Item.Attributes.Add("onmouseout", "this.bgColor=document.getElementById('DataGridPaper').getAttribute('oldValue')");
				}
			}
			else
			{
				DataGridPaper.Attributes.Add("oldValue", "#F7F7F7");
				DataGridPaper.Attributes.Add("singleValue", "#FFFFFF");
			}
		}
		#endregion

		#region//*******删除选择帐户*******
		protected void ButDelete_Click(object sender, System.EventArgs e)
		{
			if (Request["chkSelect"]!=null)
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlConn.Open();
				SqlCommand SqlCmd=null;

				int intPaperID=0;
				string[] textArray=Request["chkSelect"].ToString().Split(',');
				for (int j=0;j<textArray.Length;j++)
				{
					intPaperID=Convert.ToInt32(DataGridPaper.Items[Convert.ToInt32(textArray[j])].Cells[0].Text);
					if (ObjFun.GetValues("select UserScoreID from UserScore where PaperID="+intPaperID+"","UserScoreID")=="")
					{
						//数据表PaperTest
						SqlCmd=new SqlCommand("delete from PaperTest where PaperID="+intPaperID+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//数据表PaperPolicy
						SqlCmd=new SqlCommand("delete from PaperPolicy where PaperID="+intPaperID+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//数据表PaperTestType
						SqlCmd=new SqlCommand("delete from PaperTestType where PaperID="+intPaperID+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//数据表PaperUser
						SqlCmd=new SqlCommand("delete from PaperUser where PaperID="+intPaperID+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
						//数据表PaperInfo
						SqlCmd=new SqlCommand("delete from PaperInfo where PaperID="+intPaperID+"",SqlConn);
						SqlCmd.ExecuteNonQuery();
					}
				}

				//自动翻页
				if(textArray.Length==DataGridPaper.Items.Count&&DataGridPaper.CurrentPageIndex>0)
				{
					DataGridPaper.CurrentPageIndex--;
				}

				SqlConn.Close();
				ShowData(strSql);//显示数据
			}
		}
		#endregion

		#region//*******进行编辑信息*******
		private void DataGridPaper_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=(int)e.Item.ItemIndex;
			ShowData(strSql);
		}
		#endregion

		#region//*******删除选中试卷*******
		private void DataGridPaper_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int intPaperID=Convert.ToInt32(e.Item.Cells[0].Text);
			if (ObjFun.GetValues("select UserScoreID from UserScore where PaperID="+intPaperID+"","UserScoreID")=="")
			{
				string strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection SqlConn=new SqlConnection(strConn);
				SqlCommand SqlCmd=null;
				SqlConn.Open();
				//数据表PaperTest
				SqlCmd=new SqlCommand("delete from PaperTest where PaperID="+intPaperID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				//数据表PaperPolicy
				SqlCmd=new SqlCommand("delete from PaperPolicy where PaperID="+intPaperID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				//数据表PaperTestType
				SqlCmd=new SqlCommand("delete from PaperTestType where PaperID="+intPaperID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				//数据表PaperUser
				SqlCmd=new SqlCommand("delete from PaperUser where PaperID="+intPaperID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				//数据表PaperInfo
				SqlCmd=new SqlCommand("delete from PaperInfo where PaperID="+intPaperID+"",SqlConn);
				SqlCmd.ExecuteNonQuery();
				SqlConn.Close();
				SqlConn.Dispose();

				//自动翻页
				if (DataGridPaper.Items.Count==1&&DataGridPaper.CurrentPageIndex>0)
				{
					DataGridPaper.CurrentPageIndex--;
				}        

				ShowData(strSql);
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('此试卷正在使用，请先删除相应答卷后再进行此操作！')</script>");
				return;
			}
		}
		#endregion
		
		#region//*******取消编辑信息*******
		private void DataGridPaper_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGridPaper.EditItemIndex=-1;
			ShowData(strSql);
		}
		#endregion

		#region//*******条件查询信息*******
		protected void ButQuery_Click(object sender, System.EventArgs e)
		{
			bWhere=true;
			strSql="select a.PaperID,a.PaperName,a.PaperType,case a.ProduceWay when 1 then '题序固定' when 2 then '题序随机' when 3 then '试题随机' end as ProduceWay,case a.ShowModal when 1 then '整卷模式' when 2 then '逐题模式' end as ShowModal,a.StartTime,a.EndTime,a.TestCount,a.PaperMark,a.CreateWay,b.LoginID as CreateLoginID,convert(varchar(10),a.CreateDate,120) as CreateDate from PaperInfo a LEFT OUTER JOIN UserInfo b ON a.CreateUserID=b.UserID where a.PaperType=2";
			//显示以名称为条件的数据
			if (txtPaperName.Text.Trim()!="")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
				}
				else
				{
					strSql=strSql+" where a.PaperName like '%"+ObjFun.CheckString(txtPaperName.Text.Trim())+"%'";
					bWhere=true;
				}
			}
			//显示以创建方式为条件的数据
			if (DDLCreateWay.SelectedItem.Value!="0")
			{
				if (bWhere)
				{
					strSql=strSql+" and a.CreateWay='"+DDLCreateWay.SelectedItem.Value+"'";
				}
				else
				{
					strSql=strSql+" where a.CreateWay='"+DDLCreateWay.SelectedItem.Value+"'";
					bWhere=true;
				}
			}
			strSql=strSql+" order by a.PaperID desc";

			DataGridPaper.CurrentPageIndex=0;
			ShowData(strSql);
			LabCondition.Text=strSql;
		}
		#endregion

		#region//*******转到第一页*******
		protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=0;
			ShowData(strSql);
		}
		#endregion

		#region//*******转到上一页*******
		protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex>0)
			{
				DataGridPaper.CurrentPageIndex-=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到下一页*******
		protected void LinkButNextPage_Click(object sender, System.EventArgs e)
		{
			if (DataGridPaper.CurrentPageIndex<(DataGridPaper.PageCount-1))
			{
				DataGridPaper.CurrentPageIndex+=1;
				ShowData(strSql);
			}
		}
		#endregion

		#region//*******转到最后页*******
		protected void LinkButLastPage_Click(object sender, System.EventArgs e)
		{
			DataGridPaper.CurrentPageIndex=(DataGridPaper.PageCount-1);
			ShowData(strSql);
		}
		#endregion

		#region//*******数据排序*******
		private void DataGridPaper_SortCommand (object source , System.Web.UI.WebControls.DataGridSortCommandEventArgs e )
		{
			//获得列名
			//string sColName = e.SortExpression ;

			string ImgDown = "<img border=0 src=" + Request.ApplicationPath + "/Images/uparrow.gif>";
			string ImgUp = "<img border=0 src=" + Request.ApplicationPath + "/Images/downarrow.gif>";
			string SortExpression = e.SortExpression.ToString();
			string SortDirection = "ASC";
			int colindex = -1;
			//清空之前的图标
			for (int i = 0; i < DataGridPaper.Columns.Count; i++)
			{
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgDown, "");
				DataGridPaper.Columns[i].HeaderText = (DataGridPaper.Columns[i].HeaderText).ToString().Replace(ImgUp, "");
			}
			//找到所点击的HeaderText的索引号
			for (int i = 0; i < DataGridPaper.Columns.Count; i++)
			{
				if (DataGridPaper.Columns[i].SortExpression == e.SortExpression)
				{
					colindex = i;
					break;
				}
			}
			if (SortExpression == DataGridPaper.Attributes["SortExpression"])
			{
 
				SortDirection = (DataGridPaper.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC");
 
			}
			DataGridPaper.Attributes["SortExpression"] = SortExpression;
			DataGridPaper.Attributes["SortDirection"] = SortDirection;
			if (DataGridPaper.Attributes["SortDirection"] == "ASC")
			{
				DataGridPaper.Columns[colindex].HeaderText = DataGridPaper.Columns[colindex].HeaderText + ImgDown;
			}
			else
			{
				DataGridPaper.Columns[colindex].HeaderText = DataGridPaper.Columns[colindex].HeaderText + ImgUp;
			}
			ShowData(strSql);
		}
		#endregion
	}
}
