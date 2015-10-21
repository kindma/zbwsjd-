window.focus();
function jscomResizeWindow(width,height)
{
	var left,top;
	left=(screen.width-width)/2;
	if(left<0){ left=0;}

	top=(screen.height-60-height)/2;
	if(top<0){ top=0;}
	
	window.resizeTo(width,height);
	window.moveTo(left,top);
}

function jscomNewOpenFullScreen(url,target)
{
	var w=window.open(url,target,"fullscreen=yes,toolbar=no,scrollbars=yes");
	try{
		w.focus();
	}catch(e){}
}

function jscomNewOpenBySize(url,target,width,height){
        var tt,w,left,top;
		if (!width) width=screen.width;
		if (!height) height=screen.height-60;
		left=(screen.width-width)/2;
		if(left<0){ left=0;}

		top=(screen.height-60-height)/2;
		if(top<0){ top=0;}

        tt="toolbar=no, menubar=no, scrollbars=yes,resizable=yes,location=no, status=yes,";
        tt=tt+"width="+width+",height="+height+",left="+left+",top="+top;
	    w=window.open(url,target,tt);
		try{
			w.focus();
		}catch(e){}
}

function jscomNewOpenByFixSize(url,target,width,height){
        var tt,w,left,top;
		if (!width) width=screen.width;
		if (!height) height=screen.height-60;
		left=(screen.width-width)/2;
		if(left<0){ left=0;}

		top=(screen.height-60-height)/2;
		if(top<0){ top=0;}

        tt="toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=yes,";
        tt=tt+"width="+width+",height="+height+",left="+left+",top="+top;
	    w=window.open(url,target,tt);
	try{
		w.focus();
	}catch(e){}
}

function jscomNewOpenBySizePos(url,target,width,height,left,top){
			var tt;

	        tt="toolbar=no, menubar=no, scrollbars=no,resizable=yes,location=no, status=yes,";
			tt=tt+",width="+width+",height="+height;
			tt=tt+",left="+left+",top="+top;
			w=window.open(url,target,tt);
	try{
		w.focus();
	}catch(e){}
}

function jscomNewOpenModalDialog(url,width,height){
	return showModalDialog(url,window,'dialogWidth:'+width+'px; dialogHeight:'+height+'px;help:0;status:0;resizeable:1;');
}

function jscomGetParentFromSrc(src,parTag){
	if(src && src.tagName!=parTag)
		src=getParentFromSrc(src.parentElement,parTag);
	return src;
}

function jscomGetSubString(str,begin_pos,num){
	return str.toString().substring(begin_pos,begin_pos+num);
}

function jscomDoNothing(){
}

//过滤特殊符号（如' "） 
function jscomFiltrateSomeKeyForKeyPress(){
	if(event.keyCode==39 || event.keyCode==34){
		event.keyCode=0;
	}
}

//只能输入数字
function jscomOnlyNumForKeypress(){
	//alert(event.keyCode);
	if(event.keyCode<48 || event.keyCode>57){   //0=>48  9=>57
		event.keyCode=0;
		return false;
	}else{
		return true;
	}
}

//设置检查框 flag =1 全部选中 或 0=全部清除
function jscomSelectCheckFlag(frm,flag){
	var src;
	for (var i=0;i<frm.elements.length;i++){
		src=frm.elements[i];
		if(src.type=="checkbox"){
			if(flag==1){
				src.checked=true;
			}else{
				src.checked=false;
			}
		}
    }
}
//判断是否有检查框被选中
//返回 true有  false 无
function jscomIsCheckBoxSelect(frm){
	var frm,src;

	flag=false;
	for (var i=0;i<frm.elements.length;i++){
		src=frm.elements[i];
		if(src.type=="checkbox" && src.checked){
				flag=true;
				break;
		}
    }
    return flag;
}

