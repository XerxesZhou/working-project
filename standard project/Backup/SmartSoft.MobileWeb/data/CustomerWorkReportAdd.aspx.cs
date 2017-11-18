using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;
using System.Configuration;

namespace SmartSoft.MobileWeb.data
{
    public partial class CustomerWorkReportAdd : MobilePageBase
    {
        public int? rowID
        {
            get
            {
                if (Request.QueryString["ID"] + "" != "")
                {
                    return int.Parse(Request.QueryString["ID"] + "");
                }
                return null;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                hfPCWebDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"];
                bindData();
            }
            
        }

        private void bindData()
        {
            InitDataSourceAddEmpty(ddlwdNotifier, "CD_Operators");

            if (CurrentOperatorID != "") 
            {
                //ddlwdNotifier.SelectedValue = CurrentOperatorID;
                hfCurrentOperatorID.Value = CurrentOperatorID;
            }
        }
    }
}