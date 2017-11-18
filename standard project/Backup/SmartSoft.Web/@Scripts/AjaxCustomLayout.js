/*****************************

编辑用户自定义布局的页面中使用

*****************************/
var imagePrePath = '../@images/';
var lastMouseX;
var lastMouseY;
var curPopupWindow = null;
var isIE = navigator.appName.indexOf("Microsoft") != -1;
var isIE5 = navigator.userAgent.indexOf('MSIE 5.0') > 0;
var isIE55 = navigator.userAgent.indexOf('MSIE 5.5') > 0;
var isSelfService = '0';
var isCaseClose = '0';
var openedWindow = null;
var currentSelectedObj = null;
var selectedBucket = new Array();
var mousedDown = false;
var currentOffsetX = -1;
var currentOffsetY = -1;
var currentHighlightedObj = null;
var initUsedFields = new Array();
var maxSection = 0;
var maxSectionTable = 0;
var currentDisplayedDiv = null;
var availableSectionInitPosX;
var availableSectionInitPosY;
var availableSectionPosInited = false;
var cru;    
var displayProperties = false;
var maxValues = 10;
document.onmousemove = handleMouseMove;
if (!isIE5) document.onmouseup = handleMouseUp;


//数据类型关联数组
var dataType;

//关联数组，用于columnID到tableID间的映射，showProperties时用到
var columnID_TableID = new Array();     

//关联数组，用于tableID到TableName间的映射
var tableID_TableName = new Array();

//获得对象的X坐标值
function getObjX(obj)
{
    try
    {
        if(!obj.offsetParent) return 0;
        var x = getObjX(obj.offsetParent);
        return obj.offsetLeft + x;
    }
    catch(err)
    {
        displayErrorMsg("getObjX", err);
    }
}

//获得对象的Y坐标值
function getObjY(obj)
{
    try
    {
        if(!obj.offsetParent) return 0;
        var y = getObjY(obj.offsetParent);
        return obj.offsetTop + y;
    }
    catch(err)
    {
        displayErrorMsg("getObjY", err);
    }
}

function getScrollX()
{
    try
    {
        if (window.pageXOffset) return window.pageXOffset;
        if (document.body.scrollHeight) return document.body.scrollLeft;
    }
    catch(err)
    {
        displayErrorMsg("getScrollX", err);
    }
}

function getScrollY()
{
    try
    {
        if (window.pageYOffset) return window.pageYOffset;
        if (document.body.scrollWidth) return document.body.scrollTop;
    }
    catch(err)
    {
        displayErrorMsg("getScrollY", err);
    }
}

function getMouseX(evt) 
{
    try
    {
        if (evt.pageX) return evt.pageX;
        obj = getSrcElement(evt);
        return getScrollX() + evt.x;
    }
    catch(err)
    {
        displayErrorMsg("getMouseX", err);
    }
}

function getMouseY(evt)
{
    try
    {
        if (evt.pageY) return evt.pageY;
        return getScrollY() + evt.y;
    }
    catch(err)
    {
        displayErrorMsg("getMouseY", err);
    }   
}

function setLastMousePosition(e) 
{
    try
    {
        if (navigator.appName.indexOf("Microsoft") != -1) e = window.event;
        lastMouseX = e.screenX;
        lastMouseY = e.screenY;
    }
    catch(err)
    {
        displayErrorMsg("setLastMousePosition", err);
    }   
}

function openPopup(url, name, pWidth, pHeight, features, snapToLastMousePosition) 
{
    try
    {
        openPopupFocus (url, name, pWidth, pHeight, features, snapToLastMousePosition, true);
    }
    catch(err)
    {
        displayErrorMsg("openPopup", err);
    }
}

function openPopupFocus(url, name, pWidth, pHeight, features, snapToLastMousePosition, closeOnLoseFocus) 
{
    try
    {
        closePopup();

        if (snapToLastMousePosition) 
        {
            if (lastMouseX - pWidth < 0) 
            {
                lastMouseX = pWidth;
            }
            if (lastMouseY + pHeight > screen.height) 
            {
                lastMouseY -= (lastMouseY + pHeight + 50) - screen.height;
            }
            lastMouseX -= pWidth;
            lastMouseY += 10;
            features += ",screenX=" + lastMouseX + ",left=" + lastMouseX + ",screenY=" + lastMouseY + ",top=" + lastMouseY;
        }

        if (closeOnLoseFocus) 
        {
            curPopupWindow = window.open(url, name, features, false);
            curPopupWindow.focus ();
        } else 
        {
            // assign the open window to a dummy var so when closePopup() is called it won't be assigned to curPopupWindow
            win = window.open(url, name, features, false);
            win.focus ();
        }
    }
    catch(err)
    {
        displayErrorMsg("openPopupFocus", err);
    }
}

function closePopup() 
{
    try
    {
        if (curPopupWindow != null) 
        {
            if (!curPopupWindow.closed) 
            {
                curPopupWindow.close();
            }
            curPopupWindow = null;
        }
    }
    catch(err)
    {
        displayErrorMsg("closePopup", err);
    }
}

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

//功能：获得不同IE版本的触发事件的元素
//evt：事件处理源
function getSrcElement(evt) 
{
    try
    {
        if (isIE) return evt.srcElement;
        return evt.currentTarget;
    }
    catch(err)
    {
        displayErrorMsg("getSrcElement", err);
    }   
}

//功能：鼠标按下处理事件
//evt：事件处理源
function handleMouseDown(evt) 
{
    try
    {
        if (isIE5) return;
        clearTextSelection();
        evt = getEvent(evt);
        obj = getSrcElement(evt);
        if (!evt.shiftKey && !evt.ctrlKey && !isSelected(obj)) 
        {
            clearSelected();
            setSelected(obj);
            if (!currentSelectedObj) return;
        }
        mousedDown = true;
    }
    catch(err)
    {
        displayErrorMsg("handleMouseDown", err);
    }
    
}

//功能：清除所有选择的元素的选择状态
function clearSelected() 
{
    try
    {
        for (var i = 0; i < selectedBucket.length; i++) 
        {
            if (!selectedBucket[i].used || selectedBucket[i].used == false) 
            {
                selectedBucket[i].style.backgroundColor = selectedBucket[i].originalBGColor;
            }
        }
        selectedBucket = new Array();
        currentSelectedObj = null;
    }
    catch(err)
    {
        displayErrorMsg("clearSelected", err);
    }
}

