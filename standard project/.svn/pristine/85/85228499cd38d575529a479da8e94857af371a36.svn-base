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
using UDEF.Service;
using UDEF.Domain;
using UDEF.Domain.Enumerate;

namespace UDEF.Web.UserDefine
{
    public partial class EditLayout : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxService));
        }


        public string GetTableName()
        {
            int tableID = int.Parse(this.Request.QueryString["TableID"].ToString());

            return _systemtable.Select(tableID).TableName;
        }
    }
}
