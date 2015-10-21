<%@ Page language="c#" Inherits="EasyExam.RubricManag.NewTest" CodeFile="NewTest.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>新建试题</title>
		<meta content="Microsoft FrontPage 6.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
				function TestTypeNameChange()
				{
				    var i;
					var strTestTypeName=Form1.DDLTestTypeName.value;
					document.all("OptionContent").style.display="none";
					document.all("PlSelect").style.display="none";
					document.all("PlJudge").style.display="none";
					document.all("PlOther").style.display="none";
					document.all("PlType").style.display="none";
					document.all("PlOperate").style.display="none";
					if (strTestTypeName.indexOf("单选类")>0)
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="试题选项：";
						document.all("PlSelect").style.display="block";
						for(i=1;i<=6;i++)
						{
							document.all("PlOneSelect"+i).style.display="block";
							document.all("PlMultiSelect"+i).style.display="none";
						}
						document.all("txtTestMark").value="2";
					}
					if (strTestTypeName.indexOf("多选类")>0)
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="试题选项：";
						document.all("PlSelect").style.display="block";
						for(i=1;i<=6;i++)
						{
							document.all("PlOneSelect"+i).style.display="none";
							document.all("PlMultiSelect"+i).style.display="block";
						}
						document.all("txtTestMark").value="2";
					}
					if (strTestTypeName.indexOf("判断类")>0)
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="标准答案：";
						document.all("PlJudge").style.display="block";
						document.all("txtTestMark").value="2";
					}
					if ((strTestTypeName.indexOf("填空类")>0)||(strTestTypeName.indexOf("问答类")>0)||(strTestTypeName.indexOf("作文类")>0))
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="标准答案：";
						document.all("PlOther").style.display="block";
						document.all("txtTestMark").value="10";
					}
					if (strTestTypeName.indexOf("打字类")>0)
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="试题参数：";
						document.all("PlType").style.display="block";
						document.all("txtTestMark").value="10";
					}
					if (strTestTypeName.indexOf("操作类")>0)
					{
						document.all("OptionContent").style.display="block";
						document.all("LabStandardAnswer").innerHTML="下载文件：";
						document.all("PlOperate").style.display="block";
						document.all("txtTestMark").value="10";
					}
				}
				function OptionNumChange()
				{
					var i;
					var num=Form1.DDLOptionNum.value;
					for(i=1;i<=6;i++)
					{
						document.all("trOneSelect"+i).style.display="none";
					}
					for(i=1;i<=num;i++)
					{
						document.all("trOneSelect"+i).style.display="block";
					}
				}
				function OpenHtmlEditor(index)
				{
					var win,width,height,left,top;
					document.all("txtEditorIndex").value=index;
					width=475;
					height=319;
					left=(screen.width-width)/2;
					if (left<0)	{ left=0;}
					top=(screen.height-60-height)/2;
					if (top<0) { top=0;}
					win=window.open("HtmlEditor.aspx","HtmlEditor","toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no,status=yes,width="+width+",height="+height+",left="+left+",top="+top);
				}
				function SetHtmlEditorText()
				{
					return document.all("txtTestContent"+document.all("txtEditorIndex").value).value;
				}
				function HtmlEditorCallBack(text){
					document.all("txtTestContent"+document.all("txtEditorIndex").value).value=text;
				}
		</script>
		<LINK href="../CSS/STYLE.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="200" cellSpacing="0" cellPadding="0" width="670" align="center" border="0">
				<tr>
					<td height="0"></td>
				</tr>
				<tr>
					<td>
						<!--内容开始-->
						<table style="BORDER-COLLAPSE: collapse" borderColor="#6699ff" height="200" cellSpacing="0"
							cellPadding="0" width="100%" align="center" border="1">
							<tr>
								<td style="COLOR: #990000" align="center" background="../Images/bg_dh_middle.gif" bgColor="#ffffff"
									height="24" colspan="6"><font style="FONT-SIZE: 16px" face="黑体" color="#ffffff">新建试题</font></td>
							</tr>
							<TR>
								<td style="WIDTH: 75px" align="right" colSpan="1" height="23" rowSpan="1">科目名称：</td>
								<td width="148" height="23"><FONT face="宋体"><asp:dropdownlist id="DDLSubjectName" runat="server" AutoPostBack="True" Width="110px" onselectedindexchanged="DDLSubjectName_SelectedIndexChanged">
											<asp:ListItem Value="0">--请选择--</asp:ListItem>
										</asp:dropdownlist></FONT></td>
								<td style="WIDTH: 75px" align="right" height="23">
									知识点：</td>
								<td width="148" height="23"><FONT face="宋体"><asp:dropdownlist id="DDLLoreName" runat="server" Width="110px">
											<asp:ListItem Value="0">--请选择--</asp:ListItem>
										</asp:dropdownlist></FONT></td>
								<td style="WIDTH: 75px" align="right" height="23">题型名称：</td>
								<td width="148" height="23"><asp:dropdownlist id="DDLTestTypeName" runat="server" Width="110px" onchange="TestTypeNameChange()">
										<asp:ListItem Value="0">--请选择--</asp:ListItem>
									</asp:dropdownlist></td>
							</TR>
							<TR>
								<td style="WIDTH: 75px; HEIGHT: 23px" align="right" height="19">试题难度：</td>
								<td height="19" width="148"><asp:dropdownlist id="DDLTestDiff" runat="server" Width="110px">
										<asp:ListItem Value="易">易</asp:ListItem>
										<asp:ListItem Value="较易">较易</asp:ListItem>
										<asp:ListItem Value="中等" Selected="True">中等</asp:ListItem>
										<asp:ListItem Value="较难">较难</asp:ListItem>
										<asp:ListItem Value="难">难</asp:ListItem>
									</asp:dropdownlist></td>
								<td style="WIDTH: 75px; HEIGHT: 23px" align="right" height="19">
									选项数目：</td>
								<td height="19" width="148"><asp:dropdownlist id="DDLOptionNum" runat="server" Width="110px" onchange="OptionNumChange()">
										<asp:ListItem Value="2">2</asp:ListItem>
										<asp:ListItem Value="3">3</asp:ListItem>
										<asp:ListItem Value="4" Selected="True">4</asp:ListItem>
										<asp:ListItem Value="5">5</asp:ListItem>
										<asp:ListItem Value="6">6</asp:ListItem>
									</asp:dropdownlist></td>
								<td style="WIDTH: 75px; HEIGHT: 23px" align="right" height="19">试题分数：</td>
								<td height="19" width="148"><FONT face="宋体"><asp:textbox id="txtTestMark" runat="server" Width="110px"></asp:textbox></FONT></td>
							</TR>
							<TR>
								<td style="WIDTH: 75px" align="right" height="23">试题内容：</td>
								<td width="586" colSpan="6" height="23" style="WORD-BREAK: break-all">
									<TABLE id="table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR runat="server">
											<TD>
												<ASP:TEXTBOX id="txtTestContent0" runat="server" Width="100%" Height="65px" TextMode="MultiLine"
													CssClass="Text"></ASP:TEXTBOX></TD>
											<TD vAlign="top" width="16"><A href="javascript:OpenHtmlEditor(0)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
							<TR id="OptionContent" style="DISPLAY: none">
								<td style="WIDTH: 75px" align="right" height="23"><asp:label id="LabStandardAnswer" runat="server" Height="12px">试题选项：</asp:label></td>
								<td width="586" colSpan="5" style="WORD-BREAK: break-all">
									<asp:panel id="PlSelect" style="DISPLAY: none" runat="server" Width="100%">
										<TABLE id="SelectTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR id="trOneSelect1" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect1" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect1" runat="server" GroupName="Select_Option" TextAlign="Left" Text="A"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect1" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect1" runat="server" TextAlign="Left" Text="A"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent1" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(1)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
											<TR id="trOneSelect2" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect2" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect2" runat="server" GroupName="Select_Option" TextAlign="Left" Text="B"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect2" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect2" runat="server" TextAlign="Left" Text="B"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent2" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(2)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
											<TR id="trOneSelect3" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect3" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect3" runat="server" GroupName="Select_Option" TextAlign="Left" Text="C"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect3" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect3" runat="server" TextAlign="Left" Text="C"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent3" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(3)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
											<TR id="trOneSelect4" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect4" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect4" runat="server" GroupName="Select_Option" TextAlign="Left" Text="D"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect4" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect4" runat="server" TextAlign="Left" Text="D"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent4" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(4)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
											<TR id="trOneSelect5" style="DISPLAY: none" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect5" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect5" runat="server" GroupName="Select_Option" TextAlign="Left" Text="E"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect5" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect5" runat="server" TextAlign="Left" Text="E"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent5" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(5)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
											<TR id="trOneSelect6" style="DISPLAY: none" runat="server">
												<TD style="WIDTH: 32px">
													<asp:panel id="PlOneSelect6" runat="server" Width="100%">
														<ASP:RADIOBUTTON id="rbOneSelect6" runat="server" GroupName="Select_Option" TextAlign="Left" Text="F"></ASP:RADIOBUTTON>
													</asp:panel>
													<asp:panel id="PlMultiSelect6" runat="server" Width="100%">
														<ASP:CHECKBOX id="chkMultiSelect6" runat="server" TextAlign="Left" Text="F"></ASP:CHECKBOX>
													</asp:panel></TD>
												<TD>
													<ASP:TEXTBOX id="txtTestContent6" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(6)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
										</TABLE>
									</asp:panel>
									<asp:panel id="PlJudge" style="DISPLAY: none" runat="server" Width="100%">
										<TABLE id="JudgeTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR id="trJudge" runat="server">
												<TD>
													<ASP:RADIOBUTTON id="rbJudgeRight" runat="server" GroupName="Judge_Option" Text="正确"></ASP:RADIOBUTTON>
													<ASP:RADIOBUTTON id="rbJudgeWrong" runat="server" GroupName="Judge_Option" Text="错误"></ASP:RADIOBUTTON></TD>
											</TR>
										</TABLE>
									</asp:panel>
									<asp:panel id="PlOther" style="DISPLAY: none" runat="server" Width="100%">
										<TABLE id="OtherTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR id="trOther" runat="server">
												<TD>
													<ASP:TEXTBOX id="txtTestContent7" runat="server" Width="100%" CssClass="Text" TextMode="MultiLine"
														Height="55px" ToolTip="填空类试题内容中用3个下划线“_”表示填空位置，各空答案间用半角逗号“,”隔开"></ASP:TEXTBOX></TD>
												<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(7)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
											</TR>
										</TABLE>
									</asp:panel>
									<asp:panel id="PlType" style="DISPLAY: none" runat="server" Width="100%">
										<TABLE id="TypeTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR id="trType" runat="server">
												<TD>打字时间：
													<asp:TextBox id="txtTypeTime" runat="server" Width="32px" CssClass="text">5</asp:TextBox>分钟&nbsp;&nbsp; 
													标准速度：
													<asp:TextBox id="txtStandardSpeed" runat="server" Width="32px" CssClass="text">30</asp:TextBox>个字/分钟</TD>
											</TR>
										</TABLE>
									</asp:panel>
									<asp:panel id="PlOperate" style="DISPLAY: none" runat="server" Width="100%">
										<TABLE id="OperateTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR id="trOperate" runat="server">
												<TD><INPUT class="text" id="TestFile" style="WIDTH: 264px; HEIGHT: 22px" type="file" size="24"
														name="TestFile" runat="server">
													<asp:linkbutton id="lbtTestFile" runat="server" ToolTip="点击下载" Visible="False">lbtTestFile</asp:linkbutton></TD>
											</TR>
										</TABLE>
									</asp:panel></td>
							</TR>
							<TR>
								<td style="WIDTH: 75px; HEIGHT: 23px" align="right">试题解析：</td>
								<td style="HEIGHT: 23px" width="586" colSpan="5" style="WORD-BREAK: break-all">
									<TABLE id="TestParseTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR id="trTestParse" runat="server">
											<TD>
												<ASP:TEXTBOX id="txtTestContent8" runat="server" Width="100%" Height="55px" TextMode="MultiLine"
													CssClass="Text"></ASP:TEXTBOX></TD>
											<TD vAlign="top" width="15"><A href="javascript:OpenHtmlEditor(8)"><IMG title="HTML编辑器" height="16" src="../Images/Html_Editor.GIF" width="16" border="0"></A></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
							<TR>
								<td style="WIDTH: 75px" align="right" height="23">创建日期：</td>
								<td width="586" colSpan="5" height="23"><asp:textbox id="txtCreateDate" runat="server" Width="80px" Enabled="False"></asp:textbox><INPUT id="txtEditorIndex" style="WIDTH: 26px; HEIGHT: 21px" type="hidden" size="1" name="txtEditorIndex"></td>
							</TR>
							<tr height="30">
								<td align="center" width="100%" colSpan="6" height="23">
									<A href="../MainFrm.aspx"></A>
									<asp:button id="ButInput" runat="server" CssClass="button" Text="提 交" onclick="ButInput_Click"></asp:button>&nbsp;
									<INPUT class="button" onclick="window.close();" type="button" value="取 消" name="Button"></td>
							</tr>
						</table>
					</td>
					<td width="1" height="24"></td>
				</tr>
			</table>
			<!--内容结束--> </TD></TR></TABLE>
		</form>
		<script language="javascript">
			TestTypeNameChange();
			OptionNumChange();
		</script>
	</body>
</HTML>
