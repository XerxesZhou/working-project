<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowHistoryList.aspx.cs" Inherits="SmartSoft.MobileWeb.CustomerFollowHistoryList" %>

<!DOCTYPE html>
<html>
<head>
    <title>跟进列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link rel="stylesheet" href="../css/jquery.mobile-1.4.5.min.css">
    <script type="text/javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery.mobile-1.4.5.min.js"></script>
    <script type="text/javascript" src="../scripts/BaseHelper.js"></script>
    <link href="../css/PullToRefresh.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/iscroll.js" type="text/javascript"></script>
    <script src="../scripts/PullToRefresh.js" type="text/javascript"></script>
    <script src="../scripts/CustomerList.js" type="text/javascript"></script>
    <script src="../scripts/colorful.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>

    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function handleClickLike(id, cfhOperatorID) {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            var cfhID = id;

            var ccdate = "{currentOperatorID:'" + currentOperatorID + "',cfhID:'" + cfhID + "',cfhOperatorID:'" + cfhOperatorID + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetClickLikeInfoForHistory",
                data: ccdate,
                dataType: 'json',
                success: function (result) {
                    if (result.d != "") {
                        var operators = result.d.split("|");
                        $("#lblLikeCount" + id).html("点赞(" + operators.length + ")");
                    }
                    else {
                        $("#lblLikeCount" + id).html("点赞(0)");
                    }
                    var operatorName = $("#hfCurrentOperatorName").val();
                    if ((result.d + "|").indexOf(operatorName) > -1) {
                        $("#divLike" + id).removeClass("cssNotLike");
                        $("#divLike" + id).addClass("cssLike");
                        $("#imgLike" + id).attr("src", "../@images/likeClick.png");
                    }
                    else {
                        $("#divLike" + id).removeClass("cssLike");
                        $("#divLike" + id).addClass("cssNotLike");
                        $("#imgLike" + id).attr("src", "../@images/like.png");
                    }
                }
            })
        }

        function GotoFollowHistory(id, fromSource) {
            window.location.href = "CustomerFollowHistoryEditForm.aspx?ID=" + id + "&FromSource=" + fromSource;
        }

        FunctionName = "GetCustomerFollowDate";

        function getParameterStr(mode) {

            var cusKindID = $("#ddlCustomerLinkType").val();

            var cusOperator = $("#ddlOperator").val();
            var cusDateBegin = $("#txtDateBegin").val();
            var cusDateEnd = $("#txtDateEnd").val();

            var keyWord = $("#txtKeyWord").val();

            var count = parseInt($("#hfCurrentCount").val());

            var EachCount = parseInt($("#hfEachCount").val());

            var para = "{count:'" + count + "',EachCount:'" + EachCount + "',cusKindID:'" + cusKindID + "',keyword:'" + keyWord + "',cusOperator:'" + cusOperator + "',cusDateBegin:'" + cusDateBegin + "',cusDateEnd:'" + cusDateEnd + "',currentOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cusColumn:'" + $("#ddlTableColumn").val() + "',orderby:'" + $("#ddlOrder").val() + "',mode:'" + mode + "'}";

            return para;
        }

        function getLiContent(item) {
            var li = "";
            li += '<li id="liFollowHistory' + item.cfhID + '" class="MainPanelli">';
            li += '<div class="MainView"><div>';
            li += '<table cellpadding="0" cellspacing="0"  style=" padding:5px;width:100%;">';
            li += '<tr onclick="javascript:GotoFollowHistory(' + item.cfhID + ',\'HistoryList\');">';
            li += '<td style="width:50%;">';
            li += '<label style="color: #3C96DE; width:25%">' + item.opeName + '</label>';
            li += '<label style="color: #3C96DE; width:25%; border: 1px solid #3C96DE;margin-left: 10px;border-radius: 3px;padding: 3px 3px 3px 3px;font-size: 10px;">' + item.cfhTypeName + '</label>';
            li += '</td><td style="width: 50%;text-align: right;">';
            li += '<label>' + item.cfhAddDate + '</label></td></tr>';
            li += '<tr onclick="javascript:GotoFollowHistory(' + item.cfhID + ',\'HistoryList\');">';
            li += '<td colspan="2" style=" line-height:2em">';
            li += '<label>' + item.cfhContent + '</label></td></tr>';
            li += '<tr><td colspan="2">' + item.cfhFile + '</td></tr>';
            li += '<tr onclick="javascript:GotoFollowHistory(' + item.cfhID + ',\'HistoryList\');">';
            li += '<td colspan="2"><label>' + item.cfhAddress + '</label></td></tr></table></div>';
            li += '<div><table style="width:100%;"><tr>';
            li += '<td>' + item.cfhLikeAndComment + '</td>';
            li += '</tr></table></div>';
            li += '<div style="padding:10px;"><table style="width:100%;"><tr><th><label>来自：' + item.cfhFromName + '</label></th></tr></div>'
            li+='</div></li>';
            return li;
        }

    </script>

    <style type="text/css">
        .ui-mobile label, .ui-controlgroup-label
        {
            margin:0px;
            padding:0px;
            display:inline;
            font-size:14px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divList" data-role="page">
            <div style=" margin-bottom:10px;">
            <table width="100%" style=" background-color:White; height:3em">
                <tr>
                <td style=" text-align:center; width:15%; border-right: 1px solid #ddd;"> <img src="../@images/Condition.png" id="Condition" style=" height:25px;" /></td>
                <td style=" text-align:center; width:15%; border-right: 1px solid #ddd;" id="imgOrderBy"> <img src="../@images/orderBy.png" id="orderBy" style=" height:25px;" /></td>
                <td style=" text-align:right;"><input type="text" placeholder="请输入搜索内容" data-role="none" id="txtKeyWord"  style=" height:30px; padding:0px; margin:0px;border:none;outline:none;" /> </td> 
                <td style=" text-align:left; width:13%"><img src="../@images/搜索.png" id="imgSearch" onclick="javascript:doSearch()" style=" height:25px;    float: left;" /></td>
                </tr> 
            </table>
       </div>
        <div data-role="content">
        
            <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />
            <asp:HiddenField runat="server" ID="hfCurrentOperatorName" />
            <asp:HiddenField ID="hfSumCount" runat="server" />
            <asp:HiddenField ID="hfCurrentCount" runat="server"/>
            <asp:HiddenField ID="hfEachCount" runat="server" />
            <asp:HiddenField ID="hfStatus" runat="server" Value="OK"/>

           <div id="wrapper">

             <ul data-role="listview" id="ulList" style=" margin:0px;" data-icon="false" data-filter="false" data-filter-placeholder="输入关键字">
                
            </ul>
            
            </div>

            
            <div id="divSearchPanel" class="ui-header"  style=" background-color:#eee;z-index:999; display:none" >
                <div style="background-color:#fff;">
                <table class="searchTable" cellpadding="0" cellspacing="0" style="font-size: small;
                    font-weight: normal;">
                    <tr>
                        <th>
                            联系类型:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlCustomerLinkType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            业务员:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlOperator">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            录入日期:
                        </th>
                        <td>
                            <div class="dataSearch">
                                <span class="dataspan">从：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateBegin" type="date"
                                    data-role="none"></asp:TextBox></div>
                            <div class="dataSearch">
                                <span class="dataspan">至：</span><asp:TextBox runat="server"  CssClass="DateSelect" ID="txtDateEnd" data-role="none"
                                    type="date"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div data-role="navbar">
                                <ul data-theme="b">
                                    <li><a href="javascript:doSearch();">查询</a></li>
                                    <li><a href='javascript:CloseSearch();'>关闭</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                </div>
            </div>
             <%--排序条件--%>
            <div  class="ui-header" id="divOrderPanel" style=" background-color:#eee;z-index:999; display:none">
            <div style=" background-color:#fff;">
                <table class="searchTable" id="Table1" cellpadding="0" cellspacing="0" style="font-size: small;
                    font-weight: normal;line-height:3.5em;">
                    <tr>
                        <th>
                            栏目:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlTableColumn">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Text="跟进类型" Value="cfhTypeID"></asp:ListItem>
                                <asp:ListItem Text="来源" Value="cfhSource"></asp:ListItem>
                                <asp:ListItem Text="录入人" Value="cfhAddOperatorID"></asp:ListItem>
                                <asp:ListItem Text="录入时间" Value="cfhAddDate" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            排序:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlOrder">
                                <asp:ListItem value="desc">倒序</asp:ListItem>
                                <asp:ListItem value="asc">顺序</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div data-role="navbar">
                                <ul data-theme="b">
                                    <li><a href="javascript:doSearch();">查询</a></li>
                                    <li><a href='javascript:CloseSearch();'>关闭</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
       
        </div>

        <div data-role="footer" data-position="fixed" data-theme="b" data-tap-toggle="false"
            class="footerRecord">
            记录数：<asp:Label runat="server" ID="lblQuantity"></asp:Label>/<asp:Label runat="server" ID="lblSumQuantity"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
