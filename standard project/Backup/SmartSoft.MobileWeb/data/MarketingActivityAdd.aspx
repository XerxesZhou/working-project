<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketingActivityAdd.aspx.cs"
    Inherits="SmartSoft.MobileWeb.MarketingActivityAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增市场活动</title>
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
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            var h = window.innerHeight;
            $(".cssMainPanel").css("height", h);
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
                            <div id="divCustomerEdit" class="cssMainPanel" style="width:100%;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            活动名称
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfmaID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入名称（必填）" ID="txtmaName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            开始时间
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtmaStartDate" placeholder="请选择日期" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            结束时间
                                        </th>
                                        <td>
                                            <input type="text" readonly="readonly" onclick="javascript:SelectDate(this)" id="txtmaEndDate" placeholder="请选择日期" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            市场活动类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            描述
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入描述" TextMode="MultiLine" ID="txtmaDescription"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计成本
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计成本" ID="txtmaPredictCost"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计收入
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计收入" ID="txtmaPredictAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际成本
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入实际成本" ID="txtmaActualCost"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计人数
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计人数" ID="txtmaPredictNum"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际人数
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入实际人数" ID="txtmaActualNum"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            备注
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" TextMode="MultiLine"  placeholder="请输入备注" ID="txtmaRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div  class="OperatorButton" style="text-align: center;text-shadow:none; width:100%;">
                                    <a class="SavaBtn" href="javascript:AddMarketingActivityInfo();" data-role="none">保存</a>
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
