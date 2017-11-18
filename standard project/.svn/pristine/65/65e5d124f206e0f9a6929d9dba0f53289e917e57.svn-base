<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SmartSoft.Web.Register" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no">
    <meta http-equiv="Cache-Control" content="no-transform">
    <title>注册 - 创捷易互联网工作平台 </title>
    <link rel="shortcut icon" href="favicon.ico" />
    <style type="text/css">
    html,body{ margin:0; padding:0; color:#333; font:14px/1.5 "Helvetica Neue","Hiragino Sans GB","WenQuanYi Micro Hei","Microsoft Yahei",sans-serif;}
    body.bodybg{ background:#f5f6f7;}
    form{ margin:0;}
    a{ text-decoration:none; color:#29e; outline:none; transition:background-color 0.3s ease 0s, color 0.3s ease 0s;}
    a:hover{ text-decoration:none;}
    a.inherit{ color:inherit;}
    input{ outline:none; font-family:inherit; background:none;}
    input::-moz-placeholder{color:#aaa;}
    input:-moz-placeholder{color:#aaa;}
    input:-ms-input-placeholder{color:#aaa;}
    input::-ms-clear{display:none;}
    input[type="password"]::-ms-reveal{display:none;}


    .wraper{ margin:0 auto; padding:55px 15px 20px; min-width:290px; max-width:440px; *min-width:440px;}
    body.client-browser .wraper, body.client.notop .wraper{ padding:43px 15px 20px;}
    .wraper.neterror{ padding-top:130px;}
    body.client-browser .wraper.neterror{ padding-top:78px;}
    .logo{ margin:0 0 30px; text-align:center; display:block;}
    .logo .text{ font-size:30px;}
    .inputwrap{ background:#fff; margin:0 0 12px; height:48px; border:1px solid #d9d9d9; position:relative; *z-index:20;}
    .inputwrap.focus{ border:1px solid #29e;}
    .inputwrap input[type="text"], .inputwrap input[type="password"], .inputwrap input[type="number"]{ padding:15px 15px 15px 45px; width:100%; *width:373px; height:100%; *height:17px; line-height:18px; font-size:14px; border:0 none; box-sizing:border-box;}
    .lable-login{ position:absolute; top:12px; left:17px;}
    .submitwrap{ margin:0 0 12px; padding:10px 0 0; position:relative;}
    a.btn-submit{ background:#29e; color:#fff; width:100%; height:100%; line-height:48px; font-size:18px; text-align:center; display:inline-block; border-radius:2px;}
    a.btn-submit:hover{ background:#00a8ff;}
    a.btn-submit:active{ background:#07c;}

    [class^="icon-"], [class*="icon-"]{ background:url(/@images/icons.png) no-repeat; display:inline-block; vertical-align:middle;}
    .icon-user{ background-position:0 0; width:14px; height:14px;}
    .icon-lock{ background-position:-20px 0; width:13px; height:16px;}
    /* retina兼容 */
    @media only screen and (-webkit-min-device-pixel-ratio :1.5),(min-resolution:120dpi),(-ms-high-contrast:active),(-ms-high-contrast:none){
    [class^="icon-"], [class*="icon-"]{ background:url(/@images/icons.png) no-repeat; background-size:600px 200px;}
    .icon-user{ background-position:0 0; width:14px; height:14px;}
    .icon-lock{ background-position:-20px 0; width:13px; height:16px;}
    }
    </style>
    <script src="@Scripts/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        if (top.location != self.location)
            top.location = self.location;

        function getCode() {
            

            var com = $("#companyname").val();
            if (com == null || com == '') {
                alert('请输入公司名称!');
                return false;
            }

            var contactor = $("#contactor").val();
            if (contactor == null || contactor == '') {
                alert('请输入联系人名称!');
                return false;
            }

            var m = $("#username").val();
            if (m == null || m == '' || m.length != 11) {
                alert('请输入正确手机号!');
                return false;
            }

            m = m + ":" + com + ":" + contactor;

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "AjaxMethods.asmx/SendCode",
                data: "{mobile:'" + m + "'}",
                dataType: 'json',
                success: function (result) {
                    alert(result);
                }
            });
        }

        function check() {

            var com = $("#companyname").val();
            if (com == null || com == '') {
                alert('请输入公司名称!');
                return false;
            }

            var contactor = $("#contactor").val();
            if (contactor == null || contactor == '') {
                alert('请输入联系人名称!');
                return false;
            }


            var m = $("#username").val();
            if (m == null || m == '') {
                alert('请输入手机号!');
                return false;
            }

            var c = $("#code").val();
            if (c == null || c == '') {
                alert('请输入验证码!');
                return false;
            }

            document.getElementById('tb_username').value = m + ":" + com + ":" + contactor;
            document.getElementById('tb_code').value = c;
            document.getElementById('btn_Ok').click();
            return true;
        }

        function OpenMainWindow() {
            alert("注册成功！用户名和密码都是您手机号。请下载“钉钉”手机应用，用此手机号码注册后即可体验创捷易手机版。");
            document.location = "main.aspx";
        }
    </script>

   

</head>
<body class="bodybg" onkeypress="handleKeyPress();" style="display: block;">
    <form id="form1" runat="server">
        <div class="login_wrap">
            <div class="wraper">
            <div class="logo" style="font-size:40px; color:#2299EE;">
                创捷易工作平台注册
            </div>
             <div class="inputwrap login-input">
                <label class="lable-login">
                    <i class="icon-user"></i>
                </label>
                <input name="companyname" tabindex="1" id="companyname" type="text" runat="server" placeholder="请输入公司名称"/>
            </div>
            <br />

             <div class="inputwrap login-input">
                <label class="lable-login">
                    <i class="icon-user"></i>
                </label>
                <input name="contactor" tabindex="2" id="contactor" type="text" runat="server" placeholder="请输入联系人姓名"/>
            </div>
            <br />

            <div class="inputwrap login-input">
                <label class="lable-login">
                    <i class="icon-user"></i>
                </label>
                <input name="username" tabindex="3" id="username" type="text" runat="server" placeholder="请输入手机号"/>
            </div>
            <br />
            <div style="width:100%">
                <div class="inputwrap login-input">
                    <label class="lable-login">
                        <i class="icon-lock"></i>
                    </label>
                    <input name="code" tabindex="4" id="code" type="text" style="width:60%;float:left;" placeholder="请输入验证码"/>
                    <a class="btn-submit" href="javascript:void(0);" onclick="getCode();" style="width:40%;height:100%; background:rgba(238, 62, 34, 0.75)">获取验证码</a>
                </div>
            </div>
            <div>
                <asp:Label runat="server" ID="lblMsg" style="font: 12px; color:Red;"></asp:Label>
            </div>
            <br />
            <div class="submitwrap">
                <a class="btn-submit" href="javascript:void(0);" onclick="check();">注 册</a>
            </div>
        </div>
        </div>
        <div style="display:none">
            <asp:TextBox ID="tb_username" runat="server"></asp:TextBox>
            <asp:TextBox ID="tb_code" runat="Server" TextMode="password"></asp:TextBox>
            <asp:Button ID="btn_Ok" runat="Server" Text="注 册" OnClick="btn_Ok_Click" TabIndex="0"/>
        </div>
    </form>

    <script language="javascript" type="text/javascript">
        var tu = document.getElementById('companyname');
        if (tu != null) {
            tu.focus();
        }
    </script>
</body>
</html>




