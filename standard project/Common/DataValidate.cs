using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ZWL.Common
{
    /// <summary>
    /// DataValidate 的摘要说明
    /// </summary>
    public class DataValidate
    {
        public DataValidate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //单引号替换成双引号
        public static String GetQuotedString(string pStr)
        {
            return (pStr.Replace("'", "''"));
        }
        #region DataRow
        #region 验证数据类型
        /// <summary>
        /// 验证一个变量能否转换为字符串类型
        /// </summary>
        /// <param name="val">待验证变量</param>
        /// <returns>如果能,返回true;否则,返回false</returns>
        public static bool IsString(object val)
        {
            try
            {
                Convert.ToString(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDouble(object val)
        {
            try
            {
                Convert.ToDouble(val);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsInt(object val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDateTime(object val)
        {
            try
            {
                Convert.ToDateTime(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion 验证数据类型
        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为字符串类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.String.Empty</returns>
        public static string ValidateDataRow_S(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return row[colname].ToString();
            else
                return System.String.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static int ValidateDataRow_N(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToInt32(row[colname]);
            else
                return System.Int32.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为浮点数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Double.MinValue</returns>
        public static double ValidateDataRow_F(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToDouble(row[colname]);
            else
                return System.Double.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为时间类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.DateTime.MinValue;</returns>
        public static DateTime ValidateDataRow_T(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToDateTime(row[colname]);
            else
                return System.DateTime.MinValue;
        }
        #endregion DataRow

        #region DataReader

        /// <summary>
        /// 从SqlDataReader中安全获取数据
        /// </summary>
        /// <param name="reader">数据读取器SqlDataReader</param>
        /// <param name="colname">列名</param>
        /// <returns>列中的字符串数据，如果为空，则返回System.String.Empty</returns>
        public static string ValidateDataReader_S(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetString(reader.GetOrdinal(colname));
            else
                return System.String.Empty;
        }

        public static int ValidateDataReader_N(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetInt32(reader.GetOrdinal(colname));
            else
                return System.Int32.MinValue;
        }

        public static double ValidateDataReader_F(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetDouble(reader.GetOrdinal(colname));
            else
                return System.Double.MinValue;
        }

        public static DateTime ValidateDataReader_T(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetDateTime(reader.GetOrdinal(colname));
            else
                return System.DateTime.MinValue;
        }

        #endregion DataReader
    }
}
