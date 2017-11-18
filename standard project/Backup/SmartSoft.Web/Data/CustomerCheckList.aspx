<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCheckList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>客户查重</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="format-detection"content="telephone=no" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" href="../css/jquery.mobile-1.4.5.min.css">
    <script type="text/javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery.mobile-1.4.5.min.js"></script>
    <link href="../css/PullToRefresh.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/iscroll.js" type="text/javascript"></script>
    <script src="../scripts/PullToRefresh.js" type="text/javascript"></script>
    <script src="../scripts/colorful.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/BaseHelper.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function Load() {

        }

        function doSearch() {
            $("#ulList").empty();
            var keyword = $("#txtKeyWord").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SearchCustomer",
                data: "{KeyWord:'" + keyword + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            var item = result[i];
                            var li = "";
                            li += '<li><a class="ui-btn ui-corner-all ui-shadow ui-btn-inline">';
                            li += '<h3>';
                            li += item.Name + '</h3>';
                            li += '<div class="GroupDiv">';
                            li += '<div class="ListLeft">';
                            li += '    <div class="dis">';
                            li += item.OperatorName + '|' + item.DepartmentName + '</div>';
                            li += '</div>';
                            li += '<div class="ListRight">';
                            li += '        <div class="dis">';
                            li += '<lable class="lblccStatus">';
                            li += item.SourceName + '</lable></div>';
                            li += '</div>';
                            li += '</div>';

                            li += '<div class="GroupDiv">';
                            li += '<div class="ListLeft">';
                            li += '<div class="dis">';
                            li += item.AddDate + '</div>';
                            li += '    </div>';
                            li += '</div>';
                            li += '</a>';
                            li += '</li>';

                            $("#ulList").append(li);
                        }
                       
                    }
                }
            })
        }
    </script>

    <style type="text/css">
        .pullUp
        {
            display:none !important;
        }
        
        #ulList li a
        {
            background-color:#fff;
            text-shadow:#fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divList" data-role="page">
        <div>
            <table width="100%" style="background-color: White; height: 3em">
                <tr>
                    <td style="text-align: right;">
                        <input type="text" placeholder="请输入搜索内容" data-role="none" id="txtKeyWord" style="height: 30px;
                            padding: 0px; margin: 0px; border: none; outline: none; width:96%;" />
                    </td>
                    <td style="text-align: left; width: 13%">
                        <img src="../@images/搜索.png" id="imgSearch" onclick="javascript:doSearch()" style="height: 25px;
                            float: left;" />
                    </td>
                </tr>
            </table>
        </div>
        <div data-role="content">
            <div id="wrapper" style="margin-top: 10px; background-color: #fff;">
                <ul data-role="listview" id="ulList" style=" margin:0px;" data-icon="false" data-filter="false">
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
