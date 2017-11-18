<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatorPlanEditForm.aspx.cs" Inherits="SmartSoft.Web.Data.OperatorPlanEditForm" %>

<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
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

    <script language="javascript" type="text/javascript">
        function onpress() {
            if (event.keyCode == 13) {
                __doPostBack("ToolBar1$lb_Search", "")
            }
        }

        function InsertBatch() {
            try {
                var url = "CustomerBudgetSetting.aspx";
                OpenEditForm(url);
            }
            catch (err) {
                displayErrorMsg("InsertBatch", err);
            }
        }
    </script>
        <style type="text/css">
    html
    {
         overflow:auto;
        }
    </style>
</head>
<body onload="javascript:SetGridViewScrolling('up1');"
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
              <%--  ------------%>
                    <th>
                        业务员名称：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txtopeName"></asp:TextBox>
                    </td>
                  <%--  ------------%>
                    <th>
                        类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchopTypeID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                   <%--  ---------%>
                    <th>
                        年份：
                    </th>
                    <td>
                         <asp:DropDownList ID="ddlYear" runat="server" Width="60px" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                    </th>

                
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridOperatorEdit" runat="server" CssClass="Grid" GridLines="Both"
                    AutoGenerateColumns="False" AllowSorting="True">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                                        <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opeID")%>'
                                    Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="opeName" HeaderText="名称" >
                        </asp:BoundField> 
                        <asp:BoundField DataField="yID" HeaderText="年份" >
                        </asp:BoundField> 
                        <asp:BoundField DataField="sName" HeaderText="类型" >
                        </asp:BoundField> 
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="1月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM1")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="2月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM2")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="3月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM3")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="4月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM4")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="5月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM5")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="6月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM6")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="7月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM7")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="8月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM8")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="9月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM9")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="10月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM10")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="11月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM11")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField ItemStyle-Height="16px" HeaderText="12月">
                            <ItemTemplate>
                                <asp:TextBox Width="80px" ID="txtM12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "opM12")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>    
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfSelectCheck" runat="server" />
    </form>
</body>
</html>



