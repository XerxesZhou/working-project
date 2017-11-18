<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomerPoolList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerPoolList" %>

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
    <link href="../@Css/easyui.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <%--<script type="text/javascript" src="../@Scripts/jquery.easyui.min.js"></script>--%>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>

    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>

    <script language="javascript" type="text/javascript">
    var EditFormUrl = "CustomerEditForm.aspx";
    
    
    //行上双击处理事件
function dbclick(obj, action)
{
    try
    {
        var td = obj.cells[0];
        var checkbox = td.childNodes[0];
        
        var url = EditFormUrl + "?FromType=&Action=" + action + "&ID=";
        url += checkbox.value;
        //window.open(url, "rform","","");
        OpenEditForm(url,1000,700);
    }
    catch(err)
    {
        displayErrorMsg("dbclick", err)
    }
}

function ShowChangeOperator() {
        if (this.selectCheck.length > 0)
        {
            //var obj = document.getElementById("UpdatePanel4");
            //var divChange = document.getElementById("divChange");

            //divChange.style.display = "";
            $("#divChange").show();
            $(".black_overlay").show();
        }
        else
        {
            alert("请选择需要转移的客户！");
        }
    }
    
   
    function CloseDiv(id)
    {
        var div = document.getElementById(id);
        div.style.display = "none";
        var divfade = document.getElementById("fade");
        divfade.style.display = "none";
    }

    function ConvertToLink() {
        ConvertToLinkCommon("GridCustomerPool", "客户名称");
    }

    </script>
    <style type="text/css">
        .bg
        {
            position: absolute;
            z-index: 100;
            top: 0px;
            left: 0px;
            background-color: #000;
            filter: alpha(opacity=60);
            -moz-opacity: 0.6;
            opacity: 0.6;
        }
      
      
      #divChange
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
    </style>

</head>

<body onload="javascript:SetGridViewScrolling('up1');"  style="overflow: visible; padding-bottom:0px; margin-bottom:0px;  ">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td  colspan="8">
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
            <tbody id="searchMain"  >
                <tr>
                    <th>
                        关键字：
                    </th>
                    <td>
                       <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        所属部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcusDepartmentID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        业务员：
                    </th>
                    <td>
                         <asp:DropDownList ID="SearchcusOperatorID" runat="server"  class="easyui-combobox">
                        </asp:DropDownList>
                    </td>
                    <th>
                        所属区域：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlcusAreaID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        客户类型：
                    </th>
                    <td>
                         <asp:DropDownList ID="SearchcusKindID" runat="server">
                        </asp:DropDownList>
                    </td>
                   
                    <th>
                        客户来源：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcusSourceID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                    客户状态：
                </th>
                <td>
                    <asp:DropDownList ID="SearchcusStatusID" runat="server">
                    </asp:DropDownList>
                </td>
                    <td colspan="2" style="text-align:center; vertical-align:middle;">
                        <input type="button" onclick='__doPostBack("ToolBar1$lb_Search","")' value="查询" style="width:60px;" class="btn_name"/>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerPool" runat="server" CssClass="Grid" GridLines="Both"
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
            <tr>
            <td>
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
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="300" Value="300"></asp:ListItem>
                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                            <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                        </asp:DropDownList>条记录
                    </div>
            </td>
            </tr>
            </table>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfSelectCheck" runat="server" />
        
        <div id="divChange">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                        <div style="margin: 0 auto;">
                            <span id="sWidth" style=" height:35px;">&nbsp;</span>
                        </div>
                        <table class="ctable" style="width: 300px; height: 100px;" align="center" border="0"
                            cellpadding="1" cellspacing="2">
                            <tr valign="Top">
                                <td class="tdTitle">
                                    &middot;选择转移至业务员
                                    <hr style="color: #79639D; size: 2px;" noshade />
                                </td>
                            </tr>
                            <tr valign="Top">
                                <td>
                                    <asp:DropDownList ID="ddlOperators" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr valign="bottom">
                                <td style="width: 73px" colspan="2">
                                    <asp:Button ID="btn_ConfirmChangeOperator" runat="server" Text="确定" CssClass="button" OnClick="btn_ConfirmChangeOperator_Click" />
                                    <input type="button" value="关闭" class="button" id="close" name="close" onclick="CloseDiv('divChange');" />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Change" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="fade" class="black_overlay" style="display: none; z-index:99;"></div>

    </form>
</body>
</html>
