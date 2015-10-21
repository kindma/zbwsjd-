<%@ Page Language="c#" Inherits="EasyExam.PersonInfo.JoinStudyList" CodeFile="JoinStudyList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
		<HEAD>
		<title>电子书籍</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
        <link rel="stylesheet" type="text/css" href="/common.css">
        <style type="text/css">
		 
		 *{ font-size:14px; }
        </style>
        <script language="JavaScript" src="../JavaScript/Common.js"></script>
		<script language="JavaScript">
			function RefreshForm()
			{
				document.all("hidcommand").value="RefreshForm";
				Form1.submit();
			}
		</script>
		</HEAD>
		<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
          <table height="563" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
            <tr>
              <td height="10"></td>
            </tr>
            <tr>
              <td valign="top"><!--内容开始-->
                
                <table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="523" cellSpacing="0"
							cellPadding="0" width="100%" align="left" border="1">
                  <tr>
                    <td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">电子书籍</font></td>
                  </tr>
                  <tr style="display:none;">
                    <td style="HEIGHT: 23px">&nbsp; 科目名称：
                      <asp:DropDownList id="DDLSubjectName" runat="server" Width="115px" AutoPostBack="True">
                        <asp:ListItem Value="0" Selected="True">--全部--</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;&nbsp;章名：
                      <asp:TextBox id="txtChapterName" runat="server" Width="115px"></asp:TextBox>
                      &nbsp;&nbsp;节名：
                      <asp:TextBox id="txtSectionName" runat="server" Width="115px"></asp:TextBox>
                      <asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>
                      <asp:Label id="LabCondition" runat="server" Visible="False">条件</asp:Label></td>
                  </tr>
                  <tr>
                    <td vAlign="top" align="center"><asp:datagrid id="DataGridBook" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="100" AllowSorting="True">
                        <ItemStyle Height="32px"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
                        <Columns>
                      <asp:BoundColumn Visible="False" DataField="SectionID" ReadOnly="True" HeaderText="SectionID"></asp:BoundColumn>
                      <asp:TemplateColumn HeaderText="序号">
                          <ItemTemplate> <%#RowNum++%> </ItemTemplate>
                        </asp:TemplateColumn>
                      <asp:BoundColumn DataField="SubjectName"  Visible="false" SortExpression="SubjectName" HeaderText="科目名称"></asp:BoundColumn>
                      <asp:BoundColumn DataField="ChapterName" Visible="false" SortExpression="ChapterName" HeaderText="章名"></asp:BoundColumn>
                      <asp:TemplateColumn SortExpression="SectionName" HeaderText="节名">
                          <ItemTemplate>
                          <asp:LinkButton id="LinkButSectionName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"SectionName") %>' CommandName="SectionName" CausesValidation="false" ToolTip="点击浏览"> </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateColumn>
                      <asp:BoundColumn DataField="BrowNumber"  Visible="false" SortExpression="BrowNumber" HeaderText="浏览次数"></asp:BoundColumn>
                      <asp:BoundColumn DataField="CreateLoginID"  Visible="false" SortExpression="CreateLoginID" HeaderText="创建人"></asp:BoundColumn>
                      <asp:TemplateColumn HeaderText="操作" Visible="false">
                          <HeaderStyle HorizontalAlign="Center" Width="35px"></HeaderStyle>
                          <ItemStyle HorizontalAlign="Center"></ItemStyle>
                          <ItemTemplate>
                          <asp:LinkButton id="LinkButBrowBook" runat="server"  Visible="false">浏览</asp:LinkButton>
                          &nbsp; </ItemTemplate>
                        </asp:TemplateColumn>
                      </Columns>
                        <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                      </asp:datagrid>
                      <div style="display:none;">
                      <FONT face="宋体">共有
                      <asp:Label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>
                      条记录&nbsp;
                      <asp:Label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>
                      页&nbsp;当前是第
                      <asp:Label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:Label>
                      页&nbsp;</FONT>
                      <asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>
                      &nbsp;
                      <asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>
                      &nbsp;
                      <asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>
                      &nbsp;
                      <asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
                      </div>
                  </tr>
                </table>
                
                <!--内容结束--></td>
            </tr>
          </table>
          <input type="hidden" size="4" name="hidcommand">
        </form>
</body>
</HTML>
