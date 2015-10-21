<%@ Page language="c#" Inherits="EasyExam.RubricManag.HtmlEditor" CodeFile="HtmlEditor.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HTML编辑器</title>
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			function ReturnText()
			{
				try
				{
					opener.HtmlEditorCallBack(EditorIframe.getHTML());
				}catch(e){};
				window.close();
			}
			function GetHtmlEditorText()
			{
				EditorIframe.setHTML(opener.SetHtmlEditorText());
			}
		</script>
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="GetHtmlEditorText()">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" style="WIDTH: 475px; BORDER-COLLAPSE: collapse; HEIGHT: 304px" cellSpacing="0"
				borderColorDark="#6699ff" cellPadding="0" width="475" align="center" borderColorLight="#6699ff"
				border="1">
				<TR>
					<td width="520" colSpan="6"><IFRAME id="EditorIframe" style="WIDTH: 100%; HEIGHT: 288px" src="../Editor/Editor.aspx?SectionID=<%=strSectionID%>"
							frameBorder="0" scrolling="no"></IFRAME>
					</td>
				</TR>
				<tr height="30">
					<td align="center" width="513" colSpan="6"><INPUT class="button" onclick="ReturnText();" type="button" value="提 交" name="Button">&nbsp;
						<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
