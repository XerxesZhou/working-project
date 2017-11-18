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
using UDEF.Domain;
using UDEF.Domain.Enumerate;
using UDEF.Component;

namespace UDEF.Web.UserDefine
{
    public partial class ListColumnViewExpression : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private ColumnViewExpressionService _columnviewexpression;
        public ColumnViewExpressionService columnviewexpression
        {
            set { _columnviewexpression = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxService));
            if (!IsPostBack)
            {
                dataInit();
                dataBind();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void dataInit()
        {
            ListControlDataInit.Init(
                this.SearchUseDataType,
                DataType.GetListItemCollection(),
                "NAME",
                "ID");
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            ColumnViewExpression deOperator = new ColumnViewExpression();
            string strDynamicSQL = DynamicSQLHelper.GetSearchCondition(deOperator, this);
            this.GridView1.DataSource = _columnviewexpression.SelectDynamicAll(strDynamicSQL, this.GetSortExpression());
            this.GridView1.DataBind();
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            dataBind();
        }

        protected string GetOperation(string operatorID, bool stopTag)
        {
            string sHtml = string.Empty;
            if (stopTag)
            {
                sHtml += "<a onclick=\"javascript:EditOperator('" + operatorID + "')\" href=\"#Edit\" class=\"View\">修改</a>";
                sHtml += " | <a onclick=\"javascript:OpenOperator('" + operatorID + "')\" href=\"#Open\" class=\"View\">启用</a>";
            }
            else
            {
                sHtml += "<a onclick=\"javascript:EditOperator('" + operatorID + "')\" href=\"#Edit\" class=\"View\">修改</a>";
                sHtml += " | <a onclick=\"javascript:StopOperator('" + operatorID + "')\" href=\"#Stop\" class=\"View\">停用</a>";
            }
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
