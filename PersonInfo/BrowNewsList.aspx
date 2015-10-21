<%@ Page language="c#" Inherits="EasyExam.PersonInfo.BrowNewsList" CodeFile="BrowNewsList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>���Ź���</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<meta content="JavaScript" name="vs_defaultClientScript">
<script language="JavaScript" src="../JavaScript/Common.js"></script>
<script src="/jquery-1.11.3.min.js" type="text/javascript"></script>
</HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">�Ͳ��������ල��<br>
���߿���ϵͳ</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <asp:datagrid id="DataGridNews" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" ForeColor="Black" AllowSorting="True">
    <ItemStyle Height="32px"></ItemStyle>
    <HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
    <Columns>
    <asp:BoundColumn Visible="False" DataField="NewsID" ReadOnly="True" HeaderText="NewsID"></asp:BoundColumn>
    <asp:TemplateColumn Visible="false" HeaderText="&lt;IMG src='../images/arrow.gif'' border='0'">
      <ItemStyle HorizontalAlign="Center"></ItemStyle>
      <ItemTemplate>
        <asp:image id="ImgArrow" runat="server" ImageUrl="../Images/arrow.gif"></asp:image>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn Visible="false" HeaderText="���">
      <ItemTemplate> <%#RowNum++%> </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn SortExpression="NewsTitle" HeaderText="����">
      <ItemTemplate>
        <asp:LinkButton id="LinkButNewsTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NewsTitle") %>' CommandName="NewsTitle" CausesValidation="false" ToolTip="������"> </asp:LinkButton>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn Visible="false" DataField="BrowNumber" SortExpression="BrowNumber" HeaderText="�������"></asp:BoundColumn>
    <asp:BoundColumn Visible="false" DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="������"></asp:BoundColumn>
    <asp:TemplateColumn SortExpression="CreateDate" HeaderText="��������">
      <ItemTemplate>
        <asp:Label id="labCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreateDate") %>'> </asp:Label>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn Visible="false" HeaderText="����">
      <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
      <ItemStyle HorizontalAlign="Center"></ItemStyle>
      <ItemTemplate>
        <asp:LinkButton id="LinkButBrowNews" runat="server" Text="���" CommandName="BrowNews" CausesValidation="false">���</asp:LinkButton>
      </ItemTemplate>
    </asp:TemplateColumn>
    </Columns>
    <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
  </asp:datagrid>
  <div style="display:none;"
  <FONT face="����">����
  <asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>
  ����¼&nbsp;
  <asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>
  ҳ&nbsp;��ǰ�ǵ�
  <asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:Label>
  ҳ&nbsp;</FONT>
  <asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>
  &nbsp;
  <asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>
  &nbsp;
  <asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>
  &nbsp;
  <asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton>
  </div>
  <script>
var $table = $('#DataGridNews');
$table.css("display","none");
var rows = $table.find('tr').length-1;
 
var cols = $table.find('tr:eq(0)').find('td').length;

 for(y=1;y<rows;y++){
	for(x=0;x<cols;x++){
 		 titlestr=$table.find('tr:eq(0)').find('td:eq(' + x + ')').text();
		 opcon=$table.find('tr:eq(' + y + ')').find('td:eq(' + x + ')').html();
		 
		 document.writeln(titlestr+":");
		 document.writeln(opcon+"<br />");
	  
	}
		 document.writeln("<hr />");
 }

  
</script>
</form>
</body>
</HTML>
