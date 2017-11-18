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
using System.Collections.Generic;

using UDEF.Component;
using UDEF.Service;
using UDEF.Domain;
using UDEF.Domain.Enumerate;

using SmartSoft.Presentation;
using SmartSoft.Domain.Common;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Domain.Data;
using SmartSoft.Service;

namespace SmartSoft.Web
{
    public partial class center : PageBase
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private IMessageService _message;
        public IMessageService message
        {
            set { _message = value; }
        }

        private BaseService _baseservice;
        public BaseService baseservice
        {
            set { _baseservice = value; }
        }


        private int PageSize
        {
            get
            {
                int result = 10;
                try
                {
                    result = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSizeOnIndex"].ToString());
                }
                catch
                {}

                return result;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindMessage();
                this.BindFair();
                this.BindFollowWorkFlow();
            }
        }

        private void BindMessage()
        {
            GridMessage.PageSize = this.PageSize;
            IList<sysMessage> msgList = _message.SelectReceiveMessageListByCount(this.PageSize, this.Operator.opeID);

            GridMessage.DataSource = msgList;
            GridMessage.DataBind();
        }

        /*
        private void BindCalendar()
        {
            GridCalendar.PageSize = this.PageSize;
            IList<SmartSoft.Domain.Common.Calendar> list = _calendar.SelectCalendarByCount(this.PageSize, this.Operator.opeID);

            GridCalendar.DataSource = list;
            GridCalendar.DataBind();
        }*/

        private void BindFair()
        {
            //GridFair.PageSize = this.PageSize;
            //IList<SmartSoft.Domain.Data.Fair> list = _baseservice.SelectDynamic<Fair>(" AND fStartDate > getdate()", " fStartDate ASC");
            //GridFair.DataSource = list;
            //GridFair.DataBind();
        }

        private void BindFollowWorkFlow()
        {
            //GridFollowingWorkFlow.PageSize = this.PageSize;
            //IList<FollowingWorkFlow> list = _baseservice.SelectDynamic<FollowingWorkFlow>("", " fwfOrderNO asc");

            //GridFollowingWorkFlow.DataSource = list;
            //GridFollowingWorkFlow.DataBind();
        }

    }
}
