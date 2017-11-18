var EditFormUrl = "SellOrderItemEditForm.aspx";

/************************************************************
* 
*  主从表模式中的主表的特效实现脚本
*
*************************************************************/

//共用全局变量
var htDataType = new Array();           //用于客户端格式化

var txtPrimaryIDField_id = "";          //”客户ID“或者"供应商ID" 文本框的控件编号 
var txtNameField_id = "FieldcusName"; //”客户名称“文本框的控件编号
var txtMainAmountField_id = "";         //"货品总金额" 文本框控件的编号
var lbName_id = "lbName";               //”名称“下拉框的控件编号
var divName_id = "ShowNameResult";      //”名称“div的控件编号


var openWidth = 600;
var openHeight = 300; 
//用窗口打开一个记录
function checkBoxOpenItemHandle(url, message, width, height, selectArray)
{
    try
    {
        //新增
        if (url.indexOf("Action=Insert") > -1) 
        {
            openwinsimpscroll(url,width,height);
        }
        else if(selectArray.length == 1)    //修改和查看
        {
            url += "&lineNO=" + selectArray[0];
            GetElementById("lineNO").value = selectArray[0];
            GetElementById("btnSetItemDetail").click();
            openwinsimpscroll(url,width,height);
        }   
        else 
        {
            alert(message);
        }
    }
    catch(err)
    {
        displayErrorMsg("checkBoxOpenItemHandle", err)
    }
}

//编辑子表的公用函数
function EditItemCommon(strFieldID, action)
{ 
    try
    {
        var strUrl = getStandItemUrl(strFieldID, action);
        checkBoxOpenItemHandle(strUrl,"请选择一条需要修改的记录!", openWidth, openHeight,  this.selectCheck);
    }
    catch(err)
    {
        displayErrorMsg("EditItemCommon", err);
    }
}

function getStandItemUrl(strFieldID, action)
{
    var strUrl = EditFormUrl + "?ID=" + getUrlParameter("ID");
    
    if (strFieldID != null && strFieldID != "")
    {
        var mainIDValue = GetValue(GetElementById(strFieldID));
        strUrl += "&CustomerID=" + mainIDValue;
    }    
    if (action != null && action != "")
    {
        strUrl += "&Action=" + action;
    }

    return strUrl;
}

//查看子表
function View()
{
    EditItemCommon(txtPrimaryIDField_id, "View");
}


//修改子表
function Update()
{
     EditItemCommon(txtPrimaryIDField_id, "Update");
     this.selectCheck = new Array();
}

//新增子表
function Insert()
{
    EditItemCommon(txtPrimaryIDField_id, "Insert");
    this.selectCheck = new Array();
}

//删除子表
function Delete()
{
    if(CheckBoxMultiHandle('确定要删除吗？\r\n删除后将不可恢复!', this.selectCheck))
    {
        GetElementById("btnDeleteLine").click();
        this.selectCheck = new Array();
    }
}

//子表双击事件处理
function dbclick(obj, action)
{
    try
    {
        var td = obj.cells[0];
        var checkbox = td.childNodes[0];
        
        var url = EditFormUrl + "?Action=" + action + "&ID=" + checkbox.value;
        GetElementById("lineNO").value = checkbox.value;
        GetElementById("btnSetItemDetail").click();
        openwinsimpscroll(url, openWidth, openHeight);
    }
    catch(err)
    {
        displayErrorMsg("dbclick", err)
    }
}

//根据从子表里反馈回来的信息设置并更新主表内容
//在子表的保存事件里调用
function SetItem(itemDetail)
{
    try
    {
        GetElementById("itemDetail").value = itemDetail;
        GetElementById("btnRefresh").click();
    }
    catch(err)
    {
        displayErrorMsg("SetItem", err);
    }
}


//设置列表页面中包含GridView的div的属性，使其支持滚动
function SetGridViewScrolling(divID)
{   
    try
    {
        var height = "560px";
        document.body.style.overflow = "visible";
        if (divID == null || divID == "")
        {
            divID = "up1";
        }
        var divElement = document.getElementById(divID);
        if (divElement != null)
        {
            divElement.style.height = height;
            divElement.style.width = "1000px";
            divElement.style.overflow = "auto";
        }
    }
    catch(err)
    {
    }
}


