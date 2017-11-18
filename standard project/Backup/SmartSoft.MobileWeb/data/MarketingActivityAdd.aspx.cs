using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.AdvancedAPIs;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class MarketingActivityAdd : MobilePageBase
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