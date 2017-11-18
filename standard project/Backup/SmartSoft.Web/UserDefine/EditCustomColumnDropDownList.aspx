<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditCustomColumnDropDownList.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditCustomField_DropDownList" %>

<html>
<head id="Head1" runat="server">
    <title>编辑自定义字段</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" language="javascript" src="../@Scripts/AjaxCustomField.js"></script>

    <script type="text/javascript" language="javascript">
    <!--
    function OnLoadEvent()
    {  
        var columnID = getUrlParameter("ColumnID");
        if (columnID != "")
        {
            initCustomSelectTypePage(columnID);   
        }
        
    }

    function backToList(TableID)
    {
        window.location = "ListColumn.aspx?TableID=" + TableID;
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
                        &middot;编辑自定义数据源
                    </td>
                </tr>
                <tr>
                    <input id="txtColumnText" type="hidden" name="headerText" value='<%=Request.QueryString["ColumnText"].ToString() %>' />
                    <input id="txtRemark" type="hidden" name="Remark" value='<%=Request.QueryString["Remark"].ToString() %>' />
                    <td align="center" colspan="2">
                        <a id="ToolBar1_lb_Save" class="lbclass" href="javascript:saveSelectTypeAll();backToList(<%=Request.QueryString["TableID"].ToString() %>)" ;">
                            <img src="../@images/save.gif" alt="" />保存</a> <a onclick="javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);"
                                id="ToolBar1_lb_Close" class="lbclass" href="javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);">
                                <img src="../@images/close.gif" alt="" />关闭</a>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>设置下拉框内容</legend>
                <table id="tblDistributeTable" width="95%" class="List" align="center">
                    <tr id="row1">
                        <th>
                            分发至单位
                        </th>
                        <th>
                            数量
                        </th>
                        <th>
                            操作
                        </th>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
