<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.StartJobOne" CodeFile="StartJobOne.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>正在作业</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
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
				form1.target="";
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&CheckAnswer=yes";
				form1.submit();
			}
			function trysubmit()//尝试提交
			{
				form1.target="";
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&TrySubmit=yes";
				form1.submit();
			}
			function cancel()//取消提交
			{
				submitexam3.style.visibility="hidden";
			}
			function cancel1()//关闭检查答卷
			{
				submitexam4.style.visibility="hidden";
			}
			function save()//保存答卷
			{
				submitexam.style.visibility="hidden";
				submitexam1.style.visibility="visible";
				submitexam2.style.visibility="hidden";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="hidden";
				form1.action="SaveJobOne.aspx";
				form1.target="iframe1";
				form1.submit();
				form1.target="";
				form1.action="";
			}
			function submit()//正式提交
			{
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="visible";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="hidden";
				submitexam.style.visibility="hidden";
				form1.target="";
				form1.action="SubmJobOne.aspx";
				form1.submit();
			}
			function timeend()//时间结束
			{
				submitexam.style.visibility="visible";
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="hidden";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="hidden";
				form1.target="";
				form1.action="SubmJobOne.aspx";
				form1.submit();
			}

			function seltestnum()//选择题号
			{
				submitexam0.style.visibility="visible";
				form1.target="";
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&SelTestNum="+document.all("select1").value+"&SelectTest=yes";
				form1.submit()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
			}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
			function priobtnclick()//选择上一题
			{
				submitexam0.style.visibility="visible";
				form1.target=""                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&PriorTest=yes";
				form1.submit()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
			}
			function nextbtnclick()//选择下一题
			{
				submitexam0.style.visibility="visible";
				form1.target="";
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&NextTest=yes";
				form1.submit();
			}
			function selecttest(intTestNum)//跳转题号
			{
				submitexam0.style.visibility="visible";
				form1.target="";
				form1.action="StartJobOne.aspx?PaperID="+document.all("PaperID").value+"&UserScoreID="+document.all("UserScoreID").value+"&TestNum="+document.all("irow").value+"&SelTestNum="+intTestNum+"&SelectTest=yes";
				form1.submit()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
			}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
		</script>
	</HEAD>
	<body onselectstart="return false" onkeydown="onKeyDown();" leftMargin="0" topMargin="0"
		onload="starttime();" rightMargin="0">
		<form name="form1" action="SubmJobOne.aspx" method="post">
			<input type=hidden size=4 value="<%=intPaperID%>" name=PaperID> <input type=hidden size=4 value="<%=intPassMark%>" name=PassMark>
			<input type=hidden size=4 value="<%=intFillAutoGrade%>" name=FillAutoGrade> <input type=hidden size=4 value="<%=intSeeResult%>" name=SeeResult>
			<input type=hidden size=4 value="<%=intAutoJudge%>" name=AutoJudge> <input type=hidden size=4 value="<%=intUserScoreID%>" name=UserScoreID>
			<input type="hidden" size="4" value="0" name="timeminute"> <input type="hidden" size="4" value="0" name="timesecond">
			<script language="JavaScript">
				document.all("timeminute").value=time_minute;
				document.all("timesecond").value=time_second;
			</script>
			<table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
				<tr>
					<td height="6"></td>
				</tr>
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="1" width="100%" align="center" border="0" style="LINE-HEIGHT: 200%">
							<tr>
								<td vAlign="bottom" width="20%"><FONT face="宋体"></FONT></td>
								<td vAlign="bottom" align="center" width="60%"><span id="lblTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 14pt"><%=strPaperName%></span></td>
								<td vAlign="bottom" align="right" width="20%"><span id="lblSubTitle">总共<%=strTestCount%>
										题共<%=strPaperMark%>分</span></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" bgColor="#0000ff" height="2"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 20px" height="20"><span id="lblMessage" style="COLOR: red"></span></TD>
				</TR>
			</table>
			<TABLE cellSpacing="0" borderColorDark="#ffffff" cellPadding="2" width="95%" align="center"
				bgColor="#cee7ff" borderColorLight="#0099cc" border="1">
				<TR>
					<TD align="center" bgColor="#0099cc" colSpan="2"><font color="#ffffff">帐号:<%=strLoginID%>&nbsp;&nbsp;姓名:<%=strUserName%><!--&nbsp;&nbsp;考试时间:<%=intExamTime%>
							分钟&nbsp;&nbsp;剩余时间:<label id="label_time">00分00秒</label>--></font>
					</TD>
				</TR>
				<TR>
					<TD align="left" bgColor="#cee7ff" colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td align="left" width="50%" height="20"><%=strTestTypeMark%></td>
								<td align="right" width="50%" height="20"><strong>进度:</strong><%=strPlan%>&nbsp;</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="WORD-BREAK: break-all" bgColor="#ffffff" colSpan="2" height="400">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<%=strPaperContent%>
						</div>
					</TD>
				</TR>
				<TR align="center">
					<TD class="tablebody" align="center" colSpan="2" height="32">&nbsp;<span>选择题号:</span><%=strSelectOption%>&nbsp;&nbsp;
						<INPUT class="button" id="priobtn" onclick="priobtnclick();" type="button" value=" 上一题 "
							name="priobtn">&nbsp; <INPUT class="button" id="nextbtn" onclick="nextbtnclick();" type="button" value=" 下一题 "
							name="nextbtn"> &nbsp;&nbsp; <input class="button" onclick="save();" type="button" value="保存答卷" name="submitbtn1">&nbsp;
						<input class="button" onclick="checkanswer();" type="button" value="检查答卷" name="submitbtn2">&nbsp;
						<input class="button" onclick="trysubmit();" type="button" value="提交答卷" name="submitbtn3">
					</TD>
				</TR>
			</TABLE>
			<input type=hidden size=4 value="<%=intTestNum%>" name=irow>
		</form>
		<div id="submitexam2" style="Z-INDEX:120;           ; LEFT:expression((document.body.clientWidth-300)/2);           VISIBILITY:hidden;           POSITION:absolute;           ; TOP:expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table width="300" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td width="300" height="50" bgcolor="#ff99cc">
						<div align="center">正在提交答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam1" style="Z-INDEX:120;           ; LEFT:expression((document.body.clientWidth-300)/2);           VISIBILITY:hidden;           POSITION:absolute;           ; TOP:expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table width="300" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td width="300" height="50" bgcolor="#33cccc"><div align="center">正在保存答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam0" style="Z-INDEX:120;           ; LEFT:expression((document.body.clientWidth-300)/2);           VISIBILITY:hidden;           POSITION:absolute;           ; TOP:expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table width="300" height="50" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td width="300" height="50" bgcolor="#3333cc"><div align="center"><font color="#ffffff">正在载入试题，请稍候...</font></div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam" style="Z-INDEX:120;           ; LEFT:expression((document.body.clientWidth-300)/2);           VISIBILITY:hidden;           POSITION:absolute;           ; TOP:expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table width="300" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td width="300" height="50" bgcolor="#ff99cc"><div align="center">考试时间已到，正在提交答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<%=strSubmitExam%>
		<%=strCheckAnswer%>
		<iframe name="iframe1" src="SaveJobOne.aspx" frameBorder="0" width="0%" scrolling="yes"
			height="0%"></iframe>
		<%=strJavaScript%>
	</body>
</HTML>
