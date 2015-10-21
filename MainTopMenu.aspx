<%@ Page Language="c#" Inherits="EasyExam.MainTopMenu" CodeFile="MainTopMenu.aspx.cs" %>
<HTML>
	<HEAD>
		<TITLE>网络考试系统</TITLE>
		<meta http-equiv="Content-Language" content="zh-cn">
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="JavaScript/MouseEvent.js"></script>
		<SCRIPT LANGUAGE="JAVASCRIPT">
          function LogOut()
          {
            if (confirm('您确定要注销考试系统吗？'))
            {
    		  NWH=window.open('Login.aspx','Login','titlebar=yes,menubar=yes,toolbar=yes,location=yes,directories=yes,status=yes,scrollbars=yes,resizable=yes,copyhistory=yes,top=0,left=0,width=screen.availWidth,height=screen.availHeight');
			  NWH.moveTo(0,0);
        	  NWH.resizeTo(screen.availWidth,screen.availHeight);
			  window.opener=null;
			  window.open('','_self');
			  window.close();
			  window.parent.close();
            }
          }
		</SCRIPT>
	</HEAD>
	<body onselectstart="return false" aLink="#333333" bgColor="#6699ff" leftMargin="0" link="#333333"
		topMargin="0" vLink="#333333">
		<table
			width="100%" height="75" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#111111" background="Images/topbj.jpg" id="AutoNumber1" style="BORDER-COLLAPSE: collapse">
			<tr>
				<td width="54%" align="left"><FONT style="FONT-SIZE: 25pt; FILTER: shadow(color=green); WIDTH: 100%; COLOR: white; LINE-HEIGHT: 150%; FONT-FAMILY: 宋体"><B>&nbsp;在线考试答题系统</B></FONT>  </td>
		        <td width="46%" height="75" align="right"><table width="624" height="75" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td valign="bottom" background="Images/topwzbj.jpg"><table width="624" height="36" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="292"><font style="FONT-SIZE: 10pt" color="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;帐号：
                          <asp:Label ID="LoginID" runat="server" Width="72px" Font-Size="9pt" ForeColor="White"></asp:Label>
                          姓名：</font>
                            <asp:Label ID="UserName" runat="server" Width="72px" Font-Size="9pt" ForeColor="White"></asp:Label></td>
                        <td width="110"><font style="FONT-SIZE: 10pt" color="#ffffff"><a style="TEXT-DECORATION: none" target="main" href="MainFrm.aspx"><font color="#ffffff">首页</font></a>┊<a href="#" style="TEXT-DECORATION: none" onClick="showModalDialog('Help/About.aspx',0,'dialogWidth:306px;dialogHeight:224px;help:no;center:yes;resizable:no;status:no;scroll:no');"><font color="#ffffff">关于</font></a>┊<a href="#" style="TEXT-DECORATION: none" onClick="LogOut();"><font color="#ffffff">注销</font></a></font></td>
                      </tr>
                    </table></td>
                  </tr>
                </table></td>
			</tr>
	</table>
		
	</body>
</HTML>
