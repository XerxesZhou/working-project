<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DefineList.aspx.cs" Inherits="SmartSoft.Web.SystemConfig.DefineList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>参数定义列表</title>
    <meta http-equiv='pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache, must-revalidate' />
    <meta http-equiv='expires' content='-1' />
    <link href="../@Css/gridStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .EditDataField
        {
             padding: 12px!important; 
             background-color: #E2E2E2; 
             border-width:0px!important;
        }
        .EditDataField
        { 
            padding: 0px; 
            padding-left: 1px; 
            font-family: verdana; 
            font-size: 10px; 
            height: 13px;
            width: 98%!important; 
        } 
    </style>

    <script language="javascript" type="text/javascript">
      function onCallbackError(excString)
      {
        if (confirm('输入数据有误！')) alert(excString); 
        Grid1.page(1); 
      }
            
      function editGrid(rowId)
      {
        Grid1.edit(Grid1.getItemFromClientId(rowId)); 
      }
      
      function editRow()
      {
        Grid1.editComplete();     
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ComponentArt:Grid ID="Grid1" EnableViewState="true" EditOnClickSelectedItem="true"
                AllowEditing="true" ShowHeader="False" CssClass="Grid" KeyboardEnabled="false"
                FooterCssClass="GridFooter" RunningMode="Callback" PagerStyle="Numbered" PagerTextCssClass="PagerText"
                PageSize="25" ImagesBaseUrl="../@images/" AutoCallBackOnUpdate="true" ClientSideOnCallbackError="onCallbackError"
                Width="100%" Height="500px" LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
                LoadingPanelPosition="MiddleCenter" runat="server" OnNeedDataSource="Grid1_NeedDataSource"
                OnNeedRebind="Grid1_NeedRebind" OnPageIndexChanged="Grid1_PageIndexChanged" OnSortCommand="Grid1_SortCommand"
                OnUpdateCommand="Grid1_UpdateCommand">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="ParameterID" ShowTableHeading="false" ShowSelectorCells="true"
                        SelectorCellCssClass="SelectorCell" SelectorCellWidth="18" SelectorImageUrl="selector.gif"
                        SelectorImageWidth="17" SelectorImageHeight="15" HeadingSelectorCellCssClass="SelectorCell"
                        HeadingCellCssClass="HeadingCell" HeadingRowCssClass="HeadingRow" HeadingTextCssClass="HeadingCellText"
                        DataCellCssClass="DataCell" RowCssClass="Row" SelectedRowCssClass="SelectedRow"
                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                        SortImageHeight="10" EditCellCssClass="EditDataCell" EditFieldCssClass="EditDataField"
                        EditCommandClientTemplateId="EditCommandTemplate">
                        <Columns>
                            <ComponentArt:GridColumn AllowEditing="false" DataField="ID" Width="120" />
                            <ComponentArt:GridColumn DataField="Name" HeadingText="名称" />
                            <ComponentArt:GridColumn DataField="TableName" HeadingText="表名" />
                            <ComponentArt:GridColumn AllowEditing="false" DataField="UseTag" HeadingText="已用"
                                ColumnType="CheckBox" />
                            <ComponentArt:GridColumn DataField="Remark" HeadingText="备注" />
                            <ComponentArt:GridColumn AllowSorting="false" HeadingText="操作" DataCellClientTemplateId="EditTemplate"
                                EditControlType="EditCommand" Width="100" Align="Center" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="EditTemplate">
                        <a href="javascript:editGrid('## DataItem.ClientId ##');">修改</a>
                    </ComponentArt:ClientTemplate>
                    <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                        <a href="javascript:editRow();">保存</a> | <a href="javascript:Grid1.EditCancel();">取消</a>
                    </ComponentArt:ClientTemplate>
                    <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td style="font-size: 11px;">
                                    正在加载中...&nbsp;
                                </td>
                                <td>
                                    <img src="../@images/spinner.gif" width="16" height="16" border="0">
                                </td>
                            </tr>
                        </table>
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
            </ComponentArt:Grid>
        </div>
    </form>
</body>
</html>
