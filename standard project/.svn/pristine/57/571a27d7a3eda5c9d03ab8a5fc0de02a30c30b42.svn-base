<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWorkReportAdd.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerWorkReportAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增日报</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    
    <script src="../scripts/jquery-1.8.3.min.js"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js"></script>
    <script src="../scripts/BaseHelper.js"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function SaveWorkDiary() {
            var wdDate = $("#txtwdDate").val();
            var wdTitle = $("#txtwdTitle").val();
            var wdContent = $("#txtwdContent").val();
            var t = $(".ReportTypeSelected label").html();
            var wdTypeID = "";
            switch (t) {
                case "日报":
                    wdTypeID = "1";
                    break;
                case "周报":
                    wdTypeID = "2";
                    break;
                case "月报":
                    wdTypeID = "3";
                    break;
            }
            var wdFile = $("#hffollowFile").val();
            if (wdTitle == "") {
                alert("总结不能为空!");
                return false;
            }
            if (wdContent == "") {
                alert("计划不能为空！");
                return false;
            }
            var wdNotifier = $("#ddlwdNotifier").val();
            var wdAddOperator = $("#hfCurrentOperatorID").val();

            var wddata = "{wdDate:'" + wdDate + "',wdTitle:'" + wdTitle + "',wdContent:'" + wdContent + "',wdNotifierID:'" + wdNotifier + "',wdAddOperatorID:'" + wdAddOperator + "',wdTypeID:'" + wdTypeID + "',wdFile:'" + wdFile + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/SaveWorkDiary",
                data: wddata,
                dataType: 'json',
                success: function (result) {
                    if (result + 0 > 0) {
                        window.location.href = "../data/CustomerWorkReportList.aspx";
                    }
                }
            })
        }

        $(function () {
            LoadSelectDate();
            $("div", $("#ReportType")).click(function () {
                $("#txtwdDate").empty();
                $("#txtwdDate").css("direction", "rtl");
                $("div", $("#ReportType")).removeClass("ReportTypeSelected");
                $(this).addClass("ReportTypeSelected");
                LoadSelectDate();
            })
        })

        function LoadSelectDate() {
            var CurrentDate = new Date();
            var y = CurrentDate.getFullYear();
            var m = CurrentDate.getMonth() + 1;
            var d = CurrentDate.getDate();
            var a = CurrentDate.getDay();

            var t = $(".ReportTypeSelected label").html();
            var html = "";
            switch (t) {
                case "日报":
                    for (var i = d - 4; i <= d + 1; i++) {
                        html += "<option value='" + y + '-' + m + '-' + i + "'>" + y + '-' + m + '-' + i + "</option>";
                    }
                    $("#txtwdDate").append(html);
                    var v = y + "-" + m + "-" + d;
                    $("#txtwdDate").val(v);
                    break;
                case "周报":
                    $("#txtwdDate").css("direction", "ltr");
                    switch (a) {
                        case 1:
                            var d1 = d - 0;
                            var d2 = d + 6;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 2:
                            var d1 = d - 1;
                            var d2 = d + 5;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 3:
                            var d1 = d - 2;
                            var d2 = d + 4;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 4:
                            var d1 = d - 3;
                            var d2 = d + 3;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 5:
                            var d1 = d - 4;
                            var d2 = d + 2;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 6:
                            var d1 = d - 5;
                            var d2 = d + 1;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                        case 7:
                            var d1 = d - 6;
                            var d2 = d + 0;
                            html += "<option value='" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "'>" + y + '-' + m + '-' + d1 + '~' + y + '-' + m + '-' + d2 + "</option>";
                            $("#txtwdDate").append(html);
                            break;
                    }
                    break;

                case "月报":
                    if (m >= 5) {
                        for (var i = m - 4; i <= m + 1; i++) {
                            if (i <= 12) {
                                html += "<option value='" + y + '-' + i + "'>" + y + '-' + i + "</option>";
                            }
                            else {
                                i1 = i - 12;
                                y1 = y + 1;
                                html += "<option value='" + y1 + '-' + i1 + "'>" + y1 + '-' + i1 + "</option>";
                            }
                        }
                    }
                    else {
                        for (var i = m - 4; i <= m + 1; i++) {

                            i1 = 12 + i;
                            if (i1 > 12) {
                                i1 = i1 - 12;
                                html += "<option value='" + y + '-' + i1 + "'>" + y + '-' + i1 + "</option>";
                            }
                            else {
                                y1 = y - 1;
                                html += "<option value='" + y1 + '-' + i1 + "'>" + y1 + '-' + i1 + "</option>";
                                alert(html);
                            }
                        }
                    }

                    $("#txtwdDate").append(html);
                    var v = y + "-" + m;
                    $("#txtwdDate").val(v);
                    break;
            }
        }
    </script>

    <script type="text/javascript">
        function DelectFile(piccn, postfix) {
            var picName = piccn + "." + postfix;
            var v = $("#hf" + controlID).val();
            var k = v.replace(picName + "|", "");
            $("#hf" + controlID).val(k);
            $("#pic" + piccn).remove();
            //删除服务器上的文件
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "DeleteFileFromService.aspx",
                data: "picname=" + picName,
                dataType: 'json',
                success: function (result) {

                }
            })
        }

        var controlID;
        function uploadFile(id) {
            controlID = id;
            var files = document.getElementById(id).files;
            var cusID = $("#hfcusID").val();
            if (files.length > 0) {
                var fd = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fd.append(id, files[i]);
                }
                var xhr = new XMLHttpRequest();
                xhr.upload.addEventListener("progress", uploadProgress, false);
                xhr.addEventListener("load", uploadComplete, false);
                xhr.addEventListener("error", uploadFailed, false);
                xhr.addEventListener("abort", uploadCanceled, false);
                xhr.open("POST", "Upload.ashx?cusID=" + cusID);
                xhr.send(fd);
            }
            else {
                alert("请选择上传的图片");
            }
        }

        function uploadProgress(evt) {//上传中    
            if (evt.lengthComputable) {
                //document.getElementById('progressNumber').innerHTML = "文件上传中...";
            }
            else {
                document.getElementById('progressNumber').innerHTML = 'unable to compute';
            }
        }
        function uploadComplete(evt) {//上传完成    
            if (evt.target.responseText != "") {
                var h = "";
                var PCWebDomain = $("#hfPCWebDomain").val();
                var path = PCWebDomain + "UploadFile/CustomerFile/";
                var filepath = evt.target.responseText;
                var f = filepath.split('|');
                for (var i = 0; i < f.length; i++) {
                    if (f[i] != "") {
                        var ff = f[i].split('.');
                        h += "<div style='float:left;' id='pic" + ff[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + ff[0] + "','" + ff[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src='" + path + f[i] + "' /></div></div>";
                    }
                }
                $("#picBox").html(h);

                $("#hf" + controlID).val(evt.target.responseText);
            }
            else {
                alert("该文件上传失败");
            }
        }

        function uploadFailed(evt) {//上传失败    
            alert("There was an error attempting to upload the file.");
        }
        function uploadCanceled(evt) {//上传被取消    
            alert("The upload has been canceled by the user or the browser dropped the connection.");
        }
    </script>

    <style type="text/css">
        #ReportType
        {
            margin: 0 auto;
            width: 95%;
            text-align: center;
        }
        
        #ReportType div
        {
            float:left;
            width:33.33%;
            text-align:center;
            text-shadow: none;
            color:#3C96DE;
            border: 1px solid
        }
        
        .ReportTypeSelected
        {
            background-color:#3C96DE;
        }
        
        .ReportTypeSelected label
        {
             color:#fff;
        }
        
        .SumReportData
        {
            padding:10px;
        }
        
         .SumReportData div
        {
            float:left;
            text-align:center;
            color: #828181;
            padding-right:5px;
        }
        
        .cssMainPanel
        {
            height:100% !important;
        }
    </style>

</head>
<body style="background: #eee;">
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto;">
                   
                    <div id="divBusinessView" class="cssMainPanel" style=" width:100%;">
                        <table  class="CustomerTable" cellpadding="0" cellspacing="0" > 
                            <tr>
                                <td colspan="2" style="height:55px;">
                                    <div id="ReportType">
                                        <div class="ReportTypeSelected" style="border-top-left-radius: 7px;border-bottom-left-radius: 7px;border-right:0px;">
                                            <label>日报</label>
                                        </div>
                                        <div style="border-right:0px;">
                                            <label>周报</label>
                                        </div>
                                        <div  style="border-top-right-radius: 7px;border-bottom-right-radius: 7px;">
                                            <label>月报</label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    日期
                                </th>
                                <td>
                                   <%--<input type="date" id="txtwdDate"  placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd hh:mm")%>' />--%>
                                   <select id="txtwdDate" data-role="none"></select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                   <div class="SumReportData">
                                       <div>客户(<label runat="server" id="lblNewCustomer">0</label>);</div>
                                       <div>跟进记录(<label runat="server" id="lblNewFollow">0</label>);</div>
                                       <div>商机(<label runat="server" id="lblNewOpptunity">0</label>);</div>
                                       <div>商机金额(<label runat="server" id="lblNewOpptunityAmount">0</label>);</div>
                                       <div>合同(<label runat="server" id="lblNewBusiness">0</label>);</div>
                                       <div>合同金额(<label runat="server" id="lblNewBusinessAmount">0</label>);</div>
                                       <div>回款金额(<label runat="server" id="lblNewPayment">0</label>);</div>
                                       <div>线索(<label runat="server" id="lblNewClue">0</label>);</div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtwdTitle" cssClass="cssTextarea" TextMode="MultiLine" placeholder="请输入过去事项的总结"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtwdContent" cssClass="cssTextarea" TextMode="MultiLine" placeholder="请输入接下来的计划"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <script type="text/javascript">
                                        function ClickFile() {
                                            $("#followFile").click();
                                        }
                                    </script>
                                    <div onclick="javascript:ClickFile()">
                                        <img src="../@images/UploadPic.png" style=" width:50px;" />
                                    </div>
                                    <input type="file" data-role="none"  id="followFile" onchange="javascript:uploadFile('followFile')" style="opacity:0; display:none;" multiple/>
                                    <asp:hiddenfield runat="server" id="hffollowFile" />
                                    <div style="width:100%; float:left">
                                        <div id="picBox"></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    通知人
                                </th>
                                <th>
                                   <asp:DropDownList ID="ddlwdNotifier" data-role="none" runat="server"></asp:DropDownList>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="2" style=" padding-left:0px;">
                                    <div  class="OperatorButton" style="text-align: center;text-shadow:none; width:100%;">
                                        <a class="SavaBtn" href="javascript:SaveWorkDiary();" data-role="none">保存</a>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
    <asp:HiddenField ID="hfPCWebDomain" runat="server" />
    </form>
</body>
</html>
