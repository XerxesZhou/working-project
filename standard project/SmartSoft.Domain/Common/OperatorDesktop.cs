/***************************************************************************
 * 
 *       功能：     用户桌面设置表实体类
 *       作者：     Jack
 *       日期：     2016/6/1
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
    /// OperatorDesktop 
    /// </summary>
    [Serializable]
    public class OperatorDesktop
    {
        public OperatorDesktop()
        {

        }

        ///<sumary>
        /// 
        ///</sumary>
        public int odtID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? odtOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public string odtModelName
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? odtLookNum
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public int? odtOrderBy
        {
            get;
            set;
        }
        ///<sumary>
        /// 
        ///</sumary>
        public DateTime? odtDate
        {
            get;
            set;
        }
    }
}
