<%@ Page language="c#" Inherits="EasyExam.GradeManag.ManagGrade" CodeFile="ManagGrade.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�ɼ�����</title>
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
					<td height="10"></td>
				</tr>
				<tr>
					<td valign="top">
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff"><asp:label id="labPaperType" runat="server" ForeColor="White"></asp:label>�ɼ�����</font></td>
							</tr>
							<tr>
								<td height="32">&nbsp;�Ծ����ƣ�<asp:textbox id="txtPaperName" runat="server" Width="152px" CssClass="text"></asp:textbox>&nbsp; 
									���ڣ���
									<asp:textbox id="txtStartTime" onclick="jcomOpenCalender('txtStartTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox></FONT><A onClick="jcomOpenCalender('txtStartTime');" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A>��
									<asp:textbox id="txtEndTime" onclick="jcomOpenCalender('txtEndTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox></FONT><A onClick="jcomOpenCalender('txtEndTime');" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A>&nbsp;
									<asp:button id="ButQuery" runat="server" CssClass="button" Text="�� ѯ" onclick="ButQuery_Click"></asp:button>
									<asp:label id="LabCondition" runat="server" Visible="False">����</asp:label></td>
							</tr>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridPaper" runat="server" Width="100%" PageSize="15" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle Height="32px" HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="PaperID" ReadOnly="True" HeaderText="PaperID">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="���">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="�Ծ�����"></asp:BoundColumn>
											<asp:BoundColumn DataField="ProduceWay" SortExpression="ProduceWay" HeaderText="���ⷽʽ"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="��Чʱ��">
												<ItemTemplate>
													<asp:Label id="labAvaiTime" runat="server">��Чʱ��</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PaperMark" SortExpression="PaperMark" HeaderText="�ܷ�"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="������"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="ͳ��">
												<HeaderStyle Width="173px"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButAverage" runat="server" Text="ƽ����" CommandName="Average" CausesValidation="false">ƽ����</asp:LinkButton>
													<asp:LinkButton id="LinkButGrade" runat="server" Text="�ɼ�" CommandName="Grade" CausesValidation="false">�ɼ�</asp:LinkButton>
													<asp:LinkButton id="LinkButLore" runat="server" Text="֪ʶ��" CommandName="Lore" CausesValidation="false">֪ʶ��</asp:LinkButton>
													<asp:LinkButton id="LinkButTestType" runat="server" Text="����" CommandName="TestType" CausesValidation="false">����</asp:LinkButton>
													<asp:LinkButton id="LinkButTest" runat="server" Text="����" CommandName="Test" CausesValidation="false">����</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="��ʼʱ��"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ManagerAccount" ReadOnly="True" HeaderText="������Ա"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="����">����<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>����¼&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>ҳ&nbsp;��ǰ�ǵ�<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>ҳ&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
							</TR>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
