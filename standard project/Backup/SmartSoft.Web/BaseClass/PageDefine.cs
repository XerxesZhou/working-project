/***************************************************************************
 * 
 *       功能：     用于主界面的自定义
 *       作者：     彭伟
 *       日期：     2007/01/22
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/

namespace SmartSoft.Web.BaseClass
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Xml;
    using System.Collections;
    using System.Collections.Generic;

    public class PageDefine
    {
        public PageDefine()
        { 
            
        }

        public IList<Pages> GetDefinePages()
        {
            IList<Pages> pagelist = null;

            XmlDocument xdoc = new XmlDocument();
            string configPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PageConfig"]);
            string PageDefinePath = configPath + "/PageDefine.xml";

            try
            {
                xdoc.Load(PageDefinePath);
            }
            catch (Exception e)
            {
                throw new ApplicationException(PageDefinePath + "文件不存在,加载XmlDocment出错,原因如下:" + e.Message);
            }

            XmlNodeList pageNodeList = xdoc.SelectNodes("Pages/page");
            foreach (XmlNode node in pageNodeList)
            {
                Pages page = new Pages();
                page.Id = node.Attributes["id"].Value;
                page.Name = node.Attributes["name"].Value;
                page.Src = node.Attributes["src"].Value;
                page.Align = node.Attributes["align"].Value;

                pagelist.Add(page);
            }


            return pagelist;
        }


        public Pages GetPageById(string id)
        {
            XmlDocument xdoc = new XmlDocument();
            string configPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PageConfig"]);
            string PageDefinePath = configPath + "/PageDefine.xml";

            try
            {
                xdoc.Load(PageDefinePath);
            }
            catch (Exception e)
            {
                throw new ApplicationException(PageDefinePath + "文件不存在,加载XmlDocment出错,原因如下:" + e.Message);
            }

            XmlNodeList pageNodeList = xdoc.SelectNodes("Pages/page");

            Pages page = new Pages();
           
            foreach (XmlNode node in pageNodeList)
            {
                if (node.Attributes["id"].Value == id)
                {
                    page.Id = node.Attributes["id"].Value;
                    page.Name = node.Attributes["name"].Value;
                    page.Src = node.Attributes["src"].Value;
                    page.Align = node.Attributes["align"].Value;
                }
            }

            return page;
        }
    }
}
