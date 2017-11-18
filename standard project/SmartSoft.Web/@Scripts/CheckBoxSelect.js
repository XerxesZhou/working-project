/************************************************************
* 
*  用于列表页面中及主从表模式中的主表编辑页面
*
*************************************************************/
var sPrefit = "Field";
//编辑窗口
var editFormObj = null;
function OpenEditForm(url, width, height)
{
    //打开窗口的外观
    width = width == null ? window.screen.availWidth * 0.95 : width;
    height = height == null ? window.screen.availHeight * 0.9 : height;
    var sEditFormFeatures = "width=" + width +",height=" + height+ ",location=no, resizable=yes, scrollbars=yes, status=yes"; 
    if (top.editFormObj != null)
        top.editFormObj.close();
    if (editFormObj != null)
    {
        editFormObj.close();
    }
    //top.editFormObj = window.open(url, "rform", sEditFormFeatures, "");
    //editFormObj = opendialog(url, width, height);
    editFormObj = openwinsimpscroll(url, width, height);
}

function moveToCenter(width, height)
{
    width = width == null ? window.screen.availWidth * 0.95 : width;
    height = height == null ? window.screen.availHeight * 0.9 : height;
    var top  = (window.screen.availHeight - height) / 2;
    var left = (window.screen.availWidth - width) / 2;
    if (editFormObj != null)
        editFormObj.moveTo(top, left);
}

//全局变量，把已选的值保存起来,供后面的事件处理
var selectCheck = new Array();
Array.prototype.Remove = function(dx)
{
    if(isNaN(dx)||dx>this.length)
    {return false;}
    this.splice(dx,1);
}
Array.prototype.FindCheck = function(value)
{
    for ( var i = 0 ; i < this.length ; ++i ) 
    {
        if (this[i] == value)
        {
            return i;
        }
    }
    return -1;
}

function SetSelectCheck(obj, selectArray)
{
    var iFindIndex = selectArray.FindCheck(obj.value);
    if (obj.checked == true)
    {
        if (iFindIndex == -1)
        {
            selectArray.push(obj.value);
        }
    }
    else
    {
        if (iFindIndex != -1)
        {
            selectArray.Remove(iFindIndex);
        }
    }
    //把所选的设置到隐藏域中
    setSelectedToHiddenField(this.selectCheck);
}

//将所有已选的选项加入selectArray
//在全选按钮事件中调用
function setSelectArray()
{
    this.selectCheck = new Array();

    if (document.all.checkboxname != null)
    {    
        
        if(document.all.checkboxname.length > 1)
        {
            for(var i = 0; i < document.all.checkboxname.length; i++)
            {
                if(document.all.checkboxname[i].checked)
                {
                    this.selectCheck.push(document.all.checkboxname[i].value);
                }
            }
        }
        else
        {
            if(document.all.checkboxname.checked)
                this.selectCheck.push(document.all.checkboxname.value);
        }
    }
    
    //alert(this.selectCheck.length);
}

// 多选的全选与取消
function checkJs(boolvalue)
{
    try
    {
        if (document.all.checkboxname != null)
        {
            if(document.all.checkboxname.length>1)
            {
                for(var i=0;i<document.all.checkboxname.length;i++)
                {
                    document.all.checkboxname[i].checked = boolvalue;          
                }
            }
            else
            {
                document.all.checkboxname.checked = boolvalue;
            }
        }
        
        setSelectArray();
        
        //选择全部时设置行样式相关
        selectAll(boolvalue);
        
            //把所选的设置到隐藏域中
        setSelectedToHiddenField(this.selectCheck);
    }
    catch(err)
    {
        displayErrorMsg("checkJs", err)
    }
}


//设置选中时的样式
function setSelectedStyle(obj)
{
    obj.style.backgroundColor="LightBlue";                            
}

//恢复初始样式
function resumeStyle(obj)
{
    obj.style.backgroundColor = obj["oldBackgroundColor"];
}

//保存初始样式
function saveOldStyle(obj)
{
    if (obj["oldBackgroundColor"] == null)
    {
        obj["oldBackgroundColor"] = obj.style.backgroundColor;
    }
}

