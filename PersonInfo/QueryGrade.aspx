<%@ Page language="c#" Inherits="EasyExam.PersonInfo.QueryGrade" CodeFile="QueryGrade.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>�ɼ���ѯ</title>
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
<dd class="webtitle">�Ͳ��������ල��<br>
���߿���ϵͳ</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
  <table height="540" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    <tr>
      <td height="10"></td>
    </tr>
    <tr>
      <td valign="top"><!--���ݿ�ʼ-->
        
        <table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
          <tr style="display:none;">
            <td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="����" color="#ffffff">
              <asp:label id="labPaperType" runat="server" ForeColor="White"></asp:label>
              �ɼ���ѯ</font></td>
          </tr>
          <tr style="display:none;">
            <td style="HEIGHT: 22px">&nbsp; �Ծ����ƣ�
              <asp:textbox id="txtPaperName" runat="server" Width="152px" CssClass="text"></asp:textbox>
              &nbsp; 
              ���ڣ���
              <asp:textbox id="txtStartTime" onclick="jcomOpenCalender('txtStartTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox>
              </FONT><A onClick="jcomOpenCalender('txtStartTime');" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A>��
              <asp:textbox id="txtEndTime" onclick="jcomOpenCalender('txtEndTime');" runat="server" Width="80px"
										CssClass="text" MaxLength="50"></asp:textbox>
              </FONT><A onClick="jcomOpenCalender('txtEndTime');" href="#"><IMG height="18" alt="ѡ��" src="../images/Calendar.gif" width="22" border="0"></A>&nbsp;
              <asp:button id="ButQuery" runat="server" CssClass="button" Text="�� ѯ" onclick="ButQuery_Click"></asp:button>
              <asp:label id="LabCondition" runat="server" Visible="False">����</asp:label></td>
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
                <asp:TemplateColumn HeaderText="���"  Visible="false" >
                  <ItemTemplate> <%#RowNum++%> </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="PaperName" SortExpression="PaperName" HeaderText="�Ծ�����"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="����ʱ��">
                  <ItemTemplate>
                    <asp:Label id="labExamTime" runat="server">����ʱ��</asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="TotalMark" HeaderText="�ɼ�">
                  <ItemTemplate>
                    <asp:Label id="LabTotalMark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalMark") %>'> </asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="PassState" HeaderText="ͨ��״̬">
                  <ItemTemplate>
                    <asp:Label id="LabPassState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PassState") %>'> </asp:Label>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="JudgeState" Visible="false"  SortExpression="JudgeState" HeaderText="����״̬"></asp:BoundColumn>
                <asp:BoundColumn Visible="false"  DataField="JudgeLoginID" SortExpression="JudgeLoginID" HeaderText="������"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="����"  Visible="false" >
                  <HeaderStyle Width="90px"></HeaderStyle>
                  <ItemTemplate>
                    <asp:LinkButton Visible="false" id="LinkButOrder" runat="server" Text="����" CommandName="Order" CausesValidation="false">����</asp:LinkButton>
                    <asp:LinkButton id="LinkButAnswer" runat="server" Text="���" CommandName="Answer" CausesValidation="false">���</asp:LinkButton>
                    <asp:LinkButton id="LinkButStatis" runat="server" Text="ͳ��" CommandName="Statis" CausesValidation="false">ͳ��</asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="False" DataField="StartTime" ReadOnly="True" HeaderText="��ʼʱ��"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="EndTime" ReadOnly="True" HeaderText="����ʱ��"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="SeeResult" ReadOnly="True" HeaderText="�鿴���"></asp:BoundColumn>
                </Columns>
                <PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
              </asp:datagrid>
              <div style="display:none"><FONT face="����">����
                <asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>
                ����¼&nbsp;
                <asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>
                ҳ&nbsp;��ǰ�ǵ�
                <asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="����">0</asp:label>
                ҳ&nbsp;</FONT>
                <asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">��һҳ</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">��һҳ</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">��һҳ</asp:linkbutton>
                &nbsp;
                <asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">���ҳ</asp:linkbutton>
              </div></td>
          </tr>
        </table>
        
        <!--���ݽ���--></td>
    </tr>
  </table>
</form>
</body>
</HTML>
