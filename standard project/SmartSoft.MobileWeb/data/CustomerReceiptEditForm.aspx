<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerReceiptEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.data.CustomerReceiptEditForm" %>

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
    <link href="../css/jquery.mobile-1.3.2.min.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.mobile-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cssMainPanel" style="margin: 0px auto; line-height: 2em;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <th>
                    业务名称：
                </th>
                <td>
                    <label runat="server" id="lblcrBusinessName">
                    </label>
                </td>
            </tr>
            <tr>
                <th>
                    收款金额：
                </th>
                <td>
                    <label runat="server" id="lblcrAmount">
                    </label>
                </td>
            </tr>
            <tr>
                <th>
                    业务员：
                </th>
                <td>
                    <label runat="server" id="lblcrOperatorName">
                    </label>
                </td>
            </tr>
            <tr>
                <th>
                    收款日期：
                </th>
                <td>
                    <label runat="server" id="lblcrDate">
                    </label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
