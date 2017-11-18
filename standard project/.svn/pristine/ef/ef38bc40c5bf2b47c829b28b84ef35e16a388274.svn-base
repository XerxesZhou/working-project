<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerBusinessAdd.aspx.cs"
    Inherits="SmartSoft.MobileWeb.CustomerBusinessAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>商机转为合同</title>
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
    <script src="../scripts/BaseHelper.js"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
                            <div id="divCustomerBusinessEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            合同名称
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfcbID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入合同名称（必填）" ID="txtcbName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbBusinessTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同日期
                                        </th>
                                        <td>
                                            <input type="date" id="txtcbDate" placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同金额
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同金额" ID="txtcbTotalAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            业务员
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            合同描述
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入合同描述" TextMode="MultiLine" ID="txtcbRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            到期日期：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" class="inputDate" ID="txtcbExpiredDate"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            通知人:
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlcbNotifyOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div  class="OperatorButton" style="text-align: center;text-shadow:none; width:100%;">
                                    <a class="SavaBtn" href="javascript:AddCustomerBusinessInfo();" data-role="none">保存</a>
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
