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
    /// CustomerClue 
    /// </summary>
    [Serializable]
    public class CustomerClue
    {
        public CustomerClue()
        {

        }

        ///<sumary>
        /// ID
        ///</sumary>
        public int ccID
        {
            get;
            set;
        }
        ///<sumary>
        /// 客户名称
        ///</sumary>
        public string ccCustomerName
        {
            get;
            set;
        }
        ///<sumary>
        /// 姓名
        ///</sumary>
        public string ccName
        {
            get;
            set;
        }
        ///<sumary>
        /// 电话
        ///</sumary>
        public string ccTel
        {
            get;
            set;
        }
        ///<sumary>
        /// 手机
        ///</sumary>
        public string ccMobile
        {
            get;
            set;
        }
        ///<sumary>
        /// 地址
        ///</sumary>
        public string ccAddress
        {
            get;
            set;
        }
        ///<sumary>
        /// 状态
        ///</sumary>
        public int? ccStatusID
        {
            get;
            set;
        }
        ///<sumary>
        /// 备注
        ///</sumary>
        public string ccRemark
        {
            get;
            set;
        }
        ///<sumary>
        /// 负责人
        ///</sumary>
        public int? ccOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 部门
        ///</sumary>
        public int? ccDepartmentID
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入人
        ///</sumary>
        public int? ccAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 录入时间
        ///</sumary>
        public DateTime? ccAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改人
        ///</sumary>
        public int? ccModifyOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改时间
        ///</sumary>
        public DateTime? ccModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 关联客户
        ///</sumary>
        public int? ccCustomerID
        {
            get;
            set;
        }
        ///<sumary>
        /// 相关活动
        ///</sumary>
        public int? ccActivityID
        {
            get;
            set;
        }
    }
}
