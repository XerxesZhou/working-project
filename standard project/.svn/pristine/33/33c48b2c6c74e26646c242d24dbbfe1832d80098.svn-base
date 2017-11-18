/**********************************

客户端脚本公共方法,用于所有页面

**********************************/
//获得控件的值
function GetValue(control)
{
    if (control == null)
        return null;
        
    if(control.tagName == "SPAN")
    {
	    return control.innerText;
    }
    else if(control.tagName == "INPUT" || control.tagName == "TEXTAREA")
    {
	    return control.value;
    }
    else if(control.tagName == "SELECT")
    {
	    return control.value;
    } 
    else if (control.type == "checkbox" || control.type == "radio")
    {
        return control.checked ? "1" : "0";
    }
}

function GetValueByColumnName(columnName)
{
    return GetValue(GetElementById(extend + columName));
}

//设置控件的值
function SetValue(control, value)
{
    try
    {
        if(value != null)
        {
            if (control == null)
                return;
                
      	    if(control.tagName == "SPAN")
	        {
		        control.innerText = value;
	        }
	        else if(control.tagName == "INPUT" || control.tagName == "TEXTAREA")
	        {
		        control.value = value;
	        }
	        else if(control.tagName == "SELECT")
	        {
		        control.value = value;
	        }  
	        else if (control.type == "checkbox" || control.type == "radio")
	        {
	            if (value == "1" || value == "True" || sRawValue == "true")
	            {
	                control.checked = true;
	            }
	            else
	            {   
	                control.checked = false;
	            }
	        }
	    }
    }
    catch(err)
    {
        displayErrorMsg("SetValue", err);
    }

}

function SetValueByColumnName(columnName, ctrValue)
{
    SetValue(GetElementById(extend + columnName), ctrValue);
}
//将字符串str中的oldChar替换成newChar
function ReplaceStr( str, oldChar, newChar )
{
    try
    {
        if (str == null)
        {
            return "";
        }
     	var retVal = "";
	    str = str.toString();
	    var strs = str.split(oldChar);
	    for( i = 0; i < strs.length; i++ )
	    {
		    retVal += strs[i] + newChar;
	    }
	    retVal = retVal.substring(0, retVal.length - newChar.length);
	    return retVal;   
    }
    catch(err)
    {
        displayErrorMsg("ReplaceStr", err);
    }

}

//四舍五入，保留unit位小数
function Round(strNum, unit)
{
    try
    {
	    strNum = Math.round (strNum*Math.pow(10,unit))/Math.pow(10,unit);
        return strNum;
	}
    catch(err)
    {
        displayErrorMsg("Round", err);
    }

}

