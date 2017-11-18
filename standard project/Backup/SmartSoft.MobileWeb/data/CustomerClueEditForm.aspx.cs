using System;

using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;
using System.Configuration;

namespace SmartSoft.MobileWeb
{

    public partial class CustomerClueEditForm : MobilePageBase
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
                        hfPCWebDomain.Value = ConfigurationManager.AppSettings["PCWebDomain"];
                        AddOperatorLog("查看客户资料 ID：" + rowID, rowID);
                }
                else
                {
                    initControls();
                }
            }
        }

        private void initControls()
        {
            InitDataSource(ddlccDepartmentID, "CD_Department");
            InitDataSource(ddlccOperatorID, "CD_Operators");
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");            
            InitDataSource(ddlChangeClueOperatorID, "CD_Operators");
            InitDataSource(ddlcfhStatusID, "CD_CustomerClueStatus");
            InitDataSource(ddlccActivityID, "CD_MarketingActivity");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
                this.ddlccOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlccDepartmentID.SelectedValue = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + CurrentOperatorID);
                
            }

            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
        }

        private void bindRecords()
        {

            DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where cfhSource='Clue' and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
            this.repFollowHistory.DataSource=dt;
            this.repFollowHistory.DataBind();


            dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource='Clue' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
            this.repCustomerFollowPlan.DataSource = dt;
            this.repCustomerFollowPlan.DataBind();

            dt = DbHelperSQL.GetDataTable(@"select A.*,B.opeName as OperatorName,C.opeName as AddOperatorName,D.depName as DepartmentName,E.sname as StatusName,F.maName as ActivityName from CustomerClue A 
left outer join Operators B on A.ccOperatorID=B.opeID
left outer join Operators C on A.ccAddOperatorID =C.opeID
left outer join Department D on A.ccDepartmentID=D.depID
left outer join sdefCommon E on A.ccStatusID=E.sID
left outer join MarketingActivity F on A.ccActivityID=F.maID  where ccID=" + rowID);
            if (dt != null && dt.Rows.Count == 1)
            {
                lblccName.Text = dt.Rows[0]["ccCustomerName"] + "-" + dt.Rows[0]["ccName"] + "";
                lblccAddress.Text = dt.Rows[0]["ccAddress"] + "";
                lblccOperatorName.Text = dt.Rows[0]["OperatorName"] + "";
                lbl_ccName.InnerText = dt.Rows[0]["ccName"] + "";
                lblccCustomerName.InnerText = dt.Rows[0]["ccCustomerName"] + "";
                lblccTel.InnerText = dt.Rows[0]["ccTel"] + "";
                linkccTel.HRef = "tel:" + dt.Rows[0]["ccTel"] + "";
                lblccMobile.InnerText = dt.Rows[0]["ccMobile"] + "";
                linkccMobile.HRef = "tel:" + dt.Rows[0]["ccMobile"] + "";
                lblStatus.InnerText = dt.Rows[0]["StatusName"] + "";
                lblccRemark.InnerText = dt.Rows[0]["ccRemark"] + "";
                lblccOperator.InnerText = dt.Rows[0]["OperatorName"] + "";
                lblccAddoperator.InnerText = dt.Rows[0]["AddOperatorName"] + "";
                lblccDepartment.InnerText = dt.Rows[0]["DepartmentName"] + "";
                lblccAddDate.InnerText = dt.Rows[0]["ccAddDate"] + "";
                lblccActivityName.InnerText = dt.Rows[0]["ActivityName"] + "";

                txtccName.Text = dt.Rows[0]["ccName"] + "";
                txtccCustomerName.Text = dt.Rows[0]["ccCustomerName"] + "";
                txtccTel.Text = dt.Rows[0]["ccTel"] + "";
                txtccMobile.Text = dt.Rows[0]["ccMobile"] + "";
                txtccAddress.Text = dt.Rows[0]["ccAddress"] + "";
                txtccRemark.Text = dt.Rows[0]["ccRemark"] + "";
                ddlccOperatorID.SelectedValue = dt.Rows[0]["ccOperatorID"] + "";
                ddlccDepartmentID.SelectedValue = dt.Rows[0]["ccDepartmentID"] + "";
                ddlccActivityID.SelectedValue = dt.Rows[0]["ccActivityID"] + "";
                
                this.hfKeyID.Value = rowID.ToString();
                this.hfCustomerClueStatusID.Value = dt.Rows[0]["ccStatusID"] + "";
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
            return CommonFunction.GetEachFile(filePath);
        }
    }
   
}