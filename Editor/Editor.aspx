<%@ Page Language="c#" Inherits="EasyExam.Editor.Editor" CodeFile="Editor.aspx.cs" %>
<HTML>
	<HEAD>
		<title>HTML���߱༭��</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<link rel="STYLESHEET" type="text/css" href="Css/editor.css">
			<script language="JavaScript" type="text/JavaScript">
//�˵��б�
var menu_table="<table width='100%' cellspacing='0' cellpadding='2'>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_cr.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertTable()'>������</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_sx.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tableProp()'>�������</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_sx2.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='cellProp()'>��Ԫ������</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_tr.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(1)'>����һ��</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_trdel.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(2)'>ɾ��һ��</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_td.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(3)'>����һ��</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_tddel.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(4)'>ɾ��һ��</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_hby.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(5)'>���Һϲ�</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_hbx.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(6)'>���ºϲ�</a></td></tr>";
menu_table+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/table_cf.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='tablecommand(7)'>��ֵ�Ԫ��</a></td></tr>";
menu_table+="</table>";
var menu_chars="<table width='100%' cellspacing='0' cellpadding='2'>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars1.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(0)'>���з�</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars2.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(1)'>��Ȩ����</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars3.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(2)'>ע���̱�</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars4.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(3)'>�̱����</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars5.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(4)'>Բ��</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars6.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(5)'>ʡ�Ժ�</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars7.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(6)'>���ۺ�</a></td></tr>";
menu_chars+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/chars8.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertChars(7)'>�л���</a></td></tr>";
menu_chars+="</table>";
var menu_eq="<table width='100%' cellspacing='0' cellpadding='2'>";
menu_eq+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/eq1.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InsertEQ()'>���빫ʽ</a></td></tr>";
menu_eq+="<tr onmouseout='scolor(this)' onmouseover='rcolor(this)'><td><img src='Images/eq2.gif' width='16' height='16' align='absmiddle'></td><td><a href='#' onclick='InstallEQ()'>��װ��ʽ�༭�����</a></td></tr>";
menu_eq+="</table>";

//�����˵���ش���
 var h;
 var w;
 var l;
 var t;
 var topMar = 1;
 var leftMar = -2;
 var space = 1;
 var isvisible;
 var MENU_SHADOW_COLOR='#E1F4EE';//���������˵���Ӱɫ
 var global = window.document
 global.fo_currentMenu = null
 global.fo_shadows = new Array

function HideMenu() 
{
 var mX;
 var mY;
 var vDiv;
 var mDiv;
	if (isvisible == true)
{
		vDiv = document.all("menuDiv");
		mX = window.event.clientX + document.body.scrollLeft;
		mY = window.event.clientY + document.body.scrollTop;
		if ((mX < parseInt(vDiv.style.left)) || (mX > parseInt(vDiv.style.left)+vDiv.offsetWidth) || (mY < parseInt(vDiv.style.top)-h) || (mY > parseInt(vDiv.style.top)+vDiv.offsetHeight)){
			vDiv.style.visibility = "hidden";
			isvisible = false;
		}
}
}

function ShowMenu(vMnuCode,tWidth) {
	vSrc = window.event.srcElement;
	vMnuCode = "<table id='submenu' cellspacing=1 cellpadding=3 style='width:"+tWidth+"' class=menu onmouseout='HideMenu()'><tr height=23><td nowrap align=left class=MenuBody>" + vMnuCode + "</td></tr></table>";

	h = vSrc.offsetHeight;
	w = vSrc.offsetWidth;
	l = vSrc.offsetLeft + leftMar+4;
	t = vSrc.offsetTop + topMar + h + space-2;
	vParent = vSrc.offsetParent;
	while (vParent.tagName.toUpperCase() != "BODY")
	{
		l += vParent.offsetLeft;
		t += vParent.offsetTop;
		vParent = vParent.offsetParent;
	}

	menuDiv.innerHTML = vMnuCode;
	menuDiv.style.top = t;
	menuDiv.style.left = l;
	menuDiv.style.visibility = "visible";
	isvisible = true;
    makeRectangularDropShadow(submenu, MENU_SHADOW_COLOR, 4)
}

