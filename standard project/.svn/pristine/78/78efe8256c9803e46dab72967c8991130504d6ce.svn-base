<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerLinkManList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerLinkManList" %>


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
    

    
    <script language="javascript" type="text/javascript">
        var EditFormUrl = "CustomerEditForm.aspx?FromType=linkman&showPanel=divLinkManList&ctr=LinkMan";

    //发送短信
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

    //短信关闭
    function CloseDiv(id)
    {
        var div = document.getElementById(id);
        div.style.display = "none";
        //$(".black_overlay").hide();
        var divfade = document.getElementById("fade");
        divfade.style.display = "none";

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
    
    function ConvertToLink() {
        ConvertToLinkCommon("GridCustomerLinkMan", "联系人");
    }
    </script>
     <style>
        
           #divSendEmail
        {
            position: absolute;
            top: 27%;
            left: 33%;
            margin:-70px 0px 0px -190px;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color:#eeeeee;
            filter:alpha(opacity=90);
            }
      #divSendMobileMessage
        {
            position: absolute;
            top: 40%;
            left: 40%;
            margin:-70px 0px 0px -190px;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color:#eeeeee;
            filter:alpha(opacity=90);
            
            }

            .black_overlay {
                position: fixed;
                z-index:1000;
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
   
<body onload="javascript:SetGridViewScrolling('up1');" style="overflow: visible;">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <table runat="server" class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
            <tbody id="searchHeader" title="隐藏查询条件" >
                <tr>
                    <td class="TableSearchHeader" onclick="showHideSearchCondition();">
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
            <tbody id="searchMain">
                  <tr>
                    <th>
                        关键字
                    </th>
                    <td>
                       <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        业务员：
                    </th>
                    <td>
                         <asp:DropDownList ID="SearchcusOperatorID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        所属部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcusDepartmentID" runat="server">
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
                        生日：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlStartMonth" runat="server" Width="60px">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        </asp:DropDownList>月
                        <asp:DropDownList ID="ddlStartDay" runat="server" Width="60px">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                        </asp:DropDownList>日 至
                        <asp:DropDownList ID="ddlEndMonth" runat="server" Width="60px">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        </asp:DropDownList>月
                        <asp:DropDownList ID="ddlEndDay" runat="server" Width="60px">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                        </asp:DropDownList>日
                    </td>
                      <th>
                        信息来源：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcusSourceID" runat="server">
                        </asp:DropDownList>
                        
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerLinkMan" runat="server" CssClass="Grid" GridLines="Both"
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
        <%--发送邮件--%>
         <div id="divSendEmail" >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                        <div style="margin: 0 auto;">
                            <span id="sWidth" style=" height:35px;">&nbsp;</span>
                        </div>
                        <table class="ctable" style="width: 800px; height: 400px;" align="center" border="0"
                            cellpadding="1" cellspacing="2">
                            <tr valign="Top" style="height:30px;">
                                <td class="tdTitle" colspan="2">
                                    &middot;填写邮件内容
                                    <hr style="color: #79639D; size: 2px;" noshade />
                                </td>
                            </tr>
                            
                            <tr style="height:20px;">
                                <td>
                                    邮件主题:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    邮件内容:
                                </td>
                                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:TextBox ID="TxtContent" runat="server" Height="320px" Columns="10" Width="600px"></asp:TextBox>
                                    <%--<IFRAME src="../eWebEditor/ewebeditor.htm?id=TxtContent&style=mini" frameborder='0' scrolling='no' width='99%' height='320'></IFRAME>--%>
                                </td>
                            </tr>
                            
                            <tr valign="bottom">
                                <td style="width: 73px" colspan="2" align="center">
                                    <asp:Button ID="btn_Confirm" runat="server" Text="确定" CssClass="button" OnClientClick="return checkEmail();" OnClick="btn_Confirm_Click" />
                                    <input type="button" value="关闭" class="button" id="close" name="close" onclick="CloseDiv('divSendEmail');" />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_SendEmail" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
       <%-- 发短信区域--%>
         <div id="divSendMobileMessage" >
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                      <%--  <div style="margin: 0 auto;">
                            <span id="Span1" style=" height:35px;">&nbsp;</span>
                        </div>--%>
                        <table class="ctable" style="width: 500px; height: 200px;" align="center" border="0"
                            cellpadding="1" cellspacing="2">
                            <tr valign="Top" style="height:30px;">
                                <td class="tdTitle" colspan="2">
                                    &middot;填写短信内容
                                    <hr style="color: #79639D; size: 2px;" noshade />
                                </td>
                            </tr>
                            
                            <tr style="height:20px;">
                                <td>
                                    短信內容:
                                </td>
                                <td>
                                    <asp:TextBox Columns="5" Height="100px" ID="txtMobileMessage" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2">
                                    发送时间：<asp:TextBox ID="txtSendDate" Width="120px" runat="server" CssClass="inputDate"></asp:TextBox><span style=" color:Red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写发送时间" ControlToValidate="txtSendDate" ValidationGroup="Share" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="bottom">
                                <td style="width: 73px" colspan="2" align="center">
                                    <asp:Button ID="btn_ConfirmSendMobileMessage" runat="server" Text="确定" CssClass="button" OnClientClick="return checkSms()" OnClick="btn_ConfirmSendMobileMessage_Click" />
                                    <input type="button" value="关闭" class="button" id="Button2" name="close" onclick="CloseDiv('divSendMobileMessage');" />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolBar1$lb_SendEmail" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


            <div id="fade" class="black_overlay" style="display: none; z-index:99;">
        </div>
    </form>
</body>
</html>

