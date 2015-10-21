<%@ Page language="c#" Inherits="EasyExam.PersonInfo.TypeWord" CodeFile="TypeWord.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>打字窗口</title>
		<base target="_self">
		<meta name="GENERATOR" Content="Microsoft FrontPage 5.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../CSS/Style.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript">
			var time_type=<%=intTypeTime%>*60;
			var time_passed=0;
			function show_secs()
			{
				time_passed++;
				curmin=Math.floor(time_passed/60);
    			cursec=Math.floor(time_passed-curmin*60);
    			show_time.innerText="已经用时:"+curmin+"分"+cursec+"秒";
    			text1=document.form1.showword.value;
				type_speed.innerText='打字速度:'+Math.round((parseInt(document.form1.typeright.value)+parseInt(document.form1.typewrong.value))/time_passed*60)+"个字/分钟";
				document.form1.typespeed.value=Math.round((parseInt(document.form1.typeright.value)+parseInt(document.form1.typewrong.value))/time_passed*60);
			
				if (time_passed==time_type)
				{
					returntext();
				}
				window.setTimeout('show_secs()',1000);
			}
			function ok()
			{
				text1=document.form1.showword.value;
				text2=document.form1.textarea.value;
				while(text2.indexOf('\r\n')>-1)
				{
					text2=text2.replace('\r\n',"<br>")
				}
				wrong=0;
				right=0;
				flagword="";
				for (i=0;i<Math.min(text1.length,text2.length);i++)
				{
					if (text1.substr(i,4)=="<br>")
					{
						if (text2.substr(i,4)==text1.substr(i,4))
						{
							right=right+4;
						}
						else
						{
							wrong=wrong+4;
						}
						flagword=flagword+"<br>"
						i=i+3;
					}
					else
					{
						if ((text2.substr(i,1)==text1.substr(i,1))||((text2.substr(i,1).charCodeAt(0)==32)&&(text1.substr(i,1).charCodeAt(0)==160)))
						{
							right=right+1;
							flagword=flagword+"<span class='bg_right'>"+text1.substr(i,1)+"</span>";
						}
						else
						{
							wrong=wrong+1;
							flagword=flagword+"<span class='bg_wrong'>"+text1.substr(i,1)+"</span>";
						}
					}
				}
				show_word.innerHTML=flagword+text1.substr(i,text1.length);
				right_rate.innerText='正确率:'+Math.round((right/text1.length)*100,1)+"%";
				document.form1.typeright.value=right;
				document.form1.typewrong.value=wrong;
				document.form1.rightrate.value=Math.round((right/text1.length)*100,1);
			}
			function returntext()
			{ 
				var TestNum=document.form1.testnum.value;
				var obj=window.dialogArguments;
				obj.document.all('Answer'+TestNum).item(0).value=document.form1.typespeed.value;
				obj.document.all('Answer'+TestNum).item(1).value=document.form1.rightrate.value;
				window.close();
			}
		</script>
	</HEAD>
	<body oncontextmenu="return false" onLoad="show_secs();">
		<form name="form1" method="post" action="">
			<table width="560" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#6699ff">
				<tr>
					<td width="560" bgcolor="#004e98">
						<table width="100%" border="0" align="center" cellpadding="8" cellspacing="0">
							<tr>
								<div style="TABLE-LAYOUT:fixed;OVERFLOW-Y:scroll;WIDTH:560px;WORD-BREAK:break-all;HEIGHT:250px"
									class="typetxt" id="show_word">
									<%=strTestContent%>
								</div>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="bottom" width="560">
						<table width="100%" height="27" border="0" align="center" cellpadding="3" cellspacing="0"
							class="while">
							<tr>
								<td align="left" id="type_time">打字时间:<%=intTypeTime%>分钟</td>
								<td align="left" id="show_time">已经用时:0秒</td>
								<td align="left" id="type_speed">打字速度:0个字/分钟</td>
								<td align="left" id='right_rate'>正确率:0%</td>
								<td align="center" width="100"><input name="typebtn" type="submit" class="button" id="typebtn" value="提 交" onClick="javascript:returntext();"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" width="560"><textarea name="textarea" cols="58" rows="4" onpaste="return false" onKeyUp="ok()" class="typetxtarea"
							style="TABLE-LAYOUT:fixed;WIDTH:560px;WORD-BREAK:break-all;HEIGHT:80px"></textarea></td>
				</tr>
				<tr>
					<td height="30" colspan="2" width="560">
						<input name="testnum" type="hidden" id="testnum" value="<%=intTestNum%>"> <input name="showword" type="hidden" value="<%=strTestContent%>">
						<input name="typeright" type="hidden" id="typeright" value="0"> <input name="typewrong" type="hidden" id="typewrong" value="0">
						<input name="typespeed" type="hidden" id="typespeed" value="0"> <input name="rightrate" type="hidden" id="rightrate" value="0">
				</tr>
			</table>
		</form>
	</body>
</HTML>
