<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageDetail.aspx.cs" Inherits="SmartSoft.Web.Common.MessageDetail" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统消息</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="ctable" style="width: 100%; margin: 0 auto;" border="0"
            cellpadding="0" cellspacing="0">
            <tr style="height: 25px">
                <td style="height: 25px">
                    <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="Top">
                            <td align="left" class="TopBorder" style="height: 20px">
                                <a href="#" onclick="javascript:window.close();">
                                    <img src="../@images/close.gif" style="border: 0px" alt="" />关闭 </a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="Top">
                <td valign="top">
                    <asp:Literal ID="lt_Content" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
