<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.JoinExam" CodeFile="JoinExam.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>�μӿ���</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<style type="text/css">
.beginks {
	width: 100px;
	height: 40px;
	padding: 10px;
	font-size: 20px;
	border: 1px solid #555;
	background-color: #eeeeee;
}
</style>
<script src="/jquery-1.11.3.min.js" type="text/javascript"></script>
</HEAD>
<body ><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">�Ͳ��������ල��<br>
���߿���ϵͳ</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <asp:datagrid id="DataGridPaper" runat="server" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" Width="100%" AllowSorting="True">
    <ItemStyle Height="32px"></ItemStyle>
    <HeaderStyle HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
    <Columns>
    <asp:BoundColumn Visible="False" DataField="PaperID" ReadOnly="True" HeaderText="PaperID">
      <ItemStyle HorizontalAlign="Center"></ItemStyle>
    </asp:BoundColumn>
    <asp:TemplateColumn  Visible="False"  HeaderText="���">
      <ItemTemplate> <%#RowNum++%> </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="�Ծ�����"></asp:BoundColumn>
    <asp:BoundColumn DataField="ProduceWay" SortExpression="ProduceWay" HeaderText="���ⷽʽ"></asp:BoundColumn>
    <asp:BoundColumn DataField="ExamTime" SortExpression="ExamTime" HeaderText="����ʱ��(����)"></asp:BoundColumn>
    <asp:TemplateColumn  Visible="False" HeaderText="��Чʱ��">
      <ItemTemplate>
        <asp:Label id="labAvaiTime" runat="server">��Чʱ��</asp:Label>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn DataField="TestCount" SortExpression="TestCount" HeaderText="����"></asp:BoundColumn>
    <asp:BoundColumn DataField="PaperMark" SortExpression="PaperMark" HeaderText="�ܷ�"></asp:BoundColumn>
    <asp:BoundColumn  Visible="False"  DataField="CreateLoginID" SortExpression="CreateLoginID" HeaderText="������"></asp:BoundColumn>
    <asp:TemplateColumn HeaderText="����">
      <HeaderStyle Width="60px"></HeaderStyle>
      <ItemTemplate>
        <asp:LinkButton Visible="false" id="LinkButStartExam2"   runat="server" Text="��ʼ����"  CausesValidation="false">��ʼ����</asp:LinkButton>
        <asp:Label id="LinkButStartExam" CssClass="beginks"   runat="server" Text="��ʼ����">��ʼ����</asp:Label>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn Visible="False" DataField="PaperType" ReadOnly="True" HeaderText="�Ծ�����"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="��ʼʱ��"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="CreateWay" ReadOnly="True" HeaderText="���ʽ"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="ShowModal" ReadOnly="True" HeaderText="��ʾģʽ"></asp:BoundColumn>
    </Columns>
    <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
  </asp:datagrid>
  <div style="display:none;"> <FONT face="����">����
    <asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>
    ����¼&nbsp;
    <asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>
    ҳ&nbsp;��ǰ�ǵ�
    <asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="����" Font-Bold="True">0</asp:label>
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
var $table = $('#DataGridPaper');
$table.css("display","none");
var rows = $table.find('tr').length;
 
var cols = $table.find('tr:eq(0)').find('td').length;
 
for(x=0;x<cols;x++){
  for(y=0;y<2;y++){
	  
	 opstr=$table.find('tr:eq(' + y + ')').find('td:eq(' + x + ')').text();
	 
	 if(opstr.indexOf("��ʼ����")>0) document.writeln($table.find('tr:eq(' + y + ')').find('td:eq(' + x + ')').html());
	 else document.writeln(opstr);
	 
	if(y==0) document.write(":");	 
	 
  }
 
	document.writeln("<br>");
}
</script>
</form>
</body>
</HTML>
