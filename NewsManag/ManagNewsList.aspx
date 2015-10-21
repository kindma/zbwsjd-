<%@ Page language="c#" Inherits="EasyExam.NewsManag.ManagNewsList" CodeFile="ManagNewsList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>新闻管理</title>
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
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<TBODY>
								<tr>
									<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
										height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">新闻管理</font></td>
								</tr>
								<tr>
									<td style="HEIGHT: 22px">&nbsp;&nbsp;新闻标题：<asp:textbox id="txtNewsTitle" runat="server" CssClass="text" Width="247px"></asp:textbox>
										<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
								</tr>
								<tr>
									<td vAlign="top" align="center"><asp:datagrid id="DataGridNews" runat="server" Width="100%" AllowSorting="True" PageSize="15"
											CellPadding="0" BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False">
											<ItemStyle Height="32px"></ItemStyle>
											<HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="NewsID" ReadOnly="True" HeaderText="NewsID"></asp:BoundColumn>
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
												<asp:TemplateColumn SortExpression="NewsTitle" HeaderText="新闻标题">
													<ItemTemplate>
														<asp:Label id="labNewsTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NewsTitle") %>'>新闻标题</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="BrowNumber" SortExpression="BrowNumber" HeaderText="浏览次数"></asp:BoundColumn>
												<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="发布人"></asp:BoundColumn>
												<asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="发布日期"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="操作">
													<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="LinkButEditNews" runat="server" CausesValidation="false" CommandName="Edit"
															Text="修改">修改</asp:LinkButton>&nbsp;
														<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="删除">删除</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>页&nbsp;</FONT>
										<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
										<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
										<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
										<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
								</tr>
								<tr>
									<td style="HEIGHT: 22px" align="center"><FONT face="宋体"><asp:button id="ButIssuNews" runat="server" CssClass="button" Text="发布通知" ></asp:button>&nbsp;
											<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除通知" onclick="ButDelete_Click"></asp:button></FONT></td>
								</tr>
				</tr>
			</table>
			<!--内容结束--> </TD></TR></TBODY></TABLE><input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
