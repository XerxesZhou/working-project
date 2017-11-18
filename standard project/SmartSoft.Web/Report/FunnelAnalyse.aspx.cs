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

    public partial class FunnelAnalyse : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "销售漏斗";
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


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlObject.Items.Clear();
            switch (this.ddlType.SelectedValue)
            {
                case "2":
                    DbHelperSQL.BindDropDownList("select * from Department", this.ddlObject, "depName", "depID");
                    break;
                case "3":
                    DbHelperSQL.BindDropDownList("select * from Operators", this.ddlObject, "opeName", "opeID");
                    break;
            }
            bindGrid();
        }

        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindGrid();
        }

        private string getAmount(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "0";
            }
            else
            {
                return Math.Round(decimal.Parse(str) / 10000, 2).ToString();
            }
        }

        protected override void bindGrid()
        {
            CheckPagePermission();

            DataTable dt = this.getDataSource();

            decimal minNum = 10000000000.0m;
            decimal maxNum = 0.0m;
            string data1 = "";
            string data2 = "";
            foreach (DataRow item in dt.Rows)
            {
                if (data1 == "")
                {
                    data1 = "['" + item["Name"] + "'";
                }
                else
                {
                    data1 += ",'" + item["Name"] + "'";
                }

                if (data2 == "")
                {
                    data2 = "[{value:" + getAmount(item["PredictAmount"] + "") + ",name:'" + item["Name"] + "'}";
                }
                else
                {
                    data2 += ",{value:" + getAmount(item["PredictAmount"] + "") + ",name:'" + item["Name"] + "'}";
                }

                if (item["PredictAmount"] + "" != "")
                {
                    decimal preAmount = decimal.Parse(item["PredictAmount"] + "");
                    if (preAmount > maxNum)
                    {
                        maxNum = preAmount;
                    }
                    if (preAmount < minNum)
                    {
                        minNum = preAmount;
                    }
                }
            }

            data1 += "]";
            data2 += "]";

            minNum = Math.Round(minNum / 10000,2);
            maxNum = Math.Round(maxNum / 10000,2);
            hfMinNum.Value = minNum.ToString();
            hfMaxNum.Value = maxNum.ToString();
            hfData1.Value = data1;
            hfData2.Value = data2;
            DataRow r = dt.NewRow();

            string sql = @"select '合计' AS Name,NULL AS Rate,count(distinct coID) AS Count, sum(coPredictAmount) AS PredictAmount, sum(coPredictAmount*copRate) AS Amount from defCustomerOpptunityPhase A 
	            LEFT OUTER JOIN CustomerOpptunity B ON A.copID = B.coPhaseID
                LEFt OUTER JOIN Operators C ON C.opeID = coOperatorID
	            LEFT OUTER JOIN Department D ON C.opeDepartmentID = D.depID
                where (1=1) ";
            switch (this.ddlType.SelectedValue)
            {
                case "2":
                    sql += " AND depID = " + this.ddlObject.SelectedValue;
                    break;
                case "3":
                    sql += " AND opeID = " + this.ddlObject.SelectedValue;
                    break;
            }

            DataRow row = DbHelperSQL.GetDataRow(sql);
            r["Count"] = row["Count"];
            r["PredictAmount"] = row["PredictAmount"];
            r["Amount"] = row["Amount"];
            dt.Rows.Add(r);
            this.GridOpptunityReport.DataSource = dt;
            this.GridOpptunityReport.DataBind();

            setSumRowStyle();

            WebFunc.ExecuteAjaxJScript("buildFunnel();", this.Page);
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
            base.isQueryPage = true;
            base.Grid = this.GridOpptunityReport;
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
            string sql = @"select A.copName AS Name,A.copRate AS Rate,B.* from defCustomerOpptunityPhase A
left outer join
(select copID,ISNULL(count(distinct coID),0) AS Count, ISNULL(sum(coPredictAmount),0) AS PredictAmount, ISNULL(sum(coPredictAmount*copRate),0) AS Amount from defCustomerOpptunityPhase A 
	            LEFT OUTER JOIN CustomerOpptunity B ON A.copID = B.coPhaseID
                LEFt OUTER JOIN Operators C ON C.opeID = coOperatorID
	            LEFT OUTER JOIN Department D ON C.opeDepartmentID = D.depID
                where (1=1) ";
            switch (this.ddlType.SelectedValue)
            {
                case "2":
                    sql += " AND depID = " + this.ddlObject.SelectedValue;
                    break;
                case "3":
                    sql += " AND opeID = " + this.ddlObject.SelectedValue;
                    break;
            }

            sql +=  " group by copID) B ON A.copID = B.copID order by A.copOrderBy asc";

            DataTable dt = DbHelperSQL.GetDataTable(sql);
            return dt;
        }
        #endregion

    }
}


