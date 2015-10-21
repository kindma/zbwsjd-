<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.StartJobAll" CodeFile="StartJobAll.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
		<HEAD>
		<title>正在作业</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
		<link rel="stylesheet" type="text/css" href="/common.css">
		<style type="text/css">
.button {
	width: 90%;
	height: 40px;
	margin:5px;
}
</style>
		<script language="JavaScript" src="../JavaScript/MouseEvent.js"></script>
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
		<script language="JavaScript">
			var time_minute=<%=intRemMinute%>;
			var time_second=<%=intRemSecond%>;
			var time_passed=0;
			var timeid=null;
			var timerun=false;
			var autosavetime=<%=intAutoSaveTime%>;
			function stoptime()
			{
  				if (timerun) clearTimeout(timeid);
				timerun=false;
			}
			function starttime()
			{
  				stoptime();
  				showtime();
			}
			function showtime()
			{
   				time_passed+=1;
  				if (time_passed%autosavetime==0)
  				{
    				save();//保存答卷
    				time_passed=0;
  				};
   				timeid=setTimeout("showtime()",1000);
   				timerun=true;
			}
			
			function textcheck()//文本检查
			{
			    var inputstr=window.event.srcElement.value;
                if (inputstr.indexOf(",")>0)
                {
					var reg=new RegExp(",","g");
                    inputstr=inputstr.replace(reg,"，");
                    //alert("注：填空类试题答案中不能包含半角逗号“,”，将自动替换为全角逗号“，”。");
                    window.event.srcElement.value=inputstr;
                    window.event.keyCode=0;
                    window.event.srcElement.focus;
                    window.event.returnValue=0;
                }
			}
			function testcheck(TestNum)//试题检查
			{
				var testcheck=false;
				var BaseTestType=document.all("BaseTestType"+TestNum).value;
				var slen=document.all("Answer"+TestNum).length;
				if (slen==undefined) slen=1;
				var icount=0;
				switch(BaseTestType)
				{
					case "单选类":
						if (slen<=1)
						{
							if (document.all("Answer"+TestNum).checked)
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else
						{
							for(j=0;j<=slen-1;j++)
							{
								if (document.all("Answer"+TestNum).item(j).checked)
								{
									testcheck=true;
									return testcheck;
								}
							}
						};
						break;
					case "多选类":
						if (slen<=1)
						{
							if (document.all("Answer"+TestNum).checked)
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else
						{
							for(j=0;j<=slen-1;j++)
							{
								if (document.all("Answer"+TestNum).item(j).checked)
								{
									testcheck=true;
									return testcheck;
								}
							}
						};
						break;
					case "判断类":
						if (slen<=1)
						{
							if (document.all("Answer"+TestNum).checked)
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else
						{
							for(j=0;j<=slen-1;j++)
							{
								if (document.all("Answer"+TestNum).item(j).checked)
								{
									testcheck=true;
									return testcheck;
								}
							}
						};
						break;
					case "填空类":
					    if (slen<=1)
					    {
					        if (trim(document.all("Answer"+TestNum).value)!="")
					        {
								testcheck=true;
								return testcheck;
							}
						}	
						else
						{    
							for(j=0;j<=slen-1;j++)
							{
								if (trim(document.all("Answer"+TestNum).item(j).value)!="")	icount=icount+1;
							}
							if (icount==slen)
							{
								testcheck=true;
								return testcheck;
							}
						};
						break;
					case "问答类":
						if (slen<=1)
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else 
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						};
						break;
					case "作文类":
						if (slen<=1)
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else 
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						};
						break;
					case "操作类":
						if (slen<=1)
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						}	
						else 
						{
							if (trim(document.all("Answer"+TestNum).value)!="")
							{
								testcheck=true;
								return testcheck;
							}
						};
						break;	
					case "打字类":
					    if (slen<=1)
					    {
					        if (trim(document.all("Answer"+TestNum).value)!="")
					        {
								testcheck=true;
								return testcheck;
							}
						}	
						else
						{    
							for(j=0;j<=slen-1;j++)
							{
								if (trim(document.all("Answer"+TestNum).item(j).value)!="")	icount=icount+1;
							}
							if (icount==slen)
							{
								testcheck=true;
								return testcheck;
							}
						};
						break;
				}
				return testcheck;
			}
			function checkanswer()//检查答卷
			{
				total=document.all("irow").value;
				for(i=1;i<=total;i++)
				{
					if (testcheck(i)==false)
					{
						document.all("icon"+i+"1").style.display="block";
						document.all("icon"+i+"2").style.display="none";
					}
					else
					{
						document.all("icon"+i+"1").style.display="none";
						document.all("icon"+i+"2").style.display="block";
					}
				}
				submitexam.style.display="none"
				submitexam1.style.display="none"
				submitexam2.style.display="none"
				submitexam3.style.display="none"
				submitexam4.style.display="";
			}
			function trysubmit()//尝试提交
			{
				total=document.all("irow").value;
				for(i=1;i<=total;i++)
				{
					if (testcheck(i)==false)
					{
						submitexam.style.display="none"
						submitexam1.style.display="none"
						submitexam2.style.display="none"
						submitexam3.style.display="";
						submitexam4.style.display="none"
						document.all.labmessage.innerHTML="第"+i+"题您还没有做，是否确定交卷？";
						return; 
					}
				}
				submitexam.style.display="none"
				submitexam1.style.display="none"
				submitexam2.style.display="none"
				submitexam3.style.display="";
				submitexam4.style.display="none"
				document.all.labmessage.innerHTML="所有题目已做，是否确定交卷？";
			}
			function cancel()//取消提交
			{
				total=document.all("irow").value;
				for(i=1;i<=total;i++)
				{
					if (testcheck(i)==false)
					{
						window.event.returnvalue=0;
						location="#l"+i;
						submitexam3.style.display="none"
						return;
					}
				}
				submitexam3.style.display="none"
			}
			function cancel1()//关闭检查答卷
			{
				submitexam4.style.display="none"
			}
			function save()//保存答卷
			{
				submitexam.style.display="none"
				submitexam1.style.display="";
				submitexam2.style.display="none"
				submitexam3.style.display="none"
				submitexam4.style.display="none"
				form1.action="SaveJobAll.aspx?b";
				form1.target="iframe1";
				form1.submit();
				form1.target="";
				form1.action="";
			}
			function submit()//正式提交
			{
				timediv.style.display="none"
				submitexam1.style.display="none"
				submitexam2.style.display="";
				submitexam3.style.display="none"
				submitexam4.style.display="none"
				submitexam.style.display="none"
				form1.target="iframe1";
				form1.action="SubmJobAll.aspx";
				form1.submit();
			}
			function timeend()//时间结束
			{
				timediv.style.display="none"
				submitexam.style.display="";
				submitexam1.style.display="none"
				submitexam2.style.display="none"
				submitexam3.style.display="none"
				submitexam4.style.display="none"
				form1.target="";
				form1.action="SubmJobAll.aspx";
				form1.submit();
			}
		</script>
		</HEAD>
		<body  onload="starttime();"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="form1" action="SubmJobAll.aspx" method="post">
          <input type=hidden size=4 value="<%=intPaperID%>" name=PaperID>
          <input type=hidden size=4 value="<%=intPassMark%>" name=PassMark>
          <input type=hidden size=4 value="<%=intFillAutoGrade%>" name=FillAutoGrade>
          <input type=hidden size=4 value="<%=intSeeResult%>" name=SeeResult>
          <input type=hidden size=4 value="<%=intAutoJudge%>" name=AutoJudge>
          <input type=hidden size=4 value="<%=intUserScoreID%>" name=UserScoreID>
          <input type="hidden" size="4" value="0" name="timeminute">
          <input type="hidden" size="4" value="0" name="timesecond">
          <script language="JavaScript">
				document.all("timeminute").value=time_minute;
				document.all("timesecond").value=time_second;
			</script>
          <table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
            <tr>
              <td height="6"></td>
            </tr>
            <tr>
              <td align="center"><table cellSpacing="0" cellPadding="1" width="100%" align="center" border="0" style="LINE-HEIGHT: 200%">
                  <tr>
                  <td vAlign="bottom" width="20%"><FONT face="宋体"></FONT></td>
                  <td vAlign="bottom" align="center" width="50%"><span id="lblTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 14pt"><%=strPaperName%></span></td>
                  <td vAlign="bottom" align="right" width="30%"><span id="lblSubTitle">总共<%=strTestCount%>题<br>