function selectAll(boolvalue)
{
    if (document.all.checkboxname != null)
    {
        //alert(document.all.checkboxname.length);
        for (var i = 0; i < document.all.checkboxname.length; i++)
        {
            var trObj = document.all.checkboxname[i].parentNode.parentNode;
            saveOldStyle(trObj);
            if (boolvalue)
            {
                setSelectedStyle(trObj);
            }
            else
            {
                resumeStyle(trObj);
            }
        }
    }
}

//行上双击处理事件
function dbclick(obj, action)
{
    try
    {
        var td = obj.cells[0];
        var checkbox = td.childNodes[0];

        var url = EditFormUrl + "?Action=" + action + "&ID=";
        if (EditFormUrl.indexOf("?") > -1) {
            url = EditFormUrl + "&Action=" + action + "&ID=";
        }
        url += checkbox.value;
        OpenEditForm(url);
    }
    catch(err)
    {
        displayErrorMsg("dbclick", err)
    }
}

//在复选框上单击事件处理
function OnCheck(obj)
{
    SetSelectCheck(obj, this.selectCheck);
}  

// 改变行的颜色
if (!objbeforeItem)
{
    var objbeforeItem=null;
    var objbeforeItembackgroundColor=null;
}

//从行上穿过的事件处理
function ItemOver(obj)
{
    //保存原来的样式
    saveOldStyle(obj);
    
    objbeforeItembackgroundColor=obj.style.backgroundColor;
    obj.style.backgroundColor="#B9D1F3";   
    obj.style.cursor = "pointer";                                    
    objbeforeItem=obj;
}

//从行上穿出的事件处理
function ItemOut(obj)
{            
    if(objbeforeItem)
    {
        objbeforeItem.style.backgroundColor=objbeforeItembackgroundColor;
    }
    
    //设置背景
    if (obj.cells[0].childNodes[0].checked)
    {
        setSelectedStyle(obj);
    }
    else
    {
        resumeStyle(obj);
    }
}


//在行上单击
function Onclick(obj)
{
    try
    {
        var td = obj.cells[0];
        var checkbox = td.childNodes[0];
        if(checkbox.checked)
        {
            checkbox.checked = false;
        }
        else
        {
            checkbox.checked = true;
        }
        OnCheck(checkbox);
    }
    catch(err)
    {
        displayErrorMsg("Onclick", err);
    }
}

//打开一个记录
function CheckBoxLocationHandle(url, message, selectArray)
{
    try
    {
        if(selectArray.length == 1)
        {
            url += selectArray[0];
            OpenEditForm(url);
        }
        else
        {
            alert(message);
        }
    }
    catch(err)
    {
        displayErrorMsg("CheckBoxLocationHandle", err)
    }
}

//可打设置多个数据到hfSelectCheck隐藏框中
function CheckBoxMultiHandle(message, selectArray)
{
    try
    {
        if(selectArray.length > 0)
        {
            setSelectedToHiddenField(selectArray);

            return confirm(message);
        }
        else
        {
            alert("无选择记录!");
            return false;
        }
        return true;
    }
    catch(err)
    {
        displayErrorMsg("CheckBoxMultiHandle", err)
    }
}

function setSelectedToHiddenField(selectArray)
{
    try
    {
        //把数组元素用逗号分隔后返回，如果数组为空，则返回空字符串
        SetValue(GetElementById("hfSelectCheck"), selectArray.join(","));
    }
    catch(err)
    {
        displayErrorMsg("setSelectedToHiddenField", err);
    }

}

//新增
function Insert()
{
    try
    {
        var url = EditFormUrl + "?Action=Insert";
        OpenEditForm(url);
    }
    catch(err)
    {
        displayErrorMsg("Insert", err);
    }
}

//修改
function Update()
{
    try
    {
        var url = EditFormUrl + "?";
        url += "Action=Update&ID=";
        CheckBoxLocationHandle(url,"请选择一条需要修改的记录!", this.selectCheck);
    }
    catch(err)
    {
        displayErrorMsg("Update", err)
    }
}

