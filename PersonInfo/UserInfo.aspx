<%@ Page language="c#" Inherits="EasyExam.PersonInfo.UserInfo" CodeFile="UserInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>帐户信息</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<style type="text/css">
* {
	font-size: 14px;
	line-height: 1.5em;
}
</style>
<script language="JavaScript" src="../JavaScript/Common.js"></script>
</HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <table height="408" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
     
    <tr>
      <td><!--内容开始-->
        
        <table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="368" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
          <tr>
            <td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">帐户信息</font></td>
          </tr>
          <tr>
            <td><TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
										cellPadding="0" width="100%" border="0">
                <TR>
                  <TD align="center" colSpan="3" height="23"></TD>
                </TR>
                <TR>
                  <td style="WIDTH: 178px" align="right" height="23">证件号：</td>
                  <td width="336" height="23" colspan="2"><asp:textbox id="txtLoginID" runat="server" CssClass="text" Enabled="False"></asp:textbox></td>
                </TR>
                <TR >
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right">姓名：</td>
                  <td style="HEIGHT: 23px" width="248"><asp:textbox id="txtUserName" runat="server" CssClass="text" Enabled="False"></asp:textbox></td>
                  <td style="HEIGHT: 23px" width="81" rowspan="4"><asp:image id="ImageUser" runat="server" Width="60px" Height="90px" ImageUrl="../Images/UserImage.gif"
													ToolTip="帐户照片"></asp:image></td>
                </TR>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px" align="right" height="23">性别：</td>
                  <td width="248" height="23"><asp:radiobuttonlist id="RBLUserSex" runat="server" Width="56px" RepeatDirection="Horizontal" Enabled="False"
													Height="2px">
                      <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                      <asp:ListItem Value="女">女</asp:ListItem>
                    </asp:radiobuttonlist></td>
                </TR>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px" align="right" height="23"><FONT face="宋体">出生年月：</FONT></td>
                  <td width="248" height="23"><asp:textbox id="txtBirthday" runat="server" CssClass="text" Font-Size="9pt" Width="123px" ReadOnly="True"
													Enabled="False"></asp:textbox></td>
                </TR>
                <tr  style="display:none;">
                  <td style="WIDTH: 178px" align="right" height="23"><FONT face="宋体">所属部门：</FONT></td>
                  <td width="248" height="23"><asp:textbox id="txtDept" runat="server" CssClass="text" Width="88px" Font-Size="9pt" ReadOnly="True"
													Enabled="False"></asp:textbox></td>
                </tr>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px" align="right" height="23">职务：</td>
                  <td width="336" height="23" colspan="2"><asp:textbox id="txtJob" runat="server" CssClass="text" Width="88px" Font-Size="9pt" ReadOnly="True"
													Enabled="False"></asp:textbox>
                    <A onClick="jcomOpenCalender('txtBirthday');" href="#"></A></td>
                </TR>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right">电话：</td>
                  <td style="HEIGHT: 23px" width="336" colspan="2"><asp:textbox id="txtTelephone" runat="server" CssClass="text" Enabled="False"></asp:textbox></td>
                </TR>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right">证件类型：</td>
                  <td style="HEIGHT: 23px" width="336" colspan="2"><asp:textbox id="txtCertType" runat="server" CssClass="text" Enabled="False"></asp:textbox></td>
                </TR>
                <TR  style="display:none;">
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right" height="21">证件号码：</td>
                  <td width="336" height="21" colspan="2" style="HEIGHT: 23px"><asp:textbox id="txtCertNum" runat="server" CssClass="text" Enabled="False"></asp:textbox></td>
                </TR>
                <TR  style="display:none;">
                  <TD style="WIDTH: 178px" align="right" height="23">登录IP：</TD>
                  <TD width="336" height="23" colspan="2"><FONT face="宋体">
                    <asp:textbox id="txtLoginIP" runat="server" CssClass="text" Enabled="False"></asp:textbox>
                    </FONT></TD>
                </TR>
                <TR>
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right" height="17">类型：</td>
                  <td width="336" height="17" colspan="2" style="HEIGHT: 23px"><asp:textbox id="txtUserType" runat="server" CssClass="text" Width="88px" Font-Size="9pt" ReadOnly="True"
													Enabled="False"></asp:textbox></td>
                </TR>
                <tr>
                  <td style="WIDTH: 178px; HEIGHT: 23px" align="right">状态：</td>
                  <td style="HEIGHT: 23px" width="336" colspan="2"><asp:textbox id="txtUserState" runat="server" CssClass="text" Width="88px" Font-Size="9pt" ReadOnly="True"
													Enabled="False"></asp:textbox></td>
                </tr>
                <TR>
                  <TD align="center" width="514" colSpan="3" height="23"><FONT face="宋体"></FONT></TD>
                </TR>
              </TABLE></td>
          </tr>
          <tr>
            <td align="center" height="23"><INPUT class="button" onClick="history.go(-1);" type="button" value="返 回"
										name="button1"></td>
          </tr>
        </table>
        
        <!--内容结束--></td>
    </tr>
  </table>
</form>
</body>
</HTML>
