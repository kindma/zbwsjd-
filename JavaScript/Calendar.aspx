<%@ Page %>
<HTML>
	<HEAD>
		<TITLE>����</TITLE>
		<link rel="stylesheet" href="../Css/Common.css">
			<script language="C#" runat="server">
	//Response.Buffer = True 
	//Response.ExpiresAbsolute = Now()-0.1 
	//Response.Expires = 0 
	//Response.CacheControl = "no-cache"
			</script>
			<STYLE TYPE="text/css">
				.normal{FONT-FAMILY: "����"; FONT-SIZE: 9pt;BACKGROUND: #ffffff}
				.today {FONT-FAMILY: "����"; FONT-SIZE: 9pt;BACKGROUND: #6699cc}
				.satday{FONT-FAMILY: "����"; FONT-SIZE: 9pt;color:green}
				.sunday{FONT-FAMILY: "����"; FONT-SIZE: 9pt;color:red}
				.days  {FONT-FAMILY: "����"; FONT-SIZE: 9pt;font-weight:bold}
				.clickday {FONT-FAMILY: "����"; FONT-SIZE: 9pt;BACKGROUND: #FDBF6E}
			</STYLE>
			<SCRIPT LANGUAGE="JavaScript">
			var parameter=window.dialogArguments;
			var hasTime=parameter.hasTime;
			var CurrentDate=new Date(parameter.oldValue);
			
			
//�����·�,�������ʾӢ���·ݣ��༭�����ע��
/*var months = new Array("January?, "February?, "March",
"April", "May", "June", "July", "August", "September",
"October", "November", "December");*/
var months = new Array("һ��", "����", "����",
"����", "����", "����", "����", "����", "����",
"ʮ��", "ʮһ��", "ʮ����");
var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31,
30, 31, 30, 31);
//������ �������ʾ Ӣ�ĵģ��༭�����ע��
/*var days = new Array("Sunday", "Monday", "Tuesday",
"Wednesday", "Thursday", "Friday", "Saturday");*/
var days = new Array("��","һ", "��", "��","��", "��", "��");
function getDays(month, year) {
//�������δ������жϵ�ǰ�Ƿ��������
if (1 == month)
return ((0 == year % 4) && (0 != (year % 100))) ||
(0 == year % 400) ? 29 : 28;
else
return daysInMonth[month];
}

function getToday() {
//�õ��������,��,��
this.now = new Date();
this.year = this.now.getFullYear();
this.month = this.now.getMonth();
this.day = this.now.getDate();
}
today = new getToday();
var todayID="";
function preYear(){
	if(document.all.year.selectedIndex==0) return;
	document.all.year.selectedIndex=document.all.year.selectedIndex-1;
	newCalendar();
}
function nextYear(){
	if(document.all.year.selectedIndex==eval(document.all.year.length-1)) return;
	document.all.year.selectedIndex=document.all.year.selectedIndex+1;
	newCalendar();
}
function preMonth(){
	if(document.all.month.selectedIndex==0){
		if(document.all.year.selectedIndex==0) return;
		document.all.year.selectedIndex=document.all.year.selectedIndex-1;
		document.all.month.selectedIndex=document.all.month.length-1;
		newCalendar();
		return;
	 }
	document.all.month.selectedIndex=document.all.month.selectedIndex-1;
	newCalendar();
}
function nextMonth(){
	if(document.all.month.selectedIndex==eval(document.all.month.length-1)){
		if(document.all.year.selectedIndex==eval(document.all.year.length-1)) return;
		document.all.year.selectedIndex=document.all.year.selectedIndex+1;
		document.all.month.selectedIndex=0;
		newCalendar();
		return;
	 }
	document.all.month.selectedIndex=document.all.month.selectedIndex+1;
	newCalendar();
}

function newCalendar() {

today = new getToday();
var parseYear = parseInt(document.all.year[document.all.year.selectedIndex].text);
var newCal = new Date(parseYear,
document.all.month.selectedIndex, 1);
var day = -1;
var startDay = newCal.getDay();
var daily = 0;
if ((today.year == newCal.getFullYear()) &&(today.month == newCal.getMonth()))
day = today.day;
var tableCal = document.all.calendar.tBodies.dayList;
var intDaysInMonth =getDays(newCal.getMonth(), newCal.getFullYear());
for (var intWeek = 0; intWeek < tableCal.rows.length;intWeek++)
	for (var intDay = 0;intDay < tableCal.rows[intWeek].cells.length;intDay++)
	{
		var cell = tableCal.rows[intWeek].cells[intDay];
		if ((intDay == startDay) && (0 == daily))
			daily = 1;
		if(day==daily)
		{
			//���죬���ý����Class
			cell.className = "today";
			todayID="tdDay"+intWeek+"_"+intDay;
		}
		else if(intDay==6)
			//����
			cell.className = "sunday";
		else if (intDay==0)
			//����
			cell.className ="satday";
		else
			//ƽ��
			cell.className="normal";
			if ((daily > 0) && (daily <= intDaysInMonth))
			{ 
				cell.innerText = daily;
				daily++;
			}
			else
				cell.innerText = "";
	}
	
	sOldClickDate=todayID;
}

function Cancel() {
	window.returnValue=-1;
	window.close();
}

function getDate() {
	var sDate;
	//��δ��봦������������
	if ("TD" == event.srcElement.tagName)
	{
		if ("" != event.srcElement.innerText)
		{
			var returnVaue=document.all.year.value + "-" + document.all.month.value + "-" + event.srcElement.innerText + "";
			if(document.all("chkReturnTime").checked) returnVaue=returnVaue+" "+document.all("txtHour").value+":"+document.all("txtMinute").value+":"+document.all("txtSecond").value
			window.returnValue=returnVaue
			window.close();
		}
	}
	else
	{
		 if(event.srcElement.id=="btnOK")
		 {
			try{
				var returnVaue=document.all.year.value + "-" + document.all.month.value + "-" + document.all(sOldClickDate).innerText + "";
				if(document.all("chkReturnTime").checked) returnVaue=returnVaue+" "+document.all("txtHour").value+":"+document.all("txtMinute").value+":"+document.all("txtSecond").value
				window.returnValue=returnVaue
				window.close();
			}catch(e){
				alert("��ѡ�����ڣ�");
			}
		 }
	}
}

var sOldClickDate="";
function setClickDate()
{
	if(event.srcElement.innerText=="") return;
	if(sOldClickDate!="")
		if(todayID==sOldClickDate)
			document.all(sOldClickDate).className="today";
		else
			document.all(sOldClickDate).className="";
	event.srcElement.className="clickday";
	sOldClickDate=event.srcElement.id;
}

			</SCRIPT>
	</HEAD>
	<BODY ONLOAD="newCalendar()" style="MARGIN:0px;OVERFLOW:auto">
		<TABLE cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
			<TR height="30" bgcolor="#fdbf6e">
				<TD ALIGN="center"><INPUT id="btnPreYear" title="��һ��" onclick="preYear();" type="button" value="<<" class="GoButton"><SELECT ID="year" ONCHANGE="newCalendar()"><SCRIPT LANGUAGE="JavaScript">
							for (var intLoop = today.year-50; intLoop < (today.year + 11);
							intLoop++)
							document.write("<OPTION VALUE= " + intLoop + " " +
							(today.year == intLoop ?
							"Selected" : "") + ">" +
							intLoop);
						</SCRIPT>
					</SELECT><INPUT id="btnNextYear" title="��һ��" onclick="nextYear();" type="button" value=">>" name="btnNextYear"
						class="GoButton"> <INPUT id="btnPreMonth" title="��һ��" onclick="preMonth();" type="button" value="<<" name="btnPreMonth"
						class="GoButton"><SELECT ID="month" ONCHANGE="newCalendar()">
						<SCRIPT LANGUAGE="JavaScript">
for (var intLoop = 0; intLoop < months.length;intLoop++)
document.write("<OPTION VALUE= " + (intLoop + 1) + " " +(today.month == intLoop ?"Selected" : "") + ">" +months[intLoop]);
						</SCRIPT>
					</SELECT><INPUT id="btnNextMonth" title="��һ��" onclick="nextMonth();" type="button" value=">>" name="btnNextMonth"
						class="GoButton"> <Input type="button" id="btnOK" value="ȷ��" OnClick="getDate();" class="ShortButton"><Input type="button" id="btnEmpty" value="���" OnClick="window.returnValue='';window.close();"
						class="ShortButton" title="���ؿհ�"><Input type="button" value="ȡ��" OnClick="Cancel();" class="ShortButton">
				</TD>
			</TR>
			<tr bgcolor="#8a622e">
				<td height="1">
				</td>
			</tr>
			<tr>
				<td height="5">
				</td>
			</tr>
		</TABLE>
		<TABLE ID="calendar" cellspacing="0" cellpadding="2" width="100%" border="0" bgcolor="white"
			align="center">
			<TBODY>
				<TR CLASS="days" ALIGN="center">
					<SCRIPT LANGUAGE="JavaScript">
document.write("<TD class=satday>" + days[0] + "</TD>");
for (var intLoop = 1; intLoop < days.length-1;
intLoop++) 
document.write("<TD >" + days[intLoop] + "</TD>");
document.write("<TD class=sunday>" + days[intLoop] + "</TD>");
					</SCRIPT>
				</TR>
				<TBODY width="100%" border="1" cellspacing="0" cellpadding="0" ID="dayList" ALIGN="center"
					ONDBLCLICK="getDate()">
					<SCRIPT LANGUAGE="JavaScript">
for (var intWeeks = 0; intWeeks < 6; intWeeks++) {
document.write("<TR style='cursor:hand'>");
for (var intDays = 0; intDays < days.length;intDays++)
document.write("<TD id='tdDay"+intWeeks+"_"+intDays+"' name='tdDay"+intWeeks+"_"+intDays+"' ONCLICK='setClickDate()'></TD>");
document.write("</TR>");
}
					</SCRIPT>
				</TBODY>
		</TABLE>
		<table id="tblTime" cellspacing="0" cellpadding="2" width="100%" border="0" bgcolor="white"
			align="center">
			<tr>
				<td nowrap width="15px"></td>
				<td nowrap><input id="chkReturnTime" type="checkbox" onclick="chkReturnTime_Onclick()">ʱ�䣺</td>
				<td><input type="text" class="Text" disabled="true" id="txtHour" value="00" style="WIDTH:22px"
						maxLength="2" onblur="CheckInputHour();"></td>
				<td nowrap>
					<select id="ddlHour" disabled="true" onchange="document.all('txtHour').value=document.all('ddlHour').value;">
						<SCRIPT LANGUAGE="JavaScript">
							for(var i=0;i<2;i++)
							{
								for(var j=0;j<10;j++)
								{
									document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
								}
							}
						</SCRIPT>
						<OPTION value="20">20</OPTION>
						<OPTION value="21">21</OPTION>
						<OPTION value="22">22</OPTION>
						<OPTION value="23">23</OPTION>
					</select>Сʱ
				</td>
				<td><input type="text" class="Text" disabled="true" id="txtMinute" value="00" style="WIDTH:22px"
						maxLength="2" onblur="CheckInputMinute();"></td>
				<td nowrap>
					<select id="ddlMinute" disabled="true" onchange="document.all('txtMinute').value=document.all('ddlMinute').value;">
						<SCRIPT LANGUAGE="JavaScript">
							for(var i=0;i<6;i++)
							{
								for(var j=0;j<10;j++)
								{
									document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
								}
							}
						</SCRIPT>
					</select>����
				</td>
				<td><input type="text" class="Text" disabled="true" id="txtSecond" value="00" style="WIDTH:22px"
						maxLength="2" onblur="CheckInputSecond();"></td>
				<td nowrap>
					<select id="ddlSecond" disabled="true" onchange="document.all('txtSecond').value=document.all('ddlSecond').value;">
						<SCRIPT LANGUAGE="JavaScript">
							for(var i=0;i<6;i++)
							{
								for(var j=0;j<10;j++)
								{
									document.write("<OPTION value="+i+j+">"+i+j+"</OPTION>");
								}
							}
						</SCRIPT>
					</select>��
				</td>
				<td width="100%"><FONT face="����"></FONT></td>
			</tr>
		</table>
		<br>
		<SCRIPT LANGUAGE="JavaScript">
			function chkReturnTime_Onclick()
			{
				if(document.all("chkReturnTime").checked)
				{
					document.all("txtHour").disabled=false;
					document.all("txtMinute").disabled=false;
					document.all("txtSecond").disabled=false;
					
					document.all("ddlHour").disabled=false;
					document.all("ddlMinute").disabled=false;
					document.all("ddlSecond").disabled=false;
				}
				else
				{
					document.all("txtHour").disabled=true;
					document.all("txtMinute").disabled=true;
					document.all("txtSecond").disabled=true;
					
					document.all("ddlHour").disabled=true;
					document.all("ddlMinute").disabled=true;
					document.all("ddlSecond").disabled=true;
				}
			}
			
			function CheckInputHour()
			{
				if(document.all('txtHour').value.length==1) document.all('txtHour').value="0"+document.all('txtHour').value;
				document.all('ddlHour').value=document.all('txtHour').value;
				if(document.all('ddlHour').value=='')
				{
					alert('���Ϸ���Сʱ��');document.all('txtHour').focus();
				}
				
			}
			
			function CheckInputMinute()
			{
				if(document.all('txtMinute').value.length==1) document.all('txtMinute').value="0"+document.all('txtMinute').value;
				document.all('ddlMinute').value=document.all('txtMinute').value;
				if(document.all('ddlMinute').value=='')
				{
					alert('���Ϸ��ķ��ӣ�');document.all('txtMinute').focus();
				}
			}
			
			function CheckInputSecond()
			{
				if(document.all('txtSecond').value.length==1) document.all('txtSecond').value="0"+document.all('txtSecond').value;
				document.all('ddlSecond').value=document.all('txtSecond').value;
				if(document.all('ddlSecond').value=='')
				{
					alert('���Ϸ����룡');document.all('txtSecond').focus();
				}
			}
			
			if(hasTime=="yes")
			{
				document.all("chkReturnTime").checked=true;
				chkReturnTime_Onclick();
				document.all("chkReturnTime").style.display="none";
			}
			else if(hasTime=="no")
			{
				document.all("chkReturnTime").checked=false;
				chkReturnTime_Onclick();
				document.all("tblTime").style.display="none";
			}
		</SCRIPT>
	</BODY>
</HTML>
