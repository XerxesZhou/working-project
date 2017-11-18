<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desk.aspx.cs" Inherits="SmartSoft.Web.Desk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1.0">
    <title>hello world</title>
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <script src="@Scripts/jquery-2.2.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="@Scripts/jquery.mobile-1.4.5.min.js" type="text/javascript" charset="utf-8"></script>
    <link href="@Css/jquery.mobile-1.4.5.min.css" rel="stylesheet" type="text/css" />
    <style>
        .ui-block-a, .ui-block-b, .ui-block-c
        {
            height: 150px;
            font-weight: bold;
            text-align: center;
            padding: 30px;
            background-repeat: no-repeat;
        }
        img
        {
            max-width: 80px;
            max-height: 80px;
        }
        a
        {
            font-size: 12px;
            text-decoration: none;
            text-align: center;
            display: block;
        }
        ul li
        {
            float: left;
            text-align: center;
            float: left;
            margin-right: 10px;
            display: inline-block; /*让文字和图片各占一行*/
        }
    </style>
</head>
<body>
    <!--data-theme="b"-->
    <div data-role="page">
        <div data-role="header" data-add-back-btn="true">
            <h1>
                欢迎来到首页！</h1>
        </div>
        <div data-role="content">
            <p>
                三列样式布局：</p>
            <ul class="ui-grid-b">
                <li class="ui-block-a">
                    <img src="../@images/img/yewutongji.png" title="客户管理" />
                    <a href="#" title="客户管理">客户管理</a> </li>
                <li class="ui-block-b">
                    <img src="../@images/img/yewutongji.png" title="客户统计" />
                    <a href="#" title="客户统计">客户统计</a></li>
                <li class="ui-block-c">
                    <img src="../@images/img/yewutongji.png" title="业务统计" />
                    <a href="#" title="业务统计">业务统计</a></li>
                <li class="ui-block-a">
                    <img src="../@images/img/yewutongji.png" title="业务管理" /><a href="#" title="业务管理">业务管理</a></li>
                <li class="ui-block-b">
                    <img src="../@images/img/yewutongji.png" title="计划管理" />
                    <a href="#" title="计划管理">计划管理</a></li>
                <li class="ui-block-c">
                    <img src="../@images/img/yewutongji.png" title="收款管理" />
                    <a href="#" title="收款管理">收款管理</a></li>
                <li class="ui-block-a">
                    <img src="../@images/img/yewutongji.png" title="收款统计" />
                    <a href="#" title="收款统计">收款统计</a></li>
                <li class="ui-block-b">
                    <img src="../@images/img/yewutongji.png" title=" 业务统计" />
                    <a title="业务统计">业务统计</a></li>
            </ul>
        </div>
        <div data-role="footer">
            <h1>
                Copyright:@raoqi</h1>
        </div>
    </div>
</body>
</html>
