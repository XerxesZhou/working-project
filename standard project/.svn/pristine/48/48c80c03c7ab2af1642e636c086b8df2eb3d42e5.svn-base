<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectAdd.aspx.cs" Inherits="SmartSoft.MobileWeb.data.ProjectAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>新增报备</title>
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
        $(function () {
            var h = window.innerHeight;
            $(".cssMainPanel").css("height", h);

            var currentOperatorID = $("#hfCurrentOperatorID").val();
            $("#ddlcusOperatorID").val(currentOperatorID);
            $("#ddlcfhOperatorID").val(currentOperatorID);
            $("#ddlcoOperatorID").val(currentOperatorID);
            $("#ddlcbOperatorID").val(currentOperatorID);
            $("#ddlcrOperatorID").val(currentOperatorID);
            $("#ddlcfpOperatorID").val(currentOperatorID);
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
                            <div id="divProjctEdit" class="cssMainPanel" style="width:100%;">
                                <table cellpadding="0" cellspacing="0">
                                     <tr>
                                        <th>
                                            项目名称
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入项目名称（必填）" ID="txtpjName"></asp:TextBox>
                                        </td>
                                    </tr>
                                   
                                   
                                    <tr>
                                        <th>
                                            公司名称
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入公司名称" ID="txtpjCompanyName"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            备案号
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入备案号" ID="txtpjNO"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            项目配置
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入项目配置" ID="txtpjDetail"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            产品型号
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入产品型号" ID="txtpjProduct"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            项目金额
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入项目金额" ID="txtpjAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系人
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入联系人" ID="txtpjContactor"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            价格详情
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入价格详情" ID="txtpjPrice"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            申请人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlpjOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            到期日期
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" type="date" placeholder="请输入到期日期" ID="txtpjExpiredDate"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            通知人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="pjToApproveOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtpjRemark" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div  class="OperatorButton" style="text-align: center;text-shadow:none; width:100%;">
                                    <a class="SavaBtn" href="javascript:AddProject();" data-role="none">保存</a>
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
