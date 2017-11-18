﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAchievementReportAnalyse.aspx.cs" Inherits="SmartSoft.Web.Report.CustomerAchievementReportAnalyse" %>

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
        <style type="text/css">
    html
    {
         overflow:auto;
        }
    </style>
</head>
<body style="overflow: visible;">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader">
                        <%=title %>&middot;查询条件
                    </td>
                    <td colspan="12">
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
                        年份：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" Width="60px">
                        </asp:DropDownList>
                    </td>
                    <th>
                        业绩类型：
                    </th>
                    <td>
                       <asp:DropDownList ID="ddlAchievementType" runat="server">
                            <asp:ListItem Value="1">合同额</asp:ListItem>
                            <asp:ListItem Value="2">回款额</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        分析类别：
                    </th>
                    <td>
                       <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="1">按照业务员分析</asp:ListItem>
                            <asp:ListItem Value="2">按照部门分析</asp:ListItem>
                            <asp:ListItem Value="3">按照客户分析</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        名称：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="SearchName"></asp:TextBox>
                    </td>
                    <th>
                        显示类型：
                    </th>
                    <td>
                         <asp:DropDownList ID="ddlDisplayType" runat="server"  Width="60px">
                            <asp:ListItem Value="1">柱图</asp:ListItem>
                            <asp:ListItem Value="2">饼图</asp:ListItem>
                            <asp:ListItem Value="3">折线图</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerAchievementReportAnalyse" runat="server" CssClass="Grid" GridLines="Both"
                    AutoGenerateColumns="False" AllowSorting="True">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <Columns>
                        <asp:BoundField DataField="TongJiFenLei" HeaderText="统计分类" >
                        </asp:BoundField>   
                        <asp:BoundField DataField="TongJiShuLiang" HeaderText="统计数量" >
                        </asp:BoundField>                               
                    </Columns>
                </asp:GridView>
 
        <div style="text-align:center;">
            <asp:Chart
                ID="Chart1" runat="server">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
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

