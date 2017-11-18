/***************************************************************************
 * 
 *       功能：     客户跟进计划实体类
 *       作者：     HongYue
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
    /// CustomerFollowPlan 
    /// </summary>
    [Serializable]
    public class CustomerFollowPlan
    {
        public CustomerFollowPlan()
        {

        }

        ///<sumary>
        /// 
        ///</sumary>
        public int cfpID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cfpSource
        {
            get;
            set;
        }
        ///<sumary>
        /// 客户
        ///</sumary>
        public int? cfpRelatedID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? cfpDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cfpOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cfpContent
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cfpAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? cfpAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cfpModifyOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? cfpModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cfpFilePath
        {
            get;
            set;
        }
    }
}
