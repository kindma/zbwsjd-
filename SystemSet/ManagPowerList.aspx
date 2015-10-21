<%@ Page language="c#" Inherits="EasyExam.SystemSet.ManagPowerList" CodeFile="ManagPowerList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>权限设置</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="540" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">权限设置</font></td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px">&nbsp;部门：<asp:dropdownlist id="DDLDept" runat="server" Width="78px"></asp:dropdownlist>
									帐号：<asp:textbox id="txtLoginID" runat="server" CssClass="text" Width="80px"></asp:textbox>
									姓名：<asp:textbox id="txtUserName" runat="server" Width="80px" CssClass="text"></asp:textbox>
									<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>&nbsp;
									<asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
							</tr>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridUser" runat="server" PageSize="15" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle Height="32px" HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="UserID" HeaderText="UserID"></asp:BoundColumn>
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
											<asp:TemplateColumn HeaderText="设置">
												<HeaderStyle HorizontalAlign="Center" Width="160px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButUser" runat="server" Text="评卷帐号" CommandName="User" CausesValidation="false">评卷帐号</asp:LinkButton>
													<asp:LinkButton id="LinkButTestType" runat="server" Text="评卷题型" CommandName="TestType" CausesValidation="false">评卷题型</asp:LinkButton>
													<asp:LinkButton id="LinkButMenu" runat="server" Text="角色菜单" CommandName="Menu" CausesValidation="false">角色菜单</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>条记录&nbsp;<asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;当前是第<asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
