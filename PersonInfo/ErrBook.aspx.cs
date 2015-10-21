using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyExam;

public partial class PersonInfo_ErrBook : System.Web.UI.Page
{
    protected System.Web.UI.WebControls.ImageButton ImgButSubject;
    protected System.Web.UI.WebControls.TextBox txtSubjectName;
    protected int RowNum = 0;

    string strSql = "";
    string myUserID = "";
    string myLoginID = "";
    PublicFunction ObjFun = new PublicFunction();
    int intUserID = 0;
    int rubid = 0;
    int typeid = 0;
    string selectCondi = "";
    public string testTypeContext = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            myUserID = Session["UserID"].ToString();
            myLoginID = Session["LoginID"].ToString();
            intUserID = Convert.ToInt32(myUserID);
            rubid = Convert.ToInt32(Request["RubricID"]);

            typeid =  Convert.ToInt32(Request["DropDownList"]);

        }
        catch
        {
        }
        if (myLoginID == "")
        {
            Response.Redirect("../Login.aspx");
        }
        //strSql = "select p.PaperID ,p.PaperName"+
        // " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a"+
        // " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID"+
        // " and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID +
        // " group by  p.PaperID, p.PaperName order by p.PaperID desc";

        if (typeid == 0)
        {
            selectCondi = "";
        }
        else
        {
            selectCondi = "and r.TestTypeID=" + typeid.ToString();
        }

        strSql = "select r.RubricID, r.TestContent , r.StandardAnswer, COUNT(r.RubricID) as cnt"+
		 " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a, dbo.RubricInfo r"+
		 " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID and a.RubricID = r.RubricID"+
         " and r.TestTypeID <> 32 and  r.TestTypeID <> 31 and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID +selectCondi+
         " group by  r.RubricID, r.TestContent , r.StandardAnswer order by r.RubricID desc";

        
        //if (!IsPostBack || rubid !=0 )
        //{
            if (DataGridPaper.Attributes["SortExpression"] == null)
            {
                DataGridPaper.Attributes["SortExpression"] = "RubricID";
                DataGridPaper.Attributes["SortDirection"] = "DESC";
            }
            ShowData(strSql);
        //}
    }

    private void ShowData(string strSql)
    {
        string strConn = ConfigurationSettings.AppSettings["strConn"];
        SqlConnection SqlConn = new SqlConnection(strConn);

        if (rubid != 0)
        {
            string upifuser = " update UserAnswer set ifDel = 1 where RubricID = " + rubid + " and " +
             " UserScoreID in ( select u.UserScoreID " +
             " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a, dbo.RubricInfo r" +
             " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID and a.RubricID = r.RubricID" +
             " and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID + " and r.RubricID = " + rubid + " )";

            SqlConn.Open();

            SqlCommand sqlCmd = new SqlCommand(upifuser, SqlConn);
            
            sqlCmd.ExecuteNonQuery();
        }

        string selTypeID = "select t.TestTypeID, t.TestTypeName, isnull(r.cnt,0) as cnt "+
                        " from TestTypeInfo t left join(select count(a.RubricID) cnt,a.TestTypeID,a.BaseTestType"+
                        " from(select a.RubricID,b.TestTypeID,t.BaseTestType"+
                        " from UserAnswer a,RubricInfo b , TestTypeInfo t "+
                        " where b.TestTypeID <> 32 and  b.TestTypeID <> 31 and a.RubricID=b.RubricID and b.testtypeid = t.TestTypeID "+
                        " and a.UserScoreID in ( select u.UserScoreID"+
                        " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a, dbo.RubricInfo r"+
                        " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID and a.RubricID = r.RubricID"+
                        " and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID + ")" +
                        " group by 	a.RubricID,b.TestTypeID,t.BaseTestType	 "+
                        " ) a group by a.TestTypeID,a.BaseTestType"+
                        " ) r on t.TestTypeID = r.TestTypeID";


        SqlDataAdapter typeIDCmd = new SqlDataAdapter(selTypeID, SqlConn);
        DataSet TypeIDDS = new DataSet();
        typeIDCmd.Fill(TypeIDDS, "typeIDInfo");

        testTypeContext = "<input type='hidden' id='hdCnt' name='hdCnt' value='" + TypeIDDS.Tables["typeIDInfo"].Rows.Count + "' />";

        for (int i = 0; i < TypeIDDS.Tables["typeIDInfo"].Rows.Count; i++)
        {
            if (testTypeContext == "")
            {
                testTypeContext = "<label >" + TypeIDDS.Tables["typeIDInfo"].Rows[i]["TestTypeName"].ToString() +
                    "</label><input type='text' id='curr" + i + "' name='curr" + i + "' style='width:20' value='0' >/<input type='text' id='cnt" + i + "' name='cnt" + i + "' readonly disable style='width:20' value='" +
                    TypeIDDS.Tables["typeIDInfo"].Rows[i]["cnt"].ToString() + "'><input type='hidden' id='hd" + i + "' name='hd" + i + "' value='" +
                    TypeIDDS.Tables["typeIDInfo"].Rows[i]["TestTypeID"].ToString() + "' />";
            }
            else
            {
                testTypeContext = testTypeContext + "<label >" + TypeIDDS.Tables["typeIDInfo"].Rows[i]["TestTypeName"].ToString() +
                                "</label><input type='text' id='curr" + i + "' name='curr" + i + "' style='width:20' value='0'>/<input type='text' id='cnt" + i + "' name='cnt" + i + "' readonly disable style='width:20' value='" +
                                TypeIDDS.Tables["typeIDInfo"].Rows[i]["cnt"].ToString() + "'><input type='hidden' id='hd" + i + "' name='hd" + i + "' value='" +
                                TypeIDDS.Tables["typeIDInfo"].Rows[i]["TestTypeID"].ToString() + "' />";

            }
        }
        
        string selType = "  select 0 as testtypeid, '' as testtypename union all select TestTypeID, TestTypeName from TestTypeInfo";

        SqlDataAdapter typeCmd = new SqlDataAdapter(selType, SqlConn);
        DataSet TypeDS = new DataSet();
        typeCmd.Fill(TypeDS,"typeInfo");

        DropDownList.DataSource = TypeDS;
        DropDownList.DataTextField = "TestTypeName";
        DropDownList.DataValueField = "TestTypeID";
        DropDownList.DataBind();

        DropDownList.SelectedValue = typeid.ToString();

        SqlDataAdapter SqlCmd = new SqlDataAdapter(strSql, SqlConn);                
        DataSet SqlDS = new DataSet();
        SqlCmd.Fill(SqlDS, "PaperInfo");
        RowNum = DataGridPaper.CurrentPageIndex * DataGridPaper.PageSize + 1;

        string SortExpression = DataGridPaper.Attributes["SortExpression"];
        string SortDirection = DataGridPaper.Attributes["SortDirection"];
        SqlDS.Tables["PaperInfo"].DefaultView.Sort = SortExpression + " " + SortDirection;

        DataGridPaper.DataSource = SqlDS.Tables["PaperInfo"].DefaultView;
        DataGridPaper.DataBind();
        for (int i = 0; i < DataGridPaper.Items.Count; i++)
        {
            LinkButton LBDelExam = (LinkButton)DataGridPaper.Items[i].FindControl("LinkButSeeTest");
            LBDelExam.Attributes.Add("onclick", "javascript:{if (confirm('您确定要查看吗？')==true) {NewExam=window.open('ErrShow.aspx?RubricID=" + DataGridPaper.Items[i].Cells[0].Text.Trim() + "&UserID=" + intUserID + "&standanswer=" + DataGridPaper.Items[i].Cells[1].Text.Trim() + "','StartDel','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewExam.moveTo(0,0);NewExam.resizeTo(screen.availWidth,screen.availHeight);}else{return false;}}");

            LBDelExam = (LinkButton)DataGridPaper.Items[i].FindControl("LinkButDelExam");
            //LBDelExam.Attributes.Add("onclick", "javascript:{if (confirm('您确定要删除吗？')==true) {NewExam=window.open('ErrBook.aspx?RubricID=" + DataGridPaper.Items[i].Cells[0].Text.Trim() + "&UserID=" + intUserID + "&Start=yes','StartDel','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=no,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');NewExam.moveTo(0,0);NewExam.resizeTo(screen.availWidth,screen.availHeight);}else{return false;}}");

            LBDelExam.Attributes.Add("onclick", "javascript:{if (confirm('您确定要删除吗？')==true) {var Form1 = document.getElementById('Form1'); Form1.action='ErrBook.aspx?RubricID=" + DataGridPaper.Items[i].Cells[0].Text.Trim() + "';Form1.submit();}else{return false;}}");

        }
        LabelRecord.Text = Convert.ToString(SqlDS.Tables["PaperInfo"].Rows.Count);
        LabelCountPage.Text = Convert.ToString(DataGridPaper.PageCount);
        LabelCurrentPage.Text = Convert.ToString(DataGridPaper.CurrentPageIndex + 1);
        SqlConn.Dispose();
    }

 

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
        this.DataGridPaper.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridPaper_ItemDataBound);

    }
    #endregion

    #region//*******表格翻页事件*******
    private void DataGridPaper_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        DataGridPaper.CurrentPageIndex = e.NewPageIndex;
        ShowData(strSql);
    }
    #endregion

    #region//*******行列颜色变换*******
    private void DataGridPaper_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.bgColor='#ebf5fa'");

            if (e.Item.ItemIndex % 2 == 0)
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

    #region//*******进行编辑信息*******
    private void DataGridPaper_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataGridPaper.EditItemIndex = (int)e.Item.ItemIndex;
        ShowData(strSql);
    }
    #endregion

    #region//*******取消编辑信息*******
    private void DataGridPaper_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataGridPaper.EditItemIndex = -1;
        ShowData(strSql);
    }
    #endregion

    #region//*******转到第一页*******
    protected void LinkButFirstPage_Click(object sender, System.EventArgs e)
    {
        DataGridPaper.CurrentPageIndex = 0;
        ShowData(strSql);
    }
    #endregion

    #region//*******转到上一页*******
    protected void LinkButPirorPage_Click(object sender, System.EventArgs e)
    {
        if (DataGridPaper.CurrentPageIndex > 0)
        {
            DataGridPaper.CurrentPageIndex -= 1;
            ShowData(strSql);
        }
    }
    #endregion

    #region//*******转到下一页*******
    protected void LinkButNextPage_Click(object sender, System.EventArgs e)
    {
        if (DataGridPaper.CurrentPageIndex < (DataGridPaper.PageCount - 1))
        {
            DataGridPaper.CurrentPageIndex += 1;
            ShowData(strSql);
        }
    }
    #endregion

    #region//*******转到最后页*******
    protected void LinkButLastPage_Click(object sender, System.EventArgs e)
    {
        DataGridPaper.CurrentPageIndex = (DataGridPaper.PageCount - 1);
        ShowData(strSql);
    }
    #endregion

    #region//*******数据排序*******
    private void DataGridPaper_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
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