<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeView.aspx.cs"
    Inherits="SmartSoft.Web.Data.KnowledgeView" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=title %>
    </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="TableMain" cellpadding="0" cellspacing="0">
        <tr class="ToolBar" style="height:50px;">
             <td>
                &middot;<%=title %>
            </td>
            <td>
                <toolBar:ToolBar ID="ToolBar1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="MainPanel" runat="server">
                </asp:Panel>
                <table class="TablePanel" border="0" id="Table1">
                    <tr>
                        <th>
                            主题：
                        </th>
                        <td style="width: 120px;">
                            <asp:Label runat="server" ID="lbl_Theme"></asp:Label>
                        </td>
                         <th>
                            操作时间：
                        </th>
                        <td style="width: 120px;">
                            <asp:Label runat="server" ID="lbl_OperateDate"></asp:Label>
                        </td>
                        <th>
                            类别：
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbl_Type"></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>
                        <th>
                            内容：
                        </th>
                        <td colspan="5">
                            <asp:Literal runat="server" ID="lit_Content"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            附件：
                        </th>
                        <td colspan="5">
                          <asp:Literal runat="server" ID="ltl_wendang"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
