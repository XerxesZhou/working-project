/***************************************************************************
 * 
 *       功能：     公用定义服务层接口
 *       作者：     肖秋李
 *       日期：     2007-1-31
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Interface.Data
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;


    public interface IDefCommonService
    {
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <returns>公用定义实体列表</returns>
        IList<defCommon> GetDefCommonList(string tableName);

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <returns>公用定义实体列表</returns>
        DataTable GetDefCommonListForDataTable(string tableName);

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        /// <returns>公用定义实体</returns>
        defCommon GetDefCommon(string tableName, int id);

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
        int AddDefCommon(string tableName, defCommon defcommon);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="defcommon">公用定义实体</param>
        void UpdateDefCommon(string tableName, defCommon defcommon);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        void DeleteDefCommon(string tableName, int id);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        void OpenDefCommon(string tableName, int id);

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="tableName">表枚举</param>
        /// <param name="id">ID</param>
        void StopDefCommon(string tableName, int id);
    }
}
