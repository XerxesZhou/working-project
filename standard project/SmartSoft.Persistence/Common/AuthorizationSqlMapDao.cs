/***************************************************************************
 * 
 *       功能：     系统授权相关持久层基类
 *       作者：     彭伟
 *       日期：     2007-1-27
 * 
 *       修改日期： 2007-02-01
 *       修改人：   彭伟
 *       修改内容： 加入系统参数部分
 * 
 *       修改日期： 2007-03-22
 *       修改人：   彭伟
 *       修改内容： 增加单据审批人对应表持久层
 * 
 *       修改日期： 2007-04-03
 *       修改人：   彭伟
 *       修改内容： 增加业务助理与客户/供应商对照关系设置
 * 
 *       修改日期： 2007-04-05
 *       修改人：   彭伟
 *       修改内容： 增加视图布局权限设置
 * 
 *       修改日期： 2007-06-04
 *       修改人：   彭伟
 *       修改内容： 增加用户配置设定
 *
 * *************************************************************************/
namespace SmartSoft.Persistence.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using IBatisNet.Common;
    using IBatisNet.DataMapper;
    using IBatisNet.Common.Exceptions;

    using SmartSoft.Domain.Common;
    using SmartSoft.Domain.Purview;

    public class AuthorizationSqlMapDao : BaseSqlMapDao
    {
        public AuthorizationSqlMapDao()
        {

        }

        #region 系统角色部分

        /// <summary>
        /// 新增系统角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns>int</returns>
        public int AddSysRole(sysRole role)
        {
            //和员工表用相同的流水号发生器
            role.RoleID = GetId("Operators");
            ExecuteInsert("InsertsysRole", role);
            return role.RoleID;
        }

        /// <summary>
        /// 得到系统角色明细
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>sysRole</returns>
        public sysRole GetSysRoleDetail(int roleId)
        {
            return ExecuteQueryForObject<sysRole>("SelectsysRole", roleId);
        }

        /// <summary>
        /// 修改系统角色
        /// </summary>
        /// <param name="role"></param>
        public void UpdateSysRole(sysRole role)
        {
            ExecuteUpdate("UpdatesysRole", role);
        }

        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        public void DeleteSysRole(int roleId)
        {
            ExecuteDelete("DeletesysRole", roleId);
        }

        /// <summary>
        /// 得到系统角色列表
        /// </summary>
        /// <returns></returns>
        public IList<sysRole> GetSysRoleList()
        {
            return ExecuteQueryForList<sysRole>("SelectsysRole", null);
        }

        #endregion

        #region 系统功能

        /// <summary>
        /// 得到所有的系统功能列表
        /// </summary>
        /// <returns></returns>
        public IList<sysFunction> GetAllSysFunctionList()
        {
            return ExecuteQueryForList<sysFunction>("SelectsysFunction", null);
        }

        /// <summary>
        /// 得到面页的系统功能列表
        /// </summary>
        /// <returns></returns>
        public IList<sysFunction> GetPageSysFunctionList(int FunctionCount)
        {
            return ExecuteQueryForList<sysFunction>("SelectPagesysFunction", FunctionCount);
        }

        /// <summary>
        /// 根据权限码得到系统功能
        /// </summary>
        /// <param name="purviewCode">权限码</param>
        /// <returns></returns>
        public sysFunction GetSysFunctionByPurviewCode(int purviewCode)
        {
            return ExecuteQueryForObject<sysFunction>("SelectsysFunctionByPurviewCode",purviewCode);
        }

        /// <summary>
        /// 根据功能代码得到功能明细
        /// </summary>
        /// <param name="sysFunctionCode">系统功能代码</param>
        /// <returns></returns>
        public sysFunction GetSysFunctionByCode(string sysFunctionCode)
        {
            return ExecuteQueryForObject<sysFunction>("SelectsysFunction", sysFunctionCode);
        }

        #endregion

        #region 系统页面

        /// <summary>
        /// 新增系统页面配置
        /// </summary>
        /// <param name="page"></param>
        /// <returns>int</returns>
        public int AddSysPage(sysPage page)
        {
            ExecuteInsert("InsertsysPage", page);
            return page.PageID;
        }

        /// <summary>
        /// 根据条件得到系统页面列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        public IList<sysPage> GetsysPageByCondition(string condition)
        {
            string sql = " pageid, pagename, pagefilepath, menuparentpageid, menuorderby, menuimagepath," +
                        "menuselectedimagepath, toolbarparentpageid, toolorderby, toolbarimagepath, toolbarselectedimagepath," +
                        "ismenudirectory, istoolbardirectory, ismenu, istoolbar, functioncount, '' as ParentPageName, exefilepath, formname,PageNo " +
                        " From sysPage";
            if (condition != string.Empty)
            {
                sql += " Where " + condition;
            }
            //sql += " Order By MenuOrderBy, ToolOrderBy";
            //throw new ApplicationException(sql);
            return ExecuteQueryForList<sysPage>("GetsysPageByCondition", sql);
        }

        /// <summary>
        /// 修改系统页面配置
        /// </summary>
        public void UpdatesysPage(sysPage syspage)
        {
            ExecuteUpdate("UpdatesysPage", syspage);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public sysPage GetsysPageDetail(int PageID)
        {
            return ExecuteQueryForObject<sysPage>("SelectsysPage", PageID);
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxsysPageID()
        {
            return ExecuteQueryForObject<int>("GetMaxsysPageID", "");
        }
        /// <summary>
        /// 删除系统页面
        /// </summary>
        /// <param name="id">页面ID</param>
        public void DeletesysPage(int PageID)
        {
            ExecuteDelete("DeletesysPage", PageID);
        }

        /// <summary>
        /// 得到有权限查看的菜单列表
        /// </summary>
        /// <param name="ObjectIDArray">权限对象集，以“,”隔开</param>
        /// <returns></returns>
        public IList<sysPage> GetSysPageMenu(string ObjectIDArray, int MenuParentPageID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ObjectIDArray", ObjectIDArray);
            ht.Add("MenuParentPageID", MenuParentPageID);

            return ExecuteQueryForList<sysPage>("SelectMenusysPage", ht);
        }


        public IList<sysPage> SelectsysPageForOAQuanXi(int OperatorID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("OperatorID", OperatorID);
            return ExecuteQueryForList<sysPage>("SelectsysPageForOAQuanXi", ht);
        }

        /// <summary>
        /// 得到有权限查看的工具栏列表
        /// </summary>
        /// <param name="ObjectIDArray">权限对象集，以“,”隔开</param>
        /// <returns></returns>
        public IList<sysPage> GetSysPageToolBar(string ObjectIDArray, int ToolBarParentPageID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ObjectIDArray", ObjectIDArray);
            ht.Add("ToolBarParentPageID", ToolBarParentPageID);

            return ExecuteQueryForList<sysPage>("SelectToolBarsysPage", ht);
        }

        public int GetsysPagesByPageFilePath(string pageFilePath)
        {
            return ExecuteQueryForObject<int>("SelectsysPageIDByPageFilePath", pageFilePath);
        }

        #endregion

        #region 系统角色与操作员关系

        /// <summary>
        /// 新增角色操作员关系
        /// </summary>
        /// <param name="oir"></param>
        public void AddOperatorInRole(sysOperatorInRole oir)
        {
            ExecuteInsert("InsertsysOperatorInRole", oir);
        }

        /// <summary>
        /// 删除某操作员与角色的关系
        /// </summary>
        /// <param name="operatorId">操作员ID</param>
        public void DeleteOperatorInRole(int operatorId)
        {
            ExecuteDelete("DeletesysOperatorInRole", operatorId);
        }

        /// <summary>
        /// 得到操作员所属的角色
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        public IList<sysOperatorInRole> GetOperatorRoles(int opeID)
        {
            return ExecuteQueryForList<sysOperatorInRole>("SelectsysOperatorInRole", opeID);
        }

        public IList<sysRole> SelectOperatorRoles(int opeId)
        {
            return ExecuteQueryForList<sysRole>("SelectOperatorRoles", opeId);
        }

        public IList<sysOperatorInRole> SelectManager()
        {
            return ExecuteQueryForList<sysOperatorInRole>("SelectManager","");
        }
        #endregion

        #region 操作员下属
        public void AddOperatorSubordinate(sysOperatorSubordinate oir)
        {
            ExecuteInsert("InsertsysOperatorSubordinate", oir);
        }

        public void DeleteOperatorSubordinates(int operatorId)
        {
            ExecuteDelete("DeletesysOperatorSubordinates", operatorId);
        }

        public IList<sysOperatorSubordinate> GetOperatorSubordinates(int opeID)
        {
            return ExecuteQueryForList<sysOperatorSubordinate>("SelectsysOperatorSubordinates", opeID);
        }
        #endregion

        #region 对象页面权限

        /// <summary>
        /// 增加对象页面权限
        /// </summary>
        /// <param name="pagePurview"></param>
        public void AddObjectPagePurview(sysObjectPurview pagePurview)
        {
            ExecuteInsert("InsertsysObjectPurview", pagePurview);
        }

        /// <summary>
        /// 删除对象在所有页面的权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        public void DeleteObjectPagePurview(int objectId)
        {
            ExecuteDelete("DeletesysObjectPurview", objectId);
        }

        /// <summary>
        /// 删除对象页面权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        /// <param name="pageId">页面ID</param>
        public void DeleteObjectPagePurview(int objectId, int pageId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ObjectID", objectId);
            ht.Add("PageID", pageId);
            ExecuteDelete("DeletesysObjectPagePurview", ht);
        }

        /// <summary>
        /// 得到单个对象在某页面的权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        public sysObjectPurview GetObjectPagePurview(int objectId, int pageId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ObjectID", objectId);
            ht.Add("PageID", pageId);
            return ExecuteQueryForObject<sysObjectPurview>("SelectsysObjectPagePurview", ht);
        }

        /// <summary>
        /// 得到一组对象在某页面的权限
        /// </summary>
        /// <param name="objectIdString">对象ID组成的字符串，调用该方法时注意传入该值的格式</param>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        public IList<sysObjectPurview> GetObjectSTRPagePurview(string objectIdString, int pageId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("objectIdString", objectIdString);
            ht.Add("PageID", pageId);
            return ExecuteQueryForList<sysObjectPurview>("SelectsysObjectSTRPagePurview", ht);
        }
        #endregion

        #region 系统参数

        /// <summary>
        /// 得到系统参数
        /// </summary>
        /// <returns></returns>
        public IList<sysParameter> GetAllSysParameter()
        {
            return ExecuteQueryForList<sysParameter>("SelectsysParameter", null);
        }

        /// <summary>
        /// 新增系统参数
        /// </summary>
        /// <param name="param">系统参数</param>
        /// <returns></returns>
        public int AddSysParameter(sysParameter param)
        {
            param.ParameterID = GetId("sysParameter");
            ExecuteInsert("InsertsysParameter", param);
            return param.ParameterID;
        }

        /// <summary>
        /// 修改系统参数
        /// </summary>
        /// <param name="param"></param>
        public void UpdateSysParam(sysParameter param)
        {
            ExecuteUpdate("UpdatesysParameter", param);
        }

        /// <summary>
        /// 根据Id得到系统参数
        /// </summary>
        /// <param name="parameterId">参数Id</param>
        /// <returns></returns>
        public sysParameter GetSysParameter(int parameterId)
        {
            return ExecuteQueryForObject<sysParameter>("SelectsysParameter", parameterId);
        }

        #endregion

        #region 菜单工具栏

        /// <summary>
        /// 得到菜单列表
        /// </summary>
        /// <param name="menuParentId">父菜单Id</param>
        public IList<Menu> GetMenuListByParentId(int menuParentId)
        {
            return ExecuteQueryForList<Menu>("SelectMenuListByParentId", menuParentId);
        }

        /// <summary>
        /// 新建菜单
        /// </summary>
        public int AddMenu(Menu menu)
        {
            int id = GetId("Menu");
            menu.MenuId = id;

            ExecuteInsert("InsertMenu", menu);
            return id;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        public void UpdateMenu(Menu menu)
        {
            ExecuteUpdate("UpdateMenu", menu);
        }

        /// <summary>
        /// 得到菜单明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu GetMenuDetail(int MenuId)
        {
            return ExecuteQueryForObject<Menu>("MenuDetail", MenuId);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="MenuId"></param>
        public void DeleteMenu(int MenuId)
        {
            ExecuteDelete("DeleteMenu", MenuId);
        }

        /// <summary>
        /// 得到工具栏列表
        /// </summary>
        public IList<Toolbar> SelectToolbarListByParentId(int toolbarParentId)
        {
            return ExecuteQueryForList<Toolbar>("SelectToolbarListByParentId", toolbarParentId);
        }

        /// <summary>
        /// 新建工具栏项
        /// </summary>
        public int AddToolbar(Toolbar toolbar)
        {
            int id = GetId("Toolbar");
            toolbar.ToolbarId = id;

            ExecuteInsert("InsertToolbar", toolbar);

            return id;
        }
        /// <summary>
        /// 修改工具栏
        /// </summary>
        public void UpdateToolbar(Toolbar toolbar)
        {
            ExecuteUpdate("UpdateToolbar", toolbar);
        }

        /// <summary>
        /// 得到工具栏明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Toolbar GetToolbarDetail(int toolbarId)
        {
            return ExecuteQueryForObject<Toolbar>("ToolbarDetail", toolbarId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteToolbar(int toolbarId)
        {
            ExecuteDelete("DeleteToolbar", toolbarId);
        }

        #endregion

        #region 菜单工具栏授权

        /// <summary>
        /// 新增菜单授权
        /// </summary>
        /// <param name="mp"></param>
        public void AddMenuPurview(MenuPurview mp)
        {
            ExecuteInsert("InsertMenuPurview", mp);
        }

        /// <summary>
        /// 删除菜单授权
        /// </summary>
        /// <param name="objectId">对象Id</param>
        public void DeleteMenuPurview(int objectId)
        {
            ExecuteDelete("DeleteMenuPurview", objectId);
        }

        /// <summary>
        /// 得到可以查看的菜单
        /// </summary>
        /// <param name="parentId">菜单父菜单Id</param>
        /// <param name="objectIds">对象Id字符串</param>
        /// <returns></returns>
        public IList<Menu> GetAllowMenuListByParentId(int parentId, string objectIds)
        {
            IList objParameter = (new KeyWordSearch(objectIds)).KeywordList;
            Hashtable ht = new Hashtable();
            ht.Add("MenuParentId", parentId);
            ht.Add("KeywordList", objParameter);

            return ExecuteQueryForList<Menu>("SelectAllowMenuListByParentId", parentId);
            
        }

        #endregion

        #region 视图布局权限设置

        /// <summary>
        /// 新增角色与布局视图关系表
        /// </summary>
        public int AddRoleLayoutView(sysRoleLayoutView sysrolelayoutview)
        {
            int id = GetId("sysRoleLayoutView");
            sysrolelayoutview.rlvID = id;
            ExecuteInsert("InsertsysRoleLayoutView", sysrolelayoutview);
            return id;
        }

        /// <summary>
        /// 删除角色与布局视图关系表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="controlID">控件ID</param>
        /// <param name="actionID">动作ID</param>
        public void DeleteRoleLayoutViewByRoleControlAction(int roleID,int controlID,int? actionID, int layoutviewID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ObjectID", roleID);
            ht.Add("ControlID", controlID);
            if (actionID.HasValue)
            {
                ht.Add("ActionID", actionID);
            }
            ht.Add("LayoutOrViewID", layoutviewID);
            ExecuteDelete("DeletesysRoleLayoutViewByRoleControlAction", ht);
        }

        public void DeleteRoleLayoutViewByRoleControlAction(int? roleID, int controlID, int? actionID)
        {
            Hashtable ht = new Hashtable();
            if (roleID.HasValue)
            {
                ht.Add("ObjectID", roleID);
            }
            ht.Add("ControlID", controlID);
            if (actionID.HasValue)
            {
                ht.Add("ActionID", actionID);
            }

            ExecuteDelete("DeletesysRoleLayoutViewByRoleControlAction", ht);
        }

        /// <summary>
        /// 根据角色数组控件动作得到相应的布局或视图列表
        /// </summary>
        /// <param name="roleIDs"></param>
        /// <param name="controlID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(int[] roleIDs, int controlID, int? actionID)
        {
            Hashtable ht = new Hashtable();
            if (roleIDs == null || roleIDs.Length <= 0)
            {
                roleIDs = new int[1];
                roleIDs[0] = -1;
            }

            ht.Add("ObjectIDs", roleIDs);
            ht.Add("ControlID", controlID);
            if (actionID.HasValue)
            {
                ht.Add("ActionID", actionID);
            }

            return ExecuteQueryForList<sysRoleLayoutView>("SelectsysRoleLayoutViewByRolesControlAction", ht);
        }

        /// <summary>
        /// 根据角色控件动作得到相应的布局或视图列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="controlID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(int roleID, int controlID, int? actionID)
        {
            Hashtable ht = new Hashtable();

            ht.Add("ObjectID", roleID);
            ht.Add("ControlID", controlID);
            if (actionID.HasValue)
            {
                ht.Add("ActionID", actionID);
            }

            return ExecuteQueryForList<sysRoleLayoutView>("SelectsysRoleLayoutViewByRoleControlAction", ht);
        }

        /// <summary>
        /// 得到布局视图控件名列表
        /// </summary>
        public IList<sysViewLayoutControl> GetViewLayoutControlList()
        {
            return ExecuteQueryForList<sysViewLayoutControl>("SelectsysViewLayoutControl", null);
        }

        /// <summary>
        /// 新增布局视图控件名
        /// </summary>
        public int AddViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol)
        {
            int id = GetId("sysViewLayoutControl");
            sysviewlayoutcontrol.ControlID = id;
            ExecuteInsert("InsertsysViewLayoutControl", sysviewlayoutcontrol);
            return id;
        }
        /// <summary>
        /// 修改布局视图控件名
        /// </summary>
        public void UpdateViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol)
        {
            ExecuteUpdate("UpdatesysViewLayoutControl", sysviewlayoutcontrol);
        }

        /// <summary>
        /// 得到布局视图控件名明细
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public sysViewLayoutControl GetViewLayoutControlDetail(int ControlID)
        {
            return ExecuteQueryForObject<sysViewLayoutControl>("SelectsysViewLayoutControl", ControlID);
        }

        /// <summary>
        /// 删除布局视图控件名
        /// </summary>
        /// <param name=""></param>
        public void DeleteViewLayoutControl(int ControlID)
        {
            ExecuteDelete("DeletesysViewLayoutControl", ControlID);
        }

        /// <summary>
        /// 得到页面的布局视图控件
        /// </summary>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        public IList<sysViewLayoutControl> SelectViewLayoutControlByPageID(int pageId)
        {
            return ExecuteQueryForList<sysViewLayoutControl>("SelectsysViewLayoutControlByPageID", pageId);
        }

        /// <summary>
        /// 根据页面路径和控件名称得到控件的ＩＤ
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public sysViewLayoutControl SelectsysViewLayoutControlByPageControlName(string controlName, string pageUrl)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ControlName", controlName);
            ht.Add("PageFilePath", pageUrl);

            return ExecuteQueryForObject<sysViewLayoutControl>("SelectsysViewLayoutControlByPageControlName", ht);
        }

        #endregion

        #region 系统设置

        /// <summary>
        /// 得到系统配置
        /// </summary>
        public IList<sysConfig> SelectsysConfigList()
        {
            return ExecuteQueryForList<sysConfig>("SelectsysConfig", null);
        }

        #endregion

        #region 操作员查看编辑页面权限
        public DataTable GetEditPagePurviewForCustomer(string tableName, string cusIDColumnName, string cbrBranchIDColumnName,
            string operaotrIDColumnName, string billIDColumnName, int billID)
        {

            Hashtable htOutputParameter = new Hashtable();
            Hashtable ht = new Hashtable();
            ht["tableName"] = tableName;
            ht["cusIDColumnName"] = cusIDColumnName;
            ht["cbrBranchIDColumnName"] = cbrBranchIDColumnName;
            ht["operatorIDColumnName"] = operaotrIDColumnName;
            ht["billIDColumnName"] = billIDColumnName;
            ht["billID"] = billID;

            DataTable dt = ExecuteQueryForDataTable("GetEditPagePurviewForCustomer", ht, htOutputParameter);

            return dt;
        }

        public DataTable GetEditPagePurviewForSupplier(string tableName, string supIDColumnName, string sbrBranchIDColumnName,
            string operatorIDColumnName, string billIDColumnName, int billID)
        {

            Hashtable htOutputParameter = new Hashtable();

            Hashtable ht = new Hashtable();
            ht["tableName"] = tableName;
            ht["supIDColumnName"] = supIDColumnName;
            ht["sbrBranchIDColumnName"] = sbrBranchIDColumnName;
            ht["operatorIDColumnName"] = operatorIDColumnName;
            ht["billIDColumnName"] = billIDColumnName;
            ht["billID"] = billID;

            DataTable dt = ExecuteQueryForDataTable("GetEditPagePurviewForSupplier", ht, htOutputParameter);

            return dt;
        }
        #endregion

    }
}
