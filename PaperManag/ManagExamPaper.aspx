<%@ Page language="c#" Inherits="EasyExam.PaperManag.ManagExamPaper" CodeFile="ManagExamPaper.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>考试试卷管理</title>
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
					<td height="10"></td>
				</tr>
				<tr>
					<td valign="top">
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24">
									<font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">考试试卷管理</font></td>
							</tr>
							<tr>
								<td height="23">&nbsp;试卷名称：<asp:textbox id="txtPaperName" runat="server" Width="251px" CssClass="text"></asp:textbox>&nbsp; 
									组卷方式：<asp:dropdownlist id="DDLCreateWay" runat="server" Width="78px">
										<asp:ListItem Value="0">--全部--</asp:ListItem>
										<asp:ListItem Value="1">随机组卷</asp:ListItem>
										<asp:ListItem Value="2">手工组卷</asp:ListItem>
									</asp:dropdownlist>
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
											<asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="试卷名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="ProduceWay" SortExpression="ProduceWay" HeaderText="出题方式"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="有效时间">
												<ItemTemplate>
													<asp:Label id="labAvaiTime" runat="server">有效时间</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="TestCount" SortExpression="TestCount" HeaderText="题量"></asp:BoundColumn>
											<asp:BoundColumn DataField="PaperMark" SortExpression="PaperMark" HeaderText="总分"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="出卷人"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="创建日期"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="操作">
												<HeaderStyle Width="86px"></HeaderStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButEditPaper" runat="server" Text="修改">修改</asp:LinkButton>
													<asp:LinkButton id="LinkButDel" runat="server" Text="删除" CommandName="Delete" CausesValidation="false">删除</asp:LinkButton>
													<asp:LinkButton id="LinkButPreviewPaper" runat="server" Text="预览" CommandName="PreviewPaper" CausesValidation="false">预览</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="PaperType" ReadOnly="True" HeaderText="试卷类型"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="开始时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="结束时间"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="CreateWay" ReadOnly="True" HeaderText="组卷方式"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center">
									<asp:button id="ButRandPaper" runat="server" CssClass="button" Text="随机组卷"></asp:button>&nbsp;
									<asp:button id="ButCustomPaper" runat="server" CssClass="button" Text="手工组卷"></asp:button>&nbsp;
									<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除试卷" onclick="ButDelete_Click"></asp:button></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
