<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageControlForm.aspx.cs" Inherits="SmartSoft.Web.Common.PageControlForm" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>����ҳ��ؼ�</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <style>
        lbclass img
        {
             margin-bottom:8px;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table class="ctable" style="width: 100%;" border="0"
            cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="Top" class="ToolBar" style="height:50px;">
                            <td>
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                                <%--<hr style="color: #79639D; size: 2px;" noshade />--%>
                            </td>
                            <td>
                                <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                    <img src="../images/img_newclose.png" style="border: 0px;width:30px;height:30px; margin-bottom:8px;" />�ر� </a>
                                    <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" />����
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="Top">
                <td valign="top">
                    <table class="ctable" style="width: 100%; height: 100%; margin: 0 auto;" cellspacing="1"
                        cellpadding="1" border="0">
                      <%--  <tr align="left">
                            <td colspan="2" class="tdTitle" style="height: 30px">
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                                <hr style="color: #79639D; size: 2px;" noshade />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>ҳ������</td>
                            <td><asp:Literal ID="lt_PageName" runat="Server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>ҳ��Url</td>
                            <td><asp:Literal ID="lt_url" runat="Server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>
                                �ؼ�����
                            </td>
                            <td>
                                <asp:TextBox ID="tb_ControlName" runat="server" BorderStyle="Groove"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_ControlName"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                ��ͼ/����
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbl_LayoutOrView" runat="Server" Width="152px" RepeatDirection="horizontal">
                                    <asp:ListItem Selected="True" Value="1">����</asp:ListItem>
                                    <asp:ListItem Value="2">��ͼ</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>����ϵͳ��</td>
                            <td><asp:DropDownList ID="ddl_TableID" runat="server" Width="300px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_TableID"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <asp:TextBox ID="tb_PageID" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="tb_ControlID" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
