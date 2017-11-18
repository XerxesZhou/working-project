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

    public partial class CustomerReceiptPlanList : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "客户收款计划";

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
                //string message =  _datacontroller.Delete("CustomerReceiptPlan", this.hfSelectCheck.Value, CurrentOperatorID);
                //this.AspNetPager1.CurrentPageIndex = 0;
                //base.bindGrid();
                //WebFunc.Alert(message, Page);
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
            //this.ToolBar1.lb_Delete.Visible = true;
            //this.ToolBar1.lb_Delete.Attributes.Add("onclick", "javascript:return Delete();");
            //this.ToolBar1.lb_Delete.Click += lb_Delete_Click;

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;

            this.ToolBar1.lb_Close.Visible = true;
            this.ToolBar1.lb_Close.Attributes.Add("href", "javascript:window.close();");

            base.whereCondition += _datacontroller.GetWhereConditionForUser("crpOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "");

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "crpID";

            base.Grid = this.GridCustomerReceiptPlan;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();

            if (CustomerID > 0)
            {
                this.whereCondition += " AND crpCustomerID = " + CustomerID;
            }

            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                base.whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%'))", k);
            }

            if (ddlcrpStatus.SelectedValue != "")
            {
                if (ddlcrpStatus.SelectedValue == "未完成")
                {
                    this.whereCondition += " AND crpStatus in('未收款','部分收款')";
                }
                else
                {
                    this.whereCondition += " AND crpStatus = '" + ddlcrpStatus.SelectedValue + "'";
                }
            }
        }
        #endregion

    }
}


