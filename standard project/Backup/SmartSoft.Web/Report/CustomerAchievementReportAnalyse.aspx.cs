namespace SmartSoft.Web.Report
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

    public partial class CustomerAchievementReportAnalyse : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "业绩分析";
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.原因：" + ex.Message, this.Page);
            }
        }




        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected override void bindGrid()
        {
            base.bindGrid();
            if (ddlYear.SelectedValue != "")
            {
                Chart1.Visible = true;
                DataTable dt = this.getDataSourceForChart();
                this.Chart1.Series[0].Points.DataBindXY(dt.DefaultView, "Name", dt.DefaultView, "Total");
                switch (ddlDisplayType.SelectedValue)
                {
                    case "1":
                        Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                        break;
                    case "2":
                        Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                        break;
                    case "3":
                        Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                        break;
                    default:
                        Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                        break;
                }
            }
            else
            {
                Chart1.Visible = false;
            }
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += base.lb_Search_Click;

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            base.requiredColumn = "Name";
            base.isQueryPage = true;
            base.Grid = this.GridCustomerAchievementReportAnalyse;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();

            if (!Page.IsPostBack)
            {
                int currentYear = DateTime.Now.Year;
                this.ddlYear.Items.Add(new ListItem("",""));
                for (int i = currentYear - 6; i <= currentYear + 1; i++)
                {
                    this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }

            if (this.ddlYear.SelectedValue != "")
            {
                this.whereCondition += " AND Y = " + this.ddlYear.SelectedValue;
            }
            if (this.ddlAchievementType.SelectedValue != "")
            {
                this.whereCondition += " AND AchievementType = " + this.ddlAchievementType.SelectedValue;
            }
            if (this.ddlType.SelectedValue != "")
            {
                this.whereCondition += " AND Type = " + this.ddlType.SelectedValue;
            }
        }
        #endregion
    }
}


