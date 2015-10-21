<%@ Page language="c#" Inherits="EasyExam.ProcessManag.JudgeAnswer" CodeFile="JudgeAnswer.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>手动评卷</title>
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
		<script language="JavaScript">
			function checkscore(maxvalue)
			{
				var input_value=window.event.srcElement.value;
				if (isNaN(input_value)||(trim(input_value)==""))
				{
					input_value="-1";
				}
				if ((parseFloat(input_value)>maxvalue)||(parseFloat(input_value)<0))
				{
					alert("分数输入错误！\n分数应在0~"+maxvalue+"之间。");
					if (parseFloat(input_value)<0)
					{
						window.event.srcElement.value=0;
					}
					else
					{
						window.event.srcElement.value=maxvalue;
					}
					window.event.keyCode=0;
					window.event.srcElement.focus;
					window.event.returnValue=0;
				}
			}
			function checkall()
			{
				var total=document.all("irow").value;
				for(i=1;i<=total;i++)
				{
					var testnum=document.all("TestNum"+i).value;
					if (isNaN(document.all("UserScore"+i).value)||(trim(document.all("UserScore"+i).value)==""))
					{
						alert("第"+testnum+"题您还没有评分呢！");
						window.event.ReturnValue=0;
						document.all("UserScore"+i).focus;
						return false;
					}
				}
				if (confirm('您确定提交手动评分吗？')==false) window.event.returnValue=false;
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server" onsubmit="return checkall()">
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
				<tr>
					<td align="center" style="HEIGHT: 20px" height="20"><span id="lblMessage">帐号:<%=strLoginID%>&nbsp;&nbsp;姓名:<%=strUserName%>&nbsp;&nbsp;答题时间:<%=strExamTime%>&nbsp;&nbsp;通过分数:<%=strPassMark%>&nbsp;&nbsp;考生得分:<%=strTotalMark%></span></td>
				</tr>
				<tr>
					<td bgColor="#cee7ff">
						<table id="tblPaper" cellSpacing="0" cellPadding="1" width="100%" align="center" bgColor="white"
							border="0">
							<%=strPaperContent%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center"><asp:button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:button>&nbsp;
						<input class="button" onclick="window.close();" type="button" value="取 消" name="Button">
						<A onclick="window.close();" href="#"></A>
					</td>
				</tr>
			</table>
			<input type=hidden size=4 value="<%=intTestNo%>" name=irow>
		</form>
	</body>
</HTML>