//获得Url中的参数的值
function getUrlParameter(asName)
{
    try
    {
        var lsURL = window.location.href;
        loU = lsURL.split("?");
        if (loU.length>1)
        {
            var loallPm = loU[1].split("&");
            for (var i=0; i<loallPm.length; i++)
            {
                var loPm = loallPm[i].split("=");
                if (loPm[0]==asName)
                {
                    if (loPm.length>1)
                    {
                        return loPm[1];
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }
        return null;
    }
    catch(err)
    {
        displayErrorMsg("getUrlParameter", err);
    } 
}

function getUrlParameter2(asName)
{
    try
    {
        var lsURL = window.location.href;
        loU = lsURL.split("?");
        if (loU.length>1)
        {
            var loallPm = loU[1].split("&");
            for (var i=0; i<loallPm.length; i++)
            {
                var loPm = loallPm[i].split("=");
                if (loPm[0]==asName)
                {
                    if (loPm.length>1)
                    {
                        return loPm[1];
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }
        return "";
    }
    catch(err)
    {
        displayErrorMsg("getUrlParameter2", err);
    } 
}


//功能：把该实体的所有属性对应的控件置空
function setEntityNull(deObj)
{
    if (deObj == null) return;
    
    var strAllPro = "";
    for (var proName in deObj)
    {
        strAllPro += extend + proName + ",";
    } 
    if (strAllPro.length > 0)
    {
        strAllPro = strAllPro.substring(0, strAllPro.length-1); 
    }  
    setnull(strAllPro);
}

//置空某些文本框值,strcontrol是以逗号隔开的
function setnull(strcontrol) 
{
    try
    {
        var s = strcontrol.split(',');
        for( var i = 0; i < s.length-1; i++ )
        {
            try
            {
                var obj = eval("document.all." + s[i]);
                if (obj != null)
                {
                    obj.value = "";
                }
            }
            catch(e)
            {
            }
        }  
    }
    catch(err)
    {
        displayErrorMsg("setnull", err);
    }
}
  
//功能：绑定记录系统字段的文本显示
function BindObjectToControls(deObj, htDataType)
{
    try
    {
        for (var proName in deObj)
        {
            var element = document.getElementById(extend + proName);
            if (element != null)
            {
                if (element.tagName == "SPAN")
                {
                    element.innerText = getFormatValue(deObj[proName], proName, htDataType);
                }
                else if (element.type == "checkbox")
                {
                    if (deObj[proName].toString() == "1" || deObj[proName].toString() == "true")
                    {
                        element.checked = true;
                    }
                    else
                    {
                        element.checked = false;
                    }
                }
                else if (element.type == "select-one")
                {
                    element.value = deObj[proName];
                    //其前面的替代文本框也要更新
                    var newElement = document.getElementById(proName);
                    if (newElement != null && element.selectedIndex != -1)
                    { 
                        newElement.value = element.options[element.selectedIndex].text;
                    }
                }
                else
                {
                    element.value = getFormatValue(deObj[proName], proName, htDataType);
                }
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("BindObjectToControls", err);
    } 
} 

//获得格式化后的值
function getFormatValue(rawValue, proName, htDataType)
{
    var result = rawValue;
    try
    {
        if (htDataType != null && htDataType[proName] != null)
        {
            result = getFormatStr(rawValue, htDataType[proName]);
        }  
         
    }
    catch(err)
    {
        displayErrorMsg("getFormatValue", err);
    }
    
    return result; 
 }

var extend = "Field";

//将某表的所有数据绑定到控件
function BindRecordToControlByTableName(tableName, rowID, prefix)
{
    try
    {
        var results = new Array();
        results = SmartSoft.Presentation.BaseController.GetRecordAllByTableName(tableName, rowID).value;
        if (results != null && results.length > 0)
        {
            bindRecordToControl(results, prefix);
        }
    }
    catch(err)
    {
        displayErrorMsg("BindRecordToControlByTableName", err);
    }
}

//将数据绑定到控件
//并进行相应的格式化
function bindRecordToControl(results, prefix)
{
    try
    {
        if (results == null || results.length <= 0)
            return;
        var sColumnInfo = "";
        var sColumnName = "";
        var sFormatValue = "";
        var sRawValue = "";
        //ColumnName|FormatValue|RawValue
        for (var i = 0; i < results.length; i++)
        {
            if (results[i] != null)
            {
                sColumnInfo =  results[i].split("|");
                sColumnName = sColumnInfo[0];
                sFormatValue = sColumnInfo[1];
                sRawValue = sColumnInfo[2];
                var element;
                if (prefix)
                {
                    element = document.getElementById(prefix + sColumnName);
                }
                else
                {
                    element = document.getElementById(extend + sColumnName);
                }
                if (element != null)
                {
                    if (element.tagName == "SPAN")
                    {
                        element.innerText = sFormatValue;
                    }
                    else if (element.type == "checkbox" || element.type == "radio")
                    {
                        element.checked = (sRawValue == "True" || sRawValue == "true" || sRawValue == true || sRawValue == "1");
                    }
                    else if (element.tagName == "SELECT")
                    {
                        element.value = sRawValue;
                    }
                    else
                    {
                        element.value = sFormatValue;
                    }
                }
            }
        } 
    }
    catch(err)
    {
        displayErrorMsg("bindRecordToControl", err);
    }
}
//功能：绑定记录的字段值到页面的相应控件中
//deObj：包含记录的系统字段信息的对象
//tableID,rowID：一起可从数据库表中获得该记录对应自定义字段的所有信息
function BindFieldToControl(deObj, tableID, rowID, htDataType)
{
    BindObjectToControls(deObj, htDataType); 
}
//功能：置代表该属于该table自定义字段属性的页面元素为空
function SetCustomValueNull(tableID)
{
    try
    {
        var columnNames = new Array();
        columnNames = SmartSoft.Presentation.BaseController.GetCustomColumnNamesByTableID(tableID).value;
        if (columnNames != null)
        {
            for (var i = 0; i < columnNames.length; i++)
            {
                var element = document.getElementById(extend + columnNames[i]);
                if (element != null)
                {
                    element.value = "";
                }
                //如果自定义字段是下拉框，则显示的是我们新增的文本框，因此也需要清除
                element = document.getElementById(columnNames[i]);  
                if (element != null)
                {
                    element.value = "";
                }
            } 
        }
    }
    catch(err)
    {
        displayErrorMsg("SetCustomValueNull", err);
    }  
}

//功能：获得格式化字符串
function getFormatStr(rawValue, dataType)
{
    try
    {
        var formatStr = rawValue;
        switch(dataType)
        {
            case 2:
                formatStr = getDateTimeFormatStr(rawValue);
                break;
            case 3:
                formatStr = getCurrencyStr(rawValue, 2);
                break;
            case 4:
                formatStr = getDateFormatStr(rawValue);
                break;
            default:
                break;
        }
    }
    catch(err)
    {
        displayErrorMsg("getFormatStr", err);
    } 
    
    return formatStr;
}

//功能：获得日期时间格式化字符串 
function getDateTimeFormatStr(rawValue)
{
    var formatStr = rawValue;

    try
    {
        if (/(\d{1,4})\-(\d{1,2})\-(\d{1,2})\s*(([0-1]?\d|2[0-3]})\:([0-5]?\d)(:([0-5]?\d))?)?/.test(rawValue))
        {
            var details = rawValue.split(" ");
            var date = details[0];
            
            var dateDetail = date.split("-");
            if (dateDetail.length == 3)
            {
                var year = dateDetail[0];
                var month = dateDetail[1];
                var day = dateDetail[2];
                if (month.length < 2)
                {
                    month = "0" + month;
                }
                if (day.length < 2)
                {
                    day = "0" + day;
                }
                formatStr = year + "-" + month + "-" + day;
            }
            
            if (details.length >= 2)
            {
                var time = details[1];
                var timeDetail = time.split(":");
                
                if (timeDetail.length == 3)
                {
                    var hour = timeDetail[0];
                    var minute = timeDetail[1];
                    var second = timeDetail[2];
                    if (hour.length < 2)
                    {
                        hour = "0" + hour;
                    }
                    if (minute.length < 2)
                    {
                        minute = "0" + minute;
                    }
                    if (second.length < 2)
                    {
                        second = "0" + second;
                    }
                    formatStr += " " + hour + ":" + minute;
                }
            }
            else
            {
                formatStr += " 00:00";
            } 
        }   
    }
    catch(err)
    {
        displayErrorMsg("getDateTimeFormatStr", err);
    } 
   
    return formatStr;
}

//功能：获得日期格式化字符串
function getDateFormatStr(rawValue)
{
    var formatStr = rawValue;

    try
    {
        if (/(\d{1,4})\-(\d{1,2})\-(\d{1,2})\s*(([0-1]?\d|2[0-3]})\:([0-5]?\d)(:([0-5]?\d))?)?/.test(rawValue))
        {
            var details = rawValue.split(" ");
            var date = details[0];
            var dateDetail = date.split("-");
            if (dateDetail.length == 3)
            {
                var year = dateDetail[0];
                var month = dateDetail[1];
                var day = dateDetail[2];
                if (month.length < 2)
                {
                    month = "0" + month;
                }
                if (day.length < 2)
                {
                    day = "0" + day;
                }
                formatStr = year + "-" + month + "-" + day;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("getDateFormatStr", err);
    } 
    
    return formatStr;
}

//功能：获得货币格式化字符串, 保留radixLength位小数
function getCurrencyStr(rawValue, radixLength)
{
    var formatStr = rawValue.toString();

    try
    {
        var index = formatStr.indexOf('.');
        if (index == -1)
        {
            formatStr += ".";
            index = formatStr.length - 1;
        }
        
        //不够长的补0
        for (var i = formatStr.length; i <= index + radixLength; i++)
        {
            formatStr += "0";
        }
        
        //太长的截去
        formatStr = formatStr.substring(0, index + radixLength + 1);
    }
    catch(err)
    {
        displayErrorMsg("getCurrencyStr", err);
    } 
    
    return formatStr;
}

//返回List页面
function GoBack(url)
{
   window.location = url;
}
      
//功能：遍历一个对象的所有属性，调试时用
function display(obj, attribute)
{
    var meg = getAttr(obj, attribute);
    alert(meg);
}

//功能：获得对象obj的属性,只有obj参数时将列出其所有属性
//      只有两个参数且第二个参数是*时将列出obj的所有非空属性
//      否则输出指向属性
function getAttr(obj)
{
    try
    {
        var meg = "";
        var i = 0;
        if (arguments.length <= 1)  //输出对象所有属性值对
        {
            for (var att in obj)
            {
                meg += att + ":" + obj[att] + "\t";
                if (++i % 2 == 0)
                {
                    meg += "\n";
                }  
            }
        }                           //输出对象所有非空属性值对
        else if (arguments.length == 2 && arguments[1].toString() == "*")
        {
            for (var att in obj)
            {
                if (obj[att] != null)
                {
                    meg += att + ":" + obj[att] + "\t";
                    if (++i % 2 == 0)
                    {
                        meg += "\n";
                    } 
                }
            }
        }
        else                    //输出对象所有指定属性值对
        {
            for (var att in obj)
            {
                if (obj[att] != null)
                {
                    var bHas = false;
                    for (var j = 1; j < arguments.length; j++)
                    {
                        if (att == arguments[j].toString())
                        {
                            bHas = true;  
                            break;
                        }
                    }
                    if (bHas)
                    {
                        meg += att + ":" + obj[att] + "\t";
                        if (++i % 2 == 0)
                        {
                            meg += "\n";
                        } 
                    }
                }
            }
        }
        
        return meg;   
    }
    catch(err)
    {
        displayErrorMsg("getAttr", err);
    }    
}

//功能：提示错误信息
function displayErrorMsg(fnName, err)
{
    alert(fnName + "函数中出错，请检查！" + "\r\n错误名称：" + err.name + "\r\n错误信息：" + err.message);
}

var oldValue = ""; 
var MONEY_FLAG = ""; 

//控制输入的值为有效的数字,在文本框的KeyPress事件中调用
function controlNumberKeyPress(textbox) 
{
    try
    {
        oldValue = textbox.value.replace(MONEY_FLAG,''); 
        return /\d/.test(String.fromCharCode(event.keyCode))||(textbox.value.indexOf('-') < 0 ? String.fromCharCode(event.keyCode) == "-" : false); 
    }
    catch(err)
    {
        displayErrorMsg("controlNumberKeyPress", err);
    } 
} 
 
//是否有效的数字
function isValidNumberInput(txtValue)
{
    txtValue = txtValue.toString();
    if (/^-?\d*.{0,1}\d*$/.test(txtValue))
    {
        for (var i = 0; i < txtValue.length; i++)
        {
            var c = txtValue.charAt(i);
            if (!(c >= '0' && c <= '9' || c == '.' || c == '-'))
            {
                return false;
            }
        }
        return true;
    }
    return false;
}

//控制数字录入keyUp事件 
function controlNumberOnKeyUp(textbox) 
{ 
    try
    {
        //如果为向左、向右、退格键，则不处理
        var nKeyCode = event.keyCode;
        if (nKeyCode == 37 || nKeyCode == 39 || nKeyCode == 8)
        {
            return;
        }
        var newValue = textbox.value;
        newValue = newValue.replace(/\,/g, "");
        if (isValidNumberInput(newValue))
        {
            textbox.value = newValue;
            return;
        }
        while(!isValidNumberInput(newValue) && newValue.length > 0)
        {
            newValue = newValue.substring(0, newValue.length-1);
        }

        textbox.value = newValue;
    }
    catch(err)
    {
        displayErrorMsg("controlNumberOnKeyUp", err);
    }
} 

//控制数字录入blur事件 
function  controlNumberOnblur(textbox, unit)
{
    try
    {
        if (unit == null || unit == "undefine")
        {
            unit = 2;
        }
        var textValue = textbox.value;
        if (textValue == "")
        {
            return;
        }
        textValue = textValue.replace(/\,/g, "");
        textValue = Round(textValue, unit);
        //textValue = ChangedWithThousandTag(textValue);
        textbox.value = textValue;
    }
    catch(err)
    {
        displayErrorMsg("controlNumberOnblur", err);
    }    
}


//获得没有千分号的
function GetNumberWithoutThousandTag(number)
{
    return number.toString().replace(/\,/g, "") - 0;
}

function ChangedWithThousandTag(number)
{
    try
    {
        var result = ""
        var number = number.toString();
        var paras = number.split(".");
        if (paras.length > 1)
        {
            var integerPart = paras[0];
            var sectionPart = paras[1];
            result = commafy(integerPart).toString() + "." + sectionPart.toString();
        }
        else
        {
            result = commafy(number);
        }
        
        return result;
    }
    catch(err)
    {
        displayErrorMsg("ChangedWithThousandTag", err);
    }
}

function commafy(num)
{       
    num = num + "";       
    var re = /(-?\d+)(\d{3})/       
    while(re.test(num))
    {       
        num = num.replace(re, "$1,$2")       
    } 
     
    return num;       
}   

 
//判断是否是数字 
function isNumber(param) 
{ 
    try
    {
        return /\d/.test(String.fromCharCode(param)) ||  (param >= 96 && param <= 105);//加上小键盘处理 
    }
    catch(err)
    {
        displayErrorMsg("isNumber", err);
    }
} 

//是否日期
function isDate(theDate)
{
    try
    {
        var reg = /^\d{1,4}-((0?\d)|(1[0-2]{1}))-((0?[1-9]{1})|([1-2]{1}\d)|(3[0-1]{1}))(\s+(([0-1]?\d|2[0-3])\:([0-5]?\d))?(:([0-5]?\d))?)?$/;  
        var reg1 = /^\d{1,4}\/((0?\d)|(1[0-2]{1}))\/((0?[1-9]{1})|([1-2]{1}\d)|(3[0-1]{1}))(\s+(([0-1]?\d|2[0-3])\:([0-5]?\d))?(:([0-5]?\d))?)?$/;  
        var result = true;
        if(!reg.test(theDate) && !reg1.test(theDate))
        {
            result = false;
        }
        else
        {
            var arr_hd=theDate.split("-");
            if(arr_hd.length <= 1)
            {
                arr_hd=theDate.split("/");
            }
            var dateTmp;
            dateTmp = new Date(arr_hd[0],parseFloat(arr_hd[1])-1,parseFloat(arr_hd[2]));
            if(
                dateTmp.getFullYear()!=parseFloat(arr_hd[0])
                || dateTmp.getMonth()!=parseFloat(arr_hd[1]) -1 
                || dateTmp.getDate()!=parseFloat(arr_hd[2])
              )
            {
                result = false
            }
        }
        return result;
    }
    catch(err)
    {
        displayErrorMsg("isDate", err);
        return false;
    }
}

function handleCalendarBlur(txtObj)
{
    try
    {
        if (txtObj.value != "" && !isDate(txtObj.value))
        {
            alert("请输入格式如:2007-06-09的合法日期!");
            txtObj.value = "";
            txtObj.focus();
        }
        
    }
    catch(err)
    {
        displayErrorMsg("handleCalendarFocus", err);
    }
}

//控制日期显示或者清空的复选框的OnChange事件处理函数
function ChangeTimeCommon(obj, objName1, objName2)
{
    try
    {
        var dateBegin = document.getElementById(objName1);
        var dateEnd = document.getElementById(objName2);
        if(obj.checked)
        {
            dateBegin.disabled = false;    
            dateBegin.value = dateBegin.valueBackup != null ? dateBegin.valueBackup : "";
            dateEnd.disabled = false;
            dateEnd.value = dateEnd.valueBackup != null ? dateEnd.valueBackup : "";
        }
        else
        {
            dateBegin.disabled = true;
            dateBegin.valueBackup = dateBegin.value;
            dateBegin.value = "";
            dateEnd.disabled = true;
            dateEnd.valueBackup = dateEnd.value;
            dateEnd.value = "";
        }
        changeOnlyStyle(obj.checked, dateBegin);
        changeOnlyStyle(obj.checked, dateEnd);
    }
    catch(err)
    {
        displayErrorMsg("ChangeTimeCommon", err);
    } 
}

//文本框显示或清空的复选框的OnChange事件处理函数
function ChangeTextCommon(obj,objText)
{
    try
    {
        var txtValue=document.getElementById(objText);
        if(obj.checked)
        {
            txtValue.disabled = false;
            txtValue.value = txtValue.valueBackup != null ? txtValue.valueBackup : "" ;
        }
        else
        {
            txtValue.disabled = true;
            txtValue.valueBackup = txtValue.value;
            txtValue.value = "";
        }
        changeOnlyStyle(obj.checked, txtValue);
    }
    catch(err)
    {
        displayErrorMsg("ChangeTextCommon", err);
    }
}

//供以上调用,改变文本框样式
function changeOnlyStyle(check, obj)
{
    if (!check)
    {
        obj.style.borderWidth = 0;
        obj.style.borderBottomWidth = 1;
        obj.style.borderColor = "#BBBBBB";
    }
    else
    {
        obj.style.borderWidth = 1;
        obj.style.borderColor = "#3A6AB0";
    }
}


var isIE = navigator.appName.indexOf("Microsoft") != -1;

//功能：获得不同IE版本的事件处理源
//evt：事件处理源
function getEvent(evt) 
{
    try
    {
        if (isIE) return window.event;
        return evt;
    }
    catch(err)
    {
        displayErrorMsg("getEvent", err);
    }
}

//功能：事件不做处理
//evt：事件
function doNothing(evt) 
{
    try
    {
        evt = getEvent(evt);
        evt.cancelBubble = true;
    }
    catch(err)
    {
        displayErrorMsg("doNothing", err);
    }
    
}

      
//功能：国家与省份二级级联，在用到国家省份级联的页面<body onload>中加入该函数即可
function initNationEvent(strNationID, ctrProviceID)
{
    try
    {
        var ddlNation = document.getElementById(strNationID);
        var ddlProvince = document.getElementById(ctrProviceID);
        if (ddlNation != null && ddlNation.type == "select-one" && ddlProvince != null && ddlProvince.type == "select-one")
        {
            document.getElementById(strNationID).onchange = function (){handleNationChange(strNationID, ctrProviceID)}; 
            var ddlProvince = document.getElementById(ctrProviceID);
            var selectedValue = "";
            if (ddlProvince.options.length > 0)
            {
                selectedValue = ddlProvince.value;
            }
            document.getElementById(strNationID).onchange();
            ddlProvince.value = selectedValue;
        }
    }
    catch(err)
    {
        displayErrorMsg("initNationEvent", err);
    } 
}

//功能：国家下拉框的onchange事件处理,供上面函数调用
function handleNationChange(ctrNationID, ctrProviceID)
{
    try
    {
        var nationSelect = document.getElementById(ctrNationID);
        var provinceSelect = document.getElementById(ctrProviceID);
        if (nationSelect != null && provinceSelect != null && nationSelect.type == "select-one")
        {
            var nationID = nationSelect.value;
            
            //清除原省份下拉框信息
            provinceSelect.options.length = 0;
            
            if (nationID != "")
            {
                var provinceList = new Array();
                provinceList = SmartSoft.Presentation.BaseController.SelectProvincesByNationID(nationID).value;

                
                //重新获得并初始化
                provinceSelect.options[provinceSelect.options.length] = new Option("", ""); 
                for (var i = 0; i < provinceList.length; i++)
                {
                    var province = provinceList[i];
                    if (province != null)
                    {
                        provinceSelect.options[provinceSelect.options.length] = new Option(province.provProvinceName, province.provProvinceID); 
                    }
                }
            }
            
        }
    }
    catch(err)
    {
        displayErrorMsg("handleNationChange", err);
    } 
}


/****************************
*
*    主从表模式共用部分
*
****************************/

//功能：获得对象的X坐标
function getObjX(obj)
{
    if(!obj.offsetParent) return 0;
    var x = getObjX(obj.offsetParent);
    var scrollLeft = obj.scrollLeft;
    return obj.offsetLeft + x - scrollLeft;
}

//功能：获得对象的Y坐标
function getObjY(obj)
{
    if(!obj.offsetParent) return 0;
    var y = getObjY(obj.offsetParent);
    
    //考虑滚动条
    var scrollTop = obj.scrollTop;
    return obj.offsetTop + y - scrollTop;
}

//隐藏ID号为 elementID 的页面元素
function HideElementById(elementID)
{
    try
    {
    
        var element = GetElementById(elementID);
        if (element != null)
        {
            element.style.display = "none";
        }  
    }
    catch(err)
    {
        displayErrorMsg("HideElementById", err);
    }
}

//功能：当输入文本框获得焦点时且其对应的的下拉列表框有多项选择时显示下拉列表
function GetFocusEvent(listboxID, divID)
{
    try
    {
        var lb = GetElementById(listboxID);
        var divResult = GetElementById(divID);
        if (lb != null && lb.options.length > 0)
        {
            divResult.style.display = "";
        }   
    }
    catch(err)
    {
        displayErrorMsg("GetFocusEvent", err);
    }
}


//功能：从layoutID中获得其要格式化的字段的数据类型
function getHtDataTypeByLayout(layoutID)
{
    try
    {
        if (layoutID == null) return;
        
        var result = new Array();
        var columnName_DataType =  SmartSoft.Presentation.BaseController.GetColumnNameDataTypeByLayoutID(layoutID).value;
        for (var i = 0; i < columnName_DataType.length; i++)
        {
            var name_dataType = columnName_DataType[i].split("|");
            result[name_dataType[0]] = name_dataType[1];
        }
        return result;
    }
    catch(err)
    {
        displayErrorMsg("getHtDataTypeByLayout", err);
    }
}

//从页面获得元素对象
var profit = ""; 
function GetElementById(elementID)
{
    return document.getElementById(profit + elementID);
}

function SetControlReadonly(elementID)
{
    var e = GetElementById(elementID);
    
    if (e != null)
    {
        e.readOnly = "true";
        e["class"] = "ReadOnly";
    }
}
//功能：显示隐藏布局部分
//tcObj布局部分的标题容器
function showHideLayoutPart(tcObj)
{
    try
    {
        var trObj = tcObj.parentElement;
        var tableObj = trObj.parentElement;
        
        var rowCount = tableObj.rows.length;

        //隐藏
        if (tcObj.className == "TableHeader")
        {
            if (rowCount > 1)
            {
                for (var i = 1; i < rowCount; i++)
                {
                    tableObj.rows[i].style.display = "none";
                }  
            } 
            
            tcObj.className = "TableHeaderHidden";
        }
        else //展开
        {
            if (rowCount > 1)
            {
                for (var i = 1; i < rowCount; i++)
                {
                    tableObj.rows[i].style.display = "";
                }  
            } 
            
            tcObj.className = "TableHeader";
        }
    }
    catch(err)
    {
        displayErrorMsg("showHideLayoutPart", err);
    }
    
}

//把标签类型的控件转换成文本框控件
function ConvertToInput(controlID)
{
    var element = GetElementById(controlID);
    if (element != null)
    {
        if (element.tagName == "SPAN")
        {
            var newTextElement = document.createElement("input");
            newTextElement.type = "input";
            newTextElement.id = element.id;
            newTextElement.style.width = element.style.width;
            newTextElement.value = GetValue(element);
            element.replaceNode(newTextElement);
        }
    }
}


    


//预览文档
function ViewDocument(tableID, rowID)
{
    try
    {
        var url = "../File/CommonFile.aspx?tableid=" + tableID + "&rowid=" + rowID;
        openwinsimp(url, 800, 600);
    }
    catch(err)
    {
        displayErrorMsg("ViewDocument", err);
    }
}


//<![CDATA[
function ShowEditTab(EditTabIDNum,EditTabNum)
{
	
    for(var i=0;i<2;i++)
    {
        document.getElementById("EditTabContent_"+EditTabIDNum+i).style.display="none";
		
    }
    for(var i=0;i<2;i++)
    {
        document.getElementById("EditTabMenu_"+EditTabIDNum+i).className="EditTabSpanOff";
    }
    document.getElementById("EditTabMenu_"+EditTabIDNum+EditTabNum).className="EditTabSpan";
    document.getElementById("EditTabContent_"+EditTabIDNum+EditTabNum).style.display="block";	
}	

function ShowEditAddressTab(EditTabIDNum,EditTabNum)
{
	
    for(var i=0;i<3;i++)
    {
        document.getElementById("EditTabContent_"+EditTabIDNum+i).style.display="none";
		
    }
    for(var i=0;i<3;i++)
    {
        document.getElementById("EditTabMenu_"+EditTabIDNum+i).className="EditTabSpanOff";
    }
    document.getElementById("EditTabMenu_"+EditTabIDNum+EditTabNum).className="EditTabSpan";
    document.getElementById("EditTabContent_"+EditTabIDNum+EditTabNum).style.display="block";	
}
//]]>


//触发复选框单击事件,
//在查询列表中，使日期初始化为非选择状态时用
function ForceCheckBoxClick(cbID)
{
    try
    {
        var cbObj = document.getElementById(cbID);   
        if (cbObj != null)
        {
            cbObj.click();
            cbObj.checked = false;
        }
    }
    catch(err)
    {
    }
}

//按钮权限控制

function SetNonePurview( funName )
{
	for( i = 0; i < document.getElementsByTagName("A").length; i++)
	{
	    var sID = document.getElementsByTagName("A").item(i).id;
	    var nIndex  = sID.indexOf(funName);
		if(nIndex >= 0 && sID.substring(nIndex-1, nIndex) == "_")
		{
		    document.getElementsByTagName("A").item(i).style.display = "none";
			document.getElementsByTagName("A").item(i).disabled = true;
			document.getElementsByTagName("A").item(i).onclick = "";
			document.getElementsByTagName("A").item(i).removeAttribute("href");
		}
	}
	
	for( i = 0; i < document.getElementsByTagName("INPUT").length; i++)
	{
	    var sID = document.getElementsByTagName("INPUT").item(i).id;
	    var nIndex  = sID.indexOf(funName);
		if(nIndex >= 0 && sID.substring(nIndex-1, nIndex) == "_")
		{
		    document.getElementsByTagName("INPUT").item(i).style.display = "none";
			document.getElementsByTagName("INPUT").item(i).disabled = true;
			document.getElementsByTagName("INPUT").item(i).onclick = "";
		}
	}
}

//当前操作员ID
var currentOperatorID = 0;
//当前布局
var currentLayoutID = 0;
function SetCurrentOperatorAndLayout(opeID, layoutID)
{
    currentOperatorID = opeID;
    currentLayoutID = layoutID;
}

var unitCommon = 0;
var unitPrice = 0;
var unitQty = 0;
var unitAmount;
function SetUnit(common, price, qty, amount)
{
    unitCommon =  common;
    unitPrice = price;
    unitQty = qty;
    unitAmount = amount;
}
//设置列表页面中包含GridView的div的属性，使其支持滚动
function SetGridViewScrolling(divID)
{   
    if (divID == null || divID == "")
    {
        divID = "up1";
    }
    var divElement = document.getElementById(divID);
    if (divElement != null) {
        var h = getContainerHeight();
       
        divElement.style.height = h;
        divElement.style.width = "100%";
        divElement.style.overflow = "auto";

    }

    /*var height = parseInt($(".sData").css("height").replace("px", "")) + 65;
    $(".sData").css("height", height + "px");
    alert($(".sData").css("height"));*/
}

function SetGridViewScroll(divID)
{   
    if (divID == null || divID == "")
    {
        divID = "up1";
    }
    var divElement = document.getElementById(divID);
    if (divElement != null)
    {
        divElement.style.height = "280px";
        divElement.style.width = "100%";
        divElement.style.overflow = "auto";
        
    }
}

function SetGridViewNoScroll(divID)
{  
    if (divID == null || divID == "")
    {
        divID = "up1";
    }
    var divElement = document.getElementById(divID);
    if (divElement != null)
    {
        divElement.style.overflow = "visible";
        
    }
}

function getContainerWidth()
{
    var divWidth = 0;
    divWidth = document.body.clientWidth - 5;
    
    return divWidth;
}

//获取表格的高
function getContainerHeight() {
    var height = 0;
    height = document.body.clientHeight - 30;
    var tableSearch = document.getElementById("TableSearch");
    if (tableSearch != null) {
        height = height - tableSearch.clientHeight;
       
    }

    return height;
}

function InitListControlDataSource(ctrObj, dataList, dataTextField, dataValueField, isInsertBlank)
{
    try
    {  
        if (ctrObj["options"] != null)
        {
            ctrObj.options.length = 0;
            if (isInsertBlank)
            {
                ctrObj.options[0] = new Option("", ""); 
            }
            
            for (var i = 0; i < dataList.length; i++)
            {
                dataObj = dataList[i];
                ctrObj.options[ctrObj.options.length] = new Option(dataObj[dataTextField], dataObj[dataValueField]); 
            }
        }
        else
        {
            alert("not listcontrol!please check!");
        }
    }
    catch(err)
    {
        displayErrorMsg("InitListControlDataSource", err);
    }
}



//功能：追加事件
//obj:对象实体
//strEventName:对象的事件属性名称如onclick
//strFunc:追加的事件如 myFunction('1', '2');
function AttachFunction(obj, strEventName, strFunc)
{
    try
    {
        var prefunction = "";
        if (obj[strEventName] != null)
        {
           prefunction = obj[strEventName].toString();
           if (prefunction.indexOf("function anonymous()") > -1)
           {
                prefunction = prefunction.replace("function anonymous()", "");
           }
           else if (prefunction.indexOf("function()") > -1 || prefunction.indexOf("function ()"))
           {
                prefunction = prefunction.replace("function()", "");
                prefunction = prefunction.replace("function ()", "");
           }
           //prefunction = prefunction.substring(1, prefunction.length - 1);
        }
        var codeString = "obj[strEventName]= function(){ " + prefunction + strFunc + "}";
        eval(codeString);
    }
    catch(err)
    {
        displayErrorMsg("AttachFunction", err);
    }
}

function setSumRowPosition(rowElement)
{ 
    try
    {
        divElement = GetElementById("up1");
        if (divElement != null)
        {
            rowElement.top = divElement.top + divElement.height;
        }
    }
    catch(err)
    {
        displayErrorMsg("setSumRowPosition", err);
    }
}

function ControlColumnLength(txtObj, columnLength)
{
    try
    {
        if (getTextValueLength(txtObj.value) > columnLength)
        {
            txtObj.value = txtObj.value.substring(0, columnLength);
        }
        
        while(getTextValueLength(txtObj.value) > columnLength)
        {
            txtObj.value = txtObj.value.substring(0, txtObj.value.length-1);
        }
    }
    catch(err)
    {
        displayErrorMsg("setSumRowPosition", err);
    }
}

function getTextValueLength(str)
{
    return str.replace(/[^\x00-\xff]/g,"**").length;   
}

  
function disableOtherSubmit()
{   
    var obj = event.srcElement;
    var objs = document.getElementsByTagName('A');
    for(var i=0; i<objs.length; i++)
    {
        objs[i].disabled = true;
        objs[i].onclick = function(){return false;};
    }
}

function ConvertToLinkCommon(gridID, keyword) {
    var table = $("#" + gridID, ".sData").get(0);
    if (table != null && table.rows.length > 1) {
        var customerNameIndex = 0;
        for (var i = 0; i < table.rows[0].cells.length; i++) {
            if (table.rows[0].cells[i].innerText.indexOf(keyword) > -1) {
                customerNameIndex = i;
            }
        }

        for (var i = 1; i < table.rows.length; i++) {

            var row = table.rows[i];
            var tdCustomerName = row.cells[customerNameIndex].innerText;
            var td = row.cells[0];
            var checkbox = td.childNodes[0];
            var url = EditFormUrl + "?Action=View&ID=";
            if (EditFormUrl.indexOf("?") > -1) {
                url = EditFormUrl + "&Action=View&ID=";
            }
            url += checkbox.value;

            row.cells[customerNameIndex].innerHTML = "<a href=\"javascript:OpenEditForm('" + url + "',1000,700)\">" + tdCustomerName + "</a>";
        }
    }
}
