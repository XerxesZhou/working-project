<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditSystemColumn.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditSystemColumn" %>

<html>
<head id="Head1" runat="server">
    <title>编辑系统字段</title>

    <script type="text/javascript" language="javascript" src="../Scripts/BaseHelper.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
</head>

<script type="text/javascript">
function backToList(TableID)
{
    window.location = "ListColumn.aspx?TableID=" + TableID;
}


function handleDataTypeChange(obj)
{
    var objSelectType = document.getElementById("FieldSelectType");
    if (obj.value == '5')   //只有是下拉列表类型的数据“下拉列表数据”下拉框才可选
    {
        objSelectType.disabled = '';
    }
    else
    {
        objSelectType.selectedIndex = -1;
        objSelectType.disabled = 'disabled';
    }
}

function handleIsForeignTableChange(obj)
{
    var objForeignTableID = document.getElementById("ddlForeignTableID");
    if (obj.checked)
    {
        objForeignTableID.disabled = '';
    }
    else
    {
        objForeignTableID.selectedIndex = -1;
        objForeignTableID.disabled = 'disabled';
        document.getElementById("FieldForeignTableID").value = 0;
    }
}

function handleddlForeignTableIDChange()
{
    document.getElementById("FieldForeignTableID").value = document.getElementById("ddlForeignTableID").value;
}
function handleOnLoad()
{
    var objDataType = document.getElementById("FieldDataType");
    var objIsForeignTable = document.getElementById("checkboxIsForeignTable");
    var objForeignTableID = document.getElementById("FieldForeignTableID");
  
    handleDataTypeChange(objDataType)
    
    if (objForeignTableID.value != "" && objForeignTableID.value > 0)
    {
        objIsForeignTable.checked = true;
        document.getElementById("ddlForeignTableID").value = objForeignTableID.value;
    }
    handleIsForeignTableChange(objIsForeignTable);
}
</script>

<base target="_self"></base>
<body onload="javascript:handleOnLoad();">
    <form id="form1" runat="server">
        <div>
            <table class="TableMain" cellpadding="0" cellspacing="0">
                <tr class="ToolBar">
                     <td class="ListTitle">
                        &middot;编辑系统字段信息
                    </td>
                    <td>
                         <a id="lb_Search" class="lbclass" href="javascript:backToList(<%=TableID %>);">
                            <img src="../images/img_back2_25.png" alt="" />返回</a>
                        <asp:LinkButton ID="lb_Add" CssClass="lbclass" runat="Server" OnClick="btnSubmit_Click"><img src="../images/img_160.png" alt="" />保存</asp:LinkButton>
                       
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>系统字段资料</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            字段编码：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldColumnID" runat="server" CssClass="OnlyBottom" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            字段名称：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldColumnName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            显示文本：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldColumnText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            字段类型：
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldDataType" runat="server" onchange="javascript:handleDataTypeChange(this);">
                            </asp:DropDownList>
                        </td>
                        <th>
                            下拉列表类型：
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldSelectType" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否外键：
                        </th>
                        <td>
                            <asp:CheckBox ID="checkboxIsForeignTable" runat="server" onclick="javascript:handleIsForeignTableChange(this);" />
                        </td>
                        <th>
                            外键表：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlForeignTableID" runat="server" Enabled="false" onchange="javascript:handleddlForeignTableIDChange();">
                            </asp:DropDownList>
                            <asp:TextBox ID="FieldForeignTableID" runat="server" CssClass="Hidden">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            总是只读：
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldIsAlwaysReadonly" runat="server" />
                        </td>
                        <th>
                            总是必需：
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldIsAlwaysRequired" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            总是显示：
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldIsAlwaysDisplayed" runat="server" />
                        </td>
                        <th>
                            总是隐藏：
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldIsAlwaysHide" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            字段描述：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldRemark" runat="server" TextMode="MultiLine" Columns="3" Width="100%"></asp:TextBox>
                        </td>
                        <th>
                            最后修改时间：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldModifyDate" runat="server" CssClass="OnlyBottom" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:TextBox ID="FieldTableID" runat="server" CssClass="Hidden"></asp:TextBox>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
