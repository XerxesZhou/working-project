namespace SmartSoft.Web.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using SmartSoft.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Presentation;
    using SmartSoft.Service;
    using SmartSoft.Service.Interface.Common;
    using UDEF.Component;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Service;

    public partial class CustomerBusinessEditForm : PageBaseEdit
    {
        private int _rowid = 0;
        public new int rowID
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
                        if (fromType == "receipt")
                        {
                            string crBusinessID = DbHelperSQL.GetSHSL("select crBusinessID from CustomerReceipt where crID = " + Request.QueryString["ID"]);
                            return int.Parse(crBusinessID);
                        }
                        else if (fromType == "receiptplan")
                        {
                            string crpBusinessID = DbHelperSQL.GetSHSL("select crpBusinessID from CustomerReceiptPlan where crpID = " + Request.QueryString["ID"]);
                            return int.Parse(crpBusinessID);
                        }
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
                    AddOperatorLog("查看合同资料 ID：" + rowID, rowID);
                    string PCWebDomain = ConfigurationManager.AppSettings["PCWebDomain"];
                    hfPCWebDomain.Value = PCWebDomain;
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
            InitDataSourceAddEmpty(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSourceAddEmpty(ddlcwOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcbBusinessType, "CD_defCustomerBusinessType");
            InitDataSourceAddEmpty(ddlcbOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcbNotifyOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcrOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcrpOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlBusinessStatusID, "CD_defBusinessStatus");
            InitDataSourceAddEmpty(ddlcrTypeID, "CD_defCustomerReceiptType");
            InitDataSourceAddEmpty(ddlcrpTypeID, "CD_defCustomerReceiptType");
            InitDataSourceAddEmpty(ddlAfterSalesStatusID, "CD_defAfterStatus");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
            }

            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";

            if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
            {
                liOrder.Visible = true;
            }
            else
            {
                liOrder.Visible = false;
            }
        }

        private void bindRecords()
        {

            DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where (cfhSource='Business' or cfhSource='AfterSales') and cfhRelatedID=" + rowID + " order by cfhAddDate desc");
            this.repFollowHistory.DataSource = dt;
            this.repFollowHistory.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource='Business' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
            this.repCustomerFollowPlan.DataSource = dt;
            this.repCustomerFollowPlan.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cwOperatorName,C.depName as cwOperatorDepartment from CoWorker A left outer join Operators B on A.cwOperatorID=B.opeID left outer join Department C on B.opeDepartmentID=C.depID where cwSource='Business' and cwRelatedID=" + rowID);
            this.repCoWorker.DataSource = dt;
            this.repCoWorker.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as opeName,C.Name as crTypeName from CustomerReceipt A left outer join Operators B on A.crOperatorID=B.opeID left outer join defCommon C on A.crTypeID=C.ID where crBusinessID=" + rowID + " order by crAddDate desc");
            this.repBusinessReceipt.DataSource = dt;
            this.repBusinessReceipt.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as opeName,C.Name as crpTypeName from CustomerReceiptPlan A left outer join Operators B on A.crpOperatorID=B.opeID left outer join defCommon C on A.crpTypeID=C.ID where crpBusinessID=" + rowID + " order by crpAddDate desc");
            this.repBusinessReceiptPlan.DataSource = dt;
            this.repBusinessReceiptPlan.DataBind();

            if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
            {
                dt = DbHelperSQL.GetDataTable(@"select * 
from v_SellOrderItem  A where FBillNoDD in (select cbName from CustomerBusiness where cbID = " + rowID + ") order by FDateDD desc");
                this.repOrder.DataSource = dt;
                this.repOrder.DataBind();
            }

            

            dt = DbHelperSQL.GetDataTable(@"select A.*,
cusName,
B.opeName as cbOperator,
C.opeName as cbAddOperator,
D.Name as cbBusinessTypeName,
E.sName as cbStatusName,
F.sName as cbAfterName,
G.opeName as cbNotifyOperatorName 
from CustomerBusiness A
left outer join Customer CC on A.cbCustomerID = CC.cusID
left outer join Operators B on A.cbOperatorID=B.opeID 
left outer join Operators C on A.cbAddOperatorID=C.opeID 
left outer join Operators G on A.cbNotifyOperatorID = G.opeID
left outer join defCommon D on A.cbBusinessType=D.ID 
left outer join sdefCommon E on A.cbStatus = E.sID
left outer join sdefCommon F on A.cbAfterID = F.sID
where cbID=" + rowID);
            if (dt.Rows.Count > 0)
            {
                lblcbName.InnerText = dt.Rows[0]["cbName"] + "";
                lblcbAddDate.InnerText = dt.Rows[0]["cbAddDate"] + "";
                lblcbAddOperator.InnerText = dt.Rows[0]["cbAddOperator"] + "";
                lblcbBusinessType.InnerText = dt.Rows[0]["cbBusinessTypeName"] + "";
                lblcbOperator.InnerText = dt.Rows[0]["cbOperator"] + "";
                lblcbRemark.InnerText = dt.Rows[0]["cbRemark"] + "";
                lblcbTotalAmount.InnerText = dt.Rows[0]["cbTotalAmount"] + "";
                lblcbGotAmount.InnerText = dt.Rows[0]["cbGotAmount"] + "";
                lblcbNotGotAmount.InnerText = dt.Rows[0]["cbNotGotAmount"] + "";
                lblcbNotifyOperatorName.InnerText = dt.Rows[0]["cbNotifyOperatorName"] + "";

                lblcbDate.InnerText = DateTime.Parse(dt.Rows[0]["cbDate"] + "").ToString("yyyy-MM-dd");
                txtcbDate.Text = DateTime.Parse(dt.Rows[0]["cbDate"] + "").ToString("yyyy-MM-dd");
                if (dt.Rows[0]["cbExpiredDate"] + "" != "")
                {
                    lblcbExpiredDate.InnerText = DateTime.Parse(dt.Rows[0]["cbExpiredDate"] + "").ToString("yyyy-MM-dd HH:mm");
                    txtcbExpiredDate.Text = DateTime.Parse(dt.Rows[0]["cbExpiredDate"] + "").ToString("yyyy-MM-dd");
                }

                lblBusinessName.Text = dt.Rows[0]["cbName"] + "";
                lblcbStatus.InnerText = dt.Rows[0]["cbStatusName"] + "";
                lblcbAfterID.InnerText = dt.Rows[0]["cbAfterName"] + "";
                lblcbExpiredDate.InnerText = dt.Rows[0]["cbExpiredDate"] + "";
                txtcbName.Text = dt.Rows[0]["cbName"] + "";
                txtcbRemark.Text = dt.Rows[0]["cbRemark"] + "";
                txtcbTotalAmount.Text = dt.Rows[0]["cbTotalAmount"] + "";
                ddlcbBusinessType.SelectedValue = dt.Rows[0]["cbBusinessType"] + "";
                ddlcbOperatorID.SelectedValue = dt.Rows[0]["cbOperatorID"] + "";
                ddlcbNotifyOperatorID.SelectedValue = dt.Rows[0]["cbNotifyOperatorID"] + "";

                hfcusID.Value = dt.Rows[0]["cbCustomerID"] + "";
                hfKeyID.Value = dt.Rows[0]["cbID"] + "";

                lblcfhFromName.InnerHtml = "关联客户：<a data-ajax='false' href='CustomerEditForm.aspx?ID=" + dt.Rows[0]["cbCustomerID"] + "'>" + dt.Rows[0]["cusName"] + "</a>";
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
