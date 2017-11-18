<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowPlanEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.data.CustomerFollowPlanEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提醒详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.8.3.min.js"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js"></script>
    <style>
        .cssHeader
        {
            text-align: center;
            color: #333333;
        }
        .cssNav
        {
            /*width: 29%; */
            float: left;
        }
        .cssNav a
        {
            float: left;
        }
        .cssMainPanel
        {
            font-size: small;
            margin-left: 3px;
            margin-right: 3px;
            margin-top: 0px; /*width: 65%; 
            
             float: right;*/
        }
        .cssMainPanel label
        {
            font-weight: bold;
        }
        .cssMainPanel div label
        {
            font-weight: normal;
        }
        .cssMainPanel div select
        {
            font-weight: normal;
        }
        .cssMainPanel div input
        {
            font-weight: normal;
        }
        .cssMainPanel a
        {
            margin-top: 0px;
            margin-bottom: 20px;
        }
        .cssMainPanel li
        {
            margin: 8px;
        }
        .cssMainPanel li a
        {
            margin-bottom: 0px;
        }
    </style>
    <style>
        .cssNav
        {
            width: 100%;
        }
        
        .cssNav a
        {
            width: 25%;
            font-size: 14px !important;
            box-sizing: border-box;
        }
        
        .cssMainPanel table
        {
            font-size: 14px;
            line-height: 2em;
            width: 100%;
            font-family: @微软雅黑;
            border: 0;
        }
        .cssMainPanel table tr th
        {
            text-align: right;
            width: 35%;
            color: Gray;
            border-bottom: 1px solid #CCC;
        }
        
        
        
        .OperatorButton div
        {
            width: 100%;
        }
        
        .OperatorButton div a
        {
            box-sizing: border-box;
            width: 50%;
        }
        
        textarea
        {
            border: 0;
        }
        
        .Address
        {
            display: none !important;
        }
        
        select
        {
            width: 100%;
            height: 35px;
            border: 0;
            outline: none;
            background: url("../images/combo_arrow.png") no-repeat 99%;
            font-size: 14px;
        }
        
        #divNavigation
        {
            width: 100%;
            height: 33px;
            overflow: hidden;
            position: relative;
        }
        
        #divNavigation ul
        {
            position: absolute;
            height: 33px;
        }
        
        #divNavigation ul li
        {
            float: left;
            width: 25%;
            height: 33px;
        }
        
        #divNavigation ul a
        {
            width: 100%;
        }
        
        #divNavigation .preNext
        {
            width: 33px;
            height: 100px;
            position: absolute;
            top: -33px;
            background: url(../images/sprite.png) no-repeat 0 0;
            cursor: pointer;
        }
        #divNavigation .pre
        {
            left: 0;
        }
        #divNavigation .next
        {
            right: 0;
            background-position: right top;
        }
        
        #lblopeName
        {
            font-family: "微软雅黑";
            font-weight: normal;
            font-size: 16px;
            float: left;
            padding: 5px 0px 5px 12px;
            text-shadow: none;
            width: 100%;
            text-align: left;
        }
        
        .CustomerTable th
        {
            text-align: left;
            font-size: 12px;
            color: #989191;
            text-shadow: none;
            font-weight: 300;
        }
        .cssLikePerson
        {
            padding: 10px 10px 10px 10px;
            background-color: #3C96DE;
            border-radius: 25px;
            color: #fff;
            text-shadow: none;
            font-weight: normal;
        }
        .cssLike
        {
            color: Green;
        }
        .cssNotLike
        {
            color: Gray;
        }
        .li_title
        {
            border-bottom: 1px solid #3C96DE;
        }
        .picView
        {
            text-align: center;
            max-width: 30%;
            margin-right: 10px;
        }
        .cssCommentLi
        {
            margin: 0px;
            padding: 0px;
            margin-bottom: 20px;
        }
        .cssCommentDiv
        {
            overflow: auto;
            margin: 0px 4px 7px 4px;
            border: 1px solid #ccc;
            border-radius: 7px;
            margin-top: 0px;
        }
    </style>
    <script type="text/javascript">
        function pageSetRight() {
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '删除', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    var cfpID = $("#hfcfpID").val();
                    var cfpRelatedID = $("#hfcfpRelatedID").val();
                    var cfpSource = $("#hfcfpSource").val();
                    dd.device.notification.confirm({
                        message: "确定删除吗？",
                        title: "提示",
                        buttonLabels: ['确认', '取消'],
                        onSuccess: function (result) {
                            if (result.buttonIndex == 0) {
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json",
                                    url: "../AjaxMethods.asmx/DeleteFollowPlan",
                                    data: "{ID:" + cfpID + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                                    dataType: 'json',
                                    success: function (result) {
                                        if (result.d + 0 > 0) {
                                            dd.device.notification.toast({
                                                icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                                text: "操作成功", //提示信息
                                                duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                                delay: 0, //延迟显示，单位秒，默认0
                                                onSuccess: function (result) {
                                                    history.back();
                                                },
                                                onFail: function (err) { }
                                            })
                                        }
                                        else {
                                            ShowErrorMessage();
                                        }
                                    }
                                });
                            }
                        },
                        onFail: function (err) {
                        }
                    })

                },
                onFail: function (err) {
                    alert(JSON.stringify(err));
                }
            });
        }

        function SetMessageRead() {
            var messageID = $("#hfMessageID").val();
            var cfpID = $("#hfcfpID").val();
            var CurrentOperatorID = $("#hfCurrentOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SetFollowPlanMessageRead",
                data: "{cfpID:" + cfpID + ",messageID:'" + messageID + "',CurrentOperatorID:'" + CurrentOperatorID + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        dd.device.notification.toast({
                            icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                            text: "操作成功", //提示信息
                            duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                            delay: 0, //延迟显示，单位秒，默认0
                            onSuccess: function (result) {
                                $("#divSetMessage").hide();
                                $("#divWriteFollow").show();
                            },
                            onFail: function (err) { }
                        })
                    }
                }
            })
        }

        function GoAndFollow() {
            var cfpRelatedID = $("#hfcfpRelatedID").val();
            var cfpSource = $("#hfcfpSource").val();
            if (cfpSource == "Clue") {
                window.location.href = "CustomerClueEditForm.aspx?Action=View&ID=" + cfpRelatedID;
            }
            else if (cfpSource == "Customer") {
                window.location.href = "CustomerEditForm.aspx?Action=View&ID=" + cfpRelatedID;
            }
            else if (cfpSource == "Opptunity") {
                window.location.href = "CustomerOpptunityEditForm.aspx?Action=View&ID=" + cfpRelatedID;
            }
            else if (cfpSource == "Business") {
                window.location.href = "CustomerBusinessEditForm.aspx?Action=View&ID=" + cfpRelatedID;
            }
        }
    </script>
