/***************************************
 * ���ߣ�    zsj
 * ��ʼʱ�䣺2007-01-26
 * 
 * 
 * ������
 *      �����ݿ��ϵͳ���л���û����Լ�ϵͳ�ֶ���Ϣ
 *      ֻ���ڲ�ѯ
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

