<%@ Page Language="c#" Inherits="EasyExam.ProcessManag.AbsentUser" CodeFile="AbsentUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ȱ���ʺ�</title>
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
					<TD align="center" height="6"><FONT face="����"></FONT></TD>
				</TR>
				<tr>
					<td align="center"><FONT style="FONT-SIZE: 16px" face="����" color="#0054a6">��<asp:label id="labPaperName" runat="server" ForeColor="#0054a6"></asp:label>��ȱ���ʺ�</FONT></td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" bgColor="#0000ff" height="2"><FONT face="����"></FONT></td>
				</tr>
				<tr>
					<td align="center"><asp:datagrid id="DataGridUser" runat="server" PageSize="15" CellPadding="0" BorderWidth="1px"
							AllowPaging="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True">
							<ItemStyle Height="23px"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="UserID" ReadOnly="True" HeaderText="UserID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="���">
									<ItemTemplate>
										<%#RowNum++%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LoginID" SortExpression="LoginID" HeaderText="�ʺ�"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserSex" SortExpression="UserSex" HeaderText="�Ա�"></asp:BoundColumn>
								<asp:BoundColumn DataField="Birthday" SortExpression="Birthday" HeaderText="��������"></asp:BoundColumn>
								<asp:BoundColumn DataField="DeptName" SortExpression="DeptName" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="JobName" SortExpression="JobName" HeaderText="ְ��"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserType" SortExpression="UserType" HeaderText="����"></asp:BoundColumn>
								<asp:BoundColumn DataField="UserState" SortExpression="UserState" HeaderText="״̬"></asp:BoundColumn>
							</Columns>
							<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><FONT face="����">����<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>����¼&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>ҳ&nbsp;��ǰ�ǵ�<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>ҳ&nbsp;</FONT>
						<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
						<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="ButExport" runat="server" CssClass="button" Text="�� ��" onclick="ButExport_Click"></asp:button>&nbsp;
						<INPUT class="button" onclick="window.close();" type="button" value="�� ��" name="button1"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
