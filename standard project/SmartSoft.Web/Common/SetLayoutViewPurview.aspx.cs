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
using SmartSoft.Domain.Enumerate;
using SmartSoft.Service.Interface.Common;

using UDEF.Domain;
using UDEF.Service;

namespace SmartSoft.Web.Common
{
    public partial class SetLayoutViewPurview : SmartSoft.Presentation.PageBase
    {
        #region 定义
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        private LayoutService _layout;
        public LayoutService layout
        {
            set { _layout = value; }
        }

        private ColumnViewService _columnview;
        public ColumnViewService columnview
        {
            set { _columnview = value; }
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.PageID.HasValue)
                {
                    //this.InitToolbar();
                    this.BindPageControl(this.PageID.Value);
                    this.BindAction();
                    this.BindRole();

                    sysPage page = _authorization.GetsysPageDetail(this.PageID.Value);
                    lb_PageName.Text = page.PageName;
                }
                else
                {
                    WebFunc.AlertClose("页面参数错误！", Page);
                }
            }
        }

        public void InitToolbar()
        {
            //this.ToolBar1.lb_Save.Visible = true;
            //this.ToolBar1.lb_Save.Click += lbt_Save_Click;

            //this.ToolBar1.lb_Close.Visible = true;
        }


        /// <summary>
        /// 绑定页面控件
        /// </summary>
        /// <param name="pageId">页面Id</param>
        private void BindPageControl(int pageId)
        {
            IList<sysViewLayoutControl> svcList = _authorization.SelectViewLayoutControlByPageID(pageId);
            (new WebFunc()).BindListControl<sysViewLayoutControl>(svcList, ddl_ControlID, "ControlName", "ControlID");
        }

        /// <summary>
        /// 绑定动作
        /// </summary>
        private void BindAction()
        { 
            ddl_ActionID.Items.Clear();
            ListItem defaultItem = new ListItem("请选择", "");
            ddl_ActionID.Items.Add(defaultItem);

            ListItem allItem = new ListItem("全部", "ALL");
            ddl_ActionID.Items.Add(allItem);

            foreach (int i in Enum.GetValues(typeof(FormAction)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(FormAction), i), i.ToString());
                ddl_ActionID.Items.Add(item);
            }
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindRole()
        {
            IList<sysRole> roleList = _authorization.GetSysRoleList();
            (new WebFunc()).BindListControl<sysRole>(roleList, ddl_ObjectID, "RoleName", "RoleID", "全部", "ALL");
        }

        /// <summary>
        /// 保存授权信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                int controlId = int.Parse(ddl_ControlID.SelectedValue);
                int? objectId = null;
                if (ddl_ObjectID.SelectedValue != "ALL")
                {
                    objectId = int.Parse(ddl_ObjectID.SelectedValue);
                }
                int? actionId = null;
                if (ddl_ActionID.SelectedValue != "ALL" && ddl_ActionID.SelectedValue != "")
                {
                    actionId = int.Parse(ddl_ActionID.SelectedValue);
                }

                ArrayList al = new ArrayList();
                foreach (ListItem item in cbl_LvList.Items)
                {
                    if (item.Selected == true)
                    {
                        if (objectId.HasValue)
                        {
                            sysRoleLayoutView srlv = new sysRoleLayoutView();
                            srlv.ControlID = controlId;
                            srlv.ObjectID = objectId.Value;
                            srlv.ActionID = actionId;

                            srlv.LayoutOrViewID = int.Parse(item.Value.Trim());
                            al.Add(srlv);
                        }
                        else
                        {
                            for (int i = 0; i < ddl_ObjectID.Items.Count; i++)
                            {
                                if (ddl_ObjectID.Items[i].Value != "ALL" && ddl_ObjectID.Items[i].Value != "")
                                {
                                    objectId = int.Parse(ddl_ObjectID.Items[i].Value);

                                    if (ddl_ActionID.SelectedValue == "ALL")
                                    {
                                        for (int j = 0; j < ddl_ActionID.Items.Count; j++)
                                        {
                                            if (ddl_ActionID.Items[j].Value != "ALL" && ddl_ActionID.Items[j].Value != "")
                                            {
                                                actionId = int.Parse(ddl_ActionID.Items[j].Value);

                                                sysRoleLayoutView srlv = new sysRoleLayoutView();
                                                srlv.ControlID = controlId;
                                                srlv.ObjectID = objectId.Value;
                                                srlv.ActionID = actionId;

                                                srlv.LayoutOrViewID = int.Parse(item.Value.Trim());
                                                al.Add(srlv);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        
                                        sysRoleLayoutView srlv = new sysRoleLayoutView();
                                        srlv.ControlID = controlId;
                                        srlv.ObjectID = objectId.Value;
                                        srlv.ActionID = actionId;

                                        srlv.LayoutOrViewID = int.Parse(item.Value.Trim());
                                        al.Add(srlv);
                                    }
                                }
                            }
                        }
                    }
                }

                _authorization.AddRoleLayoutView(al, objectId, controlId, actionId);

                WebFunc.Alert("保存成功！", Page);
            }
            catch (Exception ex)
            {
                WebFunc.AjaxAlertError(ex.Message, Page);
            }
        }

        protected void ddl_ControlID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ControlID.SelectedValue != "")
            {
                sysViewLayoutControl svc = _authorization.GetViewLayoutControlDetail(int.Parse(ddl_ControlID.SelectedValue));

                if (svc.LayoutOrView == 1)
                {

                    IList<Layout> layoutList = _layout.SelectDynamic(" AND TableID=" + svc.TableID);
                    cbl_LvList.DataTextField = "LayoutName";
                    cbl_LvList.DataValueField = "LayoutID";
                    cbl_LvList.DataSource = layoutList;
                    cbl_LvList.DataBind();
                }
                else
                {
                    IList<ColumnView> viewList = _columnview.SelectDynamic(" AND TableID=" + svc.TableID);
                    cbl_LvList.DataTextField = "ViewName";
                    cbl_LvList.DataValueField = "ViewID";
                    cbl_LvList.DataSource = viewList;
                    cbl_LvList.DataBind();
                }

                if (ddl_ObjectID.SelectedValue != "ALL")
                {
                    this.BindPurview();
                }
            }
        }

        protected void ddl_ObjectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ControlID.SelectedValue != string.Empty && ddl_ObjectID.SelectedValue != "ALL")
            {
                this.BindPurview();
            }
        }

        private void BindPurview()
        {
            foreach (ListItem item in cbl_LvList.Items)
            {
                item.Selected = false;
            }

            sysViewLayoutControl svc = _authorization.GetViewLayoutControlDetail(int.Parse(ddl_ControlID.SelectedValue));
            //得到已授权的信息
            int roleId = int.Parse(ddl_ObjectID.SelectedValue);
            int controlId = int.Parse(ddl_ControlID.SelectedValue);
            int? actionId = null;
            if (ddl_ActionID.SelectedValue != string.Empty && ddl_ActionID.SelectedValue != "ALL")
            {
                actionId = int.Parse(ddl_ActionID.SelectedValue);
            }
            IList<sysRoleLayoutView> srlvList = _authorization.SelectRoleLayoutViewByRoleControlAction(roleId, controlId, actionId);

            foreach (sysRoleLayoutView srlv in srlvList)
            {
                foreach (ListItem item in cbl_LvList.Items)
                {
                    if (srlv.LayoutOrViewID.ToString() == item.Value)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void ddl_ActionID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ActionID.SelectedValue != "ALL" && ddl_ActionID.SelectedValue != "" && ddl_ControlID.SelectedValue != string.Empty && ddl_ObjectID.SelectedValue != "ALL")
            {
                sysViewLayoutControl svc = _authorization.GetViewLayoutControlDetail(int.Parse(ddl_ControlID.SelectedValue));

                if (svc.LayoutOrView == 1)
                {
                    IList<Layout> layoutList = _layout.SelectDynamic(" AND TableID=" + svc.TableID);
                    cbl_LvList.DataTextField = "LayoutName";
                    cbl_LvList.DataValueField = "LayoutID";
                    cbl_LvList.DataSource = layoutList;
                    cbl_LvList.DataBind();
                }
                else
                {
                    IList<ColumnView> viewList = _columnview.SelectDynamic(" AND TableID=" + svc.TableID);
                    cbl_LvList.DataTextField = "ViewName";
                    cbl_LvList.DataValueField = "ViewID";
                    cbl_LvList.DataSource = viewList;
                    cbl_LvList.DataBind();
                }

                this.BindPurview();
            }
        }
    }
}
