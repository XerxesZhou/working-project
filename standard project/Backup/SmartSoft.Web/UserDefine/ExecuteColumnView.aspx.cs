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
    public partial class ExecuteColumnView : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private string strOrderBy = string.Empty;
        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        private SelectDataDynamicService _selectdatadynamic;
        public SelectDataDynamicService selectdatadynamic
        {
            set { _selectdatadynamic = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int viewID = int.Parse(Request.QueryString["ViewID"].ToString());
            dataInit();
            if (!IsPostBack)
            {
                _userdefinebase.InitQueryDropdownlist(this, viewID);
                dataBind();
            }
        }


        /// <summary>
        /// 初始化
        /// </summary>
        private void dataInit()
        {
            int viewID = int.Parse(Request.QueryString["ViewID"].ToString());
            this.GridView1.Columns.Clear();
            _userdefinebase.InitGridViewStyle(this.GridView1, 1000);
            _userdefinebase.InitGridViewColumn(viewID, this.GridView1, true);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            int viewID = int.Parse(Request.QueryString["ViewID"].ToString());
            DataTable dt = _selectdatadynamic.GetViewDataSource(viewID, "", "", this.GetSortExpression());
            this.GridView1.DataSource = dt;
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
    }
}
