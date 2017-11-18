<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerEditForm" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <script src="../scripts/jquery-1.8.3.min.js"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=dupR0OqZhqawUyWcBo4jwRju"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #divNavigation ul li
        {
            float: left;
            width: 25%;
            height: 40px;
        }
    </style>

    <script type="text/javascript">
        fromSource = "Customer";
        function getPosition() {
            var address = $("#txtcusAddress").val();
            var myGeo = new BMap.Geocoder();
            // 将地址解析结果显示在地图上,并调整地图视野
            myGeo.getPoint(address, function (point) {
                if (point) {
                    $("#txtcusLongtitude").val(point.lng);
                    $("#txtcusLatitude").val(point.lat);
                }
            }, "深圳市");
        }

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
                ShowListCommon('divCustomerEdit')
            }
        });

        $(function () {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            $("#ddlcoOperatorID").val(currentOperatorID);
            $("#ddlcbOperatorID").val(currentOperatorID);
            $("#ddlcrOperatorID").val(currentOperatorID);
            $("#ddlcfpOperatorID").val(currentOperatorID);
        })

        function CloseThisDiv(id) {
            $("#" + id).hide();
        }

        function pageSetRight() {
            var cusID = $("#hfKeyID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/CheckInCustomerPool",
                data: "{ID:" + cusID + "}",
                dataType: 'json',
                success: function (result) {
                    var buttons = ["编辑", "删除", "转给他人", "放入公海", "刷新"];
                    if (result.d + 0 > 0) {
                        buttons = ["编辑", "删除", "转给他人", "认领", "刷新"]; //在公海中
                    }
                    dd.biz.navigation.setRight({
                        show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                        control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                        text: '更多', //控制显示文本，空字符串表示显示默认文本
                        onSuccess: function (result) {
                            dd.device.notification.actionSheet({
                                title: "更多操作", //标题
                                cancelButton: '取消', //取消按钮文本
                                otherButtons: buttons,
                                onSuccess: function (result) {
                                    if (result.buttonIndex == 0) {
                                        ShowListCommon('divCustomerEdit', this);
                                    }
                                    if (result.buttonIndex == 2) {
                                        $("#ShowddlChangeOperatorID").show();
                                    }
                                    if (result.buttonIndex == 3) {
                                        var currentOperatorID = $("#hfCurrentOperatorID").val();
                                        $.ajax({
                                            type: "POST",
                                            contentType: "application/json",
                                            url: "../AjaxMethods.asmx/PutInOrGetFromCustomerPool",
                                            data: "{ID:" + cusID + ",CurrentOperatorID:" + currentOperatorID + "}",
                                            dataType: 'json',
                                            success: function (result) {
                                                if (result.d + 0 > 0) {
                                                    ShowSuccessMessage();
                                                }
                                                else {
                                                    ShowErrorMessage();
                                                }
                                            }
                                        });
                                    }
                                    if (result.buttonIndex == 4) {
                                        window.location.reload();
                                    }
                                    else if (result.buttonIndex == 1) {
                                        var currentOperatorID = $("#hfCurrentOperatorID").val();
                                        dd.device.notification.confirm({
                                            message: "确定删除吗？",
                                            title: "提示",
                                            buttonLabels: ['确认', '取消'],
                                            onSuccess: function (result) {
                                                if (result.buttonIndex == 0) {
                                                    $.ajax({
                                                        type: "POST",
                                                        contentType: "application/json",
                                                        url: "../AjaxMethods.asmx/DeleteCustomer",
                                                        data: "{id:" + cusID + ",CurrentOperatorID:" + currentOperatorID + "}",
                                                        dataType: 'json',
                                                        success: function (result) {
                                                            if (result.d + 0 > 0) {
                                                                dd.device.notification.toast({
                                                                    icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                                                    text: "操作成功", //提示信息
                                                                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                                                    delay: 0, //延迟显示，单位秒，默认0
                                                                    onSuccess: function (result) {
                                                                        window.location.href = "CustomerList.aspx";
                                                                    },
                                                                    onFail: function (err) { }
                                                                })
                                                            }
                                                            else if(result.d + 0 == 0){
                                                                ShowMessage("操作失败！无权操作");
                                                            }
                                                            else if (result.d + 0 == -1) {
                                                                ShowMessage("操作失败！系统错误");
                                                            }
                                                        }
                                                    });
                                                }
                                            },
                                            onFail: function (err) {
                                            }
                                        })
                                    }
                                },
                                onFail: function (err) { }
                            })
                        },
                        onFail: function (err) { }
                    });
                }
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

        function ChangeOperatorID() {
            var cusID = $("#hfKeyID").val();
            var cusOperatorID = $("#ddlChangeOperatorID").val();
            var CurrentOperatorID = $("#hfCurrentOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ChangeCustomerOperator",
                data: "{cusID:" + cusID + ",cusOperatorID:" + cusOperatorID + ",CurrentOperatorID:" + CurrentOperatorID + "}",
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        ShowSuccessMessage();
                        window.location.href = "CustomerList.aspx"
                    }
                    else {
                        ShowErrorMessage();
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
                        <asp:Label runat="server" CssClass="TitlecusName" ID="lblCustomerName" Text="新增客户"></asp:Label>
                        <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                            <tr>
                                <td>
                                    地址：
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblAddress"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    负责人：
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblOperatorName"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <div id="div_Content" data-role="content" style="padding: 0px; overflow: auto; width: 100%;
                        margin-top: 122px; z-index: 1003;">
                        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
                        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
                        <asp:HiddenField ID="hfWebPCDomain" runat="server" />
                         <asp:HiddenField ID="hfPCWebDomain" runat="server" />
                        <asp:HiddenField ID="hfFirstLiLength" runat="server" value="4" />
                        <div>
                            <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
                                <ul style="margin: 0px; padding: 0px;">
                                    <li runat="server" id="liCustomerFollowHistory"><a onclick="javascript:ShowListCommon('divFollowHistoryList',this)"  class="li_title" id="FollowHistory">动态</a></li>
                                    <li runat="server" id="liCustomer"><a onclick="javascript:ShowListCommon('divCustomerView',this);" id="Base">基本</a></li>
                                    <li runat="server" id="liCustomerLinkMan"><a onclick="javascript:ShowListCommon('divLinkManList',this)" id="LinkMan">联系人</a></li>
                                    <li runat="server" id="liCoWorker"><a onclick="javascript:ShowListCommon('divCoWorkerList',this)" id="CoWorker">协作人</a></li>
                                    <li runat="server" id="liCustomerOpptunity"><a onclick="javascript:ShowListCommon('divOpptunityList',this)" id="Opptunity">商机</a></li>
                                    <li runat="server" id="liCustomerFollowPlan"><a onclick="javascript:ShowListCommon('divFollowPlanList',this)" id="Plan">提醒</a></li>
                                    <li runat="server" id="liCustomerBusiness"><a onclick="javascript:ShowListCommon('divBusinessList',this)" id="Business">合同</a></li>
                                    <li runat="server" id="liCustomerOrder"><a onclick="javascript:ShowListCommon('divOrderList',this)" id="Order">订单</a></li>
                                    <li runat="server" id="liAR"><a onclick="javascript:ShowListCommon('divARList','',this)" id="AR">应收</a></li>
                                    <li runat="server" id="liCustomerReceiptPlan"><a onclick="javascript:ShowListCommon('divReceiptPlanList',this)" id="ReceiptPlan">收款计划</a></li>
                                    <li runat="server" id="liCustomerReceipt"><a onclick="javascript:ShowListCommon('divReceiptList',this)" id="Receipt">收款</a></li>
                                    <li runat="server" id="liCustomerFeedback"><a onclick="javascript:ShowListCommon('divFeedbackList',this)" id="Feedback">反馈</a></li>
                                    <li runat="server" id="liCustomerFile"><a onclick="javascript:ShowListCommon('divDocumentList',this)" id="Document">文件</a></li>
                                </ul>
                            </div>
                            <%--转移给他人--%>
                            <div id="ShowddlChangeOperatorID" style="z-index: 1004; position: fixed; width: 100%; background-color: #aaa; height:100%; display:none;" >
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2" style="color: #fff;text-shadow: none;">
                                        转移至业务员:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:dropdownlist id="ddlChangeOperatorID" data-role="none" runat="server"></asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                                <a href="javascript:ChangeOperatorID();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                                确定</a> 
                                        </div>
                                    </td>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                                <a class="SavaBtn" style=" color:#fff; width:100%; float:left;" onclick="javascript:CloseThisDiv('ShowddlChangeOperatorID');">
                                                    取消</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                            <%--客户基本信息--%>
                            <div id="divCustomerView" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <img src="../@images/标签.png" alt="标签" class="new_img" />
                                                <a>基本</a>
                                            </td>
                                            <td style="width: 50%; text-align: right;">
                                                <a onclick="javascript:ShowListCommon('divCustomerEdit',this);">编辑</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="MainView">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th>
                                                名称：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                编号
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusCN">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                类型：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusKindID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                来源：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusSourceID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                性质：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusExtType2">
                                                </label>
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
                                                区域：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusAreaID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                简介：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusIntroduction">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                联系人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusContactor">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                电话：
                                            </th>
                                            <td>
                                                <a runat="server" id='linkcusTel' href="#">
                                                    <label runat="server" id="lblcusTel">
                                                    </label>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                地址：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusAddress">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                部门：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusDepartmentID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                负责人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusOperatorID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                创建时间：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcusAddDate">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                   
                                </div>
                            </div>
                            <div id="divCustomerEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            名称：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入名称" ID="txtcusName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            编号
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入编号" ID="txtcusCN"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            类型：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusKindID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            来源：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusSourceID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            性质：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusExtType2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            区域：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusAreaID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            简介：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入简介" TextMode="MultiLine" ID="txtcusIntroduction"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系人：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入联系人" ID="txtcusContactor"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtcusTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            地址：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtcusAddress" TextMode="MultiLine" placeholder="请输入地址"
                                                onblur="javascript:getPosition();">
                                            </asp:TextBox>
                                            <asp:TextBox runat="server" CssClass="Address" style="display:none" ID="txtcusLongtitude"></asp:TextBox>
                                            <asp:TextBox runat="server" CssClass="Address" style="display:none" ID="txtcusLatitude"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveCustomerInfo();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                             <%--跟进--%>
                            <div id="divFollowHistoryList" class="cssMainPanel">
                                <ul data-role="listview" id="ulFollowHistory" data-icon="false" style="margin: 0px;">
                                    <asp:repeater runat="server" id="repFollowHistory">
                                        <ItemTemplate>
                                            <li id='liFollowHistory<%#Eval("cfhID") %>' class="MainPanelli">
                                                <div class="MainView">
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0"  style=" padding:5px;">
                                                            <tr  onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td style="wdith:50%;">
                                                                    <label style="color: #3C96DE; width:25%"><%#Eval("cfhOperator")%></label>
                                                               
                                                                    <label style="color: #3C96DE; width:25%; border: 1px solid #3C96DE;margin-left: 10px;border-radius: 3px;padding: 3px 3px 3px 3px;font-size: 10px;"><%#Eval("cfhType") %></label>
                                                                </td>
                                                                <td style="width: 50%;text-align: right;">
                                                                    <label><%#DateTime.Parse(Eval("cfhAddDate")+"").ToString("MM-dd HH:mm")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr  onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td colspan="2" style=" line-height:2em">
                                                                    <label><%#Eval("cfhContent") %></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal runat="server" id="GetFile" text='<%#GetEachFile(Eval("cfhFilePath")+"",Eval("cfhID")+"") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                                <td colspan="2">
                                                                
                                                                    <label><%#Eval("cfhAddress") %></label>
                                                                    
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
                                    </asp:repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddFollowHistory();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divFollowHistoryEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style=" text-align:left;">
                                            <asp:hiddenfield runat="server" id="hfcfhID" value="0" />
                                            <asp:textbox CssClass="cssTextarea" height="80"  runat="server" placeholder="勤跟进，勤记录！请输入新的跟进记录..." textmode="MultiLine"  id="txtcfhContent"  ></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style=" padding-left:10px;">
                                            <script type="text/javascript">
                                                function ClickFile() {
                                                    $("#followFile").click();
                                                }
                                            </script>
                                            <div onclick="javascript:ClickFile()">
                                                <img src="../@images/UploadPic.png" style=" width:50px;" />
                                            </div>
                                            <input type="file" data-role="none"  id="followFile" onchange="javascript:uploadFile('followFile')" style="opacity:0; display:none;" multiple/>
                                            <asp:hiddenfield runat="server" id="hffollowFile" />
                                            <div style="width:100%; float:left">
                                                <div id="picBox"></div>
                                            </div>
                                        </td>   
                                    </tr>
                                    <tr>
                                        <th>
                                            跟进方式：
                                        </th>
                                        <td>
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcfhTypeID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            状态：
                                        </th>
                                        <td>
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcfhStatusID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>跟进时间：</th>
                                        <td><input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="cfhDate" placeholder="请选择日期"  /></td>
                                    </tr>
                                    <tr>
                                        <th>
                                           下次跟进：
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtcfhNextFollowTime" placeholder="请选择日期"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            跟进位置：
                                        </th>
                                        <td>

                                            <asp:label runat="server" id="cfhAddress"></asp:label>
                                            <asp:hiddenfield runat="server" id="lblcfhAddress"></asp:hiddenfield>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center; text-shadow:none;">
                                    <a href="javascript:SaveCustomerFollowHistory();" data-role="none" class="SavaBtn">保存</a>
                                </div>
                            </div>
                            <%--联系人--%>
                            <div id="divLinkManList" class="cssMainPanel">
                                <ul data-role="listview" id="ulLinkMan" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repLinkMan">
                                        <ItemTemplate>
                                            <li id='liLinkMan<%#Eval("clmID")%>' class="MainPanelli">
                                                <div>
                                                    <div class="new_div">
                                                        <table class="new_table">
                                                            <tr>
                                                                <td style="width: 33.333%; text-align: left">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    <a id="lianxiren" style="text-decoration: none;" href='javascript:LinkManView(<%#Eval("clmID")%>);'>
                                                                        联系人</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: center">
                                                                    <%-- <a id="wsd" onclick="javascript:LinkManEidt('divLinkManEdit',<%#Eval("clmID")%>);" style="">编辑</a>--%>
                                                                    <a id="wsd" onclick="javascript:LinkManView(<%#Eval("clmID")%>);" style="">编辑</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: right">
                                                                    <a href="javascript:DeleteLinkMan(<%#Eval("clmID") %>);" style="margin-right: 10px;">
                                                                        删除</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <th>
                                                                    姓名：
                                                                </th>
                                                                <td>
                                                                                
                                                                        <%#Eval("clmName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    性别：
                                                                </th>
                                                                <td>
                                                                        <%#Eval("clmSex")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    联系人类型：
                                                                </th>
                                                                <td>
                                                                    <%#Eval("clmTypeName") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    手机：
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmMobile")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    电话：
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmTel")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>Email：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmEmail")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>QQ：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmQQ")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>微信：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmWeChat")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>备注：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("clmRemark")%>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>创建人：</span>
                                                                </th>
                                                                <td class="table_b">
                                                                    <%#Eval("opeName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>创建时间：</span>
                                                                </th>
                                                                <td>
                                                                    <%#DateTime.Parse(Eval("clmAddDate") + "").ToString("yyyy-MM-dd HH:mm:ss")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul> 
                                <img src="../@images/添加.png" onclick="javascript:AddLinkMan();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divLinkManEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            姓名：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfclmID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入姓名" ID="txtclmName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            性别：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlclmSex">
                                                <asp:ListItem Value="男" Text="男"></asp:ListItem>
                                                <asp:ListItem Value="女" Text="女"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系人类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlclmTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            手机：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入手机" ID="txtclmMobile"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtclmTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            Email：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入Eamil" ID="txtclmEmail"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            QQ：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入QQ" ID="txtclmQQ"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            微信：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入微信" ID="txtclmWeChat"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入备注" ID="txtclmRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveLinkMan();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                             <%--协作人--%>
                            <div id="divCoWorkerList" class="cssMainPanel">
                                <ul data-role="listview" id="ulCoWorker" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repCoWorker">
                                        <ItemTemplate>
                                            <li id='liCoWorker<%#Eval("cwID") %>' class="MainPanelli">
                                                <div>
                                                    <div class="new_div">
                                                        <table class="new_table">
                                                            <tr>
                                                                <td style="width: 33.333%; text-align: left">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    <a style="text-decoration: none;">
                                                                        协作人</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: right">
                                                                    <a href="javascript:DeleteCoWorkerView(<%#Eval("cwID") %>);" style="margin-right: 10px;">
                                                                        删除</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <th>
                                                                    协作人：
                                                                </th>
                                                                <td>
                                                                                
                                                                        <%#Eval("cwOperatorName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    部门：
                                                                </th>
                                                                <td>
                                                                        <%#Eval("cwOperatorDepartment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul> 
                                <img src="../@images/添加.png" onclick="javascript:AddCoWorker();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divCoWorkerEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            协作人：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcwID" Value="0" />
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcwOperatorID" onchange="javascript:GetDepartment()">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hfdepartment" Value="0" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveCoWorker();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                            <%--商机--%>
                            <div id="divOpptunityList" class="cssMainPanel">
                                <ul data-role="listview" id="ulOpptunity" data-icon="false" style="margin: 0px;">
                                    <asp:Repeater runat="server" ID="repOpptunity">
                                        <ItemTemplate>
                                            <li id='liOpptunity<%#Eval("coID")%>' class="MainPanelli">
                                                <div class="new_div">
                                                    <table class=" new_table">
                                                        <tr>
                                                            <td style="width: 33.333%; text-align: left;">
                                                                <a href="javascript:GoToCenter(<%#Eval("coID")%>,'CustomerOpptunity');">
                                                                    <img src="../@images/标签.png" class="new_img" />商机</a>
                                                            </td>
                                                            <td style="width: 33.333%; text-align: center;">
                                                                <a onclick="javascript:OpptunityView(<%#Eval("coID")%>);">编辑</a>
                                                            </td>
                                                            <td style="width: 33.333%; text-align: right;">
                                                                <a href="javascript:DeleteOpptunity(<%#Eval("coID") %>);" style="margin-right: 10px;">
                                                                    删除</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                商机名称：
                                                            </th>
                                                            <td>
                                                                <%#Eval("coName")%>
                                                            </td>
                                                        </tr>
                                                            <tr>
                                                            <th>
                                                                商机日期：
                                                            </th>
                                                            <td>
                                                                    <%#Eval("coDate") + "" == "" ? "" : DateTime.Parse(Eval("coDate") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                商机阶段：
                                                            </th>
                                                            <td> 
                                                                <%#GetPhaseName(Eval("coPhaseID")+"")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                预计金额：
                                                            </th>
                                                            <td>
                                                                <%#string.Format("{0:N0}",(Eval("coPredictAmount"))) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                预签单日：
                                                            </th>
                                                            <td>
                                                                <%#Eval("coPredictFinishDate") + "" == "" ? "" : DateTime.Parse(Eval("coPredictFinishDate") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                状态：
                                                            </th>
                                                            <td>
                                                                <%#GetSatusName( Eval("coStatusID").ToString())%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                商机来源：
                                                            </th>
                                                            <td>
                                                                <%#GetTypeName( Eval("coOpptunitySourceID").ToString())%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                负责人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("opeName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                创建时间：
                                                            </th>
                                                            <td>
                                                                <%#DateTime.Parse(Eval("coAddDate")+"").ToString("yyyy-MM-dd HH:mm:ss")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddOpptunity();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divOpptunityEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            商机名称：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcoID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入商机名称" ID="txtcoName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            商机日期：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcoDate" placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            商机阶段：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoPhaseID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计金额：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计金额" ID="txtcoPredictAmount" CssClass="inputNumber2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预签单日：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcoPredictFinishDate" placeholder="请选择日期" value='<%=DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            状态：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoStatusID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            商机来源：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoOpptunitySourceID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveOpptunity();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                            <%--合同--%>
                            <div id="divBusinessList" class="cssMainPanel">
                                <ul data-role="listview" id="ulBusiness" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repCustomerBusiness">
                                        <ItemTemplate>
                                            <li id="liBusiness<%#Eval("cbID")%>"  class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.33%; text-align: left">
                                                                <a href="javascript:GoToCenter(<%#Eval("cbID")%>,'CustomerBusiness')">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    合同 </a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: center">
                                                                <a onclick="javascript:BusinessView(<%#Eval("cbID")%>);">编辑</a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: right">
                                                                <a style=" margin-right:10px;" href="javascript:DeleteBusiness(<%#Eval("cbID")%>);">删除</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                合同名称：
                                                            </th>
                                                            <td  id="BTable<%#Eval("cbID") %>">
                                                                <%#Eval("cbName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                合同类型：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbTypeName") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <th>
                                                                合同日期：
                                                            </th>
                                                            <td>
                                                                <%#DateTime.Parse(Eval("cbDate") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                合同描述：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbRemark")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                合同金额：
                                                            </th>
                                                            <td id="TotalAmount<%#Eval("cbID") %>">
                                                                <%#Eval("cbTotalAmount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                已收金额：
                                                            </th>
                                                            <td id="GotAmount<%#Eval("cbID") %>">
                                                                <%#Eval("cbGotAmount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                未收金额：
                                                            </th>
                                                            <td id="NotGotAmount<%#Eval("cbID") %>">
                                                                <%#Eval("cbNotGotAmount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                合同状态：
                                                            </th>
                                                            <td id="StatusName<%#Eval("cbID") %>">
                                                                <%#Eval("cbStatusName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                售后状态：
                                                            </th>
                                                            <td id="AfterName<%#Eval("cbID") %>">
                                                                <%#Eval("cbAfterName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                到期日期：
                                                            </th>
                                                            <td id="ExpiredDate<%#Eval("cbID") %>">
                                                                <%#Eval("cbExpiredDate") + "" == "" ? "" : DateTime.Parse(Eval("cbExpiredDate") + "").ToString("yyyy-MM-dd HH:mm")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                负责人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbOperatorName")%>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <th>
                                                                合同描述：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbRemark")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                通知人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbNotifyOperatorName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                创建人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbAddOperatorName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                创建时间：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cbAddDate") + "" == "" ? "" : DateTime.Parse(Eval("cbAddDate") + "").ToString("yyyy-MM-dd HH:mm")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddBusiness();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divBusinessEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            合同名称：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcbID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入合同名称" ID="txtcbName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同日期：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcbDate" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同类型：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbBusinessType">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <th>
                                            合同金额：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同金额" ID="txtcbTotalAmount" CssClass="inputNumber2"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            合同描述：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同描述" TextMode="MultiLine" ID="txtcbRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            到期日期：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcbExpiredDate" value='<%=DateTime.Now.AddYears(1).ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            通知人:
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbNotifyOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveBusiness();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                            <%--订单--%>
                            <div id="divOrderList" class="cssMainPanel">
                                <ul data-role="listview" id="ulOrder" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repOrder">
                                        <ItemTemplate>
                                            <li class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.33%; text-align: left">
                                                                <a>
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    订单明细 </a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: center">
                                                               
                                                            </td>
                                                            <td style="width: 33.33%; text-align: right">
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                订单号：
                                                            </th>
                                                            <td>
                                                                <%#Eval("FBillNoDD")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                订单日期：
                                                            </th>
                                                            <td>
                                                                <%#DateTime.Parse(Eval("FDateDD") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                货品编号：
                                                            </th>
                                                            <td>
                                                                <%#Eval("FItemIDNumber")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                货品名称：
                                                            </th>
                                                            <td>
                                                                <%#Eval("FItemIDName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                货品型号：
                                                            </th>
                                                            <td>
                                                                <%#Eval("Fmodel")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                货品名称：
                                                            </th>
                                                            <td>
                                                                <%#Eval("FItemIDName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                数量：
                                                            </th>
                                                            <td>
                                                                <%#decimal.Parse(Eval("FQty") + "").ToString("N0")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                单价：
                                                            </th>
                                                            <td>
                                                                <%#decimal.Parse(Eval("FPrice") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                金额：
                                                            </th>
                                                            <td>
                                                                <%#decimal.Parse(Eval("FAmount") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                已出数量：
                                                            </th>
                                                            <td>
                                                                <%#decimal.Parse(Eval("FCommitQty") + "").ToString("N0")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                未出数量：
                                                            </th>
                                                            <td>
                                                                <%#(decimal.Parse(Eval("FQty") + "") - decimal.Parse(Eval("FCommitQty") + "")).ToString("N0")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <%--应收--%>
                            <div id="divARList" class="cssMainPanel">
                                <ul data-role="listview" id="ulAR" data-icon="false" style=" margin:0px;">
                                     <asp:Repeater runat="server" ID="repAR">
                                        <ItemTemplate>
                                            <li class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.33%; text-align: left">
                                                                <a>
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                     <%#Eval("Y") + "年" + Eval("M") + "月"%></a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: center">
                                                               
                                                            </td>
                                                            <td style="width: 33.33%; text-align: right">
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                       <tr>
                                                            <th>
                                                                期初余款：
                                                            </th>
                                                            <td>
                                                                <%#Eval("StartBalance") + "" == "" ? "" : decimal.Parse(Eval("StartBalance") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                本期应收：
                                                            </th>
                                                            <td>
                                                                <%#Eval("MustAmount") + "" == "" ? "" : decimal.Parse(Eval("MustAmount") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                本期实收：
                                                            </th>
                                                            <td>
                                                                <%#Eval("RealAmount") + "" == "" ? "" : decimal.Parse(Eval("RealAmount") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                期末余额：
                                                            </th>
                                                            <td>
                                                                <%#Eval("EndBalance") + "" == "" ? "" : decimal.Parse(Eval("EndBalance") + "").ToString("N2")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <%--收款--%>
                            <div id="divReceiptList" class="cssMainPanel">
                                <ul data-role="listview" id="ulReceipt" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repCustomerReceipt">
                                        <ItemTemplate>
                                            <li id="liReceipt<%#Eval("crID")%>" class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.33%; text-align: left">
                                                                <img src="../@images/标签.png" class="new_img" />
                                                                <a href='javascript:ReceiptView(<%#Eval("crID")%>);'>收款</a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: center">
                                                                <a onclick="javascript:ReceiptView(<%#Eval("crID")%>);">编辑</a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: right">
                                                                <a style=" margin-right:10px;" href="javascript:DeleteReceipt(<%#Eval("crID")%>);">删除</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                合同名称：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crBusinessName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                收款类型：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crTypeName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                期次：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crBatchID")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                收款金额：
                                                            </th>
                                                            <td id="crAmount<%#Eval("crID") %>">
                                                                <%#Eval("crAmount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                负责人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("opeName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                收款日期：
                                                            </th>
                                                            <td>
                                                                <%#DateTime.Parse(Eval("crDate") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                备注：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crRemark")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" class="" onclick="javascript:AddReceipt();"
                                    style="height: 45px; position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divReceiptEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            关联合同：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrBusinessID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            收款类型：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            收款金额：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcrID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入收款金额" ID="txtcrAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            期次：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrBatchID">
                                                <asp:ListItem Text="第一期" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="第二期" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="第三期" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="第四期" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="第五期" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="第六期" Value="6"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            收款日期：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcrDate" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注：
                                        </th>
                                        <td>
                                            <input  type="text" id="txtcrRemark"/>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveReceipt();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>

                            </div>
                             <%--收款计划--%>
                            <div id="divReceiptPlanList" class="cssMainPanel">
                                <ul data-role="listview" id="ulReceiptPlan" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repBusinessReceiptPlan">
                                        <ItemTemplate>
                                            <li id="liReceiptPlan<%#Eval("crpID")%>" class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.33%; text-align: left">
                                                                <img src="../@images/标签.png" class="new_img" />
                                                                <a href='javascript:ReceiptPlanView(<%#Eval("crpID")%>);'>计划</a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: center">
                                                                <a onclick="javascript:ReceiptPlanView(<%#Eval("crpID")%>);">编辑</a>
                                                            </td>
                                                            <td style="width: 33.33%; text-align: right">
                                                                <a style=" margin-right:10px;" href="javascript:DeleteReceiptPlan(<%#Eval("crpID")%>);">删除</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                合同名称：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crpBusinessName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                收款类型：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crpTypeName") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                计划金额：
                                                            </th>
                                                            <td id="crpAmount<%#Eval("crpID") %>">
                                                                <%#Eval("crpAmount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                收款期次：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crpBatchID") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                业务员：
                                                            </th>
                                                            <td>
                                                                <%#Eval("opeName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                计划日期：
                                                            </th>
                                                            <td>
                                                                <%#DateTime.Parse(Eval("crpDate") + "").ToString("yyyy-MM-dd")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                完成状态：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crpStatus")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                备注：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crpRemark") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddReceiptPlan();"
                                    style="height: 45px; position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divReceiptPlanEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            关联合同：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrpBusinessID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            收款类型：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrpTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            计划金额：
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcrpID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入收款金额" ID="txtcrpAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            期次：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrpBatchID">
                                                <asp:ListItem Text="第一期" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="第二期" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="第三期" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="第四期" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="第五期" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="第六期" Value="6"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            业务员：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcrpOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            计划日期：
                                        </th>
                                        <td>
                                            <input type="date" id="txtcrpDate" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注：
                                        </th>
                                        <td>
                                            <input type="text" id="txtcrpRemark"  />
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveReceiptPlan();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>

                            </div>
                            <%--提醒--%>
                            <div id="divFollowPlanList" class="cssMainPanel">
                                <ul data-role="listview" id="ulFollowPlan" data-icon="false" style="margin: 0px;">
                                    <asp:repeater runat="server" id="repCustomerFollowPlan">
                                        <ItemTemplate>
                                            <li class="MainPanelli">
                                                <div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0" style=" padding:5px;">
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td style="wdith:50%;">
                                                                    <label style="color: #3C96DE; width:25%"><%#Eval("cfpOperator")%></label>
                                                                </td>
                                                                <td style="width: 50%;text-align: right;">
                                                                    <label><%#DateTime.Parse(Eval("cfpDate")+"").ToString("MM-dd HH:mm")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td colspan="2" style=" line-height:2em">
                                                                    <label><%#Eval("cfpContent")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal runat="server" text='<%#GetEachFile(Eval("cfpFilePath")+"",Eval("cfpID")+"") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                                <td colspan="2" style=" text-align:right;">
                                                                
                                                                    <label><%#DateTime.Parse(Eval("cfpAddDate")+"").ToString("yyyy-MM-dd HH:mm:ss") %></label>
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                            
                                            </li>
                                        </ItemTemplate>
                                    </asp:repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddFollowPlan();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divFollowPlanEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2" style=" text-align:left;">
                                            <asp:hiddenfield runat="server" id="hfcfpID" value="0" />
                                            <asp:textbox height="80" cssClass="cssTextarea"  runat="server" placeholder="提醒内容" textmode="MultiLine"  id="txtcfpContent"  ></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style=" padding-left:10px;">
                                            <script type="text/javascript">
                                                function ClickPlanFile() {
                                                    $("#followplanfile").click();
                                                }
                                            </script>
                                            <div onclick="javascript:ClickPlanFile()">
                                                <img src="../@images/UploadPic.png" style=" width:50px;" />
                                            </div>
                                            <input type="file" data-role="none"  id="followplanfile" onchange="javascript:uploadPlanFile('followplanfile')" style="opacity:0; display:none;" multiple/>
                                            <asp:hiddenfield runat="server" id="hffollowplanfile" />
                                            <div style="width:100%; float:left">
                                                <div id="Div1"></div>
                                            </div>
                                        </td>   
                                    </tr>
                                    <tr>
                                        <th>提醒时间：</th>
                                        <td><input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtcfpDate" placeholder="请选择日期"  />
                                            
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveFollowPlan();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>
                            </div>
                            <%--反馈--%>
                            <div id="divFeedbackList" class="cssMainPanel">
                                <ul data-role="listview" id="ulFeedback" data-icon="false" style=" margin:0px;>
                                    <asp:Repeater runat="server" ID="repFeedback">
                                        <ItemTemplate>
                                            <li id="liFeedback<%#Eval("cfbID")%>"  class="MainPanelli">
                                                <div class="new_div">
                                                    <table class="new_table">
                                                        <tr>
                                                            <td style="width: 33.333%; text-align: left;">
                                                                <a href="javascript:GoToCenter(<%#Eval("cfbID")%>,'CustomerFeedback')">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    反馈
                                                                </a>
                                                            </td>
                                                            <td style="width: 33.333%; text-align: center;">
                                                                <a onclick="javascript:FeedbackView(<%#Eval("cfbID")%>)">编辑</a>
                                                            </td>
                                                            <td style="width: 33.333%; text-align: right;">
                                                                <a href="javascript:DeleteFeedback(<%#Eval("cfbID")%>);" style="margin-right: 10px;">
                                                                    删除</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="MainView">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <th>
                                                                反馈类型：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbTypeName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                联系人：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbLinkman")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                联系电话：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbTelephone")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                电子邮件:
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbEmail")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                相关合同：
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbOrderRelated")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                反馈内容:
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbContent") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                处理结果:
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbResult") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                处理状态:
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbStatus") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                申请处理人:
                                                            </th>
                                                            <td>
                                                                <%#Eval("cfbHandleOperator")%>
                                                            </td>
                                                        </tr>
                                                        <tr class="trHandOperator<%#Eval("cfbHandleOperatorID")%>" style="display: none">
                                                            <td colspan="2">
                                                                <a onclick="javascript:showDivHandle(<%#Eval("cfbID") %>)" style="cursor: pointer;
                                                                    padding: 10px; background-color: #2489ce; text-align: center; display: block;
                                                                    margin: 0 auto; color: #fff; font-size: 16px;font-family:Helvetica,Arial,sans-serif; font-weight: normal;
                                                                    border-radius: 5px;">处理</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:HiddenField ID="hfHandleOperatorID" runat="server" />
                                </ul>
                                <script type="text/javascript">
                                    $(function () {
                                        var HandleOperatorID = $("#hfHandleOperatorID").val();
                                        if (HandleOperatorID != "") {
                                            $(".trHandOperator" + HandleOperatorID).css("display", "block");
                                        }
                                    })
                                </script>
                                <img src="../@images/添加.png" onclick="javascript:AddFeedback();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divFeedbackEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            反馈类型：
                                        </th>
                                        <td>
                                            <asp:HiddenField ID="hfcfbID" runat="server" Value="0" />
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcfbFeedbackTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系人：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtcfbLinkman" runat="server" placeholder="请输入联系人"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系电话：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入联系电话" ID="txtcfbTelephone"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电子邮件：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电子邮件" ID="txtcfbEmail"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            相关合同：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入相关合同" ID="txtcfbOrderRelated"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            反馈内容：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入反馈内容" ID="txtcfbContent" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trcfbResult" style="display: none">
                                        <th>
                                            处理结果：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入处理结果" ID="txtcfbResult" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trcfbStatus" style="display: none">
                                        <th>
                                            处理状态：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcfbStatus">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            申请处理人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcfbHandleOperator">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveFeedback();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
                                </div>

                            </div>
                            <%--文件--%>
                            <div id="divDocumentList" class="cssMainPanel">
                                <ul data-role="listview" id="ulDocument" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repDocument">
                                        <ItemTemplate>
                                            <li id='liDocument<%#Eval("cfID") %>' class="MainPanelli">
                                                <div>
                                                    <div class="new_div">
                                                        <table class="new_table">
                                                            <tr>
                                                                <td style="width: 33.333%; text-align: left">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    <a style="text-decoration: none;">
                                                                        文件</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: right">
                                                                    <a href="javascript:DeleteDocument(<%#Eval("cfID") %>);" style="margin-right: 10px;">
                                                                        删除</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <th>
                                                                    文件描述：
                                                                </th>
                                                                <td>      
                                                                    <%#Eval("cfName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal ID="Literal1" runat="server" text='<%#GetEachFile(Eval("cfCN")+"",Eval("cfID")+"") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <%#Eval("cfUploadTime") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul> 
                                <img src="../@images/添加.png" onclick="javascript:AddDocument();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divDocumentEdit" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 100%;">
                                                <label class="cssEditlbl">编辑区域</label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            文件描述：
                                        </th>
                                        <td>
                                            <asp:HiddenField ID="hfcfID" runat="server" Value="0" />
                                            <asp:TextBox ID="txtcfName" runat="server" placeholder="请输入文件描述"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style=" padding-left:10px;">
                                            <script type="text/javascript">
                                                function ClickDocumentFile() {
                                                    $("#DocumentFile").click();
                                                }
                                            </script>
                                            <div onclick="javascript:ClickDocumentFile()">
                                                <img src="../@images/UploadPic.png" style=" width:50px;" />
                                            </div>
                                            <input type="file" data-role="none"  id="DocumentFile" onchange="javascript:uploadDocumentFile('DocumentFile')" style="opacity:0; display:none;" multiple/>
                                            <asp:hiddenfield runat="server" id="hfDocumentFile" />
                                            <div style="width:100%; float:left">
                                                <div id="DivDocumentFile"></div>
                                            </div>
                                        </td>   
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                        <a href="javascript:SaveDocument();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                        确定</a>
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
