using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SmartSoft.Component;


namespace SmartSoft.MobileWeb
{
    public partial class DashBoard : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (CurrentOperatorID == "" || CurrentOperatorID == "-1")
            {
                GetParmaters();
            }
            else
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                hfCurrentOperatorName.Value = CurrentOperatorName;
            }
        }
    }
}