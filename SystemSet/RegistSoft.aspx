<%@ Page language="c#" Inherits="EasyExam.SystemSet.RegistSoft" CodeFile="RegistSoft.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>软件注册</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../JavaScript/Calendar.js"></script>
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="200" cellSpacing="0" cellPadding="0" width="520" align="center" border="0">
				<tr>
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="160" cellSpacing="0"
							cellPadding="0" width="500" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">软件注册</font></td>
							</tr>
							<tr>
								<td>
									<TABLE id="Table1" style="BORDER-COLLAPSE: collapse" cellSpacing="0" borderColorDark="#6699ff"
										cellPadding="0" width="500" align="center" borderColorLight="#6699ff" border="0">
										<TR>
											<TD align="right" height="23" colspan="2">
											</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 161px" align="right" height="23">使用单位：</TD>
											<TD width="408" height="23"><asp:textbox id="txtRegistUnit" runat="server" CssClass="text" Width="240px"></asp:textbox></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 161px" align="right" height="23"><FONT face="宋体">机&nbsp;器&nbsp;码：</FONT></TD>
											<TD width="408" height="23"><FONT face="宋体">
													<asp:textbox id="txtMachineCode" runat="server" Width="128px" CssClass="text" ReadOnly="True"
														Enabled="False"></asp:textbox></FONT></TD>
										</TR>
										<TR>
											<td style="WIDTH: 161px" align="right" height="23"><FONT face="宋体">注&nbsp;册&nbsp;码：</FONT></td>
											<td width="408" height="23">
												<asp:textbox id="txtRegistCode" runat="server" Width="128px" CssClass="text"></asp:textbox>&nbsp;<a href="mailto:yxlbliss@163.com?subject=索取注册码">索取注册码</a></td>
										</TR>
										<tr>
											<td align="right" height="23" colspan="2">
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23"><asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onclick="window.location='../MainFrm.aspx';" type="button" value="返 回"
										name="button1"></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
