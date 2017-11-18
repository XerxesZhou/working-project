<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditCustomSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditCustomSelectType" %>

<html>
<head id="Head2" runat="server">
    <title>编辑自定义数据源信息</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" language="javascript" src="../@Scripts/AjaxCustomField.js"></script>

    <script type="text/javascript" language="javascript">
    <!--
    function OnLoadEvent()
    {  
        var selectTypeID = getUrlParameter("SelectTypeID");
        if (selectTypeID != "")
        {
            initCustomSelectTypePage(selectTypeID);   
        }
        
    }

    function backToList()
    {
        window.location = "ListCustomSelectType.aspx";
    }
    
    -->
    </script>

</head>
<body onload="javascript:OnLoadEvent();">
    <form id="form1" runat="server">
        <div>
            <table class="TableMain">
                <tr>
                    <td class="ListTitle">
                        &middot; 编辑自定义数据源信息
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <a id="ToolBar1_lb_Save" class="lbclass" href="javascript:saveSelectTypeAll();backToList();">
                            <img src="../@images/save.gif" alt="" />保存</a> <a onclick="javascript:backToList();"
                                id="ToolBar1_lb_Close" class="lbclass" href="javascript:backToList();">
                                <img src="../@images/close.gif" alt="" />关闭</a>
                    </td>
                </tr>
            </table>
            <br />
            <fieldset>
                <legend>自定义数据源</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldSelectTypeID" runat="server" ReadOnly="true" CssClass="OnlyBottom"></asp:TextBox>
                        </td>
                        <th>
                            显示文本：
                        </th>
                        <td>
                            <asp:TextBox ID="FieldSelectTypeText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="FieldRemark" runat="server" TextMode="MultiLine" Columns="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>设置下拉框内容</legend>
                <table id="dropDownListOptionTable" width="95%"  class="TablePanel" align="center">
                    <tr id="row1">
                        <th style="width: 10%; text-align: center;" align="center">
                            列表项
                        </th>
                        <th style="width: 30%; text-align: center;">
                            值
                        </th>
                        <th style="width: 30%; text-align: center;" align="center">
                            备注
                        </th>
                        <th style="width: 50px; text-align: center;" align="center">
                            是否停用
                        </th>
                        <th style="text-align: center;" align="center">
                            操作
                        </th>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
