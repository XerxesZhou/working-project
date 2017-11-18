/***************************************************************************
 * 
 *       ���ܣ�     ϵͳ��־�־ò����
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007-06-11
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
        /// �õ�ϵͳ_��־�б�
        /// </summary>
        public IList<sysLog> GetsysLogList()
        {
            return ExecuteQueryForList<sysLog>("SelectsysLog", null);
        }

        /// <summary>
        /// ����ϵͳ_��־
        /// </summary>
        public int AddsysLog(sysLog syslog)
        {
            int id = GetId("sysLog");
            syslog.LogID = id;
            ExecuteInsert("InsertsysLog", syslog);
            return id;
        }
        /// <summary>
        /// �޸�ϵͳ_��־
        /// </summary>
        public void UpdatesysLog(sysLog syslog)
        {
            ExecuteUpdate("UpdatesysLog", syslog);
        }

        /// <summary>
        /// �õ�ϵͳ_��־��ϸ
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public sysLog GetsysLogDetail(int LogID)
        {
            return ExecuteQueryForObject<sysLog>("SelectsysLog", LogID);
        }

        /// <summary>
        /// ɾ��ϵͳ_��־
        /// </summary>
        /// <param name=""></param>
        public void DeletesysLog(int LogID)
        {
            ExecuteDelete("DeletesysLog", LogID);
        }
    }
}
