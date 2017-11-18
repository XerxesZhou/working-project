/***************************************************************************
 * 
 *       功能：     部门信息录入/修改
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


    public partial class DepartmentEdit : WebForm<Department>
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindBranch();

                if (Request.QueryString["depID"] != null && Request.QueryString["depID"] != string.Empty)
                {
                    lt_title.Text = "编辑部门信息";
                    int depID = int.Parse(Request.QueryString["depID"]);
                    Department department = _org.GetDepartmentDetail(depID);
                    this.FormData = department;
                }
                else
                {
                    lt_title.Text = "录入部门信息";
                }
            }
        }

        private void BindBranch()
        {
            //IList<Branch> branchList = _org.GetBranchList();
            //(new WebFunc()).BindListControl<Branch>(branchList, ddl_depBranchID, "braBranchName", "braBranchID","","");
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Department department = this.FormData;
                if (Request.QueryString["depID"] != null && Request.QueryString["depID"] != string.Empty)
                {
                    _org.UpdateDepartment(department);
                }
                else
                {
                    _org.AddDepartment(department);
                }

                //清除缓存
                _userdefinebase.ClearCache("Department");

                //保存成功
                string strScripts = "<script language='javascript'>alert('保存成功！');window.opener.document.forms[0].btn_Refresh.click();window.close();</script>";
                WebFunc.ExecuteJScript(strScripts, Page);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
