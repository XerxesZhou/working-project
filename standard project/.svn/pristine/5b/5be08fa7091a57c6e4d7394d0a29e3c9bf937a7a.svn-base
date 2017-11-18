<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDefineDefault.aspx.cs" Inherits="UDEF.Web._UserDefineDefault" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自定义字段功能</title>
    <link href="../@Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function setMainHeight()
	{
	    var objMenu = document.getElementById("Menu");
	    var objiframe = document.getElementById("main");
	    objiframe.height = document.body.clientHeight - 45;
	}
    </script>

</head>
<body scroll="no">
    <form id="form1" runat="server">
        <div id="Menu" height="30">
            <input id="btnView" type="button" value="视图管理" class="Button" onclick="javascript:main.location='ListColumnView.aspx'" />
            <input id="btnOperator" type="button" value="视图符号管理" class="Button" onclick="javascript:main.location='ListColumnViewExpression.aspx'" />
            <input id="btnLayout" type="button" value="布局管理" class="Button" onclick="javascript:main.location='ListLayout.aspx'" />
            <input id="btnColumn" type="button" value="字段管理" class="Button" onclick="javascript:main.location='ListColumn.aspx'" />
            <input id="btnSystemSelectType" type="button" value="系统数据源" class="Button" onclick="javascript:main.location='ListSystemSelectType.aspx'" />
            <input id="btnCustomSelectType" type="button" value="自定义数据源" class="Button" onclick="javascript:main.location='ListCustomSelectType.aspx'" />
            <input id="btnTable" type="button" value="表管理" class="Button" onclick="javascript:main.location='ListTable.aspx'" />
        </div>
        <iframe id="main" src="ListTable.aspx" frameborder="0" width="100%" height="100%"
            ></iframe>
    </form>
</body>
</html>
<script type="text/javascript">
    setMainHeight();
</script>