function makeRectangularDropShadow(el, color, size)
{
	var i;
	for (i=size; i>0; i--)
	{
		var rect = document.createElement('div');
		var rs = rect.style
		rs.position = 'absolute';
		rs.left = (el.style.posLeft + i) + 'px';
		rs.top = (el.style.posTop + i) + 'px';
		rs.width = el.offsetWidth + 'px';
		rs.height = el.offsetHeight + 'px';
		rs.zIndex = el.style.zIndex - i;
		rs.backgroundColor = color;
		var opacity = 1 - i / (i + 1);
		rs.filter = 'alpha(opacity=' + (100 * opacity) + ')';
		el.insertAdjacentElement('afterEnd', rect);
		global.fo_shadows[global.fo_shadows.length] = rect;
	}
}
function scolor(obj)
{
  obj.style.backgroundColor="";
}
function rcolor(obj)
{
  obj.style.backgroundColor="#E1F4EE";
}
			</script>
	</HEAD>
	<body bgcolor="#ffffff" leftmargin='0' topmargin='0' onmousemove='HideMenu()'>
		<div id="menuDiv" style='Z-INDEX: 1000; VISIBILITY: hidden; WIDTH: 1px; POSITION: absolute; HEIGHT: 1px; BACKGROUND-COLOR: #9cc5f8'></div>
		<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%'>
			<tr>
				<td>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' class='Toolbar' id='eWebEditor_Toolbar'
						height="82">
						<tr>
							<td height="82">
								<div class="yToolbar" style="WIDTH: 100%">
									<div class="TBHandle"></div>
									<div class="Btn" TITLE="ȫ��ѡ��" LANGUAGE="javascript" onclick="format('selectall')">
										<img class="Ico" src="Images/selectall.gif" WIDTH="20" HEIGHT="20"></div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="ɾ��" LANGUAGE="javascript" onclick="format('delete')">
										<img class="Ico" src="Images/del.gif" WIDTH="20" HEIGHT="20"></div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="����" LANGUAGE="javascript" onclick="format('cut')">
										<img class="Ico" src="Images/cut.gif" WIDTH="20" HEIGHT="20"></div>
									<div class="Btn" TITLE="����" LANGUAGE="javascript" onclick="format('copy')">
										<img class="Ico" src="Images/copy.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="ճ��" LANGUAGE="javascript" onclick="format('paste')">
										<img class="Ico" src="Images/paste.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="��word��ճ��" LANGUAGE="javascript" onclick="word()">
										<img class="Ico" src="Images/wordpaste.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="����" LANGUAGE="javascript" onclick="format('undo')">
										<img class="Ico" src="Images/undo.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="�ָ�" LANGUAGE="javascript" onclick="format('redo')">
										<img class="Ico" src="Images/redo.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="����" LANGUAGE="javascript" onclick="findstr()">
										<img class="Ico" src="Images/find.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="��ӡ" LANGUAGE="javascript" onclick="format('Print')">
										<img class="Ico" src="Images/print.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="���" LANGUAGE="javascript" onclick="format('insertorderedlist')">
										<img class="Ico" src="Images/num.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="��Ŀ����" LANGUAGE="javascript" onclick="format('insertunorderedlist')">
										<img class="Ico" src="Images/list.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="����������" LANGUAGE="javascript" onclick="format('outdent')">
										<img class="Ico" src="Images/outdent.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="����������" LANGUAGE="javascript" onclick="format('indent')">
										<img class="Ico" src="Images/indent.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="�����" NAME="Justify" LANGUAGE="javascript" onclick="format('justifyleft')">
										<img class="Ico" src="Images/aleft.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="����" NAME="Justify" LANGUAGE="javascript" onclick="format('justifycenter')">
										<img class="Ico" src="Images/acenter.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="�Ҷ���" NAME="Justify" LANGUAGE="javascript" onclick="format('justifyright')">
										<img class="Ico" src="Images/aright.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="ɾ�����ָ�ʽ" LANGUAGE="javascript" onclick="format('RemoveFormat')">
										<img class="Ico" src="Images/clear.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
								</div>
							</td>
						<tr>
						<tr>
							<td height="82">
								<div class="yToolbar" style="WIDTH: 100%">
									<div class="TBHandle"></div>
									<select ID="formatSelect" class="TBGen" onchange="format('FormatBlock',this[this.selectedIndex].value);this.selectedIndex=0">
										<option selected>�����ʽ</option>
										<option VALUE="<P>">��ͨ</option>
										<option VALUE="<PRE>">�ѱ��Ÿ�ʽ</option>
										<option VALUE="<H1>">����һ</option>
										<option VALUE="<H2>">�����</option>
										<option VALUE="<H3>">������</option>
										<option VALUE="<H4>">������</option>
										<option VALUE="<H5>">������</option>
										<option VALUE="<H6>">������</option>
										<option VALUE="<H7>">������</option>
									</select>
									<select id="FontName" class="TBGen" onchange="format('fontname',this[this.selectedIndex].value);this.selectedIndex=0">
										<option selected>����</option>
										<option value="����">����</option>
										<option value="����">����</option>
										<option value="����_GB2312">����</option>
										<option value="����_GB2312">����</option>
										<option value="����">����</option>
										<option value="��Բ">��Բ</option>
										<option value="Arial">Arial</option>
										<option value="Arial Black">Arial Black</option>
										<option value="Arial Narrow">Arial Narrow</option>
										<option value="Brush Script&#9;MT">Brush Script MT</option>
										<option value="Century Gothic">Century Gothic</option>
										<option value="Comic Sans MS">Comic Sans MS</option>
										<option value="Courier">Courier</option>
										<option value="Courier New">Courier New</option>
										<option value="MS Sans Serif">MS Sans Serif</option>
										<option value="Script">Script</option>
										<option value="System">System</option>
										<option value="Times New Roman">Times New Roman</option>
										<option value="Verdana">Verdana</option>
										<option value="Wide&#9;Latin">Wide Latin</option>
										<option value="Wingdings">Wingdings</option>
									</select>
									<select id="FontSize" class="TBGen" onchange="format('fontsize',this[this.selectedIndex].value);this.selectedIndex=0">
										<option selected>�ֺ�</option>
										<option value="7">һ��</option>
										<option value="6">����</option>
										<option value="5">����</option>
										<option value="4">�ĺ�</option>
										<option value="3">���</option>
										<option value="2">����</option>
										<option value="1">�ߺ�</option>
									</select>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="������ɫ" LANGUAGE="javascript" onclick="foreColor()">
										<img class="Ico" src="Images/fgcolor.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="���ֱ���ɫ" LANGUAGE="javascript" onclick="backColor()">
										<img class="Ico" src="Images/fgbgcolor.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="�Ӵ�" LANGUAGE="javascript" onclick="format('bold')">
										<img class="Ico" src="Images/bold.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="б��" LANGUAGE="javascript" onclick="format('italic')">
										<img class="Ico" src="Images/italic.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="�»���" LANGUAGE="javascript" onclick="format('underline')">
										<img class="Ico" src="Images/underline.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="�ϱ�" LANGUAGE="javascript" onclick="format('superscript')">
										<img class="Ico" src="Images/sup.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="�±�" LANGUAGE="javascript" onclick="format('subscript')">
										<img class="Ico" src="Images/sub.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
								</div>
							</td>
						</tr>
						<tr>
							<td height="82">
								<div class="yToolbar" style="WIDTH: 100%">
									<div class="TBHandle">
									</div>
									<div class="Btn" TITLE="���볬������" LANGUAGE="javascript" onclick="CreateLink()">
										<img class="Ico" src="Images/url.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="ȡ����������" LANGUAGE="javascript" onclick="UserDialog('unLink')">
										<img class="Ico" src="Images/nourl.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="������ͨˮƽ��" LANGUAGE="javascript" onclick="format('InsertHorizontalRule')">
										<img class="Ico" src="Images/line.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="��������ˮƽ��" LANGUAGE="javascript" onclick="hr()">
										<img class="Ico" src="Images/sline.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="�����ֶ���ҳ��" LANGUAGE="javascript" onclick="page()"><img class="Ico" src="Images/page.gif" WIDTH="18" HEIGHT="18">
									</div>
									<div class="Btn" TITLE="���뵱ǰ����" LANGUAGE="javascript" onclick="nowdate()">
										<img class="Ico" src="Images/date.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="���뵱ǰʱ��" LANGUAGE="javascript" onclick="nowtime()">
										<img class="Ico" src="Images/time.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="������Ŀ��" LANGUAGE="javascript" onclick="FIELDSET()">
										<img class="Ico" src="Images/fieldset.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="������ҳ" LANGUAGE="javascript" onclick="iframe()"><img class="Ico" src="Images/htm.gif" WIDTH="18" HEIGHT="18">
									</div>
									<div class="Btn" TITLE="����Excel���" LANGUAGE="javascript" onclick="excel()">
										<img class="Ico" src="Images/excel.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="���˵�" LANGUAGE="javascript" onclick="ShowMenu(menu_table,100)">
										<img class="Ico" src="Images/table.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="���Ų˵�" LANGUAGE="javascript" onclick="ShowMenu(menu_chars,100)">
										<img class="Ico" src="Images/chars.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="����ͼƬ��֧�ָ�ʽΪ��jpg��gif��bmp��png" LANGUAGE="javascript" onclick="pic()">
										<img class="Ico" src="Images/img.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="����flash��ý���ļ�" LANGUAGE="javascript" onclick="swf()">
										<img class="Ico" src="Images/flash.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="������/��Ƶ�ļ���֧�ָ�ʽΪ��wav��mp3��mid��avi��wmv��mpg��asf" LANGUAGE="javascript" onclick="wmv()">
										<img class="Ico" src="Images/wmv.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="Btn" TITLE="���������ļ�" LANGUAGE="javascript" onclick="otherfile()">
										<img class="Ico" src="Images/file.gif" WIDTH="20" HEIGHT="20">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="������" LANGUAGE="javascript" onclick="calculator()">
										<img class="Ico" src="Images/calculator.gif" WIDTH="18" HEIGHT="18">
									</div>
									<div class="TBSep"></div>
									<div class="Btn" TITLE="��װ��ʽ�༭�����" LANGUAGE="javascript" onclick="InstallEQ()">
										<img class="Ico" src="Images/eq2.gif" WIDTH="18" HEIGHT="18">
									</div>
									<div class="Btn" TITLE="���빫ʽ" LANGUAGE="javascript" onclick="InsertEQ()">
										<img class="Ico" src="Images/eq1.gif" WIDTH="18" HEIGHT="18">
									</div>
									<div class="TBSep"></div>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height='100%'>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%'>
						<tr>
							<td height='100%'>
								<iframe class="Composition" ID="HtmlEdit" src="Content.aspx?SectionID=<%=strSectionID%>" MARGINHEIGHT="1" MARGINWIDTH="1"
									style="WIDTH: 100%; HEIGHT: 100%" scrolling="yes">
								</iframe></IFRAME>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="25">
					<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" class="StatusBar" height="25">
						<TR valign="middle">
							<td>
								<table border="0" cellpadding="0" cellspacing="0" height="20">
									<tr>
										<td width="10"></td>
										<td><img id="setMode0" src="Images/Editor2.gif" width="59" height="20" onClick="setMode(0)"></td>
										<td width="5"></td>
										<td><img id="setMode1" src="Images/html.gif" width="59" height="20" onClick="setMode(1)"></td>
										<td width="5"></td>
									</tr>
								</table>
							</td>
						</TR>
					</TABLE>
				</td>
			</tr>
		</table>
		<script type="text/javascript">
