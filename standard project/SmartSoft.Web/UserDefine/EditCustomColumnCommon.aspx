<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditCustomColumnCommon.aspx.cs"
    Inherits="UDEF.Web.UserDefine.EditCustomField_First" %>

<html>
<head id="Head1" runat="server">
    <title>�༭�Զ����ֶ�</title>

    <script type="text/javascript" language="javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" src="../@Scripts/AjaxCustomField.js"></script>

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />

    <script type="text/javascript">
    function backToList(TableID)
    {
        window.location = "ListColumn.aspx?TableID=" + TableID;
    }
    
    function handleDataTypeChange()
    {
        var ddlDataType = document.getElementById("FieldDataType");
        var ddlSelectType = document.getElementById("FieldSelectType");
        
        if (ddlDataType.value == '5')   //����������������ѡ������Դ��ѡ��
        {
            ddlSelectType.style.display = '';
        }
        else
        {
            ddlSelectType.style.display = 'none'; 
            ddlSelectType.selectedIndex = -1; 
        }
    }
    
    function handleIsFormulaChange()
    {
        var trFormula = document.getElementById("trFormula");
        var cbIsFormula = document.getElementById("FieldIsFormula"); 
        var txtColumnName = document.getElementById("FieldColumnName");
        if (cbIsFormula.checked)  //�����ʽ��������ʾ�༭��ʽ����
        {
            trFormula.style.display = "";
        }
        else
        {
            trFormula.style.display = "none";
            txtColumnName.value = "";
        } 
    }
 
    function handleOnLoad()
    {
        handleDataTypeChange();
        //handleIsFormulaChange();
    }
    </script>

</head>
<body onload="javascript:handleOnLoad();">
    <form id="form1" runat="server">
        <div>
            <table class="TableMain">
                <tr>
                    <td class="ListTitle" style="height: 20px">
                        &middot;�༭�Զ����ֶ���Ϣ
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="lb_Save" runat="Server" CssClass="lbclass" OnClick="btnSubmit_Click"><img src="../@images/save.gif" alt="" />����</asp:LinkButton>
                        <a onclick="javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);"
                                id="ToolBar1_lb_Close" class="lbclass" href="javascript:backToList(<%=Request.QueryString["TableID"].ToString() %>);">
                                <img src="../@images/close.gif" alt="" />�ر�</a>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>�Զ����ֶ�����</legend>
                <table width="95%" class="TablePanel" align="center">
                    <tr>
                        <th>
                            �ֶα��룺
                        </th>
                        <td>
                            <asp:TextBox ID="FieldColumnID" runat="server" CssClass="OnlyBottom" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            �ֶ����ͣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="FieldDataType" runat="server" onchange="javascript:handleDataTypeChange();">
                            </asp:DropDownList>
                            <asp:DropDownList ID="FieldSelectType" runat="server">
                            </asp:DropDownList>
                            <%--<asp:CheckBox ID="FieldIsFormula" runat="server" onclick="javascript:handleIsFormulaChange();" />--%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ��������˵����
                        </th>
                        <td>
                            <asp:TextBox ID="txtDetails" runat="server" TextMode="multiLine" Width="100%" CssClass="OnlyBottom"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trFormula">
                        <th>
                            �ֶ�����
                        </th>
                        <td>
                            <asp:TextBox TextMode="MultiLine" ID="FieldColumnName" runat="server" Height="50px"
                                Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            ��ʾ�ı���
                        </th>
                        <td>
                            <asp:TextBox ID="FieldColumnText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Ĭ��ֵ��
                        </th>
                        <td>
                            <asp:TextBox ID="FieldDefaultValue" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            �ֶ�������
                        </th>
                        <td>
                            <asp:TextBox ID="FieldRemark" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div>
            &nbsp;
            <br />
        </div>
    </form>
</body>
</html>
