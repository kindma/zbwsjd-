<%@ Page language="c#" Inherits="EasyExam.UserManag.ImportUser" CodeFile="ImportUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>导入批量帐户</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="224" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="184" cellSpacing="0"
							cellPadding="0" width="100%" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">导入帐户</font></td>
							</tr>
							<tr>
								<td>
									<TABLE id="Table2" height="150" cellSpacing="0" cellPadding="0" width="500" border="0"
										style="BORDER-COLLAPSE: collapse" bordercolor="#111111">
										<tr>
											<td align="center" colSpan="2" height="23">
											</td>
										</tr>
										<tr>
											<td align="center" colSpan="2" height="23"><FONT face="宋体">文件：</FONT><INPUT class="text" id="UpLoadFile" style="WIDTH: 221px; HEIGHT: 20px" type="file" size="17"
													name="File1" runat="server"><FONT face="宋体">&nbsp;</FONT><asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
												<INPUT class="button" onClick="window.location='../MainFrm.aspx';" type="button" value="返 回"
													name="button1"></td>
										</tr>
										<tr>
											<td align="center" colSpan="2" height="23">导入模板下载：<FONT face="宋体"><A href="../TempletFiles/ImportUser.xls">ImportUser.xls</A></FONT></td>
										</tr>
										<TR>
											<TD align="center" colSpan="2" height="23"><FONT face="宋体"><asp:label id="LabelMessage" runat="server" ForeColor="Red"></asp:label></FONT></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2">
												<asp:DataGrid id="DataGridErr" runat="server" Width="486px" Visible="False" HorizontalAlign="Center"
													Height="62px" AutoGenerateColumns="False">
													<ItemStyle Height="23px"></ItemStyle>
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow"
														BackColor="SteelBlue"></HeaderStyle>
													<Columns>
														<asp:BoundColumn DataField="ErrID" HeaderText="序号">
															<HeaderStyle Width="60px"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="RowNum" HeaderText="行号">
															<HeaderStyle Width="60px"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="ErrInfo" HeaderText="错误描述"></asp:BoundColumn>
													</Columns>
												</asp:DataGrid></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
