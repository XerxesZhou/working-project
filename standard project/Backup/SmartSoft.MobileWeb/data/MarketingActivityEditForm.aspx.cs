using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.AdvancedAPIs;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class MarketingActivityEditForm : MobilePageBase
    {
        private int _rowid = 0;
        public int rowID
        {
            get
            {
                if (_rowid > 0)
                {
                    return _rowid;
                }
                else
                {
                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                    {
                        return int.Parse(Request.QueryString["ID"]);
                    }
                }

                return 0;
            }
            set
            {
                _rowid = value;
            }
        }

        private string fromType
        {
            get
            {
                string sfromType = "";
                if (Request.QueryString["FromType"] != null && Request.QueryString["FromType"] != string.Empty)
                {
                    sfromType = Request.QueryString["FromType"].ToString();
                }
                return sfromType;
            }
        }

        private FormAction action
        {
            get
            {
                string sAction = string.Empty;
                if (Request.QueryString["Action"] != null && Request.QueryString["Action"] != string.Empty)
                {
                    sAction = Request.QueryString["Action"].ToString();
                }
                return FormActionClass.GetFormAction(sAction);
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                if (rowID > 0)
                {
                    initControls();
                    bindRecords();
                    AddOperatorLog("查看市场活动资料 ID：" + rowID, rowID);
                    string PCWebDomain = ConfigurationManager.AppSettings["PCWebDomain"];
                    hfPCWebDomain.Value = PCWebDomain;
                }
                else
                {
                    initControls();
                }
            }
        }

        private void initControls()
        {
            InitDataSource(ddlmaDepartmentID, "CD_Department");
            InitDataSource(ddlmaOperatorID, "CD_Operators");
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSource(ddlcfhStatusID, "CD_defMarketingActivityStatus");
            InitDataSource(ddlmaTypeID, "CD_defMarketActivityType");
            InitDataSource(ddlccDepartmentID, "CD_Department");
            InitDataSource(ddlccOperatorID, "CD_Operators");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
                this.ddlmaOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlmaDepartmentID.SelectedValue = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + CurrentOperatorID);

            }

            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
        }

        private void bindRecords()
        {

            DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where cfhSource='MarketingActivity' and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
            this.repFollowHistory.DataSource = dt;
            this.repFollowHistory.DataBind();

            dt = DbHelperSQL.GetDataTable(@"select A.*,B.sName as ccStatusName,C.opeName as ccOperator,D.depName as ccDepartment,E.opeName as ccAddoperator from CustomerClue A 
left outer join sdefCommon B on A.ccStatusID=B.sID and sTypeName='CustomerClueStatus'
left outer join Operators C on A.ccOperatorID =C.opeID
left outer join Department D on A.ccDepartmentID=D.depID
left outer join Operators E on A.ccAddOperatorID=E.opeID 
where ccActivityID=" + rowID);
            this.repCustomerClue.DataSource = dt;
            this.repCustomerClue.DataBind();

            dt = DbHelperSQL.GetDataTable(@"select A.*,B.Name as TypeName,C.sName as StatusName,
D.opeName as opeName,
E.depName as depName from MarketingActivity A 
left outer join defCommon B on A.maTypeID=B.ID
left outer join sdefCommon C on A.maStatusID=C.sID and sTypeName='defMarketingActivityStatus'
left outer join Operators D on A.maOperatorID=D.opeID
left outer join Department E on A.maDepartmentID=E.depID where maID=" + rowID);
            if (dt != null && dt.Rows.Count == 1)
            {
                lblccName.Text = dt.Rows[0]["maName"] + "";
                lblccAddress.Text = dt.Rows[0]["TypeName"] + "";
                lblccOperatorName.Text = dt.Rows[0]["opeName"] + "";
                lblmaName.InnerText = dt.Rows[0]["maName"] + "";
                lblmaStartDate.InnerText = dt.Rows[0]["maStartDate"] + "";
                lblmaEndDate.InnerText = dt.Rows[0]["maEndDate"] + "";
                lblmaTypeName.InnerText = dt.Rows[0]["TypeName"] + "";
                lblmaStatusName.InnerText = dt.Rows[0]["StatusName"] + "";
                lblmaDescription.InnerText = dt.Rows[0]["maDescription"] + "";
                lblmaPredictCost.InnerText = dt.Rows[0]["maPredictCost"] + "";
                lblmaPredictAmount.InnerText = dt.Rows[0]["maPredictAmount"] + "";
                lblmaActualCost.InnerText = dt.Rows[0]["maActualCost"] + "";
                lblmaPredictNum.InnerText = dt.Rows[0]["maPredictNum"] + "";
                lblmaActualNum.InnerText = dt.Rows[0]["maActualNum"] + "";
                lblmaRemark.InnerText = dt.Rows[0]["maRemark"] + "";
                lblmaDepartmentID.InnerText = dt.Rows[0]["depName"] + "";
                lblmaOperator.InnerText = dt.Rows[0]["opeName"] + "";

                txtmaName.Text = dt.Rows[0]["maName"] + "";
                ddlmaTypeID.SelectedValue = dt.Rows[0]["maTypeID"] + "";
                txtmaDescription.Text = dt.Rows[0]["maDescription"] + "";
                txtmaPredictCost.Text = dt.Rows[0]["maPredictCost"] + "";
                txtmaPredictAmount.Text = dt.Rows[0]["maPredictAmount"] + "";
                txtmaActualCost.Text = dt.Rows[0]["maActualCost"] + "";
                txtmaPredictNum.Text = dt.Rows[0]["maPredictNum"] + "";
                txtmaActualNum.Text = dt.Rows[0]["maActualNum"] + "";
                txtmaRemark.Text = dt.Rows[0]["maRemark"] + "";
                ddlmaDepartmentID.SelectedValue = dt.Rows[0]["maDepartmentID"] + "";
                ddlmaOperatorID.SelectedValue = dt.Rows[0]["maOperatorID"] + "";

                this.hfKeyID.Value = rowID.ToString();

            }
            else
            {
                this.hfKeyID.Value = "0";
            }
        }

        public string GetPhaseName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                string name = DbHelperSQL.GetSHSL("select copName from defCustomerOpptunityPhase where copID=" + id);
                return name;
            }
        }

        public string GetTypeName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                string name = DbHelperSQL.GetSHSL("select Name from defCommon where ID=" + id);
                return name;
            }
        }

        public string GetSatusName(string id)
        {
            if (id != "")
            {
                string name = DbHelperSQL.GetSHSL("select sName from sdefCommon where  sTypeName='OpptunityStatus' and sID=" + id);
                return name;
            }
            else
            {
                return "";
            }
        }

        public string GetEachFile(string filePath, string cfhID)
        {
            return CommonFunction.GetEachFileForWeb(filePath);
        }
    }
   
}