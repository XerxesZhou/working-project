<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExecuteColumnView.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ExecuteColumnView" %>

<html>
<head id="Head1" runat="server">
    <title>视图运行结果</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript">
    <!--
    function backToList(TableID)
    {
        window.location = "ListColumnView.aspx?TableID=" + TableID;
    }
    
    -->
    </script>

</head>
<body onload="javascript:SetGridViewScrolling();">
    <form id="form1" runat="server">
        <div>
            <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
                <tr class="ToolBar" style="height:50px;">
                    <td>
                        &middot; 运行视图结果列表
                    </td>
                    <td>
                        <a id="lb_Add" style="margin-right:10px;" class="lbclass" href="javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);">
                            <img src="../images/img_newclose.png" alt="" />返回</a>
                    </td>
                </tr>
            </table>
           
            <fieldset>
                <table width="100%">
                    <tr>
                        <td>
                            <div id="up1">
                            <asp:GridView ID="GridView1" GridLines="Both" runat="server" AutoGenerateColumns="true"
                                OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <EmptyDataTemplate>
                                    没有记录！
                                </EmptyDataTemplate>
                                <FooterStyle CssClass="GridFooter" />
                                <RowStyle CssClass="Row" />
                                <HeaderStyle CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="RowA" />
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
