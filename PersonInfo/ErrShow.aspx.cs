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
using System.Text;
using System.Text.RegularExpressions;
using EasyExam;

public partial class PersonInfo_ErrShow : System.Web.UI.Page
{
    protected string strPaperName = "";
    protected string strTestCount = "";
    protected string strPaperMark = "";
    protected string strPaperContent = "";

    protected string strLoginID = "";
    protected string strUserName = "";
    protected string strExamTime = "";
    protected string strPassMark = "";
    protected string strTotalMark = "";

    protected int intPaperID = 0;
    protected int intUserID = 0;
    protected int intUserScoreID = 0;
    protected int intTestNum = 0;

    string myUserID = "";
    string myLoginID = "";
    PublicFunction ObjFun = new PublicFunction();
    int k = 0, intProduceWay = 0, intOptionNum = 0, intSeeResult = 0;
    double dblTotalMark = 0;
    string strTestContent = "";
    string strManageUser = "";
    string[] strArrOptionContent, strArrTypeStandardAnswer, strArrTypeUserAnswer;
    int RubricID = 0;
    string standanswer = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            myUserID = Session["UserID"].ToString();
            myLoginID = Session["LoginID"].ToString();
            RubricID = Convert.ToInt32(Request["RubricID"]);
            standanswer = Request["standanswer"];
        }
        catch
        {
        }
        if (myLoginID == "")
        {
            Response.Redirect("../Login.aspx");
        }
        //清除缓存
        Response.Expires = 0;
        Response.Buffer = true;
        Response.Clear();

        strPaperContent = "";
        //intUserScoreID = Convert.ToInt32(Request["UserScoreID"]);
        //strManageUser = Convert.ToString(Request["ManageUser"]);
        ////if (!IsPostBack)
        ////{
        ////权限判断
           
            string strConn = ConfigurationSettings.AppSettings["strConn"];
            SqlConnection SqlConn = new SqlConnection(strConn);
            SqlCommand ObjCmd = null;
            SqlDataAdapter SqlCmd = null, SqlCmdTestType = null, SqlCmdTest = null;
            DataSet SqlDS = null, SqlDSTestType = null, SqlDSTest = null;

            intTestNum = 0;
            //SqlCmdTestType = new SqlDataAdapter("select a.TestTypeID,b.BaseTestType,a.TestTypeTitle,a.TestTypeMark,a.TestAmount,a.TestTotalMark from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.PaperID=" + intPaperID + " order by a.PaperTestTypeID asc", SqlConn);
            //SqlDSTestType = new DataSet();
            //SqlCmdTestType.Fill(SqlDSTestType, "PaperTestType");
            //for (int i = 0; i < SqlDSTestType.Tables["PaperTestType"].Rows.Count; i++)
            //{
               
                strPaperContent = strPaperContent + "<tr>";
                strPaperContent = strPaperContent + "<td width='10' nowrap></td>";
                strPaperContent = strPaperContent + "<td width='100%'>";
                strPaperContent = strPaperContent + "<table   style='FONT-SIZE:16' cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent1'>";


                string sqlStr = "select top 1 a.RubricID,b.TestTypeID,t.BaseTestType, b.TestDiff,b.OptionNum,"+
                                " b.TestContent,a.TestFile,a.TestFileName,b.OptionContent,b.StandardAnswer,a.UserAnswer, b.TestParse" +
                                " from UserAnswer a,RubricInfo b , TestTypeInfo t "+
                                " where a.RubricID=b.RubricID and b.testtypeid = t.TestTypeID and b.RubricID = " + RubricID +
                                " and a.UserScoreID in ( select u.UserScoreID"+
                                " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a, dbo.RubricInfo r"+
                                " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID and a.RubricID = r.RubricID"+
                                " and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID + " and r.RubricID = " + RubricID + " )";

                SqlCmdTest = new SqlDataAdapter(sqlStr, SqlConn);
                SqlDSTest = new DataSet();
                SqlCmdTest.Fill(SqlDSTest, "UserAnswer");
                for (int j = 0; j < SqlDSTest.Tables["UserAnswer"].Rows.Count; j++)
                {
                    intTestNum = intTestNum + 1;
                    strTestContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
                    //if ((Convert.ToDouble(SqlDSTest.Tables["UserAnswer"].Rows[j]["UserScore"].ToString()) == 0))
                    //{
                    strPaperContent = strPaperContent + "<tr>" + strTestContent + "</tr><tr>";
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "填空类")
                        {
                            intOptionNum = 0;
                            Regex Reg = new Regex("___", RegexOptions.IgnoreCase);
                            while (strTestContent.IndexOf("___") >= 0)
                            {
                                strTestContent = Reg.Replace(strTestContent, "", 1);
                                intOptionNum = intOptionNum + 1;
                            }
                            strTestContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
                            strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');
                            for (k = 1; k <= intOptionNum; k++)
                            {
                                //if (k <= strArrOptionContent.Length)
                                //{
                                //    strTestContent = Reg.Replace(strTestContent, "<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' size='16' class=filltext value='" + strArrOptionContent[k - 1] + "' onBlur='textcheck()' readonly title='试题答案中不能包含半角逗号“,”'>", 1);
                                //}
                                //else
                                //{
                                    strTestContent = Reg.Replace(strTestContent, "<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' size='16' class=filltext value='' onBlur='textcheck()' readonly title='试题答案中不能包含半角逗号“,”'>", 1);
                                //}
                            }
                        }
                        strPaperContent = strPaperContent + "<td colspan='2' width='100%'></td>";
                        strPaperContent = strPaperContent + "</tr>";
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "单选类")
                        {
                            intOptionNum = Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
                            strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
                            for (k = 1; k <= intOptionNum; k++)
                            {
                                strPaperContent = strPaperContent + "<tr>";
                                //if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64 + k)) >= 0)
                                //{
                                //    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' checked disabled>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                //}
                                //else
                                //{
                                    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' disabled>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                //}
                                strPaperContent = strPaperContent + "</tr>";
                            }
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "多选类")
                        {
                            intOptionNum = Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
                            strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
                            for (k = 1; k <= intOptionNum; k++)
                            {
                                strPaperContent = strPaperContent + "<tr>";
                                //if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(Convert.ToChar(64 + k)) >= 0)
                                //{
                                //    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' checked disabled>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                //}
                                //else
                                //{
                                    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' disabled>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                //}
                                strPaperContent = strPaperContent + "</tr>";
                            }
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "判断类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10' nowrap>";
                            //if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("正确") >= 0)
                            //{
                            //    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='正确' checked disabled>正确</td>";
                            //}
                            //else
                            //{
                                strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='正确' disabled>正确</td>";
                            //}
                            strPaperContent = strPaperContent + "</tr>";
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10' nowrap>";
                            //if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf("错误") >= 0)
                            //{
                            //    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='错误' checked disabled>错误</td>";
                            //}
                            //else
                            //{
                                strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='错误' disabled>错误</td>";
                            //}
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "填空类")
                        {
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "问答类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' style='width:100%' class=Text readonly>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString() + "</TEXTAREA></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "作文类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' style='width:100%' class=Text readonly>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString() + "</TEXTAREA></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "打字类")
                        {
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "操作类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><a href='../PersonInfo/DownLoadFile.aspx?UserScoreID=" + intUserScoreID + "&RubricID=" + SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString() + "' title='点击下载'>下载文件：</a><b>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["TestFileName"].ToString() + "</b></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTest.Tables["UserAnswer"].Rows[j]["BaseTestType"].ToString() == "打字类")
                        {
                            strArrTypeStandardAnswer = SqlDSTest.Tables["UserAnswer"].Rows[j]["StandardAnswer"].ToString().Split(',');
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>标准答案：标准速度：" + strArrTypeStandardAnswer[1] + "个字/分钟&nbsp;正确率：100%</font></td>";
                            strPaperContent = strPaperContent + "</tr>";

                            if (SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().IndexOf(",") >= 0)
                            {
                                strArrTypeUserAnswer = SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');
                            }
                            else
                            {
                                strArrTypeUserAnswer = "0,0".Split(',');
                            }
                            //strPaperContent = strPaperContent + "<tr>";
                            //strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>考生答案：打字速度：" + strArrTypeUserAnswer[0] + "个字/分钟&nbsp;正确率：" + strArrTypeUserAnswer[1] + "%</font></td>";
                            //strPaperContent = strPaperContent + "</tr>";
                        }
                        else
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>标准答案：" + SqlDSTest.Tables["UserAnswer"].Rows[j]["StandardAnswer"].ToString() + "</font></td>";
                            strPaperContent = strPaperContent + "</tr>";

                            //strPaperContent = strPaperContent + "<tr>";
                            //strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>考生答案：" + SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString() + "</font></td>";
                            //strPaperContent = strPaperContent + "</tr>";
                        }

                    
                        strPaperContent = strPaperContent + "<tr>";
                        strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>试题解析：" + SqlDSTest.Tables["UserAnswer"].Rows[j]["TestParse"].ToString() + "</font></td>";
                        strPaperContent = strPaperContent + "</tr>";

                        strPaperContent = strPaperContent + "<tr>";
                        strPaperContent = strPaperContent + "<td colspan='2' height='10'></td>";
                        strPaperContent = strPaperContent + "</tr>";
                    //}
                }
                strPaperContent = strPaperContent + "</table>";
                strPaperContent = strPaperContent + "</td>";
                strPaperContent = strPaperContent + "</tr>";
            //}

            SqlConn.Close();
            SqlConn.Dispose();
        
        //}
    }
}