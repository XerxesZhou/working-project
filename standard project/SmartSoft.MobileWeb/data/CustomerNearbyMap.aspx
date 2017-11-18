<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerNearbyMap.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerNearbyMap" %>

<!DOCTYPE html>
<html>
<head>
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
    <style>
        body, html{width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
        #mymap{width: 100%;height: 480px;overflow: hidden;margin:0;font-family:"微软雅黑";}
    </style>
    <script type="text/javascript">
        function ViewCustomer(id) {
            window.open("CustomerEditForm.aspx?Action=View&ID=" + id);
        }

        var currentPoint;
        var customerList;
        var map;
        function SetLayers() {
            var geolocation = new BMap.Geolocation();
            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    map.centerAndZoom(r.point, 11);
                    map.setCurrentCity("深圳市");
                    var myIcon = new BMap.Icon("images/here.jpg", new BMap.Size(45, 45));
                    var marker = new BMap.Marker(r.point, { icon: myIcon });  // 创建标注
                    map.addOverlay(marker);
                    marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画       
                    currentPoint = r.point;
                    var currentOperatorID = $("#hfCurrentOperatorID").val();
                    var paradata = "{currentLongtitude:" + r.point.lng + ",currentLatitude:" + r.point.lat + ",currentOperatorID:" + currentOperatorID + "}";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "../AjaxMethods.asmx/GetCustomerNearby",
                        data: paradata,
                        dataType: 'json',
                        success: function (result) {
                            if (result.d.length > 0) {
                                customerList = result.d;
                                for (var i = 0; i < result.d.length; i++) {
                                    var item = result.d[i];
                                    var pt = new BMap.Point(item.cusLongtitude, item.cusLatitude);
                                    var distance = (map.getDistance(currentPoint, pt) / 1000).toFixed(2);
                                    var url = "<a href='DrivingRoute.aspx?lng1=" + currentPoint.lng + "&lat1=" + currentPoint.lat + "&lng2=" + pt.lng + "&lat2=" + pt.lat + "'>导航路线</a>";
                                    //url = "<a href='baidumap://map/direction?origin=" + currentPoint.lng + "," + currentPoint.lat + "&destination=" + pt.lng + "," + pt.lat + "&mode=driving&src=创捷易'>百度导航</a>";
                                    var content = "<p style='font-size:14px;'>" + "<b>" + item.cusName + "</b>&nbsp;&nbsp;&nbsp;&nbsp;<a href='tel:" + item.cusTel + "'> CALL:" + item.cusContactor + "</a><br/>" + item.cusAddress + "<br/>" + distance + "公里" + "&nbsp;&nbsp;&nbsp;&nbsp;" + url + "</p>";
                                    var myIcon = new BMap.Icon("images/customer.gif", new BMap.Size(16, 16));
                                    //var marker2 = new BMap.Marker(pt, { icon: myIcon });  // 创建标注
                                    var marker2 = new BMap.Marker(pt);  // 创建标注
                                    map.addOverlay(marker2);              // 将标注添加到地图中
                                    addClickHandler(content, marker2);
                                }
                            }
                            else {
                                alert("附近没有客户。");
                            }
                        }
                    });
                }
            });
        }

        function addClickHandler(content, marker) {
            marker.addEventListener("click", function (e) {
                openInfo(content, e)
            }
		);
        }

        function openInfo(content, e) {
            var p = e.target;
            var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
            var infoWindow = new BMap.InfoWindow(content);  // 创建信息窗口对象 
            map.openInfoWindow(infoWindow, point); //开启信息窗口
        }

        $(function () {
            map = new BMap.Map("mymap");
            SetLayers();
        });
    </script>
</head>
<body>
    <form runat="server" id="form1">
        <div id="mymap"></div>
        <asp:HiddenField runat="server" ID="hfCurrentOperatorID" />
    </form>
</body>
</html>
