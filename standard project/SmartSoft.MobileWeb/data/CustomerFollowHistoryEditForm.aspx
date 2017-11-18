<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowHistoryEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.data.CustomerFollowHistoryEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动态详情</title>
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
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/BaseHelper.js"></script>

    <style>
        .cssLikePerson
        {
            padding: 10px 10px 10px 10px; 
            background-color: #3C96DE; 
            border-radius: 25px;
            color: #fff; 
            text-shadow: none; 
            font-weight: normal;
        }
        
        .cssCommentLi
        {
             margin:0px; 
             padding:0px; 
             margin-bottom:20px;
        }
        .cssCommentDiv
        {
             overflow: auto;margin: 0px 4px 7px 4px;border: 1px solid #ccc;border-radius: 7px;margin-top: 0px;
        }
    </style>

    <script type="text/javascript">

        function ShowListCommon(id, ctr) {
            $(".divPanel").hide();
            $("#" + id).show();
            if (ctr != null) {
                $("div", $("#tddivCssChange")).removeClass("li_title");
                $(ctr).addClass("li_title");
            }
        }

        function ShowContentDiv() {
            //bcSourceID（1为跟进，2为日志）
            var bcSourceID = "1";
            var bcOperatorID = $("#hfCurrentOperatorID").val();
            var bccfhID = $("#hfcfhID").val();
            var cfhOperatorID = $("#hfcfhOperatorID").val();
            dd.ui.input.plain({
                placeholder: '说点什么吧', //占位符
                text: '', //默认填充文本
                onSuccess: function (data) {
                    //onSuccess将在点击发送之后调用
                    /*{
                    text: String
                    }*/
                    if (data.text != null && data.text != "undefined") {

                        var bcContent = data.text;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "../AjaxMethods.asmx/SavaBillComment",
                            data: "{bcOperatorID:'" + bcOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bccfhID + "',cfhOperatorID:'" + cfhOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
                            dataType: 'json',
                            success: function (result) {
                                if (result.d + 0 > 0) {
                                    var today = new Date();
                                    var currentDateTimeStr = today.getFullYear() + "-" + today.getMonth() + "-" + today.getDay() + "-" + today.getHours() + "-" + today.getMinutes() + "-" + today.getSeconds();
                                    var html = '<li style="margin:0px; padding:0px; margin-bottom:20px;">';
                                    html += '<div class="cssCommentDiv" >';
                                    html += '<div>';
                                    html += '<table>';
                                    html += '<tr>';
                                    html += '<td style="width: 50%;">';
                                    html += '<label>';
                                    html += $("#hfCurrentOperatorName").val() + '</label>';
                                    html += '</td>';
                                    html += '<td style="width: 50%; text-align: right;">';
                                    html += '<label>';
                                    html += new Date().Format("yyyy-MM-dd hh:mm:ss") + '</label>';
                                    html += '</td>';
                                    html += '</tr>';
                                    html += '<tr>';
                                    html += '<td colspan="2">';
                                    html += '<label>';
                                    html += bcContent + '</label>';
                                    html += '</td>';
                                    html += '</tr>';
                                    html += '<tr><td colspan="2" style="text-align: right;"><label style="color:#3C96DE">回复</label></td></tr></table></div></div></li>';
                                    $("#ulFollowHistory").append(html);

                                    $("#divComment").removeClass("cssNotLike");
                                    $("#divComment").addClass("cssLike");

                                    $("#lblCommentCount").html("评论(" + $("#ulFollowHistory li").length + ")");

                                    dd.device.notification.toast({
                                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                        text: "评论成功", //提示信息
                                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                        delay: 0, //延迟显示，单位秒，默认0
                                        onSuccess: function (result) {

                                        },
                                        onFail: function (err) { }
                                    })
                                }
                            }
                        })
                    }
                },
                onFail: function () {

                }
            })
        }

        function SendContent() {
            $("#divSendContent").hide();
        }

        function pageSetRight() {
            if ($("#hfPermissionCode").val() == "1") {
                dd.biz.navigation.setRight({
                    show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                    control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                    text: '删除', //控制显示文本，空字符串表示显示默认文本
                    onSuccess: function (result) {
                        var cfhID = $("#hfcfhID").val();
                        var cfhRelatedID = $("#hfRelatedID").val();
                        var cfhFromSource = $("#hfFromSource").val();
                        dd.device.notification.confirm({
                            message: "确定删除吗？",
                            title: "提示",
                            buttonLabels: ['确认', '取消'],
                            onSuccess: function (result) {
                                if (result.buttonIndex == 0) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json",
                                        url: "../AjaxMethods.asmx/DeleteFollowHistory",
                                        data: "{ID:" + cfhID + ",CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                                        dataType: 'json',
                                        success: function (result) {
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
            else {
                dd.biz.navigation.setRight({
                    show: false, //控制按钮显示， true 显示， false 隐藏， 默认true
                    control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                    text: '帮助', //控制显示文本，空字符串表示显示默认文本
                    onSuccess: function (result) {
                        window.location = "../help/help.htm";
                    },
                    onFail: function (err) {
                        alert(JSON.stringify(err));
                    }
                });
            }
        }

        function handleClickLike(id) {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            var cfhID = id;
            var cfhOperatorID = $("#hfcfhOperatorID").val();

            var ccdate = "{currentOperatorID:'" + currentOperatorID + "',cfhID:'" + cfhID + "',cfhOperatorID:'" + cfhOperatorID + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetClickLikeInfoForHistory",
                data: ccdate,
                dataType: 'json',
                success: function (result) {
                    var clicked = false;
                    $("#divLikePanel").empty();
                    if (result.d != "") {
                        var operators = result.d.split("|");
                        $("#lblLikeCount").html("点赞(" + operators.length + ")");
                        var operatorName = $("#hfCurrentOperatorName").val();
                        for (var i = 0; i < operators.length; i++) {
                            if (operatorName == operators[i]) {
                                clicked = true;
                            }
                            $("#divLikePanel").append("<lable class='cssLikePerson'>" + operators[i] + "</label>");
                        }
                    }
                    else {
                        $("#lblLikeCount").html("点赞(0)");
                    }

                    if (clicked) {
                        $("#divLike").removeClass("cssNotLike");
                        $("#divLike").addClass("cssLike");
                        $("#imgLike").attr("src", "../@images/likeClick.png");
                    }
                    else {
                        $("#divLike").removeClass("cssLike");
                        $("#divLike").addClass("cssNotLike");
                        $("#imgLike").attr("src", "../@images/like.png");
                    }
                }
            })
        }

        $(function () {
            if ($("#hfHasLike").val() + 0 >= 1) {
                $("#divLike").removeClass("cssNotLike");
                $("#divLike").addClass("cssLike");
                $("#imgLike").attr("src", "../@images/likeClick.png");
            }
            else {
                $("#divLike").removeClass("cssLike");
                $("#divLike").addClass("cssNotLike");
                $("#imgLike").attr("src", "../@images/like.png");
            }

            if ($("#hfHasComment").val() + 0 >= 1) {
                $("#divComment").removeClass("cssNotLike");
                $("#divComment").addClass("cssLike");
                $("#imgComment").attr("src", "../@images/HasContent.png");
            }
            else {
                $("#divComment").removeClass("cssLike");
                $("#divComment").addClass("cssNotLike");
                $("#imgComment").attr("src", "../@images/Content.png");
            }
        });

        function ReplyContent(Operator, replyOperatorID) {
            //bcSourceID（1为跟进，2为日志）
            var bcSourceID = "1";
            var CurrentOperatorID = $("#hfCurrentOperatorID").val();
            var bccfhID = $("#hfcfhID").val();
            dd.ui.input.plain({
                placeholder: '说点什么吧', //占位符
                text: '@' + Operator + ' ', //默认填充文本
                onSuccess: function (data) {
                    //onSuccess将在点击发送之后调用
                    /*{
                    text: String
                    }*/
                    if (data.text != null && data.text != "undefined") {

                        var bcContent = data.text;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "../AjaxMethods.asmx/ReplyBillComment",
                            data: "{bcOperatorID:'" + CurrentOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bccfhID + "',replyOperatorID:'" + replyOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
                            dataType: 'json',
                            success: function (result) {
                                if (result.d + 0 > 0) {
                                    var today = new Date();
                                    var currentDateTimeStr = today.getFullYear() + "-" + today.getMonth() + "-" + today.get
                                    var html = '<li style="margin:0px; padding:0px; margin-bottom:20px;">';
                                    html += '<div class="cssCommentDiv" >';
                                    html += '<div>';
                                    html += '<table>';
                                    html += '<tr>';
                                    html += '<td style="width: 50%;">';
                                    html += '<label>';
                                    html += $("#hfCurrentOperatorName").val() + '</label>';
                                    html += '</td>';
                                    html += '<td style="width: 50%; text-align: right;">';
                                    html += '<label>';
                                    html += new Date().Format("yyyy-MM-dd hh:mm:ss") + '</label>';
                                    html += '</td>';
                                    html += '</tr>';
                                    html += '<tr>';
                                    html += '<td colspan="2">';
                                    html += '<label>';
                                    html += bcContent + '</label>';
                                    html += '</td>';
                                    html += '</tr>';
                                    html += '<tr><td colspan="2" style="text-align: right;"><label onclick="javascript:ReplyContent(' + $("#hfCurrentOperatorName").val() + ',' + CurrentOperatorID + ')"  style="color:#3C96DE">回复</label></td></tr></table></div></div></li>';
                                    $("#ulFollowHistory").append(html);

                                    $("#divComment").removeClass("cssNotLike");
                                    $("#divComment").addClass("cssLike");

                                    $("#lblCommentCount").html("评论(" + $("#ulFollowHistory li").length + ")");

                                    dd.device.notification.toast({
                                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                        text: "回复成功", //提示信息
                                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                        delay: 0, //延迟显示，单位秒，默认0
                                        onSuccess: function (result) {

                                        },
                                        onFail: function (err) { }
                                    })
                                }
                            }
                        })
                    }
                },
                onFail: function () {

                }
            })
        }
    </script>
</head>
<body style="background: #eee;">
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto;">
                    <div id="divFollowHistoryList" class="cssMainPanel">
                        <table cellpadding="0" cellspacing="0" style="background-color: #F8F8F8;">
                            <tr>
                                <td>
                                    <ul data-role="listview" id="ulLinkMan" data-icon="false" style="margin: 0px;">
                                        <li style="margin: 0px; padding: 0px; margin-bottom: 20px;">
                                            <div>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td style="wdith: 50%;">
                                                                <label runat="server" id="lblcfhOperator" style="color: #3C96DE; width: 25%">
                                                                </label>
                                                                <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />
                                                                <asp:HiddenField runat="server" ID="hfCurrentOperatorName" />
                                                                <asp:HiddenField runat="server" ID="hfHasLike" />
                                                                <asp:HiddenField runat="server" ID="hfHasComment" />
                                                                <asp:HiddenField runat="server" ID="hfPermissionCode" />
                                                                <asp:HiddenField runat="server" ID="hfcfhID" />
                                                                <asp:HiddenField runat="server" ID="hfFromSource" />
                                                                <asp:HiddenField runat="server" ID="hfRelatedID" />
                                                                <asp:HiddenField runat="server" ID="hfcfhOperatorID" />
                                                                <label runat="server" id="lblcfhType" style="color: #3C96DE; width: 25%; border: 1px solid #3C96DE;
                                                                    margin-left: 10px; border-radius: 3px; padding: 3px 3px 3px 3px; font-size: 10px;">
                                                                </label>
                                                            </td>
                                                            <td style="width: 50%; text-align: right;">
                                                                <label runat="server" id="lblcfhDate">
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <label runat="server" id="lblcfhContent">
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Literal runat="server" ID="GetFile"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <label id="lblcfhAddress" runat="server">
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td colspan="2">
                                                                创建时间：<label id="lblcfhAddDate" runat="server"></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <label runat="server" id="lblcfhFromName"></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" id="tddivCssChange">
                                                                <div onclick="javascript:ShowListCommon('divContentPanel',this)" class="li_title"
                                                                    style="float: left;">
                                                                    <label id="lblCommentCount">
                                                                        评论（<%=GetCommentCount() %>）</label>
                                                                    </div>
                                                                <div onclick="javascript:ShowListCommon('divLikePanel',this)" style="float: left;">
                                                                    <label id="lblLikeCount">
                                                                        点赞（<%=GetLikeCount() %>）</label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div id="divContentPanel" class="divPanel" style=" background-color:#eee;">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <ul data-role="listview" id="ulFollowHistory" data-icon="false" style="margin: 0px;">
                                                                                    <asp:repeater runat="server" id="repComment">
                                                                                        <ItemTemplate>
                                                                                            <li style=" margin:0px; padding:0px; margin-bottom:20px;">
                                                                                                <div class="cssCommentDiv" >
                                                                                                    <div>
                                                                                                        <table> 
                                                                                                            <tr>
                                                                                                                <td style="width: 50%;">
                                                                                                                    <label>
                                                                                                                        <%#Eval("bcOperator") %></label>
                                                                                                                </td>
                                                                                                                <td style="width: 50%; text-align: right;">
                                                                                                                    <label>
                                                                                                                        <%#DateTime.Parse(Eval("bcDate")+"").ToString("yyyy-MM-dd HH:mm:ss") %></label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="2">
                                                                                                                    <label>
                                                                                                                        <%#Eval("bcContent") %></label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="2" style="text-align: right;">
                                                                                                                    <label onclick="javascript:ReplyContent('<%#Eval("bcOperator") %>',<%#Eval("bcOperatorID") %>)" style="color:#3C96DE">
                                                                                                                        回复</label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </div>  
                                                                                            </li>
                                                                                        </ItemTemplate>
                                                                                    </asp:repeater>
                                                                                </ul>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div id="divLikePanel" class="divPanel" style="display: none; margin-top: 10px; style=" background-color:#eee;">
                                                                    <asp:Literal runat="server" ID="litLikePersons"></asp:Literal>
                                                                    
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div data-role="footer" data-position="fixed" data-theme="b" data-tap-toggle="false"
                        style="background: #fff; text-shadow: none; border-color: #fff; color: #333;
                        font-weight: normal; line-height: 3em; font-size: 14px; width: 100%; font-family: '微软雅黑';">
                        <div style="width: 100%; float: left; border-bottom: 1px solid #ddd;">
                            <div onclick="javascript:ShowContentDiv()" style="float: left; width: 50%; text-align: center;" id="divComment" class="cssLike">
                                <img src="../@images/Content.png" style="width: 18px;" id="imgComment"/>
                                <label>
                                    评论</label>
                            </div>
                            <div style="float: left; width: 50%; text-align: center;" onclick="javascript:handleClickLike(<%=rowID%>);" id="divLike" class="cssLike" >
                                <img src="../@images/like.png" style="width: 18px;" id="imgLike"/>
                                <label>
                                    点赞</label>
                            </div>
                        </div>
                        <div id="divSendContent" style="width: 100%; display: none;">
                            <div style="float: left; width: 80%; height: 35px;">
                                <input type="text" id="txtbcContent" placeholder="请进行评论" style="width: 100%; height: 100%;
                                    border: none; outline: none;" data-role="none" />
                            </div>
                            <div onclick="javascript:SendContent()" style="float: left; background-color: #3C96DE;
                                height: 35px; width: 20%; text-align: center; color: #fff; line-height: 35px;
                                font-size: 14px;">
                                <a data-role="none">发送</a>
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
