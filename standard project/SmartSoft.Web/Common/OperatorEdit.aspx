<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OperatorEdit.aspx.cs" Inherits="SmartSoft.Web.Common.OperatorEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�༭����Ա��Ϣ</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/BaseHelper.js" type="text/javascript"></script>
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script src="../@Scripts/webCalendar.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetTime(obj,d1,d2)
        {
            if(obj.checked)
            {
                d1.disabled = false;
                d2.disabled = false;
            }
            else
            {
                d1.disabled = true;
                d2.disabled = true;
            }
        }
    </script>
        <style type="text/css">
    html
    {
         overflow:auto;
        }
    </style>
</head>
<style type="text/css">
    #tb_opeName
    {
        width: 106px;
      
    }
    .lbclass img
    {
        margin-bottom: 8px;
    }
    #edittable tr
    {
        line-height: 2.5em;
    }
    .input
    {
        line-height: 20px;
        border: solid 1px rgb(204,204,204);
        width: 160px;
    }
    .inp_Remark
    {
        line-height: 20px;
        border: solid 1px rgb(204,204,204);
    }
    .tdFont
    {
        width: 200px;
        text-align: right;
    }
    #tb_opeReceiptAmount
    {
        line-height: 20px;
        border: solid 1px rgb(204,204,204);
        width: 160px;
    }
    
    #tb_opeOrderAmount
    {
        line-height: 20px;
        border: solid 1px rgb(204,204,204);
        width: 160px;
    }
</style>
<body>
    <form id="form1" runat="server">
        <table class="ctable" style="width: 100%; height: auto; margin: 0 auto;" border="0"
            cellpadding="0" cellspacing="0">
            <tr style="height: 25px">
                <td style="height: 25px">
                    <table style="height: 20px; width: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="Top" class="ToolBar" style="height:30px;">
                            <td>
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                                <%--<hr style="color: #79639D; size: 2px;" noshade />--%>
                            </td>
                            <td>
                                <a href="#" id="lb_Add" onclick="javascript:window.close();">
                                    <img src="../images/img_newclose.png" style="border: 0px;width:15px;height:15px; margin-bottom:8px;" />�ر� </a>
                                <asp:LinkButton ID="lbt_Save" runat="Server" CssClass="lbclass" OnClick="lbt_Save_Click">
                                    <img src="../images/img_160.png" border="0" style=" height:15px;" />����
                                </asp:LinkButton>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="Top">
                <td valign="top">
                    <table class="ctable" style="width: 100%;" cellspacing="1"
                        cellpadding="1" border="0">
                        <%--<tr align="left">
                            <td colspan="4" class="tdTitle" style="height: 30px">
                                &middot;<asp:Literal ID="lt_title" runat="Server"></asp:Literal>
                                <hr style="color: #79639D; size: 2px;" noshade />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdFont" align="center" style="width: 106px">
                                ��¼����</td>
                            <td>
                                <asp:TextBox ID="tb_opeName" runat="Server"  CssClass="input"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_opeName"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                            <td class="tdFont" align="center" style="width: 106px">
                                ���룺</td>
                            <td>
                                <asp:TextBox ID="tb_opePassword" runat="Server" CssClass="input" TextMode="password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_opePassword"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>[���մ����޸�]</td>
                        </tr>
                        <tr>
                             <td class="tdFont" align="center">
                                �������ţ�</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddl_opeDepartmentID"></asp:DropDownList>
                            </td>
                            <td class="tdFont" align="center">
                                ����Ȩ�ޣ�</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddl_opeDataRange">
                                    <asp:ListItem Text="������" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="������" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="ȫ��˾" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                           <td class="tdFont" align="center">
                                �Ƿ����Ա��</td>
                            <td>
                                <asp:RadioButtonList ID="rbl_opeIsAdmin" runat="server" RepeatDirection="horizontal">
                                    <asp:ListItem Text="��" Value="False" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="tdFont" align="center">
                                �Ƿ�ɵ�¼��ϵͳ��</td>
                            <td>
                                <asp:RadioButtonList ID="rbl_opeIsCanLogin" runat="server" RepeatDirection="horizontal">
                                    <asp:ListItem Text="��" Value="False"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="True" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                             <td class="tdFont" align="center" style="width: 106px">
                                ǩԼ�</td>
                            <td>
                                <asp:TextBox ID="tb_opeOrderAmount" runat="Server"  CssClass="inputNumber" ></asp:TextBox>
                            </td>
                            <td class="tdFont" align="center" style="width: 106px">
                                �ؿ�</td>
                            <td>
                                <asp:TextBox ID="tb_opeReceiptAmount" runat="Server"  CssClass="inputNumber"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                             <td class="tdFont" align="center" style="width: 106px">
                                �ֻ��ţ�</td>
                            <td>
                                <asp:TextBox ID="tb_opeMobile" runat="Server" CssClass="input"></asp:TextBox>
                            </td>
                            <td class="tdFont" align="center" style="width: 106px">
                                �������䣺</td>
                            <td>
                                <asp:TextBox ID="tb_opeEmail" runat="Server" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                             <td class="tdFont" align="center" style="width: 106px">
                                ��ְ���ڣ�</td>
                            <td>
                                <asp:TextBox ID="tb_opeEnterDate" runat="Server"  CssClass="inputDate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend class="tdTitle">������ɫ</legend>
                        <table class="ctable" cellpadding="1" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="cbl_Role" runat="server" RepeatColumns="8" RepeatDirection="horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend class="tdTitle">����ҵ��</legend>
                        <table class="ctable" cellpadding="1" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="cbl_Subordinate" runat="server" RepeatColumns="8" RepeatDirection="horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="tb_opeID" runat="server" Visible="false">0</asp:TextBox>
        <asp:TextBox ID="tb_password" runat="server" Visible="false"></asp:TextBox>
    </form>
    
    
</body>
</html>
