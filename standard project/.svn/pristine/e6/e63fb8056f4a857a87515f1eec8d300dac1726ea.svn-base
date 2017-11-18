/***************************************************************************
 * 
 *       功能：     公用定义持久层基类
 *       作者：     Leo
 *       日期：     2007-01-31
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
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
            // TODO: 此处添加defCommonSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <returns>公用定义实体列表</returns>
        public IList<defCommon> GetDefCommonList(string tableName)
        {
            string querystr = string.Empty;

            querystr = "select * from defCommon where tablename = '" + tableName + "' order by OrderBy asc";

            return ExecuteQueryForList<defCommon>("SelectdefCommon", querystr);
        }

        /// <summary>
        /// 得到列表DataTable
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <returns>公用定义实体列表</returns>
        public DataTable GetDefCommonListForDataTable(string tableName)
        {
            string querystr = string.Empty;
            Hashtable ht = new Hashtable();

            querystr = "select * from defCommon where tablename = '" + tableName + "' order by OrderBy asc";

            return ExecuteQueryForDataTable("SelectdefCommon", querystr, ht);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        /// <returns>公用定义实体</returns>
        public defCommon GetDefCommon(string tableName, int id)
        {
            string querystr = "select * from defCommon where [ID] = " + id.ToString();
            
            return ExecuteQueryForObject<defCommon>("BaseSelectdefCommon", querystr);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
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
        /// 修改
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
        public void UpdateDefCommon(string tableName, defCommon defcommon)
        {
            string querystr = string.Empty;
            querystr = string.Format("update defCommon set Name = '{0}',Remark = '{1}',OrderBy='{3}' where [ID] = {2}", defcommon.Name, defcommon.Remark, defcommon.ID,defcommon.OrderBy);

            ExecuteUpdate("UpdatedefCommon", querystr);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void DeleteDefCommon(string tableName, int id)
        {
            string querystr = string.Empty;
            querystr += "DELETE FROM defCommon WHERE [ID] = " + id;
            ExecuteDelete("DeletedefCommon", querystr);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void OpenDefCommon(string tableName, int id)
        {
            StopOrOpenDefCommon(tableName, id, true);
        }

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void StopDefCommon(string tableName, int id)
        {
            StopOrOpenDefCommon(tableName, id, false);
        }

        /// <summary>
        /// 停/启用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        /// <param name="IsStop">是否停用</param>
        public void StopOrOpenDefCommon(string tableName, int id, bool usetag)
        {
            string querystr = string.Empty;
            string sUse = (usetag) ? "1" : "0";
            querystr += " UPDATE defCommon SET UseTag = " + sUse + " WHERE [ID] = " + id.ToString();

            ExecuteDelete("OpendefCommon", querystr);
        }
    }
}
