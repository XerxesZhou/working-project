
namespace SmartSoft.Web
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Service;
    using System.Net;
    using System.Management;
    using System.IO;
    using SmartSoft.Service.Implement.Common;
    public partial class Login : SmartSoft.Presentation.PageBase
    {
        #region IOC Component
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserName = this.Context.User.Identity.Name;
                tb_username.Text = UserName;
                Session.Clear();

                string ComanyName =ConfigurationManager.AppSettings["CompanyName"] + "";
               ltlCompanyName.Text ="登录-"+ ComanyName+"工作平台";
               lbCompanyName.Text = ComanyName;

            }
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                string empcode = tb_username.Text.Trim();
                string password = tb_password.Text.Trim();

                //加密
                password = Security.EncryptDES(password);

                Operators op = new Operators();
                bool result = _org.OperatorLogIn(empcode, password, 1000, ref op);
                if (result)
                {
                    string Ids = _org.GetOperatorHasIDs(op.opeID);

                    //将相关信息存入Session
                    op.Ids = Ids;
                    Session["Operator"] = op;
                    Session["DepartmentID"] = op.opeDepartmentID.Value;
                    Session["UserID"] = op.opeID;
                    FormsAuthentication.SetAuthCookie(op.opeName, true);
                    this.AddOperatorLog("用户登录系统.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>OpenMainWindow();</script>");
                }
                else
                {
                    this.AddOperatorLog("用户登录系统失败:" + this.tb_username);
                    this.lblMsg.Text = "登陆失败";
                }
            }
            catch (Exception exxx)
            {
                this.lblMsg.Text = "登陆失败";
            }
        }
    }
}
