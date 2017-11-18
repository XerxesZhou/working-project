<%@ Page Language="C#" AutoEventWireup="true" Inherits="Main_MyDeskSetting" CodeBehind="MyDeskSetting.aspx.cs" %>

<html>

<head>
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <%--    个人信息管理--%>
    <%--个人信息，修改密码样式--%>
    <style>
        *
        {
          
            margin: 0;
            padding: 0;
            list-style: none;
            font-family: Helvetica Neue,Helvetica,Arial,PingFang SC,Hiragino Sans GB,WenQuanYi Micro Hei,Microsoft Yahei,sans-serif;
        }

        body
        {
            font-size: 14px;
            background: #F8F8F8;
            overflow: hidden;
            background-color: White;
            font-family: 'Aria,Microsoft Sans Serif';
        }
        td
        {
             font-size:14px;
             color: #000 !important;
            }
        .lanrenzhijia
        {
            width: 100px;
            height: 198px;
            background: #fff;
            cursor: pointer;
            padding: 50px 0;
            display: none;
        }
        .lanrenzhijia ul li
        {
            z-index: 2;
            height: 40px;
            position: absolute;
        }
        .lanrenzhijia li a
        {
            height: 40px;
            line-height: 40px;
            font-size: 10px;
            border-bottom: 1px solid #F8F8F8;
            display: block;
            margin: 0px 15px;
            text-align: center;
            text-decoration: none;
        }
        .lanrenzhijia li:hover a
        {
            text-decoration: none;
        }
        /*.lanrenzhijia li.on a{color:#FF5F3E;}*/
        .lanrenzhijia .hover
        {
            width: 100px;
            height: 40px;
            position: absolute;
            left: -5px;
            top: 50px;
            background: #F8F8F8;
            border-left: 5px solid #FF5F3E;
            z-index: 1;
        }
        
        
        .btn-success
        {
            width: 90px;
            color: #fff;
            background-color: #5cb85c;
            border-color: #4cae4c;
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }
        
        
        
        .cpw
        {
            width: 100%;
            height: 100%;
            background: #fff;
            display:block;
        }
        .xx
        {

            display: none;
            width: 100%;
            height: 100%;
            background: #fff;
        }
        .mydesk
        {
            font-size: 100%;
            width: 100%;
            display: none;
            height: 100%;
            background: #fff;
           

        }
        
        
        #userself
        {
            width: auto;
            height: 310px;
            text-align: center;
            border: 1px solid #eee;
            position: absolute;
            top: 25%;
            left: 35%;
            background-color: #fff;
            z-index: 1002;
        }
        
        #lbt_save
        {
            width: 25px;
            height: 25px;
        }
        #str
        {
               text-align: center;
        }
        
        .div_DeskSet
        {
            width: 100%;
            height: 8%;
            border-bottom: 1px solid blue;
        }
        .div_DeskSet ul
        {
        }
        .div_DeskSet ul li
        {
            list-style-type: none;
            float: right;
            height: 100%;
            margin-left: 8%;
            cursor: pointer;
            border-radius: 5px;
            position: relative;
            right: 4%;
        }
        
        .active
        {
            background-color: #09CFF9;
            color: White;
        }
        .div_DeskSet ul li a
        {
            font-size: 115%;
            display: block;
            margin-top: 16%;
            padding: 7px;
            border-radius: 5px;
        }
        
        .D_save
        {
            display: block;
            margin-top: 50%;
        }
        .D_close
        {
            display: block;
            margin-top: 23%;
        }
        .ipassword
        {
            height: 1.8em;
            width: 94%;
            height: 34px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
        }
        
        #txtOrderAmount
        {
            height: 1.8em;
            width: 94%;
            height: 34px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
        }
        
        #txtReceiptAmount
        {
            height: 1.8em;
            width: 94%;
            height: 34px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
        }
        #txtEnterDate
        {
            height: 1.5em;
            width: 94%;
            height: 34px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
        }
        
        #ListBox1, #ListBox2
        {
            text-align: center;
        }
        .IE_div_DeskSet
        {
            width: 100%;
            height: 49px;
            border-bottom: 1px solid blue;
        }


    </style>
    <script type="text/javascript">
        $(function () {
            $("#D_Password").addClass("active");
        
        })

        $(function () {
            $("#D_Desk").parent().click(function () {
                $(".xx").hide();
                $(".cpw").hide();
                $(".mydesk").show();
                $("#mydesk").show();
                $("#D_Desk").addClass("active");
                $("#D_Personal").removeClass("active");
                $("#D_Password").removeClass("active");
                $("#str").show();

            });
            $("#D_Personal").parent().click(function () {
                $(".xx").show();
                $(".cpw").hide();
                $(".mydesk").hide();
                $("#D_Personal").addClass("active");
                $("#D_Password").removeClass("active");
                $("#D_Desk").removeClass("active");
                $("#str").hide();
            });
            $("#D_Password").parent().click(function () {
                $(".xx").hide();
                $(".mydesk").hide();
                $(".cpw").show();
                $("#D_Password").addClass("active");
                $("#D_Personal").removeClass("active");
                $("#D_Desk").removeClass("active");
                $("#str").hide();

            })

        });
        
    </script>
    <%-- 取消修改密码--%>
    <script type="text/javascript">
        //修改密码--取消
        $(function () {

            $(".Esc").click(function () {
                $("#userself").hide();
                $(".black_overlay").hide();
                $(".gm").css("background-color", "white");
                return false;
            });
        });
    
    
    </script>
    <%--end--%>

     <script type="text/javascript">
         $(function () {

             if (navigator.appName == "Microsoft Internet Explorer") {
                 $("#firstpane h3 img").css("margin-left", "-40px");
                 $(".intoh3").css("background-color", "none !important");
                 $("#firstpane h3").css("background-color", "#0082E5");
                 $("#firstpane h3").css("color", "#FFF");
                 $("#firstpane h3 img").css("margin-top", "10px");
             }


             //IE浏览器
             if (navigator.userAgent.indexOf("MSIE") > 0) {


                 $("#div_DeskSet").css("height", "49px");
                


             }

             //火狐浏览器
             if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                 $("#div_DeskSet").css("height", "49px");
             }
             //谷歌浏览器
             if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {

                 $("#div_DeskSet").css("height", "49px");
               
             }
             if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {

             }


             if (isMozilla = navigator.userAgent.indexOf("Gecko") > 0) {

             }
         })  
    </script>
    <title>企业信息化管理平台</title>
   
