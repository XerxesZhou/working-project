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
using UDEF.Domain;

using UDEF.Domain.Enumerate;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class ListSystemSelectType : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private SystemSelectTypeService _systemselecttype;
        public SystemSelectTypeService systemselecttype
        {
            set { _systemselecttype = value; }
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
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            SystemSelectType objSystemSelectType = new SystemSelectType();
            string strDynamicSQL = DynamicSQLHelper.GetSearchCondition(objSystemSelectType, this);
            string orderByExpression = this.GetSortExpression();
            orderByExpression = orderByExpression == "" ? " SelectTypeName ASC" : orderByExpression;
            this.GridView1.DataSource = _systemselecttype.SelectDynamic(strDynamicSQL, orderByExpression);
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

        protected string GetOperation(string selectTypeID)
        {
            string sHtml = string.Empty;
            sHtml += "<a onclick=\"javascript:EditSystemSelectType('" + selectTypeID + "')\" href=\"#Update\" class=\"View\">修改</a>";
            sHtml += " | <a onclick=\"javascript:DeleteSystemSelectType('" + selectTypeID + "')\" href=\"#Delete\" class=\"View\">删除 </a>";
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

