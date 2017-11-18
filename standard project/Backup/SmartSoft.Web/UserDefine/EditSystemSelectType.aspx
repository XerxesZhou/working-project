<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditSystemSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditSystemSelectType" %>

<html>
<head id="Head1" runat="server">
    <title>�༭����Դ��Ϣ��</title>

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript">
    function backToList() {
        window.location = "ListSystemSelectType.aspx";
    }
</script>

<body>
    <form id="form1" runat="server">
        <div>
            <table class="TableMain" cellpadding="0" cellspacing="0" >
                <tr class="ToolBar">
                    <td class="ListTitle">
                       <asp:Literal runat="server" ID="liTitle"></asp:Literal>ϵͳ����Դ
                    </td>
                    <td>
                        <a id="lb_Search" style=" margin-right:10px; margin-left:15px;" class="lbclass" href="javascript:backToList();">
                            <img src="../images/img_back2_25.png" alt="" />����</a>
                        <asp:LinkButton ID="lb_Add" CssClass="lbclass" runat="Server" OnClick="btnSubmit_Click"><img src="../images/img_160.png" alt="" />����</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>ϵͳ�ֶ�����Դ</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            ���ƣ�
                        </th>
                        <td>
                            <asp:TextBox ID="FieldSelectTypeName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            ��ʾ�ı���
                        </th>
                        <td>
                            <asp:TextBox ID="FieldSelectTypeText" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            �����ƣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldTableName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FieldTableName_SelectedIndexChanged"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ���б�ֵ���ֶΣ�
                        </th>
                        <td>
                            <asp:TextBox ID="FieldPrimaryID" runat="server" ReadOnly="true" CssClass="OnlyBottom"></asp:TextBox>
                        </td>
                        <th>
                            ���б��ı����ֶΣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldPrimaryName" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <th>
                            ͣ�ñ�־���ֶΣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldStopTagName" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ��ע��
                        </th>
                        <td colspan="5">
                            <asp:TextBox ID="FieldRemark" runat="server" TextMode="MultiLine" Columns="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ����������
                        </th>
                        <td colspan="5">
                            <asp:TextBox ID="FieldWhereCondition" runat="server" TextMode="MultiLine" Columns="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ����������
                        </th>
                        <td colspan="5">
                            <asp:TextBox ID="FieldOrderByCondition" runat="server" TextMode="MultiLine" Columns="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
