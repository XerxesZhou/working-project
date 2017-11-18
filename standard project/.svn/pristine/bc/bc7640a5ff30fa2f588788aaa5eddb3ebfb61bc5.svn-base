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
    public partial class PageList : WebList<sysPage>
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

            this.ListGrid = GridPageList;
            this.initToolBar();

            if (!Page.IsPostBack)
            {
                IList<sysPage> pageList = _authorization.GetsysPageByCondition(" menuparentpageid=0 order by MenuOrderBy ASC, PageFilePath ASC");
                (new WebFunc()).BindListControl<sysPage>(pageList, SearchModel, "PageName", "PageID");
            }
        }

        protected void lb_Search_Click(object sender, EventArgs e)
        {
            this.BindPage();
        }

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
            if (type == "toolbar")
            {
                strWhere = " (menuparentpageid=" + parentId + " or pageid=" + parentId + ") and istoolbar='1' Order by PageID ASC, ToolOrderBy ASC";
            }
            if (type == "other")
            {
                strWhere = " ismenu='0' and istoolbar='0' Order by PageFilePath ASC";
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

            GridPageList.DataSource = bindPagelist;
            GridPageList.DataBind();
        }
        private void BindChildAndLeaf(sysPage parentPage, ArrayList bindPagelist, string type)
        {
            //绑定子节点
            string strWhere = string.Empty;
            if (type == "menu")
            {
                strWhere = " menuparentpageid=" + parentPage.PageID + " Order by MenuOrderBy ASC, PageFilePath ASC";
            }
            if (type == "toolbar")
            {
                strWhere = " toolbarparentpageid=" + parentPage.PageID + " Order by ToolOrderBy ASC, PageFilePath ASC";
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
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += this.lb_Search_Click;
        }

        protected void GridPageList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.cursor='hand';this.style.backgroundColor='#B9D1F3';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';this.style.cursor='hand';");

                int pageId = int.Parse(GridPageList.DataKeys[e.Row.RowIndex].Values["PageID"].ToString());
                bool ismenuDirectory = bool.Parse(GridPageList.DataKeys[e.Row.RowIndex].Values["IsMenuDirectory"].ToString());

                if (!ismenuDirectory)
                {
                    //添加"设置页面控件"按钮
                    LinkButton lt_Control = new LinkButton();
                    lt_Control.ID = "ltControl";
                    lt_Control.Text = "设置页面控件";
                    lt_Control.CssClass = "lbclass";
                    string c_url = "openwinsimpscroll('PageControlList.aspx?pageId=" + pageId + "',800,600);return false;";
                    lt_Control.Attributes.Add("onclick", c_url);
                    e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(lt_Control);

                    Label lb = new Label();
                    lb.ID = "lb";
                    lb.Text = "　|　";
                    e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(lb);

                    LinkButton lt_purview = new LinkButton();
                    lt_purview.ID = "ltpurview";
                    lt_purview.CssClass = "lbclass";
                    lt_purview.Text = "设置页面视图/布局";
                    string s_url = "openwinsimpscroll('SetLayoutViewPurview.aspx?pageId=" + pageId + "',600,480);return false;";
                    lt_purview.Attributes.Add("onclick", s_url);
                    e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(lt_purview);
                }
            }


        }
    }
}
