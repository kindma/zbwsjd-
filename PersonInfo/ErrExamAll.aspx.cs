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
public partial class PersonInfo_ErrExamAll : System.Web.UI.Page
{
    protected string strPaperName = "";
    protected string strTestCount = "";
    protected string strPaperMark = "";
    protected string strPaperContent = "";

    protected int intRemTime = 0;
    protected int intRemMinute = 0;
    protected int intRemSecond = 0;
    protected int intAutoSaveTime = 0;

    protected string strLoginID = "";
    protected string strUserName = "";
    protected int intExamTime = 0;

    protected int intPaperID = 0;
    protected int intPassMark = 0;
    protected int intFillAutoGrade = 0;
    protected int intSeeResult = 0;
    protected int intAutoJudge = 0;
    protected int intUserID = 0;
    protected int intUserScoreID = 0;
    protected int intTestNum = 0;
    protected string strtable = "";

    string myUserID = "";
    string myLoginID = "";
    string myUserName = "";
    string putfor = "";
    string chkFlag = "";

    PublicFunction ObjFun = new PublicFunction();
    int k = 0, intProduceWay = 0, intOptionNum = 0;
    double dblTotalMark = 0;
    string strTestContent = "";
    string[] strArrOptionContent, strArrTypeUserAnswer;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            myUserID = Session["UserID"].ToString();
            myLoginID = Session["LoginID"].ToString();
            myUserName = Session["UserName"].ToString();
            putfor = Request["putfor"];
            chkFlag = Convert.ToString( Request["chkFlag"]);
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

        intUserScoreID = Convert.ToInt32(Request["UserScoreID"]);

        intUserID = Convert.ToInt32(myUserID);

        strPaperName = "错题卡练习";

