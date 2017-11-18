<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SmartSoft.Web.Common.ChangePassword" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function SaveData()
        {
            var p1 = document.getElementById('<%=tb_newpassword1.ClientID%>').value;
            var p2 = document.getElementById('<%=tb_newpassword2.ClientID%>').value;
                        
            if(p1 != p2)
            {
                alert('两次输入的新密码不一致！');
                return false;
            }
        }
    </script>

    <style type="text/css">
        #lbt_save
        {
            text-decoration:none;
        }
        #lb_close
        {
            text-decoration:none;
        }
        
        .ctable
        {
            border-style:none;
        }
        
        .ctable th
        {
            font-family: "Aria,Microsoft Sans Serif,宋体";
            height: 25px;
            font-weight: bold;
            font-size: 9pt;
            color: black;
            background-color:#EEE;
            
        }
        
        .ctable tr td
        {
            line-height:30px;
            font-family: 'Aria,Microsoft Sans Serif,宋体';
            padding-right: 3px;
            padding-left: 3px;
            padding-bottom: 3px;
            padding-top: 3px;
            background-color:white;
        }
        
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%; margin:auto 0;" border="0" cellpadding="0" cellspacing="0">
            <tr>
	            <td>
		            <table style="width:100%" cellSpacing="0" cellPadding="0" border="0">
			            <tr>
				            <td class="TopBorder">
					            
					            <a id="lb_Close" href="#" onclick="javascript:window.close();">
                                    <img src="../images/img_newclose.gif" style="border: 0px;width:17px;height:17px;" />关闭 </a>
				            </td>
			            </tr>
		            </table>
	            </td>
            </tr>
            <tr>
	            <td valign="top">
	                <table class="ctable" style=" width:400px; margin-top:0; margin-left:auto; margin-right:auto; margin-bottom:0;" cellSpacing="1" cellPadding="1" border="0">
		                <tr align="center">
			                <th colSpan="2" style="height: 25px">
				                修改密码
			                </th>
		                </tr>
		                <tr>
		                    <td style="width: 88px">旧密码：</td>
		                    <td>
		                        <asp:TextBox ID="tb_oldpassword" TextMode="password" runat="Server" BorderStyle="groove"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_oldpassword"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		                </tr>
		                <tr>
		                    <td style="width: 88px">新密码：</td>
		                    <td><asp:TextBox ID="tb_newpassword1" runat="Server" TextMode="password" BorderStyle="groove"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_oldpassword"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		                </tr>
		                <tr>
		                    <td style="width: 88px">确认新密码：</td>
		                    <td><asp:TextBox ID="tb_newpassword2" runat="Server" TextMode="password" BorderStyle="Groove"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_newpassword2"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
		                </tr>
                        <tr>
		                    <td style="width: 88px"></td>
                             <td align="RIGHT">
                                <asp:linkbutton id="lbt_save" runat="server" OnClick="lbt_save_Click">
						            <IMG src="../images/img_160.gif" border="0" />保存
					            </asp:linkbutton>
                             </td>
		                    
		                </tr>
		            </table>
		        </td>
		    </tr>
	    </table>
    </form>
</body>
</html>
