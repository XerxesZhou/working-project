namespace SmartSoft.Web.SystemConfig
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using SmartSoft.Component;
    using SmartSoft.Service.Interface.Data;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Presentation;
    using UDEF.Service;

    public partial class CustomerOpptunityPhaseEditForm : WebForm<Customer>
    {
        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    //±à¼­×´Ì¬
                    DataTable dt = DbHelperSQL.GetDataTable("select * from defCustomerOpptunityPhase where copID = "+Request.QueryString["ID"]);
                    this.tb_copName.Text = dt.Rows[0]["copName"] + "";
                    this.tb_copRate.Text = dt.Rows[0]["copRate"] + "";
                    this.tb_copOrderBy.Text = dt.Rows[0]["copOrderBy"] + "";
                    this.tb_copRemark.Text = dt.Rows[0]["copRemark"] + "";
                }
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            
            decimal rate = 0;
            if (tb_copRate.Text != "")
            {
                decimal.TryParse(tb_copRate.Text, out rate);
            }

            int orderBy = 0;
            if (tb_copOrderBy.Text != "")
            {
                int.TryParse(tb_copOrderBy.Text, out orderBy);
            }

            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
            {
                DbHelperSQL.ExecuteSQL("update defCustomerOpptunityPhase set copName='" + this.tb_copName.Text + "',copRate='" + rate + "', copOrderBy = " + orderBy + ", copRemark='" + this.tb_copRemark.Text + "' where copID = " + Request.QueryString["ID"]);
            }
            else
            {
                DbHelperSQL.ExecuteSQL("insert into defCustomerOpptunityPhase(copName,copRate,copOrderBy,copRemark) Values('" + this.tb_copName.Text + "','" + rate + "','" + orderBy + "','" + this.tb_copRemark.Text + "')");
            }
            //Çå³ý»º´æ
            _userdefinebase.ClearCache("defCustomerOpptunityPhase");

            string strScripts = "<script language='javascript'>alert('±£´æ³É¹¦£¡');window.opener.Refresh();window.close();</script>";
            WebFunc.ExecuteJScript(strScripts, Page);
        }
    }
}