SEP_PADDING = 5;
HANDLE_PADDING = 7;

var yToolbars =	new Array();
var YInitialized = false;
var bLoad=false;
var pureText=true;
var bodyTag="<head><style type='text/css'>body {font-size:	9pt}</style><meta http-equiv=Content-Type content='text/html; charset=gb2312'></head><BODY bgcolor='#FFFFFF' MONOSPACE>";
var EditMode=true;
var SourceMode=false;
var PreviewMode=false;
var CurrentMode=0;
function document.onreadystatechange()
{
  if (YInitialized) return;
  YInitialized = true;

  var i, s, curr;

  for (i=0; i<document.body.all.length;	i++)
  {
    curr=document.body.all[i];
    if (curr.className == "yToolbar")
    {
      InitTB(curr);
      yToolbars[yToolbars.length] = curr;
    }
  }

  DoLayout();
  window.onresize = DoLayout;

  //HtmlEdit.document.open();
  //HtmlEdit.document.write(bodyTag);
  //HtmlEdit.document.close();
  HtmlEdit.document.designMode="On";
}

function InitBtn(btn)
{
  btn.onmouseover = BtnMouseOver;
  btn.onmouseout = BtnMouseOut;
  btn.onmousedown = BtnMouseDown;
  btn.onmouseup	= BtnMouseUp;
  btn.ondragstart = YCancelEvent;
  btn.onselectstart = YCancelEvent;
  btn.onselect = YCancelEvent;
  btn.YUSERONCLICK = btn.onclick;
  btn.onclick =	YCancelEvent;
  btn.YINITIALIZED = true;
  return true;
}

function InitBtnMenu(BtnMenu)
{
  BtnMenu.onmouseover = BtnMenuMouseOver;
  BtnMenu.onmouseout = BtnMenuMouseOut;
  BtnMenu.onmousedown = BtnMenuMouseDown;
  BtnMenu.onmouseup	= BtnMenuMouseUp;
  BtnMenu.ondragstart = YCancelEvent;
  BtnMenu.onselectstart = YCancelEvent;
  BtnMenu.onselect = YCancelEvent;
  BtnMenu.YUSERONCLICK = BtnMenu.onclick;
  BtnMenu.onclick =	YCancelEvent;
  BtnMenu.YINITIALIZED = true;
  return true;
}

function InitTB(y)
{
  y.TBWidth = 0;

  if (!	PopulateTB(y)) return false;

  y.style.posWidth = y.TBWidth;

  return true;
}


function YCancelEvent()
{
  event.returnValue=false;
  event.cancelBubble=true;
  return false;
}

function PopulateTB(y)
{
  var i, elements, element;

  elements = y.children;
  for (i=0; i<elements.length; i++) {
    element = elements[i];
    if (element.tagName	== "SCRIPT" || element.tagName == "!") continue;

    switch (element.className) {
      case "Btn":
        if (element.YINITIALIZED == null)	{
          if (! InitBtn(element))
          return false;
        }
        element.style.posLeft = y.TBWidth;
        y.TBWidth	+= element.offsetWidth + 1;
        break;
      
	  case "BtnMenu":
        if (element.YINITIALIZED == null)	{
          if (! InitBtnMenu(element))
          return false;
        }
        element.style.posLeft = y.TBWidth;
        y.TBWidth	+= element.offsetWidth + 1;
        break;

      case "TBGen":
        element.style.posLeft = y.TBWidth;
        y.TBWidth	+= element.offsetWidth + 1;
        break;

      case "TBSep":
        element.style.posLeft = y.TBWidth	+ 2;
        y.TBWidth	+= SEP_PADDING;
        break;

      case "TBHandle":
        element.style.posLeft = 2;
        y.TBWidth	+= element.offsetWidth + HANDLE_PADDING;
        break;

      default:
        return false;
      }
  }

  y.TBWidth += 1;
  return true;
}

function DebugObject(obj)
{
  var msg = "";
  for (var i in	TB) {
    ans=prompt(i+"="+TB[i]+"\n");
    if (! ans) break;
  }
}

