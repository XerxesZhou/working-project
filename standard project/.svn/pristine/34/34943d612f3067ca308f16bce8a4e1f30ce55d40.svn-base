<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DepartmentList.aspx.cs"
    Inherits="SmartSoft.Web.Common.DepartmentList" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门资料列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <link href="../@Css/Myself.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="../@Scripts/window.js" type="text/javascript"></script>
    <script type="text/javascript" src="../@Scripts/CheckBox.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    
    <script language="javascript" type="text/javascript">
    function Add()
    {
        openwinsimp("DepartmentEdit.aspx", 700, 350);
    }
    
    function Edit()
    {
        try
        {
            var url = "DepartmentEdit.aspx?";
            CheckBoxOpenWindowHandle(url + "&depID=", 420, 350, "请选择一条需要修改的记录!");
        }
        catch(err)
        {
            displayErrorMsg("Edit", err)
        }
    }
    
    function Delete()
    {
        return CheckBoxMultiHandle('确定要删除吗？');
    }
    

    function ShowOperator(id)
    {
        var obj = document.getElementById("UpdatePanel1");
        var divExport = document.getElementById("divExport");
        
        divExport.style.left = getObjX(obj);
        divExport.style.top = getObjY(obj);
        divExport.style.width = getContainerWidth();
        divExport.style.height = getContainerHeight();
        
        var objWidth = document.getElementById("sWidth");
        
        //objWidth.style.height = (getContainerHeight()-500)/2;
        divExport.style.display = "";
       
        SetValue(GetElementById("tb_depID"),id);
        __doPostBack("btn_Load","");
    }
    
    function CloseDiv()
    {
        var divExport = document.getElementById("divExport");
        divExport.style.display = "none";
    }
    
    function ShowManager(id)
    {
        var obj = document.getElementById("UpdatePanel1");
        var divExport = document.getElementById("divManager");
        
        divExport.style.left = getObjX(obj);
        divExport.style.top = getObjY(obj);
        divExport.style.width = getContainerWidth();
        divExport.style.height = getContainerHeight();
        
        divExport.style.display = "";
       
        SetValue(GetElementById("tb_depID"),id);
        __doPostBack("btn_LoadManager","");
    }
    
    function CloseDivManager()
    {
        var divExport = document.getElementById("divManager");
        divExport.style.display = "none";
    }
    </script>
        <style type="text/css">
    html
    {
         overflow:auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
            <tr class="ToolBar">
                <td>
                    &middot;部门资料列表
                </td>
                <td>
                    <toolBar:ToolBar ID="ToolBar1" runat="server"></toolBar:ToolBar>
                </td>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <progressBar:ProgressBar ID="Progress1" runat="server"></progressBar:ProgressBar>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridDepartment" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                    PageSize="20" AllowPaging="false" ShowHeader="True" DataKeyNames="depID" AllowSorting="True"
                    OnRowDataBound="GridDepartment_RowDataBound">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                    <Columns>
                        <asp:BoundField DataField="Id" Visible="false" />
                        <asp:TemplateField ItemStyle-Width="20px" ItemStyle-Height="16px" HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;">
                            <ItemTemplate>
                                <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "depID")%>'
                                    onclick='SingleCheckJs(this);' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="depName" HeaderText="名称" />
                        <asp:BoundField DataField="depChargeMan" HeaderText="负责人" />
                        <asp:BoundField DataField="depTel" HeaderText="电话" />
                        <asp:BoundField DataField="depFax" HeaderText="传真" />
                        <asp:BoundField DataField="depOrderAmount" HeaderText="合同额" />
                        <asp:BoundField DataField="depReceiptAmount" HeaderText="收款额" />
                        <asp:BoundField DataField="depRemark" HeaderText="备注" />
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Refresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
            
        <asp:Button ID="btn_Refresh" runat="server" CssClass="hidden" OnClick="btn_Refresh_Click" />
        <asp:TextBox ID="tb_depID" runat="server" CssClass="hidden"></asp:TextBox>
        <asp:HiddenField ID="hfSelectCheck" runat="server" />
    </form>
</body>
</html>
