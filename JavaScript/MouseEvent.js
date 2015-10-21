if (window.Event) 
	document.captureEvents(Event.MOUSEUP); 

function nocontextmenu() 
{
	event.cancelBubble = true;
	event.returnValue = false;
	return false;
}

function norightclick(e) 
{
	if (window.Event) 
	{
	if (e.which == 2 || e.which == 3)
		return false;
	}
	else
	if (event.button == 2 || event.button == 3)
	{
		event.cancelBubble = true;
		event.returnValue = false;
		return false;
	}
}

function onKeyDown()
{ 
//屏蔽鼠标右键、Ctrl+n、shift+F10、F5刷新、退格键
//alert("ASCII代码是："+event.keyCode);
  if ((window.event.altKey)&&
      ((window.event.keyCode==37)||   //屏蔽 Alt+ 方向键 ←
       (window.event.keyCode==39))){  //屏蔽 Alt+ 方向键 →
     alert("不准你使用ALT+方向键前进或后退网页！");
     event.returnValue=false;
     }
  if ((event.keyCode==116)||                 //屏蔽 F5 刷新键
      (event.keyCode==112)||                 //屏蔽 F1 刷新键
      (event.ctrlKey && event.keyCode==82)){ //Ctrl + R
     event.keyCode=0;
     event.returnValue=false;
     }
  if ((event.ctrlKey)&&(event.keyCode==78))   //屏蔽 Ctrl+n
     event.returnValue=false;
  if ((event.shiftKey)&&(event.keyCode==121)) //屏蔽 shift+F10
     event.returnValue=false;
  if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
      window.event.returnValue = false;  //屏蔽 shift 加鼠标左键新开一网页
  if ((window.event.altKey)&&(window.event.keyCode==115)){ //屏蔽Alt+F4
      window.event.returnValue = false;}
}

document.oncontextmenu = nocontextmenu; // for IE5+
document.onmousedown = norightclick; // for all others
document.onkeydown = onKeyDown;

//转换
function cint(str)
{
  var retint=0;
  for(var i=0 ;i<str.length; i++)
  {
    if (str.charAt(i)>"9" || str.charAt(i)<"0")
    {
      break;
    }
    retint=retint*10+(str.charAt(i)-"0")
  }
  return retint;
}
//日期输入检验
function datecheck()
{ 
  if ((event.keyCode<48 || event.keyCode>57) && (event.keyCode!=45))
   {
    event.returnValue=false;
   }	
}
//数字输入检验
function pagecheck()
{ 
  if (event.keyCode<48 || event.keyCode>57)
   {
    event.returnValue=false;
   }
}
//数字输入检验
function numbercheck()
{ 
  if (event.keyCode<48 || event.keyCode>57)
   {
    event.returnValue=false;
   }
}