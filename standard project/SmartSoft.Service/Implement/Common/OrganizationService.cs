/***************************************************************************
 * 
 *       ���ܣ�     ��֯���������ӿ�ʵ����
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007-1-26
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
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
        /// ��������
        /// </summary>
        /// <param name="department"></param>
        public int AddDepartment(Department department)
        {
            return _org.AddDepartment(department);
        }

        /// <summary>
        /// �õ�������ϸ
        /// </summary>
        /// <param name="depID">����ID</param>
        /// <returns></returns>
        public Department GetDepartmentDetail(int depID)
        {
            return _org.GetDepartmentDetail(depID);
        }

        /// <summary>
        /// �޸Ĳ�����Ϣ
        /// </summary>
        /// <param name="department"></param>
        public void UpdateDepartment(Department department)
        {
            _org.UpdateDepartment(department);
        }

        /// <summary>
        /// ɾ������
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
        /// �������ñ�־�õ������б�
        /// </summary>
        /// <param name="usetag"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentListByUsetag(bool usetag)
        {
            return _org.GetDepartmentListByUsetag(usetag);
        }

        /// <summary>
        /// ����ͣ�ñ�־�õ������б�
        /// </summary>
        /// <param name="stoptag"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentListByStoptag(bool stoptag)
        {
            return _org.GetDepartmentListByStoptag(stoptag);
        }

        /// <summary>
        /// ���ò������ñ�־
        /// </summary>
        /// <param name="depID">����ID</param>
        /// <param name="usetag">���ñ�־</param>
        public void SetDepartmentUsetag(int depID, bool usetag)
        {
            _org.SetDepartmentUsetag(depID, usetag);
        }

        /// <summary>
        /// ���ò���ͣ�ñ�־
        /// </summary>
        /// <param name="depID">����ID</param>
        /// <param name="stoptag">ͣ�ñ�־</param>
        public void SetDepartmentStoptag(int depID, bool stoptag)
        {
            _org.SetDepartmentStoptag(depID, stoptag);
        }

        public void SetDepartmentNoManager(int depID)
        {
            _org.SetDepartmentNoManager(depID);
        }

        /// <summary>
        /// �õ�Ա���б�
        /// </summary>
        /// <returns></returns>
        public IList<Operators> GetOperatorsList()
        {
            return _org.GetOperatorsList();
        }

        public int AddOperators(Operators operators, ArrayList al_role, ArrayList al_subordinate)
        {
            //�ж��û����Ƿ��ظ�
            IList<Operators> oplist = _org.GetOperatorListByName(operators.opeName);
            if (oplist.Count > 0)
            {
                throw new Exception("UserNameExist" + operators.opeName);
            }
            else
            {
                //�������û�
                int opeId = _org.AddOperators(operators, al_role, al_subordinate);

                return opeId;
            }
        }

        /// <summary>
        /// �޸�Ա����Ϣ
        /// </summary>
        /// <param name="operators"></param>
        public void UpdateOperators(Operators operators)
        {
            _org.UpdateOperators(operators);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateOperators(Operators operators,ArrayList al_role, ArrayList al_subordinate)
        {
            //��ִ���޸�
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
        /// �õ�Ա����ϸ
        /// </summary>
        /// <param name="opeID">Ա��ID</param>
        /// <returns></returns>
        public Operators GetOperatorsDetail(int opeID)
        {
            return _org.GetOperatorsDetail(opeID);
        }

        /// <summary>
        /// ɾ��Ա��������
        /// </summary>
        /// <param name="opeID">Ա��ID</param>
        [Transaction(TransactionMode.Requires)]
        public void DeleteOperators(int opeID)
        {
            _org.DeleteOperators(opeID);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="opeID">Ա��ID</param>
        /// <param name="newpassword">������</param>
        /// <param name="oldpassword">ԭ����</param>
        public void UpdatePassword(int opeID, string newpassword, string oldpassword)
        {
            Operators op = _org.GetOperatorsDetail(opeID);
            if (op.opePassword == oldpassword)
            {
                _org.UpdatePassword(opeID, newpassword);
            }
            else
            {
                throw new ApplicationException("ԭ���벻��ȷ��");
            }
        }

        /// <summary>
        /// �����û�������õ�����ԱID
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
        /// ��¼
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
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
                throw new ApplicationException("�û������������!");
            }
            return result;
        }

        /// <summary>
        /// ����û��������Ƿ��ظ�
        /// </summary>
        /// <param name="username">�û���</param>
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
        /// �����û����õ�Ա����ϸ
        /// </summary>
        /// <param name="username">�û���</param>
        /// <returns></returns>
        public IList<Operators> GetOperatorListByName(string username)
        {
            return _org.GetOperatorListByName(username);
        }


        /// <summary>
        /// �õ�����Ա��ID���ϣ���������ID����ɫID
        /// </summary>
        /// <param name="opeID">����ԱID</param>
        /// <returns></returns>
        public string GetOperatorHasIDs(int opeID)
        {
            //�����ڽ�����м������ԱID
            string result = opeID.ToString().Trim();

            ////��ѯ�ò���Ա��������ID
            //Operators op = _org.GetOperatorsDetail(opeID);
            //int depId = op.opeDepartmentID.Value;

            ////�ڽ�����м��벿��ID
            //result = result + "," + depId.ToString().Trim();

            //�õ��ò���Ա�����Ľ�ɫID
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
            //�����ڽ�����м������ԱID
            string result = "|";

            //�õ��ò���ԱOAҳ���б�
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
        /// �õ����к�
        /// </summary>
        /// <returns></returns>
        public IList<Operators> SelectSerialNumber(string sn)
        {
            return _org.SelectSerialNumber(sn);
        }
    }
}
