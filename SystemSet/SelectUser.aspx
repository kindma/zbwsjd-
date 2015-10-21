<%@ Page language="c#" Inherits="EasyExam.SystemSet.SelectUser" CodeFile="SelectUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>设置评卷帐号</title>
		<base target="_self">
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="480" align="center" border="0">
				<tr height="25">
					<td style="WIDTH: 499px" align="center"><font style="FONT-SIZE: 16px" face="黑体" color="#0054a6">设置评卷帐号</font></td>
				</tr>
				<tr>
					<td style="WIDTH: 499px" align="center">
						<table id="AutoNumber1" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
							cellPadding="0" width="400" border="0">
							<TR>
								<TD style="HEIGHT: 16px" align="center" width="398" colSpan="2"><FONT face="宋体">
										<ASP:RADIOBUTTON id="rbAllAccount" runat="server" Text="所有帐号" GroupName="SelectAccount" Checked="True"></ASP:RADIOBUTTON>&nbsp;&nbsp;&nbsp;<ASP:RADIOBUTTON id="rbSelectAccount" runat="server" Text="选择帐号" GroupName="SelectAccount"></ASP:RADIOBUTTON></FONT></TD>
							</TR>
							<tr>
								<td style="HEIGHT: 19px" width="88">
									<p align="right">&nbsp;选择部门：</p>
								</td>
								<td style="HEIGHT: 19px" width="310"><asp:dropdownlist id="DDLDeptName" runat="server" AutoPostBack="True" Width="235px" onselectedindexchanged="DDLDept_SelectedIndexChanged"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td width="88">
									<p align="right">&nbsp;人员查询：</p>
								</td>
								<td vAlign="bottom" width="310"><asp:textbox id="txtQuery" runat="server" Width="235px" ToolTip="可输入帐号或姓名进行查询" CssClass="text"
										MaxLength="20"></asp:textbox>
									<asp:button id="ButQuery" runat="server" Text="查 询" CssClass="button" onclick="ButQuery_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD style="WIDTH: 499px" align="center">
						<TABLE style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0" cellPadding="0"
							width="400" border="0">
							<TR>
								<TD style="WIDTH: 173px" width="173" height="25">
									<P align="left">&nbsp;&nbsp;&nbsp; 待选部门/帐号</P>
								</TD>
								<TD align="center" width="36" colSpan="1" height="25" rowSpan="1"><FONT face="宋体"></FONT></TD>
								<TD width="174" height="25">
									<P align="left">已选部门/帐号</P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 173px" align="right" width="173">
									<asp:listbox id="LBSelect" runat="server" Width="150px" Height="230px" SelectionMode="Multiple"></asp:listbox></TD>
								<TD align="center" width="36">
									<asp:button id="butOneSelect" title="增加选择人员" style="FONT-WEIGHT: bold" runat="server" Text=">"
										Width="32" onclick="butOneSelect_Click"></asp:button><BR>
									<HR>
									<asp:button id="butAllSelect" title="增加所有人员" style="FONT-WEIGHT: bold" runat="server" Text=">>"
										Width="32" onclick="butAllSelect_Click"></asp:button><BR>
									<HR>
									<asp:button id="butOneDel" title="删除选择人员" style="FONT-WEIGHT: bold" runat="server" Text="<"
										Width="32" onclick="butOneDel_Click"></asp:button><BR>
									<HR>
									<asp:button id="butAllDel" title="删除所有人员" style="FONT-WEIGHT: bold" runat="server" Text="<<"
										Width="32" onclick="butAllDel_Click"></asp:button></TD>
								<TD width="173">
									<asp:listbox id="LBSelected" runat="server" Width="150px" Height="230px" SelectionMode="Multiple"></asp:listbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 499px" align="center">
						<asp:Button id="ButInput" runat="server" Text="提 交" CssClass="button" onclick="ButInput_Click"></asp:Button>&nbsp;
						<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
