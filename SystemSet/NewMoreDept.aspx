<%@ Page language="c#" Inherits="EasyExam.SystemSet.NewMoreDept" CodeFile="NewMoreDept.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量新建部门</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="100" cellSpacing="0"
							cellPadding="0" width="100%" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">批量新建部门</font></td>
							</tr>
							<tr>
								<td>
									<!--内容-->
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="500" align="center" border="0">
										<tr>
											<td colspan="2" height="23"><br>
											</td>
										</tr>
										<TR>
											<td style="WIDTH: 137px" align="right">部门名称：</td>
											<td><asp:TextBox runat="server" Height="182px" TextMode="MultiLine" Width="256px" CssClass="text"
													ID="txtDeptName"></asp:TextBox>
											</td>
										</TR>
										<tr>
											<td colspan="2" align="center" height="23"><font color="red">注：部门名称之间用半角逗号（,）分隔。</font><br>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23"><asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="返 回" name="button1"></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
