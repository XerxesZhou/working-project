/***************************************************************************
 * 
 *       功能：     系统_日志实体类
 *       作者：     彭伟
 *       日期：     2007-6-11
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
	/// sysLog 
	/// </summary>
	[Serializable]
	public class sysLog
	{
		public sysLog()
		{
			
		}
		
		private int _logid;
		private DateTime _logtime;
		private int _operatorid;
		private string _ip;
		private string _action;
		private string _keywords;
		private string _pageurl;
		private string _tablename;
		
		///<sumary>
		/// 日志ID
		///</sumary>
		public int LogID
		{
			get{return _logid;}
			set{_logid = value;}
		}
		///<sumary>
		/// 记录时间
		///</sumary>
		public DateTime LogTime
		{
			get{return _logtime;}
			set{_logtime = value;}
		}
		///<sumary>
		/// 操作人ID
		///</sumary>
		public int OperatorID
		{
			get{return _operatorid;}
			set{_operatorid = value;}
		}
		///<sumary>
		/// 操作人IP
		///</sumary>
		public string IP
		{
			get{return _ip;}
			set{_ip = value;}
		}
		///<sumary>
		/// 动作
		///</sumary>
		public string Action
		{
			get{return _action;}
			set{_action = value;}
		}
		///<sumary>
		/// 关键字
		///</sumary>
		public string Keywords
		{
			get{return _keywords;}
			set{_keywords = value;}
		}
		///<sumary>
		/// 页面
		///</sumary>
		public string PageUrl
		{
			get{return _pageurl;}
			set{_pageurl = value;}
		}
		///<sumary>
		/// 数据库表名
		///</sumary>
		public string TableName
		{
			get{return _tablename;}
			set{_tablename = value;}
		}
	}
}