//检查输入：	只能输入有效数字 失去交点时判断
function jscomOnBlurCheckForOnlyNumber(){
	var src=window.event.srcElement;
	
	if(src.tagName=="INPUT"){
		if(isNaN(src.value)){
			window.alert("请输入数字类型！");
			src.focus();
/*		}else if(parseFloat(src.value)<0){
			window.alert("请输入大于0的数字！");
			src.focus();
*/
		}
	}
}
//检查输入：	只能输入整数 失去交点时判断
function jscomOnBlurCheckForOnlyInteger(){
	var src=window.event.srcElement;
	
	if(src.tagName=="INPUT"){
		if(isNaN(src.value)){
			window.alert("请输入数字类型！");
			src.focus();
		}else if(src.value.indexOf(".",0)>-1){
			window.alert("请输入整型数字！");
			src.focus();
/*
		}else if(parseInt(src.value)<0){
			window.alert("请输入大于0的整型数字！");
			src.focus();
*/
		}
	}
}

function jscomIsEmptyString(str){ 
	return ((!str)||(str.length == 0)); 
}

//检查日期是否正确  格式2002-09-11
function jscomIsValidDate(strDate){ 
	if (jscomIsEmptyString(strDate)){
		//alert("请输入日期!");
		return false;
	}
	var lthdatestr=strDate.length ;
	var tmpy="";
	var tmpm="";
	var tmpd="";
	var status=0;

	for (i=0;i<lthdatestr;i++){
		if (strDate.charAt(i)== '-'){
			status++;
		}
		if (status>2){
			//alert("请用'-'作为日期分隔符！");
			return false;
		}
		if ((status==0) && (strDate.charAt(i)!='-')){
			tmpy=tmpy+strDate.charAt(i)
		}
		if ((status==1) && (strDate.charAt(i)!='-')){
			tmpm=tmpm+strDate.charAt(i)
		}
		if ((status==2) && (strDate.charAt(i)!='-')){
			tmpd=tmpd+strDate.charAt(i)
		}
	}

	year=new String(tmpy);
	month=new String(tmpm);
	day=new String(tmpd)

	if ((tmpy.length!=4) || (tmpm.length>2) || (tmpd.length>2)){
		//alert("日期格式错误！");
		return false;
	}
	if (!((1<=month) && (12>=month) && (31>=day) && (1<=day)) ){
		//alert ("错误的月份或天数！");
		return false;
	}
	if (!((year % 4)==0) && (month==2) && (day==29)){
		//alert ("这一年不是闰年！");
		return false;
	}
	if ((month<=7) && ((month % 2)==0) && (day>=31)){
		//alert ("这个月只有30天！");
		return false;
	}
	if ((month>=8) && ((month % 2)==1) && (day>=31)){
		//alert ("这个月只有30天！");
		return false;
	}
	if ((month==2) && (day==30)){
		//alert("2月永远没有这一天！");
		return false;
	}
	return true;
}


/*格式化数字
	num  要格式化的数值
	decimal_num	小数位数 
	has_split 是否要千分为分割符 true or false
	
	返回 格式化的字符串
*/
function jscomFormatNumber(num,decimal_num,has_split){
	if(isNaN(num)){return num;} //非数值，直接返回
	
	
	var tmp_num,tmp_decimal_num;
	
	tmp_decimal_num=decimal_num;
	if(isNaN(decimal_num)){tmp_decimal_num=0;} 
	
	tmp_num=num*Math.pow(10,tmp_decimal_num);
	tmp_num=Math.round(tmp_num);
	tmp_num=tmp_num / Math.pow(10,tmp_decimal_num);
	
	if(!has_split){ return tmp_num;}
	
	return tmp_num;	//千分为分割符 以后处理
}	

