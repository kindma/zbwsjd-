<%@ Page language="c#" Inherits="EasyExam.PersonInfo.PassWord" CodeFile="PassWord.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>修改密码</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312"> 
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
</HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <table   cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    
    <tr>
      <td><!--内容开始-->
        
        <table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff"   cellSpacing="0"
							cellPadding="0" width="100%" border="1">
          <tr>
            <td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">修改密码</font></td>
          </tr>
          <tr>
            <td><TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
										cellPadding="0"  border="0">
                <TR>
                  <TD   align="right" colSpan="2"  ></TD>
                </TR>
                <TR>
                  <td   align="right"  >帐 号：</td>
                  <td ><asp:textbox id="txtLoginID" runat="server" CssClass="text" Enabled="False" Width="151px"></asp:textbox></td>
                </TR>
                <TR>
                  <TD   align="right" height="24">旧 密 码：</TD>
                  <TD  ><asp:textbox id="txtOldPwd" runat="server" CssClass="text" Width="151px" Font-Size="9pt" TextMode="Password"></asp:textbox></TD>
                </TR>
                <TR>
                  <td   align="right" height="17">新 密 码：</td>
                  <td  ><asp:textbox id="txtNewPwd" runat="server" CssClass="text" Width="151px" Font-Size="9pt" TextMode="Password"></asp:textbox></td>
                </TR>
                <tr>
                  <td   align="right">确认密码：</td>
                  <td  ><asp:textbox id="txtSureNewPwd" runat="server" CssClass="text" Width="151px" Font-Size="9pt"
													TextMode="Password"></asp:textbox></td>
                </tr>
                <TR>
                  <TD align="center" width="514" colSpan="2" height="23"><FONT face="宋体"></FONT></TD>
                </TR>
              </TABLE></td>
          </tr>
          <tr>
            <td align="center" height="23"><asp:Button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:Button>
              &nbsp;
              </td>
          </tr>
        </table>
        
        <!--内容结束--></td>
    </tr>
  </table>
</form>
</body>
</HTML>
