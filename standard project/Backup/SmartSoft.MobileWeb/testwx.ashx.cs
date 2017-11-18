using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.IO;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.MessageHandlers;
namespace SmartSoft.MobileWeb
{
    /// <summary>
    /// testwx 的摘要说明
    /// </summary>
    public class testwx : System.Web.UI.Page
    {

        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        override public void  ProcessRequest(HttpContext context)
        {
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                
            }
            else
            {
                Auth();
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }

        /// <summary>
        /// 成为开发者的第一步，验证并相应服务器的数据
        /// </summary>
        private void Auth()
        {
            #region 获取关键参数
            string token = ConfigurationManager.AppSettings["CorpToken"];//从配置文件获取Token
            string encodingAESKey = ConfigurationManager.AppSettings["AppSecret"];//从配置文件获取EncodingAESKey
            string corpId = ConfigurationManager.AppSettings["CorpId"];//从配置文件获取corpId
            #endregion

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            string decryptEchoString = "";
            if (new CorpBasicApi().CheckSignature(token, signature, timestamp, nonce, corpId, encodingAESKey, echoString, ref decryptEchoString))
            {
                if (!string.IsNullOrEmpty(decryptEchoString))
                {
                    HttpContext.Current.Response.Write(decryptEchoString);
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}