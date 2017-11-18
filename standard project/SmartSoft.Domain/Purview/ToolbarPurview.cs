/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     彭伟
 *       日期：     2007-2-2
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain.Purview
{
	using System;
	
	/// <summary>
	/// ToolbarPurview 
	/// </summary>
	[Serializable]
	public class ToolbarPurview
	{
		public ToolbarPurview()
		{
			
		}
		
		private int _toolbarid;
		private int _objectid;
		
		///<sumary>
		/// 
		///</sumary>
		public int ToolbarId
		{
			get{return _toolbarid;}
			set{_toolbarid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public int ObjectId
		{
			get{return _objectid;}
			set{_objectid = value;}
		}
	}
}
