<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOpptunityList.aspx.cs" Inherits="SmartSoft.MobileWeb.CustomerOpptunityList" %>
<!DOCTYPE html>
<html>
<head>
    <title>商机列表</title>
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
    <script src="../scripts/CustomerList.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/BaseHelper.js"></script>
     <link href="../css/PullToRefresh.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/iscroll.js" type="text/javascript"></script>
    <script src="../scripts/PullToRefresh.js" type="text/javascript"></script>
    <script src="../scripts/colorful.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ViewCustomerOpptunity(id) {
            window.location.href = "CustomerOpptunityEditForm.aspx?Action=View&ctr=Opptunity&ID=" + id;
        }

        FunctionName = "GetOpptunityData";

        function getParameterStr(mode) {

            var coPhaseID = $("#ddlcoPhaseID").val();
            var coOpptunitySourceID = $("#ddlcoOpptunitySourceID").val();
            var coStatusID = $("#ddlcoStatusID").val();
            var coDateBegin = $("#txtDateBegin").val();
            var coDateEnd = $("#txtDateEnd").val();

            var count = parseInt($("#hfCurrentCount").val());

            var EachCount = parseInt($("#hfEachCount").val());

            var conditionType = getUrlParameter("ConditionType");
            var conditionValue = getUrlParameter("ConditionValue");

            var keyWord = $("#txtKeyWord").val();
            var para = "{count:'" + count + "',EachCount:'" + EachCount + "',coPhaseID:'" + coPhaseID + "',keyword:'" + keyWord + "',coOpptunitySourceID:'" + coOpptunitySourceID + "',coStatusID:'" + coStatusID + "',coDateBegin:'" + coDateBegin + "',coDateEnd:'" + coDateEnd + "',currentOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cusColumn:'" + $("#ddlTableColumn").val() + "',orderby:'" + $("#ddlOrder").val() + "',mode:'" + mode + "',conditionType:'" + conditionType + "',conditionValue:'" + conditionValue + "'}";
            
            return para;
        }

        function getLiContent(item) {
            var li = "";
            li += '<li><a class="ui-btn ui-corner-all ui-shadow ui-btn-inline" onclick="ViewCustomerOpptunity(' + item.coID + ')">';
            li += '<h3>';
            li += '' + item.cusName + '-' + item.coName + '</h3>';
            li += '<div class="GroupDiv">';
            li += ' <div class="ListLeft">';
            li += '        <div class="dis">';
            li += ChangedWithThousandTag(item.coPredictAmount) + '</div>';
            li += '    </div>';
            li += '<div class="ListRight">';
            li += '    <div class="dis"><lable  class="lblccStatus">';
            li += item.coPhaseID + '</lable></div>';
            li += '</div>';
            li += '</div>';
            li += '<div class="GroupDiv">';
            li += '<div style="float: left; width: 100%;">';
            li += '        <div class="dis">';
            li += item.coStatusID + '</div>';
            li += '</div>';
            li += '</div>';
            li += '<div class="GroupDiv">';
            li += '<div class="ListLeft">';
            li += '<div class="dis">';
            li += item.coOperatorName + "|" + item.coAddDate + '</div>';
            li += '    </div>';
            li += '</div>';
            li += '</a>';
            li += '</li>';
            return li;
        }
    </script>

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
         </tr> </table>
         </div>
        <div data-role="content">
            <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />

            <asp:HiddenField ID="hfSumCount" runat="server" />
            <asp:HiddenField ID="hfCurrentCount" runat="server"/>
            <asp:HiddenField ID="hfEachCount" runat="server" />
            <asp:HiddenField ID="hfStatus" runat="server" Value="OK"/>

            <div id="wrapper">

            <ul data-role="listview" id="ulList" style=" margin:0px;" data-icon="false" data-filter="false" data-filter-placeholder="输入关键字">
               
            </ul>

            </div>
           
           <div id="divSearchPanel"  class="ui-header"  style=" background-color:#eee;z-index:999; display:none">
            <div style=" background-color:#fff;">
                <table class="searchTable" id="SearchTable" cellpadding="0" cellspacing="0"  style="font-size: small; font-weight: normal;">

                    <tr>
                        <th>
                            商机阶段:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoPhaseID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商机来源:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoOpptunitySourceID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商机状态:
                        </th>
                        <td>
                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoStatusID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <th>
                            录入日期:
                        </th>
                        <td>
                            <div class="dataSearch"><span class="dataspan">从：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateBegin" type="date" data-role="none"></asp:TextBox></div>
                            <div class="dataSearch"><span class="dataspan">至：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateEnd" data-role="none" type="date"></asp:TextBox></div>
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
                                <asp:ListItem Text="商机来源" Value="coOpptunitySourceID"></asp:ListItem>
                                <asp:ListItem Text="商机阶段" Value="coStatusID"></asp:ListItem>
                                <asp:ListItem Text="商机状态" Value="coPhaseID"></asp:ListItem>
                                <asp:ListItem Text="负责人" Value="coOperatorID"></asp:ListItem>
                                <asp:ListItem Text="录入人" Value="coAddOperatorID"></asp:ListItem>
                                <asp:ListItem Text="录入时间" Value="coAddDate" Selected="True"></asp:ListItem>
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
