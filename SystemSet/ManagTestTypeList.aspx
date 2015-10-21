<%@ Page language="c#" Inherits="EasyExam.SystemSet.ManagTestTypeList" CodeFile="ManagTestTypeList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>题型设置</title>
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
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24">
									<font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">题型设置</font></td>
							</tr>
							<tr>
								<td height="23">&nbsp;<FONT face="宋体">题型名称：<asp:textbox id="txtTestTypeName" runat="server" CssClass="text" Width="74px"></asp:textbox>&nbsp;基本类型：<asp:dropdownlist id="DDLBaseTestType" runat="server" Width="68px">
											<asp:ListItem Value="单选类">单选类</asp:ListItem>
											<asp:ListItem Value="多选类">多选类</asp:ListItem>
											<asp:ListItem Value="判断类">判断类</asp:ListItem>
											<asp:ListItem Value="填空类">填空类</asp:ListItem>
											<asp:ListItem Value="问答类">问答类</asp:ListItem>
											<asp:ListItem Value="作文类">作文类</asp:ListItem>
											<asp:ListItem Value="打字类">打字类</asp:ListItem>
											<asp:ListItem Value="操作类">操作类</asp:ListItem>
										</asp:dropdownlist>
										<asp:button id="ButNewDept" runat="server" CssClass="button" Text="添加" onclick="ButNewDept_Click"></asp:button>&nbsp;
										<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button><asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></FONT></td>
							</tr>
							<TR>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridTestType" runat="server" Width="100%" PageSize="15" CellPadding="0"
										BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle Height="32px" HorizontalAlign="Center" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="TestTypeID" ReadOnly="True" HeaderText="TestTypeID"></asp:BoundColumn>
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
											<asp:BoundColumn DataField="TestTypeName" SortExpression="TestTypeName" HeaderText="题型名称"></asp:BoundColumn>
											<asp:TemplateColumn SortExpression="BaseTestType" HeaderText="基本类型">
												<ItemTemplate>
													<asp:Label id="labBaseTestType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BaseTestType") %>'>
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:DropDownList id="DDLTestType" runat="server" Width="68px"></asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="使用状态">
												<ItemTemplate>
													<asp:Label id="labUseState" runat="server">使用状态</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编辑" CancelText="取消" EditText="编辑">
												<HeaderStyle Width="60px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn HeaderText="删除">
												<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="删除">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="BaseTestType" ReadOnly="True" HeaderText="BaseTestType"></asp:BoundColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:label id="LabelRecord" runat="server" Font-Names="宋体" Font-Bold="True" ForeColor="Blue">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" Font-Names="宋体" Font-Bold="True" ForeColor="Blue">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" Font-Names="宋体" Font-Bold="True" ForeColor="Blue">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</TR>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="宋体"><asp:button id="ButDelete" runat="server" CssClass="button" Text="删除题型" onclick="ButDelete_Click"></asp:button></FONT></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
