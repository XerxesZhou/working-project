$(document).ready(function () {
    var today = new Date();
    today.getDate();
    $("#cfhTimeStart").val(today.getDate());
});

function enableButton() {
    $(currentButton).removeAttr("disabled");
    dd.device.notification.hidePreloader({
        onSuccess: function (result) {
            /*{}*/
        },
        onFail: function (err) { }
    })
}

var currentButton;
function disableButton(ctr) {
    currentButton = ctr;
    $(currentButton).attr("disabled", "disabled");
    dd.device.notification.showPreloader({
        text: "处理中...", //loading显示的字符，空表示不显示文字
        showIcon: true, //是否显示icon，默认true
        onSuccess: function (result) {
            /*{}*/
        },
        onFail: function (err) { }
    })
}

var code = "";

var CurrentOperatorID = $("#hfCurrentOperatorID").val();

function ShowSuccessMessage() {
    enableButton();
    dd.device.notification.toast({
        icon: 'success',
        text: "操作成功",
        duration: 2,
        delay: 0,
        onSuccess: function (result) { }
    });
}

function ShowErrorMessage() {
    enableButton();
    ShowMessage("操作失败");
}

function ShowMessage(message) {
    dd.device.notification.toast({
        icon: 'error', //icon样式，有success和error，默认为空 0.0.2
        text: message, //提示信息
        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
        delay: 0, //延迟显示，单位秒，默认0
        onSuccess: function (result) { }
    });
}

function SaveCustomerInfo() {
    var cusID = $("#hfKeyID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var cusdata = "{cusID:" + cusID + ",cusName:'" + $("#txtcusName").val() + "',cusKindID:" + $("#ddlcusKindID").val() + ",cusSourceID:" + $("#ddlcusSourceID").val() + ",cusIntroduction:'" + $("#txtcusIntroduction").val() + "',cusAddress:'" + $("#txtcusAddress").val() + "',cusDepartmentID:" + $("#ddlcusDepartmentID").val() + ",cusOperatorID:" + $("#ddlcusOperatorID").val() + ",cusLongtitude:'" + $("#txtcusLongtitude").val() + "',cusLatitude:'" + $("#txtcusLatitude").val() + "',cusContactor:'" + $("#txtcusContactor").val() + "',cusTel:'" + $("#txtcusTel").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "',cusExtType2:'" + $("#ddlcusExtType2").val() + "',cusAreaID:'" + $("#ddlcusAreaID").val() + "',cusCN:'" + $("#txtcusCN").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveCustomerInfo",
        data: cusdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var id = getUrlParameter("ID");
                if (id == 0) {
                    location.href = "CustomerEditForm.aspx?Action=View&ID=" + result.d;
                }
                else {
                    RefreshCustomerInfo();
                    ShowListCommon('divCustomerView', null);
                }
            }
            else {
                ShowErrorMessage();
            }
        }
    });
}

function AddCustomerInfo() {
    var cusID = $("#hfcusID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var OpptunityID = $("#hfOpptunityID").val();
    var ClueID = $("#hfClueID").val();
    var cusdata = "{cusID:" + cusID + ",cusName:'" + $("#txtcusName").val() + "',cusKindID:" + $("#ddlcusKindID").val() + ",cusSourceID:" + $("#ddlcusSourceID").val() + ",cusIntroduction:'" + $("#txtcusIntroduction").val() + "',cusAddress:'" + $("#txtcusAddress").val() + "',cusDepartmentID:" + $("#ddlcusDepartmentID").val() + ",cusOperatorID:" + $("#ddlcusOperatorID").val() + ",cusLongtitude:'" + $("#txtcusLongtitude").val() + "',cusLatitude:'" + $("#txtcusLatitude").val() + "',cusContactor:'" + $("#txtcusContactor").val() + "',cusTel:'" + $("#txtcusTel").val() + "',clmSex:'" + $("#ddlLinkManSex").val() + "',clmMobile:'" + $("#txtLinkManMobile").val() + "',coName:'" + $("#txtOpptunity").val() + "',coPhaseID:'" + $("#ddlcoPhaseID").val() + "',coPredictAmount:'" + $("#txtcoPredictAmount").val() + "',coPredictFinishDate:'" + $("#txtcoPredictFinishDate").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "',cusExtType2:'" + $("#ddlcusExtType2").val() + "',cusAreaID:'" + $("#ddlcusAreaID").val() + "',OpptunityID:'" + OpptunityID + "',ClueID:'" + ClueID + "',cusCN:'" + $("#txtcusCN").val() + "'}";

    var cusName = $("#txtcusName").val();
    if (cusName == "") {
        ShowMessage("请输入名称！");
        return;
    }
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/AddCustomerInfo",
        data: cusdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                document.location.href = "CustomerList.aspx";
            }
            else {
                ShowErrorMessage();
            }
        }
    });
}

function RefreshCustomerInfo() {
    $("#lblcusName").html($("#txtcusName").val());
    $("#lblcusKindID").html($("#ddlcusKindID option:selected").text());
    $("#lblcusSourceID").html($("#ddlcusSourceID option:selected").text());
    $("#lblcusIntroduction").html($("#txtcusIntroduction").val());
    $("#lblcusContactor").html($("#txtcusContactor").val());
    $("#lblcusTel").html($("#txtcusTel").val());
    $("#linkcusTel").attr("href", "tel:" + $("#txtcusTel").val());
    $("#lblcusAddress").html($("#txtcusAddress").val());
    $("#lblcusDepartmentID").html($("#ddlcusDepartmentID option:selected").text());
    $("#lblcusOperatorID").html($("#ddlcusOperatorID option:selected").text());
    $("#lblcusExtType2").html($("#ddlcusExtType2 option:selected").text());
    $("#lblcusAreaID").html($("#ddlcusAreaID option:selected").text());
}


var fromSource = "Customer";

function AddFollowHistory() {
    $("#txtcfhContent").val("");
    $("#ddlcfhTypeID").val("");
    $("#cfhDate").val(new Date().Format("yyyy-MM-dd hh:mm"));
    $("#txtcfhNextFollowTime").val("");
    ShowListCommon('divFollowHistoryEdit', null);
}

function SaveCustomerFollowHistory() {
    var date = new Date().Format("yyyy-MM-dd hh:mm:ss");
    var keyID = $("#hfKeyID").val();
    var cfhID = $("#hfcfhID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var cfhFilePath = $("#hffollowFile").val();
    var cfhStatusID = $("#ddlcfhStatusID").val();
    
    if (fromSource == "Business") {
        cfhStatusID = $("#ddlBusinessStatusID").val();
    }
    else if (fromSource == "AfterSales") {
        cfhStatusID = $("#ddlAfterSalesStatusID").val();
    }
    else {
        cfhStatusID = $("#ddlcfhStatusID").val();
    }
    var cfhdata = "{cfhSource:'" + fromSource + "',cfhRelatedID:" + keyID + ",cfhID:" + cfhID + ",cfhContent:'" + $("#txtcfhContent").val() + "',cfhTypeID:" + $("#ddlcfhTypeID").val() + ",cfhStatusID:'" + cfhStatusID + "',cfhDate:'" + $("#cfhDate").val() + "',cfhAddress:'" + $("#lblcfhAddress").val() + "',cfhAddOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cfhNextFollowTime:'" + $("#txtcfhNextFollowTime").val() + "',cfhFilePath:'" + cfhFilePath + "',CurrentOperatorID:'" + CurrentOperatorID + "'}";
    if (cfhFilePath == "" && $("#txtcfhContent").val() == "") {
        ShowMessage("请填写内容或者图片");
        return;
    }

    disableButton(this);

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveCustomerFollowHistory",
        data: cfhdata,
        dataType: 'json',
        success: function (result) {
            if (result.d != "") {
                ShowSuccessMessage();
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/GetEachFile",
                    data: "{filePath:'" + cfhFilePath + "'}",
                    dataType: 'json',
                    success: function (r) {
                        var li = "";
                        li += '<li style=" margin:0px; padding:0px; margin-bottom:20px;">';
                        li += '<div style="overflow: auto;margin: 0px 4px 7px 4px;border: 1px solid #ccc;border-radius: 7px;margin-top: 0px;" >';
                        li += '<div><table><tr  onclick="javascript:GotoFollowHistory(' + result.d + ',\'Customer\');">';
                        li += '<td style="wdith:50%;"><label style="color: #3C96DE; width:25%">' + $("#hfCurrentOperatorName").val() + '</label>';
                        li += '<label style="color: #3C96DE; width:25%; border: 1px solid #3C96DE;margin-left: 10px;border-radius: 3px;padding: 3px 3px 3px 3px;font-size: 10px;">' + $("#ddlcfhTypeID option:selected").text() + '</label>';
                        li += '</td><td style="width: 50%;text-align: right;">';
                        li += '<label>' + date + '</label>';
                        li += '</td></tr><tr  onclick="javascript:GotoFollowHistory(' + result.d + ',\'Customer\');">';
                        li += '<td colspan="2" style=" line-height:2em"><label>' + $("#txtcfhContent").val() + '</label></td></tr>';
                        li += '<tr><td colspan="2">' + r.d + '</td></tr>';
                        li += '<tr onclick="javascript:GotoFollowHistory(' + result.d + ',\'Customer\');">';
                        li += '<td colspan="2"><label>' + $("#lblcfhAddress").val() + '</label>';
                        li += '</td></tr></table></div>';
                        li += '<div><table><tr><td>';
                        li += '<div style=" width:50%; float:left; text-align:center"  onclick="javascript:GotoFollowHistory(' + result.d + ',\'Customer\');">';
                        li += '<img src="../@images/Content.png" style="width:18px;" /><label>评论（0）</label></div>';
                        li += '<div onclick="javascript:handleClickLike(' + result.d + ',\'this\');" style=" width:50%; float:left; text-align:center"><img src="../@images/like.png" style=" width:18px;" /><label>点赞（0）</label></div>';
                        li += '</td></tr></table></div></div></li>';

                        $("#liFollowHistory" + $("#hfcfhID").val()).remove();
                        $("#ulFollowHistory").prepend(li);


                        ShowListCommon('divFollowHistoryList', null);

                        $("#lblStatus").html($("#ddlcfhStatusID option:selected").text());
                    }
                });

            }
            else {
                ShowErrorMessage();
            }
        }
    });
}

