<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.ShowStatis" CodeFile="ShowStatis.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>答卷统计</title>
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
				<tr>
					<td height="6"></td>
				</tr>
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="1" width="100%" align="center" border="0" style="LINE-HEIGHT: 200%">
							<tr>
								<td vAlign="bottom" width="20%"><FONT face="宋体"></FONT></td>
								<td vAlign="bottom" align="center" width="60%"><span id="lblTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 14pt"><%=strPaperName%></span></td>
								<td vAlign="bottom" align="right" width="20%"><span id="lblSubTitle">总共<%=strTestCount%>
										题共<%=strPaperMark%>分</span></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" bgColor="#0000ff" height="2"></td>
				</tr>
				<tr>
					<td align="center" style="HEIGHT: 20px" height="20"><span id="lblMessage">帐号:<%=strLoginID%>&nbsp;&nbsp;姓名:<%=strUserName%>&nbsp;&nbsp;答题时间:<%=strExamTime%>&nbsp;&nbsp;通过分数:<%=strPassMark%>&nbsp;&nbsp;考生得分:<%=strTotalMark%></span></td>
				</tr>
				<tr>
					<td align="center">
						<asp:datagrid id="DataGridStatis" runat="server" Width="100%" CellPadding="0" BorderWidth="1px"
							AutoGenerateColumns="False" PageSize="20" AllowSorting="True">
							<FooterStyle ForeColor="White"></FooterStyle>
							<ItemStyle Height="23px"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="序号">
									<ItemTemplate>
										<%#RowNum++%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="SubjectName" SortExpression="SubjectName" HeaderText="科目名称"></asp:BoundColumn>
								<asp:BoundColumn DataField="LoreName" SortExpression="LoreName" HeaderText="知识点"></asp:BoundColumn>
								<asp:BoundColumn DataField="TestCount" SortExpression="TestCount" HeaderText="试题数量"></asp:BoundColumn>
								<asp:BoundColumn DataField="TestMark" SortExpression="TestMark" HeaderText="试题分数"></asp:BoundColumn>
								<asp:BoundColumn DataField="TotalMark" SortExpression="TotalMark" HeaderText="考生得分"></asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Rate" HeaderText="得分率">
									<HeaderStyle Width="200px"></HeaderStyle>
									<ItemTemplate>
										<asp:image id="ImgRate" runat="server" Width="50px" Height="15px" ImageUrl="../Images/Percent.gif"
											ToolTip=""></asp:image>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td align="center"><input class="button" onclick="window.close();" type="button" value="关 闭" name="button1"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