function LayoutTBs()
{
  NumTBs = yToolbars.length;

  if (NumTBs ==	0) return;

  var i;
  var ScrWid = (document.body.offsetWidth) - 12;
  var TotalLen = ScrWid;
  for (i = 0 ; i < NumTBs ; i++) {
    TB = yToolbars[i];
    if (TB.TBWidth > TotalLen) TotalLen	= TB.TBWidth;
  }

  var PrevTB;
  var LastStart	= 0;
  var RelTop = 0;
  var LastWid, CurrWid;
  var TB = yToolbars[0];
  TB.style.posTop = 0;
  TB.style.posLeft = 0;

  var Start = TB.TBWidth;
  for (i = 1 ; i < yToolbars.length ; i++) {
    PrevTB = TB;
    TB = yToolbars[i];
    CurrWid = TB.TBWidth;

    if ((Start + CurrWid) > ScrWid) {
      Start = 0;
      LastWid =	TotalLen - LastStart;
    }
    else {
       LastWid =	PrevTB.TBWidth;
       RelTop -=	TB.offsetHeight;
    }

    TB.style.posTop = RelTop;
    TB.style.posLeft = Start;
    PrevTB.style.width = LastWid;

    LastStart =	Start;
    Start += CurrWid;
  }

  TB.style.width = TotalLen - LastStart;

  i--;
  TB = yToolbars[i];
  var TBInd = TB.sourceIndex;
  var A	= TB.document.all;
  var item;
  for (i in A) {
    item = A.item(i);
    if (! item)	continue;
    if (! item.style) continue;
    if (item.sourceIndex <= TBInd) continue;
    if (item.style.position == "absolute") continue;
    item.style.posTop =	RelTop;
  }
}

function DoLayout()
{
  LayoutTBs();
}

function BtnMouseOver()
{
  if (event.srcElement.tagName != "IMG") return	false;
  var image = event.srcElement;
  var element =	image.parentElement;

  if (image.className == "Ico")	element.className = "BtnMouseOverUp";
  else if (image.className == "IcoDown") element.className = "BtnMouseOverDown";

  event.cancelBubble = true;
}

function BtnMouseOut()
{
  if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    return false;
  }

  var image = event.srcElement;
  var element =	image.parentElement;
  yRaisedElement = null;

  element.className = "Btn";
  image.className = "Ico";

  event.cancelBubble = true;
}

function BtnMouseDown()
{
  if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    event.returnValue=false;
    return false;
  }

  var image = event.srcElement;
  var element =	image.parentElement;

  element.className = "BtnMouseOverDown";
  image.className = "IcoDown";

  event.cancelBubble = true;
  event.returnValue=false;
  return false;
}

function BtnMouseUp() {

 if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    return false;
  }

  var image = event.srcElement;
  var element = image.parentElement;


  //��Ҫ��element.YUSERONCLICK

  if (element.YUSERONCLICK) {
      var test = element.YUSERONCLICK.toString();
      var start = test.indexOf('{');
      var end = test.indexOf('}');
      window.eval(test.substr(start, end));
    }
  
  

   //if (element.YUSERONCLICK) eval();

  element.className = "BtnMouseOverUp";
  image.className = "Ico";

  event.cancelBubble = true;
  return false;
}

function anonymous() {


 }


function BtnMenuMouseOver()
{
  if (event.srcElement.tagName != "IMG") return	false;
  var image = event.srcElement;
  var element =	image.parentElement;

  if (image.className == "Ico")	element.className = "BtnMenuMouseOverUp";
  else if (image.className == "IcoDown") element.className = "BtnMenuMouseOverDown";

  event.cancelBubble = true;
}

function BtnMenuMouseOut()
{
  if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    return false;
  }

  var image = event.srcElement;
  var element =	image.parentElement;
  yRaisedElement = null;

  element.className = "BtnMenu";
  image.className = "Ico";

  event.cancelBubble = true;
}

function BtnMenuMouseDown()
{
  if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    event.returnValue=false;
    return false;
  }

  var image = event.srcElement;
  var element =	image.parentElement;

  element.className = "BtnMenuMouseOverDown";
  image.className = "IcoDown";

  event.cancelBubble = true;
  event.returnValue=false;
  return false;
}

function BtnMenuMouseUp()
{
  if (event.srcElement.tagName != "IMG") {
    event.cancelBubble = true;
    return false;
  }

 

  var image = event.srcElement;
  var element =	image.parentElement;

    if (element.YUSERONCLICK) eval(element.YUSERONCLICK +	"anonymous()");

    element.className = "BtnMenuMouseOverUp";
    image.className = "Ico";

  event.cancelBubble = true;
  return false;
}

function cleanHtml()
{
  var fonts = HtmlEdit.document.body.all.tags("FONT");
  var curr;
  for (var i = fonts.length - 1; i >= 0; i--) {
    curr = fonts[i];
    if (curr.style.backgroundColor == "#ffffff") curr.outerHTML	= curr.innerHTML;
  }
}

function validateMode()
{
  if (EditMode) return true;
  alert("���ȵ�༭���·��ġ��༭����ť�����롰�༭��״̬��Ȼ����ʹ��ϵͳ�༭����!");
  HtmlEdit.focus();
  return false;
}

function UserDialog(what)
{
  if (!validateMode()) return;

  HtmlEdit.document.execCommand(what,true);

  pureText = false;
  HtmlEdit.focus();
}

function format(what,opt)
{
  if (!validateMode()) return;
  if (opt=="removeFormat")
  {
    what=opt;
    opt=null;
  }

  if (opt==null) HtmlEdit.document.execCommand(what);
  else HtmlEdit.document.execCommand(what,"",opt);

  pureText = false;
  HtmlEdit.focus();
}

function setMode(newMode)
{
  var cont;
  if (CurrentMode==newMode){
    return false;
  }
  
  if (newMode==0)
  {
	setMode0.src="Images/Editor2.gif";
	setMode1.src="Images/html.gif";
	if (PreviewMode){
	  document.all.HtmlEdit.style.display="";
	}
	if(SourceMode){
	  cont=HtmlEdit.document.body.innerText;
      HtmlEdit.document.body.innerHTML=cont;
     
	}
    EditMode=true;
	SourceMode=false;
	PreviewMode=false;
  }
  else if (newMode==1)
  {
	setMode0.src="Images/Editor.gif";
	setMode1.src="Images/html2.gif";
	if (PreviewMode){
	  document.all.HtmlEdit.style.display="";
	}
	if(EditMode){
	  cleanHtml();
      cleanHtml();
      cont=HtmlEdit.document.body.innerHTML;
      HtmlEdit.document.body.innerText=cont;
	}
    EditMode=false;
	SourceMode=true;
	PreviewMode=false;
  }
  else if (newMode==2)
  {
	setMode0.src="Images/Editor.gif";
	setMode1.src="Images/html.gif";
	var str1="<head><style type='text/css'>body {font-size:	9pt}</style><meta http-equiv=Content-Type content='text/html; charset=gb2312'></head><BODY bgcolor='#F6F6F6' MONOSPACE>";
	if(CurrentMode==0){
	  str1=str1+HtmlEdit.document.body.innerHTML;
	}
	else{
	  str1=str1+HtmlEdit.document.body.innerText;
	}
    document.all.HtmlEdit.style.display="none";
	PreviewMode=true;
  }
  CurrentMode=newMode;
  HtmlEdit.focus();
}
//��ȡ�༭��������
function getHTML()
{
	var html;
	if (CurrentMode==0)
	{
		html=HtmlEdit.document.body.innerHTML;
	}
	else
	{
		html=HtmlEdit.document.body.innerText;
	}	
	return html;
}
//���ñ༭��������
function setHTML(html)
{
	if (CurrentMode==0)
	{
		HtmlEdit.document.body.innerHTML=html;
	}
	else
	{
		HtmlEdit.document.body.innerText=html;
	}	
}

