<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Login.aspx.cs" Inherits="SmartSoft.Web.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no">
    <meta http-equiv="Cache-Control" content="no-transform">
    <%--<title>登录 - 互联网工作平台 </title>--%>
    <title><asp:Literal runat="server" ID="ltlCompanyName"></asp:Literal></title>
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
    .wraper{ margin:0 auto; padding:95px 15px 20px; min-width:290px; max-width:440px; *min-width:440px;}
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
    a.btn-submit{ background:#29e; color:#fff; width:100%; height:56px; line-height:56px; font-size:18px; text-align:center; display:inline-block; border-radius:2px;}
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

    <script language="javascript" type="text/javascript">
    	if (top.location != self.location)
		top.location = self.location;

        function check()
        {
            var username = document.getElementById('username').value;
            var password = document.getElementById('password').value;
            
            if(username == null || username == '')
            {
                alert('请输入用户名!');
                return false;
            }
            
            if(password == null || password == '')
            {
                alert('请输入密码!');
                return false;
            }

            document.getElementById('tb_username').value = username;
            document.getElementById('tb_password').value = password;
            document.getElementById('btn_Ok').click();
            return true;
        }
        
        function OpenMainWindow()
        {
            document.location = "main.aspx";
        }

        function handleKeyPress() {
            if (event.keyCode == 13) {
                check();
            }
        }
    </script>

   

</head>
<body class="bodybg" onkeypress="handleKeyPress();" style="display: block;">
    <form id="form1" runat="server">
        <div class="login_wrap">
            <div class="wraper">
            <div class="logo" style="font-size:40px; color:#2299EE;">
                <asp:Label ID="lbCompanyName" runat="server"></asp:Label>工作平台
            </div>
            <div class="inputwrap login-input">
                <label class="lable-login">
                    <i class="icon-user"></i>
                </label>
                <input name="username" tabindex="1" id="username" type="text" placeholder="账号(手机/邮箱/登录帐号)"/>
            </div>
            <br />
            <div>
                <div class="inputwrap login-input">
                    <label class="lable-login">
                        <i class="icon-lock"></i>
                    </label>
                    <input name="password" tabindex="2" id="password" type="password" placeholder="密码(20位以内，区分大小写)"/>
                </div>
            </div>
            <div>
                <asp:Label runat="server" ID="lblMsg" style="font: 12px; color:Red;"></asp:Label>
            </div>
            <br />
            <div class="submitwrap">
                <a class="btn-submit" href="javascript:void(0);" onclick="check();">登 录</a>
            </div>
        </div>
        </div>
        <div style="display:none">
            <asp:TextBox ID="tb_username" CssClass="txtstyle"  runat="server" BorderStyle="groove" Width="180"></asp:TextBox>
            <asp:TextBox ID="tb_password" CssClass="txtstyle" runat="Server" BorderStyle="Groove" TextMode="password" Width="180"></asp:TextBox>
            <asp:Button ID="btn_Ok" runat="Server" Text="登 入" OnClick="btn_Ok_Click" TabIndex="0" BorderStyle="NotSet" />
        </div>
    </form>

    <script language="javascript" type="text/javascript">
        var tu = document.getElementById('username');
        if (tu != null)
        {
            tu.focus();
        }
    </script>
</body>
</html>



