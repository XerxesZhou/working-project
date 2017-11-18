<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SetObjectPurview.aspx.cs"
    Inherits="SmartSoft.Web.Common.SetObjectPurview" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置权限</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <link href="../@Css/treeStyle.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>
    <script language="javascript" type="text/javascript">
        function CheckAllPurview(obj)
        {
            var t = obj.parentNode;
            var r = t.parentNode;
            $("input", r).attr("checked", obj.checked);
        }

        function CheckAllAll(obj)
        {
            var t = document.getElementById("GridsysPage");
            var cbArray = t.getElementsByTagName("INPUT");
            for(i=0;i<cbArray.length;i++)
            {
                if(obj.checked)
                {
                    cbArray[i].checked = true;
                }
                else
                {
                    cbArray[i].checked = false;
                }
            }
        }
    </script>
        <style type="text/css">
#up1
{
    height:87% !important;
    }
    </style>
</head>
<body onload="javascript:SetGridViewScrolling('up1');">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader">
                <tr>
                    <td class="TableSearchHeader">
                        &middot;系统授权查询条件 <div style="float:right;">选择所有:<input type="checkbox" id="cb_CheckAll" name="cb_CheckAll" style="width:20px;" onclick="CheckAllAll(this)" /></div>
                    </td>
                    <td colspan="8">
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
                    <th style="height: 25px">授权类型：</th>
                    <td style="height: 25px">
                        <asp:DropDownList ID="SearchPurviewType" runat="Server" AutoPostBack="True" Width="139px" OnSelectedIndexChanged="SearchPurviewType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SearchPurviewType"
                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    <th style="height: 25px">角色/操作员：</th>
                    <td style="height: 25px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>   
                                <asp:DropDownList ID="SearchObjectID"  AutoPostBack="True" runat="Server" OnSelectedIndexChanged="lb_Search_Click"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SearchObjectID"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="SearchPurviewType" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <%--<th style="height: 25px">页面类型：</th>
                    <td style="height: 25px">
                        <asp:DropDownList ID="SearchType" runat="Server" AutoPostBack="True"  OnSelectedIndexChanged="lb_Search_Click" Width="139px">
                        </asp:DropDownList>
                    </td>--%>
                     <th style="height: 25px">
                        模块：
                    </th>
                    <td style="height: 25px">
                        <asp:DropDownList ID="SearchModel" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="lb_Search_Click"></asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>   
                <asp:GridView ID="GridsysPage" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                    PageSize="20" AllowPaging="false" ShowHeader="True"
                    DataKeyNames="PageID" AllowSorting="True" OnRowDataBound="GridsysPage_RowDataBound">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <Columns>
                        <asp:BoundField DataField="PageID" Visible="false" />
                        <asp:BoundField DataField="ParentPageName" HeaderText="所属节点" ItemStyle-ForeColor="Blue"
                            ItemStyle-Width="120px" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="PageName" HeaderText="页面名称" ItemStyle-ForeColor="Red"
                            ItemStyle-Width="120px" ItemStyle-HorizontalAlign="left" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="checkbox" id="cb_CheckAll" name="cb_CheckAll" onclick="CheckAllPurview(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="权限" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBoxList ID="cbl_PagePurview" runat="server" RepeatDirection="horizontal">
                                </asp:CheckBoxList>
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
    </form>
</body>
</html>
