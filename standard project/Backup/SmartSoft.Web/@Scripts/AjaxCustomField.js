/****************************

编辑自定义字段页面中使用

****************************/

var currentRowCount = 1;
var dropDownListOptionTableObj = null;


//功能：初始化页面
function initCustomSelectTypePage(selectTypeID)
{
    var deOptions = new Array();
    if (dropDownListOptionTableObj == null)
    {
        dropDownListOptionTableObj = document.getElementById("dropDownListOptionTable");
    }
    if (selectTypeID != "")
    {
        deOptions = UDEF.Service.AjaxService.SelectDropdownlistOptionBySelectTypeID(selectTypeID).value;
        
        for (var i = 1; i <= deOptions.length; i++)
        {
            var row = addTemplete();
            row.cells[1].childNodes[0].value = deOptions[i-1].OptionValue;
            row.cells[1].childNodes[0].id = deOptions[i-1].OptionID;
            row.cells[2].childNodes[0].value = deOptions[i-1].Remark;
            row.cells[3].childNodes[0].checked = deOptions[i-1].StopTag;
        }
    }
    var lastRowIndex = dropDownListOptionTableObj.rows.length-1;
    var lastRow = dropDownListOptionTableObj.rows[lastRowIndex];
    addBtnAddCell(lastRow);  
}

//功能：新增一个模板列
function addTemplete()
{
    var row = dropDownListOptionTableObj.insertRow();
    row.id = "dropDownListOptionRow" + (currentRowCount);
    currentRowCount++;
    addOptionIndexCell(row);
    addOptionValueCell(row);
    addOptionRemarkCell(row);
    addOptionStopTagCell(row);
    addBtnDelCell(row);
    return row;
}

//功能：新增事件处理函数
function insertOptionRow()
{

    //dropDownListOptionTableObj.rows[currentRowCount-1].deleteCell(4);
    previousRow = dropDownListOptionTableObj.rows[currentRowCount-1];
    var cell = previousRow.cells[previousRow.cells.length - 1];
    cell.removeChild(cell.lastChild);

    var row = addTemplete();
    addBtnAddCell(row);
}

//功能：添加放置选项顺序的单元格
function addOptionIndexCell(row)
{
    var cell = row.insertCell();
    cell.innerHTML = "选项" + (currentRowCount-1).toString();
}

//功能：添加放置选择值的单元格
function addOptionValueCell(row)
{
    var cell = row.insertCell();
    var input = document.createElement("<input type='text' id='txtOptionValue' name='txtOptionValue' onblur='javascript:validOptionValueOnBlur(this);' style='width:100%'>");
    cell.appendChild(input);
    
}


//功能：添加放置选择值相应备注的单元格
function addOptionRemarkCell(row)
{
    var cell = row.insertCell();
    var input = document.createElement("<input type='text' name='txtOptionRemark' id='txtOptionRemark' style='width:100%'>");
    cell.appendChild(input);
    
}

//功能：添加放置是否停用选择框的单元格
function addOptionStopTagCell(row)
{
    var cell = row.insertCell();
    var checkbox = document.createElement("<input type='checkbox' id='cbOptionStopTag' name='cbOptionStopTag'>");
    cell.appendChild(checkbox);
}


//功能：添加放置删除按钮的单元格
function addBtnDelCell(row)
{
    cell = row.insertCell(); 
    var deleteBtnId = "delete" + row.id; 
    newElement = document.createElement("<input class='button' name='" + deleteBtnId + "' id='"+deleteBtnId+"' type='Button' value='删除' onclick='javascript:deleteRow("+row.id+")'>"); 
    cell.appendChild(newElement);   
}

//功能：添加放置新增按钮的单元格
function addBtnAddCell(row)
{
    //var cell = row.insertCell();
    var cell = row.cells[row.cells.length - 1];
    cell.innerHTML += "&nbsp;";
    var btnAdd = document.createElement("<input class='button' type='button' name='btnAdd' id='btnAdd' value='新增' onclick='javascript:insertOptionRow();>");
    cell.appendChild(btnAdd);
}

 
//功能：删除当前行 
function deleteRow(row) 
{  
    var rowOfIndex = row.rowIndex;
    if(dropDownListOptionTableObj.rows.length == 1) 
    { 
        return 
    } 
    else if (rowOfIndex == dropDownListOptionTableObj.rows.length-1) 
    { 
        //删除最后一行时，保持增加按纽在最后一行 
        var row = dropDownListOptionTableObj.rows[currentRowCount-2];
        addBtnAddCell(row);
    }  
    dropDownListOptionTableObj.deleteRow(rowOfIndex); 
    currentRowCount--;
    for (var i = 1; i < dropDownListOptionTableObj.rows.length; i++)
    {
        dropDownListOptionTableObj.rows[i].cells[0].innerHTML = "选项" + i;
    }
} 

