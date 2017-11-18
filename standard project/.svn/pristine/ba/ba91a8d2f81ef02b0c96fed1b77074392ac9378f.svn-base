/***************************************************************************
 * 
 *       功能：     所有实体类的基类，使其支持SQLMAP的所有操作
 *       作者：     彭伟
 *       日期：     2007-7-27
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;

    using IBatisNet.Common;
    using IBatisNet.Common.Utilities;
    using IBatisNet.Common.Pagination;
    using IBatisNet.DataMapper;
    using IBatisNet.DataMapper.Scope;
    using IBatisNet.DataMapper.MappedStatements;
    using IBatisNet.DataMapper.Configuration.Statements;
    using IBatisNet.DataMapper.Exceptions;
    using IBatisNet.DataMapper.Configuration;

    using SmartSoft.Domain.Common;

    public class DomainBase<T>
    {
        /// <summary>
        /// 得到所有列表
        /// </summary>
        /// <returns></returns>
        public static IList<T> SelectAll()
        {
            ISqlMapper mapper = Mapper.Instance();

            return mapper.QueryForList<T>(typeof(T).Name + ".Select", null);
        }
        
        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public static T Get(object primaryKey)
        {
            ISqlMapper mapper = Mapper.Instance();

            return mapper.QueryForObject<T>(typeof(T).Name + ".Select", primaryKey);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public T Save()
        {
            ISqlMapper mapper = Mapper.Instance();

            return (T)mapper.Insert(typeof(T).Name + ".Insert", this);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public void Update()
        {
            ISqlMapper mapper = Mapper.Instance();
            mapper.Update(typeof(T).Name + ".Update", this);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            ISqlMapper mapper = Mapper.Instance();
            mapper.Delete(typeof(T).Name + ".Delete", this);
        }

        /// <summary>
        /// 查询,返回ILIST
        /// </summary>
        /// <param name="statementName">SQLMAP中的Statement</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        public static IList<T> Find(string statementName, object parameterObject)
        {
            try
            {
                statementName = GetStatement(statementName);
                ISqlMapper mapper = Mapper.Instance();
                return mapper.QueryForList<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 查询,返回DATATABLE
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public static DataTable FindforTable(string statementName, object paramObject)
        {
            statementName = GetStatement(statementName);
            IDbCommand cmd = GetDbCommand(statementName, paramObject);

            return QueryForDataTable(cmd);
        }

        /// <summary>
        /// 查询,返回DATATABLE
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static DataTable FindforTable(string sql)
        {
            IDbCommand cmd = GetDbCommand(sql);

            return QueryForDataTable(cmd);
        }

        #region 为返回DATATABLE服务
        private static DataTable QueryForDataTable(IDbCommand cmd)
        {
            ISqlMapper mapper = Mapper.Instance();

            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = mapper.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(mapper);
                session.OpenConnection();
                isSessionLocal = true;
            }

            try
            {
                cmd.Connection = session.Connection;
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

        private static IDbCommand GetDbCommand(string statementName, object paramObject)
        {

            ISqlMapper mapper = Mapper.Instance();


            IDalSession session = new SqlMapSession(mapper);

            if (mapper.LocalSession != null)
            {
                session = mapper.LocalSession;
            }
            else
            {
                session = mapper.OpenConnection();
            }

            IStatement statement = mapper.GetMappedStatement(statementName).Statement;

            IMappedStatement mapStatement = mapper.GetMappedStatement(statementName);
            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);

            string sql = request.PreparedStatement.PreparedSql;

            IDbCommand command = session.CreateCommand(statement.CommandType);
            command.CommandText = sql;


            return command;

        }

        private static IDbCommand GetDbCommand(string sql)
        {
            ISqlMapper mapper = Mapper.Instance();


            IDalSession session = new SqlMapSession(mapper);

            if (mapper.LocalSession != null)
            {
                session = mapper.LocalSession;
            }
            else
            {
                session = mapper.OpenConnection();
            }

            IDbCommand command = session.CreateCommand(CommandType.Text);
            command.CommandText = sql;

            return command;

        }
        #endregion

        /// <summary>
        /// 得到Statement的id
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private static string GetStatement(string statement)
        {
            return typeof(T).Name + "." + statement;
        }


        //public int GetStreamID()
        //{
        //    string tableName = typeof(T).Name;
        //    try
        //    {
        //        sysStream stream = sqlMap.QueryForObject("GetStreamId", tableName) as sysStream;
        //        return stream.MaxID;
        //    }
        //    catch (Exception e)
        //    {
        //        throw (e);
        //    }
        //}
    }
}
