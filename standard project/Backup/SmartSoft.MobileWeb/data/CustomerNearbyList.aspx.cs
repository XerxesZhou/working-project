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
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class CustomerNearbyList : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string currentLongtitude = this.hfLongtitude.Value;
            string currentLatitude = this.hfLatitude.Value;
            string whereCondition = GetPermissionWhereCondition("cusOperatorID");

            DataTable dt = null;

            dt = DbHelperSQL.GetDataTable(string.Format("select top 100 [dbo].[LatLonRadiusDistance](cusLatitude,cusLongtitude,{0},{1}) AS cusDistance,cusID,cusName,cusAddress,cusLongtitude,cusLatitude,cusContactor,cusTel from Customer where cusLongtitude is not null and cusLatitude is not null  {2} order by [dbo].[LatLonRadiusDistance](cusLatitude,cusLongtitude,{0},{1}) asc", currentLatitude, currentLongtitude, whereCondition));
            this.repCustomer.DataSource = dt;
            this.repCustomer.DataBind();
        }
    }
}