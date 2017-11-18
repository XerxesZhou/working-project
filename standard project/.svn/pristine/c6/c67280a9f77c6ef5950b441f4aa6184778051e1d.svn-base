<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SysPageList.aspx.cs" Inherits="SmartSoft.Web.Common.SysPageList" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统页面设置</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <style type="text/css">
    #up1
    {
        height:87% !important;
        }
    
    </style>
    <script type="text/javascript">
    function AddSubFunction (pageID)
    {
        try
        {
            CheckBoxOpenWindowHandle("SysPageEdit.aspx?parentId=",1100,600, "请选择一条需要增加的记录!");
        }
        catch(err)
        {
            displayErrorMsg("AddSubFunction", err)
        }
    }
    
    function Add()
    {
        try
        {
            openwinsimpscroll("SysPageEdit.aspx",1100,600);
        }
        catch(err)
        {
            displayErrorMsg("Add", err)
        }
    }
    
    function Modify()
    {
        try
        {
            CheckBoxOpenWindowHandle("SysPageEdit.aspx?PageID=",1100,600, "请选择一条需要修改的记录!");
        }
        catch(err)
        {
            displayErrorMsg("Modify", err)
        }
    }
    
    function DeletePage()
    {
        if (window.confirm("确定删除?")) {
            document.getElementById("btnDeletePage").click();
        }
    }

    function SetSearchFlag() {
    }
    function ClearSelected() {
    }
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader">
                <tr>
                    <td class="TableSearchHeader">
                        &middot;系统页面设置查询条件
                    </td>
                    <td  colspan="4">
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
                        类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchType" runat="Server">
                            <asp:ListItem Text="菜单" Value="menu" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="编辑页" Value="other"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        模块：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchModel" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridsysPage" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="20" AllowPaging="false" ShowHeader="True" DataKeyNames="PageID" AllowSorting="True"
                        OnRowDataBound="GridsysPage_RowDataBound" OnRowCreated="GridsysPage_RowCreated">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="RowA" />
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                                <ItemTemplate>
                                    <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "PageID")%>'
                                        onclick='SingleCheckJs(this);' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PageID" HeaderText="编号" />
                            <asp:BoundField DataField="ParentPageName" HeaderText="所属节点" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="PageName" HeaderText="名称" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="PageFilePath" HeaderText="访问路径" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="MenuOrderBy" HeaderText="菜单排序" ItemStyle-HorizontalAlign="left" />
                            <asp:TemplateField HeaderText="是否菜单" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#bool.Parse(Eval("IsMenu").ToString()) ? "√" : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否菜单节点" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#bool.Parse(Eval("IsMenuDirectory").ToString()) ? "√" : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="left" />
                        <PagerSettings Visible="false" />
                    </asp:GridView>
            </ContentTemplate>      
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField runat="server" ID="hfSelectedItems" />
        <asp:Button runat="server" ID="btnDeletePage" CssClass="Hidden" OnClick="btnDeletePage_Click"/>
    </form>
</body>
</html>
