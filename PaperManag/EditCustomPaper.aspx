<%@ Page language="c#" Inherits="EasyExam.PaperManag.EditCustomPaper" CodeFile="EditCustomPaper.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�޸��ֹ����</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
		<!-- -->
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="300" cellSpacing="0" cellPadding="0" width="680" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--���ݿ�ʼ-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="300" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24">
									<font face="����" style="FONT-SIZE: 16px" color="#ffffff">�޸��ֹ����</font></td>
							</tr>
							<tr>
								<td vAlign="top" align="center">
									<TABLE id="Table1" style="BORDER-COLLAPSE: collapse" borderColor="#dadada" cellSpacing="0"
										borderColorDark="#dadada" cellPadding="0" width="680" borderColorLight="#dadada" border="0">
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" style="HEIGHT: 20px"><STRONG>��������</STRONG></td>
										</tr>
										<TR>
											<td style="WIDTH: 67px" align="right">�Ծ����ƣ�</td>
											<td width="483" colSpan="3"><asp:textbox id="txtPaperName" runat="server" CssClass="text" Width="402px" MaxLength="50"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px; HEIGHT: 17px" align="right">�Ծ����ͣ�</td>
											<td style="HEIGHT: 17px" width="198"><asp:dropdownlist id="DDLPaperType" runat="server" Width="80px">
													<asp:ListItem Value="1">����</asp:ListItem>
													<asp:ListItem Value="2">��ҵ</asp:ListItem>
												</asp:dropdownlist></td>
											<td style="HEIGHT: 17px" align="right" width="62">���ⷽʽ��</td>
											<td style="HEIGHT: 17px" width="223"><asp:dropdownlist id="DDLProduceWay" runat="server" Width="80px">
													<asp:ListItem Value="1">����̶�</asp:ListItem>
													<asp:ListItem Value="2">�������</asp:ListItem>
												</asp:dropdownlist></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px; HEIGHT: 16px" align="right">��ʾģʽ��</td>
											<td style="HEIGHT: 16px" width="198"><FONT face="����"><asp:dropdownlist id="DDLShowModal" runat="server" Width="80px">
														<asp:ListItem Value="2">����ģʽ</asp:ListItem>
														<asp:ListItem Value="1">����ģʽ</asp:ListItem>
													</asp:dropdownlist></FONT></td>
											<td style="HEIGHT: 16px" align="right" width="62">����ʱ�䣺</td>
											<td style="HEIGHT: 16px" width="191"><FONT face="����"><asp:textbox id="txtExamTime" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox>����</FONT></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px" align="right">��ʼʱ�䣺</td>
											<td width="198"><FONT face="����"><asp:textbox id="txtStartTime" onclick="jcomSelectDateTime('txtStartTime',0);" runat="server"
														CssClass="text" Width="139px" MaxLength="50"></asp:textbox><A onclick="jcomSelectDateTime('txtStartTime',0);" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A></FONT></td>
											<td align="right" width="62">����ʱ�䣺</td>
											<td width="191"><asp:textbox id="txtEndTime" onclick="jcomSelectDateTime('txtEndTime',0);" runat="server" CssClass="text"
													Width="139px" MaxLength="50"></asp:textbox><A onclick="jcomSelectDateTime('txtEndTime',0);" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A></td>
										</TR>
										<TR>
											<TD style="WIDTH: 67px; HEIGHT: 15px" align="right">�Ծ��ܷ֣�</TD>
											<TD style="HEIGHT: 15px" width="198"><asp:textbox id="txtPaperMark" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox></TD>
											<TD style="HEIGHT: 15px" align="right" width="62">ͨ��������</TD>
											<TD style="HEIGHT: 15px" width="191"><FONT face="����"><asp:textbox id="txtPassMark" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox></FONT></TD>
										</TR>
										<tr>
											<TD style="WIDTH: 67px; HEIGHT: 15px" align="right" rowSpan="2">�������壺</TD>
											<TD style="HEIGHT: 19px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbMarkDefine1" runat="server" Checked="True" Text="ʹ�������������������ܷ�����Ϊ�Ծ��ܷ�" GroupName="MarkDefine"></ASP:RADIOBUTTON></TD>
										</tr>
										<tr>
											<TD style="HEIGHT: 15px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbMarkDefine2" runat="server" Text="������������������������ָ����������" GroupName="MarkDefine"></ASP:RADIOBUTTON></TD>
										</tr>
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" height="20"><STRONG>�Ծ�ѡ��</STRONG></td>
										</tr>
										<tr>
											<td width="265" colSpan="2"><asp:checkbox id="chkRepeatExam" runat="server" Text="�����βμӿ���"></asp:checkbox></td>
											<td width="253" colSpan="2"><FONT face="����"><asp:checkbox id="chkFillAutoGrade" runat="server" Text="��������������Զ�����"></asp:checkbox></FONT></td>
										</tr>
										<tr>
											<td width="265" colSpan="2"><asp:checkbox id="chkSeeResult" runat="server" Text="����鿴������"></asp:checkbox></td>
											<td width="253" colSpan="2"><FONT face="����"><asp:checkbox id="chkAutoSave" runat="server" Text="�����Զ�������"></asp:checkbox>&nbsp;���ʱ�䣺<asp:textbox id="txtSaveTime" runat="server" CssClass="text" Width="24px" MaxLength="50">5</asp:textbox>����</FONT>
											</td>
										</tr>
										<TR bgColor="#ebf5fa" height="16">
											<TD align="center" colSpan="4" height="20"><STRONG>�������</STRONG></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="4"><FONT face="����"><asp:button id="ButAddPolicy" runat="server" CssClass="text" Text="��Ӳ���" onclick="ButAddPolicy_Click"></asp:button></FONT></TD>
										</TR>
										<TR>
											<td colSpan="4"><asp:datagrid id="DataGridPolicy" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="1px"
													CellPadding="0" PageSize="5" Height="6px">
													<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="PaperPolicyID" ReadOnly="True" HeaderText="PaperPolicyID"></asp:BoundColumn>
														<asp:BoundColumn DataField="SubjectName" HeaderText="��Ŀ����"></asp:BoundColumn>
														<asp:BoundColumn DataField="LoreName" HeaderText="֪ʶ��"></asp:BoundColumn>
														<asp:BoundColumn DataField="TestTypeName" HeaderText="��������"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="����ID">
															<ItemTemplate>
																<asp:textbox id="txtSelectTest" runat="server" CssClass="text" Width="180px" MaxLength="800"
																	ReadOnly="True"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="����">
															<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:LinkButton id="LinkButEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="�޸�">�޸�</asp:LinkButton>
																<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="ɾ��">ɾ��</asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn Visible="False" DataField="SubjectID" ReadOnly="True" HeaderText="SubjectID"></asp:BoundColumn>
														<asp:BoundColumn Visible="False" DataField="LoreID" ReadOnly="True" HeaderText="LoreID"></asp:BoundColumn>
														<asp:BoundColumn Visible="False" DataField="TestTypeID" ReadOnly="True" HeaderText="TestTypeID"></asp:BoundColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</TR>
										<TR bgColor="#ebf5fa" height="16">
											<TD align="center" colSpan="4" height="20"><STRONG>���ͱ���</STRONG></TD>
										</TR>
										<TR>
											<td colSpan="4"><asp:datagrid id="DataGridTestType" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="1px"
													CellPadding="0" PageSize="5" Height="2px">
													<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="PaperTestTypeID" ReadOnly="True" HeaderText="PaperTestTypeID"></asp:BoundColumn>
														<asp:BoundColumn DataField="TestTypeName" HeaderText="��������"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="���ͱ���">
															<ItemTemplate>
																<asp:textbox id="txtTestTypeTitle" runat="server" CssClass="text" Width="120px" MaxLength="50"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="ÿ�����">
															<ItemTemplate>
																<asp:textbox id="txtTestTypeMark" runat="server" CssClass="text" Width="40px" MaxLength="50"
																	ToolTip="ֻ�԰����Ͷ��������Ч"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="TestAmount" HeaderText="����"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="˳��">
															<HeaderStyle Width="60px"></HeaderStyle>
															<ItemTemplate>
																<asp:LinkButton id="LinkButMoveUp" runat="server" Text="����" CommandName="MoveUp" CausesValidation="false">����</asp:LinkButton>
																<asp:LinkButton id="LinkButMoveDown" runat="server" Text="����" CommandName="MoveDown" CausesValidation="false">����</asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn Visible="False" DataField="BaseTestType" ReadOnly="True" HeaderText="BaseTestType"></asp:BoundColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</TR>
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" height="20"><strong>��Ա����</strong></td>
										</tr>
										<tr>
											<td style="HEIGHT: 23px" align="right" width="67"><FONT face="����">�ο���Ա��</FONT></td>
											<td style="HEIGHT: 23px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbAllAccount" runat="server" Checked="True" Text="�����ʺ�" GroupName="SelectExam"></ASP:RADIOBUTTON><FONT face="����">&nbsp;</FONT>&nbsp;<ASP:RADIOBUTTON id="rbSelectAccount" runat="server" Text=" " GroupName="SelectExam"></ASP:RADIOBUTTON><asp:button id="ButSelectExam" runat="server" CssClass="text" Width="54px" Text="ѡ��..." ToolTip="ѡ��ο���Ա" onclick="ButSelectExam_Click"></asp:button><asp:textbox id="txtSelectExam" runat="server" CssClass="text" Width="270px" MaxLength="50"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 25px" align="right" width="67"><FONT face="����">������Ա��</FONT>
											</td>
											<td style="HEIGHT: 25px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbAllManagerAccount" runat="server" Checked="True" Text="���й���Ա" GroupName="SelectManager"></ASP:RADIOBUTTON><ASP:RADIOBUTTON id="rbSelectManagerAccount" runat="server" Text=" " GroupName="SelectManager"></ASP:RADIOBUTTON><asp:button id="ButSelectManager" runat="server" CssClass="text" Width="54px" Text="ѡ��..." ToolTip="ѡ��������Ա" onclick="ButSelectManager_Click"></asp:button><asp:textbox id="txtSelectManager" runat="server" CssClass="text" Width="270px" MaxLength="50"></asp:textbox></td>
										</tr>
										<tr height="30">
											<td align="center" width="580" colSpan="4" height="10"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px" align="center"><asp:Button id="ButInput" runat="server" CssClass="button" Text="�� ��" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="ȡ ��" name="button1"></td>
							</tr>
						</table>
						<!--���ݽ���-->
					</td>
				</tr>
			</table>
		</form>
		<div id="submitexam1" style="Z-INDEX: 120;                               ; LEFT: expression((document.body.clientWidth-300)/2);                               VISIBILITY: hidden;                               POSITION: absolute;                               ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table cellSpacing="1" cellPadding="0" width="300" bgColor="#666666" border="0">
				<tr>
					<td width="300" bgColor="#33cccc" height="50">
						<div align="center">����������Ժ�...</div>
					</td>
				</tr>
			</table>
		</div>
	</body>
</HTML>
