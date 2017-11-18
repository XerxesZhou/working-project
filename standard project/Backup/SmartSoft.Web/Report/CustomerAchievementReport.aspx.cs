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

    public partial class CustomerAchievementReport : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "业绩统计";
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                CheckPagePermission();
                if (!IsPostBack)
                {
                    lb_Search_Click(sender, e);
                }
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "handleFixedTitle", "$(function(){handleFixedTitle();});", true);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.原因：" + ex.Message, Page);
            }
        }

        protected override void lb_Search_Click(object sender, EventArgs e)
        {
            try
            {
                this.bindGrid();
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "lb_Search_Click", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }


        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindGrid();
        }

        protected override void bindGrid()
        {
            CheckPagePermission();

            DataTable dt = this.getDataSource();

            this.Chart1.Series[0].Points.DataBindXY(dt.DefaultView, "TongJiFenLei", dt.DefaultView, "TongJiShuLiang");
            switch (DropDownList2.SelectedValue)
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

            DataRow r = dt.NewRow();
            string beginDate = "1900-01-01";
            string endDate = "2099-12-31";
            string totalAmount = "";
            if (this.SearchDateBegin.Text != "")
            {
                beginDate = this.SearchDateBegin.Text;
            }
            if (this.SearchDateEnd.Text != "")
            {
                endDate = this.SearchDateEnd.Text;
            }
            if (this.ddlAchievementType.SelectedValue == "1")
            {
                totalAmount = DbHelperSQL.GetSHSLInt("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where cbDate >= '" + beginDate + "' AND cbDate <= '" + endDate + "'");
            }
            else
            {
                totalAmount = DbHelperSQL.GetSHSLInt("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where crDate >= '" + beginDate + "' AND crDate <= '" + endDate + "'");
            }

            r[2] = decimal.Parse(totalAmount);
            dt.Rows.Add(r);
            this.GridCustomerAchievementReport.DataSource = dt;
            this.GridCustomerAchievementReport.DataBind();

            setSumRowStyle();
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
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            base.requiredColumn = "OperatorID";
            base.isQueryPage = true;
            base.Grid = this.GridCustomerAchievementReport;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();
        }

        protected override DataTable getDataSource()
        {
            string beginDate = "1900-01-01";
            string endDate = "2099-12-31";
            if (this.SearchDateBegin.Text != "")
            {
                beginDate = this.SearchDateBegin.Text;
            }
            if (this.SearchDateEnd.Text != "")
            {
                endDate = this.SearchDateEnd.Text;
            }
            DataTable dt = null;
            if (ddlAchievementType.SelectedValue == "1")
            {
                dt = DbHelperSQL.GetDataTable("select " + DropDownList1.SelectedValue.ToString() + " as DropStr," + DropDownList1.SelectedValue.ToString() + @" as TongJiFenLei,sum(cbTotalAmount) as TongJiShuLiang from CustomerBusiness K
                INNER JOIN Customer A ON K.cbCustomerID = A.cusID
	            LEFT OUTER JOIN Operators B ON A.cusOperatorID = B.opeID
	            LEFT OUTER JOIN Department C ON A.cusDepartmentID = C.depID
	            LEFT OUTER JOIN (select A1.caName as caName,A2.caID AS caID from(select caID,caCode,caName from defCustomerArea where caCode like '__') A1 INNER JOIN defCustomerArea A2 ON A2.caCode like A1.caCode + '%') G ON A.cusAreaID = G.caID
                LEFT OUTER JOIN (select * from defCustomerArea where caCode like '____') D ON A.cusAreaID = D.caID
	            LEFT OUTER JOIN defCommon E ON A.cusKindID = E.ID
	            LEFT OUTER JOIN defCommon F ON cbBusinessType = F.ID " + @"
                where cbDate >= '" + beginDate + "' and cbDate <= '" + endDate + @"'
                group by " + DropDownList1.SelectedValue.ToString() + @"
                order by sum(cbTotalAmount) DESC");
            }
            else
            {
                dt = DbHelperSQL.GetDataTable("select " + DropDownList1.SelectedValue.ToString() + " as DropStr," + DropDownList1.SelectedValue.ToString() + @" as TongJiFenLei,sum(crAmount) as TongJiShuLiang from CustomerReceipt K
                INNER JOIN Customer A ON K.crCustomerID = A.cusID
                LEFT OUTER JOIN CustomerBusiness KK ON cbID = crBusinessID
	            LEFT OUTER JOIN Operators B ON A.cusOperatorID = B.opeID
	            LEFT OUTER JOIN Department C ON A.cusDepartmentID = C.depID
	            LEFT OUTER JOIN (select A1.caName as caName,A2.caID AS caID from(select caID,caCode,caName from defCustomerArea where caCode like '__') A1 INNER JOIN defCustomerArea A2 ON A2.caCode like A1.caCode + '%') G ON A.cusAreaID = G.caID
                LEFT OUTER JOIN (select * from defCustomerArea where caCode like '____') D ON A.cusAreaID = D.caID
	            LEFT OUTER JOIN defCommon E ON A.cusKindID = E.ID
	            LEFT OUTER JOIN defCommon F ON cbBusinessType = F.ID " + @"
                where crDate >= '" + beginDate + "' and crDate <= '" + endDate + @"'
                group by " + DropDownList1.SelectedValue.ToString() + @"
                order by sum(crAmount) DESC");
            }
            return dt;
        }
        #endregion

    }
}


