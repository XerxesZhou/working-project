<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ListLayout.aspx.cs" Inherits="UDEF.Web.UserDefine.ListLayout" %>


<html >
<head id="Head1" runat="server">
    <title>ҳ�沼���б�</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript">
    function DeleteLayout(ID) 
    {
        var res = true;
        if (window.confirm("���Ҫɾ��������"))
        {
            res = UDEF.Service.AjaxService.DeleteLayout(ID).value;
        }
        if (res != false && res != "false")
        {
            alert("ɾ�����ֳɹ���");
            document.getElementById("btnRefresh").click();
        }
        else
        {
            alert("ɾ������ʧ�ܣ�");
        }
    }
    
    function AddLayout()
    {
        var tableID = document.getElementById("ddlSeleteTable").value;
        var strUrl = "EditLayout.aspx?EditMode=add&TableID=" + tableID;
        window.location = strUrl;
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
    function backToTableList()
    {
        window.location = "ListTable.aspx";
    }
</script>

<body>
    <form id="form1" runat="server">
        <div>
            <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
                <tr class="ToolBar">
                     <td>
                        &middot;���ֹ���
                    </td>
                    <td>
                        ѡ���
                        <asp:DropDownList ID="ddlSeleteTable" runat="server" OnSelectedIndexChanged="ddlSeleteTable_SelectedIndexChanged"
                            AutoPostBack="true" Width="200px">
                        </asp:DropDownList>
                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="Hidden" />
                        <a onclick="javaScript:AddLayout();" id="lb_Add" style="margin-right:10px;" class="lbclass" href="javaScript:AddLayout();">
                            <img src="../images/img_160.png" alt="" />����</a>
                        <a id="lb_GoBack" class="lbclass" style="color:#FFF; text-decoration:underline;" href="javascript:backToTableList();">
                            <img src="../@images/close.gif" alt="" />�����</a>
                    </td>
                </tr>
            </table>
            <div id="divGridView">
                <asp:GridView CssClass="Grid" ID="GridView1" GridLines="Both" runat="server" HorizontalAlign="center"
                    AllowSorting="true" OnSorting="GridView1_Sorting" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="false"  OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="LayoutID" HeaderText="���ֱ��" />
                        <asp:BoundField DataField="LayoutName" HeaderText="��������" />
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%#GetOperation(Eval("LayoutID").ToString(), Eval("TableID").ToString(),bool.Parse(Eval("IsSystemDefine").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        û�в��֣�
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
