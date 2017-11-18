<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="SmartSoft.Web.Common.MessageList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../UserControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="toolBar" %>
<%@ Register Src="../UserControl/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="progressBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ϵͳ��Ϣ��ѯ</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>
    <script type="text/javascript" src="../@Scripts/CheckBoxSelect.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script language="javascript" type="text/javascript">
        function onpress() {
            if (event.keyCode == 13) {
                __doPostBack("ToolBar1$lb_Search", "");
            }
        }
        function checkSms() {
            var content = document.getElementById("txtContent").value;
            var OperID = document.getElementById("txtUsers").value;
            if (content == "") {
                alert("��¼�����ݣ�");
                return false;
            }
            if (OperID == "") {
                alert("��ѡ������ˣ�")
                return false;
            }
            return true;
        }

        function CloseDiv(id) {
            var div = document.getElementById(id);
            div.style.display = "none";
            var divfade = document.getElementById("fade");
            divfade.style.display = "none";
        }

        function showDiv() {
            $("#divSendMessage").show();
        }

        function selectuser() {
            $("#div_selectuser").show();
        }
        function showDepartmentcount() {
            $("#ddlDepartment").show();
            $("#ListBox1").show();
            $("#ActiveOperator").hide();
            $("#ListBox2").hide();
            $("#Departmentcount").addClass("bac")
            $("#opertorcount").removeClass("bac");
        }

        function showopertorcount() {
            $("#ListBox1").hide();
            $("#ddlDepartment").hide();
            $("#ActiveOperator").show();
            $("#ListBox2").show();
            $("#Departmentcount").removeClass();
            $("#opertorcount").addClass("bac");
            $("#Departmentcount").removeClass("bac")
        }


        function quxiao() {
            $("#div_selectuser").hide();
            $("#ListBox3").html("");
        }

        function Change() {
            var id = $("#ddlDepartment").val();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/GetListbox",
                data: "{id:'" + id + "'}",
                dataType: 'json',
                success: function (result) {
                
                }

            })
        }
    </script>
    <script type="text/javascript">
        function Right() {

            //��ListBox1��ѡ��ֵ��ֵ��ListBox3
            var strlist = document.getElementById("ListBox1"); //��ȡListbox  
            var str = "";
            if (strlist.options.length > 0) {

                for (var i = 0; i < strlist.options.length; i++) {
               
                    if (strlist.options[i].selected == true) {
                        var j = strlist.options[i].text;
                        var v = strlist.options[i].value;
                        var option = document.createElement("OPTION");
                        option.value = v;
                        option.text = j;
                        str += v + ","; //��Valueֵ������
                        
                        var list = document.getElementById("ListBox3");
                        list.add(option);
                    }
                }

            }
        };

        function Left() {

            //��ListBox1��ѡ��ֵ��ֵ��ListBox3
            var strlist = document.getElementById("ListBox3"); //��ȡListbox  
            var str = "";
            if (strlist.options.length > 0) {
              
                for (var i = 0; i < strlist.options.length; i++) {
                    if (strlist.options[i].selected == true) {
                        var j = strlist.options[i].text;
                        var v = strlist.options[i].value;
                        var option = document.createElement("OPTION");
                        option.value = v;
                        option.text = j;
                        str += v + ","; //��Valueֵ������
                        strlist.remove(option);
                    }
                }

            }
        };
        
        function AllRight() {
            var strlist = document.getElementById("ListBox1"); //��ȡListbox  
            var str = "";
            if (strlist.value.length > 0) {
              
                for (var i = 0; i < strlist.value.length; i++) {
                        var j = strlist.options[i].text;
                        var v = strlist.options[i].value;
                        var option = document.createElement("OPTION");
                        option.value = v;
                        option.text = j;
                        str += v + ","; //��Valueֵ������
                    
                        var list = document.getElementById("ListBox3");
                        list.add(option);
                }

            }

        }


        function sendMessage() {
            //��ListBox1��ѡ��ֵ��ֵ��ListBox3
            var strlist = document.getElementById("ListBox1"); //��ȡListbox  
            var str = "";
            var content = document.getElementById("txtContent").value;
            if (strlist.options.length > 0) {
                for (var i = 0; i < strlist.options.length; i++) {
                    if (strlist.options[i].selected == true) {
                        var j = strlist.options[i].text;
                        var v = strlist.options[i].value;
                        alert(v);
                        var option = document.createElement("OPTION");
                        option.value = v;
                        option.text = j;
                        str += v + ","; //��Valueֵ������
                    }
                }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../AjaxMethods.asmx/SendMessage",
                    data: "{MessageContent:'" + content + "',ID:'" + str + "',CurrentOperatorID:" + $("#hfCurrentOperatorID").val() + "}",
                    dataType: 'json',
                    success: function (result) {
                        if (result == "OK") {
                            alert("���ͳɹ���");
                            quxiao();
                        }
                        else {
                            alert("����ʧ�ܣ�");

                        }
                    }
                })

            }
        
        }
    </script>
    <style type="text/css">
        
        .bac
        {
             background-color:#ccc;
            }
     
          #Departmentcount:link, #Departmentcount:visited, #Departmentcount:hover
          {
             background-color:#ccc;
          }
      
          #opertorcount:link, #opertorcount:visited, #opertorcount:hover
         {
            background-color:#ccc;
         }
        #SendMessage:link, #SendMessage:visited, #SendMessage:hover
        {
            color: Red;
        }
        
        #divSendMessage
        {
            position: absolute;
            top: 40%;
            left: 40%;
            display: none;
            text-align: center;
            background-color: #F9F9F9;
            z-index: 1002;
            background-color: #eeeeee;
            filter: alpha(opacity=90);
        }
        .btn_send
        {
            background-color: #0174CF;
            border: none;
            width: 58px;
            margin-top: 3px;
            height: 25px;
            color: #fff;
            cursor: pointer;
            border-radius: 3px;
        }
        .listbox
        {
            height: 205px;
            background-color: #fff;
            width: 140px;
        }
        .listbox2
        {
            height: 205px;
            background-color: #fff;
            width: 140px;
            display: none;
        }
        .ddlselect
        {
            background-color: #fff;
            width: 140px;
        }
        .ddlselect2
        {
            background-color: #fff;
            width: 140px;
            display: none;
        }
        .btn_oper
        {
            border: 1px solid #7A9AD4;
            border-radius: 3px;
            height: 24px;
            background-color: #7A9AD4;
            color: #fff;
            float: right;
            margin-right: 20px;
        }
    </style>
