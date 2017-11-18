<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DefineCommonEdit.aspx.cs"
    Inherits="SmartSoft.Web.SystemConfig.DefineCommonEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通用定义编辑</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />

    <style>
        .lbclass img
        {
            margin-bottom:8px;
            }
            
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
            html
            {
                overflow:auto;
                
                }
       
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="ctable" style="width:100%; height: auto; margin: 0 auto;" border="0"
                cellpadding="0" cellspacing="0">
                <tr style="height: 25px">
                    <td style="height: 25px">
                        <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                            <tr valign="Top" class="ToolBar" style="height:30px;">
                                <td>
                                    &middot;<asp:Literal ID="lt_title" Text="定义修改" runat="Server"></asp:Literal>
                                    <%--<hr style="color: #79639D; size: 2px;" noshade />--%>
                                </td>
                                <td>
                                    
                                    <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                        <img src="../images/img_newclose.png" style="border: 0px;width:15px;height:15px; margin-bottom:8px;" />关闭 </a>
                                    <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" style=" height:15px;" />保存
                                    </asp:LinkButton>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="Top">
                    <td valign="top">
                        <table id="edittable" class="ctable" style="width: 100%;  border:1px solid #ccc; height: 100%; margin: 0 auto;"
                            cellspacing="1" cellpadding="1" border="0">
                            <%--<tr align="left">
                                <td colspan="4" class="tdTitle" style="height: 30px">
                                    &middot;<asp:Literal ID="lt_title" Text="定义修改" runat="Server"></asp:Literal>
                                    <hr style="color: #79639D; size: 2px;" noshade />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 100px; text-align:right;">
                                    ID：
                                </td>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="lb_ID" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td  style="text-align:right;">  
                                名称：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="tb_Name" CssClass="input" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    排序：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="tb_OrderBy" CssClass="input" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    备注：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox TextMode="MultiLine" Rows="4" ID="tb_Remark" CssClass="input" runat="server" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