function foreColor()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var RangeType = HtmlEdit.document.selection.type;
  if (RangeType != "Text"){
    alert("����ѡ��һ�����֣�");
    return;
  }
  var arr = showModalDialog("Dialog/selcolor.htm", "", "dialogWidth:300px; dialogHeight:280px; help: no; scroll: no; status: no");
  if (arr != null) format('forecolor', arr);
  else HtmlEdit.focus();
}

function backColor()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var RangeType = HtmlEdit.document.selection.type;
  if (RangeType != "Text"){
    alert("����ѡ��һ�����֣�");
    return;
  }
  var arr = showModalDialog("Dialog/selcolor.htm", "", "dialogWidth:300px; dialogHeight:280px; help: no; scroll: no; status: no");
  if (arr != null){
    range.pasteHTML("<span style='background-color:"+arr+"'>"+range.text+"</span> ");
	range.select();
  }
  HtmlEdit.focus();
}

function CreateLink()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var RangeType = HtmlEdit.document.selection.type;
  if (RangeType != "Text"){
    alert("����ѡ��һ�����֣�");
    return;
  }
  var arr = showModalDialog("Dialog/hyperlink.htm", "", "dialogWidth:430px; dialogHeight:155px; help: no; scroll: no; status: no");
  if (arr != null) format('CreateLink', arr);
  else HtmlEdit.focus();
}

function page()
{
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  if(range.text!=""){
    alert("�벻Ҫѡ���κ��ı�");
  }
  else{
    range.text="\n\n[NextPage]\n\n";
 
  }
}

function InsertTable()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/inserttable.htm", "", "dialogWidth:450px;dialogHeight:210px;help: no; scroll: no; status: no");

  if (arr != null){
	range.pasteHTML(arr);
  }
  HtmlEdit.focus();
}

function FIELDSET()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/fieldset.htm", "", "dialogWidth:400px; dialogHeight:210px; help: no; scroll: no; status: no");
  if (arr != null){
    range.pasteHTML(arr);
  }
  HtmlEdit.focus();
}

function iframe()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/insertiframe.htm", "", "dialogWidth:30em; dialogHeight:12em; help: no; scroll: no; status: no");  
  if (arr != null){
    range.pasteHTML(arr);
  }
  HtmlEdit.focus();
}

function hr()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/inserthr.htm", "", "dialogWidth:30em; dialogHeight:12em; help: no; scroll: no; status: no"); 
  if (arr != null){
    range.pasteHTML(arr);
  }
  HtmlEdit.focus();
}

function pic() {
     
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/insertpic.htm", "", "dialogWidth:31em; dialogHeight:17em; help: no; scroll: no; status: no");  
  if (arr != null){
    var ss=arr.split("$$$");
    range.pasteHTML(ss[0]); 
 
  }
  HtmlEdit.focus();
}

function swf()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/insertflash.htm", "", "dialogWidth:31em; dialogHeight:12em; help: no; scroll: no; status: no"); 
  if (arr != null){
    var ss=arr.split("$$$");
    range.pasteHTML(ss[0]);
 
 
  }
  HtmlEdit.focus();
}

function wmv()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/insertmedia.htm", "", "dialogWidth:500px; dialogHeight:210px; help: no; scroll: no; status: no");
  if (arr != null){
    var ss=arr.split("$$$");
    range.pasteHTML(ss[0]);
 
 
  }
  HtmlEdit.focus();
}

function otherfile()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range = HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/insertfile.htm", "", "dialogWidth:495px; dialogHeight:185px; help: no; scroll: no; status: no");  
  if (arr != null){
    var ss=arr.split("$$$");
    range.pasteHTML(ss[0]);
 
  }
  HtmlEdit.focus();
}

function excel()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var str1="<object classid='clsid:0002E510-0000-0000-C000-000000000046' id='Spreadsheet1' codebase='msowc.cab' width='100%' height='250'><param name='EnableAutoCalculate' value='-1'><param name='DisplayTitleBar' value='0'><param name='DisplayToolbar' value='-1'><param name='ViewableRange' value='1:65536'></object>";
  range.pasteHTML(str1);
  HtmlEdit.focus();
}

function nowdate()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var d = new Date();
  var str1=d.getYear()+"��"+(d.getMonth() + 1)+"��"+d.getDate() +"��";
  range.pasteHTML(str1);
  HtmlEdit.focus();
}

function nowtime()
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var d = new Date();
  var str1=d.getHours() +":"+d.getMinutes()+":"+d.getSeconds();
  range.pasteHTML(str1);
  HtmlEdit.focus();
}

function findstr()
{
  if (!	validateMode())	return;
  var arr = showModalDialog("Dialog/find.htm", window, "dialogWidth:420px; dialogHeight:160px; help: no; scroll: no; status: no");
}

 

function save()
{
  if (CurrentMode==0){
//�༭��Ƕ��������ҳʱʹ��������һ�䣨�뽫form1�ĳ���Ӧ������
    parent.myform.Content.value=HtmlEdit.document.body.innerHTML;
//�����򿪱༭��ʱʹ��������һ�䣨�뽫form1�ĳ���Ӧ������  
//  self.opener.form1.content.value+=HtmlEdit.document.body.innerHTML;
  }
  else if(CurrentMode==1){
//�༭��Ƕ��������ҳʱʹ��������һ�䣨�뽫form1�ĳ���Ӧ������
    parent.myform.Content.value=HtmlEdit.document.body.innerText;
//�����򿪱༭��ʱʹ��������һ�䣨�뽫form1�ĳ���Ӧ������  
//  self.opener.form1.content.value+=HtmlEdit.document.body.innerText;
  }
  else
  {
    alert("Ԥ��״̬���ܱ��棡���Ȼص��༭״̬���ٱ���");
  }
  HtmlEdit.focus();
}


