<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="SmartSoft.Web.Left" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>企业信息化管理平台</title>
    <link href="@Css/navStyle.css" type="text/css" rel="Stylesheet" />
    <link href="@Css/style.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="@Scripts/window.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ComponentArt:NavBar id="NavBar1" Width="100%" Height="530px" 
              CssClass="NavBar" 
              DefaultItemLookID="TopItemLook"
              DefaultItemTextAlign="Center"
              ExpandSinglePath="true"
              FullExpand="true"
              ImagesBaseUrl="@images/"
              ShowScrollBar="false"
              ExpandTransition="Fade"
              ExpandDuration="200"
              CollapseTransition="Fade"
              CollapseDuration="200"
              ScrollUpImageUrl="scrollup.gif"
              ScrollUpHoverImageUrl="scrollup_hover.gif"
              ScrollUpActiveImageUrl="scrollup_active.gif"
              ScrollDownImageUrl="scrolldown.gif"
              ScrollDownHoverImageUrl="scrolldown_hover.gif"
              ScrollDownActiveImageUrl="scrolldown_active.gif"
              ScrollUpImageWidth="16"
              ScrollUpImageHeight="16"
              ScrollDownImageWidth="16"
              ScrollDownImageHeight="16"
              SiteMapXmlFile="@Xml/Leftbar.xml"
              runat="server" >
            <ItemLooks>
              <ComponentArt:ItemLook LookID="TopItemLook" CssClass="TopItem" HoverCssClass="TopItemHover" ActiveCssClass="TopItemActive" />
              <ComponentArt:ItemLook LookID="Level2ItemLook" ImageUrl="folder.gif" CssClass="Level2Item" HoverCssClass="Level2ItemHover" />
            </ItemLooks>
        </ComponentArt:NavBar>
    </form>
</body>
</html>
