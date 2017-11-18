<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWorkReportEditForm.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerWorkReportEditForm" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script src="../scripts/BaseHelper.js"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>

    <style type="text/css">
        .SumReportData div
        {
            float:left;
            text-align:center;
            color: #828181;
            padding-right:5px;
        }

        #divMainInfoView
        {
            height:100% !important;
        }
        
        .cssMainPanel
        {
            height: initial !important;
        }
    </style>

    <script type="text/javascript">

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
        
        function SavaBillComment() {
            //bcSourceID（1为跟进，2为日志）
            var bcwdID = $("#hfwdID").val();
            var cfhOperatorID = $("#hfwdOperatorID").val();
            var bcSourceID = "2";
            var bcOperatorID = $("#hfCurrentOperatorID").val();
            var bcContent = $("#cfhComment").val();
            var CurrentOperatorName = $("#hfCurrentOperatorName").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SavaBillComment",
                data: "{bcOperatorID:'" + bcOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bcwdID + "',cfhOperatorID:'" + cfhOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result + 0 > 0) {
                        alert(result);
                        var today = new Date();
                        var m = today.getMonth() + 1;
                        var currentDateTimeStr = today.getFullYear() + "/" + m + "/" + today.getDate() + " " + today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                        var html = '';
                        html += "<li id='liBillCommenet" + result + "' style=' border-bottom:1px solid #ccc; width:96%;'>";
                        html += "<table style='width:100%;'><tr><td><label>" + CurrentOperatorName + "</label></td>";
                        html += "<td style='text-align:right;'><a style='cursor:pointer;' onclick='javascript:DeleteBillComment(" + result + ")'>删除</a> | <a style='cursor:pointer;'  onclick=\"javascript:ShowThisReplyDiv('DivForReply" + result + "','" + CurrentOperatorName + "'," + result + ")\">回复</a></td></tr>";
                        html += "<tr><td colspan='2'><label>" + currentDateTimeStr + "</label></td></tr>";
                        html += "<tr><td colspan='2'><label>" + bcContent + "</label></td></tr>";
                        html += "<tr><td colspan='2'><div style='line-height: 40px;text-align: right; display:none;' id='DivForReply" + result + "'>";
                        html += "<input type='text' id='txtReply" + result + "' style='width: 100%;outline: none;height: 24px;' />";
                        html += "<a class='MeunButton' onclick=\"javascript:ReplyContent('" + CurrentOperatorName + "'," + bcOperatorID + "," + result + ",'Reply')\">回复</a><a class='CancelBtn' onclick=\"javascript:CloseThisDiv('DivForReply" + result + "')\">取消</a>";
                        html += "</div></td></tr></table></li>";
                        $("#CommentDetailShow").append(html);
                        alert("评论成功");
                        $("#cfhComment").val("");
                        $("#divComment" + bcwdID).removeClass("cssNotLike");
                        $("#divComment" + bcwdID).addClass("cssLike");
                        $("#imgContent" + bcwdID).attr("src", "../@images/HasContent.png");
                        HideDiv();
                        $("#lblCommentCount").html("评论(" + $("#CommentDetailShow li").length + ")");
                    }
                }
            })
        }

        function ShowThisReplyDiv(id, bcOperator, bcID) {
            $("#" + id).show();
            var v = "@" + bcOperator + " ";
            $("#txtReply" + bcID).val(v);
        }

        function ReplyContent(Operator, replyOperatorID, id) {
            //bcSourceID（1为跟进，2为日志）
            var bcwdID = $("#hfwdID").val();
            var cfhOperatorID = $("#hfwdOperatorID").val();
            var bcSourceID = "2";
            var bcOperatorID = $("#hfCurrentOperatorID").val();
            var bcContent = $("#txtReply" + id).val();
            var CurrentOperatorName = $("#hfCurrentOperatorName").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ReplyBillComment",
                data: "{bcOperatorID:'" + bcOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bcwdID + "',replyOperatorID:'" + replyOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result + 0 > 0) {
                        var today = new Date();
                        var m = today.getMonth() + 1;
                        var currentDateTimeStr = today.getFullYear() + "/" + m + "/" + today.getDate() + " " + today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                        var html = '';
                        html += "<li id='liBillCommenet" + result + "' style=' border-bottom:1px solid #ccc; width:96%;'>";
                        html += "<table style='width:100%;'><tr><td><label>" + CurrentOperatorName + "</label></td>";
                        html += "<td style='text-align:right;'><a style='cursor:pointer;' onclick='javascript:DeleteBillComment(" + result + ")'>删除</a> | <a style='cursor:pointer;'  onclick=\"javascript:ShowThisReplyDiv('DivForReply" + result + "','" + CurrentOperatorName + "'," + result + ")\">回复</a></td></tr>";
                        html += "<tr><td colspan='2'><label>" + currentDateTimeStr + "</label></td></tr>";
                        html += "<tr><td colspan='2'><label>" + bcContent + "</label></td></tr>";
                        html += "<tr><td colspan='2'><div style='line-height: 40px;text-align: right; display:none;' id='DivForReply" + result + "'>";
                        html += "<input type='text' id='txtReply" + result + "' style='width: 100%;outline: none;height: 24px;' />";
                        html += "<a class='MeunButton' onclick=\"javascript:ReplyContent('" + CurrentOperatorName + "'," + CurrentOperatorID + "," + result + ",'Reply')\">回复</a><a class='CancelBtn' onclick=\"javascript:CloseThisDiv('DivForReply" + result + "')\">取消</a>";
                        html += "</div></td></tr></table></li>";
                        $("#CommentDetailShow").append(html);
                        alert("回复成功");
                        $("#divComment" + bcwdID).removeClass("cssNotLike");
                        $("#divComment" + bccfhID).addClass("cssLike");
                        $("#DivForReply" + id).hide();
                        $("#lblCommentCount").html("评论(" + $("#CommentDetailShow li").length + ")");
                    }
                }
            })
        }

        function handleClickLike(id) {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            var cfhID = id;
            var cfhOperatorID = $("#hfwdOperatorID").val();

            var ccdate = "{currentOperatorID:'" + currentOperatorID + "',cfhID:'" + cfhID + "',cfhOperatorID:'" + cfhOperatorID + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetClickLikeInfoForWorkReport",
                data: ccdate,
                dataType: 'json',
                success: function (result) {
                    var clicked = false;
                    $("#divLikePanel").empty();
                    if (result != "") {
                        var operators = result.split("|");
                        $("#lblLikeCount" + id).html("点赞(" + operators.length + ")");
                        var operatorName = $("#hfCurrentOperatorName").val();
                        for (var i = 0; i < operators.length; i++) {
                            if (operatorName == operators[i]) {
                                clicked = true;
                            }
                            $("#divLikePanel").append("<div class='cssLikePerson'>" + operators[i] + "</div>");
                        }
                        $("#divLikePanel").append("<div class='cssTxt'>觉得很赞哦</div>");
                    }
                    else {
                        $("#lblLikeCount" + id).html("点赞(0)");
                    }

                    if (clicked) {
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
    </script>

</head>
<body style="background: #eee;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfwdID" runat="server" />
    <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
    <asp:HiddenField ID="hfwdOperatorID" runat="server" />
    <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
    <asp:HiddenField runat="server" ID="hfHasLike" />
    <asp:HiddenField runat="server" ID="hfHasComment" />

    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto; height:100% !important;">
                    <div id="Header" data-role="header" data-theme="b" style="height: 65px; background-color: White;
                        width: 100%;border-bottom: 1px solid #eee;border-top: 1px solid #eee;" class="cssHeader">
                        <table id="CustomerTable" class="TitleTable" style=" padding-left:18px;">
                            <tr>
                                <td colspan="2"><label id="lblopeName" class="TitlecusName" runat="server"></label></td>
                            </tr>
                            <tr>
                                <td>
                                    通知人：
                                </td>
                                <td>
                                    <label id="lblwdNotifierID" runat="server">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="div1" style="margin: 0px auto; line-height: 2em; background-color:#fff; margin-top:8px;border-top: 1px solid #eee;">
                        <table class="TitleTable" cellpadding="0" cellspacing="0" style=" width:100%; padding:10px;"> 
                            <tr>
                                <td>
                                    <div class="SumReportData">
                                       <div>客户(<label runat="server" id="lblNewCustomer">0</label>);</div>
                                       <div>跟进记录(<label runat="server" id="lblNewFollow">0</label>);</div>
                                       <div>商机(<label runat="server" id="lblNewOpptunity">0</label>);</div>
                                       <div>商机金额(<label runat="server" id="lblNewOpptunityAmount">0</label>);</div>
                                       <div>合同(<label runat="server" id="lblNewBusiness">0</label>);</div>
                                       <div>合同金额(<label runat="server" id="lblNewBusinessAmount">0</label>);</div>
                                       <div>回款金额(<label runat="server" id="lblNewPayment">0</label>);</div>
                                       <div>线索(<label runat="server" id="lblNewClue">0</label>);</div>
                                    </div>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <div id="divBusinessView" style="margin: 0px auto; line-height: 2em; background-color:#fff;border-bottom: 1px solid #eee;border-top: 1px solid #eee;">
                        <table class="TitleTable" cellpadding="0" cellspacing="0" style=" padding: 10px 0px 10px 12px;"> 
                            <tr>
                                <th style="color: #989191;font-weight: normal; text-align:left;">
                                过去的总结
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblwdTitle" runat="server">
                                    </label>
                                </td>
                            </tr>
                           
                        </table>
                    </div>
                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;">
                        <table class="TitleTable" cellpadding="0" cellspacing="0" style=" padding: 10px 0px 10px 12px;">
                             <tr>
                                <th style="color: #989191;font-weight: normal; text-align:left;">
                                未来的计划
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <label runat="server" id="lblwdContent">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;">
                        <table class="TitleTable" cellpadding="0" cellspacing="0" style=" padding: 10px 0px 10px 12px;">
                             <tr>
                                <th style="color: #989191;font-weight: normal;">
                                    图片
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <label runat="server" id="lblwdFile">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;border-top: 1px solid #eee; margin-top:8px;">
                        <table class="TitleTable" cellpadding="0" cellspacing="0" style=" padding-left:12px;">
                            <tr>
                                <th style="color: #989191;font-weight: normal;">
                                    报告提交时间：
                                </th>
                                <th style="color: #989191;font-weight: normal;">
                                    <label id="lblwdAddDateDetails" runat="server">
                                    </label>
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div  class="cssMainPanel" style="width:100%;">
                        <table>
                            <tr>
                                <td style="background-color: #F5F9FF;">
                                    <label id="cfhLikeAndComment" runat="server">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td id="divLikePanel">
                                    <label id="litLikePersons" runat="server">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="border: 1px solid #ccc;">
                                        <div style="text-align: center; padding-top: 10px;">
                                            <input type="text" id="cfhComment" data-role="none" onclick="javascript:ShowBtn()" style="width: 98%;
                                                outline: none; height: 24px;" />
                                        </div>
                                        <div style="height: 40px; line-height: 40px; text-align: right; display: none;" id="DivForBtn">
                                            <a class="MeunButton" onclick="javascript:SavaBillComment();">评论</a> <a class="CancelBtn"
                                                onclick="javascript:HideDiv();">取消</a>
                                        </div>
                                        <div style="border: 1px dotted #ccc; width: 98%; margin: 0 auto;">
                                        </div>
                                        <div>
                                            <ul id="CommentDetailShow" style="list-style: none;">
                                                <asp:Repeater ID="repBillComment" runat="server">
                                                    <ItemTemplate>
                                                        <li id="liBillCommenet<%#Eval("bcID") %>" style="border-bottom: 1px solid #ccc; width: 96%;">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <%#Eval("bcOperator") %></label>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <a style="cursor: pointer;" onclick="javascript:DeleteBillComment(<%#Eval("bcID") %>)">
                                                                            删除</a> | <a style="cursor: pointer;" onclick="javascript:ShowThisReplyDiv('DivForReply<%#Eval("bcID") %>','<%#Eval("bcOperator") %>',<%#Eval("bcID") %>)">
                                                                                回复</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <%#Eval("bcDate") %></label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <%#Eval("bcContent") %></label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="line-height: 40px; text-align: right; display: none;" id="DivForReply<%#Eval("bcID") %>">
                                                                            <input type="text" id="txtReply<%#Eval("bcID") %>" data-role="none" style="width: 100%; outline: none;
                                                                                height: 24px;" />
                                                                            <a class="MeunButton" onclick="javascript:ReplyContent('<%#Eval("bcOperator") %>',<%#Eval("bcOperatorID") %>,<%#Eval("bcID") %>,'Reply')">
                                                                                回复</a> <a class="CancelBtn" onclick="javascript:CloseThisDiv('DivForReply<%#Eval("bcID") %>')">
                                                                                    取消</a>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>



