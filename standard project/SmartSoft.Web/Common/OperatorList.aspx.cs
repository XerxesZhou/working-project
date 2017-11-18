/***************************************************************************
 * 
 *       功能：     操作员列表
 *       作者：     彭伟
 *       日期：     2007/01/28
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Common
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;

    using UDEF.Service;
    using SmartSoft.Service.Implement.Common;

    public partial class OperatorList : WebList<Operators>
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            //检测页面权限
            CheckPagePermission();

            AjaxPro.Utility.RegisterTypeForAjax(typeof(SmartSoft.Presentation.BaseController));
            this.ScriptManager1.RegisterAsyncPostBackControl(this.ToolBar1.lb_Delete);
            initToolBar();
            if (!IsPostBack)
            {
                initControls();
            }
            this.ListGrid = GridOperator;
            btn_Search_Click(sender, e);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.SetDataSource();
        }

        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_del = this.GetCheckedValues();
                foreach (string key in str_list_del)
                {
                    int id = int.Parse(key);
                    
                    string ddCorpID = ConfigurationManager.AppSettings["CorpId"] + "";
                    string ddSecrect = ConfigurationManager.AppSettings["AppSecret"] + "";
                    if (ddCorpID != "")
                    {
                        Operators opOld = _org.GetOperatorsDetail(id);
                        if (opOld != null && !string.IsNullOrEmpty(opOld.opeDingDingUserID))
                        {
                            AccessTokenResponse atr = DDHelper.GetAccessToken(ddCorpID, ddSecrect);
                            DDHelper.DeleteUser(atr.access_token, opOld.opeDingDingUserID);
                        }
                    }

                    _org.DeleteOperators(id);
                }
                this.SetDataSource();
                //清除缓存
                _userdefinebase.ClearCache("Operators");

            }
            catch (Exception ex)
            {
                WebFunc.AlertError("删除时出错！错误信息：此操作员在其他模块已启用，无法删除！", Page);
            }
        }

        protected void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.SetDataSource();
        }


        protected void GridOperator_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.getSorting(e.SortExpression);
            SetDataSource();
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 查询排序
        /// </summary>
        /// <param name="SortExpression"></param>
        protected void getSorting(string SortExpression)
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState.Add("SortExpression", "");
            }

            if (ViewState["SortDirection"] == null)
            {
                ViewState.Add("SortDirection", "");
            }

            if (ViewState["SortExpression"].ToString().Equals(SortExpression))
            {
                if (ViewState["SortDirection"].ToString().Equals("DESC"))
                    ViewState["SortDirection"] = "ASC";
                else
                    ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortExpression"] = SortExpression;
                ViewState["SortDirection"] = "ASC";
            }
        }

        /// <summary>
        /// 得到排序
        /// </summary>
        /// <returns></returns>
        protected string getSortExpression()
        {
            if (ViewState["SortExpression"] == null ||
                ViewState["SortExpression"].ToString().Equals(""))
            {
                return "";
            }
            else
            {
                return ViewState["SortExpression"] + " " + ViewState["SortDirection"];
            }
        }

        private void initToolBar()
        {
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Edit();");

            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "return Delete();");
        }

        private void initControls()
        {
            IList<Department> deplist = _org.GetDepartmentList();
            (new WebFunc()).BindListControl<Department>(deplist, SearchopeDepartmentID, "depName", "depID", "选择部门", "");
        }
        private void SetDataSource()
        {
            string sql = "select * from Operators where 1=1 ";
            if (txtKeyWord.Text != "")
            {
                sql += string.Format(" AND (opeName like '%{0}%' or opeMobile like '%{0}%' or opeEmail like '%{0}%')", this.txtKeyWord.Text);
            }
            if (this.SearchopeDepartmentID.SelectedValue != "")
            {
                sql += " AND opeDepartmentID = " + SearchopeDepartmentID.SelectedValue;
            }
            if (this.SearchopeDataRange.SelectedValue != "")
            {
                sql += " AND opeDataRange = " + this.SearchopeDataRange.SelectedValue;
            }

            if (!this.Operator.opeIsDeveloper)
            {
                sql += " AND ISNULL(opeIsDeveloper,0) = 0";
            }

            string orderby = this.getSortExpression();

            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " opeName asc";
            }
            sql += " order by " + orderby;
            DataTable dtSource = DbHelperSQL.GetDataTable(sql);
            this.GridOperator.DataSource = dtSource;
            this.GridOperator.DataBind();
        }

        public string GetDepartment(string depid)
        {
            Department dep = _org.GetDepartmentDetail(int.Parse(depid));
            if (dep != null)
            {
                return dep.depName;
            }
            return "";
        }

        public string GetDataRangeName(string dataRange)
        {
            if (dataRange == "3")
            {
                return "全公司";
            }
            else if (dataRange == "2")
            {
                return "本部门";
            }
            else
            {
                return "仅本人";
            }
        }
        #endregion
    }
}
