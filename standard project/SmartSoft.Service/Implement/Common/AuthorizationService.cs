/***************************************************************************
 * 
 *       功能：     权限管理部分服务层接口实现类
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
namespace SmartSoft.Service.Implement.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web;

    using System.Data;
    using SmartSoft.Domain.Common;
    using SmartSoft.Domain.Purview;
    using SmartSoft.Persistence.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Persistence;

    using Castle.Facilities.AutomaticTransactionManagement;
    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    [Transactional]
    [UsesAutomaticSessionCreation]
    public class AuthorizationService : IAuthorizationService
    {
        private AuthorizationSqlMapDao _authorization;
        public AuthorizationService(AuthorizationSqlMapDao authorization)
        {
            _authorization = authorization;
        }

        #region 系统角色部分

        /// <summary>
        /// 新增系统角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns>int</returns>
        [Transaction(TransactionMode.Requires)]
        public int AddSysRole(sysRole role)
        {
            return _authorization.AddSysRole(role);
        }

        /// <summary>
        /// 修改系统角色
        /// </summary>
        /// <param name="role"></param>
        public void UpdateSysRole(sysRole role)
        {
            _authorization.UpdateSysRole(role);
        }

        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        public void DeleteSysRole(int roleId)
        {
            _authorization.DeleteSysRole(roleId);
        }

        /// <summary>
        /// 得到系统角色明细
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public sysRole GetSysRoleDetail(int roleId)
        {
            return _authorization.GetSysRoleDetail(roleId);
        }

        /// <summary>
        /// 得到系统角色列表
        /// </summary>
        /// <returns></returns>
        public IList<sysRole> GetSysRoleList()
        {
            return _authorization.GetSysRoleList();
        }

        #endregion

        #region 操作员角色关系

        /// <summary>
        /// 得到操作员所属的系统角色
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        public IList<sysOperatorInRole> GetOperatorRoles(int opeID)
        {
            return _authorization.GetOperatorRoles(opeID);
        }

        public IList<sysRole> SelectOperatorRoles(int opeId)
        {
            return _authorization.SelectOperatorRoles(opeId);
        }

        #endregion

        public IList<sysOperatorSubordinate> GetOperatorSubordinates(int opeID)
        {
            return _authorization.GetOperatorSubordinates(opeID);
        }

        #region 系统功能

        /// <summary>
        /// 根据权限码得到系统功能
        /// </summary>
        /// <param name="purviewCode">权限码</param>
        /// <returns></returns>
        public sysFunction GetSysFunctionByPurviewCode(int purviewCode)
        {
            return _authorization.GetSysFunctionByPurviewCode(purviewCode);
        }

        /// <summary>
        /// 根据功能码得到功能明细
        /// </summary>
        /// <param name="sysFunctionCode">系统功能码</param>
        /// <returns></returns>
        public sysFunction GetSysFunctionByCode(string sysFunctionCode)
        {
            return _authorization.GetSysFunctionByCode(sysFunctionCode);
        }

        /// <summary>
        /// 得到所有的系统功能列表
        /// </summary>
        /// <returns></returns>
        public IList<sysFunction> GetAllSysFunctionList()
        {
            return _authorization.GetAllSysFunctionList();
        }

        public IList<sysFunction> GetPageSysFunctionList(int FunctionCount)
        {
            return _authorization.GetPageSysFunctionList(FunctionCount);
        }

        #endregion

        #region 系统页面

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <param name="page">页面实体</param>
        /// <returns>int</returns>
        [Transaction(TransactionMode.Requires)]
        public int AddSysPage(sysPage page)
        {
            return _authorization.AddSysPage(page);
        }

        public void UpdatesysPage(sysPage syspage)
        {
            _authorization.UpdatesysPage(syspage);
        }

        public sysPage GetsysPageDetail(int PageID)
        {
            return _authorization.GetsysPageDetail(PageID);
        }

        public void DeletesysPage(int PageID)
        {
            _authorization.DeletesysPage(PageID);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxsysPageID()
        {
            return _authorization.GetMaxsysPageID();
        }
        public IList<sysPage> GetsysPageByCondition(string condition)
        {
            return _authorization.GetsysPageByCondition(condition);
        }

        public IList<sysPage> SelectsysPageForOAQuanXi(int operatorID)
        {
            return _authorization.SelectsysPageForOAQuanXi(operatorID);
        }

        public IList<sysPage> GetSysPageMenu(string objectIdString, int MenuParentPageID)
        {
            return _authorization.GetSysPageMenu(objectIdString, MenuParentPageID);
        }

        public IList<sysPage> GetSysPageToolBar(string objectIdString, int ToolBarParentPageID)
        {
            return _authorization.GetSysPageToolBar(objectIdString, ToolBarParentPageID);
        }

        #endregion

        #region 对象页面权限

        public void AddObjectPagePurview(sysObjectPurview pagePurview)
        {
            _authorization.AddObjectPagePurview(pagePurview);
        }

        public void AddObjectPagePurview(ArrayList al)
        {
            for (int i = 0; i < al.Count; i++)
            {
                sysObjectPurview pagePurview = (sysObjectPurview)al[i];

                _authorization.DeleteObjectPagePurview(pagePurview.ObjectID, pagePurview.PageID);
                _authorization.AddObjectPagePurview(pagePurview);

            }
        }

        public void DeleteObjectPagePurview(int objectId)
        {
            _authorization.DeleteObjectPagePurview(objectId);
        }

        public void DeleteObjectPagePurview(int objectId, int pageId)
        {
            _authorization.DeleteObjectPagePurview(objectId, pageId);
        }

        public sysObjectPurview GetObjectPagePurview(int objectId, int pageId)
        {
            return _authorization.GetObjectPagePurview(objectId, pageId);
        }

        public IList<sysObjectPurview> GetObjectSTRPagePurview(string objectIdString, int pageId)
        {
            return _authorization.GetObjectSTRPagePurview(objectIdString, pageId);
        }

        public IList<Menu> GetAllowMenuListByParentId(int parentId, string objectIds)
        {
            return _authorization.GetAllowMenuListByParentId(parentId, objectIds);
        }

        #region 用户验证页面权限
        /// <summary>
        /// 根据页面得到PageID，用于页面权限验证时
        /// </summary>
        /// <param name="Container"></param>
        /// <returns>PageID</returns>
        public int GetPageID(HttpContext context)
        {
            String strUrl;
            string applicationPath = context.Request.ApplicationPath;

            if (applicationPath == "/")
            {
                strUrl = context.Request.Url.AbsolutePath.Substring(1);
            }
            else
            {
                strUrl = context.Request.Url.AbsolutePath.Substring(applicationPath.Length + 1);
            }
            int pageID = _authorization.GetsysPagesByPageFilePath(strUrl);
            if (pageID > 0)
                return pageID;
            else
                return 0;
        }

        /// <summary>
        /// 根据路径得到PageID
        /// </summary>
        /// <param name="pageFilePath">页面地址</param>
        /// <returns>PageID</returns>
        public int GetsysPagesByPageFilePath(string pageFilePath)
        {
            return _authorization.GetsysPagesByPageFilePath(pageFilePath);
        }

        /// <summary>
        /// 验证用户页面权限
        /// 设置不用可用的功能选项
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="ObjectIDArray"></param>
        /// <returns></returns>
        public string CheckPurview(int pageID, string ObjectIDArray)
        {
            string sHtml = string.Empty;
            if (!StringHelper.IsValidDBInt(pageID) || ObjectIDArray == "")
                return null;

            IList<sysObjectPurview> objectPurviewList = GetObjectSTRPagePurview(ObjectIDArray, pageID);
            if (objectPurviewList.Count <= 0)
            {
                //返回到非法权限页面的javascript脚本
                sHtml += "location.replace('../@Message/PurviewError.htm');\r\n";
            }
            else
            {
                IList<sysFunction> sysFunctionList = _authorization.GetAllSysFunctionList();
                long[] functionArray = new long[sysFunctionList.Count];
                long[] noFunctionArray = new long[sysFunctionList.Count];
                long iFunctionArray = 0;
                long iNoFunctionArrary = 0;
                foreach (sysObjectPurview objectPurview in objectPurviewList)
                {
                    foreach (sysFunction func in sysFunctionList)
                    {
                        if (!inFunction(functionArray, func.FunctionPurviewCode))
                        {
                            if ((objectPurview.PurviewCode & func.FunctionPurviewCode) <= 0
                                && !inFunction(noFunctionArray, func.FunctionPurviewCode))
                            {
                                //sHtml += "设置ID为" + func.FunctionCode + "开头的控件不用可</br>";
                                sHtml += " SetNonePurview(\"" + func.FunctionCode + "\");\r\n";

                                noFunctionArray[iNoFunctionArrary] = func.FunctionPurviewCode;
                                iNoFunctionArrary++;
                            }
                            else
                            {
                                //将已验证通过的又还没有加入“通过权限码集合”的权限码加入
                                functionArray[iFunctionArray] = func.FunctionPurviewCode;
                                iFunctionArray++;
                            }
                        }
                    }
                }
            }
            return sHtml;
        }

        /// <summary>
        /// 判断权限码是否已数组中
        /// </summary>
        /// <param name="functionArray"></param>
        /// <param name="purviewCode"></param>
        /// <returns></returns>
        private bool inFunction(long[] functionArray, long purviewCode)
        {
            foreach (long ifunc in functionArray)
            {
                if (ifunc == purviewCode)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #endregion

        #region 系统参数部分


        public IList<sysParameter> GetAllSysParameter()
        {
            return _authorization.GetAllSysParameter();
        }

        [Transaction(TransactionMode.Requires)]
        public int AddSysParameter(sysParameter param)
        {
            return _authorization.AddSysParameter(param);
        }

        public void UpdateSysParameter(sysParameter param)
        {
            _authorization.UpdateSysParam(param);
        }

        public sysParameter GetSysParameter(int parameterId)
        {
            return _authorization.GetSysParameter(parameterId);
        }

        #endregion

        #region 视图布局权限设置

        [Transaction(TransactionMode.Requires)]
        public int AddRoleLayoutView(sysRoleLayoutView sysrolelayoutview)
        {
            return _authorization.AddRoleLayoutView(sysrolelayoutview);
        }

        [Transaction(TransactionMode.Requires)]
        public void AddRoleLayoutView(ArrayList al, int? roleID, int controlID, int? actionID)
        {
            this.DeleteRoleLayoutViewByRoleControlAction(roleID, controlID, actionID);
            for (int i = 0; i < al.Count; i++)
            {
                sysRoleLayoutView srlv = (sysRoleLayoutView)al[i];
                _authorization.AddRoleLayoutView(srlv);
            }
        }

        public void DeleteRoleLayoutViewByRoleControlAction(int? roleID, int controlID, int? actionID)
        {
            _authorization.DeleteRoleLayoutViewByRoleControlAction(roleID, controlID, actionID);
        }

        public IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(int roleID, int controlID, int? actionID)
        {
            return _authorization.SelectRoleLayoutViewByRoleControlAction(roleID, controlID, actionID);
        }

        public IList<sysViewLayoutControl> GetViewLayoutControlList()
        {
            return _authorization.GetViewLayoutControlList();
        }

        [Transaction(TransactionMode.Requires)]
        public int AddViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol)
        {
            return _authorization.AddViewLayoutControl(sysviewlayoutcontrol);
        }

        public void UpdateViewLayoutControl(sysViewLayoutControl sysviewlayoutcontrol)
        {
            _authorization.UpdateViewLayoutControl(sysviewlayoutcontrol);
        }

        public sysViewLayoutControl GetViewLayoutControlDetail(int ControlID)
        {
            return _authorization.GetViewLayoutControlDetail(ControlID);
        }

        public void DeleteViewLayoutControl(int ControlID)
        {
            _authorization.DeleteViewLayoutControl(ControlID);
        }

        public IList<sysViewLayoutControl> SelectViewLayoutControlByPageID(int pageId)
        {
            return _authorization.SelectViewLayoutControlByPageID(pageId);
        }

        public IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(string pageUrl, string controlName, int[] roleId, int? actionId)
        {
            sysViewLayoutControl svc = _authorization.SelectsysViewLayoutControlByPageControlName(controlName, pageUrl);

            return _authorization.SelectRoleLayoutViewByRoleControlAction(roleId, svc.ControlID, actionId);
        }

        public IList<sysRoleLayoutView> SelectRoleLayoutViewByRoleControlAction(string pageUrl, string controlName, int operatorId, int? actionId)
        {
            sysViewLayoutControl svc = _authorization.SelectsysViewLayoutControlByPageControlName(controlName, pageUrl);
            if (svc != null)
            {
                IList<sysOperatorInRole> opInRolelist = _authorization.GetOperatorRoles(operatorId);
                int[] roles = null;
                if (opInRolelist.Count > 0)
                {
                    roles = new int[opInRolelist.Count];
                    for (int i = 0; i < opInRolelist.Count; i++)
                    {
                        roles[i] = opInRolelist[i].RoleID;
                    }
                }

                return _authorization.SelectRoleLayoutViewByRoleControlAction(roles, svc.ControlID, actionId);

            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 系统设置

        /// <summary>
        /// 得到系统配置
        /// </summary>
        public IList<sysConfig> SelectsysConfigList()
        {
            return _authorization.SelectsysConfigList();
        }

        #endregion

        #region 操作员查看编辑页面权限
        public DataTable GetEditPagePurviewForCustomer(string tableName, string cusIDColumnName, string cbrBranchIDColumnName,
            string operaotrIDColumnName, string billIDColumnName, int billID)
        {

            return _authorization.GetEditPagePurviewForCustomer(tableName, cusIDColumnName, cbrBranchIDColumnName, operaotrIDColumnName, billIDColumnName, billID);
        }

        public DataTable GetEditPagePurviewForSupplier(string tableName, string supIDColumnName, string sbrBranchIDColumnName,
            string operatorIDColumnName, string billIDColumnName, int billID)
        {
            return _authorization.GetEditPagePurviewForSupplier(tableName, supIDColumnName, sbrBranchIDColumnName, operatorIDColumnName, billIDColumnName, billID);
        }
        #endregion
    }
}
