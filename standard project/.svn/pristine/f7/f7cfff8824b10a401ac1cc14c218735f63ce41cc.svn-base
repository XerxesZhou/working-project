/***************************************************************************
 * 
 *       功能：     组织机构服务层接口实现类
 *       作者：     彭伟
 *       日期：     2007-1-26
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Implement.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using SmartSoft.Domain.Common;
    
    using SmartSoft.Persistence.Common;
    using SmartSoft.Service.Interface.Common;


    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    [Transactional]
    public class OrganizationService : IOrganizationService
    {
        private OrganizationSqlMapDao _org;
        private AuthorizationSqlMapDao _authorization;

        public OrganizationService(OrganizationSqlMapDao ORG,AuthorizationSqlMapDao authorization)
        {
            _org = ORG;
            _authorization = authorization;
        }

       
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="department"></param>
        public int AddDepartment(Department department)
        {
            return _org.AddDepartment(department);
        }

        /// <summary>
        /// 得到部门明细
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <returns></returns>
        public Department GetDepartmentDetail(int depID)
        {
            return _org.GetDepartmentDetail(depID);
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="department"></param>
        public void UpdateDepartment(Department department)
        {
            _org.UpdateDepartment(department);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="depID"></param>
        public void DeleteDepartment(int depID)
        {
            _org.DeleteDepartment(depID);
        }


        public IList<Department> GetDepartmentList()
        {
            return _org.GetDepartmentList();
        }

        /// <summary>
        /// 根据已用标志得到部门列表
        /// </summary>
        /// <param name="usetag"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentListByUsetag(bool usetag)
        {
            return _org.GetDepartmentListByUsetag(usetag);
        }

        /// <summary>
        /// 根据停用标志得到部门列表
        /// </summary>
        /// <param name="stoptag"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentListByStoptag(bool stoptag)
        {
            return _org.GetDepartmentListByStoptag(stoptag);
        }

        /// <summary>
        /// 设置部门已用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="usetag">已用标志</param>
        public void SetDepartmentUsetag(int depID, bool usetag)
        {
            _org.SetDepartmentUsetag(depID, usetag);
        }

        /// <summary>
        /// 设置部门停用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="stoptag">停用标志</param>
        public void SetDepartmentStoptag(int depID, bool stoptag)
        {
            _org.SetDepartmentStoptag(depID, stoptag);
        }

        public void SetDepartmentNoManager(int depID)
        {
            _org.SetDepartmentNoManager(depID);
        }

        /// <summary>
        /// 得到员工列表
        /// </summary>
        /// <returns></returns>
        public IList<Operators> GetOperatorsList()
        {
            return _org.GetOperatorsList();
        }

        public int AddOperators(Operators operators, ArrayList al_role, ArrayList al_subordinate)
        {
            //判断用户用是否重复
            IList<Operators> oplist = _org.GetOperatorListByName(operators.opeName);
            if (oplist.Count > 0)
            {
                throw new Exception("UserNameExist" + operators.opeName);
            }
            else
            {
                //先增加用户
                int opeId = _org.AddOperators(operators, al_role, al_subordinate);

                return opeId;
            }
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="operators"></param>
        public void UpdateOperators(Operators operators)
        {
            _org.UpdateOperators(operators);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateOperators(Operators operators,ArrayList al_role, ArrayList al_subordinate)
        {
            //先执行修改
            _org.UpdateOperators(operators);

            _authorization.DeleteOperatorInRole(operators.opeID);
            for (int i = 0; i < al_role.Count; i++)
            {
                sysOperatorInRole sysOpRole = (sysOperatorInRole)al_role[i];
                sysOpRole.OperatorID = operators.opeID;
                _authorization.AddOperatorInRole(sysOpRole);
            }

            _authorization.DeleteOperatorSubordinates(operators.opeID);
            for (int i = 0; i < al_subordinate.Count; i++)
            {
                sysOperatorSubordinate sysSubordinate = (sysOperatorSubordinate)al_subordinate[i];
                sysSubordinate.OperatorID = operators.opeID;
                _authorization.AddOperatorSubordinate(sysSubordinate);
            }
        }

        /// <summary>
        /// 得到员工明细
        /// </summary>
        /// <param name="opeID">员工ID</param>
        /// <returns></returns>
        public Operators GetOperatorsDetail(int opeID)
        {
            return _org.GetOperatorsDetail(opeID);
        }

        /// <summary>
        /// 删除员工，彻底
        /// </summary>
        /// <param name="opeID">员工ID</param>
        [Transaction(TransactionMode.Requires)]
        public void DeleteOperators(int opeID)
        {
            _org.DeleteOperators(opeID);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="opeID">员工ID</param>
        /// <param name="newpassword">新密码</param>
        /// <param name="oldpassword">原密码</param>
        public void UpdatePassword(int opeID, string newpassword, string oldpassword)
        {
            Operators op = _org.GetOperatorsDetail(opeID);
            if (op.opePassword == oldpassword)
            {
                _org.UpdatePassword(opeID, newpassword);
            }
            else
            {
                throw new ApplicationException("原密码不正确！");
            }
        }

        /// <summary>
        /// 根据用户名密码得到操作员ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int GetOperatorID(string username, string password)
        {
            int opeID = 0;
            Operators op = _org.OperatorLogIn(username, password);
            if (op != null)
            {
                opeID = op.opeID;
            }
            return opeID;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool OperatorLogIn(string username, string password, int userCount, ref Operators op)
        {
            bool result = false;
            op = _org.OperatorLogIn(username, password);
            if (op != null && op.opeIsCanLogin)
            {
                result = true;
                Operators current = _org.GetOperatorsDetail(op.opeID);
                current.opeLastLoginTime = DateTime.Now;
                current.opeLastActiveTime = DateTime.Now;
                _org.UpdateOperators(current);
            }
            else
            {
                throw new ApplicationException("用户名或密码错误!");
            }
            return result;
        }

        /// <summary>
        /// 检查用户名密码是否重复
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool CheckUserName(string username)
        {
            bool result = false;
            IList<Operators> oplist = _org.GetOperatorListByName(username);
            if (oplist.Count > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据用户名得到员工明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public IList<Operators> GetOperatorListByName(string username)
        {
            return _org.GetOperatorListByName(username);
        }


        /// <summary>
        /// 得到操作员的ID集合，包括部门ID，角色ID
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        /// <returns></returns>
        public string GetOperatorHasIDs(int opeID)
        {
            //首先在结果集中加入操作员ID
            string result = opeID.ToString().Trim();

            ////查询该操作员所属部门ID
            //Operators op = _org.GetOperatorsDetail(opeID);
            //int depId = op.opeDepartmentID.Value;

            ////在结果集中加入部门ID
            //result = result + "," + depId.ToString().Trim();

            //得到该操作员所属的角色ID
            IList<sysOperatorInRole> opInRolelist = _authorization.GetOperatorRoles(opeID);
            result += ",";
            foreach (sysOperatorInRole opInRole in opInRolelist)
            {
                result += opInRole.RoleID.ToString().Trim() + ",";
            }

            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }


        public string GetOperatorOAQuanXi(int opeID)
        {
            //首先在结果集中加入操作员ID
            string result = "|";

            //得到该操作员OA页面列表
            IList<sysPage> opOASysPages = _authorization.SelectsysPageForOAQuanXi(opeID);
            IList<sysFunction> opFunctions = _authorization.GetAllSysFunctionList();


            foreach (sysPage p in opOASysPages)
            {
                foreach (sysFunction f in opFunctions)
                {
                    if (f.Remark + "" != "")
                    {
                        if ((p.FunctionCount & f.FunctionPurviewCode) > 0)
                        {
                            result += p.exeFilePath + f.Remark + "|";
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 得到序列号
        /// </summary>
        /// <returns></returns>
        public IList<Operators> SelectSerialNumber(string sn)
        {
            return _org.SelectSerialNumber(sn);
        }
    }
}
