<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditColumnViewExpression.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditColumnViewExpression" %>

<html>
<head id="Head1" runat="server">
    <title>�༭��ͼ�������ű�</title>

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
</head>
<base target="_self"></base>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TableMain">
                <tr>
                    <td class="ListTitle">
                        &middot;�༭��ͼ����
                    </td>
                </tr>
                <tr class="ToolBar">
                    <td>
                        <asp:LinkButton ID="lb_Save" CssClass="lbclass" runat="Server" OnClick="btnSubmit_Click"><img src="../@images/save.gif" alt="" />����</asp:LinkButton>
                        <a id="lb_GoBack" class="lbclass" href="javascript:window.close();">
                            <img src="../@images/close.gif" alt="" />����</a>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>��ͼ������������</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            ���ű�ţ�
                        </th>
                        <td>
                            <asp:TextBox ID="FieldExpressionID" runat="server" ReadOnly="true" CssClass="OnlyBottom"></asp:TextBox>
                        </td>
                        <th>
                            �������ͣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldUseDataType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            �������ƣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldExpressionName" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th>
                        </th>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ���ã�
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldUseTag" runat="server" Enabled="false"></asp:CheckBox>
                        </td>
                        <th>
                            ͣ�ã�
                        </th>
                        <td>
                            <asp:CheckBox ID="FieldStopTag" runat="server" Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
        </div>
    </form>
</body>
</html>
