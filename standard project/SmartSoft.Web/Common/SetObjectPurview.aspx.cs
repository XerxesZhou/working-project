/***************************************************************************
 * 
 *       功能：     授权列表
 *       作者：     彭伟
 *       日期：     2007/01/29
 * 
 *       修改日期： 2007/1/30
 *       修改人：    肖秋李
 *       修改内容： 增加用户部门权限并优化
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Common
{
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

    using System.Collections.Generic;
    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;

    public partial class SetObjectPurview : SmartSoft.Presentation.PageBase
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        private string Type
        {
            get
            {
                return Request.QueryString["Type"] + "";
            }
        }
        //对象为用户时，用户所属角色
        public IList<sysOperatorInRole> opRoleList
        {
            get
            {
                if (SearchPurviewType.SelectedValue != string.Empty && SearchPurviewType.SelectedValue == "ope")
                {
                    if (SearchObjectID.SelectedValue != string.Empty)
                    {
                        int objectId = int.Parse(SearchObjectID.SelectedValue);
                        return _authorization.GetOperatorRoles(objectId);
                    }
                }
                return null;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitToolBar();
            if (!Page.IsPostBack)
            {
                IList<sysPage> pageList = null;
                if (this.Operator.opeIsDeveloper)
                {
                    pageList = _authorization.GetsysPageByCondition(" menuparentpageid=0 order by MenuOrderBy ASC, PageFilePath ASC");
                }
                else
                {
                    pageList = _authorization.GetsysPageByCondition(" menuparentpageid=0 and pageid != 11206 order by MenuOrderBy ASC, PageFilePath ASC");
                }

                (new WebFunc()).BindListControl<sysPage>(pageList, SearchModel, "PageName", "PageID");

                ListItem li1 = new ListItem("请选择", "");
                ListItem li2 = new ListItem("系统角色", "role");
                ListItem li3 = new ListItem("系统操作员", "ope");
                SearchPurviewType.Items.Add(li1);
                SearchPurviewType.Items.Add(li2);
                SearchPurviewType.Items.Add(li3);

                if (Type == "Role")
                {
                    SearchPurviewType.SelectedValue = "role";
                    SearchPurviewType.Enabled = false;
                    SearchPurviewType_SelectedIndexChanged(null, null);
                    SearchObjectID.SelectedValue = Request.QueryString["ID"] + "";
                    SearchObjectID.Enabled = false;
                    this.BindPage();
                }
                else if (Type == "Ope")
                {
                    SearchPurviewType.SelectedValue = "ope";
                    SearchPurviewType.Enabled = false;
                    SearchPurviewType_SelectedIndexChanged(null, null);
                    SearchObjectID.SelectedValue = Request.QueryString["ID"] + "";
                    SearchObjectID.Enabled = false;
                    this.BindPage();
                }
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
            //string type = SearchType.SelectedValue;
            int parentId = 0;
            if (SearchModel.SelectedValue != string.Empty)
            {
                int.TryParse(SearchModel.SelectedValue, out parentId);
            }

            if (this.Operator.opeIsDeveloper)
            {
                strWhere = " (menuparentpageid=" + parentId + " or pageid=" + parentId + ")  and isMenu = 1  Order by  MenuParentPageID asc,MenuOrderBy ASC";
            }
            else
            {
                strWhere = " (menuparentpageid=" + parentId + " or pageid=" + parentId + ")  and pageid != 11206  and isMenu = 1 Order by  MenuParentPageID asc, MenuOrderBy ASC";
            }
            
            IList<sysPage> pageList = _authorization.GetsysPageByCondition(strWhere);
            ArrayList bindPagelist = new ArrayList();
            bindPagelist.Clear();
            foreach (sysPage page in pageList)
            {
                bindPagelist.Add(page);

                if (page.PageID != parentId)
                {
                    this.BindChildAndLeaf(page, bindPagelist);
                }
            }
            GridsysPage.DataSource = bindPagelist;
            GridsysPage.DataBind();
        }

        private void BindChildAndLeaf(sysPage parentPage, ArrayList bindPagelist)
        {
            //绑定子节点
            string strWhere = string.Empty;
            strWhere = " menuparentpageid=" + parentPage.PageID + "   and isMenu = 1 Order by  MenuOrderBy ASC";
            

            IList<sysPage> pageList = _authorization.GetsysPageByCondition(strWhere);
            foreach (sysPage page in pageList)
            {
                page.ParentPageName = parentPage.PageName;
                bindPagelist.Add(page);

                this.BindChildAndLeaf(page, bindPagelist);
            }
        }
        #endregion

        protected void GridsysPage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //对象ID
            int objectId = 0;
            if (SearchObjectID.SelectedValue != string.Empty)
            {
                objectId = int.Parse(SearchObjectID.SelectedValue);
            }

            //绑定页面的权限
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //创建复选框控件
                CheckBoxList cbl = (CheckBoxList)e.Row.FindControl("cbl_PagePurview");
                //cbl.ID = "cbl_PagePurview";
                //cbl.RepeatDirection = RepeatDirection.Horizontal;

                //得到该行的PageId
                int PageId = Convert.ToInt32(GridsysPage.DataKeys[e.Row.RowIndex].Value);
                sysPage syspage = _authorization.GetsysPageDetail(PageId);

                //得到对象对该Page的权限
                long objectPurview = 0;
                if (objectId != 0)
                {
                    sysObjectPurview purview = _authorization.GetObjectPagePurview(objectId, PageId);
                    if (purview != null)
                    {
                        objectPurview = purview.PurviewCode;
                    }
                }

                //得到所有系统功能
                IList<sysFunction> sysFunctionList = _authorization.GetAllSysFunctionList();

                //用户所有角色权限码
                IDictionary<int, long> htRolwRurviewCode = new Dictionary<int, long>();
                //Hashtable htRolwRurviewCode = new Hashtable();

                //部门权限
                //sysObjectPurview depPurview = new sysObjectPurview();
                if (SearchPurviewType.SelectedValue != string.Empty && SearchPurviewType.SelectedValue == "ope")
                {
                    //得到用户所有角色权限码,装入Hashtable
                    for (int i = 0; i < this.opRoleList.Count; i++)
                    {
                        sysOperatorInRole sysOpInRole = this.opRoleList[i];
                        sysObjectPurview rolePurview = _authorization.GetObjectPagePurview(sysOpInRole.RoleID, PageId);
                        if (rolePurview != null)
                        {
                            htRolwRurviewCode[sysOpInRole.RoleID] = rolePurview.PurviewCode;
                        }
                    }

                    //得到所属部门权限
                    //depPurview = _authorization.GetObjectPagePurview(this.departmentID, PageId);
                }
                //页面权限总和 按位与 系统每一个功能的权限码，大于0则显示
                foreach (sysFunction sysfunc in sysFunctionList)
                {
                    if ((syspage.FunctionCount & sysfunc.FunctionPurviewCode) > 0)
                    {
                        ListItem item = new ListItem(sysfunc.FunctionName, sysfunc.FunctionPurviewCode.ToString());

                        //判断对象是否有该权限，如果有则默认选中状态
                        if ((objectPurview & sysfunc.FunctionPurviewCode) > 0)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            //此处判断如果是操作员的情况
                            //判断操作员所属的角色或部门是否拥有该权限
                            //如果已经拥有，则显示选中并Enable
                            if (SearchPurviewType.SelectedValue != string.Empty && SearchPurviewType.SelectedValue == "ope")
                            {
                                if (objectId != 0)
                                {
                                    foreach (long p in htRolwRurviewCode.Values)
                                    {
                                        //判断是否有权限
                                        if ((p & sysfunc.FunctionPurviewCode) > 0)
                                        {
                                            item.Selected = true;
                                            item.Enabled = false;
                                        }
                                    }
                                }
                            }
                        }
                        cbl.Items.Add(item);
                    }
                }
            }
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchObjectID.SelectedValue != string.Empty)
                {
                    ArrayList al = new ArrayList();
                    int ObjectId = int.Parse(SearchObjectID.SelectedValue);
                    foreach (GridViewRow row in GridsysPage.Rows)
                    {
                        int PageId = Convert.ToInt32(GridsysPage.DataKeys[row.RowIndex].Value);
                        CheckBoxList cbl = (CheckBoxList)row.FindControl("cbl_PagePurview");
                        long Count = 0;
                        foreach (ListItem item in cbl.Items)
                        {
                            if (item.Selected == true && item.Enabled != false)
                            {
                                Count += long.Parse(item.Value);
                            }
                        }

                        //根据ObjectId和PageId得到授权信息如果已有授权信息，但Count又为0则需修改
                        sysObjectPurview sysobjectpurview = _authorization.GetObjectPagePurview(ObjectId, PageId);
                        if ((Count != 0) || (sysobjectpurview != null))
                        {
                            sysObjectPurview objectPurview = new sysObjectPurview();
                            objectPurview.PageID = PageId;
                            objectPurview.PurviewCode = Count;
                            objectPurview.ObjectID = ObjectId;

                            al.Add(objectPurview);
                        }
                    }

                    _authorization.AddObjectPagePurview(al);

                    string strScript = string.Empty;
                    if (Type != "")
                    {
                        strScript = "alert('操作成功！'); window.close();";
                    }
                    else
                    {
                        strScript = "alert('操作成功！');__doPostBack('ToolBar1$lb_Search','');";
                    }
                    WebFunc.ExecuteAjaxJScript(strScript, Page);
                }
            }
            catch (Exception ex)
            {
                WebFunc.AjaxAlert(ex.Message, Page);
            }
        }

        private void InitToolBar()
        {
            //保存
            this.ToolBar1.lb_Save.Visible = true;
            this.ToolBar1.lb_Save.Click += this.btn_Ok_Click;

            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += this.lb_Search_Click;
        }

        protected void SearchPurviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据选择授权类型来绑定objectID
            if (SearchPurviewType.SelectedValue != string.Empty)
            {
                SearchObjectID.Items.Clear();
                string type = SearchPurviewType.SelectedValue;
                WebFunc webfun = new WebFunc();
                if (type == "role")
                {
                    IList<sysRole> roleList = _authorization.GetSysRoleList();
                    webfun.BindListControl<sysRole>(roleList, SearchObjectID, "RoleName", "RoleID");
                }
                else if (type == "ope")
                {
                    IList<Operators> opeList = _org.GetOperatorsList();
                    webfun.BindListControl<Operators>(opeList, SearchObjectID, "opeName", "opeID");
                }
            }
        }
    }
}
