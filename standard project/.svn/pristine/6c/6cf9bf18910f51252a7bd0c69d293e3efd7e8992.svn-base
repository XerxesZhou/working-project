/***************************************************************************
 * 
 *       功能：     组织机构服务层接口
 *       作者：     彭伟
 *       日期：     2007-1-26
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Interface.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using SmartSoft.Domain.Common;

    public interface IOrganizationService
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        int AddDepartment(Department department);

        /// <summary>
        /// 得到部门信息明细
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <returns></returns>
        Department GetDepartmentDetail(int depID);

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        void UpdateDepartment(Department department);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <returns></returns>
        void DeleteDepartment(int depID);

        IList<Department> GetDepartmentList();

        /// <summary>
        /// 根据已用标志得到部门列表
        /// </summary>
        /// <param name="usetag">已用标志</param>
        /// <returns>IList</returns>
        IList<Department> GetDepartmentListByUsetag(bool usetag);

        /// <summary>
        /// 根据停用标志得到部门列表
        /// </summary>
        /// <param name="stoptag">停用标志</param>
        /// <returns>IList</returns>
        IList<Department> GetDepartmentListByStoptag(bool stoptag);

        /// <summary>
        /// 设置部门已用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="usetag">已用标志</param>
        /// <returns></returns>
        void SetDepartmentUsetag(int depID, bool usetag);

        /// <summary>
        /// 设置部门停用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="stoptag">停用标志</param>
        /// <returns></returns>
        void SetDepartmentStoptag(int depID, bool stoptag);


        /// <summary>
        /// 得到员工列表
        /// </summary>
        /// <returns></returns>
        IList<Operators> GetOperatorsList();

        int AddOperators(Operators operators, ArrayList al_role, ArrayList al_subordinate);


        /// <summary>
        /// 修改
        /// </summary>
        void UpdateOperators(Operators operators);

        void UpdateOperators(Operators operators, ArrayList al_role, ArrayList al_subordinate);

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Operators GetOperatorsDetail(int opeID);

        /// <summary>
        /// 删除员工，其实就是停用
        /// </summary>
        /// <param name="opeID"></param>
        void DeleteOperators(int opeID);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="opeID">员工ID</param>
        /// <param name="oldpassword">原密码</param>
        /// <param name="newpassword">新密码</param>
        void UpdatePassword(int opeID, string newpassword, string oldpassword);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool OperatorLogIn(string username, string password, int userCount, ref Operators op);

        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool CheckUserName(string username);

        /// <summary>
        /// 根据用户名得到员工明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        IList<Operators> GetOperatorListByName(string username);

        /// <summary>
        /// 得到操作员的ID集合，包括部门ID，角色ID
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        string GetOperatorHasIDs(int opeID);

        /// <summary>
        /// 得到操作员的OA权限字符串
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        string GetOperatorOAQuanXi(int opeID);

        /// <summary>
        /// 得到序列号
        /// </summary>
        /// <returns></returns>
        IList<Operators> SelectSerialNumber(string sn);

        /// <summary>
        /// 根据用户名密码得到操作员ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int GetOperatorID(string username, string password);
    }
}
