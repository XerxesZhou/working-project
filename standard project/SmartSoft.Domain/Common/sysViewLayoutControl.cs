/***************************************************************************
 * 
 *       功能：     系统_布局视图控件名实体类
 *       作者：     彭伟
 *       日期：     2007-4-5
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
	/// sysViewLayoutControl 
	/// </summary>
	[Serializable]
	public class sysViewLayoutControl
	{
		public sysViewLayoutControl()
		{
			
		}
		
		private int _controlid;
		private int _pageid;
		private string _controlname;
		private int _layoutorview;
		private int _tableid;
        private string _pagename;
        private string _tabletext;

        public string PageName
        {
            get { return _pagename; }
            set { _pagename = value; }
        }

        public string TableText
        {
            get { return _tabletext; }
            set { _tabletext = value; }
        }

		///<sumary>
		/// 控件ID
		///</sumary>
		public int ControlID
		{
			get{return _controlid;}
			set{_controlid = value;}
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
		/// 控件名称
		///</sumary>
		public string ControlName
		{
			get{return _controlname;}
			set{_controlname = value;}
		}
		///<sumary>
		/// 视图或布局
		///</sumary>
		public int LayoutOrView
		{
			get{return _layoutorview;}
			set{_layoutorview = value;}
		}
		///<sumary>
		/// 表ID
		///</sumary>
		public int TableID
		{
			get{return _tableid;}
			set{_tableid = value;}
		}
	}
}
