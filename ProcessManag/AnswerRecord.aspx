<%@ Page language="c#" Inherits="EasyExam.ProcessManag.AnswerRecord" CodeFile="AnswerRecord.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����¼</title>
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
			<table height="660" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="660" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">����¼</font></td>
							</tr>
							<tr>
								<td align="center" height="23">&nbsp;�Ծ����ƣ�<asp:label id="labPaperName" runat="server"></asp:label>&nbsp; 
									�Ծ��ܷ֣�<asp:label id="labPaperMark" runat="server"></asp:label>&nbsp;ͨ��������<asp:label id="labPassMark" runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD style="HEIGHT: 23px" align="center">&nbsp;<SPAN id="lblMessage"><FONT face="����">���ţ�
											<asp:dropdownlist id="DDLDept" runat="server" Width="78px"></asp:dropdownlist>�ʺţ�
											<asp:textbox id="txtLoginID" runat="server" CssClass="text" Width="76px"></asp:textbox>������
											<asp:textbox id="txtUserName" runat="server" CssClass="text" Width="76px"></asp:textbox>���״̬��
											<asp:dropdownlist id="DDLExamState" runat="server" Width="64px">
												<asp:ListItem Value="-1">-ȫ��-</asp:ListItem>
												<asp:ListItem Value="0">�����</asp:ListItem>
												<asp:ListItem Value="1">�ѽ���</asp:ListItem>
											</asp:dropdownlist>����״̬��
											<asp:dropdownlist id="DDLJudgeState" runat="server" Width="64px">
												<asp:ListItem Value="-1">-ȫ��-</asp:ListItem>
												<asp:ListItem Value="0">δ����</asp:ListItem>
												<asp:ListItem Value="1">������</asp:ListItem>
											</asp:dropdownlist>
											<asp:button id="ButQuery" runat="server" CssClass="button" Text="�� ѯ" onclick="ButQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">����</asp:label></FONT></SPAN></TD>
							</TR>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridScore" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" AllowSorting="True">
										<ItemStyle Height="23px"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="UserScoreID" ReadOnly="True" HeaderText="UserScoreID">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
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
											<asp:BoundColumn DataField="TotalMark" SortExpression="TotalMark" HeaderText="�ɼ�"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="���ʱ��">
												<ItemTemplate>
													<asp:Label id="labExamTime" runat="server">���ʱ��</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LoginIP" SortExpression="LoginIP" ReadOnly="True" HeaderText="��¼IP"></asp:BoundColumn>
											<asp:BoundColumn DataField="ExamState" SortExpression="ExamState" HeaderText="���״̬"></asp:BoundColumn>
											<asp:BoundColumn DataField="JudgeLoginID" SortExpression="JudgeLoginID" HeaderText="������"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="����">
												<HeaderStyle Width="116px"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButAnswer" runat="server" Text="���" CommandName="Answer" CausesValidation="false">���</asp:LinkButton>
													<asp:LinkButton id="LinkButStatis" runat="server" Text="ͳ��" CommandName="Statis" CausesValidation="false">ͳ��</asp:LinkButton>
													<asp:LinkButton id="LinkButJudge" runat="server" Text="����" CommandName="Judge" CausesValidation="false">����</asp:LinkButton>
													<asp:LinkButton id="LinkButDelete" runat="server" Text="ɾ��" CommandName="Delete" CausesValidation="false">ɾ��</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="��ʼʱ��"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ImpScore" ReadOnly="True" HeaderText="�͹������"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="����">����<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>����¼&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>ҳ&nbsp;��ǰ�ǵ�<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>ҳ&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center">
									<asp:button id="ButDelete" runat="server" CssClass="button" Text="ɾ�����" onclick="ButDelete_Click"></asp:button>&nbsp;
									<asp:button id="ButRefresh" runat="server" CssClass="button" Text="ˢ�´��"></asp:button>&nbsp;
									<asp:button id="ButAbsentUser" runat="server" CssClass="button" Text="ȱ���ʻ�"></asp:button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="�� ��" name="Button"></td>
							</tr>
						</table>
						<!--���ݽ���--></td>
				</tr>
			</table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
