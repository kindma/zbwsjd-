<%@ Page language="c#" Inherits="EasyExam.SystemSet.ManagTestTypeList" CodeFile="ManagTestTypeList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��������</title>
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
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24">
									<font style="FONT-SIZE: 16px" face="����" color="#ffffff">��������</font></td>
							</tr>
							<tr>
								<td height="23">&nbsp;<FONT face="����">�������ƣ�<asp:textbox id="txtTestTypeName" runat="server" CssClass="text" Width="74px"></asp:textbox>&nbsp;�������ͣ�<asp:dropdownlist id="DDLBaseTestType" runat="server" Width="68px">
											<asp:ListItem Value="��ѡ��">��ѡ��</asp:ListItem>
											<asp:ListItem Value="��ѡ��">��ѡ��</asp:ListItem>
											<asp:ListItem Value="�ж���">�ж���</asp:ListItem>
											<asp:ListItem Value="�����">�����</asp:ListItem>
											<asp:ListItem Value="�ʴ���">�ʴ���</asp:ListItem>
											<asp:ListItem Value="������">������</asp:ListItem>
											<asp:ListItem Value="������">������</asp:ListItem>
											<asp:ListItem Value="������">������</asp:ListItem>
										</asp:dropdownlist>
										<asp:button id="ButNewDept" runat="server" CssClass="button" Text="���" onclick="ButNewDept_Click"></asp:button>&nbsp;
										<asp:button id="ButQuery" runat="server" CssClass="button" Text="�� ѯ" onclick="ButQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">����</asp:label></FONT></td>
							</tr>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridTestType" runat="server" Width="100%" PageSize="15" CellPadding="0"
										BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle Height="32px" HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="TestTypeID" ReadOnly="True" HeaderText="TestTypeID"></asp:BoundColumn>
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
											<asp:BoundColumn DataField="TestTypeName" SortExpression="TestTypeName" HeaderText="��������"></asp:BoundColumn>
											<asp:TemplateColumn SortExpression="BaseTestType" HeaderText="��������">
												<ItemTemplate>
													<asp:Label id="labBaseTestType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BaseTestType") %>'>
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:DropDownList id="DDLTestType" runat="server" Width="68px"></asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="ʹ��״̬">
												<ItemTemplate>
													<asp:Label id="labUseState" runat="server">ʹ��״̬</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="����" HeaderText="�༭" CancelText="ȡ��" EditText="�༭">
												<HeaderStyle Width="60px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn HeaderText="ɾ��">
												<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="ɾ��">ɾ��</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="BaseTestType" ReadOnly="True" HeaderText="BaseTestType"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="����">����<asp:label id="LabelRecord" runat="server" Font-Names="����" Font-Bold="True" ForeColor="Blue">0</asp:label>����¼&nbsp;<asp:label id="LabelCountPage" runat="server" Font-Names="����" Font-Bold="True" ForeColor="Blue">0</asp:label>ҳ&nbsp;��ǰ�ǵ�<asp:label id="LabelCurrentPage" runat="server" Font-Names="����" Font-Bold="True" ForeColor="Blue">0</asp:label>ҳ&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="����"><asp:button id="ButDelete" runat="server" CssClass="button" Text="ɾ������" onclick="ButDelete_Click"></asp:button></FONT></td>
							</tr>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
