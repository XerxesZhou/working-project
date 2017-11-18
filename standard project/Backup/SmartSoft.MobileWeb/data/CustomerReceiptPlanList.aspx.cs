using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class CustomerReceiptPlanList : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                this.hfCurrentOperatorID.Value = CurrentOperatorID;
                initControls();
            }
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){hideSearchPanel();})", true);
        }

        protected void initControls()
        {
            InitDataSourceAddEmpty(ddlDepartment, "CD_Department");
            InitDataSourceAddEmpty(ddlOperator, "CD_Operators");
        }
    }
}