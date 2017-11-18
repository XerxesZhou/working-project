﻿ <%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomerClueList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerClueList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
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
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>

    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script language="javascript" type="text/javascript">
    var EditFormUrl = "CustomerClueEditForm.aspx";
    
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
            OpenEditForm(url,800,560);
        }
        catch(err)
        {
            displayErrorMsg("dbclick", err)
        }
    }

    function Insert() {
        var url = "CustomerClueAdd.aspx";
        OpenEditForm(url, 450, 600);
    }

    function ShowChangeOperator(id)
    {
        if (this.selectCheck.length > 0)
        {
            var obj = document.getElementById("UpdatePanel4");
            var divChange = document.getElementById("divChange");
           
            divChange.style.display = "";
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
        ConvertToLinkCommon("GridCustomerClue", "客户名称");
    }


    function ShowSendMobileMessage() {
        if (this.selectCheck.length > 0) {
            var obj = document.getElementById("UpdatePanel3");
            var divChange = document.getElementById("divSendMobileMessage");
            $("#divSendMobileMessage").show();
            $(".black_overlay").show();
        }
        else {
            alert("请选择需要发短信的联系人！");
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
    
    function checkSms()
    {
        var content = document.getElementById("txtMobileMessage").value;
        if (content == "")
        {
            alert("请录入内容");
            return false;
        }

        return true;
    }
    </script>
    <style type="text/css">
            .GridCustomer_box
            {
                     width: 99%;
                
                 margin-bottom:none;
                    padding: 0px;
                    overflow: hidden;

                }
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
        
        #divSendMobileMessage
        {
            position: absolute;
            top: 40%;
            left: 40%;
            margin: -70px 0px 0px -190px;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color: #eeeeee;
            filter: alpha(opacity=90);
        }
        
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
        
        #txtSendDate
        {
             margin-left:3px;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            //页面高度
            var wheight = document.body.clientHeight;
         
            //查询条件高度
            var tbSearch = document.getElementById("TableSearch").offsetHeight
         
            //显示列表高度
            var divElement = document.getElementById("up1");
            var tableSearch = document.getElementById("TableSearch");
            var searchMain = document.getElementById("searchMain");
            var searchHeader = document.getElementById("searchHeader");
           
        })
    </script>

</head>
<body onload="javascript:SetGridViewScrolling('up1');" style="overflow: visible; padding-bottom:0px; margin-bottom:0px;  ">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件">
                <tr>
                    <td class="TableSearchHeader"  onclick="showHideSearchCondition();">
                        查询条件 <span id="searchHeaderText">[点击隐藏]</span>
                    </td>
                    <td  colspan="8" >
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
                        所属部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchccDepartmentID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        负责人：
                    </th>
                    <td>
                         <asp:DropDownList ID="SearchccOperatorID" runat="server"  class="easyui-combobox">
                        </asp:DropDownList>
                    </td>
                   <th>
                        状态：
                    </th>
                    <td>
                         <asp:DropDownList ID="ddlccStatusID" runat="server"  class="easyui-combobox">
                             <asp:ListItem Value="1" Text="未完成"></asp:ListItem>
                            <asp:ListItem Value="2" Text="已完成"></asp:ListItem>
                            <asp:ListItem Value="3" Text="全部"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        关键字：
                    </th>
                    <td>
                       <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <th>
                        <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchccAddDateBegin','SearchccAddDateEnd');" />
                        录入日期：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchccAddDateBegin" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchccAddDateEnd" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                   
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server" >
            <ContentTemplate>
                <asp:GridView ID="GridCustomerClue" runat="server" CssClass="Grid" GridLines="Both"
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
                <asp:AsyncPostBackTrigger ControlID="btn_ConfirmChangeOperator" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="btn_FiUpload" EventName="Click" />--%>
            </Triggers>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div  class="footpager">
                <table cellpadding="0" cellspacing="0" border="0" style=" width:100%">
                <tr>
                <td style=" width:100%">
                    <div class="Pager" style=" width:100%; float:left;" >
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager> 
                </div>
                </td>
                <td>
                    <div style="width:200px; float:right; ">
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
        

        <%--转移--%>
      <div id="divChange">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                        <div style="margin: 0 auto;">
                            <span id="sWidth" style=" height:35px;">&nbsp;</span>
                        </div>
                        <table class="ctable" style="width: 300px; height: 100px;padding: 20px 10px 20px 10px;" align="center" border="0"
                            cellpadding="1" cellspacing="2">
                            <tr valign="Top" >
                                <td class="tdTitle" colspan="2" >
                                    &middot;选择转移至业务员
                                    <hr style="color: #79639D; size: 2px;"  />
                                </td>
                            </tr>
                            <tr valign="Top" >
                                <td >
                                    <asp:DropDownList ID="ddlOperators" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr valign="bottom">
                                <td style="width: 73px" colspan="2" align="center">
                                    <asp:Button ID="btn_ConfirmChangeOperator" runat="server" Text="确定" CssClass="button" OnClick="btn_ConfirmChangeOperator_Click" />
                                  <input type="button" style=" margin-top:15px;" value="关闭" class="button" id="close" name="close" onclick="CloseDiv('divChange');" />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_Change" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

      
     <%--上传文件--%>
                <div id="divFileUpload">
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
                </div>
          <div id="fade" class="black_overlay" style="display: none; z-index:99;"></div>
         </form>
</body>
</html>
