<%@ Page language="c#" Inherits="EasyExam.UserManag.NewMoreUser" CodeFile="NewMoreUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����½��ʻ�</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="224" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="10"></td>
				</tr>
				<tr>
					<td valign="top">
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="184" cellSpacing="0"
							cellPadding="0" width="100%" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">�����½��ʻ�</font></td>
							</tr>
							<tr>
								<td>
									<TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" height="200"
										cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr height="25">
											<td align="center" colSpan="2" height="23"></td>
										</tr>
										<TR>
											<td style="WIDTH: 176px" align="right" height="23">��ʼ�ʺţ�</td>
										  <td width="324" height="23" align="left"><asp:textbox id="txtSLoginID" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 176px" align="right" height="23">��ֹ�ʺţ�</td>
										  <td width="324" height="23" align="left"><asp:textbox id="txtELoginID" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 176px" align="right" height="23">�������ţ�</td>
										  <td width="324" height="23" align="left"><asp:dropdownlist id="DDLDept" runat="server" Width="88px"></asp:dropdownlist></td>
										</TR>
										<tr>
											<td style="WIDTH: 176px" align="right" height="23">ְ&nbsp;&nbsp; &nbsp;��</td>
										  <td width="324" height="23" align="left"><asp:dropdownlist id="DDLJob" runat="server" Width="88px"></asp:dropdownlist></td>
										</tr>
										<TR>
											<td style="WIDTH: 176px" align="right" height="23">��&nbsp;&nbsp; &nbsp;�ͣ�</td>
											<td width="324" height="23" align="left"><asp:dropdownlist id="DDLUserType" runat="server" Width="88px">
													<asp:ListItem Value="0">��ͨ�ʻ�</asp:ListItem>
													<asp:ListItem Value="1">�����ʻ�</asp:ListItem>
										  </asp:dropdownlist></td>
										</TR>
										<TR>
											<TD style="WIDTH: 176px" align="right" height="23">״&nbsp;&nbsp;&nbsp; ̬��</TD>
											<TD width="324" height="23" align="left"><FONT face="����">
											  <asp:dropdownlist id="DDLUserState" runat="server" Width="88px">
														<asp:ListItem Value="1" Selected="True">����</asp:ListItem>
														<asp:ListItem Value="0">��ֹ</asp:ListItem>
										  </asp:dropdownlist></FONT></TD>
										</TR>
										<tr>
											<td align="center" width="500" colSpan="2" height="23"></td>
										</tr>
								  </TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23">&nbsp;<asp:button id="ButInput" runat="server" CssClass="button" Text="�� ��" onclick="ButInput_Click"></asp:button>&nbsp;
									<INPUT class="button" onClick="window.location='../MainFrm.aspx';" type="button" value="�� ��"
										name="button1"></td>
							</tr>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