//功能：检查输入的选项值是否合法
function validOptionValueOnBlur(textbox)
{
    var textValue = textbox.value;
    var equalCount = 0;
    for (var i = 1; i < dropDownListOptionTableObj.rows.length; i++)
    {
        var textOptionValue = dropDownListOptionTableObj.rows[i].cells[1].childNodes[0].value;
        if (textValue == textOptionValue)
        {
            equalCount++;
        }
    }
    if (equalCount > 1)
    {
        textbox.value = "有重复值，请重新填写！";
        textbox.select();
    }
}


//功能：获得所有选择的值，以实体形式返回
function getOptionValuesEntity()
{
    try
    {
        var items = new Array();
        for (var i = 1; i < dropDownListOptionTableObj.rows.length; i++)
        {
            var optionEntity = new Object();
            //属性之间以#相隔，项之间用|相隔
            var optionID = dropDownListOptionTableObj.rows[i].cells[1].childNodes[0].id ;      //OptionID  
            optionEntity.OptionID = (optionID == null || optionID == "") ? 0 : optionID; 
            optionEntity.OptionValue = dropDownListOptionTableObj.rows[i].cells[1].childNodes[0].value; //OptionValue
            optionEntity.Remark = dropDownListOptionTableObj.rows[i].cells[2].childNodes[0].value;      //Remark
            optionEntity.StopTag = dropDownListOptionTableObj.rows[i].cells[3].childNodes[0].checked;     //StopTagValue
            
            items[i-1] = optionEntity;
            
        }
        
        return items;
    }
    catch(err)
    {
        displayErrorMsg("getOptionValuesEntity", err);
    }
}

//功能：获得自定义数据源类型的实体信息     
function getSelectTypeDetailEntity()
{
    try
    {
        var selectTypeDetail = "";
        var selectTypeID = "";
        var selectTypeText = "";
        var remark = "";
        selectTypeID = document.getElementById('FieldSelectTypeID').value;
        selectTypeText = document.getElementById('FieldSelectTypeText').value;
        remark = document.getElementById('FieldRemark').value;
        
        var selectType = new Object();
        selectType.SelectTypeID = selectTypeID == "" ? 0 : selectTypeID;
        selectType.SelectTypeText = selectTypeText;
        selectType.Remark = remark;
        
        return selectType;
    }
    catch(err)
    {
        displayErrorMsg("getSelectType", err);
    }
}
 
//功能：存储自定义数据源类型
function saveSelectTypeAll()
{
    try
    {
        var selectTypeDetail = getSelectTypeDetailEntity();

        var optionValues = getOptionValuesEntity();

        var selectTypeID = getUrlParameter("SelectTypeID");
        var r = false;


        if (selectTypeID == null || selectTypeID == "") //新增
        {   
            r = UDEF.Service.AjaxService.AddCustomSelectTypeAll(selectTypeDetail, optionValues).value;
        }
        else    //修改
        {
            r = UDEF.Service.AjaxService.UpdateCustomSelectTypeAll(selectTypeDetail, optionValues).value;
        }
        
        if (r == true || r == "true" || r > 0)
        {
            alert('操作成功！');
        }
        else
        {
            alert('操作失败！');

        }
    }
    catch(err)
    {
        displayErrorMsg("saveSelectTypeAll", err);
    }
}





//功能：遍历一个对象的所有属性，调试时用
function display(obj, attribute)
{
    try
    {
        var meg = getAttr(obj, attribute);
        alert(meg);
    }
    catch(err)
    {
        displayErrorMsg("display", err);
    }

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

//功能：保存自定义字段
function saveCustomColumnAll()
{
    try
    {
        var columnID = document.getElementById("FieldColumnID").value; 
        var columnName = document.getElementById("FieldColumnName").value; 
        var columnText = document.getElementById("FieldColumnText").value; 
        var dataType = document.getElementById("FieldDataType").value;
        var selectType = document.getElementById("FieldSelectType").value;
        var remark = document.getElementById("FieldRemark").value;
        var tableID = getUrlParameter("TableID");
        var obj = new Object();
        obj.ColumnID = columnID == null || columnID == '' ? 0 : columnID;
        obj.ColumnText = columnText;
        obj.DataType = dataType;
        obj.SelectType = selectType == null || selectType == "" ? 0 : selectType;
        obj.Remark = remark;
        obj.TableID = tableID;
        obj.ColumnName = columnName;
        var bSuccess = UDEF.Service.AjaxService.SaveCustomColumn(obj).value;
        if (bSuccess)
        {
            alert('操作成功！');
            window.location = 'ListColumn.aspx?TableID=' + tableID;
        }
        else
        {
            alert('操作失败！');
        }
    }
    catch(err)
    {
        displayErrorMsg("saveCustomColumnAll", err);
    }   
}

    
