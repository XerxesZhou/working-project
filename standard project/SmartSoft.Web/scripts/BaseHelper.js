/**********************************

客户端脚本公共方法,用于所有页面

**********************************/

Date.prototype.Format = function (fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function ChangedWithThousandTag(number) {
    try {
        var result = ""
        var number = number.toString();
        var paras = number.split(".");
        if (paras.length > 1) {
            var integerPart = paras[0];
            var sectionPart = paras[1];
            result = commafy(integerPart).toString() + "." + sectionPart.toString();
        }
        else {
            result = commafy(number);
        }

        return result;
    }
    catch (err) {
        displayErrorMsg("ChangedWithThousandTag", err);
    }
}

function commafy(num) {
    num = num + "";
    var re = /(-?\d+)(\d{3})/
    while (re.test(num)) {
        num = num.replace(re, "$1,$2")
    }

    return num;
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

$(document).ready(function () { $('.inputDate').click(function () { laydate({ istime: false, format: 'YYYY-MM-DD' }); }) });

$(document).ready(function () { $('.inputDate2').click(function () { laydate({ istime: true, format: 'YYYY-MM-DD hh:mm' }); }) });

$(document).ready(function () { $('.inputDate').each(function () { $(this).attr("class", $(this).attr("class") + " laydate-icon"); }) });

$(document).ready(function () { $('.inputDate2').each(function () { $(this).attr("class", $(this).attr("class") + " laydate-icon"); }) });

$(document).ready(function () { $('.inputNumber2').keyup(function () { controlNumberOnKeyUp(this); }).blur(function () { controlNumberOnblur(this, 3) }).keypress(function () { controlNumberKeyPress(this) }); })

$(document).ready(function () { $('.inputNumber').keyup(function () { controlNumberOnKeyUp(this); }).blur(function () { controlNumberOnblur(this, 0) }).keypress(function () { controlNumberKeyPress(this) }); })

$(document).ready(function () { $('.inputNumber4').keyup(function () { controlNumberOnKeyUp(this); }).blur(function () { controlNumberOnblur(this, 4) }).keypress(function () { controlNumberKeyPress(this) }); })

