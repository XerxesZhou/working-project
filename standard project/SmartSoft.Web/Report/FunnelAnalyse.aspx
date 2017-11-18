<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunnelAnalyse.aspx.cs" Inherits="SmartSoft.Web.Report.FunnelAnalyse" %>

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
    <script src="../@Scripts/echarts-all.js" type="text/javascript"></script>
    <script type="text/javascript">
        //销售漏斗
        function buildFunnel() {
            $(function () {
                //我的销售漏斗
                var mydata = eval($("#hfData1").val());
                var mydata2 = eval($("#hfData2").val());
                var minNum = eval($("#hfMinNum").val());
                var maxNum = eval($("#hfMaxNum").val());
                
                var myFunnelChart = echarts.init(document.getElementById('divFunnel'));
                optionMyFunnel = {
                    tooltip: {
                        trigger: 'item',
                        formatter: "{b} : {c}万"
                    },
                    legend: {
                        data: mydata
                    },
                    calculable: true,
                    series: [
                    {
                        name: '漏斗图',
                        type: 'funnel',
                        min: 0,
                        max: maxNum,
                        data: mydata2
                    }
                ]
                };
                myFunnelChart.setOption(optionMyFunnel);
            });
        }
        
    </script>
    <style type="text/css">
    #divFunnel
    {
        height:250px;
        width:100%;
        text-align:center;
    }
    html
    {
         overflow:auto;
        
        }
    </style>
</head>
<body style="overflow: visible;">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
         <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
                    <tbody id="searchHeader" title="隐藏查询条件">
                        <tr>
                            <td class="TableSearchHeader">
                                <%=title %>&middot;查询条件
                            </td>
                            <td  colspan="12">
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
                                预测对象：
                            </th>
                            <td>
                               <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1">全公司</asp:ListItem>
                                    <asp:ListItem Value="2">部门</asp:ListItem>
                                    <asp:ListItem Value="3">业务员</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlObject" runat="server" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>

                        </tr>
                    </tbody>
                </table>

                <asp:GridView ID="GridOpptunityReport" runat="server" CssClass="Grid" GridLines="Both"
                    AutoGenerateColumns="False" AllowSorting="True">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="阶段名称" >
                        </asp:BoundField>   
                        <asp:BoundField DataField="Rate" HeaderText="转化率" >
                        </asp:BoundField>  
                        <asp:BoundField DataField="Count" HeaderText="数量"  HtmlEncode="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>  
                        <asp:BoundField DataField="PredictAmount" HeaderText="商机金额"  HtmlEncode="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>    
                        <asp:BoundField DataField="Amount" HeaderText="预测成交金额"  HtmlEncode="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>       
                    </Columns>
                </asp:GridView>
                <div style="text-align:center;">
                    <asp:HiddenField runat="server" ID="hfMinNum" Value="0"/>
                    <asp:HiddenField runat="server" ID="hfMaxNum" Value="14"/>
                    <asp:HiddenField runat="server" ID="hfData1" Value="['初期沟通', '立项评估', '需求分析', '方案认证', '商务谈判', '合同签订']"/>
                    <asp:HiddenField runat="server" ID="hfData2" Value="[{ value: 12, name: '初期沟通' },{ value: 14, name: '立项评估' },{ value: 8, name: '需求分析' },{ value: 6, name: '方案认证' },{ value: 4, name: '商务谈判' },{ value: 2.5, name: '合同签订' }]"/>
                    <div id="divFunnel"></div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
                
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Delete" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="AspNetPager1" EventName="PageChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
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