//获得指定其日的字符串
function jscomGetDateStr(ftype_name){
	var ret_str,objDate;
	var year,month,day;
	
	objDate=new Date();
	year=objDate.getFullYear();
	month=objDate.getMonth()+1;
	day=objDate.getDate();

	switch(ftype_name){
		case "now_date":	//本日
				ret_str=year+"-"+month+"-"+day;
				break;
		case "yestoday":	//昨天
				objDate.setDate(objDate.getDate()-1);
				year=objDate.getFullYear();
				month=objDate.getMonth()+1;
				day=objDate.getDate();
				ret_str=year+"-"+month+"-"+day;
				break;
		case "month_begin":	//本月初
				ret_str=year+"-"+month+"-1";
				break;
		case "month_end":	//本月末
				objDate.setMonth(month);
				objDate.setDate(0);
				ret_str=year+"-"+month+"-"+objDate.getDate();
				break;
		case "pre_month_begin":	//上月初
				objDate.setMonth(objDate.getMonth()-1);
				year=objDate.getFullYear();
				month=objDate.getMonth()+1;
				day=objDate.getDate();
				ret_str=year+"-"+month+"-1";
				break;
		case "pre_month_end":	//上月末
				objDate.setMonth(month-1);
				objDate.setDate(0);
				year=objDate.getFullYear();
				month=objDate.getMonth()+1;
				day=objDate.getDate();
				ret_str=year+"-"+month+"-"+day;
				break;
		case "year_begin":	//本年初
				ret_str=year+"-01-01";
				break;
		case "year_end":	//本年末
				objDate.setMonth(12);
				objDate.setDate(0);
				ret_str=year+"-12-"+objDate.getDate();
				break;
		case "pre_year_begin":	//上年初
				year=year-1;
				ret_str=year+"-01-01";
				break;
		case "pre_year_end":	//上年末
				objDate.setYear(objDate.getYear()-1);
				objDate.setMonth(12);
				objDate.setDate(0);
				year=objDate.getFullYear();
				month=objDate.getMonth()+1;
				day=objDate.getDate();
				ret_str=year+"-"+month+"-"+day;
				break;
		default:	//本日
				ret_str=year+"-"+month+"-"+day;
				break;
	}
	return ret_str;
}

function jscomTrimString(str){
	var ts = "";

	if(str.length < 1) return "";

	for (var i = (str.length - 1); i!=-1; i--) {
		if (str.charAt(i) != ' ') {break;}
	}
	ts = str.substring(0, i+1); 

	for (var i = 0 ; i < ts.length ; i++) {
		if (str.charAt(i) != ' ') {break;}
	}
	return ts.substring(i, ts.length); 
}

function jscomCancelClick(){
	var frms=document.forms;
	for (i=0;i<frms.length;i++) {
		frms(i).reset();
	}
	return false;
}

//伸缩对象，点一下伸开，再点一下收回，并且还可以带图片显示伸缩的标至
function jscomFlexObject(obj,imageObj,onImagePath,offImagePath)
{
	if (obj.style.display=="none")
	{
		obj.style.display = "block";
		if (imageObj && onImagePath)
			imageObj.src = onImagePath;
		//oElement.alt = "收缩";
	}
	else
	{
		obj.style.display = "none";
		if (imageObj && offImagePath)
			imageObj.src = offImagePath;
		//oElement.alt = "展开";
	}
}

//客户端字符串编码
function ClientHtmlEncode(str)
{
	var strRtn="";
	if(!str) return strRtn;
	for (var i=str.length-1;i>=0;i--)
	{	
		strRtn+=str.charCodeAt(i);
		if (i) strRtn+="a"; //with a to split
	}
	return strRtn;
}

//客户端字符串解码
function ClientHtmlDecode(str)
{
	var strArr;
	var strRtn="";
	if(!str) return strRtn;
	strArr=str.split("a");
	for (var i=strArr.length-1;i>=0;i--)
	{
		if(strArr[i]!='') strRtn+=String.fromCharCode(eval(strArr[i]));
	}
	return strRtn;
}

//关闭窗体时检查有没有更新，如有则刷新父窗体
function jscomCloseAndRefreshOpener(IsRefreshOpener){
	if(!IsRefreshOpener)
	{
		IsRefreshOpener=false;
		try{
			if(document.all("hidHasUpdate").value=="True")
				IsRefreshOpener=true;
			else
				IsRefreshOpener=false;
		}catch(e){}
	}
	if(!top)
	{
		try{
			if(IsRefreshOpener==true)
				opener.RefreshForm();
		}catch(e){}
		window.close();
	}
	else
	{
		try{
			if(IsRefreshOpener==true)
				top.opener.RefreshForm();
		}catch(e){}
		top.close();
	}
}

function jcomOpenCalender(sSourceControlName,nIndex)
{
	jcomOpenCalenderWithTime("no",sSourceControlName,nIndex);
}

function jcomSelectDateTime(sSourceControlName,nIndex)
{
	jcomOpenCalenderWithTime("yes",sSourceControlName,nIndex);
}

