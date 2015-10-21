<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.ShowAnswer" CodeFile="ShowAnswer.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>查看答卷</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312"> 
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<script language="JavaScript" src="../JavaScript/Common.js"></script>
</HEAD>
<body ><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <table cellSpacing="0" cellPadding="1" width="95%" align="center" border="0">
    <tr>
      <td></td>
    </tr>
    <tr>
      <td align="center"><table cellSpacing="0" cellPadding="1" width="100%" align="center" border="0"  >
          <tr>
            <td  width="20%"></td>
            <td  align="center" width="41%"><span id="lblTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 14pt"><%=strPaperName%></span></td>
            <td  align="right" width="39%"><span id="lblSubTitle">总共<%=strTestCount%> 题<br>
              共<%=strPaperMark%>分</span></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td style="HEIGHT: 2px" bgColor="#0000ff"></td>
    </tr>
    <tr>
      <td align="center" style="HEIGHT: 20px"><span id="lblMessage" style="font-size:14px;">帐号:<%=strLoginID%>&nbsp;&nbsp;姓名:<%=strUserName%> <br>
        答题时间:<br>
        <%=strExamTime%>&nbsp;&nbsp;<br>
        通过分数:<%=strPassMark%>&nbsp;&nbsp;考生得分:<%=strTotalMark%></span></td>
    </tr>
    <tr>
      <td align="center"><table cellSpacing="0" cellPadding="1" width="100%" align="center" border="0" >
          <tr>
            <td align="center"><asp:RadioButton id="RadioButAll" runat="server" Text="全部显示" GroupName="ShowTest" Checked="True"
										AutoPostBack="True" oncheckedchanged="RadioButAll_CheckedChanged"></asp:RadioButton>
              <asp:RadioButton id="RadioButWrong" runat="server" Text="错题显示" GroupName="ShowTest" AutoPostBack="True" oncheckedchanged="RadioButWrong_CheckedChanged"></asp:RadioButton></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td bgColor="#cee7ff"><table id="tblPaper" cellSpacing="0" cellPadding="1" width="100%" align="center" bgColor="white"
							border="0">
          <%=strPaperContent%>
        </table></td>
    </tr>
  </table>
</form>
</body>
</HTML>
