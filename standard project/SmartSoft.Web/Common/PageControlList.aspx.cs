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
using SmartSoft.Domain.Common;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Presentation;


namespace SmartSoft.Web.Common
{
    public partial class PageControlList : WebList<sysViewLayoutControl>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        public int? PageID
        {
            get 
            {
                int? result = null;
                if (Request.QueryString["pageId"] != null && Request.QueryString["pageId"] != string.Empty)
                {
                    result = int.Parse(Request.QueryString["pageId"]);
                }
                return result;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            this.ListGrid = GridPageControl;
            this.InitToolBar();
            this.SetDataSource();

            if (this.PageID.HasValue)
            {
                sysPage page = _authorization.GetsysPageDetail(this.PageID.Value);
                lt_title.Text = page.PageName + "(" + page.PageFilePath + ")";
            }

            base.Page_Load(sender, e);
        }

        public void SetDataSource()
        {
            if (this.PageID.HasValue)
            {
                IList<sysViewLayoutControl> svcList = _authorization.SelectViewLayoutControlByPageID(this.PageID.Value);
                this.DataSource = svcList;
            }
        }

        private void InitToolBar()
        {
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Edit();");

            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "javascript:return Delete();");
            this.ToolBar1.lb_Delete.Click += new EventHandler(lb_Delete_Click);
        }

        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_del = this.GetCheckedValues();
                foreach (string key in str_list_del)
                {
                    int id = int.Parse(key);
                    _authorization.DeleteViewLayoutControl(id);
                }

                this.SetDataSource();
                this.ReBindGrid();
            }
            catch (Exception ex)
            {
                WebFunc.AjaxAlertError("删除时出错！\r\n原因：\r\n" + ex.Message, Page);
            }
        }

        protected void GridPageControl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }
    }
}
