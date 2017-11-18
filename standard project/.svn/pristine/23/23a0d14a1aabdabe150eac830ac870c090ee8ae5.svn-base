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

    public partial class CustomerTypeEditForm : WebForm<Customer>
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
                    DataTable dt = DbHelperSQL.GetDataTable("select * from defCustomerType where ctID = " + Request.QueryString["ID"]);
                    this.tb_ctName.Text = dt.Rows[0]["ctName"] + "";
                    this.tb_ctProtectDays.Text = dt.Rows[0]["ctProtectDays"] + "";
                    this.tb_ctIncreaseProtectDays.Text = dt.Rows[0]["ctIncreaseProtectDays"] + "";
                    this.tb_ctOrderBy.Text = dt.Rows[0]["ctOrderBy"] + "";
                    this.tb_ctRemark.Text = dt.Rows[0]["ctRemark"] + "";
                }
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            int protectDays = 0;
            if (tb_ctProtectDays.Text != "")
            {
                int.TryParse(tb_ctProtectDays.Text, out protectDays);
            }

            int increaseProtectDays = 0;
            if (tb_ctIncreaseProtectDays.Text != "")
            {
                int.TryParse(tb_ctIncreaseProtectDays.Text, out increaseProtectDays);
            }

            int orderBy = 0;
            if (tb_ctOrderBy.Text != "")
            {
                int.TryParse(tb_ctOrderBy.Text, out orderBy);
            }

            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
            {
                DbHelperSQL.ExecuteSQL("update defCustomerType set ctName='" + this.tb_ctName.Text + "',ctProtectDays='" + protectDays + "',ctIncreaseProtectDays = " + increaseProtectDays + ", ctOrderBy = " + orderBy + ", ctRemark='" + this.tb_ctRemark.Text + "' where ctID = " + Request.QueryString["ID"]);
            }
            else
            {
                DbHelperSQL.ExecuteSQL("insert into defCustomerType(ctName,ctProtectDays,ctIncreaseProtectDays,ctOrderBy,ctRemark) Values('" + this.tb_ctName.Text + "','" + protectDays + "','" + increaseProtectDays + "','" + orderBy + "','" + this.tb_ctRemark.Text + "')");
            }
            //Çå³ý»º´æ
            _userdefinebase.ClearCache("defCustomerType");

            string strScripts = "<script language='javascript'>alert('±£´æ³É¹¦£¡');window.opener.Refresh();window.close();</script>";
            WebFunc.ExecuteJScript(strScripts, Page);
        }
    }
}