共<%=strPaperMark%>分</span></td>
                </tr>
                </table></td>
            </tr>
            <tr>
              <td style="HEIGHT: 2px" bgColor="#0000ff" height="2"></td>
            </tr>
            <TR>
              <TD style="HEIGHT: 20px" height="20"><span id="lblMessage" style="COLOR: red"></span></TD>
            </TR>
            <tr>
              <td bgColor="#cee7ff"><table id="tblPaper" cellSpacing="0" cellPadding="1" width="100%" align="center" bgColor="white"
							border="0">
                  <%=strPaperContent%>
                </table></td>
            </tr>
          </table>
          <input type=hidden size=4 value="<%=intTestNum%>" name=irow>
        </form>
        
        <!----------------------------提示信息------------------------>
        <div id="submitexam2" style="display:none">
          <table cellSpacing="1" cellPadding="0"   bgColor="#666666" border="0" width="100%">
            <tr>
              <td  bgColor="#ff99cc" height="50"><div align="center">正在提交答卷，请稍候...</div></td>
            </tr>
          </table>
        </div>
        <div id="submitexam1" style="display:none">
          <table cellSpacing="1" cellPadding="0"   bgColor="#666666" border="0" width="100%">
            <tr>
              <td  bgColor="#33cccc" height="50"><div align="center">正在保存答卷，请稍候...</div></td>
            </tr>
          </table>
        </div>
        <div id="submitexam" style="display:none">
          <table cellSpacing="1" cellPadding="0"  bgColor="#666666" border="0" width="100%">
            <tr>
              <td  bgColor="#ff99cc" height="50"><div align="center">考试时间已到，正在提交答卷，请稍候...</div></td>
            </tr>
          </table>
        </div>
        
        <!-------------------------交卷提示 -------------------------------->
        <div id="submitexam3" style="display:none">
          <table  cellSpacing="0" borderColorDark="#ffffff"  bgColor="#ffffff"	borderColorLight="#000000" border="1" width="95%" align="center">
            <tr bgColor="#336699">
              <td  ><span><font color="#ffffff">提示信息</font></span></td>
              <td align="right"  ><IMG style="CURSOR: hand" onclick="cancel();" alt="关闭" src="../images/close.gif" border="0"></td>
            </tr>
            <tr>
              <td vAlign="middle" align="center"   bgColor="#cccccc" colSpan="2" height="90"><img src="../images/warning.gif" hspace="5" align="absMiddle">
                <label id="labmessage"></label></td>
            </tr>
            <tr>
              <td align="center" bgColor="#cccccc" colSpan="2" height="35"><input class="button" id="yes" onclick="submit();" type="button" value=" 确 定 " name="yes"  ><br>
