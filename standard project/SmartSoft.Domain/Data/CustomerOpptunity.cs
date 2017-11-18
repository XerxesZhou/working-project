/***************************************************************************
 * 
 *       功能：     商机实体类
 *       作者：     洪月
 *       日期：     2016/7/26
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
    /// CustomerOpptunity 
    /// </summary>
    [Serializable]
    public class CustomerOpptunity
    {
        public CustomerOpptunity()
        {

        }

        ///<sumary>
        /// 
        ///</sumary>
        public int coID
        {
            get;
            set;
        }
        ///<sumary>
        /// 客户
        ///</sumary>
        public int? coCustomerID
        {
            get;
            set;
        }
        ///<sumary>
        /// 商机名称
        ///</sumary>
        public string coName
        {
            get;
            set;
        }
        ///<sumary>
        /// 负责人
        ///</sumary>
        public int? coOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 日期
        ///</sumary>
        public DateTime? coDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 预计签单日期
        ///</sumary>
        public DateTime? coPredictFinishDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 预计金额
        ///</sumary>
        public decimal? coPredictAmount
        {
            get;
            set;
        }
        ///<sumary>
        /// 需求
        ///</sumary>
        public string coRequirement
        {
            get;
            set;
        }
        ///<sumary>
        /// 决策流程
        ///</sumary>
        public string coDecisionFlow
        {
            get;
            set;
        }
        ///<sumary>
        /// 竞争对手
        ///</sumary>
        public string coCompetitors
        {
            get;
            set;
        }
        ///<sumary>
        /// 商机来源
        ///</sumary>
        public int? coOpptunitySourceID
        {
            get;
            set;
        }
        ///<sumary>
        /// 阶段
        ///</sumary>
        public int? coPhaseID
        {
            get;
            set;
        }
        ///<sumary>
        /// 状态
        ///</sumary>
        public int? coStatusID
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入时间
        ///</sumary>
        public DateTime? coAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入人
        ///</sumary>
        public int? coAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改时间
        ///</sumary>
        public DateTime? coModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改人
        ///</sumary>
        public int? coModifyOperatorID
        {
            get;
            set;
        }
    }
}
