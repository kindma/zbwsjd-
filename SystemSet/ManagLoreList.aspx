<%@ Page language="c#" Inherits="EasyExam.SystemSet.ManagLoreList" CodeFile="ManagLoreList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>知识点设置</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="500" cellSpacing="0" cellPadding="0" width="560" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">【<asp:label id="labSubject" runat="server" ForeColor="White"></asp:label>】知识点设置</font></td>
							</tr>
							<tr>
								<td height="23">&nbsp;知识点名称：<asp:textbox id="txtLore" runat="server" Width="88px" CssClass="text"></asp:textbox>
									<asp:button id="ButNewDept" runat="server" CssClass="button" Text="添加" onclick="ButNewDept_Click"></asp:button>&nbsp;
									<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>&nbsp;
									<asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
							</tr>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridLore" runat="server" Width="100%" PageSize="15" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True">
										<ItemStyle Height="23px"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="LoreID" ReadOnly="True" HeaderText="LoreID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="&lt;input type='checkbox' id='chkSelectAll' name='chkSelectAll' onclick='jcomSelectAllRecords()' title='全选或全部取消' &gt;">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<INPUT id=chkSelect type=checkbox value='<%#LinNum++%>' name=chkSelect>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="序号">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LoreName" SortExpression="LoreName" HeaderText="知识点名称"></asp:BoundColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑">
												<HeaderStyle Width="60px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn HeaderText="删除">
												<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="删除">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="宋体">
										<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除选择" onclick="ButDelete_Click"></asp:button></FONT>&nbsp;&nbsp;<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button"></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
