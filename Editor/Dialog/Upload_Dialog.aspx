<%@ Page Language="c#" Inherits="EasyExam.Editor.Dialog.Upload_Dialog" CodeFile="Upload_Dialog.aspx.cs" %>
<HTML>
	<HEAD>
		<title>�ϴ��ļ�</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<LINK href="../Css/editor_dialog.css" type="text/css" rel="stylesheet">
			<SCRIPT language="javascript">
				function check() 
				{
					var strFileName=document.all("UpLoadFile").value;
					if (strFileName=="")
					{
    					alert("��ѡ��Ҫ�ϴ����ļ�");
						document.all("UpLoadFile").focus();
    					return false;
  					}
				}
			</SCRIPT>
	</HEAD>
	<body bgColor="menu" leftMargin="5" topMargin="0">
		<form name="form1" onsubmit="return check()" method="post" encType="multipart/form-data"
			runat="server">
			<input class="tx1" id="UpLoadFile" type="file" size="35" name="UpLoadFile" runat="server">
			<asp:button id="btnUpLoad" runat="server" Text="�ϴ�" onclick="btnUpLoad_Click"></asp:button><input 
id=DialogType type=hidden value="<%=strFileType%>" name=DialogType>
		</form>
	</body>
</HTML>
