<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrBook.aspx.cs" Inherits="PersonInfo_ErrBook" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head>
		<title>参加考试</title>
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
        <script language=javascript>

            function openExam() {

                var typeCnt = document.getElementById("hdCnt").value;

                var putfor = "";
                                
                for(var i=0;i<typeCnt;i++) {

                    var currObj = document.getElementById("curr" + i).value;
                    var cntObj = document.getElementById("cnt"+i).value;
                    var hdObj = document.getElementById("hd" + i).value;
                                                                          
                    if (parseInt(currObj) != 0) {
                        if (parseInt(currObj) < 0 || parseInt(currObj) > parseInt(cntObj)) {
                            alert("练习题数不能大于或小于原有题数.");
                            return;
                        }
                    }

                    if (parseInt(currObj) != 0) {
                        if (putfor == "") {
                            putfor = hdObj + ":" + currObj + ":" + cntObj;
                        }
                        else {
                            putfor = putfor + "," + hdObj + ":" + currObj + ":" + cntObj;
                        }
                    }
                }

                if (putfor != "") {
                    window.open("ErrExamAll.aspx?putfor=" + putfor);
                }
                else {
                    alert("请选择题目.");
                }
//                alert(putfor)
            }
        
        
        </script>
	</head>
	<body leftMargin="0" topMargin="0" rightMargin="0"><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><form id="Form1" method="post" runat="server">
			<table height="540" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr>
					<td height="40" align="center">
                        <label >类型</label>
                        <asp:DropDownList ID="DropDownList" runat="server" Width="150px"></asp:DropDownList>&nbsp;
                        <asp:Button ID="btnSelect" runat="server" Text="查询" />&nbsp;
                        <input type="button" name ="btnOpen" value="练习" onclick="openExam()" />
                    </td>
				</tr>
                <tr>
                     <td>
                        <%=testTypeContext%>
                    </td>
                </tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="500" cellSpacing="0"
							cellPadding="0" width="780" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">错题卡</font></td>
							</tr>
							<tr>
								<td vAlign="top" align="center"><asp:datagrid id="DataGridPaper" runat="server" AutoGenerateColumns="False" AllowPaging="True"
										BorderWidth="1px" CellPadding="0" PageSize="15" Width="100%" AllowSorting="True">
										<ItemStyle Height="23px"></ItemStyle>
										<headerStyle HorizontalAlign="Center" Height="23px" ForeColor="Black" CssClass="HeadRow" BackColor="#076AA3"></HeaderStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="RubricID" ReadOnly="True" HeaderText="RubricID">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" DataField="StandardAnswer" ReadOnly="True" HeaderText="StandardAnswer"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="序号">
												<ItemTemplate>
													<%#RowNum++%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="TestContent" SortExpression="TestContent" HeaderText="试题名称"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="cnt" SortExpression="cnt" HeaderStyle-Width="60" HeaderText="出错次数"><ItemStyle HorizontalAlign="Center"></ItemStyle></asp:BoundColumn>											
											<asp:TemplateColumn HeaderText="操作">
												<headerStyle Width="60px"></HeaderStyle>
                                                <ItemTemplate>
													<asp:LinkButton id="LinkButSeeTest" runat="server" Text="查看" CommandName="SeeTest" CausesValidation="false">查看</asp:LinkButton>												
													<asp:LinkButton id="LinkButDelExam" runat="server" Text="删除" CommandName="DelExam" CausesValidation="false">删除</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>										
										</Columns>
										<PagerStyle Height="23px" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><font face="宋体">共有<asp:label id="LabelRecord" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>条记录&nbsp;<asp:label id="LabelCountPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;当前是第<asp:label id="LabelCurrentPage" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="True">0</asp:label>页&nbsp;</FONT>
									<asp:linkbutton id="LinkButFirstPage" runat="server" onclick="LinkButFirstPage_Click">第一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButPirorPage" runat="server" onclick="LinkButPirorPage_Click">上一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButNextPage" runat="server" onclick="LinkButNextPage_Click">下一页</asp:linkbutton>&nbsp;
									<asp:linkbutton id="LinkButLastPage" runat="server" onclick="LinkButLastPage_Click">最后页</asp:linkbutton></td>
							</tr>
						</table>
						<!--内容结束-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

