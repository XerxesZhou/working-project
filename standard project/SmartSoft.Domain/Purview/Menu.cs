/***************************************************************************
 * 
 *       功能：     菜单实体类
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
	/// Menu 
	/// </summary>
	[Serializable]
	public class Menu
	{
		public Menu()
		{
			
		}
		
		private int _menuid;
		private string _menuname;
		private int _menupageid;
		private int _menuparentid;
		private string _menuimageurl;
		private decimal _menuorderbyno;

        private string _menupagename;
        private string _menupageurl;
        private int _pagesortid;
		
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
		public string MenuName
		{
			get{return _menuname;}
			set{_menuname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public int MenuPageId
		{
			get{return _menupageid;}
			set{_menupageid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public int MenuParentId
		{
			get{return _menuparentid;}
			set{_menuparentid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public string MenuImageUrl
		{
			get{return _menuimageurl;}
			set{_menuimageurl = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        public decimal MenuOrderByNo
		{
			get{return _menuorderbyno;}
			set{_menuorderbyno = value;}
		}

        public string MenuPageName
        {
            get { return _menupagename; }
            set { _menupagename = value; }
        }

        public string MenuPageUrl
        {
            get { return _menupageurl; }
            set { _menupageurl = value; }
        }

        public int PageSortId
        {
            get { return _pagesortid; }
            set { _pagesortid = value; }
        }
	}
}