//有时间，但要用户自已选择
function jcomSelectDateOrTime(sSourceControlName,nIndex)
{
	jcomOpenCalenderWithTime("select",sSourceControlName,nIndex);
}

function jcomOpenCalenderWithTime(HasTime,sSourceControlName,nIndex)
{
	if(nIndex==null) nIndex=0;
	var sOldDate;

	if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
		sOldDate=document.all(sSourceControlName)[nIndex].value;
	else
		sOldDate=document.all(sSourceControlName).value;
	var parameter=new Object();
	parameter.hasTime=HasTime;
	parameter.oldValue=sOldDate;
	
	var height=240;
	if(HasTime=="no") height=220;
	
	var strNode=showModalDialog('../JavaScript/Calendar.aspx',parameter,"dialogWidth:380px;dialogHeight:"+height+"px;status:no;scroll:no");
	//var strNode=window.open('../JavaScript/Calendar.aspx',0,"dialogWidth:320px;dialogHeight:185px;status:no");
	if (strNode!=-1 && typeof(strNode)!='undefined')
	{
		if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
			document.all(sSourceControlName)[nIndex].value=strNode;
		else
			document.all(sSourceControlName).value=strNode;
	}
}

function jcomSelectTime(sSourceControlName,nIndex)
{
	var sOldTime;
	if(!nIndex){
		sOldTime=document.all(sSourceControlName).value;
	}else{
		if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
			sOldTime=document.all(sSourceControlName)[nIndex].value;
		else
			sOldTime=document.all(sSourceControlName).value;
	}
	var strNode=showModalDialog('../JavaScript/SelectTime.aspx',sOldTime,"dialogWidth:380px;dialogHeight:150px;status:no;scrollbars=no");
	if (strNode!=-1 && typeof(strNode)!='undefined')
	{
		if(!nIndex){
			document.all(sSourceControlName).value=strNode;
		}else{
			if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
				document.all(sSourceControlName)[nIndex].value=strNode;
			else
				document.all(sSourceControlName).value=strNode;
		}
	}
}

function jcomSelectAllRecords(SelectAllName,SelectName)
{
	if(!SelectAllName)
		SelectAllName="chkSelectAll";
	if(!SelectName)
		SelectName="chkSelect";
	if(document.all(SelectName)==null) return "";	
	var checked=document.all(SelectAllName).checked;
	var sSelectedIDs="";
	if(typeof(document.all(SelectName).length) == 'undefined')
	{
		document.all(SelectName).checked=checked;
		sSelectedIDs=document.all(SelectName).value;
		return sSelectedIDs;
	}
	else
	{
		for (i=0; i < document.all(SelectName).length; i ++) 
		{
			document.all(SelectName)[i].checked=checked;
			sSelectedIDs=sSelectedIDs+document.all(SelectName).value+",";
		}
	}
	if(sSelectedIDs!="") sSelectedIDs=sSelectedIDs.substr(0,sSelectedIDs.length-1);
	return sSelectedIDs;
}

