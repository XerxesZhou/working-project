<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerNearbyList.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerNearbyList" %>

<!DOCTYPE html>
<html>
<head>
    <title>附近客户</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"> 
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <script src="../scripts/jquery-1.8.3.min.js"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js"></script>
    <script src="/scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=dupR0OqZhqawUyWcBo4jwRju"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script>
        function ViewCustomer(id) {
            window.open("CustomerEditForm.aspx?Action=View&ID=" + id);
        }

        $(function () {
            var geolocation = new BMap.Geolocation();
            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    currentPoint = r.point;
                    $("#hfLongtitude").val(r.point.lng);
                    $("#hfLatitude").val(r.point.lat);
                    $("#btnSearch").click();
                }
            });
        });

        function ViewMap() {
            window.open("CustomerNearbyMap.aspx");
        }

    </script>

    <style type="text/css">
        .ui-listview a
        {
            background-color:White;
            font-weight:normal;
        }
        .ui-listview a h3
        {
            font-weight:normal;
        }
        
        .ui-listview a div
        {
            color: #828181;
             font-weight:normal;
        }
    </style>
</head>
<body>
    <form runat="server" id="form1">
    <asp:HiddenField runat="server" ID="hfLongtitude" />
    <asp:HiddenField runat="server" ID="hfLatitude" />
    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Width="0px" style="display:none"/>
            
    <div data-role="page">
    	<a data-role="button" style=" background-color:#5E97F6; font-weight:normal" href="javascript:ViewMap();">地图模式查看</a>
        <div data-role="content">
            <ul data-role="listview" data-icon="false" data-filter="false" data-filter-placeholder="输入关键字">
                <asp:Repeater runat="server" ID="repCustomer">
                    <ItemTemplate>
                        <li><a href='javascript:ViewCustomer(<%#Eval("cusID")%>)'>
                                <h3><%#Eval("cusName")%></h3>
                                <div style="font-size:small; font-weight:normal;">
                                    <div style="float:left; width:78%; overflow:hidden;">
                                        <div><%#Eval("cusAddress")%></div>
                                    </div>
                                    <div style="float:right; width:20%;">
                                        <%#Eval("cusDistance")%>公里
                                    </div>
                                </div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
