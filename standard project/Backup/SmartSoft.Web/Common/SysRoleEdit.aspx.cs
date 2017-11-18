/***************************************************************************
 * 
 *       功能：     系统角色录入/修改
 *       作者：     彭伟
 *       日期：     2007/01/27
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


    public partial class SysRoleEdit : WebForm<sysRole>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["RoleID"] != null && Request.QueryString["RoleID"] != string.Empty)
                {
                    lt_title.Text = "编辑系统角色信息";
                    int roleId = int.Parse(Request.QueryString["RoleID"]);
                    sysRole role = _authorization.GetSysRoleDetail(roleId);
                    this.FormData = role;
                }
                else
                {
                    lt_title.Text = "录入系统角色信息";
                }
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                sysRole role = this.FormData;
                if (Request.QueryString["RoleID"] != null && Request.QueryString["RoleID"] != string.Empty)
                {
                    _authorization.UpdateSysRole(role);
                }
                else
                {
                    _authorization.AddSysRole(role);
                }

                string script = "<script language='javascript'>alert('保存成功！');window.opener.document.forms[0].btn_Refresh.click();window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "roleSave", script);
            }
            catch (Exception ex)
            {
                WebFunc.Alert("保存时出错！" + ex.Message, Page);
            }
        }
    }
}
