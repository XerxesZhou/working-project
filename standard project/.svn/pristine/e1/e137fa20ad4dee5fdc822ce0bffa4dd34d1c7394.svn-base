/************************

编辑自定义视图页面中使用

************************/

var tbConditions    =   null;       //用于展示组合条件的表控件 
var tableName       =   null;       //记录查询的表 
var fieldsInfo      =   null;       //保存从服务端取得的对应表的字段信息 
var conditionNo     =   1;          //为防止出现重复id保存的计数器 
var tbSorts      =   null;       //用于展示排序组合的表控件
var orderByNo       =   1;          //为防止出现重复id保存的计数器 

var columnIDsSelected = new Array();    //关联数组，用于存取已取的字段的ID
var currentTableID = 0;                 //当前选择的表ID
var columnID_TableID = new Array();     //关联数组，用于columnID到tableID间的映射


//初始化视图编辑页面
function initViewPage(tbID, viewID, selObj)
{
    try
    {
        tableName =  tbID; 
        if (tbConditions == null)  tbConditions = document.getElementById("tbConditions"); 
        addBtnAdd(tbConditions.rows[0]);
        if (tbSorts == null) tbSorts = document.getElementById("tbSorts"); 
        sortAddBtnAdd(tbSorts.rows[0]);
        
        //将该表及所有外键表加入selObj下拉框元素中
        addFieldSourceTable(tbID, selObj);
        
        //取得服务器端传来的视图的所有查询和排序条件信息
        if (viewID != null && viewID != 0)
        {
            var columnView = new Object();
            columnView = UDEF.Service.AjaxService.GetViewDetail(viewID).value;  
            document.getElementById("txtViewName").value = columnView.ViewName;
            var conditionItems = new Array();
            var sortItems = new Array();
            var orderItems = new Array();
            
            conditionItems = UDEF.Service.AjaxService.SelectConditionByViewID(viewID).value;
            sortItems = UDEF.Service.AjaxService.SelectSortByViewID(viewID).value;
            orderItems = UDEF.Service.AjaxService.SelectOrderByViewID(viewID).value;
            
            initViewPageCondition(conditionItems);
            initViewPageSort(sortItems);
            initViewPageOrder(orderItems);
        }

        //左边列表显示备选字段
        var selObj = document.getElementById("ddlTable");

        handleTableChange(selObj);
    }
    catch(err)
    {
        displayErrorMsg("initViewPage", err);
    }
    
}