        if (!IsPostBack)
        {
            //if (intPaperID != 0)
            //{
            string strConn = ConfigurationSettings.AppSettings["strConn"];
            SqlConnection SqlConn = new SqlConnection(strConn);
            SqlCommand ObjCmd = null;
            SqlDataAdapter SqlCmd = null, SqlCmdTestType = null, SqlCmdTest = null;
            DataSet SqlDS = null, SqlDSTestType = null, SqlDSTest = null;
            //生成试卷
            SqlConn.Open();
            //SqlCmd = new SqlDataAdapter("select a.*,b.LoginID,b.UserName from UserScore a,UserInfo b where a.UserScoreID=" + intUserScoreID + " and b.UserID=a.UserID order by a.UserScoreID asc", SqlConn);
            //SqlDS = new DataSet();
            //SqlCmd.Fill(SqlDS, "UserScore");
            //intPaperID = Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["PaperID"]);
            //intRemTime = Convert.ToInt32(SqlDS.Tables["UserScore"].Rows[0]["RemTime"]);

            //if (intUserScoreID == 0)
            //{
            //    ObjFun.Alert("您已经参加了此试卷的考试！");
            //}
            //if (intUserScoreID == -1)
            //{
            //    ObjFun.Alert("生成答卷意外错误，请稍候再试！");
            //}
            //strLoginID = Convert.ToString(myLoginID);
            //strUserName = Convert.ToString(myUserName);

            //SqlCmd = new SqlDataAdapter("select PaperName,ProduceWay,TestCount,PaperMark,ExamTime,AutoSave,PassMark,FillAutoGrade,SeeResult,AutoJudge from PaperInfo where PaperID=" + intPaperID + " order by PaperID asc", SqlConn);
            //SqlDS = new DataSet();
            //SqlCmd.Fill(SqlDS, "PaperInfo");
            //strPaperName = SqlDS.Tables["PaperInfo"].Rows[0]["PaperName"].ToString() + "-错题重做";
            //intProduceWay = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["ProduceWay"]);
            //strTestCount = SqlDS.Tables["PaperInfo"].Rows[0]["TestCount"].ToString();
            //strPaperMark = SqlDS.Tables["PaperInfo"].Rows[0]["PaperMark"].ToString();
            //intExamTime = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["ExamTime"]);
            //intAutoSaveTime = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["AutoSave"]) * 60;
            //intPassMark = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["PassMark"]);
            //intFillAutoGrade = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["FillAutoGrade"]);
            //intSeeResult = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["SeeResult"]);
            //intAutoJudge = Convert.ToInt32(SqlDS.Tables["PaperInfo"].Rows[0]["AutoJudge"]);

            //intRemMinute = intRemTime / 60;
            //intRemSecond = intRemTime % 60;

            intTestNum = 0;


            string[] arrPutFor = putfor.Split(',');

            

            strPaperContent = strPaperContent + "<input type='hidden' id='putfor' name='putfor' value='" + putfor + "'>";
            strPaperContent = strPaperContent + "<input type='hidden' id='chkFlag' name='chkFlag' value='" + chkFlag + "'>";
            for (int i = 0; i < arrPutFor.Length; i++)
            {
                
                SqlCmdTestType = new SqlDataAdapter("select top 1 a.TestTypeID,b.BaseTestType,a.TestTypeTitle from PaperTestType a,TestTypeInfo b where a.TestTypeID=b.TestTypeID and a.TestTypeID = " + arrPutFor[i].Split(':')[0], SqlConn);
                SqlDSTestType = new DataSet();
                SqlCmdTestType.Fill(SqlDSTestType, "PaperTestType");
                
                strPaperContent = strPaperContent + "<tr>";
                strPaperContent = strPaperContent + "<td colspan='2' bgcolor='#CEE7FF' height='20' style='CURSOR:hand' onclick='jscomFlexObject(trTestTypeContent" + Convert.ToString(i + 1) + ")' title='点击伸缩该大题'>" + ObjFun.convertint(Convert.ToString(i + 1)) + "." + SqlDSTestType.Tables["PaperTestType"].Rows[0]["TestTypeTitle"].ToString() + "（共" + arrPutFor[i].Split(':')[1] + "题）</td>";
                strPaperContent = strPaperContent + "</tr>";
                strPaperContent = strPaperContent + "<tr>";
                strPaperContent = strPaperContent + "<td width='10' nowrap></td>";
                strPaperContent = strPaperContent + "<td width='100%'>";
                strPaperContent = strPaperContent + "<table style='FONT-SIZE:16' cellSpacing='0' cellPadding='1' width='100%' align='center' border='0' id='trTestTypeContent" + Convert.ToString(i + 1) + "'>";

                string newStr = "select top " + arrPutFor[i].Split(':')[1] + " a.RubricID,b.TestTypeID,t.BaseTestType, b.TestContent,a.TestFileName,b.OptionContent, b.StandardAnswer, b.OptionNum" +
                                " from UserAnswer a,RubricInfo b , TestTypeInfo t "+
                                " where a.RubricID=b.RubricID and b.testtypeid = t.TestTypeID "+
                                " and a.UserScoreID in ( select u.UserScoreID"+
                                " from dbo.PaperInfo p, dbo.UserScore u, dbo.UserAnswer a, dbo.RubricInfo r"+
                                " where p.PaperID = u.PaperID and u.UserScoreID = a.UserScoreID and a.RubricID = r.RubricID"+
                                " and p.PaperType = 1 and a.UserScore = 0 and a.ifdel = 0 and userid = " + myUserID + " and b.TestTypeID = " + arrPutFor[i].Split(':')[0] + ")" +
                                " group by a.RubricID,b.TestTypeID,t.BaseTestType, b.TestContent,a.TestFileName,b.OptionContent, b.StandardAnswer,b.OptionNum";



                SqlCmdTest = new SqlDataAdapter(newStr, SqlConn);
                SqlDSTest = new DataSet();
                SqlCmdTest.Fill(SqlDSTest, "UserAnswer");
                for (int j = 0; j < SqlDSTest.Tables["UserAnswer"].Rows.Count; j++)
                {
                    intTestNum = intTestNum + 1;
                    strTestContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();

                        strPaperContent = strPaperContent + "<tr>";
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "填空类")
                        {
                            intOptionNum = 0;
                            Regex Reg = new Regex("___", RegexOptions.IgnoreCase);
                            while (strTestContent.IndexOf("___") >= 0)
                            {
                                strTestContent = Reg.Replace(strTestContent, "", 1);
                                intOptionNum = intOptionNum + 1;
                            }
                            strTestContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["TestContent"].ToString();
                            //strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString().Split(',');

                            strArrOptionContent = new string[4];

                            if (Request["Answer" + intTestNum.ToString()] != null)
                            {
                                strArrOptionContent = Request["Answer" + intTestNum.ToString()].ToString().Split(',');
                            }

                            for (k = 1; k <= intOptionNum; k++)
                            {
                                if (k <= strArrOptionContent.Length)
                                {
                                    strTestContent = Reg.Replace(strTestContent, "<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' size='16' class=filltext value='" + strArrOptionContent[k - 1] + "' onBlur='textcheck()' title='试题答案中不能包含半角逗号“,”'>", 1);
                                }
                                else
                                {
                                    strTestContent = Reg.Replace(strTestContent, "<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' size='16' class=filltext value='' onBlur='textcheck()' title='试题答案中不能包含半角逗号“,”'>", 1);
                                }
                            }
                        }
                        strPaperContent = strPaperContent + "<td colspan='2' width='100%'><input type='hidden' id='TestTypeTitle" + intTestNum.ToString() + "' name='TestTypeTitle" + intTestNum.ToString() + "' value='" + SqlDSTestType.Tables["PaperTestType"].Rows[0]["TestTypeTitle"].ToString() + "'><input type='hidden' id='RubricID" + intTestNum.ToString() + "' name='RubricID" + intTestNum.ToString() + "' value='" + SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString() + "'><input type='hidden' id='BaseTestType" + intTestNum.ToString() + "' name='BaseTestType" + intTestNum.ToString() + "' value='" + SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() + "'><a id='l" + intTestNum.ToString() + "' style='color:black'>" + intTestNum.ToString() + "</a>." + strTestContent + "<font color='red'></font></td>";
                        strPaperContent = strPaperContent + "</tr>";
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "单选类")
                        {
                            intOptionNum = Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
                            strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
                            for (k = 1; k <= intOptionNum; k++)
                            {
                                strPaperContent = strPaperContent + "<tr>";

                                if (Request["Answer" + intTestNum.ToString()] == null)
                                {
                                    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "'>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                }
                                else
                                {
                                    if (Request["Answer" + intTestNum.ToString()].ToString().IndexOf(Convert.ToChar(64 + k)) >= 0)
                                    {
                                        strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' checked>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                    }
                                    else
                                    {
                                        strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "'>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                    }
                                }

                                strPaperContent = strPaperContent + "</tr>";
                            }
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "多选类")
                        {
                            intOptionNum = Convert.ToInt32(SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionNum"]);
                            strArrOptionContent = SqlDSTest.Tables["UserAnswer"].Rows[j]["OptionContent"].ToString().Split('|');
                            for (k = 1; k <= intOptionNum; k++)
                            {
                                strPaperContent = strPaperContent + "<tr>";

                                if (Request["Answer" + intTestNum.ToString()] == null)
                                {
                                    strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "'>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";

                                }
                                else
                                {
                                    if (Request["Answer" + intTestNum.ToString()].ToString().ToString().IndexOf(Convert.ToChar(64 + k)) >= 0)
                                    {
                                        strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "' checked>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                    }
                                    else
                                    {
                                        strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><input type='checkbox' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='" + Convert.ToChar(64 + k) + "'>" + Convert.ToChar(64 + k) + "." + strArrOptionContent[k - 1] + "</td>";
                                    }
                                }
                                strPaperContent = strPaperContent + "</tr>";
                            }
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "判断类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10' nowrap>";


                            if (Request["Answer" + intTestNum.ToString()] == null)
                            {
                                strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='正确'>正确</td>";
                            }
                            else
                            {
                                if (Request["Answer" + intTestNum.ToString()].ToString().ToString().IndexOf("正确") >= 0)
                                {
                                    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='正确' checked>正确</td>";
                                }
                                else
                                {
                                    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='正确'>正确</td>";
                                }
                            }
                            strPaperContent = strPaperContent + "</tr>";
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10' nowrap>";

                            if (Request["Answer" + intTestNum.ToString()] == null)
                            {
                                strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='错误'>错误</td>";
                            }
                            else
                            {

                                if (Request["Answer" + intTestNum.ToString()].ToString().ToString().IndexOf("错误") >= 0)
                                {
                                    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='错误' checked>错误</td>";
                                }
                                else
                                {
                                    strPaperContent = strPaperContent + "<td width='100%'><input type='radio' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' value='错误'>错误</td>";
                                }
                            }
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "填空类")
                        {
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "问答类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><TEXTAREA rows=6 cols=40 id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' style='width:100%' class=Text>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString() + "</TEXTAREA></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "作文类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><TEXTAREA rows=10 cols=40 id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' style='width:100%' class=Text>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["UserAnswer"].ToString() + "</TEXTAREA></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "打字类")
                        {

                            if (Request["Answer" + intTestNum.ToString()] == null)
                            {
                                strArrTypeUserAnswer = "0,0".Split(',');
                            }
                            else
                            {
                                if (Request["Answer" + intTestNum.ToString()].ToString().ToString().IndexOf(",") >= 0)
                                {
                                    strArrTypeUserAnswer = Request["Answer" + intTestNum.ToString()].ToString().ToString().Split(',');
                                }
                                else
                                {
                                    strArrTypeUserAnswer = "0,0".Split(',');
                                }
                            }
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('TypeWord.aspx?TestNum=" + intTestNum + "&UserScoreID=" + intUserScoreID + "&RubricID=" + SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString() + "',window,'dialogHeight:430px;dialogWidth:570px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='点击开始打字'>开始打字</a>&nbsp;打字速度：<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' class=Text size='3' ReadOnly='true' value='" + strArrTypeUserAnswer[0] + "'>个字/分钟&nbsp;正确率：<input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' class=Text size='3' ReadOnly='true' value='" + strArrTypeUserAnswer[1] + "'>%</td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }
                        if (SqlDSTestType.Tables["PaperTestType"].Rows[0]["BaseTestType"].ToString() == "操作类")
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><a href='DownLoadFile.aspx?UserScoreID=" + intUserScoreID + "&RubricID=" + SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString() + "' title='点击下载'>下载文件：</a><b>" + SqlDSTest.Tables["UserAnswer"].Rows[j]["TestFileName"].ToString() + "</b></td>";
                            strPaperContent = strPaperContent + "</tr>";
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td width='10px' nowrap><td width='100%'><a href='#'onclick=window.showModelessDialog('UpLoadFile.aspx?TestNum=" + intTestNum + "&UserScoreID=" + intUserScoreID + "&RubricID=" + SqlDSTest.Tables["UserAnswer"].Rows[j]["RubricID"].ToString() + "',window,'dialogHeight:150px;dialogWidth:330px;edge:Raised;center:Yes;help:Yes;resizable:No;scroll:No;status:No;'); title='点击上传'>上传文件：</a><input type='text' id='Answer" + intTestNum.ToString() + "' name='Answer" + intTestNum.ToString() + "' class=Text size='16' ReadOnly='true' value=''></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }

                        if (chkFlag != null)
                        {
                            strPaperContent = strPaperContent + "<tr>";
                            strPaperContent = strPaperContent + "<td colspan='2'><font color='blue'>标准答案：" + SqlDSTest.Tables["UserAnswer"].Rows[j]["StandardAnswer"].ToString() + "</font></td>";
                            strPaperContent = strPaperContent + "</tr>";
                        }

                        strPaperContent = strPaperContent + "<tr>";
                        strPaperContent = strPaperContent + "<td colspan='2' height='10'></td>";
                        strPaperContent = strPaperContent + "</tr>";
                    
                }
                strPaperContent = strPaperContent + "</table>";
                strPaperContent = strPaperContent + "</td>";
                strPaperContent = strPaperContent + "</tr>";
            }

            SqlConn.Close();
            SqlConn.Dispose();

            //检查试卷表格
            strtable = strtable + "<table height='100%' width='100%'>";
            strtable = strtable + "<tr>";
            strtable = strtable + "<td>";
            strtable = strtable + "<div style='FONT-SIZE: 9pt; OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%'>";
            strtable = strtable + "<table style='FONT-SIZE: 10pt' cellSpacing='0' borderColorDark='#cccccc' cellPadding='0' border='1'>";
            int n = 1;
            for (int i = 1; i <= intTestNum; i++)
            {
                if (n == 1)
                {
                    strtable = strtable + "<tr>";
                    strtable = strtable + "<td align='right' width='39' height='18'><span id='icon" + i.ToString() + "1'>" + i.ToString() + ":<img src='../images/nofinished.gif' border='0'></span><span id='icon" + i.ToString() + "2' style='DISPLAY: none'>" + i.ToString() + ":<img src='../images/finished.gif' border='0'></span></td>";
                    n = n + 1;
                }
                else
                {
                    if (n == 10)
                    {
                        strtable = strtable + "<td align='right' width='39' height='18'><span id='icon" + i.ToString() + "1'>" + i.ToString() + ":<img src='../images/nofinished.gif' border='0'></span><span id='icon" + i.ToString() + "2' style='DISPLAY: none'>" + i.ToString() + ":<img src='../images/finished.gif' border='0'></span></td>";
                        strtable = strtable + "</tr>";
                        n = 1;
                    }
                    else
                    {
                        strtable = strtable + "<td align='right' width='39' height='18'><span id='icon" + i.ToString() + "1'>" + i.ToString() + ":<img src='../images/nofinished.gif' border='0'></span><span id='icon" + i.ToString() + "2' style='DISPLAY: none'>" + i.ToString() + ":<img src='../images/finished.gif' border='0'></span></td>";
                        n = n + 1;
                    }
                }
            }
            strtable = strtable + "</table>";
            strtable = strtable + "</div>";
            strtable = strtable + "</td>";
            strtable = strtable + "</tr>";
            strtable = strtable + "</table>";
            //}
        } 
      
    }
}