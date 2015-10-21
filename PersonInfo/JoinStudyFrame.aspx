<%@ Page Language="c#" Inherits="EasyExam.PersonInfo.JoinStudyFrame" CodeFile="JoinStudyFrame.aspx.cs" %>
<HTML>
	<HEAD>
		<TITLE>参加学习</TITLE>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
		<frameset id="StudyBottom" name="StudyBottom" border="0" frameSpacing="0" frameBorder="0" cols="230,*"
			runat="server">
			<frame id="studytree" name="studytree" src="JoinStudyTree.aspx" scrolling="auto" runat="server" noresize>
			</frame>
			<frame id="studymain" name="studymain" src="JoinStudyList.aspx" scrolling="auto" runat="server">
			</frame>
			<noframes>
				<body>
					<p>此网页使用了框架，但您的浏览器不支持框架。</p>
				</body>
			</noframes>
		</frameset>
</HTML>