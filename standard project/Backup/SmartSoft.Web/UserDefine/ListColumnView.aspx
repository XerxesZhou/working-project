<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListColumnView.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListColumnView" %>

<html>
<head id="Head1" runat="server">
    <title>视图管理</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script id="Infragistics" type="text/javascript">
    <!--

    function InsertView()
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
        var strUrl = "EditColumnView.aspx?EditMode=add&TableID=" + tableID;
        window.location = strUrl;
    }

    function CopyView( ViewID )
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
	    var strUrl = "EditColumnView.aspx?EditMode=copy&ViewID=" + ViewID + "&TableID=" + tableID;
	    window.location = strUrl;
    }
    
    function ExecuteView( ViewID )
    {     
	    var tableID = document.getElementById("ddlSeleteTable").value;
	    var strUrl = "ExecuteColumnView.aspx?EditMode=update&ViewID=" + ViewID + "&TableID=" + tableID;
	    window.location = strUrl;
    }
    
    function EditView( ViewID )
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
	    var strUrl = "EditColumnView.aspx?ViewID=" + ViewID + "&TableID=" + tableID;
	    window.location = strUrl;
    }

    function DeleteView( ID )
    {
        if (window.confirm("确定要删除？"))
        {
            var bSuccess = UDEF.Service.AjaxService.DeleteColumnView(ID).value;
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
                    &middot;视图管理
                </td>
                <td>
                    选择表：
                    <asp:DropDownList ID="ddlSeleteTable" runat="server" OnSelectedIndexChanged="ddlSeleteTable_SelectedIndexChanged"
                        AutoPostBack="true" Width="200px">
                    </asp:DropDownList>
                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="Hidden" />
                    <a onclick="javaScript:InsertView();" id="lb_Add" style=" margin-right:10px;" class="lbclass" href="javaScript:InsertView();">
                        <img src="../images/img_11.png" alt="" />新增</a>  
                    <a id="lb_GoBack" class="lbclass" style="color:#FFF; text-decoration:underline;" href="javascript:backToTableList();">
                            <img src="../@images/close.gif" alt="" />表管理</a>
                </td>
            </tr>
        </table>
        <div id="divGridView">
            <asp:GridView CssClass="Grid" GridLines="Both" ID="GridView1" runat="server" HorizontalAlign="center"
                AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="true"
                AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ViewID" HeaderText="视图编号" />
                    <asp:BoundField DataField="ViewName" HeaderText="视图名称" />
                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <%#GetOperation(Eval("ViewID").ToString(), bool.Parse(Eval("IsSystemDefine").ToString())) %>
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
    </form>
</body>
</html>
