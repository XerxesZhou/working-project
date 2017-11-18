<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListCustomSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListCustomSelectType" %>

<html>
<head id="Head1" runat="server">
    <title>自定义数据源</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script id="Infragistics" type="text/javascript">
    <!--
    function EditCustomSelectType(selectTypeID)
    {
        
	    var strUrl = "EditCustomSelectType.aspx";
	    if (selectTypeID != null)
        {
            strUrl += "?SelectTypeID=" + selectTypeID;
        }
        window.location = strUrl;
    }

    function DeleteCustomSelectType( ID )
    {
        try
        {
            if (window.confirm("确定要删除？\r建议不要删除！"))
            {
                var bSuccess = UDEF.Service.AjaxService.DeleteCustomSelectType(ID).value;
                if (bSuccess)
                {
                    alert("删除成功！");
                    document.getElementById("btnRefresh").click();
                }
                else
                {
                   alert("删除失败！");
                }
            }
        }
        catch(err)
        {
            displayErrorMsg("DeleteCustomSelectType", err);
        }
    }

    // 改变行的颜色
    if (!objbeforeItem)
    {
        var objbeforeItem=null;
        var objbeforeItembackgroundColor=null;
    }

    function ItemOver(obj)
    {
        objbeforeItembackgroundColor=obj.style.backgroundColor;
        obj.style.backgroundColor="#B9D1F3";   
        obj.style.cursor = "pointer";                                    
        objbeforeItem=obj;
    }
        
    function ItemOut(obj)
    {            
        if(objbeforeItem)
        {
            objbeforeItem.style.backgroundColor=objbeforeItembackgroundColor;
        }    
    }
    // -->
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="lb_Search">
        <table class="ListHeader" id="ListHeader">
            <tr class="ListTitle">
                <td colspan="2">
                    &middot; 自定义数据源
                </td>
            </tr>
            <tr class="ToolBar">
                <td>
                    <asp:LinkButton ID="lb_Search" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../@images/search.gif" alt=""/>查询</asp:LinkButton>
                    <a id="lb_Add" class="lbclass" href="javaScript:EditCustomSelectType();">
                        <img src="../@images/new.gif" alt="" />新增</a>
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            数据源编号：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeID" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            数据源显示文本：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" ID="GridView1" runat="server" HorizontalAlign="center"
                    AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false"
                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SelectTypeID" HeaderText="数据源编号" ItemStyle-HorizontalAlign="center"
                            SortExpression="SelectTypeID" />
                        <asp:BoundField DataField="SelectTypeText" HeaderText="显示名称" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeText" />
                        <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="left"
                            SortExpression="Remark" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("SelectTypeID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有资料！
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" />
                    <PagerStyle HorizontalAlign="right" CssClass="GridFooter" />
                    <AlternatingRowStyle CssClass="GridAlternating" />
                    <RowStyle CssClass="GridRow" />
                    <HeaderStyle CssClass="GridHeader" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
