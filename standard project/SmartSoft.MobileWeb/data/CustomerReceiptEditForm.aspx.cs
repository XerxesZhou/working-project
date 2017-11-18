using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb.data
{
    public partial class CustomerReceiptEditForm : MobilePageBase
    {
        public int? rowID
        {
            get
            {
                if (Request.QueryString["ID"] + "" != "")
                {
                    return int.Parse(Request.QueryString["ID"] + "");
                }
                return null;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                bindData();
            }
        }

        private void bindData()
        {
            if (rowID.HasValue)
            {
                string sql = @"select A.*,B.cbName as cbName,C.opeName as opeName from CustomerReceipt A
left outer join CustomerBusiness B on A.crBusinessID=b.cbID
left outer join Operators C on A.crOperatorID =C.opeID where crID=" + rowID;
                DataRow dr = DbHelperSQL.GetDataRow(sql);
                if (dr != null)
                {
                    lblcrBusinessName.InnerText = dr["cbName"] + "";
                    lblcrAmount.InnerText = dr["crAmount"] + "";
                    lblcrOperatorName.InnerText = dr["opeName"] + "";
                    lblcrDate.InnerText = dr["crDate"] + "";
                }
            }
        }
    }
}