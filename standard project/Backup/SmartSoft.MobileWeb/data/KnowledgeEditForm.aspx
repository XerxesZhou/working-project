<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.data.KnowledgeEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.8.3.min.js"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js"></script>
    <script src="../scripts/BaseHelper.js"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>

    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>

    <style>
        .cssHeader
        {
            text-align: center;
            color: #333333;
        }
        .cssNav
        {
            /*width: 29%; */
            float: left;
        }
        .cssNav a
        {
            float: left;
        }
        .cssMainPanel
        {
            font-size: small;
            margin-left: 3px;
            margin-right: 3px;
            margin-top: 0px; /*width: 65%; 
            
             float: right;*/
        }
        .cssMainPanel label
        {
            font-weight: bold;
        }
        .cssMainPanel div label
        {
            font-weight: normal;
        }
        .cssMainPanel div select
        {
            font-weight: normal;
        }
        .cssMainPanel div input
        {
            font-weight: normal;
        }
        .cssMainPanel a
        {
            margin-top: 0px;
            margin-bottom: 20px;
        }
        .cssMainPanel li
        {
            margin: 8px;
        }
        .cssMainPanel li a
        {
            margin-bottom: 0px;
        }
    </style>
    <style>
        .cssNav
        {
            width: 100%;
        }
        
        .cssNav a
        {
            width: 25%;
            font-size: 14px !important;
            box-sizing: border-box;
        }
        
        .cssMainPanel table
        {
            font-size: 14px;
            line-height: 2em;
            width: 100%;
            font-family: @微软雅黑;
            border: 0;
        }
        .cssMainPanel table tr th
        {
            text-align: right;
            width: 35%;
            color: Gray;
            border-bottom: 1px solid #CCC;
        }
        
        .cssMainPanel table tr td
        {
            text-align: left;
            border-bottom: 1px solid #CCC;
        }
        
        .OperatorButton div
        {
            width: 100%;
        }
        
        .OperatorButton div a
        {
            box-sizing: border-box;
            width: 50%;
        }
        
        textarea
        {
            border: 0;
        }
        
        .Address
        {
            display: none !important;
        }
        
        select
        {
            width: 100%;
            height: 35px;
            border: 0;
            outline: none;
            background: url("../images/combo_arrow.png") no-repeat 99%;
            font-size: 14px;
        }
        
        #divNavigation
        {
            width: 100%;
            height: 33px;
            overflow: hidden;
            position: relative;
        }
        
        #divNavigation ul
        {
            position: absolute;
            height: 33px;
        }
        
        #divNavigation ul li
        {
            float: left;
            width: 25%;
            height: 33px;
        }
        
        #divNavigation ul a
        {
            width: 100%;
        }
        
        #divNavigation .preNext
        {
            width: 33px;
            height: 100px;
            position: absolute;
            top: -33px;
            background: url(../images/sprite.png) no-repeat 0 0;
            cursor: pointer;
        }
        #divNavigation .pre
        {
            left: 0;
        }
        #divNavigation .next
        {
            right: 0;
            background-position: right top;
        }
        
        #lblopeName
        {
            font-family: "微软雅黑";
            font-weight: normal;
            font-size: 16px;
            float: left;
            padding: 5px 0px 5px 12px;
            text-shadow: none;
            width: 100%;
            text-align: left;
        }
        
        .CustomerTable th
        {
            text-align: left;
            font-size: 12px;
            color: #989191;
            text-shadow: none;
            font-weight: 300;
        }
    </style>
</head>
<body style="background: #eee; overflow:visible"  >
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfwdID" runat="server" />
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow:visible;">
                    <div id="Header" data-role="header" data-theme="b" style="height: 65px; background-color: White;
                        width: 100%;border-bottom: 1px solid #eee;border-top: 1px solid #eee;" class="cssHeader">
                        <label id="lblopeName" runat="server"></label>
                        <table class="CustomerTable" style=" padding-left:12px;">
                            <tr>
                                <th>
                                    知识类型：
                                </th>
                                <th>
                                    <label id="lblknowType" runat="server">
                                    </label>
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div id="divBusinessView" style="margin: 0px auto; line-height: 2em; background-color:#fff; margin-top:8px;border-bottom: 1px solid #eee;border-top: 1px solid #eee;">
                        <table  class="CustomerTable" cellpadding="0" cellspacing="0" style=" padding: 10px 12px 10px 12px;"> 
                            <tr>
                                <th>
                                    主题
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblknowTheme" runat="server">
                                    </label>
                                </td>
                            </tr>
                           
                        </table>
                    </div>

                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;">
                        <table  class="CustomerTable" cellpadding="0" cellspacing="0" style=" padding: 10px 12px 10px 12px;">
                             <tr>
                                <th>
                                    内容
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                    <asp:Literal runat="server" id="lblknowContent"></asp:Literal>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;">
                        <table  class="CustomerTable" cellpadding="0" cellspacing="0" style=" padding: 10px 12px 10px 12px;">
                             <tr>
                                <th>
                                    附件
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <a id="lblknowFilePath" runat="server"><label id="lblURL" runat="server"></label></a>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="margin: 0px auto;line-height: 2em;background-color: #fff; border-bottom: 1px solid #eee;border-top: 1px solid #eee; margin-top:8px;">
                        <table  class="CustomerTable" cellpadding="0" cellspacing="0" style=" padding-left:12px;">
                            <tr>
                                <th>
                                    知识录入时间：
                                </th>
                                <th>
                                    <label id="lblknowOperateDate" runat="server">
                                    </label>
                                </th>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
