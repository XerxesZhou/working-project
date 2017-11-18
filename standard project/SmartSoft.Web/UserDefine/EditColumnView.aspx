<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditColumnView.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditColumnView" %>

<html>
<head id="Head1" runat="server">
    <title>
        <%=Request.QueryString["ViewID"] != null ? "修改" : "新增"%>
        视图</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" language="javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" language="javascript" src="../@Scripts/webCalendar.js"></script>

    <script type="text/javascript" language="javascript" src="../@Scripts/AjaxCustomView.js"></script>

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>
    <style type="text/css">
    html
    {
         overflow:auto;
     }
    
    </style>
</head>
<body onload="javascript:OnLoadEvent(<%=Request.QueryString["TableID"].ToString() %>);">
    <form id="form1" runat="server">
        <table class="TableMain" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                 <td class="ListTitle">
                    &middot;编辑视图(<%=GetTableName() %>)
                </td>
                <td>
                     <a id="lb_Add" class="lbclass"
                            href="javascript:GoBackUrl(<%=Request.QueryString["TableID"].ToString() %>);">
                            <img src="../images/img_back2_25.png" alt="" />返回</a>
                            <a id="lb_Search" class="lbclass" href="javascript:Save();">
                        <img src="../images/img_160.png" alt="" />保存</a>
                </td>
            </tr>
            <tr>
                <td>
                    视图名称：<asp:TextBox ID="txtViewName" CssClass="Normal" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <fieldset>
                            <legend>设置查询条件</legend>
                            <table id="tbConditions" width="95%" class="TablePanel">
                                <tr id="row1" align="center">
                                    <th>
                                        字段
                                    </th>
                                    <th>
                                        条件
                                    </th>
                                    <th>
                                        值
                                    </th>
                                    <th>
                                        连接
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>设置排序条件</legend>
                            <table id="tbSorts" width="95%" class="TablePanel">
                                <tr id="myRow1" align="center">
                                    <th>
                                        字段
                                    </th>
                                    <th>
                                        顺序
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>选择列 选择范围：20列以内 按下Ctrl或者Shift键可进行多选</legend>
                            <table width="95%" class="List" align="center">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    字段来源：<select id="ddlTable" onchange="javascript:handleTableChange(this);"><option
                                                        selected="selected">本表</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="lbColumn" runat="server" Height="400px" Width="300px" SelectionMode="Multiple">
                                                    </asp:ListBox>
                                                </td>
                                                <td>
                                                    <input id="btnAdd" type="button" value="添 加" class="button" onclick="javascript:addSelected();" />
                                                    <br />
                                                    <br />
                                                    <input id="btnDelete" type="button" value="删 除" class="button" onclick="javascript:deleteSelected();" />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <input id="Button1" type="button" value="顶 部" class="button" onclick="javascript:topSelected();" />
                                                    <br />
                                                    <br />
                                                    <input id="Button2" type="button" value="向 上" class="button" onclick="javascript:upSelected();" />
                                                    <br />
                                                    <br />
                                                    <input id="Button3" type="button" value="向 下" class="button" onclick="javascript:downSelected();" />
                                                    <br />
                                                    <br />
                                                    <input id="Button4" type="button" value="底 部" class="button" onclick="javascript:bottomSelected();" />
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lbOrder" runat="server" Height="400px" Width="300px" SelectionMode="Multiple">
                                                    </asp:ListBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtColumn" runat="server" CssClass="hidden"></asp:TextBox>
                                                    <asp:TextBox ID="txtSort" runat="server" CssClass="hidden"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
