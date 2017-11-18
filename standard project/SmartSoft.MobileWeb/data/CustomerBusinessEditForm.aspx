<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerBusinessEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerBusinessEditForm" %>

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
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js"></script>
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
        fromSource = "Business";
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
                ShowListCommon('divCustomerBusinessEdit')
            }
        });

        $(function () {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            if ($("#ddlcbOperatorID").val() == "") {
                $("#ddlcbOperatorID").val(currentOperatorID);
            }
            $("#ddlcrOperatorID").val(currentOperatorID);
            $("#ddlcrpOperatorID").val(currentOperatorID);
        })

        function pageSetRight() {
            var cbID = $("#hfKeyID").val();
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '更多', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {

                    dd.device.notification.actionSheet({
                        title: "更多操作", //标题
                        cancelButton: '取消', //取消按钮文本
                        otherButtons: ["编辑", "删除", "刷新"],
                        onSuccess: function (result) {
                            if (result.buttonIndex == 0) {
                                ShowListCommon('divCustomerBusinessEdit', this);
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
                                                url: "../AjaxMethods.asmx/DeleteBusiness",
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
                                                                ShowMessage("操作成功！");
                                                                window.close();
                                                            },
                                                            onFail: function (err) { }
                                                        })
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

        function ShowListCommon2(div, ctr, source) {
            $("#ddlBusinessStatusID").hide();
            $("#ddlAfterSalesStatusID").hide();
            $("#ddl" + source + "StatusID").show();
            fromSource = source;
            ShowListCommon(div, ctr);
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
                        <asp:label runat="server" cssclass="TitlecusName" id="lblBusinessName"></asp:label>
                        <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                            <tr>
                                <td>
                                    合同类型：
                                </td>
                                <td>
                                    <asp:label runat="server" id="lblBusinessType"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label runat="server" id="lblcfhFromName"></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="div_Content" data-role="content" style="padding: 0px; overflow: auto; width: 100%;
                        margin-top: 122px; z-index: 1003;">
                        <asp:hiddenfield id="hfCurrentOperatorID" runat="server" />
                        <asp:hiddenfield id="hfCurrentOperatorName" runat="server" />
                        <asp:hiddenfield id="hfWebPCDomain" runat="server" />
                        <asp:hiddenfield id="hfOpptunityID" runat="server" />
                        <asp:hiddenfield id="hfPCWebDomain" runat="server" />
                        <asp:HiddenField ID="hfFirstLiLength" runat="server" value="4" />

                        <div>
                            <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
                                <ul style="margin: 0px; padding: 0px;">
                                    <li><a onclick="javascript:ShowListCommon2('divFollowHistoryList',this,'Business')" id="FollowHistory" class="li_title">
                                        动态</a></li>
                                    <li runat="server" id="liBusiness"><a onclick="javascript:ShowListCommon('divCustomerBusinessList',this)"
                                        id="Business">详情</a></li>
                                    <li><a onclick="javascript:ShowListCommon('divFollowPlanList',this)" id="Plan">提醒</a></li>
                                    <li><a onclick="javascript:ShowListCommon('divCoWorkerList',this)" id="CoWorker">协作人</a></li>
                                    <li><a onclick="javascript:ShowListCommon('divReceiptPlanList',this)" id="ReceiptPlan">收款计划</a></li>
                                    <li><a onclick="javascript:ShowListCommon('divReceiptList',this)" id="Receipt">收款</a></li>
                                    <li runat="server" id="liOrder"><a onclick="javascript:ShowListCommon('divOrderList','',this)" id="Order">订单</a></li>
                                    <li><a onclick="javascript:ShowListCommon2('divFollowHistoryList',this,'AfterSales')" id="Aftermarket">售后</a></li>
                                </ul>
                            </div>
                            <%--合同详情--%>
                            <div id="divCustomerBusinessList" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 33.333%;">
                                                <img src="../@images/标签.png" alt="标签" class="new_img" />
                                                <a>基本</a>
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
                                                合同名称：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                合同日期：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbDate">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                合同类型：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbBusinessType">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                合同金额：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbTotalAmount">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                已收金额：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbGotAmount">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                未收金额：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbNotGotAmount">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                合同状态：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbStatus">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                售后状态：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbAfterID">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                到期日期：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbExpiredDate">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                负责人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbOperator">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                合同描述：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbRemark">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                通知人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbNotifyOperatorName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                创建人：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbAddOperator">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                创建时间：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblcbAddDate">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="divCustomerBusinessEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            合同名称
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
                                            <asp:HiddenField runat="server" ID="hfcusID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入合同名称（必填）" ID="txtcbName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbBusinessType">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同日期
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" type="date" ID="txtcbDate" placeholder="请选择日期（必填）"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同金额
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同金额" ID="txtcbTotalAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            业务员
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同描述
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同描述" TextMode="MultiLine" ID="txtcbRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            到期日期：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" type="date" ID="txtcbExpiredDate"></asp:TextBox>
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
                            <%--动态--%>
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
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlBusinessStatusID">
                                            </asp:dropdownlist>
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlAfterSalesStatusID" style="display:none;">
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
                                                                    <asp:Literal ID="Literal1" runat="server" text='<%#GetEachFile(Eval("cfpFilePath")+"",Eval("cfpID")+"") %>'></asp:Literal>
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
                            <%--收款--%>
                            <div id="divReceiptList" class="cssMainPanel">
                                <ul data-role="listview" id="ulReceipt" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater runat="server" ID="repBusinessReceipt">
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
                                                                收款类型：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crTypeName") %>
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
                                                                收款期次：
                                                            </th>
                                                            <td>
                                                                <%#Eval("crBatchID") %>
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
                                                                <%#Eval("crRemark") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddReceipt();"
                                    style="height: 45px; position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divReceiptEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
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
                                            业务员：
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
                                            <input type="text" id="txtcrRemark"  />
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
                                                                <%#DateTime.Parse(Eval("crpDate") + "").ToString("yyyy-MM-dd HH:mm:ss")%>
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
                                            <input type="text"  readonly="readonly" onclick="javascript:SelectDate(this)" id="txtcrpDate" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
