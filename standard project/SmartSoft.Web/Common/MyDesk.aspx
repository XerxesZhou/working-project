<%@ Page Language="C#" AutoEventWireup="true" Inherits="MyDesk" CodeBehind="MyDesk.aspx.cs" %>

<html>
<head>
    <title>创捷易工作平台</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/CheckBoxSelect.js" type="text/javascript"></script>
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script src="../@Scripts/window.js" type="text/javascript"></script>
    <script src="../@Scripts/echarts-all.js" type="text/javascript"></script>
    <style type="text/css">
    html
    {
        overflow-y: scroll !important;
    }
    .DashboardHeader
    {
        background-color:White;
        border-bottom:1px solid rgb(204,204,204);
        margin-top:10px;
    }
    ul li
    {
        float:left;
        text-align:center;float:left;margin-right: 10px;display: inline-block;/*让文字和图片各占一行*/
        padding:5px 10px 10px 10px;
    }
    
    ul img
    {
        width:60px; height:60px;
        border-radius: 50%;
        border:1px solid #e0e0e0;
        background-color:#f1f1f1;
        cursor:pointer;
    }
        
    a
    {
        display: block;
        text-decoration:none;
        margin-top:2px;
    }
    
    .black_overlay {
        position: fixed;
        z-index:1000;
        width: 100%;
        height: 100%;
        top: 0;
        left: 0;
        filter: alpha(opacity=80);
        opacity: 0.3;
        overflow: hidden;
        background-color: #000;
    }
    
    .HerCss
    {
        cursor:pointer;
    }
    
    #animation2 ul li a
    {
        color:#018BE5;
        width:63px;
         font-size:14px;
    }
         
    .div_model
    {
        display:none;
        z-index:100;
        position: absolute;
        top: 40%;
        left: 50%;
        background-color: #eeeeee;
        margin: -70px 0px 0px -190px;
        text-align: center;
    }
   
    #divOrderFinishRate
    {
        height:250px;
        width:100%;
    }
    #divReceiptFinishRate
    {
        height:250px;
        width:100%;
    }
    #divMyFunnel
    {
        height:250px;
        width:100%;
        text-align:center;
    }
    #divAllFunnel
    {
        height:250px;
        width:100%;
        text-align:center;
    }
