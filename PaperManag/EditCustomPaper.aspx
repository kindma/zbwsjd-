<%@ Page language="c#" Inherits="EasyExam.PaperManag.EditCustomPaper" CodeFile="EditCustomPaper.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>修改手工组卷</title>
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
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="300" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24">
									<font face="黑体" style="FONT-SIZE: 16px" color="#ffffff">修改手工组卷</font></td>
							</tr>
							<tr>
								<td vAlign="top" align="center">
									<TABLE id="Table1" style="BORDER-COLLAPSE: collapse" borderColor="#dadada" cellSpacing="0"
										borderColorDark="#dadada" cellPadding="0" width="680" borderColorLight="#dadada" border="0">
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" style="HEIGHT: 20px"><STRONG>基本属性</STRONG></td>
										</tr>
										<TR>
											<td style="WIDTH: 67px" align="right">试卷名称：</td>
											<td width="483" colSpan="3"><asp:textbox id="txtPaperName" runat="server" CssClass="text" Width="402px" MaxLength="50"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px; HEIGHT: 17px" align="right">试卷类型：</td>
											<td style="HEIGHT: 17px" width="198"><asp:dropdownlist id="DDLPaperType" runat="server" Width="80px">
													<asp:ListItem Value="1">考试</asp:ListItem>
													<asp:ListItem Value="2">作业</asp:ListItem>
												</asp:dropdownlist></td>
											<td style="HEIGHT: 17px" align="right" width="62">出题方式：</td>
											<td style="HEIGHT: 17px" width="223"><asp:dropdownlist id="DDLProduceWay" runat="server" Width="80px">
													<asp:ListItem Value="1">题序固定</asp:ListItem>
													<asp:ListItem Value="2">题序随机</asp:ListItem>
												</asp:dropdownlist></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px; HEIGHT: 16px" align="right">显示模式：</td>
											<td style="HEIGHT: 16px" width="198"><FONT face="宋体"><asp:dropdownlist id="DDLShowModal" runat="server" Width="80px">
														<asp:ListItem Value="2">逐题模式</asp:ListItem>
														<asp:ListItem Value="1">整卷模式</asp:ListItem>
													</asp:dropdownlist></FONT></td>
											<td style="HEIGHT: 16px" align="right" width="62">答题时间：</td>
											<td style="HEIGHT: 16px" width="191"><FONT face="宋体"><asp:textbox id="txtExamTime" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox>分钟</FONT></td>
										</TR>
										<TR>
											<td style="WIDTH: 67px" align="right">开始时间：</td>
											<td width="198"><FONT face="宋体"><asp:textbox id="txtStartTime" onclick="jcomSelectDateTime('txtStartTime',0);" runat="server"
														CssClass="text" Width="139px" MaxLength="50"></asp:textbox><A onclick="jcomSelectDateTime('txtStartTime',0);" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A></FONT></td>
											<td align="right" width="62">结束时间：</td>
											<td width="191"><asp:textbox id="txtEndTime" onclick="jcomSelectDateTime('txtEndTime',0);" runat="server" CssClass="text"
													Width="139px" MaxLength="50"></asp:textbox><A onclick="jcomSelectDateTime('txtEndTime',0);" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A></td>
										</TR>
										<TR>
											<TD style="WIDTH: 67px; HEIGHT: 15px" align="right">试卷总分：</TD>
											<TD style="HEIGHT: 15px" width="198"><asp:textbox id="txtPaperMark" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox></TD>
											<TD style="HEIGHT: 15px" align="right" width="62">通过分数：</TD>
											<TD style="HEIGHT: 15px" width="191"><FONT face="宋体"><asp:textbox id="txtPassMark" runat="server" CssClass="text" Width="40px" MaxLength="50"></asp:textbox></FONT></TD>
										</TR>
										<tr>
											<TD style="WIDTH: 67px; HEIGHT: 15px" align="right" rowSpan="2">分数定义：</TD>
											<TD style="HEIGHT: 19px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbMarkDefine1" runat="server" Checked="True" Text="使用题库中试题分数，将总分折算为试卷总分" GroupName="MarkDefine"></ASP:RADIOBUTTON></TD>
										</tr>
										<tr>
											<TD style="HEIGHT: 15px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbMarkDefine2" runat="server" Text="忽略题库中试题分数，按题型指定分数折算" GroupName="MarkDefine"></ASP:RADIOBUTTON></TD>
										</tr>
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" height="20"><STRONG>试卷选项</STRONG></td>
										</tr>
										<tr>
											<td width="265" colSpan="2"><asp:checkbox id="chkRepeatExam" runat="server" Text="允许多次参加考试"></asp:checkbox></td>
											<td width="253" colSpan="2"><FONT face="宋体"><asp:checkbox id="chkFillAutoGrade" runat="server" Text="允许填空类试题自动评卷"></asp:checkbox></FONT></td>
										</tr>
										<tr>
											<td width="265" colSpan="2"><asp:checkbox id="chkSeeResult" runat="server" Text="允许查看评卷结果"></asp:checkbox></td>
											<td width="253" colSpan="2"><FONT face="宋体"><asp:checkbox id="chkAutoSave" runat="server" Text="允许自动保存答卷"></asp:checkbox>&nbsp;间隔时间：<asp:textbox id="txtSaveTime" runat="server" CssClass="text" Width="24px" MaxLength="50">5</asp:textbox>分钟</FONT>
											</td>
										</tr>
										<TR bgColor="#ebf5fa" height="16">
											<TD align="center" colSpan="4" height="20"><STRONG>试题策略</STRONG></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="4"><FONT face="宋体"><asp:button id="ButAddPolicy" runat="server" CssClass="text" Text="添加策略" onclick="ButAddPolicy_Click"></asp:button></FONT></TD>
										</TR>
										<TR>
											<td colSpan="4"><asp:datagrid id="DataGridPolicy" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="1px"
													CellPadding="0" PageSize="5" Height="6px">
													<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="PaperPolicyID" ReadOnly="True" HeaderText="PaperPolicyID"></asp:BoundColumn>
														<asp:BoundColumn DataField="SubjectName" HeaderText="科目名称"></asp:BoundColumn>
														<asp:BoundColumn DataField="LoreName" HeaderText="知识点"></asp:BoundColumn>
														<asp:BoundColumn DataField="TestTypeName" HeaderText="题型名称"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="试题ID">
															<ItemTemplate>
																<asp:textbox id="txtSelectTest" runat="server" CssClass="text" Width="180px" MaxLength="800"
																	ReadOnly="True"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="操作">
															<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:LinkButton id="LinkButEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="修改">修改</asp:LinkButton>
																<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="删除">删除</asp:LinkButton>
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
											<TD align="center" colSpan="4" height="20"><STRONG>题型标题</STRONG></TD>
										</TR>
										<TR>
											<td colSpan="4"><asp:datagrid id="DataGridTestType" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="1px"
													CellPadding="0" PageSize="5" Height="2px">
													<HeaderStyle HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="PaperTestTypeID" ReadOnly="True" HeaderText="PaperTestTypeID"></asp:BoundColumn>
														<asp:BoundColumn DataField="TestTypeName" HeaderText="题型名称"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="题型标题">
															<ItemTemplate>
																<asp:textbox id="txtTestTypeTitle" runat="server" CssClass="text" Width="120px" MaxLength="50"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="每题分数">
															<ItemTemplate>
																<asp:textbox id="txtTestTypeMark" runat="server" CssClass="text" Width="40px" MaxLength="50"
																	ToolTip="只对按题型定义分数有效"></asp:textbox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="TestAmount" HeaderText="题量"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="顺序">
															<HeaderStyle Width="60px"></HeaderStyle>
															<ItemTemplate>
																<asp:LinkButton id="LinkButMoveUp" runat="server" Text="上移" CommandName="MoveUp" CausesValidation="false">上移</asp:LinkButton>
																<asp:LinkButton id="LinkButMoveDown" runat="server" Text="下移" CommandName="MoveDown" CausesValidation="false">下移</asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn Visible="False" DataField="BaseTestType" ReadOnly="True" HeaderText="BaseTestType"></asp:BoundColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></td>
										</TR>
										<tr bgColor="#ebf5fa" height="16">
											<td align="center" colSpan="4" height="20"><strong>人员安排</strong></td>
										</tr>
										<tr>
											<td style="HEIGHT: 23px" align="right" width="67"><FONT face="宋体">参考人员：</FONT></td>
											<td style="HEIGHT: 23px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbAllAccount" runat="server" Checked="True" Text="所有帐号" GroupName="SelectExam"></ASP:RADIOBUTTON><FONT face="宋体">&nbsp;</FONT>&nbsp;<ASP:RADIOBUTTON id="rbSelectAccount" runat="server" Text=" " GroupName="SelectExam"></ASP:RADIOBUTTON><asp:button id="ButSelectExam" runat="server" CssClass="text" Width="54px" Text="选择..." ToolTip="选择参考人员" onclick="ButSelectExam_Click"></asp:button><asp:textbox id="txtSelectExam" runat="server" CssClass="text" Width="270px" MaxLength="50"></asp:textbox></td>
										</tr>
										<tr>
											<td style="HEIGHT: 25px" align="right" width="67"><FONT face="宋体">评卷人员：</FONT>
											</td>
											<td style="HEIGHT: 25px" width="483" colSpan="3"><ASP:RADIOBUTTON id="rbAllManagerAccount" runat="server" Checked="True" Text="所有管理员" GroupName="SelectManager"></ASP:RADIOBUTTON><ASP:RADIOBUTTON id="rbSelectManagerAccount" runat="server" Text=" " GroupName="SelectManager"></ASP:RADIOBUTTON><asp:button id="ButSelectManager" runat="server" CssClass="text" Width="54px" Text="选择..." ToolTip="选择评卷人员" onclick="ButSelectManager_Click"></asp:button><asp:textbox id="txtSelectManager" runat="server" CssClass="text" Width="270px" MaxLength="50"></asp:textbox></td>
										</tr>
										<tr height="30">
											<td align="center" width="580" colSpan="4" height="10"></td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px" align="center"><asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="button1"></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
		<div id="submitexam1" style="Z-INDEX: 120;                               ; LEFT: expression((document.body.clientWidth-300)/2);                               VISIBILITY: hidden;                               POSITION: absolute;                               ; TOP: expression(document.body.scrollTop+(window.screen.availHeight-50)/2-20)">
			<table cellSpacing="1" cellPadding="0" width="300" bgColor="#666666" border="0">
				<tr>
					<td width="300" bgColor="#33cccc" height="50">
						<div align="center">正在组卷，请稍候...</div>
					</td>
				</tr>
			</table>
		</div>
	</body>
</HTML>