</head>
<body style="background: #eee;">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hfcfpRelatedID"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hfcfpSource"></asp:HiddenField>
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto;">
                    <div id="divWarnView" class="cssMainPanel">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: center; border-bottom: 1px solid #ddd;">
                                    <label runat="server" id="lblcfpDate" style="height: 60px; line-height: 60px; text-align: center;
                                        border: 1px solid #3C96DE; padding: 10px 30px 10px 30px; border-radius: 20px;
                                        background-color: #3C96DE; color: #fff; text-shadow: none;">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hfcfpID" Value="0" />
                                    <label style="height: 50px; line-height: 50px;" runat="server" id="lblcfpContent">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Literal runat="server" ID="lblcfpFilePath"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="height: 50px; line-height: 50px;" runat="server" id="lblcfpFromName">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div data-role="footer" data-position="fixed" data-theme="b" data-tap-toggle="false"
                        style="background: #fff; text-shadow: none; border-color: #fff; color: #333;
                        font-weight: normal; line-height: 3em; font-size: 14px; width: 100%; font-family: '微软雅黑';">
                        <div style="width: 100%; float: left; border-bottom: 1px solid #ddd;">
                            <div runat="server" onclick="javascript:SetMessageRead()" style="display:none; float: left; width: 100%;
                                text-align: center;" id="divSetMessage" class="cssLike">
                                <img src="../@images/Content.png" style="width: 18px;" />
                                <label>
                                    设置为已完成
                                </label>
                            </div>
                            <div runat="server" onclick="javascript:GoAndFollow()" style="display:none; float: left; width: 100%;
                                text-align: center;" id="divWriteFollow" class="cssLike">
                                <img src="../@images/Content.png" style="width: 18px;" />
                                <label>
                                    写跟进
                                </label>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hfMessageID"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hfCurrentOperatorID"></asp:HiddenField>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
