/***************************************************************************
 * 
 *       功能：     系统操作功能实体类
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
	/// sysFunction 
	/// </summary>
	[Serializable]
	public class sysFunction
	{
		public sysFunction()
		{
			
		}
		
		private int _functionid;
		private string _functionname;
		private long _functionpurviewcode;
		private string _functioncode;
		private string _remark;
		
		///<sumary>
		/// 功能ID
		///</sumary>
		public int FunctionID
		{
			get{return _functionid;}
			set{_functionid = value;}
		}
		///<sumary>
		/// 功能名称
		///</sumary>
		public string FunctionName
		{
			get{return _functionname;}
			set{_functionname = value;}
		}
		///<sumary>
		/// 功能权限码
		///</sumary>
        public long FunctionPurviewCode
		{
			get{return _functionpurviewcode;}
			set{_functionpurviewcode = value;}
		}
		///<sumary>
		/// 功能代码
		///</sumary>
		public string FunctionCode
		{
			get{return _functioncode;}
			set{_functioncode = value;}
		}
		///<sumary>
		/// 备注
		///</sumary>
		public string Remark
		{
			get{return _remark;}
			set{_remark = value;}
		}
	}
}
