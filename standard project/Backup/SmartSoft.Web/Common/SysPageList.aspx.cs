/***************************************************************************
 * 
 *       功能：     系统页面列表
 *       作者：     彭伟
 *       日期：     2007/01/28
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Common
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
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;


    public partial class SysPageList : WebList<sysPage>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            //检测页面权限
            CheckPagePermission();

            this.ListGrid = GridsysPage;
            this.initToolBar();
            if (!Page.IsPostBack)
            {
                IList<sysPage> pageList = _authorization.GetsysPageByCondition(" menuparentpageid=0 order by MenuOrderBy ASC, PageFilePath ASC");
                (new WebFunc()).BindListControl<sysPage>(pageList, SearchModel, "PageName", "PageID");
            }
        }

        #region Event
        protected void GridsysPage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }

        protected void GridsysPage_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnDeletePage_Click(object sender, EventArgs e)
        {
            string ids = Request.Form.Get("checkboxname");
            DbHelperSQL.ExecuteSQL("Delete from sysPage where PageID in (" + ids + ")");
            lb_Search_Click(sender, e);
        }

        protected void lb_Search_Click(object sender, EventArgs e)
        {
            this.BindPage();
        }

        #endregion

        #region Method

        #region 绑定GridView
        private void BindPage()
        {
            //得到顶级节点
            string strWhere = string.Empty;
            string type = SearchType.SelectedValue;
            int parentId = 0;
            if (SearchModel.SelectedValue != string.Empty)
            {
                int.TryParse(SearchModel.SelectedValue, out parentId);
            }

            if (type == "menu")
            {
                strWhere = " (menuparentpageid=" + parentId + " or pageid=" + parentId + ") and ismenu='1' Order by PageID ASC, MenuOrderBy ASC";
            }
            if (type == "other")
            {
                strWhere = " (menuparentpageid=" + parentId + " or pageid=" + parentId + ") and ismenu='0' and istoolbar='0' Order by PageFilePath ASC";
            }

            IList<sysPage> pageList = _authorization.GetsysPageByCondition(strWhere);
            ArrayList bindPagelist = new ArrayList();
            bindPagelist.Clear();
            
            foreach (sysPage page in pageList)
            {
                bindPagelist.Add(page);
                
                if (type != "other" && page.PageID != parentId)
                {
                    this.BindChildAndLeaf(page, bindPagelist, type);
                }
            }

            GridsysPage.DataSource = bindPagelist;
            GridsysPage.DataBind();
        }
        private void BindChildAndLeaf(sysPage parentPage, ArrayList bindPagelist, string type)
        {
            //绑定子节点
            string strWhere = string.Empty;
            if (type == "menu")
            {
                strWhere = " menuparentpageid=" + parentPage.PageID + " Order by MenuOrderBy ASC, PageFilePath ASC";
            }

            IList<sysPage> pageList = _authorization.GetsysPageByCondition(strWhere);
            foreach (sysPage page in pageList)
            {
                page.ParentPageName = parentPage.PageName;
                bindPagelist.Add(page);
                this.BindChildAndLeaf(page, bindPagelist, type);
            }
        }
        #endregion

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //增加
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            //增加子节点
            this.ToolBar1.lb_AddChild.Visible = true;
            this.ToolBar1.lb_AddChild.Attributes.Add("href", "javascript:AddSubFunction();");

            //删除
            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Attributes.Add("href", "javascript:DeletePage();");

            //编辑
            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Modify();");

            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += this.lb_Search_Click;
        }
        #endregion
    }
}
