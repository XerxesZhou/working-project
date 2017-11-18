<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListTable.aspx.cs" Inherits="UDEF.Web.UserDefine.ListTable" %>

<html>
<head id="Head1" runat="server">
    <title>表管理</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script id="Infragistics" type="text/javascript">
    <!--
    function EditTable( tableID )
    {
	    var strUrl = "EditTable.aspx";
	    if (tableID != null)
        {
            strUrl += "?TableID=" + tableID;
        }
        window.location = strUrl;
    }

    function DeleteTable( ID )
    {
        if (window.confirm("确定要删除？\r如果表已经建立布局或视图,将一并删除！"))
        {
            var bSuccess = UDEF.Service.AjaxService.DeleteSystemTable(ID).value;
            if (bSuccess)
            {
                alert("删除成功！");
                document.getElementById("btnRefresh").click();
            }
            else
            {
               alert("删除失败！请检查。");
            }
        }
    }

    function ListColumn(tableID)
    {
        var strUrl = "ListColumn.aspx";
	    if (tableID != null)
        {
            strUrl += "?TableID=" + tableID;
        }
        window.location = strUrl;
    }
    
    function ListLayout(tableID)
    {
        var strUrl = "ListLayout.aspx";
	    if (tableID != null)
        {
            strUrl += "?TableID=" + tableID;
        }
        window.location = strUrl;
    }
    
    function ListColumnView(tableID)
    {
        var strUrl = "ListColumnView.aspx";
	    if (tableID != null)
        {
            strUrl += "?TableID=" + tableID;
        }
        window.location = strUrl;
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
    <script type="text/javascript">
        $(function () {
           
            var wheight = document.body.clientHeight;
          
            var divGridView = document.getElementById("divGridView");
            //显示列表高度
            divGridView.style.height = (wheight - 75) + "px";
        })
       
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="lb_Search">
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                <td>
                    &middot; 表管理
                </td>
                <td>
                    <a style=" margin-right:10px;" id="lb_Add" class="lbclass" href="javaScript:EditTable();">
                        <img src="../images/img_11.png" alt="" style="width:30px; height:30px; margin-bottom:3px;" />新增</a>
                        <asp:LinkButton ID="lb_Search" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../images/img_search1.png" alt=""/>查询</asp:LinkButton>
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            数据库表名称：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchTableName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            显示名称：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchTableText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" GridLines="Both" ID="GridView1" runat="server" AllowSorting="true"
                    OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false" AutoGenerateColumns="false"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TableID" HeaderText="表编号" SortExpression="TableID" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="TableName" HeaderText="数据库表名称" SortExpression="TableName"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="TableText" HeaderText="显示名称" SortExpression="TableText"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="PrimaryColumn" HeaderText="主键字段" SortExpression="PrimaryColumn"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="Remark" HeaderText="描述" SortExpression="Remark"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("TableID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有资料！
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
