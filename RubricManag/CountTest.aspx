<%@ Page language="c#" Inherits="EasyExam.RubricManag.CountTest" CodeFile="CountTest.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���ͳ��</title>
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
									<font face="����" style="FONT-SIZE: 16px" color="#ffffff">���ͳ��</font></td>
							</tr>
							<tr>
								<td vAlign="top" align="center">
									<asp:datagrid id="DataGridCount" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SubjectID" ReadOnly="True" HeaderText="SubjectID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="���">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SubjectName" SortExpression="SubjectName" HeaderText="��Ŀ����"></asp:BoundColumn>
											<asp:BoundColumn DataField="TestCount" SortExpression="TestCount" HeaderText="��������"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="����ֲ�">
												<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButCountTestDist" runat="server" CausesValidation="false" CommandName="Edit"
														Text="����ֲ�">����ֲ�</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="����">����<asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>����¼&nbsp;<asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>ҳ&nbsp;��ǰ�ǵ�<asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>ҳ&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
							</tr>
						</table>
						<!--���ݽ���-->
					</td>
				</tr>
		  </table>
		</form>
	</body>
</HTML>
