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


using UDEF.Service;
using UDEF.Domain;
using UDEF.Domain.Enumerate;
using UDEF.Component;

using SmartSoft.Service.Interface.Common;
using SmartSoft.Domain.Data;
using SmartSoft.Component;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Presentation;
using SmartSoft.Service;
using SmartSoft.Domain.Common;
using SmartSoft.Service.Implement.Common;

namespace SmartSoft.Web.Data
{
    public partial class CustomerWorkReportAdd : PageBaseEdit
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