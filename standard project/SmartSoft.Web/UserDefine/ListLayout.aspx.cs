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
using UDEF.Service;
using UDEF.Domain;
using UDEF.Domain.Enumerate;

namespace UDEF.Web.UserDefine
{
    public partial class ListLayout : BasePage
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

        private LayoutService _layout;
        public LayoutService layout
        {
            set { _layout = value; }
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
                this.GridView1.DataSource = _layout.SelectDynamic(strDynamicSQL, this.GetSortExpression());
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

        protected string GetOperation(string LayoutID, string TableID, bool IsSystemDefine)
        {
            string sHtml = string.Empty;
            sHtml += "<a href=\"EditLayout.aspx?EditMode=update&LayoutID=" + LayoutID + "&TableID=" + TableID + "\" class=\"View\">编辑</a>";
            sHtml += " | <a href=\"EditLayout.aspx?EditMode=copy&LayoutID=" + LayoutID + "&TableID=" + TableID + "\" class=\"View\">复制</a>";
            sHtml += " | <a href=\"#Delete\" onclick=\"javascript:DeleteLayout(" + LayoutID + ")\" class=\"View\">删除</a>";
            return sHtml;
        }

        protected string GetTableID()
        {
            return this.ddlSeleteTable.SelectedValue;
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
