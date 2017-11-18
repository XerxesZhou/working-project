/***************************************************************************
 * 
 *       功能：     客户项目编辑
 *       作者：     朱少军
 *       日期：     2009-07-17
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Data
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;


    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;

    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Data;
    using SmartSoft.Component;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Presentation;
    using SmartSoft.Service;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Implement.Common;

    public partial class CustomerEditForm : SmartSoft.Presentation.PageBaseEdit
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
                        if (fromType == "linkman")
                        {
                            string clmCustomerID = DbHelperSQL.GetSHSL("select clmCustomerID from CustomerLinkMan where clmID = " + Request.QueryString["ID"]);
                            return int.Parse(clmCustomerID);
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

        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
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
            InitDataSource(ddlcusDepartmentID, "CD_Department");
            InitDataSource(ddlcusOperatorID, "CD_Operators");
            InitDataSource(ddlcusKindID, "CD_defCustomerType");
            InitDataSource(ddlcusSourceID, "CD_defCustomerSourceType");
            InitDataSource(ddlcfhTypeID, "CD_defCustomerFollowHistoryType");
            InitDataSource(ddlcbOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcbNotifyOperatorID, "CD_Operators");
            InitDataSource(ddlcrOperatorID, "CD_Operators");
            InitDataSource(ddlcrpOperatorID, "CD_Operators");

            DbHelperSQL.BindDropDownListAddEmpty("select cbID,cbName from CustomerBusiness where cbCustomerID = " + rowID + " order by cbDate desc", this.ddlcrBusinessID, "cbName", "cbID");
            DbHelperSQL.BindDropDownListAddEmpty("select cbID,cbName from CustomerBusiness where cbCustomerID = " + rowID + " order by cbDate desc", this.ddlcrpBusinessID, "cbName", "cbID");
            InitDataSource(ddlcoPhaseID, "CD_defCustomerOpptunityPhase");
            InitDataSource(ddlcoStatusID, "CD_OpptunityStatus");
            InitDataSource(ddlcoOpptunitySourceID, "CD_defCustomerSourceType");
            InitDataSource(ddlcoOperatorID, "CD_Operators");
            InitDataSource(ddlcfbFeedbackTypeID, "CD_defCustomerFeedbackType");
            InitDataSource(ddlcfbHandleOperator, "CD_Operators");
            InitDataSource(ddlcfbStatus, "CD_defCustomerFeedbackStatus");
            InitDataSource(ddlcusExtType2, "CD_defCustomerProperty");
            InitDataSource(ddlcusAreaID, "CD_defCustomerArea");

            InitDataSource(ddlcwOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlcfhStatusID, "CD_defCustomerStatus");
            InitDataSource(ddlcrTypeID, "CD_defCustomerReceiptType");
            InitDataSource(ddlcrpTypeID, "CD_defCustomerReceiptType");
            InitDataSource(ddlcbBusinessType, "CD_defCustomerBusinessType");
            InitDataSource(ddlChangeOperatorID, "CD_Operators");
            InitDataSource(ddlclmTypeID, "CD_defCustomeLinkManType");

            if (CurrentOperatorID != "")
            {
                hfCurrentOperatorID.Value = CurrentOperatorID;
                string CurrentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                hfCurrentOperatorName.Value = CurrentOperatorName;
                this.ddlcusOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcoOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcbOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcrOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcrpOperatorID.SelectedValue = CurrentOperatorID;
                this.ddlcfbHandleOperator.SelectedValue = CurrentOperatorID;
            }


            hfWebPCDomain.Value = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";

            if (DbHelperSQL.Exists("select * from XCustomerPool where cusID = " + rowID))
            {
                aDelete.Visible = false;
                aChange.Visible = false;
                aPutInOrGetFromPool.InnerText = "认领";
            }
            else
            {
                if (CommonFunction.CheckPermissionForCustomer(rowID.ToString(), CurrentOperatorID))
                {
                    aPutInOrGetFromPool.InnerText = "放入公海";
                    aDelete.Visible = true;
                    aChange.Visible = true;
                }
                
            }

            
            this.liCustomerBusiness.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 140737488355328);
            this.liCustomerFeedback.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 70368744177664);
            this.liCustomerFile.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 562949953421312);
            this.liCustomerFollowHistory.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 17592186044416);
            this.liCustomerFollowPlan.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 35184372088832);
            this.liCustomerLinkMan.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 8796093022208);
            this.liCustomerOpptunity.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 9007199254740992);
            this.liCustomerReceipt.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 281474976710656);
            this.liCustomerReceiptPlan.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 18014398509481984);
            if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
            {
                this.liCustomerOrder.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 140737488355328);
                this.liAR.Visible = CommonFunction.HasViewSepcialPageCode("CustomerList.aspx", CurrentOperatorID, 140737488355328);
            }
            else
            {
                liCustomerOrder.Visible = false;
                liAR.Visible = false;
            }
        }

        private void bindRecords()
        {

            DataTable dt = DbHelperSQL.GetDataTable(@"select clmID,cusName,clmName,clmSex,clmTel,clmEmail,clmRemark,clmModifyDate,opeName,clmAddDate,clmMobile,clmQQ,clmWeChat,D.Name as clmTypeName from CustomerLinkMan A INNER JOIN Customer B ON A.clmCustomerID = B.cusID left outer join Operators C on A.clmAddOperatorID=C.opeID
left outer join defCommon D on A.clmTypeID=D.ID where cusID=" + rowID);
            this.repLinkMan.DataSource = dt;
            this.repLinkMan.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as opeName,C.opeName as AddopeName,D.opeName as ModifyOperatorName from CustomerOpptunity A left outer join Operators B on A.coOperatorID=B.opeID left outer join Operators C on A.coAddOperatorID=C.opeID left outer join Operators D on A.coModifyOperatorID=D.opeID  where coCustomerID=" + rowID + " order by coDate desc");
            this.repOpptunity.DataSource = dt;
            this.repOpptunity.DataBind();

            string clueID = DbHelperSQL.GetSHSL("select ccID from CustomerClue where ccCustomerID = " + rowID);
            if (clueID == "")
            {
                clueID = "0";
            }
            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cfhOperator,ISNULL(C.Name,'系统跟进') as cfhType from CustomerFollowHistory A left outer join Operators B on A.cfhOperatorID=B.opeID left outer join defCommon C on A.cfhTypeID=C.ID where ((cfhSource = 'Customer' and cfhRelatedID=" + rowID + ") or (cfhSource = 'Clue' and cfhRelatedID=" + clueID + ") or (cfhSource = 'Opptunity' and cfhRelatedID in(select coID from CustomerOpptunity where coCustomerID = " + rowID + ")) or (cfhSource = 'Business' and cfhRelatedID in(select cbID from CustomerBusiness where cbCustomerID = " + rowID + ")) or (cfhSource = 'AfterSales' and cfhRelatedID in(select cbID from CustomerBusiness where cbCustomerID = " + rowID + "))) order by cfhAddDate desc");
            this.repFollowHistory.DataSource = dt;
            this.repFollowHistory.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,C.opeName as cfpOperator from CustomerFollowPlan A left outer join Operators C on A.cfpOperatorID=C.opeID where cfpSource = 'Customer' and cfpRelatedID=" + rowID + " order by cfpAddDate desc");
            this.repCustomerFollowPlan.DataSource = dt;
            this.repCustomerFollowPlan.DataBind();

            dt = DbHelperSQL.GetDataTable(@"select A.*,B.opeName AS cbOperatorName,G.opeName AS cbNotifyOperatorName,H.opeName AS cbAddOperatorName,C.Name as cbTypeName,E.sName as cbStatusName,F.sName as cbAfterName 
from CustomerBusiness  A 
left outer join Operators B ON A.cbOperatorID= B.opeID 
left outer join Operators G ON A.cbNotifyOperatorID = G.opeID
left outer join Operators H ON A.cbAddOperatorID = H.opeID
left outer join defCommon C on A.cbBusinessType =C.ID 
left outer join sdefCommon E on A.cbStatus = E.sID
left outer join sdefCommon F on A.cbAfterID = F.sID
where cbCustomerID=" + rowID + " order by cbDate desc");
            this.repCustomerBusiness.DataSource = dt;
            this.repCustomerBusiness.DataBind();

            if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
            {
                string cusCN = DbHelperSQL.GetSHSL("select cusCN from Customer where cusID = " + rowID);
                dt = DbHelperSQL.GetDataTable(@"select * 
from v_SellOrderItem  A where CustNO = '" + cusCN + "' OR CustShortNO = '" + cusCN + "' OR FBillNoDD in (select cbName from CustomerBusiness where cbCustomerID = " + rowID + ") order by FDateDD desc");
                this.repOrder.DataSource = dt;
                this.repOrder.DataBind();

                dt = DbHelperSQL.GetDataTable(@"usp_getYearMonthAmount '" + cusCN + "'");
                this.repAR.DataSource = dt;
                this.repAR.DataBind();
            }

            dt = DbHelperSQL.GetDataTable("select A.*,B.cbName,C.opeName AS opeName, D.cbName AS crBusinessName,E.name as crTypeName  from CustomerReceipt A LEFT OUTER JOIN CustomerBusiness B ON A.crBusinessID = B.cbID  left outer join Operators C on A.crOperatorID=C.opeID left outer join CustomerBusiness D on A.crBusinessID=D.cbID left outer join defCommon E on A.crTypeID=E.ID where crCustomerID=" + rowID + " order by crDate desc");
            this.repCustomerReceipt.DataSource = dt;
            this.repCustomerReceipt.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.cbName,C.opeName AS opeName, D.cbName AS crpBusinessName,E.name as crpTypeName  from CustomerReceiptPlan A LEFT OUTER JOIN CustomerBusiness B ON A.crpBusinessID = B.cbID  left outer join Operators C on A.crpOperatorID=C.opeID left outer join CustomerBusiness D on A.crpBusinessID=D.cbID left outer join defCommon E on A.crpTypeID=E.ID where crpCustomerID=" + rowID + " order by crpDate desc");
            this.repBusinessReceiptPlan.DataSource = dt;
            this.repBusinessReceiptPlan.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.Name as cfbTypeName,C.opeName as AddOperatorName,D.opeName as cfbHandleOperator,E.name as cfbStatus from CustomerFeedback A left outer join defCommon B on A.cfbFeedbackTypeID=B.ID left outer join Operators C on A.cfbAddOperatorID=C.opeID left outer join Operators D on A.cfbHandleOperatorID=D.opeID left outer join defCommon E on A.cfbStatusID=E.ID where cfbCustomerID=" + rowID + " order By cfbAddDate desc");
            foreach (DataRow dr in dt.Rows)
            {
                string cfbHandleOperatorID = dr["cfbHandleOperatorID"] + "";
                if (cfbHandleOperatorID != "" && cfbHandleOperatorID == CurrentOperatorID)
                {
                    hfHandleOperatorID.Value = cfbHandleOperatorID.ToString();
                }
            }
            this.repFeedback.DataSource = dt;
            this.repFeedback.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as cwOperatorName,C.depName as cwOperatorDepartment from CoWorker A left outer join Operators B on A.cwOperatorID=B.opeID left outer join Department C on B.opeDepartmentID=C.depID where cwSource = 'Customer' and cwRelatedID=" + rowID);
            this.repCoWorker.DataSource = dt;
            this.repCoWorker.DataBind();

            dt = DbHelperSQL.GetDataTable("select * from CustomerFile where cfCustomerID=" + rowID + " order by cfID desc");
            this.repDocument.DataSource = dt;
            this.repDocument.DataBind();

            dt = DbHelperSQL.GetDataTable("select A.*,B.opeName AS cusOperatorName,C.depName AS cusDepartmentName,D.ctName AS cusKindName,E.Name AS cusSourceName,F.Name as cusExtType2Name,G.caName AS cusAreaName,S.sName AS cusStatusName from Customer A LEFT OUTER JOIN Operators B ON A.cusOperatorID=B.opeID LEFT OUTER JOIN Department C ON A.cusDepartmentID=C.depID LEFT OUTER JOIN defCustomerType D ON A.cusKindID=D.ctID LEFT OUTER JOIN defCommon E  ON A.cusSourceID = E.ID LEFT OUTER JOIN defCommon F ON A.cusExtType2=F.ID LEFT OUTER JOIN defCustomerArea G ON A.cusAreaID = G.caID LEFT OUTER JOIN sdefCommon S ON cusStatusID = S.sID where cusID=" + rowID);
            if (dt != null && dt.Rows.Count == 1)
            {
                this.lblCustomerName.Text = dt.Rows[0]["cusName"] + "";
                this.lblcusName.InnerText = dt.Rows[0]["cusName"] + "";
                this.lblcusAddDate.InnerText = dt.Rows[0]["cusAddDate"] + "";
                this.lblcusAddress.InnerText = dt.Rows[0]["cusAddress"] + "";
                this.lblcusDepartmentID.InnerText = dt.Rows[0]["cusDepartmentName"] + "";
                this.lblcusIntroduction.InnerText = dt.Rows[0]["cusIntroduction"] + "";
                this.lblcusKindID.InnerText = dt.Rows[0]["cusKindName"] + "";
                this.lblcusOperatorID.InnerText = dt.Rows[0]["cusOperatorName"] + "";
                this.lblcusSourceID.InnerText = dt.Rows[0]["cusSourceName"] + "";
                this.lblcusContactor.InnerText = dt.Rows[0]["cusContactor"] + "";
                this.lblcusTel.InnerText = dt.Rows[0]["cusTel"] + "";

                this.lblcusExtType2.InnerText = dt.Rows[0]["cusExtType2Name"] + "";
                this.lblcusAreaID.InnerText = dt.Rows[0]["cusAreaName"] + "";
                this.lblcusCN.InnerText = dt.Rows[0]["cusCN"] + "";

                linkcusTel.HRef = "tel:" + dt.Rows[0]["cusTel"] + "";
                this.txtcusName.Text = dt.Rows[0]["cusName"] + "";
                this.txtcusAddress.Text = dt.Rows[0]["cusAddress"] + "";
                this.txtcusIntroduction.Text = dt.Rows[0]["cusIntroduction"] + "";
                this.ddlcusDepartmentID.SelectedValue = dt.Rows[0]["cusDepartmentID"] + "";
                this.ddlcusKindID.SelectedValue = dt.Rows[0]["cusKindID"] + "";
                this.ddlcusOperatorID.SelectedValue = dt.Rows[0]["cusOperatorID"] + "";
                this.ddlcusSourceID.SelectedValue = dt.Rows[0]["cusSourceID"] + "";
                this.txtcusLongtitude.Text = dt.Rows[0]["cusLongtitude"] + "";
                this.txtcusLatitude.Text = dt.Rows[0]["cusLatitude"] + "";
                this.txtcusContactor.Text = dt.Rows[0]["cusContactor"] + "";
                this.txtcusTel.Text = dt.Rows[0]["cusTel"] + "";
                this.ddlcusExtType2.SelectedValue = dt.Rows[0]["cusExtType2"] + "";
                this.ddlcusAreaID.SelectedValue = dt.Rows[0]["cusAreaID"] + "";
                this.txtcusCN.Text = dt.Rows[0]["cusCN"] + "";
                this.lblOperatorName.Text = dt.Rows[0]["cusOperatorName"] + "";
                this.lblAddress.Text = dt.Rows[0]["cusAddress"] + "";

                lblStatus.InnerText = dt.Rows[0]["cusStatusName"] + "";

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
                string name = DbHelperSQL.GetSHSL("select sName from sdefCommon where sID=" + id);
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

        public string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID) 
        {
            return CommonFunction.GetLikeAndCommentCountHtml(cfhID, cfhOperatorID, CurrentOperatorID);
        }
       
    }
}
