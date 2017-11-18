<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAreaEditForm.aspx.cs" Inherits="SmartSoft.Web.SystemConfig.CustomerAreaEditForm" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>设定客户所属区域</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/BaseHelper.js" type="text/javascript"></script>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <style>
           #edittable tr
       {
           line-height:2.5em;
          
           }
        .input
        {
                line-height: 20px;
    border: solid 1px rgb(204,204,204);
    width:160px;
    
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <table class="ctable" style="width: 100%; height: 250px;" border="0"
                cellpadding="0" cellspacing="0">
                <tr style="height: 25px">
                    <td style="height: 25px">
                        <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                            <tr valign="Top" class="ToolBar" style="height:50px;">
                                <td>
                                    &middot;设定客户所属区域
                                </td>
                                <td>
                                    <a href="#" id="lb_Search" onclick="javascript:window.close();">
                                        <img src="../images/img_newclose.png" style="border: 0px" />关闭 </a>
                                    <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                        <img src="../images/img_160.png" border="0" />保存
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="Top">
                    <td valign="top">
                        <table id="edittable" class="ctable" style="width:100%;  border:1px solid #ccc;"
                            cellspacing="1" cellpadding="1" border="0">
                            <%--<tr align="left">
                                <td colspan="4" class="tdTitle" style="height: 30px">
                                    &middot;设定客户所属区域
                                    <hr style="color: #79639D; size: 2px;" noshade />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 100px; text-align:right;">
                                    名称：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="tb_caName" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_caName"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </tr>
                           
                            <tr>
                                <td  style="text-align:right;">编码：</td>
                               
                                <td colspan="3">
                                    <asp:TextBox ID="tb_caCode" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_caCode"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tb_caID" runat="server" Visible="False"></asp:TextBox>
                                    <span style="text-align:left;">(规则为NN-NN-NN...)</span>
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>