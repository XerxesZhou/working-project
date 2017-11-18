<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageControlList.aspx.cs"
    Inherits="SmartSoft.Web.Common.PageControlList" %>

<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ҳ��ؼ��б� </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script language="javascript" type="text/javascript">
        var pageId = "<%=PageID%>";

        function Add() {
            openwinsimp("PageControlForm.aspx?PageID=" + pageId, 660, 520);
        }

        function Edit() {
            try {
                var url = "PageControlForm.aspx?";
                CheckBoxOpenWindowHandle(url + "PageID=" + pageId + "&ControlID=", 600, 480, "��ѡ��һ����Ҫ�޸ĵļ�¼!");
            }
            catch (err) {
                displayErrorMsg("Edit", err)
            }
        }

        function Delete() {
            return CheckBoxMultiHandle('ȷ��Ҫɾ����');
        }       
   
    </script>
    <style type="text/css">
        html
        {
            overflow: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
        <tr class="ToolBar">
            <td>
                &middot;ҳ��ؼ��б�<font color="red">[<asp:Literal ID="lt_title" runat="server"></asp:Literal>]</font>
            </td>
            <td>
                <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridPageControl" CssClass="Grid" runat="server" AutoGenerateColumns="False"
        PageSize="20" AllowPaging="true" ShowHeader="True" DataKeyNames="ControlID" AllowSorting="True"
        OnRowDataBound="GridPageControl_RowDataBound">
        <FooterStyle CssClass="GridFooter" />
        <RowStyle CssClass="Row" />
        <HeaderStyle CssClass="GridHeader" />
        <AlternatingRowStyle CssClass="RowA" />
        <Columns>
            <asp:BoundField DataField="Id" Visible="false" />
            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                <ItemTemplate>
                    <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "ControlID")%>'
                        onclick='SingleCheckJs(this);' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ControlName" HeaderText="�ؼ�����" />
            <asp:TemplateField HeaderText="��ͼ/����" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#(int.Parse(Eval("LayoutOrView").ToString()) == 1) ? "����" : "��ͼ"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PageName" HeaderText="ҳ������" />
            <asp:BoundField DataField="TableText" HeaderText="ϵͳ����" />
        </Columns>
        <PagerSettings Visible="false" />
    </asp:GridView>
    </form>
</body>
</html>
