/***************************************************************************
 * 
 *       ���ܣ�     ���ö�������ӿ�ʵ����
 *       ���ߣ�     Ф����
 *       ���ڣ�     2007-01-31
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Implement.Data
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Persistence.Data;
    using SmartSoft.Service.Interface.Data;

    using Castle.Facilities.AutomaticTransactionManagement;
    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    [Transactional]
    [UsesAutomaticSessionCreation]
    public class DefCommonService : IDefCommonService
    {
        private DefCommonSqlMapDao _defCommon;
        public DefCommonService(DefCommonSqlMapDao defcommondao)
        {
            _defCommon = defcommondao;
        }

        /// <summary>
        /// �õ���ϸ
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <returns>���ö���ʵ���б�</returns>
        public IList<defCommon> GetDefCommonList(string tableName)
        {
            return _defCommon.GetDefCommonList(tableName);
        }

        /// <summary>
        /// �õ���ϸ
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        /// <returns>���ö���ʵ��</returns>
        public defCommon GetDefCommon(string tableName, int id)
        {
            return _defCommon.GetDefCommon(tableName, id);
        }

        /// <summary>
        /// �½�
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="defcommon">���ö���ʵ��</param>
        public int AddDefCommon(string tableName, defCommon defcommon)
        {
            return _defCommon.AddDefCommon(tableName, defcommon);
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="defcommon">���ö���ʵ��</param>
        public void UpdateDefCommon(string tableName, defCommon defcommon)
        {
            _defCommon.UpdateDefCommon(tableName, defcommon);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void DeleteDefCommon(string tableName, int id)
        {
            _defCommon.DeleteDefCommon(tableName, id);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void OpenDefCommon(string tableName, int id)
        {
            _defCommon.OpenDefCommon(tableName, id);
        }

        /// <summary>
        /// ͣ��
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void StopDefCommon(string tableName, int id)
        {
            _defCommon.StopDefCommon(tableName, id);
        }

        /// <summary>
        /// �õ��б�
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDefCommonListForDataTable(string tableName)
        {
            return _defCommon.GetDefCommonListForDataTable(tableName);
        }
    }
}
