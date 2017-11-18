using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;
using System.Configuration;

namespace SmartSoft.MobileWeb.data
{
    public partial class CustomerFeedbackEditForm : MobilePageBase
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
                initControls();
                bindData();
                string PCWebDomain = ConfigurationManager.AppSettings["PCWebDomain"];
                hfPCWebDomain.Value = PCWebDomain;
            }
        }

        private void initControls()
        {
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSource(ddlcfhStatusID, "CD_defCustomerFeedbackStatus");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
            }

            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
        }

        private void bindData()
        {
            InitDataSource(ddlcfbStatus, "CD_defCustomerFeedbackStatus");

            if (rowID.HasValue)
            {

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where cfhSource='Feedback' and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
                this.repFollowHistory.DataSource = dt;
                this.repFollowHistory.DataBind();


                dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource='Feedback' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
                this.repCustomerFollowPlan.DataSource = dt;
                this.repCustomerFollowPlan.DataBind();

                hfKeyID.Value = rowID.ToString();
                string sql = @"select A.*,B.cusName as cusName,C.Name as TypeName,D.opeName as opeName,E.sName as Status,F.opeName as FeedbackOperatorName from CustomerFeedback A 
left outer join Customer B on A.cfbCustomerID=B.cusID
left outer join defCommon C on A.cfbFeedbackTypeID =C.ID
left outer join Operators D on A.cfbHandleOperatorID=D.opeID
left outer join sdefCommon E on A.cfbStatusID=E.sID
left outer join Operators F on A.cfbOperatorID=F.opeID
where cfbID=" + rowID;

                DataRow dr = DbHelperSQL.GetDataRow(sql);
                if (dr != null)
                {
                    lblcfbCustomerID.InnerText = dr["cusName"] + "";
                    lblccName.Text = dr["cusName"] + "";
                    lblcfbFeedbackTypeID.InnerText = dr["TypeName"] + "";
                    lblccAddress.Text = dr["TypeName"] + "";
                    lblContent.InnerText = dr["cfbContent"] + "";
                    lblcfbHandleOperatorID.InnerText = dr["opeName"] + "";
                    lblccOperatorName.Text = dr["FeedbackOperatorName"] + "";
                    lblcfbEmail.InnerText = dr["cfbEmail"] + "";
                    lblcfbLinkman.InnerText = dr["cfbLinkman"] + "";
                    lblcfbOrderRelated.InnerText = dr["cfbOrderRelated"] + "";
                    lblcfbResult.InnerText = dr["cfbResult"] + "";
                    lblcfbStatusID.InnerText = dr["Status"] + "";
                    lblcfbTelephone.InnerText = dr["cfbTelephone"] + "";
                }
            }
        }

        public string GetEachFile(string filePath, string cfhID)
        {
            return CommonFunction.GetEachFile(filePath);
        }
    }
}