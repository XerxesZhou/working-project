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
using SmartSoft.Service.Implement.Common;

namespace SmartSoft.Web.Common
{
    public partial class ChangePassword : SmartSoft.Presentation.PageBase
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbt_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_newpassword1.Text.Trim() == tb_newpassword2.Text.Trim())
                {
                    string newpassword = Security.EncryptDES(tb_newpassword1.Text);
                    string oldpassword = Security.EncryptDES(tb_oldpassword.Text);

                    _org.UpdatePassword(this.Operator.opeID, newpassword, oldpassword);

                    WebFunc.AlertClose("密码修改成功！", Page);
                }
                else
                {
                    WebFunc.AjaxAlertError("两次密码输入不一致！", Page);
                }
            }
            catch (Exception ex)
            {
                WebFunc.AjaxAlertError(ex.Message, Page);
            }
        }
    }
}
