<%@ Page Language="c#" Inherits="EasyExam.ProcessManag.AbsentUser" CodeFile="AbsentUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>缺考帐号</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
				<TR>
					<TD align="center" height="6"><FONT face="宋体"></FONT></TD>
				</TR>
				<tr>
					<td align="center"><FONT style="FONT-SIZE: 16px" face="黑体" color="#0054a6">【<asp:label id="labPaperName" runat="server" ForeColor="#0054a6"></asp:label>】缺考帐号</FONT></td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" bgColor="#0000ff" height="2"><FONT face="宋体"></FONT></td>
				</tr>
				<tr>
					<td align="center"><asp:datagrid id="DataGridUser" runat="server" PageSize="15" CellPadding="0" BorderWidth="1px"
							AllowPaging="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True">
							<ItemStyle Height="23px"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="UserID" ReadOnly="True" HeaderText="UserID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%#RowNum++%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LoginID" SortExpression="LoginID" HeaderText="帐号"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="姓名"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserSex" SortExpression="UserSex" HeaderText="性别"></asp:BoundColumn>
								<asp:BoundColumn DataField="Birthday" SortExpression="Birthday" HeaderText="出生年月"></asp:BoundColumn>
								<asp:BoundColumn DataField="DeptName" SortExpression="DeptName" HeaderText="部门"></asp:BoundColumn>
								<asp:BoundColumn DataField="JobName" SortExpression="JobName" HeaderText="职务"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserType" SortExpression="UserType" HeaderText="类型"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserState" SortExpression="UserState" HeaderText="状态"></asp:BoundColumn>
							</Columns>
							<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;</FONT>
						<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="ButExport" runat="server" CssClass="button" Text="导 出" onclick="ButExport_Click"></asp:button>&nbsp;
						<INPUT class="button" onclick="window.close();" type="button" value="关 闭" name="button1"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