function tablecommand(command)
{
	var cellflag=false;
	var rowflag=false;
	var tableflag=false;
	var cellindex,rowindex,tableref;
	HtmlEdit.focus();
	var xsel=HtmlEdit.document.selection;
	var xobj=HtmlEdit.document.selection.createRange();
	if(xsel.type=="None"||xsel.type=="Text"){
		xsel=xobj.parentElement();
		while(xsel.tagName!="BODY"&&cellflag==false){
			if(xsel.tagName=="TD"){cellindex=xsel.cellIndex;cellflag=true;}
			if(cellflag==false){xsel=xsel.parentElement;}
		}
	}else if(xsel.type=="Control"){
		xsel=xobj.item(0);
		if(xsel.tagName=="TD"){
			cellindex=xsel.cellIndex;
			cellflag=true;
		}else{
			while(xsel.tagName!="BODY"&&cellflag==false){
				if(xsel.tagName=="TD"){cellindex=xsel.cellIndex;cellflag=true;}
				if(cellflag==false){xsel=xsel.parentElement;}
			}
		}
	}
	if(cellflag==true){
		xsel=HtmlEdit.document.selection;
		xobj=HtmlEdit.document.selection.createRange();
		if(xsel.type=="None"||xsel.type=="Text"){
			xsel=xobj.parentElement();
			while(xsel.tagName!="BODY"&&rowflag==false){
				if(xsel.tagName=="TR"){
					rowindex=xsel.rowIndex;
					rowflag=true;
				}
				if(rowflag==false){xsel=xsel.parentElement;}
			}
		}else if(xsel.type=="Control"){
			xsel=xobj.item(0);
			if(xsel.tagName=="TR"){
				rowindex=xsel.rowIndex;
				rowflag=true;
			}else{
				while(xsel.tagName!="BODY"&&rowflag==false){
					if(xsel.tagName=="TR"){
						rowindex=xsel.rowIndex;
						rowflag=true;
					}
					if(rowflag==false){
						xsel=xsel.parentElement;
					}
				}
			}
		}
		xsel=HtmlEdit.document.selection;
		xobj=HtmlEdit.document.selection.createRange();
		if(xsel.type=="None"||xsel.type=="Text"){
			xsel=xobj.parentElement();
			while(xsel.tagName!="BODY"&&tableflag==false){
				if(xsel.tagName=="TABLE"){tableflag=true;}
				if(tableflag==false){xsel=xsel.parentElement;}
			}
		}else if(xsel.type=="Control"){
			xsel=xobj.item(0);
			if(xsel.tagName=="TABLE"){
				tableflag=true;
			}else{
				while(xsel.tagName!="BODY"&&tableflag==false){
					if(xsel.tagName=="TABLE"){tableflag=true;}
					if(tableflag==false){xsel=xsel.parentElement;}
				}
			}
		}
		if(command==3){
			var temprowcount=xsel.rows.length;
			var tempcell;
			var tempspancount=0;
			var tempspanholder;
			var tempcellwidth=xsel.rows[rowindex].cells[cellindex].width;
			var xpositequiv=-1;
			var xposcount=0;
			while(xposcount<=cellindex){
				xpositequiv+=parseInt(xsel.rows[rowindex].cells[xposcount].colSpan);
				xposcount++;
			}
			var ypositequiv=-1;
			var yposcount=0;
			var ymax=xsel.rows[rowindex].cells.length;
			while(yposcount<=ymax-1){
				ypositequiv+=parseInt(xsel.rows[rowindex].cells[yposcount].colSpan);
				yposcount++;
			}
			var idealinsert=xpositequiv+1;
			var zi2=0;
			var zirowtouse=0;
			var zirowtot=xsel.rows.length;
			var rowarray=new Array(zirowtot);
			var rowarray2=new Array(zirowtot);
			for(init1=0;init1<=zirowtot-1;init1++){
				rowarray[init1]=0;
				rowarray2[init1]=0;
			}
			for(zi1=0;zi1<=zirowtot-1;zi1++){
				zi2=0;
				while(zi2<idealinsert&&(rowarray[zi1]==null||rowarray[zi1]<idealinsert)){
					rowarray[zi1]+=parseInt(xsel.rows[zi1].cells[zi2].colSpan);
					rowarray2[zi1]++;
					zi2++;
				}
			}
			var allequal=true;
			var zi3a,zi3b;
			var zthemax=0;
			for(zi3=0;zi3<=zirowtot-1;zi3++){
				zi3a=rowarray[0];
				zi3b=rowarray[zi3];
				if(zi3b>zthemax){zthemax=zi3b;}
				if(zi3a!=zi3b){allequal=false;}
			}
			if(allequal==false){
				var zi4=0;
				var allequal2=true;
				while(zthemax<=ypositequiv&&allequal==false){
					for(zi5=0;zi5<=zirowtot-1;zi5++){
						rowarray[zi5]+=parseInt(xsel.rows[zi5].cells[rowarray2[zi5]].colSpan);
					}
					for(zi3=0;zi3<=zirowtot-1;zi3++){
						zi3a=rowarray[0];
						zi3b=rowarray[zi3];
						if(zi3b>zthemax){zthemax=zi3b;}
						if(zi3a!=zi3b){allequal2=false;}
					}
					if(allequal2==true){allequal=true;}
					for(zi8=0;zi8<=zirowtot-1;zi8++){rowarray2[zi8]++;}
					}
				}
				var zi9;
				for(zi7=0;zi7<=zirowtot-1;zi7++){
					zi9=xsel.rows[zi7].insertCell(rowarray2[zi7]);
					zi9.width=tempcellwidth;
				}
		}else if(command==4){
			var temprowcount=xsel.rows.length;
			for(iccount=0;iccount<=temprowcount-1;iccount++){
				xsel.rows[iccount].deleteCell(cellindex);
			}
			}else if(command==1){
				var tempcell;
				var tempcellb;
				var tempcellcount=xsel.rows[rowindex].cells.length;
				var cellcolarray=new Array(tempcellcount);
				var cellrowarray=new Array(tempcellcount);
				for(cacount=0;cacount<=tempcellcount-1;cacount++){
					cellcolarray[cacount]=xsel.rows[rowindex].cells(cacount).colSpan;
					cellrowarray[cacount]=xsel.rows[rowindex].cells(cacount).rowSpan;
				}
				tempcell=xsel.insertRow(rowindex);
				for(cbcount=0;cbcount<=tempcellcount-1;cbcount++){
					tempcellb=tempcell.insertCell();
					if(cellcolarray[cbcount]!=1){tempcellb.colSpan=cellcolarray[cbcount];}
				}
		}else if(command==2){
				var temprowcount=xsel.rows.length;tempcell=xsel.deleteRow(rowindex);
		}else if(command==5){
				if(xsel.rows[rowindex].cells[cellindex+1]){
					var x=parseInt(xsel.rows[rowindex].cells[cellindex].colSpan)+parseInt(xsel.rows[rowindex].cells[cellindex+1].colSpan);
					var y=xsel.rows[rowindex].cells[cellindex].innerHTML+" "+xsel.rows[rowindex].cells[cellindex+1].innerHTML;
					xsel.rows[rowindex].deleteCell(cellindex+1);
					xsel.rows[rowindex].cells[cellindex].colSpan=x;
					xsel.rows[rowindex].cells[cellindex].innerHTML=y;
				}
		}else if(command==6){
				var yatemprow=xsel.rows.length;
				var yamax=0;
				for(ya1=0;ya1<=yatemprow-1;ya1++){
					var ypositequiv=-1;
					var yposcount=0;
					var ymax=xsel.rows[ya1].cells.length;
					while(yposcount<=ymax-1){
						ypositequiv+=parseInt(xsel.rows[ya1].cells[yposcount].colSpan);
						yposcount++;
					}
					if(ypositequiv>yamax){yamax=ypositequiv;}
				}
				var rowarray=new Array();
				var rowarray2=new Array();
				var myrowcount=xsel.rows.length;
				for(ra1=0;ra1<=myrowcount-1;ra1++){
					rowarray[ra1]=new Array();
					rowarray2[ra1]=0;
					for(cr1=0;cr1<=yamax;cr1++){rowarray[ra1][cr1]=777;}
				}
				var tempra;
				var ra2=0;
				for(ra3=0;ra3<=yamax;ra3++){
					ra2=0;
					while(ra2<=myrowcount-1){
						if(xsel.rows[ra2].cells[ra3]){
							tempra=parseInt(xsel.rows[ra2].cells[ra3].rowSpan);
							if(tempra>1){
								rowarray[ra2][ra3]=ra3+rowarray2[ra2];
								for(zoo=1;zoo<=tempra-1;zoo++){rowarray2[ra2+zoo]--;}
							}
						}
						if(rowarray[ra2][ra3-1]!=ra3+rowarray2[ra2]){
							rowarray[ra2][ra3]=ra3+rowarray2[ra2];
						}else{
							rowarray[ra2][ra3]=555;
						}
						ra2++;
					}
				}
				var samx="";
				var samcount=0;
				for(rx1=0;rx1<=myrowcount-1;rx1++){
					samcount=rowarray[rx1].length;
					for(rx2=0;rx2<=samcount-1;rx2++){
						samx+="-"+rowarray[rx1][rx2];
					}
					samx+="\n";
				}
				var j=parseInt(xsel.rows[rowindex].cells[cellindex].rowSpan);
				var jcount=rowarray[rowindex].length;
				var jval=0;
				for(jc1=0;jc1<=jcount-1;jc1++){
					if(rowarray[rowindex][jc1]==cellindex){jval=jc1;}
				}
				if(xsel.rows[rowindex+j]){
					var cellindex2=rowarray[rowindex+j][jval];
					var x=parseInt(xsel.rows[rowindex].cells[cellindex].rowSpan)+parseInt(xsel.rows[rowindex+j].cells[cellindex2].rowSpan);
					var y=xsel.rows[rowindex].cells[cellindex].innerHTML+" "+xsel.rows[rowindex+j].cells[cellindex2].innerHTML;
					xsel.rows[rowindex+j].deleteCell(cellindex2);
					xsel.rows[rowindex].cells[cellindex].rowSpan=x;
					xsel.rows[rowindex].cells[cellindex].innerHTML=y;
				}
		}else if(command==7){
				var getrowspan=parseInt(xsel.rows[rowindex].cells[cellindex].rowSpan);
				var getcolspan=parseInt(xsel.rows[rowindex].cells[cellindex].colSpan);
				if(getrowspan>1){
					var xr1=getrowspan-1;
					var xrposit=rowindex;
					var xrcposit=cellindex;
					var xrholder;xsel.rows[rowindex].cells[cellindex].rowSpan=1;
					for(xr2=1;xr2<=xr1;xr2++){
						xrholder=xsel.rows[xrposit+xr2].insertCell(xrcposit);
						xrholder.colSpan=xsel.rows[rowindex].cells[cellindex].colSpan;
					}
				}
				if(getcolspan>1){
					var yr1=getcolspan-1;
					var yrposit=rowindex;
					var yrcposit=cellindex;
					var yrholder;xsel.rows[rowindex].cells[cellindex].colSpan=1;
					for(yr2=1;yr2<=yr1;yr2++){
						yrholder=xsel.rows[yrposit].insertCell(yrcposit);
						yrholder.rowSpan=xsel.rows[rowindex].cells[cellindex].rowSpan;
					}
				}
			}
		}
}


