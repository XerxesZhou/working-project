<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeFollowHistoryReport.aspx.cs" Inherits="SmartSoft.Web.Report.EmployeeFollowHistoryReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    
    <script language="javascript" type="text/javascript">
    function onpress()
    {
        if(event.keyCode == 13)
        {
            __doPostBack("ToolBar1$lb_Search","")
        }
    }
    </script>
        <style type="text/css">
    html
    {
         overflow:auto;
        };
    </style>
</head>
<body onload="javascript:SetGridViewScrolling('up1');" onkeypress="onpress();" style="overflow: visible;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        <%=title %>&middot;查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td  colspan="4">
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
                        <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchcbDateBegin','SearchcbDateEnd');" />
                        日期：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchcbDateBegin" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchcbDateEnd" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                    <th>
                    </th>
                    <td>
                         
                    </td>
                    <th>
                    </th>
                    <td>
                         
                    </td>
                    <th>
                    </th>
                    <td>
                         
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridEmployeeFollowHistoryReport" runat="server" CssClass="Grid" GridLines="Both"
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
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
            <ContentTemplate>
                <div style="margin-top:10px;">
                <div class="Pager" style="width:70%; float:left;">
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager>
                </div>
                <div style="width:200px; float:right; margin-top:15px;">
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