function DeleteCommon(id, functionName, liName, divName) {
    dd.device.notification.confirm({
        message: "确定删除吗？",
        title: "提示",
        buttonLabels: ['确认', '取消'],
        onSuccess: function (result) {
            if (result.buttonIndex == 0) {
                var currentOperatorID = $("#hfCurrentOperatorID").val();
                disableButton(this);
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/" + functionName,
                    data: "{ID:" + id + ",CurrentOperatorID:" + currentOperatorID + "}",
                    dataType: 'json',
                    success: function (result) {
                        if (result.d + 0 > 0) {
                            ShowSuccessMessage();
                            $("#" + liName + id).remove();
                            ShowListCommon(divName, null);
                        }
                        else if (result.d + 0 == 0) {
                            ShowMessage("操作失败！无权操作。");
                        }
                        else if (result.d + 0 == -1) {
                            ShowMessage("操作失败！出现异常。");
                        }
                    }
                });
            }
        },
        onFail: function (err) {
        }
    })
}

function DeleteFollowHistory(id) {
    DeleteCommon(id, "DeleteFollowHistory", "liFollowHistory", "divFollowHistoryList");
}

/*联系人*/
function LinkManView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetLinkMan",
        data: "{clmID:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {

                $("#hfclmID").val(this['clmID']);
                $("#txtclmName").val(this['clmName']);
                $("#ddlclmSex").val(this['clmSex']);
                $("#txtclmTel").val(this['clmTel']);
                $("#txtclmMobile").val(this['clmMobile']);
                $("#txtclmEmail").val(this['clmEmail']);
                $("#txtclmQQ").val(this['clmQQ']);
                $("#txtclmWeChat").val(this['clmWeChat']);
                $("#txtclmRemark").val(this['clmRemark']);

                ShowListCommon('divLinkManEdit', null);
            });
        }
    });
}

function AddLinkMan() {
    $("#hfclmID").val("0");
    $("#txtclmName").val("");
    $("#ddlclmSex").val("");
    $("#txtclmTel").val("");
    $("#txtclmMobile").val("");
    $("#txtclmEmail").val("");
    $("#txtclmQQ").val("");
    $("#txtclmWeChat").val("");
    $("#txtclmRemark").val("");

    ShowListCommon('divLinkManEdit', null);

}

function SaveLinkMan() {
    if ($("#txtclmName").val() == "") {
        ShowMessage("请填写联系人名称");
        return;
    }
    var date = new Date().Format("yyyy-MM-dd hh:mm:ss");
    var cusID = $("#hfKeyID").val();
    var clmID = $("#hfclmID").val();
    var clmdata = "{cusID:" + cusID + ",clmID:" + clmID + ",clmName:'" + $("#txtclmName").val() + "',clmSex:'" + $("#ddlclmSex").val() + "',clmTel:'" + $("#txtclmTel").val() + "',clmMobile:'" + $("#txtclmMobile").val() + "',clmEmail:'" + $("#txtclmEmail").val() + "',clmQQ:'" + $("#txtclmQQ").val() + "',clmWeChat:'" + $("#txtclmWeChat").val() + "',clmRemark:'" + $("#txtclmRemark").val() + "',clmAddOperatorID:'" + $("#hfCurrentOperatorID").val() + "',clmTypeID:'" + $("#ddlclmTypeID").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveLinkMan",
        data: clmdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liLinkMan' + result.d + '" class="MainPanelli">';
                li += '<div>';
                li += '<div  class="new_div">';
                li += '<table class="new_table">';
                li += '<tr>';
                li += '<td style=" width:33.333%; text-align:left"> ';
                li += '<img src="../@images/标签.png" class="new_img" /> <a id="lianxiren" style=" text-decoration: none;color:#2489ce;" href="javascript:LinkManView(' + result.d + ') ;">联系人</a>';
                li += '</td>';
                li += '<td style="width:33.333%; text-align:center">';
                li += ' <a id="wsd" onclick="javascript:LinkManView(' + result.d + ');" style="color:#2489ce;">编辑</a>';
                li += '</td><td style="width:33.333%; text-align:right">';
                li += ' <a href="javascript:DeleteLinkMan(' + result.d + ');"  style=" margin-right:10px; text-decoration: none;color:#2489ce;"> 删除</a>';
                li += ' </td></tr></table></div>';
                li += ' <div class="MainView"><table cellpadding="0" cellspacing="0"><tr>';
                li += '<th>姓名：</th>';
                li += '<td>' + $("#txtclmName").val() + '</td>';
                li += ' </tr><tr><th> <span>性别：</span> </th>';
                li += '<td>' + $("#ddlclmSex option:selected").text() + '</td>';
                li += ' </tr><tr><th>联系人类型：</th><td>' + $("#ddlclmTypeID option:selected").text() + '</td></tr>';
                li += '<tr><th> <span>手机：</span></th>';
                li += '<td>' + $("#txtclmMobile").val() + '</td>';
                li += ' </tr><tr><th> <span>电话：</span></th>';
                li += '<td>' + $("#txtclmTel").val() + '</td>';
                li += '</tr><tr><th> <span>Email：</span></th>';
                li += '<td>' + $("#txtclmEmail").val() + '</td>';
                li += ' </tr><tr><th> <span>QQ：</span></th>';
                li += '<td>' + $("#txtclmQQ").val() + '</td>';
                li += ' </tr><tr><th>微信：</th>';
                li += '<td>' + $("#txtclmWeChat").val() + '</td>';
                li += ' </tr><tr><th>备注：</th>';
                li += '<td>' + $("#txtclmRemark").val(); +'</td>';
                li += ' </tr><tr><th>创建人：</th>';
                li += '<td>' + $("#hfCurrentOperatorName").val() + '</td>';
                li += ' </tr><tr><th>创建时间：</th>';
                li += '<td>' + date + '</td>';
                li += ' </tr><tr><th>修改于：</th>';
                li += '<td>' + date + '</td>';
                li += '</tr>';
                li += ' </table></div></div></li>';

                $("#liLinkMan" + $("#hfclmID").val()).remove();
                $("#ulLinkMan").prepend(li);
                ShowListCommon('divLinkManList', null);

                if (clmID == 0) {
                    $("#ddlcfpLinkManID").append("<option value='" + result.d + "'>" + $("#txtclmName").val() + "</option>");
                    $("#ddlcfhLinkMan").append("<option value='" + result.d + "'>" + $("#txtclmName").val() + "</option>");
                }
                else {
                    var clmName = $("#txtclmName").val();
                    $("#ddlcfpLinkManID option[value='" + result.d + "']").text(clmName);
                    $("#ddlcfhLinkMan option[value='" + result.d + "']").text(clmName);
                }
            }
            else {
                ShowErrorMessage();
            }
        }
    });
}

function DeleteLinkMan(id) {
    DeleteCommon(id, "DeleteLinkMan", "liLinkMan", "divLinkManList");
}

/*商机*/
function OpptunityView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetOpptunity",
        data: "{coID:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {
                if (fromSource == "Business") {
                    $("#hfKeyID").val(this['coID']);
                }
                else {
                    $("#hfcoID").val(this['coID']);
                }
                $("#txtcoName").val(this['coName']);
                $("#ddlcoPhaseID").val(this['coPhaseID']);
                $("#txtcoPredictAmount").val(this['coPredictAmount']);
                $("#txtcoPredictFinishDate").val(this['coPredictFinishDate']);
                $("#ddlcoStatusID").val(this['coStatusID']);
                $("#ddlcoOpptunitySourceID").val(this['coOpptunitySourceID']);
                $("#ddlcoOperatorID").val(this['coOperatorID']);

                ShowListCommon('divOpptunityEdit', null);
            });
        }
    });
}

function AddOpptunity() {

    $("#hfcoID").val("0");
    $("#txtcoName").val("");
    $("#ddlcoPhaseID").val("");
    $("#txtcoPredictAmount").val("");
    $("#txtcoPredictFinishDate").val("");
    $("#ddlcoStatusID").val("");
    $("#ddlcoOpptunitySourceID").val("");

    ShowListCommon('divOpptunityEdit', null);

}

