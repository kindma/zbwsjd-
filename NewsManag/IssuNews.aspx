<%@ Page language="c#" Inherits="EasyExam.NewsManag.IssuNews" CodeFile="IssuNews.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>发布新闻</title>
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
			<table height="300" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td colspan="2">
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="336" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="bg_dh_middle.gif" bgColor="#ffffff"
									height="24" colspan="2"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">发布新闻</font></td>
							</tr>
							<TR>
								<TD style="WIDTH: 74px; HEIGHT: 23px" align="right">新闻标题：</TD>
								<TD style="HEIGHT: 23px" width="408"><asp:textbox id="txtNewsTitle" runat="server" Width="344px" CssClass="text" ToolTip="最多可输入100个字符"
										MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<td style="WIDTH: 74px; HEIGHT: 219px" align="right">新闻内容：</td>
								<td style="HEIGHT: 219px" width="408" colSpan="5"><FONT face="宋体"><asp:TextBox runat="server" Height="248px" TextMode="MultiLine" ToolTip="最多可输入800个字符" Width="344px"
											CssClass="text" ID="txtNewsContent" MaxLength="800"></asp:TextBox>
									</FONT>
								</td>
							</TR>
							<TR>
								<TD style="WIDTH: 74px" align="right" height="26"><FONT face="宋体">浏览人员：</FONT></TD>
								<TD width="408" colSpan="5"><ASP:RADIOBUTTON id="rbAllAccount" runat="server" GroupName="SelectAccount" Checked="True" Text="所有帐号"></ASP:RADIOBUTTON><FONT face="宋体">&nbsp;</FONT>&nbsp;
									<ASP:RADIOBUTTON id="rbSelectAccount" runat="server" GroupName="SelectAccount" Text=" "></ASP:RADIOBUTTON><asp:button id="ButSelectAccount" runat="server" Width="54px" CssClass="text" Text="选择..." ToolTip="选择浏览人员" onclick="ButSelectAccount_Click"></asp:button><asp:textbox id="txtSelectAccount" runat="server" Width="184px" CssClass="text" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<td style="WIDTH: 74px" align="right" height="24"><FONT face="宋体">发布日期：</FONT></td>
								<td width="408" colSpan="5"><asp:textbox id="txtCreateDate" runat="server" Width="80px" Enabled="False"></asp:textbox></td>
							</TR>
							<tr height="30">
								<td align="center" width="518" colSpan="6" height="24">
									<asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
									<input name="Button" type="button" class="button" value="取 消" onClick="window.close();"></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
