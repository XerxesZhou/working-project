﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="SmartSoft.MobileWeb.CustomerList" %>

<!DOCTYPE html>
<html manifest="CustomerList.cache">
<head>
    <title>客户列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="format-detection"content="telephone=no" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" href="../css/jquery.mobile-1.4.5.min.css">
    <script type="text/javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery.mobile-1.4.5.min.js"></script>
    <link href="../css/PullToRefresh.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/iscroll.js" type="text/javascript"></script>
    <script src="../scripts/PullToRefresh.js" type="text/javascript"></script>
    <script src="../scripts/CustomerList.js" type="text/javascript"></script>
    <script src="../scripts/colorful.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/BaseHelper.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        FunctionName = "GetCustomerDate";

        function getParameterStr(mode) {

            var cusKindID = $("#ddlCustomerType").val();
            var cusSource = $("#ddlSource").val();
            var cusDepartment = $("#ddlDepartment").val();
            var cusOperator = $("#ddlOperator").val();
            var cusDateBegin = $("#txtDateBegin").val();
            var cusDateEnd = $("#txtDateEnd").val();

            var keyWord = $("#txtKeyWord").val();

            var count = parseInt($("#hfCurrentCount").val());

            var EachCount = parseInt($("#hfEachCount").val());

            var conditionType =  getUrlParameter("ConditionType");
            var conditionValue = getUrlParameter("ConditionValue");
            var para = "{count:'" + count + "',EachCount:'" + EachCount + "',cusKindID:'" + cusKindID + "',keyword:'" + keyWord + "',cusSource:'" + cusSource + "',cusDepartment:'" + cusDepartment + "',cusOperator:'" + cusOperator + "',cusDateBegin:'" + cusDateBegin + "',cusDateEnd:'" + cusDateEnd + "',currentOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cusColumn:'" + $("#ddlTableColumn").val() + "',orderby:'" + $("#ddlOrder").val() + "',mode:'" + mode + "',conditionType:'" + conditionType + "',conditionValue:'" + conditionValue + "'}";
            return para;
        }

        function getLiContent(item) {
            var li = "";
            li += '<li><a class="ui-btn ui-corner-all ui-shadow ui-btn-inline" onclick="ViewCustomer(' + item.cusID + ')">';
            li += '<h3>';
            li += item.cusName + '</h3>';
            li += '<div class="GroupDiv">';
            li += '<div class="ListLeft">';
            li += '    <div class="dis">';
            li += item.cusContactor + '</div>';
            li += '</div>';
            li += '<div class="ListRight">';
            li += '        <div class="dis">';
            li += '<lable class="lblccStatus">';
            li += item.TypeName + '</lable></div>';
            li += '</div>';
            li += '</div>';

            li += '<div class="GroupDiv">';
            li += ' <div style="float: left; width: 100%;">';
            li += '        <div class="dis">';
            li += item.cusAddress + '</div>';
            li += '    </div>';
            li += '</div>';

            li += '<div class="GroupDiv">';
            li += '<div class="ListLeft">';
            li += '<div class="dis">';
            li += item.opeName + '|' + item.cusAddDate + '</div>';
            li += '    </div>';
            li += '</div>';
            li += '</a>';
            li += '</li>';
            return li;
        }

        function pageSetRight() {
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '新建', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    window.location = "CustomerAdd.aspx?Action=Insert&ID=0";
                },
                onFail: function (err) {
                    alert(JSON.stringify(err));
                }
            });
        }
    </script>

   
</head>
<body>
    <form id="form1" runat="server">
    <div id="divList" data-role="page">
        <div>
            <table width="100%" style="background-color: White; height: 3em">
                <tr>
                    <td style="text-align: center; width: 15%; border-right: 1px solid #ddd;">
                        <img src="../@images/Condition.png" id="Condition" style="height: 25px;" />
                    </td>
                    <td style="text-align: center; width: 15%; border-right: 1px solid #ddd;" id="imgOrderBy">
                        <img src="../@images/orderBy.png" id="orderBy" style="height: 25px;" />
                    </td>
                    <td style="text-align: right;">
                        <input type="text" placeholder="请输入搜索内容" data-role="none" id="txtKeyWord" style="height: 30px;
                            padding: 0px; margin: 0px; border: none; outline: none;" />
                    </td>
                    <td style="text-align: left; width: 13%">
                        <img src="../@images/搜索.png" id="imgSearch" onclick="javascript:doSearch()" style="height: 25px;
                            float: left;" />
                    </td>
                </tr>
            </table>
        </div>
        <div data-role="content">
            <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />
            <asp:HiddenField ID="hfSumCount" runat="server" />
            <asp:HiddenField ID="hfCurrentCount" runat="server" />
            <asp:HiddenField ID="hfEachCount" runat="server" Value="10" />
            <asp:HiddenField ID="hfStatus" runat="server" Value="OK" />
            <div id="wrapper" style="margin-top: 10px; background-color: #fff;">
                <ul data-role="listview" id="ulList" style=" margin:0px;" data-icon="false" data-filter="false">
                </ul>
            </div>
            <%--查询条件--%>
            <div class="ui-header" id="divSearchPanel" style="background-color: #eee; z-index: 999;
                display: none">
                <div style="background-color: #fff;">
                    <table class="searchTable" id="SearchTable" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                客户类别:
                            </th>
                            <td>
                                <asp:DropDownList runat="server" data-role="none" ID="ddlCustomerType">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                客户来源:
                            </th>
                            <td>
                                <asp:DropDownList runat="server" data-role="none" ID="ddlSource">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                所属部门:
                            </th>
                            <td>
                                <asp:DropDownList runat="server" data-role="none" ID="ddlDepartment">
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
                            </td>
                        </tr>
                        <tr>
                            <th>
                            </th>
                            <td>
                                <div class="dataSearch">
                                    <span class="dataspan">至：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateEnd" data-role="none"
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
            <div class="ui-header" id="divOrderPanel" style="background-color: #eee; z-index: 999;
                display: none">
                <div style="background-color: #fff;">
                    <table class="searchTable" id="Table1" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                栏目:
                            </th>
                            <td>
                                <asp:DropDownList runat="server" data-role="none" ID="ddlTableColumn">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Text="类型" Value="cusKindID"></asp:ListItem>
                                    <asp:ListItem Text="来源" Value="cusSourceID"></asp:ListItem>
                                    <asp:ListItem Text="录入人" Value="cusAddOperatorID"></asp:ListItem>
                                    <asp:ListItem Text="录入时间" Value="cusAddDate" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                排序:
                            </th>
                            <td>
                                <asp:DropDownList runat="server" data-role="none" ID="ddlOrder">
                                    <asp:ListItem Value="desc">倒序</asp:ListItem>
                                    <asp:ListItem Value="asc">顺序</asp:ListItem>
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
