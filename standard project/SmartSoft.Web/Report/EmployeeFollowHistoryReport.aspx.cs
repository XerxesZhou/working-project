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

    public partial class EmployeeFollowHistoryReport : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        public string title = "行动分析表";
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
            this.bindGrid();
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
            this.ToolBar1.lb_Search.Click += this.lb_Search_Click;
            ////查看所有
            //this.ToolBar1.lb_SearchAll.Visible = true;
            //this.ToolBar1.lb_SearchAll.Click += base.lb_SearchAll_Click;
            

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
            base.requiredColumn = "opeName";
            base.isQueryPage = true;
            base.Grid = this.GridEmployeeFollowHistoryReport;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";
            base.isSum = false;
            base.isAddIndexColumn = false;
            base.isQueryPage = true;
            this.initToolBar();
        }

        protected override DataTable getDataSource()
        {
            string strStartDate = "1900-01-01 0:0:0";
            string strEndDate = "2099-12-31 23:59:59";
            if (this.SearchcbDateBegin.Text != "")
            {
                strStartDate = this.SearchcbDateBegin.Text + " 0:0:0";
            }
            if (this.SearchcbDateEnd.Text != "")
            {
                strEndDate = this.SearchcbDateEnd.Text + " 23:59:59";
            }
            string sql = @"SELECT 
	opeName,
    depName AS DepartmentName,
	sum(case when cfhTypeID = 1 then 1 else 0 end) AS Type1Count,
	sum(case when cfhTypeID = 4 then 1 else 0 end) AS Type2Count,
	sum(case when cfhTypeID = 39 then 1 else 0 end) AS Type3Count,
	sum(case when cfhTypeID = 40 then 1 else 0 end) AS Type4Count
FROM CustomerFollowHistory A
LEFT OUTER JOIN Operators E ON A.cfhOperatorID = E.opeID
LEFT OUTER JOIN Department ON depID = opeDepartmentID
WHERE cfhDate > '" + strStartDate + @"' and cfhDate < '" + strEndDate + "'" + _datacontroller.GetWhereConditionForUser("cfhOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "") + @"
group by opeName,depName
order by depName asc, opeName asc";

            
            DataTable dt = DbHelperSQL.GetDataTable(sql);
           
            return dt;
        }
        #endregion

    }
}


