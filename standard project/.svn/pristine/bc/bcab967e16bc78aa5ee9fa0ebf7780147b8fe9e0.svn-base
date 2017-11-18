/***************************************************************************
 * 
 *       功能：     组织机构持久层基类
 *       作者：     彭伟
 *       日期：     2007-1-26
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Persistence.Common
{ 
	using System;
    using System.Data;
	using System.Collections;
	using System.Collections.Generic;
	using IBatisNet.Common;
	using IBatisNet.DataMapper;
	using IBatisNet.Common.Exceptions;

    using SmartSoft.Domain.Common;

	/// <summary>
	/// BranchSqlMapDao
	/// </summary>
	public class OrganizationSqlMapDao : BaseSqlMapDao
	{
        public OrganizationSqlMapDao()
		{

        }

        #region 部门
        /// <summary>
        /// 得到所有部门列表
        /// </summary>
        /// <returns>IList</returns>
        public IList<Department> GetDepartmentList()
        {
            return ExecuteQueryForList<Department>("SelectDepartment", null);
        }

        /// <summary>
        /// 根据已用标志得到部门列表
        /// </summary>
        /// <param name="usetag">已用标志</param>
        /// <returns>IList</returns>
        public IList<Department> GetDepartmentListByUsetag(bool usetag)
        {
            return ExecuteQueryForList<Department>("SelectDepartmentByUseTag", usetag);
        }

        /// <summary>
        /// 根据停用标志得到部门列表
        /// </summary>
        /// <param name="stoptag">停用标志</param>
        /// <returns>IList</returns>
        public IList<Department> GetDepartmentListByStoptag(bool stoptag)
        {
            return ExecuteQueryForList<Department>("SelectDepartmentByStopTag", stoptag);
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns>int</returns>
        public int AddDepartment(Department department)
        {
            //和员工表用相同的流水号发生器
            department.depID = GetId("Operators");
            ExecuteInsert("InsertDepartment", department);
            return department.depID;
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="department"></param>
        public void UpdateDepartment(Department department)
        {
            ExecuteUpdate("UpdateDepartment", department);
        }

        /// <summary>
        /// 设置部门已用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="usetag">已用标志</param>
        public void SetDepartmentUsetag(int depID, bool usetag)
        {
            Hashtable ht = new Hashtable();
            ht.Add("depID",depID);
            ht.Add("depUseTag",usetag);

            ExecuteUpdate("SetDepartmentUseTag", ht);
        }

        /// <summary>
        /// 设置部门停用标志
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <param name="stoptag">停用标志</param>
        public void SetDepartmentStoptag(int depID,bool stoptag)
        {
            Hashtable ht = new Hashtable();
            ht.Add("depID", depID);
            ht.Add("depStopTag", stoptag);

            ExecuteUpdate("SetDepartmentStopTag", ht);
        }

        /// <summary>
        /// 得到部门明细
        /// </summary>
        /// <param name="depID">部门ID</param>
        /// <returns>Department</returns>
        public Department GetDepartmentDetail(int depID)
        {
            return ExecuteQueryForObject<Department>("SelectDepartment", depID);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="depID">部门ID</param>
        public void DeleteDepartment(int depID)
        {
            ExecuteDelete("DeleteDepartment", depID);
        }

        public void SetDepartmentNoManager(int depID)
        {
            ExecuteUpdate("SetDepartmentNoManager", depID);
        }

        #endregion

        #region 员工

        /// <summary>
        /// 得到员工列表
        /// </summary>
        /// <returns></returns>
        public IList<Operators> GetOperatorsList()
        {
            return ExecuteQueryForList<Operators>("SelectAllOperators1", null);
        }

        public DataTable SelectAllOperators()
        {
            return ExecuteQueryForDataTable("SelectAllOperators", null);
        }

        /// <summary>
        /// 得到系统有效的操作员
        /// </summary>
        /// <returns></returns>
        public IList<Operators> SelectEffectOperators()
        {
            return ExecuteQueryForList<Operators>("SelectEffectOperators", null);
        }

        /// <summary>
        /// 得到分公司下的所有员工
        /// </summary>
        /// <param name="branchID">分公司ID</param>
        /// <returns></returns>
        public IList<Operators> GetBranchOperatorsList(int branchID)
        {
            return ExecuteQueryForList<Operators>("SelectOperatorsByBranch", branchID);
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="operators">员工实体</param>
        /// <returns>int</returns>
        public int AddOperators(Operators operators)
        {
            operators.opeID = ExecuteInsert("InsertOperators", operators);
            return operators.opeID;
        }

        /// <summary>
        /// 新增员工及与部门，角色关系
        /// </summary>
        /// <param name="operators">员工</param>
        /// <param name="al_branch">部门关系</param>
        /// <param name="al_role">角色关系</param>
        /// <returns>int</returns>
        public int AddOperators(Operators operators, ArrayList al_role, ArrayList al_subordinate)
        {
            operators.opeID = ExecuteInsert("InsertOperators", operators);
            for (int i = 0; i < al_role.Count; i++)
            {
                sysOperatorInRole sysOpRole = (sysOperatorInRole)al_role[i];
                sysOpRole.OperatorID = operators.opeID;

                ExecuteInsert("InsertsysOperatorInRole", sysOpRole);
            }

            for (int i = 0; i < al_subordinate.Count; i++)
            {
                sysOperatorSubordinate sysSubordinate = (sysOperatorSubordinate)al_subordinate[i];
                sysSubordinate.OperatorID = operators.opeID;
                ExecuteInsert("InsertsysOperatorSubordinate", sysSubordinate);
            }
            
            return operators.opeID;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateOperators(Operators operators)
        {
            ExecuteUpdate("UpdateOperators", operators);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Operators GetOperatorsDetail(int opeID)
        {
            return ExecuteQueryForObject<Operators>("OperatorDetail", opeID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOperators(int opeID)
        {
            ExecuteDelete("DeleteOperators", opeID);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="opeID">员工ID</param>
        /// <param name="password">密码</param>
        public void UpdatePassword(int opeID, string password)
        {
            Hashtable ht = new Hashtable();
            ht.Add("opeID",opeID);
            ht.Add("opePassword",password);

            ExecuteUpdate("ChangePassword", ht);
        }

        /// <summary>
        /// 根据用户名密码得到员工明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>Operators</returns>
        public Operators OperatorLogIn(string username, string password)
        {
            Hashtable ht = new Hashtable();
            ht.Add("opeUserName",username);
            ht.Add("opePassword",password);

            return ExecuteQueryForObject<Operators>("OperatorLogIn", ht);
        }

        /// <summary>
        /// 根据用户名得到员工明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public IList<Operators> GetOperatorListByName(string username)
        {
            return ExecuteQueryForList<Operators>("CheckUserName",username);
        }

        /// <summary>
        /// 得到序列号
        /// </summary>
        /// <returns></returns>
        public IList<Operators> SelectSerialNumber(string sn)
        {
            return ExecuteQueryForList<Operators>("SelectSerialNumber", sn);
        }

        #endregion
    }
}
