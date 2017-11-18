/***************************************************************************
 * 
 *       功能：     系统消息实体类
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
	/// sysMessage 
	/// </summary>
	[Serializable]
	public class sysMessage
	{
		public sysMessage()
		{
			
		}
		
		private int _messageid;
		private int _messagetypeid;
		private int _sendoperatorid;
		private DateTime _sendtime;
		private string _messagecontent;
		private DateTime? _awoketime;
		private string _url;
        private string _messagetype;
        private string _sendoperatorname;
        private string _rowid;
        private bool _isread;

        private string _title;
		
		///<sumary>
		/// 消息ID
		///</sumary>
		public int MessageID
		{
			get{return _messageid;}
			set{_messageid = value;}
		}
		///<sumary>
		/// 消息类型ID
		///</sumary>
		public int MessageTypeID
		{
			get{return _messagetypeid;}
			set{_messagetypeid = value;}
		}
		///<sumary>
		/// 发送人ID
		///</sumary>
		public int SendOperatorID
		{
			get{return _sendoperatorid;}
			set{_sendoperatorid = value;}
		}
		///<sumary>
		/// 发送时间
		///</sumary>
		public DateTime SendTime
		{
			get{return _sendtime;}
			set{_sendtime = value;}
		}
		///<sumary>
		/// 消息内容
		///</sumary>
		public string MessageContent
		{
			get{return _messagecontent;}
			set{_messagecontent = value;}
		}
		///<sumary>
		/// 提醒时间
		///</sumary>
		public DateTime? AwokeTime
		{
			get{return _awoketime;}
			set{_awoketime = value;}
		}
		///<sumary>
		/// URL
		///</sumary>
		public string URL
		{
			get{return _url;}
			set{_url = value;}
		}

        public string MessageType
        {
            get { return _messagetype; }
            set { _messagetype = value; }
        }

        public string SendOperatorName
        {
            get { return _sendoperatorname; }
            set { _sendoperatorname = value; }
        }

        public bool IsRead
        {
            get { return _isread; }
            set { _isread = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string RowID
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
	}
}
