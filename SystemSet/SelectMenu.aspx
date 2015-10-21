<%@ Page language="c#" Inherits="EasyExam.SystemSet.SelectMenu" CodeFile="SelectMenu.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>设置角色菜单</title>
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
					<td style="WIDTH: 499px" align="center"><font style="FONT-SIZE: 16px" face="黑体" color="#0054a6">设置角色菜单</font></td>
				</tr>
				<tr>
					<td style="WIDTH: 499px" align="center">
						<table id="AutoNumber1" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
							cellPadding="0" width="400" border="0">
							<TR>
								<TD style="HEIGHT: 16px" align="center" width="398" colSpan="2"><FONT face="宋体">
										<ASP:RADIOBUTTON id="rbAllMenu" runat="server" Text="所有菜单" GroupName="SelectMenu" Checked="True"></ASP:RADIOBUTTON>&nbsp;&nbsp;&nbsp;<ASP:RADIOBUTTON id="rbSelectMenu" runat="server" Text="选择菜单" GroupName="SelectMenu"></ASP:RADIOBUTTON></FONT></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<td style="WIDTH: 499px" align="center">
						<table style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0" cellPadding="0"
							width="400" border="0">
							<tr>
								<td style="WIDTH: 173px" width="173" height="25">
									<p align="left">&nbsp;&nbsp;&nbsp; 待选菜单</p>
								</td>
								<td align="center" width="36" colSpan="1" height="25" rowSpan="1"><FONT face="宋体"></FONT></td>
								<td width="174" height="25">
									<p align="left">已选菜单</p>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 173px" align="right" width="173"><asp:listbox id="LBSelect" runat="server" Width="150px" Height="230px" SelectionMode="Multiple"></asp:listbox></td>
								<td align="center" width="36"><asp:button id="butOneSelect" title="增加选择菜单" style="FONT-WEIGHT: bold" runat="server" Width="32"
										Text=">" onclick="butOneSelect_Click"></asp:button><BR>
									<HR>
									<asp:button id="butAllSelect" title="增加所有菜单" style="FONT-WEIGHT: bold" runat="server" Width="32"
										Text=">>" onclick="butAllSelect_Click"></asp:button><BR>
									<HR>
									<asp:button id="butOneDel" title="删除选择菜单" style="FONT-WEIGHT: bold" runat="server" Width="32"
										Text="<" onclick="butOneDel_Click"></asp:button><BR>
									<HR>
									<asp:button id="butAllDel" title="删除所有菜单" style="FONT-WEIGHT: bold" runat="server" Width="32"
										Text="<<" onclick="butAllDel_Click"></asp:button></td>
								<TD width="173"><asp:listbox id="LBSelected" runat="server" Width="150px" Height="230px" SelectionMode="Multiple"></asp:listbox></TD>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD style="WIDTH: 499px" align="center">
						<asp:Button id="ButInput" runat="server" Text="提 交" CssClass="button" onclick="ButInput_Click"></asp:Button>&nbsp;
						<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
