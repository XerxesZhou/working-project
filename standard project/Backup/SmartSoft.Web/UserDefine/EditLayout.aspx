<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditLayout.aspx.cs" Inherits="UDEF.Web.UserDefine.EditLayout" %>

<html>
<head id="Head1" runat="server">
    <title>布局</title>
    <style type="text/css">
        td
        {
            font-size:12px;
        }
        
    </style>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js">
    </script>

    <script type="text/javascript" language="javascript" src="../@Scripts/AjaxCustomLayout.js">
    </script>

</head>

<body onload="javascript:loadAll();" onunload="javascript:handleUnload();">
    <form id="form1" runat="server">
        <table class="TableMain" cellpadding="0" cellspacing="0">
            <tr class="ToolBar" style="height:50px;">
                 <td class="ListTitle">
                    &middot; 编辑布局(<%=GetTableName() %>)
                </td>
                <td>
                    <div>
                    <a id="ToolBar1_lb_Add" class="lbclass" style="color:#FFF;" href="javascript:openSectionEdit('', event);">
                        <img src="../images/img_11.png" alt="" />新增部分</a> <a id="ToolBar1_lb_Edit"  style="color:#FFF;" class="lbclass"
                            href="javascript:openFieldEdit(event);" onmouseup="doNothing(event);" >
                            <img src="../images/img_design.png" alt="" />编辑字段属性</a>
                    <a id="lb_Save"  style="color:#FFF;" class="lbclass" href="javascript:saveAll();">
                        <img src="../images/img_160.png" alt="" />保存</a> <a id="lb_GoBack"  style="color:#FFF;" class="lbclass"
                            href="javascript:javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);">
                            <img src="../images/img_back2_25.png" alt="" />返回</a>
                            </div>
                </td>
            </tr>
             <tr>
                <td>
                    布局名称：<asp:TextBox ID="txtLayoutName" onmouseup="doNothing(event);" CssClass="Normal" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div>
            <asp:Panel ID="Pane_Main" runat="server">

                <script language="javascript">
                </script>
                <table>
                    <tr>
                        <td valign="top">
                            <table id="mainLayoutTable" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td id="rp_table0" height="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="table0" height="5" cellspacing="1" cellpadding="2" width="400" bgcolor="#ffffff"
                                            border="0">
                                            <tbody>
                                                <tr bgcolor="#ffffff">
                                                    <td id="sec_s0" onmouseover="handleMouseOver(event);" colspan="2">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>

                                <script type="text/javascript"> document.getElementById('sec_s0') .tableId = 'table0';</script>

                            </table>
                        </td>
                        <td>
                            <table height="1" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td id="scrollBuffer" bgcolor="#0000ff" height="1">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table id="availableSectionWrapper" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td align="center">
                                            <div id="div_afields_1" style="display: none">

                                                <script> swapDivs('div_afields_1', 'div_afields_1');</script>

                                                <table cellspacing="1" cellpadding="2" width="300" bgcolor="#cccccc">
                                                    <tbody>
                                                        <tr bgcolor="#cccccc">
                                                            <td>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr valign="bottom">
                                                                            <td>
                                                                                字段来源
                                                                            </td>
                                                                            <td>
                                                                                <select id="ddlTable" onchange="javascript:handleTableChange(this);">
                                                                                    <option selected="selected">本表</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr bgcolor="#ffffff" height="15">
                                                            <td>
                                                                <table id="afields_1" cellspacing="2" width="100%" bgcolor="#ffffff">
                                                                    <tbody>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr bgcolor="#cccccc">
                                                            <td>
                                                                <table width="100%">
                                                                    <tbody>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table id="properties" style="visibility: hidden; position: absolute" cellspacing="1"
                cellpadding="1" width="150" bgcolor="#000000" border="0">
                <tr>
                    <td id="propHeader" style="font-size: 9pt; color: #ffffff; height: 17px" bgcolor="#336699">
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff">
                        <table cellspacing="1" cellpadding="1" width="100%" bgcolor="#ffffff" border="0">
                            <tr>
                                <td>
                                    <div id="propDiv0">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText0" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv1">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText1" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv2">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText2" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv3">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText3" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv4">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText4" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv5">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText5" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv6">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText6" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv7">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText7" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv8">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText8" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="propDiv9">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr valign="top">
                                                <td id="propText9" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="dragDummy" style="z-index: 1000; visibility: hidden; position: absolute;
                background-color: #000000" height="15" cellspacing="1" width="25">
                <tr>
                    <td id="dragDummyValue" style="background-color: #6699cc" nowrap>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
