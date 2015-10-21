<%@ Page Language="c#" Inherits="EasyExam.RubricManag.ManagBookTree" CodeFile="ManagBookTree.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ManagBookTree</title>
    <link href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
<%--
        <script language="JavaScript" src="../JavaScript/Common.js"></script>--%>

</head>
<body>
    <form id="Form1" runat="server">
        <table height="563" cellspacing="0" cellpadding="0" width="230" align="right" border="0">
            <tr>
                <td height="40">
                </td>
            </tr>
            <tr>
                <td>
                    <!--内容开始-->
                    <table style="width: 220px; border-collapse: collapse; height: 523px" bordercolor="#6699ff"
                        height="523" cellspacing="0" cellpadding="0" width="220" border="1" align="right">
                        <tr>
                            <td valign="top">
                                <%--<ie:TreeView id="TreeViewBook" runat="server" SystemImagesPath="../webctrl_client/1_0/treeimages/"
										ExpandedImageUrl="../webctrl_client/1_0/images/folderopen.gif" ImageUrl="../webctrl_client/1_0/images/folder.gif"
										SelectedImageUrl="../webctrl_client/1_0/images/folderopen.gif" Width="216px" Height="473px"></ie:TreeView>--%>
                                <asp:TreeView ID="TreeViewBook" runat="server" ShowLines="true" ExpandDepth="1" OnSelectedNodeChanged="TreeViewBook_SelectedNodeChanged">
                                    <SelectedNodeStyle BackColor="#E0E0E0" />
                                </asp:TreeView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="23">
                                <font face="宋体">章节名：</font>
                                <asp:TextBox ID="txtName" runat="server" Width="140px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" height="23">
                                <asp:Button ID="BubAdd" runat="server" Text="添　加" CssClass="button" OnClick="ButAdd_Click">
                                </asp:Button>
                                <asp:Button ID="ButEdit" runat="server" Text="修　改" CssClass="button" OnClick="ButEdit_Click">
                                </asp:Button>
                                <asp:Button ID="ButDel" runat="server" Text="删　除" CssClass="button" OnClick="ButDel_Click">
                                </asp:Button></td>
                        </tr>
                    </table>
                    <!--内容结束-->
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
