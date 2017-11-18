<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ToolBar.ascx.cs" Inherits="SmartSoft.Web.UserControl.ToolBar" %>

<div>
   
<asp:LinkButton ID="lb_Close" CssClass="easyui-linkbutton" data-options="plain:true"  Visible="false" runat="Server"><img src="../images/img_newclose.png"  class="new_icon"  alt="" />关闭</asp:LinkButton>
<asp:LinkButton ID="lb_Search" CssClass="easyui-linkbutton" data-options="plain:true"  Visible="false" runat="Server"><img src="../images/img_search1.png" class="new_icon" alt=""/>查询</asp:LinkButton>
<asp:LinkButton ID="lb_Add" CssClass="easyui-linkbutton"  data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_11.png"  class="new_icon" alt="" />新增</asp:LinkButton>
<asp:LinkButton ID="lb_Edit" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_design.png"  alt="" class="new_icon" />修改</asp:LinkButton>
<asp:LinkButton ID="lb_Handle" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../@images/update.gif" alt="" class="new_icon"/>处理</asp:LinkButton>

<asp:LinkButton ID="lb_Resume" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_%20recover.gif" alt=""  class="new_icon"/>恢复</asp:LinkButton>
<asp:LinkButton ID="lb_Delete" CssClass="easyui-linkbutton"  data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_delete2.png" alt=""  class="new_icon" />删除</asp:LinkButton>

<%--<asp:LinkButton ID="lb_SearchAll" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_search.png"  class="new_icon" alt=""/>大页查询</asp:LinkButton>
--%>
<asp:LinkButton ID="lb_Save" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_160.png" alt="" class="new_icon" />保存</asp:LinkButton>
<asp:LinkButton ID="lb_SendEmail" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_mail.gif" alt=""  class="new_icon"/>发邮件</asp:LinkButton>

<asp:LinkButton ID="lb_SendMobileMessage" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server" ><img src="../images/Message.png" alt=""  class="new_icon"/>发短信</asp:LinkButton>

<asp:LinkButton ID="lb_AddChild" CssClass="easyui-linkbutton" data-options="plain:true"  Visible="false"  runat="Server" ><img src="../images/img_add1.gif"  alt="" class="new_icon" />添加子项</asp:LinkButton>

<asp:LinkButton ID="lb_GoBack" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server" ><img src="../images/img_back2_25.png"  alt=""  class="new_icon"/>返回</asp:LinkButton>
<asp:LinkButton ID="lb_PutIn" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="server"><img src="../images/img_save1_44.gif"  alt="" class="new_icon" />提交</asp:LinkButton>

<asp:LinkButton ID="lb_Change" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server" ><img src="../images/img_back1_23.png"  alt="" class="new_icon" />转移</asp:LinkButton>
<asp:LinkButton ID="lb_Share" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="server"><img src="../images/img_share.gif" alt=""  class="new_icon"/>分享</asp:LinkButton>
<asp:LinkButton ID="lb_SubmitApprove" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_save1_44.gif" alt=""  class="new_icon"/>提交审批</asp:LinkButton>
<asp:LinkButton ID="lb_ApprovePass" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_yes.gif" alt="" class="new_icon" />审批通过</asp:LinkButton>
<asp:LinkButton ID="lb_ApproveNoPass" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_no.gif" alt="" class="new_icon" />审批不通过</asp:LinkButton>
<asp:LinkButton ID="lb_CancelApprove" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_close.gif" alt=""  class="new_icon"/>取消审批</asp:LinkButton>


<asp:LinkButton ID="lb_DesignLayout" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="server"><img src="../images/img_design.png" alt="" class="new_icon" />设计</asp:LinkButton>
<asp:LinkButton ID="lb_DesignView" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="server"><img src="../images/img_design.png" alt="" class="new_icon" />设计</asp:LinkButton>
<asp:LinkButton ID="lb_CustomerPickup" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_get.png" alt=""  class="new_icon"/>认领</asp:LinkButton>
<asp:LinkButton ID="lb_CustomerRelease" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_ggc.png" alt=""  class="new_icon"/>放入公共池</asp:LinkButton>


<asp:DropDownList ID="ddl_ColumnView" Visible="false" runat="server"></asp:DropDownList>
<asp:DropDownList ID="ddl_Layout" Visible="false" runat="server"></asp:DropDownList>
<asp:Label ID="lbt_message" runat="server" Visible="false"></asp:Label>

<asp:LinkButton ID="lb_Import" data-options="plain:true" Visible="false" CssClass="easyui-linkbutton"  runat="server"><img src="../images/img_download.png" alt="" class="new_icon" />导入</asp:LinkButton>
<asp:LinkButton ID="lb_Export" CssClass="easyui-linkbutton" data-options="plain:true" Visible="false" runat="Server"><img src="../images/img_ transfer.png"  class="new_icon" alt=""/>导出</asp:LinkButton>

</div>


