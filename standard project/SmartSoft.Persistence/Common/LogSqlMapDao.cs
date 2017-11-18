/***************************************************************************
 * 
 *       功能：     系统日志持久层基类
 *       作者：     彭伟
 *       日期：     2007-06-11
 * 
 * *************************************************************************/
namespace SmartSoft.Persistence.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using IBatisNet.Common;
    using IBatisNet.DataMapper;
    using IBatisNet.Common.Exceptions;

    using SmartSoft.Domain.Common;

    public class LogSqlMapDao : BaseSqlMapDao
    {
        public LogSqlMapDao()
        { 
        
        }

        /// <summary>
        /// 得到系统_日志列表
        /// </summary>
        public IList<sysLog> GetsysLogList()
        {
            return ExecuteQueryForList<sysLog>("SelectsysLog", null);
        }

        /// <summary>
        /// 新增系统_日志
        /// </summary>
        public int AddsysLog(sysLog syslog)
        {
            int id = GetId("sysLog");
            syslog.LogID = id;
            ExecuteInsert("InsertsysLog", syslog);
            return id;
        }
        /// <summary>
        /// 修改系统_日志
        /// </summary>
        public void UpdatesysLog(sysLog syslog)
        {
            ExecuteUpdate("UpdatesysLog", syslog);
        }

        /// <summary>
        /// 得到系统_日志明细
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public sysLog GetsysLogDetail(int LogID)
        {
            return ExecuteQueryForObject<sysLog>("SelectsysLog", LogID);
        }

        /// <summary>
        /// 删除系统_日志
        /// </summary>
        /// <param name=""></param>
        public void DeletesysLog(int LogID)
        {
            ExecuteDelete("DeletesysLog", LogID);
        }
    }
}
