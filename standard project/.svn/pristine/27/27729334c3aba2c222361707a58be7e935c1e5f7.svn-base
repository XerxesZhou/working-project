<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SysRoleEdit.aspx.cs" Inherits="SmartSoft.Web.Common.SysRoleEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统角色编辑窗口</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <style>
        .lbclass
        {
             margin-right:5px;
            }
        .lbclass img
        {
            margin-bottom: 8px;
        }
        #edittable tr
        {
            line-height: 2.5em;
        }
        .input
        {
            line-height: 20px;
            border: solid 1px rgb(204,204,204);
            width: 160px;
        }
        .inp_Remark
        {
             line-height: 20px;
            border: solid 1px rgb(204,204,204);
            }
            html
            {
                 overflow:auto;
                }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table class="ctable" style="width:100%; height: 200px; margin: 0 auto; border-style:none;" border="0"
            cellpadding="0" cellspacing="0">
            <tr style="height: 20px">
                <td style="height: 20px; width: 100%; background-color:White;">
                    <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="Top" class="ToolBar" style="height:30px;">
                            <td>
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                            </td>
                            <td>
                                <a id="lb_Search" href="#" onclick="javascript:window.close();">
                                    <img src="../images/img_newclose.png" style="border: 0px;width:15px;height:15px; margin-bottom:8px; margin-right:5px;" />关闭 </a>
                                <asp:LinkButton ID="lb_Add" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" style=" height:15px" />保存
                                </asp:LinkButton>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="Top">
                <td valign="top" style="width: 100%">
                    <table class="ctable" id="CssStyle" style="width: 100%; height: 100%; margin: 0 auto; border:1px solid #ccc;" cellspacing="1"
                        cellpadding="1" border="0">
                       <%-- <tr align="left">
                            <td colspan="2" class="tdTitle" style="height: 25px">
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdFont"  style="width: 114px; text-align:right;">
                                角色名称：</td>
                            <td>
                                <asp:TextBox ID="tb_RoleName" runat="Server"  CssClass="input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_RoleName"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                       <%-- <tr>
                            <td class="tdFont" align="center" style="width: 114px">
                                销售报价最低折扣率</td>
                            <td>
                                <asp:TextBox ID="tb_SellDiscount" runat="Server" BorderStyle="groove"></asp:TextBox>
                        </tr>
                        <tr>
                            <td class="tdFont" align="center" style="width: 114px">
                                客户最大信用额度</td>
                            <td>
                                <asp:TextBox ID="tb_CustomerMaxCreditAMT" runat="Server" BorderStyle="groove"></asp:TextBox>
                        </tr>
                        <tr>
                            <td class="tdFont" align="center" style="width: 114px">
                                客户最大欠款额</td>
                            <td>
                                <asp:TextBox ID="tb_CustomerMaxAMT" runat="Server" BorderStyle="groove"></asp:TextBox>
                        </tr>--%>
                        <tr>
                            <td class="tdFont" style="width: 114px; text-align:right;">
                                备&nbsp;&nbsp;注：</td>
                            <td>
                                <asp:TextBox ID="tb_Remark" TextMode="multiLine" runat="Server"  CssClass="inp_Remark"
                                    Height="50px" Width="99%"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="tb_RoleID" runat="server" Visible="false">0</asp:TextBox>
    </form>
</body>
</html>
