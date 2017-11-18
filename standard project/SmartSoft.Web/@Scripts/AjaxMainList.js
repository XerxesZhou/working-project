/************************************************************
* 
*  具有客户供应商或者货品为查询条件的列表的特效实现脚本
*
*************************************************************/

var txtProductSN = null;
var txtProductPartNumber = null;
var txtCustomerSN = null;
var txtCustomerName = null;
var txtSupplierSN = null;
var txtSupplierName = null;

var hasSearchProductSN = true;
var hasSearchProductPartNumber = true;
var hasSearchCustomerSN = true;
var hasSearchCustomerName = true;
var hasSearchSupplierSN = true;
var hasSearchSupplierName = true;

var listBoxArray = new Array();
var divArray = new Array();


//功能:查询条件页面onload事件函数
function SpecialEffectLoadEvent()
{
    try
    {
        createListBoxesAndDivs();
        //初始化各种应触发的事件
        initEnventHandle();
    }
    catch(err)
    {
        displayErrorMsg("LoadEvent", err);
    }  
}

//功能：创建listBoxes和Divs
function createListBoxesAndDivs()
{
    try
    {
        divArray[1] = document.getElementById("divProductSN");
        listBoxArray[1] = document.getElementById("lbProductSN");
       
        divArray[2] = document.getElementById("divProductPartNumber");
        listBoxArray[2] = document.getElementById("lbProductPartNumber");
       
     
        divArray[3] = document.getElementById("divCustomerSN");
        listBoxArray[3] = document.getElementById("lbCustomerSN");
       
        divArray[4] = document.getElementById("divCustomerName");
        listBoxArray[4] = document.getElementById("lbCustomerName");
        
        divArray[5] = document.getElementById("divSupplierSN");
        listBoxArray[5] = document.getElementById("lbSupplierSN");
        
        divArray[6] = document.getElementById("divSupplierName");
        listBoxArray[6] = document.getElementById("lbSupplierName");

    }
    catch(err)
    {
        displayErrorMsg("createListBoxesAndDivs", err);
    }  

}


