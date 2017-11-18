/***************************************************************************
 * 
 *       功能：     消息查看人表实体类
 *       作者：     彭伟
 *       日期：     2007-6-8
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
	/// sysMessage_Looker 
	/// </summary>
	[Serializable]
	public class sysMessage_Looker
	{
		public sysMessage_Looker()
		{
			
		}
		
		private int _messageid;
		private int _objectid;
		
		///<sumary>
		/// 消息ID
		///</sumary>
		public int MessageID
		{
			get{return _messageid;}
			set{_messageid = value;}
		}
		///<sumary>
		/// 对象ID
		///</sumary>
		public int ObjectID
		{
			get{return _objectid;}
			set{_objectid = value;}
		}
	}
}
