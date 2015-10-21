<%@ Page language="c#" Inherits="EasyExam.GradeManag.ManagGrade" CodeFile="ManagGrade.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>成绩管理</title>
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
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff"><asp:label id="labPaperType" runat="server" ForeColor="White"></asp:label>成绩管理</font></td>
							</tr>
							<tr>
								<td height="32">&nbsp;试卷名称：<asp:textbox id="txtPaperName" runat="server" Width="152px" CssClass="text"></asp:textbox>&nbsp; 
									日期：从
									<asp:textbox id="txtStartTime" onclick="jcomOpenCalender('txtStartTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox></FONT><A onClick="jcomOpenCalender('txtStartTime');" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A>到
									<asp:textbox id="txtEndTime" onclick="jcomOpenCalender('txtEndTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox></FONT><A onClick="jcomOpenCalender('txtEndTime');" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A>&nbsp;
									<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>
									<asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
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
											<asp:TemplateColumn HeaderText="序号">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="试卷名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="ProduceWay" SortExpression="ProduceWay" HeaderText="出题方式"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="有效时间">
												<ItemTemplate>
													<asp:Label id="labAvaiTime" runat="server">有效时间</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PaperMark" SortExpression="PaperMark" HeaderText="总分"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="出卷人"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="统计">
												<HeaderStyle Width="173px"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButAverage" runat="server" Text="平均分" CommandName="Average" CausesValidation="false">平均分</asp:LinkButton>
													<asp:LinkButton id="LinkButGrade" runat="server" Text="成绩" CommandName="Grade" CausesValidation="false">成绩</asp:LinkButton>
													<asp:LinkButton id="LinkButLore" runat="server" Text="知识点" CommandName="Lore" CausesValidation="false">知识点</asp:LinkButton>
													<asp:LinkButton id="LinkButTestType" runat="server" Text="题型" CommandName="TestType" CausesValidation="false">题型</asp:LinkButton>
													<asp:LinkButton id="LinkButTest" runat="server" Text="试题" CommandName="Test" CausesValidation="false">试题</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="开始时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="结束时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ManagerAccount" ReadOnly="True" HeaderText="评卷人员"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
