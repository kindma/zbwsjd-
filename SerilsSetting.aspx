<%@ Page Language="c#" Inherits="qphr.SerilsSetting" CodeFile="SerilsSetting.aspx.cs" %>
<HTML>
	<HEAD id="Head1">
		<title>  </title>
		<link href="Style/Style.css" type="text/css" rel="STYLESHEET">
	</HEAD>
	<body>
		<form id="form1" runat="server">
			<div><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT>
				<br>
				<br>
				<br>
				<br>
				<br>
				<br>
				<br>
				<br>
				<br>
				<table width="100%">
					<tr>
						<td width="250" height="20">
							您的机器码：</td>
						<td height="20">
							<asp:TextBox ID="TextBox1" runat="server" BorderWidth="0px" ReadOnly="True" Width="386px"></asp:TextBox></td>
						<td height="20">
						</td>
					</tr>
					<tr>
						<td width="250">
							序列号：</td>
						<td>
							<asp:TextBox ID="TextBox2" runat="server" Height="90px" TextMode="MultiLine" Width="464px"></asp:TextBox></td>
						<td>
						</td>
					</tr>
					<tr>
						<td width="250">
						</td>
						<td>
							<asp:Button ID="Button1" runat="server" Font-Bold="False" Height="27px" OnClick="Button1_Click"
								Text="写入序列号" Width="93px" /></td>
						<td>
						</td>
					</tr>
					<tr>
						<td width="250">
						</td>
						<td>
							<strong>
								<span style="COLOR: red">请联系我们获得软件安装序列号，联系电话：0731-88855718 &nbsp;&nbsp; 
                        &nbsp; QQ：956256325</span></strong></td>
						<td>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
