<%@ Page Language="c#" Inherits="EasyExam.RubricManag.ManagBookList" CodeFile="ManagBookList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>电子书籍</title>
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
			<table height="563" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
				<tr>
					<td height="40"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="523" cellSpacing="0"
							cellPadding="0" width="100%" align="left" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">电子书籍</font></td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px">&nbsp; 科目名称：<asp:DropDownList id="DDLSubjectName" runat="server" Width="115px" AutoPostBack="True">
										<asp:ListItem Value="0" Selected="True">--全部--</asp:ListItem>
									</asp:DropDownList>&nbsp;&nbsp;章名：<asp:TextBox id="txtChapterName" runat="server" Width="115px"></asp:TextBox>&nbsp;&nbsp;节名：
									<asp:TextBox id="txtSectionName" runat="server" Width="115px"></asp:TextBox>
									<asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>
									<asp:Label id="LabCondition" runat="server" Visible="False">条件</asp:Label></td>
							</tr>
							<tr>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridBook" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" AllowSorting="True">
										<ItemStyle Height="32px"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SectionID" ReadOnly="True" HeaderText="SectionID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="&lt;input type='checkbox' id='chkSelectAll' name='chkSelectAll' onclick='jcomSelectAllRecords()' title='全选或全部取消' &gt;">
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
											<asp:BoundColumn DataField="SubjectName" SortExpression="SubjectName" HeaderText="科目名称"></asp:BoundColumn>
											<asp:BoundColumn DataField="ChapterName" SortExpression="ChapterName" HeaderText="章名"></asp:BoundColumn>
											<asp:BoundColumn DataField="SectionName" SortExpression="SectionName" HeaderText="节名"></asp:BoundColumn>
											<asp:BoundColumn DataField="BrowNumber" SortExpression="BrowNumber" HeaderText="浏览次数"></asp:BoundColumn>
											<asp:BoundColumn DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="创建人"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="操作">
												<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LinkButEditBook" runat="server">编辑</asp:LinkButton>&nbsp;
													<asp:LinkButton id="LinkButDel" runat="server" CausesValidation="false" CommandName="Delete" Text="删除">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><FONT face="宋体">共有<asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>条记录&nbsp;<asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;当前是第<asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</tr>
							<tr>
								<td style="HEIGHT: 23px" align="center"><FONT face="宋体">
										<asp:button id="ButDelete" runat="server" CssClass="button" Text="删除章节" onclick="ButDelete_Click"></asp:button></FONT></td>
							</tr>
						</table>
						<!--内容结束--></td>
				</tr>
		  </table>
			<input type="hidden" size="4" name="hidcommand">
		</form>
	</body>
</HTML>