//功能：恢复对象的非选择状态即正常状态
//obj：被操作对象
function removeSelected(obj) 
{
    try
    {
        for (var i = 0; i < selectedBucket.length; i++) 
        {
            if (selectedBucket[i].id == obj.id) 
            {
                selectedBucket.splice(i, 1);
                obj.style.backgroundColor = obj.originalBGColor;
                defaultCursor(obj);
            }
        }
        if (currentSelectedObj == obj) 
        {
            currentSelectedObj = null;
            if (selectedBucket.length > 0) 
            {
                currentSelectedObj = selectedBucket[selectedBucket.length-1];
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("removeSelected", err);
    }   
}

//功能：设置对象的选择状态
//obj：被操作对象
function setSelected(obj) 
{
    try
    {
        if (isSelected(obj)) 
        {
            removeSelected(obj);
            return;
        }
        
        currentSelectedObj = obj;
        obj.originalBGColor = obj.style.backgroundColor;
        obj.style.backgroundColor = '#6699cc';
        moveCursor(obj);
        if (isIE5) 
        {
            selectedBucket = selectedBucket.concat(currentSelectedObj);
        } else 
        {
            selectedBucket.push(currentSelectedObj);
        }
    }
    catch(err)
    {
        displayErrorMsg("setSelected", err);
    }   
}

//功能：多项选择事件处理
//obj：触发事件的对象
function multiSelect(obj) 
{
    try
    {
        if (!currentSelectedObj) return;
        var tableId = getTableId(obj);
        
        if (getTableId(currentSelectedObj) != tableId ) 
        {
            removeSelected(currentSelectedObj);
            setSelected(obj);
            return;
        }
        
        var prevSelectedObj = currentSelectedObj;
        var rowNum1 = getRowNum(currentSelectedObj);
        var rowNum2 = getRowNum(obj);
        var colNum1 = getColNum(currentSelectedObj);
        var colNum2 = getColNum(obj);
        var startRow = Math.min(rowNum1, rowNum2);
        var endRow =  Math.max(rowNum1, rowNum2);
        var startCol =  Math.min(colNum1, colNum2);
        var endCol =  Math.max(colNum1, colNum2);
        if (startRow == endRow && startCol == endCol) return;
        clearSelected();
        for (var i = startRow; i <= endRow; i++ ) 
        {
            for (var j = startCol; j <= endCol; j++) 
            {
                var cellId = constructId(tableId, i, j);
                var cell = document.getElementById(cellId);
                
                if (!cell.used && cell.innerHTML != '') 
                {
                    setSelected(cell);
                }
            }
        }
        currentSelectedObj = prevSelectedObj;
    }
    catch(err)
    {
        displayErrorMsg("multiSelect", err);
    }
}

//功能：判断是否为已选择的对象
//obj：被操作对象
//返回值：bool 值，是已选择对象则返回true,否则false
function isSelected(obj) 
{
    try
    {
        for (var i = 0; i < selectedBucket.length; i++) 
        {
            if (selectedBucket[i].id == obj.id) return true;
        }
        return false;
    }
    catch(err)
    {
        displayErrorMsg("isSelected", err);
    }    
}

//功能：鼠标单击处理事件
//evt：事件处理源
function handleMouseClick(evt) 
{
    try
    {
        clearTextSelection();
        evt = getEvent(evt);
        if (isIE5) 
        {
            if (!currentSelectedObj || evt.ctrlKey) 
            {
                obj = getSrcElement(evt);
                if (obj.used) return;
                if (obj.innerHTML == '') return;
                setSelected(obj);
                mousedDown = true;
            }
            else if (evt.shiftKey) 
            {
                obj = getSrcElement(evt);
                if (obj.used) return;
                if (obj.innerHTML == '') return;
                multiSelect(obj);
            }
            else 
            {
                handleMouseUp(evt);
            }
        }
        else if (evt.ctrlKey) 
        {
            setSelected(obj);
        } 
        else if (evt.shiftKey) 
        {
            multiSelect(obj);
        }    
    }
    catch(err)
    {
        displayErrorMsg("handleMouseClick", err);
    }
}

//功能：鼠标释放处理事件
//evt：事件处理源
function handleMouseUp(evt) 
{
    try
    {
        clearTextSelection();
        evt = getEvent(evt);
        if (!mousedDown && !evt.ctrlKey && !evt.shiftKey) 
        {
            clearSelected();
            return;
        }
        evt = getEvent(evt);
        document.getElementById('dragDummy').style.visibility = 'hidden';
        if (currentSelectedObj) 
        {
            if (currentHighlightedObj) 
            {
                if (isSection(currentSelectedObj)) 
                {
                    swapSections(evt);
                    handleDragOut(currentHighlightedObj);
                    currentHighlightedObj = null;
                    clearSelected();
                } 
                else 
                {
                    
                    if (!(isInAvailableSection(currentSelectedObj) && isInAvailableSection(currentHighlightedObj))) 
                    {
                        insertCell(evt);
                    }
                    handleDragOut(currentHighlightedObj);
                    currentHighlightedObj = null;
                    clearSelected();
                }
            }
            if (evt.ctrlKey) 
            {
                mousedDown = false;
                return;
            }
        }
        mousedDown = false;
    }
    catch(err)
    {
        displayErrorMsg("handleMouseUp", err);
    }
    
    
}
  
//功能：鼠标移动处理事件
//evt：事件处理源 
function handleMouseMove(evt) 
{
    try
    {
        evt = getEvent(evt);
        if (currentSelectedObj && mousedDown) 
        {
            clearTextSelection();

            var obj = getSrcElement(evt);
            
            var scrollX = getScrollX();
            var scrollY = getScrollY();
            var dragDummy = document.getElementById('dragDummy');
            var dragDummyValue = document.getElementById('dragDummyValue');
            dragDummy.style.visibility = 'visible';
            dragDummyValue.innerHTML = selectedBucket.length > 1 ? '多选' : isSection(currentSelectedObj) ? '部分' : currentSelectedObj.fName;
            dragDummy.style.left = getMouseX(evt) - currentOffsetX;
            dragDummy.style.top = getMouseY(evt) - currentOffsetY;

            var currentX = getObjX(dragDummy) - scrollX;
            if (currentX > document.body.clientWidth) 
            {
                if (isIE) document.body.scrollLeft = document.body.scrollLeft + 10;
                //else window.scroll(10, 0);
            } else if (currentX < 0) 
            {
                if (isIE) document.body.scrollLeft = document.body.scrollLeft - 10;
                //else window.scroll(-10, 0);
            }
            
            var currentY = getObjY(dragDummy) - scrollY;
            if (currentY > document.body.clientHeight - 50) 
            {
                if (isIE) document.body.scrollTop = document.body.scrollTop + 50;
                //else window.scroll(0, 50);
            } 
            else if (currentY < 50) 
            {
                if (isIE) document.body.scrollTop = document.body.scrollTop - 50;
                //else window.scroll(0, -50);
            }
            
        }
    }
    catch(err)
    {
        displayErrorMsg("handleMouseMove", err);
    }  
}

//功能：鼠标经过处理事件
//evt：事件处理源
function handleMouseOver(evt) 
{
    try
    {
        evt = getEvent(evt);
        obj = getSrcElement(evt);
        
        window.status = obj.style.zIndex;
        showProperties(evt);
        if (currentSelectedObj && mousedDown) 
        {
            moveCursor(obj);
            if (isSection(currentSelectedObj)) 
            {
                if (obj != currentHighlightedObj && isSection(obj)) 
                {
                    if (currentHighlightedObj) handleDragOut(currentHighlightedObj);
                    currentHighlightedObj = obj;
                    var separator = document.getElementById(getSeparatorId(obj.tableId));
                    if (separator) 
                    {
                        separator.originalBGColor = separator.style.backgroundColor;
                        separator.style.backgroundColor = '000000';
                    }
                }
            } 
            else 
            {
                if (obj != currentHighlightedObj) 
                {
                    if (currentHighlightedObj) handleDragOut(currentHighlightedObj);
                    currentHighlightedObj = obj;
                    if (isInAvailableSection(obj)) 
                    {
                        if (!isInAvailableSection(currentSelectedObj)) 
                        {
                            var t = getTable(getTableId(obj));
                            t.style.backgroundColor = '000000';
                        }
                    } 
                    else 
                    {
                       
                        var separator = document.getElementById(getSeparatorId(obj.id));
                        if (separator) 
                        {
                            separator.originalBGColor = separator.style.backgroundColor;
                            separator.style.backgroundColor = '000000';
                        }
                    }
                }
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("handleMouseOver", err);
    }
}

//功能：鼠标拖动处理事件
//obj：触发事件的对象
function handleDragOut(obj) 
{
    try
    {
        if (currentSelectedObj) 
        {
            if (isInAvailableSection(obj)) 
            {
                var t = getTable(getTableId(obj));
                t.style.backgroundColor = 'FFFFFF';
            } 
            else 
            {
                var separatorId = isSection(obj) ? obj.tableId : obj.id;
                separatorId = getSeparatorId(separatorId)
                var separator = document.getElementById(separatorId); 
                if (separator) 
                {
                    separator.style.backgroundColor = separator.originalBGColor;
                }
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("handleDragOut", err);
    }
}

//功能：根据对象性质改变鼠标样式
//obj：被操作对象
function pointerCursor(obj) 
{
    try
    {
        if (!obj || !obj.style) return;
        if (obj.used) {
            if (isIE && !isIE5 && !isIE55) 
            {
                obj.style.cursor = 'not-allowed';
            } 
            else 
            {
                obj.style.cursor = 'default';
            }
        } 
        else if (isSelected(obj)) 
        {
            moveCursor(obj);
        } 
        else 
        {
            if (!isIE5 && !isIE55) 
            {
                obj.style.cursor = 'pointer';
            } 
            else 
            {
                obj.style.cursor = 'default';
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("pointerCursor", err);
    }
}

//功能：改变对象的鼠标样式为move
//obj：被操作对象
function moveCursor(obj) 
{
    try
    {
        if (obj && obj.style && !isIE55 && !isIE5) 
        {
            obj.style.cursor = 'move';  
        }
    }
    catch(err)
    {
        displayErrorMsg("moveCursor", err);
    }
}

//功能：改变对象的鼠标样式为default
//obj：被操作对象
function defaultCursor(obj) 
{
    try
    {
        if (obj && obj.style) 
        {
            obj.style.cursor = 'default';
        }
    }
    catch(err)
    {
        displayErrorMsg("defaultCursor", err);
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

//功能：显示触发事件的对象的属性(部分工具提示)
//evt：事件处理源
function showProperties(evt) 
{
    try
    {
        obj = getSrcElement(evt);
        if (obj.innerHTML == '') return;
        
        pointerCursor(obj);
        
        var propHeader;
        var propValues;
        if (isSection(obj)) 
        {
            obj.onmouseout = handleMouseOut;
            var t = document.getElementById(getTableIdFromSectionId(obj.id));
            propHeader = obj.innerHTML;
            var r = 1;
            if (t != null) {
                r = t.rows[0].cells.length;
            }
            propValues = new Array(
            '要移动此部分，请单击此处并将它拖放到所需的新位置。',
                '列：&nbsp;&nbsp;' + r,
                '排序：&nbsp;&nbsp;' + (obj.sortOrder == 'h'?'左右':'上下')
            );
            if (obj.detailHeading == '1' && obj.editHeading == '1') 
            {
                propValues = propValues.concat('部分标题：&nbsp;&nbsp;' + '显示');
            } 
            else if (obj.detailHeading == '1') 
            {
                propValues = propValues.concat('部分标题：&nbsp;&nbsp;' + '仅详细信息页');
            } 
            else if (obj.editHeading == '1') 
            {
                propValues = propValues.concat('部分标题：&nbsp;&nbsp;' + '仅编辑页');
            }
        } 
        else 
        {  
            var isRQ = obj.rq && obj.rq == '1';
            var isRO = obj.ro && obj.ro == '1';
            
            //这里加入表名
            propHeader = obj.fName;
            var tableName = tableID_TableName[columnID_TableID[obj.itemId]];
            propHeader += "(" + tableName + ")";
            propValues = new Array();

            if (obj.used) 
            {
                   return;
            }

            propValues = propValues.concat('类型：&nbsp;&nbsp;' + obj.fDataLabel);
      
            if (isRQ || isRO) 
            {
                propValues = propValues.concat('安全性：&nbsp;&nbsp;' + (isRQ ? '必填项':isRO ? '只读' : '可编辑'));
            }
            if (obj.custom == '1') 
            {
          	    propValues = propValues.concat('可自定义：&nbsp;&nbsp;' + '是');
            }
           
            
            if (obj.columns) 
            {
        	    var size = obj.columns.length;
        	    propValues = propValues.concat('列：');
        	    for (var i = 0; i < size; i++) 
        	    {
                    propValues = propValues.concat('&nbsp;&nbsp;' + obj.columns[i].label);
        	    }
        	    for (var i = 0; i < size; i++) 
        	    {
        		    var sortOrder = obj.columns[i].sorting;
        		    if (sortOrder != 0) 
        		    {
        			    // TODO: i18n problems with concatenation
        			    var str = sortOrder == 1 ? '':'(D)';
	            	    propValues = propValues.concat('排序：&nbsp;&nbsp;' + obj.columns[i].label + str);
				    }
			    }
            }

            if (obj.fLength) 
            {
                propValues = propValues.concat('长度：&nbsp;&nbsp;' + obj.fLength);
            }

            if (obj.fPrecision) 
            {
                propValues = propValues.concat('长度：&nbsp;&nbsp;' + obj.fPrecision);
            }

            if (obj.fScale) 
            {
                propValues = propValues.concat('小数位数：&nbsp;&nbsp;' + obj.fScale);
            }

            if (isInAvailableSection(obj)) 
            {
                propValues = propValues.concat('<br>');
                if (obj.fieldType == 'F' || obj.fieldType == 'C') 
                {
                    propValues = propValues.concat('要将此字段添加到页面布局中，请将它拖放到字段部分。');
                } 
                else if (obj.fieldType == 'K') 
                {
                    propValues = propValues.concat('要将此自定义链接添加到页面布局中，请将它拖放到 ' + getLinksSectionName('自定义链接') +' 部分。');
                } 
                else 
                {
                    propValues = propValues.concat('要将此相关列表添加到页面布局中，请将它拖放到相关列表部分。');
                }

            }
                    
        }
        showMouseOver(evt, propHeader, propValues, 1000);
    }
    catch(err)
    {
        displayErrorMsg("showProperties", err);
    }
}


//功能：鼠标移出事件处理
//evt：事件处理源
function handleMouseOut(evt) 
{
    try
    {
        hideMouseOver(0);
    }
    catch(err)
    {
        displayErrorMsg("handleMouseOut", err);
    }
}

//功能：鼠标经过link对象的处理事件
//evt：事件处理源
function handleLinkMouseOver(evt) 
{
    try
    {
        evt = getEvent(evt);
        obj = getSrcElement(evt);
        pointerCursor(obj);
        doNothing(evt);
    }
    catch(err)
    {
        displayErrorMsg("handleLinkMouseOver", err);
    }
}

//功能：清空IE的selection属性
function clearTextSelection()
{
    try
    {
        if (isIE) 
        {
            document.selection.empty(); 
        } 
        else 
        {
            window.getSelection().removeAllRanges();
        }
    }
    catch(err)
    {
        displayErrorMsg("clearTextSelection", err);
    }
}


//功能：设置字段的layouts属性
//field：被操作字段
//layouts：新的layouts属性值
function setLayoutIds(field, layouts) 
{
    try
    {
        field.layouts = layouts;
    }
    catch(err)
    {
        displayErrorMsg("setLayoutIds", err);
    }	
}


//功能：设置字段的属性值
//field：存储字段信息的cell对象
//itemID：columnID
//d: dname
//f: fname
//l: 长度
//p: 精密度
//s: 小数位
//typeLabel： 类型
//itemType: 字段类型（字段，自定义链接或列表）
//rq: 必需
//ro: 只读
//ad:
function setFieldAttributes(field, itemId, d, f, l, p, s, typeLabel, itemType, rq, ro, isForeign, ad, arq, aro, anrq, anro, cust) 
{
    try
    {
        if (field.initialized == '1') return;
        field.itemId = itemId;
        field.dName = d;
        field.fName = f;
        if (l != '0') field.fLength = l;
        if (s != '0' && p != '0') 
        {
            field.fPrecision = p;
            field.fScale = s;
        }
        field.fDataLabel = typeLabel;
    //        setCellRequiredNess(field, rq);
    //        setCellReadonlyNess(field, ro);
        field.rq = rq;
        field.ro = ro;
        field.isForeign = isForeign;
        field.custom = cust;
        field.ad = ad;  
        if (ad == '1') 
        {
            field.style.fontWeight = 'bold';
        }
        field.arq = arq;    // always required
        field.aro = aro;    // always readonly
        field.anrq = anrq;  // always not required
        field.anro = anro;  // always not readonly
        field.fieldType = itemType;
        
        field.onmousedown = handleMouseDown;
        field.onclick = handleMouseClick;
        if (initUsedFields[itemId]) 
        {
            field.originalBGColor = 'CCCCAA';
            setCellToUsed(field);   //已拖到布局里，变为已用状态
            if (document.getElementById(initUsedFields[itemId]) != null)
                document.getElementById(initUsedFields[itemId]).originalId = field.id;
        } 
        else 
        {
            field.style.backgroundColor = 'CCCCAA';
            initUsedFields[itemId] = field.id;      
        }   
        field.initialized = '1';
        formatField(field);
    }
    catch(err)
    {
        displayErrorMsg("function", err);
    }  
}

//功能：设置字段的必填属性
//f：被操作字段单元格
//required：rq属性新值
function setCellRequiredNess(f, required) 
{
    try
    {
        f.rq = required ? '1' : '0';
    }
    catch(err)
    {
        displayErrorMsg("setCellRequiredNess", err);
    }   
}

//功能：设置字段的只读属性
//f：被操作字段单元格
//required：ro属性新值
function setCellReadonlyNess(f, readonly) 
{
    try
    {
    f.ro = readonly ? '1' : '0';
    }
    catch(err)
    {
        displayErrorMsg("setCellReadonlyNess", err);
    }
}

//功能：设置字段的il属性
//f：被操作字段单元格
//required：il属性新值
function setCellInlinedNess(f, inlined) 
{
    try
    {
        f.il = inlined ? '1' : '0';
    }
    catch(err)
    {
        displayErrorMsg("setCellInlinedNess", err);
    }
}

//功能：设置字段的custom属性
//f：被操作字段单元格
//required：custom属性新值
function setCellCustomNess(f, custom) 
{
    try
    {
        f.custom = custom ? '1' : '0';
    }
    catch(err)
    {
        displayErrorMsg("setCellCustomNess", err);
    }
}

//功能：设置字段的fLength属性
function setCellLengthNess(f, l)
{
    try
    {
        f.fLength = l;
    }
    catch(err)
    {
        displayErrorMsg("setCellLengthNess", err);
    }   
}

function setListSelectedColumns(selectedList, selectedColumns, sortAlias, sortAscending)
{
    try
    {
        var columns = new Array();

	    var options = selectedColumns.options;
	    for (var i = 0; i < options.length; i++) 
	    {
		    var column = new Object();
		    column.label = options[i].text;
		    column.alias = options[i].value;
    		
		    if (sortAlias == column.alias) 
		    {
			    column.sorting = sortAscending ? 1 : -1;
		    } 
		    else 
		    {
			    column.sorting = 0;
		    }
		    columns.push(column);
	    }
	    // Compare the list to the default.
	    var isSame = false;
	    selectedList.columns = columns;
	    if (selectedList.columns.length == selectedList.defaultColumns.length) 
	    {
		    isSame = true;
		    for (var i = 0; i < selectedList.columns.length; i++) 
		    {
			    var left = selectedList.columns[i];
			    var right = selectedList.defaultColumns[i];
			    if (left.alias != right.alias || left.sorting != right.sorting) 
			    {
				    isSame = false;
				    break;
			    }
		    }
	    }
	    if (isSame) selectedList.columns = selectedList.defaultColumns;
	    selectedList.hasChildren = true;
	    formatField(selectedList);
    }
    catch(err)
    {
        displayErrorMsg("setListSelectedColumns", err);
    }
}

//功能：格式化字段
//field：被操作字段
function formatField(field) 
{
    try
    {
        var name = field.dName;

        if (!name) name = field.fName;
        if (!name) return;
        if (field.ro == '1' || field.aro == '1') 
        {
            field.innerHTML = '<img src="' + imagePrePath + 'readonly.gif" border="0" alt="只读" title="只读" width=15 height=15 onclick="doNothing(event);" onmousedown="doNothing(event);" align="top" onmouseover="doNothing(event);">' + '&nbsp;' + field.dName;
        } 
        else if (field.rq == '1' || field.arq == '1') 
        {
            field.innerHTML = '<img src="' + imagePrePath + 'required.gif" border="0" alt="必填项" title="必填项" width=15 height=15 onclick="doNothing(event);" onmousedown="doNothing(event);" align="top" onmouseover="doNothing(event);">' + '&nbsp;' + field.dName;
        } 
        else if (field.il == '1') 
        {
            field.innerHTML = '<img src="' + imagePrePath + 's.gif" border="0" width=15 height=15 onclick="doNothing(event);" onmousedown="doNothing(event);" align="top" onmouseover="doNothing(event);">' + '&nbsp;' + field.dName;
        } 
        else 
        {
            field.innerHTML = '<img src="' + imagePrePath + 's.gif" border="0" width=15 height=15 onclick="doNothing(event);" onmousedown="doNothing(event);" align="top" onmouseover="doNothing(event);">' + '&nbsp;' + field.dName;
        }
        //如果是本表字段，并且还没有使用的
        if (field.isForeign == "1" && !field.used)
        {
            field.style.backgroundColor = "CDEFAB";
        }
    }
    catch(err)
    {
        displayErrorMsg("formatField", err);
    }   
}

//功能：清空字段所有属性
//c：被操作字段(字段关联为单元格)
function setCellToEmpty(c) 
{
    try
    {
        c.itemId = '';
        c.innerHTML = '';
        c.originalId = '';
        c.onmousedown = '';
        c.onmouseout = '';
        c.onclick = '';
        if (isIE5) c.onclick = handleMouseClick;
        c.used = false;
        c.ro = '0';
        c.rq = '0';
        c.ad = '0';
        c.arq = '0';
        c.aro = '0';
        c.anrq = '0';
        c.anro = '0';
        c.fieldType = '';
        c.custom = '0';
    }
    catch(err)
    {
        displayErrorMsg("setCellToEmpty", err);
    }
}

//功能：设置字段为已用状态（变灰且不再响应事件）
//c：被操作字段(字段关联为单元格)
function setCellToUsed(c) 
{
    try
    {
        c.style.backgroundColor = 'EEEEEE';
        c.style.color = 'B0B0B0';
        c.onmousedown = '';
        c.used = true;
    }
    catch(err)
    {
        displayErrorMsg("setCellToUsed", err);
    }
}

//功能：复制单元格事件及属性（字段）
//to：目标单元格
//from：源单元格
function copyCell(to, from)
{
    try
    {
        if (from.innerHTML == '') 
        {
            setCellToEmpty(to);
            return;
        }
        to.innerHTML = from.innerHTML;
        to.originalId = from.originalId ? from.originalId : from.id;
        to.onmousedown = handleMouseDown;
        to.onmouseout = handleMouseOut;
        to.onclick = handleMouseClick;
        to.onmouseover = handleMouseOver;
        to.used = false;
        to.dName =         from.dName;
        to.fName =         from.fName;
        to.fDataLabel =    from.fDataLabel;
        to.itemId = from.itemId;
        to.fieldType = from.fieldType;
        if (from.fLength) to.fLength = from.fLength;
        if (from.fPrecision) to.fPrecision = from.fPrecision;
        if (from.fScale) to.fScale = from.fScale;
        if (from.ro) to.ro = from.ro;
        if (from.isForeign) to.isForeign = from.isForeign;
        if (from.rq) to.rq = from.rq;
        if (from.ad) to.ad = from.ad;
        if (from.il) to.il = from.il;
        if (from.arq) to.arq = from.arq;
        if (from.aro) to.aro = from.aro;
        if (from.anrq) to.anrq = from.anrq;
        if (from.anro) to.anro = from.anro;
        to.custom = from.custom;
        to.columns = from.columns;
        to.defaultColumns = from.defaultColumns;
        to.hasChildren = from.hasChildren;
    }
    catch(err)
    {
        displayErrorMsg("copyCell", err);
    }
}

//功能：插入单元格处理事件
//evt：事件处理源
function insertCell(evt) 
{
    try
    {
        var tablesToReformat = new Array();
        if (isInAvailableSection(currentHighlightedObj)) 
        {
            for (var i = 0; i < selectedBucket.length; i++) 
            {
                
                var originalCell = document.getElementById(selectedBucket[i].originalId);
                //如果是相应的表视图，则变为可用，否则不处理
                if (originalCell != null && originalCell.itemId == selectedBucket[i].itemId)
                {
                    if (selectedBucket[i].ad == '0') 
                    {
                        copyCell(originalCell, selectedBucket[i]);
                        originalCell.style.backgroundColor = 'CCCCAA';
                        originalCell.style.color = '000000';
                        originalCell.innerHTML = originalCell.dName;
                        setCellRequiredNess(originalCell, false);
                        setCellReadonlyNess(originalCell, false);
                        formatField(originalCell);

				        // Refresh the available sections
                        if ((i+1) == selectedBucket.length) 
                        {
                            var divId = getDivIdFromTableId(getTableId(originalCell));
					        var availableDropDown = document.getElementById('availableDropDown');
                            if (availableDropDown) 
                            {
						        // Refresh the dropdown
						        function getDivPrefixFromDivId(dId) 
						        {
							        var lastSeparatorIndex = dId.lastIndexOf('_');
							        if (lastSeparatorIndex > -1) 
							        {
								        return dId.substring(0, lastSeparatorIndex);
							        }
							        return dId;
						        }
						        var divPrefix = getDivPrefixFromDivId(divId);
                    	        var options = availableDropDown.options;
						        for (var selIndex = 0; selIndex < options.length; selIndex++) 
						        {
							        if (getDivPrefixFromDivId(options[selIndex].value) == divPrefix) 
							        {
								        availableDropDown.selectedIndex = selIndex;
							        }
						        }
					        }
					        // refresh the section
                            if (document.getElementById(divId)) swapDivs(divId, divId);
                        }
                    }
                }
                if (!isInAvailableSection(selectedBucket[i])) 
                {
                    var tableToReformatId = getTableId(selectedBucket[i]);
                    tablesToReformat[tableToReformatId] = tableToReformatId;
                }
            }
            var numAlwaysDisplayedField = 0;
            for (var i = 0; i < selectedBucket.length; i++) 
            {
                if (selectedBucket[i].ad && selectedBucket[i].ad == '1') 
                {
                    numAlwaysDisplayedField++;
                    continue;
                }
                setCellToEmpty(selectedBucket[i]);
            }
            
            if (numAlwaysDisplayedField == 1) 
            {
                alert('此字段可能无法从页面布局中删除。');
            } else if (numAlwaysDisplayedField > 1) 
            {
                alert('没有删除所选的一个或多个字段，因为它们必须包括在页面布局中。');
            }
        } 
        else 
        {
            var tableId = getTableId(currentHighlightedObj);
            var rowNum = getRowNum(currentHighlightedObj) - 1;
            var colNum = getColNum(currentHighlightedObj) - 1;
            var t = getTable(tableId);

            if (!t) return;
            var selectedData = new Array();

            var isGoingToStandardSection = isInStandardSection(currentHighlightedObj);
            var isGoingToListsSection = isInListsSection(currentHighlightedObj);
            var isGoingToLinksSection = isInLinksSection(currentHighlightedObj); 
            var foundRestrictedStandardField = 0;
            var foundRestrictedListField = 0;
            var foundRestrictedLinkField = 0;
            for (var j = 0; j < selectedBucket.length; j++) {
                if (selectedBucket[j].fieldType == 'F' || selectedBucket[j].fieldType == 'C' ) 
                {
                    if (!isGoingToStandardSection) 
                    {
                        foundRestrictedStandardField++;
                        selectedBucket[j].style.backgroundColor = selectedBucket[j].originalBGColor;
                        continue;
                    }
                } 
                else if (selectedBucket[j].fieldType == 'K') 
                {
                    if (!isGoingToLinksSection) 
                    {
                        foundRestrictedLinkField++;
                        selectedBucket[j].style.backgroundColor = selectedBucket[j].originalBGColor;
                        continue;
                    }
                } 
                else 
                {
                    if (!isGoingToListsSection) 
                    {
                        foundRestrictedListField++;
                        selectedBucket[j].style.backgroundColor = selectedBucket[j].originalBGColor;
                        continue;
                    }
                }
                        
                var selData = new Object();
                copyCell(selData, selectedBucket[j]);
                selectedData = selectedData.concat(selData);
                if (!isInAvailableSection(selectedBucket[j])) 
                {
                    var tableToReformatId = getTableId(selectedBucket[j]);
                    tablesToReformat[tableToReformatId] = tableToReformatId;
                    setCellToEmpty(selectedBucket[j]);
                } 
                else 
                {
                    setCellToUsed(selectedBucket[j]);
                }
                
            }
            
            for (var j = 0; j < selectedData.length; j++) 
            {
                var previousCell = new Object();
                copyCell(previousCell, selectedData[j]);

                // Anywhere you insert a cell, start from there, and shift everything down.
                for (var i = rowNum*2; i < t.rows.length; i++) 
                {
                    var row = t.rows[i];
                    if (!row || i%2 == 0) continue;
                    var cell = row.cells[colNum];

                    var tempCell = new Object();
                    copyCell(tempCell, cell);
                    copyCell(cell, previousCell);
                    previousCell = tempCell;
                }
                rowNum++;
                addRow(tableId);
            }
            
            if (foundRestrictedListField > 0) 
            {
                alert('必须将相关列表放入相关列表部分。');
            }
            if (foundRestrictedLinkField > 0) 
            {
                alert(
                    '必须将自定义链接放入“' 
                    + getLinksSectionName('自定义链接')
                    + '”部分。'
                    );
            }
            if (foundRestrictedStandardField > 0) 
            {
                alert('必须将字段放入字段部分。');
            }   
            
        }
        for (var tId in tablesToReformat) 
        {
            reformatTable(document.getElementById(tId));
        }
        if (!isInAvailableSection(currentHighlightedObj)) 
        {
            reformatTable(document.getElementById(getTableId(currentHighlightedObj)));
        }
        selectedBucket = new Array();
    }
    catch(err)
    {
        displayErrorMsg("insertCell", err);
    }   
}

//功能：解析后获得links部分的名称
//defaultName：解析失败时返回的默认名称
function getLinksSectionName(defaultName) 
{
    try
    {
        for (var i = 0; i <= maxSection; i++) 
        {
            var section = document.getElementById('sec_' + 'k' + i);
            if (section && section.masterLabel && section.masterLabel.replace(/\s/g, '').length != 0) 
            {
                return escapeJS(section.masterLabel, true, false);
            }
        }
        return defaultName;
    }
    catch(err)
    {
        displayErrorMsg("getLinksSectionName", err);
    }
}


//功能：交换两个页面部分（Part)
//evt：事件处理源
function swapSections(evt) 
{
    try
    {
        if (currentSelectedObj == currentHighlightedObj) return;
        
        currentSelectedObj.style.backgroundColor = currentSelectedObj.originalBGColor;
        handleDragOut(currentSelectedObj);
        handleDragOut(currentHighlightedObj);


        var currentTableId = currentSelectedObj.tableId;
        var highlightedTableId = currentHighlightedObj.tableId;
        var currentTable = document.getElementById(currentTableId);
        var currentDetailHeading = currentSelectedObj.detailHeading;
        var currentEditHeading = currentSelectedObj.editHeading;
        var currentCanEditLabel = currentSelectedObj.canEditLabel;
        var currentMasterLabel = currentSelectedObj.masterLabel;
        var currentSortOrder = currentSelectedObj.sortOrder;
        var currentItemId = currentSelectedObj.itemId;
        var columnCount = currentSelectedObj.columnCount;
        var currentBody = currentTable.cloneNode(true);
        
                deleteSectionRow(currentTableId);
        insertSectionRow(highlightedTableId, currentTableId, currentBody);
        // need this here for NS
        document.getElementById(currentSelectedObj.id).tableId = currentTableId;
        
        initSectionTable(document.getElementById(currentSelectedObj.id), currentTableId, currentSortOrder, currentDetailHeading, currentEditHeading, currentCanEditLabel, currentMasterLabel, currentItemId, columnCount);
        initSection(document.getElementById(getTableIdFromSectionId(currentSelectedObj.id)));

        if (!isIE) copyCells(currentTable.rows[1].cells[0].childNodes[0], currentBody.rows[1].cells[0].childNodes[0]);
        if (!isIE) reformatTable(document.getElementById(getTableIdFromSectionId(currentSelectedObj.id)));

        clearTextSelection();
    }
    catch(err)
    {
        displayErrorMsg("swapSections", err);
    }
}

//功能：复制Table
//fromT：源Table对象
//toT：目标Table对象
function copyCells(fromT, toT) 
{
    try
    {
        cellBuffer = new Array();
        for (var i = 0; i < fromT.rows.length; i++)
        {
            if (i % 2 == 0) continue;
            var fromR = fromT.rows[i];
            var toR = toT.rows[i];
            for (var j = 0; j < fromR.cells.length; j++) 
            {
                var fromC = fromR.cells[j];
                var toC = toR.cells[j];
                copyCell(toC, fromC);
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("copyCells", err);
    }
}

//功能：初始化代表Part的Table
//sectionTable：代表Part的Table对象
//tableID：代表Part的Table对象ID
//sortOrder...：Part的一系列属性
function initSectionTable(sectionTable, tableId, sortOrder, detailHeading, editHeading, canEditLabel, masterLabel, itemId, columnCount) 
{
    try
    {
        sectionTable.onmouseover = handleMouseOver;
        sectionTable.onmouseout = handleMouseOut;
        sectionTable.onmousedown = handleMouseDown;
        sectionTable.onclick = handleMouseClick;
        sectionTable.tableId = tableId;
        sectionTable.sortOrder = sortOrder;
        sectionTable.detailHeading = detailHeading;
        sectionTable.editHeading = editHeading;
        sectionTable.canEditLabel = canEditLabel;
        sectionTable.masterLabel = masterLabel;
        sectionTable.itemId = itemId;
        sectionTable.columnCount = columnCount;
    }
    catch(err)
    {
        displayErrorMsg("initSectionTable", err);
    }
}

//功能：初始化代表Part的Table
//t：代表Part的table对象
function initSection(t) 
{
    try
    {
        for (var i = 0; i < t.rows.length; i++) 
        {
            var row = t.rows[i];
            if (!row || i%2 == 0) continue;
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.innerHTML != '') 
                {
                    cell.onmousedown = handleMouseDown;
                    cell.onclick = handleMouseClick;
                    cell.onmouseover = handleMouseOver;
                    cell.onmouseout = handleMouseOut;
                    cell.used = false;
                } 
                else 
                {
                    cell.onmousedown = '';
                    cell.onclick = handleMouseClick;
                    cell.onmouseover = handleMouseOver;
                    cell.onmouseout = handleMouseOut;
                    cell.used = false;
                }
            }
            
        }
    }
    catch(err)
    {
        displayErrorMsg("initSection", err);
    }
}

//功能：删除Part的事件处理
//sectionID：代表Part的Table标识符
function deleteSection(sectionId) 
{
    try
    {
        var t = getTable(getTableIdFromSectionId(sectionId));
        if (t.rows.length > 2) 
        {
            if (!window.confirm('删除此部分会将其所有字段移回“可用字段”。' + '\n\n' + '是否确定？')) 
            {
                return;
            }   

        }
        var hasAlwaysDisplayedField = false;
        for (var i = 0; i < t.rows.length - 2 && !hasAlwaysDisplayedField; i++) 
        {
            if (i % 2 == 0) continue;
            var row = t.rows[i];
            for (var j = 0; j < row.cells.length && !hasAlwaysDisplayedField; j++) 
            {
                var cell = row.cells[j];
                if (cell.ad && cell.ad == '1') 
                {
                    hasAlwaysDisplayedField = true; 
                }
            }
        }
        if (hasAlwaysDisplayedField) 
        {
            alert('不能删除此部分，因为至少有一个字段不能从页面布局中删除。');
            return;
        }

        var sectionHeader = document.getElementById(getSectionHeaderId(t.id));
        if (sectionHeader.canEditLabel == '0') 
        {
            var okayToDelete = confirm(
                '此部分由 800crm.com 提供。此部分的名称已针对国际用户进行翻译。' 
                + '\n' 
                + '如果删除此部分，则不能再添加它。'
                + '\n\n'
                + '我们建议您从此部分中删除所有字段，而不是删除此部分。' 
                + '\n' 
                + '这样，此部分就不会出现在页面上，而您在将来仍然能够使用它。'
                + '\n\n' 
                + '是否确定？'); 
            if (!okayToDelete) return;   
        }

        for (var i = 0; i < t.rows.length - 2; i++) 
        {
            if (i % 2 == 0) continue;
            var row = t.rows[i];
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.originalId) 
                {
                    var originalCell = document.getElementById(cell.originalId);
                    if (originalCell) 
                    { 
                        copyCell(originalCell, cell);
                        originalCell.style.backgroundColor = originalCell.originalBGColor;
                        setCellRequiredNess(originalCell, false);
                        setCellReadonlyNess(originalCell, false);
                        formatField(originalCell);
                    }
                }

            }

        }
        deleteSectionRow(document.getElementById(sectionId).tableId);
    }
    catch(err)
    {
        displayErrorMsg("deleteSection", err);
    }
}

//功能：删除mainLayoutTable Table对象中代表layoutPart的三行
//tableID：table对象的标识符
function deleteSectionRow(tableId) 
{
    try
    {
        var mainTable = document.getElementById('mainLayoutTable');
        var rpId =  getSeparatorId(tableId);
        var rowNum = -1;
        for (var i = 0; rowNum < 0 && i < mainTable.rows.length; i++) 
        {
            var row = mainTable.rows[i];
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.id == rpId) 
                {
                    rowNum = i;
                }
            }
        }
        mainTable.deleteRow(rowNum);
        mainTable.deleteRow(rowNum);
        mainTable.deleteRow(rowNum);
    }
    catch(err)
    {
        displayErrorMsg("deleteSectionRow", err);
    }
}

//功能：插入Part
//sectionText..：Part的一系列属性
function insertSection(sectionText, c, sortOrder, detailHeading, editHeading) 
{
    try
    {
        if (sectionText == '') 
        {
            alert('必须输入部分名称！');
            return;
        }
        var numColumns = parseInt(c);
        maxSectionTable++;
        maxSection++;
        var sectionName = "s" + maxSection;
        var tableName = "table" + maxSectionTable;
        var newSection = '';
        newSection += '<table cellspacing=1 cellpadding=2 style="background-color:cccccc" width="400" id="' + tableName + '">';
        newSection +=   '<tr style="background-color:#99CCFF"><td>';
        newSection +=       '<table border="0" cellspacing="0" cellpadding="0" width="100%">';
        newSection +=           '<tr valign=bottom ><td style="color:#000000" id="' + getSectionHeaderId(sectionName) + '"  align="left" width="99%">' + sectionText + '</td>';
        newSection +=               '<td align=right nowrap style="color:#000000">';
        newSection +=                   '<a onmouseover="handleLinkMouseOver(event);" onmousedown="doNothing(event);" onclick="openSectionEdit(\'' +  getSectionHeaderId(sectionName) + '\', event);">';
        newSection +=                       '编辑';
        newSection +=                   '</a>';
        newSection +=                   ' | ';
        newSection +=                   '<a onmouseover="handleLinkMouseOver(event);" onmousedown="doNothing(event);" onclick="deleteSection(\'' + getSectionHeaderId(sectionName) + '\');">';
        newSection +=                       '删除';
        newSection +=                   ' </a>';
        newSection +=               ' </td></tr>';
        newSection +=       '</table></td>';
        newSection +=   '</tr>';
        newSection +=   '<tr style="background-color:FFFFFF" height=15>';
        newSection +=       '<td>';
        newSection +=           '<table id="' + sectionName + '" width="100%" cellspacing=2 bgcolor="FFFFFF">';
        newSection +=               '<tr height=2>';
        for (var i = 0; i < numColumns; i++) 
        {
            newSection +=               '<td id="' + getSeparatorId(constructId(sectionName, 1, i+1)) + '" width="200"></td>';
        }
        newSection +=               '</tr>';
        newSection +=               '<tr height=10>';
        for (var i = 0; i < numColumns; i++) 
        {
            newSection +=               '<td id="' + constructId(sectionName, 1, i+1) + '"></td>';
        }
        newSection +=               '</tr>';
        newSection +=           '</table>';
        newSection +=       '</td>';
        newSection +=   '</tr>';
        newSection += '</table>';
        
        insertSectionRow('table0', tableName, newSection);
        initSectionTable(document.getElementById(getSectionHeaderId(sectionName)), tableName, sortOrder, detailHeading, editHeading, '1', '0', '', c);
        initSection(document.getElementById(sectionName));
        // NN does not suppport focus on just any elements
        if (isIE) document.getElementById(sectionName).focus();
    }
    catch(err)
    {
        displayErrorMsg("insertSection", err);
    }
}

//功能：
//
function insertSectionRow(tableId, newTableId, newSection) 
{
    try
    {
        var mainTable = document.getElementById('mainLayoutTable');
        var rpId =  getSeparatorId(tableId);
        var rowNum = -1;
        for (var i = 0; rowNum < 0 && i < mainTable.rows.length; i++) 
        {
            var row = mainTable.rows[i];
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.id == rpId) 
                {
                    rowNum = i;
                }
            }
        }
        if (rowNum < 0) 
        {
            rowNum = mainTable.rows.length - 2;
        }
        var newRow = mainTable.insertRow(rowNum);
        var newCell = newRow.insertCell(0);
        newCell.height = 5;
        newCell.id = getSeparatorId(newTableId);

        newRow = mainTable.insertRow(rowNum+1);
        newCell = newRow.insertCell(0);
        if (!newSection.nodeType) 
        {
            newCell.innerHTML = newSection;
        } 
        else 
        {
            newCell.insertBefore(newSection, null);
        }
        newRow = mainTable.insertRow(rowNum+2);
        newCell = newRow.insertCell(0);
        newCell.height = 10;
    }
    catch(err)
    {
        displayErrorMsg("insertSectionRow", err);
    }
}

//功能：打开Part编辑窗口
//sectionID：要打开的Part的标识符
//evt：事件处理源
function openSectionEdit(sectionId, evt) 
{
    try
    {
        //evt = getEvent(window.event);
        //setLastMousePosition(window.event);
        var url = 'AddLayoutPart.aspx';

        if (sectionId != '') 
        {
            var host = new String(window.location);
            host = host.substring(0, host.indexOf('AddLayoutPart.aspx'));
            url = host + 'EditLayoutPart.aspx' + '?sectionId=' + sectionId; 
        }
        OpenEditForm(url, 450, 270);

    }
    catch(err)
    {
        displayErrorMsg("openSectionEdit", err);
    }
}

//功能：打开字段编辑窗口
//evt：事件处理源
function openFieldEdit(evt) 
{
    try
    {
        if (selectedBucket.length < 1) 
        {
            alert('请在页面布局部分选择至少一个字段。');
            return;
        }
        var hasAtLeastOneField = false;
        for (var i = 0; i < selectedBucket.length; i++) 
        {
            f = selectedBucket[i];
            if (isInAvailableSection(f)) 
            {
                alert('此功能仅可用于页面布局部分内的字段。');
                return; 
            }
            if (isInStandardSection(f)) 
            {
                hasAtLeastOneField = true;
            }
        }
        if (!hasAtLeastOneField) 
        {
            alert('此功能仅用于字段。请选择一个或多个字段。');
            return;
        }
        //evt = getEvent(evt);
        //setLastMousePosition(window.event);
        var h = (selectedBucket.length > 2) ? 400 : 200;
        OpenEditForm('EditLayoutDetail.aspx', 450, h);
    }
    catch(err)
    {
        displayErrorMsg("openFieldEdit", err);
    }
}


function OpenEditForm(url, width, height)
{
    //打开窗口的外观
    width = width == null ? window.screen.availWidth * 0.95 : width;
    height = height == null ? window.screen.availHeight * 0.9 : height;
    var sEditFormFeatures = "width=" + width +",height=" + height+ ",scrollbars=yes,toolbar=no,status=no,directories=no,menubar=no,resizable=yes"; 
    editFormObj = window.open(url, "", sEditFormFeatures, "");
    var top  = (window.screen.availHeight - height) / 2;
    var left = (window.screen.availWidth - width) / 2;
    editFormObj.moveTo(top, left);
}


//功能：打开相关列表编辑窗口
//evt：事件处理源
//mym：
function openRelatedListEdit(evt,mym) 
{
    try
    {
        if (selectedBucket.length != 1) 
        {
            alert('请从相关列表部分选择一个相关列表。');
            return;
        }
        f = selectedBucket[0];
      
        if (isInAvailableSection(f)) 
        {
            alert('此功能仅可用于页面布局部分内的相关列表。');
            return; 
        }
        if (!isInListsSection(f)) 
        {
            alert('此功能仅用于相关列表。请选择一个或多个相关列表。');
            return;
        }

        if (!f.custom || f.custom == '0') 
        {
            alert('无法编辑所选相关列表。');
            return;
        }
        evt = getEvent(evt);
        setLastMousePosition(evt);
        var url = 'rellistedit.jsp';
        url += '?mid=' + f.itemId+'&mym='+mym;
         if (f.layouts != undefined) url += '&lids=' + f.layouts;
        var h = 450;
        var w = 690;
        var features = 'width='+w+',height='+h+',scrollbars=yes,toolbar=no,status=no,directories=no,menubar=no,resizable=yes';
        if (window.showModalDialog) 
        {
    	    h+=20;
            openPopup(url , 'sectionEdit', w, h, features, true);
        } 
        else 
        {
            openPopupModal(url , 'sectionEdit', w, h, features, window);
        }
    }
    catch(err)
    {
        displayErrorMsg("openRelatedListEdit", err);
    }
}


 //功能：判断是否代表Part的对象
 //obj：需要判断的对象
 //返回值：bool
function isSection(obj) 
{
    try
    {
        return obj.id.indexOf('sec_') > -1;
    }
    catch(err)
    {
        displayErrorMsg("isSection", err);
    }
}

//功能：从secitonID中按照某种协议解析并返回TableID
//sectionId：需要解析的section的标识符
function getTableIdFromSectionId(sectionId) 
{
    try
    {
        return sectionId.substring(sectionId.indexOf('sec_')+4, sectionId.length);
    }
    catch(err)
    {
        displayErrorMsg("getTableIdFromSectionId", err);
    }
}

//功能：从tableID中获得section的索引号
//tableId：被解析table的标识标
function getSectionIndex(tableId) 
{
    try
    {
        return tableId.substring(tableId.indexOf('table') + 5, tableId.length);
    }
    catch(err)
    {
        displayErrorMsg("getSectionIndex", err);
    }
}

//功能：获得该table名称的对应ID
//tableName：table的名称
function getTable(tableName) 
{
    try
    {
        return document.getElementById(tableName);
    }
    catch(err)
    {
        displayErrorMsg("getTable", err);
    }
}

//功能：获得该table名称的对应Body对象
//tableName：table的名称
function getTableBody(tableName) 
{
    try
    {
        return document.getElementById(tableName).tBodies[0];
    }
    catch(err)
    {
        displayErrorMsg("getTableBody", err);
    }
}

//功能：从对象Id中解析是否合法Section
//返回值：bool
function isInAvailableSection(obj) 
{
    try
    {
        return getTableId(obj).indexOf('a') > -1;
    }
    catch(err)
    {
        displayErrorMsg("isInAvailableSection", err);
    }
}

//功能：从sectionHeaderId中解析是否标准Section
//obj：sectionHeader对象
//返回值：bool
function isStandardSection(sectionHeaderId) 
{
    try
    {
        var sectionId = getTableIdFromSectionId(sectionHeaderId);
        return sectionId.indexOf('s') == 0;
    }
    catch(err)
    {
        displayErrorMsg("isStandardSection", err);
    }
}

//功能：从对象Id中解析是否标准Section
//obj：table对象
//返回值：bool
function isInStandardSection(obj) 
{
    try
    {
        return getTableId(obj).indexOf('s') > -1;
    }
    catch(err)
    {
        displayErrorMsg("isInStandardSection", err);
    }
}

//功能：从对象Id中解析是否列表Section
//obj：table对象
//返回值：bool
function isInListsSection(obj) 
{
    try
    {
        return getTableId(obj).indexOf('l') > -1;
    }
    catch(err)
    {
        displayErrorMsg("isInListsSection", err);
    }
}

//功能：从对象Id中解析是列表Section
//obj：table对象
//返回值：bool
function isInLinksSection(obj) 
{
    try
    {
        return getTableId(obj).indexOf('k') > -1;
    }
    catch(err)
    {
        displayErrorMsg("isInLinksSection", err);
    }
}

//功能：获得对象对应的tableID
//obj：被解析的对象
function getTableId(obj) 
{
    try
    {
        return obj.id.substring(0, obj.id.indexOf('r'));
    }
    catch(err)
    {
        displayErrorMsg("getTableId", err);
    }
}

//功能：从tableID中获取对应的Div标识符
//tableId：要解析的table对象的标识符
function getDivIdFromTableId(tableId) 
{
    try
    {
        return 'div_'+tableId;
    }
    catch(err)
    {
        displayErrorMsg("getDivIdFromTableId", err);
    }
}

//功能：从row对象中获取其对应的索引
//obj：row对象
function getRowNum(obj) 
{
    try
    {
        return parseInt(obj.id.substring(obj.id.indexOf('r')+1, obj.id.indexOf('c')));
    }
    catch(err)
    {
        displayErrorMsg("getRowNum", err);
    }
}

//功能：从column对象中获取其对应的索引
//obj：column对象
function getColNum(obj) 
{
    try
    {
        return parseInt(obj.id.substring(obj.id.indexOf('c')+1, obj.id.length));
    }
    catch(err)
    {
        displayErrorMsg("getColNum", err);
    }
}

//功能：按某各协议构造Id
//tableID：table标识符
//rowNum：row索引值
//colNum：column索引值
function constructId(tableId, rowNum, colNum) 
{
    try
    {
        return tableId + 'r' + rowNum + 'c' + colNum;
    }
    catch(err)
    {
        displayErrorMsg("constructId", err);
    }
}

//功能：按协议构造分离条的id
function getSeparatorId(baseId) 
{
    try
    {
        return 'rp_' + baseId;
    }
    catch(err)
    {
        displayErrorMsg("getSeparatorId", err);
    }
}

//功能：按协议构造SectionHeader的id
function getSectionHeaderId(baseId) 
{
    try
    {
        return 'sec_' + baseId;
    }
    catch(err)
    {
        displayErrorMsg("getSectionHeaderId", err);
    }
}

//功能：增加一行
function addRow(tableName) 
{
    try
    {
        var columns = getTable(tableName).rows[0].cells.length;
        addRowWithColumn(tableName, columns);
    }
    catch(err)
    {
        displayErrorMsg("addRow", err);
    }
}

//功能：增加行中的列
//tableName：
//columns：需要增加的列数
function addRowWithColumn(tableName, columns) 
{
    try
    {
        var tr;
        var td;
        var columnWidth = 400 / columns;
        var tableBody = getTableBody(tableName);
        var length = tableBody.rows.length;
        var rowNum = (length/2) + 1;
        tr = tableBody.insertRow(tableBody.rows.length);
        tr.setAttribute("height", "2");

        for (var i = 0; i < columns; i++) 
        {
            td = tr.insertCell(tr.cells.length);
            td.setAttribute("id", getSeparatorId(constructId(tableName, rowNum, i+1)));
            td.setAttribute("width", columnWidth);
        }
        
        tr = tableBody.insertRow(tableBody.rows.length);
        tr.setAttribute("height", "10");

        for (var i = 0; i < columns; i++) 
        {
            td = tr.insertCell(tr.cells.length);
            td.setAttribute("id", constructId(tableName, rowNum, i+1));
            td.onmouseover = handleMouseOver;
            td.onclick = handleMouseClick;
        }
    }
    catch(err)
    {
        displayErrorMsg("addRowWithColumn", err);
    }
}

//功能：删除行
//tableName：所要删除行的table
function delRow(tableName) 
{
    try
    {
        var tableBody = getTableBody(tableName);
        if (tableBody.childNodes.length > 0) {
            var lastRow = tableBody.childNodes[tableBody.childNodes.length-1];
            getTableBody(tableName).removeChild(lastRow);
        }
    }
    catch(err)
    {
        displayErrorMsg("delRow", err);
    }
}

//功能：处理当鼠标经过部分时的事件
function sectionHandleMouseOver(evt) 
{
    try
    {
        function getIECurrentTarget(evt, functionName) 
        {
            var eventHandler = eval('evt.srcElement.on'+evt.type);
            if (eventHandler && eventHandler.toString().indexOf(functionName) > -1) 
            {
                return evt.srcElement;
            }
            return null; 
        }
        evt = getEvent(evt);
        obj = isIE ? getIECurrentTarget(evt, 'sectionHandleMouseOver') : getSrcElement(evt);
    }
    catch(err)
    {
        displayErrorMsg("sectionHandleMouseOver", err);
    }
}

var time = 0;
//功能：重新格式化Table
//t:需要格式化的table
function reformatTable(t) 
{
    try
    {
        if (t == null) return;
        var isInListsSection = t.id && t.id.indexOf('l') > -1;
        for (var i = 0; i < t.rows.length; i++) 
        {
            var row = t.rows[i];
            if (!row || i%2 == 0) continue;
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.innerHTML != '' && i > 2) 
                {
                    var previousRow = t.rows[i-2];
                    var previousCell = previousRow.cells[j];
                    var done = false;
                    var k = 1;
                    while(!done && previousCell.innerHTML == '') 
                    {
                        copyCell(previousCell, cell);
                        setCellToEmpty(cell);
                        cell = previousCell;
                        k++;
                        if (i > (2*k)) 
                        {
                            previousCell = t.rows[i-(2*k)].cells[j];
                        } 
                        else 
                        {
                            done = true;
                        }
                            
                    }
                }
            }
            
        }
        var foundOneEmptyRow = false;
        for (var i = 0; i < t.rows.length; i++) 
        {
            var row = t.rows[i];
            if (!row || i%2 == 0) continue;
            var allCellsEmpty = true;
            for (var j = 0; j < row.cells.length; j++) 
            {
                var cell = row.cells[j];
                if (cell.innerHTML != '') 
                {
                    allCellsEmpty = false;
                    
                    cell.style.backgroundColor = 'CCCCAA';
                    cell.style.color = '000000';

                    if (cell.ad == '1') 
                    {
                        cell.style.fontWeight = 'bold';
                    } 
                    else 
                    {
                        cell.style.fontWeight = 'normal';
                    }
                    if (cell.arq == '1') 
                    {
                        cell.rq = '1';
                    }
                    if (cell.aro == '1') 
                    {
                        cell.ro = '1';
                    }
                    if (cell.anrq == '1') 
                    {
                        cell.rq = '0';
                    }
                    if (cell.anro == '1') 
                    {
                        cell.ro = '0';
                    }
                    formatField(cell);
                } 
                else 
                {
                    setCellToEmpty(cell);
                    cell.style.backgroundColor = 'FFFFFF';
                    cell.style.cursor = 'default';
                }
                if (cell.fieldType == 'K' && isInListsSection) 
                {
                    setCellInlinedNess(cell, true);
                    formatField(cell);
                }  
                
            }
            if (allCellsEmpty) 
            {
                if (foundOneEmptyRow) 
                {
                    t.deleteRow(i-1);
                    t.deleteRow(i-1);
                    i = i - 2;
                } 
                else 
                {
                    foundOneEmptyRow = true;
                    row.cells[0].firstemptyrow = '1';
                }
            }
        }
        if (!foundOneEmptyRow) addRow(t.id);
    }
    catch(err)
    {
        displayErrorMsg("reformatTable", err);
    }
}

//功能：重新布局部分中的单元格
function toggleColumns(sectionId, columnNum) 
{
    try
    {
        var t = document.getElementById(getTableIdFromSectionId(sectionId));
        makeColumn(t, columnNum);
        reformatTable(t);
    }
    catch(err)
    {
        displayErrorMsg("toggleColumns", err);
    }
}

//功能：复制table里的单元格
//t：需要复制的table
//sortOrder：排列方式
function cloneTableCells(t, sortOrder) 
{
    try
    {
        var originalTable = t.cloneNode(true);
        if (originalTable.rows.length == 0) return new Array();
        
        var cells = new Array(originalTable.rows.length * originalTable.rows[0].cells.length);
        var currentCellIndex = 0;
        if (sortOrder && sortOrder == 'h') 
        {
            for (var i = 0; i < originalTable.rows.length; i++) 
            {
                var row = originalTable.rows[i];
                var oRow = t.rows[i];
                for (var j = 0; j < row.cells.length; j++) 
                {
                    var cell = row.cells[j];
                    var oCell = oRow.cells[j];
                    copyCell(cell, oCell);
                    cells[currentCellIndex++] = cell;
                }

            }
        } 
        else 
        {
            for (var i = 0; i < originalTable.rows.length; i++) 
            {
                var row = originalTable.rows[i];
                var oRow = t.rows[i];
                for (var j = 0; j < row.cells.length; j++) 
                {
                    currentCellIndex = (j * originalTable.rows.length) + i;
                    var cell = row.cells[j];
                    var oCell = oRow.cells[j];
                    copyCell(cell, oCell);
                    cells[currentCellIndex] = cell;
                }

            }
        }
        return cells;
    }
    catch(err)
    {
        displayErrorMsg("cloneTableCells", err);
    }
}


//功能：部分设置成columnNum列
//t：设置的部分
function makeColumn(t, columnNum) 
{
    try
    {
        var cells = cloneTableCells(t, document.getElementById(getSectionHeaderId(t.id)).sortOrder);

        while (t.rows.length > 0) 
        {
            t.deleteRow(0);
        }
        var row1;
        var row2;
        var cell;
        var rowNum = 1;
        var cellIndex = 0;
        var currentCell = 0;    
        for (var i = 0; i < cells.length; i++ )
        {
            if (cells[i].innerHTML == '') continue;
            
            if (cellIndex == 0) 
            {
                row1 = t.insertRow(t.rows.length);
                row1.height = 2;
                row2 = t.insertRow(t.rows.length);
                row2.height = 10;
                for (var c = 0; c < columnNum; c++)
                { 
                    cell = row1.insertCell(c);
                    cell.width = 400 / columnNum;
                    cell.id = getSeparatorId(constructId(t.id, rowNum, c+1));
                    
                    cell = row2.insertCell(c);
                    cell.id = constructId(t.id, rowNum, c+1);
                   
                }
                cell = row2.cells(0);
                copyCell(cell, cells[i]);
                cell.style.backgroundColor = cells[i].style.backgroundColor;
                rowNum++;
            } 
            else
            {
                cell = row2.cells[cellIndex];
                copyCell(cell, cells[i]);
                cell.style.backgroundColor = cells[i].style.backgroundColor;
            }
            currentCell++;
            cellIndex = currentCell % columnNum;
        }
        if (t.rows.length > 0)
        {
        for (var i = cellIndex; i < columnNum; i++)
        {
            cell = t.rows[t.rows.length-1].cells[i];
            cell.onmousedown = handleMouseDown;
            cell.onmouseout = handleMouseOut;
            cell.onclick = handleMouseClick;
            cell.onmouseover = handleMouseOver;
            cell.used = false;
        }
        }
        if (t.rows.length == 0)
        {
            addRowWithColumn(t.id, columnNum);
        } 
        else 
        {
            addRow(t.id);
        }
    }
    catch(err)
    {
        displayErrorMsg("makeColumn", err);
    }
}

//功能：交换视图中的DIV
//div1：隐藏的div
//div2：显示的div
function swapDivs(div1, div2) 
{
    try
    {
        if (currentDisplayedDiv) 
        {
            currentDisplayedDiv.style.display = 'none';
        }
        if (div1 != div2) 
        {
            var d1 = document.getElementById(div1);
            d1.style.display = 'none';
        }
        var d2 = document.getElementById(div2);
        d2.style.display = 'block';
        d2.style.zIndex = 0;
        currentDisplayedDiv = d2;
    }
    catch(err)
    {
        displayErrorMsg("swapDivs", err);
    }
    
}

//功能：视图中交换有用的类型。
function swapAvailableType(sel) 
{
    try
    {
        if (currentDisplayedDiv) 
        {
            currentDisplayedDiv.style.display = 'none';
        }
        var divId = sel.options[sel.selectedIndex].value;
        var firstDiv = document.getElementById(divId);
        if (firstDiv) 
        {
            firstDiv.style.display = 'block';
            currentDisplayedDiv = firstDiv;
        }       
    }
    catch(err)
    {
        displayErrorMsg("swapAvailableType", err);
    }
    

}

//功能：右边的视图跟随窗口移动
function scrollAvailableSection()
{
    try
    {
        var doTableHeightScrolling = isIE || true;
        if (!cru) cru = (!doTableHeightScrolling) ? document.getElementById('availableSectionWrapper') : document.getElementById('scrollBuffer');
        if (!cru) return;
        
        if(!availableSectionPosInited) 
        {
            availableSectionInitPosX = getObjX(cru)
            availableSectionInitPosY = getObjY(cru)
            availableSectionPosInited = true
            cru.style.zIndex = 0;
            return
        }
        
        //don't scroll it till it goes offscreen
        if(availableSectionInitPosY+5 < (window.pageYOffset ? window.pageYOffset : document.body.scrollTop)){
            if (doTableHeightScrolling) 
            {
                cru.height = (window.pageYOffset ? window.pageYOffset : document.body.scrollTop) - availableSectionInitPosY + 5;
            } 
            else 
            {
                cru.style.position='absolute'
                cru.style.left=availableSectionInitPosX
                cru.style.top= (window.pageYOffset ? window.pageYOffset : document.body.scrollTop) + 5
                cru.style.zIndex = 0;
            }   
        } 
        else if(cru.style.position=='absolute' || !isIE)
        {
            if (doTableHeightScrolling) 
            {
                cru.height = 0;
            } 
            else 
            {
                cru.style.left=0;
                cru.style.top=0;
                cru.style.zIndex = 0;
                cru.style.position='static'
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("scrollAvailableSection", err);
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

//功能：调整窗口大小
//document.body.onresizeend = scrollAvailableSection;

//功能：调整滚动条
//document.body.onscroll = scrollAvailableSection;

//如果不是IE浏览品，每2秒调整一次滚动条
if (!isIE)
    setInterval("doScroll()",200);
    
var lastPageYOffset = 0;
function doScroll() 
{
    try
    {
        if (window.pageYOffset != lastPageYOffset) 
        { 
            scrollAvailableSection();
            lastPageYOffset = window.pageYOffset;
        }
    }
    catch(err)
    {
        displayErrorMsg("doScroll", err);
    }
    
}

//功能：处理复选框的依赖关系。
//checkBoxElement：处理的复选框
function handleCheckBoxDependencies(checkBoxElement) 
{
    try
    {
        var autoAssign = document.getElementById('autoAssign');
        var autoAssignOn = document.getElementById('autoAssignOn');
        var autoNotify = document.getElementById('autoNotify');
        var autoNotifyOn = document.getElementById('autoNotifyOn');
        if (autoAssign == checkBoxElement)
        {
            if (!autoAssign.checked) 
            {
                autoAssignOn.checked = false;
            }
        } 
        else if (autoAssignOn == checkBoxElement) 
        {
            if (autoAssignOn.checked) 
            {
                autoAssign.checked = true;
            }
        } 
        else if (autoNotify == checkBoxElement) 
        {
            if (!autoNotify.checked) 
            {
                autoNotifyOn.checked = false;
            }
        } 
        else if (autoNotifyOn == checkBoxElement) 
        {
            if (autoNotifyOn.checked) 
            {
                autoNotify.checked = true;
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("handleCheckBoxDependencies", err);
    }
    

}


//properties


//功能：显示鼠标经过时的属性表格
//evt：触发的事件
//header：属性表格的标题文字
//v：属性数组
//timeout：超时时间
function showMouseOver(evt, header, v, timeout) 
{
    try
    {
        var properties = document.getElementById('properties');

        var positionX = getMouseX(evt) + 10;
        var scrollX = getScrollX();
        if ((positionX + properties.offsetWidth) > (document.body.clientWidth + scrollX)) 
        {
            positionX = positionX - ((positionX + properties.offsetWidth) - document.body.clientWidth) + scrollX; 

        }
        var positionY = getMouseY(evt) + 20;
        var scrollY = getScrollY();
        if ((positionY + properties.offsetHeight) > (document.body.clientHeight + scrollY)) 
        {
            positionY = positionY - (40 + properties.offsetHeight); 

        }
	    properties.style.left = positionX;
	    properties.style.top = positionY;
	    properties.style.zIndex = 100;

	    var propName = document.getElementById('propHeader');
	    propName.innerHTML = header;

	    var i = 0;
	    for (; i < v.length && i < maxValues;  i++) 
	    {
		    document.getElementById('propDiv'+i).style.display = 'block';
		    document.getElementById('propText'+i).innerHTML = v[i];			
	    }

	    for (; i < maxValues; i++) 
	    {
		    var emptyRow = document.getElementById('propDiv'+i);
		    if (emptyRow) 
		    {
			    emptyRow.style.display = 'none';
		    }
	    }

	    displayProperties = true;
	    setTimeout(makePropertiesVisible, timeout);
    }
    catch(err)
    {
        displayErrorMsg("showMouseOver", err);
    }
	
}

//功能：隐藏属性，设置超时时间
//timeout：超时时间
function hideMouseOver(timeout) 
{
    try
    {
        displayProperties = false;
	    setTimeout(makePropertiesHidden, timeout);
    }
    catch(err)
    {
        displayErrorMsg("hideMouseOver", err);
    }
	
}

//功能：设置ID为properties的宽度为w
//w：宽度
function setMouseOverWidth(w) 
{
    try
    {
        document.getElementById('properties').width = w;		
    }
    catch(err)
    {
        displayErrorMsg("setMouseOverWidth", err);
    }
	
}

//功能：显示ID为properties的属性Tabl
function makePropertiesVisible() 
{
    try
    {
        if (displayProperties) 
        {
            var properties = document.getElementById('properties');
            properties.style.visibility = 'visible';
        }
    }
    catch(err)
    {
        displayErrorMsg("makePropertiesVisible", err);
    }
    
}

//功能：隐藏ID为properties的属性Table
function makePropertiesHidden() 
{
    try
    {
        var properties = document.getElementById('properties');
        properties.style.visibility = 'hidden';
    }
    catch(err)
    {
        displayErrorMsg("makePropertiesHidden", err);
    }
    
}
//Xml

//功能：如果打开的窗口还在，就关闭找开的窗口
function handleUnload() 
{
    try
    {
        if (openedWindow) 
	    {
		    openedWindow.close();
	    }
	    closePopup();
    }
    catch(err)
    {
        displayErrorMsg("handleUnload", err);
    }
	
}


//功能：替换HTML中的',<,>,空格与换行号成可以显示的符号
//v：需要替换的HTML语句
//escapeHTML：布尔值，是否替换&,<,>
//escapeWhiteSpace：布尔值，是否替换空换与换行
function escapeJS(v, escapeHTML, escapeWhiteSpace) 
{
    try
    {
        v = v.replace(/'/g, "\'");
        
        if (escapeHTML) {
            v = v.replace(/&/g, '&amp;');
            v = v.replace(/</g, '&lt;');
            v = v.replace(/>/g, '&gt;');
        }

        if (escapeWhiteSpace) {
            v = v.replace(/\t/g, "&nbsp;&nbsp;");
            v = v.replace(/\n/g, "<br>");
        }
        return v;       
    }
    catch(err)
    {
        displayErrorMsg("escapeJS", err);
    }
    

}









//功能：从代表字段的单元格中取出字段信息,以实体返回
//cellObj：存取字段信息的单元格对象
//另外字段的orderBy属性在外面遍历时获取
function getFieldEntity(cellObj)
{
    try
    {
        
        var columnID    = cellObj.itemId;
        var fName       = cellObj.fName;
        var required    = cellObj.rq;
        var readOnly    = cellObj.ro;
        var isForeign   = cellObj.isForeign;
        var length      = cellObj.fLength;
        var ad          = cellObj.ad;
        
        var layoutDetail = new Object();
        layoutDetail.ColumnID = columnID;
        layoutDetail.Required = required == "1";
        layoutDetail.Readonly = readOnly == "1";
        layoutDetail.IsForeign = isForeign == "1";
        layoutDetail.Length = length;
        layoutDetail.OrderBy = 0;
        return layoutDetail;
    }
    catch(err)
    {
        displayErrorMsg("getFieldEntity", err);
    }
    
}



//功能：获得LayoutPart信息,以实体返回
//cellObj：存取LayoutPart信息的单元格对象
//其顺序按照数据库中其partID升序;column_count由外部遍历时决定
function getLayoutPartEntity(cellObj)
{
    try
    {
        var header          = cellObj.innerHTML;
        var sortOrder       = cellObj.sortOrder;
        var detailHeading   = cellObj.detailHeading;
        var editHeading     = cellObj.editHeading;
        var canEditLabel    = cellObj.canEditLabel;
        var master          = cellObj.masterLabel;
        var columnCount     = cellObj.columnCount;
        
        var layoutPart = new Object();
        layoutPart.PartName =  header;
        layoutPart.OrderMode = sortOrder;
        layoutPart.TitleIsShow = editHeading == "1";
        layoutPart.IsCanEdit = canEditLabel == "1";
        layoutPart.Master = master;
        layoutPart.ColumnCount = columnCount;
        
        return layoutPart;
    }
    catch(err)
    {
        displayErrorMsg("getLayoutPartEntity", err);
    }
    
}

//功能：获得一个layoutPart的所有信息,以实体返回
//layoutPartTableObj存放layoutPart所有信息的Table对象
//layoutPart存放本部分自身的信息
//layoutDetails存放该部分的所有布局字段信息，Array类型
 //返回本部分共有几个布局字段
function getLayoutPartAllEntity(layoutPartTableObj, layoutParts, layoutDetails, currentPartIndex)
{
    try
    {
        //存储Part自身信息的cell单元对象
        var layoutPartCellObj = layoutPartTableObj.rows[0].cells[0].childNodes[0].rows[0].cells[0];
        //从存储Part自身信息的cell单元对象获得Part的信息
        layoutParts[currentPartIndex] = getLayoutPartEntity(layoutPartCellObj);
        
        var layoutColumnsTableObj = layoutPartTableObj.rows[1].cells[0].childNodes[0];
        
        //每行存取的字段数
        var columnCount = 0;
        if (layoutColumnsTableObj.rows.length > 0)
        {
            columnCount = layoutColumnsTableObj.rows[0].cells.length;
        }
        
        //每行之前都有分离行，因此除2;最后一行为空白行，因此减一
        var fieldRowsCount = layoutColumnsTableObj.rows.length / 2 - 1;
        var fieldRow = null;
        var fieldCell = null;
        var i = 0;
        var j = 0;
        //遍历取得所有字段信息
        var currentColumnCount = 0;
        var k = 1;
        for (i = 0; i < fieldRowsCount; i++)
        {
            fieldRow = layoutColumnsTableObj.rows[2*i+1];
            
            for (j = 0; j < fieldRow.cells.length; j++)
            {
                fieldCell = fieldRow.cells[j];

                if (fieldCell.itemId != undefined && fieldCell.itemId != null && fieldCell.itemId != "" && fieldCell.style.backgroundColor != "#ffffff") {
                     var layoutDetail = new Object();
                     layoutDetail = getFieldEntity(fieldCell);
                     layoutDetail.OrderBy = k;
                     layoutDetails[currentColumnCount] = layoutDetail;
                     currentColumnCount++;
                }

                k++;
            }
        }

        //返回本部分共有几个布局字段
        return currentColumnCount;
    }
    catch(err)
    {
        displayErrorMsg("getLayoutPartAllStr", err);
    }
    
}


//功能：获得一个layoutPart的所有字段ID,以字符串返回
//layoutPartTableObj存放layoutPart所有信息的Table对象
function getLayoutPartFieldIDStr(layoutPartTableObj)
{
    try
    {
        var result = "";
       
        var layoutColumnsTableObj = layoutPartTableObj.rows[1].cells[0].childNodes[0];
        
        //每行存取的字段数
        var columnCount = 0;
        if (layoutColumnsTableObj.rows.length > 0)
        {
            columnCount = layoutColumnsTableObj.rows[0].cells.length;
        }
        
        //每行之前都有分离行，因此除2;最后一行为空白行，因此减一
        var fieldRowsCount = layoutColumnsTableObj.rows.length / 2 - 1;
        var fieldRow = null;
        var fieldCell = null;
        var fieldID = "";
        var i = 0;
        var j = 0;
        
        //遍历取得所有字段信息
        for (i = 0; i < fieldRowsCount; i++)
        {
            fieldRow = layoutColumnsTableObj.rows[2*i+1];
            
            for (j = 0; j < fieldRow.cells.length; j++)
            {
                fieldCell = fieldRow.cells[j];
                if (fieldCell.itemId != undefined && fieldCell.style.backgroundColor != "#ffffff")
                {
                    fieldID = fieldCell.itemId + ":" + fieldCell.id;
                }
                //以,分隔字段
                result += fieldID + ",";
            }
        }
        
        //去掉最后的,符号
        if (result.length > 0)
        {
            result = result.substring(0, result.length-1);
        }
        return result;
    }
    catch(err)
    {
        displayErrorMsg("getLayoutPartFieldIDStr", err);
    }
    
}

//功能：获得对应数据库中某布局的所有信息（包含部分及其所有字段信息）
//mainLayoutTableObj：对应页面中的mainLayoutTable对象
//layout布局的信息
//layoutParts该布局所有的部分信息
//layoutDetails该布局的所有布局字段信息
//partCounts该布局各个部分的布局字段个数
function getLayoutTable(mainLayoutTableObj, layout, layoutParts, layoutDetails, partCounts)
{
    try
    {
        var lenOfRows = mainLayoutTable.rows.length; 
        var totalParts =  (lenOfRows-2) / 3;  
        
        var i = 0;  
        for(i = 0; i < totalParts; i++) 
        {           
            var partTable = mainLayoutTable.rows[i*3+1].cells[0].childNodes[0]; 
            var partLayoutDetails = new Array();  
            partCounts[i] = getLayoutPartAllEntity(partTable, layoutParts, partLayoutDetails, i);
            for (j = 0; j < partLayoutDetails.length; j++)
            {
               layoutDetails[layoutDetails.length] = partLayoutDetails[j];
            }
        } 
        
        //layout自身的名称信息
        layout.LayoutName = document.getElementById("txtLayoutName").value;
    }
    catch(err)
    {
        displayErrorMsg("getLayoutTable", err);
    }
    
}

//功能：获得对应数据库中某数据库表的所有信息（包含部分及其所有字段信息）
//mainLayoutTableObj：对应页面中的mainLayoutTable对象
function getFieldIDsUsed(mainLayoutTableObj)
{
    try
    {
        var lenOfRows = mainLayoutTable.rows.length; 
        var result = new Array();
        var totalParts =  (lenOfRows-2) / 3;  
        
        var i = 0;  
        for(i = 0; i < totalParts; i++) 
        {           
            var partTable = mainLayoutTable.rows[i*3+1].cells[0].childNodes[0];    
            result[i] = getLayoutPartFieldIDStr(partTable);
        } 
        
        return result;
    }
    catch(err)
    {
        displayErrorMsg("getFieldIDsUsed", err);
    }
    
}

function setDataType()
{
    try
    {
        dataType = new Array();
        var dataTypeArray = UDEF.Service.AjaxService.SelectDataTypeAll().value;
        for (var i = 0; i < dataTypeArray.length; i++)
        {
            var deDataType  = dataTypeArray[i];
            dataType[deDataType.TypeID] =  deDataType.TypeText;
        }
    }
    catch(err)
    {
        displayErrorMsg("setDataType", err);
    }
}

//功能：将数据库中的字段实体插入到单元格对象中
//field：数据库中的字段实体
//cellObj：页面中存储字段信息的单元格对象
function addPrimaryKeyField(field, cellObj)
{ 
    try
    {
        var rq = field.Required;
        rq = (rq != null && rq) ? "1" : "0";
        var ro = field.Readonly;
        ro = (ro != null && ro) ? "1" : "0";
        var isForeign = field.IsForeign;
        isForeign = (isForeign != null && isForeign) ? "1" : "0";
        var ad = field.IsAlwaysDisplayed;
        ad = (ad != null && ad) ? "1" : "0";
        var aro = field.IsAwaysReadonly;
        aro = (aro != null && aro) ? "1" : "0";
        var arq = field.IsAlwaysRequired;
        arq = (arq != null && arq) ? "1" : "0";
        var length = field.Length;
        length = (length != null && length != undefined) ? length : 150;
        var typeLabel = dataType[field.DataType];
        setFieldAttributes(cellObj, field.ColumnID, field.ColumnText, field.ColumnText, length, 0, 0, typeLabel, "F", rq, ro, isForeign, ad, arq, aro, "0", "0", "0");
    }
    catch(err)
    {
        displayErrorMsg("addPrimaryKeyField", err);
    }  
    
}


//功能：将数据库中的字段实体插入到单元格对象中
//field：数据库中的字段实体
//cellObj：页面中存储字段信息的单元格对象
function addForeignKeyField(field, cellObj)
{  
    try
    {
        field.Required = false;
        field.Readonly = true;
        field.IsAlwaysDisplayed = false;
        field.IsAwaysReadonly = true;
        field.IsAlwaysRequired = false;
        field.IsForeign = true;
        addPrimaryKeyField(field, cellObj);
    }
    catch(err)
    {
        displayErrorMsg("addForeignKeyField", err);
    } 
    
    
}

//功能：插入Part的所有信息
//partID：数据库字段
function insertSectionAll(layoutPart) 
{
    try
    {
        //获得该部分自身的信息
        var sectionText = layoutPart.PartName;
        var c = layoutPart.ColumnCount;
        var sortOrder = layoutPart.OrderMode;
        var detailHeading = layoutPart.TitleIsShow;
        detailHeading = detailHeading ? "1" : "0";
        var editHeading = layoutPart.IsCanEdit;
        editHeading = editHeading ? "1" : "0";
        var numColumns = parseInt(c);

        //该部分的所有列字段的信息
        var columnList = new Array();
        columnList = UDEF.Service.AjaxService.SelectLayoutDetailByPartID(layoutPart.PartID).value;
        var fieldCount = columnList.length;
        var cellCount = 0;
        var rowCount = 0;

        if (fieldCount > 0)
        {
            var lastColumn = columnList[fieldCount-1];
            cellCount = lastColumn.OrderBy - 0;
        }
        if (numColumns != 0)
        {
            rowCount = Math.ceil(cellCount/numColumns);  
        }
        maxSectionTable++;
        maxSection++;
        var sectionName = "s" + maxSection;
        var tableName = "table" + maxSectionTable;
        var newSection = '';
        newSection += '<table cellspacing=1 cellpadding=2 style="background-color:cccccc" width="400" id="' + tableName + '">';
        newSection +=   '<tr style="background-color:#99CCFF"><td>';
        newSection +=       '<table border="0" cellspacing="0" cellpadding="0" width="100%">';
        newSection +=           '<tr valign=bottom ><td style="color:#000000" id="' + getSectionHeaderId(sectionName) + '"  align="left" width="99%">' + sectionText + '</td>';
        newSection +=               '<td align=right nowrap>';
        newSection +=                   '<a onmouseover="handleLinkMouseOver(event);" onmousedown="doNothing(event);" onclick="openSectionEdit(\'' +  getSectionHeaderId(sectionName) + '\', event);">';
        newSection +=                       '编辑';
        newSection +=                   '</a>';
        newSection +=                   ' | ';
        newSection +=                   '<a onmouseover="handleLinkMouseOver(event);" onmousedown="doNothing(event);" onclick="deleteSection(\'' + getSectionHeaderId(sectionName) + '\');">';
        newSection +=                       '删除';
        newSection +=                   ' </a>';
        newSection +=               ' </td></tr>';
        newSection +=       '</table></td>';
        newSection +=   '</tr>';
        newSection +=   '<tr style="background-color:FFFFFF" height=15>';
        newSection +=       '<td>';
        newSection +=           '<table id="' + sectionName + '" width="100%" cellspacing=2 bgcolor="FFFFFF">';
        //应多出一行，以获得焦点
        for (var r = 1; r <= rowCount + 1; r++)
        {
        newSection +=               '<tr height=2>';
            for (var i = 0; i < numColumns; i++) 
            {
                newSection +=          '<td id="' + getSeparatorId(constructId(sectionName, r, i+1)) + '" width="200"  onmouseover="handleMouseOver(event);" onmouseout="handleMouseOut(event)"></td>';
            }
        newSection +=               '</tr>';
        newSection +=               '<tr height=10>';
            for (var i = 0; i < numColumns; i++) 
            {
                newSection +=          '<td id="' + constructId(sectionName, r, i+1) + '"></td>';
            }
        newSection +=               '</tr>';
        }
        newSection +=           '</table>';
        newSection +=       '</td>';
        newSection +=   '</tr>';
        newSection += '</table>';
        
        insertSectionRow('table0', tableName, newSection);
        initSectionTable(document.getElementById(getSectionHeaderId(sectionName)), tableName, sortOrder, detailHeading, editHeading, '1', '0', '', c);
        initSection(document.getElementById(sectionName));
        
        var currentIndex = 0;
        for (var r = 1; r <= rowCount; r++)
        {
            for (var i = 0; i < numColumns; i++)
            {
                var field = columnList[currentIndex];
                if (field != null && field.OrderBy == (r-1) * numColumns + i + 1)
                {
                    var cellId = constructId(sectionName, r, i+1);
                    var cellObj = document.getElementById(cellId);
                    if (field.IsForeign)
                    {
                        addForeignKeyField(field, cellObj);
                    }
                    else
                    {
                        addPrimaryKeyField(field, cellObj);
                    }
                    currentIndex++;
                }
            }
        }
        
        reformatTable(document.getElementById(sectionName));
        
        
        // NN does not suppport focus on just any elements
        if (isIE) document.getElementById(sectionName).focus();
    }
    catch(err)
    {
        displayErrorMsg("insertSectionAll", err);
    }
    
}

//功能：将数据库中ID为LayoutID的表的所有Part及相关的字段信息插入到table对象
//LayoutID：数据库中对应的数据库表索引
function addLayoutAll(LayoutID)
{
    try
    {
        if (LayoutID != null && LayoutID > 0)
        {
            var layoutPartList = new Array();
            layoutPartList =  UDEF.Service.AjaxService.SelectLayoutPartByLayoutID(LayoutID).value; 
            //获得布局的名称
            var layoutName =  UDEF.Service.AjaxService.GetLayoutName(LayoutID).value;
            document.getElementById("txtLayoutName").value = layoutName; 
            //初始化布局的所有layoutPart
            for (var i = 0; i < layoutPartList.length; i++)
            {
                var layoutPart = layoutPartList[i];
                insertSectionAll(layoutPart); 
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("addLayoutAll", err);
    }
    
}

//功能：创建存放视图所有字段信息的Table对象
//LayoutID：对应数据库中的表ID
//columnCount：每行的字段数
//isForeign：是否外键表
function createViewTable(tableID, columnCount, isForeign)
{
    try
    {
        //创建一个table对象
        var table = document.createElement("table");
       
        //设置该table的属性
        table.id = "afields_1";
        table.cellspacing = "2px";
        table.cellpadding = "2px";
        table.width = "100%";
        table.bgcolor = "#ffffff";
        
        var elementTBody = document.createElement("tBody");
        table.appendChild(elementTBody);
        
        var fieldList = new Array();
        fieldList = UDEF.Service.AjaxService.SelectColumnByTableID(tableID).value;
        var rowCount = Math.ceil(fieldList.length / columnCount);
        for (var i = 1; i <= rowCount; i++)
        {
            var row1 = table.insertRow();
            row1.height = "2px";
            var row2 = table.insertRow();
            row2.height = "10px";
            for (var j = 1; j <= columnCount; j++)
            {
                var cell1 = row1.insertCell();
    //                var cell2 = row2.insertCell();
                //var cell2 = document.createElement("<td onmouseover=\"handleMouseOver(event);\" onmouseout=\"handleMouseOut(event);\"></td>");
                var cell2 = document.createElement("td");
                cell2.onmouseover = function () { handleMouseOver(event); }
                cell2.onmouseout = function () { handleMouseOut(event); };
                cell1.id = "rp_" + table.id + "r" + i + "c" + j;
                cell1.width = "200px";
                
                cell2.id = table.id + "r" + i + "c" + j;
                cell2.width = "200px";
                var index = (i-1)*columnCount+j-1;
                if (index < fieldList.length)
                {
                    var field = fieldList[index];
                    if (isForeign)
                    {
                        addForeignKeyField(field, cell2);
                    }
                    else
                    {
                        addPrimaryKeyField(field, cell2);
                    }
                    row2.appendChild(cell2);
                }
            }
        }
          
        return table;
    }
    catch(err)
    {
        displayErrorMsg("createViewTable", err);
    }
    
}

//功能：显示所选表的所有字段
//当字段来源下拉列表变化时触发
function handleTableChange(selObj)
{
    try
    {
        var tableID = selObj.value;
        var viewTableObj = document.getElementById("afields_1");
        var columnCount = 2;
        //删除最后两行
        viewTableObj.deleteRow(viewTableObj.rows.length-1);
        viewTableObj.deleteRow(viewTableObj.rows.length-1);
        
        //重新标识已用字段
        resetFieldUsed();
        var isForeign = selObj.selectedIndex > 0;
        addTableFields(tableID, viewTableObj, columnCount, isForeign);
    }
    catch(err)
    {
        displayErrorMsg("handleTableChange", err);
    }
    
}

//功能：将ID为tableID的表的所有外键表添加到下拉框中
function addFieldSourceTable(tableID, selObj)
{
    try
    {
        tableID_TableName[tableID] = "本表";
        var foreignKeyTables = new Array();
        foreignKeyTables =  UDEF.Service.AjaxService.SelectForeignKeyTableByTableID(tableID, "").value;
        for (var i = 0; i < foreignKeyTables.length; i++)
        {
            var sysTable = foreignKeyTables[i];
            selObj.options[i+1] = new Option(sysTable.TableText, sysTable.TableID);
            //在showProperties中用到
            tableID_TableName[sysTable.TableID] = sysTable.TableText;
        }
        selObj.options[0].value = tableID;
    }
    catch(err)
    {
        displayErrorMsg("addFieldSourceTable", err);
    }
    
}

//功能：当表切换时，重新标识已选的字段
function resetFieldUsed()
{
    try
    {
        var mainLayoutTable = document.getElementById("mainLayoutTable");
        var result = new Array();
        result = getFieldIDsUsed(mainLayoutTable);
        
        initUsedFields = new Array();
        for (var i = 0; i < result.length; i++)
        {
            var fields = result[i].split(",");
            for (var j = 0; j < fields.length; j++)
            {
                fieldDetails = fields[j].split(":");
                initUsedFields[fieldDetails[0]] = fieldDetails[1];
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("resetFieldUsed", err);
    }
    
}

//功能：初始化右边供选择的字段视图
function addViewAll(tableID, viewTableObj, columnCount, selObj)
{
    try
    {
        //将ID为tableID的表的所有外键表添加到下拉框中
        addFieldSourceTable(tableID, selObj);
        
        //把相应表的所有字段插入页面中的viewTableObj中
        addTableFields(tableID, viewTableObj, columnCount, false);
    }
    catch(err)
    {
        displayErrorMsg("addViewAll", err);
    }
    
}

//功能：把相应表的所有字段插入页面中的viewTableObj中
//tableID：数据库中的表ID
//viewTableObj：页面中的存放视图的表对象
//columnCount：每行的字段数
//isForeign：是否外键表
function addTableFields(tableID, viewTableObj, columnCount, isForeign)
{
    try
    {
        var row1 = viewTableObj.insertRow();
        
        row1.bgcolor = "#cccccc";
        row1.valign = "bottom";
        var cell1 = row1.insertCell();
        cell1.align = "middle";
        cell1.colspan = "2";
        
        var contentRow = viewTableObj.insertRow();
        contentRow.bgcolor = "#ffffff";
        contentRow.height = "15px";
        
        var contentCell = contentRow.insertCell();
        
        var table = createViewTable(tableID, columnCount, isForeign);
        
        contentCell.appendChild(table);
    }
    catch(err)
    {
        displayErrorMsg("addTableFields", err);
    }
    
    
}

//功能：更新layout
function UpdateLayoutAll()
{
    try
    {
        var LayoutID = getUrlParameter("LayoutID");
        var mainLayoutTable = document.getElementById("mainLayoutTable");
        var layout = new Object();
        var layoutParts = new Array();
        var layoutDetails = new Array();
        var partColumnCounts = new Array();
        getLayoutTable(mainLayoutTable, layout, layoutParts, layoutDetails, partColumnCounts); 
        layout.LayoutID = LayoutID;
        UDEF.Service.AjaxService.UpdateLayoutAll(layout, layoutParts, layoutDetails, partColumnCounts, FinishSave_CallBack);
    }
    catch(err)
    {
        displayErrorMsg("UpdateLayoutAll", err);
    }

} 

//功能：插入layout
function InsertLayoutAll()
{
    try
    {
        var TableID = getUrlParameter("TableID");
        var mainLayoutTable = document.getElementById("mainLayoutTable");
        var result = new Array();
        var layout = new Object();
        var layoutParts = new Array();
        var layoutDetails = new Array();
        var partColumnCounts = new Array();
        getLayoutTable(mainLayoutTable, layout, layoutParts, layoutDetails, partColumnCounts);
        layout.TableID = TableID;
        
        UDEF.Service.AjaxService.InsertLayoutAll(layout, layoutParts, layoutDetails, partColumnCounts, FinishSave_CallBack);
    }
    catch(err)
    {
        displayErrorMsg("InsertLayoutAll", err);
    }

}

function FinishSave_CallBack(response)
{
    try
    {
        if(response.error != null)
	    {
		    alert("页面布局保存失败，请再试一次，如果不成功，请与管理员联系！");
		    return;
	    }
	    else
	    {
		    alert("页面布局保存成功！");
		    var tableID = getUrlParameter("TableID");
		    backToList(tableID);
	    }
    }
    catch(err)
    {
        displayErrorMsg("FinishSave_CallBack", err);
    }
}


 
function loadAll()
{  
    try
    {
        //将所有数据类型存于关联数组dataType中,方便之后引用
        setDataType();
        
        var TableID = getUrlParameter("TableID");
        var editMode = getUrlParameter("EditMode");
        var LayoutID = editMode == "add" ? UDEF.Service.AjaxService.GetstandardLayoutID(TableID).value : getUrlParameter("LayoutID");
        
        //设置好        
        var foreignKeyTables = new Array();
        foreignKeyTables =  UDEF.Service.AjaxService.SelectForeignKeyTableByTableID(TableID).value;
        setColumnID_tableIDRelation(TableID, foreignKeyTables);
        
        var viewTableObj = document.getElementById("afields_1");
        var selObj = document.getElementById("ddlTable");
        addLayoutAll(LayoutID);
        addViewAll(TableID, viewTableObj, 2, selObj);
        if (editMode == "add")
        {
            var txtLayoutName = document.getElementById("txtLayoutName");
            txtLayoutName.value = "布局模板";
            txtLayoutName.select();
        }
        


    }
    catch(err)
    {
        displayErrorMsg("loadAll", err);
    }
}
  
function saveAll()
{
    try
    {
        var LayoutID = getUrlParameter("LayoutID");
        var editMode = getUrlParameter("EditMode");
        if (editMode == "add" || editMode == "copy")
        {
            InsertLayoutAll();
        }
        else
        {
            UpdateLayoutAll();
        }
    }
    catch(err)
    {
        displayErrorMsg("saveAll", err);
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

function backToList(TableID)
{
    if (getUrlParameter("GoBackURL") == "" || getUrlParameter("GoBackURL") == null)
    {
        window.location = "ListLayout.aspx?TableID=" + TableID;
    }
    else
    {
        window.opener.location.reload();
        window.close();
    }
}