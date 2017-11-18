<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerClueAdd.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerClueAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增线索</title>
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
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function pageSetRight() {
            dd.biz.navigation.setRight({
                show: true, //控制按钮显示， true 显示， false 隐藏， 默认true
                control: true, //是否控制点击事件，true 控制，false 不控制， 默认false
                text: '扫名片', //控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    dd.biz.util.scanCard({ // 无需传参数
                        onSuccess: function (data) {
                            $("#txtccName").val(data.NAME);
                            $("#txtccCustomerName").val(data.COMPANY);
                            $("#txtccTel").val(data.PHONE);
                            $("#txtccMobile").val(data.MPHONE);
                            $("#txtccAddress").val(data.ADDRESS);
                            $("#txtccRemark").val(data.POSITION);
                        },
                        onFail: function (err) {
                        }
                    })
                },
                onFail: function (err) { }
            });
        }

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

                        <div>                        
                            <div id="divCustomerClueEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            联系人
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfccID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入联系人（必填）" ID="txtccName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            公司名称
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入公司名称" ID="txtccCustomerName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            相关活动
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccActivityID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtccTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            手机
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入手机" ID="txtccMobile"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            状态
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccStatusID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入备注" TextMode="MultiLine" ID="txtccRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <th>
                                            地址
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtccAddress" TextMode="MultiLine" placeholder="请输入地址">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <th>
                                            部门
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                                <div class="OperatorButton" style="text-align: center;text-shadow:none; width:100%">
                                            <a class="SavaBtn" href="javascript:AddCustomerClue();" data-role="none">保存</a>
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
