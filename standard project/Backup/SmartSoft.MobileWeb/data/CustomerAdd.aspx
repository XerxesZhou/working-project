<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增客户</title>
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
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function getPosition() {
            var address = $("#txtcusAddress").val();
            if (address != "") {
                var myGeo = new BMap.Geocoder();
                // 将地址解析结果显示在地图上,并调整地图视野
                myGeo.getPoint(address, function (point) {
                    if (point) {
                        $("#txtcusLongtitude").val(point.lng);
                        $("#txtcusLatitude").val(point.lat);
                    } else {
                        alert("您输入的地址没有解析到结果!请重新输入。");
                        $("#txtcusAddress").select();
                    }
                }, "深圳市");
            }
        }

        $(function () {
            var currentOperatorID = $("#hfCurrentOperatorID").val();
            $("#ddlcusOperatorID").val(currentOperatorID);
            $("#ddlcfhOperatorID").val(currentOperatorID);
            $("#ddlcoOperatorID").val(currentOperatorID);
            $("#ddlcbOperatorID").val(currentOperatorID);
            $("#ddlcrOperatorID").val(currentOperatorID);
            $("#ddlcfpOperatorID").val(currentOperatorID);
        })

        function pageSetRight() {
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '扫名片', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    dd.biz.util.scanCard({ // 无需传参数
                        onSuccess: function (data) {
                            $("#txtcusContactor").val(data.NAME + data.POSITION);
                            $("#txtcusName").val(data.COMPANY);
                            $("#txtcusTel").val(data.PHONE);
                            $("#txtcusMobile").val(data.MPHONE);
                            $("#txtcusAddress").val(data.ADDRESS);
                            if (data.ADDRESS && data.ADDRESS + "" != "") {
                                getPosition();
                            }
                        },
                        onFail: function (err) {
                        }
                    })
                },
                onFail: function (err) { }
            });
        }
    </script>

    <style type="text/css">
        .lbl {
          position: relative;
          display: block;
          height: 20px;
          width: 44px;
          background: #898989;
          border-radius: 100px;
          cursor: pointer;
          transition: all 0.3s ease;
        }
        .lbl:after {
          position: absolute;
          left: -2px;
          top: -3px;
          display: block;
          width: 26px;
          height: 26px;
          border-radius: 100px;
          background: #fff;
          box-shadow: 0px 3px 3px rgba(0,0,0,0.05);
          content: '';
          transition: all 0.3s ease;
          border:1px #eee solid;
        }
        .lbl:active:after {
          transform: scale(1.15, 0.85);
        }
        .cbx:checked ~ label {
          background: #5E97F6;
        }
        .cbx:checked ~ label:after {
          left: 20px;
          background: #5E97F6;
        }
        .cbx:disabled ~ label {
          background: #d5d5d5;
          pointer-events: none;
        }
        .cbx:disabled ~ label:after {
          background: #bcbdbc;
        }

        .hidden {
          display: none;
        }
        
        .trIsShowOpptunity
        {
            display:none;
        }
    
    </style>

    <script type="text/javascript">
        function CheckCheckBox() {
            var TOF = $("#unchecked").is(":checked");
            if (TOF == false) {
                $(".trIsShowOpptunity").show();

            }
            else {
                $(".trIsShowOpptunity").hide();
            }

        }

        $(function () {
            var OpptunityID = $("#hfOpptunityID").val();
            if (OpptunityID != "") {
                $("#trOpptunity").hide();
                $(".trIsShowOpptunity").hide();
            }
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="height: 500px;">
                    <div data-role="content" style="padding: 0px;background-color: #fff;">
                    <asp:HiddenField ID="hfCurrentOperatorID" runat="server"/>
                    <asp:HiddenField ID="hfOpptunityID" runat="server"/>
                    <asp:HiddenField ID="hfClueID" runat="server" Value="0"/>
                        <div>                        
                            <div id="divCustomerEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            名称
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcusID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入名称（必填）" ID="txtcusName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            编号
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入编号" ID="txtcusCN"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusKindID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            来源
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusSourceID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            性质
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusExtType2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            区域
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusAreaID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            简介
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入简介" TextMode="MultiLine" ID="txtcusIntroduction"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系人
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入联系人" ID="txtcusContactor"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            性别
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlLinkManSex">
                                                <asp:ListItem Value="男" Text="男"></asp:ListItem>
                                                <asp:ListItem Value="女" Text="女"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtcusTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            手机
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入手机" ID="txtLinkManMobile"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr runat="server" id="trOpptunity">
                                        <th>
                                            设置商机
                                        </th>
                                        <td>
                                            <div>
                                                <input type="checkbox" id="unchecked" class="cbx hidden" data-role="none"/>
                                                <label for="unchecked" id="SetOpptunity" style=" float:right;" class="lbl" onclick="javascript:CheckCheckBox()" data-role="none"></label>
                                            </div>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr class="trIsShowOpptunity">
                                        <th>
                                            商机
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入商机" ID="txtOpptunity"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr class="trIsShowOpptunity">
                                        <th>
                                            商机阶段
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcoPhaseID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="trIsShowOpptunity">
                                        <th>
                                            预计金额
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计金额" ID="txtcoPredictAmount" CssClass="inputNumber2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="trIsShowOpptunity">
                                        <th>
                                            预计成交
                                        </th>
                                        <td>
                                            <input type="date" id="txtcoPredictFinishDate" placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            地址
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtcusAddress" TextMode="MultiLine" placeholder="请输入地址"
                                                onblur="javascript:getPosition();">
                                            </asp:TextBox>
                                            <asp:TextBox runat="server" CssClass="Address" ID="txtcusLongtitude" style="display:none"></asp:TextBox>
                                            <asp:TextBox runat="server" CssClass="Address" ID="txtcusLatitude" style="display:none"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcusOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div  class="OperatorButton" style="text-align: center;text-shadow:none; width:100%;">
                                    <a class="SavaBtn" href="javascript:AddCustomerInfo();" data-role="none">保存</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
