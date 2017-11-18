<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditLayoutPart.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditLayoutPart" %>

<html>
<head id="Head1" runat="server">
    <title>布局块 </title>
    <meta http-equiv='pragma' content='no-cache'>
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate'>
    <meta http-equiv='expires' content='-1'>

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var isSelfService = '1' == window.opener.isSelfService;		
	    var isCaseClose = '1' == window.opener.isCaseClose;

        var secId = getUrlParameter("sectionId");
        var tableId = window.opener.getTableIdFromSectionId(secId);
        var section = window.opener.document.getElementById(secId);
        var secName = section.innerHTML;
        var sortOrder = section.sortOrder;
        var detailHeading = section.detailHeading;
        var editHeading = section.editHeading;
        var canEditLabel = section.canEditLabel;
        var numColumns = section.columnCount;
        var isStandardSection = window.opener.isStandardSection(secId);
        function windowClose()
        {
            window.close();
        }
        function mySave() 
        {
            try
            {
                if (!isValid()) return;
                            
                if (canEditLabel == '1') 
                {
                    var sectionNameValue = document.forms['editForm'].sName.value;
                    section.innerHTML = sectionNameValue; 
                    section.masterLabel = sectionNameValue;
                }
                if (!isSelfService && !isCaseClose) 
                {
                    section.detailHeading = document.forms['editForm'].sDetailHeading.checked ? '1' : '0';
                }

                if(isStandardSection) 
                {
                    section.sortOrder = document.forms['editForm'].sOrder.options[document.forms['editForm'].sOrder.selectedIndex].value;
                    if (!isSelfService) 
                    {
                        section.editHeading = document.forms['editForm'].sEditHeading.checked ? '1' : '0';
                    }
                    var newNumColumns = document.forms['editForm'].numCols.options[document.forms['editForm'].numCols.selectedIndex].value;
                    if (parseInt(newNumColumns) != numColumns) 
                    {
                        section.columnCount = newNumColumns;
                        window.opener.toggleColumns(secId, newNumColumns);
                    }
                }
                window.close();
            }
            catch(err)
            {
                displayErrorMsg("mySave", err);
            }
        }
        
 
    </script>

    <meta content="MSHTML 6.00.3790.186" name="GENERATOR">
</head>
<body>
    <form name="editForm">
        <table class="TableMain">
            <tr>
                <td colspan="2">
                    修改布局部分
                </td>
            </tr>
            <tr class="ToolBar">
                <tr>
                    <td colspan="2">
                        <a id="ToolBar1_lb_Save" class="lbclass" href="javascript:mySave();">
                            <img src="../@images/save.gif" alt="" />保存</a> <a onclick="javascript:window.close();"
                                id="ToolBar1_lb_Close" class="lbclass" href="javascript:window.close();">
                                <img src="../@images/close.gif" alt="" />关闭</a>
                    </td>
                </tr>
            </tr>
            <tr>
                <td colspan="2">
                    名称:
                    <input name="sName" style="width:300px"/>
                </td>
            </tr>
        </table>
        <table class="TablePanel">
            <tbody>
                <tr>
                    <th>
                        列:
                    </th>
                    <td>
                        <select onchange="handleColumnNumChange();" name="numCols">
                            <option value="1" selected>1（单倍）</option>
                            <option value="2">2（两倍）</option>
                            <option value="3">3（三倍）</option>
                            <option value="4">4（四倍）</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        选项卡排序:
                    </th>
                    <td>
                        <select name="sOrder">
                            <option value="h" selected>左右</option>
                            <option value="v">上下</option>
                        </select>
                    </td>
                </tr>

                <script>
        if (!isSelfService) {
			if (!isCaseClose) {
	            document.write(
	                '<tr>' +
	                    '<th> 详细信息页显示部分标题:</td>'+
	                    '<td><input type=checkbox name=sDetailHeading></td>' +
	                '</tr>');
			}
			document.write(
                '<tr>' +
                    '<th> 编辑页显示部分标题:</td>' +
                    '<td><input type=checkbox name=sEditHeading></td>' +
                '</tr>');
        }
                </script>

                <tr>
                    <td colspan="2">
                    </td>
                </tr>
            </tbody>
        </table>
        <br />

        <script type="text/javascript">
    document.forms['editForm'].sName.value = secName.replace(/&amp;/g, '&').replace(/&lt;/g,'<').replace(/&gt;/g,'>');
    
    if (canEditLabel != '1') {
        document.forms['editForm'].sName.disabled = true;   
    }   
    if (sortOrder && sortOrder == 'h') {
        document.forms['editForm'].sOrder.selectedIndex = 0;
    } else {
        document.forms['editForm'].sOrder.selectedIndex = 1;
    }
    if (numColumns == 1) {
        document.forms['editForm'].numCols.selectedIndex = 0;
    } else if (numColumns == 2) {
        document.forms['editForm'].numCols.selectedIndex = 1;
    }
    else if (numColumns == 3) {
        document.forms['editForm'].numCols.selectedIndex = 2;
    }
        
    if (!isSelfService) {
		if (!isCaseClose) {
	        if (detailHeading == 1) {
	            document.forms['editForm'].sDetailHeading.checked = true;
	        }
		}
        if (editHeading == 1) {
            document.forms['editForm'].sEditHeading.checked = true;
        }
    }


    if (canEditLabel != '1') {
        document.forms['editForm'].sOrder.focus();
    } else {
        document.forms['editForm'].sName.focus();
    }

    if (!isStandardSection) {
        document.forms['editForm'].sOrder.disabled = true;
        document.forms['editForm'].numCols.disabled = true;
        if (!isSelfService) document.forms['editForm'].sEditHeading.disabled = true;
    }
    function isValid() {
        var valid = true;
        
        if (!document.forms['editForm'].sName.disabled){
            var sectionName = document.forms['editForm'].sName.value;
            sectionName = sectionName.replace(/\s/g, '');
            if (sectionName.length == 0) {
                alert('必须输入部分名称');
                valid = false;
            }
        }
        return valid;
    }
    
    function handleNameChange(newValue) {
        document.getElementById('header').innerHTML = newValue;
    }
    
    function handleColumnNumChange() {
        var sIndex = document.forms['editForm'].numCols.selectedIndex;
        if (sIndex == 0) {
			document.forms['editForm'].sOrder.selectedIndex = 1;
            document.forms['editForm'].sOrder.disabled = true;
        } else if (top.isStandardSection) {
            document.forms['editForm'].sOrder.disabled = false;
        }

    }
    handleColumnNumChange();    
        </script>

    </form>
</body>
</html>
