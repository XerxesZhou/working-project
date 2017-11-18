<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DepartmentEdit.aspx.cs"
    Inherits="SmartSoft.Web.Common.DepartmentEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门信息编辑</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <style>
        .lbclass img
        {
            margin-bottom: 8px;
        }
        html
        {
            overflow: auto;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table class="ctable" style="width: 100%; height: 300px; margin: 0 auto;" border="0"
        cellpadding="0" cellspacing="0">
        <tr style="height: 25px">
            <td style="height: 25px">
                <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="Top" class="ToolBar" style="height: 30px;">
                        <td>
                            &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                            <%-- <hr style="color: #79639D; size: 2px;" noshade />--%>
                        </td>
                        <td>
                            <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                <img src="../images/img_newclose.png" style="border: 0px; width: 15px; height: 15px;
                                    margin-bottom: 8px;" />关闭 </a>
                            <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0"  style=" height:15px;" />保存
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="Top">
            <td valign="top">
                <table class="ctable" style="width: 100%; height: 100%; border-collapse:separate; border-spacing:0px 10px; margin: 0 auto;" cellspacing="1"
                    cellpadding="1" border="0">
              
                    <tr>
                        <td class="tdFont" align="center">
                            部门名称
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depName" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_depName"
                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            负责人
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depChargeMan" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            部门电话
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depTel" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            部门传真
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depFax" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            合同额
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depOrderAmount" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            收款额
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depReceiptAmount" runat="Server" BorderStyle="groove" Width="265px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdFont" align="center">
                            备注
                        </td>
                        <td>
                            <asp:TextBox ID="tb_depRemark" runat="Server" BorderStyle="groove" Width="100%"
                                TextMode="MultiLine" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 100%">
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="btn_depID" runat="server" Visible="false">0</asp:TextBox>
    </form>
</body>
</html>
