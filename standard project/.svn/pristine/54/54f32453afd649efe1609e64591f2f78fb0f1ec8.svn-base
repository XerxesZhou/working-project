<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListSystemSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListSystemSelectType" %>

<html>
<head id="Head1" runat="server">
    <title>系统数据源</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>

    <script id="Infragistics" type="text/javascript">


        $(function () {
            var h = window.innerHeight;
            $("#divGridView").css("height", h - 71);
        })

        function EditSystemSelectType( selectTypeID )
        {
        
	        var strUrl = "EditSystemSelectType.aspx";
	        if (selectTypeID != null)
            {
                strUrl += "?SelectTypeID=" + selectTypeID;
            }
            window.location = strUrl;
        }

        function DeleteSystemSelectType( ID )
        {
            if (window.confirm("确定要删除？\r建议不要删除！"))
            {
                var bSuccess = UDEF.Service.AjaxService.DeleteSystemSelectType(ID).value;
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
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="lb_Search">
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                 <td>
                    &middot; 系统数据源
                </td>
                <td>
                    <a id="lb_Add" style="margin-right:10px;" class="lbclass" href="javaScript:EditSystemSelectType();">
                        <img src="../images/img_11.png" alt=""style="width:30px; height:30px; margin-bottom:3px;" />新增</a>
                    <asp:LinkButton ID="lb_Search" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../images/img_search1.png" alt=""/>查询</asp:LinkButton>
                    
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            数据源名称：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeName" runat="server"></asp:TextBox>
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
                <asp:GridView CssClass="Grid" ID="GridView1" GridLines="Both" runat="server"
                    AllowSorting="true" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
                    PageSize="15" AllowPaging="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="SelectTypeName" HeaderText="数据源名称" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeName" />
                        <asp:BoundField DataField="SelectTypeText" HeaderText="显示名称" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeText" />
                        <asp:BoundField DataField="PrimaryID" HeaderText="绑定列表值的字段" ItemStyle-HorizontalAlign="left"
                            SortExpression="PrimaryID" />
                        <asp:BoundField DataField="PrimaryName" HeaderText="绑定列表文本的字段" ItemStyle-HorizontalAlign="left"
                            SortExpression="PrimaryName" />
                        <asp:BoundField DataField="TableName" HeaderText="表名称" ItemStyle-HorizontalAlign="left"
                            SortExpression="表名称" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("SelectTypeID").ToString())%>
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
