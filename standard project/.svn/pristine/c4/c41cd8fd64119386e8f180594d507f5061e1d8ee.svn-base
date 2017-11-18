/***************************************************************************
 * 
 *       功能：     部门资料实体类
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
	
	/// <summary>
	/// Department 
	/// </summary>
    [Serializable]
    public class Department
    {
        public Department()
        {

        }

        private int _depid;
        private string _depname;
        private string _depchargeman;
        private string _deptel;
        private string _depfax;
        private string _depremark;
        private int _depparentid;

        ///<sumary>
        /// 部门编码
        ///</sumary>
        public int depID
        {
            get { return _depid; }
            set { _depid = value; }
        }
        ///<sumary>
        /// 部门名称
        ///</sumary>
        public string depName
        {
            get { return _depname; }
            set { _depname = value; }
        }
        ///<sumary>
        /// 部门负责人
        ///</sumary>
        public string depChargeMan
        {
            get { return _depchargeman; }
            set { _depchargeman = value; }
        }

        ///<sumary>
        /// 部门电话
        ///</sumary>
        public string depTel
        {
            get { return _deptel; }
            set { _deptel = value; }
        }


        ///<sumary>
        /// 部门传真
        ///</sumary>
        public string depFax
        {
            get { return _depfax; }
            set { _depfax = value; }
        }


        ///<sumary>
        /// 部门备注
        ///</sumary>
        public string depRemark
        {
            get { return _depremark; }
            set { _depremark = value; }
        }

        public decimal? depOrderAmount
        {
            get;
            set;
        }

        public decimal? depReceiptAmount
        {
            get;
            set;
        }

        ///<sumary>
        /// 部门上级目录
        ///</sumary>
        public int depParentID
        {
            get { return _depparentid; }
            set { _depparentid = value; }
        }
    }
}
