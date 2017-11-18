<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditTable.aspx.cs" Inherits="UDEF.Web.UserDefine.EditTable" %>

<html>
<head id="Head1" runat="server">
    <title>编辑系统表</title>
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />

    <style>
        .lbclass img
        {
            margin-bottom:8px;
            }
    </style>

</head>
<base target="_self">
</base>

<script type="text/javascript">

    $(function () {
        var h = window.innerHeight;
        $("#divGridView").css("height", h - 155);
    })

// 多选的全选与取消
function checkJs(boolvalue)
{
    for (var i = 0; i < document.form1.elements.length; i++)
    {
        var element = document.form1.elements[i];
        if(element.type == 'checkbox' 
        && element.id.indexOf('checkboxname') != -1 
        && element.disabled != true) 
        {
            element.checked = boolvalue;
        } 
    } 
}

function backToList()
{
    window.location = "ListTable.aspx";
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

<body>
    <form id="form1" runat="server">
        <div>
            <table class="TableMain" cellpadding="0" cellspacing="0">
                <tr class="ToolBar" style="height:50px;">
                    <td>
                        &middot;编辑数据表
                    </td>
                    <td>
                        
                        <a id="lb_Add" class="lbclass" href="javascript:backToList();">
                            <img src="../images/img_back2_25.png" alt="" style="width:30px; height:30px; margin-bottom:8px;"/>返回</a>
                            <asp:LinkButton ID="lb_Search" CssClass="lbclass" runat="Server" OnClick="btnSubmit_Click"><img src="../images/img_160.png" alt="" />保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>系统表资料</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            系统表编码：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldTableID" runat="server" Width="300px" ReadOnly="true" CssClass="OnlyBottom"></asp:TextBox>
                        </td>
                        <th>
                            数据库表名称：
                        </th>
                        <td>
                            <asp:DropDownList ID="dropdownlistTableName" Width="200px" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="FieldTableName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="FieldTableName" runat="server" CssClass="Hidden">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            显示名称：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldTableText" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <th>
                            主键字段名称：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldPrimaryColumn" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="FieldRemark" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" ID="GridView1" GridLines="Both" runat="server" AllowSorting="true"
                    OnSorting="GridView1_Sorting" PageSize="15" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:CheckBox ID="checkboxname" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrderBy" HeaderText="序号" SortExpression="OrderBy" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="ColumnName" HeaderText="字段名称" SortExpression="OrderBy"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="Description" HeaderText="字段备注" SortExpression="ColumnName"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="DataType" HeaderText="字段数据库类型" SortExpression="DataType"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="IsPrimary" HeaderText="是否主键" SortExpression="IsPrimary"
                            ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="IsNullable" HeaderText="是否可空" SortExpression="IsNullable"
                            ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="ColumnLength" HeaderText="字节数" SortExpression="ColumnLength"
                            ItemStyle-HorizontalAlign="left" />
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
