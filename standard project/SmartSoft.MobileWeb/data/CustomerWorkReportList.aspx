<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWorkReportList.aspx.cs" Inherits="SmartSoft.MobileWeb.CustomerWorkReportList" %>

<!DOCTYPE html>
<html>
<head>
    <title>工作日报</title>
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
    <link href="../css/PullToRefresh.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/iscroll.js" type="text/javascript"></script>
    <script src="../scripts/PullToRefresh.js" type="text/javascript"></script>
    <script src="../scripts/CustomerList.js" type="text/javascript"></script>
    <script src="../scripts/colorful.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/BaseHelper.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ViewWorkReportDetail(id) {
            window.location.href = "CustomerWorkReportEditForm.aspx?Action=View&ID=" + id;
        }

        FunctionName = "GetWorkDiaryDate";

        function getParameterStr(mode) {

            var wdDateBegin = $("#txtDateBegin").val();
            var wdDateEnd = $("#txtDateEnd").val();

            var keyWord = $("#txtKeyWord").val();

            var count = parseInt($("#hfCurrentCount").val());

            var EachCount = parseInt($("#hfEachCount").val());

            var para = "{count:'" + count + "',EachCount:'" + EachCount + "',keyword:'" + keyWord + "',wdDateBegin:'" + wdDateBegin + "',wdDateEnd:'" + wdDateEnd + "',currentOperatorID:'" + $("#hfCurrentOperatorID").val() + "',cusColumn:'" + $("#ddlTableColumn").val() + "',orderby:'" + $("#ddlOrder").val() + "',mode:'" + mode + "'}";

            return para;
        }

        function getLiContent(item) {
            var li = "";
            li += '<li><a  href="#" class="ui-btn ui-corner-all ui-shadow ui-btn-inline" style="background-color:#fff;" onclick="ViewWorkReportDetail(' + item.wdID + ')">';
            li += '<h3  style="font-weight: normal;font-size: 16px;font-family: "微软雅黑";">';
            li += item.wdAddOperatorName + '的' + item.wdType + '' + item.wdAddDate + '</h3>';
            li += '<h5 style="color: #989191;font-weight: 400;">';
            li += item.wdAddDate + '</h5>'
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
                    window.location = "CustomerWorkReportAdd.aspx?Action=Insert&ID=0";
                },
                onFail: function (err) { }
            });
        }

    </script>

    <script type="text/javascript">

        var toScrollFrame = function (iFrame, mask) {
            if (!navigator.userAgent.match(/iPad|iPhone/i))
                return false;
            //do nothing if not iOS devie  
            var mouseY = 0;
            var mouseX = 0;
            jQuery(iFrame).ready(function () {//wait for iFrame to load  
                //remeber initial drag motition  
                jQuery(iFrame).contents()[0].body.addEventListener('touchstart', function (e) {
                    mouseY = e.targetTouches[0].pageY;
                    mouseX = e.targetTouches[0].pageX;
                });

                //update scroll position based on initial drag position  
                jQuery(iFrame).contents()[0].body.addEventListener('touchmove', function (e) {
                    e.preventDefault();
                    //prevent whole page dragging  

                    var box = jQuery(mask);
                    box.scrollLeft(box.scrollLeft() + mouseX - e.targetTouches[0].pageX);
                    box.scrollTop(box.scrollTop() + mouseY - e.targetTouches[0].pageY);
                    //mouseX and mouseY don't need periodic updating, because the current position  
                    //of the mouse relative to th iFrame changes as the mask scrolls it.  
                });
            });

            return true;
        };

        toScrollFrame('.myFrame', '.myMask');  
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

             <ul data-role="listview" id="ulList" style=" margin:0px" data-icon="false" data-filter="false" data-filter-placeholder="输入关键字">
                
            </ul>
            
            </div>

            <div id="divSearchPanel" class="ui-header"  style=" background-color:#eee;z-index:999; display:none">
            <div style=" background-color:#fff;">
                <table class="searchTable" id="SearchTable" cellpadding="0" cellspacing="0" style="font-size: small;
                    font-weight: normal;line-height:3.5em;"">
                   <tr>
                        <th>
                            录入日期:
                        </th>
                        <td><div class="dataSearch">
                                <span class="dataspan">从：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateBegin" type="date"
                                    data-role="none"></asp:TextBox></div></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td><div class="dataSearch">
                                <span class="dataspan">至：</span><asp:TextBox runat="server" CssClass="DateSelect" ID="txtDateEnd" data-role="none"
                                    type="date"></asp:TextBox></div></td>
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
                                <asp:ListItem Text="录入时间" Value="wdAddDate" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="录入人" Value="wdAddOperatorID"></asp:ListItem>
                                <asp:ListItem Text="报告类别" Value="wdTypeID"></asp:ListItem>
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
