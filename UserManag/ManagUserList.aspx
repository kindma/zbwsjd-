<%@ Page language="c#" Inherits="EasyExam.UserManag.ManagUserList" CodeFile="ManagUserList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ʻ�����</title>
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
					<td height="10"><FONT face="����"></FONT></td>
				</tr>
				<tr>
					<td valign="top">
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">�ʻ�����</font></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px">&nbsp; ���ţ�
									<asp:dropdownlist id="DDLDept" runat="server" Width="78px"></asp:dropdownlist>�ʺţ�
									<asp:textbox id="txtLoginID" runat="server" Width="100px" CssClass="text"></asp:textbox>������
									<asp:textbox id="txtUserName" runat="server" Width="100px" CssClass="text"></asp:textbox>&nbsp;<asp:button id="BtnQuery" runat="server" CssClass="button" Text="�� ѯ" onclick="btnQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">����</asp:label></td>
							</tr>
							<tr>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridUser" runat="server" Width="100%" PageSize="15" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" ForeColor="Transparent">
										<ItemStyle Height="32px"  ForeColor="Black"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" BorderColor="White" CssClass="HeadRow"
											BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="UserID" HeaderText="UserID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="&lt;input type='checkbox' id='chkSelectAll' name='chkSelectAll' onclick='jcomSelectAllRecords()' title='ȫѡ��ȫ��ȡ��' &gt;">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<INPUT id=chkSelect type=checkbox value='<%#LinNum++%>' name=chkSelect>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="���">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LoginID" SortExpression="LoginID" HeaderText="�ʺ�"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="����"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserSex" SortExpression="UserSex" HeaderText="�Ա�">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DeptName" SortExpression="DeptName" HeaderText="����"></asp:BoundColumn>
											<asp:BoundColumn DataField="JobName" SortExpression="JobName" HeaderText="ְ��"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserType" SortExpression="UserType" HeaderText="����"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserState" SortExpression="UserState" HeaderText="״̬">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="������"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="��������"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="����">
												<HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButEditUser" runat="server">�޸�</asp:LinkButton>&nbsp;
													<asp:LinkButton id="LinkButDel" runat="server" Text="ɾ��" CommandName="Delete" CausesValidation="false">ɾ��</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="����">����<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>����¼&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>ҳ&nbsp;��ǰ�ǵ�<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>ҳ&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="����">
										<asp:button id="ButNewOneUser" runat="server" CssClass="button" Text="�½��ʻ�"></asp:button>&nbsp;
										<asp:button id="ButDelete" runat="server" CssClass="button" Text="ɾ���ʻ�" onclick="butDelete_Click"></asp:button>&nbsp;
										<asp:button id="ButLock" runat="server" CssClass="button" Text="�����ʻ�" onclick="butLock_Click"></asp:button>&nbsp;
										<asp:button id="ButUnLock" runat="server" CssClass="button" Text="�����ʻ�" onclick="ButUnLock_Click"></asp:button>&nbsp;
										<asp:button id="ButClearPwd" runat="server" CssClass="button" Text="�����ÿ�" onclick="ButClearPwd_Click"></asp:button>&nbsp;
										<asp:button id="ButDelAnswer" runat="server" CssClass="button" Text="ɾ�����" onclick="ButDelAnswer_Click"></asp:button>&nbsp;
										<asp:button id="ButExport" runat="server" CssClass="button" Text="�����ʻ�" onclick="ButExport_Click"></asp:button></FONT></td>
							</tr>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
