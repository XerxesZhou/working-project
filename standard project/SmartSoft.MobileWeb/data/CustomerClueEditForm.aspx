<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerClueEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerClueEditForm" %>

<!DOCTYPE html>
<html>
<head>
    <title>线索详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <script src="../scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <%--钉钉--%>
    <script type="text/javascript">
        fromSource = "Clue";
        globalInterval = 500;
        function pageSetRight() {
            var ccID = $("#hfKeyID").val();
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '更多', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    dd.device.notification.actionSheet({
                        title: "更多操作", //标题
                        cancelButton: '取消', //取消按钮文本
                        otherButtons: ["编辑", "删除", "转为客户", "转给他人", "刷新"],
                        onSuccess: function (result) {
                            if (result.buttonIndex == 0) {
                                ShowListCommon('divCustomerClueEdit', this);
                            }
                            else if (result.buttonIndex == 1) {
                                dd.device.notification.confirm({
                                    message: "确定删除吗？",
                                    title: "提示",
                                    buttonLabels: ['确认', '取消'],
                                    onSuccess: function (result) {
                                        if (result.buttonIndex == 0) {
                                            $.ajax({
                                                type: "POST",
                                                contentType: "application/json",
                                                url: "../AjaxMethods.asmx/DeleteClue",
                                                data: "{ID:" + ccID + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                                                dataType: 'json',
                                                success: function (result) {
                                                    if (result.d + 0 > 0) {
                                                        dd.device.notification.toast({
                                                            icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                                            text: "操作成功", //提示信息
                                                            duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                                            delay: 0, //延迟显示，单位秒，默认0
                                                            onSuccess: function (result) {
                                                                window.location.href = "CustomerClueList.aspx?dd_nav_bgcolor=FF5E97F6";
                                                            },
                                                            onFail: function (err) { }
                                                        })
                                                    }
                                                    else {
                                                        ShowMessage("操作失败！请确认有权操作后重试");
                                                    }
                                                }
                                            });
                                        }
                                    },
                                    onFail: function (err) {
                                    }
                                })
                            }
                            else if (result.buttonIndex == 2) {
                                var ccStatusID = $("#hfCustomerClueStatusID").val();
                                if (ccStatusID != "58") {
                                    window.location.href = "CustomerAdd.aspx?Action=Insert&ID=0&ClueID=" + ccID;
                                }
                                else {
                                    dd.device.notification.toast({
                                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                        text: "该线索已转为客户", //提示信息
                                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                        delay: 0, //延迟显示，单位秒，默认0
                                        onSuccess: function (result) {
                                        }
                                    })
                                    return false;
                                }
                            }
                            else if (result.buttonIndex == 3) {
                                $("#ShowddlChangeClueOperatorID").show();
                            }
                            else if (result.buttonIndex == 4) {
                                window.location.reload();
                            }
                        },
                        onFail: function (err) { }
                    })

                },
                onFail: function (err) { }
            });
        }

        $(function () {
            dd.ready(function () {
                dd.device.geolocation.get({
                    targetAccuracy: 200,
                    coordinate: 1,
                    withReGeocode: true,
                    onSuccess: function (result) {
                        var address = result.address;
                        $("#lblcfhAddress").val(address);
                        $("#cfhAddress").html(address.substring(0, 12) + "...");
                    },
                    onFail: function (err) {
                    }
                });
            });
        });

        $(function () {
            var showPanel = getUrlParameter("showPanel");
            var ctr = getUrlParameter("ctr");
            $(".cssMainPanel").hide();
            if (showPanel == null || showPanel == "") {
                $("#divFollowHistoryList").show();
            }
            else {
                ShowListCommon(showPanel, ctr);
                $("#" + ctr).addClass("li_title");
            }

            var id = getUrlParameter("ID");
            if (id == 0) {
                ShowListCommon('divFollowHistoryEdit')
            }
        });

        $(function () {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
        })

        function CloseThisDiv(id) {
            $("#" + id).hide();
        }

        function ChangeClueOperatorID() {
            var ccID = $("#hfKeyID").val();
            var ccOperatorID = $("#ddlChangeClueOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ChangeClueOperator",
                data: "{ccID:" + ccID + ",ccOperatorID:'" + ccOperatorID + "',CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        window.location.href = "CustomerClueEditForm.aspx?Action=View&ID=" + ccID;
                    }
                }
            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto;">
                    <div id="divTitle" data-role="header" data-theme="b" style="height: 80px; background-color: White;
                        width: 100%; position: fixed; top: 0px; z-index: 3;" class="cssHeader">
                        <asp:Label runat="server" CssClass="TitlecusName" ID="lblccName" Text="新增客户"></asp:Label>
                        <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                            <tr>
                                <td>
                                    地址：
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblccAddress"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    负责人：
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblccOperatorName"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
                        <ul style="margin: 0px; padding: 0px;">
                            <li><a onclick="javascript:ShowListCommon('divFollowHistoryList',this)" class="li_title"
                                id="LinkMan">动态</a></li>
                            <li><a onclick="javascript:ShowListCommon('divCustomerClueView',this);" id="Base">基本</a></li>
                            <li runat="server" id="liOpptunity"><a onclick="javascript:ShowListCommon('divFollowPlanList',this)"
                                id="Opptunity">提醒</a></li>
                        </ul>
                    </div>
                    <div id="div_Content" data-role="content" style="padding: 0px; overflow: auto; width: 100%;
                        margin-top: 122px; z-index: 1003;">
                        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
                        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
                        <asp:HiddenField ID="hfWebPCDomain" runat="server" />
                        <asp:HiddenField ID="hfCustomerClueStatusID" runat="server" />
                        <asp:HiddenField ID="hfPCWebDomain" runat="server" />
                        <%--转移给他人--%>
                        <div id="ShowddlChangeClueOperatorID" style="z-index: 1004; position: fixed; width: 100%;
                            background-color: #aaa; height: 100%; display: none;">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2" style="color: #fff; text-shadow: none;">
                                        转移至业务员:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlChangeClueOperatorID" data-role="none" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                                            <a href="javascript:ChangeClueOperatorID();" class="SavaBtn" style="color: #fff;
                                                width: 100%; float: left;">确定</a>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                                            <a class="SavaBtn" style="color: #fff; width: 100%; float: left;" onclick="javascript:CloseThisDiv('ShowddlChangeClueOperatorID');">
                                                取消</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <%--线索基本信息--%>
                            <div id="divCustomerClueView" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <img src="../@images/标签.png" alt="标签" class="new_img" />
                                                <a>基本</a>
                                            </td>
                                            <td style="width: 50%; text-align: right;">
                                                <a onclick="javascript:ShowListCommon('divCustomerClueEdit',this);">编辑</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="MainView">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th>
                                                联系人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lbl_ccName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                公司名称：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccCustomerName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                相关活动：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccActivityName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                电话：
                                            </th>
                                            <td>
                                                <a runat="server" id='linkccTel' href="#">
                                                    <label runat="server" id="lblccTel">
                                                    </label>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                手机：
                                            </th>
                                            <td>
                                                <a runat="server" id='linkccMobile' href="#">
                                                    <label runat="server" id="lblccMobile">
                                                    </label>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                状态：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblStatus">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                备注：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccRemark">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                负责人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccOperator">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                创建人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccAddoperator">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                所属部门：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccDepartment">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                创建时间：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblccAddDate">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="divCustomerClueEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            联系人：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入联系人姓名" ID="txtccName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            公司名称：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入公司名称" ID="txtccCustomerName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            相关活动
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccActivityID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtccTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            手机：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入手机" ID="txtccMobile"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入备注" TextMode="MultiLine" ID="txtccRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            地址：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtccAddress" TextMode="MultiLine" placeholder="请输入地址">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <th>
                                            部门：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;">
                                    <a href="javascript:SaveCustomerClue();" class="SavaBtn">保存</a>
                                </div>
                            </div>
                            <%--提醒--%>
                            <div id="divFollowPlanList" class="cssMainPanel">
                                <ul data-role="listview" id="ulFollowPlan" data-icon="false" style="margin: 0px;">
                                    <asp:Repeater runat="server" ID="repCustomerFollowPlan">
                                        <ItemTemplate>
                                            <li class="MainPanelli">
                                                <div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0" style="padding: 5px;">
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td style="wdith: 50%;">
                                                                    <label style="color: #3C96DE; width: 25%">
                                                                        <%#Eval("cfpOperator")%></label>
                                                                </td>
                                                                <td style="width: 50%; text-align: right;">
                                                                    <label>
                                                                        <%#DateTime.Parse(Eval("cfpDate")+"").ToString("MM-dd HH:mm")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td colspan="2" style="line-height: 2em">
                                                                    <label>
                                                                        <%#Eval("cfpContent")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal ID="Literal1" runat="server" Text='<%#GetEachFile(Eval("cfpFilePath")+"",Eval("cfpID")+"") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td colspan="2" style="text-align: right;">
                                                                    <label>
                                                                        <%#DateTime.Parse(Eval("cfpAddDate")+"").ToString("yyyy-MM-dd HH:mm:ss") %></label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddFollowPlan();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divFollowPlanEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style="text-align: left;">
                                            <asp:HiddenField runat="server" ID="hfcfpID" Value="0" />
                                            <asp:TextBox Height="80" CssClass="cssTextarea" runat="server" placeholder="提醒内容"
                                                TextMode="MultiLine" ID="txtcfpContent"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-left: 10px;">
                                            <script type="text/javascript">
                                                function ClickPlanFile() {
                                                    $("#followplanfile").click();
                                                }
                                            </script>
                                            <div onclick="javascript:ClickPlanFile()">
                                                <img src="../@images/UploadPic.png" style="width: 50px;" />
                                            </div>
                                            <input type="file" data-role="none" id="followplanfile" onchange="javascript:uploadPlanFile('followplanfile')"
                                                style="opacity: 0; display: none;" multiple />
                                            <asp:HiddenField runat="server" ID="hffollowplanfile" />
                                            <div style="width: 100%; float: left">
                                                <div id="Div1">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            提醒时间：
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtcfpDate"
                                                placeholder="请选择日期" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                                    <a href="javascript:SaveFollowPlan();" class="SavaBtn" style="color: #fff; width: 100%;
                                        float: left;">确定</a>
                                </div>
                            </div>
                            <%--动态--%>
                            <div id="divFollowHistoryList" class="cssMainPanel">
                                <ul data-role="listview" id="ulFollowHistory" data-icon="false" style="margin: 0px;">
                                    <asp:Repeater runat="server" ID="repFollowHistory">
                                        <ItemTemplate>
                                            <li id='liFollowHistory<%#Eval("cfhID") %>' class="MainPanelli">
                                                <div class="MainView">
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" style="padding: 5px;">
                                                            <tr onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td style="wdith: 50%;">
                                                                    <label style="color: #3C96DE; width: 25%">
                                                                        <%#Eval("cfhOperator")%></label>
                                                                    <label style="color: #3C96DE; width: 25%; border: 1px solid #3C96DE; margin-left: 10px;
                                                                        border-radius: 3px; padding: 3px 3px 3px 3px; font-size: 10px;">
                                                                        <%#Eval("cfhType") %></label>
                                                                </td>
                                                                <td style="width: 50%; text-align: right;">
                                                                    <label>
                                                                        <%#DateTime.Parse(Eval("cfhAddDate")+"").ToString("MM-dd HH:mm")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td colspan="2" style="line-height: 2em">
                                                                    <label>
                                                                        <%#Eval("cfhContent") %></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal runat="server" ID="GetFile" Text='<%#GetEachFile(Eval("cfhFilePath")+"",Eval("cfhID")+"") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td colspan="2">
                                                                    <label>
                                                                        <%#Eval("cfhAddress") %></label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <%#GetLikeAndCommentCountHtml(Eval("cfhID") + "", Eval("cfhOperatorID") + "")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddFollowHistory();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divFollowHistoryEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style="text-align: left;">
                                            <asp:HiddenField runat="server" ID="hfcfhID" Value="0" />
                                            <asp:TextBox CssClass="cssTextarea" Height="80" runat="server" placeholder="勤跟进，勤记录！请输入新的跟进记录..."
                                                TextMode="MultiLine" ID="txtcfhContent"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-left: 10px;">
                                            <script type="text/javascript">
                                                function ClickFile() {
                                                    $("#followFile").click();
                                                }
                                            </script>
                                            <div onclick="javascript:ClickFile()">
                                                <img src="../@images/UploadPic.png" style="width: 50px;" />
                                            </div>
                                            <input type="file" data-role="none" id="followFile" onchange="javascript:uploadFile('followFile')"
                                                style="opacity: 0; display: none;" multiple />
                                            <asp:HiddenField runat="server" ID="hffollowFile" />
                                            <div style="width: 100%; float: left">
                                                <div id="picBox">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            跟进方式：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcfhTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            状态：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcfhStatusID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            跟进时间：
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="cfhDate"
                                                placeholder="请选择日期" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            下次跟进：
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtcfhNextFollowTime"
                                                placeholder="请选择日期" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            跟进位置：
                                        </th>
                                        <td>
                                            <asp:Label runat="server" ID="cfhAddress"></asp:Label>
                                            <asp:HiddenField runat="server" ID="lblcfhAddress"></asp:HiddenField>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                                    <a href="javascript:SaveCustomerFollowHistory();" data-role="none" class="SavaBtn">保存</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
