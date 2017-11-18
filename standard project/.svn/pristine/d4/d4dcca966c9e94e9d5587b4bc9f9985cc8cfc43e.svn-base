/************************************************************
* 
*  主从表模式中的子表的特效实现脚本
*
*************************************************************/


//共用全局变量未初始化的将在页面中统一进行初始化

var htDataType = new Array();               //用于客户端格式化
var txtProductIDField_id = "";              //”货品编号“ 文本框的控件编号 
var txtPriceField_id = "";                  //”单价“ 文本框的控件编号
var txtQtyField_id = "";                    //"数量" 文本框控件的编号
var txtItemAmountField_id = "";             //"金额" 文本框控件的编号

var lbType_id = "lbType";                   //货品编码文本框控件编号
var txtPartNumberField_id = "FieldItemname"; //货品类型文本框控件编号
var divType_id = "ShowTypeResult";          //货品类型div控件编号

var divPrice_id = "ShowPriceResult";        //单价div
var lbPrice_id = "lbPrice";                 //单价下拉框控件编号

//功能：提交子表记录，暂时存于主表单的列表中
var extend = "Field";

var arrayProductIDNameList = new Array();
var maxProductReturned = 100;
function getAllProductIDAndNameList()
{
    if (arrayProductIDNameList == null || arrayProductIDNameList.length == 0)
    {
        arrayProductIDNameList = SmartSoft.Presentation.BaseController.GetProductIDAndTypeListAll().value;
    } 
    return arrayProductIDNameList;
}

