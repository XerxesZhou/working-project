/***************************************************************************
 * 
 *       功能：     客户资料
 *       作者：     朱少军
 *       日期：     2009-7-17
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/



namespace SmartSoft.Web.Data
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using UDEF.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Component;
    using Wuqi.Webdiyer;
    using SmartSoft.Presentation;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Common;
    using System.Collections.Generic;
    using SmartSoft.Service.Implement.Common;
    using System.IO;



    public partial class MarketingActivityList : PageBaseList
    {
        #region Fields
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "市场活动资料列表";
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                
                if (!IsPostBack)
                {
                    BindControls();
                }
                
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                this.Show("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.");
            }
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){ConvertToLink();})", true);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string message = _datacontroller.Delete("MarketingActivity", this.hfSelectCheck.Value, CurrentOperatorID);
                this.AspNetPager1.CurrentPageIndex = 0;
                bindGrid();
                this.Show(message);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Delete_Click", ex);
                bindGrid();
                this.Show("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.");
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.bindGrid();
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //增加
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Insert();");
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += base.lb_Search_Click;
            //删除
            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "javascript:return Delete();");
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export2_Click;

            //转移
            this.ToolBar1.lb_Change.Visible = true;
            this.ToolBar1.lb_Change.Attributes.Add("href", "javascript:ShowChangeOperator();");

            //发短信
            this.ToolBar1.lb_SendMobileMessage.Visible = true;
            this.ToolBar1.lb_SendMobileMessage.Attributes.Add("href", "javascript:ShowSendMobileMessage();");

            //导入
            //this.ToolBar1.lb_Import.Click += lb_Import_Click;
            this.ToolBar1.lb_Import.Visible = true;
            this.ToolBar1.lb_Import.Attributes.Add("href", "javascript:ShowFileUpload();");
           
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "maID";
            base.isSum = false;
            base.Grid = this.GridMarketingActivity;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;



            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                whereCondition += string.Format(" And maName like '%{0}%' OR maDescription like '%{0}%' OR maRemark like '%{0}%'", k);
            }

            base.isSort = true;
            base.checkBoxCss = "";
             
            this.initToolBar();
        }


        private void BindControls()
        {
            //绑定DropDownList
            IList<Operators> odlist = _org.GetOperatorsList();
            (new WebFunc()).BindListControl<Operators>(odlist, ddlOperators, "opeName", "opeID", "选择业务员", "");
        }
        #endregion
    }
}


