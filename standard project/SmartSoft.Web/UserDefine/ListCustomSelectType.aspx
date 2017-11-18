<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListCustomSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListCustomSelectType" %>

<html>
<head id="Head1" runat="server">
    <title>�Զ�������Դ</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script id="Infragistics" type="text/javascript">
    <!--
    function EditCustomSelectType(selectTypeID)
    {
        
	    var strUrl = "EditCustomSelectType.aspx";
	    if (selectTypeID != null)
        {
            strUrl += "?SelectTypeID=" + selectTypeID;
        }
        window.location = strUrl;
    }

    function DeleteCustomSelectType( ID )
    {
        try
        {
            if (window.confirm("ȷ��Ҫɾ����\r���鲻Ҫɾ����"))
            {
                var bSuccess = UDEF.Service.AjaxService.DeleteCustomSelectType(ID).value;
                if (bSuccess)
                {
                    alert("ɾ���ɹ���");
                    document.getElementById("btnRefresh").click();
                }
                else
                {
                   alert("ɾ��ʧ�ܣ�");
                }
            }
        }
        catch(err)
        {
            displayErrorMsg("DeleteCustomSelectType", err);
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
    <form id="form1" runat="server" defaultbutton="lb_Search">
        <table class="ListHeader" id="ListHeader">
            <tr class="ListTitle">
                <td colspan="2">
                    &middot; �Զ�������Դ
                </td>
            </tr>
            <tr class="ToolBar">
                <td>
                    <asp:LinkButton ID="lb_Search" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../@images/search.gif" alt=""/>��ѯ</asp:LinkButton>
                    <a id="lb_Add" class="lbclass" href="javaScript:EditCustomSelectType();">
                        <img src="../@images/new.gif" alt="" />����</a>
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            ����Դ��ţ�
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeID" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            ����Դ��ʾ�ı���
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" ID="GridView1" runat="server" HorizontalAlign="center"
                    AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="false"
                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SelectTypeID" HeaderText="����Դ���" ItemStyle-HorizontalAlign="center"
                            SortExpression="SelectTypeID" />
                        <asp:BoundField DataField="SelectTypeText" HeaderText="��ʾ����" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeText" />
                        <asp:BoundField DataField="Remark" HeaderText="��ע" ItemStyle-HorizontalAlign="left"
                            SortExpression="Remark" />
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("SelectTypeID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        û�����ϣ�
                    </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" />
                    <PagerStyle HorizontalAlign="right" CssClass="GridFooter" />
                    <AlternatingRowStyle CssClass="GridAlternating" />
                    <RowStyle CssClass="GridRow" />
                    <HeaderStyle CssClass="GridHeader" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
