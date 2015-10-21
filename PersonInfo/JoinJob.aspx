<%@ Page language="c#" Inherits="EasyExam.PersonalInfo.JoinJob" CodeFile="JoinJob.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>模拟考试/作业</title>
<meta content="C#" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<META http-equiv="Content-Type" content="text/html; charset=gb2312">
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<style type="text/css">
#DataGridPaper__ctl3_LinkButStartExam {
	border: 1px solid #444;
	padding: 5px;
	background: #eaeaea;
}
</style>
<script src="/jquery-1.11.3.min.js" type="text/javascript"></script>
</HEAD>
<body ><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <asp:datagrid id="DataGridPaper" runat="server" Width="100%" PageSize="100" CellPadding="0" BorderWidth="1px"
										AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True">
    <ItemStyle  ></ItemStyle>
    <HeaderStyle HorizontalAlign="Center"  ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
    <Columns>
    <asp:BoundColumn Visible="False" DataField="PaperID" ReadOnly="True" HeaderText="PaperID">
      <ItemStyle HorizontalAlign="Center"></ItemStyle>
    </asp:BoundColumn>
    <asp:TemplateColumn Visible="False" HeaderText="序号">
      <ItemTemplate> <%#RowNum++%> </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="试卷名称"></asp:BoundColumn>
    <asp:BoundColumn DataField="ProduceWay" SortExpression="ProduceWay" HeaderText="出题方式"></asp:BoundColumn>
    <asp:BoundColumn DataField="ShowModal" SortExpression="ShowModal" HeaderText="显示模式"></asp:BoundColumn>
    <asp:TemplateColumn Visible="False" HeaderText="有效时间">
      <ItemTemplate>
        <asp:Label id="labAvaiTime" runat="server">有效时间</asp:Label>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn DataField="TestCount" SortExpression="TestCount" HeaderText="题量"></asp:BoundColumn>
    <asp:BoundColumn DataField="PaperMark" SortExpression="PaperMark" HeaderText="总分"></asp:BoundColumn>
    <asp:BoundColumn DataField="CreateLoginID" Visible="false" SortExpression="CreateLoginID" HeaderText="出卷人"></asp:BoundColumn>
    <asp:TemplateColumn HeaderText="操作">
      <HeaderStyle Width="60px"></HeaderStyle>
      <ItemTemplate>
        <asp:LinkButton id="LinkButStartExam2" Visible="false" runat="server" Text="开始作业" CommandName="StartExam" CausesValidation="false">开始模拟考试</asp:LinkButton>
        <asp:Label id="LinkButStartExam" CssClass="beginks"   runat="server" Text="开始模拟考试">开始模拟考试/作业</asp:Label>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:BoundColumn Visible="False" DataField="PaperType" ReadOnly="True" HeaderText="试卷类型"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="开始时间"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="结束时间"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="CreateWay" ReadOnly="True" HeaderText="组卷方式"></asp:BoundColumn>
    <asp:BoundColumn Visible="False" DataField="ShowModal" ReadOnly="True" HeaderText="显示模式"></asp:BoundColumn>
    </Columns>
    <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
  </asp:datagrid>
  <div style="display:none"> <FONT face="宋体">共有
    <asp:label id="LabelRecord" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>
    条记录&nbsp;
    <asp:label id="LabelCountPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>
    页&nbsp;当前是第
    <asp:label id="LabelCurrentPage" runat="server" Font-Bold="True" Font-Names="宋体" ForeColor="Blue">0</asp:label>
    页&nbsp;</FONT>
    <asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>
    &nbsp;
    <asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>
    &nbsp;
    <asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>
    &nbsp;
    <asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton>
  </div>
  <div id="ex"></div>
</form>
<script>
var $table = $('#DataGridPaper');
$table.css("display","none");
var rows = $table.find('tr').length;
 
var cols = $table.find('tr:eq(0)').find('td').length;
 
for(x=0;x<cols;x++){
  for(y=0;y<2;y++){
	  
       opstr=$table.find('tr:eq(' + y + ')').find('td:eq(' + x + ')').text();
	 
	 	 if(opstr.indexOf("开始模拟")>0) document.writeln($table.find('tr:eq(' + y + ')').find('td:eq(' + x + ')').html());
	 else document.writeln(opstr);
	 
	 
	if(y==0) document.write(":");	 
	 
  }
  	document.writeln("<br>"); 
}
</script>
</body>
</HTML>