function tableProp(){
	var tableflag=false;
	HtmlEdit.focus();
	var xsel=HtmlEdit.document.selection;
	var xobj=HtmlEdit.document.selection.createRange();
	if(xsel.type=="None"||xsel.type=="Text"){
		xsel=xobj.parentElement();
		while(xsel.tagName!="BODY"&&tableflag==false){
			if(xsel.tagName=="TABLE"){tableflag=true;}
			if(tableflag==false){xsel=xsel.parentElement;}
		}
	}else if(xsel.type=="Control"){
		xsel=xobj.item(0);
		if(xsel.tagName=="TABLE"){
			tableflag=true;
		}else{
			while(xsel.tagName!="BODY"&&tableflag==false){
				if(xsel.tagName=="TABLE"){tableflag=true;}
				if(tableflag==false){xsel=xsel.parentElement;}
			}
		}
	}
	if(tableflag==true){
		if(xsel.className!=""&&xsel.className!=null){tableclass=xsel.className;}else{tableclass="";}
		if(xsel.width!=""&&xsel.width!=null){tablewidthspecified="yes";tablewidth=xsel.width;}else{tablewidthspecified="no";tablewith="";}
		if(xsel.align!=""&&xsel.align!=null){tablealign=xsel.align;}else{tablealign="";}
		if(xsel.border!=""&&xsel.border!=null){tablebordersize=xsel.border;}else{tablebordersize="";}
		if(xsel.cellPadding!=""&&xsel.cellPadding!=null){tablecellpadding=xsel.cellPadding;}else{tablecellpadding="";}
		if(xsel.cellSpacing!=""&&xsel.cellSpacing!=null){tablecellspacing=xsel.cellSpacing;}else{tablecellspacing="";}
		if(xsel.borderColor!=""&&xsel.borderColor!=null){tablebordercolor=xsel.borderColor;}else{tablebordercolor="";}
		if(xsel.bgColor!=""&&xsel.bgColor!=null){tablebackgroundcolor=xsel.bgColor;}else{tablebackgroundcolor="";}
		tableiscancel="";
		window.showModalDialog("Dialog/tableprops.htm",window," dialogWidth: 500px; dialogHeight: 330px; help: no;scroll: no; status: no");
		if(tableiscancel=="no"){
			if(tablewidthspecified=="yes"){
				var tw1="";
				if(tablewidthtype=="percentage"){
					tw1=tablewidth+"%";
				}else{
					tw1=tablewidth;
				}
				xsel.width=tw1;
			}else{
				xsel.removeAttribute("width",0);
			}
			if(tablealign!=""&&tablealign!="Default"){xsel.align=tablealign;}else{xsel.removeAttribute("align",0);}
			if(tableclass!=""&&tableclass!="Default"){xsel.className=tableclass;}else{xsel.removeAttribute("className",0);}
			if(tablebordersize!=""&&tablebordersize!=null){xsel.border=tablebordersize;}else{xsel.removeAttribute("border",0);}
			if(tablecellpadding!=""&&tablecellpadding!=null){xsel.cellPadding=tablecellpadding;}else{xsel.removeAttribute("cellPadding",0);}
			if(tablecellspacing!=""&&tablecellspacing!=null){xsel.cellSpacing=tablecellspacing;}else{xsel.removeAttribute("cellSpacing",0);}
			if(tablebordercolor!=""&&tablebordercolor!="Default"){xsel.borderColor=tablebordercolor;}else{xsel.removeAttribute("borderColor",0);}
			if(tablebackgroundcolor!=""&&tablebackgroundcolor!="Default"){xsel.bgColor=tablebackgroundcolor;}else{xsel.removeAttribute("bgColor",0);}
		}
	}
}

