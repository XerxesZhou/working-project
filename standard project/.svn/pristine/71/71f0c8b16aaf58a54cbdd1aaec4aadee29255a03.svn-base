using System;
using System.Collections.Generic;
using System.Text;
using SmartSoft.Persistence;
using System.Collections;
using System.Data;
using SmartSoft.Component;

namespace SmartSoft.Service
{
    public class BaseService : BaseSqlMapDao
    {
       
        #region Standard Methods
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseService()
        {
            
        }

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns></returns>
        public IList<T> SelectAll<T>()
        {
            try
            {
                string className = typeof(T).Name;
                return this.ExecuteQueryForList<T>("Select" + className, null);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        public IList<T> SelectDynamic<T>(string whereCondition)
        {
            return SelectDynamic<T>(whereCondition, "");
        }

        /// <summary>
        /// 动态获得符合条件的记录并排序
        /// </summary>
        /// <param name="whereCondition">选择条件如：" AND ID > 20"</param>
        /// <param name="orderByExpression">排序条件如："ID DESC"</param>
        /// <returns>实体记录集</returns>
        public IList<T> SelectDynamic<T>(string whereCondition, string orderByExpression)
        {
            try
            {
                string className = typeof(T).Name;
                Hashtable ht = new Hashtable();
                ht["whereCondition"] = whereCondition;
                ht["orderByExpression"] = orderByExpression;

                return ExecuteQueryForList<T>("Select" + className + "Dynamic", ht);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        public DataTable SelectForDataTable(string statementName, object objectParameter, Hashtable htOutPutParameters)
        {
            try
            {
                return this.ExecuteQueryForDataTable(statementName, objectParameter, htOutPutParameters);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="objUser">新增的记录实体</param>
        /// <returns>新增的记录编号</returns>
        public int Add<T>(T obj)
        {
            try
            {
                string className = typeof(T).Name;
                return this.ExecuteInsert("Insert" + className, obj);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="objUser">修改的记录实体</param>
        /// <returns>修改是否成功</returns>
        public void Update<T>(T obj)
        {
            try
            {
                string className = typeof(T).Name;
                this.ExecuteUpdate("Update" + className, obj);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        public void Update(string statementName, object parameterObject)
        {
            try
            {
                this.ExecuteUpdate(statementName, parameterObject);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// 选择记录
        /// </summary>
        /// <param name="rowID">记录编号</param>
        /// <returns>记录实体对象</returns>
        public T GetDetail<T>(int rowID)
        {
            try
            {
                string className = typeof(T).Name;
                return this.ExecuteQueryForObject<T>("Select" + className, rowID);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        public void Delete(string statementName, object parameterObject)
        {
            try
            {
                this.ExecuteDelete(statementName, parameterObject);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="rowID">被删除的记录编号</param>
        /// <returns>删除是否成功</returns>
        public void Delete<T>(int rowID)
        {
            try
            {
                string className = typeof(T).Name;
                this.ExecuteDelete("Delete" + className, rowID);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        public bool BatchDelete<T>(string ids)
        {
            try
            {
                string className = typeof(T).Name;
                return this.BatchDelete("Delete" + className, ids);
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }
        #endregion

        
    }
}
