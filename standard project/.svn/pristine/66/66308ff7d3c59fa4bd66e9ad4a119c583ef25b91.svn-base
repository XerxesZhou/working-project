/***************************************************************************
 * 
 *       功能：     基于IBatisNet的数据访问基类
 *       作者：     彭伟
 *       日期：     2006/12/31
 * 
 *       修改日期： 2008-05-19
 *       修改人：chexf
 *       修改内容：调整cmd超时时间
 * 
 * *************************************************************************/

namespace SmartSoft.Persistence
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using IBatisNet.Common;
    using IBatisNet.Common.Utilities;
    using IBatisNet.Common.Pagination;
    using IBatisNet.DataMapper;
    using IBatisNet.DataMapper.Scope;
    using IBatisNet.DataMapper.MappedStatements;
    using IBatisNet.DataMapper.Configuration.Statements;
    using IBatisNet.DataMapper.Exceptions;
    using IBatisNet.DataMapper.Configuration;

    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using Castle.Core.Resource;

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;

    public class BaseSqlMapDao
    {
        private ISqlMapper sqlMap;
        public ISqlMapper sqlServerSqlMap
        {
            set
            {
                sqlMap = value;
            }
        }

        public BaseSqlMapDao()
        {

        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="statementName">操作名称，对应xml中的Statement的id</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        public IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            try
            {
                IList<T> list = sqlMap.QueryForList<T>(statementName, parameterObject);

                return list;
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 得到指定数量的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject">参数</param>
        /// <param name="skipResults">跳过的记录数</param>
        /// <param name="maxResults">最大返回的记录数</param>
        /// <returns></returns>
        public IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 得到分页的列表
        /// </summary>
        /// <param name="statementName">操作名称</param>
        /// <param name="parameterObject">参数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int pageSize)
        {
            try
            {
                return sqlMap.QueryForPaginatedList(statementName, parameterObject, pageSize);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 查询得到对象的一个实例
        /// </summary>
        /// <typeparam name="T">对象type</typeparam>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        public T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            try
            {
                return sqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 查询得到对象的一个实例
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        public object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            try
            {
                return sqlMap.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 执行添加
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        public int ExecuteInsert(string statementName, object parameterObject)
        {
            try
            {
                object ret = sqlMap.Insert(statementName, parameterObject);
                if (StringHelper.IsValidDBInt(ret))
                {
                    return int.Parse(ret.ToString());
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        public void ExecuteUpdate(string statementName, object parameterObject)
        {
            try
            {
                sqlMap.Update(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for update.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        public void ExecuteDelete(string statementName, object parameterObject)
        {
            try
            {
                sqlMap.Delete(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for delete.  Cause: " + e.Message, e);
            }
        }

        public bool BatchDelete(string statementName, string ids)
        {
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    sqlMap.BeginTransaction();
                    string[] items = ids.Split(",".ToCharArray());
                    foreach (string item in items)
                    {
                        this.ExecuteDelete(statementName, int.Parse(item));
                    }
                    sqlMap.CommitTransaction();
                }
                return true;
            }
            catch (ApplicationException e)
            {
                sqlMap.RollBackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 得到流水号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int GetId(string tableName)
        {
            try
            {
                sysStream stream = sqlMap.QueryForObject("GetStreamId", tableName) as sysStream;
                return stream.MaxID;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void UpdateBatch()
        {

        }

        /// <summary>
        /// 为实现In的效果
        /// </summary>
        protected internal class KeyWordSearch
        {
            private IList keywordList = new ArrayList();

            public KeyWordSearch(String keywords)
            {
                StringTokenizer splitter = new StringTokenizer(keywords, ",", false);
                string token = null;

                IEnumerator enumerator = splitter.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    token = (string)enumerator.Current;
                    keywordList.Add(token);
                }
            }

            public IList KeywordList
            {
                get
                {
                    return keywordList;
                }
                set
                {
                    keywordList = value;
                }
            }
        }

        /// <summary>
        /// 得到参数化后的SQL
        /// </summary>
        public string GetSql(string statementName, object paramObject)
        {

            IStatement statement = sqlMap.GetMappedStatement(statementName).Statement;

            IMappedStatement mapStatement = sqlMap.GetMappedStatement(statementName);

            IDalSession session = new SqlMapSession(sqlMap);

            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);

            return request.PreparedStatement.PreparedSql;

        }

        private IDbCommand GetDbCommand(string statementName, object paramObject)
        {
            IStatement statement = sqlMap.GetMappedStatement(statementName).Statement;

            IMappedStatement mapStatement = sqlMap.GetMappedStatement(statementName);

            IDalSession session = new SqlMapSession(sqlMap);

            if (sqlMap.LocalSession != null)
            {
                session = sqlMap.LocalSession;
            }
            else
            {
                session = sqlMap.OpenConnection();
            }

            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);

            mapStatement.PreparedCommand.Create(request, session, statement, paramObject);

            return request.IDbCommand;

        }

        /// <summary>
        /// 通用的以DataTable的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="statementName">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <param name="htOutPutParameter)">Output参数值哈希表</param>
        /// <returns>得到的DataTable</returns>
        public DataTable ExecuteQueryForDataTable(string statementName, object paramObject, Hashtable htOutPutParameter)
        {
            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = sqlMap.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(sqlMap);
                session.OpenConnection();
                isSessionLocal = true;
            }

            IDbCommand cmd = GetDbCommand(statementName, paramObject);

            try
            {
                cmd.Connection = session.Connection;
                cmd.CommandTimeout = 30000;
                IDbDataAdapter adapter = session.CreateDataAdapter(cmd);
                adapter.Fill(ds);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }

            foreach (IDataParameter parameter in cmd.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Output)
                {
                    htOutPutParameter[parameter.ParameterName] = parameter.Value;
                }
            }

            return ds.Tables[0];

        }


        /// <summary>
        /// 通用的以DataTable的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="statementName">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>得到的DataTable</returns>
        public DataTable ExecuteQueryForDataTable(string statementName, object paramObject)
        {
            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = sqlMap.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(sqlMap);
                session.OpenConnection();
                isSessionLocal = true;
            }

            IDbCommand cmd = GetDbCommand(statementName, paramObject);

            try
            {
                cmd.Connection = session.Connection;
                cmd.CommandTimeout = 30000; //chexf 2008-05-20 调整cmd超时时间
                IDbDataAdapter adapter = session.CreateDataAdapter(cmd);
                adapter.Fill(ds);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 通用的以DataSet的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="statementName">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>得到的DataDataSet</returns>
        public DataSet ExecuteQueryForDataSet(string statementName, object paramObject)
        {
            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = sqlMap.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(sqlMap);
                session.OpenConnection();
                isSessionLocal = true;
            }

            IDbCommand cmd = GetDbCommand(statementName, paramObject);

            try
            {
                cmd.Connection = session.Connection;
                cmd.CommandTimeout = 30000; //chexf 2008-05-20 调整cmd超时时间
                IDbDataAdapter adapter = session.CreateDataAdapter(cmd);
                adapter.Fill(ds);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }

            return ds;
        }
    }

}
