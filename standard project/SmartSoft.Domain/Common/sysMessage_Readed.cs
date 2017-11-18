/***************************************************************************
 * 
 *       功能：     消息已读表实体类
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
	/// sysMessage_Readed 
	/// </summary>
	[Serializable]
	public class sysMessage_Readed
	{
		public sysMessage_Readed()
		{
			
		}
		
		private int _messageid;
		private int _operatorid;
		
		///<sumary>
		/// 消息ID
		///</sumary>
		public int MessageID
		{
			get{return _messageid;}
			set{_messageid = value;}
		}
		///<sumary>
		/// 操作员ID
		///</sumary>
		public int OperatorID
		{
			get{return _operatorid;}
			set{_operatorid = value;}
		}
	}
}