//功能：修改模式时初始化查询条件
//conditionItems:视图的所有查询条件
function initViewPageCondition(conditionItems)
{
    try
    {
        if (tbConditions == null)  tbConditions = document.getElementById("tbConditions"); 
        for (var i = 0; i < conditionItems.length; i++)
        {

            //添加新行模板
            var row = addTemplete("tbConditions");
            
            var conditionItem = conditionItems[i];
            
            var rowID = row.id;
            
            //添加字段栏并选定初值
            addField(row);
            var fieldInputId = "field" + rowID;
            var fieldSelect = document.getElementById(fieldInputId);
            fieldSelect.value = conditionItem.ColumnID;
            
            //添加操作符栏并选定初值
            addOpt(row);
            var optId = "opt" + rowID;
            var optSelect = document.getElementById(optId);
            optSelect.value = conditionItem.ExpressionID;
            
            //添加输入栏并初始化
            addInput(row);
            var inputValueID = "input" + rowID;
            var inputObj = document.getElementById(inputValueID);
            if (inputObj.type == "checkbox")
            {
                inputObj.checked = conditionItem.ConditionValue == "1";
            }
            else
            {
                inputObj.value = conditionItem.ConditionValue;
            }
            
            //添加连接条件栏并初始化
            addConnection(row);
            var relateID = "relate" + rowID;
            var relateSelect = document.getElementById(relateID);
            relateSelect.selectedIndex = conditionItem.Connector;
            
            //添加删除按钮
            addBtnDelete(row);
            
            //调整新增按钮位置,即如果是最后一行，则添加新增按钮,并删除前一行的新增按扭
            if (i == conditionItems.length-1)
            {
                addBtnAdd(row);
                tbConditions.rows[0].deleteCell(5);
                
                //令最后一行的连接条件隐藏
                tbConditions.rows[i+1].cells[3].childNodes[0].style.display = "none"; 
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("initViewPageCondition", err);
    }
}

//功能：修改模式时初始化排序条件
//sortItems:视图的所有排序项
function initViewPageSort(sortItems)
{
    try
    {
        if (tbSorts == null) tbSorts = document.getElementById("tbSorts"); 
 
        for (var i = 0; i < sortItems.length; i++)
        {
            //添加行模板
            var row = sortAddTemplete("tbSorts");
            
            //以|分离出排序条件的属性
            var sortItem = sortItems[i];
            
            var rowID = tbSorts.rows[i+1].id;

            //添加字段栏并初始化
            sortAddField(row);
            var fieldInputId = "sortField" + rowID;
            var fieldSelect = document.getElementById(fieldInputId);

            fieldSelect.value = sortItem.ColumnID;
            
            //添加排序顺序栏并初始化
            sortAddSortMode(row);
            var sortID =  "sortRelate" + rowID;
            var sortSelect = document.getElementById(sortID);
            sortSelect.selectedIndex = sortItem.IsDesc ? 1 : 0; //0为升序，1为降序

            //添加删除按钮
            sortAddBtnDelete(row);
            
            //调整新增按钮位置
            if (i == sortItems.length-1)
            {
                sortAddBtnAdd(row);
                tbSorts.rows[0].deleteCell(3);
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("initViewPageSort", err);
    }

}


// 添加一待填模板行 
function addTemplete(tbPanelId)
{
    try
    {
        if (tbConditions == null)  tbConditions= document.getElementById(tbPanelId); 
        if(tableName == null) tableName = "1"; 
        var row    = tbConditions.insertRow();
        row.align = "center"; 
        conditionNo++; 
        row.id = "row"  + conditionNo; 
        return row;
    }
    catch(err)
    {
        displayErrorMsg("addTemplete", err);
    }
}

//添加字段选择部分
function addField(row)
{
    try
    {
        var cell            = row.insertCell(); 
        var fieldInputId    = "field"+row.id;
        $(cell).append("<SELECT name='" + fieldInputId + "' id='" + fieldInputId + "' onchange='javascript:getOpts(" + row.id + ");getInput(" + row.id + ")'></SELECT>"); 
         
        //cell.insertBefore(newElement); 
         
        //取出字段信息,首次从服务端取得 
        if(fieldsInfo == null) 
        { 
            fieldsInfo = UDEF.Service.AjaxService.GetFields(tableName.toString()).value; 
        } 
        
        //分解字段信息，格式为“字段中文名|字段名” 
     
        var fieldsInfoLen; 
        var i,index; 
        var fieldValue; 
         
        fieldsInfoLen = fieldsInfo.length 
         
        for(i=0;i<fieldsInfoLen;i++) 
        { 
            fieldValue = fieldsInfo[i]; 
            index = fieldValue.indexOf("|"); 
            newElement.options[newElement.options.length] = new Option(fieldValue.substring(0,index),fieldValue.substring(index+1,fieldValue.length)); 
        } 
    }
    catch(err)
    {
        displayErrorMsg("addField", err);
    }

}

//提取字段对应的运算符
function addOpt(row)
{
    try
    {
        var cell = row.insertCell(); 
        var optInputId = "opt" +row.id;
        $(cell).append("<SELECT id='" + optInputId + "' name='" + optInputId + "'></SELECT>"); 
        //cell.insertBefore(newElement); 
        getOpts(row); 
    }
    catch(err)
    {
        displayErrorMsg("addOpt", err);
    }

}


//设置字段条件值的录入（选择）框
function addInput(row)
{
    getInput(row); 
}

//生成连接条件选择框
function addConnection(row)
{
    try
    {
        var cell = row.insertCell(); 
        var relateInputId = "relate"+row.id;
        $(cell).append("<SELECT id='" + relateInputId + "' name='" + relateInputId + "'></SELECT>");
        //cell.insertBefore(newElement);
        var newElement = document.getElementById(relateInputId);
        newElement.options[newElement.options.length] = new Option("并且","And"); 
        newElement.options[newElement.options.length] = new Option("或者","Or"); 
    }
    catch(err)
    {
        displayErrorMsg("addConnection", err);
    }

}

//生成删除当前行按纽
function addBtnDelete(row)
{
    try
    {
        var cell = row.insertCell(); 
        var deleteBtnId = "delete"+row.id;
        $(cell).append("<INPUT id='" + deleteBtnId + "' name='" + deleteBtnId + "' type='Button' class='button' value='删除' onclick='javascript:DelRow(" + row.id + ")'/>"); 
        //cell.insertBefore(newElement);  
    }
    catch(err)
    {
        displayErrorMsg("addBtnDelete", err);
    } 
}

//生成增加下一行按纽
function addBtnAdd(row)
{
    try
    {
        var cell = row.insertCell();
        var addBtnId = "add" + row.id;
        var newElement = $("<input id='" + addBtnId + "' name='" + addBtnId + "' type='button' value='新增' class='button' onclick='javascript:addRow(\'" + row.id + "\')'></input>");
        $(cell).append(newElement); 
    }
    catch(err)
    {
        displayErrorMsg("addBtnAdd", err);
    }
}

//添加查询条件编辑行 
function addTerm(tbPanelId) 
{ 
    try
    {         
        //添加一待填模板
        var row = addTemplete(tbPanelId);
        
        //添加字段栏
        addField(row);
        
        //添加运算符栏
        addOpt(row);
        
        //添加输入值栏
        addInput(row);
        
        //添加连接条件栏
        addConnection(row);
        
        //添加删除按钮
        addBtnDelete(row)
        
        //添加新增按钮
        addBtnAdd(row);

    }
    catch(err)
    {
        displayErrorMsg("addTerm", err);
    }

} 

// 添加一待填模板行 
function sortAddTemplete(tbPanelId)
{
    try
    {
        tbSorts = document.getElementById(tbPanelId); 
        if(tableName == null) tableName = "1"; 
         
        var row    = tbSorts.insertRow(); 
        row.align = "center"; 
        orderByNo++; 
        row.id = "orderByRow" + orderByNo; 
        return row;
    }
    catch(err)
    {
        displayErrorMsg("sortAddTemplete", err);
    }

}

//添加字段选择部分
function sortAddField(row)
{
  
    try
    {
        var cell            = row.insertCell(); 
        var fieldInputId    = "sortField"+row.id;
        $(cell).append("<SELECT id='" + fieldInputId + "' name='" + fieldInputId + "'></SELECT>"); 
         
        //cell.insertBefore(newElement);

        var newElement = document.getElementById(fieldInputId);
        //取出字段信息,首次从服务端取得 
        if(fieldsInfo == null) 
        { 
            fieldsInfo = UDEF.Service.AjaxService.GetFields(tableName.toString()).value; 
        } 
         
        //分解字段信息，格式为“字段中文名|字段名” 
     
        var fieldsInfoLen; 
        var i,index; 
        var fieldValue; 
         
        fieldsInfoLen = fieldsInfo.length 
         
        for(i = 0;i < fieldsInfoLen; i++) 
        { 
            fieldValue = fieldsInfo[i]; 
            index = fieldValue.indexOf("|"); 
            newElement.options[newElement.options.length] = new Option(fieldValue.substring(0,index),fieldValue.substring(index+1,fieldValue.length)); 
        }  
    }
    catch(err)
    {
        displayErrorMsg("sortAddField", err);
    }     

}

//生成排序条件选择框
function sortAddSortMode(row)
{
    try
    {
        cell = row.insertCell(); 
        var relateInputId = "sortRelate"+row.id;
        $(cell).append("<SELECT id='" + relateInputId + "' name='" + relateInputId + "'></SELECT>");
        //cell.insertBefore(newElement);
        var newElement = document.getElementById(relateInputId);
        newElement.options[newElement.options.length] = new Option("升序","Asc"); 
        newElement.options[newElement.options.length] = new Option("降序","Desc"); 
    }
    catch(err)
    {
        displayErrorMsg("sortAddSortMode", err);
    }

}

//生成删除当前行按纽
function sortAddBtnDelete(row)
{
    try
    {
        var cell = row.insertCell(); 
        var deleteBtnId = "sortDelete" + row.id;
        $(cell).append("<INPUT id='" + deleteBtnId + "' name='" + deleteBtnId + "' type='Button' class='button' value='删除' onclick='javascript:sortDelRow(" + row.id + ")'>"); 
        //cell.insertBefore(newElement); 
    }
    catch(err)
    {
        displayErrorMsg("sortAddBtnDelete", err);
    }

}

 //生成增加下一行按纽
function sortAddBtnAdd(row)
{
    try
    {
        var cell = row.insertCell(); 
        var addBtnId = "sortAdd" + row.id;
       $(cell).append("<INPUT id='" + addBtnId + "' name='" + addBtnId + "' type='Button' class='button' value='新增' onclick='javascript:orderByAddRow(" + row.id + ")'>"); 
        //cell.insertBefore(newElement); 
    }
    catch(err)
    {
        displayErrorMsg("sortAddBtnAdd", err);
    }

    
}

//添加排序条件编辑行 
function sortAddTerm(tbPanelId) 
{ 
    try
    {
        //添加一待填模板行 
        var row = sortAddTemplete(tbPanelId); 
          
        //添加字段栏
        sortAddField(row);
        
        //添加排序栏
        sortAddSortMode(row);
        
        //添加删除按钮
        sortAddBtnDelete(row);
        
        //添加新增按钮
        sortAddBtnAdd(row);
    }
    catch(err)
    {
        displayErrorMsg("sortAddTerm", err);
    }



} 

//取得并设置运算符 
function getOpts(row) 
{ 
    try
    {
        var rowId = row.id; 
         
        var fieldSelId = "field"+rowId; 
        var field = document.getElementById(fieldSelId); 
         
        var elemID = "opt"+rowId; 
        var element = document.getElementById(elemID); 
         
        var optInfo = UDEF.Service.AjaxService.GetOpts(field.value).value; 
        var optValue; 
        var j,index; 
     
        //先移除原有项 
        element.options.length = 0;
       
        for(j=0;j<optInfo.length;j++) 
        { 
            optValue = optInfo[j]; 
            index = optValue.indexOf("|"); 
            element.options[element.options.length] = new Option(optValue.substring(0,index),optValue.substring(index+1,optValue.length)); 
        } 
    }
    catch(err)
    {
        displayErrorMsg("getOpts", err);
    }
   
} 

//设置值录入控件,取得可能存在的枚举值 
function getInput(row) 
{ 
    try
    {
        var rowId = row.id; 
         
        var fieldSelId = "field"+rowId; 
        var field = document.getElementById(fieldSelId); 

        var inputId = "input"+rowId; 
        var inputElement = document.getElementById(inputId); 
         
        if (inputElement != null) 
        { 
            row.deleteCell(2); 
        } 
         
        //获取可能存在的枚举值 
        var enums = UDEF.Service.AjaxService.GetEnums(field.value).value; 
        var oCell; 
        if (enums.length <= 1)//不存在枚举值，使用text 
        { 
            var elementSyntax = "<INPUT ID='"+elemID+"' type='text'>"; 
            oCell = row.insertCell(2); 
            var elemID = "input"+rowId; 
            
            switch(enums[0]) 
            { 
                case "int": 
                    elementSyntax = "<INPUT ID='"+elemID+"' type='text' onKeyPress='javascript:return controlNumberKeyPress(this)' onfocus='this.select()' style='TEXT-ALIGN:right' onpaste='return !clipboardData.getData(\"text\").match(/\D/);'>" 
                    break; 
                case "decimal": 
                    elementSyntax = "<INPUT ID='"+elemID+"' type='text' onKeyPress='javascript:return controlNumberKeyPress(this)' onfocus='this.select()' style='TEXT-ALIGN:right' onpaste='return !clipboardData.getData(\"text\").match(/\D/);' onblur='javascript:controlNumberOnblur(this);'>" 
                    break; 
                case "datetime": 
                    elementSyntax = "<INPUT ID='"+elemID+"' type='text' onfocus='javascript:calendar();' onkeypress='return false' onselectstart='return false;' readonly='true' onpaste='return false;' >" 
                    break; 
                case "check":
                    elementSyntax = "<INPUT ID='"+elemID+"' type='checkbox'>" 
                    break;
                default: 
                    elementSyntax = "<INPUT ID='"+elemID+"' type='text'>" 
                    break; 
            }

            $(oCell).append(elementSyntax); 
            //oCell.insertBefore(oNewItem); 
        } 
        else //存在枚举值，使用select 
        { 
            oCell = row.insertCell(2); 
            var elemID = "input"+rowId;
            $(oCell).append("<SELECT id='" + elemID + "' name='" + elemID + "'></SELECT>");
            //oCell.insertBefore(oNewItem);
            var oNewItem = document.getElementById(elemID);
            var enumValue; 
            var j,index; 
            var element = document.getElementById(elemID); 
             
            for(j = 1; j < enums.length; j++) 
            { 
                enumValue = enums[j]; 
                index = enumValue.indexOf("|"); 
                element.options[element.options.length] = new Option(enumValue.substring(0,index),enumValue.substring(index+1,enumValue.length)); 
            } 
        } 
         
        //用于值的数据类型 
        $(oCell).append("<INPUT id='type" + rowId + "' name='type" + rowId + "' type='hidden'>"); 
        //oCell.insertBefore(fieldType); 
        document.getElementById("type"+rowId).value = enums[0]; 
    }
    catch(err)
    {
        displayErrorMsg("getInput", err);
    }
    
} 
 
//删除查询条件当前行 
function DelRow(row) 
{
    try
    {
        var rowOfIndex = row.rowIndex; 
        
        if(tbConditions.rows.length == 1) 
        { 
            return 
        } 
        else if (rowOfIndex == tbConditions.rows.length-1) 
        { 
            //删除最后一行时，保持增加按纽在最后一行 
            var cell = tbConditions.rows[rowOfIndex-1].insertCell(); 
            var addBtnId = "add"+tbConditions.rows[rowOfIndex-1].id;
            $(cell).append("<INPUT id='" + addBtnId + "' name='" + addBtnId + "' type='Button' class='button' value='新增' onclick='javascript:addRow(" + tbConditions.rows[rowOfIndex - 1].id + ")'>"); 
            //cell.insertBefore(newElement); 
            
             //删除最后一行时，保持最后一行的连接条件不可编辑
            if (tbConditions.rows.length > 2)
            {
                tbConditions.rows[rowOfIndex-1].cells[3].childNodes[0].style.display = "none";
            }
        } 
         
        tbConditions.deleteRow(rowOfIndex); 
    }
    catch(err)
    {
        displayErrorMsg("DelRow", err);
    } 
    
} 

//删除排序条件当前行 
function sortDelRow(row)
{
    try
    {
        var rowOfIndex = row.rowIndex; 
        
        if(tbSorts.rows.length == 1) 
        { 
            return 
        } 
        else if (rowOfIndex == tbSorts.rows.length-1) 
        { 
            //删除最后一行时，保持增加按纽在最后一行 
     
            var cell = tbSorts.rows[rowOfIndex-1].insertCell(); 
            var addBtnId = "sortAdd" + tbSorts.rows[rowOfIndex-1].id;
            $(cell).append("<INPUT id='" + addBtnId + "' name='" + addBtnId + "' type='Button' value='新增' class='button' onclick='javascript:orderByAddRow(" + tbSorts.rows[rowOfIndex - 1].id + ")'>"); 
            //cell.insertBefore(newElement); 
        } 
         
        tbSorts.deleteRow(rowOfIndex); 
    }
    catch(err)
    {
        displayErrorMsg("sortDelRow", err);
    }
    
}

//查询条件新增行 
function addRow(rowid) 
{
    try {

        var row = document.getElementById(rowid);
        row.deleteCell(5); 
        addTerm(tbConditions.id); 
        
        //令其前面的连接条件下拉框可选
        var tbLength = tbConditions.rows.length;
        if (tbLength >= 2)
        {
            if (tbLength > 2)
            {
                tbConditions.rows[tbLength-2].cells[3].childNodes[0].style.display = "";
            }
            tbConditions.rows[tbLength-1].cells[3].childNodes[0].style.display = "none";
        }
    }
    catch(err)
    {
        displayErrorMsg("addRow", err);
    } 

} 

//排序条件新增行
function orderByAddRow(row) 
{
    try
    {
        row.deleteCell(3); 
        sortAddTerm(tbSorts.id); 
    }
    catch(err)
    {
        displayErrorMsg("orderByAddRow", err);
    } 
    
} 


//验证查询条件合法性：为空表示无出错信息，否则返回出错信息
function validConditions()
{
    try
    {
        var lenOfRows = tbConditions.rows.length; 
        var i = 0; 
        var field = "", opt = "", inputValue = ""; 
        
        var meg = "";
        
        //验证查询条件的完整性
        for(i = 1; i < lenOfRows; i++) 
        { 
            field     = tbConditions.rows[i].cells[0].childNodes[0].value; 
            opt         = tbConditions.rows[i].cells[1].childNodes[0].value; 
            inputValue  = tbConditions.rows[i].cells[2].childNodes[0].value; 
            if (field == "" || field == null || opt == "" || opt == null || inputValue == "" || inputValue == null)
            {
                meg += "第" + i + "行查询条件没有填完整！\n";
            }
            
        } 
        
        return meg;  
    }
    catch(err)
    {
        displayErrorMsg("validConditions", err);
    }
}

//获得视图的所有查询条件信息，以实体形式返回
function getCondtionItems()
{
    try
    {
        var conditionItems = new Array();
        
        var lenOfRows = tbConditions.rows.length; 
        var fieldID = "", opt="", inputValue="", jointMode="";
        var inputObj;
        
        for(var i = 1; i < lenOfRows; i++) 
        { 
            var deCondition = new Object();
            
            fieldID = tbConditions.rows[i].cells[0].childNodes[0].value; 
            opt  = tbConditions.rows[i].cells[1].childNodes[0].value; 
            inputObj  = tbConditions.rows[i].cells[2].childNodes[0]; 
            inputValue = inputObj.type == "checkbox" ? inputObj.checked ? "1" : "0" : inputObj.value;
            //0表示并且，1表示或者
            jointMode    = tbConditions.rows[i].cells[3].childNodes[0].selectedIndex;
            
            deCondition.ColumnID = fieldID;
            deCondition.ExpressionID = opt;
            deCondition.Connector = jointMode;
            deCondition.ConditionValue = inputValue;
            
            conditionItems[i-1] = deCondition;
        }  
        
        return  conditionItems;
    }
    catch(err)
    {
        displayErrorMsg("getCondtionItems", err);
    }

}

//获得视图的所有排序信息，以实体集形式返回
function getSortItems()
{
    try
    {
        var sortItems = new Array();
        
        var lenOfRows = tbSorts.rows.length; 
        var fieldID = "", sortMode = ""; 
        
        for(var i = 1; i < lenOfRows; i++) 
        { 
            var deSort = new Object();
            deSort.ColumnID = tbSorts.rows[i].cells[0].childNodes[0].value; 
            //0表示升序，1表示降序
            sortMode = tbSorts.rows[i].cells[1].childNodes[0].selectedIndex; 
            deSort.IsDesc = sortMode == 1; 
            
            sortItems[i-1] = deSort;
        } 
        
        return sortItems; 
    }
    catch(err)
    {
        displayErrorMsg("getSortItems", err);
    }
 
}

//获得所有视图列，以实体集形式返回
function getOrderItems() 
{
    try
    {
        var orderItems = new Array();
        
        var targetSelect = document.getElementById("lbOrder");
        var totalNum = targetSelect.options.length;
        
        for (var i = 0; i < totalNum; i++)
        {
            var deOrder = new Object();
            deOrder.ColumnID = targetSelect.options[i].value;
            
            orderItems[i] = deOrder;
        }
        
        return orderItems;
    }
    catch(err)
    {
        displayErrorMsg("getOrderItems", err);
    }

}

//以数组形式返回列表框sourceSelect中的选择项
function privateGetSelectedOptions(sourceSelect)
{
     //列表框的选择
    var options = sourceSelect.options;

    //存储所有已选择的列表项
    var selectedIds = new Array ();
    
    //获得所有选择的选项
    var index = 0;
    for (var i = 0; i < sourceSelect.length; i++) 
    {
        if (options[i].selected) 
        {
            selectedIds[index] = i;
            index++;
        }
    }
    
    return selectedIds;

}

//顶部按钮事件处理
function topSelected() 
{
    try
    {
        var sourceSelect = document.getElementById("lbOrder");
        
        if (sourceSelect.length > 1) 
        {
            var options = sourceSelect.options;

            var selectedInds = privateGetSelectedOptions(sourceSelect);

            for (var n = 0; n < selectedInds.length; n++)
            {
                var index = selectedInds[n];
                privateMoveFromDownToUp(options, n, index);
            }
            
            for (var i = 0; i < sourceSelect.options.length; i++)
            {
                sourceSelect.options[i].selected = false;
            }
            for (var j = 0; j < selectedInds.length; j++)
            {
                options[j].selected = true;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("topSelected", err);
    }
}

//把选择位置为downIndex的选项排到upIndex位置，其前面至upIndex的向下移动一位
function privateMoveFromDownToUp(options, upIndex, downIndex)
{
    var newOption = new Option (options[downIndex].text, options[downIndex].value);
    for (var i = downIndex - 1; i >= upIndex; i--)
    {
        var option = new Option (options[i].text, options[i].value);
        options[i+1].text = option.text;
        options[i+1].value = option.value;
    }
    options[upIndex] = newOption;
}

//向上按钮事件处理
function upSelected() 
{
    try
    {
        //要进行向下处理的列表框
        var sourceSelect = document.getElementById("lbOrder");
        
        if (sourceSelect.length > 1) 
        {
            //列表框的选择
            var options = sourceSelect.options;

            //存储所有已选择的列表项
            var selectedIds = new Array ();
            var index = 0;
            
            //获得所有需要向上移动的选项(第一项不需要向上移动）
            for (var i = 1; i < sourceSelect.length; i++) 
            {
                if (options[i].selected) 
                {
                    selectedIds[index] = i;
                    index++;
                }
            }

            // 所有选择列向上移动
            var selId;
            for (var i = 0; i < selectedIds.length; i++) 
            {
                selId = selectedIds[i];
                privateMoveUp (options, selId);
                options[selId].selected = false;
                options[selId-1].selected = true;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("upSelected", err);
    }
    
}

//位置为index的选择跟其前面的互换
function privateMoveUp (options, index) 
{
    try
    {
        var newOption = new Option (options[index-1].text, options[index-1].value);
        options[index-1].text = options[index].text;
        options[index-1].value = options[index].value;
        options[index].text = newOption.text;
        options[index].value = newOption.value;
    }
    catch(err)
    {
        displayErrorMsg("privateMoveUp", err);
    }
    
}


//向下按钮事件处理
function downSelected() 
{
    try
    {
        //需要处理的列表框
        var sourceSelect = document.getElementById("lbOrder");
        
        if (sourceSelect.length > 1) 
        {
            var options = sourceSelect.options;

            // find which ones are selected
            var selectedIds = new Array ();
            var index = 0;
 
            //获得需要向下移动的选项(最后一项不需要向下移动了）
            for (var i = sourceSelect.length - 2; i >= 0; i--) 
            {
                if (sourceSelect.options[i].selected) 
                {
                    selectedIds[index] = i;
                    index++;
                }
            }
            var selId;
            for (var i = 0; i < selectedIds.length; i++) 
            {
                selId = selectedIds[i];
                privateMoveDown (options, selId);
                options[selId].selected = false;
                options[selId+1].selected = true;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("downSelected", err);
    }

}

//位置为index的选择跟其下的互换
function privateMoveDown (options, index) 
{
    try
    {
        var newOption = new Option (options[index+1].text, options[index+1].value);
        options[index+1].text = options[index].text;
        options[index+1].value = options[index].value;
        options[index].text = newOption.text;
        options[index].value = newOption.value;
    }
    catch(err)
    {
        displayErrorMsg("privateMoveDown", err);
    }
    
}



//底部按钮事件处理
function bottomSelected() 
{
    try
    {
        var sourceSelect = document.getElementById("lbOrder");
        
        if (sourceSelect.length > 1) 
        {
            var options = sourceSelect.options;
            var optionLength = options.length;

            var selectedInds = privateGetSelectedOptions(sourceSelect);
            var current = 0;
            for (var n = selectedInds.length - 1; n >= 0; n--)
            {
                var index = selectedInds[n];
                privateMoveFromUpToDown(options, index, optionLength - current - 1);
                current++;
            }
            
            //所有选项先全变为未选择
            for (var i = 0; i < sourceSelect.options.length; i++)
            {
                sourceSelect.options[i].selected = false;
            }
            
            //把已经调整好的选择项变为已选择
            for (var j = 0; j < selectedInds.length; j++)
            {
                options[optionLength - j - 1].selected = true;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("bottomSelected", err);
    }
}

//把选择位置为upIndex的选项排到downIndex位置，其后面至upIndex的向上移动一位
function privateMoveFromUpToDown(options, upIndex, downIndex)
{
    var newOption = new Option (options[upIndex].text, options[upIndex].value);
    for (var i = upIndex; i < downIndex; i++)
    {
        var option = new Option (options[i+1].text, options[i+1].value);
        options[i].text = option.text;
        options[i].value = option.value;
    }
    options[downIndex] = newOption;
}

//添加按钮事件处理
function addSelected() 
{
    try
    {
        var sourceSelect = document.getElementById("lbColumn");
        var targetSelect = document.getElementById("lbOrder");
        var totalNum = targetSelect.options.length;
        //把选择的字段加入已选数组
        while (sourceSelect.selectedIndex > -1)
        {   
            var totalNum = targetSelect.options.length;
//            if (totalNum >= 30)
//            {
//                alert('已经超过最大限定列数30，请重新调整！');
//                return;
//            }
            var index = sourceSelect.selectedIndex;
            var columnID = sourceSelect.value;
            var headerText = sourceSelect[sourceSelect.selectedIndex].text;
            columnIDsSelected[columnID] = headerText;
            moveOption(sourceSelect, targetSelect, index);
            

        }
    }
    catch(err)
    {
        displayErrorMsg("addSelected", err);
    }
    
}

//从源列表框选择位置为index的项添加到目标列表框
//并把源列表框中的index删除
function moveOption(sourceSelect, desSelect, index)
{
    var option = new Option (sourceSelect.options[index].text, sourceSelect.options[index].value);
    var desLength = desSelect.options.length;
    desSelect.options[desLength] = option;
    sourceSelect.remove(index);
}

//删除事件处理
function deleteSelected() 
{
    try
    {
        var sourceSelect = document.getElementById("lbColumn");
        var targetSelect = document.getElementById("lbOrder");
        
        while (targetSelect.selectedIndex > -1)
        {
            var selectedIndex = targetSelect.selectedIndex;
            var columnID = targetSelect.value;
            //选中的字段是当前表的字段时的处理
            if (currentTableID == columnID_TableID[columnID])
            {
               moveOption(targetSelect, sourceSelect, selectedIndex);
            }
            else    //删除当前项
            {
                targetSelect.options[selectedIndex] = null;
            }
            columnIDsSelected[columnID] = null;
        }
    }
    catch(err)
    {
        displayErrorMsg("deleteSelected", err);
    }
    
}
//功能：将相应表的所有字段导入
function handleTableChange(selObj)
{
    try
    {
        currentTableID = selObj.value; 
        var fields = new Array(); 
        fields = UDEF.Service.AjaxService.SelectColumnByTableID(currentTableID).value;
        var sourceSelect = document.getElementById("lbColumn");
        sourceSelect.options.length = 0;
        for (var i = 0; i < fields.length; i++)
        {
            var columnID = fields[i].ColumnID;
            if (columnIDsSelected[columnID] == null)
            {
                var selLength = sourceSelect.options.length;
                sourceSelect.options[selLength] = new Option(fields[i].ColumnText, columnID);
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("handleTableChange", err);
    }
    
}

//功能：把字段跟相应的table(包括本身的和外键的表)关联起来
//primaryKeyTableID：主表的ID
//foreignKeyTables：外键表数组
function setColumnID_tableIDRelation(primaryKeyTableID, foreignKeyTables)
{
    try
    {
        var fields = new Array();
        var tableID = primaryKeyTableID;
        fields = UDEF.Service.AjaxService.SelectColumnByTableID(tableID).value;
        //处理主表字段
        
        for (var i = 0; i < fields.length; i++)
        {
            columnID_TableID[fields[i].ColumnID] = tableID;
        }

        for (var i = 0; i < foreignKeyTables.length; i++)
        {
            tableID = foreignKeyTables[i].TableID;

            fields = UDEF.Service.AjaxService.SelectColumnByTableID(tableID).value; 
            for (var j = 0; j < fields.length; j++)
            {
                columnID_TableID[fields[j].ColumnID] = fields[j].TableID;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("setColumnID_tableIDRelation", err);
    }
    
}

//功能：将ID为tableID的表的所有外键表添加到下拉框中,并设置好字段与表间的关系
function addFieldSourceTable(tableID, selObj)
{
    try
    {
        var foreignKeyTables = new Array();
        foreignKeyTables =  UDEF.Service.AjaxService.SelectForeignKeyTableByTableID(tableID).value;
        for (var i = 0; i < foreignKeyTables.length; i++)
        {
            var sysTable = foreignKeyTables[i];
            selObj.options[i+1] = new Option(sysTable.TableText, sysTable.TableID);
        }
        selObj.options[0].value = tableID;
        
        //设置好映射关系
        setColumnID_tableIDRelation(tableID, foreignKeyTables);
        
        currentTableID = tableID;
    }
    catch(err)
    {
        displayErrorMsg("addFieldSourceTable", err);
    }
    
}

//初始化视图的list字段
//orderItems：视图列表项集合
function initViewPageOrder(orderItems)
{
    try
    {
        var targetSelect = document.getElementById("lbOrder");
        targetSelect.options.length = 0;

        for (var i = 0; i < orderItems.length; i++)
        {
            var orderItem = orderItems[i];
            var columnID = orderItem.ID;
            var headerText = orderItem.Name;
            targetSelect.options[i] = new Option(headerText, columnID);  
            columnIDsSelected[columnID] =  headerText; 
        }   
    }
    catch(err)
    {
        displayErrorMsg("initViewPageOrder", err);
    }

}




//验证填写合法性,返回布尔值
function validAll()
{
    try
    {
        var strFieldItems = getOrderItems();
        var strViewName = document.getElementById("txtViewName").value;
        var strValidCondtions = validConditions();
        var meg = "";
        var bSuccess = true;
        if (strViewName == "" || strViewName == null)
        {
            meg += "视图名称不能为空！\n";
        }
        if (strFieldItems == "" || strFieldItems == null)
        {
            meg += "字段栏选择不能为空！\n";
        }
        meg += strValidCondtions;
        
        if (meg != "")
        {
            alert(meg);
            bSuccess = false;
        }
        
        return bSuccess;
    }
    catch(err)
    {
        displayErrorMsg("validAll", err);
    }
 
}

function Save()
{
    try
    {
        var tableID = getUrlParameter("TableID");
        var viewID = getUrlParameter("ViewID");
        var editMode = getUrlParameter("EditMode");
        var strViewName = document.getElementById("txtViewName").value;
        
        if (validAll())
        {   
            var columnView = new Object();          //视图自身信息
            var orderItems = getOrderItems();       //视图列信息
            var sortItems = getSortItems();         //视图排序信息
            var conditionItems = getCondtionItems();//视图条件信息
            
            if (editMode == "add" || editMode == "copy")
            {
                columnView.ViewName = strViewName;
                columnView.TableID = tableID;
                
                UDEF.Service.AjaxService.InsertColumnViewAll(columnView, orderItems, conditionItems, sortItems, Finish_CallBack);
            }
            else
            {
                
                columnView.ViewName = strViewName;
                columnView.ViewID = viewID;
                
                UDEF.Service.AjaxService.UpdateColumnViewAll(columnView, orderItems, conditionItems, sortItems, Finish_CallBack);
            }
             
        }
    }
    catch(err)
    {
        displayErrorMsg("Save", err);
    }

}

function Finish_CallBack(response)
{
    try
    {
	    if(response.error != null)
	    {
		    alert(response.error);
		    return;
	    }
	    else
	    {
		    alert("操作成功！");
		    GoBackUrl(tableName);
	    }
    }
    catch(err)
    {
	    displayErrorMsg("Finish_CallBack", err);  
    }
}

function OnLoadEvent(tableID)
{
    try
    {
        var editMode = getUrlParameter("EditMode");
        var viewID = editMode == "add" ? UDEF.Service.AjaxService.GetstandardViewID(tableID).value : getUrlParameter("ViewID");
        var selObj = document.getElementById("ddlTable");
        initViewPage(tableID, viewID, selObj);
        if (editMode == "add")
        {
            var txtViewName = document.getElementById("txtViewName");
            txtViewName.value = "视图名称";
            txtViewName.select();   
        }
    }
    catch(err)
    {
        displayErrorMsg("OnLoadEvent", err);  
    } 
}
    
function GoBackUrl(TableID)
{
    if (getUrlParameter("GoBackURL") != "" && getUrlParameter("GoBackURL")!= null)
    {
        window.opener.location.reload();
        window.close();
//        window.location = getUrlParameter("GoBackURL").replace("#", "?").replace(/\*/g, "&").replace(/\|/g, "=");
    }
    else
    {
        window.location = "ListColumnView.aspx?TableID=" + TableID;
    }
}