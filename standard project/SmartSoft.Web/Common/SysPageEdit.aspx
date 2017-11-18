<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SysPageEdit.aspx.cs" Inherits="SmartSoft.Web.Common.SysPageEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑页面信息</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <style type="text/css">
        #cbl_PageFunction
        {
            width: 1000px;
        }
        
        #rbl_MenuImagePath tr td
        {
            background-color: #0174CF;
        }
        .lbclass img
        {
            margin-bottom: 8px;
        }
        
        .input
        {
            line-height: 20px;
            border: solid 1px rgb(204,204,204);
            width: 180px;
        }
        .tdFont
        {
            text-align: right;
        }
        html
        {
            overflow: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="ctable" style="width:100%; height: 500px; margin: 0 auto;" border="0"
            cellpadding="0" cellspacing="0">
            <tr style="height: 25px;">
                <td style="height: 25px">
                    <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="Top" class="ToolBar" style="height:50px;">
                            <td>
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                              <%--  <hr style="color: #79639D; size: 2px;" noshade />--%>
                            </td>
                            <td >
                                <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                    <img src="../images/img_newclose.png" style="border: 0px;width:30px;height:30px; margin-bottom:8px;" />关闭 </a>
                                     <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" />保存
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="Top">
                <td valign="top">
                    <table class="ctable" style="width: 100%; height: 100%; margin: 0 auto;" cellspacing="1"
                        cellpadding="1" border="0">
                        <%--<tr align="left">
                            <td colspan="4" class="tdTitle" style="height: 30px">
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                                <hr style="color: #79639D; size: 2px;" noshade />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdFont" align="center" style="width: 106px">
                                编号：</td>
                            <td>
                                <asp:UpdatePanel ID="upRefresh" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="tb_PageID" runat="Server"  CssClass="input"></asp:TextBox>
                                   </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="tdFont" align="center" style="width: 106px">
                                显示名称：</td>
                            <td>
                                <asp:TextBox ID="tb_PageName" runat="Server" CssClass="input" ></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="tdFont" align="center" style="width: 106px">
                                地址：</td>
                            <td>
                                <asp:TextBox ID="tb_PageFilePath" runat="Server" CssClass="input"  Width="330px"></asp:TextBox>&nbsp;</td>
                            <td class="tdFont" align="center">
                                是否菜单节点：</td>
                            <td>
                                <asp:RadioButtonList ID="rbl_IsMenuDirectory" runat="server" RepeatDirection="horizontal">
                                    <asp:ListItem Text="否" Value="False" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="是" Value="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <%--<td class="tdFont" align="center" style="width: 106px">
                                版面号</td>
                            <td>
                                <asp:TextBox ID="tb_PageNo" runat="Server" BorderStyle="groove"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr style="height: 100%">
                            <td colspan="4">
                                <fieldset>
                                    <legend>菜单选项</legend>
                                    <table>
                                        <tr>
                                            <td class="tdFont" align="center">
                                                是否菜单：</td>
                                            <td>
                                                <asp:RadioButtonList ID="rbl_IsMenu" runat="server" RepeatDirection="horizontal">
                                                    <asp:ListItem Text="否" Value="False" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="是" Value="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFont" align="center" style="width: 106px">
                                                所属菜单节点：</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_MenuParentPageID" runat="Server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFont" align="center" style="width: 106px">
                                                排序号：</td>
                                            <td>
                                                <asp:TextBox ID="tb_MenuOrderBy" CssClass="input"   runat="Server">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFont" colspan="2">
                                                菜单图片：
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="background-color:#0174CF;">
                                                <asp:RadioButtonList ID="rbl_MenuImagePath" runat="server" RepeatDirection="horizontal"
                                                    RepeatColumns="10">
                                                   <asp:ListItem Text="不用" Value="" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_02.png'/>" Value="../@images/img_02.png"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_03.gif' />" Value="../@images/img_03.gif"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_06.png' />" Value="../@images/img_06.png"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_10.gif' />" Value="../@images/img_10.gif"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_12.gif' />" Value="../@images/img_12.gif"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_18.png' />" Value="../@images/img_18.png"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_19.png' />" Value="../@images/img_19.png"></asp:ListItem>
                                                    <asp:ListItem Text="<img src='../@images/img_32.png' />" Value="../@images/img_32.png"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr style="height: 100%">
                            <td colspan="4">
                                <fieldset>
                                    <legend class="tdTitle">选择页面功能</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="cbl_PageFunction" runat="server" RepeatColumns="7" RepeatDirection="horizontal">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr style="height: 100%">
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