function SaveOpptunity() {
    var date = new Date().Format("yyyy-MM-dd hh:mm:ss");

    var cusID = $("#hfKeyID").val();
    var coID = $("#hfcoID").val();

    if (fromSource == "Opptunity") {
        cusID = $("#hfcusID").val();
        coID = $("#hfKeyID").val();
    }

    if ($("#txtcoName").val() == "") {
        ShowMessage("请输入商机名称");
        return;
    }
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var codata = "{cusID:" + cusID + ",coID:" + coID + ",coName:'" + $("#txtcoName").val() + "',coPhaseID:'" + $("#ddlcoPhaseID").val() + "',coPredictAmount:'" + $("#txtcoPredictAmount").val() + "',coPredictFinishDate:'" + $("#txtcoPredictFinishDate").val() + "',coStatusID:'" + $("#ddlcoStatusID").val() + "',coOpptunitySourceID:'" + $("#ddlcoOpptunitySourceID").val() + "',coOperatorID:'" + $("#ddlcoOperatorID").val() + "',coDate:'" + $("#txtcoDate").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveOpptunity",
        data: codata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liOpptunity' + result.d + '"  class="MainPanelli">';
                li += '<div class="new_div"><table class=" new_table">';
                li += '<tr><td style=" width:33.333%; text-align:left;">';
                li += '<a href="javascript:GoToCenter(' + result.d + ',\'CustomerOpptunity\');" style="text-decoration: none;color:#2489ce;"><img src="../@images/标签.png" class="new_img"/>商机</a>';
                li += '</td><td style=" width:33.333%; text-align:center; ">';
                li += '<a onclick="javascript:OpptunityView(' + result.d + ');" style="color:#2489ce;">编辑</a></td>';
                li += '<td style=" width:33.333%; text-align:right;">';
                li += '<a href="javascript:DeleteOpptunity(' + result.d + ');" style=" margin-right:10px; text-decoration: none;color:#2489ce;" >删除</a></td>';
                li += '</tr></table></div>';
                li += '<div class="MainView" ><table cellpadding="0" cellspacing="0">';
                li += '<tr><th>商机名称：</th>';
                li += '<td>' + $("#txtcoName").val() + '</td>';
                li += '</tr><tr><th>商机日期：</th>';
                li += '<td>' + $("#txtcoDate").val() + '</td>';
                li += '</tr><tr><th>商机阶段：</th>';
                li += '<td>' + $("#ddlcoPhaseID option:selected").text() + '</td>';
                li += '</tr><tr><th>预计销售金额：</th>';
                li += '<td>' + $("#txtcoPredictAmount").val() + '</td>';
                li += '</tr><tr><th>预计签单日期：</th>';
                li += '<td>' + $("#txtcoPredictFinishDate").val() + '</td>';
                li += '</tr><tr><th>状态：</th>';
                li += '<td>' + $("#ddlcoStatusID option:selected").text() + '</td>';
                li += '</tr><tr><th> 商机来源：</th>';
                li += '<td>' + $("#ddlcoOpptunitySourceID option:selected").text() + '</td>';
                li += '</tr><tr><th>负责人：</th>';
                li += '<td>' + $("#ddlcoOperatorID option:selected").text() + '</td>';
                li += '</tr><tr><th>创建时间：</th>';
                li += '<td>' + date + '</td>';
                li += '</tr>';
                li += ' </table></div></li>';

                $("#liOpptunity" + result.d).remove();
                $("#ulOpptunity").prepend(li);

                ShowListCommon('divOpptunityList', null);

                if (coID == 0) {
                    $("#ddlcfhOpptunityID").append("<option value='" + result.d + "'>" + $("#txtcoName").val() + "</option>");
                }
                else {
                    var coNewName = $("#txtcoName").val();
                    $("#ddlcfhOpptunityID option[value='" + result.d + "']").text(coNewName);

                }

            }
            else {
                ShowErrorMessage();
            }
        }
    });
}

function DeleteOpptunity(id) {
    DeleteCommon(id, "DeleteOpptunity", "liOpptunity", "divOpptunityList");
}

function AddFollowPlan() {
    $("#hfcfpID").val("0");
    $("#txtcfpContent").val("");
    $("#ddlcfpFollowTypeID").val("");
    ShowListCommon('divFollowPlanEdit', null);
}

function SaveFollowPlan() {
    var date = new Date().Format("yyyy-MM-dd hh:mm:ss");
    var cfpID = $("#hfcfpID").val();
    var cfpRelatedID = $("#hfKeyID").val();
    var cfpContent = $("#txtcfpContent").val();
    var cfpFilePath = $("#hffollowplanfile").val();
    var cfpDate = $("#txtcfpDate").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var CurrentOperatorName = $("#hfCurrentOperatorName").val();
    if (cfpDate == "") {
        ShowMessage("请输入提醒时间");
        return;
    }
    var cfpData = "{cfpID:" + cfpID + ",cfpRelatedID:'" + cfpRelatedID + "',cfpSource:'" + fromSource + "' ,cfpContent:'" + cfpContent + "',cfpFilePath:'" + cfpFilePath + "',cfpDate:'" + cfpDate + "',cfpOperatorID:'" + CurrentOperatorID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveFollowPlan",
        data: cfpData,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();

                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/GetEachFile",
                    data: "{filePath:'" + cfpFilePath + "'}",
                    dataType: 'json',
                    success: function (r) {
                        var li = "";
                        li += '<li class="MainPanelli"><div>';
                        li += ' <div class="MainView">';
                        li += '<table cellpadding="0" cellspacing="0" style=" padding:5px;">';
                        li += ' <tr onclick="javascript:GoToFollowPlan(' + result.d + ',\'Customer\')">';
                        li += '<td style="wdith:50%;">';
                        li += '<label style="color: #3C96DE; width:25%">' + CurrentOperatorName + '</label></td>';
                        li += '<td style="width: 50%;text-align: right;">';
                        li += '<label>' + cfpDate + '</label></td></tr>';
                        li += '<tr onclick="javascript:GoToFollowPlan(' + result.d + ',\'Customer\')">';
                        li += ' <td colspan="2" style=" line-height:2em">';
                        li += '<label>' + cfpContent + '</label></td></tr>';
                        li += '<tr><td colspan="2">' + r.d + '</td></tr>';
                        li += '<tr onclick="javascript:GoToFollowPlan(' + result.d + ',\'Customer\')">';
                        li += '<td colspan="2" style=" text-align:right;">';
                        li += '<label>' + date + '</label>';
                        li += '</td></tr></table></div></div></li>';

                        $("#ulFollowPlan").prepend(li);
                        ShowListCommon('divFollowPlanList', null);
                    }
                })
            }
            else {
                ShowErrorMessage();
            }
        }
    })
}

function DeleteFollowPlan(id) {
    DeleteCommon(id, "DeleteFollowPlan", "liFollowPlan", "divFollowPlanList");
}

function BusinessView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetBusiness",
        data: "{id:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {
                if (fromSource == "Business") {
                    $("#hfKeyID").val(this['cbID']);
                }
                else {
                    $("#hfcbID").val(this['cbID']);
                }
                $("#txtcbName").val(this['cbName']);
                $("#txtcbTotalAmount").val(this['cbTotalAmount']);
                $("#ddlcbOperatorID").val(this['cbOperatorID']);
                $("#ddlcbNotifyOperatorID").val(this['cbNotifyOperatorID']);
                $("#txtcbDate").val(this['cbDate']);
                $("#txtcbRemark").val(this['cbRemark'])
                $("#ddlcbBusinessType").val(this['cbBusinessType']);
                $("#txtcbExpiredDate").val(this['cbExpiredDate']);
                ShowListCommon('divBusinessEdit', null)
            });
        }
    });
}

function AddBusiness() {
    $("#hfcbID").val(0);
    $("#txtcbName").val("");
    $("#txtcbDate").val("");
    $("#txtcbExpiredDate").val("");
    $("#ddlcbNotifyOperatorID").val("");
    $("#txtcbTotalAmount").val("");
    $("#txtcbRemark").val("");
    ShowListCommon('divBusinessEdit', null)
}

function SaveBusiness() {
    var cusID = $("#hfKeyID").val();
    var cbID = $("#hfcbID").val();
    if (fromSource == "Business") {
        cusID = $("#hfcusID").val();
        cbID = $("#hfKeyID").val();
    }

    var cbTotalAmount = $("#txtcbTotalAmount").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    if ($("#txtcbName").val() == "") {
        ShowMessage("请输入合同名称");
        return;
    }

    if ($("#txtcbDate").val() == "") {
        ShowMessage("请输入合同日期");
        return;
    }
    if (cbTotalAmount == "") {
        ShowMessage("请输入合同金额");
        return;
    }

    var cfhdata = "{cbCustomerID:" + cusID + ",cbID:" + cbID + ",cbName:'" + $("#txtcbName").val() + "',cbTotalAmount:'" + cbTotalAmount + "',cbOperatorID:" + $("#ddlcbOperatorID").val() + ",cbDate:'" + $("#txtcbDate").val() + "',cbRemark:'" + $("#txtcbRemark").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "',cbBusinessType:'" + $("#ddlcbBusinessType").val() + "',cbExpiredDate:'" + $("#txtcbExpiredDate").val() + "',cbNotifyOperatorID:'" + $("#ddlcbNotifyOperatorID").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveBusiness",
        data: cfhdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) 
            {
                ShowSuccessMessage();

                var li = "";
                li += '<li id="liBusiness' + result.d + '"  class="MainPanelli">';
                li += '<div class="new_div"><table class="new_table"><tr>';
                li += '<td style="width: 33.33%; text-align: left">';
                li += '<a href="javascript:GoToCenter(' + result.d + ',\'CustomerBusiness\');"  style=" text-decoration: none;color:#2489ce;"><img src="../@images/标签.png" class="new_img" />合同 </a>';
                li += ' </td><td style="width: 33.33%; text-align: center"><a style="color:#2489ce;" onclick="javascript:BusinessView(' + result.d + '); ">编辑</a>';
                li += ' </td><td style="width: 33.33%; text-align: right"><a href="javascript:DeleteBusiness(' + result.d + ');" style=" text-decoration: none;color:#2489ce;">删除</a></td>';
                li += ' </tr></table></div><div class="MainView">';
                li += '<table cellpadding="0" cellspacing="0"><tr>';
                li += ' <th>合同名称：</th>';
                li += ' <td   id="BTable' + result.d + '">' + $("#txtcbName").val() + '</td></tr>';
                li += '<tr><th>合同类型：</th><td>' + $("#ddlcbBusinessType option:selected").text() + '</td></tr>';
                li += '<tr><th>合同日期：</th>';
                li += '<td >' + $("#txtcbDate").val().toString("yyyy-MM-dd") + '</td> </tr>';
                li += '<tr><th>合同金额：</th>';
                li += '<td id="TotalAmount' + result.d + '">' + $("#txtcbTotalAmount").val() + '</td></tr>';
                li += '<tr><th>负责人：</th>';
                li += '<td >' + $("#ddlcbOperatorID option:selected").text() + '</td></tr>';
                li += ' <tr><th>合同描述：</th>';
                li += '<td >' + $("#txtcbRemark").val() + '</td></tr>';
                li += '<tr><th>到期日期：</th>';
                li += '<td >' + $("#txtcbExpiredDate").val() + '</td> </tr>';
                li += '<tr><th>通知人：</th>';
                li += '<td >' + $("#ddlcbNotifyOperatorID option:selected").text() + '</td></tr>';
                li += '</table></div></li>';

                $("#liBusiness" + result.d).remove();
                $("#ulBusiness ").prepend(li);
                ShowListCommon('divBusinessList', null);

                if (cbID == 0) {
                    $("#ddlcrBusinessID").append("<option value='" + result.d + "'>" + $("#txtcbName").val() + "</option>");
                }
                else {
                    var cbNewName = $("#txtcbName").val();
                    $("#ddlcrBusinessID option[value='" + result.d + "']").text(cbNewName);
                }

            }
            else if(result.d + 0 == 0) {
                ShowMessage("操作失败！无权操作。");
            }
            else if(result.d + 0 == -1) {
                ShowMessage("操作失败！系统错误。");
            }
        }
    });
}

