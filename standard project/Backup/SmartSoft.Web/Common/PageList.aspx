<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PageList.aspx.cs" Inherits="SmartSoft.Web.Common.PageList" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��ͼ/����Ȩ������</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    
</head>
<body onload="javascript:SetGridViewScrolling('up1');">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader">
                <tr>
                    <td class="TableSearchHeader">
                        &middot;��ͼ/����Ȩ�����ò�ѯ����
                    </td>
                    <td colspan="4">
                        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
                            <tr class="ToolBar">
                                <td align="left" style="height: 25px">
                                    <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <progressBar:ProgressBar ID="Progress1" runat="server"></progressBar:ProgressBar>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
            <tbody id="searchMain">
                <tr>
                    <th>
                        ���ͣ�
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchType" runat="Server">
                            <asp:ListItem Text="�˵�" Value="menu" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="������" Value="toolbar"></asp:ListItem>
                            <asp:ListItem Text="����" Value="other"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        ģ�飺
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchModel" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridPageList" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" ShowHeader="True" DataKeyNames="PageID,IsMenuDirectory,IsToolBarDirectory"
                    AllowSorting="True" OnRowCreated="GridPageList_RowCreated">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <Columns>
                        <asp:BoundField DataField="PageID" Visible="false" />
                        <asp:BoundField DataField="ParentPageName" HeaderText="�����ڵ�" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="PageName" HeaderText="ҳ������" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="PageFilePath" HeaderText="����·��" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="����" />
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
