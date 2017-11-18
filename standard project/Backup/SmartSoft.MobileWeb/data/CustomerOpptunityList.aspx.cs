﻿using System;
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
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class CustomerOpptunityList : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (IsDebug)
            {
            }
            else
            {
                base.Page_Load(sender, e);
            }
            if (!IsPostBack)
            {
                this.hfCurrentOperatorID.Value = CurrentOperatorID;
                initControls();
                string sID = DbHelperSQL.GetSHSL("select sID from sdefCommon where sTypeName='OpptunityStatus' and sName = '活动'");
                this.ddlcoStatusID.SelectedValue = sID;
            }
        }


        protected void initControls()
        {
            InitDataSourceAddEmpty(ddlcoPhaseID, "CD_defCustomerOpptunityPhase");
            InitDataSourceAddEmpty(ddlcoStatusID, "CD_OpptunityStatus");
            InitDataSourceAddEmpty(ddlcoOpptunitySourceID, "CD_defCustomerSourceType");
        }
    }
}