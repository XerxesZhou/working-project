<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="SmartSoft.MobileWeb.FeedBack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>反馈建议</title>
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css"/>
    <script type="text/javascript" src="../scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery.mobile-1.3.2.min.js"></script> 
    <!--微信浏览器取消缓存的方法 end-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
 
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function SubmitFeedback() {
            var inputcontent = document.getElementById("inputcontent").value;
            var inputEmail = "";
            var inputphone = "";
            var operatorid = document.getElementById("HiddenField1").value;

            if (operatorid == "") {
                dd.device.notification.toast({
                    icon: 'error', //icon样式，有success和error，默认为空 0.0.2
                    text: "用户为空！", //提示信息
                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                    delay: 0, //延迟显示，单位秒，默认0
                    onSuccess: function (result) { }
                });
                return false;
            }

            if (inputcontent == "") {
                dd.device.notification.toast({
                    icon: 'error', //icon样式，有success和error，默认为空 0.0.2
                    text: "内容不能为空！", //提示信息
                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                    delay: 0, //延迟显示，单位秒，默认0
                    onSuccess: function (result) { }
                });
                return false;
            }

         

            var fdData = "{fdOperatorID:'" + operatorid + "',fdContent:'" + inputcontent + "',fdOperatorEmail:'" + inputEmail + "',fdOperatorPhone:'" + inputphone + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SaveHelpCenterFeedBackInfo",
                data: fdData,
                dataType: 'json',
                success: function (result) {
                    if (result.d + 0 > 0) {
                        dd.device.notification.toast({
                            icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                            text: "感谢您的反馈，我们将第一时间为您处理。", //提示信息
                            duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                            delay: 0, //延迟显示，单位秒，默认0
                            onSuccess: function (result) { }
                        });
                    }
                }
            })
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="FeedBack">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <p class="question">
            反馈建议：</p>
        <div style="height: 220px; margin-top: 10px; margin-bottom: 10px">
            <textarea class="quetext" style="height: 300px;width: 100%; outline:none; text-align:left;" runat="server" id="inputcontent" placeholder="您可以将使用中的问题，或对功能的需求反馈给我们~"></textarea>
        </div>
        <div>
              <a class="SavaBtn" onclick="javascript:SubmitFeedback();" data-role="none">立即提交</a>
        </div>
        <p style="font-size: 14px; text-align: center; padding-top: 60px;">
            创捷易CRM咨询电话 <a href="tel:0755-33160172">0755-33160172</a></p>
    </div>
    </form>
</body>
</html>
