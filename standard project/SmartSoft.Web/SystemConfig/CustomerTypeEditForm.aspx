<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerTypeEditForm.aspx.cs" Inherits="SmartSoft.Web.SystemConfig.CustomerTypeEditForm" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ͻ�����</title>
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
                                    &middot;�趨�ͻ�����
                                </td>
                                <td>
                                     <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                        <img src="../images/img_newclose.png" style="border: 0px" />�ر� </a>
                                    <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" />����
                                    </asp:LinkButton>
                                   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="Top">
                    <td valign="top">
                        <table id="edittable" class="ctable" style="width: 100%; border:1px solid #ccc;"
                            cellspacing="1" cellpadding="1" border="0">
                            
                            <tr>
                                <td style=" width:40%; text-align:right;">
                                    ���ƣ�
                                </td>
                                <td colspan="3" >
                                    <asp:TextBox ID="tb_ctName" CssClass="input" runat="server" ></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_ctName"
                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="text-align:right;">����������</td>
                                <td colspan="3">
                                    <asp:TextBox ID="tb_ctProtectDays" CssClass="input" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">�ӱ�������</td>
                                <td colspan="3">
                                    <asp:TextBox ID="tb_ctIncreaseProtectDays" CssClass="input" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">˳��</td>
                                <td colspan="3">
                                    <asp:TextBox ID="tb_ctOrderBy" CssClass="input" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">��ע��</td>
                                <td colspan="3">
                                    <asp:TextBox ID="tb_ctRemark" CssClass="input" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>