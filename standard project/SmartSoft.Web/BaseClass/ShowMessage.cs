using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Caching;
using System.Xml;
using System.Collections.Generic;
using System.Web.SessionState;
using SmartSoft.Component;
using SmartSoft.Domain.Common;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Service.Interface.Common;

namespace SmartSoft.Web.BaseClass
{
    public class ShowMessage : IHttpHandler,IReadOnlySessionState,IRequiresSessionState
    {
        private static IMessageService _message = (new TopvisionContainer())["message"] as IMessageService;

        XmlDocument xmlDocument = new XmlDocument();

        int pageSize = 10;

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                InitUserOnlineDoc(context);

                context.Response.ContentType = "text/xml";
                context.Response.Write(xmlDocument.OuterXml);

                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(5));
                context.Response.Cache.SetAllowResponseInBrowserHistory(false);
                context.Response.Cache.SetValidUntilExpires(true);
            }
            catch (Exception ex)
            {

            }
        }

        private void InitUserOnlineDoc(HttpContext context)
        {
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", null, null);
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);

            XmlElement rootElement = xmlDocument.CreateElement("UserOnline");
            xmlDocument.AppendChild(rootElement);

            XmlElement postsElement = xmlDocument.CreateElement("Posts");
            rootElement.AppendChild(postsElement);


            string ip = HttpContext.Current.Request.UserHostAddress;

            //得到操作员Id
            int operatorId = this.GetOperator().opeID;

            //得到我的弹出信息
            IList<sysMessage> messageList = _message.SelectShowMessageList(operatorId);

            foreach (sysMessage message in messageList)
            {
                XmlElement PostElement = xmlDocument.CreateElement("Post");

                XmlElement PostIDElement = xmlDocument.CreateElement("PostID");
                PostIDElement.InnerText = message.MessageID.ToString();
                PostElement.AppendChild(PostIDElement);

                //添加主题
                XmlElement SubjectElement = xmlDocument.CreateElement("Subject");
                SubjectElement.InnerText = message.MessageType;
                PostElement.AppendChild(SubjectElement);

                //添加主体摘要
                XmlElement BodySummaryElement = xmlDocument.CreateElement("BodySummary");
                BodySummaryElement.InnerText = message.Title;
                PostElement.AppendChild(BodySummaryElement);

                //添加作者
                XmlElement AuthorElement = xmlDocument.CreateElement("Author");
                AuthorElement.InnerText = message.SendOperatorName;
                PostElement.AppendChild(AuthorElement);

                //添加链接地址
                XmlElement UrlElement = xmlDocument.CreateElement("URL");
                string messageURL = message.URL;
                if (string.IsNullOrEmpty(messageURL))
                {
                    messageURL = "Common/MessageDetail.aspx?ID=" + message.MessageID;
                }
                else
                {
                    messageURL = message.URL;
                    if (!messageURL.Contains("MessageID="))
                    {
                        messageURL += "&MessageID=" + message.MessageID;
                    }
                }

                UrlElement.InnerText = messageURL;

                PostElement.AppendChild(UrlElement);

                postsElement.AppendChild(PostElement);
            }

            rootElement.AppendChild(postsElement);

            xmlDocument.AppendChild(rootElement);
        }

        #region GetUrl
        //private string GetUrl(sysMessage message)
        //{
        //    string url = string.Empty;
        //    if (message.BillTypeID.HasValue && message.BillID.HasValue)
        //    {
        //        if (message.BillTypeID.Value == (int)BillType.SellOrder)
        //        {
        //            url = "Sell/SellOrderEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.StockOut)
        //        {
        //            url = "Inventory/SellStockOutEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.StockIn)
        //        {
        //            url = "Inventory/PurchaseStockInEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.SellQuote)
        //        {
        //            url = "Sell/SellQuoteEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.PurchaseQuote)
        //        {
        //            url = "Purchase/PurchaseQuoteEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.PurchaseOrder)
        //        {
        //            url = "Purchase/PurchaseOrderEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.SellReturn)
        //        {
        //            url = "Sell/SellReturnEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.PurchaseReturn)
        //        {
        //            url = "Purchase/PurchaseReturnEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.StockTransfer)
        //        {
        //            url = "Inventory/ProductTransferEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.CustomerTradeTerm)
        //        {
        //            url = "Data/CustomerTradeTermEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.SupplierTradeTerm)
        //        {
        //            url = "Data/SupplierTradeTermEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.CustomerInvoice)
        //        {
        //            url = "Finance/CustomerInvoiceEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //        if (message.BillTypeID.Value == (int)BillType.SupplierInvoice)
        //        {
        //            url = "Finance/SupplierInvoiceEditForm.aspx?Action=View&ID=" + message.BillID.Value + "&MessageID=" + message.MessageID;
        //        }
        //    }

        //    return url;
        //}
        #endregion

        /// <summary>
        /// 从Session中得到操作员信息
        /// </summary>
        /// <returns></returns>
        private Operators GetOperator()
        {
            Operators op = new Operators();
            if (HttpContext.Current.Session["Operator"] != null)
            {
                op = HttpContext.Current.Session["Operator"] as Operators;
            }
            else
            {
                //op.opeID = 1;
                //op.opeIsAdmin = true;
                HttpContext.Current.Response.Redirect("~/login.aspx");
            }

            return op;
        }
    }
}
