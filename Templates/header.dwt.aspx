<%@ Page language="c#" Inherits="EasyExam.PersonInfo.BrowBook" CodeFile="BrowBook.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<!-- TemplateBeginEditable name="doctitle" -->
<title>�������</title>
<!-- TemplateEndEditable -->
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<!-- TemplateBeginEditable name="head" -->
<!-- TemplateEndEditable -->
</HEAD>
<body>

<form id="Form1" method="post" runat="server">
<div style=" margin:5px;">
  <asp:Label id="labSectionName" runat="server" Font-Bold="True"></asp:Label>
  <hr color="#6699ff">
  ���������
  <asp:Label  id="labBrowNumber" runat="server">****</asp:Label>
  <br>
  <asp:Label Visible="false" id="labCreateLoginID" runat="server">****</asp:Label>
  ���ڣ�
  <asp:Label id="labCreateDate" runat="server">****</asp:Label>
  <div style="300px;">
  <%=strSectionContent%>
  </div>
  </div>
</form>
</body>
</HTML>
