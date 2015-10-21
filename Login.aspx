<%@ Page Language="c#" Inherits="EasyExam.Login" CodeFile="Login.aspx.cs" %>
<HTML>
<HEAD>
<TITLE>网络考试系统-登录界面</TITLE>
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
</HEAD>
<BODY >
<!-- #BeginLibraryItem "/Library/header.lbi" -->
<dl class="headerdd">
  <dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
  <dd class="webtitle">淄博市卫生监督局<br>
    在线考试系统</dd>
  <dd class="menuright"> </dd>
</dl>
<!-- #EndLibraryItem --><script>
if (this != top) parent.location = "login.aspx";
</script>
<FORM id="login" method="post" runat="server" >
  <dl class="loginpage">
  <dt><img src="Images/yhdl.jpg" width="100%"></dt>
  <dd></dd>
  <dd> <span>身份证号：</span>
    <br>
    <asp:textbox id="LoginID" runat="server"   ></asp:textbox>
    <asp:requiredfieldvalidator id="LoginIDRequiredFieldValidator" runat="server" Width="88px" controlToValidate="LoginID"
						errormessage="帐号不能为空！" display="None" Font-Size="9pt"> 帐号不能为空！</asp:requiredfieldvalidator>
  </dd>
  <dd> <span>姓名：<br>
  </span>
    <asp:textbox id="UserPwd" runat="server"   ></asp:textbox>
  </dd>
  <dd>
    <asp:button id="ButLogin" runat="server" Text="登 录"   onclick="ButLogin_Click"></asp:button>
    <br>
    技术咨询：13969390450
    <asp:validationsummary id="ErrorSummary" Width="93px" DisplayMode="List" Height="32px" ForeColor="Black"
			ShowMessageBox="True" Font-Bold="True" ShowSummary="False" Runat="server" HeaderText=""></asp:validationsummary>
  </dd>
  <dd><br>
    <asp:button Visible="false" id="ButRegist" runat="server" Text="注册新用户" CausesValidation="False"  
						Enabled="False"></asp:button>
  </dd>
</FORM>
</BODY>
</HTML>
