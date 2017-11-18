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

    public partial class CustomerAreaEditForm : WebForm<Customer>
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
                    int id = int.Parse(Request.QueryString["ID"]);
                    DataTable dt = DbHelperSQL.GetDataTable("select * from defCustomerArea where caID = " + Request.QueryString["ID"]);
                    this.tb_caID.Text = dt.Rows[0]["caID"].ToString();
                    this.tb_caName.Text = dt.Rows[0]["caName"].ToString();
                    this.tb_caCode.Text = dt.Rows[0]["caCode"].ToString();
                }
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
            {
                DbHelperSQL.ExecuteSQL("update defCustomerArea set caName='" + this.tb_caName.Text + "',caCode='" + this.tb_caCode.Text + "' where caID = " + Request.QueryString["ID"]);
            }
            else
            {
                DbHelperSQL.ExecuteSQL("insert into defCustomerArea(caName,caCode) Values('" + this.tb_caName.Text + "','" + this.tb_caCode.Text + "')");
            }
            //Çå³ý»º´æ
            _userdefinebase.ClearCache(50);

            string strScripts = "<script language='javascript'>alert('±£´æ³É¹¦£¡');window.opener.Refresh();window.close();</script>";
            WebFunc.ExecuteJScript(strScripts, Page);
        }
    }
}
