<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="SmartSoft.Web.top" %>
<html>
<head runat="server">
    <title>企业信息化管理平台</title>
    <script type="text/javascript" src="@Scripts/window.js"></script>
    <script type="text/javascript">
        function logout()
        {
            window.parent.location.href="login.aspx";
        }
    </script>
</head>
<body onselectstart="return false" ondragstart="return false" leftmargin="0" topmargin="0"
    oncontextmenu="return false">
    <form id="form1" runat="server">
        <span>
            <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td>
                            <table height="30" cellspacing="0" cellpadding="0" width="100%" border="0" style="background-image:url(@images/logo4.jpg); background-repeat:no-repeat;">
                                <tbody>
                                    <tr>
                                         <td width="500" align="right" valign="bottom">
                                        </td>
                                        <td style="padding-right:20px" align="right">
                                            <span style="font-size: 11px"><asp:Literal ID="lt_Operator" runat="server"></asp:Literal></span> &nbsp; &nbsp; &nbsp; &nbsp;
                                            <span style="font-size: 13px; margin-right:0; margin-left:auto;">
                                               <%-- <a href="UserManual.doc" style="color:Gray;" target="blank">用户手册</a>&nbsp;&nbsp;|&nbsp;&nbsp;--%>
                                                <a href="main/MyDesk.aspx" style="color:Gray;" target="main">主页</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                <%--<a href="#" style="color:Gray;" onclick="openwinsimp('Common/SwitchBranch.aspx',300,150)">切换公司</a>&nbsp;&nbsp;|&nbsp;&nbsp;--%>
                                                <a style="color:Gray;"  href="personal/ChangPwd.aspx" target="main">更改密码</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                <asp:LinkButton ID="lb_logout" ForeColor="gray" runat="server" OnClick="lb_logout_Click">注销</asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                <a href="#" style="color:Gray;" onclick="window.parent.close();">退出</a>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </span>
    </form>
</body>
</html>
