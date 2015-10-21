<%@ Page language="c#" Inherits="EasyExam.PaperManag.EditCustomPolicy" CodeFile="EditCustomPolicy.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改手工策略</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="780" align="center" border="0">
				<tr>
					<td colSpan="3" height="25" align="center" rowSpan="1"><FONT face="宋体"><FONT style="FONT-SIZE: 16px" face="黑体" color="#0054a6">修改手工策略</FONT></FONT></td>
				</tr>
				<TR>
					<TD align="center" colSpan="3" height="22" rowSpan="1"><FONT face="宋体">科目名称：
							<asp:dropdownlist id="DDLSubjectName" runat="server" Width="92px" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDLSubjectName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist>知识点：
							<asp:dropdownlist id="DDLLoreName" runat="server" Width="92px" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDLLoreName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist>题型名称：
							<asp:dropdownlist id="DDLTestTypeName" runat="server" Width="92px" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDLTestTypeName_SelectedIndexChanged">
								<asp:ListItem Value="0">--请选择--</asp:ListItem>
							</asp:dropdownlist>
							<asp:Label id="LabCondition" runat="server" Visible="False">条件</asp:Label></FONT></TD>
				</TR>
				<tr>
					<td width="1" style="HEIGHT: 380px"></td>
					<td width="780" height="432" valign="top" style="HEIGHT: 432px" align="center">
						<!--内容-->
						<div align="center">
							<center>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="780" border="0" style="BORDER-COLLAPSE: collapse"
									bordercolor="#111111">
									<TR>
										<td align="center" width="780" vAlign="top"><asp:datagrid id="DataGridTest" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
												BorderWidth="1px" CellPadding="0" PageSize="15" AllowSorting="True">
												<ItemStyle Height="23px"></ItemStyle>
												<HeaderStyle HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
												<Columns>
													<asp:TemplateColumn HeaderText="选择">
														<HeaderStyle Width="30px"></HeaderStyle>
														<ItemTemplate>
															<asp:CheckBox id="CheckBoxSel" runat="server"></asp:CheckBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="RubricID" SortExpression="RubricID" HeaderText="试题ID"></asp:BoundColumn>
													<asp:BoundColumn DataField="SubjectName" SortExpression="SubjectName" HeaderText="科目名称"></asp:BoundColumn>
													<asp:BoundColumn DataField="LoreName" SortExpression="LoreName" HeaderText="知识点"></asp:BoundColumn>
													<asp:BoundColumn DataField="TestTypeName" SortExpression="TestTypeName" HeaderText="题型名称"></asp:BoundColumn>
													<asp:BoundColumn DataField="TestDiff" SortExpression="TestDiff" HeaderText="试题难度"></asp:BoundColumn>
													<asp:BoundColumn DataField="TestMark" SortExpression="TestMark" HeaderText="试题分数"></asp:BoundColumn>
													<asp:TemplateColumn SortExpression="TestContent" HeaderText="试题内容">
														<HeaderStyle Width="200px"></HeaderStyle>
														<ItemTemplate>
															<asp:Label id="labTestContent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TestContent") %>'>
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="创建人"></asp:BoundColumn>
													<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="创建日期"></asp:BoundColumn>
												</Columns>
												<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
											</asp:datagrid><FONT face="宋体">共有<asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>条记录&nbsp;<asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;当前是第<asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;</FONT>
											<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
											<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
											<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
											<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
									</TR>
								</TABLE>
								<CENTER></CENTER>
						</div>
						</CENTER>
					</td>
					<td width="1" style="HEIGHT: 371px"></td>
				</tr>
				<tr>
					<td colSpan="3" height="1" align="center"><FONT face="宋体">
							<asp:Button id="ButInput" runat="server" Text="提 交" CssClass="button" onclick="ButInput_Click"></asp:Button>&nbsp;
							<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button">
						</FONT>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
