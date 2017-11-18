<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SysRoleList.aspx.cs" Inherits="SmartSoft.Web.Common.SysRoleList" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统角色列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script language="javascript" type="text/javascript">
        function Add()
        {
            openwinsimp("SysRoleEdit.aspx",380,320);
        }
        
        function Edit()
        {
            try
            {
                var url = "SysRoleEdit.aspx?";
                CheckBoxOpenWindowHandle(url + "&RoleID=",380,320, "请选择一条需要修改的记录!");
            }
            catch(err)
            {
                displayErrorMsg("Edit", err)
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
        function SetRolePurview(roleId)
        {
             openwinsimp("SetObjectPurview.aspx?Type=Role&ID=" + roleId,900,640);
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                 <td>
                    &middot;角色列表
                </td>
                <td>
                    <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
                </td>
            </tr>
        </table>
        <asp:Panel ID="panel" runat="server" ScrollBars="Auto" Height="600px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridRole" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="200" AllowPaging="true" ShowHeader="True" DataKeyNames="RoleID" AllowSorting="True"
                        OnRowDataBound="GridRole_RowDataBound">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="RowA" />
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                                <ItemTemplate>
                                    <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "RoleID")%>'
                                        onclick='SingleCheckJs(this);' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoleName" HeaderText="角色名称" />
                            <asp:BoundField DataField="Remark" HeaderText="备注" />
                            <asp:TemplateField ItemStyle-Width="80px" ItemStyle-Height="16px" HeaderText="操作">
                                <ItemTemplate>
                                    <a href='javascript:SetRolePurview(<%# DataBinder.Eval(Container.DataItem, "RoleID")%>)'>功能权限</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Refresh" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btn_Refresh" runat="server" CssClass="hidden" OnClick="btn_Refresh_Click" />
    </form>
</body>
</html>
