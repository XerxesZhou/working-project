/***************************************************************************
 * 
 *       功能：     部门列表
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
    using SmartSoft.Service.Implement.Common;
    using UDEF.Service;


    public partial class DepartmentList : WebList<Department>
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
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

            this.ScriptManager1.RegisterAsyncPostBackControl(this.ToolBar1.lb_Delete);
            initToolBar();
            this.ListGrid = GridDepartment;
            if (!IsPostBack)
            {
                this.SetDataSource();
                this.ReBindGrid();
            }
            base.Page_Load(sender, e);
        }

        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_del = this.GetCheckedValues();
                foreach (string key in str_list_del)
                {
                    int id = int.Parse(key);
                    _org.DeleteDepartment(id);
                }

                //清除缓存
                _userdefinebase.ClearCache("Department");

                this.SetDataSource();
                this.ReBindGrid();
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("删除时出错！错误信息：" + ex.Message, Page);
            }
        }

        protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetDataSource();
            this.ReBindGrid();
        }

        protected void GridDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }

        protected void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.SetDataSource();
            this.ReBindGrid();
        }
        #endregion

        #region Mothed
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

        private void SetDataSource()
        {
            IList<Department> deplist = _org.GetDepartmentList();
            this.DataSource = deplist;
        }
        #endregion
    }
}
