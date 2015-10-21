<%@ Page language="c#" Inherits="EasyExam.PaperManag.EditRandPolicy" CodeFile="EditRandPolicy.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改随机策略</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<td align="center" colSpan="3" height="24" rowSpan="1"><FONT face="宋体"><FONT style="FONT-SIZE: 16px" face="黑体" color="#0054a6">修改随机策略</FONT></FONT></td>
				</tr>
				<TR>
					<TD align="center" colSpan="3" height="22" rowSpan="1"><FONT face="宋体">科目名称：
							<asp:dropdownlist id="DDLSubjectName" runat="server" AutoPostBack="True" Width="92px" Enabled="False" onselectedindexchanged="DDLSubjectName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist>知识点：
							<asp:dropdownlist id="DDLLoreName" runat="server" AutoPostBack="True" Width="92px" Enabled="False" onselectedindexchanged="DDLLoreName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist>题型名称：
							<asp:dropdownlist id="DDLTestTypeName" runat="server" AutoPostBack="True" Width="92px" Enabled="False" onselectedindexchanged="DDLTestTypeName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist><asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></FONT></TD>
				</TR>
				<tr>
					<td style="HEIGHT: 70px" width="1"></td>
					<td style="HEIGHT: 70px" vAlign="top" width="500" height="70">
						<!--内容-->
						<div align="center">
							<center>
								<TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
									cellPadding="0" width="500" border="0">
									<TR>
										<td vAlign="top" align="center" width="500"><asp:datagrid id="DataGridPolicy" runat="server" Width="500px" PageSize="5" CellPadding="0" BorderWidth="1px"
												AutoGenerateColumns="False">
												<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
												<Columns>
													<asp:BoundColumn DataField="TestCount" HeaderText="试题总数"></asp:BoundColumn>
													<asp:TemplateColumn HeaderText="易">
														<ItemTemplate>
															<asp:textbox id="txtTestDiff1" runat="server" CssClass="text" Width="24px" MaxLength="5" Text="0"></asp:textbox>
															<asp:Label id="labSplip1" runat="server">/</asp:Label>
															<asp:Label id="labTestDiff1" runat="server">00</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="较易">
														<ItemTemplate>
															<asp:textbox id="txtTestDiff2" runat="server" CssClass="text" Width="24px" MaxLength="5" Text="0"></asp:textbox>
															<asp:Label id="labSplip2" runat="server">/</asp:Label>
															<asp:Label id="labTestDiff2" runat="server">00</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="中等">
														<ItemTemplate>
															<asp:textbox id="txtTestDiff3" runat="server" CssClass="text" Width="24px" MaxLength="5" Text="0"></asp:textbox>
															<asp:Label id="labSplip3" runat="server">/</asp:Label>
															<asp:Label id="labTestDiff3" runat="server">00</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="较难">
														<ItemTemplate>
															<asp:textbox id="txtTestDiff4" runat="server" CssClass="text" Width="24px" MaxLength="5" Text="0"></asp:textbox>
															<asp:Label id="labSplip4" runat="server">/</asp:Label>
															<asp:Label id="labTestDiff4" runat="server">00</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="难">
														<ItemTemplate>
															<asp:textbox id="txtTestDiff5" runat="server" CssClass="text" Width="24px" MaxLength="5" Text="0"></asp:textbox>
															<asp:Label id="labSplip5" runat="server">/</asp:Label>
															<asp:Label id="labTestDiff5" runat="server">00</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
											</asp:datagrid><FONT face="宋体"></FONT></td>
									</TR>
								</TABLE>
								<CENTER><FONT face="宋体"></FONT></CENTER>
						</div>
						</CENTER></td>
					<td style="HEIGHT: 70px" width="1"></td>
				</tr>
				<tr>
					<td align="center" colSpan="3" height="1"><FONT face="宋体"><FONT face="宋体">
								<asp:button id="ButInput" runat="server" Text="提 交" CssClass="button" onclick="ButInput_Click"></asp:button>&nbsp;
								<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button"></FONT>
						</FONT>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
