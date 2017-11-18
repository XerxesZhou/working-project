<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListColumnViewExpression.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListColumnViewExpression" %>

<%@ Import Namespace="UDEF.Component" %>
<%@ Import Namespace="UDEF.Domain.Enumerate" %>
<html>
<head id="Head1" runat="server">
    <title>��ͼ�������ű�</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script id="Infragistics" type="text/javascript">
    <!--
    function EditOperator( operatorID )
    {
	    var strUrl = "EditColumnViewExpression.aspx";

	    if (operatorID != null)
        {
            strUrl += "?ExpressionID=" + operatorID;
        }
        var retVal = window.showModalDialog(strUrl, "", "dialogHeight:200px; dialogWidth:500px; center:yes; status:no; scroll:no;");
        if(retVal)
        {
		    alert('�ѳɹ��༭��ͼ�������ű����ϣ�');
		    document.getElementById("btnRefresh").click();
		}
    }

    function StopOperator( ID )
    {
        if (window.confirm("ȷ��Ҫͣ����\n����ʹ��������ţ�"))
        {
            var bSuccess = UDEF.Service.AjaxService.StopOrOpenColumnOperator(ID, true).value;
            if (bSuccess)
            {
                alert("ͣ�óɹ���");
                document.getElementById("btnRefresh").click();
            }
            else
            {
               alert("ͣ��ʧ�ܣ�");
            }
        }
    }
    
    function OpenOperator( ID )
    {
        if (window.confirm("ȷ��Ҫ������\n��������������ţ�"))
        {
            var bSuccess = UDEF.Service.AjaxService.StopOrOpenColumnOperator(ID, false).value;
            if (bSuccess)
            {
                alert("���óɹ���");
                document.getElementById("btnRefresh").click();
            }
            else
            {
               alert("����ʧ�ܣ�");
            }
        }
    }

    // �ı��е���ɫ
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
    // -->
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="ListHeader" id="ListHeader">
            <tr class="ListTitle">
                <td colspan="2">
                    &middot;��ͼ�������ű�
                </td>
            </tr>
            <tr class="ToolBar">
                <td>
                    <asp:LinkButton ID="btnRefresh" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../@images/search.gif" alt=""/>��ѯ</asp:LinkButton>
                    <a id="btnAdd" class="lbclass" href="javaScript:EditOperator();">
                        <img src="../@images/new.gif" alt="" />����</a>
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            �������ƣ�
                        </th>
                        <td>
                            <asp:TextBox ID="SearchExpressionName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            ����ֵ��
                        </th>
                        <td>
                            <asp:TextBox ID="SearchExpressionValue" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            �������ͣ�
                        </th>
                        <td>
                            <asp:DropDownList ID="SearchUseDataType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" GridLines="Both" ID="GridView1" runat="server" HorizontalAlign="center"
                    AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false"
                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ExpressionID" HeaderText="���ű��" SortExpression="ExpressionID" />
                        <asp:TemplateField HeaderText="������������" SortExpression="UseDataType">
                            <ItemTemplate>
                                <%# StringHelper.IsValidDBInt(Eval("UseDataType")) ? DataType.GetText(int.Parse(Eval("UseDataType").ToString())) : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpressionName" HeaderText="��������" SortExpression="ExpressionName" />
                        <%--<asp:BoundField DataField="ExpressionValue" HeaderText="����ֵ" />--%>
                        <asp:TemplateField HeaderText="����" SortExpression="UseTag">
                            <ItemTemplate>
                                <%# bool.Parse(Eval("UseTag").ToString()) ? "��" : "��"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ͣ��" SortExpression="StopTag">
                            <ItemTemplate>
                                <%# bool.Parse(Eval("StopTag").ToString()) ? "��" : "��"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("ExpressionID").ToString(), bool.Parse(Eval("StopTag").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        û�����ϣ�
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
