/***************************************************************************
 * 
 *       功能：     顶讯网络自己的容器 :) 
 *       作者：     彭伟
 *       日期：     2007-1-27
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Component
{
    using System.Configuration;

    using IBatisNet.DataMapper;

    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;

    using TotalBase.Ikey;
    using TotalBase.Common;

    public class TopvisionContainer : WindsorContainer
    {
        /// <summary>
        /// Constructor. The configuration is read from the web.config.
        /// </summary>
        public TopvisionContainer()
            : base(new XmlInterpreter())
        {
            //ConnectStringInfo curConnectString;
            ISqlMapper sqlMap = this["sqlServerSqlMap"] as ISqlMapper;
            string connString = ConfigurationManager.AppSettings["ConnectString"].ToString();
            sqlMap.DataSource.ConnectionString = connString;

            /*
            string isDebug = ConfigurationManager.AppSettings["Debug"];
            if (isDebug == "True")
            {
                
            }
            else
            {
                //设置为0 表示从config文件中读加密信息
                if (ConfigurationManager.AppSettings["UseServerDog"].ToString() == "0")
                {
                    curConnectString = new ConnectStringInfo(connString, ConfigurationManager.AppSettings["ConnectStringKey"].ToString());
                }
                else
                {
                    //读取ikey中的密匙进行字符串的解密
                    ServerDog _currentKey = new ServerDog();
                    curConnectString = new ConnectStringInfo(connString, _currentKey.ConnectStringKey);
                }
                sqlMap.DataSource.ConnectionString = curConnectString.RawConnectString;
                
            }*/
        }
    }
}
