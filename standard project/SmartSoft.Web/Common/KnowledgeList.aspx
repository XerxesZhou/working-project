<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeList.aspx.cs" Inherits="SmartSoft.Web.Data.KnowledgeList" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=title%>
    </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/window.js"></script>

    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>
 
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    
    <style>
        #up1
        {
            height:418px !important;
            }
    </style>

    <script language="javascript" type="text/javascript">



    var EditFormUrl = "KnowledgeEditForm.aspx";
    
    function onpress()
    {
        if(event.keyCode == 13)
        {
            __doPostBack("ToolBar1$lb_Search","")
        }
    }

    //行上双击处理事件
    function dbclick(obj, action) {
        try {
            var td = obj.cells[0];
            var checkbox = td.childNodes[0];

            var url = "KnowledgeView.aspx?ID=";
            url += checkbox.value;
            OpenEditForm(url);
        }
        catch (err) {
            displayErrorMsg("dbclick", err)
        }
    }
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');__doPostBack('ToolBar1$lb_Search','');"
    onkeypress="onpress();" style="overflow: visible;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        &middot;<%=title %>查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td colspan="8">
                        <table class="ListHeader" id="ListHeader" cellspacing="0" cellpadding="0" border="0">
                            <tr class="ToolBar">
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
                        内容：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        类别：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchknowType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        主题名：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchknowTheme" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchknowOperateDateBegin','SearchknowOperateDateEnd');" />
                        操作时间：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchknowOperateDateBegin" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchknowOperateDateEnd" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridKnowledge" runat="server" CssClass="Grid" GridLines="Both"
                    AutoGenerateColumns="False" AllowSorting="True">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
                
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Delete" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="AspNetPager1" EventName="PageChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-top:10px;">
                <div class="Pager" style="width:70%; float:left;">
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager>
                </div>
                <div style="width:200px; float:right;">
                     每页显示：
                    <asp:DropDownList Width="50px" runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>条记录
                </div>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfSelectCheck" runat="server" />
    </form>
</body>
</html>

