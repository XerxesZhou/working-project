<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerTypeList.aspx.cs" Inherits="SmartSoft.Web.SystemConfig.CustomerTypeList" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户类型</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    
    

    <script type="text/javascript">
    function Edit()
    {
        try
        {
            CheckBoxOpenWindowHandle("CustomerTypeEditForm.aspx?ID=",650,300, "请选择一条需要修改的记录!");
        }
        catch(err)
        {
            displayErrorMsg("Modify", err)
        }
    }
    
    function Delete()
    {
        return CheckBoxMultiHandle('确定要删除吗？');
    }
    
    function Refresh()
    {
        __doPostBack('ToolBar1$lb_Search','');
    }

    function Add()
    {
        openwinsimp("CustomerTypeEditForm.aspx",650,300);
    }
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');__doPostBack('ToolBar1$lb_Search','');" style="overflow:visible;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="man1" runat="server"></asp:ScriptManager>
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar" style="height:50px;">
                <td>
                    &middot;客户类型
                </td>
                <td>
                    <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerType" runat="server"
                CssClass="Grid" AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="GridCustomerType_RowDataBound">
                <FooterStyle CssClass="GridFooter" />
                <RowStyle CssClass="Row" />
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="RowA" />
                <PagerSettings Visible="false" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                        <ItemTemplate>
                            <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "ctID")%>'
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
                    <asp:BoundField DataField="ctName" HeaderText="名称" />
                    <asp:BoundField DataField="ctProtectDays" HeaderText="保护天数" />
                    <asp:BoundField DataField="ctIncreaseProtectDays" HeaderText="可延保天数" />
                    <asp:BoundField DataField="ctOrderBy" HeaderText="顺序" />
                    <asp:BoundField DataField="ctRemark" HeaderText="备注" />
                </Columns>
            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Delete" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