//写入货品对照
function WriteCustomerProduct()
{
    var selectArray = this.selectCheck;
    if(selectArray.length > 0)
    {
        setSelectedToHiddenField(selectArray);
        GetElementById("btnWriteCustomerProduct").click();
        this.selectCheck = new Array();
    }
    else
    {
        alert("无选择记录!");
    }
}

//加入库存下限
function AddStockLowerLimit()
{
    var selectArray = this.selectCheck;
    if(selectArray.length > 0)
    {
        setSelectedToHiddenField(selectArray);
        GetElementById("btnAddStockLowerLimit").click();
        this.selectCheck = new Array();
    }
    else
    {
        alert("无选择记录!");
    }
}

function Copy()
{
    try
    {
        var url = CopyFormUrl + "?";
        url += "Action=Copy&ID=" + getUrlParameter("ID");
        
        //OpenEditForm(url);
        //window.close();
        window.location.href = url;
    }
    catch(err)
    {
        displayErrorMsg("Copy", err)
    }
}

//显示
function View()
{
    try
    {
        var url = EditFormUrl + "?Action=View&ID=";
        CheckBoxLocationHandle(url,"请选择一条需要显示的记录!", this.selectCheck);
    }
    catch(err)
    {
        displayErrorMsg("View", err)
    }
}

//删除
function Delete()
{   
    return CheckBoxMultiHandle('确定要删除吗？\r\n删除后将不可恢复!', this.selectCheck);
}

function SaveToDoWriteOff()
{
    try
    {
        if(this.selectCheck.length > 0)
        {
            setSelectedToHiddenField(this.selectCheck);
        }
        else
        {
            alert("无选择记录!");
            return false;
        }
        return true;
    }
    catch(err)
    {
        displayErrorMsg("SaveToDoWriteOff", err)
    }
}


//恢复
function Resume()
{
    return CheckBoxMultiHandle('确定要恢复吗？', this.selectCheck);
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

var searchFlag = "";
//设置查看及查看所有标志
function SetSearchFlag(flag)
{
    searchFlag = flag;
}

//清除所选
function ClearSelected()
{
    this.selectCheck = new Array();
}

//功能：显示隐藏查询条件
function showHideSearchCondition(obj)
{
    var searchMain = document.getElementById("searchMain");
    var searchHeader = document.getElementById("searchHeader");
    var searchHeaderText = document.getElementById("searchHeaderText");
    
    if (searchMain.style.display == "none")
    {
        searchMain.style.display = "";
        searchHeader.title = "隐藏查询条件";
        searchHeaderText.innerText = "[点击隐藏]";
    }
    else
    {
        searchMain.style.display = "none";
        searchHeader.title = "显示查询条件";
        searchHeaderText.innerText = "[点击显示]";
    }

    setGridViewHeight("up1");
}

//设置列表页面中包含GridView的div的高
function setGridViewHeight(divID)
{   
    try
    {
        if (divID == null || divID == "")
        {
            divID = "up1";
        }
        var divElement = document.getElementById(divID);
        var tableSearch = document.getElementById("TableSearch");
        var searchMain = document.getElementById("searchMain");
        if (tableSearch != null && searchMain != null && divElement != null) {
            var h = divElement.style.height.toString().replace("px", "") - 0;
          
            var xh = (tableSearch.rows.length - 1) * (tableSearch.rows[0].clientHeight);
         
            if (searchMain.style.display == "none") {
                divElement.style.height = (h + xh) + "px";
                $(".sData").parent().parent().css("height", (h + xh - 20) + "px");
                $(".sData").css("height", (h + xh - 50) + "px");
            }
            else {
                divElement.style.height = (h - xh) + "px";
                $(".sData").parent().parent().css("height", (h - xh - 20) + "px");
                $(".sData").css("height", (h - xh - 50) + "px");
            }
        }
        else {
            alert("a");
        }

    }
    catch(err)
    {
    }
}

function BackToList()
{
    //history.go(-1);
    window.close();
}