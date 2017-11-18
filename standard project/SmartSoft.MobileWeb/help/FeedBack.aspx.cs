using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartSoft.Component;
namespace SmartSoft.MobileWeb
{
    public partial class FeedBack : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (CurrentOperatorID != "")
            {
                this.HiddenField1.Value = this.CurrentOperatorID;
            }
            else
            {

                return;
            }
        }
      
    }
}