<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditLayoutDetail.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditLayoutDetail" %>

<html>
<head>
    <title>字段信息</title>
    <style type="text/css">
        BODY { MARGIN: 0px }
		</style>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript"> 
    var selectedFields = window.opener.selectedBucket;
    </script>

</head>
<body>
    <form name="editForm">
        <table class="TableMain" cellpadding="0" cellspacing="0">
            <tr class="ToolBar" style="height:50px;">
                <td>
                    编辑布局字段属性
                </td>
                    <td>
                        <a id="lb_Search" class="lbclass"
                                href="javascript:window.close();">
                                <img src="../images/img_back2_25.png" alt="" />返回</a>
                          <a id="lb_Add" class="lbclass" href="javascript:save();">
                            <img src="../images/img_160.png" alt="" />保存</a>
                    </td>
            </tr>
        </table>
        <table class="TablePanel">
            <tbody>
                <tr>
                    <td>
                        项目名</td>
                    <td>
                        只读</td>
                    <td>
                        必填项</td>
                    <td>
                        长度</td>
                </tr>

                <script type="text/javascript">
    if (selectedFields.length > 1) {
        document.write('<tr bgcolor="AAAACC">');
        document.write('<th>');
        document.write('<b>');
        document.write('全操作');
        document.write('</b></th>');
        document.write('<td><input type=checkbox onclick="toggleReadonly(this.checked);" name="ro"></td>');
        document.write('<td><input type=checkbox onclick="toggleRequired(this.checked);" name="rq"></td>');
        document.write('<td><input type=text onblur="toggleLengthInput(this);" name="l" onKeyPress="javascript:return controlNumberKeyPress(this);" onfocus="this.select();"></td>');
        document.write('</tr>');
    }
    for (var i = 0; i < selectedFields.length; i++) {
        var field = selectedFields[i];
        document.write('<tr>');
        document.write('<th>');
        document.write(window.opener.escapeJS(field.fName));
        document.write('</th>');
        document.write('<td>');
        var alwaysReadonly = field.aro && field.aro == '1';
        var alwaysRequired = field.arq && field.arq == '1';
		var alwaysNotRequired = field.anrq && field.anrq == '1';
		var alwaysNotReadonly = field.anro && field.anro == '1';

        if (field.fieldType == 'F' || field.fieldType == 'C') {
            var readonlyChecked = field.ro && field.ro == '1' && !alwaysNotReadonly;
            document.write('<input type=checkbox name=ro_' + field.id + ' id=ro_' + field.id + ' ' + (readonlyChecked ? 'checked' : '') + ' onclick=setReadonly("' + field.id + '",this.checked); >');
            if (alwaysReadonly || alwaysRequired || alwaysNotReadonly) {
                document.getElementById('ro_'+field.id).disabled = true;
            }
        } else {
            document.write('无');
        } 
        document.write('</td>');
        document.write('<td>');
        if (field.fieldType == 'F' || field.fieldType == 'C') {
            var requiredChecked = field.rq && field.rq == '1' && !alwaysNotRequired;
            document.write('<input type=checkbox name=rq_' + field.id + ' id=rq_' + field.id + ' ' + (requiredChecked ? 'checked' : '') + ' onclick=setRequired("' + field.id + '",this.checked);>');
            if (alwaysReadonly || alwaysRequired || alwaysNotRequired) {
                document.getElementById('rq_'+field.id).disabled = true;
            }
        } else {
            document.write('无');
        }
        document.write('</td>');
        document.write('<td>');
        document.write('<input type=text name=l_' + field.id + ' id=l_' + field.id + ' ' + ' value=' +  field.fLength + ' onkeypress="return controlNumberKeyPress(this);" onfocus="this.select();" style="TEXT-ALIGN:right" onpaste="return !clipboardData.getData(\'text\').match(/\D/);" onblur="javascript:controlNumberOnblur(this);">');
        document.write('</td>');
        document.write('</tr>');

    }

                </script>

                <tr>
                    <td bgcolor="#ffffff" colspan="4" height="30px">
                    </td>
                </tr>
            </tbody>
        </table>
        <br>

        <script type="text/javascript">
    function setReadonly(fieldId, wasChecked) {
        if (wasChecked) {
            document.forms['editForm'].elements['rq_' + fieldId].checked = false;
        }
        if (selectedFields.length > 1) {
            document.forms['editForm'].elements['rq'].checked = false;
            document.forms['editForm'].elements['ro'].checked = false;
        }
    }
    
    function setRequired(fieldId, wasChecked) {
        if (wasChecked) {
            document.forms['editForm'].elements['ro_' + fieldId].checked = false;
        }
        if (selectedFields.length > 1) {
            document.forms['editForm'].elements['rq'].checked = false;
            document.forms['editForm'].elements['ro'].checked = false;
        }
    }
    
    function toggleReadonly(wasChecked) {
        var editForm = document.forms['editForm'];
        for (var i = 0; i < selectedFields.length; i++) {
            var field = selectedFields[i];
            var otherElem = editForm.elements['ro_' + field.id];
            if (otherElem && !otherElem.disabled) {
                otherElem.checked = wasChecked;
                if (wasChecked) {
                    editForm.elements['rq_' + field.id].checked = false;
                }
            }
        }   
        if (wasChecked && selectedFields.length > 1) {
            editForm.elements['rq'].checked = false;
        }
    }
    
    function toggleRequired(wasChecked) {
        var editForm = document.forms['editForm'];
        for (var i = 0; i < selectedFields.length; i++) {
            var field = selectedFields[i];
            var otherElem = editForm.elements['rq_' + field.id];
            if (otherElem && !otherElem.disabled) {
                otherElem.checked = wasChecked;
                if (wasChecked) {
                    editForm.elements['ro_' + field.id].checked = false;
                }
            }
        }   
        if (wasChecked && selectedFields.length > 1) {
            editForm.elements['ro'].checked = false;
        }
    }
    
    function toggleLengthInput(textbox) {
    
        if (validLengthInput(textbox))
        {
            var editForm = document.forms['editForm'];
            for (var i = 0; i < selectedFields.length; i++) 
            {
                var field = selectedFields[i];
                var inputElem = editForm.elements['l_' + field.id];
                inputElem.value = textbox.value;
            } 
        }  
    }
    
    function toggleInlined(wasChecked) {
        var editForm = document.forms['editForm'];
        for (var i = 0; i < selectedFields.length; i++) {
            var field = selectedFields[i];
            if (editForm.elements['il_' + field.id] && !editForm.elements['il_' + field.id].disabled) {
                editForm.elements['il_' + field.id].checked = wasChecked;
            }
        }   
    }
    
    
    function validLengthInput(textbox)
    {
        var length = parseFloat(textbox.value);
        if (length > 1000 || length < 20)
        {
            alert("输入范围应在20~1000之间！");
            textbox.value = "150";
            textbox.select();
            return false;
        } 
        return true;
    }
    
    function save() {
        var editForm = document.forms['editForm'];
        for (var i = 0; i < selectedFields.length; i++) {
            var field = selectedFields[i];
            var readonlyInput = editForm.elements['ro_' + field.id];
            var requiredInput = editForm.elements['rq_' + field.id];
            var lengthInput = editForm.elements['l_' + field.id];
            if (readonlyInput) {
                window.opener.setCellReadonlyNess(field, readonlyInput && readonlyInput.checked);
            }
            if (requiredInput) {
                window.opener.setCellRequiredNess(field, requiredInput && requiredInput.checked); 
            }
            window.opener.setCellLengthNess(field, lengthInput.value);
            window.opener.formatField(field);
        }
        window.close();
    }
        </script>

    </form>
</body>
</html>
