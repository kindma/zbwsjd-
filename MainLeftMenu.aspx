<%@ Page Language="c#" Inherits="EasyExam.MainLeftMenu" CodeFile="MainLeftMenu.aspx.cs" %>
<HTML>
<HEAD>
<TITLE>网络考试系统</TITLE>
<meta http-equiv="Content-Type" content="text/html;charset=gb2312">

<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=yes" id="viewport" name="viewport">
 
<link rel="stylesheet" type="text/css" href="common.css">
<script language="JavaScript" src="JavaScript/MouseEvent.js"></script>
</HEAD>
<body><!-- #BeginLibraryItem "/Library/header.lbi" --><dl class="headerdd">
<dd class="homeimg"><a href="/MainLeftMenu.aspx"><img src="/home2.png"  /></a></dd>
<dd class="webtitle">淄博市卫生监督局<br>
在线考试系统</dd>
<dd class="menuright"> </dd>
</dl><!-- #EndLibraryItem --><div id="tt" >
  <table cellSpacing="0" cellPadding="0" width="100%" >
    <%=strMainMenu%>
  </table>
</div>
<script language="javascript">
						if(this!=top) top.location.href=location.href;
						function aa(Dir)
						{tt.doScroll(Dir);Timer=setTimeout('aa("'+Dir+'")',100)}//这里100为滚动速度
						function StopScroll(){if(Timer!=null)clearTimeout(Timer)
						}

						function initIt()
						{
						divColl=document.all.tags("DIV");
						for(i=0; i<divColl.length; i++) {
						whichEl=divColl(i);
						if(whichEl.className=="child")whichEl.style.display="none";}
						}

						function expands(el)
						{
						whichEl1=eval(el+"Child");
						if (whichEl1.style.display=="none"){
						initIt();
						whichEl1.style.display="block";
						}else{whichEl1.style.display="none";}
						}

						var tree= 0;
						function loadThreadFollow()
						{
						if (tree==0){
						document.frames["hiddenframe"].location.replace("LeftTree.asp");
						tree=1
						}
						}

						function showsubmenu(sid)
						{
						whichEl = eval("submenu" + sid);
						imgmenu = eval("imgmenu" + sid);
						if (whichEl.style.display == "none")
						{
						eval("submenu" + sid + ".style.display=\"\";");
						imgmenu.background="images/menuup.gif";
						}
						else
						{
						eval("submenu" + sid + ".style.display=\"none\";");
						imgmenu.background="images/menudown.gif";
						}
						}

						function loadingmenu(id)
						{
						var loadmenu =eval("menu" + id);
						if (loadmenu.innerText=="Loading..."){
						document.frames["hiddenframe"].location.replace("LeftTree.asp?menu=menu&id="+id+"");
						}
						}
						 
					</script>
</body>
</HTML>
