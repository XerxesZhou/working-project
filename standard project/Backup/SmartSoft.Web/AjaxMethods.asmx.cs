using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SmartSoft.Service;
using SmartSoft.Domain;
using SmartSoft.Presentation;
using SmartSoft.Component;
using System.Data;
using SmartSoft.Domain.Data;
using System.Configuration;


namespace SmartSoft.Web
{
    /// <summary>
    /// AjaxMethods 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AjaxMethods : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetOperatorMessageCount(string opeID)
        {
            return CommonFunction.GetOperatorMessageCount(opeID);
        }

        [WebMethod]
        public string SendCode(string mobile)
        {
            string[] details = mobile.Split(':');
            DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from Operators where opeMobile like '%{0}%'", details[0]));
            if (dt != null && dt.Rows.Count >= 1)
            {
                return "该手机号已经注册。如有需要，请联系 18823310469(朱先生)。";
            }

            dt = DbHelperSQL.GetDataTable(string.Format("select * from SmsLog where Message like '验证码:{0}:%' and AddTime >='{1}' and AddTime < '{2}'", details[0], DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            if (dt != null && dt.Rows.Count >= 3)
            {
                return "一天内只能发送三次验证码，请明天再试。如有需要，请联系 18823310469(朱先生)。";
            }

            Random ran = new Random();
            int RandKey = ran.Next(100000, 999999);
            string strContent = "验证码:" + details[0] + ":" + RandKey;

            DbHelperSQL.ExecuteSQL(string.Format("insert into SmsLog(Message,AddTime) values('{0}','{1}')", strContent, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            return "您本次操作的验证码是:" + RandKey + "。请输入验证码完成注册。";

        }

        [WebMethod]
        public string SendMessage(string MessageContent, string ID, string CurrentOperatorID)
        {
            return CommonFunction.SendMessage(MessageContent, ID, CurrentOperatorID);
        }

        [WebMethod]
        public CustomerFollowHistiryDetailData GetFollowHistoryDetailData(string cfhID,string CurrentOperatorID) 
        {
            return CommonFunction.GetFollowHistoryDetailData(cfhID, CurrentOperatorID);
        }

        [WebMethod]
        public CustomerFollowPlanResult GetFollowPlanDetailData(string cfpID, string CurrentOperatorID) 
        {
            return CommonFunction.GetFollowPlanDetailData(cfpID, CurrentOperatorID);
        }

        [WebMethod]
        public string DeleteBillComment(string id) 
        {
            return CommonFunction.DeleteBillComment(id);
        }

        [WebMethod]
        public string DeleteBillCommentForRelatedID(string id)
        {
            return CommonFunction.DeleteBillCommentForRelatedID(id);
        }

        [WebMethod]
        public string ReplyBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string replyOperatorID, string bcSourceID)
        {
            return CommonFunction.ReplyBillComment(bcOperatorID, bcContent, bcRelatedID, replyOperatorID, bcSourceID);
        }

        [WebMethod]
        public string SavaBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string cfhOperatorID, string bcSourceID)
        {
            return CommonFunction.SavaBillComment(bcOperatorID, bcContent, bcRelatedID, cfhOperatorID, bcSourceID);
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
        public string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID, string CurrentOperatorID)
        {
            return CommonFunction.GetLikeAndCommentCountHtml(cfhID, cfhOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public string AddCustomerBusinessInfo(string cbName, string cbBusinessTypeID, string cbDate, string cbTotalAmount, string cbOperatorID, string cbRemark, string CurrentOperatorID, string OpptunityID, string cbExpiredDate, string cbNotifyOperatorID) 
        {
            return CommonFunction.AddCustomerBusinessInfo(cbName, cbBusinessTypeID, cbDate, cbTotalAmount, cbOperatorID, cbRemark, CurrentOperatorID, OpptunityID, cbExpiredDate, cbNotifyOperatorID);
        }

        [WebMethod]
        public IList<CustomerCheck> SearchCustomer(string KeyWord)
        {
            return CommonFunction.SearchCustomer(KeyWord);
        }

        #region Workbench
        [WebMethod]
        public WorkbenchList GetWorkbenchDataByCondition(string DateValue, string RoleValue, string CurrentOperatorID)
        {
            return CommonFunction.GetWorkbenchDataByCondition(DateValue, RoleValue, CurrentOperatorID);
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
        #endregion

        #region CustomerEdit
        [WebMethod]
        public string SaveCustomerFollowHistory(string cfhSource, int cfhRelatedID, int cfhID, string cfhContent, string cfhTypeID, string cfhStatusID, string cfhDate, string cfhAddress, string cfhAddOperatorID, string cfhNextFollowTime, string cfhFilePath, string CurrentOperatorID)
        {
            return CommonFunction.SaveCustomerFollowHistory(cfhSource, cfhRelatedID, cfhID, cfhContent, cfhTypeID, cfhStatusID, cfhDate, cfhAddress, cfhAddOperatorID, cfhNextFollowTime, cfhFilePath, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string CurrentOperatorID, string cusExtType2, string cusAreaID, string cusCN) 
        {
            return CommonFunction.SaveCustomerInfo(cusID, cusName, cusKindID,cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, cusLongtitude, cusLatitude, cusContactor, cusTel, CurrentOperatorID, cusExtType2, cusAreaID, cusCN);
        }

        [WebMethod]
        public CustomerLinkManResult GetLinkMan(int clmID) 
        {
            return CommonFunction.GetLinkMan(clmID);
        }

        [WebMethod]
        public string DeleteLinkMan(int ID, int CurrentOperatorID) 
        {
            return CommonFunction.DeleteLinkMan(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveLinkMan(int cusID, int clmID, string clmName, string clmSex, string clmTel, string clmMobile, string clmEmail, string clmQQ, string clmWeChat, string clmRemark, int clmAddOperatorID,string clmTypeID) 
        {
            return CommonFunction.SaveLinkMan(cusID, clmID, clmName, clmSex, clmTel, clmMobile, clmEmail, clmQQ, clmWeChat, clmRemark, clmAddOperatorID,clmTypeID);
        }

        [WebMethod]
        public string GetDepartment(string cwOperatorID) 
        {
            return CommonFunction.GetDepartment(cwOperatorID);
        }

        [WebMethod]
        public string SaveCoWorker(string cwRelatedID, string cwOperatorID, string CurrentOperatorID, string cwSource) 
        {
            return CommonFunction.SaveCoWorker(cwRelatedID, cwOperatorID, CurrentOperatorID, cwSource);
        }

        [WebMethod]
        public int DeleteCoWorkerView(int ID, int CurrentOperatorID) 
        {
            return CommonFunction.DeleteCoWorkerView(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveFollowPlan(int cfpID, string cfpRelatedID, string cfpSource, string cfpContent, string cfpFilePath, string cfpDate, string cfpOperatorID) 
        {
            return CommonFunction.SaveFollowPlan(cfpID, cfpRelatedID, cfpSource, cfpContent, cfpFilePath, cfpDate, cfpOperatorID);
        }

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
        public CustomerReceiptResult GetReceipt(int id) 
        {
            return CommonFunction.GetReceipt(id);
        }

        [WebMethod]
        public string DeleteReceipt(int ID,int CurrentOperatorID)
        {
            return CommonFunction.DeleteReceipt(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string GetBusinessReceiptAmount(string crBusinessID, string crAmount, string crID) 
        {
            return CommonFunction.GetBusinessReceiptAmount(crBusinessID, crAmount, crID);
        }

        [WebMethod]
        public string SaveReceipt(int crCustomerID, int crID, string crBusinessID, string crAmount, string crOperatorID, string crDate, string CurrentOperatorID, string crTypeID, string crBatchID, string crRemark) 
        {
            return CommonFunction.SaveReceipt(crCustomerID, crID, crBusinessID, crAmount, crOperatorID, crDate, CurrentOperatorID, crTypeID, crBatchID, crRemark);
        }

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
        public string GetBusinessReceiptPlanAmount(string crpBusinessID, string crpAmount, string crpID)
        {
            return CommonFunction.GetBusinessReceiptPlanAmount(crpBusinessID, crpAmount, crpID);
        }

        [WebMethod]
        public string SaveReceiptPlan(int crpCustomerID, int crpID, string crpBusinessID, string crpAmount, string crpOperatorID, string crpDate, string CurrentOperatorID, string crpTypeID, string crpBatchID, string crpRemark)
        {
            return CommonFunction.SaveReceiptPlan(crpCustomerID, crpID, crpBusinessID, crpAmount, crpOperatorID, crpDate, CurrentOperatorID, crpTypeID, crpBatchID, crpRemark);
        }

        [WebMethod]
        public FeedbackList GetFeedback(int id) 
        {
            return CommonFunction.GetFeedback(id);
        }

        [WebMethod]
        public string DeleteFeedback(int ID, int CurrentOperatorID) 
        {
            return CommonFunction.DeleteFeedback(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveFeedback(int cfbCustomerID, int cfbID, string cfbFeedbackTypeID, string cfbLinkman, string cfbTelephone, string cfbEmail, string cfbOrderRelated, string cfbContent, string cfbHandleOperatorID, string CurrentOperatorID, string cfbResult, string cfbStatusID) 
        {
            return CommonFunction.SaveFeedback(cfbCustomerID, cfbID, cfbFeedbackTypeID, cfbLinkman, cfbTelephone, cfbEmail, cfbOrderRelated, cfbContent, cfbHandleOperatorID, CurrentOperatorID, cfbResult, cfbStatusID);
        }

        [WebMethod]
        public CustomerOpptunityResult GetOpptunity(int coID) 
        {
            return CommonFunction.GetOpptunity(coID);
        }

        [WebMethod]
        public string DeleteOpptunity(int ID,int CurrentOperatorID) 
        {
            return CommonFunction.DeleteOpptunity(ID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveOpptunity(int cusID, int coID, string coName, string coPhaseID, string coPredictAmount, string coPredictFinishDate, string coStatusID, string coOpptunitySourceID, string coOperatorID, string coDate, string CurrentOperatorID) 
        {
            return CommonFunction.SaveOpptunity(cusID, coID, coName, coPhaseID, coPredictAmount, coPredictFinishDate, coStatusID, coOpptunitySourceID, coOperatorID, coDate, CurrentOperatorID);
        }

        [WebMethod]
        public string ChangeOpptunityStatus(int ID, string coStatusID, int CurrentOperatorID)
        {
            return CommonFunction.ChangeOpptunityStatus(ID, coStatusID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveDocument(string cfNane, string cfCN, string cfUserID, string cfCustomerID) 
        {
            return CommonFunction.SaveDocument(cfNane, cfCN, cfUserID, cfCustomerID);
        }

        [WebMethod]
        public string DeleteDocument(int ID, int CurrentOperatorID)
        {
            return CommonFunction.DeleteDocument(ID, CurrentOperatorID);
        }

        [WebMethod]
        public int ChangeCustomerOperator(int cusID, int cusOperatorID, int CurrentOperatorID) 
        {
            return CommonFunction.ChangeCustomerOperator(cusID, cusOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public int PutInOrGetFromCustomerPool(int ID, int CurrentOperatorID) 
        {
            return CommonFunction.PutInOrGetFromCustomerPool(ID, CurrentOperatorID);
        }

        [WebMethod]
        public int DeleteCustomer(int id, int CurrentOperatorID) 
        {
            return CommonFunction.DeleteCustomer(id, CurrentOperatorID);
        }

        [WebMethod]
        public string AddCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string clmSex, string clmMobile, string coName, string coPhaseID, string coPredictAmount, DateTime coPredictFinishDate, string CurrentOperatorID, string cusExtType2, string cusAreaID, string OpptunityID, string ClueID, string cusCN) 
        {
            return CommonFunction.AddCustomerInfo(cusID, cusName, cusKindID, cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, cusLongtitude, cusLatitude, cusContactor, cusTel, clmSex, clmMobile, coName, coPhaseID, coPredictAmount, coPredictFinishDate, CurrentOperatorID, cusExtType2, cusAreaID, OpptunityID, ClueID,cusCN);
        }

        [WebMethod]
        public string GetEachFileForWeb(string filePath)
        {
            return CommonFunction.GetEachFileForWeb(filePath);
        }
        #endregion

        #region CustomerClueEdit
        [WebMethod]
        public string SaveCustomerClue(string ccActivityID, int ccID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID) 
        {
            return CommonFunction.SaveCustomerClue(ccActivityID,ccID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
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
        public string AddCustomerClue(string ccActivityID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID) 
        {
            return CommonFunction.AddCustomerClue(ccActivityID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
        }

        [WebMethod]
        public string AddMarketingActivityInfo(string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID) 
        {
            return CommonFunction.AddMarketingActivityInfo(maName, maStartDate, maEndDate, maTypeID, maDescription, maPredictCost, maPredictAmount, maActualCost, maPredictNum, maActualNum, maRemark, maDepartmentID, maOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveMarketingActivityClue(int ccID,string ccActivityID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID) 
        {
            return CommonFunction.SaveMarketingActivityClue(ccID,ccActivityID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID);
        }

        [WebMethod]
        public string SaveMarketingActivity(string maID, string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID) 
        {
            return CommonFunction.SaveMarketingActivity(maID,maName, maStartDate, maEndDate, maTypeID, maDescription, maPredictCost, maPredictAmount, maActualCost, maPredictNum, maActualNum, maRemark, maDepartmentID, maOperatorID, CurrentOperatorID);
        }

        [WebMethod]
        public CustomerCLueInfo GetMarketActivityClue(int ID) 
        {
            return CommonFunction.GetMarketActivityClue(ID);
        }

        #endregion

        #region ProjectEdit
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

        [WebMethod]
        public string GetSuccessFile(string filePath) 
        {
            return CommonFunction.GetSuccessFile(filePath);
        }
        #endregion

        #region CustomerFeedback
        [WebMethod]
        public string HandleFeedback(int cfbID, string cfbResult, string cfbStatus)
        {
            return CommonFunction.HandleFeedback(cfbID, cfbResult, cfbStatus);
        }
        #endregion

        #region WorkDiary
        public string SaveWorkDiary(string wdDate, string wdTitle, string wdContent, string wdNotifierID, string wdAddOperatorID, string wdTypeID, string wdFile) 
        {
            return CommonFunction.SaveWorkDiary(wdDate, wdTitle, wdContent, wdNotifierID, wdAddOperatorID, wdTypeID, wdFile);
        }

        #endregion
    }
}
