<%@ Page language="c#" Inherits="EasyExam.PersonInfo.BrowNews" CodeFile="BrowNews.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>浏览新闻</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
</HEAD>
<body ><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <TABLE id="Table1" cellSpacing="0" cellPadding="0"   align="center" border="0"
				 >
    <TR>
      <TD   align="center" height="10" colSpan="1" rowSpan="1"></TD>
    </TR>
    <TR>
      <TD  align="center" colSpan="1"  rowSpan="1"><asp:Label id="labNewsTitle" runat="server" Font-Bold="True"></asp:Label>
        <hr color="#6699ff"></TD>
    </TR>
    <tr>
      <td   align="center"> 浏览次数：
        <asp:Label id="labBrowNumber" runat="server">****</asp:Label>
        &nbsp;&nbsp;
        <asp:Label Visible="false" id="labCreateLoginID" runat="server">****</asp:Label>
        &nbsp;&nbsp; 
        发布日期：
        <asp:Label id="labCreateDate" runat="server">****</asp:Label></td>
    </tr>
    <TR>
      <TD style="  WORD-BREAK: break-all; line-height:22px; vAlign="baseline" align="left"><br>
        <br>
        <%=strNewsContent%></TD>
    </TR>
    <TR>
      <TD style=" align="center"><input class="button" onclick="window.close();" type="button" value="关 闭" name="button1"></TD>
    </TR>
  </TABLE>
</form>
</body>
</HTML>
