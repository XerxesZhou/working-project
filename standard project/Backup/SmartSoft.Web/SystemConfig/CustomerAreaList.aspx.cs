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
    using SmartSoft.Presentation;
    using SmartSoft.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Service.Interface.Data;
    using UDEF.Service;

    public partial class CustomerAreaList : WebList<Customer>
    {
        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }


        protected override void Page_Load(object sender, EventArgs e)
        {
            initToolBar();

            bindGrid();
        }

        protected void GridCustomerType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }

        private void initToolBar()
        {
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.CssClass = "hidden";
            this.ToolBar1.lb_Search.Click += lb_Search_Click;

            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Edit();");

            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "return Delete();");
        }

        private void bindGrid()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from defCustomerArea order by  caCode asc;");

            this.GridCustomerArea.DataSource = dt;
            this.GridCustomerArea.DataBind();
        }

        protected void lb_Search_Click(object sender, EventArgs e)
        {
            this.bindGrid();
        }

        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_del = this.GetCheckedValues();
                foreach (string key in str_list_del)
                {
                    int id = int.Parse(key);

                    DbHelperSQL.ExecuteSQL("delete from defCustomerArea where caID = " + id + ";");
                }

                this.bindGrid();

                //Çå³ý»º´æ
                _userdefinebase.ClearCache(50);
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("É¾³ýÊ±³ö´í£¡´íÎóÐÅÏ¢£º" + ex.Message, Page);
            }
        }
    }
}
