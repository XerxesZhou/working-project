/***************************************************************************
 * 
 *       功能：     权限管理部分服务层接口
 *       作者：     彭伟
 *       日期：     2007-1-27
 * 
 *       修改日期： 2007-02-01
 *       修改人：   彭伟
 *       修改内容： 加入系统参数部分
 * 
 *       修改日期： 2007-03
 *       修改人：   彭伟
 *       修改内容： 加入审批权限
 *                  业务助理与供应商/客户对照关系设置
 * 
 *       修改日期： 2007-04-05
 *       修改人：   彭伟
 *       修改内容： 加入视图布局权限设置
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Interface.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web;
    using SmartSoft.Domain.Common;
    using SmartSoft.Domain.Purview;

    public interface IAuthorizationService
    {
        #region 系统角色部分

        /// <summary>
        /// 新增系统角色
        /// </summary>
        /// <param name="role"></param>
        int AddSysRole(sysRole role);

        /// <summary>
        /// 修改系统角色
        /// </summary>
        /// <param name="role"></param>
        void UpdateSysRole(sysRole role);

        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        void DeleteSysRole(int roleId);

        /// <summary>
        /// 得到系统角色明细
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>sysRole</returns>
        sysRole GetSysRoleDetail(int roleId);

        /// <summary>
        /// 得到系统角色列表
        /// </summary>
        /// <returns>IList</returns>
        IList<sysRole> GetSysRoleList();

        #endregion

        #region 操作员角色关系

        /// <summary>
        /// 得到操作员所属的系统角色
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        IList<sysOperatorInRole> GetOperatorRoles(int opeID);

        IList<sysRole> SelectOperatorRoles(int opeId);

        #endregion

        #region 操作员下属
        IList<sysOperatorSubordinate> GetOperatorSubordinates(int opeID);
        #endregion

        #region 系统功能
        /// <summary>
        /// 根据权限码得到系统功能
        /// </summary>
        /// <param name="purviewCode">权限码</param>
        /// <returns></returns>
        sysFunction GetSysFunctionByPurviewCode(int purviewCode);

        /// <summary>
        /// 根据功能代码得到功能明细
        /// </summary>
        /// <param name="sysFunctionCode">系统功能代码</param>
        /// <returns></returns>
        sysFunction GetSysFunctionByCode(string sysFunctionCode);

        /// <summary>
        /// 得到所有的系统功能列表
        /// </summary>
        /// <returns></returns>
        IList<sysFunction> GetAllSysFunctionList();

        /// <summary>
        /// 得到页面的功能列表
        /// </summary>
        /// <param name="sysFunctionCount">功能码的总和</param>
        /// <returns></returns>
        IList<sysFunction> GetPageSysFunctionList(int FunctionCount);
        #endregion

        #region 系统页面

        /// <summary>
        /// 新增系统页面配置
        /// </summary>
        /// <param name="page"></param>
        int AddSysPage(sysPage page);

        /// <summary>
        /// 修改系统页面配置
        /// </summary>
        void UpdatesysPage(sysPage syspage);

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        sysPage GetsysPageDetail(int PageID);

        /// <summary>
        /// 得到最大ID
        /// </summary>
        /// <returns></returns>
        int GetMaxsysPageID();

        /// <summary>
        /// 删除系统页面,如果已设置权限则不能删除
        /// </summary>
        /// <param name="id">页面ID</param>
        void DeletesysPage(int PageID);

        /// <summary>
        /// 根据条件得到系统页面列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        IList<sysPage> GetsysPageByCondition(string condition);

        IList<sysPage> SelectsysPageForOAQuanXi(int operatorID);

        /// <summary>
        /// 得到可查看的并排序的菜单列表
        /// </summary>
        /// <param name="objectIdString">对象ID组成的字符串，调用该方法时注意传入该值的格式</param>
        /// <returns></returns>
        IList<sysPage> GetSysPageMenu(string objectIdString, int MenuParentPageID);

        /// <summary>
        /// 得到可查看的并排序的工具栏列表
        /// </summary>
        /// <param name="objectIdString">对象ID组成的字符串，调用该方法时注意传入该值的格式</param>
        /// <returns></returns>
        IList<sysPage> GetSysPageToolBar(string objectIdString, int ToolBarParentPageID);

        int GetPageID(HttpContext context);

        int GetsysPagesByPageFilePath(string pageFilePath);

        #endregion

        #region 对象页面权限

        /// <summary>
        /// 增加对象页面权限
        /// </summary>
        /// <param name="pagePurview"></param>
        void AddObjectPagePurview(sysObjectPurview pagePurview);
        void AddObjectPagePurview(ArrayList al);

        /// <summary>
        /// 删除对象在所有页面的权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        void DeleteObjectPagePurview(int objectId);

        /// <summary>
        /// 删除对象页面权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        /// <param name="pageId">页面ID</param>
        void DeleteObjectPagePurview(int objectId, int pageId);

        /// <summary>
        /// 得到单个对象在某页面的权限
        /// </summary>
        /// <param name="objectId">对象ID</param>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        sysObjectPurview GetObjectPagePurview(int objectId, int pageId);

        /// <summary>
        /// 得到一组对象在某页面的权限
        /// </summary>
        /// <param name="objectIdString">对象ID组成的字符串，调用该方法时注意传入该值的格式</param>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        IList<sysObjectPurview> GetObjectSTRPagePurview(string objectIdString, int pageId);

        /// <summary>
        /// 根据对象ID集和PageID验证用户权限并返回String脚本
        /// </summary>
        /// <param name="pageID">页面ID</param>
        /// <param name="ObjectIDArray">对象ID集</param>
        /// <returns>脚本</returns>
        string CheckPurview(int pageID, string ObjectIDArray);

        IList<Menu> GetAllowMenuListByParentId(int parentId, string objectIds);

        #endregion

        #region 系统参数

        IList<sysParameter> GetAllSysParameter();

        int AddSysParameter(sysParameter param);

        void UpdateSysParameter(sysParameter param);

        /// <summary>
        /// 根据Id得到系统参数
        /// </summary>
        /// <param name="parameterId">参数Id</param>
        /// <returns></returns>
        sysParameter GetSysParameter(int parameterId);

        #endregion

        #region 视图布局权限设置

        /// <summary>
        /// 新增角色与布局视图关系表
        /// </summary>
        int AddRoleLayoutView(sysRoleLayoutView sysrolelayoutview);

        void AddRoleLayoutView(ArrayList al, int? roleID, int controlID, int? actionID);

        /// <summary>
        /// 删除角色与布局视图关系表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="controlID">控件ID</param>
        /// <param name="actionID">动作ID</param>
        /// <param name="layoutviewID">布局或视图ID</param>
        void DeleteRoleLayoutViewByRoleControlAction(int? roleID, int controlID, int? actionID);

        /// <summary>
        /// 根据角色控件动作得到相应的布局或视图列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="controlID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(int roleID, int controlID, int? actionID);
        IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(string pageUrl, string controlName, int[] roleId, int? actionId);
        IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(string pageUrl, string controlName, int operatorId, int? actionId);



        /// <summary>
        /// 得到布局视图控件名列表
        /// </summary>
        IList<sysViewLayoutControl> GetViewLayoutControlList();

        /// <summary>
        /// 新增布局视图控件名
        /// </summary>
        int AddViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol);

        /// <summary>
        /// 修改布局视图控件名
        /// </summary>
        void UpdateViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol);

        /// <summary>
        /// 得到布局视图控件名明细
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        sysViewLayoutControl GetViewLayoutControlDetail(int ControlID);

        /// <summary>
        /// 删除布局视图控件名
        /// </summary>
        /// <param name=""></param>
        void DeleteViewLayoutControl(int ControlID);

        /// <summary>
        /// 得到页面的布局视图控件
        /// </summary>
        /// <param name="pageId">页面ID</param>
        /// <returns></returns>
        IList<sysViewLayoutControl> SelectViewLayoutControlByPageID(int pageId);

        #endregion

        #region 系统设置

        /// <summary>
        /// 得到系统配置
        /// </summary>
        IList<sysConfig> SelectsysConfigList();

        #endregion

        #region 操作员查看编辑页面权限
        DataTable GetEditPagePurviewForCustomer(string tableName, string cusIDColumnName, string cbrBranchIDColumnName,
            string operaotrIDColumnName, string billIDColumnName, int billID);

        DataTable GetEditPagePurviewForSupplier(string tableName, string supIDColumnName, string sbrBranchIDColumnName,
            string operatorIDColumnName, string billIDColumnName, int billID);
        #endregion

    }
}
