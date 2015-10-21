<%@ Page language="c#" Inherits="EasyExam.ProcessManag.AnswerRecord" CodeFile="AnswerRecord.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>答卷记录</title>
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
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="660" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">答卷记录</font></td>
							</tr>
							<tr>
								<td align="center" height="23">&nbsp;试卷名称：<asp:label id="labPaperName" runat="server"></asp:label>&nbsp; 
									试卷总分：<asp:label id="labPaperMark" runat="server"></asp:label>&nbsp;通过分数：<asp:label id="labPassMark" runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD style="HEIGHT: 23px" align="center">&nbsp;<SPAN id="lblMessage"><FONT face="宋体">部门：
											<asp:dropdownlist id="DDLDept" runat="server" Width="78px"></asp:dropdownlist>帐号：
											<asp:textbox id="txtLoginID" runat="server" CssClass="text" Width="76px"></asp:textbox>姓名：
											<asp:textbox id="txtUserName" runat="server" CssClass="text" Width="76px"></asp:textbox>答卷状态：
											<asp:dropdownlist id="DDLExamState" runat="server" Width="64px">
												<asp:ListItem Value="-1">-全部-</asp:ListItem>
												<asp:ListItem Value="0">答卷中</asp:ListItem>
												<asp:ListItem Value="1">已交卷</asp:ListItem>
											</asp:dropdownlist>评卷状态：
											<asp:dropdownlist id="DDLJudgeState" runat="server" Width="64px">
												<asp:ListItem Value="-1">-全部-</asp:ListItem>
												<asp:ListItem Value="0">未评卷</asp:ListItem>
												<asp:ListItem Value="1">已评卷</asp:ListItem>
											</asp:dropdownlist>
											<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></FONT></SPAN></TD>
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
											<asp:TemplateColumn HeaderText="&lt;input type='checkbox' id='chkSelectAll' name='chkSelectAll' onclick='jcomSelectAllRecords()' title='全选或全部取消' &gt;">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<INPUT id=chkSelect type=checkbox value='<%#LinNum++%>' name=chkSelect>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="序号">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LoginID" SortExpression="LoginID" HeaderText="帐号"></asp:BoundColumn>
											<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="姓名"></asp:BoundColumn>
											<asp:BoundColumn DataField="TotalMark" SortExpression="TotalMark" HeaderText="成绩"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="答卷时间">
												<ItemTemplate>
													<asp:Label id="labExamTime" runat="server">答卷时间</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="LoginIP" SortExpression="LoginIP" ReadOnly="True" HeaderText="登录IP"></asp:BoundColumn>
											<asp:BoundColumn DataField="ExamState" SortExpression="ExamState" HeaderText="答卷状态"></asp:BoundColumn>
											<asp:BoundColumn DataField="JudgeLoginID" SortExpression="JudgeLoginID" HeaderText="评卷人"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="操作">
												<HeaderStyle Width="116px"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButAnswer" runat="server" Text="答卷" CommandName="Answer" CausesValidation="false">答卷</asp:LinkButton>
													<asp:LinkButton id="LinkButStatis" runat="server" Text="统计" CommandName="Statis" CausesValidation="false">统计</asp:LinkButton>
													<asp:LinkButton id="LinkButJudge" runat="server" Text="评卷" CommandName="Judge" CausesValidation="false">评卷</asp:LinkButton>
													<asp:LinkButton id="LinkButDelete" runat="server" Text="删除" CommandName="Delete" CausesValidation="false">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="开始时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="结束时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ImpScore" ReadOnly="True" HeaderText="客观题分数"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center">
									<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除答卷" onclick="ButDelete_Click"></asp:button>&nbsp;
									<asp:button id="ButRefresh" runat="server" CssClass="button" Text="刷新答卷"></asp:button>&nbsp;
									<asp:button id="ButAbsentUser" runat="server" CssClass="button" Text="缺考帐户"></asp:button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="关 闭" name="Button"></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
