/***************************************************************************
 * 
 *       功能：     页面信息录入/修改
 *       作者：     肖秋李
 *       日期：     2007/01/28
 * 
 *       修改日期： 2007/01/29
 *       修改人：   彭伟
 *       修改内容： 1、加入页面功能复选框操作
 *                  2、修改绑定ListControl方法
 * 
 * *************************************************************************/

namespace SmartSoft.Web.Common
{
    using System;
    using System.Drawing;
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
    using System.Text.RegularExpressions;

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;


    public partial class SysPageEdit : WebForm<sysPage>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
                if (Request.QueryString["PageID"] == string.Empty || Request.QueryString["PageID"] == null)
                {
                   tb_PageID.Text = (_authorization.GetMaxsysPageID() + 1).ToString();
                }
                bindPage();
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PageID"] != string.Empty && Request.QueryString["PageID"] != null)
            {
                sysPage page = this.FormData;
                page.FunctionCount = this.GetSelectedFunctionCount();

                if (Request.QueryString["PageID"] != string.Empty && Request.QueryString["PageID"] != null)
                {
                    //修改
                    _authorization.UpdatesysPage(page);
                }
                else
                {
                    //新增
                    _authorization.AddSysPage(page);
                }

                //保存成功
                string strScripts = "<script language='javascript'>alert('保存成功！');window.close();</script>";
                WebFunc.ExecuteJScript(strScripts, this.Page);
           
            }
            else
            {
                if (this.tb_PageID.Text != null && this.tb_PageID.Text != string.Empty)
                {
                    sysPage sp = _authorization.GetsysPageDetail(int.Parse(this.tb_PageID.Text));
                    if (sp == null)
                    {
                        sysPage page = this.FormData;
                        page.FunctionCount = this.GetSelectedFunctionCount();

                        if (Request.QueryString["PageID"] != string.Empty && Request.QueryString["PageID"] != null)
                        {
                            //修改
                            _authorization.UpdatesysPage(page);
                        }
                        else
                        {
                            //新增
                            _authorization.AddSysPage(page);
                        }

                        //保存成功
                        string strScripts = "<script language='javascript'>alert('保存成功！');window.close();</script>";
                        WebFunc.ExecuteJScript(strScripts, this.Page);
                    }
                    else
                    {
                        WebFunc.Alert("系统中已存在ID:" + this.tb_PageID.Text + "保存失败！", Page);
                    }
                }
            }
            
        }

        protected void bt_CheckPageId_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Method

        private void initPage()
        {
            BindPageFunctionCheckBoxList();
            BindControls();
            
            if (Request.QueryString["PageID"] != string.Empty && Request.QueryString["PageID"] != null)
            {
                lt_title.Text = "编辑页面信息";
                tb_PageID.ReadOnly = true;
            }
            else
            {
                lt_title.Text = "录入页面信息";
                if (Request.QueryString["parentId"] != string.Empty && Request.QueryString["parentId"] != null)
                {
                    int parentId = int.Parse(Request.QueryString["parentId"]);
                    sysPage page = _authorization.GetsysPageDetail(parentId);
                    if (page.IsMenuDirectory == true)
                    {
                        this.ddl_MenuParentPageID.SelectedValue = parentId.ToString();
                    }
                }
            }
        }

        private void bindPage()
        {
            if (Request.QueryString["PageID"] != string.Empty && Request.QueryString["PageID"] != null)
            {
                int pageID = int.Parse(Request.QueryString["PageID"]);
                sysPage page = _authorization.GetsysPageDetail(pageID);

                this.FormData = page;
                this.BindCheckBoxListByPurviewCode(page.FunctionCount);
            }
        }

        private void BindControls()
        {
            ListItem defaultItem = new ListItem("请选择...","");
            ddl_MenuParentPageID.Items.Add(defaultItem);

            //得到顶级节点
            string strWhere = string.Empty;
            string menuReplaceOperator = string.Empty;
            string toolbarReplaceOperator = string.Empty;
            
            #region 菜单部分
            //绑定菜单顶级节点
            strWhere = " menuparentpageid=0 and ismenudirectory='1' and ismenu='1'";

            IList<sysPage> MenuPageList = _authorization.GetsysPageByCondition(strWhere);
            foreach (sysPage page in MenuPageList)
            {
                ListItem item = new ListItem(page.PageName,page.PageID.ToString());
                ddl_MenuParentPageID.Items.Add(item);
                this.BindMenuChild(page.PageID, menuReplaceOperator);
            }

            #endregion
        }

        private void BindMenuChild(int parentPageId, string menuReplaceOperator)
        {
            string strWhere = "menuparentpageid=" + parentPageId + " and ismenudirectory='1' and ismenu='1'";
            menuReplaceOperator += "--";

            IList<sysPage> MenuPageList = _authorization.GetsysPageByCondition(strWhere);
            foreach (sysPage page in MenuPageList)
            {
                ListItem item = new ListItem(menuReplaceOperator + page.PageName, page.PageID.ToString());
                ddl_MenuParentPageID.Items.Add(item);

                this.BindMenuChild(page.PageID, menuReplaceOperator);
            }
        }

        /// <summary>
        /// 绑定系统功能
        /// </summary>
        private void BindPageFunctionCheckBoxList()
        {
            //得到系统定义的所有功能绑定
            IList<sysFunction> sysfunctionlist = _authorization.GetAllSysFunctionList();
            //绑定功能名称和权限码
            (new WebFunc()).BindListControl<sysFunction>(sysfunctionlist, cbl_PageFunction, "FunctionName", "FunctionPurviewCode", "", "");
        }

        /// <summary>
        /// 得到选择的系统功能的功能码之和
        /// </summary>
        /// <returns></returns>
        private long GetSelectedFunctionCount()
        {
            long Count = 0;
            foreach (ListItem item in cbl_PageFunction.Items)
            {
                if (item.Selected == true)
                {
                    Count += long.Parse(item.Value);
                }
            }
            return Count;
        }

        /// <summary>
        /// 根据权限码绑定系统功能复选框
        /// </summary>
        /// <param name="functionCount"></param>
        private void BindCheckBoxListByPurviewCode(long functionCount)
        {
            foreach (ListItem item in cbl_PageFunction.Items)
            {
                if ((functionCount & long.Parse(item.Value)) > 0)
                {
                    item.Selected = true;
                }
            }
        }

        #endregion
    }
}
