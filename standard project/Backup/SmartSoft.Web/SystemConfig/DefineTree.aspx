<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DefineTree.aspx.cs" Inherits="SmartSoft.Web.SystemConfig.DefineTree" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统定义</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />

    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../@Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setMainHeight()
	    {
	        var objiframe = document.getElementById("define");
	        objiframe.height = document.body.clientHeight;
	    }

	    $(function () {
	        var h = window.innerHeight;
	        $("#iframe").css("height", h);
	    })
    </script>

</head>
<body style="overflow:visible;">
    <form id="form1" runat="server">
        <div >
            <table width="100%">
                <tr>
                    <td style="width: 200px;" id="left">
                        <div style=" margin-bottom:15px;">
                            系统定义</div>
                        <ComponentArt:TreeView ID="TreeView1" Height="480" Width="200" DragAndDropEnabled="false"
                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                            ItemSpacing="0" NodeLabelPadding="1" 
                            EnableViewState="true" SiteMapXmlFile="../@Xml/DefineTreeData.xml"
                            runat="server">
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="UnreadItemsTemplate">
                                    <div style="padding-left: 1px;">
                                        <b>## DataItem.GetProperty('Text') ##</b> <font color="blue">(## DataItem.GetProperty('UnreadItems')
                                            ##)</font>
                                    </div>
                                </ComponentArt:ClientTemplate>
                                <ComponentArt:ClientTemplate ID="InfoItemsTemplate">
                                    <template>
                                        <b>## DataItem.GetProperty('Text') ##</b>
                                        <font color="green">[## DataItem.GetProperty('InfoItems') ##]</font>
                                    </template>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:TreeView>
                    </td>
                    <td id="right">
                        <iframe frameborder="0" width="100%" name="define" id="iframe" style="" src="DefineCommonList.aspx?TableName=defCustomerType">
                        </iframe>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
