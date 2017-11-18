<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkDiaryList.aspx.cs" Inherits="SmartSoft.Web.Data.WorkDiaryList" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=title%>
    </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../@Scripts/window.js"></script>

    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>

    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>
    
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>

    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <style type="text/css">
        #divFileUpload
        {
            position: absolute;
            top: 40%;
            left: 50%;
            margin: -70px 0px 0px -190px;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color: #eeeeee;
            filter: alpha(opacity=90);
        }
        
        .black_overlay
        {
            position: fixed;
            z-index: 1000;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            filter: alpha(opacity=80);
            opacity: 0.3;
            overflow: hidden;
            background-color: #000;
        }
        
        #up1
        {
            overflow :hidden !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
    var EditFormUrl = "CustomerWorkReportEditForm.aspx";

    function Insert() {
        var url = "CustomerWorkReportAdd.aspx";
        OpenEditForm(url, 450, 600);
    }

    function onpress()
    {
        if(event.keyCode == 13)
        {
            __doPostBack("ToolBar1$lb_Search","")
        }
    }

    function ConvertToLink() {
        var table = $("#GridWorkDiary", ".sData").get(0);
        if (table != null && table.rows.length > 1) {
            for (var i = 1; i < table.rows.length; i++) {

                var row = table.rows[i];
                var tdCustomerName = row.cells[2].innerText;
                var td = row.cells[0];
                var checkbox = td.childNodes[0];
                var url = "CustomerEditForm.aspx?FromType=sellorder&Action=View&ID=";
                url += checkbox.value;

                row.cells[2].innerHTML = "<a href=\"javascript:OpenEditForm('" + url + "',1000,700)\">" + tdCustomerName + "</a>";
            }
        }
    }

    function ShowFileUpload() {
        var divFileUpload = document.getElementById("divFileUpload");
        $("#divFileUpload").show();
        $(".black_overlay").show();
    }

    function checkFileUpload() {
        var file = document.getElementById("FileUpload1").value;
        if (file == "") {
            alert("请选择文件！");
            return false;
        }
        return true;
    }
    function CloseDiv(id) {
        var div = document.getElementById(id);
        div.style.display = "none";
        var divfade = document.getElementById("fade");
        divfade.style.display = "none";
    }
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');__doPostBack('ToolBar1$lb_Search','');"
    onkeypress="onpress();" style="overflow: hidden;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td  class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        <%=title %>&middot;查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td colspan="6">
                        <table class="ListHeader" id="ListHeader" cellspacing="0" cellpadding="0" border="0">
                            <tr class="ToolBar">
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
                    </td>
                </tr>
            </tbody>
            <tbody id="searchMain">
                <tr>
                    <th>
                        创建人：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="SearchwdAddOperatorID">
                        </asp:DropDownList>
                    </td>
                    <th>
                        <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchwdDateBegin','SearchwdDateEnd');" />
                        日期：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchwdDateBegin" Width="120px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchwdDateEnd" Width="120px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridWorkDiary" runat="server" CssClass="Grid" GridLines="Both"
                    AutoGenerateColumns="False" AllowSorting="True">
                    <FooterStyle CssClass="GridFooter" />
                    <RowStyle CssClass="Row" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="RowA" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Search" EventName="Click" />
                
                <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Delete" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="AspNetPager1" EventName="PageChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <div class="footpager" >
              <table cellpadding="0" cellspacing="0" border="0" style=" width:100%">
              <tr >
              <td style=" width:100%;">
               <div class="Pager" style="width:100%; float:left;">
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager>
                </div>
              </td>
              <td>
               <div style="width:200px; float:right;">
                     每页显示：
                    <asp:DropDownList Width="50px" runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>条记录
                </div>
              </td>
              </tr>
              </table>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfSelectCheck" runat="server" />
         <%--<div id="divFileUpload">
                    <div style="margin: 0 auto;">
                        <span id="Span2" style="height: 35px;">&nbsp;</span>
                    </div>
                    <table class="ctable" style="width: 300px; height: 100px; padding: 20px 10px 20px 10px;"
                        align="center" border="0" cellpadding="1" cellspacing="2">
                        <tr valign="Top">
                            <td class="tdTitle" colspan="2">
                                &middot;选择上传文件
                                <hr style="color: #79639D; size: 2px;" />
                            </td>
                        </tr>
                        <tr valign="Top">
                            <td>
                                <asp:FileUpload ID="FileUpload1" CssClass="easyui-FileUpload1" runat="server" />
                            </td>
                        </tr>
                        <tr valign="bottom">
                            <td style="width: 73px" colspan="2" align="center">
                                <asp:Button ID="btn_FiUpload" runat="server" Text="确定" CssClass="button" OnClientClick="return checkFileUpload()"
                                    OnClick="btn_FiUpload_Click" />
                                <input type="button" style="margin-top: 15px;" value="关闭" class="button" id="Button3"
                                    name="close" onclick="CloseDiv('divFileUpload');" />
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <div id="fade" class="black_overlay" style="display: none; z-index:99;"></div>
    </form>
</body>
</html>

