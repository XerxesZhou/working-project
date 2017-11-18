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

using UDEF.Service;
using UDEF.Component;

namespace UDEF.Web.UserDefine
{
    public partial class ListColumnView : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        private ColumnViewService _columnview;
        public ColumnViewService columnview
        {
            set { _columnview = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxService));
            if (!IsPostBack)
            {
                dataInit();
                if (Request.QueryString["TableID"] != null)
                {
                    this.ddlSeleteTable.SelectedValue = Request.QueryString["TableID"].ToString();
                }
                dataBind();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void dataInit()
        {
            ListControlDataInit.Init(
                this.ddlSeleteTable,
                _systemtable.SelectAll(),
                "TableText",
                "TableID",
                false);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            if (!string.IsNullOrEmpty(this.ddlSeleteTable.SelectedValue))
            {
                string strDynamicSQL = " AND TableID = " + this.ddlSeleteTable.SelectedValue;
                this.GridView1.DataSource = _columnview.SelectDynamic(strDynamicSQL, this.GetSortExpression());
                this.GridView1.DataBind();
            }
        }

        /// <summary>
        /// 翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            dataBind();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState.Add("SortExpression", "");
            }

            if (ViewState["SortDirection"] == null)
            {
                ViewState.Add("SortDirection", "");
            }

            if (ViewState["SortExpression"].ToString().Equals(e.SortExpression))
            {
                if (ViewState["SortDirection"].ToString().Equals("DESC"))
                    ViewState["SortDirection"] = "ASC";
                else
                    ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression;
                ViewState["SortDirection"] = "ASC";
            }
            dataBind();
        }

        /// <summary>
        /// 下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSeleteTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            dataBind();
        }

        protected string GetOperation(string ViewID, bool IsSystemDefine)
        {
            string sHtml = string.Empty;
            sHtml += "<a onclick=\"javascript:ExecuteView('" + ViewID + "')\" href=\"#Update\" class=\"View\">运行</a>";
            sHtml += " | <a onclick=\"javascript:EditView('" + ViewID + "')\" href=\"#Update\" class=\"View\">修改</a>";
            sHtml += " | <a onclick=\"javascript:CopyView('" + ViewID + "')\" href=\"#Update\" class=\"View\">复制</a>";
            sHtml += " | <a onclick=\"javascript:DeleteView('" + ViewID + "')\" href=\"#Delete\" class=\"View\">删除 </a>";
            return sHtml;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "ItemOver(this)";
                e.Row.Attributes["onmouseout"] = "ItemOut(this)";
            }
        }
    }
}
