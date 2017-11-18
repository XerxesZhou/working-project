<%@ Page Language="C#" AutoEventWireup="true" Codebehind="center.aspx.cs" Inherits="SmartSoft.Web.center" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息化管理平台</title>
    <meta http-equiv="refresh" content="120">
    <link href="@Css/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="@Scripts/window.js"></script>
</head>
<body style="overflow:visible">
    <form id="form1" runat="server">
        <table style="width:100%;height:200px;" cellpadding="0" cellspacing="0" border="0">
            <tr valign="Top" style="background:#99CCFF;height:20px">
                <td align="left" class="tdTitle">&middot;最新消息</td>
                <td align="right" style="padding-right:20px"><a href="Common/MessageList.aspx" target="main">更多...</a></td>
            </tr>
            <tr valign="Top">
                <td colspan="2" valign="Top">
                    <asp:GridView ID="GridMessage" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="10" AllowPaging="false" ShowHeader="false" DataKeyNames="MessageID"
                        AllowSorting="false" GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                   <%# bool.Parse(Eval("IsRead").ToString()) ? "<img src='../@images/1.gif' />" : "<img src='../@images/0.gif' />"%>
                                   <%--<img src="@images/arrow.gif" alt="" /> --%>
                                   <a href="#" onclick="openwinsimpscroll('<%# (Eval("URL") != null && Eval("URL").ToString() != string.Empty) ? Eval("URL") : "Common/MessageDetail.aspx?ID=" + Eval("MessageID") %>',800,600);" class='lbclass'><%# "[" + Eval("MessageType") + "]" + Eval("Title") + "(" + Eval("SendOperatorName") + ")"%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SendTime" ItemStyle-Width="100px" DataFormatString="{0:g}" HtmlEncode="false" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
      <%--  <table style="width:100%;height:200px;" cellpadding="0" cellspacing="0" border="0">
            <tr valign="Top" style="background:#99CCFF;height:20px">
                <td align="left" class="tdTitle">&middot;展会安排</td>
                <td align="right" style="padding-right:20px"><a href="Data/FairList.aspx" target="main">更多...</td>
            </tr>
            <tr valign="Top">
                <td colspan="2" valign="Top">
                    <asp:GridView ID="GridFair" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="10" AllowPaging="false" ShowHeader="false" 
                        AllowSorting="false" GridLines="Horizontal">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <img src="@images/arrow.gif" alt="" /> 
                                    <a href="#" onclick="openwinsimpscroll('<%# "Data/FairEditForm.aspx?Action=View&ID=" + Eval("fID")%>' ,800,600);" class='lbclass'><%# "(" + DateTime.Parse(Eval("fStartDate").ToString()).ToString("yyyy-MM-dd HH:mm") + "~" + DateTime.Parse(Eval("fEndDate").ToString()).ToString("yyyy-MM-dd HH:mm") + ")[" + Eval("fContent") + "]"%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
        <table style="width:100%;" cellpadding="0" cellspacing="0" border="0">
            <tr valign="Top" style="background:#99CCFF;height:20px">
                <td align="left" class="tdTitle">&middot;跟单流程</td>
                <td align="right" style="padding-right:20px"></td>
            </tr>
            <tr valign="Top">
                <td colspan="2" valign="Top">
                    <asp:GridView ID="GridFollowingWorkFlow" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                        PageSize="10" AllowPaging="false" ShowHeader="false" 
                        AllowSorting="false" GridLines="Horizontal">
                        <FooterStyle CssClass="GridFooter" />
                        <RowStyle CssClass="Row" />
                        <HeaderStyle CssClass="GridHeader" />
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <img src="@images/arrow.gif" alt="" /> 
                                    <a href="#" onclick="openwinsimpscroll('<%# "Data/FollowingWorkFlowEditForm.aspx?Action=View&ID=" + Eval("fwfID")%>' ,800,600);" class='lbclass'><%# "(" + Eval("fwfName").ToString() + ")[" + Eval("fwfDescription") + "]"%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
        </table>--%>
    </form>
</body>
</html>
