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

    public partial class CustomerCheckList : MobilePageBase
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
            
        }

    }
}