//初始化相关特效事件
function initEnventHandle()
{
    try
    {
        inputs = document.getElementsByTagName("INPUT");
        for (var i = 0; i < inputs.length; i++)
        {
            sID = inputs[i].id;
            if (sID.indexOf("proSN") >= 0)
            {
                txtProductSN = inputs[i];
            }
            else if (sID.indexOf("proPartNumber") >= 0 && sID.indexOf("proPartNumber2") == -1)
            {
                txtProductPartNumber = inputs[i];
            }
            else if (sID.indexOf("cusSN") >= 0)
            {
                txtCustomerSN = inputs[i];
            }
            else if (sID.indexOf("cusName") >= 0)
            {
                txtCustomerName = inputs[i];
            }
            else if (sID.indexOf("supSN") >= 0)
            {
                txtSupplierSN = inputs[i];
            }
            else if (sID.indexOf("supName") >= 0)
            {
                txtSupplierName = inputs[i];
            }
        }
        
        //货品编码事件
        if (txtProductSN != null && hasSearchProductSN)
        {
            txtProductSN.onkeyup = function(){Search(this);}
            txtProductSN.onfocusout = function(){HideAll();}
            txtProductSN.onfocusin = function(){DisplayList(1);}
            if (listBoxArray[1] != null)
            {
                listBoxArray[1].onclick = function(){SetSelect(1, this);}
            }
        }

        //货品类型事件
        if (txtProductPartNumber != null && hasSearchProductPartNumber)
        {
            txtProductPartNumber.onkeyup = function(){Search(this);}
            txtProductPartNumber.onfocusout = function(){HideAll();}
            txtProductPartNumber.onfocusin = function(){DisplayList(2);}
            if (listBoxArray[2] != null)
            {
                listBoxArray[2].onclick = function(){SetSelect(2, this);}
            }
        }
        
        //客户编码事件
        if (txtCustomerSN != null && hasSearchCustomerSN)
        {
            txtCustomerSN.onkeyup = function(){Search(this);}
            txtCustomerSN.onfocusout = function(){HideAll();}
            txtCustomerSN.onfocusin = function(){DisplayList(3);}
            if (listBoxArray[3] != null)
            {
                listBoxArray[3].onclick = function(){SetSelect(3, this);}
            }
        }
        
        //客户名称事件
        if (txtCustomerName != null && hasSearchCustomerName)
        {
            txtCustomerName.onkeyup = function(){Search(this);}
            txtCustomerName.onfocusout = function(){HideAll();}
            txtCustomerName.onfocusin = function(){DisplayList(4);}
            if (listBoxArray[4] != null)
            {
                listBoxArray[4].onclick = function(){SetSelect(4, this);}
            }
        }
        
        //供应商编码事件
        if (txtSupplierSN != null && hasSearchSupplierSN)
        {
            txtSupplierSN.onkeyup = function(){Search(this);}
            txtSupplierSN.onfocusout = function(){HideAll();}
            txtSupplierSN.onfocusin = function(){DisplayList(5);}
            if (listBoxArray[5] != null)
            {
                listBoxArray[5].onclick = function(){SetSelect(5, this);}
            }
        }
        
        //供应商名称事件
        if (txtSupplierName != null && hasSearchSupplierName)
        {
            txtSupplierName.onkeyup = function(){Search(this);}
            txtSupplierName.onfocusout = function(){HideAll();}
            txtSupplierName.onfocusin = function(){DisplayList(6);}
            if (listBoxArray[6] != null)
            {
                listBoxArray[6].onclick = function(){SetSelect(6, this);}
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("initEnventHandle", err);
    }
}

//功能：根据选择设置相关信息,在下拉列表的onclick事件中触发
//typeID:
//  1:货品编码
//  2:货品型号
//  3:客户编码
//  4:客户名称
//  5:供应商编码
//  6:供应商名称
//  sourceSelect:对应的lb控件
function SetSelect(typeID, sourceSelect)
{
    try
    {
        var selectedIndex = sourceSelect.selectedIndex;
        if (selectedIndex > -1)
        {
            var sID = GetValue(sourceSelect);
            var objEntity = new Object();
            switch(typeID)
            {
                case 1:
                case 2:
                    objEntity = SmartSoft.Presentation.BaseController.GetProductByID(sID).value;
                    SetValue(txtProductSN, objEntity.proSN);
                    SetValue(txtProductPartNumber, objEntity.proPartNumber);
                    break;
                    
                case 3:
                case 4:
                    objEntity = SmartSoft.Presentation.BaseController.GetCustomerByID(sID).value;
                    SetValue(txtCustomerSN, objEntity.cusSN);
                    SetValue(txtCustomerName, objEntity.cusName);
                    break;
                    
                case 5:
                case 6:
                    objEntity = SmartSoft.Presentation.BaseController.GetSupplierByID(sID).value;
                    SetValue(txtSupplierSN, objEntity.supSN);
                    SetValue(txtSupplierName, objEntity.supName);
                    break;        
            }
        }

        HideAll1();
    }
    catch(err)
    {
        displayErrorMsg("SetSelect", err);
    }
}

function HideAll()
{
    try
    {
        for (var i = 1; i <= 6; i++)
        {
            //得到当前的鼠标的位置
            var mouse_x = window.event.x;
            var mouse_y = window.event.y;    

            //得到此文本框的范围
            var   oRect   =   divArray[i].getBoundingClientRect(); 
            
 　        // alert("范围左" + oRect.left + "范围右" + oRect.right +"范围上" + oRect.top + "范围下" + oRect.bottom + "鼠标x" + mouse_x + "鼠标y" + mouse_y);
            //如果当前鼠标在文本框范围内，则不执行任何代码，直接退出
            if( mouse_x > oRect.left && mouse_x < oRect.right  && mouse_y < oRect.bottom && mouse_y > oRect.top)
            {
                return;       
            }
            else
            {
                divArray[i].style.display = "none";
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("HideAll", err);
    } 
}

function HideAll1()
{
    try
    {
        for (var i = 1; i <= 6; i++)
        {
            if(divArray[i] != null)
            {
                divArray[i].style.display = "none";
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("HideAll1", err);
    } 
}

function DisplayList(typeID)
{
    try
    {
        if (divArray[typeID] != null && listBoxArray[typeID] != null)
        {
            if (listBoxArray[typeID].options.length > 0)
            {
                divArray[typeID].style.display = "";
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("DisplayList", err);
    } 
}

function getTypeID(elementID)
{
    if (elementID.indexOf("proSN") >= 0)
    {
        return 1;
    }
    else if (elementID.indexOf("proPartNumber") >= 0)
    {
        return 2;
    }
    else if (elementID.indexOf("cusSN") >= 0)
    {
        return 3;
    }
    else if (elementID.indexOf("cusName") >= 0)
    {
        return 4;
    }
    else if (elementID.indexOf("supSN") >= 0)
    {
        return 5;
    }
    else if (elementID.indexOf("supName") >= 0)
    {
        return 6;
    }
}

function getListBox(typeID)
{
    try
    {
        return listBoxArray[typeID];
    }
    catch(err)
    {
        displayErrorMsg("getListBox", err);
    }
}

function getDiv(typeID)
{
    try
    {
        return divArray[typeID];
    }
    catch(err)
    {
        displayErrorMsg("getDiv", err);
    }
}

//功能：通过SN获得相关信息并赋值,在keyup事件中调用
//element:需要查询信息的文本框对象
function Search(element)
{
    try
    {
        //作为主键的编码如
        var typeID = getTypeID(element.id);
        var ctrListBox = getListBox(typeID);
        var ctrDiv  = getDiv(typeID);
        
        var selectIndex = ctrListBox.selectedIndex;
        var searchText = GetValue(element);
        var nKeyCode = event.keyCode;
       
        //回车时的处理
        if (nKeyCode == 13) 
        {
            SetSelect(typeID, ctrListBox);
        }
        else if (nKeyCode == 38 || nKeyCode == 40)    //处理向上向下方向键
        {
            ctrDiv.style.display = "";   
            var selectedIndex = ctrListBox.selectedIndex;
            var optionLength = ctrListBox.options.length;
            
            if (ctrListBox.options.length > 0 && ctrListBox.selectedIndex > -1)
            {  
                selectedIndex = optionLength + selectedIndex;
                selectedIndex = nKeyCode == 38 ? selectedIndex - 1 : selectedIndex + 1;
                selectedIndex = selectedIndex % optionLength;
                ctrListBox.selectedIndex = selectedIndex;
                SetValue(element, ctrListBox.options[selectedIndex].text);
            }
            SetSelect(typeID, ctrListBox);
            DisplayList(typeID);
        }
        else
        {
            if (element.value.length > 0)    //输入字符事件处理
            {
                ctrDiv.style.left = getObjX(element);
                ctrDiv.style.top = getObjY(element) + 20;
                ctrListBox.options.length = 0;
                var selectsource = new Array();
                selectsource = getListDataSource(typeID, searchText);
                if (selectsource == null || selectsource.length <= 0)   //如果下拉框里没有合适的选择项，则回退
                {
                    searchText = searchText.substring(0, searchText.length-1);
                    selectsource = getListDataSource(searchText); 
                }
                ctrDiv.style.display = "";   
                var i;
                var sValue;
                var sValueArray = new Array();
                
                var nTime = selectsource == null ? 0 : selectsource.length; 
                for (i = 0; i < nTime; i++)
                {
                    sValue = selectsource[i];
                    sValueArray = sValue.split("|");
                    ctrListBox.options[i] = new Option(sValueArray[1], sValueArray[0]); 
                }
                if (ctrListBox.options.length > 0)
                {
                    ctrListBox.selectedIndex = 0;
                }
            }
        }   
    }
    catch(err)
    {
        displayErrorMsg("Search", err);
    }
}

//功能：根据文本查询对应的数据
//typeID:
//  1:货品编码
//  2:货品型号
//  3:客户编码
//  4:客户名称
//  5:供应商编码
//  6:供应商名称
//searchText:模糊查询的文本
function getListDataSource(typeID, searchText)
{
   try
    {
        var selectsource = new Array();
        switch(typeID)
        {
            case 1:
                selectsource = SmartSoft.Presentation.BaseController.GetPrdSNList(searchText).value;
                break;
           case 2:
                selectsource = SmartSoft.Presentation.BaseController.GetProductIDAndTypeList(searchText).value;
                break;
           case 3:
                selectsource = SmartSoft.Presentation.BaseController.GetCustSNList(searchText, currentOperatorID).value;
                break;
           case 4:
                selectsource = SmartSoft.Presentation.BaseController.GetCustIDAndCustNameList(searchText, currentOperatorID).value;
                break;
           case 5:
                selectsource = SmartSoft.Presentation.BaseController.GetSupSNList(searchText, currentOperatorID).value;
                break;
           case 6:
                selectsource = SmartSoft.Presentation.BaseController.GetSupIDAndSupNameList(searchText, currentOperatorID).value;
                break;     
        }
        return selectsource;
    }
    catch(err)
    {
        displayErrorMsg("getListDataSource", err);
    }   
}
