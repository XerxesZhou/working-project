/***************************************************************************
 * 
 *       功能：     系统_用户表实体类
 *       作者：     彭伟
 *       日期：     2007-1-26
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain.Common
{
	using System;
    using System.Collections;
	
	/// <summary>
	/// Operators 
	/// </summary>
    [Serializable]
    public class Operators
    {
        public Operators()
        {

        }

        //add-in
        /// <summary>
        /// ID集合
        /// </summary>
        public string Ids
        {
            get;
            set;
        }

        public string DepartmentName
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int opeID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string opeName
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeStopTag
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeUseTag
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public bool opeIsAdmin
        {
            get;
            set;
        }
        ///<sumary>
        /// 1:仅个人，2：本部门，3：全公司
        ///</sumary>
        public int? opeDataRange
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string opePassword
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeDepartmentID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeLastLoginTime
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeLastModifyPasswordTime
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeLastActiveTime
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeModifyOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? opeEmployeeID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public bool opeIsCanLogin
        {
            get;
            set;
        }
        ///<sumary>
        /// 合同额
        ///</sumary>
        public decimal? opeOrderAmount
        {
            get;
            set;
        }
        ///<sumary>
        /// 收款额
        ///</sumary>
        public decimal? opeReceiptAmount
        {
            get;
            set;
        }
        ///<sumary>
        /// 手机
        ///</sumary>
        public string opeMobile
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string opeEmail
        {
            get;
            set;
        }
        ///<sumary>
        /// 头像
        ///</sumary>
        public string opeUrl
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? opeEnterDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public bool opeIsDeveloper
        {
            get;
            set;
        }

        public string opeDingDingUserID
        {
            get;
            set;
        }

        public string opeWeChatUserID
        {
            get;
            set;
        }
    }
}
