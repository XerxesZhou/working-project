/***************************************
 * 作者：    zsj
 * 创始时间：2007-01-26
 * 
 * 
 * 描述：
 *      从数据库的系统表中获得用户表以及系统字段信息
 *      只用于查询
 *   
****************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.Common.Exceptions;
using SmartSoft.Domain.Data;

namespace SmartSoft.Persistence
{
    public class DataBaseColumnSqlMapDao : BaseSqlMapDao
    {

        public DataBaseColumnSqlMapDao()
        {
        }

        public IList<DataBaseColumn> SelectDynamic(string tableName, string whereCondition)
        {
            Hashtable ht = new Hashtable();
            ht["tableName"] = tableName;
            ht["whereCondition"] = whereCondition;

            return ExecuteQueryForList<DataBaseColumn>("SelectDataBaseColumnDynamic", ht);
        }
    }
}

