<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFeedbackEditForm.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerFeedbackEditForm" %>


<!DOCTYPE html>
<html>
<head>
    <title>反馈详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--微信浏览器取消缓存的方法 start-->
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--微信浏览器取消缓存的方法 end-->
    <link rel="stylesheet" href="../css/jquery.mobile-1.3.2.min.css">
    <script src="../scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery.mobile-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/CustomerEdit.js" type="text/javascript"></script>
    <script src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js" type="text/javascript"></script>
    <script src="../scripts/BaseHelper.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/webCalendar.js" type="text/javascript"></script>
    <%--钉钉--%>
    <script type="text/javascript">
        fromSource = "Feedback";
        $(function () {
            var showPanel = getUrlParameter("showPanel");
            var ctr = getUrlParameter("ctr");
            $(".cssMainPanel").hide();
            if (showPanel == null || showPanel == "") {
                $("#divFollowHistoryList").show();
            }
            else {
                ShowListCommon(showPanel, ctr);
                $("#" + ctr).addClass("li_title");
            }
        });

        function showDivHandle() {
            $("#txtcfbResult").val("");
            $("#ddlcfbStatus").val("");
            $("#divHandlePanel").show();
        }

        function CanelHandle() {
            $("#txtcfbResult").val("");
            $("#ddlcfbStatus").val("");
            $("#divHandlePanel").hide();
        }

        function HandleFeedback() {
            var cfbID = getUrlParameter("ID");
            var cfbResult = $("#txtcfbResult").val();
            var cfbStatus = $("#ddlcfbStatus").val();
            var feedbackdata = "{cfbID:'" + cfbID + "',cfbResult:'" + cfbResult + "',cfbStatus:'" + cfbStatus + "'}";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/HandleFeedback",
                data: feedbackdata,
                dataType: 'json',
                success: function (result) {
                    if (result == "OK") {
                        ShowSuccessMessage("操作成功");
                        $("#lblcfbResult").html(cfbResult);
                        var Status = $("#ddlcfbStatus option:selected").text();
                        $("#lblcfbStatusID").html(Status);
                        $("#divHandlePanel").hide();
                    }
                }
            })
        }

        $(function () {
            var h = window.innerHeight;
            $(".cssMainPanel").css("height", h);
        })
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div data-role="page" id="divMainInfoView" style="overflow: auto;">
        <div id="divTitle" data-role="header" data-theme="b" style="height: 80px; background-color: White;
            width: 100%; position: fixed; top: 0px; z-index: 3;" class="cssHeader">
            <div style=" width:50%; float:left;">
                <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                    <tr>
                        <th colspan="2">
                            <asp:Label runat="server" CssClass="TitlecusName" ID="lblccName" Text="新增客户"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            反馈类型：
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblccAddress"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            反馈人：
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblccOperatorName"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style=" width:50%; float:left;">
                <div>
                    <ul id="MenuLi">
                        <li>
                            <a data-role="none" id="HandOperator" runat="server" onclick="javascript:showDivHandle()" class="MeunButton" >处理</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
            <ul style="margin: 0px; padding: 0px;">
                <li><a onclick="javascript:ShowListCommon('divFollowHistoryList','',this)" class="li_title"
                    id="LinkMan">动态</a></li>
                <li><a onclick="javascript:ShowListCommon('divFeedbackList','',this);" id="Base">基本</a></li>
                <li runat="server" id="liOpptunity"><a onclick="javascript:ShowListCommon('divFollowPlanList','',this)"
                    id="Opptunity">提醒</a></li>
            </ul>
        </div>

        <div id="divHandlePanel" style="z-index: 1004; position: fixed; width: 100%; background-color: #eee;
            height: 100%; display: none;">
            <table style="width: 100%;">
                <tr>
                    <td colspan="2" style="color: #333; text-shadow: none;">
                        填写处理意见:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtcfbResult" CssClass="cssTextarea" Height="100px" Width="100%" Font-Size="15px" runat="server"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlcfbStatus" data-role="none" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                            <a href="javascript:HandleFeedback();" class="SavaBtn" style="color: #fff; width: 100%;
                                float: left;">确定</a>
                        </div>
                    </td>
                    <td>
                        <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                            <a class="SavaBtn" style="color: #fff; width: 100%; float: left;" onclick="javascript:CanelHandle();">
                                取消</a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_Content" data-role="content" style="padding: 0px; overflow: auto; width: 100%;
            margin-top: 122px; z-index: 1003;">
            <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
            <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
            <asp:HiddenField ID="hfWebPCDomain" runat="server" />
            <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
            <asp:HiddenField ID="hfPCWebDomain" runat="server" />
            <asp:HiddenField ID="hfcfhOperatorID" runat="server" />
            <div>
                <%--基本--%>
                <div id="divFeedbackList" class="cssMainPanel" style=" width:100%;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                客户名称：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbCustomerID">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                反馈类型：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbFeedbackTypeID">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                联系人：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbLinkman">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                联系电话：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbTelephone">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                联系电邮：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbEmail">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                相关合同：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbOrderRelated">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                反馈内容：
                            </th>
                            <td>
                                <label runat="server" id="lblContent">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                处理结果：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbResult">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                处理状态：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbStatusID">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                申请处理人：
                            </th>
                            <td>
                                <label runat="server" id="lblcfbHandleOperatorID">
                                </label>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--提醒--%>
                <div id="divFollowPlanList" class="cssMainPanel">
                    <ul data-role="listview" id="ulFollowPlan" data-icon="false" style="margin: 0px;">
                        <asp:repeater runat="server" id="repCustomerFollowPlan">
                            <ItemTemplate>
                                <li class="MainPanelli">
                                    <div>
                                        <div class="MainView">
                                            <table cellpadding="0" cellspacing="0" style=" padding:5px;">
                                                <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                    <td style="wdith:50%;">
                                                        <label style="color: #3C96DE; width:25%"><%#Eval("cfpOperator")%></label>
                                                    </td>
                                                    <td style="width: 50%;text-align: right;">
                                                        <label><%#DateTime.Parse(Eval("cfpDate")+"").ToString("MM-dd HH:mm")%></label>
                                                    </td>
                                                </tr>
                                                <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                    <td colspan="2" style=" line-height:2em">
                                                        <label><%#Eval("cfpContent")%></label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Literal ID="Literal1" runat="server" text='<%#GetEachFile(Eval("cfpFilePath")+"",Eval("cfpID")+"") %>'></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr onclick="javascript:GoToFollowPlan(<%#Eval("cfpID") %>,'Customer')">
                                                    <td colspan="2" style=" text-align:right;">     
                                                        <label><%#DateTime.Parse(Eval("cfpAddDate")+"").ToString("yyyy-MM-dd HH:mm:ss") %></label>          
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>                     
                                </li>
                            </ItemTemplate>
                        </asp:repeater>
                    </ul>
                    <img src="../@images/添加.png" onclick="javascript:AddFollowPlan();" style="height: 45px;
                        position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                </div>
                <div id="divFollowPlanEdit" class="cssMainPanel">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" style=" text-align:left;">
                                <asp:hiddenfield runat="server" id="hfcfpID" value="0" />
                                <asp:textbox height="80" cssClass="cssTextarea"  runat="server" placeholder="提醒内容" textmode="MultiLine"  id="txtcfpContent"  ></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" padding-left:10px;">
                                <script type="text/javascript">
                                    function ClickPlanFile() {
                                        $("#followplanfile").click();
                                    }
                                </script>
                                <div onclick="javascript:ClickPlanFile()">
                                    <img src="../@images/UploadPic.png" style=" width:50px;" />
                                </div>
                                <input type="file" data-role="none"  id="followplanfile" onchange="javascript:uploadPlanFile('followplanfile')" style="opacity:0; display:none;" multiple/>
                                <asp:hiddenfield runat="server" id="hffollowplanfile" />
                                <div style="width:100%; float:left">
                                    <div id="Div1"></div>
                                </div>
                            </td>   
                        </tr>
                        <tr>
                            <th>提醒时间：</th>
                            <td>
                                <input type="text" id="txtcfpDate" class="inputDate2" placeholder="请选择日期时间" />
                            </td>
                        </tr>
                    </table>
                    <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                        <a href="javascript:SaveFollowPlan();" class="SavaBtn" style="color: #fff; width: 100%;
                            float: left;">确定</a>
                    </div>
                </div>
                <div id="divFollowPlanDeatil" class="cssMainPanel">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center; border-bottom: 1px solid #ddd;">
                                <label runat="server" id="lblcfpDate" style="height: 60px; line-height: 60px; text-align: center;
                                    border: 1px solid #3C96DE; padding: 10px 30px 10px 30px; border-radius: 20px;
                                    background-color: #3C96DE; color: #fff; text-shadow: none;">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
                                <label style="height: 50px; line-height: 50px;" runat="server" id="lblcfpContent">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                <label runat="server" ID="lblcfpFilePath"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="height: 50px; line-height: 50px;" runat="server" id="lblcfpFromName">
                                </label>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--动态--%>
                <div id="divFollowHistoryList" class="cssMainPanel">
                    <div>
                        <ul data-role="listview" id="ulFollowHistory" data-icon="false" style="margin: 0px;">
                        <asp:repeater runat="server" id="repFollowHistory">
                            <ItemTemplate>
                                <li id='liFollowHistory<%#Eval("cfhID") %>' class="MainPanelli">
                                    <div class="MainView">
                                        <div>
                                            <table cellpadding="0" cellspacing="0"  style=" padding:5px;">
                                                <tr  onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                    <td style="wdith:50%;">
                                                        <label style="color: #3C96DE; width:25%"><%#Eval("cfhOperator")%></label>
                                                               
                                                        <label style="color: #3C96DE; width:25%; border: 1px solid #3C96DE;margin-left: 10px;border-radius: 3px;padding: 3px 3px 3px 3px;font-size: 10px;"><%#Eval("cfhType") %></label>
                                                    </td>
                                                    <td style="width: 50%;text-align: right;">
                                                        <label><%#DateTime.Parse(Eval("cfhAddDate")+"").ToString("MM-dd HH:mm")%></label>
                                                    </td>
                                                </tr>
                                                <tr  onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                    <td colspan="2" style=" line-height:2em">
                                                        <label><%#Eval("cfhContent") %></label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Literal runat="server" id="GetFile" text='<%#GetEachFile(Eval("cfhFilePath")+"",Eval("cfhID")+"") %>'></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr onclick="javascript:GotoFollowHistory(<%#Eval("cfhID")%>,'Customer');">
                                                    <td colspan="2">
                                                                
                                                        <label><%#Eval("cfhAddress") %></label>
                                                                    
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%--<div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <%#GetLikeAndCommentCountHtml(Eval("cfhID") + "", Eval("cfhOperatorID") + "")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>--%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:repeater>
                    </ul>
                    </div>
                    <img src="../@images/添加.png" onclick="javascript:AddFollowHistory();" style="height: 45px;
                        position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                </div>
                <div id="divFollowHistoryEdit" class="cssMainPanel">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" style=" text-align:left;">
                                <asp:hiddenfield runat="server" id="hfcfhID" value="0" />
                                <asp:textbox CssClass="cssTextarea" height="80"  runat="server" placeholder="勤跟进，勤记录！请输入新的跟进记录..." textmode="MultiLine"  id="txtcfhContent"  ></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" padding-left:10px;">
                                <script type="text/javascript">
                                    function ClickFile() {
                                        $("#followFile").click();
                                    }
                                </script>
                                <div onclick="javascript:ClickFile()">
                                    <img src="../@images/UploadPic.png" style=" width:50px;" />
                                </div>
                                <input type="file" data-role="none"  id="followFile" onchange="javascript:uploadFile('followFile')" style="opacity:0; display:none;" multiple/>
                                <asp:hiddenfield runat="server" id="hffollowFile" />
                                <div style="width:100%; float:left">
                                    <div id="picBox"></div>
                                </div>
                            </td>   
                        </tr>
                        <tr>
                            <th>
                                跟进方式：
                            </th>
                            <td>
                                <asp:dropdownlist runat="server" data-role="none" id="ddlcfhTypeID">
                                </asp:dropdownlist>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                状态：
                            </th>
                            <td>
                                <asp:dropdownlist runat="server" data-role="none" id="ddlcfhStatusID">
                                </asp:dropdownlist>
                            </td>
                        </tr>
                        <tr>
                            <th>跟进时间：</th>
                            <td><input type="text" class="inputDate2" id="cfhDate" placeholder="请选择日期时间"  /></td>
                        </tr>
                        <tr>
                            <th>
                                下次跟进：
                            </th>
                            <td>
                                <input type="text" class="inputDate2" id="txtcfhNextFollowTime" placeholder="请选择日期时间" />
                            </td>
                        </tr>
                        <tr style="display:none">
                            <th>
                                跟进位置：
                            </th>
                            <td>

                                <asp:label runat="server" id="cfhAddress"></asp:label>
                                <asp:hiddenfield runat="server" id="lblcfhAddress"></asp:hiddenfield>
                            </td>
                        </tr>
                    </table>
                    <div class="OperatorButton" style="text-align: center; text-shadow: none;">
                        <a href="javascript:SaveCustomerFollowHistory();" data-role="none" class="SavaBtn">保存</a>
                    </div>
                </div>
                <div id="divFollowHistoryDetail" class="cssMainPanel">
                    <table style=" width:100%; margin:0 auto; padding:10px;" id="TableComment">
                        <tr>
                            <td>
                                <label class="lblName" id="cfhOperatorName"></label>
                                <label class="lblType" id="cfhTypeName"></label>
                            </td>
                            <td style=" float:right;">
                                <a data-role="none" onclick="javascript:EditFollowHistory(this)"
                                    href="#" class="MeunButton">修改</a><a data-role="none" onclick="javascript:DeleteFollowHistory(this)"
                                            href="#" class="MeunButton">删除</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label id="cfhAddDate"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label id="cfhContent"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label id="cfhFilePath"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label id="Label1"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"  style=" background-color:#F5F9FF;">
                                    <label id="cfhLikeAndComment"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="divLikePanel">
                                <label id="litLikePersons"></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style=" border:1px solid #ccc;">
                                    <div style="text-align: center;padding-top: 10px;">
                                        <input data-role="none" type="text" id="cfhComment" onclick="javascript:ShowBtn()" style="width: 98%;outline: none;height: 24px;" />
                                    </div>
                                    <div style="height: 40px;line-height: 40px;text-align: right; display:none;" id="DivForBtn">
                                                    
                                            <a data-role="none" class="MeunButton" onclick="javascript:SavaBillComment();">评论</a>
                                                   
                                            <a data-role="none" class="CancelBtn" onclick="javascript:HideDiv();">取消</a>
                                                    
                                    </div>
                                                
                                    <div style="border: 1px dotted #ccc;width: 98%;margin: 0 auto;"></div>
                                    <div>
                                        <ul id="CommentDetailShow" style=" list-style:none;">
                                                        
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
