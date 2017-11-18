/***************************************************************************
 * 
 *       ���ܣ�     ��Ȩ�б�
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007/01/29
 * 
 *       �޸����ڣ� 2007/1/30
 *       �޸��ˣ�    Ф����
 *       �޸����ݣ� �����û�����Ȩ�޲��Ż�
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
        //����Ϊ�û�ʱ���û�������ɫ
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

                ListItem li1 = new ListItem("��ѡ��", "");
                ListItem li2 = new ListItem("ϵͳ��ɫ", "role");
                ListItem li3 = new ListItem("ϵͳ����Ա", "ope");
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

        #region ��GridView
        private void BindPage()
        {
            //�õ������ڵ�
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
            //���ӽڵ�
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
            //����ID
            int objectId = 0;
            if (SearchObjectID.SelectedValue != string.Empty)
            {
                objectId = int.Parse(SearchObjectID.SelectedValue);
            }

            //��ҳ���Ȩ��
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //������ѡ��ؼ�
                CheckBoxList cbl = (CheckBoxList)e.Row.FindControl("cbl_PagePurview");
                //cbl.ID = "cbl_PagePurview";
                //cbl.RepeatDirection = RepeatDirection.Horizontal;

                //�õ����е�PageId
                int PageId = Convert.ToInt32(GridsysPage.DataKeys[e.Row.RowIndex].Value);
                sysPage syspage = _authorization.GetsysPageDetail(PageId);

                //�õ�����Ը�Page��Ȩ��
                long objectPurview = 0;
                if (objectId != 0)
                {
                    sysObjectPurview purview = _authorization.GetObjectPagePurview(objectId, PageId);
                    if (purview != null)
                    {
                        objectPurview = purview.PurviewCode;
                    }
                }

                //�õ�����ϵͳ����
                IList<sysFunction> sysFunctionList = _authorization.GetAllSysFunctionList();

                //�û����н�ɫȨ����
                IDictionary<int, long> htRolwRurviewCode = new Dictionary<int, long>();
                //Hashtable htRolwRurviewCode = new Hashtable();

                //����Ȩ��
                //sysObjectPurview depPurview = new sysObjectPurview();
                if (SearchPurviewType.SelectedValue != string.Empty && SearchPurviewType.SelectedValue == "ope")
                {
                    //�õ��û����н�ɫȨ����,װ��Hashtable
                    for (int i = 0; i < this.opRoleList.Count; i++)
                    {
                        sysOperatorInRole sysOpInRole = this.opRoleList[i];
                        sysObjectPurview rolePurview = _authorization.GetObjectPagePurview(sysOpInRole.RoleID, PageId);
                        if (rolePurview != null)
                        {
                            htRolwRurviewCode[sysOpInRole.RoleID] = rolePurview.PurviewCode;
                        }
                    }

                    //�õ���������Ȩ��
                    //depPurview = _authorization.GetObjectPagePurview(this.departmentID, PageId);
                }
                //ҳ��Ȩ���ܺ� ��λ�� ϵͳÿһ�����ܵ�Ȩ���룬����0����ʾ
                foreach (sysFunction sysfunc in sysFunctionList)
                {
                    if ((syspage.FunctionCount & sysfunc.FunctionPurviewCode) > 0)
                    {
                        ListItem item = new ListItem(sysfunc.FunctionName, sysfunc.FunctionPurviewCode.ToString());

                        //�ж϶����Ƿ��и�Ȩ�ޣ��������Ĭ��ѡ��״̬
                        if ((objectPurview & sysfunc.FunctionPurviewCode) > 0)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            //�˴��ж�����ǲ���Ա�����
                            //�жϲ���Ա�����Ľ�ɫ�����Ƿ�ӵ�и�Ȩ��
                            //����Ѿ�ӵ�У�����ʾѡ�в�Enable
                            if (SearchPurviewType.SelectedValue != string.Empty && SearchPurviewType.SelectedValue == "ope")
                            {
                                if (objectId != 0)
                                {
                                    foreach (long p in htRolwRurviewCode.Values)
                                    {
                                        //�ж��Ƿ���Ȩ��
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

                        //����ObjectId��PageId�õ���Ȩ��Ϣ���������Ȩ��Ϣ����Count��Ϊ0�����޸�
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
                        strScript = "alert('�����ɹ���'); window.close();";
                    }
                    else
                    {
                        strScript = "alert('�����ɹ���');__doPostBack('ToolBar1$lb_Search','');";
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
            //����
            this.ToolBar1.lb_Save.Visible = true;
            this.ToolBar1.lb_Save.Click += this.btn_Ok_Click;

            //��ѯ
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += this.lb_Search_Click;
        }

        protected void SearchPurviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����ѡ����Ȩ��������objectID
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
