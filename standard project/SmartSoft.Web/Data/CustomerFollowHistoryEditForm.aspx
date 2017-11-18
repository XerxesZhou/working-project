<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowHistoryEditForm.aspx.cs"
    Inherits="SmartSoft.Web.Data.CustomerFollowHistoryEditForm" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=title %>
    </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/AjaxMainEdit.js"></script>
    <style type="text/css">
        .lblName
        {
            font-size: 14px;
            color: #666;
        }
        
        .lblType
        {
            padding: 5px;
            color: #0174CF;
            border: 1px solid;
            border-radius: 5px;
        }
        
        .cssLike
        {
            color: #3C96DE;
        }
        .cssNotLike
        {
            color: Gray;
        }
        
        .cssLikePerson
        {
            width: 60px;
            height: 30px;
            line-height: 30px;
            background-color: #0174CF;
            color: #fff;
            text-align: center;
            border-radius: 15px;
            margin: 5px;
            float: left;
        }
        
        .cssTxt
        {
            height: 30px;
            line-height: 30px;
            float: left;
            margin: 5px;
        }
        
        .MeunButton
        {
            text-decoration: none;
            color: #fff;
            background-color: #0174CF;
            padding: 5px 10px 5px 10px;
            border: 1px solid #0174CF;
            border-radius: 5px;
            margin-left:10px;
            cursor:pointer;
        }
        
         .CancelBtn
        {
            border: 1px solid #ccc;
            padding: 5px 10px 5px 10px;
            border-radius: 5px;
            margin-right: 5px;
        }
        
        ::-webkit-scrollbar
        {
            width: 3px;
        }
        ::-webkit-scrollbar-track
        {
            background-color: #ddd;
        }
        ::-webkit-scrollbar-thumb
        {
            background-color: #aaa;
        }
    </style>

    <script type="text/javascript">

        function ShowBtn() {
            $("#cfhComment").height(48);
            $("#DivForBtn").show();
        }

        function HideDiv() {
            $("#DivForBtn").fadeOut();
            $("#cfhComment").height(24);
        }

        function DeleteBillComment(id) {
            if (window.confirm("确定删除该回复?")) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/DeleteBillComment",
                    data: "{id:'" + id + "'}",
                    dataType: 'json',
                    success: function (result) {
                        if (result + 0 > 0) {
                            $("#liBillCommenet" + id).remove();
                        }
                    }
                })
            }
        }

        function ShowThisReplyDiv(id, bcOperator, bcID) {
            $("#" + id).show();
            var v = "@" + bcOperator + " ";
            $("#txtReply" + bcID).val(v);
        }

        function CloseThisDiv(id) {
            $("#" + id).hide();
        }

        function ReplyContent(Operator, replyOperatorID, id) {
            //bcSourceID（1为跟进，2为日志）
            var bcSourceID = "1";
            var CurrentOperatorID = $("#hfCurrentOperatorID").val();
            var bccfhID = $("#hfCfhID").val();
            var bcContent = $("#txtReply" + id).val();
            var CurrentOperatorName = $("#hfCurrentOperatorName").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ReplyBillComment",
                data: "{bcOperatorID:'" + CurrentOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bccfhID + "',replyOperatorID:'" + replyOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
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
                        $("#divComment" + bccfhID).removeClass("cssNotLike");
                        $("#divComment" + bccfhID).addClass("cssLike");
                        $("#DivForReply" + id).hide();
                        $("#lblCommentCount").html("评论(" + $("#CommentDetailShow li").length + ")");
                    }
                }
            })
        }

        function SavaBillComment() {
            //bcSourceID（1为跟进，2为日志）
            var bcSourceID = "1";
            var bcOperatorID = $("#hfCurrentOperatorID").val();
            var bccfhID = $("#hfCfhID").val();
            var cfhOperatorID = $("#hfcfhOperatorID").val();
            var bcContent = $("#cfhComment").val();
            var CurrentOperatorName = $("#hfCurrentOperatorName").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SavaBillComment",
                data: "{bcOperatorID:'" + bcOperatorID + "',bcContent:'" + bcContent + "',bcRelatedID:'" + bccfhID + "',cfhOperatorID:'" + cfhOperatorID + "',bcSourceID:'" + bcSourceID + "'}",
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
                        html += "<a class='MeunButton' onclick=\"javascript:ReplyContent('" + CurrentOperatorName + "'," + bcOperatorID + "," + result + ",'Reply')\">回复</a><a class='CancelBtn' onclick=\"javascript:CloseThisDiv('DivForReply" + result + "')\">取消</a>";
                        html += "</div></td></tr></table></li>";
                        $("#CommentDetailShow").append(html);
                        alert("评论成功");
                        $("#cfhComment").val("");
                        $("#divComment" + bccfhID).removeClass("cssNotLike");
                        $("#divComment" + bccfhID).addClass("cssLike");
                        $("#imgContent" + bccfhID).attr("src", "../@images/HasContent.png");
                        HideDiv();
                        $("#lblCommentCount").html("评论(" + $("#CommentDetailShow li").length + ")");
                    }
                }
            })
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
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; float: left; height: 100%; overflow: auto;">
        <asp:HiddenField ID="hfcfhOperatorID" runat="server" />
        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
        <asp:HiddenField ID="hfCfhID" runat="server" />
        <table style="width: 100%; margin: 0 auto; padding: 10px;" id="TableComment">
            <tr>
                <td>
                    <label class="lblName" id="cfhOperatorName" runat="server">
                    </label>
                    <label class="lblType" id="cfhTypeName" runat="server">
                    </label>
                </td>
                <td style="float: right;">
                    <a onclick="javascript:EditFollowHistory(this)" href="#" class="MeunButton">修改</a><a
                        onclick="javascript:DeleteFollowHistory(this)" href="#" class="MeunButton">删除</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label id="cfhAddDate" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label id="cfhContent" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label id="cfhFilePath" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label id="cfhAddress" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label runat="server" id="lblcfhFromName"></label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #F5F9FF;">
                    <label id="cfhLikeAndComment" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2" id="divLikePanel">
                    <label id="litLikePersons" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="border: 1px solid #ccc;">
                        <div style="text-align: center; padding-top: 10px;">
                            <input type="text" id="cfhComment" onclick="javascript:ShowBtn()" style="width: 98%;
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
                                                            <input type="text" id="txtReply<%#Eval("bcID") %>" style="width: 100%; outline: none;
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
    </form>
</body>
</html>
