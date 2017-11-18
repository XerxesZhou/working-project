/***************************************************************************
 * 
 *       功能：     公用定义服务层接口实现类
 *       作者：     肖秋李
 *       日期：     2007-01-31
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
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
        /// 得到明细
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <returns>公用定义实体列表</returns>
        public IList<defCommon> GetDefCommonList(string tableName)
        {
            return _defCommon.GetDefCommonList(tableName);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        /// <returns>公用定义实体</returns>
        public defCommon GetDefCommon(string tableName, int id)
        {
            return _defCommon.GetDefCommon(tableName, id);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
        public int AddDefCommon(string tableName, defCommon defcommon)
        {
            return _defCommon.AddDefCommon(tableName, defcommon);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
        public void UpdateDefCommon(string tableName, defCommon defcommon)
        {
            _defCommon.UpdateDefCommon(tableName, defcommon);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void DeleteDefCommon(string tableName, int id)
        {
            _defCommon.DeleteDefCommon(tableName, id);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void OpenDefCommon(string tableName, int id)
        {
            _defCommon.OpenDefCommon(tableName, id);
        }

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        public void StopDefCommon(string tableName, int id)
        {
            _defCommon.StopDefCommon(tableName, id);
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDefCommonListForDataTable(string tableName)
        {
            return _defCommon.GetDefCommonListForDataTable(tableName);
        }
    }
}
