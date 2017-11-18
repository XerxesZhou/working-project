<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DefineCommonList.aspx.cs"
    Inherits="SmartSoft.Web.SystemConfig.DefineCommon" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=TableText%>
        列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script src="../@Scripts/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../@Scripts/window.js"></script>
    
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" src="../@Scripts/CheckBox.js" charset="gb2312"></script>

    <script type="text/javascript">

    function Edit()
    {
        try
        {
            var TableName = getUrlParameter("TableName");
            var TableText = getUrlParameter("TableText");
            var url = "DefineCommonEdit.aspx?Action=Update";
            if (TableName != null)
            {
                url += "&TableName=" + TableName;
            }
            if (TableText != null) {
                url += "&TableText=" + TableText;
            }
            CheckBoxOpenWindowHandle(url + "&ID=",650,350, "请选择一条需要修改的记录!");
        }
        catch(err)
        {
            displayErrorMsg("Modify", err)
        }
    }
    
    function Delete()
    {
        return CheckBoxMultiHandle('确定要删除吗？\r\n当该选项已用时，该选项删除后置为停用!');
    }
    
    function Resume()
    {
        return CheckBoxMultiHandle('确定要启用吗？');
    }
    function Add()
    {
        var TableName = getUrlParameter("TableName");
        var TableText = getUrlParameter("TableText");
        var url = "DefineCommonEdit.aspx?Action=Insert";
        if (TableName != null)
        {
            url += "&TableName=" + TableName;
        }
        if (TableText != null) {
            url += "&TableText=" + TableText;
        }
        openwinsimp(url,700,350);
    }
    function Refresh()
    {
        __doPostBack('ToolBar1$lb_Search','');
    }
    
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');__doPostBack('ToolBar1$lb_Search','');" style="overflow:visible;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="man1" runat="server"></asp:ScriptManager>
        <table class="ListHeader" id="ListHeader" cellspacing="0" cellpadding="0">
            <tr class="ToolBar">
                 <td>
                    &middot;<%=TableText%>列表
                </td>
                <td>
                    <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridDefCommon" OnRowDataBound="GridDefCommon_RowDataBound" runat="server"
                CssClass="Grid" AutoGenerateColumns="False" AllowSorting="True">
                <FooterStyle CssClass="GridFooter" />
                <RowStyle CssClass="Row" />
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="RowA" />
                <PagerSettings Visible="false" />
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" />
                    <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                        <ItemTemplate>
                            <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                onclick='SingleCheckJs(this);' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" InsertVisible="False">
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle HorizontalAlign="Center" Width="5%" />
                     <ItemTemplate>
                      <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="名称" />
                    <asp:BoundField DataField="OrderBy" HeaderText="排序" />
                    <asp:BoundField DataField="Remark" HeaderText="备注" />
                </Columns>
            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Delete" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Resume" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