function jcomOpenHtmlEditor()
{
	jscomNewOpenByFixSize("../HtmlEditor/HtmlEditor.aspx","HtmlEditor",760,560);
}
//导出到Word
function jscomExportTableToWord(tableName)
{
	if(document.all(tableName).rows.length==0)
	{
		alert("没有内容可导！");
		return;
	}

	var oWord;
	try
	{
		oWord = new ActiveXObject("Word.Application"); // Get a new workbook.
	}
	catch(e)
	{
		alert("无法调用Office对象，请确保您的机器已安装了Office并已将本系统的站点名加入到IE的信任站点列表中！");
		return;
	}
	var oDocument = oWord.Documents.Add();
	var oDocument = oWord.ActiveDocument; 
	//oDocument.Paragraphs.Add();
	oDocument.Paragraphs.Last.Alignment = 1;
	oDocument.Paragraphs.Last.Range.Bold = true;
	oDocument.Paragraphs.Last.Range.Font.Size = 16;
	oDocument.Paragraphs.Last.Range.Font.name = "宋体";
	oDocument.Paragraphs.Last.Range.InsertAfter(document.all("lblTitle").innerText);

	oDocument.Paragraphs.Add();
	oDocument.Paragraphs.Last.Alignment = 2;
	oDocument.Paragraphs.Last.Range.Bold = false;
	oDocument.Paragraphs.Last.Range.Font.Size = 12;
	oDocument.Paragraphs.Last.Range.Font.name = "宋体";
	oDocument.Paragraphs.Last.Range.InsertAfter(document.all("lblSubTitle").innerText);

	if (document.all("lblMessage").innerText!="")
	{
		oDocument.Paragraphs.Add();
		oDocument.Paragraphs.Last.Alignment = 1;
		oDocument.Paragraphs.Last.Range.Bold = false;
		oDocument.Paragraphs.Last.Range.Font.Size = 8;
		oDocument.Paragraphs.Last.Range.Font.name = "宋体";
		oDocument.Paragraphs.Last.Range.InsertAfter(document.all("lblMessage").innerText);
	}
		
	var table = document.all(tableName);
	var nRows = table.rows.length; 
	var nCols = table.rows(0).cells.length;
	for (i=0;i<nRows;i++)
	{
		nCol=0;
		nCols=table.rows(i).cells.length;
		for (j=0;j<nCols;j++) 
		{ 
			if((table.rows(i).cells(j))&&table.rows(i).cells(j).innerText!="")
			{
				oDocument.Paragraphs.Add();
				oDocument.Paragraphs.Last.Alignment = 0;
				oDocument.Paragraphs.Last.Range.Bold =false;
				if(j==0)
					oDocument.Paragraphs.Last.Range.Font.Size = 14;
				else
					oDocument.Paragraphs.Last.Range.Font.Size = 10;
				oDocument.Paragraphs.Last.Range.Font.name = "宋体";
				oDocument.Paragraphs.Last.Range.InsertAfter(table.rows(i).cells(j).innerText);
			}
			nCol=nCol+1;
		} 
	}
	oWord.Visible = true;
}
//导出到Excel
function jscomExportTableToExcel(tableName,ignoreCols,ignoreLastRows) 
{ 
	// Start Excel and get Application object.
	//if (!confirm("注意：\n导出到Excel将比较耗时，1000条记录大约要一分钟，建议一次导出记录数不超过1000条。你确定要导出吗？")) return;
	//if (!confirm("Exporting will spend long time if there are too many records(1000 Records will spend abount 1 minute).Are you sure to export?")) return;
	var oXL;
	try{
		oXL = new ActiveXObject("Excel.Application"); // Get a new workbook.
	}catch(e)
	{
		alert("无法调用Office对象，请确保您的机器已安装了Office并已将本系统的站点名加入到IE的信任站点列表中！");
		return;
	}
	try{
		var oWB = oXL.Workbooks.Add();
		var oSheet = oWB.ActiveSheet; 
		var table = document.all(tableName);
		var nRows = table.rows.length; 
		if(ignoreLastRows && ignoreLastRows.valueOf()!=0) nRows=nRows-ignoreLastRows.valueOf();
		var nCols = table.rows(0).cells.length; // Add table headers going cell by cell.
		var nCol=0;
		if (!ignoreCols) ignoreCols='';
		//提高效率
		if(ignoreCols=='')
		{
			for (i=0;i<nRows;i++)
			{
				nCol=0;
				for (j=0;j<nCols;j++) 
				{ 
					//if (ignoreCols.indexOf(','+j+',')==-1)
					//{
						if(table.rows(i).cells(nCol)) oSheet.Cells(i+1,nCol+1).value = table.rows(i).cells(nCol).innerText;
						nCol=nCol+1;
					//}
				} 
			}
		}else{
			ignoreCols=','+ignoreCols+',';
			for (i=0;i<nRows;i++)
			{
				nCol=0;
				for (j=0;j<nCols;j++) 
				{ 
					if (ignoreCols.indexOf(','+j+',')==-1)
					{
						if(table.rows(i).cells(j)) oSheet.Cells(i+1,nCol+1).value = table.rows(i).cells(j).innerText;
						nCol=nCol+1;
					}
				} 
			}
		}
		oXL.Visible = true;
		oXL.UserControl = true;
	}catch(e){
		alert("导出失败！"+e);
		//alert("Exporting Fails!"+e);
	}
}
//删除左右两端的空格
function trim(str)
{                                        
	return str.replace(/(^\s*)|(\s*$)/g, "");
}