function DeleteBusiness(id) {
    DeleteCommon(id, "DeleteBusiness", "liBusiness", "divBusinessList");
}

function ReceiptView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetReceipt",
        data: "{id:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {
                $("#hfcrID").val(this['crID']);
                $("#txtcrAmount").val(this['crAmount']);
                $("#ddlcrBusinessID").val(this['crBusinessID']);
                $("#ddlcrOperatorID").val(this['crOperatorID']);
                $("#txtcrDate").val(this['crDate']);
                $("#ddlcrBatchID").val(this['crBatchID']);
                $("#ddlcrTypeID").val(this['crTypeID']);
                $("#txtcrRemark").val(this['crRemark']);
                ShowListCommon('divReceiptEdit', null);
            });
        }
    });
}

function AddReceipt() {

    $("#hfcrID").val(0);
    $("#txtcrAmount").val("");
    $("#ddlcrBusinessID").val("");
    ShowListCommon('divReceiptEdit', null);
}


function SaveReceipt() {
    if ($("#txtcrAmount").val() == "") {
        ShowMessage("请输入收款金额");
        return;
    }
    if ($("#txtcrDate").val() == "") {
        ShowMessage("请输入收款日期");
        return;
    }
    var cusID;
    var cbID;
    if (fromSource == "Business") {
        cusID = $("#hfcusID").val();
        cbID = $("#hfKeyID").val();
    }
    else {
        if ($("#ddlcrBusinessID").val() == "") {
            ShowMessage("请选定关联合同");
            return;
        }
        cbID = $("#ddlcrBusinessID").val();
        cusID = $("#hfKeyID").val();
    }

    var crID = $("#hfcrID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var cfhdata = "{crCustomerID:" + cusID + ",crID:" + crID + ",crBusinessID:" + cbID + ",crAmount:" + $("#txtcrAmount").val() + ",crOperatorID:" + $("#ddlcrOperatorID").val() + ",crDate:'" + $("#txtcrDate").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "',crTypeID:'" + $("#ddlcrTypeID").val() + "',crBatchID:'" + $("#ddlcrBatchID").val() + "',crRemark:'" + $("#txtcrRemark").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveReceipt",
        data: cfhdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liReceipt' + result.d + '" class="MainPanelli">';
                li += '<div class="new_div"><table class="new_table"><tr>';
                li += '<td style=" width:33.33%; text-align:left">';
                li += '<img src="../@images/标签.png" class="new_img" /><a style=" text-decoration: none;color:#2489ce;" href="javascript:ReceiptView(' + result.d + ');">收款</a>';
                li += '</td><td style=" width:33.33%; text-align:center">';
                li += '<a onclick="javascript:ReceiptView(' + result.d + ');" style="color:#2489ce;">编辑</a></td>';
                li += ' <td style=" width:33.33%; text-align:right"><a href="javascript:DeleteReceipt(' + result.d + ');"  style=" text-decoration: none;color:#2489ce;"> 删除</a></td>';
                li += '</tr></table></div><div class="MainView">';
                li += '<table cellpadding="0" cellspacing="0">';
                if (fromSource == "Customer") {
                    li += '<tr><th>业务名称：</th>';
                    li += '<td >' + $("#ddlcrBusinessID option:selected").text() + '</td></tr>';
                }
                li += '<tr><th>收款类型：</th><td>' + $("#ddlcrTypeID option:selected").text() + '</td></tr>';
                li += '<tr><th>收款金额：</th>';
                li += '<td >' + $("#txtcrAmount").val() + '</td></tr>';
                li += '<tr><th>期次：</th><td >' + $("#ddlcrBatchID").val() + '</td></tr>';
                li += '<tr><th>业务员：</th>';
                li += '<td >' + $("#ddlcrOperatorID option:selected").text() + '</td>';
                li += ' </tr><tr><th>收款日期：</th>';
                li += '<td >' + $("#txtcrDate").val() + '</td></tr>';
                li += ' <tr><th>备注：</th><td >' + $("#txtcrRemark").val() + '</td></tr>';
                li += '</table></div></li>';
                $("#liReceipt" + $("#hfcrID").val()).remove();
                $("#ulReceipt ").prepend(li);
                ShowListCommon('divReceiptList', null);
            }
            else {
                ShowErrorMessage();
            }
        }
    });
}


function DeleteReceipt(id) {
    dd.device.notification.confirm({
        message: "确定删除吗？",
        title: "提示",
        buttonLabels: ['确认', '取消'],
        onSuccess: function (result) {
            if (result.buttonIndex == 0) {
                var crAmount = $("#crAmount" + id).text();
                crAmount = crAmount.replace(/\ +/g, "").replace(/[\r\n]/g, "");
                disableButton(this);
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/DeleteReceipt",
                    data: "{ID:" + id + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                    dataType: 'json',
                    success: function (result) {
                        if (result.d + 0 > 0) {
                            ShowSuccessMessage();
                            $("#liReceipt" + id).remove();
                            ShowListCommon('divReceiptList', null);
                            var bID = result.d;
                            var GotAmount = $("#GotAmount" + bID).text();
                            GotAmount = GotAmount.replace(/\ +/g, "").replace(/[\r\n]/g, "");
                            var newAmount = parseInt(GotAmount * 1) - parseInt(crAmount * 1);
                            $("#GotAmount" + bID).text(newAmount);
                        }
                        else if (result.d + 0 == 0) {
                            ShowMessage("操作失败！无权操作。");
                        }
                        else if (result.d + 0 == -1) {
                            ShowMessage("操作失败！系统错误。");
                        }
                    }
                });
            }
        },
        onFail: function (err) {
            enableButton();
        }
    })

}

//收款计划
function ReceiptPlanView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetReceiptPlan",
        data: "{id:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {
                $("#hfcrpID").val(this['crpID']);
                $("#txtcrpAmount").val(this['crpAmount']);
                $("#ddlcrpBusinessID").val(this['crpBusinessID']);
                $("#ddlcrpOperatorID").val(this['crpOperatorID']);
                $("#txtcrpDate").val(this['crpDate']);
                $("#ddlcrpBatchID").val(this['crpBatchID']);
                $("#ddlcrpTypeID").val(this['crpTypeID']);
                $("#txtcrpRemark").val(this['crpRemark']);
                ShowListCommon('divReceiptPlanEdit', null);
            });
        }
    });
}

function AddReceiptPlan() {
    $("#hfcrpID").val(0);
    $("#txtcrpAmount").val("");
    $("#ddlcrpBusinessID").val("");
    ShowListCommon('divReceiptPlanEdit', null);
}

function SaveReceiptPlan() {
    if ($("#txtcrpAmount").val() == "") {
        ShowMessage("请输入收款金额");
        return;
    }
    if ($("#txtcrpDate").val() == "") {
        ShowMessage("请输入计划日期");
        return;
    }
    var cusID;
    var cbID;
    if (fromSource == "Business") {
        cusID = $("#hfcusID").val();
        cbID = $("#hfKeyID").val();
    }
    else {
        if ($("#ddlcrpBusinessID").val() == "") {
            ShowMessage("请选定关联合同");
            return;
        }
        cbID = $("#ddlcrpBusinessID").val();
        cusID = $("#hfKeyID").val();
    }

    var crpID = $("#hfcrpID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var cfhdata = "{crpCustomerID:" + cusID + ",crpID:" + crpID + ",crpBusinessID:" + cbID + ",crpAmount:" + $("#txtcrpAmount").val() + ",crpOperatorID:" + $("#ddlcrpOperatorID").val() + ",crpDate:'" + $("#txtcrpDate").val() + "',CurrentOperatorID:'" + CurrentOperatorID + "',crpTypeID:'" + $("#ddlcrpTypeID").val() + "',crpBatchID:'" + $("#ddlcrpBatchID").val() + "',crpRemark:'" + $("#txtcrpRemark").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveReceiptPlan",
        data: cfhdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liReceiptPlan' + result.d + '" class="MainPanelli">';
                li += '<div class="new_div"><table class="new_table"><tr>';
                li += '<td style=" width:33.33%; text-align:left">';
                li += '<img src="../@images/标签.png" class="new_img" /><a style=" text-decoration: none;color:#2489ce;" href="javascript:ReceiptPlanView(' + result.d + ');">收款</a>';
                li += '</td><td style=" width:33.33%; text-align:center">';
                li += '<a onclick="javascript:ReceiptPlanView(' + result.d + ');" style="color:#2489ce;">编辑</a></td>';
                li += ' <td style=" width:33.33%; text-align:right"><a href="javascript:DeleteReceiptPlan(' + result.d + ');"  style=" text-decoration: none;color:#2489ce;"> 删除</a></td>';
                li += '</tr></table></div><div class="MainView">';
                li += '<table cellpadding="0" cellspacing="0">';
                if (fromSource == "Customer") {
                    li += '<tr><th>业务名称：</th>';
                    li += '<td >' + $("#ddlcrpBusinessID option:selected").text() + '</td></tr>';
                }
                li += '<tr><th>收款类型：</th><td>' + $("#ddlcrpTypeID option:selected").text() + '</td></tr>';
                li += '<tr><th>收款金额：</th><td >' + $("#txtcrpAmount").val() + '</td></tr>';
                li += '<tr><th>期次：</th><td >' + $("#ddlcrpBatchID").val() + '</td></tr>';
                li += '<tr><th>业务员：</th>';
                li += '<td >' + $("#ddlcrpOperatorID option:selected").text() + '</td>';
                li += ' </tr><tr><th>收款日期：</th><td >' + $("#txtcrpDate").val() + '</td></tr>';
                li += ' </tr><tr><th>备注：</th><td >' + $("#txtcrpRemark").val() + '</td></tr>';
                li += '</table></div></li>';
                $("#liReceiptPlan" + $("#hfcrpID").val()).remove();
                $("#ulReceiptPlan ").prepend(li);
                ShowListCommon('divReceiptPlanList', null);
            }
            else {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    });
}

