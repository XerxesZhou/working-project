<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tool.aspx.cs" Inherits="SmartSoft.MobileWeb.Tool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
    <script type="text/javascript">
        function convertToRedirectURL() {
            var url = document.getElementById("txtURL").value;

            var newUrl = encodeURIComponent(url);
            var reNewUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=5&redirect_uri=" + newUrl + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect";
            document.getElementById("txtRedirectURL").value = reNewUrl;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <th>
                    URL：
                </th>
                <td>
                    <asp:TextBox runat="server" ID="txtURL" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Redirct_URL：
                </th>
                <td>
                    <asp:TextBox runat="server" ID="txtRedirectURL" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="转换成重定向URL" onclick="javascript:convertToRedirectURL()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