function getProductIDAndNameList(strName)
{
    try
    {
        strName = strName.toString();
        getAllProductIDAndNameList();
        var arrayResult = new Array();
        var gotCount = 0;
        if (arrayProductIDNameList != null)
        {
            for (var i = 0; i < arrayProductIDNameList.length; i++)
            {
                var details = arrayProductIDNameList[i].toString().split("|");
                if (details.length == 2)
                {
                    if (details[1].indexOf(strName) != -1)
                    {
                        arrayResult.push(arrayProductIDNameList[i]);
                        gotCount++;
                    }
                    if (gotCount >= maxProductReturned)
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
        displayErrorMsg("getProductIDAndNameList", err);
    }
}
//按货品类型搜索货品，在货品输入文本框的keyup事件中触发
function SearchType(prdTypeText)
{
    try
    {
        var divResult = GetElementById(divType_id);
        var prdIDText = GetElementById(txtProductIDField_id);
        var lbPrdType = GetElementById(lbType_id);
        var nKeyCode = event.keyCode;
        var prdID = GetValue(prdIDText);
        var strType = prdTypeText.value;
        
        if(nKeyCode == 13)
        {
            SetSelectProduct(lbPrdType);
        }
        else if (nKeyCode == 38 || nKeyCode == 40)    //处理向上向下方向键
        {
            divResult.style.display = "";
            var optionLength = lbPrdType.options.length;
            var selectedIndex = lbPrdType.selectedIndex;
            if (lbPrdType.options.length > 0 && lbPrdType.selectedIndex > -1)
            {  
                selectedIndex = optionLength + selectedIndex;
                selectedIndex = nKeyCode == 38 ? selectedIndex - 1 : selectedIndex + 1;
                selectedIndex = selectedIndex % optionLength;
                lbPrdType.selectedIndex = selectedIndex;
                
                prdTypeText.value = lbPrdType.options[selectedIndex].text;
                SetValue(prdIDText, lbPrdType.value);
            }
        }
        else
        {
            divResult.style.left = $(prdTypeText).position().left;
            divResult.style.top = $(prdTypeText).position().top + 20;
            lbPrdType.options.length = 0;
            var selectsource = new Array();
            
            selectsource = getProductIDAndNameList(strType);
            if (selectsource == null || selectsource.length <= 0)                
            {                            
                strType = strType.substring(0, strType.length-1);
                SetValue(prdTypeText, strType);
                selectsource = getProductIDAndNameList(strType);
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
                if (sValueArray[1] != "")
                {
                    lbPrdType.options[lbPrdType.options.length] = new Option(sValueArray[1],sValueArray[0]); 
                }
            }
            if (lbPrdType.options.length > 0)
            {
                lbPrdType.selectedIndex = 0; 
            }
        }  
    }
    catch(err)
    {
        displayErrorMsg("SearchType", err);
    }
}


//功能：根据选择ID设置货品相关信息,在下拉列表的onclick事件中触发
function SetSelectProduct(sourceSelect)
{
    try
    {
        var selectedIndex = sourceSelect.selectedIndex;
        if (selectedIndex > -1)
        {
            var prdID = sourceSelect.options[selectedIndex].value;
            var prdIDText = GetElementById(txtProductIDField_id);
            SetValue(prdIDText, prdID);
            BindRecordToControlByTableName("XProduct", prdID, "FieldItem")
        }
        SetValue(GetElementById(txtProductIDField_id), prdID);

        //隐藏下拉框
        HideType(); 
        
    }
    catch(err)
    {
        displayErrorMsg("SetSelectProduct", err);
    }
}

//隐藏货品TYPE列表框,在货品TYPE输入文本框失去焦点时调用
function HideType()
{
    HideElementById(divType_id);
}

//功能：当货品类别输入文本框获得焦点时且有多项选择时显示下拉列表
function GetTypeFocus(prdTypeText)
{
    var divResult = GetElementById(divType_id);
    divResult.style.left = $(prdTypeText).position().left;
    divResult.style.top = $(prdTypeText).position().top + 20;
    GetFocusEvent(lbType_id, divType_id);
    SearchType(prdTypeText);
}

//隐藏Price列表框,在货品Price输入文本框失去焦点时调用
function HidePrice()
{
    HideElementById(divPrice_id);
}

//开关变量
var hasProductSearch = true;    //是否包含货品编码和货品型号的搜索功能
var hasPriceSearch  = false;     //是否包含历史报价的搜索
var hasCountAmount  = false;     //是否包含计算金额

//功能：当Price输入文本框获得焦点时且有多项选择时显示下拉列表
function GetPriceFocus(priceText)
{   
    try
    {
        var divResult = GetElementById(divPrice_id);
        var lbPrice = GetElementById(lbPrice_id);
        var prdText = "";
        prdText = GetValue(GetElementById(txtPartNumberField_id));
        var pID = GetValue(GetElementById(txtPrimaryIDField_id));
        if (prdText != "" && pID != "")
        {
            lbPrice.options.length = 0;
            var selectsource = new Array();
            selectsource = SmartSoft.Presentation.SellController.GetPriceList(pID, prdText).value;
            var i;
            if (selectsource != null)
            {
                for (i = 0; i < selectsource.length; i++)
                {
                    if (selectsource[i] != "")
                    {
                        lbPrice.options[i] = new Option(selectsource[i],selectsource[i]); 
                    }
                }
                lbPrice.options[0].selected = true;
            }
            divResult.style.left = $(priceText).position().left;
            divResult.style.top = $(priceText).position().top + 20;
            divResult.style.display = "";
        }
    }
    catch(err)
    {
        displayErrorMsg("GetPriceFocus", err);
    }
}


//功能：设置选择的价格,在下拉列表的onclick事件中触发
function SetSelectPrice(sourceSelect)
{
    try
    {
        var selectedIndex = sourceSelect.selectedIndex;

        if (selectedIndex > -1)
        {
            var txtPrice = GetElementById(txtPriceField_id);
            var sValueArray = sourceSelect.options[selectedIndex].value.split("[");
            var price = sValueArray[0];   
            SetValue(txtPrice, Round(price, unitPrice));
            HandlePriceOrQtyBlur();
        }
        HidePrice(); 
        
    }
    catch(err)
    {
        displayErrorMsg("SetSelectPrice", err);
    }
}


//在价格输入文本框的keyup事件中触发
function HandlePriceKeyUp()
{
    try
    {
        var divResult = GetElementById(divPrice_id);
        var priceText = GetElementById(txtPriceField_id);
        var lbPrice = GetElementById(lbPrice_id);
        var nKeyCode = event.keyCode;
        var price = 0;
        
        if(nKeyCode == 13)
        {
            SetSelectPrice(lbPrice);
        }
        else if (nKeyCode == 38 || nKeyCode == 40)    //处理向上向下方向键
        {
            divResult.style.display = "";
            var optionLength = lbPrice.options.length;
            var selectedIndex = lbPrice.selectedIndex;
            if (lbPrice.options.length > 0 && lbPrice.selectedIndex > -1)
            {  
                selectedIndex = optionLength + selectedIndex;
                selectedIndex = nKeyCode == 38 ? selectedIndex - 1 : selectedIndex + 1;
                selectedIndex = selectedIndex % optionLength;
                lbPrice.selectedIndex = selectedIndex;
                
                var priceValues = lbPrice.options[selectedIndex].value.toString().split("[");
                
                SetValue(priceText, priceValues[0]);
            }
        }
        else
        {
            controlNumberOnKeyUp(priceText);
        }
    }
    catch(err)
    {
        displayErrorMsg("HandlePriceKeyUp", err);
    }
}

function HandlePriceOrQtyBlur()
{
    try
    {
        var txtPrice = GetElementById(txtPriceField_id);
        var txtQty = GetElementById(txtQtyField_id);
        var lblItemAmount = GetElementById(txtItemAmountField_id);
        if (txtPrice != null && txtQty != null && lblItemAmount != null)
        {
            var price = GetNumber(GetValue(txtPrice));
            var qty = GetNumber(GetValue(txtQty));
            SetValue(lblItemAmount, ChangedWithThousandTag(Round(price*qty, unitAmount)));
        }
        HidePrice();
    }
    catch(err)
    {
        displayErrorMsg("HandlePriceOrQtyBlur", err);
    }
}

function GetNumber(formatString)
{
    try
    {
        return formatString.toString().replace(/\,/g, "") - 0;
    }
    catch(err)
    {
        displayErrorMsg("GetNumber", err);
    }
}

var checkIsChanged = true;
var isChanged = false;
function SetChangeTag()
{
    window.opener.isChanged = true;
    isChanged = true;
}

function saveBeforeClose()
{
    GetElementById("ToolBar1_lb_Save").click();      
    alert("正在保存......请稍确定！");
}

function window.onunload()
{
    if(s == "fresh")
    {
        if(window.screenLeft > 10000 && checkIsChanged && isChanged)
        {
            if (window.confirm("已有改动！是否需要保存?"))
            {
                saveBeforeClose();
            }
        }
    }
    else if (checkIsChanged && isChanged)
    {
        if (window.confirm("已有改动！是否需要保存?"))
        {
            saveBeforeClose();
        }
    }
}

window.onbeforeunload = function()
{
   s = "fresh";
}