function cellProp(){
	var cellflag=false;
	HtmlEdit.focus();
	var xsel=HtmlEdit.document.selection;
	var xobj=HtmlEdit.document.selection.createRange();
	if(xsel.type=="None"||xsel.type=="Text"){
		xsel=xobj.parentElement();
		while(xsel.tagName!="BODY"&&cellflag==false){
			if(xsel.tagName=="TD"){cellflag=true;}
			if(cellflag==false){xsel=xsel.parentElement;}
		}
	}else if(xsel.type=="Control"){
		xsel=xobj.item(0);
		if(xsel.tagName=="TD"){
			cellflag=true;
		}else{
			while(xsel.tagName!="BODY"&&cellflag==false){
				if(xsel.tagName=="TD"){cellflag=true;}
				if(cellflag==false){xsel=xsel.parentElement;}
			}
		}
	}
	if(cellflag==true){
		if(xsel.width!=""&&xsel.width!=null){tablewidthspecified="yes";tablewidth=xsel.width;}else{tablewidthspecified="no";tablewith="";}
		if(xsel.align!=""&&xsel.align!=null){tablealign=xsel.align;}else{tablealign="";}
		if(xsel.className!=""&&xsel.className!=null){tablecellclass=xsel.className;}else{tablecellclass="";}
		if(xsel.vAlign!=""&&xsel.vAlign!=null){tablevalign=xsel.vAlign;}else{tablevalign="";}
		if(xsel.borderColor!=""&&xsel.borderColor!=null){tablebordercolor=xsel.borderColor;}else{tablebordercolor="";}
		if(xsel.bgColor!=""&&xsel.bgColor!=null){tablebackgroundcolor=xsel.bgColor;}else{tablebackgroundcolor="";}
		tableiscancel="";
		window.showModalDialog("Dialog/cellprops.htm",window,"dialogWidth: 400px; dialogHeight: 278px;help: no;scroll: no; status: no");
		if(tableiscancel=="no"){
			if(tablewidthspecified=="yes"){
				var tw1="";
				if(tablewidthtype=="percentage"){tw1=tablewidth+"%";}else{tw1=tablewidth;}
				xsel.width=tw1;
			}else{
				xsel.removeAttribute("width",0);
			}
			if(tablealign!=""&&tablealign!="Default"){xsel.align=tablealign;}else{xsel.removeAttribute("align",0);}
			if(tablevalign!=""&&tablevalign!="Default"){xsel.vAlign=tablevalign;}else{xsel.removeAttribute("vAlign",0);}
			if(tablecellclass!=""&&tablecellclass!="Default"){xsel.className=tablecellclass;}else{xsel.removeAttribute("className",0);}
			if(tablebordercolor!=""&&tablebordercolor!="Default"){xsel.borderColor=tablebordercolor;}else{xsel.removeAttribute("borderColor",0);}
			if(tablebackgroundcolor!=""&&tablebackgroundcolor!="Default"){xsel.bgColor=tablebackgroundcolor;}else{xsel.removeAttribute("bgColor",0);}
		}
	}
}
function table_ir()
{
	tablecommand("ir");
}
function table_dr()
{
	tablecommand("dr");
}
function table_ic()
{
	tablecommand("ic");
}
function table_dc()
{
	tablecommand("dc");
}
function table_mc()
{
	tablecommand("mc");
}
function table_md()
{
	tablecommand("md");
}
function table_sc()
{
	tablecommand("sc");
}

function word()
{
	HtmlEdit.document.execCommand("Paste",false);
	var editBody=HtmlEdit.document.body;
	for(var intLoop=0;intLoop<editBody.all.length;intLoop++){
		el=editBody.all[intLoop];
		el.removeAttribute("className","",0);
		el.removeAttribute("style","",0);
		el.removeAttribute("font","",0);
	}
	var html=HtmlEdit.document.body.innerHTML;
	html=html.replace(/<o:p>&nbsp;<\/o:p>/g,"");
	html=html.replace(/o:/g,"");
	html=html.replace(/<font>/g, "");
	html=html.replace(/<FONT>/g, "");
	html=html.replace(/<span>/g, "");
	html=html.replace(/<SPAN>/g, "");
	html=html.replace(/<SPAN lang=EN-US>/g, "");
	html=html.replace(/<P>/g, "");
	html=html.replace(/<\/P>/g, "");
 	html=html.replace(/<\/SPAN>/g, "");
	HtmlEdit.document.body.innerHTML = html;
	format('selectall');
	format('RemoveFormat');
}

function InsertChars(CharIndex)
{
  if (!	validateMode())	return;
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var Chars=new Array("<br>","&copy;","&reg;","&#8482;","&#8226;","&#8230;","&#8212;","&#8211;");
  range.pasteHTML(Chars[CharIndex]);
  HtmlEdit.focus();
}
function InsertEQ()
{
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/inserteq.htm", "", "dialogWidth:655px; dialogHeight:325px; scroll:no;status:0;help:0");
  
  if (arr != null){
    var ss;
    ss=arr.split("*")
    a=ss[0];
    b=ss[1];
    var str1;
    str1="<applet codebase='./' code='webeq3.ViewerControl' WIDTH=320 HEIGHT=100>"
    str1=str1+"<PARAM NAME='parser' VALUE='mathml'><param name='color' value='"+b+"'><PARAM NAME='size' VALUE='18'>"
    str1=str1+"<PARAM NAME=eq id=eq VALUE='"+a+"'></applet>"
    range.pasteHTML(str1);
  }
  HtmlEdit.focus();
}
function InstallEQ()
{
  window.open ("Dialog/installeq.htm", "", "height=200, width=340,left="+(screen.AvailWidth-300)/2+",top="+(screen.AvailHeight-200)/2+", toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no")
}
function calculator()
{
  HtmlEdit.focus();
  var range =HtmlEdit.document.selection.createRange();
  var arr = showModalDialog("Dialog/calculator.htm", "", "dialogWidth:205px; dialogHeight:230px;scroll:no;status:0;help:0");
  
  if (arr != null){
    var ss;
    ss=arr.split("*")
    a=ss[0];
    b=ss[1];
    var str1;
    str1=""+a+""
    range.pasteHTML(str1);
  }
  HtmlEdit.focus();
}
		</script>
	</body>
</HTML>