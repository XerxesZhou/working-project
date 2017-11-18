<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOpptunityEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerOpptunityEditForm" %>

<!DOCTYPE html>
<html>
<head>
    <title>商机详情</title>
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

    <style type="text/css">
        #divNavigation ul li
        {
            float: left;
            width: 25%;
            height: 40px;
        }
    </style>

    <script type="text/javascript">
        fromSource = "Opptunity";
        globalInterval = 500;
        function pageSetRight() {
            var coID = $("#hfKeyID").val();
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '更多', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {

                    dd.device.notification.actionSheet({
                        title: "更多操作", //标题
                        cancelButton: '取消', //取消按钮文本
                        otherButtons: ["转为合同", "调整状态", "删除", "刷新"],
                        onSuccess: function (result) {
                            //onSuccess将在点击button之后回调
                            /*{
                            buttonIndex: 0 //被点击按钮的索引值，Number，从0开始, 取消按钮为-1
                            }*/
                            if (result.buttonIndex == 0) {
                                var opptunityID = $("#hfKeyID").val();
                                location.href = "CustomerBusinessAdd.aspx?OpptunityID=" + opptunityID;
                            }
                            else if (result.buttonIndex == 1) {
                                $("#ShowddlChangeOpptunityStatusID").show();
                            }
                            else if (result.buttonIndex == 2) {
                                dd.device.notification.confirm({
                                    message: "确定删除吗？",
                                    title: "提示",
                                    buttonLabels: ['确认', '取消'],
                                    onSuccess: function (result) {
                                        if (result.buttonIndex == 0) {
                                            $.ajax({
                                                type: "POST",
                                                contentType: "application/json",
                                                url: "../AjaxMethods.asmx/DeleteOpptunity",
                                                data: "{ID:" + coID + ",CurrentOperatorID:" + $("#hfCurrrentOperatorID").val() + "}",
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
                                                        ShowMessage("操作失败！请确认是否有权操作后重试");
                                                    }
                                                }
                                            });
                                        }
                                    },
                                    onFail: function (err) {
                                    }
                                })
                            }
                            else if (result.buttonIndex == 3) {
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
        })

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

        dd.error(function (error) {
            alert('dd error: ' + err.errorMessage);
        });


        function ChangeOpptunityStatusID() {
            var coID = $("#hfKeyID").val();
            var coStatusID = $("#ddlChangeOpptunityStatusID").val();
            var CurrentOperatorID = $("#hfCurrentOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ChangeOpptunityStatus",
                data: "{ID:" + coID + ",coStatusID:" + coStatusID + ",CurrentOperatorID:" + CurrentOperatorID + "}",
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        ShowSuccessMessage();
                       
                        window.location.href = "CustomerOpptunityEditForm.aspx?Action=View&ID=" + coID;
                    }
                    else {
                        ShowErrorMessage();
                        $("#ShowddlChangeOpptunityStatusID").hide();
                    }
                }
            })
        }


        function CloseThisDiv(id) {
            $("#" + id).hide();
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
                        <asp:label runat="server" cssclass="TitlecusName" id="lblOpptunityName"></asp:label>
                        <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                            <tr>
                                <td>
                                    商机阶段：
                                </td>
                                <td>
                                    <asp:label runat="server" id="lblPhase"></asp:label>
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
                        <asp:hiddenfield id="hfKeyID" runat="server" />
                        <asp:hiddenfield id="hfcusID" runat="server" />
                        <asp:hiddenfield id="hfPCWebDomain" runat="server" />
                        <div>
                            <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
                                <ul style="margin: 0px; padding: 0px;">
                                    <li><a onclick="javascript:ShowListCommon('divFollowHistoryList',this)" id="FollowHistory"  class="li_title">
                                        动态</a></li>
                                    <li runat="server" id="liOpptunity"><a onclick="javascript:ShowListCommon('divOpptunityList',this)"
                                        id="Opptunity">商机</a></li>
                                    
                                    <li><a onclick="javascript:ShowListCommon('divFollowPlanList',this)" id="Plan">提醒</a></li>
                                    <li><a onclick="javascript:ShowListCommon('divCoWorkerList',this)" id="CoWorker">协作人</a></li>
                                </ul>
                            </div>
                            <%--调整状态--%>
                            <div id="ShowddlChangeOpptunityStatusID" style="z-index: 1004; position: fixed; width: 100%; background-color: #aaa; height:100%; display:none;" >
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2" style="color: #fff;text-shadow: none;">
                                        调整状态为:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:dropdownlist id="ddlChangeOpptunityStatusID" data-role="none" runat="server"></asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                                <a href="javascript:ChangeOpptunityStatusID();" class="SavaBtn" style=" color:#fff; width:100%; float:left;">
                                                确定</a> 
                                        </div>
                                    </td>
                                    <td>
                                        <div class="OperatorButton" style="text-align: center;text-shadow: none;">
                                                <a class="SavaBtn" style=" color:#fff; width:100%; float:left;" onclick="javascript:CloseThisDiv('ShowddlChangeOpptunityStatusID');">
                                                    取消</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                             <%--商机基本信息--%>
                            <div id="divOpptunityList" class="cssMainPanel">
                                <ul data-role="listview" id="ulOpptunity" data-icon="false" style="margin: 0px;">
                                    <asp:repeater runat="server" id="repOpptunity">
                                        <ItemTemplate>
                                            <li id='liOpptunity<%#Eval("coID")%>' class="MainPanelli">
                                                <div class="new_div">
                                                    <table class=" new_table">
                                                        <tr>
                                                            <td style="width: 33.333%; text-align: left;">
                                                                <a href='javascript:OpptunityView(<%#Eval("coID")%>);'>
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
                                    </asp:repeater>
                                </ul>
                            </div>
                            <div id="divOpptunityEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            商机名称：
                                        </th>
                                        <td>
                                            <asp:textbox runat="server" placeholder="请输入商机名称" id="txtcoName"></asp:textbox>
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
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcoPhaseID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计金额：
                                        </th>
                                        <td>
                                            <asp:textbox runat="server" placeholder="请输入预计金额" id="txtcoPredictAmount" cssclass="inputNumber2"></asp:textbox>
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
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcoStatusID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            商机来源：
                                        </th>
                                        <td>
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcoOpptunitySourceID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:dropdownlist runat="server" data-role="none" id="ddlcoOperatorID">
                                            </asp:dropdownlist>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                                    <a href="javascript:SaveOpptunity();" class="SavaBtn" style="color: #fff; width: 100%;
                                        float: left;">确定</a>
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
                                            商机阶段：
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
                            <%--计划--%>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
