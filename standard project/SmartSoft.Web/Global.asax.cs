namespace SmartSoft.Web
{
    using System;
    using System.Configuration;


    using Castle.Windsor;

    using SmartSoft.Component;

    using TotalBase.Ikey;
    using System.Management;

    public class Global : System.Web.HttpApplication, IContainerAccessor
    {
        private static WindsorContainer container;

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                container = new TopvisionContainer();
                initSecurity();
                /*string userCountKey = ConfigurationManager.AppSettings["UserCountKey"].ToString();
                //用默认密匙给拥护数密匙解密
                userCountKey = Security.DecryptDES(userCountKey);

                string userCount;

                //设置为0 表示从config文件中读加密信息
                if (ConfigurationManager.AppSettings["UseServerDog"].ToString() == "0")
                {
                    userCount = Security.DecryptDES(ConfigurationManager.AppSettings["UserCount"].ToString(), userCountKey);
                }
                else
                {
                    //读取ikey中的密匙进行字符串的解密
                    ServerDog _currentKey = new ServerDog();
                    //用解密后的密匙给加密狗用的用户数密文解密
                    userCount = Security.DecryptDES(_currentKey.UserCount, userCountKey);
                }

                Application["AllowedUserCount"] = userCount;*/
            }
            catch (Exception ex)
            {
            }
        }

 
        protected void Application_End(object sender, EventArgs e)
        {
            container.Dispose();
        }

        public void Application_Error(Object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError().GetBaseException();
        }

        #region IContainerAccessor Members

        public IWindsorContainer Container
        {
            get { return container; }
        }

        #endregion

        private void initSecurity()
        {
            string information = DbHelperSQL.GetSHSL("select top 1 USBKEY from sysConfig");
            Application["key"] = information;
            if (!string.IsNullOrEmpty(information))
            {
                string splitor = "$|@";
                string details = Security.DecryptDES(information);
                string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                if (items.Length == 3)
                {
                    Application["UserUnique"] = items[0];
                    Application["AllowedUserCount"] = items[1];
                    Application["ExpireDate"] = items[2];
                }
            }
        }
    }
}