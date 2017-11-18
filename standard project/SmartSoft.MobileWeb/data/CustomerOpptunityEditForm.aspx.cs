using System;

using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb
{

    public partial class CustomerOpptunityEditForm : MobilePageBase
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
                        AddOperatorLog("查看客户资料 ID：" + rowID, rowID);
                        hfPCWebDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"];
                        hfKeyID.Value = rowID.ToString();
                }
                else
                {
                    initControls();
                }
            }
        }

        private void initControls()
        {
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSource(ddlcoPhaseID, "CD_defCustomerOpptunityPhase");
            InitDataSource(ddlcoStatusID, "CD_OpptunityStatus");
            InitDataSource(ddlcoOpptunitySourceID, "CD_defCustomerSourceType");
            InitDataSource(ddlcoOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcwOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcfhStatusID, "CD_defCustomerOpptunityPhase");
            InitDataSourceAddEmpty(ddlChangeOpptunityStatusID, "CD_OpptunityStatus");
            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
                this.ddlcoOperatorID.SelectedValue = CurrentOperatorID;
            }


            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
        }

        private void bindRecords()
        {
            DataTable  dt = DbHelperSQL.GetDataTable("select A.*,cusName,B.opeName as opeName,C.opeName as AddopeName,D.opeName as ModifyOperatorName from CustomerOpptunity A left outer join Operators B on A.coOperatorID=B.opeID left outer join Operators C on A.coAddOperatorID=C.opeID left outer join Operators D on A.coModifyOperatorID=D.opeID left outer join Customer CC ON A.coCustomerID=CC.cusID where coID=" + rowID + " order by coDate desc");
            this.repOpptunity.DataSource = dt;
            this.repOpptunity.DataBind();

            lblOpptunityName.Text = dt.Rows[0]["coName"].ToString();
            lblPhase.Text = GetPhaseName(dt.Rows[0]["coPhaseID"].ToString());
            lblcfhFromName.InnerHtml = "关联客户：<a data-ajax='false' href='CustomerEditForm.aspx?ID=" + dt.Rows[0]["coCustomerID"] + "'>" + dt.Rows[0]["cusName"] + "</a>";

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where cfhSource='Opptunity' and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
            this.repFollowHistory.DataSource = dt;
            this.repFollowHistory.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource='Opptunity' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
            this.repCustomerFollowPlan.DataSource = dt;
            this.repCustomerFollowPlan.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cwOperatorName,C.depName as cwOperatorDepartment from CoWorker A left outer join Operators B on A.cwOperatorID=B.opeID left outer join Department C on B.opeDepartmentID=C.depID where cwSource='Opptunity' and cwRelatedID=" + rowID);
            this.repCoWorker.DataSource = dt;
            this.repCoWorker.DataBind();

            hfcusID.Value = DbHelperSQL.GetSHSL("select coCustomerID from CustomerOpptunity where coID = " + rowID);
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