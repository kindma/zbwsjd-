<%@ Page language="c#" Inherits="EasyExam.PersonInfo.BrowBook" CodeFile="BrowBook.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>�������</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312"> 
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
</HEAD>
<body><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">�Ͳ��������ල��<br>
���߿���ϵͳ</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <div style=" margin:5px;">
    <asp:Label id="labSectionName" runat="server" Font-Bold="True"></asp:Label>
    <hr color="#6699ff">
    ���������
    <asp:Label  id="labBrowNumber" runat="server">****</asp:Label>
    <br>
    <asp:Label Visible="false" id="labCreateLoginID" runat="server">****</asp:Label>
    ���ڣ�
    <asp:Label id="labCreateDate" runat="server">****</asp:Label>
    <div style="300px;"> <%=strSectionContent%> </div>
  </div>
</form>
</body>
</HTML>
