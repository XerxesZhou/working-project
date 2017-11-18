using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using SmartSoft.Component;
using SmartSoft.Domain.Common;
using System.Configuration;

namespace SmartSoft.MobileWeb
{
    /// <summary>
    /// AjaxMethods 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class AjaxMethods : System.Web.Services.WebService
    {
        #region Message
        [WebMethod]
        public string SetMessageReaded(string MessageID, string OperatorID)
        {
            return CommonFunction.SetMessageReaded(MessageID, OperatorID);
        }
        #endregion

        #region Customer
        //保存客户基本信息
        [WebMethod]
        public string SaveCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string CurrentOperatorID, string cusExtType2, string cusAreaID, string cusCN)
        {
            return CommonFunction.SaveCustomerInfo(cusID, cusName, cusKindID, cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, cusLongtitude, cusLatitude, cusContactor, cusTel, CurrentOperatorID, cusExtType2, cusAreaID, cusCN);
        }

        [WebMethod]
        public string AddCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string clmSex, string clmMobile, string coName, string coPhaseID, string coPredictAmount, DateTime coPredictFinishDate, string CurrentOperatorID, string cusExtType2, string cusAreaID, string OpptunityID, string ClueID, string cusCN)
        {
            return CommonFunction.AddCustomerInfo(cusID, cusName, cusKindID, cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, cusLongtitude, cusLatitude, cusContactor, cusTel, clmSex, clmMobile, coName, coPhaseID, coPredictAmount, coPredictFinishDate, CurrentOperatorID, cusExtType2, cusAreaID, OpptunityID, ClueID, cusCN);
        }

        [WebMethod]
        public List<CustomerInfo> GetCustomerNearby(float currentLongtitude, float currentLatitude, int currentOperatorID)
        {
            try
            {
                DataTable dt = null;

                string whereCondition = CommonFunction.GetPermissionWhereCondition("cusOperatorID", currentOperatorID.ToString());

                dt = DbHelperSQL.GetDataTable(string.Format("select top 100 cusID,cusName,cusAddress,cusLongtitude,cusLatitude,cusContactor,cusTel from Customer where cusLongtitude is not null and cusLatitude is not null  {2} order by [dbo].[LatLonRadiusDistance](cusLatitude,cusLongtitude,{0},{1}) asc", currentLatitude, currentLongtitude, whereCondition));

                List<CustomerInfo> list = new List<CustomerInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerInfo cus = new CustomerInfo();
                    cus.cusID = row["cusID"] + "";
                    cus.cusName = row["cusName"] + "";
                    cus.cusAddress = row["cusAddress"] + "";
                    cus.cusLongtitude = row["cusLongtitude"] + "";
                    cus.cusLatitude = row["cusLatitude"] + "";
                    cus.cusContactor = row["cusContactor"] + "";
                    cus.cusTel = row["cusTel"] + "";
                    list.Add(cus);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public int DeleteCustomer(int id, int CurrentOperatorID)
        {
            return CommonFunction.DeleteCustomer(id, CurrentOperatorID);
        }

        [WebMethod]
        public int CheckInCustomerPool(int ID)
        {
            return CommonFunction.CheckInCustomerPool(ID);
        }

        [WebMethod]
        public int PutInOrGetFromCustomerPool(int ID, int CurrentOperatorID)
        {
            return CommonFunction.PutInOrGetFromCustomerPool(ID, CurrentOperatorID);
        }

        [WebMethod]
        public int ChangeCustomerOperator(int cusID, int cusOperatorID, int CurrentOperatorID)
        {
            return CommonFunction.ChangeCustomerOperator(cusID, cusOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public IList<CustomerCheck> SearchCustomer(string KeyWord)
        {
            return CommonFunction.SearchCustomer(KeyWord);
        }
        #endregion

        #region CustomerLinkMan
        [WebMethod]
        public CustomerLinkManResult GetLinkMan(int clmID)
        {
            return CommonFunction.GetLinkMan(clmID);
        }

        [WebMethod]
        public string SaveLinkMan(int cusID, int clmID, string clmName, string clmSex, string clmTel, string clmMobile, string clmEmail, string clmQQ, string clmWeChat, string clmRemark, int clmAddOperatorID,string clmTypeID)
        {
            return CommonFunction.SaveLinkMan(cusID, clmID, clmName, clmSex, clmTel, clmMobile, clmEmail, clmQQ, clmWeChat, clmRemark, clmAddOperatorID,clmTypeID);
        }

        [WebMethod]
        public string DeleteLinkMan(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteLinkMan(ID, CurrentOperatorID);
        }
        #endregion

        #region CustomerOpptunity
        [WebMethod]
        public CustomerOpptunityResult GetOpptunity(int coID)
        {
            return CommonFunction.GetOpptunity(coID);
        }

        [WebMethod]
        public string SaveOpptunity(int cusID, int coID, string coName, string coPhaseID, string coPredictAmount, string coPredictFinishDate, string coStatusID, string coOpptunitySourceID, string coOperatorID, string coDate,string CurrentOperatorID)
        {
            return CommonFunction.SaveOpptunity(cusID, coID, coName, coPhaseID, coPredictAmount, coPredictFinishDate, coStatusID, coOpptunitySourceID, coOperatorID, coDate,CurrentOperatorID);
        }

        [WebMethod]
        public string DeleteOpptunity(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteOpptunity(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string ChangeOpptunityStatus(int ID, string coStatusID, int CurrentOperatorID)
        {
            return CommonFunction.ChangeOpptunityStatus(ID, coStatusID, CurrentOperatorID);
        }
        
        #endregion

        #region CustomerFollowHistory
        [WebMethod]
        public string DeleteFollowHistory(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteFollowHistory(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveCustomerFollowHistory(string cfhSource, int cfhRelatedID, int cfhID, string cfhContent, string cfhTypeID, string cfhStatusID, string cfhDate, string cfhAddress, string cfhAddOperatorID, string cfhNextFollowTime, string cfhFilePath, string CurrentOperatorID)
        {
            return CommonFunction.SaveCustomerFollowHistory(cfhSource, cfhRelatedID, cfhID, cfhContent, cfhTypeID, cfhStatusID, cfhDate, cfhAddress, cfhAddOperatorID, cfhNextFollowTime, cfhFilePath, CurrentOperatorID);
        }

        [WebMethod]
        public string GetEachFile(string filePath)
        {
            return CommonFunction.GetEachFile(filePath);
        }
        #endregion

        #region CustomerFollowPlan
        [WebMethod]
        public CustomerFollowPlanResult GetFollowPlan(int id)
        {
            return CommonFunction.GetFollowPlan(id);
        }

        [WebMethod]
        public string DeleteFollowPlan(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteFollowPlan(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveFollowPlan(int cfpID, string cfpRelatedID, string cfpSource, string cfpContent, string cfpFilePath, string cfpDate, string cfpOperatorID) 
        {
           return CommonFunction.SaveFollowPlan(cfpID, cfpRelatedID, cfpSource, cfpContent, cfpFilePath, cfpDate, cfpOperatorID) ;
        }
        #endregion

        #region CustomerBusiness
        [WebMethod]
        public CustomerBusinessResult GetBusiness(int id)
        {
            return CommonFunction.GetBusiness(id);
        }

        [WebMethod]
        public string DeleteBusiness(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteBusiness(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveBusiness(int cbCustomerID, int cbID, string cbName, string cbTotalAmount, string cbOperatorID, string cbDate, string cbRemark, string CurrentOperatorID, string cbBusinessType, string cbExpiredDate, string cbNotifyOperatorID)
        {
            return CommonFunction.SaveBusiness(cbCustomerID, cbID, cbName, cbTotalAmount, cbOperatorID, cbDate, cbRemark, CurrentOperatorID, cbBusinessType, cbExpiredDate, cbNotifyOperatorID);
        }

        [WebMethod]
        public string AddCustomerBusinessInfo(string cbName, string cbBusinessTypeID, string cbDate, string cbTotalAmount, string cbOperatorID, string cbRemark, string CurrentOperatorID, string OpptunityID, string cbExpiredDate, string cbNotifyOperatorID) 
        {
            return CommonFunction.AddCustomerBusinessInfo(cbName, cbBusinessTypeID, cbDate, cbTotalAmount, cbOperatorID, cbRemark, CurrentOperatorID, OpptunityID, cbExpiredDate, cbNotifyOperatorID);
        }
        #endregion

        #region CustomerReceipt
        [WebMethod]
        public CustomerReceiptResult GetReceipt(int id)
        {
            return CommonFunction.GetReceipt(id);
        }

        [WebMethod]
        public string DeleteReceipt(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteReceipt(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveReceipt(int crCustomerID, int crID, string crBusinessID, string crAmount, string crOperatorID, string crDate,string CurrentOperatorID,string crTypeID, string crBatchID, string crRemark)
        {
            return CommonFunction.SaveReceipt(crCustomerID, crID, crBusinessID, crAmount, crOperatorID, crDate, CurrentOperatorID, crTypeID, crBatchID, crRemark);
        }

        [WebMethod]
        public string GetBusinessReceiptAmount(string crBusinessID,string crAmount,string crID) 
        {
            return CommonFunction.GetBusinessReceiptAmount(crBusinessID, crAmount, crID);
        }

        #endregion

        #region CustomerReceiptPlan
        [WebMethod]
        public CustomerReceiptPlanResult GetReceiptPlan(int id)
        {
            return CommonFunction.GetReceiptPlan(id);
        }

        [WebMethod]
        public string DeleteReceiptPlan(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteReceiptPlan(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveReceiptPlan(int crpCustomerID, int crpID, string crpBusinessID, string crpAmount, string crpOperatorID, string crpDate, string CurrentOperatorID, string crpTypeID, string crpBatchID, string crpRemark)
        {
            return CommonFunction.SaveReceiptPlan(crpCustomerID, crpID, crpBusinessID, crpAmount, crpOperatorID, crpDate, CurrentOperatorID, crpTypeID, crpBatchID, crpRemark);
        }

        [WebMethod]
        public string GetBusinessReceiptPlanAmount(string crpBusinessID, string crpAmount, string crpID)
        {
            return CommonFunction.GetBusinessReceiptPlanAmount(crpBusinessID, crpAmount, crpID);
        }

        #endregion

        #region CustomerFeedback
        [WebMethod]
        public string HandleFeedback(int cfbID, string cfbResult, string cfbStatus)
        {
            return CommonFunction.HandleFeedback(cfbID, cfbResult, cfbStatus);
        }

        [WebMethod]
        public FeedbackList GetFeedback(int id)
        {
            return CommonFunction.GetFeedback(id);
        }

        [WebMethod]
        public string SaveFeedback(int cfbCustomerID, int cfbID, string cfbFeedbackTypeID, string cfbLinkman, string cfbTelephone, string cfbEmail, string cfbOrderRelated, string cfbContent, string cfbHandleOperatorID, string CurrentOperatorID, string cfbResult, string cfbStatusID)
        {
            return CommonFunction.SaveFeedback(cfbCustomerID, cfbID, cfbFeedbackTypeID, cfbLinkman, cfbTelephone, cfbEmail, cfbOrderRelated, cfbContent, cfbHandleOperatorID, CurrentOperatorID, cfbResult, cfbStatusID);
        }

        [WebMethod]
        public string DeleteFeedback(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteFeedback(ID, CurrentOperatorID);
        }


        [WebMethod]
        public string SaveHelpCenterFeedBackInfo(string fdOperatorID, string fdContent, string fdOperatorEmail, string fdOperatorPhone)
        {
            return CommonFunction.SaveHelpCenterFeedBackInfo(fdOperatorID, fdContent, fdOperatorEmail, fdOperatorPhone);
        }
        #endregion

        #region WorkDiary
        [WebMethod]
        public string SaveWorkDiary(string wdDate, string wdTitle, string wdContent, string wdNotifierID,string wdAddOperatorID,string wdTypeID,string wdFile) 
        {
            return CommonFunction.SaveWorkDiary(wdDate, wdTitle, wdContent, wdNotifierID, wdAddOperatorID, wdTypeID, wdFile);
        }

        [WebMethod]
        public string DeleteWorkDiary(string wdID, int CurrentOperatorID) 
        {
            return CommonFunction.DeleteWorkDiary(wdID, CurrentOperatorID);
        }
        #endregion

        #region CustomerClue
        [WebMethod]
        public string SaveCustomerClue(string ccActivityID,int ccID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            return CommonFunction.SaveCustomerClue(ccActivityID,ccID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
        }

        [WebMethod]
        public string AddCustomerClue(string ccActivityID,string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            return CommonFunction.AddCustomerClue(ccActivityID,ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
        }

        [WebMethod]
        public int DeleteClue(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteClue(ID, CurrentOperatorID);
        }

        [WebMethod]
        public int ChangeClueOperator(int ccID, string ccOperatorID, int CurrentOperatorID)
        {
            return CommonFunction.ChangeClueOperator(ccID, ccOperatorID, CurrentOperatorID);
        }

       
        [WebMethod]
        public string SavaBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string cfhOperatorID, string bcSourceID)
        {
            return CommonFunction.SavaBillComment(bcOperatorID, bcContent, bcRelatedID, cfhOperatorID, bcSourceID);
        }

        [WebMethod]
        public string ReplyBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string replyOperatorID, string bcSourceID)
        {
            return CommonFunction.ReplyBillComment(bcOperatorID, bcContent, bcRelatedID, replyOperatorID, bcSourceID);
        }

        [WebMethod]
        public string GetClickLikeInfoForHistory(string currentOperatorID, string cfhID, string cfhOperatorID)
        {
            return CommonFunction.GetClickLikeInfoForHistory(currentOperatorID, cfhID, cfhOperatorID);
        }

        [WebMethod]
        public string GetClickLikeInfoForWorkReport(string currentOperatorID, string cfhID, string cfhOperatorID)
        {
            return CommonFunction.GetClickLikeInfoForWorkReport(currentOperatorID, cfhID, cfhOperatorID);
        }

        [WebMethod]
        public string SetFollowPlanMessageRead(int cfpID, string messageID, string CurrentOperatorID)
        {
            return CommonFunction.SetFollowPlanMessageRead(cfpID, messageID, CurrentOperatorID);
        }

        #endregion

        #region CoWorker
        [WebMethod]
        public string SaveCoWorker(string cwRelatedID, string cwOperatorID, string CurrentOperatorID, string cwSource)
        {
            return CommonFunction.SaveCoWorker(cwRelatedID, cwOperatorID, CurrentOperatorID, cwSource);
        }

        [WebMethod]
        public string GetDepartment(string cwOperatorID)
        {
            return CommonFunction.GetDepartment(cwOperatorID);
        }

        [WebMethod]
        public int DeleteCoWorkerView(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteCoWorkerView(ID, CurrentOperatorID);
        }
        #endregion

        #region Document
        [WebMethod]
        public string SaveDocument(string cfNane, string cfCN, string cfUserID, string cfCustomerID)
        {
            return CommonFunction.SaveDocument(cfNane, cfCN, cfUserID, cfCustomerID);
        }
        #endregion

        #region Project
        [WebMethod]
        public string SaveProject(int pjID, string pjNO, string pjName, string pjCompanyName, string pjDetail, string pjProduct, string pjAmount, string pjContactor, string pjPrice, string pjRemark, string pjOperatorID, string pjToApproveOperatorID, string pjExpiredDate, string CurrentOperatorID)
        {
            return CommonFunction.SaveProject(pjID, pjNO, pjName, pjCompanyName, pjDetail, pjProduct, pjAmount, pjContactor, pjPrice, pjRemark, pjOperatorID, pjToApproveOperatorID, pjExpiredDate, CurrentOperatorID);
        }

        [WebMethod]
        public int DeleteProject(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteProject(ID, CurrentOperatorID);
        }

        [WebMethod]
        public int ApproveProject(int ID, int CurrentOperatorID)
        {
            return CommonFunction.ApproveProject(ID, CurrentOperatorID);
        }
        #endregion

        #region DashBoard
        [WebMethod]
        public WorkbenchList GetWorkbenchDataByCondition(string DateValue, string RoleValue, string CurrentOperatorID)
        {
            return CommonFunction.GetWorkbenchDataByCondition(DateValue, RoleValue, CurrentOperatorID);
        }

        [WebMethod]
        public string GetPermissionString(string CurrentOperatorID)
        {
            return CommonFunction.GetPermissionString(CurrentOperatorID);
        }

        [WebMethod(EnableSession = true)]
        public string GetCurrentOperatorInfo(string code)
        {
            UserDetailResponse userDetail = null;
            string AccessToken = HttpContext.Current.Cache["AccessToken"] + "";
            UserInfoResponse user = DDHelper.GetUserInfo(AccessToken, code);
            userDetail = DDHelper.GetUserDetailInfo(AccessToken, user.userid);
            try
            {
                DataRow row = DbHelperSQL.GetDataRow(string.Format("select top 1 [opeID],opeName from Operators where opeName = '{0}' OR opeMobile='{0}'", userDetail.mobile));
                HttpContext.Current.Session["CurrentOperatorName"] = row["opeName"] + "";
                HttpContext.Current.Session["CurrentOperatorID"] = row["opeID"] + "";
                return row["opeID"] + ":" + row["opeName"];
            }
            catch (Exception exx)
            {
            }
            return "";
        }
        #endregion

        #region WorkCalendar
        [WebMethod]
        public string GetWorkCalendarMonthContent(string currentOperatorID, int year, int month)
        {
            return CommonFunction.GetWorkCalendarMonthContent(currentOperatorID, year, month);
        }

        [WebMethod]
        public string[] GetWorkCalendarDateContent(string currentOperatorID, int year, int month, int day)
        {
            return CommonFunction.GetWorkCalendarDateContent(currentOperatorID, year, month, day);
        }

        [WebMethod]
        public string GetToDoCount(string currentOperatorID, string conditionType, string conditionValue)
        {
            return CommonFunction.GetToDoCount(currentOperatorID, conditionType, conditionValue);
        }
        #endregion

        #region GetData

        [WebMethod]
        public IList<CustomerCLueInfo> GetCustomerClueDate(int count, int EachCount, string ccStatusID, string keyword, string ccOperatorID, string ccDepartmentID, string ccAddOperatorID, string ccDateBegin, string ccDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            return CommonFunction.GetCustomerClueDate(count, EachCount, ccStatusID, keyword, ccOperatorID, ccDepartmentID, ccAddOperatorID, ccDateBegin, ccDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<CustomerInfoResult> GetCustomerDate(int count, int EachCount, string cusKindID, string keyword, string cusSource, string cusDepartment, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            return CommonFunction.GetCustomerDate(count, EachCount, cusKindID, keyword, cusSource, cusDepartment, cusOperator, cusDateBegin, cusDateEnd, currentOperatorID, cusColumn, orderby, mode, conditionType, conditionValue);
        }

        [WebMethod]
        public IList<CustomerInfoResult> GetCustomerPublicPoolDate(int count, int EachCount, string cusKindID, string keyword, string cusSource, string cusDepartment, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            return CommonFunction.GetCustomerPublicPoolDate(count, EachCount, cusKindID, keyword, cusSource, cusDepartment, cusOperator, cusDateBegin, cusDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<CustomerLinkManResult> GetCustomerLinkManData(int count, int EachCount, string clmTypeID, string keyword, string clmAddOperatorID, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            return CommonFunction.GetCustomerLinkManData(count, EachCount, clmTypeID, keyword, clmAddOperatorID, cusDateBegin, cusDateEnd, currentOperatorID, cusColumn, orderby, mode, conditionType, conditionValue);
        }

        [WebMethod]
        public IList<CustomerOpptunityResult> GetOpptunityData(int count, int EachCount, string coPhaseID, string keyword, string coOpptunitySourceID, string coStatusID, string coDateBegin, string coDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            return CommonFunction.GetOpptunityData(count, EachCount, coPhaseID, keyword, coOpptunitySourceID, coStatusID, coDateBegin, coDateEnd, currentOperatorID, cusColumn, orderby, mode, conditionType, conditionValue);
        }

        [WebMethod]
        public IList<CustomerFollowHistoryResult> GetCustomerFollowDate(int count, int EachCount, string cusKindID, string keyword, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
           return CommonFunction.GetCustomerFollowDate(count, EachCount, cusKindID, keyword, cusOperator, cusDateBegin, cusDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<CustomerBusinessResult> GetCustomerBusinessData(int count, int EachCount, string keyWord, string cbBusinessType, string ddlReceipt, string cbDepartmentID, string cbOperatorID, string cbDateBegin, string cbDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            return CommonFunction.GetCustomerBusinessData(count, EachCount, keyWord, cbBusinessType, ddlReceipt, cbDepartmentID, cbOperatorID, cbDateBegin, cbDateEnd, CurrentOperatorID, cusColumn, orderby, mode, conditionType, conditionValue);
        }

        [WebMethod]
        public List<CustomerReceiptResult> GetCustomerReceiptData(int count, int EachCount, string KeyWord, string crDepartmentID, string crOperatorID, string crDateBegin, string crDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode)
        {
            return CommonFunction.GetCustomerReceiptData(count, EachCount, KeyWord, crDepartmentID, crOperatorID, crDateBegin, crDateEnd, CurrentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public List<CustomerReceiptPlanResult> GetCustomerReceiptPlanData(int count, int EachCount, string KeyWord, string crpDepartmentID, string crpOperatorID, string crpDateBegin, string crpDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode, string crpStatus)
        {
            return CommonFunction.GetCustomerReceiptPlanData(count, EachCount, KeyWord, crpDepartmentID, crpOperatorID, crpDateBegin, crpDateEnd, CurrentOperatorID, cusColumn, orderby, mode, crpStatus);
        }

        [WebMethod]
        public IList<WorkDiaryList> GetWorkDiaryDate(int count, int EachCount, string keyword, string wdDateBegin, string wdDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
           return CommonFunction.GetWorkDiaryDate(count, EachCount, keyword, wdDateBegin, wdDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<CustomerFeedbackResult> GetCustomerFeedbackDate(int count, int EachCount, string cusKindID, string keyword, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            return CommonFunction.GetCustomerFeedbackDate(count, EachCount, cusKindID, keyword, cusOperator, cusDateBegin, cusDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<KnowList> GetKnowDate(int count, int EachCount, string keyword, string knowOperateDateBegin, string knowOperateDateEnd, string KnowType, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            return CommonFunction.GetKnowDate(count, EachCount, keyword, knowOperateDateBegin, knowOperateDateEnd, KnowType, currentOperatorID, cusColumn, orderby, mode);
        }

        [WebMethod]
        public IList<ProjectDetail> GetProjectDate(int count, int EachCount, string pjStatusID, string keyword, string pjOperatorID, string pjAddOperatorID, string pjDateBegin, string pjDateEnd, string pjApproveTag, string currentOperatorID, string column, string orderby, string mode)
        {
            return CommonFunction.GetProjectDate(count, EachCount, pjStatusID, keyword, pjOperatorID, pjAddOperatorID, pjDateBegin, pjDateEnd, pjApproveTag, currentOperatorID, column, orderby, mode);
        }

        [WebMethod]
        public IList<MarketingActivityDate> GetMarketingActivityDate(int count, int EachCount, string maTypeID, string keyword, string maStatusID, string maDepartmentID, string maOperatorID, string maDateBegin, string maDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode) 
        {
            return CommonFunction.GetMarketingActivityDate(count, EachCount, maTypeID, keyword, maStatusID, maDepartmentID, maOperatorID, maDateBegin, maDateEnd, currentOperatorID, cusColumn, orderby, mode);
        }



        #endregion

        #region MarketingActivity
        [WebMethod]
        public string AddMarketingActivityInfo(string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID) 
        {
            return CommonFunction.AddMarketingActivityInfo(maName, maStartDate, maEndDate, maTypeID, maDescription, maPredictCost, maPredictAmount, maActualCost, maPredictNum, maActualNum, maRemark, maDepartmentID, maOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveMarketingActivity(string maID, string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID)
        {
            return CommonFunction.SaveMarketingActivity(maID, maName, maStartDate, maEndDate, maTypeID, maDescription, maPredictCost, maPredictAmount, maActualCost, maPredictNum, maActualNum, maRemark, maDepartmentID, maOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveMarketingActivityClue(int ccID, string ccActivityID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            return CommonFunction.SaveMarketingActivityClue(ccID, ccActivityID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
        }
        
        [WebMethod]
        public CustomerCLueInfo GetMarketActivityClue(int ID)
        {
            return CommonFunction.GetMarketActivityClue(ID);
        }
        #endregion
    }
}