</head>
<body onload="javascript:SetGridViewScrolling('up1');" onkeypress="onpress();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <table class="TableSearch" id="TableSearch" cellpadding="0" cellspacing="0" style="margin: auto 0">
        <tbody id="searchHeader" title="���ز�ѯ����">
            <tr>
                <td class="TableSearchHeader" onclick="showHideSearchCondition();">
                    ��ѯ���� <span id="searchHeaderText">[�������]</span>
                </td>
                <td colspan="8">
                    <table class="ListHeader" id="ListHeader" cellpadding="0" cellspacing="0">
                        <tr class="ToolBar">
                            <td>
                                <a id="SendMessage" style="cursor: pointer;" onclick="javascript:showDiv();" cssclass="easyui-linkbutton"
                                    data-options="plain:true" runat="Server">
                                    <img src="../images/JianTou/��Ϣ.png" alt="" title="������Ϣ" class="new_icon" />
                                    ������Ϣ</a>
                            </td>
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
                    <input type="checkbox" id="cbID1" class="Checkbox" checked="true" onclick="ChangeTimeCommon(this, 'SearchSendTimeBegin','SearchSendTimeEnd');" />
                    ����ʱ�䣺
                </th>
                <td>
                    <asp:TextBox ID="SearchSendTimeBegin" runat="server" CssClass="inputDate"></asp:TextBox>��
                    <asp:TextBox ID="SearchSendTimeEnd" runat="server" CssClass="inputDate"></asp:TextBox>
                </td>
                <th>
                    ����״̬��
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStatus">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="�Ѵ���" Value="�Ѵ���"></asp:ListItem>
                        <asp:ListItem Text="δ����" Value="δ����" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    ���⣺
                </th>
                <td>
                    <asp:TextBox runat="server" ID="SearchTitle"></asp:TextBox>
                </td>
                <th>
                    ��Ϣ���ݣ�
                </th>
                <td>
                    <asp:TextBox runat="server" ID="SearchMessageContent"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridMessage" CssClass="Grid" runat="server" AutoGenerateColumns="False"
                PageSize="20" AllowPaging="false" ShowHeader="True" DataKeyNames="MessageID,URL,Title"
                AllowSorting="True" OnRowDataBound="GridMessage_RowDataBound">
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
            <div style="margin-top: 10px;">
                <div class="Pager" style="width: 70%; float: left;">
                    <webdiyer:AspNetPager ID="AspNetPager1" PagingButtonSpacing="10" HorizontalAlign="Center"
                        runat="server" PageSize="10" CustomInfoSectionWidth="200px" ShowCustomInfoSection="Left"
                        ShowInputBox="Never" NumericButtonCount="6" AlwaysShow="true" FirstPageText="&lt;&lt;"
                        LastPageText="&gt;&gt;" Width="100%">
                    </webdiyer:AspNetPager>
                </div>
                <div style="width: 200px; float: right;">
                    ÿҳ��ʾ��
                    <asp:DropDownList Width="50px" runat="server" ID="ddlPageSize" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                    ����¼
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tesk" id="divSendMessage">
        <div style="width: auto; height: auto; display: block; position: fixed; top: 22%;
            left: 20%; opacity: 1;">
            <div class="fancybox-skin" style="padding: 15px;">
                <div class="fancybox-outer">
                    <div class="fancybox-inner" style="width: auto; height: auto; overflow: visible;">
                        <div id="schedule_postform" style="width: 570px">
                            <div style="background-color: #CAC8C8; padding: 35px 25px 40px 25px; border-radius: 5px;">
                                <div id="iwk_postform_container">
                                    <div style="margin-bottom: 12px;">
                                        <textarea class="txtara" runat="server" style="width: 524px; height: 110px; max-width: 525px;
                                            border-radius: 3px;" id="txtContent" placeholder="����������..."></textarea>
                                    </div>
                                    <div style="margin-bottom: 12px;">
                                        <table style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 78px;">
                                                        <span style="float: left; text-align: right; width: 60px; margin-right: 8px;">��Ϣ������</span>
                                                    </td>
                                                    <td>
                                                        <div id="task_user_picker" class="iwk_multi_user_picker multi_picker">
                                                            <input type="text" style="width: 93%; height: 2em;" runat="server" id="txtUsers"
                                                                readonly="readonly" class="ipt" placeholder="+ ��Ϣ������" />
                                                            <img src="../images/JianTou/Add.png" onclick="javascript:selectuser()" style="border: 0px;
                                                                float: right; height: 28px;" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div>
                                        <span style="float: right; margin-top: 4px;"><a class="btn_close" onclick="CloseDiv('divSendMessage');">
                                            <span style="color: #fff; padding: 5px 15px 5px 15px; display: block; background-color: #0174CF;
                                                cursor: pointer; border-radius: 3px; margin-left: 30px;">ȡ��</span></a> </span>
                                        <span style="float: right; margin-right: 30px">
                                            <asp:Button ID="btn_ConfirmSendMessage" runat="server" CssClass="btn_send" Text="����"
                                                OnClientClick="return checkSms()" OnClick="btn_ConfirmSendMessage_Click" />
                                        </span>
                                        <div class="klear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="div_selectuser" style="height: 350px; position: absolute;  display:none; left: 30%; top: 20%;
        z-index: 1003; background-color: rgba(99, 100, 105, 0.6);">
        <table style=" padding:10px 15px 10px 15px">
            <tr>
                <td colspan="3" style="text-align: center; font-family: ΢���ź�; font-size: 16px; color: #fff;
                    height: 35px;">
                    ѡ����������Ϣ�Ķ���
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <p id="Departmentcount" style="cursor: pointer; border: 1px solid #fff; height: 20px;
                        padding-top: 5px;" onclick="javascript:showDepartmentcount();">
                        ������Ա</p>
                </td>
                <td colspan="2" style="text-align: center; "  >
                    <p id="opertorcount" style="cursor: pointer; border: 1px solid #fff; margin-left: 42px; height: 20px;
                        padding-top: 5px;" onclick="javascript:showopertorcount();">
                        ������Ա</p>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
              <%--  ������Ա--%>
                    <asp:DropDownList ID="ddlDepartment" AutoPostBack="true"  onchange="javascript:Change();"  CssClass="ddlselect"  OnSelectedIndexChanged="ddl_SelectedChange"
                        runat="server">
                    </asp:DropDownList>
                    <%--������Ա--%>
                    <asp:DropDownList ID="ActiveOperator" CssClass="ddlselect2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="ListBox1" CssClass="listbox" SelectionMode="Multiple" runat="server">
                    </asp:ListBox>
                    <asp:ListBox ID="ListBox2" CssClass="listbox2" SelectionMode="Multiple" runat="server">
                    </asp:ListBox>
                </td>
                 <td>
                    <table>
                        <tr>
                            <td>
                          <%--  <asp:ImageButton ID="ImageButton1"  ImageUrl="~/images/JianTou/right.png" 
                                    runat="server" onclick="ImageButton1_Click" />--%>
                            <img  alt="ѡ��" title="ѡ��"  style=" cursor:pointer" src="../images/JianTou/right.png" onclick="javascript:Right();" />
                            </td>
                      
                        </tr>
                        <tr>
                            <td>
                         
                             <img  alt="ȫѡ" title="ȫѡ" style=" cursor:pointer;" src="../images/JianTou/allright.png"  onclick="javascript:AllRight()" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <img alt="�Ƴ�" title=" �Ƴ�" style=" cursor:pointer;" src="../images/JianTou/left.png"  onclick="javascript:Left();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <img alt="ȫ���Ƴ�" title="ȫ���Ƴ�" style=" cursor:pointer;" src="../images/JianTou/allleft.png"  />
                            </td>
                        </tr>
                    </table>
                </td>

                <td>
                    <asp:ListBox ID="ListBox3" CssClass="listbox" SelectionMode="Multiple" runat="server">
                    </asp:ListBox>
                </td>

            </tr>
            <tr>
                <td colspan="3">
                    <input type="button" id="ipt_quxiao" onclick="javascript:quxiao();" class=" btn_oper" value="ȡ��" />
                    <input type="button"  id="SendMessageToOperator" value="ѡ��" class=" btn_oper"  onclick="javascript:sendMessage()" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfSelectCheck" runat="server" />
    <asp:HiddenField ID="hfCurrentOperatorID" runat="server" />
    <div id="fade" class="black_overlay">
    </div>
    </form>
</body>
</html>
