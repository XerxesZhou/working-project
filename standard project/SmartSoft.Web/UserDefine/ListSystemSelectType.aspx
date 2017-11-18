<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListSystemSelectType.aspx.cs"
    Inherits="UDEF.Web.UserDefine.ListSystemSelectType" %>

<html>
<head id="Head1" runat="server">
    <title>ϵͳ����Դ</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>

    <script id="Infragistics" type="text/javascript">


        $(function () {
            var h = window.innerHeight;
            $("#divGridView").css("height", h - 71);
        })

        function EditSystemSelectType( selectTypeID )
        {
        
	        var strUrl = "EditSystemSelectType.aspx";
	        if (selectTypeID != null)
            {
                strUrl += "?SelectTypeID=" + selectTypeID;
            }
            window.location = strUrl;
        }

        function DeleteSystemSelectType( ID )
        {
            if (window.confirm("ȷ��Ҫɾ����\r���鲻Ҫɾ����"))
            {
                var bSuccess = UDEF.Service.AjaxService.DeleteSystemSelectType(ID).value;
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
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="lb_Search">
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                 <td>
                    &middot; ϵͳ����Դ
                </td>
                <td>
                    <a id="lb_Add" style="margin-right:10px;" class="lbclass" href="javaScript:EditSystemSelectType();">
                        <img src="../images/img_11.png" alt=""style="width:30px; height:30px; margin-bottom:3px;" />����</a>
                    <asp:LinkButton ID="lb_Search" OnClick="btnRefresh_Click" CssClass="lbclass" runat="Server"><img src="../images/img_search1.png" alt=""/>��ѯ</asp:LinkButton>
                    
                </td>
            </tr>
        </table>
        <div>
            <fieldset>
                <table width="100%" class="List">
                    <tr>
                        <th>
                            ����Դ���ƣ�
                        </th>
                        <td>
                            <asp:TextBox ID="SearchSelectTypeName" runat="server"></asp:TextBox>
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
                <asp:GridView CssClass="Grid" ID="GridView1" GridLines="Both" runat="server"
                    AllowSorting="true" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
                    PageSize="15" AllowPaging="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="SelectTypeName" HeaderText="����Դ����" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeName" />
                        <asp:BoundField DataField="SelectTypeText" HeaderText="��ʾ����" ItemStyle-HorizontalAlign="left"
                            SortExpression="SelectTypeText" />
                        <asp:BoundField DataField="PrimaryID" HeaderText="���б�ֵ���ֶ�" ItemStyle-HorizontalAlign="left"
                            SortExpression="PrimaryID" />
                        <asp:BoundField DataField="PrimaryName" HeaderText="���б��ı����ֶ�" ItemStyle-HorizontalAlign="left"
                            SortExpression="PrimaryName" />
                        <asp:BoundField DataField="TableName" HeaderText="������" ItemStyle-HorizontalAlign="left"
                            SortExpression="������" />
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("SelectTypeID").ToString())%>
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
