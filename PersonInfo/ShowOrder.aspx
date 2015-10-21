<%@ Page language="c#" Inherits="EasyExam.PersonInfo.ShowOrder" CodeFile="ShowOrder.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>查看排名</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="270" align="center" border="0"
				height="56" style="WIDTH: 270px; HEIGHT: 56px">
				<tr>
					<td style="WIDTH: 60px; HEIGHT: 88px" align="center">
						<img border='0' src='../images/Information.gif'>
					</td>
					<td style="WIDTH: 210px; HEIGHT: 88px" align="left"><asp:Label id="labOrder" runat="server"></asp:Label>
					</td>
				</tr>
				<TR>
					<TD style="WIDTH: 270px; HEIGHT: 23px" align="center" colspan="2"><input class="button" onclick="window.close();" type="button" value="关 闭" name="button1">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
