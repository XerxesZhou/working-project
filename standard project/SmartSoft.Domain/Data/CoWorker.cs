/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     Jack
 *       日期：     2017/6/19
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain.Data
{
    using System;

    /// <summary>
    /// CoWorker 
    /// </summary>
    [Serializable]
    public class CoWorker
    {
        public CoWorker()
        {

        }

        ///<sumary>
        /// 
        ///</sumary>
        public int cwID
        {
            get;
            set;
        }
        ///<sumary>
        /// 类型
        ///</sumary>
        public string cwSource
        {
            get;
            set;
        }
        ///<sumary>
        /// 单据ID
        ///</sumary>
        public int? cwRelatedID
        {
            get;
            set;
        }
        ///<sumary>
        /// 协作人
        ///</sumary>
        public int? cwOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入人
        ///</sumary>
        public int? cwAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入时间
        ///</sumary>
        public DateTime? cwAddDate
        {
            get;
            set;
        }
    }
}
