<%@ Page language="c#" Inherits="EasyExam.RegistUser" CodeFile="RegistUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>帐户注册</title>
		<base target="_self">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
		<script language="JavaScript" src="../JavaScript/Calendar.js"></script>
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JavaScript/Common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="392" cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse; display:none;" borderColor="#6699ff" height="392" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1" id="regtable">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">帐户注册</font></td>
							</tr>
							<tr>
								<td align="center">
									<TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
										cellPadding="0" width="500" border="0">
										<tr height="25">
											<td align="center" colSpan="2" height="23">
											</td>
										</tr>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">证件号：</td>
											<td width="336" height="23"><asp:textbox id="txtLoginID" runat="server" CssClass="text"></asp:textbox><asp:button id="ButCheck" runat="server" CssClass="text" Text="检测重复" onclick="ButCheck_Click"></asp:button>(<FONT face="宋体" color="#ff0033">*</FONT>)</td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">姓名：</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtUserName" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">姓名：</td>
											<td width="336" height="23"><asp:textbox id="txtNewUserPwd" runat="server" CssClass="text"   Font-Size="9pt"></asp:textbox></td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px" align="right" height="23">姓名：</td>
											<td width="336" height="23"><asp:textbox id="txtSureUserPwd" runat="server" CssClass="text" TextMode="Password" Font-Size="9pt"></asp:textbox></td>
										</TR>
										<tr style="display:none;">
											<td style="WIDTH: 178px" align="right" height="23">性&nbsp;&nbsp;&nbsp;&nbsp;别：</td>
											<td width="336" height="23"><asp:radiobuttonlist id="RBLUserSex" runat="server" Width="56px" RepeatDirection="Horizontal">
													<asp:ListItem Value="男" Selected="True">男</asp:ListItem>
													<asp:ListItem Value="女">女</asp:ListItem>
												</asp:radiobuttonlist></td>
										</tr>
										<TR style="display:none;">
											<td style="WIDTH: 178px" align="right" height="23">出生年月：</td>
											<td width="336" height="23"><asp:textbox id="txtBirthday" runat="server" CssClass="text" Font-Size="9pt" Width="123px" onclick="jcomOpenCalender('txtBirthday');"></asp:textbox><A onclick="jcomOpenCalender('txtBirthday');" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A></td>
										</TR>
										<tr style="display:none;">
											<td style="WIDTH: 178px" vAlign="bottom" align="right" height="23">上传照片：</td>
											<td width="336" height="23"><INPUT class="text" id="UpUserPhoto" type="file" size="20" name="File1" runat="server"></td>
										</tr>
										<TR style="display:none;">
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">所属部门：</td>
											<td style="HEIGHT: 23px" width="336"><asp:dropdownlist id="DDLDept" runat="server" Width="88px"></asp:dropdownlist></td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">职&nbsp;&nbsp;&nbsp;&nbsp;务：</td>
											<td style="HEIGHT: 23px" width="336"><asp:dropdownlist id="DDLJob" runat="server" Width="88px"></asp:dropdownlist></td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">电&nbsp;&nbsp;&nbsp;&nbsp;话：</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtTelephone" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px; HEIGHT: 23px" align="right">证件类型：</td>
											<td style="HEIGHT: 23px" width="336"><asp:textbox id="txtCertType" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR style="display:none;">
											<td style="WIDTH: 178px" align="right" height="23">证件号码：</td>
											<td width="336" height="23"><asp:textbox id="txtCertNum" runat="server" CssClass="text"></asp:textbox></td>
										</TR>
										<TR>
											<TD style="WIDTH: 178px" align="right" height="23">登&nbsp;录&nbsp;IP：</TD>
											<TD width="336" height="23"><FONT face="宋体"><asp:textbox id="txtLoginIP" runat="server" CssClass="text"></asp:textbox></FONT></TD>
										</TR>
										<TR>
											<td style="WIDTH: 178px" align="right" height="23">类&nbsp;&nbsp;&nbsp;&nbsp;型：</td>
											<td width="336" height="23"><asp:dropdownlist id="DDLUserType" runat="server" Width="80px">
													<asp:ListItem Value="0">普通帐户</asp:ListItem>
												</asp:dropdownlist></td>
										</TR>
										<tr>
											<td align="center" colSpan="2" width="520" height="23"><FONT face="宋体"></FONT>
											</td>
										</tr>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" height="23">
									<asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>&nbsp;
									<input name="Button" type="button" class="button" value="取 消" onClick="window.close();"></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
