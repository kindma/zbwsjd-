<%@ Page language="c#" Inherits="EasyExam.PersonInfo.QueryGrade" CodeFile="QueryGrade.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>成绩查询</title>
<META http-equiv="Content-Type" content="text/html; charset=gb2312"> 
<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
<link rel="stylesheet" type="text/css" href="/common.css">
<style type="text/css">
*{ font-size:12px; line-height:1.5em; }
</style>
<script language="JavaScript" src="../JavaScript/Common.js"></script>
</HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <table height="540" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    <tr>
      <td height="10"></td>
    </tr>
    <tr>
      <td valign="top"><!--内容开始-->
        
        <table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
          <tr style="display:none;">
            <td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">
              <asp:label id="labPaperType" runat="server" ForeColor="White"></asp:label>
              成绩查询</font></td>
          </tr>
          <tr style="display:none;">
            <td style="HEIGHT: 22px">&nbsp; 试卷名称：
              <asp:textbox id="txtPaperName" runat="server" Width="152px" CssClass="text"></asp:textbox>
              &nbsp; 
              日期：从
              <asp:textbox id="txtStartTime" onclick="jcomOpenCalender('txtStartTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox>
              </FONT><A onClick="jcomOpenCalender('txtStartTime');" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A>到
              <asp:textbox id="txtEndTime" onclick="jcomOpenCalender('txtEndTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox>
              </FONT><A onClick="jcomOpenCalender('txtEndTime');" href="#"><IMG height="18" alt="选择" src="../images/Calendar.gif" width="22" border="0"></A>&nbsp;
              <asp:button id="ButQuery" runat="server" CssClass="button" Text="查 询" onclick="ButQuery_Click"></asp:button>
              <asp:label id="LabCondition" runat="server" Visible="False">条件</asp:label></td>
          </tr>
          <tr>
            <td vAlign="top" align="center">
            <asp:datagrid id="DataGridGrade" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="150" AllowSorting="True">
                <ItemStyle Height="32px"></ItemStyle>
                <HeaderStyle HorizontalAlign="Center" Height="32px" ForeColor="Black" CssClass="HeadRow" BackColor="#eeeeff"></HeaderStyle>
                <Columns>
                <asp:BoundColumn Visible="False" DataField="UserScoreID" ReadOnly="True" HeaderText="UserScoreID">
                  <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="PaperID" ReadOnly="True" HeaderText="PaperID"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="序号"  Visible="false" >
                  <ItemTemplate> <%#RowNum++%> </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="试卷名称"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="答题时间">
                  <ItemTemplate>
                    <asp:Label id="labExamTime" runat="server">答题时间</asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="TotalMark" HeaderText="成绩">
                  <ItemTemplate>
                    <asp:Label id="LabTotalMark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalMark") %>'> </asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="PassState" HeaderText="通过状态">
                  <ItemTemplate>
                    <asp:Label id="LabPassState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PassState") %>'> </asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="JudgeState" Visible="false"  SortExpression="JudgeState" HeaderText="评卷状态"></asp:BoundColumn>
                <asp:BoundColumn Visible="false"  DataField="JudgeLoginID" SortExpression="JudgeLoginID" HeaderText="评卷人"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="操作"  Visible="false" >
                  <HeaderStyle Width="90px"></HeaderStyle>
                  <ItemTemplate>
                    <asp:LinkButton Visible="false" id="LinkButOrder" runat="server" Text="排名" CommandName="Order" CausesValidation="false">排名</asp:LinkButton>
                    <asp:LinkButton id="LinkButAnswer" runat="server" Text="答卷" CommandName="Answer" CausesValidation="false">答卷</asp:LinkButton>
                    <asp:LinkButton id="LinkButStatis" runat="server" Text="统计" CommandName="Statis" CausesValidation="false">统计</asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="开始时间"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="结束时间"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="SeeResult" ReadOnly="True" HeaderText="查看结果"></asp:BoundColumn>
                </Columns>
                <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
              </asp:datagrid>
              <div style="display:none"><FONT face="宋体">共有
                <asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>
                条记录&nbsp;
                <asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>
                页&nbsp;当前是第
                <asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="宋体">0</asp:label>
                页&nbsp;</FONT>
                <asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton>
              </div></td>
          </tr>
        </table>
        
        <!--内容结束--></td>
    </tr>
  </table>
</form>
</body>
</HTML>
