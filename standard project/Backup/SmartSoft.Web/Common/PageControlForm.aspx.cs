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

using UDEF.Domain;
using UDEF.Service;
using SmartSoft.Presentation;


namespace SmartSoft.Web.Common
{
    public partial class PageControlForm : WebForm<sysViewLayoutControl>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        private SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    this.BindSystemTable();
                    if (Request.QueryString["ControlID"] != null && Request.QueryString["ControlID"] != string.Empty)
                    {
                        //修改状态
                        lt_title.Text = "编辑页面控件";
                        int controlId = int.Parse(Request.QueryString["ControlID"]);
                        sysViewLayoutControl svc = _authorization.GetViewLayoutControlDetail(controlId);

                        this.FormData = svc;
                        this.BindPageNameUrl(svc.PageID);
                    }
                    else
                    {
                        lt_title.Text = "新增页面控件";
                        if (Request.QueryString["PageID"] != null && Request.QueryString["PageID"] != string.Empty)
                        {
                            tb_PageID.Text = Request.QueryString["PageID"];
                            this.BindPageNameUrl(int.Parse(tb_PageID.Text.Trim()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    WebFunc.AlertClose("页面参数错误！", Page);
                }
            }
        }

        private void BindPageNameUrl(int PageID)
        {
            sysPage page = _authorization.GetsysPageDetail(PageID);

            lt_PageName.Text = page.PageName;
            lt_url.Text = page.PageFilePath;
        }

        private void BindSystemTable()
        {
            IList<SystemTable> tableList = _systemtable.SelectAll();

            (new WebFunc()).BindListControl<SystemTable>(tableList, ddl_TableID, "TableText", "TableID", "请选择", "");
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                sysViewLayoutControl svc = this.FormData;

                if (Request.QueryString["ControlID"] != null && Request.QueryString["ControlID"] != string.Empty)
                {
                    _authorization.UpdateViewLayoutControl(svc);
                }
                else
                {
                    _authorization.AddViewLayoutControl(svc);
                }

                string strScript = "<script language='javascript'>alert('保存成功！');window.close();window.opener.location.reload();</script>";
                WebFunc.ExecuteJScript(strScript, Page);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
