<%@ Page language="c#" Inherits="EasyExam.UserManag.NewOneUser" CodeFile="NewOneUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�½��ʻ�</title>
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
			<table height="392" cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="392" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">�½��ʻ�</font></td>
							</tr>
							<tr>
								<td align="center">
									<TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
										cellPadding="0" width="500" border="0">
										<tr height="25">
											<td align="center" colSpan="2" height="23"></td>
										</tr>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">��&nbsp;&nbsp;&nbsp;&nbsp;�ţ�</td>
											<td width="336" height="23"><asp:textbox id="txtLoginID" runat="server" CssClass="text"></asp:textbox><asp:button id="ButCheck" runat="server" CssClass="text" Text="����ظ�" onclick="ButCheck_Click"></asp:button>(<FONT face="����" color="#ff0033">*</FONT>)</td>
										</TR>
										<TR>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtUserName" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">��¼���룺</td>
											<td width="336" height="23"><asp:textbox id="txtNewUserPwd" runat="server" CssClass="text" Font-Size="9pt" TextMode="Password"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">ȷ�����룺</td>
											<td width="336" height="23"><asp:textbox id="txtSureUserPwd" runat="server" CssClass="text" Font-Size="9pt" TextMode="Password"></asp:textbox></td>
										</TR>
										<tr>
											<td style="WIDTH: 178px" align="right" height="23">��&nbsp;&nbsp;&nbsp;&nbsp;��</td>
											<td width="336" height="23"><asp:radiobuttonlist id="RBLUserSex" runat="server" RepeatDirection="Horizontal" Width="56px">
													<asp:ListItem Value="��" Selected="True">��</asp:ListItem>
													<asp:ListItem Value="Ů">Ů</asp:ListItem>
												</asp:radiobuttonlist></td>
										</tr>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">�������£�</td>
											<td width="336" height="23"><asp:textbox id="txtBirthday" onclick="jcomOpenCalender('txtBirthday');" runat="server" CssClass="text"
													Font-Size="9pt" Width="123px"></asp:textbox><A onclick="jcomOpenCalender('txtBirthday');" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A></td>
										</TR>
										<tr>
											<td style="WIDTH: 178px" vAlign="bottom" align="right" height="23">�ϴ���Ƭ��</td>
											<td width="336" height="23"><INPUT class="text" id="UpUserPhoto" type="file" size="20" name="File1" runat="server"></td>
										</tr>
										<TR>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">�������ţ�</td>
											<td style="HEIGHT: 23px" width="336"><asp:dropdownlist id="DDLDept" runat="server" Width="88px"></asp:dropdownlist></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">ְ&nbsp;&nbsp;&nbsp;&nbsp;��</td>
											<td style="HEIGHT: 23px" width="336"><asp:dropdownlist id="DDLJob" runat="server" Width="88px"></asp:dropdownlist></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtTelephone" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">֤�����ͣ�</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtCertType" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">֤�����룺</td>
											<td width="336" height="23"><asp:textbox id="txtCertNum" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<TD style="WIDTH: 178px" align="right" height="23">��&nbsp;¼&nbsp;IP��</TD>
											<TD width="336" height="23"><FONT face="����"><asp:textbox id="txtLoginIP" runat="server" CssClass="text"></asp:textbox></FONT></TD>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">��&nbsp;&nbsp;&nbsp;&nbsp;�ͣ�</td>
											<td width="336" height="23"><asp:dropdownlist id="DDLUserType" runat="server" Width="80px">
													<asp:ListItem Value="0">��ͨ�ʻ�</asp:ListItem>
													<asp:ListItem Value="1">�����ʻ�</asp:ListItem>
												</asp:dropdownlist></td>
										</TR>
										<tr>
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">״&nbsp;&nbsp;&nbsp;&nbsp;̬��</td>
											<td style="HEIGHT: 23px" width="336"><asp:dropdownlist id="DDLUserState" runat="server" Width="80px">
													<asp:ListItem Value="1">����</asp:ListItem>
													<asp:ListItem Value="0">��ֹ</asp:ListItem>
												</asp:dropdownlist></td>
										</tr>
										<tr>
											<td align="center" width="514" colSpan="2" height="23"><FONT face="����"></FONT></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23"><asp:button id="ButInput" runat="server" CssClass="button" Text="�� ��" onclick="ButInput_Click"></asp:button>&nbsp;
									<input class="button" onclick="window.close();" type="button" value="ȡ ��" name="Button"></td>
							</tr>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