//功能：主记录详细中的onload函数
function LoadEventPrimary1()
{
    try
    {  
        
        var action = getUrlParameter("Action");
        
        //查看模式时可链接到详细信息
        if (action == "View")
        {
            return;
        }
        else
        {
            var customerid = getUrlParameter("CustomerID");
            if (customerid != null && customerid > 0)
            {
                return;
            }
        }
        ConvertToInput(txtNameField_id);
        //初始化特效控件的事件
        
        initEnventHandle();
        var IDText = GetElementById(txtPrimaryIDField_id);
        var nameText = GetElementById(txtNameField_id);
        //绑定外键实体及自定义信息
        var ctrValue = GetValue(IDText);
        if (IDText != null && ctrValue != "")
        {
            setSelectOnLoad(ctrValue);
        } 
        //设置主表总金额控件为只读文本框
        SetControlReadonly(txtMainAmountField_id);
    }
    catch(err)
    {
        displayErrorMsg("LoadEventPrimary", err);
    }

}

function LoadEventPrimary()
{
    SetGridViewScrolling();
    LoadEventPrimary1();
}

//根据名称获得客户或者供应商的资料，以ID|Name形式的字符串数组返回
var arrayIDNameList = new Array();
function getAllIDAndNameList()
{
    if (arrayIDNameList == null || arrayIDNameList.length == 0)
    {
        arrayIDNameList = SmartSoft.Presentation.BaseController.GetCustIDAndCustNameListAll(currentOperatorID).value;
    } 
    return arrayIDNameList;
}
var maxReturned = 100;
function getIDAndNameList(strName)
{
    try
    {
        getAllIDAndNameList();
        var arrayResult = new Array();
        var gotCount = 0;
        if (arrayIDNameList != null)
        {
            for (var i = 0; i < arrayIDNameList.length; i++)
            {
                var details = arrayIDNameList[i].toString().split("|");
                if (details.length == 2)
                {
                    if (details[1].indexOf(strName) != -1)
                    {
                        arrayResult.push(arrayIDNameList[i]);
                        gotCount++;
                    }
                    if (gotCount >= maxReturned)
                    {
                        break;
                    }
                }
            }
        }
        
        return arrayResult;
    }
    catch(err)
    {
        displayErrorMsg("getIDAndNameList", err);
    }
}

function SearchName()
{
    SearchNameHere();
}

//功能：通过名称获得相关信息，在名称输入文本框的keyup事件中调用
function SearchNameHere()
{
    try
    {
        //获得页面上相关控件
        var nKeyCode = event.keyCode;
        var divResult = GetElementById(divName_id);
        var nameText = GetElementById(txtNameField_id);
        var IDText = GetElementById(txtPrimaryIDField_id);
        var lbName = GetElementById(lbName_id);
        //作为主键的编码如
        var primaryID = GetValue(lbName);
        var strName = GetValue(nameText);
        
        //回车时的处理
        if (nKeyCode == 13) 
        {
            SetSelectName(lbName);
        }
        else if (nKeyCode == 38 || nKeyCode == 40)    //处理向上向下方向键
        {
            divResult.style.display = "";  
            var selectedIndex = lbName.selectedIndex;
            var optionLength = lbName.options.length;
            if (lbName.options.length > 0 && lbName.selectedIndex > -1)
            {  
                selectedIndex = optionLength + selectedIndex;
                selectedIndex = nKeyCode == 38 ? selectedIndex - 1 : selectedIndex + 1;
                selectedIndex = selectedIndex % optionLength;
                lbName.selectedIndex = selectedIndex;
                
                nameText.value = lbName.options[selectedIndex].text;
                SetValue(IDText, lbName.value);
            }
        }
        else
        {
            if (nameText.value.length > 0)    //输入字符事件处理
            {
                divResult.style.left = getObjX(nameText) + "px";
                divResult.style.top = (getObjY(nameText) + 20) + "px";

                lbName.options.length = 0;
                var selectsource = new Array();
                selectsource = getIDAndNameList(strName);
                
                if (selectsource == null || selectsource.length <= 0)   //如果下拉框里没有合适的选择项，则回退
                {
                    strName = strName.substring(0, strName.length-1);
                    SetValue(nameText, strName);  
                    selectsource = getIDAndNameList(strName);
                }
                divResult.style.display = "";   
                var i;
                var sValue;
                var sValueArray = new Array();
                
                var nTime = selectsource == null ? 0 : selectsource.length; 
                for (i = 0; i < nTime; i++)
                {
                    sValue = selectsource[i];
                    sValueArray = sValue.split("|");
                    lbName.options[i] = new Option(sValueArray[1], sValueArray[0]);
                }
                
                if (lbName.options.length > 0)
                {
                    lbName.selectedIndex = 0;
                }
                SetValue(IDText, lbName.value);  
                
            }
        }   
    }
    catch(err)
    {
        displayErrorMsg("SearchName", err);
    }
}

