<%@ Page %>
<HTML>
	<HEAD>
		<TITLE>选择时间</TITLE>
		<link rel="stylesheet" href="../Css/Common.css">
			<script language="C#" runat="server">
	//Response.Buffer = True 
	//Response.ExpiresAbsolute = Now()-0.1 
	//Response.Expires = 0 
	//Response.CacheControl = "no-cache"
			</script>
			<STYLE TYPE="text/css"> .normal{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;BACKGROUND: #ffffff}
	.today {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;font-weight:bold;BACKGROUND: #6699cc}
	.satday{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;color:green}
	.sunday{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;color:red}
	.days {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;font-weight:bold}
	.clickday {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;BACKGROUND:#FDBF6E}
	</STYLE>
			<SCRIPT LANGUAGE="JavaScript">
			//var arguments=window.dialogArguments;
			//var CurrentDate=new Date(arguments);
			
			function ReturnValue()
			{
				window.returnValue=document.all("txtHour").value+":"+document.all("txtMinute").value+":"+document.all("txtSecond").value;
				window.close();
			}

			</SCRIPT>
	</HEAD>
	<BODY style="MARGIN:0px;OVERFLOW:auto">
		<table cellspacing="0" cellpadding="0" width="100%" border="0" align="center" height="100%">
			<tr height="100%">
				<td valign="middle">
					<table cellspacing="0" cellpadding="2" width="100%" border="0" bgcolor="white" align="center">
						<tr>
							<td width="50%"></td>
							<td><input type="text" class="Text" id="txtHour" value="00" style="WIDTH:22px" maxLength="2"
									onblur="CheckInputHour();"></td>
							<td nowrap>
								<select id="ddlHour" onchange="document.all('txtHour').value=document.all('ddlHour').value;">
									<SCRIPT LANGUAGE="JavaScript">
										for(var i=0;i<2;i++)
										{
											for(var j=0;j<10;j++)
											{
												document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
											}
										}
									</SCRIPT>
									<OPTION value="20">20</OPTION>
									<OPTION value="21">21</OPTION>
									<OPTION value="22">22</OPTION>
									<OPTION value="23">23</OPTION>
								</select>小时
							</td>
							<td><input type="text" class="Text" id="txtMinute" value="00" style="WIDTH:22px" maxLength="2"
									onblur="CheckInputMinute();"></td>
							<td nowrap>
								<select id="ddlMinute" onchange="document.all('txtMinute').value=document.all('ddlMinute').value;">
									<SCRIPT LANGUAGE="JavaScript">
										for(var i=0;i<6;i++)
										{
											for(var j=0;j<10;j++)
											{
												document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
											}
										}
									</SCRIPT>
								</select>分钟
							</td>
							<td><input type="text" class="Text" id="txtSecond" value="00" style="WIDTH:22px" maxLength="2"
									onblur="CheckInputSecond()"></td>
							<td nowrap>
								<select id="ddlSecond" onchange="document.all('txtSecond').value=document.all('ddlSecond').value;">
									<SCRIPT LANGUAGE="JavaScript">
										for(var i=0;i<6;i++)
										{
											for(var j=0;j<10;j++)
											{
												document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
											}
										}
									</SCRIPT>
								</select>秒
							</td>
							<td width="50%"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<TABLE cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
						<tr>
							<td height="5">
							</td>
						</tr>
						<tr bgcolor="#8a622e">
							<td height="1">
							</td>
						</tr>
						<TR height="30" bgcolor="#fdbf6e">
							<TD ALIGN="center">
								<Input type="button" id="btnOK" value="确定" OnClick="ReturnValue();" class="ShortButton">
								<Input type="button" id="btnEmpty" value="清空" OnClick="window.returnValue='';window.close();"	class="ShortButton" title="返回空白">
								<Input type="button" value="取消" OnClick="window.close();" class="ShortButton">
							</TD>
						</TR>
					</TABLE>
				</td>
			</tr>
		</table>
		<SCRIPT LANGUAGE="JavaScript">
			function CheckInputHour()
			{
				if(document.all('txtHour').value.length==1) document.all('txtHour').value="0"+document.all('txtHour').value;
				document.all('ddlHour').value=document.all('txtHour').value;
				if(document.all('ddlHour').value=='')
				{
					alert('不合法的小时！');document.all('txtHour').focus();
				}
				
			}
			
			function CheckInputMinute()
			{
				if(document.all('txtMinute').value.length==1) document.all('txtMinute').value="0"+document.all('txtMinute').value;
				document.all('ddlMinute').value=document.all('txtMinute').value;
				if(document.all('ddlMinute').value=='')
				{
					alert('不合法的分钟！');document.all('txtMinute').focus();
				}
			}
			
			function CheckInputSecond()
			{
				if(document.all('txtSecond').value.length==1) document.all('txtSecond').value="0"+document.all('txtSecond').value;
				document.all('ddlSecond').value=document.all('txtSecond').value;
				if(document.all('ddlSecond').value=='')
				{
					alert('不合法的秒！');document.all('txtSecond').focus();
				}
			}
		</SCRIPT>
	</BODY>
</HTML>
