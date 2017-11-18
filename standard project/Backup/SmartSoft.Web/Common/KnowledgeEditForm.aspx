<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeEditForm.aspx.cs"
    Inherits="SmartSoft.Web.Data.KnowledgeEditForm" %>

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
    <script type="text/javascript" src="../@Scripts/webCalendar.js"></script>
    <script type="text/javascript" src="../@Scripts/BaseHelper.js"></script>
    <script type="text/javascript" src="../@Scripts/jquery.js"></script>
    <script type="text/javascript" src="../@Scripts/window.js"></script>

    <script type="text/javascript" src="../WebEditor/scripts/jquery-1.3.2.js"></script>

    <script type="text/javascript" src="../WebEditor/scripts/jquery-ui-1.7.2.custom.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="../WebEditor/style/jqueryui/ui-lightness/jquery-ui-1.7.2.custom.css" />

    <script type="text/javascript" src="../WebEditor/scripts/jHtmlArea-0.8.js"></script>
    <link rel="Stylesheet" type="text/css" href="../WebEditor/style/jHtmlArea.css" />
    
    <style type="text/css">
        /* body { background: #ccc;} */
        div.jHtmlArea .ToolBar ul li a.custom_disk_button 
        {
            background: url(images/disk.png) no-repeat;
            background-position: 0 0;
        }
        
        div.jHtmlArea { 
                         border: solid 1px #ccc; 
                         }
        
        .ToolBar
        {
            width:662px !important;
            }
    </style>

    <script type="text/javascript">
        // You can do this to perform a global override of any of the "default" options
        // jHtmlArea.fn.defaultOptions.css = "jHtmlArea.Editor.css";

        $(function () {
            //$("textarea").htmlarea(); // Initialize all TextArea's as jHtmlArea's with default values

            $("#txtDefaultHtmlArea").htmlarea(); // Initialize jHtmlArea's with all default values

            $("#txtCustomHtmlArea").htmlarea({
                // Override/Specify the Toolbar buttons to show
                toolbar: [
                                        ["bold", "italic", "underline", "|", "forecolor"],
                                        ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
                                        ["link", "unlink", "|", "image"],
                                        [{
                                            // This is how to add a completely custom Toolbar Button
                                            css: "custom_disk_button",
                                            text: "Save",
                                            action: function (btn) {
                                                // 'this' = jHtmlArea object
                                                // 'btn' = jQuery object that represents the <A> "anchor" tag for the Toolbar Button
                                                alert('SAVE!\n\n' + this.toHtmlString());
                                            }
                                        }]
                                    ],

                // Override any of the toolbarText values - these are the Alt Text / Tooltips shown
                // when the user hovers the mouse over the Toolbar Buttons
                // Here are a couple translated to German, thanks to Google Translate.
                toolbarText: $.extend({}, jHtmlArea.defaultOptions.toolbarText, {
                    "bold": "fett",
                    "italic": "kursiv",
                    "underline": "unterstreichen"
                }),

                // Specify a specific CSS file to use for the Editor
                css: "style//jHtmlArea.Editor.css",

                // Do something once the editor has finished loading
                loaded: function () {
                    //// 'this' is equal to the jHtmlArea object
                    //alert("jHtmlArea has loaded!");
                    //this.showHTMLView(); // show the HTML view once the editor has finished loading
                }
            });
        });
     </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="TableMain" cellpadding="0" cellspacing="0">
        <tr class="ToolBar" style="HEIGHT:50px;">
            <td>
                &middot;<%=title %>
            </td>
            <td>
                <toolBar:ToolBar ID="ToolBar1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="MainPanel" runat="server">
                </asp:Panel>
                <table class="TablePanel" columncount="3" border="0" id="Table1">
                    <tr>
                        <th>
                            主题：
                        </th>
                        <td style="width: 120px;">
                            <asp:TextBox runat="server" ID="tb_Theme" Width="120px"></asp:TextBox>
                        </td>
                         <th>
                            操作时间：
                        </th>
                        <td style="width: 120px;">
                            <asp:TextBox runat="server" ID="tb_OperateDate" CssClass="ReadOnly" ReadOnly="true" Width="120px"></asp:TextBox>
                        </td>
                        <th>
                            类别：
                        </th>
                        <td>
                            <asp:DropDownList ID="tb_Type" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            内容：
                        </th>
                        <td colspan="5" style="padding-left: 5px; background-color: #ffffff">
                            <textarea id="txtDefaultHtmlArea" cols="50" rows="15" runat="server"></textarea>
                         </td>
                    </tr>
                    <tr>
                        <th>附件：</th>
                        <td colspan="5">
                            <asp:FileUpload runat="server" ID="Fup" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
