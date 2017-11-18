<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLayoutViewPurview.aspx.cs"
    Inherits="SmartSoft.Web.Common.SetLayoutViewPurview" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置角色对应的页面视图/布局</title>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td valign="top">
                <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="Top" class="ToolBar" style="height: 50px;">
                        <td>
                            &middot;设置布局/视图
                            <%--<hr style="color: #79639D; size: 2px;" noshade />--%>
                        </td>
                        <td>
                            <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                <img src="../images/img_newclose.png" style="border: 0px; margin-bottom: 8px;" />关闭
                            </a>
                            <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" />保存
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
                    <%-- <tr align="left">
                            <td class="tdTitle" style="height: 30px">
                                &middot;设置布局/视图
                                <hr style="color: #79639D; size: 2px;" noshade />
                            </td>
                        </tr>--%>
                    <tr>
                        <td>
                            <fieldset>
                                <legend class="tdTitle">授权条件</legend>
                                <table style="width: 100%;" cellspacing="1" cellpadding="1" border="0">
                                    <tr>
                                        <td style="width: 65px">
                                            页面
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_PageName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 65px">
                                            控件
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_ControlID" AutoPostBack="True" runat="server" Width="160px"
                                                OnSelectedIndexChanged="ddl_ControlID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_ControlID"
                                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 65px">
                                            动作
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_ActionID" runat="server" Width="160px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddl_ActionID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            系统角色
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_ObjectID" AutoPostBack="True" runat="Server" Width="160px"
                                                OnSelectedIndexChanged="ddl_ObjectID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_ObjectID"
                                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="up" runat="server">
                                <ContentTemplate>
                                    <fieldset>
                                        <legend class="tdTitle">授权信息</legend>
                                        <asp:CheckBoxList ID="cbl_LvList" runat="server" RepeatColumns="2" RepeatDirection="horizontal">
                                        </asp:CheckBoxList>
                                    </fieldset>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_ObjectID" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_ControlID" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_ActionID" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
