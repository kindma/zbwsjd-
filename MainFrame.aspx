<%@ Page Language="c#" Inherits="EasyExam.MainFrame" CodeFile="MainFrame.aspx.cs" %>
<HTML>
	<HEAD>
		<TITLE>���翼��ϵͳ</TITLE>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<frameset id="fraMain" border="0" frameSpacing="0" rows="75,*,25" frameBorder="0" runat="server">
		<frame id="TopMenu" name="TopMenu" src="MainTopMenu.aspx" noResize scrolling="no">
		</frame>
		<frameset id="Bottom" name="Bottom" border="0" frameSpacing="0" frameBorder="0" cols="142,*"
			runat="server">
			<frame id="fraLeft" name="fraLeft" src="MainLeftMenu.aspx" scrolling="no" runat="server" noresize>
			</frame>
			<frame id="main" name="main" src="MainFrm.aspx" scrolling="auto" runat="server">
			</frame>
		</frameset>
		<frame id="BottomMenu" src="MainBottom.aspx" name="BottomMenu" noResize scrolling="no">
		</frame>
		<noframes>
			<body>
				<p>����ҳʹ���˿�ܣ��������������֧�ֿ�ܡ�</p>
			</body>
		</noframes>
	</frameset>
</HTML>