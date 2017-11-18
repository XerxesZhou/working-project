<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="SmartSoft.Web.main" %>

<!doctype html>
<html>
<head id="Head1" runat="server">
    <link href="@Css/Myself.css" rel="stylesheet" type="text/css" />
    <script src="@Scripts/jquery.js" type="text/javascript"></script>
    <script src="@Scripts/webCalendar.js" type="text/javascript"></script>
    <link href="@Css/style.css" rel="stylesheet" type="text/css" />
    <title><asp:Literal ID="ltlCompanyName" runat="server"></asp:Literal></title>
    <%--判断浏览器内核--%>
    <style type="text/css">
        .into
        {
            color: Red !important;
        }
        
        .intoh3
        {
            background-color: #09CFF9 !important ;
        }
        #Label1
        {
            margin-top: 20px;
        }
        .second a
        {
            text-align: center;
            text-decoration: none;
            color: White;
        }
        
      .Google
        {
            width: 25px;
            height: 25px;
            float: left;
            margin-left: -18px;
            margin-top: 8px;
            margin-right: 12px;
            border: none;
        }
        
     .Google_head
        {
            height: 40px;
            line-height: 40px;
            padding-left: 40px;
            font-size: 16px;
            background-color: #0082E5;
            cursor: pointer;
            border-bottom: 1px solid #FFF;
            position: relative;
            margin: 0px;
            font-weight: bold;
            background: #0082E5 url(img/pro_left.png) center right no-repeat;
            color: #FFF;
        }
        
        .IE_topul
        {
            width: 100%;
            height: 100%;
            border-bottom: 1px solid #e0e0e0;
            margin: 0px 0px 0px 0px;
        }
        .IE
        {
            width: 25px;
            height: 25px;
            float: left;
            display:inline-block;
            margin-left: -18px !important;
            margin-top: 8px !important;
            margin-right: 12px !important;
            border: none;
      
        }
      .menu_body a
      {
       * font-size: 14px !improtant; 
      }  
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#firstpane .menu_body:eq(0)").show();
            $("#firstpane h3.menu_head").click(function () {
                $(this).addClass("current").next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });
            $("#secondpane .menu_body:eq(0)").show();
            $("#secondpane h3.menu_head").mouseover(function () {
                $(this).addClass("current").next("div.menu_body").slideDown(500).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".html_main").height($(window).height()-50);
            $(".html_main").width($(window).width() - 200);
        });
        $(window).resize(function () {//当浏览器窗口大小发生改变时重置页面大小

            $(".html_main").height($(window).height() - 60); //html_main的高是当前浏览器的高减去60px后自动获取的;
            alert
        });
        $(function () {
            $(".company_logo").click(function () {
                window.open("http://www.chuangjieyi.com");
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            getCurrentOperatorMessageCount();
            window.setInterval("getCurrentOperatorMessageCount();", 1000); // 更新
        });

        function getCurrentOperatorMessageCount() {
            var opeid = $("#hfCurrentOperatorID").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "AjaxMethods.asmx/GetOperatorMessageCount",
                data: "{opeID:" + opeid + "}",
                dataType: 'json',
                success: function (result) {
                    if (result == "0") {
                        $("#MessagePoint").hide();
                    }
                    else {
                        $("#MessagePoint").show();
                        $("#MessageCount").html(result);
                    }
                }
            });
        }

    </script>
    <script type="text/javascript">
        //注销
        $(function () {
            $(".li_close").click(function () {
                $(".sda").show();
                $(".black_overlay").show();
            })
        });
        //确定
        $(function () {
            $("#input_yes").click(function () {
                window.location.href = "Login.aspx";
            });
            //取消
            $("#input_no").click(function () {
                $(".sda").hide();
                $(".black_overlay").hide();
                return false;
            });
        })
    </script>
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

                $("h3 img").removeClass();
                $("h3 img").addClass("IE");

                //顶部ul样式
                $(".top_list ul").removeClass("..top_list ul");
                $(".top_list ul").addClass("IE_topul");

                //用户样式
                $(".li_user").removeClass(".li_user");
                $(".li_user").addClass("IE_user")

                //注销样式
                $(".li_close").removeClass(".li_close");
                $(".li_close").addClass("IE_close");


            }

            //火狐浏览器
            if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {

            
                $("h3 img").removeClass();
                $("h3 img").addClass("Google");
                $(".menu_head").removeClass(".menu_head");
                $(".menu_head").addClass("Google_head");
               
            }
            //谷歌浏览器
            if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {


                $("h3 img").removeClass();
                $("h3 img").addClass("Google");
                $(".menu_head").removeClass(".menu_head");
                $(".menu_head").addClass("Google_head");
            }
            if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
                return "Camino";
            }


            if (isMozilla = navigator.userAgent.indexOf("Gecko") > 0) {
                //                alert("123");
                //                $("#firstpane h3 img").css("margin-left", "-40px");
                //                $(".intoh3").css("background-color", "none !important");
                //                $("#firstpane h3").css("background-color", "#0082E5");
                //                $("#firstpane h3").css("color", "#FFF");
                return "Gecko";
            }
        })  
    </script>
    <style type="text/css">
        .company_logo
        {
            cursor: pointer;
        }
        #MessagePoint
        {
            background-color: #FD5959;
            width: 16px;
            height: 16px;
            border-radius: 15px;
            text-align: center;
            float:right;
            cursor: pointer;
            color: #F9F8F7;
            font-size: 12px;
        }
        #MessageCount
        {
             width:100%;
              float:right;
             line-height:16px;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#firstpane .menu_body a").click(function () {
                $("#firstpane .menu_body a").removeClass("into");
                $(this).addClass("into");
            });

        });
        $(function () {
            $("#firstpane h3").click(function () {
                $("#firstpane h3").removeClass("intoh3");
                $(this).addClass("intoh3");
            });

        });

        $(function () {
            $("#firstpane h3:first-child").addClass("intoh3");
        });
        //修改密码
        $(function () {
            $(".Esc").click(function () {
                $("#userself").hide();
                $(".black_overlay").hide();
                $(".gm").css("background-color", "white");
                return false;
            });
        });
    </script>
    <%--    个人信息管理--%>
    <style>
        *
        {
            margin: 0;
            padding: 0;
            list-style: none;
        }
        body
        {
            font-size: 12px;
            background: #F8F8F8;
            overflow: hidden;
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
        
        
        #firstpane .menu_body a:hover
        {
            background-color: #eee;
        }
        #firstpane h3:hover
        {
            background-color: #09CFF9;
        }
    </style>
    <script>
        $(function () {
            if ($("#li_username").html() === "admin") {
                $("#li_username").css("color", "red");
            }

            $(".gm").click(function () {
                $(".gm").css("background-color", "#eee");
                $(".gr").css("background-color", "white");
                $(".cpw").show();
                $(".xx").hide();
            });
            $(".gr").click(function () {
                $(".gr").css("background-color", "#eee");
                $(".gm").css("background-color", "white");
                $(".cpw").hide();
                $(".xx").show();
            });
        });
    </script>
    <style>
        .cpw
        {
            display: none;
            width: auto;
            height: auto;
            background: #fff;
            border-radius: 5px;
            border: 1px solid #eee;
        }
        .xx
        {
            display: none;
            width: auto;
            height: auto;
            background: #fff;
            border-radius: 5px;
        }
        #userself
        {
            display: none;
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
    </style>
    <script type="text/javascript" language="javascript">
        function iFrameHeight() {
            var ifm = document.getElementById("iframepage");
            var subWeb = document.frames ? document.frames["iframepage"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = subWeb.body.scrollHeight;
                ifm.width = subWeb.body.scrollWidth;
            }
        }   
    </script>
    <style>
    ::-webkit-scrollbar{width:3px;}
    ::-webkit-scrollbar-track{background-color:#ddd;}
    ::-webkit-scrollbar-thumb{background-color:#0082E5;}
  </style>

</head>
<body style="font-family: '微软雅黑'">
    <form id="form1" runat="server">
    <div class="iwk_col1" style="background-color: #0082E5;">
        <div class="company_logo" style="border-bottom: 1px solid #EEE">
            <a>
                <img alt="创捷易工作平台" src="../@images/logo2.png" title="创捷易官网" /></a>
        </div>
        <div id="firstpane" class="menu_list" >
            <asp:Literal ID="litMenu" runat="server"></asp:Literal>
        </div>
    </div>
    <!-- 左菜单 end -->
    <!-- 顶部 start -->
    <div class="top_list" id="top_list">
        <ul>
            <li><a title="创捷易工作平台" id="cjy_name" href="Data/CustomerList.aspx" target="rform"
                style="cursor: pointer; text-decoration: none; color: #018BE5; margin-top: -3px;
                padding-bottom: 3px;"><asp:Label ID="lbCompanyName" runat="server"></asp:Label>工作平台</a></li>
            <li><a title="桌面" class="li_list" href="Common/NewDesk.aspx" target="rform" >
                桌面</a></li> 
            <li style=" position:relative"><a title="通知" class="li_list" href="Common/MessageList.aspx" target="rform" >
                通知</a> 
                <span id="MessagePoint" style="display: none">
                    <asp:Label ID="MessageCount" Text="" runat="server"></asp:Label>
                </span>
            </li>
            <li id="KnowledgeList"><a title="修改密码" href="Common/KnowledgeList.aspx" class="li_list"
                target="rform" >知识</a> </li>
            <li id="Li1"><a title="帮助中心" target="_blank" href="../help/HelpCenter.htm" class="li_list"
                target="rform" >帮助中心</a> </li>
            <li><span class="li_close">
                <img src="../@images/img/zhuxiao.png" class="li_img" />
                <a title="注销" class="aaa" href="#" style="color: #018BE5;">注销</a> </span></li>
            <li><span class="li_user"><a href="Common/MyDeskSetting.aspx?uid=" target="rform"
                style="text-decoration: none; color: #018BE5;">
                <img runat="server" id="face" src="@images/img/denglu.png" class="li_img" />
                <asp:Label ID="li_username" runat="server" Text=""></asp:Label></a> </span></li>
        </ul>
        <%--<div id="MessagePoint" style="display: none">
            <asp:Label ID="MessageCount" Text="" runat="server"></asp:Label>
        </div>--%>
        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
        <%--个人资料、修改密码--%>
        <table id="userself" class="TablePanel">
            <tr>
                <%--  左边导航栏div--%>
                <td>
                    <div class="lanrenzhijia">
                        <ul>
                            <li class="gr"><a>个人设置</a></li>
                            <li class="gm" style="margin-top: 40px;"><a>修改密码</a></li>
                        </ul>
                    </div>
                </td>
                <%--修改密码--%>
                <td>
                    <div class="cpw">
                        <table>
                            <tr style="height: 40px;">
                                <td colspan="2" style="text-align: center;">
                                    修改密码
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    旧密码：
                                </td>
                                <td style="height: 40px;">
                                    <asp:TextBox ID="tb_oldpassword" TextMode="password" runat="Server" BorderStyle="groove"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_oldpassword"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    新密码：
                                </td>
                                <td style="height: 40px;">
                                    <asp:TextBox ID="tb_newpassword1" runat="Server" TextMode="password" BorderStyle="groove"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_oldpassword"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="height: 40px; text-align: center;">
                                <td style="width: 146px">
                                    确认新密码：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_newpassword2" runat="Server" TextMode="password" BorderStyle="Groove"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_newpassword2"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="height: 40px; text-align: center;">
                                <td colspan="2" style="height: 40px;">
                                    <asp:Button ID="lbt_save" runat="server" Text="保存" OnClick="lbt_save_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input class="Esc" type="button" value="取消" style="cursor: pointer; width: 50px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <%--个人信息设置--%>
                <td>
                    <div class="xx">
                        <%--头像，合同额，收款额，入职日期--%>
                        <table>
                            <tr style="height: 40px;">
                                <td colspan="2" style="text-align: center;">
                                    个人信息设置
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    头像：
                                </td>
                                <td style="height: 40px;">
                                    <img runat="server" id="face2" src="@images/img/default-avatar.png" style="float: left;
                                        width: 15px; height: 15px;" />
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="142px" />
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    手机号：
                                </td>
                                <td style="height: 40px;">
                                    <asp:TextBox ID="txtMobile" runat="Server" BorderStyle="groove"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    邮箱号：
                                </td>
                                <td style="height: 40px;">
                                    <asp:TextBox ID="txtEmail" runat="Server" BorderStyle="groove"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td style="width: 88px; height: 40px;">
                                    合同额：
                                </td>
                                <td style="height: 40px;">
                                    <asp:TextBox ID="txtOrderAmount" runat="Server" CssClass="inputNumber" BorderStyle="groove"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 40px; text-align: center;">
                                <td style="width: 146px">
                                    收款额：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReceiptAmount" runat="Server" CssClass="inputNumber" BorderStyle="Groove"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 40px; text-align: center;">
                                <td style="width: 146px">
                                    入职日期：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEnterDate" CssClass="inputDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 40px; text-align: center;">
                                <td colspan="2" style="height: 40px;">
                                    <asp:Button ID="lbt_saveOperator" class="lba_save" runat="server" OnClick="lbt_saveOperator_Click"
                                        Text="保存" CausesValidation="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input class="Esc" type="button" value="取消" style="cursor: pointer; width: 50px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
        </table>
        <%--注销--%>
        <div class="sda">
            <table>
                <tr class="T1">
                    <td>
                        注销账户
                    </td>
                </tr>
                <tr class="T2">
                    <td>
                        <br />
                        <span>确认退出系统？</span>
                        <br />
                    </td>
                </tr>
                <tr class="T3">
                    <td>
                        <input id="input_yes" type="button" value="确定" />&nbsp;&nbsp;
                        <input id="input_no" type="button" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="fade" class="black_overlay" style="display: none; z-index: 99;">
        </div>
        <div id="bgimg">
            <span></span>
        </div>
    </div>
    <div class="html_main">
        <iframe name="rform" id="iframepage" frameborder="0" scrolling="auto" marginheight="0px;"
            marginwidth="0px" width="100%" height="100%" bordercolor="#ffffFF" src="../Common/NewDesk.aspx"
            border="0"></iframe>
    </div>
    </form>
</body>
</html>
