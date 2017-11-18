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
	/// MenuPurview 
	/// </summary>
	[Serializable]
	public class MenuPurview
	{
		public MenuPurview()
		{
			
		}
		
		private int _menuid;
		private int _objectid;
		
		///<sumary>
		/// 
		///</sumary>
		public int MenuId
		{
			get{return _menuid;}
			set{_menuid = value;}
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
