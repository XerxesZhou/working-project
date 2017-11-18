/***************************************************************************
 * 
 *       功能：     客户资料实体类
 *       作者：     Jack
 *       日期：     2016/6/14
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
    /// Customer 
    /// </summary>
    [Serializable]
    public class Customer
    {
        public Customer()
        {

        }

        ///<sumary>
        /// 客户ID
        ///</sumary>
        public int cusID
        {
            get;
            set;
        }
        ///<sumary>
        /// 客户编号
        ///</sumary>
        public string cusCN
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司名称
        ///</sumary>
        public string cusName
        {
            get;
            set;
        }
        ///<sumary>
        /// 英文名称
        ///</sumary>
        public string cusEnglishName
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司电话
        ///</sumary>
        public string cusTel
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司传真
        ///</sumary>
        public string cusFax
        {
            get;
            set;
        }
        ///<sumary>
        /// 电子邮件
        ///</sumary>
        public string cusEmail
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司网址
        ///</sumary>
        public string cusWebsite
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司地址
        ///</sumary>
        public string cusAddress
        {
            get;
            set;
        }
        ///<sumary>
        /// 区域名称
        ///</sumary>
        public int? cusAreaID
        {
            get;
            set;
        }
        ///<sumary>
        /// 客户类型
        ///</sumary>
        public int? cusKindID
        {
            get;
            set;
        }
        ///<sumary>
        /// 负责人
        ///</sumary>
        public int? cusOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 所属部门
        ///</sumary>
        public int? cusDepartmentID
        {
            get;
            set;
        }
        ///<sumary>
        /// 信息来源
        ///</sumary>
        public int? cusSourceID
        {
            get;
            set;
        }
        ///<sumary>
        /// 创建人
        ///</sumary>
        public int? cusAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 创建时间
        ///</sumary>
        public DateTime? cusAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改人
        ///</sumary>
        public int? cusModifyOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改时间
        ///</sumary>
        public DateTime? cusModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 公司简介
        ///</sumary>
        public string cusIntroduction
        {
            get;
            set;
        }
        ///<sumary>
        /// 备注
        ///</sumary>
        public string cusRemark
        {
            get;
            set;
        }
        ///<sumary>
        /// 证件类型
        ///</sumary>
        public int? cusCertificationType
        {
            get;
            set;
        }
        ///<sumary>
        /// 证件号码
        ///</sumary>
        public string cusCertificationNO
        {
            get;
            set;
        }
        ///<sumary>
        /// 联系人
        ///</sumary>
        public string cusContactor
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cusExtType1
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cusExtType2
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cusExtType3
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cusExtText1
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cusExtText2
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string cusExtText3
        {
            get;
            set;
        }
        ///<sumary>
        /// 经度
        ///</sumary>
        public double cusLongtitude
        {
            get;
            set;
        }
        ///<sumary>
        /// 纬度
        ///</sumary>
        public double cusLatitude
        {
            get;
            set;
        }
        ///<sumary>
        /// 最近跟进日期
        ///</sumary>
        public DateTime? cusLastFollowDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? cusExpiredDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? cusProtectDaysLeft
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? cusLastOrderDate
        {
            get;
            set;
        }

        public int? cusStatusID
        {
            get;
            set;
        }
    }
}
