<%@ Page language="c#" Inherits="EasyExam.PersonInfo.UpLoadFile" CodeFile="UpLoadFile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>上传文件</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="118" cellSpacing="0"
				cellPadding="0" width="300" align="center" border="0">
				<TR>
					<td style="WIDTH: 499px" align="center" height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#0054a6">上传文件</font></td>
				</TR>
				<tr>
					<td style="WIDTH: 499px; HEIGHT: 42px" align="center">
						<table id="AutoNumber1" style="WIDTH: 321px; BORDER-COLLAPSE: collapse; HEIGHT: 24px" borderColor="#111111"
							cellSpacing="0" cellPadding="0" width="321" border="0">
							<tr>
								<td width="88" height="23">
									<p align="right">文件：</p>
								</td>
								<td vAlign="bottom" width="310" height="23"><FONT face="宋体"><INPUT class="text" id="TestFile" style="WIDTH: 264px; HEIGHT: 22px" type="file" size="24"
											name="TestFile" runat="server"></FONT></td>
							</tr>
							<tr>
								<td width="321" colspan="2" height="23">
								</td>
							</tr>
							<tr>
								<td width="321" colspan="2" align="center" height="23">
									<asp:Button id="ButInput" runat="server" Text="提 交" CssClass="button" onclick="ButInput_Click"></asp:Button>&nbsp
									<input class="button" onclick="window.close();" type="button" value="取 消" name="button1"></td>
							</tr>
							<tr>
								<td width="321" colspan="2" height="23">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
