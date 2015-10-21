<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RedoExamOne.aspx.cs" Inherits="PersonInfo_RedoExamOne" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head>
		<title>正在考试</title>
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
  				if (time_second==0)
  				{
    				time_second=59;
    				time_minute-=1;
    				time_passed+=1;
  				} 
  				else
  				{
    				time_second-=1;
    				time_passed+=1;
  				};
  				if (time_passed%autosavetime==0)
  				{
    				save();//保存答卷
    				time_passed=0;
  				};
  				if (time_minute<0)//答题时间到，自动提交答卷
  				{ 
    				timediv.style.visibility="hidden";
     				submitexam.style.visibility="visible"
	   				submitexam1.style.visibility="hidden";
    				submitexam2.style.visibility="hidden";
    				submitexam3.style.visibility="hidden";
    				submitexam4.style.visibility="hidden";
    				timeend();
  				}
  				else
  				{
    				document.all.label_time.innerHTML=time_minute+"分"+time_second+"秒";
    				timeid=setTimeout("showtime()",1000);
    				timerun=true;
    				document.all("timeminute").value=time_minute;                                                
    				document.all("timesecond").value=time_second;
  				}
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
				submitexam.style.visibility="hidden";
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="hidden";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="visible";
			}
			function trysubmit()//尝试提交
			{
				total=document.all("irow").value;
				for(i=1;i<=total;i++)
				{
					if (testcheck(i)==false)
					{
						submitexam.style.visibility="hidden";
						submitexam1.style.visibility="hidden";
						submitexam2.style.visibility="hidden";
						submitexam3.style.visibility="visible";
						submitexam4.style.visibility="hidden";
						document.all.labmessage.innerHTML="第"+i+"题您还没有做，是否确定交卷？";
						return; 
					}
				}
				submitexam.style.visibility="hidden";
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="hidden";
				submitexam3.style.visibility="visible";
				submitexam4.style.visibility="hidden";
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
						submitexam3.style.visibility="hidden";
						return;
					}
				}
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
				form1.action="SaveExamAll.aspx";
				form1.target="iframe1";
				form1.submit();
				form1.target="";
				form1.action="";
			}
			function submit()//正式提交
			{
				timediv.style.visibility="hidden";
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="visible";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="hidden";
				submitexam.style.visibility="hidden";
				form1.target="";
				form1.action="SubRedoOne.aspx";
				form1.submit();
			}
			function timeend()//时间结束
			{
				timediv.style.visibility="hidden";
				submitexam.style.visibility="visible";
				submitexam1.style.visibility="hidden";
				submitexam2.style.visibility="hidden";
				submitexam3.style.visibility="hidden";
				submitexam4.style.visibility="hidden";
				form1.target="";
				form1.action="SubRedoOne.aspx";
				form1.submit();
			}
			 function showFuHao()
            {
                //window.open("fuHao.aspx");
                window.open('fuHao.aspx','newwindow','height=200,width=400,top=500,left=700,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
            } 
		</script>
	</head>
	<body onselectstart="return false" onkeydown="onKeyDown();" leftMargin="0" topMargin="0"
		onload="starttime();" rightMargin="0">
		<form id="form1" action="SubRedoOne.aspx" method="post">
			<input type=hidden size=4 value="<%=intPaperID%>" name=PaperID> <input type=hidden size=4 value="<%=intPassMark%>" name=PassMark>
			<input type=hidden size=4 value="<%=intFillAutoGrade%>" name=FillAutoGrade> <input type=hidden size=4 value="<%=intSeeResult%>" name=SeeResult>
			<input type=hidden size=4 value="<%=intAutoJudge%>" name=AutoJudge> <input type=hidden size=4 value="<%=intUserScoreID%>" name=UserScoreID>
			<input type="hidden" size="4" value="0" name="timeminute"> <input type="hidden" size="4" value="0" name="timesecond">
			<script language="JavaScript">
			    document.all("timeminute").value = time_minute;
			    document.all("timesecond").value = time_second;
			</script>
			<table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
				<tr>
					<td height="6"></td>
				</tr>
				<tr>
					<td align="center">
						<table style="LINE-HEIGHT: 200%" cellSpacing="0" cellPadding="1" width="100%" align="center"
							border="0">
							<tr>
								<td vAlign="bottom" width="20%"><font face="宋体"></FONT></td>
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
				<tr>
					<td bgColor="#cee7ff">
						<table id="tblPaper" cellSpacing="0" cellPadding="1" width="100%" align="center" bgColor="white"
							border="0">
							<%=strPaperContent%>
						</table>
					</td>
				</tr>
			</table>
			<input type=hidden size=4 value="<%=intTestNum%>" name=irow>
		</form>
		<div id="timediv" style="Z-INDEX: 120;                    ; LEFT: expression(document.body.clientWidth-167);                    POSITION: absolute;                    ; TOP: expression(this.style.pixelHeight)">
			<table cellSpacing="0" cellPadding="0" width="160" border="0">
				<tr>
					<td class="tableheader" align="center">帐号:<%=strLoginID%><br>
						姓名:<%=strUserName%><br>
						共<%=intExamTime%>
						分钟,剩余<label id="label_time">00分00秒</label><br>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<%--<tr>
								<td align="center" height="30"><input class="button" onclick="save();" type="button" value="保存答卷" name="submitbtn1">
								</td>
							</tr>
							<tr>
								<td align="center" height="30"><input class="button" onclick="checkanswer();" type="button" value="检查答卷" name="submitbtn2">
								</td>
							</tr>--%>
							<tr>
								<td align="center" height="30"><input class="button" onclick="trysubmit();" type="button" value="提交答卷" name="submitbtn3">
								</td>
							</tr>
							 <tr>
								<td align="center" height="30"><input class="button" onclick="showFuHao();" type="button" value="特殊符号" name="submitbtn4">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam2" style="Z-INDEX: 120;                    ; LEFT: expression((document.body.clientWidth-300)/2);                    VISIBILITY: hidden;                    POSITION: absolute;                    ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table cellSpacing="1" cellPadding="0" width="300" bgColor="#666666" border="0">
				<tr>
					<td width="300" bgColor="#ff99cc" height="50">
						<div align="center">正在提交答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam1" style="Z-INDEX: 120;                    ; LEFT: expression((document.body.clientWidth-300)/2);                    VISIBILITY: hidden;                    POSITION: absolute;                    ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table cellSpacing="1" cellPadding="0" width="300" bgColor="#666666" border="0">
				<tr>
					<td width="300" bgColor="#33cccc" height="50">
						<div align="center">正在保存答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam" style="Z-INDEX: 120;                    ; LEFT: expression((document.body.clientWidth-300)/2);                    VISIBILITY: hidden;                    POSITION: absolute;                    ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table cellSpacing="1" cellPadding="0" width="300" bgColor="#666666" border="0">
				<tr>
					<td width="300" bgColor="#ff99cc" height="50">
						<div align="center">考试时间已到，正在提交答卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam3" style="Z-INDEX: 120;                    ; LEFT: expression((document.body.clientWidth-320)/2);                    VISIBILITY: hidden;                    POSITION: absolute;                    ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-90)/2-20)">
			<table style="CURSOR: default" cellSpacing="0" borderColorDark="#ffffff" width="320" bgColor="#ffffff"
				borderColorLight="#000000" border="1">
				<tr bgColor="#336699">
					<td width="320"><span><font color="#ffffff">提示信息</font></span></td>
					<td align="right" width="10"><IMG style="CURSOR: hand" onclick="cancel();" alt="关闭" src="../images/close.gif" border="0"></td>
				</tr>
				<tr>
					<td vAlign="middle" align="center" width="320" bgColor="#cccccc" colSpan="2" height="90"><img src="../images/warning.gif" hspace="5" align="absMiddle"><label id="labmessage"></label></td>
				</tr>
				<tr>
					<td align="center" bgColor="#cccccc" colSpan="2" height="35"><input class="button" id="yes" onclick="submit();" type="button" value=" 确 定 " name="yes">&nbsp;&nbsp;&nbsp;&nbsp;
						<input class="button" id="no" onclick="cancel();" type="button" value=" 取 消 " name="no">
					</td>
				</tr>
			</table>
		</div>
		<div id="submitexam4" style="Z-INDEX: 120;                    ; LEFT: expression((document.body.clientWidth-435)/2);                    VISIBILITY: hidden;                    POSITION: absolute;                    ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-190)/2-20)">
			<table style="CURSOR: default" cellSpacing="0" borderColorDark="#ffffff" width="435" bgColor="#ffffff"
				borderColorLight="#000000" border="1">
				<tr bgColor="#336699">
					<td width="435"><span><font color="#ffffff">提示信息 已答题:<img src="../images/finished.gif" align="absMiddle">未答题:<img src="../images/nofinished.gif" align="absMiddle"></font></span></td>
					<td align="right" width="10"><IMG style="CURSOR: hand" onclick="cancel1();" alt="关闭" src="../images/close.gif" border="0"></td>
				</tr>
				<tr>
					<td vAlign="middle" align="center" width="435" colSpan="2" bgColor="#cccccc" height="190"><%=strtable%></td>
				</tr>
				<tr>
					<td align="center" bgColor="#cccccc" colSpan="2" height="35"><input class="button" id="close" onclick="cancel1();" type="button" value=" 关 闭 " name="no">
					</td>
				</tr>
			</table>
		</div>
		<iframe name="iframe1" src="SaveExamAll.aspx" frameBorder="0" width="0%" scrolling="yes"
			height="0%"></iframe>
		<SCRIPT language="javascript">		    //自由拖动浮标  
		    function xytop() {
		        item = eval('document.all.timediv.style');
		        item.pixelLeft = document.body.scrollLeft + w_x - 34;
		        item.pixelTop = w_y + document.body.scrollTop - 64;
		        timediv.style.pixeltop = document.body.scrollTop + timediv.style.pixelHeight;
		        timediv.style.pixelLeft = document.body.clientWidth - 167;
		        item.visibility = 'visible';
		    }  
		</SCRIPT>
		<SCRIPT language="javascript">		    //自由拖动浮标  
		    self.onError = null;
		    currentX = currentY = 30;
		    whichIt = null;
		    lastScrollX = 0; lastScrollY = 0;
		    NS = (document.layers) ? 1 : 0;
		    IE = (document.all) ? 1 : 0;
		    function heartBeat() {
		        if (IE) { diffY = document.body.scrollTop; diffX = document.body.scrollLeft; }
		        if (NS) { diffY = self.pageYOffset; diffX = self.pageXOffset; }
		        if (diffY != lastScrollY) {
		            percent = .1 * (diffY - lastScrollY);
		            if (percent > 0) percent = Math.ceil(percent);
		            else percent = Math.floor(percent);
		            if (IE) document.all.timediv.style.pixelTop += percent;
		            if (NS) document.timediv.top += percent;
		            lastScrollY = lastScrollY + percent;
		        }
		        if (diffX != lastScrollX) {
		            percent = .1 * (diffX - lastScrollX);
		            if (percent > 0) percent = Math.ceil(percent);
		            else percent = Math.floor(percent);
		            if (IE) document.all.timediv.style.pixelLeft += percent;
		            if (NS) document.timediv.left += percent;
		            lastScrollX = lastScrollX + percent;
		        }
		    }
		    function checkFocus(x, y) {
		        stalkerx = document.timediv.pageX;
		        stalkery = document.timediv.pageY;
		        stalkerwidth = document.timediv.clip.width;
		        stalkerheight = document.timediv.clip.height;
		        if ((x > stalkerx && x < (stalkerx + stalkerwidth)) && (y > stalkery && y < (stalkery + stalkerheight))) return true;
		        else return false;
		    }
		    function grabIt(e) {
		        if (IE) {
		            whichIt = event.srcElement;

		            while (whichIt.id.indexOf("timediv") == -1) {
		                whichIt = whichIt.parentElement;
		                if (whichIt == null) { return true; }
		            }
		            whichIt.style.pixelLeft = whichIt.offsetLeft;
		            whichIt.style.pixelTop = whichIt.offsetTop;
		            currentX = (event.clientX + document.body.scrollLeft);
		            currentY = (event.clientY + document.body.scrollTop);
		        } else {
		            window.captureEvents(Event.MOUSEDOWN);
		            if (checkFocus(e.pageX, e.pageY)) {
		                whichIt = document.timediv;
		                StalkerTouchedX = e.pageX - document.timediv.pageX;
		                StalkerTouchedY = e.pageY - document.timediv.pageY;
		            }
		        }
		        return true;
		    }
		    function moveIt(e) {
		        if (whichIt == null) { return false; }
		        if (IE) {
		            newX = (event.clientX + document.body.scrollLeft);
		            newY = (event.clientY + document.body.scrollTop);
		            distanceX = (newX - currentX); distanceY = (newY - currentY);
		            currentX = newX; currentY = newY;
		            whichIt.style.pixelLeft += distanceX;
		            whichIt.style.pixelTop += distanceY;
		            if (whichIt.style.pixelTop < document.body.scrollTop) whichIt.style.pixelTop = document.body.scrollTop;
		            if (whichIt.style.pixelLeft < document.body.scrollLeft) whichIt.style.pixelLeft = document.body.scrollLeft;
		            if (whichIt.style.pixelLeft > document.body.offsetWidth - document.body.scrollLeft - whichIt.style.pixelWidth - 20) whichIt.style.pixelLeft = document.body.offsetWidth - whichIt.style.pixelWidth - 20;
		            if (whichIt.style.pixelTop > document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5) whichIt.style.pixelTop = document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5;
		            event.returnValue = false;
		        } else {
		            whichIt.moveTo(e.pageX - StalkerTouchedX, e.pageY - StalkerTouchedY);
		            if (whichIt.left < 0 + self.pageXOffset) whichIt.left = 0 + self.pageXOffset;
		            if (whichIt.top < 0 + self.pageYOffset) whichIt.top = 0 + self.pageYOffset;
		            if ((whichIt.left + whichIt.clip.width) >= (window.innerWidth + self.pageXOffset - 17)) whichIt.left = ((window.innerWidth + self.pageXOffset) - whichIt.clip.width) - 17;
		            if ((whichIt.top + whichIt.clip.height) >= (window.innerHeight + self.pageYOffset - 17)) whichIt.top = ((window.innerHeight + self.pageYOffset) - whichIt.clip.height) - 17;
		            return false;
		        }
		        return false;
		    }
		    function dropIt() {
		        whichIt = null;
		        if (NS) window.releaseEvents(Event.MOUSEDOWN);
		        return true;
		    }
		    if (NS) {
		        window.captureEvents(Event.MOUSEUP | Event.MOUSEDOWN);
		        window.onmousedown = grabIt;
		        window.onmousemove = moveIt;
		        window.onmouseup = dropIt;
		    }
		    if (IE) {
		        document.onmousedown = grabIt;
		        document.onmousemove = moveIt;
		        document.onmouseup = dropIt;
		    }
		    if (NS || IE) action = window.setInterval("heartBeat()", 1);
		    timediv.style.visibility = "visible";
		    timediv.style.pixeltop = document.body.scrollTop + timediv.style.pixelHeight;
		    timediv.style.pixelLeft = document.body.clientWidth - 167;  
		</SCRIPT>
	</body>
</HTML>
