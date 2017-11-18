<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowHistoryList.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerFollowHistoryList" %>


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
  
        .Comment
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

    <script language="javascript" type="text/javascript">
    var EditFormUrl = "CustomerFollowHistoryEditForm.aspx";
 
    function AppendCommentButton()
    {
        ConvertToLink();
    }
    
    var currentCfhId = 0;
    function ShowAddComment(cfhID)
    {
        currentCfhId = cfhID;
        var divChange = document.getElementById("divAddComment");
        var obj = document.getElementById("UpdatePanel2");    
        document.getElementById("txtComment").value = "";
        divChange.style.display = "block";
        $("#divAddComment").show();
        $(".black_overlay").show();

    }
    
    function AddComment()
    {
        var comment = document.getElementById("txtComment").value;
        if (comment == "")
        {
            alert("请填写评论!");
            return;
        }
        var rrr = SmartSoft.Presentation.BaseController.AddFollowHistoryCommentForClient(currentCfhId, comment).value;
        if (rrr == "OK")
        {
            alert("评论成功！");
            var divChange = document.getElementById("divAddComment");
            divChange.style.display = "none";
            var divFade = document.getElementById("fade");
            divFade.style.display = "none";
            __doPostBack("ToolBar1$lb_Search","")
        }
        else
        {
            alert("发生异常，请重试！");
        }
        
        return false;
    }
    
    function CloseDiv(id)
    {
        var div = document.getElementById(id);
        div.style.display = "none";

        var divfade = document.getElementById("fade");
        divfade.style.display = "none";

    }
    
    function ConvertToLink() {
        ConvertToLinkCommon("GridCustomerFollowHistory", "跟进内容");
    }
    </script>

        <script type="text/javascript">
            $(function () {

                if (navigator.appName == "Microsoft Internet Explorer") {

                }


                //IE浏览器
                if (navigator.userAgent.indexOf("MSIE") > 0) {

                

                }

                //火狐浏览器
                if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {



                }
                //谷歌浏览器
                if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {



                }
                if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
                    return "Camino";
                }


                if (isMozilla = navigator.userAgent.indexOf("Gecko") > 0) {

                }
            })  
    </script>
</head>
<body onload="javascript:SetGridViewScrolling('up1');" style="overflow: visible;">
    <form id="form1" runat="server" defaultbutton="ToolBar1$lb_Search">
    <div></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
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
                        关键字：
                    </th>
                    <td>
                        <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchcfhDateBegin','SearchcfhDateEnd');" />
                        开始时间：
                    </th>
                    <td>
                        <asp:TextBox ID="SearchcfhDateBegin" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>至
                        <asp:TextBox ID="SearchcfhDateEnd" Width="100px" runat="server" CssClass="inputDate"></asp:TextBox>
                    </td>
                    <th>
                        联系类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcfhTypeID" runat="server"></asp:DropDownList>
                    </td>
                    <th>
                        跟进人:
                    </th>
                    <td>
                        <asp:DropDownList ID="SearchcfhOperatorID" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridCustomerFollowHistory" runat="server" CssClass="Grid" Width="100%" GridLines="Both"
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
              <div class="footpager">
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
               <div style="width:200px; float:right; ">
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
         <div id="divAddComment" class="Comment">
           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
           <div style="margin: 0 auto;">
                <span id="Span3" style=" height:35px;">&nbsp;</span>
            </div>
            
           <table class="ctable" style="width: 500px; height: 200px; border-style:none;padding: 20px 10px 20px 10px;" align="center" border="0"
                cellpadding="1" cellspacing="2">
                <tr valign="Top" style="height:30px;">
                    <td class="tdTitle" colspan="2">
                        &middot;填写评论内容
                        <hr style="color: #000; size: 2px;" noshade />
                    </td>
                </tr>
                
                <tr style="height:20px;">
                    <td>
                        评论內容:
                    </td>
                    <td>
                        <asp:TextBox Columns="5" Height="100px" ID="txtComment" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="bottom">
                    <td style="width: 73px" colspan="2" align="center">
                        <asp:Button ID="btn_ConfirmAddComment" runat="server" Text="确定" CssClass="button" OnClientClick="return AddComment()"/>
                        <input type="button" style="margin-top:15px;" value="关闭" class="button" id="Button3" name="close" onclick="CloseDiv('divAddComment');" />
                    </td>
                </tr>
            </table>
           
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="fade" class="black_overlay" style="display: none; z-index:99;"></div>
    </form>
</body>
</html>

