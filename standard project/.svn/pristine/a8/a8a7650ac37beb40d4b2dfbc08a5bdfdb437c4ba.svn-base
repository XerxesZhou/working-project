<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListColumn.aspx.cs" Inherits="UDEF.Web.UserDefine.ListColumn" %>

<%@ Import Namespace="UDEF.Service" %>
<%@ Import Namespace="UDEF.Component" %>
<%@ Import Namespace="UDEF.Domain.Enumerate" %>
<html>
<head id="Head1" runat="server">
    <title>字段管理</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <style type="Text/css">
    
    TABLE.Grid TR.GridHeader1
    {
	    background-image: url(../@images/silverGlass.gif);
	    height: 20px;
	    font-size: 12px;
	    font-family: Tahoma;
	    font-weight: bold;
	    color: Black;
    }
    
    #ddlSeleteTable
    {
        color:#33383c;
        }
        html
        {
                overflow-y: auto;
         }
    </style>

    <script id="Infragistics" type="text/javascript">
    <!--

    function InsertCustomColumn()
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
        var strUrl = "EditCustomColumnCommon.aspx?TableID=" + tableID;
        window.location = strUrl;
    }
    function UpdateCustomColumn(ColumnID)
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
        var strUrl = "EditCustomColumnCommon.aspx?ColumnID=" + ColumnID + "&TableID=" + tableID;;
        window.location = strUrl;
    }

    function UpdateSystemColumn(ColumnID)
    {     
	    var strUrl = "EditSystemColumn.aspx?ColumnID=" + ColumnID;
	    window.location = strUrl;
    }
    
    function InsertSystemColumn()
    {     
        var tableID = document.getElementById("ddlSeleteTable").value;
	    var strUrl = "EditSystemColumn.aspx?TableID=" + tableID;
	    window.location = strUrl;
    }
    
    function DeleteCustomColumn( ID )
    {
        if (window.confirm("自定义字段中的值，视图，布局中该字段的资料将全部被清除！\n确定要删除？"))
        {
            var bSuccess = UDEF.Service.AjaxService.DeleteCustomColumnAll(ID).value;
            if (bSuccess)
            {
                alert("删除成功！");
                document.getElementById("btnRefresh2").click();
            }
            else
            {
               alert("删除失败！");
            }
        }
    }

    function DeleteSystemColumn( ID )
    {
        if (window.confirm("视图，布局中该字段的资料将全部被清除！n\n确定要删除？"))
        {
            var bSuccess = UDEF.Service.AjaxService.DeleteSystemColumnAll(ID).value;
            if (bSuccess)
            {
                alert("删除成功！");
                document.getElementById("btnRefresh1").click();
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
    
    function backToTableList()
    {
        window.location = "ListTable.aspx";
    }
    // -->
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                <td>
                    &middot;字段管理
                </td>
                <td>
                    选择表：
                    <asp:DropDownList ID="ddlSeleteTable" runat="server" OnSelectedIndexChanged="ddlSeleteTable_SelectedIndexChanged"
                        AutoPostBack="true" Width="200px">
                    </asp:DropDownList>
                    <a id="lb_GoBack" style="color:#FFF; text-decoration:underline"  class="lbclass" href="javascript:backToTableList();">
                        <img src="../@images/close.gif" alt="" />表管理</a>
                    <asp:Button ID="btnRefresh1" runat="server" OnClick="btnRefresh1_Click" CssClass="Hidden" />
                    <asp:Button ID="btnRefresh2" runat="server" OnClick="btnRefresh2_Click" CssClass="Hidden" />
                </td>
            </tr>
        </table>
        <div>
            <div id="divGridView1">
                <asp:GridView CssClass="Grid" ID="GridView1" GridLines="Both" runat="server" AllowSorting="true"
                    OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false" AutoGenerateColumns="false"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <a onclick="javascript:UpdateSystemColumn('<%#Eval("ColumnID") %>')" href="#Update"
                                    class="View">修改</a> | <a onclick="javascript:DeleteSystemColumn('<%#Eval("ColumnID") %>')"
                                        href="#Delete" class="View">删除 </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ColumnID" HeaderText="字段编号" SortExpression="ColumnID"
                            ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="ColumnName" HeaderText="字段名称" SortExpression="ColumnName"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="ColumnText" HeaderText="显示文本" SortExpression="ColumnText"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:TemplateField HeaderText="数据类型" ItemStyle-HorizontalAlign="left" SortExpression="DataType">
                            <ItemTemplate>
                                <%#StringHelper.IsValidDBInt(Eval("DataType")) ? DataType.GetText(int.Parse(Eval("DataType").ToString())) : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总是只读" ItemStyle-HorizontalAlign="Center" SortExpression="IsAlwaysReadonly">
                            <ItemTemplate>
                                <%#bool.Parse(Eval("IsAlwaysReadonly").ToString()) ? "<img src='../@images/checked.gif'/>" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总是必需" ItemStyle-HorizontalAlign="Center" SortExpression="IsAlwaysRequired">
                            <ItemTemplate>
                                <%#bool.Parse(Eval("IsAlwaysRequired").ToString()) ? "<img src='../@images/checked.gif'/>" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总是显示" ItemStyle-HorizontalAlign="Center" SortExpression="IsAlwaysDisplayed">
                            <ItemTemplate>
                                <%#bool.Parse(Eval("IsAlwaysDisplayed").ToString()) ? "<img src='../@images/checked.gif'/>" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总是隐藏" ItemStyle-HorizontalAlign="Center" SortExpression="IsAlwaysHide">
                            <ItemTemplate>
                                <%#bool.Parse(Eval("IsAlwaysHide").ToString()) ? "<img src='../@images/checked.gif'/>" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否外键" ItemStyle-HorizontalAlign="Center" SortExpression="IsAlwaysHide">
                            <ItemTemplate>
                                <%#int.Parse(Eval("ForeignTableID").ToString()) > 0 ? "<img src='../@images/checked.gif'/>" : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有资料！
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader1" />
                    <AlternatingRowStyle CssClass="RowA" />
                </asp:GridView>
            </div>
            <div>
                <a id="btnAdd" class="lbclass" href="javaScript:InsertCustomColumn();">
                    <img src="../@images/new.gif" alt="" />新增</a>
            </div>
            <div id="divGridView2">
                <asp:GridView CssClass="Grid" GridLines="Both" ID="GridView2" runat="server"
                    AllowSorting="true" OnSorting="GridView2_Sorting" PageSize="15" AllowPaging="false"
                    AutoGenerateColumns="false" OnPageIndexChanging="GridView2_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <a onclick="javascript:UpdateCustomColumn('<%#Eval("ColumnID") %>')" href="#Update"
                                    class="View">修改</a> | <a onclick="javascript:DeleteCustomColumn('<%#Eval("ColumnID") %>')"
                                        href="#Delete" class="View">删除 </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ColumnID" HeaderText="字段编号" SortExpression="ColumnID"  ItemStyle-HorizontalAlign="center"/>
                        <asp:BoundField DataField="ColumnName" HeaderText="字段名称" SortExpression="ColumnName"  ItemStyle-HorizontalAlign="left"/>
                        <asp:BoundField DataField="ColumnText" HeaderText="显示文本" SortExpression="ColumnText"  ItemStyle-HorizontalAlign="left"/>
                        <asp:TemplateField HeaderText="数据类型"  ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#StringHelper.IsValidDBInt(Eval("DataType")) ? DataType.GetText(int.Parse(Eval("DataType").ToString())) : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="字段描述" SortExpression="Remark"  ItemStyle-HorizontalAlign="left"/>
                        <asp:BoundField DataField="ModifyDate" HeaderText="创建时间" SortExpression="ADD_ModifyDate"  ItemStyle-HorizontalAlign="center"/>
                        <asp:BoundField DataField="ModifyDate" HeaderText="最后修改时间" SortExpression="ModifyDate"  ItemStyle-HorizontalAlign="center"/>
                    </Columns>
                    <EmptyDataTemplate>
                        没有资料！
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader1" />
                    <AlternatingRowStyle CssClass="RowA" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
