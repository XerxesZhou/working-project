/***************************************************************************
 * 
 *       功能：     业务员签约回款计划实体类
 *       作者：     wsd
 *       日期：     2016/6/17
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
    /// OperatorPlan 
    /// </summary>
    [Serializable]
    public class OperatorPlan
    {
        public OperatorPlan()
        {

        }

        ///<sumary>
        /// ID
        ///</sumary>
        public int opID
        {
            get;
            set;
        }
        ///<sumary>
        /// 业务员
        ///</sumary>
        public int? opOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 类型：1为合同，2为回款
        ///</sumary>
        public int? opTypeID
        {
            get;
            set;
        }
        ///<sumary>
        /// 年份
        ///</sumary>
        public int? opYear
        {
            get;
            set;
        }
        ///<sumary>
        /// 1月
        ///</sumary>
        public decimal? opM1
        {
            get;
            set;
        }
        ///<sumary>
        /// 2月
        ///</sumary>
        public decimal? opM2
        {
            get;
            set;
        }
        ///<sumary>
        /// 3月
        ///</sumary>
        public decimal? opM3
        {
            get;
            set;
        }
        ///<sumary>
        /// 4月
        ///</sumary>
        public decimal? opM4
        {
            get;
            set;
        }
        ///<sumary>
        /// 5月
        ///</sumary>
        public decimal? opM5
        {
            get;
            set;
        }
        ///<sumary>
        /// 6月
        ///</sumary>
        public decimal? opM6
        {
            get;
            set;
        }
        ///<sumary>
        /// 7月
        ///</sumary>
        public decimal? opM7
        {
            get;
            set;
        }
        ///<sumary>
        /// 8月
        ///</sumary>
        public decimal? opM8
        {
            get;
            set;
        }
        ///<sumary>
        /// 9月
        ///</sumary>
        public decimal? opM9
        {
            get;
            set;
        }
        ///<sumary>
        /// 10月
        ///</sumary>
        public decimal? opM10
        {
            get;
            set;
        }
        ///<sumary>
        /// 11月
        ///</sumary>
        public decimal? opM11
        {
            get;
            set;
        }
        ///<sumary>
        /// 12月
        ///</sumary>
        public decimal? opM12
        {
            get;
            set;
        }
        ///<sumary>
        /// 合计
        ///</sumary>
        public decimal? opTotal
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入时间
        ///</sumary>
        public DateTime? opAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入人
        ///</sumary>
        public int? opAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改时间
        ///</sumary>
        public DateTime? opModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改人
        ///</sumary>
        public int? opModifyOperatorID
        {
            get;
            set;
        }
    }
}
