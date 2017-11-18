<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewDesk.aspx.cs" Inherits="SmartSoft.Web.Common.NewDesk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>工作台</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/CheckBoxSelect.js" type="text/javascript"></script>
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script src="../@Scripts/window.js" type="text/javascript"></script>
    <script src="../@Scripts/echarts-all.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            sessionStorage.clear();
            localStorage.clear();
            localStorage.setItem("currentOperatorID", $("#hfCurrentOperatorID").val());
            localStorage.setItem("currentOperatorName", $("#hfCurrentOperatorName").val());
        });

        function GetNewData() {
            var DateValue = $("#lblTitleTime").val();
            var RoleValue = $("#lblTitleRole").val();
            $("#lblAchievingRate").text(DateValue);
            $("#lblPerformanceTime").text(DateValue);
            $("#lblSalesTime").text(DateValue);
            $("#lblAchievementsTime").text(DateValue);
            GetDataByCondition(DateValue, RoleValue);
        }

        $(function () {
            var DateValue = $("#lblTitleTime").val();
            var RoleValue = $("#lblTitleRole").val();
            GetDataByCondition(DateValue, RoleValue);
        })

        function GetDataByCondition(DateValue, RoleValue) {
            if (DateValue == "") {
                DateValue = $("#lblTitleTime").val();
            }
            if (RoleValue == "") {
                RoleValue = $("#lblTitleRole").val();
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetWorkbenchDataByCondition",
                data: "{DateValue:'" + DateValue + "',RoleValue:'" + RoleValue + "',CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                dataType: 'json',
                success: function (result) {
                    $(result).each(function () {
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

                        function DrowProcess(x, y, radius, process, backColor, proColor, fontColor, CanvasID) {
                            var canvas = document.getElementById(CanvasID);
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

                        var sumAmount = $("#lblReceiptAmount").text().replace(/\,/g, "");
                        var gotAmount = $("#lblGotReceiptAmount").text().replace(/\,/g, "");
                        var pReceipt = (gotAmount / sumAmount) * 100;

                        var sumBusinessCount = $("#lblBusinessAmount").text().replace(/\,/g, "");
                        var addNewBusinessCount = $("#lblGotBusinessAmount").text().replace(/\,/g, "");
                        var pBusiness = (addNewBusinessCount / sumBusinessCount) * 100;

                        function Start() {
                            DrowProcess(90, 120, 80, pReceipt.toFixed(2), '#ddd', '#6495ED', '#6495ED', 'myCanvas');

                            DrowProcess(90, 120, 80, pBusiness.toFixed(2), '#ddd', '#E74C3C', '#E74C3C', 'mySecendCanvas');
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
            btn += "</div><div class='pre'><img src='../@images/Pre.png'/></div><div class='next'><img src='../@images/Next.png'/></div>";

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

       
    </script>

    <script type="text/javascript">
        function Pre() {
            var currentYear = $("#lblCalendarYear").html();
            var currentMonth = $("#lblCalendarMonth").html();
            var currentDate = new Date(currentYear, currentMonth - 1, 1);
            currentDate.setMonth(currentDate.getMonth() - 1); //前一月
            var year = currentDate.getFullYear();
            var month = currentDate.getMonth() + 1;
            getMonthContent(year, month, 1);
        }

        function Next() {
            var currentYear = $("#lblCalendarYear").html();
            var currentMonth = $("#lblCalendarMonth").html();
            var currentDate = new Date(currentYear, currentMonth - 1, 1);
            currentDate.setMonth(currentDate.getMonth() + 1); //下一月
            var year = currentDate.getFullYear();
            var month = currentDate.getMonth() + 1;
            getMonthContent(year, month, 1);
        }

        function gotoToday() {
            var currentDay = new Date();
            getMonthContent(currentDay.getFullYear(), currentDay.getMonth() + 1, currentDay.getDate());
        }

        $(function () {
            if (localStorage.getItem("year") == null) {
                gotoToday();
            }
            else {
                var year = localStorage.getItem("year");
                var month = localStorage.getItem("month");
                var day = localStorage.getItem("day");
                getMonthContent(year, month, day);
            }
        })

        function gotoPlanDetail(id) {
            document.location = "../data/CustomerFollowPlanEditForm.aspx?ID=" + id;
        }

        function getMonthContent(year, month, day) {
            var currentOperatorID = localStorage.getItem("currentOperatorID");
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetWorkCalendarMonthContent",
                data: "{currentOperatorID:" + currentOperatorID + ",year:" + year + ",month:" + month + "}",
                dataType: 'json',
                success: function (result) {
                    $("#divMonthContent").html(result);
                    getDayContent(year, month, day);
                }
            });
        }

        function getDayContent(year, month, day) {
            var currentOperatorID = localStorage.getItem("currentOperatorID");
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetWorkCalendarDateContent",
                data: "{currentOperatorID:" + currentOperatorID + ",year:" + year + ",month:" + month + ",day:" + day + "}",
                dataType: 'json',
                success: function (result) {
                    $("#CalendarContentTitle").html(year + "-" + month + "-" + day + " 共有" + result[1] + "项日程");
                    SetSelected(year, month, day);
                    if (result[0] != "") {
                        $("#divPlanItems").html(result[0]);
                        localStorage.setItem("year", year);
                        localStorage.setItem("month", month);
                        localStorage.setItem("day", day);
                    }
                    else {
                        var html = "";
                        html += '<div style=" text-align:center;">';
                        html += '<img src="../@images/RiCheng.png" style=" margin:0px;" /></div>';
                        html += '<div style=" text-align:center;">';
                        html += '<label>暂无日程数据哦~</label></div>';
                        $("#divPlanItems").html(html);
                    }
                }
            });
        }

        function SetSelected(year, month, day) {
            $("div").removeClass("CalandarSelected");
            $("#div" + year + "" + month + "" + day).addClass("CalandarSelected");
            $("#lblCalendarYear").html(year);
            $("#lblCalendarMonth").html(month);
        }

        function clickDay(year, month, day, td) {
            getDayContent(year, month, day);
        }
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
            width: 50%;
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
            width: 50%;
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
            height: 50px;
            line-height: 50px;
            padding-left: 20px;
            font-family: "微软雅黑";
            font-weight: normal;
            font-size: 16px;
            border-bottom: 1px solid #eee; 
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
       
        .WorkBench_div p
        {
            margin: 0px;
            font-family: 微软雅黑;
        }
        .WorkBench_div img
        {
            border-radius: 7px;
        }

        #divHonor
        {
            width: 100%;
            height: 240px;
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
            left: 5%;
             position:absolute;
             z-index:99;
        }
        #divHonor .next
        {
            right: 5%;
             position:absolute;
             z-index:99;
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
    </style>
    <style type="text/css">
        html
        {
            overflow: scroll;
        }
        img
        {
            max-width: 50px;
            max-height: 50px;
            margin-top: 10px;
            /*float: left;*/
            width: 45px;
            margin-left: 10px;
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
        body
        {
            margin: 0px;
            padding: 0px;
        }
        
        .Commission img
        {
            width: 25px;
            margin: 0px 15px 0px 15px;
        }
        .Commission p
        {
            font-size: 14px;
            margin: 0px;
            padding: 0px;
            display: inline-block;
            font-family: 微软雅黑,Simsun;
        }
        .Commission table
        {
            height: 100%;
            width: 100%;
        }
        
        .Commission li
        {
            height: 50px;
        }
        .cjyhelp li
        {
            border-bottom: 1px solid #eee;
        }
        .help_a
        {
            width: 30%;
        }
        .help_b
        {
            text-align: left;
        }
        
        .CalendarTitle
        {
            width: 30%;
            float: left;
            height: 34px;
            line-height: 35px;
            background-color:#fff;
            position:fixed;
            top:0;
            border-bottom:1px solid #ccc;
            z-index:3;
        }
        
        .Report
        {
            width: 70%;
            background-color: White; 
            z-index: 3; 
            position: fixed; 
            top: 0;
        }
        
        .CalendarTitle div
        {
            float: left;
        }
        
        .CalandarSelected
        {
            border-color: #FFB835;
            background-color: #FFB835;
            color: #fff;
            text-shadow: none;
            margin: 0 auto;
            width: 25px;
            border-radius: 15px;
            height: 25px;
            line-height: 25px;
        }
        
        .CalendarCenter
        {
            text-align: center;
            height: 40px;
            line-height: 40px;
        }
        
        #CalendarTable
        {
            width: 100%;
        }
        
        #CalendarTable tr th
        {
            text-align: center;
            border-bottom: 1px solid #eee;
            height:50px;
            font-weight:normal;
            color: #929292;
        }
        
        #CalendarTable tr td
        {
            text-align: center;
            border-bottom: 1px solid #f9f9f9;
            cursor:pointer;
        }
        
        .lblHasContent
        {
            margin-left: 2%;
            margin-top: -10px;
            position: absolute;
            border: 3px solid #3C96DE;
            border-radius: 10px;
        }
        .cssPlanItem
        {
            /*width:100%;*/
            font-size: 14px;
            padding-left: 10px;
            margin: 0px;
            height: 40px;
            line-height: 40px;
            border-bottom: 1px solid #eee;
            font-family: 微软雅黑,Simsun;
            cursor:pointer;
        }
        .CalendarFirstRow
        {
            background-color: #fff;
            text-align: center;
            font-size: 13px;
            color: #666;
            font-weight: normal;
            height:31px;
        }
        
        .ReceiptLable
        {
            background-color: #6495ED;
            padding: 5px;
            padding-right: 60px;
            border-top-right-radius: 20px;
            border-bottom-right-radius: 20px;
            color:#fff;
            font-family: "微软雅黑";
            font-size:14px;
        }
        
        .OrderLable
        {
            background-color: #E74C3C;
            padding: 5px;
            padding-right: 60px;
            border-top-right-radius: 20px;
            border-bottom-right-radius: 20px;
            color:#fff;
            font-family: "微软雅黑";
            font-size:14px;
        }
        
        .divAchieving
        {
            height:60px;
            line-height:60px;
            padding-left:15px;
        }
        
        .Empty
        {
            padding-left:15px;
        }
        
        .TrueLabel
        {
            font-size: 12px;
            line-height: 30px;
            color: #929292;
        }
        
        .AmountLabel
        {
            font-family: "Helvetica Neue", Helvetica, Microsoft Yahei, Hiragino Sans GB, WenQuanYi Micro Hei, sans-serif !important;
            color: #666666;
            font-size:28px;
        }
        
        .Performance li
        {
            width:33.33%;
            float:left;
        }
        
        #SalesTable tr th
        {
            color:#929292;
            font-family:微软雅黑;
            font-weight:normal;
        }
        
        #SalesTable tr td
        {
            font-family: "Helvetica Neue", Helvetica, Microsoft Yahei, Hiragino Sans GB, WenQuanYi Micro Hei, sans-serif !important;
            color: #666666;
            font-size:18px;
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page" id="page">
        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
        <asp:HiddenField ID="hfCommonDataSource" runat="server" />
        <div>
            <div style=" width:70%; float:left;">
                <div id="WorkDesk" class="WorkBench_div">
                    <div class="Report">
                        
                        <table style="width: 100%; height: 35px; border-bottom: 1px solid #ccc;">
                            <tr>
                                <td style="width: 50%; text-align: left;">
                                    <span>销售简报</span>
                                </td>
                                <td class="title" id="Month" style="width: 25%;
                                    text-align: center;">
                                    <span>
                                        <asp:DropDownList ID="lblTitleTime" onchange="javascript:GetNewData()" runat="server">
                                            <asp:ListItem Value="本月">本月</asp:ListItem>
                                            <asp:ListItem Value="上月">上月</asp:ListItem>
                                            <asp:ListItem Value="今年">今年</asp:ListItem>
                                            <asp:ListItem Value="去年">去年</asp:ListItem>
                                            <asp:ListItem Value="今天">今天</asp:ListItem>
                                            <asp:ListItem Value="昨天">昨天</asp:ListItem>
                                        </asp:DropDownList>
                                   </span>
                                </td>
                                <td id="MySelf" style="width: 25%; text-align: center;">
                                    <span>
                                        <asp:DropDownList ID="lblTitleRole" onchange="javascript:GetNewData();" runat="server">
                                            <asp:ListItem Value="自己">自己</asp:ListItem>
                                            <asp:ListItem Value="本部门">本部门</asp:ListItem>
                                            <asp:ListItem Value="全公司">全公司</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
            <%--达成率--%>
            <div style="padding-bottom: 10px;padding-right: 10px; background-color: #f9f9f9; margin-top:35px;">
                <h4 style="color:#78c06e;">
                    达成率（<asp:Label ID="lblAchievingRate" runat="server" Text="本月"></asp:Label>）</h4>
                <div style="background-color: #fff;width:100%;height:245px;">
                    <div style="width:50%; float:left;">
                        
                        <div style="width:50%; float:left;">

                            <div class="divAchieving">
                                <label class="ReceiptLable">收款达成率</label>
                            </div>
                            <div class="Empty">
                                <div><label class="TrueLabel">计划</label></div>
                                <div><asp:Label ID="lblReceiptAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></div>
                            </div>
                            <div class="Empty">
                                <div><label class="TrueLabel">实际</label></div>
                                <div><asp:Label ID="lblGotReceiptAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></div>
                            </div>
                        </div>
                        <div style="width:50%; float:left;">
                            <canvas id="myCanvas" width="180" height="240">您的浏览器不支持canvas！</canvas>
                        </div>
                    </div>
                    
                    <div style="width:50%; float:left;">
                        
                        <div style="width:50%; float:left;">

                            <div class="divAchieving">
                                <label class="OrderLable">签单达成率</label>
                            </div>
                            <div class="Empty">
                                <div><label class="TrueLabel">计划</label></div>
                                <div><asp:Label ID="lblBusinessAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></div>
                            </div>
                            <div class="Empty">
                                <div><label class="TrueLabel">实际</label></div>
                                <div><asp:Label ID="lblGotBusinessAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></div>
                            </div>
                        </div>
                        <div style="width:50%; float:left;">
                            <canvas id="mySecendCanvas" width="180" height="240">您的浏览器不支持canvas！</canvas>
                        </div>
                    </div>   
                </div>
            </div>
            <%--业绩简报--%>
            <div style="padding-bottom: 10px;padding-right: 10px; background-color: #f9f9f9;">
                <h4 style="color:#5c97f5">
                    业绩简报（<asp:Label ID="lblAchievementsTime"  runat="server" Text="本月"></asp:Label>）</h4>
                <div style="height: 90px;width: 100%;background-color: #fff;">
                    <ul class="Performance" style=" width: 100%;">
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            收款额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumReceiptAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            合同额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumBusinessAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                           商机额</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblOpptunityAmount" CssClass="AmountLabel" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
            </div>
            <%--绩效指标--%>
            <div style="padding-bottom: 10px;padding-right: 10px; background-color: #f9f9f9;">
                <h4 style="color:#5cc9f2;">
                    绩效指标（<asp:Label ID="lblPerformanceTime" runat="server" Text="本月"></asp:Label>）</h4>
                <div style=" height:160px; background-color:#fff;">
                    <ul class="Performance" style=" width: 100%;">
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/跟进管理.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            新增跟进数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblSumAddNewFollowHistoryCount"  CssClass="AmountLabel" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/商机.png" style="border-radius: 7px; width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            新增收款数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewReceiptCount"  CssClass="AmountLabel" runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/客户.png" style="margin-left: 8px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            新增客户数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewCustomerCount"  CssClass="AmountLabel"  runat="server" Text=""></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </li>
                        <li>
                            <table>
                                <tr>
                                    <td>
                                        <img src="../@images/商机钱.png" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <p class="TrueLabel">
                                            新增商机数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewOpptunityCount"  CssClass="AmountLabel"  runat="server" Text=""></asp:Label></p>
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
                                        <p class="TrueLabel">
                                            新增合同数</p>
                                        <p class="p_two">
                                            <asp:Label ID="lblAddNewBusinessCount"  CssClass="AmountLabel"  runat="server" Text=""></asp:Label></p>
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
                                        <p class="TrueLabel">
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
            <div style="background-color: #f9f9f9; padding-bottom:10px;padding-right: 10px;">
                <h4 style="color:#ff943e;">
                    销售漏斗（<asp:Label ID="lblSalesTime" runat="server" Text="本月"></asp:Label>）</h4>
                <div>
                    <table id="SalesTable" style="background-color: White; width: 100%; padding: 5px 10px 5px 10px;">
                        <tr style="font-size: 12px;">
                            <th>
                            </th>
                            <th>
                                单数
                            </th>
                            <th>
                                金额
                            </th>
                            <th>
                                预测金额
                            </th>
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
            <div id="Honor" style="padding-bottom: 10px;padding-right: 10px; background-color: #f9f9f9;">
                <h4 style="color:#E74C3C">
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
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:20px;">
                                            <img src="../@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
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
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:20px;">
                                            <img src="../@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
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
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:20px;">
                                            <img src="../@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
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
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:20px;">
                                            <img src="../@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
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
                                        <div  style="width:100%;float:left;position:relative;z-index:22;margin-top:20px;">
                                            <img src="../@images/Firstly.png" style="max-width: 100px; margin: 0px; width: 100px; max-height: 100px;
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

                    <div id="divcircle" style="width:80px;height:49px; position:relative; margin:0 auto;top:200px; text-align:center;">
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
        </div>
            <div style=" float:left; width:30%; background-color:#f9f9f9;">
                <div style="background-color: #f9f9f9; margin-bottom:10px;">
                    <div class="CalendarTitle">
                        <div style="width: 30%;">
                            <label style="padding-left: 10px;">
                                日程</label>
                        </div>
                        <div style="text-align: center; width: 40%;">
                            <a style="padding-right: 10px;cursor: pointer;" onclick="javascript:Pre();"><</a><label id="lblCalendarYear">2017</label>年<label
                                id="lblCalendarMonth">5</label>月<a style="padding-left: 10px;cursor: pointer;" onclick="javascript:Next();">></a>
                        </div>
                        <div style="text-align: right; width: 30%;">
                            <a style="padding-right: 10px;cursor: pointer;" onclick="javascript:gotoToday();">返回今日</a>
                        </div>
                    </div>
                    <div style="background-color: white; margin-top:35px;">
                        <div>
                            <div id="divMonthContent">
                                <table cellpadding="0" cellspacing="0" id="CalendarTable">
                                    <tr class="CalendarFirstRow">
                                        <th>
                                            周一
                                        </th>
                                        <th>
                                            周二
                                        </th>
                                        <th>
                                            周三
                                        </th>
                                        <th>
                                            周四
                                        </th>
                                        <th>
                                            周五
                                        </th>
                                        <th>
                                            周六
                                        </th>
                                        <th>
                                            周日
                                        </th>
                                    </tr>
                                </table>
                            </div>
                            <div style="background-color: #fff; text-align: center; color: #929292; height:30px; line-height:30px; font-size:14px;border-bottom: 1px solid #eee;">
                                <label id="CalendarContentTitle"> 
                                05-17日共项日程</label>
                            </div>
                            <div id="divPlanItems">
                               
                            </div>
                        </div>
                    </div>
                </div>
                <div id="Commission" class="Commission" style="margin: 0px; padding: 0px;">
                    <div class="cjyhelp" style="background-color: #f9f9f9;">
                        
                        <div style="background-color: White">
                            <h4 style="height: 30px; font-weight: 100; line-height: 30px; margin: 0; margin-left: 8px;">
                                销售助手
                            </h4>
                            <ul style="padding-left: 0px; width: 99%; margin: 0px; padding: 0px;">
                                <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerList.aspx?ConditionType=NDayNotFollowCustomer&ConditionValue=7">
                                                <p>
                                                    超过7天没有跟进的客户</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayNotFollowCustomer"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerList.aspx?ConditionType=NDayNotFollowNotSuccessCustomer&ConditionValue=7">
                                                <p>
                                                    超过7天没有跟进的未成交客户</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayNotFollowNotSuccessCustomer"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerList.aspx?ConditionType=NDayNotFollowSuccessCustomer&ConditionValue=30">
                                                <p>
                                                    超过30天没有跟进的成交客户</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayNotFollowSuccessCustomer"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerList.aspx?ConditionType=NDayNotSuccessCustomer&ConditionValue=30">
                                                <p>
                                                    超过30天没有成交的客户</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayNotSuccessCustomer"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                 <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerOpptunityList.aspx?ConditionType=NDaySuccessOpptunity&ConditionValue=30">
                                                <p>
                                                    预计30天内签单的商机</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDaySuccessOpptunity"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                               <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerBusinessList.aspx?ConditionType=NDayExpireBusiness&ConditionValue=30">
                                                <p>
                                                    30天内到期的合同</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayExpireBusiness"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                               <li style="width: 100%; display: inline-block; border-top: 1px solid #eee;">
                                    <table>
                                        <tr>
                                            <td class="help_a">
                                                <img src="../@images/SalesNotice.png" />
                                            </td>
                                            <td class="help_b">
                                                <a target="_blank" href="../Data/CustomerLinkManList.aspx?ConditionType=NDayBirthdayLinkMan&ConditionValue=30">
                                                <p>
                                                    7天内过生日的客户联系人</p>
                                                <p>
                                                    (<asp:Label runat="server" ID="lblNDayBirthdayLinkMan"></asp:Label>)</p>
                                               </a>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                               
                              
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
        
    </div>
    </form>
</body>
</html>
