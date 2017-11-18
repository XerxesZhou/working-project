/***************************************************************************
 * 
 *       功能：     系统_页面实体类
 *       作者：     LeoXiao
 *       日期：     2007-01-29
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
    /// sysPage 
    /// </summary>
    [Serializable]
    public class sysPage
    {
        public sysPage()
        {

        }

        private int _pageid;
        private string _pagename;
        private string _pagefilepath;
        private int _menuparentpageid;
        private int _menuorderby;
        private string _menuimagepath;
        private string _menuselectedimagepath;
        private int _toolbarparentpageid;
        private int _toolorderby;
        private string _toolbarimagepath;
        private string _toolbarselectedimagepath;
        private bool _ismenudirectory;
        private bool _istoolbardirectory;
        private bool _ismenu;
        private bool _istoolbar;
        private long _functioncount;
        private string _exefilepath;
        private string _formname;
        private string _pageno;

        private string _parentpagename;

        ///<sumary>
        /// 页面ID
        ///</sumary>
        public int PageID
        {
            get { return _pageid; }
            set { _pageid = value; }
        }
        ///<sumary>
        /// 页面名称
        ///</sumary>
        public string PageName
        {
            get { return _pagename; }
            set { _pagename = value; }
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
        /// 菜单父节点ID
        ///</sumary>
        public int MenuParentPageID
        {
            get { return _menuparentpageid; }
            set { _menuparentpageid = value; }
        }
        ///<sumary>
        /// 菜单排序
        ///</sumary>
        public int MenuOrderBy
        {
            get { return _menuorderby; }
            set { _menuorderby = value; }
        }
        ///<sumary>
        /// 菜单图片
        ///</sumary>
        public string MenuImagePath
        {
            get { return _menuimagepath; }
            set { _menuimagepath = value; }
        }
        ///<sumary>
        /// 菜单选择图片
        ///</sumary>
        public string MenuSelectedImagePath
        {
            get { return _menuselectedimagepath; }
            set { _menuselectedimagepath = value; }
        }
        ///<sumary>
        /// 工具栏父节点ID
        ///</sumary>
        public int ToolBarParentPageID
        {
            get { return _toolbarparentpageid; }
            set { _toolbarparentpageid = value; }
        }
        ///<sumary>
        /// 工具栏排序
        ///</sumary>
        public int ToolOrderBy
        {
            get { return _toolorderby; }
            set { _toolorderby = value; }
        }
        ///<sumary>
        /// 工具栏图片
        ///</sumary>
        public string ToolBarImagePath
        {
            get { return _toolbarimagepath; }
            set { _toolbarimagepath = value; }
        }
        ///<sumary>
        /// 工具栏选择图片
        ///</sumary>
        public string ToolBarSelectedImagePath
        {
            get { return _toolbarselectedimagepath; }
            set { _toolbarselectedimagepath = value; }
        }
        ///<sumary>
        /// 是否菜单节点
        ///</sumary>
        public bool IsMenuDirectory
        {
            get { return _ismenudirectory; }
            set { _ismenudirectory = value; }
        }
        ///<sumary>
        /// 是否工具栏节点
        ///</sumary>
        public bool IsToolBarDirectory
        {
            get { return _istoolbardirectory; }
            set { _istoolbardirectory = value; }
        }
        ///<sumary>
        /// 是否菜单
        ///</sumary>
        public bool IsMenu
        {
            get { return _ismenu; }
            set { _ismenu = value; }
        }
        ///<sumary>
        /// 是否工具栏
        ///</sumary>
        public bool IsToolBar
        {
            get { return _istoolbar; }
            set { _istoolbar = value; }
        }
        ///<sumary>
        /// 操作数
        ///</sumary>
        public long FunctionCount
        {
            get { return _functioncount; }
            set { _functioncount = value; }
        }

        public string ParentPageName
        {
            get { return _parentpagename; }
            set { _parentpagename = value; }
        }

        public string exeFilePath
        {
            get { return _exefilepath; }
            set { _exefilepath = value; }
        }

        public string FormName
        {
            get { return _formname; }
            set { _formname = value; }
        }

        /// <summary>
        /// 版面号
        /// </summary>
        public string PageNo
        {
            get { return _pageno; }
            set { _pageno = value; }
        }
    }
}
