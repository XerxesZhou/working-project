<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFollowPlanEditForm.aspx.cs" Inherits="SmartSoft.Web.Data.CustomerFollowPlanEditForm" %>


<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=title %>
    </title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/AjaxMainEdit.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lbclass
        {
            background-color: cornflowerblue;
            border: 1px solid #aaa;
        }
    </style>
</head>
<body style=" background:#fff; overflow:auto; height:100%;">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style=" width:100%;">
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
                        <asp:HiddenField runat="server" ID="hfcfpID" Value="0" />
                        <label style="height: 50px; line-height: 50px; font-size:14px;" runat="server" id="lblcfpContent">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;">
                        <asp:Literal runat="server" ID="lblcfpFilePath"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="height: 50px; line-height: 50px;font-size:14px;" runat="server" id="lblcfpFromName">
                        </label>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                <div  runat="server" id="divFollowFrequency" style=" margin-left:38%" visible="false">
                    <table>
                        
                        <tr>
                            <td>
                                <asp:Button ID="lb_AlertLater" CssClass="lbclass" runat="Server" OnClick="lb_AlertLater_Click" Text="还未完成，下次提醒" Width="200px"></asp:Button>
                            </td>
                            <td>
                                下次提醒间隔：<asp:TextBox runat="server" ID="txtDay" CssClass="inputNumber" Width="20" Text="0"></asp:TextBox>天<asp:TextBox runat="server" ID="txtHour" CssClass="inputNumber" Width="20" Text="1"></asp:TextBox>小时后提醒
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Button ID="btn_FollowNow" CssClass="lbclass" runat="Server" OnClick="btn_FollowNow_Click" Text="马上跟进,不再提醒" Width="200px"></asp:Button>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_NoAlert" CssClass="lbclass" runat="Server" OnClick="btn_NoAlert_Click" Text="确认已完成,不再提醒" Width="200px"></asp:Button>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    
                    
                    </div>
                </td>
            </tr>
            </table>
        </div>
        
        <div id="ShowNameResult" style="display: none;">
            <asp:ListBox ID="lbName" runat="server" CssClass="ListBox" Width="250px" Height="160"
                CausesValidation="false"></asp:ListBox>
        </div>
    </form>
</body>
</html>



