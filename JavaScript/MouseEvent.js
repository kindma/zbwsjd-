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
//��������Ҽ���Ctrl+n��shift+F10��F5ˢ�¡��˸��
//alert("ASCII�����ǣ�"+event.keyCode);
  if ((window.event.altKey)&&
      ((window.event.keyCode==37)||   //���� Alt+ ����� ��
       (window.event.keyCode==39))){  //���� Alt+ ����� ��
     alert("��׼��ʹ��ALT+�����ǰ���������ҳ��");
     event.returnValue=false;
     }
  if ((event.keyCode==116)||                 //���� F5 ˢ�¼�
      (event.keyCode==112)||                 //���� F1 ˢ�¼�
      (event.ctrlKey && event.keyCode==82)){ //Ctrl + R
     event.keyCode=0;
     event.returnValue=false;
     }
  if ((event.ctrlKey)&&(event.keyCode==78))   //���� Ctrl+n
     event.returnValue=false;
  if ((event.shiftKey)&&(event.keyCode==121)) //���� shift+F10
     event.returnValue=false;
  if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
      window.event.returnValue = false;  //���� shift ���������¿�һ��ҳ
  if ((window.event.altKey)&&(window.event.keyCode==115)){ //����Alt+F4
      window.event.returnValue = false;}
}

document.oncontextmenu = nocontextmenu; // for IE5+
document.onmousedown = norightclick; // for all others
document.onkeydown = onKeyDown;

//ת��
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
//�����������
function datecheck()
{ 
  if ((event.keyCode<48 || event.keyCode>57) && (event.keyCode!=45))
   {
    event.returnValue=false;
   }	
}
//�����������
function pagecheck()
{ 
  if (event.keyCode<48 || event.keyCode>57)
   {
    event.returnValue=false;
   }
}
//�����������
function numbercheck()
{ 
  if (event.keyCode<48 || event.keyCode>57)
   {
    event.returnValue=false;
   }
}