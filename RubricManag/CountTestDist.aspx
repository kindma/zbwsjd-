<%@ Page language="c#" Inherits="EasyExam.RubricManag.CountTestDist" CodeFile="CountTestDist.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����ֲ�</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="116" cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="116" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24" colSpan="2"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">��<asp:label id="labSubject" runat="server" ForeColor="White"></asp:label>������ֲ�</font></td>
							</tr>
							<TR bgColor="#ffffff">
								<TD style="WIDTH: 80px" align="right" height="23">���ͷֲ���</TD>
								<TD width="421" height="23">
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="400" border="0">
										<%=strTestType%>
									</TABLE>
								</TD>
							</TR>
							<TR bgColor="#f7f7f7">
								<TD style="WIDTH: 80px" align="right" height="23">֪ʶ��ֲ���</TD>
								<TD width="421" height="23">
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="400" border="0">
										<%=strTestLore%>
									</TABLE>
								</TD>
							</TR>
							<TR bgColor="#ffffff">
								<TD style="WIDTH: 80px" align="right" height="23">�Ѷȷֲ���</TD>
								<TD width="421" height="23">
									<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="400" border="0">
										<%=strTestDiff%>
									</TABLE>
								</TD>
							</TR>
							<TR height="30">
								<TD align="center" colSpan="2" height="23"><input class="button" onclick="window.close();" type="button" value="�� ��" name="button1"></TD>
							</TR>
						</table>
						<!--���ݽ���-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
