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

    public partial class CustomerAdd : MobilePageBase
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
            InitDataSource(ddlcusDepartmentID, "CD_Department");
            InitDataSource(ddlcusOperatorID, "CD_Operators");
            InitDataSource(ddlcusKindID, "CD_defCustomerType");
            InitDataSource(ddlcusSourceID, "CD_defCustomerSourceType");
            InitDataSourceAddEmpty(ddlcoPhaseID, "CD_defCustomerOpptunityPhase");
            InitDataSource(ddlcusExtType2, "CD_defCustomerProperty");
            InitDataSource(ddlcusAreaID, "CD_defCustomerArea");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                this.ddlcusOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcusDepartmentID.SelectedValue = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + CurrentOperatorID);
            }

            //是否启用商机
            trOpptunity.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 9007199254740992);

            if (rowID > 0) 
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from customerclue where ccID=" + rowID);
                txtcusName.Text = dr["ccCustomerName"] + "";
                txtcusContactor.Text = dr["ccName"] + "";
                txtcusTel.Text = dr["ccTel"] + "";
                txtLinkManMobile.Text = dr["ccMobile"] + "";
                txtcusAddress.Text = dr["ccAddress"] + "";
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