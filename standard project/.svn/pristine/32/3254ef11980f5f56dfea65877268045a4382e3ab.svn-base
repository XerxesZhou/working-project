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

    public partial class OperatorPlanList : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "业务员签约回款计算表";

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
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.bindGrid();
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
                string message = _datacontroller.Delete<OperatorPlan>(this.hfSelectCheck.Value, this.viewID);
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

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;

            this.ToolBar1.lb_Close.Visible = true;
            this.ToolBar1.lb_Close.Attributes.Add("href", "javascript:window.close();");

            base.whereCondition +=  this.GetWhereConditionForUser("opOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "");
            base.whereCondition += " AND ISNULL(opTotal,0.0) > 0.0";
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "opID";

            base.Grid = this.GridOperatorPlan;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();
            if (this.ddlYear.SelectedValue != "")
            {
                this.whereCondition += " AND opYear = " + this.ddlYear.SelectedValue;
            }

            if (!IsPostBack)
            {
                int currentYear = DateTime.Now.Year;
                this.ddlYear.Items.Add(new ListItem("", ""));
                for (int i = currentYear - 6; i <= currentYear + 1; i++)
                {
                    this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }
        #endregion

    }
}


