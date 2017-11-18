/***************************************************************************
 * 
 *       功能：     知识库实体类
 *       作者：     洪月
 *       日期：     2016/5/30
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
    /// Knowledge 
    /// </summary>
    [Serializable]
    public class Knowledge
    {
        public Knowledge()
        {

        }

        private int _knowid;
        private string _knowtheme;
        private string _knowcontent;
        private int? _knowtype;
        private int? _knowoperator;
        private DateTime? _knowoperatedate;
        private string _knowfilepath;

        ///<sumary>
        /// 知识ID
        ///</sumary>
        public int knowID
        {
            get { return _knowid; }
            set { _knowid = value; }
        }
        ///<sumary>
        /// 知识主题
        ///</sumary>
        public string knowTheme
        {
            get { return _knowtheme; }
            set { _knowtheme = value; }
        }
        ///<sumary>
        /// 知识内容
        ///</sumary>
        public string knowContent
        {
            get { return _knowcontent; }
            set { _knowcontent = value; }
        }
        ///<sumary>
        /// 知识类别
        ///</sumary>
        public int? knowType
        {
            get { return _knowtype; }
            set { _knowtype = value; }
        }
        ///<sumary>
        /// 操作人
        ///</sumary>
        public int? knowOperator
        {
            get { return _knowoperator; }
            set { _knowoperator = value; }
        }
        ///<sumary>
        /// 操作时间
        ///</sumary>
        public DateTime? knowOperateDate
        {
            get { return _knowoperatedate; }
            set { _knowoperatedate = value; }
        }
        ///<sumary>
        /// 文件
        ///</sumary>
        public string knowFilePath
        {
            get { return _knowfilepath; }
            set { _knowfilepath = value; }
        }
    }
}
