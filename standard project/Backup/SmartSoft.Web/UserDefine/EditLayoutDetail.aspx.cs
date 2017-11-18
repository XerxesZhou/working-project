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
using UDEF.Component;


namespace UDEF.Web.UserDefine
{
    public partial class EditLayoutDetail : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
