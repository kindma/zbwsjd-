<%@ Page language="c#" Inherits="EasyExam.SystemSet.NewMoreDept" CodeFile="NewMoreDept.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����½�����</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="100" cellSpacing="0"
							cellPadding="0" width="100%" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">�����½�����</font></td>
							</tr>
							<tr>
								<td>
									<!--����-->
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
										<tr>
											<td colspan="2" height="23"><br>
											</td>
										</tr>
										<TR>
											<td style="WIDTH: 137px" align="right">�������ƣ�</td>
											<td><asp:TextBox runat="server" Height="182px" TextMode="MultiLine" Width="256px" CssClass="text"
													ID="txtDeptName"></asp:TextBox>
											</td>
										</TR>
										<tr>
											<td colspan="2" align="center" height="23"><font color="red">ע����������֮���ð�Ƕ��ţ�,���ָ���</font><br>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23"><asp:Button id="ButInput" runat="server" CssClass="button" Text="�� ��" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="�� ��" name="button1"></td>
							</tr>
						</table>
						<!--���ݽ���-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
