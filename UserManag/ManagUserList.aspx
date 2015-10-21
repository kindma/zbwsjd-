<%@ Page language="c#" Inherits="EasyExam.UserManag.ManagUserList" CodeFile="ManagUserList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>帐户管理</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
		<script language="JavaScript">
			function RefreshForm()
			{
				document.all("hidcommand").value="RefreshForm";
				Form1.submit();
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="540" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="10"><FONT face="宋体"></FONT></td>
				</tr>
				<tr>
					<td valign="top">
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">帐户管理</font></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px">&nbsp; 部门：
									<asp:dropdownlist id="DDLDept" runat="server" Width="78px"></asp:dropdownlist>帐号：
									<asp:textbox id="txtLoginID" runat="server" Width="100px" CssClass="text"></asp:textbox>姓名：
									<asp:textbox id="txtUserName" runat="server" Width="100px" CssClass="text"></asp:textbox>&nbsp;<asp:button id="BtnQuery" runat="server" CssClass="button" Text="查 询" onclick="btnQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
							</tr>
							<tr>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridUser" runat="server" Width="100%" PageSize="15" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" ForeColor="Transparent">
										<ItemStyle Height="32px"  ForeColor="Black"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" BorderColor="White" CssClass="HeadRow"
											BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="UserID" HeaderText="UserID"></asp:BoundColumn>
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
											<asp:BoundColumn DataField="LoginID" SortExpression="LoginID" HeaderText="帐号"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="姓名"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserSex" SortExpression="UserSex" HeaderText="性别">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DeptName" SortExpression="DeptName" HeaderText="部门"></asp:BoundColumn>
											<asp:BoundColumn DataField="JobName" SortExpression="JobName" HeaderText="职务"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserType" SortExpression="UserType" HeaderText="类型"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserState" SortExpression="UserState" HeaderText="状态">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="创建人"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="创建日期"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="操作">
												<HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButEditUser" runat="server">修改</asp:LinkButton>&nbsp;
													<asp:LinkButton id="LinkButDel" runat="server" Text="删除" CommandName="Delete" CausesValidation="false">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="宋体">
										<asp:button id="ButNewOneUser" runat="server" CssClass="button" Text="新建帐户"></asp:button>&nbsp;
										<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除帐户" onclick="butDelete_Click"></asp:button>&nbsp;
										<asp:button id="ButLock" runat="server" CssClass="button" Text="禁用帐户" onclick="butLock_Click"></asp:button>&nbsp;
										<asp:button id="ButUnLock" runat="server" CssClass="button" Text="启用帐户" onclick="ButUnLock_Click"></asp:button>&nbsp;
										<asp:button id="ButClearPwd" runat="server" CssClass="button" Text="密码置空" onclick="ButClearPwd_Click"></asp:button>&nbsp;
										<asp:button id="ButDelAnswer" runat="server" CssClass="button" Text="删除答卷" onclick="ButDelAnswer_Click"></asp:button>&nbsp;
										<asp:button id="ButExport" runat="server" CssClass="button" Text="导出帐户" onclick="ButExport_Click"></asp:button></FONT></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
