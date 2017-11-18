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

using UDEF.Domain.Enumerate;
using UDEF.Component;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class ListColumn : BasePage
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

        private SystemColumnService _systemcolumn;
        public SystemColumnService systemcolumn
        {
            set { _systemcolumn = value; }
        }

        private CustomColumnService _customcolumn;
        public CustomColumnService customcolumn
        {
            set { _customcolumn = value; }
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
                dataBindSys();
                dataBindCustom();
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
        /// 绑定系统字段数据
        /// </summary>
        private void dataBindSys()
        {
            if (!string.IsNullOrEmpty(this.ddlSeleteTable.SelectedValue))
            {
                string strDynamicSQL = " AND TableID = " + this.ddlSeleteTable.SelectedValue;
                this.GridView1.DataSource = _systemcolumn.SelectDynamic(strDynamicSQL, this.GetSortExpression()); ;
                this.GridView1.DataBind();
            }
        }

        /// <summary>
        /// 绑定自定义字段数据
        /// </summary>
        private void dataBindCustom()
        {
            if (!string.IsNullOrEmpty(this.ddlSeleteTable.SelectedValue))
            {
                string strDynamicSQL = " AND TableID = " + this.ddlSeleteTable.SelectedValue;
                this.GridView2.DataSource = _customcolumn.SelectDynamic(strDynamicSQL, this.GetSortExpression());
                this.GridView2.DataBind();
            } 
        }

        /// <summary>
        /// 系统字段翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            dataBindSys();
        }

        /// <summary>
        /// 自定义字段翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView2.PageIndex = e.NewPageIndex;
            dataBindCustom();
        }

        /// <summary>
        /// 系统字段排序
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
            dataBindSys();
        }

        /// <summary>
        /// 自定义字段排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
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
            dataBindCustom();
        }


        /// <summary>
        /// 下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSeleteTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataBindSys();
            this.dataBindCustom();
        }

        protected void btnRefresh1_Click(object sender, EventArgs e)
        {
            dataBindSys();
        }

        protected void btnRefresh2_Click(object sender, EventArgs e)
        {
            dataBindCustom();
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
