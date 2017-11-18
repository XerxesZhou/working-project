<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketingActivityEditForm.aspx.cs"
    Inherits="SmartSoft.MobileWeb.MarketingActivityEditForm" %>

<!DOCTYPE html>
<html>
<head>
    <title>市场活动详情</title>
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

    <style type="text/css">
        #divNavigation ul li
        {
            float: left;
            width: 33.33%;
            height: 40px;
        }
    </style>

    <script type="text/javascript">
        fromSource = "MarketingActivity";
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

            var id = getUrlParameter("ID");
            if (id == 0) {
                ShowListCommon('divFollowHistoryEdit')
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <div data-role="page" id="divMainInfoView" style="overflow: auto;">
                    <div id="divTitle" data-role="header" data-theme="b" style="height: 80px; background-color: White;
                        width: 100%; position: fixed; top: 0px; z-index: 3;" class="cssHeader">
                        <asp:Label runat="server" CssClass="TitlecusName" ID="lblccName" Text="新增客户"></asp:Label>
                        <div style=" width:50%; float:left;">
                            <table id="CustomerTable" class="TitleTable" style="padding-left: 18px;">
                                <tr>
                                    <td>
                                        市场活动类型：
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblccAddress"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        负责人：
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblccOperatorName"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="divNavigation" class="cssNav" data-role="controlgroup" data-type="vertical">
                        <ul style="margin: 0px; padding: 0px;">
                            <li><a onclick="javascript:ShowListCommon('divFollowHistoryList',this)" class="li_title"
                                id="LinkMan">动态</a></li>
                            <li><a onclick="javascript:ShowListCommon('divMarketingActivityView',this);" id="Base">基本</a></li>
                            <li><a onclick="javascript:ShowListCommon('divCustomerClueView',this);" id="CustomerClue">线索</a></li>
                        </ul>
                    </div>
                    <div id="div_Content" data-role="content" style="padding: 0px; overflow: auto; width: 100%;
                        margin-top: 122px; z-index: 1003;">
                        <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
                        <asp:HiddenField ID="hfCurrentOperatorName" runat="server" />
                        <asp:HiddenField ID="hfWebPCDomain" runat="server" />
                        <asp:HiddenField runat="server" ID="hfKeyID" Value="0" />
                        <asp:HiddenField ID="hfPCWebDomain" runat="server" />
                        <asp:HiddenField ID="hfcfhOperatorID" runat="server" />
                       <asp:HiddenField ID="hfFirstLiLength" Value="3" runat="server" />
                        <div>
                            <%--市场活动基本信息--%>
                            <div id="divMarketingActivityView" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <img src="../@images/标签.png" alt="标签" class="new_img" />
                                                <a>基本</a>
                                            </td>
                                            <td style="width: 50%; text-align: right;">
                                                <a onclick="javascript:ShowListCommon('divMarketingActivityEdit',this);">编辑</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="MainView">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th>
                                                活动名称：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblmaName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                开始时间：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblmaStartDate">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                结束时间：
                                            </th>
                                            <td>
                                                    <label runat="server" id="lblmaEndDate">
                                                    </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                市场活动类型：
                                            </th>
                                            <td>
                                                    <label runat="server" id="lblmaTypeName">
                                                    </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                市场活动状态：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblmaStatusName">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                描述：
                                            </th>
                                            <td>
                                                <label runat="server" id="lblmaDescription">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <th>
                                            预计成本
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaPredictCost">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计收入
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaPredictAmount">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际成本
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaActualCost">
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计人数
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaPredictNum">
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际人数
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaActualNum">
                                            </label>
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            备注
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaRemark">
                                            </label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaDepartmentID">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人
                                        </th>
                                        <td>
                                            <label runat="server" id="lblmaOperator">
                                            </label>
                                        </td>
                                    </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="divMarketingActivityEdit" class="cssMainPanel">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            活动名称
                                        </th>
                                        <td>
                                            <asp:HiddenField runat="server" ID="hfmaID" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入名称（必填）" ID="txtmaName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            开始时间
                                        </th>
                                        <td>
                                            <input type="text" class="inputDate" id="txtmaStartDate" placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            结束时间
                                        </th>
                                        <td>
                                            <input type="text" class="inputDate" id="txtmaEndDate" placeholder="请选择日期" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            市场活动类型
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaTypeID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            描述
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入描述" TextMode="MultiLine" ID="txtmaDescription"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计成本
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计成本" ID="txtmaPredictCost"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计收入
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计收入" ID="txtmaPredictAmount"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际成本
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入实际成本" ID="txtmaActualCost"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            预计人数
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入预计人数" ID="txtmaPredictNum"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            实际人数
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入实际人数" ID="txtmaActualNum"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <th>
                                            备注
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" TextMode="MultiLine"  placeholder="请输入备注" ID="txtmaRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlmaOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;">
                                    <a href="javascript:SaveMarketingActivity();" class="SavaBtn">保存</a>
                                </div>
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
                                            <a data-role="none" onclick="javascript:DeleteFollowHistory(this)"
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
                            <%--线索基本信息--%>
                            <div id="divCustomerClueView" class="cssMainPanel">
                                <ul  data-role="listview" id="ulClue" data-icon="false" style=" margin:0px;">
                                    <asp:Repeater ID="repCustomerClue" runat="server">
                                        <ItemTemplate>
                                            <li id='liClue<%#Eval("ccID")%>' class="MainPanelli">
                                                <div>
                                                    <div class="new_div">
                                                        <table class="new_table">
                                                            <tr>
                                                                <td style="width: 33.333%; text-align: left">
                                                                    <img src="../@images/标签.png" class="new_img" />
                                                                    <a id="lianxiren" style="text-decoration: none;" href='javascript:ClueView(<%#Eval("ccID")%>);'>
                                                                        线索</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: center">
                                                                    <a id="wsd" onclick="javascript:ClueView(<%#Eval("ccID")%>);" style="">编辑</a>
                                                                </td>
                                                                <td style="width: 33.333%; text-align: right">
                                                                    <a href="javascript:DeleteClue(<%#Eval("ccID") %>);" style="margin-right: 10px;">
                                                                        删除</a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="MainView">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <th>
                                                                    联系人：
                                                                </th>
                                                                <td>    
                                                                    <%#Eval("ccName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    公司名称：
                                                                </th>
                                                                <td>
                                                                        <%#Eval("ccCustomerName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    电话：
                                                                </th>
                                                                <td>
                                                                    <%#Eval("ccTel") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    手机：
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("ccMobile")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>状态：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("ccStatusName")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>备注：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("ccRemark")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>负责人：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("ccOperator")%></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>创建人：</span>
                                                                </th>
                                                                <td>
                                                                    <label>
                                                                        <%#Eval("ccAddoperator")%>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>所属部门：</span>
                                                                </th>
                                                                <td class="table_b">
                                                                    <%#Eval("ccDepartment")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <span>创建时间：</span>
                                                                </th>
                                                                <td>
                                                                    <%#DateTime.Parse(Eval("ccAddDate") + "").ToString("yyyy-MM-dd HH:mm:ss")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <img src="../@images/添加.png" onclick="javascript:AddClue();" style="height: 45px;
                                    position: fixed; right: 20px; z-index: 1004; bottom: 6%;" />
                            </div>
                            <div id="divCustomerClueEdit" class="cssMainPanel">
                                <div class="new_div">
                                    <table style="background-color: #f8f8f8; line-height: 40px;">
                                        <tr>
                                            <td style="width: 100%;">
                                                <label class="cssEditlbl">编辑区域</label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th>
                                            联系人：
                                        </th>
                                        <td>
                                            <asp:HiddenField ID="hfccID" runat="server" Value="0" />
                                            <asp:TextBox runat="server" placeholder="请输入联系人姓名" ID="txtccName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            公司名称：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入公司名称" ID="txtccCustomerName"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            电话：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入电话" ID="txtccTel"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            手机：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入手机" ID="txtccMobile"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            备注：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" placeholder="请输入备注" TextMode="MultiLine" ID="txtccRemark"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            地址：
                                        </th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtccAddress" TextMode="MultiLine" placeholder="请输入地址">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            负责人：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccOperatorID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            部门：
                                        </th>
                                        <td>
                                            <asp:DropDownList runat="server" data-role="none" ID="ddlccDepartmentID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="OperatorButton" style="text-align: center;">
                                    <a href="javascript:SaveMarketingActivityClue();" class="SavaBtn">保存</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