</head>
<body style=" text-align:center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <%--导航栏--%>
    <div class="div_DeskSet" id="div_DeskSet" style="text-align: center; overflow:hidden;">
        <ul>
            <li><a id="D_Personal" class="D_DeskSet">个人信息</a></li>
            <li><a id="D_Password" class="D_DeskSet">修改密码</a></li>
            <li style="float: right;"><span class="D_close">
                <img class="HerCss" onclick="javascript:window.history.go(-1)" title="返回" style="float: right;
                    margin-right: 15px; cursor: pointer; width: 1.5em; height: 1.5em;" src="../images/img_newclose.png" /></span></li>
        </ul>
    </div>
    <%--修改密码--%>
    <div class="cpw">
       
        <table id="cpw_table" style="width: 350px; margin:auto auto; margin-top:50px;">
          <tr style="height: 40px;">
                <td colspan="2" style="text-align: center;">
                </td>
            </tr>
            <tr style="line-height: 3em;">
                <td style="text-align: right; color: #A0A0A0;">
                    登录帐号：
                </td>
                <td style="height: 40px; color: #A0A0A0;" id="uname">
                    <asp:Label ID="cpw_uname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="line-height: 3em;">
                <td style="text-align: right; height: 40px; color: #A0A0A0;">
                    当前密码：
                </td>
                <td style="height: 40px;">
                    <asp:TextBox ID="tb_oldpassword" TextMode="password" placeholder="请输入您当前的密码" runat="Server"
                        CssClass="ipassword"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_oldpassword"
                        Display="Dynamic" ValidationGroup="password" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="line-height: 3em;">
                <td style="text-align: right; height: 40px; color: #A0A0A0;">
                    新密码：
                </td>
                <td style="height: 40px;">
                    <asp:TextBox ID="tb_newpassword1" runat="Server" TextMode="password" placeholder="请输入密码"
                        CssClass="ipassword"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_oldpassword"
                        Display="Dynamic" ValidationGroup="password" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="line-height: 3em;">
                <td style="width: 146px; text-align: right; color: #A0A0A0;">
                    确认新密码：
                </td>
                <td>
                    <asp:TextBox ID="tb_newpassword2" runat="Server" TextMode="password" placeholder="请再次输入密码"
                        CssClass="ipassword"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_newpassword2"
                        Display="Dynamic" ValidationGroup="password" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 40px; text-align: center;">
                <td>
                </td>
                <td style="height: 40px;">
                    <asp:Button Text="保存" runat="server" ValidationGroup="password" OnClick="lbt_save_Click"
                        CssClass="btn-success" />
                </td>
            </tr>
        </table>
    </div>
    <%--个人信息设置--%>
    <div class="xx">
        <%--头像，合同额，收款额，入职日期--%>
        <table style="width: 350px; margin: 0px auto;margin-top: 50px;">
            <tr style="height: 40px;">
                <td colspan="2" style="text-align: center;">
                </td>
            </tr>
            <tr style="text-align: center;">
                <td style="text-align: right; color: #A0A0A0;">
                    当前头像：
                </td>
                <td style="height: 40px; text-align: center">
                    <img runat="server" id="face2" src="" style="float: left; width: 24px; height: 24px;" />
                    <span id="zhanghao" style="color: #A0A0A0;">
                        <asp:Label ID="xx_uname" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; color: #A0A0A0;">
                    选择更改头像：
                </td>
                <td style="height: 40px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="152px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; color: #A0A0A0;">
                    手机号：
                </td>
                <td style="height: 40px;">
                    <asp:TextBox ID="txtMobile" runat="Server" placeholder="请输入手机号（必须为11位）" CssClass="ipassword"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                        Display="Dynamic"  ErrorMessage="*" ValidationGroup="vgPersonal"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; color: #A0A0A0;">
                    邮箱号：
                </td>
                <td style="height: 40px;">
                    <asp:TextBox ID="txtEmail" runat="Server" placeholder="请输入正确的邮箱地址" CssClass="ipassword"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; color: #A0A0A0;">
                    合同额：
                </td>
                <td style="height: 40px;">
                    <asp:TextBox ID="txtOrderAmount" runat="Server" CssClass="inputNumber"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 40px;">
                <td style="text-align: right; color: #A0A0A0;">
                    收款额：
                </td>
                <td>
                    <asp:TextBox ID="txtReceiptAmount" runat="Server" CssClass="inputNumber"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 40px;">
                <td style="text-align: right; color: #A0A0A0;">
                    入职日期：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEnterDate"  CssClass="inputDate laydate-icon"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 40px; text-align: center;">
                <td colspan="2" style="height: 40px;">
                    <asp:Button ID="lbt_saveOperator" Text="保存" runat="server" OnClick="lbt_saveOperator_Click"
                        CssClass="btn-success" ValidationGroup="vgPersonal"/>
                    <%--   <asp:ImageButton ID=""   ImageUrl="../images/img_160.gif" runat="server" title="保存"  OnClick="lbt_saveOperator_Click"   />--%>
                </td>
            </tr>
        </table>
    </div>

    </form>
</body>
</html>
