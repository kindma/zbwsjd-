<%@ Page language="c#" Inherits="EasyExam.SystemSet.SetOther" CodeFile="SetOther.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>�ۺ�����</title>
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
			<table height="100" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="100" cellSpacing="0"
							cellPadding="0" width="100%" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24" colspan="3">
								<font face="����" style="FONT-SIZE: 16px" color="#ffffff">
								�ۺ�����</font></td>
							</tr>
							<tr>
											<TD style="WIDTH: 161px" align="right" height="34">��¼ʱ�䷶Χ��</TD>
											<TD width="408" height="34" colspan="2"><asp:textbox id="txtStartTime" runat="server" ToolTip="���ѡ��ʱ��" CssClass="text" Width="104px"
													onclick="jcomSelectTime('txtStartTime',0);"></asp:textbox>-
												<asp:textbox id="txtEndTime" runat="server" ToolTip="���ѡ��ʱ��" CssClass="text" Width="104px" onclick="jcomSelectTime('txtEndTime',0);"></asp:textbox></TD>
										</tr>
										<TR>
											<td style="WIDTH: 161px" align="right" height="34"><FONT face="����">��&nbsp;¼&nbsp;IP��Χ��</FONT></td>
											<td width="408" height="34" colspan="2"><asp:textbox id="txtStartIP" runat="server" CssClass="text" Width="104px"></asp:textbox>-
												<asp:textbox id="txtEndIP" runat="server" CssClass="text" Width="104px"></asp:textbox></td>
										</TR>
										<TR>
											<TD style="WIDTH: 161px; HEIGHT: 68px" align="right" rowSpan="2">
												<p><FONT face="����"><asp:checkbox id="chkRegistUser" runat="server" Text="�����ʻ�ע�᣺"></asp:checkbox></FONT></p>
											</TD>
											<TD width="128" height="68" rowspan="2" style="WIDTH: 128px">
												<p><FONT face="����">
														<asp:checkbox id="chkRegistManag" runat="server" Text="����ע������ʻ�"></asp:checkbox></FONT></p>
											</TD>
											<TD width="223" height="34">
												<ASP:RADIOBUTTON id="rbRegistDefine1" runat="server" Text="ע���������Ч" Checked="True" GroupName="RegistDefine"></ASP:RADIOBUTTON></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 34px" width="223">
												<P><FONT face="����">
														<ASP:RADIOBUTTON id="rbRegistDefine2" runat="server" Text="ע����˺���Ч" GroupName="RegistDefine"></ASP:RADIOBUTTON></FONT></P>
											</TD>
										</TR>
										<tr height="30">
											<td align="center" width="518" colSpan="3" height="23"><asp:Button id="ButInput" runat="server" CssClass="button" Text="�� ��" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onClick="window.location='../MainFrm.aspx';" type="button" value="�� ��"
										name="button1"></td>
										</tr>
						</table>
						<!--���ݽ���-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
