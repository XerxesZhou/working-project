/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     Jack
 *       日期：     2016/5/31
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain.Common
{
    using System;

    /// <summary>
    /// OperatorLog 
    /// </summary>
    [Serializable]
    public class OperatorLog
    {
        public OperatorLog()
        {

        }

        private int _olid;
        private int? _oloperatorid;
        private DateTime? _oldate;
        private string _olcontent;
        private string _olurl;
        private int? _olcustomerid;
        private int? _olbusinessid;
        private string _olfromip;
        private string _oloperatesource;

        ///<sumary>
        /// 
        ///</sumary>
        public int olID
        {
            get { return _olid; }
            set { _olid = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? olOperatorID
        {
            get { return _oloperatorid; }
            set { _oloperatorid = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? olDate
        {
            get { return _oldate; }
            set { _oldate = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string olContent
        {
            get { return _olcontent; }
            set { _olcontent = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string olURL
        {
            get { return _olurl; }
            set { _olurl = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? olCustomerID
        {
            get { return _olcustomerid; }
            set { _olcustomerid = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? olBusinessID
        {
            get { return _olbusinessid; }
            set { _olbusinessid = value; }
        }
        ///<sumary>
        /// IP地址
        ///</sumary>
        public string olFromIP
        {
            get { return _olfromip; }
            set { _olfromip = value; }
        }
        ///<sumary>
        /// 操作来源
        ///</sumary>
        public string olOperateSource
        {
            get { return _oloperatesource; }
            set { _oloperatesource = value; }
        }
    }
}
