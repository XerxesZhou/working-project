<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OperatorList.aspx.cs" Inherits="SmartSoft.Web.Common.OperatorList" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>操作员列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>

    <script src="../@Scripts/webCalendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
  
        function Add()
        {
            openwinsimp("OperatorEdit.aspx",720,550);
        }
        
        function Edit()
        {
            try
            {
                var url = "OperatorEdit.aspx?";
                CheckBoxOpenWindowHandle(url + "&opeID=", 720, 550, "请选择一条需要修改的记录!");
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
        function SetRolePurview(opeid) {
            openwinsimp("SetObjectPurview.aspx?Type=Ope&ID=" + opeid, 900, 640);
        }
    </script>
 
</head>
<body onload="javascript:SetGridViewScrolling('UpdatePanel1');" style="overflow:visible;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
        <tbody id="searchHeader" title="隐藏查询条件">
            <tr>
                <td class="TableSearchHeader" onclick="showHideSearchCondition();">
                    查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                </td>
                <td colspan="8">
                    <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
                        <tr class="ToolBar">
                            <td>
                            </td>
                            <td>
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
                        关键字：
                    </th>
                    <td>
                        <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                    </td>

                    <th>
                        部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchopeDepartmentID" runat="server"></asp:DropDownList>
                    </td>

                    
                    <th>
                        数据权限:
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchopeDataRange" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="仅本人" Value="1"></asp:ListItem>
                            <asp:ListItem Text="本部门" Value="2"></asp:ListItem>
                            <asp:ListItem Text="全公司" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                         <input type="button" onclick='__doPostBack("btn_Refresh","")' value="查询" style="width:60px;" class="btn_name"/>
                    </td> 
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridOperator" CssClass="Grid" runat="server" GridLines="Both" AutoGenerateColumns="False"
                        PageSize="20" AllowPaging="False" ShowHeader="True" DataKeyNames="opeID" AllowSorting="True"
                        OnRowDataBound="GridOperator_RowDataBound" OnSorting="grid_Sorting">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="RowA" />
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                                <ItemTemplate>
                                    <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "opeID")%>'
                                        onclick='SingleCheckJs(this);' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="opeName" HeaderText="登录名" SortExpression="opeName"/>
                             <asp:TemplateField HeaderText="部门" SortExpression="opeDepartmentID">
                                <ItemTemplate>
                                    <%# GetDepartment(Eval("opeDepartmentID") + "" == "" ? "0" : Eval("opeDepartmentID") + "")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否管理员" SortExpression="opeIsAdmin">
                                <ItemTemplate>
                                    <%# bool.Parse(Eval("opeIsAdmin").ToString()) ? "√" : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否可登录" SortExpression="opeIsCanLogin">
                                <ItemTemplate>
                                    <%# bool.Parse(Eval("opeIsCanLogin").ToString()) ? "√" : "" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="数据权限" SortExpression="opeDataRange">
                                <ItemTemplate>
                                    <%# GetDataRangeName(Eval("opeDataRange") + "")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="opeMobile" HeaderText="手机号" SortExpression="opeMobile"/>
                            <asp:BoundField DataField="opeDingDingUserID" HeaderText="钉钉用户ID" SortExpression="opeDingDingUserID"/>
                            <asp:BoundField DataField="opeAddDate" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:d}" SortExpression="opeAddDate" />
                            <asp:BoundField DataField="opeModifyDate" HeaderText="最后修改日期" HtmlEncode="false" DataFormatString="{0:d}"  SortExpression="opeModifyDate"/>
                            <asp:TemplateField ItemStyle-Width="80px" ItemStyle-Height="16px" HeaderText="操作">
                                <ItemTemplate>
                                    <a href='javascript:SetRolePurview(<%# DataBinder.Eval(Container.DataItem, "opeID")%>)'>功能权限</a>
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
        <asp:Button ID="btn_Refresh" runat="server" CssClass="hidden" OnClick="btn_Refresh_Click" />
        <asp:TextBox ID="tb_depID" runat="server" CssClass="hidden"></asp:TextBox>
    </form>
</body>
</html>