function DeleteReceiptPlan(id) {
    if (window.confirm("确定删除吗？")) {
        var crpAmount = $("#crpAmount" + id).text();
        crpAmount = crpAmount.replace(/\ +/g, "").replace(/[\r\n]/g, "");
        disableButton(this);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/DeleteReceiptPlan",
            data: "{ID:" + id + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
            dataType: 'json',
            success: function (result) {
                if (result.d + 0 > 0) {
                    ShowSuccessMessage();
                    $("#liReceiptPlan" + id).remove();
                    ShowListCommon('divReceiptPlanList', null);
                    var bID = result;
                    var GotAmount = $("#GotAmount" + bID).text();
                    GotAmount = GotAmount.replace(/\ +/g, "").replace(/[\r\n]/g, "");
                    var newAmount = parseInt(GotAmount * 1) - parseInt(crpAmount * 1);
                    $("#GotAmount" + bID).text(newAmount);
                }
                else if (result.d + 0 == 0) {
                    ShowMessage("操作失败！无权操作。");
                }
                else if (result.d + 0 == -1) {
                    ShowMessage("操作失败！系统错误。");
                }
            },
            onFail: function (err) {
                enableButton();
            }
        });
    }
}

//反馈
function FeedbackView(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetFeedback",
        data: "{id:" + id + "}",
        dataType: 'json',
        success: function (result) {
            $(result.d).each(function () {
                $("#hfcfbID").val(this['cfbID']);
                $("#ddlcfbFeedbackTypeID").val(this['cfbFeedbackTypeID']);
                $("#txtcfbLinkman").val(this['cfbLinkman']);
                $("#txtcfbTelephone").val(this['cfbTelephone']);
                $("#txtcfbEmail").val(this['cfbEmail']);
                $("#txtcfbOrderRelated").val(this['cfbOrderRelated']);
                $("#txtcfbContent").val(this['cfbContent']);
                $("#ddlcfbHandleOperator").val(this['cfbHandleOperatorID']);

                ShowListCommon('divFeedbackEdit', null);
                
            })
        }

    })
}

function showDivHandle(id) {
    $("#trcfbResult").show();
    $("#trcfbStatus").show();
    FeedbackView(id);
}

function AddFeedback() {
    $("#ddlcfbFeedbackTypeID").val("");
    $("#txtcfbLinkman").val("");
    $("#txtcfbTelephone").val("");
    $("#txtcfbEmail").val("");
    $("#txtcfbOrderRelated").val("");
    $("#txtcfbContent").val("");
    $("#ddlcfbHandleOperator").val("");

    ShowListCommon('divFeedbackEdit', null);
    $("#trcfbResult").hide();
    $("#trcfbStatus").hide();
}

function SaveFeedback() {
    if ($("#txtcfbContent").val() == "") {
        ShowMessage("请填写反馈内容");
        return;
    }
    var cusID = $("#hfKeyID").val();
    var cfbID = $("#hfcfbID").val();
    var cfbdata = "{cfbCustomerID:" + cusID + ",cfbID:" + cfbID + ",cfbFeedbackTypeID:'" + $("#ddlcfbFeedbackTypeID").val() + "',cfbLinkman:'" + $("#txtcfbLinkman").val() + "',cfbTelephone:'" + $("#txtcfbTelephone").val() + "',cfbEmail:'" + $("#txtcfbEmail").val() + "',cfbOrderRelated:'" + $("#txtcfbOrderRelated").val() + "',cfbContent:'" + $("#txtcfbContent").val() + "',cfbHandleOperatorID:'" + $("#ddlcfbHandleOperator").val() + "',CurrentOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cfbResult:'" + $("#txtcfbResult").val() + "',cfbStatusID:'" + $("#ddlcfbStatus").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveFeedback",
        data: cfbdata,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liFeedback' + result.d + '" class="MainPanelli">';
                li += '<div class="new_div"><table class="new_table"><tr>';
                li += '<td style=" width:33.33%; text-align:left">';
                li += '<a href="javascript:GoToCenter(' + result.d + ',\'CustomerFeedback\');"  style=" text-decoration: none;color:#2489ce;"><img src="../@images/标签.png" class="new_img" />反馈</a>';
                li += '</td><td style=" width:33.33%; text-align:center">';
                li += '<a onclick="javascript:FeedbackView(' + result.d + ');" style="color:#2489ce;">编辑</a></td>';
                li += ' <td style=" width:33.33%; text-align:right"><a href="javascript:DeleteFeedback(' + result.d + ');"   style=" margin-right:10px;text-decoration: none;color:#2489ce;" > 删除</a></td>';
                li += '</tr></table></div><div class="MainView">';
                li += '<table cellpadding="0" cellspacing="0"><tr><th>反馈类型：</th>';
                li += '<td>' + $("#ddlcfbFeedbackTypeID option:selected").text() + '</td>';
                li += '</tr><tr><th>联系人：</th>';
                li += '<td >' + $("#txtcfbLinkman").val() + '</td>';
                li += '</tr><tr><th>联系电话：</th>';
                li += '<td >' + $("#txtcfbTelephone").val() + '</td>';
                li += ' </tr><tr><th>电子邮件：</th>';
                li += '<td >' + $("#txtcfbEmail").val() + '</td>';
                li += '</tr> <tr><th>相关合同：</th>';
                li += '<td >' + $("#txtcfbOrderRelated").val() + '</td>';
                li += ' </tr><tr><th>反馈内容:</th>';
                li += ' <td >' + $("#txtcfbContent").val() + '</td>';
                li += ' </tr><tr><th>处理结果:</th>';
                li += '<td >' + $("#txtcfbResult").val() + '</td>';
                li += '</tr><tr><th>处理状态:</th>';
                li += '<td >' + $("#ddlcfbStatus option:selected").text() + '</td>';
                li += '</tr><tr><th>申请处理人:</th>';
                li += '<td >' + $("#ddlcfbHandleOperator option:selected").text() + '</td></tr>';
                li += '</table></div></li>';
                $("#liFeedback" + $("#hfcfbID").val()).remove();
                $("#ulFeedback ").prepend(li);
                ShowListCommon('divFeedbackList', null);
                $("#trcfbResult").hide();
                $("#trcfbStatus").hide();

            }
            else {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    });
}

function DeleteFeedback(id) {
    DeleteCommon(id, "DeleteFeedback", "liFeedback", "divFeedbackList");
}


function SaveCustomerClue() {
    var ccActivityID = $("#ddlccActivityID").val();
    var ccID = $("#hfKeyID").val();
    var ccName = $("#txtccName").val();
    var ccCustomerName = $("#txtccCustomerName").val();
    var ccTel = $("#txtccTel").val();
    var ccMobile = $("#txtccMobile").val();
    var ccAddress = $("#txtccAddress").val();
    var ccRemark = $("#txtccRemark").val();
    var ccOperatorID = $("#ddlccOperatorID").val();
    var ccDepartmentID = $("#ddlccDepartmentID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    if (ccName == "") {
        ShowMessage("请填写联系人姓名");
        return;
    }
    var ccdate = "{ccActivityID:'" + ccActivityID + "',ccID:" + ccID + ",ccName:'" + ccName + "',ccCustomerName:'" + ccCustomerName + "',ccTel:'" + ccTel + "',ccMobile:'" + ccMobile + "',ccAddress:'" + ccAddress + "',ccRemark:'" + ccRemark + "',ccOperatorID:" + ccOperatorID + ",ccDepartmentID:" + ccDepartmentID + ",CurrentOperatorID:'" + CurrentOperatorID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveCustomerClue",
        data: ccdate,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();

                $("#lbl_ccName").html(ccName);
                $("#lblccCustomerName").html(ccCustomerName);
                $("#linkccTel").attr("href", "tel:" + ccTel);
                $("#lblccTel").html(ccTel);
                $("#linkccMobile").attr("href", "tel:" + ccMobile);
                $("#lblccMobile").html(ccMobile);
                $("#lblccRemark").html(ccRemark);
                $("#lblccActivityName").html($("#ddlccActivityID option:selected").text());
                $("#lblccOperator").html($("#ddlccOperatorID option:selected").text());
                $("#lblccAddoperator").html($("#hfCurrentOperatorName").val());
                $("#lblccAddress").html(ccAddress);

                ShowListCommon('divCustomerClueView', null);
            }
            else {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    });
}

function AddCustomerClue() {
    var ccActivityID = $("#ddlccActivityID").val();
    var ccName = $("#txtccName").val();
    var ccCustomerName = $("#txtccCustomerName").val();

    if (ccName == "") {
        ShowMessage("请输入姓名！");
        return false;
    }

    var ccTel = $("#txtccTel").val();
    var ccMobile = $("#txtccMobile").val();
    var ccAddress = $("#txtccAddress").val();
    var ccRemark = $("#txtccRemark").val();
    var ccOperatorID = $("#ddlccOperatorID").val();
    var ccDepartmentID = $("#ddlccDepartmentID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();

    var ccdate = "{ccActivityID:'" + ccActivityID + "',ccName:'" + ccName + "',ccCustomerName:'" + ccCustomerName + "',ccTel:'" + ccTel + "',ccMobile:'" + ccMobile + "',ccAddress:'" + ccAddress + "',ccRemark:'" + ccRemark + "',ccOperatorID:" + ccOperatorID + ",ccDepartmentID:" + ccDepartmentID + ",CurrentOperatorID:'" + CurrentOperatorID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/AddCustomerClue",
        data: ccdate,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                document.location.href = "CustomerClueList.aspx";
            }
            else
            {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    })

}

function GotoFollowHistory(id, fromSource) {
    window.location.href = "CustomerFollowHistoryEditForm.aspx?ID=" + id + "&FromSource=" + fromSource;
}

function GoToFollowPlan(id,fromSource) {
    window.location.href = "CustomerFollowPlanEditForm.aspx?ID=" + id + "&FromSource=" + fromSource ;
}