//功能：当选择名称下拉列表的项时赋值，在下拉列表的onclick事件中触发
function SetSelectName(sourceSelect)
{
    try
    {
        var nameText = GetElementById(txtNameField_id);
        var selectedIndex = sourceSelect.selectedIndex;
        if (selectedIndex > -1)
        {
            SetSelect(sourceSelect.options[selectedIndex].value); 
        }  
        HideName(); 
    }
    catch(err)
    {
        displayErrorMsg("SetSelectName", err);
    }
}


//设置供应商的采购员或者客户的业务员
function SetOperator(primaryID)
{
    try
    {
        var primaryEntity = getPrimaryEntity(primaryID);

        if (primaryEntity != null)
        {
            var ddlOperator = GetElementById(ddlOpeatorIDField_id);
            if (isCustomer)
            {
                SetValue(ddlOperator, primaryEntity.cusOperatorID);
            }
            else
            {
               SetValue(ddlOperator, primaryEntity.supOperatorID); 
            } 
        }
    }
    catch(err)
    {
        displayErrorMsg("SetOperator", err);
    }
}

//功能：根据选择ID设置相关信息,在下拉列表的onclick事件中触发
function SetSelect(primaryID)
{
    try
    {
        SetCustSelect(primaryID);
        var IDText = GetElementById(txtPrimaryIDField_id);
        SetValue(IDText, primaryID);
        
    }
    catch(err)
    {
        displayErrorMsg("SetSelect", err);
    }
}

//在LoadEventPrimary事件中调用
function setSelectOnLoad(primaryID)
{
    try
    {
        SetCustSelect(primaryID);
        var IDText = GetElementById(txtPrimaryIDField_id);
        SetValue(IDText, primaryID);
    }
    catch(err)
    {
        displayErrorMsg("SetSelectOnLoad", err);
    } 
}

//绑定客户ID为primaryID的客户信息
function SetCustSelect(primaryID)
{
    try
    {
        BindRecordToControlByTableName("Customer", primaryID);
    }
    catch(err)
    {
        displayErrorMsg("SetCustSelect", err);
    }
}


//功能：隐藏Name下拉列表,在名称输入文本框失去焦点后调用
function HideName()
{
    delayHideName();
    //HideElementById(divName_id);
}

function delayHideName()
{
    var code = "HideElementById('" + divName_id + "')";
    window.setTimeout(code, 200);
}

//功能：当名称输入文本框获得焦点时且有多项选择时显示下拉列表
function GetNameFocus()
{
    GetFocusEvent(lbName_id, divName_id);
}


//功能：获得ID为primaryID的客户或者供应商的ID和Name
function getIDAndName(primaryID)
{
    try
    {
        return SmartSoft.Presentation.BaseController.GetCustIDAndCustName(primaryID).value;  
    }
    catch(err)
    {
        displayErrorMsg("getIDAndName", err);
    }
}

//主键的搜索，即：客户编码，供应商编码，客户名称，供应商名称
var hasPrimarySearch = true;            
//初始化特效事件
function initEnventHandle()
{
    try
    {
        var tbName = GetElementById(txtNameField_id);
        var lbName = GetElementById(lbName_id);
        
        if (tbName != null && hasPrimarySearch)
        {
            tbName.onkeyup = function(){SearchName();}
            tbName.onblur = function(){HideName();}
            tbName.onfocusin = function () { GetNameFocus(); }
        }
        
        if (lbName != null && hasPrimarySearch)
        {
            lbName.onclick = function(){SetSelectName(this);}
        }
    }
    catch(err)
    {
        displayErrorMsg("initEnventHandle", err);
    }  
}


function SetMainAmount(mainAmount)
{
    try
    {
        if (txtMainAmountField_id != null)
        {
            var txtMainAmount = GetElementById(txtMainAmountField_id);
            if (txtMainAmount != null)
            {
                SetValue(txtMainAmount, Round(mainAmount, unitAmount));
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("SetMainAmount", err);
    }
}

//改变背景色
function ChangeBackColor(objId)
{
    var obj = GetElementById(objId)
    if(obj != null)
    {
        obj.style.color = "red";
    }
}

function SetFieldValue(fieldID, v)
{
    if (fieldID != null)
    {
        var element = GetElementById(fieldID);
        if (element != null)
        {
            if (v != null && v != "")
            {
                SetValue(element, v);
            }
            else
            {
                SetValue(element, "");
            }
        }
    }
}

//根据ID获得客户实体
function getPrimaryEntity(ID)
{
    return SmartSoft.Presentation.BaseController.GetCustomerByID(ID).value;
}


//打印参数
var billTypeId = 0;                 //单据编号
var mainTableName = "";             //主表名称
var rowID = getUrlParameter("ID");  //主表记录编号,初始化为默认由URL中获得
var itemTableName = "";             //子表名称
var itemMainRequiredColumn = "";    //子表主键字段名
var customerID = ""
