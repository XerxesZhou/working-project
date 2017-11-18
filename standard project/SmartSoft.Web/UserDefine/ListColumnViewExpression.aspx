<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListColumnViewExpression.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListColumnViewExpression" %>

<%@ Import Namespace="UDEF.Component" %>
<%@ Import Namespace="UDEF.Domain.Enumerate" %>
<html>
<head id="Head1" runat="server">
    <title>视图条件符号表</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script id="Infragistics" type="text/javascript">
    <!--
    function EditOperator( operatorID )
    {
	    var strUrl = "EditColumnViewExpression.aspx";

	    if (operatorID != null)
        {
            strUrl += "?ExpressionID=" + operatorID;
        }
        var retVal = window.showModalDialog(strUrl, "", "dialogHeight:200px; dialogWidth:500px; center:yes; status:no; scroll:no;");
        if(retVal)
        {
		    alert('已成功编辑视图条件符号表资料！');
		    document.getElementById("btnRefresh").click();
		}
    }

    function StopOperator( ID )
    {
        if (window.confirm("确定要停用吗？\n将会使用这个符号！"))
        {
            var bSuccess = UDEF.Service.AjaxService.StopOrOpenColumnOperator(ID, true).value;
            if (bSuccess)
            {
                alert("停用成功！");
                document.getElementById("btnRefresh").click();
            }
            else
            {
               alert("停用失败！");
            }
        }
    }
    
    function OpenOperator( ID )
    {
        if (window.confirm("确定要启用吗？\n将会启用这个符号！"))
        {
            var bSuccess = UDEF.Service.AjaxService.StopOrOpenColumnOperator(ID, false).value;
            if (bSuccess)
            {
                alert("启用成功！");
                document.getElementById("btnRefresh").click();
            }
            else
            {
               alert("启用失败！");
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
    // -->
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="ListHeader" id="ListHeader">
            <tr class="ListTitle">
                <td colspan="2">
                    &middot;视图条件符号表
                </td>
            </tr>
            <tr class="ToolBar">
                <td>
                    <asp:LinkButton ID="btnRefresh" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../@images/search.gif" alt=""/>查询</asp:LinkButton>
                    <a id="btnAdd" class="lbclass" href="javaScript:EditOperator();">
                        <img src="../@images/new.gif" alt="" />新增</a>
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            符号名称：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchExpressionName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            符号值：
                        </th>
                        <td>
                            <asp:TextBox ID="SearchExpressionValue" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            数据类型：
                        </th>
                        <td>
                            <asp:DropDownList ID="SearchUseDataType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" GridLines="Both" ID="GridView1" runat="server" HorizontalAlign="center"
                    AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false"
                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ExpressionID" HeaderText="符号编号" SortExpression="ExpressionID" />
                        <asp:TemplateField HeaderText="符号数据类型" SortExpression="UseDataType">
                            <ItemTemplate>
                                <%# StringHelper.IsValidDBInt(Eval("UseDataType")) ? DataType.GetText(int.Parse(Eval("UseDataType").ToString())) : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpressionName" HeaderText="符号名称" SortExpression="ExpressionName" />
                        <%--<asp:BoundField DataField="ExpressionValue" HeaderText="符号值" />--%>
                        <asp:TemplateField HeaderText="已用" SortExpression="UseTag">
                            <ItemTemplate>
                                <%# bool.Parse(Eval("UseTag").ToString()) ? "√" : "×"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="停用" SortExpression="StopTag">
                            <ItemTemplate>
                                <%# bool.Parse(Eval("StopTag").ToString()) ? "√" : "×"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("ExpressionID").ToString(), bool.Parse(Eval("StopTag").ToString()))%>
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
