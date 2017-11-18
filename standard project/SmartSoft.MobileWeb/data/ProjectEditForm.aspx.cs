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

namespace SmartSoft.MobileWeb.data
{
    public partial class ProjectEditForm : MobilePageBase
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
                    AddOperatorLog("查看报备资料 ID：" + rowID, rowID);
                    string PCWebDomain = ConfigurationManager.AppSettings["PCWebDomain"];
                    hfPCWebDomain.Value = PCWebDomain;

                    if (DbHelperSQL.Exists("select * from Project where pjID = " + rowID + " and pjToApproveOperatorID=" + CurrentOperatorID))
                    {
                        aApprove.Visible = true;
                        //已经审核
                        if (DbHelperSQL.Exists("select * from Project where pjApproveTag = '已审核' and pjID = " + rowID))
                        {
                            aApprove.InnerHtml = "反审";
                        }
                        else
                        {
                            aApprove.InnerHtml = "审核";
                        }
                    }
                    else
                    {
                        aApprove.Visible = false;
                    }

                }
                else
                {
                    initControls();
                }
            }
        }

        private void initControls()
        {
            //InitDataSource(ddlccDepartmentID, "CD_Department");
            InitDataSource(ddlpjOperatorID, "CD_Operators");
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSource(pjToApproveOperatorID, "CD_Operators");
            InitDataSource(ddlcfhStatusID, "CD_ProjectStatus");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
                //this.ddlccOperatorID.SelectedValue = CurrentOperatorID;
                //this.ddlccDepartmentID.SelectedValue = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + CurrentOperatorID);

            }
            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
        }

        private void bindRecords()
        {

            DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where cfhSource='Project' and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
            this.repFollowHistory.DataSource = dt;
            this.repFollowHistory.DataBind();


            dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource='Project' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
            this.repCustomerFollowPlan.DataSource = dt;
            this.repCustomerFollowPlan.DataBind();

            dt = DbHelperSQL.GetDataTable(@"select A.*,B.opeName as opeName,C.opeName as AddOperatorName,D.sName as StatusName,E.opeName as ApproveOperatorName,F.opeName as ToApproveOperatorName from Project A left outer join Operators B on A.pjOperatorID=B.opeID
left outer join Operators C on A.pjOperatorID=C.opeID
left outer join sdefCommon D on A.pjStatusID=D.sID and sTypeName='ProjectStatus'
left outer join Operators F on A.pjToApproveOperatorID=F.opeID
left outer join Operators E on A.pjApproveOperatorID=E.opeID where pjID=" + rowID);

            if (dt != null && dt.Rows.Count == 1)
            {
                string expiredDate = "";
                if (dt.Rows[0]["pjExpiredDate"] + "" != "")
                {
                    expiredDate = DateTime.Parse(dt.Rows[0]["pjExpiredDate"] + "").ToString("yyyy-MM-dd");
                }

                string TitleName = dt.Rows[0]["pjName"] + "-" + dt.Rows[0]["pjCompanyName"] + "";
                string SubTitleName = TitleName;
                if (TitleName.Length > 12)
                {
                    SubTitleName = TitleName.Substring(0, 12) + "...";
                }

                lblccName.Text = SubTitleName;
                lblccAddress.Text = dt.Rows[0]["pjNO"] + "";
                lblccOperatorName.Text = dt.Rows[0]["AddOperatorName"] + "";

                lblpjNO.InnerText = dt.Rows[0]["pjNO"] + "";
                lblpjName.InnerText = dt.Rows[0]["pjName"] + "";
                lblpjCompanyName.InnerText = dt.Rows[0]["pjCompanyName"] + "";
                lblpjDetail.InnerText = dt.Rows[0]["pjDetail"] + "";
                lblpjProduct.InnerText = dt.Rows[0]["pjProduct"] + "";
                lblpjAmount.InnerText = dt.Rows[0]["pjAmount"] + "";
                lblpjContactor.InnerText = dt.Rows[0]["pjContactor"] + "";
                lblpjPrice.InnerText = dt.Rows[0]["pjPrice"] + "";
                lblpjOperatorID.InnerText = dt.Rows[0]["opeName"] + "";
                lblpjExpired.InnerText = expiredDate;
                lblpjRemark.InnerText = dt.Rows[0]["pjRemark"] + "";
                lblpjStatusName.InnerText = dt.Rows[0]["StatusName"] + "";
                lblpjApproveOperatorName.InnerText = dt.Rows[0]["ApproveOperatorName"] + "";
                lblpjToApproveOperatorName.InnerText = dt.Rows[0]["ToApproveOperatorName"] + "";
                lblpjAddDate.InnerText = dt.Rows[0]["pjAddDate"] + "";
                lblpjApproveDate.InnerText = dt.Rows[0]["pjApproveDate"] + "";

                txtpjNO.Text = dt.Rows[0]["pjNO"] + "";
                txtpjName.Text = dt.Rows[0]["pjName"] + "";
                txtpjCompanyName.Text = dt.Rows[0]["pjCompanyName"] + "";
                txtpjDetail.Text = dt.Rows[0]["pjDetail"] + "";
                txtpjProduct.Text = dt.Rows[0]["pjProduct"] + "";
                txtpjAmount.Text = dt.Rows[0]["pjAmount"] + "";
                txtpjContactor.Text = dt.Rows[0]["pjContactor"] + "";
                txtpjPrice.Text = dt.Rows[0]["pjPrice"] + "";
                ddlpjOperatorID.SelectedValue = dt.Rows[0]["pjOperatorID"] + "";
                txtpjExpiredDate.Text = expiredDate;
                txtpjRemark.Text = dt.Rows[0]["pjRemark"] + "";

                this.hfKeyID.Value = rowID.ToString();
                //this.hfCustomerClueStatusID.Value = dt.Rows[0]["ccStatusID"] + "";
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