function GetDepartment() {
    var cwOperatorID = $("#ddlcwOperatorID").val();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/GetDepartment",
        data: "{cwOperatorID:'" + cwOperatorID + "'}",
        dataType: 'json',
        success: function (result) {
            $("#hfdepartment").val(result.d);
        }
    })

 }

 function AddCoWorker() {
    ShowListCommon('divCoWorkerEdit', null);
 }

 function SaveCoWorker() {
    var cwRelatedID = $("#hfKeyID").val();
    var cwOperatorID = $("#ddlcwOperatorID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();

    var cwData = "{cwRelatedID:'" + cwRelatedID + "',cwOperatorID:'" + cwOperatorID + "',CurrentOperatorID:'" + CurrentOperatorID + "',cwSource:'" + fromSource + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveCoWorker",
        data: cwData,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                var li = "";
                li += '<li id="liCoWorker' + result.d + '" class="MainPanelli">';
                li += '<div><div class="new_div"><table class="new_table"><tr>';
                li += '<td style="width: 33.333%; text-align: left">';
                li += '<img src="../@images/标签.png" class="new_img" />';
                li += '<a style="text-decoration: none;color:#2489ce;">';
                li += '协作人</a></td>';
                li += '<td style="width: 33.333%; text-align: right">';
                li += '<a href="javascript:DeleteCoWorkerView(' + result.d + ');" style="margin-right: 10px;text-decoration: none;color:#2489ce;">';
                li += '删除</a>';
                li += '</td></tr></table></div>';
                li += '<div class="MainView"><table cellpadding="0" cellspacing="0">';
                li += '<tr><th>协作人：</th><td>' + $("#ddlcwOperatorID option:selected").text() + '</td></tr>';
                li += '<tr><th>部门：</th><td>' + $("#hfdepartment").val() + '</td></tr>';
                li += '</table></div></div></li>';

                $("#ulCoWorker").prepend(li);
                ShowListCommon('divCoWorkerList', null);
            }
            else
            {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    })
}

function DeleteCoWorkerView(id) {
    DeleteCommon(id, "DeleteCoWorkerView", "liCoWorker", "divCoWorkerList");
}

function AddCustomerBusinessInfo() {
    var cbName = $("#txtcbName").val();
    var cbBusinessTypeID = $("#ddlcbBusinessTypeID").val();
    var cbDate = $("#txtcbDate").val();
    var cbTotalAmount = $("#txtcbTotalAmount").val();
    var cbOperatorID = $("#ddlcbOperatorID").val();
    var cbRemark = $("#txtcbRemark").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var OpptunityID = getUrlParameter("OpptunityID");
    var cbData = "{cbName:'" + cbName + "',cbBusinessTypeID:'" + cbBusinessTypeID + "',cbDate:'" + cbDate + "',cbTotalAmount:'" + cbTotalAmount + "',cbOperatorID:'" + cbOperatorID + "',cbRemark:'" + cbRemark + "',CurrentOperatorID:'" + CurrentOperatorID + "',OpptunityID:'" + OpptunityID + "',cbExpiredDate:'" + $("#txtcbExpiredDate").val() + "',cbNotifyOperatorID:'" + $("ddlcbNotifyOperatorID").val() + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/AddCustomerBusinessInfo",
        data: cbData,
        dataType: 'json',
        success: function (result) {
            ShowSuccessMessage();
            window.location.href = "CustomerBusinessList.aspx";
        },
        onFail: function (err) {
            enableButton();
        }
    })
}

function AddDocument() {
    $("#txtcfName").val("");
    $("#hfDocumentFile").val("");
    ShowListCommon('divDocumentEdit', null);
}

function SaveDocument() {
    var date = new Date().Format("yyyy-MM-dd hh:mm:ss");
    var cfName = $("#txtcfName").val();
    var cfCN = $("#hfDocumentFile").val();
    if (cfCN == "") {
        ShowMessage("请选择文件");
        return false;
    }
  
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var cfCustomerID = $("#hfKeyID").val();

    var cfData = "{cfNane:'" + cfName + "',cfCN:'" + cfCN + "',cfUserID:'" + CurrentOperatorID + "',cfCustomerID:'" + cfCustomerID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveDocument",
        data: cfData,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/GetEachFile",
                    data: "{filePath:'" + cfCN + "'}",
                    dataType: 'json',
                    success: function (r) {
                        var li = "";
                        li += '<li id="liDocument' + result + '" class="MainPanelli">';
                        li += '<div><div class="new_div"><table class="new_table">';
                        li += '<tr><td style="width: 33.333%; text-align: left">';
                        li += '<img src="../@images/标签.png" class="new_img" /><a style="text-decoration: none;color: #2489ce;">文件</a>';
                        li += '</td><td style="width: 33.333%; text-align: right">';
                        li += '<a href="javascript:DeleteDocument(' + result + ');" style="margin-right: 10px;color: #2489ce;text-decoration: none;">删除</a>';
                        li += '</td></tr></table></div><div class="MainView">';
                        li += '<table cellpadding="0" cellspacing="0"><tr><th>';
                        li += '文件描述：</th><td>' + $("#txtcfName").val() + '</td></tr>';
                        li += '<tr><td colspan="2">' + r.d + '</td>';
                        li += '</tr><tr><td colspan="2">' + date + '';
                        li += '</td></tr></table></div></div></li>';

                        $("#ulDocument").prepend(li);
                        ShowListCommon('divDocumentList', null);
                    }
                })
            }
        },
        onFail: function (err) {
            enableButton();
        }
    })
}

function DeleteDocument(id) {
    DeleteCommon(id, "DeleteDocument", "liDocument", "divDocumentList");
}

function SaveProject() {
    var pjID = $("#hfKeyID").val();
    var pjNO = $("#txtpjNO").val();
    var pjName = $("#txtpjName").val();
    var pjCompanyName = $("#txtpjCompanyName").val();
    var pjDetail = $("#txtpjDetail").val();
    var pjProduct = $("#txtpjProduct").val();
    var pjAmount = $("#txtpjAmount").val();
    var pjContactor = $("#txtpjContactor").val();
    var pjPrice = $("#txtpjPrice").val();
    var pjRemark = $("#txtpjRemark").val();
    var pjOperatorID = $("#ddlpjOperatorID").val();
    var pjExpiredDate = $("#txtpjExpiredDate").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    var pjToApproveOperatorID = $("#pjToApproveOperatorID").val();
    if (pjName == "") {
        ShowMessage("请填写项目名称");
        return;
    }
    var ccdate = "{pjID:" + pjID + ",pjNO:'" + pjNO + "',pjName:'" + pjName + "',pjCompanyName:'" + pjCompanyName + "',pjDetail:'" + pjDetail + "',pjProduct:'" + pjProduct + "',pjAmount:'" + pjAmount + "',pjContactor:'" + pjContactor + "',pjPrice:'" + pjPrice + "',pjRemark:'" + pjRemark + "',pjOperatorID:'" + pjOperatorID + "',pjToApproveOperatorID:'" + pjToApproveOperatorID + "',pjExpiredDate:'" + pjExpiredDate + "',CurrentOperatorID:'" + CurrentOperatorID + "'}";
    disableButton(this);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/SaveProject",
        data: ccdate,
        dataType: 'json',
        success: function (result) {
            if (result.d + 0 > 0) {
                ShowSuccessMessage();
                if (pjID != "0") {
                    $("#lblpjNO").html(pjNO);
                    $("#lblpjName").html(pjName);
                    $("#lblpjCompanyName").html(pjCompanyName);
                    $("#lblpjDetail").html(pjDetail);
                    $("#lblpjProduct").html(pjProduct);
                    $("#lblpjAmount").html(pjAmount);
                    $("#lblpjContactor").html(pjContactor);
                    $("#lblpjPrice").html(pjPrice);
                    $("#lblpjExpired").html(pjExpiredDate);
                    $("#lblpjRemark").html(pjRemark);
                    $("#lblpjOperatorID").html($("#ddlpjOperatorID option:selected").text());
                    ShowListCommon('divProjectView', null);
                }
                else {
                    history.back();
                }
            }
            else {
                ShowErrorMessage();
            }
        },
        onFail: function (err) {
            enableButton();
        }
    });
}

function AddProject() {
    $("#hfKeyID").val("0");
    SaveProject();
}

