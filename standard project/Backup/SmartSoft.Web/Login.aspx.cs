
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
               ltlCompanyName.Text ="��¼-"+ ComanyName+"����ƽ̨";
               lbCompanyName.Text = ComanyName;

            }
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                string empcode = tb_username.Text.Trim();
                string password = tb_password.Text.Trim();

                //����
                password = Security.EncryptDES(password);

                Operators op = new Operators();
                bool result = _org.OperatorLogIn(empcode, password, 1000, ref op);
                if (result)
                {
                    string Ids = _org.GetOperatorHasIDs(op.opeID);

                    //�������Ϣ����Session
                    op.Ids = Ids;
                    Session["Operator"] = op;
                    Session["DepartmentID"] = op.opeDepartmentID.Value;
                    Session["UserID"] = op.opeID;
                    FormsAuthentication.SetAuthCookie(op.opeName, true);
                    this.AddOperatorLog("�û���¼ϵͳ.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>OpenMainWindow();</script>");
                }
                else
                {
                    this.AddOperatorLog("�û���¼ϵͳʧ��:" + this.tb_username);
                    this.lblMsg.Text = "��½ʧ��";
                }
            }
            catch (Exception exxx)
            {
                this.lblMsg.Text = "��½ʧ��";
            }
        }
    }
}
