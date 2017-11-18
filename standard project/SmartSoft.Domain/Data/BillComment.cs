/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     HY
 *       日期：     2017/5/16
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
    /// BillComment 
    /// </summary>
    [Serializable]
    public class BillComment
    {
        public BillComment()
        {

        }

        ///<sumary>
        /// ID
        ///</sumary>
        public int bcID
        {
            get;
            set;
        }
        ///<sumary>
        /// 来源
        ///</sumary>
        public int? bcSourceID
        {
            get;
            set;
        }
        ///<sumary>
        /// 内容
        ///</sumary>
        public string bcContent
        {
            get;
            set;
        }
        ///<sumary>
        /// 类型
        ///</sumary>
        public int? bcTypeID
        {
            get;
            set;
        }
        ///<sumary>
        /// 操作人
        ///</sumary>
        public int? bcOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 操作时间
        ///</sumary>
        public DateTime? bcDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 备注
        ///</sumary>
        public string bcRemark
        {
            get;
            set;
        }

        public int? bcRelatedID
        {
            get;
            set;
        }
    }
}
