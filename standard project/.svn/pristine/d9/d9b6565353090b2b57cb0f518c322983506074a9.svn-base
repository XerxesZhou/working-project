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

using SmartSoft.Component;
using SmartSoft.Domain.Common;

namespace SmartSoft.Web
{
    public partial class top : SmartSoft.Presentation.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            sysConfig config = (sysConfig)Session["Config"];

            string showInfo = "当前操作员：";
            showInfo += "<font color='red'>" + this.Operator.opeName + "</font>";
            showInfo += "&nbsp;&nbsp;时间：<font color='red'>" + DateTime.Now.ToShortDateString() + "</font>";

            //showInfo += "&nbsp;&nbsp;用户数：<font color='red'>" + config.OperatorCount + "</font>";

            this.lt_Operator.Text = showInfo;
        }

        protected void lb_logout_Click(object sender, EventArgs e)
        {
            //清除Session
            Session.Abandon();
            //清除Cookies
            //HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);


            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "logout", "<script language='javascript'>logout();</script>");
        }
    }
}
