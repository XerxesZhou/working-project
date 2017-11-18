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
using SmartSoft.Domain.Data;
using SmartSoft.Component;
using Wuqi.Webdiyer;
using SmartSoft.Presentation;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Domain.Common;
using System.Collections.Generic;
using SmartSoft.Service.Implement.Common;

namespace SmartSoft.Web.Data
{
    public partial class CustomerBusinessAdd : PageBaseEdit
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
            InitDataSource(ddlcbOperatorID, "CD_Operators");
            InitDataSource(ddlcbBusinessTypeID, "CD_defCustomerBusinessType");
            InitDataSourceAddEmpty(ddlcbNotifyOperatorID, "CD_Operators");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                this.ddlcbOperatorID.SelectedValue = CurrentOperatorID;
            }

            if (rowID > 0)
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from CustomerOpptunity where coID=" + rowID);
                txtcbName.Text = dr["coName"] + "";
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