<input class="button" id="no" onclick="cancel();" type="button" value=" 取 消 " name="no"  >
                <br> </td>
            </tr>
          </table>
        </div>
        
        
<!---------------答题数量提示------------------------------->
        <div id="submitexam4" style="display:none">
          <table>
            <tr bgColor="#336699">
              <td  ><span><font color="#ffffff">提示信息 已答题:<img src="../images/finished.gif" align="absMiddle">未答题:<img src="../images/nofinished.gif" align="absMiddle"></font></span></td>
              <td align="right" width="10"><IMG style="CURSOR: hand" onclick="cancel1();" alt="关闭" src="../images/close.gif" border="0"></td>
            </tr>
            <tr>
              <td vAlign="middle" align="center"   colSpan="2" bgColor="#cccccc" height="190"><%=strtable%></td>
            </tr>
            <tr>
              <td align="center" bgColor="#cccccc" colSpan="2" height="35"><input class="button" id="close" onclick="cancel1();" type="button" value=" 关 闭 " name="no"></td>
            </tr>
          </table>
        </div>
        
        
        <!----------------考试帐号提示------------------------>
        <div id="timediv" >
        

          <table cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
              <td class="tableheader" align="center"><hr> 
				帐号:<%=strLoginID%><br>
                姓名:<%=strUserName%><br> 
                <!--共<%=intExamTime%>
						分钟,剩余<label id="label_time">00分00秒</label><br>--> 
                <br>
                <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                  <tr>
                    <td align="center" ><input class="button" onclick="save();" type="button" value="暂时保存答卷" name="submitbtn1">
                     
                      <br></td>
                  </tr>
                  <tr>
                    <td align="center" ><input class="button" onclick="checkanswer();" type="button" value="检查答卷" name="submitbtn2">
                     
                      <br></td>
                  </tr>
                  <tr>
                    <td align="center"  ><input class="button" onclick="trysubmit();" type="button" value="交卷" name="submitbtn3">
                      
                      <br></td>
                  </tr>
                </table></td>
            </tr>
          </table>
        </div>
        <iframe name="iframe1" src="SaveJobAll.aspx" frameBorder="0" width="0%" scrolling="yes" height="0%"></iframe>
</body>
</HTML>
