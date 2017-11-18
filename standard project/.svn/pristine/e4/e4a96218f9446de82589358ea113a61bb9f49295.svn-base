<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ObjectList.aspx.cs" Inherits="SmartSoft.Web.Common.ObjectList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>对象列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <link href="../@Css/tabStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <ComponentArt:TabStrip ID="TabStrip1" CssClass="TopGroup" SiteMapXmlFile="../@Xml/tabData.xml"
            DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
            DefaultDisabledItemLookId="DisabledTabLook" DefaultGroupTabSpacing="1" ImagesBaseUrl="../@images/"
            MultiPageId="MultiPage1" runat="server">
            <ItemLooks>
                <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="DefaultTab" HoverCssClass="DefaultTabHover"
                    LabelPaddingLeft="10" LabelPaddingRight="10" LabelPaddingTop="5" LabelPaddingBottom="4"
                    LeftIconUrl="tab_left_icon.gif" RightIconUrl="tab_right_icon.gif" HoverLeftIconUrl="hover_tab_left_icon.gif"
                    HoverRightIconUrl="hover_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21"
                    RightIconWidth="3" RightIconHeight="21" />
                <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="SelectedTab" LabelPaddingLeft="10"
                    LabelPaddingRight="10" LabelPaddingTop="4" LabelPaddingBottom="4" LeftIconUrl="selected_tab_left_icon.gif"
                    RightIconUrl="selected_tab_right_icon.gif" LeftIconWidth="3" LeftIconHeight="21"
                    RightIconWidth="3" RightIconHeight="21" />
            </ItemLooks>
        </ComponentArt:TabStrip>
        <ComponentArt:MultiPage ID="MultiPage1" CssClass="MultiPage" runat="server" Width="100%"
            Height="500px">
            <ComponentArt:PageView CssClass="PageContent" runat="server">
                <table class="ListHeader" id="ListHeader">
                    <tr class="ListTitle">
                        <td>
                            &middot;角色列表
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridRole" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                    PageSize="99999" AllowPaging="False" ShowHeader="True" DataKeyNames="RoleID" AllowSorting="True"
                    OnRowCreated="GridRole_RowCreated">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <PagerSettings Visible="false" />
                    <Columns>
                        <asp:BoundField DataField="RoleID" Visible="false" />
                        <asp:BoundField DataField="RoleName" ItemStyle-Width="120px" HeaderText="角色名称" />
                        <asp:BoundField DataField="Remark" HeaderText="备注" />
                        <asp:BoundField HeaderText="操作" />
                    </Columns>
                </asp:GridView>
            </ComponentArt:PageView>
            <%--<ComponentArt:PageView CssClass="PageContent" runat="server">
                <table class="ListHeader" id="ListHeader">
                    <tr class="ListTitle">
                        <td>
                            &middot;部门列表
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridDepartment" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                    PageSize="99999" AllowPaging="False" ShowHeader="True" DataKeyNames="depID" AllowSorting="True"
                    OnRowCreated="GridDepartment_RowCreated">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <PagerSettings Visible="false" />
                    <Columns>
                        <asp:BoundField DataField="depID" Visible="False" />
                        <asp:BoundField DataField="depName" ItemStyle-Width="120px" HeaderText="名称" />
                        <asp:BoundField DataField="depFunction" HeaderText="职能" />
                        <asp:BoundField DataField="depRemark" HeaderText="备注" />
                        <asp:BoundField HeaderText="操作" />
                    </Columns>
                </asp:GridView>
            </ComponentArt:PageView>--%>
            <ComponentArt:PageView CssClass="PageContent" runat="server">
                <table class="ListHeader" id="ListHeader">
                    <tr class="ListTitle">
                        <td>
                            &middot;操作员列表
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panel" runat="server" ScrollBars="Auto" Height="600px">
                    <asp:GridView ID="GridOperator" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="99999" AllowPaging="False" ShowHeader="True" DataKeyNames="opeID" AllowSorting="True"
                        OnRowCreated="GridOperator_RowCreated">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="RowA" />
                        <PagerSettings Visible="false" />
                        <Columns>
                            <asp:BoundField DataField="opeID" Visible="False" />
                            <asp:BoundField DataField="opeName" HeaderText="登录名" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="部门" />
                            <asp:BoundField HeaderText="操作" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ComponentArt:PageView>
        </ComponentArt:MultiPage>
    </form>
</body>
</html>