function ApproveProject() {
    var id = $("#hfKeyID").val();
    var CurrentOperatorID = $("#hfCurrentOperatorID").val();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "../AjaxMethods.asmx/ApproveProject",
        data: "{ID:" + id + ",CurrentOperatorID:" + CurrentOperatorID + "}",
        dataType: 'json',
        success: function (result) {
            ShowSuccessMessage();
            location.reload();
        }
    })
}

    function ShowListCommon(id, ctr) {
        $(".cssMainPanel").hide();
        $("#" + id).show();
        if (ctr != null) {
            $("a", $("#divNavigation")).removeClass("li_title");
            $(ctr).addClass("li_title");
        }

    }

    $(function () {
        var hfFirstLiLength = $("#hfFirstLiLength").val();
        var sWidth = $("#divNavigation").width(); //获取焦点图的宽度（显示面积）
        $("#divNavigation ul").css("width", sWidth);
        var length = $("#divNavigation ul li").length;
        var liwidth = $("#divNavigation ul li").width();
        var index = 0;
        var len = (length - parseInt(hfFirstLiLength));
        //以下代码添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
        var btn = "";
        btn += "</div><div class='pre'></div><div class='next'></div>";
        $("#divNavigation").append(btn);

        //preNext
        //上一页、下一页按钮透明度处理
        $("#divNavigation .preNext").css("opacity", 0.2).hover(function () {
            $(this).stop(true, false).animate({ "opacity": "0.5" }, 300);
        }, function () {
            $(this).stop(true, false).animate({ "opacity": "0.2" }, 300);
        });

        $("#divNavigation .pre").hide();

        //上一页按钮
        $("#divNavigation .pre").click(function () {
            $("#divNavigation .next").show();
            if (index <= 1) {
                $("#divNavigation .pre").hide();
            }

            if (index <= 0) {
                return;
            }
            index -= parseInt(hfFirstLiLength);
            showPics(index);
        });

        //下一页按钮
        $("#divNavigation .next").click(function () {
            $("#divNavigation .pre").show();
            if (index >= len - 1) {
                $("#divNavigation .next").hide();
            }
            if (index >= len) {
                return;
            }
            index += parseInt(hfFirstLiLength);
            showPics(index);
        });

        //本例为左右滚动，即所有li元素都是在同一排向左浮动，所以这里需要计算出外围ul元素的宽度
        $("#divNavigation ul").css("width", liwidth * (length));

        var ulwidth = $("#divNavigation ul").width();
        var liwidth = ulwidth / length;
        $("#divNavigation ul li").css("width", liwidth);

        //显示图片函数，根据接收的index值显示相应的内容
        function showPics(index) { //普通切换
            var nowLeft = -index * liwidth; //根据index值计算ul元素的left值
            $("#divNavigation ul").stop(true, false).animate({ "left": nowLeft }, 300); //通过animate()调整ul元素滚动到计算出的position
        }
    });

    $(document).on("pageinit", "#divMainInfoView", function () {
        $("#divNavigation").on("swipeleft", function () {
            $("#divNavigation .next").click();
        })

        $("#divNavigation").on("swiperight", function () {
            $("#divNavigation .pre").click();
        })
    })


    function DelectFile(piccn, postfix) {
        var picName = piccn + "." + postfix;
        var v = $("#hf" + controlID).val();
        var k = v.replace(picName + "|", "");
        $("#hf" + controlID).val(k);
        $("#pic" + piccn).remove();
        //删除服务器上的文件
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "DeleteFileFromService.aspx",
            data: "picname=" + picName,
            dataType: 'json',
            success: function (result) {

            }
        })
    }

    
    //文件上传部分
    var controlID;
    function uploadFile(id) {
        dd.device.notification.showPreloader({
            text: "文件上传中..", //loading显示的字符，空表示不显示文字
            showIcon: true, //是否显示icon，默认true
            onSuccess: function (result) {
                /*{}*/
            },
            onFail: function (err) { }
        })
        controlID = id;
        var files = document.getElementById(id).files;
        var cusID = $("#hfcusID").val();
        if (files.length > 0) {
            var fd = new FormData();
            for (var i = 0; i < files.length; i++) {
                fd.append(id, files[i]);
            }
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", uploadProgress, false);
            xhr.addEventListener("load", uploadComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);
            xhr.open("POST", "Upload.ashx?cusID=" + cusID);
            xhr.send(fd);
        }
        else {
            dd.device.notification.alert({
                message: "请选择上传的图片！",
                title: "提示", //可传空
                buttonName: "确定",
                onSuccess: function () {
                    dd.device.notification.hidePreloader({
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                },
                onFail: function (err) { }
            });
        }
    }

    function uploadProgress(evt) {//上传中    
        if (evt.lengthComputable) {
            //document.getElementById('progressNumber').innerHTML = "文件上传中...";
            dd.device.notification.showPreloader({
                text: "文件上传中..", //loading显示的字符，空表示不显示文字
                showIcon: true, //是否显示icon，默认true
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })
        }
        else {
            document.getElementById('progressNumber').innerHTML = 'unable to compute';
        }
    }

    function uploadComplete(evt) {//上传完成    
        if (evt.target.responseText != "") {
            dd.device.notification.hidePreloader({
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })

            dd.device.notification.toast({
                icon: '', //icon样式，有success和error，默认为空 0.0.2
                text: "文件上传成功！", //提示信息
                duration: 1, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                delay: 1, //延迟显示，单位秒，默认0
                onSuccess: function (result) {
                    /*{}*/
                    var h = "";
                    var PCWebDomain = $("#hfPCWebDomain").val();
                    var path = PCWebDomain + "UploadFile/CustomerFile/";
                    var filepath = evt.target.responseText;
                    var f = filepath.split('|');
                    for (var i = 0; i < f.length; i++) {
                        if (f[i] != "") {
                            var ff = f[i].split('.');
                            h += "<div style='float:left;' id='pic" + ff[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + ff[0] + "','" + ff[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src='" + path + f[i] + "' /></div></div>";
                        }
                    }
                    $("#picBox").html(h);
                },
                onFail: function (err) { }
            })

            $("#hf" + controlID).val(evt.target.responseText);
        }
        else {
            dd.device.notification.hidePreloader({
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })
            dd.device.notification.alert({
                message: "该文件上传失败！",
                title: "提示", //可传空
                buttonName: "确定",
                onSuccess: function () {

                },
                onFail: function (err) { }
            });
        }
    }

    function uploadFailed(evt) {//上传失败    
        alert("There was an error attempting to upload the file.");
    }
    function uploadCanceled(evt) {//上传被取消    
        alert("The upload has been canceled by the user or the browser dropped the connection.");
    }


    function uploadPlanFile(id) {
        dd.device.notification.showPreloader({
            text: "文件上传中..", //loading显示的字符，空表示不显示文字
            showIcon: true, //是否显示icon，默认true
            onSuccess: function (result) {
                /*{}*/
            },
            onFail: function (err) { }
        })
        controlID = id;
        var files = document.getElementById(id).files;
        var cusID = $("#hfcusID").val();
        if (files.length > 0) {
            var fd = new FormData();
            for (var i = 0; i < files.length; i++) {
                fd.append(id, files[i]);
            }
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", uploadProgress, false);
            xhr.addEventListener("load", uploadPlanComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);
            xhr.open("POST", "Upload.ashx?cusID=" + cusID);
            xhr.send(fd);
        }
        else {
            dd.device.notification.alert({
                message: "请选择上传的图片！",
                title: "提示", //可传空
                buttonName: "确定",
                onSuccess: function () {
                    dd.device.notification.hidePreloader({
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                },
                onFail: function (err) { }
            });
        }
    }

    function uploadPlanComplete(evt) {//上传完成    
        if (evt.target.responseText != "") {
            dd.device.notification.hidePreloader({
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })

            dd.device.notification.toast({
                icon: '', //icon样式，有success和error，默认为空 0.0.2
                text: "文件上传成功！", //提示信息
                duration: 1, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                delay: 1, //延迟显示，单位秒，默认0
                onSuccess: function (result) {
                    /*{}*/
                    var h = "";
                    var PCWebDomain = $("#hfPCWebDomain").val();
                    var path = PCWebDomain + "UploadFile/CustomerFile/";
                    var filepath = evt.target.responseText;
                    var f = filepath.split('|');
                    for (var i = 0; i < f.length; i++) {
                        if (f[i] != "") {
                            var ff = f[i].split('.');
                            h += "<div style='float:left;' id='pic" + ff[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + ff[0] + "','" + ff[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src='" + path + f[i] + "' /></div></div>";
                        }
                    }
                    $("#Div1").html(h);
                },
                onFail: function (err) { }
            })

            $("#hf" + controlID).val(evt.target.responseText);
        }
        else {
            dd.device.notification.hidePreloader({
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })
            dd.device.notification.alert({
                message: "该文件上传失败！",
                title: "提示", //可传空
                buttonName: "确定",
                onSuccess: function () {

                },
                onFail: function (err) { }
            });
        }
    }

    function uploadDocumentFile(id) {
        controlID = id;
        var files = document.getElementById(id).files;
        var cusID = $("#hfKeyID").val();
        if (files.length > 0) {
            var fd = new FormData();
            for (var i = 0; i < files.length; i++) {
                fd.append(id, files[i]);
            }
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", uploadProgress, false);
            xhr.addEventListener("load", uploadDocumentComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);
            xhr.open("POST", "Upload.ashx?cusID=" + cusID);
            xhr.send(fd);
        }
        else {
            ShowMessage("请选择上传的图片！");
        }
    }

    function uploadDocumentComplete(evt) {//上传完成
        if (evt.target.responseText != "") {
            ShowMessage("文件上传成功！");
            var h = "";
            var PCWebDomain = $("#hfPCWebDomain").val();
            var path = PCWebDomain + "UploadFile/CustomerFile/";
            var filepath = evt.target.responseText;
            var f = filepath.split('|');
            for (var i = 0; i < f.length; i++) {
                if (f[i] != "") {
                    var ff = f[i].split('.');
                    h += "<div style='float:left;' id='pic" + ff[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + ff[0] + "','" + ff[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src='" + path + f[i] + "' /></div></div>";
                }
            }
            $("#DivDocumentFile").html(h);
            $("#hf" + controlID).val(evt.target.responseText);
        }
        else {
            ShowMessage("该文件上传失败！");
        }
    }
    //结束文件上传

    function handleClickLike(id, cfhOperatorID) {
        var currentOperatorID = $("#hfCurrentOperatorID").val();
        var cfhID = id;

        var ccdate = "{currentOperatorID:'" + currentOperatorID + "',cfhID:'" + cfhID + "',cfhOperatorID:'" + cfhOperatorID + "'}";
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/GetClickLikeInfoForHistory",
            data: ccdate,
            dataType: 'json',
            success: function (result) {
                if (result.d != "") {
                    var operators = result.d.split("|");
                    $("#lblLikeCount" + id).html("点赞(" + operators.length + ")");
                }
                else {
                    $("#lblLikeCount" + id).html("点赞(0)");
                }
                var operatorName = $("#hfCurrentOperatorName").val();
                if ((result.d + "|").indexOf(operatorName) > -1) {
                    $("#divLike" + id).removeClass("cssNotLike");
                    $("#divLike" + id).addClass("cssLike");
                    $("#imgLike" + id).attr("src", "../@images/likeClick.png");
                }
                else {
                    $("#divLike" + id).removeClass("cssLike");
                    $("#divLike" + id).addClass("cssNotLike");
                    $("#imgLike" + id).attr("src", "../@images/like.png");
                }
            }
        })
    }

    function GoToCenter(id, PageName) {
        var url = PageName + "EditForm.aspx?Action=View&ID=" + id;
        window.open(url);
    }

    //市场活动
    //Start
    function AddMarketingActivityInfo() {
        var maName = $("#txtmaName").val();
        if (maName == "") {
            ShowMessage("请输入活动名称");
            return;
        }
        var maStartDate = $("#txtmaStartDate").val();
        var maEndDate = $("#txtmaEndDate").val();
        var maTypeID = $("#ddlmaTypeID").val();
        var maDescription = $("#txtmaDescription").val();
        var maPredictCost = $("#txtmaPredictCost").val();
        var maPredictAmount = $("#txtmaPredictAmount").val();
        var maActualCost = $("#txtmaActualCost").val();
        var maPredictNum = $("#txtmaPredictNum").val();
        var maActualNum = $("#txtmaActualNum").val();
        var maRemark = $("#txtmaRemark").val();
        var maDepartmentID = $("#ddlmaDepartmentID").val();
        var maOperatorID = $("#ddlmaOperatorID").val();
        var CurrentOperatorID = $("#hfCurrentOperatorID").val();
        var madata = "{maName:'" + maName + "',maStartDate:'" + maStartDate + "',maEndDate:'" + maEndDate + "',maTypeID:'" + maTypeID + "',maDescription:'" + maDescription + "',maPredictCost:'" + maPredictCost + "',maPredictAmount:'" + maPredictAmount + "',maActualCost:'" + maActualCost + "',maPredictNum:'" + maPredictNum + "',maActualNum:'" + maActualNum + "',maRemark:'" + maRemark + "',maDepartmentID:'" + maDepartmentID + "',maOperatorID:'" + maOperatorID + "',CurrentOperatorID:'" + CurrentOperatorID + "'}";
        disableButton(this);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/AddMarketingActivityInfo",
            data: madata,
            dataType: 'json',
            success: function (result) {
                if (result.d + 0 > 0) {
                    ShowSuccessMessage();
                    window.location.href = "../data/MarketingActivityList.aspx";
                }
                else {
                    ShowErrorMessage();
                }
            },
            onFail: function (err) {
                enableButton();
            }
        })
    }

    function SaveMarketingActivity() {
        var maID = $("#hfKeyID").val();
        var maName = $("#txtmaName").val();
        if (maName == "") {
            ShowMessage("请输入活动名称");
            return;
        }
        var maStartDate = $("#txtmaStartDate").val();
        var maEndDate = $("#txtmaEndDate").val();
        var maTypeID = $("#ddlmaTypeID").val();
        var maDescription = $("#txtmaDescription").val();
        var maPredictCost = $("#txtmaPredictCost").val();
        var maPredictAmount = $("#txtmaPredictAmount").val();
        var maActualCost = $("#txtmaActualCost").val();
        var maPredictNum = $("#txtmaPredictNum").val();
        var maActualNum = $("#txtmaActualNum").val();
        var maRemark = $("#txtmaRemark").val();
        var maDepartmentID = $("#ddlmaDepartmentID").val();
        var maOperatorID = $("#ddlmaOperatorID").val();
        var CurrentOperatorID = $("#hfCurrentOperatorID").val();
        var madata = "{maID:'" + maID + "',maName:'" + maName + "',maStartDate:'" + maStartDate + "',maEndDate:'" + maEndDate + "',maTypeID:'" + maTypeID + "',maDescription:'" + maDescription + "',maPredictCost:'" + maPredictCost + "',maPredictAmount:'" + maPredictAmount + "',maActualCost:'" + maActualCost + "',maPredictNum:'" + maPredictNum + "',maActualNum:'" + maActualNum + "',maRemark:'" + maRemark + "',maDepartmentID:'" + maDepartmentID + "',maOperatorID:'" + maOperatorID + "',CurrentOperatorID:'" + CurrentOperatorID + "'}";
        disableButton(this);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/SaveMarketingActivity",
            data: madata,
            dataType: 'json',
            success: function (result) {
                if (result.d + 0 > 0) {
                    ShowSuccessMessage();
                    $("#lblmaName").html(maName);
                    $("#lblmaStartDate").html(maStartDate);
                    $("#lblmaEndDate").html(maEndDate);
                    $("#lblmaTypeName").html($("#ddlmaTypeID option:selected").text());
                    $("#lblmaDescription").html(maDescription);
                    $("#lblmaPredictCost").html(maPredictCost);
                    $("#lblmaPredictAmount").html(maPredictAmount);
                    $("#lblmaActualCost").html(maActualCost);
                    $("#lblmaPredictNum").html(maPredictNum);
                    $("#lblmaActualNum").html(maActualNum);
                    $("#lblmaRemark").html(maRemark);
                    $("#lblmaDepartmentID").html($("#ddlmaDepartmentID option:selected").text());
                    $("#lblmaOperator").html($("#ddlmaOperatorID option:selected").text());

                    ShowListCommon('divMarketingActivityView', this);
                }
                else {
                    ShowErrorMessage();
                }
            },
            onFail: function (err) {
                enableButton();
            }
        })
    }

    function SaveMarketingActivityClue() {
        var ccID = $("#hfccID").val();
        var ccActivityID = $("#hfKeyID").val();
        var ccName = $("#txtccName").val();
        var ccCustomerName = $("#txtccCustomerName").val();
        if (ccName == "") {
            ShowMessage("请输入姓名！");
            return false;
        }
        var ccTel = $("#txtccTel").val();
        var ccMobile = $("#txtccMobile").val();
        var ccAddress = $("#txtccAddress").val();
        var ccRemark = $("#txtccRemark").val();
        var ccOperatorID = $("#ddlccOperatorID").val();
        var ccDepartmentID = $("#ddlccDepartmentID").val();
        var CurrentOperatorID = $("#hfCurrentOperatorID").val();

        var ccdate = "{ccID:" + ccID + ",ccActivityID:'" + ccActivityID + "',ccName:'" + ccName + "',ccCustomerName:'" + ccCustomerName + "',ccTel:'" + ccTel + "',ccMobile:'" + ccMobile + "',ccAddress:'" + ccAddress + "',ccRemark:'" + ccRemark + "',ccOperatorID:" + ccOperatorID + ",ccDepartmentID:" + ccDepartmentID + ",CurrentOperatorID:'" + CurrentOperatorID + "'}";
        disableButton(this);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/SaveMarketingActivityClue",
            data: ccdate,
            dataType: 'json',
            success: function (result) {
                if (result.d + 0 > 0) {
                    ShowSuccessMessage();
                    var li = "";
                    li += '<li id="liClue' + result.d + '" class="MainPanelli">';
                    li += '<div><div class="new_div"><table class="new_table">';
                    li += '<tr><td style="width: 33.333%; text-align: left">';
                    li += '<img src="../@images/标签.png" class="new_img" />';
                    li += '<a id="lianxiren" style="text-decoration: none;" href="javascript:ClueView(' + result.d + ');">';
                    li += '线索</a></td><td style="width: 33.333%; text-align: center">';
                    li += '<a id="wsd" onclick="javascript:ClueView(' + result.d + ');" style="">编辑</a>';
                    li += '</td><td style="width: 33.333%; text-align: right">';
                    li += '<a href="javascript:DeleteClue(' + result.d + ');" style="margin-right: 10px;">';
                    li += '删除</a></td></tr></table></div>';
                    li += '<div class="MainView"><table cellpadding="0" cellspacing="0">';
                    li += '<tr><th>联系人：</th><td>' + ccName + '</td></tr>';
                    li += '<tr><th>公司名称：</th><td>' + ccCustomerName + '</td></tr>';
                    li += '<tr><th>电话：</th><td>' + ccTel + '</td></tr>';
                    li += '<tr><th>手机：</th><td>' + ccMobile + '</td></tr>';
                    li += '<tr><th>备注：</th><td>' + ccRemark + '</td></tr>';
                    li += '<tr><th>负责人：</th><td>' + $("#ddlccOperatorID option:selected").text() + '</td></tr>';
                    li += '<tr><th>创建人：</th><td>' + $("#hfCurrentOperatorName").val() + '</td></tr>';
                    li += '<tr><th>所属部门：</th><td>' + $("#ddlccDepartmentID option:selected").text() + '</td></tr>';
                    li += '</table></div></div></li>';

                    $("#liClue" + $("#hfccID").val()).remove();
                    $("#ulClue").prepend(li);
                    ShowListCommon('divCustomerClueView', null);

                }
                else {
                    ShowErrorMessage();
                }
            },
            onFail: function (err) {
                enableButton();
            }
        })
    }

    function AddClue() {
        $("#txtccName").val("");
        $("#txtccCustomerName").val("");
        $("#txtccTel").val("");
        $("#txtccMobile").val("");
        $("#txtccAddress").val("");
        $("#txtccRemark").val("");
        $("#ddlccOperatorID").val("");
        $("#ddlccDepartmentID").val("");
        ShowListCommon('divCustomerClueEdit', null);
    }

    function ClueView(id) {
        $("#hfccID").val(id);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../AjaxMethods.asmx/GetMarketActivityClue",
            data: "{ID:" + id + "}",
            dataType: 'json',
            success: function (result) {
                if (result.d != "") {
                    $(result.d).each(function () {
                        $("#txtccName").val(this['ccName']);
                        $("#txtccCustomerName").val(this['ccCustomerName']);
                        $("#txtccTel").val(this['ccTel']);
                        $("#txtccMobile").val(this['ccMobile']);
                        $("#txtccAddress").val(this['ccAddress']);
                        $("#txtccRemark").val(this['ccRemark']);
                        $("#ddlccOperatorID").val(this['ccOperatorID']);
                        $("#ddlccDepartmentID").val(this['ccDepartmentID']);
                    });
                    ShowListCommon('divCustomerClueEdit', null);
                }
            }
        })
    }

    function DeleteClue(id) {
        if (window.confirm("确定删除吗？")) {
            disableButton(this);
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/DeleteClue",
                data: "{ID:" + id + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        ShowMessage("操作成功！");
                        $("#liClue" + id).remove();
                        ShowListCommon('divCustomerClueView', null);
                    }
                    else {
                        ShowMessage("操作失败！请确认有权操作后重试");
                    }
                },
                onFail: function (err) {
                    enableButton();
                }
            });
        }
    }