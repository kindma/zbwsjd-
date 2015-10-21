<%@ Page Language="c#" Inherits="EasyExam.RubricManag.ManagBookFrame" CodeFile="ManagBookFrame.aspx.cs" %>
<HTML>
	<HEAD>
		<TITLE>电子书籍</TITLE>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<frameset id="BookBottom" name="BookBottom" border="0" frameSpacing="0" frameBorder="0" cols="230,*"
		runat="server">
		<frame id="booktree" name="booktree" src="ManagBookTree.aspx" scrolling="auto" runat="server"
			noresize>
		</frame>
		<frame id="bookmain" name="bookmain" src="ManagBookList.aspx" scrolling="auto" runat="server">
		</frame>
		<noframes>
			<body>
				<p>此网页使用了框架，但您的浏览器不支持框架。</p>
			</body>
		</noframes>
	</frameset>
</HTML>