</style>
    <script type="text/javascript">
        function SwitchMenu(theClass) {
            var alldivTags = document.getElementsByTagName("div");
            for (i = 0; i < alldivTags.length; i++) {
                if (alldivTags[i].className == theClass) {
                    if (alldivTags[i].style.display == 'none') {
                        alldivTags[i].style.display = 'block';
                    } else {
                        alldivTags[i].style.display = 'none';
                    }
                }
            }
        }

        function _delmodel(a) {
            msg = "确认不显示此模块吗?";
            if (window.confirm(msg)) {
                var opeid = $("#hfCurrentOperatorID").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/DeleteOperatorDesktop",
                    data: "{opeID:" + opeid + ",modelName:'" + a + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert("操作成功！");
                        window.location.href = "MyDesk.aspx";
                    }
                });
            }
        }

        function _editmodel(a) {
            $(".div_model").show();
            $("#lblTitle").html(a);
            $("#fade").show();
        }

        function _savemodel() {
            var a = $("#lblTitle").html();
            var num = $("#txtNum").val();
            var opeid = $("#hfCurrentOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/ModifyOperatorDesktop",
                data: "{opeID:" + opeid + ",modelName:'" + a + "',lookNum:" + num + "}",
                dataType: 'json',
                success: function (result) {
                    alert("操作成功！");
                    window.location.href = "MyDesk.aspx";
                }
            });
        }

        $(function () {

            $("#esc").click(function () {
                $(".div_model").hide();
                $("#fade").hide();

            })
        })

        $(function () {

            //销售达成率
            var orderFinishRate = $("#hfOrderFinishRate").val();
            var myChart = echarts.init(document.getElementById('divOrderFinishRate'));
            option = {
                tooltip: {
                    formatter: "{a} <br/>{b} : {c}%"
                },
                series: [
                {
                    name: '销售指标',
                    type: 'gauge',
                    detail: { formatter: '{value}%' },
                    data: [{ value: orderFinishRate, name: '完成率'}]
                }
            ]
            };
            myChart.setOption(option);

            //回款达成率
            var receiptFinishRate = $("#hfReceiptFinishRate").val();
            var myChartReceipt = echarts.init(document.getElementById('divReceiptFinishRate'));
            var optionReceipt = {
                tooltip: {
                    formatter: "{a} <br/>{b} : {c}%"
                },
                series: [
                {
                    name: '回款指标',
                    type: 'gauge',
                    detail: { formatter: '{value}%' },
                    data: [{ value: receiptFinishRate, name: '完成率'}]
                }
            ]
            };
            myChartReceipt.setOption(optionReceipt);

            //我的销售漏斗
            var mydata = eval("['初期沟通1', '立项评估', '需求分析', '方案认证', '商务谈判', '合同签订']");
            var mydata2 = eval("[{ value: 60, name: '初期沟通1' },{ value: 50, name: '立项评估' },{ value: 40, name: '需求分析' },{ value: 30, name: '方案认证' },{ value: 20, name: '商务谈判' },{ value: 10, name: '合同签订' }]");
            var myFunnelChart = echarts.init(document.getElementById('divMyFunnel'));
            optionMyFunnel = {

                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}万"
                },
                legend: {
                    data: mydata
                },
                calculable: true,
                series: [
        {
            name: '漏斗图',
            type: 'funnel',
            data: mydata2
        }
    ]
            };

            myFunnelChart.setOption(optionMyFunnel);

            //公司销售漏斗
            var allFunnelChart = echarts.init(document.getElementById('divAllFunnel'));
            optionAllFunnel = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}万"
                },
                legend: {
                    data: ['初期沟通', '立项评估', '需求分析', '方案认证', '商务谈判', '合同签订']
                },
                calculable: true,
                series: [
        {
            name: '',
            type: 'funnel',
            data: [
                { value: 60, name: '初期沟通' },
                { value: 50, name: '立项评估' },
                { value: 40, name: '需求分析' },
                { value: 30, name: '方案认证' },
                { value: 20, name: '商务谈判' },
                { value: 10, name: '合同签订' }
            ]
        }
    ]
            };
            allFunnelChart.setOption(optionAllFunnel);
        });


        $(function () {
            var w = $("#animation").width();
            if (w < 1145) {
                $("#animation2 ul li a").css("width", "83px");
                $("#animation2 ul li a img").css("width", "80px");
                $("#animation2 ul li a img").css("height", "80px");
                $("#animation2 ul li").css("padding", "5px 10px 10px 10px");

            }
        })


    </script>
</head>
<body  style="background-color:#E9E9E9">
    <form id="form1" runat="server">
    <div id="animation">
        <div id="animation2">
            <asp:Literal runat="server" ID="litLinks"></asp:Literal>
        </div>
    </div>
    <div class="div_model">
        <div style="margin: 0 auto;">
            <span id="Span2" style="height: 35px;">&nbsp;</span>
        </div>
        <table class="ctable" style="width: 300px; height: 100px; padding: 20px 10px 20px 10px;"
            align="center" border="0" cellpadding="1" cellspacing="2">
            <tr valign="Top">
                <td class="tdTitle" colspan="2">
                    &middot;<label id="lblTitle"></label>
                    <hr style="color: #79639D; size: 2px;" />
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    显示条数:
                </td>
                <td>
                    <asp:TextBox ID="txtNum" runat="Server" BorderStyle="groove"></asp:TextBox>
                </td>
            </tr>
            <tr valign="bottom">
                <td style="width: 73px" colspan="2" align="center">
                    <input id="Button2" type="button" class="button" value="保存" onclick="javascript:_savemodel()" />
                    <input id="esc" style="margin-top: 15px;" type="button" value="取消" class="button" />
                </td>
            </tr>
        </table>
    </div>
    <div id="fade" class="black_overlay" style="display: none; z-index: 99;">
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />
    <asp:HiddenField runat="server" ID="hfOrderFinishRate" Value="0" />
    <asp:HiddenField runat="server" ID="hfReceiptFinishRate" Value="0" />
    </form>
</body>
</html>
