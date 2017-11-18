/***************************************************************************
 * 
 *       功能：     系统_对象权限表实体类
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
	/// sysObjectPurview 
	/// </summary>
	[Serializable]
	public class sysObjectPurview
	{
		public sysObjectPurview()
		{
			
		}
		
		private int _objectid;
		private int _pageid;
		private long _purviewcode;

        private string _pagefilepath;
        private long _functioncount;

		///<sumary>
		/// 对象ID
		///</sumary>
		public int ObjectID
		{
			get{return _objectid;}
			set{_objectid = value;}
		}
		///<sumary>
		/// 页面ID
		///</sumary>
		public int PageID
		{
			get{return _pageid;}
			set{_pageid = value;}
		}
		///<sumary>
		/// 权限码
		///</sumary>
        public long PurviewCode
		{
			get{return _purviewcode;}
			set{_purviewcode = value;}
		}

        ///<sumary>
        /// 页面地址
        ///</sumary>
        public string PageFilePath
        {
            get { return _pagefilepath; }
            set { _pagefilepath = value; }
        }

        ///<sumary>
        /// 操作数
        ///</sumary>
        public long FunctionCount
        {
            get { return _functioncount; }
            set { _functioncount = value; }
        }
	}
}
