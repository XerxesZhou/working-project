/***************************************************************************
 * 
 *       功能：     工作日志表实体类
 *       作者：     洪月
 *       日期：     2016/9/8
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Domain.Data
{
    using System;

    /// <summary>
    /// WorkDiary 
    /// </summary>
    [Serializable]
    public class WorkDiary
    {
        public WorkDiary()
        {

        }

        ///<sumary>
        /// 
        ///</sumary>
        public int wdID
        {
            get;
            set;
        }
        ///<sumary>
        /// 日志标题
        ///</sumary>
        public string wdTitle
        {
            get;
            set;
        }
        ///<sumary>
        /// 日志时间
        ///</sumary>
        public DateTime? wdDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 日志内容
        ///</sumary>
        public string wdContent
        {
            get;
            set;
        }
        ///<sumary>
        /// 创建时间
        ///</sumary>
        public DateTime? wdAddDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 创建人
        ///</sumary>
        public int? wdAddOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改时间
        ///</sumary>
        public DateTime? wdModifyDate
        {
            get;
            set;
        }
        ///<sumary>
        /// 修改人
        ///</sumary>
        public int? wdModifyOperatorID
        {
            get;
            set;
        }
        ///<sumary>
        /// (1为日报，2为周报，3为月报)
        ///</sumary>
        public int? wdTypeID
        {
            get;
            set;
        }
        ///<sumary>
        /// 文件
        ///</sumary>
        public string wdFile
         {
            get;
            set;
        }
    }
}
