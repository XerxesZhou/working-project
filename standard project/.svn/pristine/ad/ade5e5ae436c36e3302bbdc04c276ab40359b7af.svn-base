<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrivingRoute.aspx.cs"
    Inherits="SmartSoft.MobileWeb.DrivingRoute" %>

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
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=dupR0OqZhqawUyWcBo4jwRju"></script>
    <style>
        body, html {width: 100%;height: 100%; margin:0;font-family:"微软雅黑";}
		#l-map{height:300px;width:100%;}
		#r-result,#r-result table{width:100%;}
    </style>
    <script type="text/javascript">
        $(function () {
            // 百度地图API功能
            var lng1 = getUrlParameter("lng1");
            var lat1 = getUrlParameter("lat1");
            var lng2 = getUrlParameter("lng2");
            var lat2 = getUrlParameter("lat2");
            var point1 = new BMap.Point(lng1, lat1);
            var point2 = new BMap.Point(lng2, lat2);
            var map = new BMap.Map("l-map");
            map.centerAndZoom(new BMap.Point((lng1 + lng2) / 2, (lat1 + lat2) / 2), 12);

            var driving = new BMap.DrivingRoute(map, { renderOptions: { map: map, panel: "r-result", autoViewport: true} });
            driving.search(point1, point2);
        });

        //获得Url中的参数的值
        function getUrlParameter(asName) {
            try {
                var lsURL = window.location.href;
                loU = lsURL.split("?");
                if (loU.length > 1) {
                    var loallPm = loU[1].split("&");
                    for (var i = 0; i < loallPm.length; i++) {
                        var loPm = loallPm[i].split("=");
                        if (loPm[0] == asName) {
                            if (loPm.length > 1) {
                                return loPm[1];
                            }
                            else {
                                return "";
                            }
                        }
                    }
                }
                return null;
            }
            catch (err) {
            }
        }
    </script>
</head>
<body>
	<div id="l-map"></div>
	<div id="r-result"></div>
</body>
</html>
