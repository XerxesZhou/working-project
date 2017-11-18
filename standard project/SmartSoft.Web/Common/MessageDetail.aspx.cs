using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SmartSoft.Component;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Domain.Common;
using SmartSoft.Presentation;

namespace SmartSoft.Web.Common
{
    public partial class MessageDetail : PageBase
    {
        public int ID
        {
            get
            {
                return int.Parse(Request.QueryString["ID"]);
            }
        }

        private IMessageService _message;
        public IMessageService message
        {
            set { _message = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sysMessage msg = _message.GetMessageDetail(this.ID);
                lt_Content.Text = msg.MessageContent;

                //…Ë÷√“—∂¡
                _message.InsertMessageReaded(this.ID, this.Operator.opeID);
            }
        }
    }
}
