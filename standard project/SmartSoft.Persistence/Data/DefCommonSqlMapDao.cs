/***************************************************************************
 * 
 *       ���ܣ�     ���ö���־ò����
 *       ���ߣ�     Leo
 *       ���ڣ�     2007-01-31
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
 * 
 * *************************************************************************/
namespace SmartSoft.Persistence.Data
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using IBatisNet.Common;
    using IBatisNet.DataMapper;
    using IBatisNet.Common.Exceptions;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;

    /// <summary>
    /// DefCommonSqlMapDao
    /// </summary>
    public class DefCommonSqlMapDao : BaseSqlMapDao
    {

        private string[] _columnName = { "id", "name", "remark", "tablename", "usetag","orderby"};

        public DefCommonSqlMapDao()
        {
            //
            // TODO: �˴����defCommonSqlMapDao�Ĺ��캯��
            //
        }

        /// <summary>
        /// �õ��б�
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <returns>���ö���ʵ���б�</returns>
        public IList<defCommon> GetDefCommonList(string tableName)
        {
            string querystr = string.Empty;

            querystr = "select * from defCommon where tablename = '" + tableName + "' order by OrderBy asc";

            return ExecuteQueryForList<defCommon>("SelectdefCommon", querystr);
        }

        /// <summary>
        /// �õ��б�DataTable
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <returns>���ö���ʵ���б�</returns>
        public DataTable GetDefCommonListForDataTable(string tableName)
        {
            string querystr = string.Empty;
            Hashtable ht = new Hashtable();

            querystr = "select * from defCommon where tablename = '" + tableName + "' order by OrderBy asc";

            return ExecuteQueryForDataTable("SelectdefCommon", querystr, ht);
        }

        /// <summary>
        /// �õ���ϸ
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        /// <returns>���ö���ʵ��</returns>
        public defCommon GetDefCommon(string tableName, int id)
        {
            string querystr = "select * from defCommon where [ID] = " + id.ToString();
            
            return ExecuteQueryForObject<defCommon>("BaseSelectdefCommon", querystr);
        }

        /// <summary>
        /// �½�
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="defcommon">���ö���ʵ��</param>
        public int AddDefCommon(string tableName, defCommon defcommon)
        {
            int id = GetId("defCommon");
            defcommon.ID = id;
            string querystr = string.Empty;
            querystr = string.Format("insert into defCommon(ID,Name,TableName,Remark,UseTag,OrderBy) Values({0},'{1}','{2}','{3}',{4},{5})",defcommon.ID, defcommon.Name, defcommon.TableName, defcommon.Remark, defcommon.UseTag ? "1" : "0",defcommon.OrderBy);
            ExecuteInsert("InsertdefCommon", querystr);
            return id;
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="defcommon">���ö���ʵ��</param>
        public void UpdateDefCommon(string tableName, defCommon defcommon)
        {
            string querystr = string.Empty;
            querystr = string.Format("update defCommon set Name = '{0}',Remark = '{1}',OrderBy='{3}' where [ID] = {2}", defcommon.Name, defcommon.Remark, defcommon.ID,defcommon.OrderBy);

            ExecuteUpdate("UpdatedefCommon", querystr);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void DeleteDefCommon(string tableName, int id)
        {
            string querystr = string.Empty;
            querystr += "DELETE FROM defCommon WHERE [ID] = " + id;
            ExecuteDelete("DeletedefCommon", querystr);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void OpenDefCommon(string tableName, int id)
        {
            StopOrOpenDefCommon(tableName, id, true);
        }

        /// <summary>
        /// ͣ��
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        public void StopDefCommon(string tableName, int id)
        {
            StopOrOpenDefCommon(tableName, id, false);
        }

        /// <summary>
        /// ͣ/����
        /// </summary>
        /// <param name="tableName">��ö��</param>
        /// <param name="id">ID</param>
        /// <param name="IsStop">�Ƿ�ͣ��</param>
        public void StopOrOpenDefCommon(string tableName, int id, bool usetag)
        {
            string querystr = string.Empty;
            string sUse = (usetag) ? "1" : "0";
            querystr += " UPDATE defCommon SET UseTag = " + sUse + " WHERE [ID] = " + id.ToString();

            ExecuteDelete("OpendefCommon", querystr);
        }
    }
}
