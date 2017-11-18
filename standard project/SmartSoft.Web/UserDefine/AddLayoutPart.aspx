<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AddLayoutPart.aspx.cs"
    Inherits="UDEF.Web.UserDefine.AddLayoutPart" %>

<html>
<head id="Head1" runat="server">
    <title>��������</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script>
    var isSelfService = '1' == window.opener.isSelfService;		
	var isCaseClose = '1' == window.opener.isCaseClose;

    var secName = '';
    var sortOrder = 'h';
    var detailHeading = '1';
    var editHeading = '1';
    var numColumns = 2;
    var canEditLabel = '1';
    var isStandardSection = true;
    function save() {
        if (!isValid()) return;
        var editForm = document.forms['editForm'];
        var newNumColumns = editForm.numCols.options[editForm.numCols.selectedIndex].value;
        var sectionSortOrder = editForm.sOrder.options[editForm.sOrder.selectedIndex].value;
        if (isSelfService) {
            window.opener.insertSection(editForm.sName.value, newNumColumns, sectionSortOrder,'0', '0');
		} else if (isCaseClose) {
			window.opener.insertSection(editForm.sName.value, newNumColumns, sectionSortOrder,'0', editForm.sEditHeading.checked ? '1' : '0');
        } else {
            window.opener.insertSection(editForm.sName.value, newNumColumns, sectionSortOrder,editForm.sDetailHeading.checked ? '1' : '0', editForm.sEditHeading.checked ? '1' : '0');
        }
        window.close();
    }
        
    
 
    </script>

    <meta content="MSHTML 6.00.3790.186" name="GENERATOR">
</head>
<body>
    <form name="editForm">
        <table class="TableMain" cellpadding="0" cellspacing="0">
            <tr class="ToolBar" style="height:50px;">
                <td>
                    �������ֲ���
                </td>
                <td>
                         <a onclick="javascript:window.close();"
                                id="lb_Search" class="lbclass" href="javascript:window.close();">
                                <img src="../images/img_newclose.png" alt="" />�ر�</a>
                                <a id="lb_Add" class="lbclass" href="javascript:save();">
                            <img src="../images/img_160.png" alt="" />����</a>
               </td>
            </tr>
            <tr>
                <td colspan="2">
                    ����:
                    <input name="sName" style="width:300px"/>
                </td>
            </tr>
        </table>
        <table class="TablePanel">
            <tr>
                <th style="width:200px">
                    ��:</th>
                <td>
                    <select onchange="handleColumnNumChange();" name="numCols">
                        <option value="1" selected>1��������</option>
                        <option value="2">2��������</option>
                        <option value="3">3��������</option>
                        <option value="4">4���ı���</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th style="width:200px">
                    ѡ�����:</th>
                <td>
                    <select name="sOrder">
                        <option value="h" selected>����</option>
                        <option value="v">����</option>
                    </select>
                </td>
            </tr>

            <script>
        if (!isSelfService) {
			if (!isCaseClose) {
	            document.write(
	                '<tr>' +
	                    '<th style="width:200px"> ����ϸ��Ϣҳ����ʾ���ֱ���:</td>'+
	                    '<td><input type=checkbox name=sDetailHeading></td>' +
	                '</tr>');
			}
			document.write(
                '<tr>' +
                    '<th style="width:200px"> �ڱ༭ҳ����ʾ���ֱ���:</td>' +
                    '<td><input type=checkbox name=sEditHeading></td>' +
                '</tr>');
        }
            </script>

            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
        <br>

        <script>
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
                alert('�������벿������');
                valid = false;
            }
        }
        return valid;
    }
//    function handleNameChange(newValue) {
//        document.getElementById('header').innerHTML = newValue;
//    }
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
