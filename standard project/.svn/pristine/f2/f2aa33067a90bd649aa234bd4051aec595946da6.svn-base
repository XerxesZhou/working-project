<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="SmartSoft.MobileWeb.DashBoard" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>工作台</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link type="text/css" rel="stylesheet" href="css/jquery.mobile-1.3.2.min.css">
    <script type="text/javascript" src="scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.mobile-1.3.2.min.js"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="scripts/BaseHelper.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            sessionStorage.clear();
            localStorage.clear();
            if ($("#hfCurrentOperatorID").val() == "") {
                dd.config({
                    agentId: '<%=RtHashtable()["agentId"] %>', // 必填，微应用ID
                    corpId: '<%=RtHashtable()["corpId"] %>',
                    timeStamp: '<%=RtHashtable()["timeStamp"] %>', // 必填，生成签名的时间戳
                    nonceStr: '<%=RtHashtable()["nonceStr"] %>', // 必填，生成签名的随机串
                    signature: '<%=RtHashtable()["signature"] %>', // 必填，签名
                    type: 0,
                    jsApiList: ['biz.navigation.setRight', 'device.geolocation.get',
            'biz.util.scanCard', 'device.notification.actionSheet',
            'biz.util.datetimepicker', 'biz.util.previewImage'] // 必填，需要使用的jsapi列表，注意：不要带dd。
                });
            }
            else {
                localStorage.setItem("currentOperatorID", $("#hfCurrentOperatorID").val());
                localStorage.setItem("currentOperatorName", $("#hfCurrentOperatorName").val());

                var DateValue = $("#lblTitleTime").text();
                var RoleValue = $("#lblTitleRole").text();
                GetDataByCondition(DateValue, RoleValue);
            }
        });

        dd.ready(function () {
            dd.runtime.permission.requestAuthCode({
                corpId: '<%=RtHashtable()["corpId"] %>',
                onSuccess: function (info) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "AjaxMethods.asmx/GetCurrentOperatorInfo",
                        data: "{code:'" + info.code + "'}",
                        dataType: 'json',
                        success: function (result) {
                            var items = result.d.split(":");
                            $("#hfCurrentOperatorID").val(items[0]);
                            $("#hfCurrentOperatorName").val(items[1]);
                            localStorage.setItem("currentOperatorID", $("#hfCurrentOperatorID").val());
                            localStorage.setItem("currentOperatorName", $("#hfCurrentOperatorName").val());

                            var DateValue = $("#lblTitleTime").text();
                            var RoleValue = $("#lblTitleRole").text();
                            GetDataByCondition(DateValue, RoleValue);
                        }
                    });

                }
            });
        });

        function GoToMine() {
            location.href = "Mine.htm";
        }
        function GoToTODO() {
            location.href = "TODO.htm";
        }
        function GoMobileNavigator() {
            location.href = "MobileNavigator.htm";
        }

        function ThisMonth() {
            $("#Month>img").toggle();
            $("#Mask_layer").show();
            $("#check_div").toggle();
            $("#MyCheck_div").hide();
            $("#My_Down").show();
            $("#My_Up").hide();
            //判断元素显示隐藏
            if ($("#M_Down").is(":visible") == false) {

                $("#Mask_layer").show();
            }
            else {

                $("#Mask_layer").hide();
            }
        }
        function OneSelf() {
            $("#MySelf>img").toggle();
            $("#Mask_layer").show();
            $("#MyCheck_div").toggle();
            $("#check_div").hide();
            $("#M_Down").show();
            $("#M_Up").hide();
            if ($("#My_Down").is(":visible") == false) {
                $("#Mask_layer").show();
            }
            else {
                $("#Mask_layer").hide();
            }
        }

        function HideMask_layer() {
            $("#MyCheck_div").hide();
            $("#check_div").hide();
            $("#Mask_layer").hide();
            $("#M_Down").show();
            $("#M_Up").hide();
            $("#My_Down").show();
            $("#My_Up").hide();
        }

        function goTo(url) {
            document.location = url;
        }

        $(function () {
            $("#Message").hide();
            $("#Personal").hide();
            $("#MessageReaded").hide();
            $("#IsOrNoneRead").hide();
            $("#btnSearch").hide();
            $("#divNavigation a").click(function () {
                $("a", $("#divNavigation")).removeClass("ui-btn-active");
                $(this).addClass("ui-btn-active");
            })
            $(".ui-btn-inner").css("background-color", "white");
        })

        function backToPersonal() {
            $("#footwork").removeClass("ui-btn-active");
            $("#footpersonal").addClass("ui-btn-active");
        }

        $(function () {
            $("#check_div table tr td").click(function () {
                $(this).find(":radio").attr("checked", "checked");
                var DateValue = $(this).find(":radio").val();
                $("#lblTitleTime").text(DateValue);
                $("#lblAchievementsTime").text(DateValue);
                $("#lblPerformanceTime").text(DateValue);
                $("#lblSalesTime").text(DateValue);
                $("#lblAchievingRate").text(DateValue);
                $("#check_div").hide();
                $("#MyCheck_div").hide();
                $("#Mask_layer").hide();
                $("#M_Down").show();
                $("#M_Up").hide();
                var RoleValue = $("#lblTitleRole").text();
                GetDataByCondition(DateValue, RoleValue);

            })

            $("#MyCheck_div table tr td").click(function () {
                $(this).find(":radio").attr("checked", "checked");
                var RoleValue = $(this).find(":radio").val();
                $("#lblTitleRole").text(RoleValue);
                $("#check_div").hide();
                $("#MyCheck_div").hide();
                $("#Mask_layer").hide();
                $("#My_Down").show();
                $("#My_Up").hide();
                var DateValue = $("#lblTitleTime").text();
                GetDataByCondition(DateValue, RoleValue);

            })
        })

        function GetDataByCondition(DateValue, RoleValue) {
            if (DateValue == "") {
                DateValue = $("#lblTitleTime").text();
            }
            if (RoleValue == "") {
                RoleValue = $("#lblTitleRole").text();
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "AjaxMethods.asmx/GetWorkbenchDataByCondition",
                data: "{DateValue:'" + DateValue + "',RoleValue:'" + RoleValue + "',CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                dataType: 'json',
                success: function (result) {
                    $(result.d).each(function () {
                        //收款达成率
                        $("#lblReceiptAmount").text(this['SumReceiptTargetAmount']);
                        $("#lblGotReceiptAmount").text(this['SumReceiptAmount']);
                        //签单达成率
                        $("#lblBusinessAmount").text(this['SumBusinessTargetAmount']);
                        $("#lblGotBusinessAmount").text(this['SumBusinessAmount']);

                        //业绩简报
                        //收款额
                        $("#lblSumReceiptAmount").text(this['SumReceiptAmount']);
                        //业绩简报
                        //合同额
                        $("#lblSumBusinessAmount").text(this['SumBusinessAmount']);
                        //业绩简报
                        //商机额
                        $("#lblOpptunityAmount").text(this['SumOpptunityAmount']);

                        //绩效
                        //新增跟进数
                        $("#lblSumAddNewFollowHistoryCount").text(this['SumAddNewFollowCount']);
                        //绩效
                        //新增收款数
                        $("#lblAddNewReceiptCount").text(this['SumAddNewReceiptCount']);
                        //绩效
                        //新增客户数
                        $("#lblAddNewCustomerCount").text(this['SumAddNewCustomerCount']);
                        //绩效
                        //新增商机数
                        $("#lblAddNewOpptunityCount").text(this['SumAddNewOpptunityCount']);
                        //绩效
                        //新增合同数
                        $("#lblAddNewBusinessCount").text(this['SumAddNewBusinessCount']);
                        //绩效
                        //新增线索数
                        $("#lblAddNewClueCount").text(this['SumAddNewClueCount']);

                        //销售漏斗
                        //初期沟通
                        $("#lblPhase6SumCount").text(this['Phase6SumCount']);
                        $("#lblPhase6Amount").text(this['Phase6SumAmount']);
                        $("#lblPhase6PredictAmount").text(this['Phase6SumPredictAmount']);
                        //销售漏斗
                        //立即评估
                        $("#lblPhase4Count").text(this['Phase4SumCount']);
                        $("#lblPhase4Amount").text(this['Phase4SumAmount']);
                        $("#lblPhase4PredictAmount").text(this['Phase4SumPredictAmount']);
                        //销售漏斗
                        //需求调研
                        $("#lblPhase5Count").text(this['Phase5SumCount']);
                        $("#lblPhase5Amount").text(this['Phase5SumAmount']);
                        $("#lblPhase5PredictAmount").text(this['Phase5SumPredictAmount']);
                        //销售漏斗
                        //方案论证
                        $("#lblPhase2Count").text(this['Phase2SumCount']);
                        $("#lblPhase2Amount").text(this['Phase2SumAmount']);
                        $("#lblPhase2PredictAmount").text(this['Phase2SumPredictAmount']);
                        //销售漏斗
                        //商务谈判
                        $("#lblPhase3Count").text(this['Phase3SumCount']);
                        $("#lblPhase3Amount").text(this['Phase3SumAmount']);
                        $("#lblPhase3PredictAmount").text(this['Phase3SumPredictAmount']);
                        //销售漏斗
                        //签订合同
                        $("#lblPhase7Count").text(this['Phase7SumCount']);
                        $("#lblPhase7Amount").text(this['Phase7SumAmount']);
                        $("#lblPhase7PredictAmount").text(this['Phase7SumPredictAmount']);

                        //荣誉榜
                        //收款额
                        $("#lblReceiptOperatorFace").attr("src", this['MaxReceiptAmountOperatorFace']);
                        $("#lblMaxReceiptAmount").text(this['MaxReceiptAmount']);
                        $("#lblMaxReceiptAmountName").text(this['MaxReceiptAmountOperatorName']);
                        //荣誉榜
                        //合同额
                        $("#BusinessOperatorFace").attr("src", this['MaxBusinessAmountOperatorFace']);
                        $("#lblMaxBusinessAmount").text(this['MaxBusinessAmount']);
                        $("#lblMaxBusinessAmountOperatorName").text(this['MaxBusinessAmountOperatorName']);
                        //荣誉榜
                        //商机额
                        $("#OpptunityOperatorFace").attr("src", this['MaxOpptunityAmountOperatorFace']);
                        $("#lblMaxOpptunityAmount").text(this['MaxOpptunityAmount']);
                        $("#lblMaxOpptunityAmountOperatorName").text(this['MaxOpptunityAmountOperatorName']);
                        //荣誉榜
                        //客户数
                        $("#CustomerOperatorFace").attr("src", this['MaxCustomerCountOperatorFace']);
                        $("#lblMaxCustomerCount").text(this['MaxCustomerCount']);
                        $("#lblMaxCustomerCountOperatorName").text(this['MaxCustomerCountOperatorName']);
                        //荣誉榜
                        //跟进数
                        $("#FollowHistoryOperatorFace").attr("src", this['MaxFollowHistoryCountOperatorFace']);
                        $("#lblMaxFollowCount").text(this['MaxFollowHistoryCount']);
                        $("#lblMaxFollowCountOperatorName").text(this['MaxFollowHistoryCountOperatorName']);

                        function DrowProcess(x, y, radius, process, backColor, proColor, fontColor) {
                            var canvas = document.getElementById('myCanvas');
                            if (canvas.getContext) {
                                var cts = canvas.getContext('2d');
                            } else {
                                return;
                            }

                            cts.beginPath();
                            // 坐标移动到圆心  
                            cts.moveTo(x, y);
                            // 画圆,圆心是24,24,半径24,从角度0开始,画到2PI结束,最后一个参数是方向顺时针还是逆时针  
                            cts.arc(x, y, radius, 0, Math.PI * 2, false);
                            cts.closePath();
                            // 填充颜色  
                            cts.fillStyle = backColor;
                            cts.fill();

                            cts.beginPath();
                            // 画扇形的时候这步很重要,画笔不在圆心画出来的不是扇形  
                            cts.moveTo(x, y);
                            // 跟上面的圆唯一的区别在这里,不画满圆,画个扇形  
                            cts.arc(x, y, radius, Math.PI * 1.5, Math.PI * 1.5 - Math.PI * 2 * process / 100, true);
                            cts.closePath();
                            cts.fillStyle = proColor;
                            cts.fill();

                            //填充背景白色
                            cts.beginPath();
                            cts.moveTo(x, y);
                            cts.arc(x, y, radius - (radius * 0.26), 0, Math.PI * 2, true);
                            cts.closePath();
                            cts.fillStyle = 'rgba(255,255,255,1)';
                            cts.fill();

                            // 画一条线  
                            cts.beginPath();
                            cts.arc(x, y, radius - (radius * 0.30), 0, Math.PI * 2, true);
                            cts.closePath();
                            // 与画实心圆的区别,fill是填充,stroke是画线  
                            cts.strokeStyle = backColor;
                            cts.stroke();

                            //在中间写字 
                            cts.font = "bold 9pt Arial";
                            cts.fillStyle = fontColor;
                            cts.textAlign = 'center';
                            cts.textBaseline = 'middle';
                            cts.moveTo(x, y);
                            cts.fillText(process + "%", x, y);

                        }
                        bfb = 0;
                        time = 0;

                        var sumAmount = $("#lblReceiptAmount").text().replace(/\,/g, "");
                        var gotAmount = $("#lblGotReceiptAmount").text().replace(/\,/g, "");
                        var pReceipt = (gotAmount / sumAmount) * 100;

                        var sumBusinessCount = $("#lblBusinessAmount").text().replace(/\,/g, "");
                        var addNewBusinessCount = $("#lblGotBusinessAmount").text().replace(/\,/g, "");
                        var pBusiness = (addNewBusinessCount / sumBusinessCount) * 100;

                        function Start() {
                            DrowProcess(60, 60, 55, pReceipt.toFixed(2), '#ddd', '#6495ED', '#6495ED');

                            DrowProcess(60, 180, 55, pBusiness.toFixed(2), '#ddd', '#E74C3C', '#E74C3C');
                        }

                        Start();
                    })
                }
            })
        }

        $(function () {
            var divHonor = $("#divHonor").width();
            $("#ulHonor").css("width", divHonor);
            var liWidth = $("#ulHonor li").width();
            var lilength = $("#ulHonor li").length;

            var index = 0;
            var len = (lilength - 1);

            var btn = "";
            btn += "</div><div class='pre'></div><div class='next'></div>";

            $("#divHonor").append(btn);

            $("#divHonor .preNext").css("opacity", 0.1).hover(function () {
                $(this).stop(true, false).animate({ "opacity": "0.3" }, 300);
            }, function () {
                $(this).stop(true, false).animate({ "opacity": "0.1" }, 300);
            });

            //$("#divHonor .pre").hide();

            $("#divHonor .pre").click(function () {
                $("#divHonor .next").show();
                if (index <= 1) {
                    $("#divHonor .pre").hide();

                }

                if (index <= 0) {
                    return;
                }
                index -= 1;
                var newindex = index + 2;
                var newindexs = index + 1;
                $("#circle" + newindex).removeClass("divChecked").addClass("circle");
                $("#circle" + newindexs).removeClass("circle").addClass("divChecked");

                showPics(index);
            });

            $("#divHonor .next").click(function () {
                $("#divHonor .pre").show();
                if (index >= len - 1) {
                    $("#divHonor .next").hide();

                }
                if (index >= len) {
                    return;
                }
                index += 1;
                var newindex = index + 1;
                $("#circle" + index).removeClass("divChecked").addClass("circle");
                $("#circle" + newindex).removeClass("circle").addClass("divChecked");
                showPics(index);
            });

            $("#ulHonor").css("width", liWidth * (lilength));
            var ulwidth = $("#ulHonor").width();
            var newliwidth = ulwidth / lilength;
            $("#ulHonor li").css("width", newliwidth);

            function showPics(index) {
                var nowLeft = -index * newliwidth;
                $("#ulHonor").stop(true, false).animate({ "left": nowLeft }, 300);
            }
        });

        $(document).on("pageinit", "#page", function () {
            $("#divHonor").on("swipeleft", function () {
                $("#divHonor .next").click();
            })

            $("#divHonor").on("swiperight", function () {
                $("#divHonor .pre").click();
            })
        })
    </script>

    <style type="text/css">
        html
        {
            overflow: scroll;
        }
        .ui-block-a, .ui-block-b, .ui-block-c
        {
            font-weight: bold;
            text-align: center;
            background-repeat: no-repeat;
            border-collapse: collapse;
            clear: none !important;
        }
        img
        {
            max-width: 50px;
            max-height: 50px;
            margin-top: 10px;
            float: left;
            width: 45px;
            margin-left: 10px;
        }
        a
        {
            text-decoration: none;
            text-align: center;
            display: block;
            font-size: 1em;
            margin-left: 1em;
            border-collapse: collapse;
            border-spacing: 0;
            margin: 5px 0px 5px 15px;
            color: #000 !important;
            font-family: 微软雅黑;
            font-weight: normal !important;
        }
        ul li
        {
            /* float: left;*/
            display: inline-block; /*让文字和图片各占一行*/
            border-collapse: collapse;
            border-spacing: 0;
        }
        
        .footernavbar a
        {
            border: 0px;
            border-style: none;
        }
        
        .footernavbar p
        {
            margin: 0px;
            margin-bottom: -10px;
            color: #ADACAC;
            margin-top: 3px;
        }
        
        .cssNav
        {
            width: 60%;
            margin: 0px auto;
        }
        .cssNav a
        {
            width: 50%;
            font-size: 14px;
            box-sizing: border-box;
            float: left;
        }
        
        
        #IsOrNoneRead
        {
            width: 90%;
            position: fixed;
            top: 30%;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color: #eeeeee;
            filter: alpha(opacity=90);
        }
        
        .black_overlay
        {
            position: fixed;
            z-index: 1000;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            filter: alpha(opacity=80);
            opacity: 0.3;
            overflow: hidden;
            background-color: #000;
        }
        #check_div
        {
            position: fixed;
            top: 33px;
            width: 100%;
            z-index: 3;
        }
        #check_div table
        {
            width: 100%;
        }
        
        #check_div td
        {
            border-top: 1px solid #ccc;
        }
        #check_div tr
        {
            height: 40px;
        }
        
        
        
        #MyCheck_div
        {
            position: fixed;
            top: 33px;
            width: 100%;
            z-index: 3;
        }
        #MyCheck_div table
        {
            width: 100%;
        }
        
        #MyCheck_div td
        {
            border-top: 1px solid #ccc;
        }
        #MyCheck_div tr
        {
            height: 35px;
        }
        
        input[type=checkbox], input[type=radio]
        {
            -webkit-appearance: none;
            width: 20px;
            height: 20px;
            cursor: pointer;
            vertical-align: bottom;
            background: white;
            position: relative;
            border: 1px solid #ccc;
            border-radius: 13px;
        }
        
        input[type=checkbox]:checked, input[type=radio]:checked
        {
            background: #1296db;
            border: none;
            outline: none;
        }
        
        body
        {
            margin: 0px;
            padding: 0px;
        }
        @media screen and (max-height:768px)
        {
        
            .ui-block-b
            {
                width: 50% !important;
                border-right: 1px solid #efecec;
            }
            .ui-block-a
            {
                width: 50% !important;
                border-right: 1px solid #efecec;
            }
        
        }
        @media screen and (max-height:568px)
        {
        
            .ui-block-b
            {
                width: 50% !important;
                border-right: 1px solid #efecec;
            }
            .ui-block-a
            {
                width: 50% !important;
                border-right: 1px solid #efecec;
            }
        }
        
        .title span
        {
            display: inline-block;
        }
        .WorkBench_div h4
        {
               padding: 0px;
                margin: 0px;
                background-color: white;
                font-size: 1em;
                height: 30px;
                line-height: 30px;
                padding-left: 20px;
        }
        .WorkBench_div ul
        {
            padding: 0px;
            margin: 0px;
            padding-top: 15px;
            padding-bottom: 15px;
            background-color: white;
        }
        
        .WorkBench_div ul li
        {
            display: inline-block;
        }
        .li_one
        {
            width: 50%;
        }
        .WorkBench_div p
        {
            margin: 0px;
            font-family: 微软雅黑;
        }
        .p_fist
        {
            font-size: 12px;
            color: #ccc;
            margin: 0px;
        }
        .p_two
        {
        }
        .WorkBench_div img
        {
            border-radius: 7px;
        }

        #divHonor
        {
            width: 100%;
            height: 325px;
            overflow: hidden;
            margin: 0;
            position: relative;
            background-color: white;
            z-index: 2;
        }
        
        #ulHonor
        {
            position: absolute;
            height: 160px;
        }
        
        #ulHonor li
        {
            float: left;
            height: 160px;
        }
        
        #divHonor .preNext
        {
            width: 30px;
            height: 100px;
            position: absolute;
            top: 45px;
            background: url(../images/sprite.png) no-repeat 0 0;
            cursor: pointer;
        }
        #divHonor .pre
        {
            left: 0;
        }
        #divHonor .next
        {
            right: 0;
            background-position: right top;
        }
        
        .circle
        {
            width: 8px;
            height: 8px;
            border-radius: 15px;
            background-color: #fff;
            border:1px solid #bbb;
        }
        
        .divChecked
        {
            width: 8px;
            height: 8px;
            border-radius: 15px;
            background-color: #aaa;
            border:1px solid #bbb;
        }
        
        .OperatorFace
        {
            width: 100px;
            max-width: 100px;
            margin: 0;
            max-height: 115px;
            float: none;
            border-radius:45px !important;
        }
        
        .cssReceiptAmount
        {
            font-size:16px;
            color:#333;
            font-family: "微软雅黑";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page" id="page">
        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
        <asp:HiddenField ID="hfCommonDataSource" runat="server" />
        <div id="WorkDesk" class="WorkBench_div">
            <div style="width: 100%; background-color: White; z-index: 3; position: fixed; top: 0;">
                <table style="width: 100%; height: 35px; border-top: 1px solid #ccc; border-bottom: 1px solid #ccc;">
                    <tr>
                        <td style="width: 50%; text-align: left; border-right: 1px solid #ccc">
                            <span>销售简报</span>
                        </td>
                        <td class="title" id="Month" onclick="javascript:ThisMonth();" style="width: 25%;
                            text-align: center; border-right: 1px solid #ccc">
                            <span>
                                <asp:Label ID="lblTitleTime" runat="server" Text="本月"></asp:Label></span>
                            <img id="M_Down" src="@images/下箭头.png" style="margin: 0px; padding: 0px; width: 15px;
                                float: right; margin-top: 2px;" />
                            <img id="M_Up" src="@images/上箭头.png" style="display: none; margin: 0px; padding: 0px;
                                width: 15px; float: right; margin-top: 2px;" />
                        </td>
                        <td id="MySelf" onclick="javascript:OneSelf();" style="width: 25%; text-align: center">
                            <span>
                                <asp:Label ID="lblTitleRole" runat="server" Text="自己"></asp:Label></span>
                            <img src="@images/下箭头.png" id="My_Down" style="margin: 0px; padding: 0px; width: 15px;
                                float: right; margin-top: 2px;" />
                            <img src="@images/上箭头.png" id="My_Up" style="display: none; margin: 0px; padding: 0px;
                                width: 15px; float: right; margin-top: 2px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="check_div" style="background-color: White; display: none">
                <table>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" checked="checked" value="本月" /><label>本月</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" value="上月" /><label>上月</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" value="今年" /><label>今年</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" value="去年" /><label>去年</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" value="今天" /><label>今天</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Time" data-role="none" value="昨天" /><label>昨天</label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="MyCheck_div" style="background-color: White; display: none;">
                <table>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Person" checked="checked" data-role="none" value="自己" /><label>只看自己</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Person" data-role="none" value="本部门" /><label>本部门</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="radio" name="XS_Person" data-role="none" value="全公司" /><label>全公司</label>
                        </td>
                    </tr>
                </table>
            </div>

            <%--达成率--%>
            <div style="padding-bottom: 10px; background-color: #f9f9f9; margin-top:35px;">
                <h4 style="border-bottom: 1px solid #ccc;">
                    达成率（<asp:Label ID="lblAchievingRate" runat="server" Text="本月"></asp:Label>）</h4>
                <div style="background-color: #fff;width:100%;height:245px;">
                    <div style="width:50%; float:left;">
                        <canvas id="myCanvas" width="150" height="240">您的浏览器不支持canvas！</canvas>
                    </div>
                    <div style="width:50%; float:left;">
                        <table style="width:100%; line-height:2em;">
                         <tr>
                            <td style=" font-size:14px;">收款达成率</td>
                        </tr>
                        <tr>
                            <td style=" font-size:12px;color:#ccc;">计划：<asp:Label ID="lblReceiptAmount" CssClass="cssReceiptAmount" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style=" font-size:12px;color:#ccc;">实际：<asp:Label ID="lblGotReceiptAmount"  CssClass="cssReceiptAmount" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                    </div>
                    <div style="width:50%; float:left; margin-top:15px;">
                        <table style="width:100%; line-height:2em;">
                         <tr>
                            <td style=" font-size:14px;">签单达成率</td>
                        </tr>
                        <tr>
                            <td style=" font-size:12px;color:#ccc;">计划：<asp:Label ID="lblBusinessAmount"  CssClass="cssReceiptAmount" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style=" font-size:12px;color:#ccc;">实际：<asp:Label ID="lblGotBusinessAmount"  CssClass="cssReceiptAmount" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                    </div>
                        <script type="text/javascript">
                            //x,y 坐标,radius 半径,process 百分比,backColor 中心颜色, proColor 进度颜色, fontColor 中心文字颜色
                            function DrowProcess(x, y, radius, process, backColor, proColor, fontColor) {
                                var canvas = document.getElementById('myCanvas');



                                if (canvas.getContext) {
                                    var cts = canvas.getContext('2d');
                                } else {
                                    return;
                                }

                                cts.beginPath();
                                // 坐标移动到圆心  
                                cts.moveTo(x, y);
                                // 画圆,圆心是24,24,半径24,从角度0开始,画到2PI结束,最后一个参数是方向顺时针还是逆时针  
                                cts.arc(x, y, radius, 0, Math.PI * 2, false);
                                cts.closePath();
                                // 填充颜色  
                                cts.fillStyle = backColor;
                                cts.fill();

                                cts.beginPath();
                                // 画扇形的时候这步很重要,画笔不在圆心画出来的不是扇形  
                                cts.moveTo(x, y);
                                // 跟上面的圆唯一的区别在这里,不画满圆,画个扇形  
                                cts.arc(x, y, radius, Math.PI * 1.5, Math.PI * 1.5 - Math.PI * 2 * process / 100, true);
                                cts.closePath();
                                cts.fillStyle = proColor;
                                cts.fill();

                                //填充背景白色
                                cts.beginPath();
                                cts.moveTo(x, y);
                                cts.arc(x, y, radius - (radius * 0.26), 0, Math.PI * 2, true);
                                cts.closePath();
                                cts.fillStyle = 'rgba(255,255,255,1)';
                                cts.fill();

                                // 画一条线  
                                cts.beginPath();
                                cts.arc(x, y, radius - (radius * 0.30), 0, Math.PI * 2, true);
                                cts.closePath();
                                // 与画实心圆的区别,fill是填充,stroke是画线  
                                cts.strokeStyle = backColor;
                                cts.stroke();

                                //在中间写字 
                                cts.font = "bold 9pt Arial";
                                cts.fillStyle = fontColor;
                                cts.textAlign = 'center';
                                cts.textBaseline = 'middle';
                                cts.moveTo(x, y);
                                cts.fillText(process + "%", x, y);

                            }
                            bfb = 0;
                            time = 0;

                            var sumAmount = $("#lblReceiptAmount").text();
                            var gotAmount = $("#lblGotReceiptAmount").text();
                            var pReceipt = (gotAmount / sumAmount) * 100;


                            var sumBusinessCount = $("#lblBusinessAmount").text();
                            var addNewBusinessCount = $("#lblGotBusinessAmount").text();
                            var pBusiness = (addNewBusinessCount / sumBusinessCount) * 100;

                            function Start() {
                                DrowProcess(60, 60, 55, pReceipt.toFixed(2), '#ddd', '#6495ED', '#6495ED');

                                DrowProcess(60, 180, 55, pBusiness.toFixed(2), '#ddd', '#E74C3C', '#E74C3C');
                            }

                            Start();
                        </script>
                </div>
            </div>
            <%--业绩简报--%>
            <div style="padding-bottom: 10px; background-color: #f9f9f9;">
                <h4 style="border-bottom: 1px solid #ccc;">
                    业绩简报（<asp:Label ID="lblAchievementsTime" runat="server" Text="本月"></asp:Label>）</h4>
                <div>
                    <ul style="padding-left: 0px; width: 100%; border-bottom: 1px solid #ccc;">
                        <li style="width: 50%; display: inline-block;">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            收款额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumReceiptAmount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="display: inline-block;">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            合同额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumBusinessAmount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="display: inline-block; width: 50%;">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                           商机额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblOpptunityAmount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>


                 
            </div>
            <%--绩效指标--%>
            <div style="padding-bottom: 10px; background-color: #f9f9f9;">
                <h4 style="border-bottom: 1px solid #ccc;">
                    绩效指标（<asp:Label ID="lblPerformanceTime" runat="server" Text="本月"></asp:Label>）</h4>
                <div>
                    <ul style="padding-left: 0px; width: 100%; border-bottom: 1px solid #ccc;">
                        <li class="li_one">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/跟进管理.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增跟进数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumAddNewFollowHistoryCount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/商机.png" style="border-radius: 7px; width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增收款数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewReceiptCount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li class="li_one">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/客户.png" style="margin-left: 8px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增客户数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewCustomerCount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增商机数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewOpptunityCount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li class="li_one">
                            <table>
                                <tr>
                                    <td>
                                        <img src="@images/合同管理.png" style="margin-left: 8px; border-radius: 15px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增合同数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewBusinessCount" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/合同管理.png" style="margin-left: 8px; border-radius: 15px;" />
                                    </td>
                                    <td>
                                        <p class="p_fist">
                                            新增线索数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewClueCount"  CssClass="AmountLabel"  runat="server"></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
               
            </div>
            <%-- 销售漏斗--%>
            <div style="background-color: #f9f9f9;">
                <h4 style="border-bottom: 1px solid #ccc;">
                    销售漏斗（<asp:Label ID="lblSalesTime" runat="server" Text="本月"></asp:Label>）</h4>
                <div>
                    <table style="background-color: White; width: 100%; padding: 5px 10px 5px 10px;">
                        <tr style="font-size: 12px;">
                            <td>
                            </td>
                            <td>
                                单数
                            </td>
                            <td>
                                金额
                            </td>
                            <td>
                                预测金额
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 140px; color: white; display: block; height: 0; border-top: 30px solid #37ACFE;
                                    border-left: 6px solid transparent; border-right: 6px solid transparent; text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        初期沟通</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #37ACFE"></span><span><asp:Label ID="lblPhase6SumCount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase6Amount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase6PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 126px; color: white; margin-left: 7px; display: block; height: 0;
                                    border-top: 30px solid #56CA4E; border-left: 6px solid transparent; border-right: 6px solid transparent;
                                    text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        立即评估</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #56CA4E"></span><span><asp:Label ID="lblPhase4Count" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase4Amount" runat="server" Text=""></asp:Label> </span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase4PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 112px; color: white; margin-left: 14px; display: block; height: 0;
                                    border-top: 30px solid #FBE263; border-left: 6px solid transparent; border-right: 6px solid transparent;
                                    text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        需求调研</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #FBE263"></span><span><asp:Label ID="lblPhase5Count" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase5Amount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase5PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 98px; color: white; margin-left: 21px; display: block; height: 0;
                                    border-top: 30px solid #F59E16; border-left: 6px solid transparent; border-right: 6px solid transparent;
                                    text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        方案论证</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #F59E16"></span><span><asp:Label ID="lblPhase2Count" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase2Amount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase2PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 84px; color: white; margin-left: 28px; display: block; height: 0;
                                    border-top: 30px solid #F72B1F; border-left: 6px solid transparent; border-right: 6px solid transparent;
                                    text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        商务谈判</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #F72B1F"></span><span><asp:Label ID="lblPhase3Count" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase3Amount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase3PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 70px; color: white; margin-left: 35px; display: block; height: 0;
                                    border-top: 30px solid #F71010; border-left: 6px solid transparent; border-right: 6px solid transparent;
                                    text-align: center; text-shadow:none">
                                    <p style="margin-top: -26px; font-weight: normal; font-size: 16px;">
                                        签订合同</p>
                                </div>
                            </td>
                            <td>
                                <span class="smallback" style="background-color: #F71010"></span><span><asp:Label ID="lblPhase7Count" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase7Amount" runat="server" Text=""></asp:Label></span>
                            </td>
                            <td>
                                <span><asp:Label ID="lblPhase7PredictAmount" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--荣誉榜--%>
            <div id="Honor" style="padding-bottom: 10px; background-color: #f9f9f9; margin-top: 10px;">
                <h4 style="border-bottom: 1px solid #ccc;">
                    荣誉榜</h4>
                <div id="divHonor">
                    <ul id="ulHonor" style="padding-left: 0px;">
                        <li style="width: 100%">
                            <table style="width: 100%; text-align: center; font-size: 14px;">
                                <tr style="">
                                    <td>
                                        收款额排名
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:21px;">
                                            <img src="@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
                                            float: none;" />
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        收款额：<asp:Label ID="lblMaxReceiptAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaxReceiptAmountName" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="width: 100%">
                            <table style="width: 100%; text-align: center; font-size: 14px;">
                                <tr style="">
                                    <td>
                                        合同额排名
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:21px;">
                                            <img src="@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
                                            float: none;" />
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        合同额：<asp:Label ID="lblMaxBusinessAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaxBusinessAmountOperatorName" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="width: 100%">
                            <table style="width: 100%; text-align: center; font-size: 14px;">
                                <tr style="">
                                    <td>
                                        商机额排名
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:21px;">
                                            <img src="@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
                                            float: none;" />
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        商机额：<asp:Label ID="lblMaxOpptunityAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaxOpptunityAmountOperatorName" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="width: 100%">
                            <table style="width: 100%; text-align: center; font-size: 14px;">
                                <tr style="">
                                    <td>
                                        新增客户数排名
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:21px;">
                                            <img src="@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
                                            float: none;" />
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        新增客户数：<asp:Label ID="lblMaxCustomerCount" runat="server" Text="99"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaxCustomerCountOperatorName" Text="洪月"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li style="width: 100%">
                            <table style="width: 100%; text-align: center; font-size: 14px;">
                                <tr style="">
                                    <td>
                                        新增跟进数排名
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:21px;">
                                            <img src="@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
                                            float: none;" />
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        新增跟进数：<asp:Label ID="lblMaxFollowCount" runat="server" Text="9999"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMaxFollowCountOperatorName" Text="洪月"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>

                    <div id="divcircle" style="width:80px;height:49px; position:relative; margin:0 auto;top:285px; text-align:center;">
                        <ul>
                            <li>
                                <div id="circle1" class="divChecked"></div>
                            </li>
                             <li>
                                <div id="circle2" class="circle"></div>
                            </li>
                             <li>
                                <div id="circle3" class="circle"></div>
                            </li>
                             <li>
                                <div id="circle4" class="circle"></div>
                            </li>
                             <li>
                                <div id="circle5" class="circle"></div>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>
        <div id="Mask_layer" onclick="javascript:HideMask_layer()" style="height: 100%; width: 100%;
            display: none; background-color: rgba(125, 122, 122, 0.59); position: fixed;
            top: 0px; left: 0; z-index: 2;">
        </div>
        <div data-role="footer" data-position="fixed" data-ajax="false" data-theme="c" data-tap-toggle="false">
            <div data-role="navbar" data-ajax="false" class="footernavbar">
                <ul>
                    <li style="width: 25% !important; border: none;"><a>
                        <img src="@images/工作台.png" id="img_WorkDesk1" style="width: 23px; float: none; height: 23px;
                            margin: 0px;" />
                        <p id="WorkTai" style="color: #1296DB">
                            工作台</p>
                    </a></li>
                    <li onclick="javascript:GoMobileNavigator();" style="width: 25% !important; border: none;">
                        <a href="" id="footwork" data-role="button">
                            <img src="@images/work1.png" id="img_work1" style="width: 23px; float: none; height: 23px;
                                margin: 0px;" />
                            <p id="WorkP">
                                工作</p>
                        </a></li>
                    <li onclick="javascript:GoToTODO();" style="width: 25% !important; border: none;">
                        <a href="" id="footmessage" data-role="button">
                            <img src="@images/代办1.png" id="img_message1" style="width: 23px; float: none; height: 23px;
                                margin: 0px;" />
                            <p id="CommissionP">
                                待办</p>
                        </a></li>
                    <li onclick="javascript:GoToMine();" style="width: 25% !important; border: none;"><a
                        href="" id="footpersonal" data-role="button">
                        <img src="@images/我的.png" id="img_my1" style="width: 23px; float: none; height: 23px;
                            margin: 0px;" />
                        <p id="MyP">
                            我的</p>
                    </a></li>
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
