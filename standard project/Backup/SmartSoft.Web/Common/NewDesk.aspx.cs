using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartSoft.Presentation;
using SmartSoft.Component;
namespace SmartSoft.Web.Common
{
    public partial class NewDesk : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCurrentOperatorID.Value = this.Operator.opeID.ToString();
            hfCurrentOperatorName.Value = this.Operator.opeName.ToString();
            lblNDayNotFollowCustomer.Text = CommonFunction.GetNDayNotFollowCustomerCount(this.Operator.opeID.ToString(), "7");
            lblNDayNotFollowNotSuccessCustomer.Text = CommonFunction.GetNDayNotFollowNotSuccessCustomerCount(this.Operator.opeID.ToString(), "7");
            lblNDayNotFollowSuccessCustomer.Text = CommonFunction.GetNDayNotFollowSuccessCustomerCount(this.Operator.opeID.ToString(), "30");
            lblNDayNotSuccessCustomer.Text = CommonFunction.GetNDayNotSuccessCustomerCount(this.Operator.opeID.ToString(), "30");
            lblNDaySuccessOpptunity.Text = CommonFunction.GetNDaySuccessOpptunityCount(this.Operator.opeID.ToString(), "30");
            lblNDayExpireBusiness.Text = CommonFunction.GetNDayExpireBusinessCount(this.Operator.opeID.ToString(), "30");
            lblNDayBirthdayLinkMan.Text = CommonFunction.GetNDayBirthdayLinkManCount(this.Operator.opeID.ToString(), "7");
        }
    }
}