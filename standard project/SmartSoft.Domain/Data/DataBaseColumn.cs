using System;

namespace SmartSoft.Domain.Data
{
    /// <summary>
    /// 数据库内的系统表映射实体 
    /// </summary>
    [Serializable]
    public class DataBaseColumn
    {
        private int orderby;
        /// <summary>
        /// 序号
        /// </summary>
        public int OrderBy
        {
            get { return orderby; }
            set { orderby = value; }
        }

        private string columnname;
        ///<sumary>
        /// 字段名
        ///</sumary>
        public string ColumnName
        {
            get { return columnname; }
            set { columnname = value; }
        }

        private string description;
        ///<sumary>
        /// 字段备注，初始化时可当做字段名称
        ///</sumary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string datatype;
        ///<sumary>
        /// 字段类型
        ///</sumary>
        public string DataType
        {
            get { return datatype; }
            set { datatype = value; }
        }

        private bool isprimary;
        ///<sumary>
        /// 是否主键
        ///</sumary>
        public bool IsPrimary
        {
            get { return isprimary; }
            set { isprimary = value; }
        }

        private bool isnullable;
        ///<sumary>
        /// 是否可空
        ///</sumary>
        public bool IsNullable
        {
            get { return isnullable; }
            set { isnullable = value; }
        }
    }
}

