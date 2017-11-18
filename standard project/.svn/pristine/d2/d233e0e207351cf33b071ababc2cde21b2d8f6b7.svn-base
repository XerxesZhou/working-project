   

namespace SmartSoft.Web.Data
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

    public partial class MarketingActivityAdd : PageBaseEdit
    {
        private int _rowid = 0;
        public int rowID
        {
            get
            {
                if (_rowid > 0)
                {
                    return _rowid;
                }
                else
                {
                    if (Request.QueryString["OpptunityID"] != null && Request.QueryString["OpptunityID"] != string.Empty)
                    {
                        return int.Parse(Request.QueryString["OpptunityID"]);
                    }
                    if (Request.QueryString["ClueID"] != null && Request.QueryString["ClueID"] != string.Empty)
                    {
                        hfClueID.Value = Request.QueryString["ClueID"];
                        return int.Parse(Request.QueryString["ClueID"]);
                    }
                }

                return 0;
            }
            set
            {
                _rowid = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                initControls();

                if (rowID > 0)
                {
                    hfOpptunityID.Value = rowID.ToString();
                }
            }
        }

        private void initControls()
        {
            InitDataSource(ddlmaDepartmentID, "CD_Department");
            InitDataSource(ddlmaOperatorID, "CD_Operators");
            InitDataSource(ddlmaTypeID, "CD_defMarketActivityType");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                this.ddlmaOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlmaDepartmentID.SelectedValue = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + CurrentOperatorID);
            }
        }

        public string GetPhaseName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                string name = DbHelperSQL.GetSHSL("select copName from defCustomerOpptunityPhase where copID=" + id);
                return name;
            }
        }
    }
}