<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerReceiptPlanList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerReceiptPlanList" %>


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
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script language="javascript" type="text/javascript">
        var EditFormUrl = "CustomerBusinessEditForm.aspx?FromType=receiptplan&showPanel=divReceiptPlanList&ctr=ReceiptPlan";

    function ConvertToLink() {
        ConvertToLinkCommon("GridCustomerReceiptPlan", "客户名称");
    }
    </script>
</head>
<body onload="javascript:SetGridViewScrolling('up1');__doPostBack('ToolBar1$lb_Search','');" style="overflow: visible;">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td  colspan="8">
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
                        <input type="checkbox" id="cbID2" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchcrpDateBegin','SearchcrpDateEnd');" />
                        计划日期：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchcrpDateBegin" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchcrpDateEnd" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                   
                    <th>
                        负责人：
                    </th>
                    <td>
                         <asp:DropDownList ID="SearchcrpOperatorID" runat="server">
                        </asp:DropDownList>
                    </td>
                   <th>
                        所属部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcusDepartmentID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <th>
                        关键字：
                    </th>
                    <td>
                         <asp:TextBox ID="txtKeyWord" runat="server" Width="218px"></asp:TextBox>
                    </td>
                     
                     <th>
                        收款类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcrpTypeID" runat="server">
                        </asp:DropDownList>
                       
                    </td>
                    <th>
                        状态：
                    </th>
                    <td>
                         <asp:DropDownList ID="ddlcrpStatus" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Text="未完成(未收/部分收)" Value="未完成" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="未收款" Value="未收款"></asp:ListItem>
                            <asp:ListItem Text="部分收款" Value="部分收款"></asp:ListItem>
                            <asp:ListItem Text="已完成" Value="已完成"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerReceiptPlan" runat="server" CssClass="Grid" GridLines="Both"
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
              <div class="footpager">
              <table cellpadding="0" cellspacing="0" border="0" style=" width:100%">
              <tr >
              <td style=" width:100%;">
               <div class="Pager" style="width:100%; float:left;">
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager>
                </div>
              </td>
              <td>
               <div style="width:200px; float:right;">
                     每页显示：
                    <asp:DropDownList Width="50px" runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>条记录
                </div>
              </td>
              </tr>
              </table>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:HiddenField ID="hfSelectCheck" runat="server" />
    </form>
</body>
</html>

