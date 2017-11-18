
// 多选的全选与取消
function checkJs(boolvalue)
{
    if (document.all.checkboxname != null)
    {
        if(document.all.checkboxname.length>1)
        {
            for(var i=0;i<document.all.checkboxname.length;i++)
            {
                document.all.checkboxname[i].checked = boolvalue;
                var obj = $(document.all.checkboxname[i]).parent().parent()[0];
                if (boolvalue) {
                    obj.style.backgroundColor = "LightBlue";
                }
                else {
                    obj.style.backgroundColor = null;
                }        
            }
        }
        else
        {
            document.all.checkboxname.checked = boolvalue;
        }
    }
}
//

// 只有全部选中时“全选”选中
function SingleCheckJs(obj)
{
    //if (this.obj.checked)
    var flag1=false;
    var flag2=false;
    
    if (document.form1.checkboxname.length)
    {
        for (var i=0;i<document.form1.checkboxname.length;i++)
        {
            if(document.form1.checkboxname[i].checked)
                flag1 = true;
            else
                flag2 = true;
        }
    }
    else
    {
        if(document.form1.checkboxname.checked)
            flag1 = true;
        else
            flag2 = true;
    }
    
    if(flag1==true&&flag2==false)
        document.getElementById("chk").checked = true;
    else
        document.getElementById("chk").checked = false;
    if (window.event) {
        window.event.cancelBubble = true;
    }
    else {
        event.cancelBubble = true;
    }
}  

// 改变行的颜色
if (!objbeforeItem)
{
    var objbeforeItem=null;
    var objbeforeItembackgroundColor=null;
}

function ItemOver(obj)
{
    objbeforeItembackgroundColor=obj.style.backgroundColor;
    obj.style.backgroundColor="#B9D1F3";   
    obj.style.cursor = "pointer";                                    
    objbeforeItem=obj;
}
    
function ItemOut(obj)
{            
    if(objbeforeItem)
    {
        objbeforeItem.style.backgroundColor=objbeforeItembackgroundColor;
    }

    //设置背景
    var checkbox = $("input", $(obj)).get(0);
    if (checkbox.checked) {
        obj.style.backgroundColor = "LightBlue";  
    }
    else {
        obj.style.backgroundColor = null;
    }
}

function Onclick(obj) {
    var tr = obj;
    var td = tr.cells[0];
    var checkbox = $("input", $(td)).get(0);
    if(checkbox.checked)
    {
        checkbox.checked = false;
        obj.style.backgroundColor = null ;   
    }
    else
    {
        checkbox.checked = true;
        obj.style.backgroundColor = "LightBlue";   
    }
}

function CheckBoxOpenWindowHandle(Url, width, Height, alertMsg)
{
    var arr = new Array();
    if(document.all.checkboxname != null)
    {
        if(document.all.checkboxname.length>1)
        {
            var j =0;
            for(var i=0;i<document.all.checkboxname.length;i++)
            {
                if(document.all.checkboxname[i].checked)
                {
                    arr[j] =  document.all.checkboxname[i].value;  
                    j++;
                   
                }
            }
             
        }
        else
        {
            if(document.all.checkboxname.checked)
                arr[0] =  document.all.checkboxname.value;  
        }
        
        if(arr.length == 1)
        {
//            if(arr[0] == "0")
//            {
//                alert(alertMsg);
//                return false;
//            }

            openwinsimpscroll(Url + arr[0],width,Height);
        }
        else
        {
            alert(alertMsg);
            return false;
        }
    }
    else
    {
        alert(alertMsg);
    }
}

function CheckBoxLocationHandle(Url, alertMsg)
{
    var arr = new Array();
    if(document.all.checkboxname.length>1)
    {
        var j =0;
        for(var i=0;i<document.all.checkboxname.length;i++)
        {
            if(document.all.checkboxname[i].checked)
            {
                arr[j] =  document.all.checkboxname[i].value;  
                j++;
            }
        }
    }
    else
    {
        if(document.all.checkboxname.checked)
            arr[0] =  document.all.checkboxname.value;  
    }
    
    
    if(arr.length == 1)
    {
        if(arr[0] == "0")
        {
            alert('无记录!');
            return false;
        }
        window.location = Url + arr[0];
    }
    else
    {
        alert(alertMsg);
        return false;
    }
}

function CheckBoxMultiHandle(alertMsg)
{
    try
    {
        var arr = new Array();
        if(document.all.checkboxname.length>1)
        {
            var j =0;
            for(var i=0;i<document.all.checkboxname.length;i++)
            {
                if(document.all.checkboxname[i].checked)
                {
                    arr[j] =  document.all.checkboxname[i].value;  
                    j++;
                }
            }
           
        }
        else
        {
            if(document.all.checkboxname.checked)
                arr[0] =  document.all.checkboxname.value;  
        }
        if(arr.length > 0)
        {
            return confirm(alertMsg);
        }
        else
        {
            alert('请选择记录!');
            return false;
        }
    }
    catch(err)
    {
        displayErrorMsg("CheckBoxMultiHandle", err)
    }
}