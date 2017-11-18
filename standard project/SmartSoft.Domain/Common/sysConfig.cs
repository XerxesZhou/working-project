/***************************************************************************
 * 
 *       功能：     系统配置表实体类
 *       作者：     彭伟
 *       日期：     2007-6-2
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
	/// sysConfig 
	/// </summary>
	[Serializable]
	public class sysConfig
	{
		public sysConfig()
		{
			
		}
		
		private int _operatorcount;
		private double _maxspace;
		private DateTime _validdate;
		private int? _awokedate;
		private string _usbkey;
		private bool _isstop;
        private string _databaseversion;
        private string _keys;
		
		///<sumary>
		/// 用户数
		///</sumary>
		public int OperatorCount
		{
			get{return _operatorcount;}
			set{_operatorcount = value;}
		}
		///<sumary>
		/// 空间大小
		///</sumary>
		public double MaxSpace
		{
			get{return _maxspace;}
			set{_maxspace = value;}
		}
		///<sumary>
		/// 有效期
		///</sumary>
		public DateTime ValidDate
		{
			get{return _validdate;}
			set{_validdate = value;}
		}
		///<sumary>
		/// 提前提醒时间
		///</sumary>
		public int? AwokeDate
		{
			get{return _awokedate;}
			set{_awokedate = value;}
		}
		///<sumary>
		/// USBKEY
		///</sumary>
		public string USBKEY
		{
			get{return _usbkey;}
			set{_usbkey = value;}
		}
		///<sumary>
		/// 是否停用
		///</sumary>
		public bool isStop
		{
			get{return _isstop;}
			set{_isstop = value;}
		}

        /// <summary>
        /// 数据库版本
        /// </summary>
        public string DataBaseVersion
        {
            get { return _databaseversion; }
            set { _databaseversion = value; }
        }

        /// <summary>
        /// 密匙
        /// </summary>
        public string Keys
        {
            get { return _keys; }
            set { _keys = value; }
        }
	}
}
