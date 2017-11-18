/***************************************************************************
 * 
 *       功能：     客户反馈资料
 *       作者：     朱少军
 *       日期：     2009-08-07
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

    public partial class CustomerFeedbackList : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "客户反馈资料列表";

        private int CustomerID
        {
            get
            {
                if (Request.QueryString["CustomerID"] != null && Request.QueryString["CustomerID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["CustomerID"]);
                }
                return 0;
            }
        }
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                base.Page_Load(sender, e);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){ConvertToLink();})", true);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
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
                string message = _datacontroller.Delete("CustomerFeedback", this.hfSelectCheck.Value, CurrentOperatorID); 
                this.AspNetPager1.CurrentPageIndex = 0;
                base.bindGrid();
                WebFunc.Alert(message, Page);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Delete_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
                base.bindGrid();
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
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;

            this.ToolBar1.lb_Close.Visible = true;
            this.ToolBar1.lb_Close.Attributes.Add("href", "javascript:window.close();");

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "cfbID";

            base.Grid = this.GridCustomerFeedback;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.whereCondition += _datacontroller.GetWhereConditionForFeedback(this.Operator);

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();

            if (CustomerID > 0)
            {
                this.whereCondition += " AND cfbCustomerID = " + CustomerID;
            }

            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                base.whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cfbContent like '%{0}%' OR  cfbLinkman like '%{0}%' OR cfbTelephone like '%{0}%' OR cfbEmail like '%{0}%' OR cfbResult like '%{0}%')", k);
            }
        }
        #endregion

    }
}


