using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SmartSoft.Component
{
    public abstract class CommonFunction
    {
        #region Common Method
        public static void AddOperatorLog(string dosth, int customerID, string CurrentOperatorID)
        {
            string deviceSource = "Mobile";
            if (System.Configuration.ConfigurationManager.AppSettings["DeviceSource"] + "" != "")
            {
                deviceSource = System.Configuration.ConfigurationManager.AppSettings["DeviceSource"] + "";
            }

            string olID = DbHelperSQL.GetSHSLInt("usp_GetID 'OperatorLog'");
            DbHelperSQL.ExecuteSQL(@"Insert into OperatorLog(
            olID,
            olCustomerID,
            olOperatorID,
            olContent,
            olFromIP,
            olDate,
            olOperateSource) 
            values 
            (" + olID + "," + customerID + ",'" + CurrentOperatorID + "','" + dosth + "','" + System.Web.HttpContext.Current.Request.UserHostAddress.ToString() + "','" + DateTime.Now.ToString() + "','" + deviceSource + "') ");

        }

        public static void AddOperatorLog(string dosth, string CurrentOperatorID)
        {
            AddOperatorLog(dosth, 0, CurrentOperatorID);
        }

        public static void AddsysLog(string message)
        {
            DbHelperSQL.ExecuteSQL("insert into sysLog(LogTime, Message) values(getdate(), '" + message + "')");
        }

        public static string GetOperatorName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                return DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + id);
            }
        }

        public static string GetSourceName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                return DbHelperSQL.GetSHSL("select Name from defCommon where ID=" + id);
            }
        }

        public static string GetPhaseName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                return DbHelperSQL.GetSHSL("select copName from defCustomerOpptunityPhase where copID=" + id);
            }
        }

        public static string GetStatusName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                return DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='OpptunityStatus' and sID=" + id);
            }
        }

        public static string GetOpptunityName(string id)
        {
            if (id == "")
            {
                return "";
            }
            else
            {
                return DbHelperSQL.GetSHSL("select coName from CustomerOpptunity where coID=" + id);
            }
        }

        public static bool HasViewAllCustomer(string opeID)
        {
            bool hasViewAllCustomer = DbHelperSQL.Exists(string.Format("select * from Operators where opeID = {0} and opeDataRange = 3", opeID));
            return hasViewAllCustomer;
        }

        public static bool HasViewDepartment(string opeID)
        {
            bool hasView = DbHelperSQL.Exists(string.Format("select * from Operators where opeID = {0} and opeDataRange = 2", opeID));
            return hasView;
        }

        public static string GetPermissionWhereCondition(string operatorField, string CurrentOperatorID)
        {
            string whereCondition = "";
            bool hasViewAllCustomer = HasViewAllCustomer(CurrentOperatorID);
            if (!hasViewAllCustomer)
            {
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + CurrentOperatorID + ")";
                bool hasViewDepartment = HasViewDepartment(CurrentOperatorID);
                if (hasViewDepartment)
                {
                    string departmentID = DbHelperSQL.GetSHSL("Select opeDepartmentID from Operators where opeID = " + CurrentOperatorID);
                    whereCondition += " AND " + operatorField + " in (select opeID from Operators where opeDepartmentID = " + departmentID + condition + ")";
                }
                else
                {
                    whereCondition += " AND " + operatorField + " in (select opeID from Operators where opeID = " + CurrentOperatorID + condition + ")";
                }
            }
            return whereCondition;
        }

        public static string GetPermissionWhereCondition(string operatorField, string CurrentOperatorID, string addUserFiled)
        {
            string whereCondition = "";
            bool hasViewAllCustomer = HasViewAllCustomer(CurrentOperatorID);
            if (!hasViewAllCustomer)
            {
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + CurrentOperatorID + ")";
                bool hasViewDepartment = HasViewDepartment(CurrentOperatorID);
                if (hasViewDepartment)
                {
                    string departmentID = DbHelperSQL.GetSHSL("Select opeDepartmentID from Operators where opeID = " + CurrentOperatorID);
                    whereCondition += " AND " + operatorField + " in (select opeID from Operators where opeDepartmentID = " + departmentID + condition + ")";
                }
                else
                {
                    whereCondition += " AND (" + operatorField + " in (select opeID from Operators where opeID = " + CurrentOperatorID + condition + ") OR " + addUserFiled + "=" + CurrentOperatorID + ")";
                }
            }
            return whereCondition;
        }

        public static bool IsAdmin(string operatorID)
        {
            string opeIsAdmin = DbHelperSQL.GetSHSL("select opeIsAdmin from Operators where opeID = " + operatorID);
            if (opeIsAdmin == "1" || opeIsAdmin == "True")
            {
                return true;
            }
            return false;
        }

        public static bool CheckPermissionForCustomer(string cusID, string operatorID)
        {
            //管理员有权限
            if (IsAdmin(operatorID))
            {
                return true;
            }

            //只有负责人自已有权限管理自己负责的客户，其它人没有权限
            return DbHelperSQL.Exists("select * from Customer where cusID = " + cusID + " and cusOperatorID=" + operatorID);
        }

        public static bool CheckPermissionForOpptunity(string coID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerOpptunity where coID = " + coID + " and coOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForFollowHistory(string cfhID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerFollowHistory where cfhID = " + cfhID + " and cfhOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForFollowPlan(string cfpID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerFollowPlan where cfpID = " + cfpID + " and cfpOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForBusiness(string cbID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人，或者通知人有权限
            return DbHelperSQL.Exists("select * from CustomerBusiness where cbID = " + cbID + " and (cbOperatorID = " + CurrentOperatorID + " or cbNotifyOperatorID = " + CurrentOperatorID + ")");
        }

        public static bool CheckPermissionForReceipt(string crID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerReceipt where crID = " + crID + " and crOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForReceiptPlan(string crpID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerReceiptPlan where crpID = " + crpID + " and crpOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForClue(string ccID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from CustomerClue where ccID = " + ccID + " and ccOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForProject(string pjID, string CurrentOperatorID)
        {
            //已经审核的项目不能修改和删除
            if (DbHelperSQL.Exists("select * from Project where pjApproveTag = '已审核' and pjID = " + pjID))
            {
                return false;
            }

            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人和审核人有权限
            return DbHelperSQL.Exists("select * from Project where pjID = " + pjID + " and (pjOperatorID = " + CurrentOperatorID + " or pjToApproveOperatorID=" + CurrentOperatorID + ")");
        }

        public static bool CheckPermissionForWorkDiary(string wdID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人有权限
            return DbHelperSQL.Exists("select * from WorkDiary where wdID = " + wdID + " and wdAddOperatorID = " + CurrentOperatorID);
        }

        public static bool CheckPermissionForFeedback(string cfbID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人和处理人有权限
            return DbHelperSQL.Exists("select * from CustomerFeedback where cfbID = " + cfbID + " and (cfbOperatorID = " + CurrentOperatorID + " or cfbHandleOperatorID = " + CurrentOperatorID + ")");
        }

        public static bool CheckPermissionForMarketingActivity(string maID, string CurrentOperatorID)
        {
            //管理员有权限
            if (IsAdmin(CurrentOperatorID))
            {
                return true;
            }

            //负责人和处理人有权限
            return DbHelperSQL.Exists("select * from MarketingActivity where maID = " + maID + " and maOperatorID = " + CurrentOperatorID);
        }

        public static void InitDataSource(DropDownList ddl, string dataName)
        {
            switch (dataName)
            {
                case "CD_Operators":
                    DbHelperSQL.BindDropDownList("select * from Operators order by opeName asc", ddl, "opeName", "opeID");
                    break;

                case "CD_Department":
                    DbHelperSQL.BindDropDownList("select * from Department order by depID asc", ddl, "depName", "depID");
                    break;

                case "CD_defCustomerType":
                    DbHelperSQL.BindDropDownList("select * from defCustomerType order by ctOrderBy asc", ddl, "ctName", "ctID");
                    break;

                case "CD_defCustomerOpptunityPhase":
                    DbHelperSQL.BindDropDownList("select * from defCustomerOpptunityPhase order by copOrderBy asc", ddl, "copName", "copID");
                    break;

                case "CD_defCustomerArea":
                    DbHelperSQL.BindDropDownList("select * from defCustomerArea order by caCode asc", ddl, "caName", "caID");
                    break;

                case "CD_MarketingActivity":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from MarketingActivity order by maID desc", ddl, "maName", "maID");
                    break;

                default:
                    DbHelperSQL.BindDropDownList(@"select * from
(
select ID,Name,OrderBy from defCommon where TableName = '" + dataName.Replace("CD_", "") + @"' 
union all
select sID as ID,sName as Name,sID as OrderBy from sdefCommon where sTypeName = '" + dataName.Replace("CD_", "") + @"'
) a order by OrderBy asc", ddl, "Name", "ID");
                    break;
            }
        }

        public static void InitDataSourceAddEmpty(DropDownList ddl, string dataName)
        {
            switch (dataName)
            {
                case "CD_Operators":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from Operators order by opeName asc", ddl, "opeName", "opeID");
                    break;

                case "CD_Department":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from Department order by depID asc", ddl, "depName", "depID");
                    break;

                case "CD_defCustomerType":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from defCustomerType order by ctOrderBy asc", ddl, "ctName", "ctID");
                    break;

                case "CD_defCustomerOpptunityPhase":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from defCustomerOpptunityPhase order by copOrderBy asc", ddl, "copName", "copID");
                    break;

                case "CD_defCustomerArea":
                    DbHelperSQL.BindDropDownListAddEmpty("select * from defCustomerArea order by caCode asc", ddl, "caName", "caID");
                    break;

                default:
                    DbHelperSQL.BindDropDownListAddEmpty(@"select * from
(
select ID,Name,OrderBy from defCommon where TableName = '" + dataName.Replace("CD_", "") + @"' 
union all
select sID as ID,sName as Name,sID as OrderBy from sdefCommon where sTypeName = '" + dataName.Replace("CD_", "") + @"'
) a order by OrderBy asc", ddl, "Name", "ID");
                    break;
            }
        }

        public static bool IsValidImage(string extension)
        {
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }
            else
            {
                extension = extension.ToLower();
                if (extension.Contains("png") || extension.Contains("gif") || extension.Contains("jpg") || extension.Contains("jpeg"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string GetPermissionString(string CurrentOperatorID)
        {
            string result = "Knowledge";//知识库必有
            result += HasViewSepcialPage("ProjectList.aspx", CurrentOperatorID) ? ",Project" : "";
            result += HasViewSepcialPage("CustomerClueList.aspx", CurrentOperatorID) ? ",CustomerClue" : "";
            result += HasViewSepcialPage("CustomerList.aspx", CurrentOperatorID) ? ",Customer" : "";
            result += HasViewSepcialPage("CustomerOpptunityList.aspx", CurrentOperatorID) ? ",CustomerOpptunity" : "";
            result += HasViewSepcialPage("CustomerBusinessList.aspx", CurrentOperatorID) ? ",CustomerBusiness" : "";
            result += HasViewSepcialPage("CustomerReceiptList.aspx", CurrentOperatorID) ? ",CustomerReceipt" : "";
            result += HasViewSepcialPage("CustomerPoolList.aspx", CurrentOperatorID) ? ",CustomerPool" : "";
            result += HasViewSepcialPage("CustomerFollowHistoryList.aspx", CurrentOperatorID) ? ",CustomerFollowHistory" : "";
            result += HasViewSepcialPage("CustomerFeedbackList.aspx", CurrentOperatorID) ? ",CustomerFeedback" : "";
            result += HasViewSepcialPage("WorkDiaryList.aspx", CurrentOperatorID) ? ",WorkDiary" : "";
            result += HasViewSepcialPage("CustomerLinkManList.aspx", CurrentOperatorID) ? ",CustomerLinkMan" : "";
            return result;
        }

        public static bool HasViewSepcialPage(string url, string opeID)
        {
            DataTable dt = DbHelperSQL.GetDataTable(string.Format(@"select * from sysObjectPurview A INNER JOIN sysPage B ON A.PageID = B.PageID 
                where PageFilePath like '%" + url + @"%'  and PurviewCode > 0 and (ObjectID = {0} OR ObjectID in (select RoleID from sysOperatorInRole where OperatorID = {0}))", opeID));
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasViewSepcialPageCode(string url, string opeID, long code)
        {
            DataTable dt = DbHelperSQL.GetDataTable(string.Format(@"select * from sysObjectPurview A INNER JOIN sysPage B ON A.PageID = B.PageID 
                where PageFilePath like '%" + url + @"%'  and PurviewCode > 0 and PurviewCode & convert(bigint,{1}) > 0 and (ObjectID = {0} OR ObjectID in (select RoleID from sysOperatorInRole where OperatorID = {0}))", opeID, code));
            if (dt != null &&dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetToDoCount
        //N天未跟进客户
        public static string GetNDayNotFollowCustomerCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from Customer where dateadd(day," + nDay + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID);
        }

        //N天未跟进未成交客户,statusID = 64为已成交,65为搁置
        public static string GetNDayNotFollowNotSuccessCustomerCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from Customer where cusStatusID in (61,62,63) and dateadd(day," + nDay + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID);
        }

        //N天未成交客户
        public static string GetNDayNotSuccessCustomerCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from Customer where cusStatusID in (61,62,63) and dateadd(day," + nDay + ",cusAddDate) < getdate() and cusOperatorID=" + CurrentOperatorID);
        }

        //N天未跟进成交客户数
        public static string GetNDayNotFollowSuccessCustomerCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from Customer where cusStatusID = 64 and dateadd(day," + nDay + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID);
        }

        //N天内签单的商机数
        public static string GetNDaySuccessOpptunityCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from CustomerOpptunity where coStatusID = 1 and dateadd(day," + nDay + ",coPredictFinishDate) > getdate() and coOperatorID=" + CurrentOperatorID);
        }

        //N天将到期的合同数
        public static string GetNDayExpireBusinessCount(string CurrentOperatorID, string nDay)
        {
            return DbHelperSQL.GetSHSL("select count(*) from CustomerBusiness where cbStatus in (66,67,68) and dateadd(day," + nDay + ",cbExpiredDate) > getdate() and cbOperatorID=" + CurrentOperatorID);
        }

        //N天将过生日的联系人数
        public static string GetNDayBirthdayLinkManCount(string CurrentOperatorID, string nDay)
        {
            string startBirthMonth = DateTime.Now.Month.ToString();
            string startBirthDay = DateTime.Now.Day.ToString();
            string endBirthMonth = DateTime.Now.AddDays(int.Parse(nDay) - 1).Month.ToString();
            string endBirthDay = DateTime.Now.AddDays(int.Parse(nDay) - 1).Day.ToString();
            return DbHelperSQL.GetSHSL(string.Format("select count(*) from CustomerLinkMan where month(clmBirthDay) >= '{0}' and day(clmBirthDay) >= '{1}' and month(clmBirthday) <= '{2}' and day(clmBirthday) <='{3}' and clmCustomerID in (select cusID from Customer where cusOperatorID = {4})", startBirthMonth, startBirthDay, endBirthMonth, endBirthDay, CurrentOperatorID));
        }

        public static string GetToDoCount(string currentOperatorID, string conditionType, string conditionValue)
        {
            string result = "";
            switch (conditionType)
            {
                case "NDayNotFollowCustomer":
                    return GetNDayNotFollowCustomerCount(currentOperatorID, conditionValue);

                case "NDayNotFollowNotSuccessCustomer":
                    return GetNDayNotFollowNotSuccessCustomerCount(currentOperatorID, conditionValue);

                case "NDayNotSuccessCustomer":
                    return GetNDayNotSuccessCustomerCount(currentOperatorID, conditionValue);

                case "NDayNotFollowSuccessCustomer":
                    return GetNDayNotFollowSuccessCustomerCount(currentOperatorID, conditionValue);

                case "NDaySuccessOpptunity":
                    return GetNDaySuccessOpptunityCount(currentOperatorID, conditionValue);

                case "NDayExpireBusiness":
                    return GetNDayExpireBusinessCount(currentOperatorID, conditionValue);

                case "NDayBirthdayLinkMan":
                    return GetNDayBirthdayLinkManCount(currentOperatorID, conditionValue);
            }
            return result;
        }

        #endregion

        #region Message
        public static string SetMessageReaded(string MessageID, string OperatorID)
        {
            try
            {
                DbHelperSQL.ExecuteSQL(string.Format("if not exists(select * from sysMessage_Readed where MessageID={0} and OperatorID = {1}) begin Insert into sysMessage_Readed (MessageID,OperatorID) values({0},{1}); end", MessageID, OperatorID));
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        public static void DeleteMessageByURL(string url)
        {
            DbHelperSQL.ExecuteSQL("Delete from sysMessage where URL like '" + url + "&MessageID%' and sendtime > getdate()");
        }

        public static void DeleteMessageByURL(string url, int sendOperatorID, string title, string content)
        {
            DbHelperSQL.ExecuteSQL("Delete from sysMessage where URL like '" + url + "&MessageID%' and sendoperatorid=" + sendOperatorID + " and title='" + title + "' and messagecontent = '" + content + "' and sendtime > getdate()");
        }

        public static string AddMessages(int sendOperatorID, int toOperatorID, string title, string content, string url, string d)
        {
            try 
            {
                DeleteMessageByURL(url, sendOperatorID, title, content);
                DateTime sendTime = DateTime.Now;
                DateTime.TryParse(d, out sendTime);
                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'sysMessage'");
                DbHelperSQL.ExecuteSQL(string.Format(@"Insert Into sysMessage (
				        messageid,
				        messagetypeid,
				        sendoperatorid,
				        sendtime,
				        messagecontent,
				        awoketime,
				        url,
                title
			    )Values(
				        {0},
				        1,
				        {1},
				        '{2}',
				        '{3}',
				        '{4}',
				        '{5}',
                        '{6}'
			    )", id, sendOperatorID, sendTime.ToString("yyyy-MM-dd HH:mm:ss"), content, sendTime.ToString("yyyy-MM-dd HH:mm:ss"), url, title));

                DbHelperSQL.ExecuteSQL(string.Format(@"Insert Into sysMessage_Looker (
				        messageid,
				        objectid
			    )Values(
				        {0},
				        {1}
			    )", id, toOperatorID));

                DbHelperSQL.ExecuteSQL(string.Format(@"Update sysMessage set url='{0}' where messageid={1}", url + "&MessageID=" + id, id));

                return id;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

      
        private static void SendMessageToCoWorkerForCustomer(string cusID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select cwOperatorID from CoWorker where cwSource='Customer' and cwRelatedID = " + cusID);
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(currentOperatorID) != int.Parse(row["cwOperatorID"] + ""))
                {
                    AddMessages(int.Parse(currentOperatorID), int.Parse(row["cwOperatorID"] + ""), title, messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            DataRow r = DbHelperSQL.GetDataRow("select cusOperatorID,cusName from Customer where cusID = " + cusID);
            if (int.Parse(currentOperatorID) != int.Parse(r["cusOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["cusOperatorID"] + ""), title, "[" + r["cusName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private static void SendMessageToCoWorkerForOpptunity(string coID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select cwOperatorID from CoWorker where cwSource='Opptunity' and cwRelatedID = " + coID);
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(currentOperatorID) != int.Parse(row["cwOperatorID"] + ""))
                {
                    AddMessages(int.Parse(currentOperatorID), int.Parse(row["cwOperatorID"] + ""), title, messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            DataRow r = DbHelperSQL.GetDataRow("select coOperatorID,coName from CustomerOpptunity where coID = " + coID);
            if (int.Parse(currentOperatorID) != int.Parse(r["coOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["coOperatorID"] + ""), title, "[" + r["coName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private static void SendMessageToCoWorkerForBusiness(string cbID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select cwOperatorID from CoWorker where cwSource='Business' and cwRelatedID = " + cbID);
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(currentOperatorID) != int.Parse(row["cwOperatorID"] + ""))
                {
                    AddMessages(int.Parse(currentOperatorID), int.Parse(row["cwOperatorID"] + ""), title, messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            DataRow r = DbHelperSQL.GetDataRow("select cbOperatorID,cbNotifyOperatorID, cbName from CustomerBusiness where cbID = " + cbID);
            if (int.Parse(currentOperatorID) != int.Parse(r["cbOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["cbOperatorID"] + ""), title, "[" + r["cbName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (r["cbNotifyOperatorID"] + "" != "" && int.Parse(currentOperatorID) != int.Parse(r["cbNotifyOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["cbNotifyOperatorID"] + ""), title, "[" + r["cbName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private static void SendMessageToCoWorkerForProject(string pjID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataRow r = DbHelperSQL.GetDataRow("select pjOperatorID,pjToApproveOperatorID,pjName from Project where pjID = " + pjID);
            if (int.Parse(currentOperatorID) != int.Parse(r["pjOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["pjOperatorID"] + ""), title, "[" + r["pjName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (int.Parse(currentOperatorID) != int.Parse(r["pjToApproveOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["pjToApproveOperatorID"] + ""), title, "[" + r["pjName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private static void SendMessageToCoWorkerForFeedback(string cfbID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataRow r = DbHelperSQL.GetDataRow("select cfbOperatorID,cfbHandleOperatorID,cfbContent from CustomerFeedback where cfbID = " + cfbID);
            if (int.Parse(currentOperatorID) != int.Parse(r["cfbOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["cfbOperatorID"] + ""), title, "[" + r["cfbContent"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (int.Parse(currentOperatorID) != int.Parse(r["cfbHandleOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["cfbHandleOperatorID"] + ""), title, "[" + r["cfbContent"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private static void SendMessageToCoWorkerForMarketing(string maID, string currentOperatorID, string title, string messageContent, string url)
        {
            DataRow r = DbHelperSQL.GetDataRow("select maOperatorID,maName from MarketingActivity where maID = " + maID);
            if (int.Parse(currentOperatorID) != int.Parse(r["maOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["maOperatorID"] + ""), title, "[" + r["maName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (int.Parse(currentOperatorID) != int.Parse(r["maOperatorID"] + ""))
            {
                AddMessages(int.Parse(currentOperatorID), int.Parse(r["maOperatorID"] + ""), title, "[" + r["maName"] + "]" + messageContent, url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public static string SendMessage(string MessageContent, string ID, string CurrentOperatorID)
        {
            try
            {
                string[] Array = ID.Split(',');
                foreach (string i in Array)
                {
                    if (i != "")
                    {
                        AddMessages(int.Parse(CurrentOperatorID), int.Parse(i), "用户消息", MessageContent, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                }
                return "OK";
            }
            catch
            {
                return "NO";
            }
        }
        #endregion

        #region DashBoard
        public static void UpdateOperatorLastActiveTime(string opeID)
        {
            DbHelperSQL.ExecuteSQL("update Operators set opeLastActiveTime = getdate() where opeID = " + opeID);
        }

        public static WorkbenchList GetWorkbenchDataByCondition(string DateValue, string RoleValue, string CurrentOperatorID)
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/Face/";
                WorkbenchList Item = new WorkbenchList();
                string Sql = "";
                string departmentid = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID = " + CurrentOperatorID);
                string dataRange = DbHelperSQL.GetSHSLInt("select opeDataRange from Operators where opeID = " + CurrentOperatorID);
                //荣誉榜
                //收款额
                Sql = @"select top 1 opeName,opeUrl,ISNULL(SUM(crAmount),0) as Amount from CustomerReceipt A INNER JOIN Operators B ON A.crOperatorID = B.opeID
where (1=1) " + GetWhereForDate("crDate", DateValue) + @"
group by opeName,opeUrl
order by SUM(crAmount) desc";
                DataRow dr = DbHelperSQL.GetDataRow(Sql);
                if (dr != null)
                {
                    Item.MaxReceiptAmountOperatorName = dr["opeName"] + "";
                    Item.MaxReceiptAmount = decimal.Parse(dr["Amount"] + "").ToString("N0");
                    Item.MaxReceiptAmountOperatorFace = path + dr["opeUrl"] + "";
                }
                else
                {
                    Item.MaxReceiptAmountOperatorName = "未有人员获取第一哦";
                    Item.MaxReceiptAmount = "0.00";
                    Item.MaxReceiptAmountOperatorFace = "" + path + "denglu.png";
                }
                //荣誉榜
                //合同额
                Sql = @"select top 1 opeName,opeUrl,ISNULL(SUM(cbTotalAmount),0) as Amount from CustomerBusiness A INNER JOIN Operators B ON A.cbOperatorID = B.opeID
where (1=1) " + GetWhereForDate("cbDate", DateValue) + @"
group by opeName,opeUrl
order by SUM(cbTotalAmount) desc";
                dr = DbHelperSQL.GetDataRow(Sql);
                if (dr != null)
                {
                    Item.MaxBusinessAmountOperatorName = dr["opeName"] + "";
                    Item.MaxBusinessAmount = decimal.Parse(dr["Amount"] + "").ToString("N0");
                    Item.MaxBusinessAmountOperatorFace = "" + path + dr["opeUrl"] + "";
                }
                else
                {
                    Item.MaxBusinessAmountOperatorName = "未有人员获取第一哦";
                    Item.MaxBusinessAmount = "0.00";
                    Item.MaxBusinessAmountOperatorFace = "" + path + "denglu.png";
                }
                //荣誉榜
                //商机额
                Sql = @"select top 1 opeName,opeUrl,ISNULL(SUM(coPredictAmount),0) as Amount from CustomerOpptunity A INNER JOIN Operators B ON A.coOperatorID = B.opeID
where (1=1) " + GetWhereForDate("coAddDate", DateValue) + @"
group by opeName,opeUrl
order by SUM(coPredictAmount) desc";
                dr = DbHelperSQL.GetDataRow(Sql);
                if (dr != null)
                {
                    Item.MaxOpptunityAmountOperatorName = dr["opeName"] + "";
                    Item.MaxOpptunityAmount = decimal.Parse(dr["Amount"] + "").ToString("N0");
                    Item.MaxOpptunityAmountOperatorFace = "" + path + dr["opeUrl"] + "";
                }
                else
                {
                    Item.MaxOpptunityAmountOperatorName = "未有人员获取第一哦";
                    Item.MaxOpptunityAmount = "0.00";
                    Item.MaxOpptunityAmountOperatorFace = "" + path + "denglu.png";
                }
                //荣誉榜
                //客户数
                Sql = @"select top 1 opeName,opeUrl,count(*) as Amount from Customer A INNER JOIN Operators B ON A.cusOperatorID = B.opeID
where (1=1) " + GetWhereForDate("cusAddDate", DateValue) + @"
group by opeName,opeUrl
order by count(*) desc";
                dr = DbHelperSQL.GetDataRow(Sql);
                if (dr != null)
                {
                    Item.MaxCustomerCountOperatorName = dr["opeName"] + "";
                    Item.MaxCustomerCount = dr["Amount"] + "";
                    Item.MaxCustomerCountOperatorFace = "" + path + dr["opeUrl"] + "";
                }
                else
                {
                    Item.MaxCustomerCountOperatorName = "未有人员获取第一哦";
                    Item.MaxCustomerCount = "0";
                    Item.MaxCustomerCountOperatorFace = "" + path + "denglu.png";
                }
                //荣誉榜
                //跟进数
                Sql = @"select top 1 opeName,opeUrl,count(*) as Amount from CustomerFollowHistory A INNER JOIN Operators B ON A.cfhOperatorID = B.opeID
where (1=1) " + GetWhereForDate("cfhAddDate", DateValue) + @"
group by opeName,opeUrl
order by count(*) desc";
                dr = DbHelperSQL.GetDataRow(Sql);
                if (dr != null)
                {
                    Item.MaxFollowHistoryCountOperatorName = dr["opeName"] + "";
                    Item.MaxFollowHistoryCount = dr["Amount"] + "";
                    Item.MaxFollowHistoryCountOperatorFace = "" + path + dr["opeUrl"] + "";
                }
                else
                {
                    Item.MaxFollowHistoryCountOperatorName = "未有人员获取第一哦";
                    Item.MaxFollowHistoryCount = "0";
                    Item.MaxFollowHistoryCountOperatorFace = "" + path + "denglu.png";
                }

                //业绩简报
                //收款额
                Sql = "select ISNULL(SUM(crAmount),0) from CustomerReceipt where (1=1) " + GetWhereForDate("crDate", DateValue) + GetWhereForRole("crOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumReceiptAmount = decimal.Parse(DbHelperSQL.GetSHSL(Sql)).ToString("N0");
                //业绩简报
                //合同额
                Sql = "select ISNULL(SUM(cbTotalAmount),0) from CustomerBusiness where (1=1) " + GetWhereForDate("cbDate", DateValue) + GetWhereForRole("cbOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumBusinessAmount = decimal.Parse(DbHelperSQL.GetSHSL(Sql)).ToString("N0");
                //业绩简报
                //商机额
                Sql = "select ISNULL(SUM(coPredictAmount),0) from CustomerOpptunity where (1=1) " + GetWhereForDate("coDate", DateValue) + GetWhereForRole("coOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumOpptunityAmount = decimal.Parse(DbHelperSQL.GetSHSL(Sql)).ToString("N0");

                //业绩目标
                //收款目标额
                Item.SumReceiptTargetAmount = decimal.Parse(getSumReceiptTargetAmount(CurrentOperatorID, DateValue, RoleValue, dataRange, departmentid)).ToString("N0");
                //合同目标额
                Item.SumBusinessTargetAmount = decimal.Parse(getSumOrderTargetAmount(CurrentOperatorID, DateValue, RoleValue, dataRange, departmentid)).ToString("N0");

                //绩效
                //新增客户数
                Sql = "select count(*) from Customer where (1=1) " + GetWhereForDate("cusAddDate", DateValue) + GetWhereForRole("cusOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewCustomerCount = DbHelperSQL.GetSHSL(Sql);
                //绩效
                //新增跟进数
                Sql = "select count(*) from CustomerFollowHistory where (1=1) " + GetWhereForDate("cfhDate", DateValue) + GetWhereForRole("cfhOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewFollowCount = DbHelperSQL.GetSHSL(Sql);
                //绩效
                //新增合同数
                Sql = "select count(*) from CustomerBusiness where (1=1) " + GetWhereForDate("cbDate", DateValue) + GetWhereForRole("cbOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewBusinessCount = DbHelperSQL.GetSHSL(Sql);
                //绩效
                //新增收款数
                Sql = "select count(*) from CustomerReceipt where (1=1) " + GetWhereForDate("crDate", DateValue) + GetWhereForRole("crOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewReceiptCount = DbHelperSQL.GetSHSL(Sql);
                //绩效
                //新增商机数
                Sql = "select count(*) from CustomerOpptunity where (1=1) " + GetWhereForDate("coAddDate", DateValue) + GetWhereForRole("coOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewOpptunityCount = DbHelperSQL.GetSHSL(Sql);
                //绩效
                //新增线索数
                Sql = "select count(*) from CustomerClue where (1=1) " + GetWhereForDate("ccAddDate", DateValue) + GetWhereForRole("ccOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange);
                Item.SumAddNewClueCount = DbHelperSQL.GetSHSL(Sql);

                //销售漏斗
                Sql = @"select coPhaseID,COUNT(*) AS Count, ISNULL(SUM(coPredictAmount),0) AS Amount1,ISNULL(SUM(coPredictAmount*copRate),0) AS Amount2 from CustomerOpptunity A INNER JOIN defCustomerOpptunityPhase B ON A.coPhaseID = B.copID
where (1=1) " + GetWhereForDate("coAddDate", DateValue) + GetWhereForRole("coOperatorID", CurrentOperatorID, departmentid, RoleValue, dataRange) + @"
group by coPhaseID";
                DataTable dt = DbHelperSQL.GetDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string Count = r["Count"] + "";
                        if (Count != "")
                        {
                            Count = r["Count"] + "";
                        }
                        else
                        {
                            Count = "0";
                        }
                        string Amount1 = r["Amount1"] + "";
                        if (Amount1 != "")
                        {
                            Amount1 = decimal.Parse(r["Amount1"] + "").ToString("N0");
                        }
                        else
                        {
                            Amount1 = "0";
                        }
                        string Amount2 = r["Amount2"] + "";
                        if (Amount2 != "")
                        {
                            Amount2 = decimal.Parse(r["Amount2"] + "").ToString("N0");
                        }
                        else
                        {
                            Amount2 = "0";
                        }
                        switch (r["coPhaseID"] + "")
                        {

                            //方案论证
                            case "2":
                                Item.Phase2SumCount = Count;
                                Item.Phase2SumAmount = Amount1;
                                Item.Phase2SumPredictAmount = Amount2;
                                break;
                            //商务谈判
                            case "3":

                                Item.Phase3SumCount = Count;
                                Item.Phase3SumAmount = Amount1;
                                Item.Phase3SumPredictAmount = Amount2;
                                break;
                            //立即评估
                            case "4":
                                Item.Phase4SumCount = Count;
                                Item.Phase4SumAmount = Amount1;
                                Item.Phase4SumPredictAmount = Amount2;
                                break;
                            //需求调研
                            case "5":
                                Item.Phase5SumCount = Count;
                                Item.Phase5SumAmount = Amount1;
                                Item.Phase5SumPredictAmount = Amount2;
                                break;
                            //初期沟通
                            case "6":
                                Item.Phase6SumCount = Count;
                                Item.Phase6SumAmount = Amount1;
                                Item.Phase6SumPredictAmount = Amount2;
                                break;
                            //签订合同
                            case "7":
                                Item.Phase7SumCount = Count;
                                Item.Phase7SumAmount = Amount1;
                                Item.Phase7SumPredictAmount = Amount2;
                                break;
                        }
                    }
                }
                else
                {
                    //方案论证
                    Item.Phase2SumCount = "0";
                    Item.Phase2SumAmount = "0.00";
                    Item.Phase2SumPredictAmount = "0.00";
                    //商务谈判
                    Item.Phase3SumCount = "0";
                    Item.Phase3SumAmount = "0.00";
                    Item.Phase3SumPredictAmount = "0.00";
                    //立即评估
                    Item.Phase4SumCount = "0";
                    Item.Phase4SumAmount = "0.00";
                    Item.Phase4SumPredictAmount = "0.00";
                    //需求调研
                    Item.Phase5SumCount = "0";
                    Item.Phase5SumAmount = "0.00";
                    Item.Phase5SumPredictAmount = "0.00";
                    //初期沟通
                    Item.Phase6SumCount = "0";
                    Item.Phase6SumAmount = "0.00";
                    Item.Phase6SumPredictAmount = "0.00";
                    //签订合同
                    Item.Phase7SumCount = "0";
                    Item.Phase7SumAmount = "0.00";
                    Item.Phase7SumPredictAmount = "0.00";
                }

                return Item;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string getSumOrderTargetAmount(string CurrentOperatorID, string DateValue, string RoleValue, string dataRange, string departmentid)
        {
            string sql = "";
            DateTime currentDate = DateTime.Now;
            DateTime lastYear = DateTime.Now.AddYears(-1);
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            switch (DateValue)
            {
                case "本月":
                    sql = string.Format("select sum(isnull(opM{0},isnull(opeOrderAmount,0))) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={1} and opTypeID=1 ", currentDate.Month, currentDate.Year);
                    break;
                case "上月":
                    sql = string.Format("select sum(isnull(opM{0},isnull(opeOrderAmount,0))) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={1} and opTypeID=1 ", lastMonth.Month, lastMonth.Year);
                    break;
                case "今年":
                    sql = string.Format("select sum(isnull(opM1 + opM2 + opM3 + opM4 + opM5 + opM6 + opM7 + opM8 + opM9 + opM10 + opM11 + opM12,isnull(opeOrderAmount,0) * 12)) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={0} and opTypeID=1 ", currentDate.Year);
                    break;
                case "去年":
                    sql = string.Format("select sum(isnull(opM1 + opM2 + opM3 + opM4 + opM5 + opM6 + opM7 + opM8 + opM9 + opM10 + opM11 + opM12,isnull(opeOrderAmount,0) * 12)) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={0} and opTypeID=1 ", lastYear.Year);
                    break;
            }
            sql += " where (1=1) " + GetWhereForRole("opeID", CurrentOperatorID, departmentid, RoleValue, dataRange);
            return DbHelperSQL.GetSHSL(sql);
        }

        private static string getSumReceiptTargetAmount(string CurrentOperatorID, string DateValue, string RoleValue, string dataRange, string departmentid)
        {
            string sql = "";
            DateTime currentDate = DateTime.Now;
            DateTime lastYear = DateTime.Now.AddYears(-1);
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            switch (DateValue)
            {
                case "本月":
                    sql = string.Format("select sum(isnull(opM{0},isnull(opeOrderAmount,0))) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={1} and opTypeID=2 ", currentDate.Month, currentDate.Year);
                    break;
                case "上月":
                    sql = string.Format("select sum(isnull(opM{0},isnull(opeOrderAmount,0))) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={1} and opTypeID=2 ", lastMonth.Month, lastMonth.Year);
                    break;
                case "今年":
                    sql = string.Format("select sum(isnull(opM1 + opM2 + opM3 + opM4 + opM5 + opM6 + opM7 + opM8 + opM9 + opM10 + opM11 + opM12,isnull(opeOrderAmount,0) * 12)) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={0} and opTypeID=2 ", currentDate.Year);
                    break;
                case "去年":
                    sql = string.Format("select sum(isnull(opM1 + opM2 + opM3 + opM4 + opM5 + opM6 + opM7 + opM8 + opM9 + opM10 + opM11 + opM12,isnull(opeOrderAmount,0) * 12)) from Operators A LEFT OUTER JOIN OperatorPlan B ON A.opeID = B.opOperatorID and opYear={0} and opTypeID=2 ", lastYear.Year);
                    break;
            }
            sql += " where (1=1) " + GetWhereForRole("opeID", CurrentOperatorID, departmentid, RoleValue, dataRange);
            return DbHelperSQL.GetSHSL(sql);
        }

        private static string GetWhereForRole(string userFieldName, string operatorID, string departmentid, string roleValue, string dataRangeString)
        {

            string result = "";
            int dataRange = 0;
            int.TryParse(dataRangeString, out dataRange);
            if (roleValue == "自己" && dataRange >= 1)
            {
                result = " AND (" + userFieldName + " in (select opeID from Operators where opeID = " + operatorID + "))";
            }
            else if (roleValue == "本部门" && dataRange >= 2)
            {
                result = " AND (" + userFieldName + " in (select opeID from Operators where opeDepartmentID = " + departmentid + "))";
            }
            else if (roleValue == "全公司" && dataRange >= 3)
            {
                result = "";
            }
            else
            {
                result = " AND (1=2) ";
            }
            return result;
        }

        private static string GetWhereForDate(string dateFieldName, string dateValue)
        {
            string result = "";

            switch (dateValue)
            {
                case "本月":
                    result = string.Format(" AND (year({0})={1} and month({0})={2})", dateFieldName, DateTime.Now.Year, DateTime.Now.Month);
                    break;
                case "上月":
                    result = string.Format(" AND (year({0})={1} and month({0})={2})", dateFieldName, DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);
                    break;
                case "今天":
                    result = string.Format(" AND (year({0})={1} and month({0})={2} and day({0})={3})", dateFieldName, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    break;
                case "昨天":
                    result = string.Format(" AND (year({0})={1} and month({0})={2} and day({0})={3})", dateFieldName, DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day);
                    break;
                case "今年":
                    result = string.Format(" AND (year({0})={1})", dateFieldName, DateTime.Now.Year);
                    break;
                case "去年":
                    result = string.Format(" AND (year({0})={1})", dateFieldName, DateTime.Now.AddYears(-1).Year);
                    break;
            }

            return result;
        }

        public static string GetWorkCalendarMonthContent(string currentOperatorID, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime nextMonth = startDate.AddMonths(1);
            switch (startDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    startDate = startDate.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    startDate = startDate.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    startDate = startDate.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    startDate = startDate.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    startDate = startDate.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    startDate = startDate.AddDays(-6);
                    break;

            }
            string contentHtml = "<table cellpadding=\"0\" cellspacing=\"0\" id=\"CalendarTable\">";
            contentHtml += "<tr class=\"CalendarFirstRow\"><td>周一</td><td>周二</td><td>周三</td><td>周四</td><td>周五</td><td>周六</td><td>周日</td></tr>";
            int lineCount = 6;
            string hasContent = "";

            for (int i = 0; i < lineCount; i++)
            {
                if (startDate < nextMonth)
                {
                    contentHtml += "<tr class=\"CalendarCenter\">";
                    for (int j = 0; j < 7; j++)
                    {
                        bool isExists = DbHelperSQL.Exists(string.Format("select * from CustomerFollowPlan where cfpOperatorID = {0} and cfpDate >= '{1} 0:0:0' and cfpDate <= '{1} 23:59:59'", currentOperatorID, startDate.ToString("yyyy-MM-dd")));
                        if (isExists)
                        {
                            hasContent = "lblHasContent";
                        }
                        else
                        {
                            hasContent = "";
                        }
                        contentHtml += string.Format("<td onclick='javascript:clickDay({2},{3},{0},this)'><div id='div{4}'>{0}</div><div class=\"{1}\"></div></td>", startDate.Day, hasContent, startDate.Year, startDate.Month, startDate.Year.ToString() + startDate.Month.ToString() + startDate.Day.ToString());
                        startDate = startDate.AddDays(1);
                    }
                    contentHtml += "</tr>";
                }
            }
            contentHtml += "</table>";
            return contentHtml;
        }

        public static string[] GetWorkCalendarDateContent(string currentOperatorID, int year, int month, int day)
        {
            DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from CustomerFollowPlan where cfpOperatorID = {0} and cfpDate >= '{1} 0:0:0' and cfpDate <= '{1} 23:59:59'", currentOperatorID, (new DateTime(year, month, day)).ToString("yyyy-MM-dd")));
            string contentHtml = "";
            foreach (DataRow r in dt.Rows)
            {
                contentHtml += string.Format("<div class=\"cssPlanItem\" onclick=\"gotoPlanDetail('{0}')\">{1}  {2}</div>", r["cfpID"], DateTime.Parse(r["cfpDate"] + "").ToString("HH:mm"), r["cfpContent"]);
            }
            string[] result = new string[2];
            result[0] = contentHtml;
            result[1] = dt.Rows.Count.ToString();
            return result;
        }

        public static string GetOperatorMessageCount(string opeID)
        {
            string count = DbHelperSQL.GetSHSL(string.Format("select count(*) from sysMessage where  AwokeTime >= '{0} 0:0:0' and AwokeTime <= '{0} 23:59:59' and messageid in (Select messageid from sysMessage_Looker Where objectid={1}) AND messageid not in (Select messageid from sysMessage_Deleted Where operatorid={1}) AND messageid not in (select messageid from sysMessage_Readed where operatorid = {1})", DateTime.Now.ToString("yyyy-MM-dd"), opeID));
            return count;
        }

        public static string SendCode(string mobile)
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
            /*
            int corpID;
            int.TryParse(ConfigurationManager.AppSettings["SmsCorpID"], out corpID);
            string serverName = ConfigurationManager.AppSettings["SmsServer"];
            string loginName = ConfigurationManager.AppSettings["SmsLoginName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];

            IntPtr hWnd = include.FindWindow(null, "测试");
            Random ran = new Random();
            int RandKey = ran.Next(100000, 999999);
            string strContent = "验证码:" + details[0] + ":" + RandKey;
            int connectResult = include.Sms_Connect(serverName, corpID, loginName, password, 30, hWnd);
            int lSmsID;
            int sendStatus = 0;
            if (connectResult == 0)
            {
                sendStatus = include.Sms_Send(details[0], "您本次操作的验证码是:" + RandKey + "。欢迎使用创捷易CRM！", out lSmsID);
                if (sendStatus > 0)
                {
                    string myphone = ConfigurationManager.AppSettings["MyMobile"];
                    sendStatus = include.Sms_Send(myphone, "注册商机--公司：" + details[1] + ",联系人：" + details[2] + ",号码:" + details[0] + "。请尽快联系！", out lSmsID);
                }
                include.Sms_DisConnect();
            }

            if (sendStatus > 0)
            {
                DbHelperSQL.ExecuteSQL(string.Format("insert into SmsLog(Message,AddTime) values('{0}','{1}')", strContent, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                return "发送成功 ! 请查收。";
            }
            */
            return "发送失败 ! 请重试。";
        }
        #endregion

        #region Customer
        //保存客户基本信息
        public static string SaveCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string CurrentOperatorID, string cusExtType2, string cusAreaID, string cusCN)
        {
            try
            {
                //相同客户已经存在
                bool isExist = DbHelperSQL.Exists("select * from Customer where cusID != " + cusID + " and cusName = '" + cusName + "'");
                if (isExist)
                {
                    return "0";
                }
                if (CheckPermissionForCustomer(cusID.ToString(), CurrentOperatorID))
                {
                    float lng = 0.0f, lat = 0.0f;
                    float.TryParse(cusLongtitude, out lng);
                    float.TryParse(cusLatitude, out lat);
                    if (cusExtType2 == "" || cusExtType2 == "null")
                    {
                        cusExtType2 = "0";
                    }
                    if (cusAreaID == "" || cusAreaID == "null")
                    {
                        cusAreaID = "0";
                    }
                    if (cusID > 0)
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update Customer set cusName = '{0}',cusKindID={1},cusSourceID={2},cusIntroduction='{3}',cusAddress='{4}',cusDepartmentID={5},cusOperatorID={6},cusModifyDate=getdate(), cusLongtitude = {8}, cusLatitude = {9},cusContactor='{10}', cusTel='{11}',cusExtType2='{12}', cusAreaID='{13}',cusCN='{14}'  where cusID={7}", cusName, cusKindID, cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, cusID, lng, lat, cusContactor, cusTel, cusExtType2, cusAreaID,cusCN));
                        SaveCustomerFollowHistory("Customer", cusID, 0, "修改了客户资料", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                        AddOperatorLog("修改客户资料 ID:" + cusID, cusID, CurrentOperatorID);
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            return cusID.ToString();
        }

        public static string AddCustomerInfo(int cusID, string cusName, int cusKindID, int cusSourceID, string cusIntroduction, string cusAddress, int cusDepartmentID, int cusOperatorID, string cusLongtitude, string cusLatitude, string cusContactor, string cusTel, string clmSex, string clmMobile, string coName, string coPhaseID, string coPredictAmount, DateTime coPredictFinishDate, string CurrentOperatorID, string cusExtType2, string cusAreaID, string OpptunityID, string ClueID, string cusCN)
        {
            try
            {
                float lng = 0.0f, lat = 0.0f;
                if (cusLongtitude != "")
                {
                    float.TryParse(cusLongtitude, out lng);
                }
                if (cusLatitude != "")
                {
                    float.TryParse(cusLatitude, out lat);
                }
                if (cusExtType2 == "" || cusExtType2 == "null")
                {
                    cusExtType2 = "0";
                }
                if (cusAreaID == "" || cusAreaID == "null")
                {
                    cusAreaID = "0";
                }

                string sql = "select * from Customer where cusName = '" + cusName + "'";
                //相同客户已经存在
                bool isExist = DbHelperSQL.Exists(sql);
                if (isExist)
                {
                    return "0";
                }

                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'Customer'");
                string clmid = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerLinkMan'");
                string coid = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerOpptunity'");
                int rID = DbHelperSQL.ExecuteSQL(string.Format("Insert into Customer(cusID,cusName,cusKindID,cusSourceID,cusIntroduction,cusAddress,cusDepartmentID,cusOperatorID,cusAddDate, cusLongtitude, cusLatitude,cusContactor,cusTel,cusExtType2,cusAreaID,cusAddOperatorID,cusCN)values({0},'{1}',{2},{3},'{4}','{5}',{6},{7},getdate(),{8},{9},'{10}','{11}','{12}','{13}',{7},'{14}')", id, cusName, cusKindID, cusSourceID, cusIntroduction, cusAddress, cusDepartmentID, cusOperatorID, lng, lat, cusContactor, cusTel, cusExtType2, cusAreaID,cusCN));
                AddOperatorLog("新增客户 ID：" + id, int.Parse(id), CurrentOperatorID);
                SaveCustomerFollowHistory("Customer", int.Parse(id), 0, "录入了客户资料", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                if (rID > 0 && !string.IsNullOrEmpty(cusContactor) && !string.IsNullOrEmpty(clmMobile) || !string.IsNullOrEmpty(cusTel))
                {
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerLinkMan(clmID,clmCustomerID,clmName,clmSex,clmTel,clmMobile,clmAddOperatorID,clmAddDate) values({0},{1},'{2}','{3}','{4}','{5}',{6},getdate())", clmid, id, cusContactor, clmSex, cusTel, clmMobile, cusOperatorID));
                    AddOperatorLog("新增联系人 ID：" + clmid, int.Parse(id), CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", int.Parse(id), 0, "新增联系人" + cusContactor, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                }
                if (rID > 0 && !string.IsNullOrEmpty(coName))
                {
                    if (coPhaseID == "")
                        coPhaseID = "0";
                    if (coPredictAmount == "")
                        coPredictAmount = "0";
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerOpptunity(coID,coCustomerID,coName,coPhaseID,coPredictAmount,coPredictFinishDate,coStatusID,coAddDate,coAddOperatorID,coDate,coOperatorID) values({0},{1},'{2}',{3},'{4}','{5}',1,getdate(),{6},getdate(),{7})", coid, id, coName, coPhaseID, coPredictAmount, coPredictFinishDate, cusOperatorID, cusOperatorID));
                    AddOperatorLog("新增商机 ID：" + coid, int.Parse(id), CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", int.Parse(id), 0, "新增商机" + coName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                }

                if (OpptunityID != "")
                {
                    DbHelperSQL.ExecuteSQL(string.Format("Update CustomerOpptunity set coCustomerID='" + id + "' where coID=" + OpptunityID));
                }

                if (ClueID != "0")
                {
                    DbHelperSQL.ExecuteSQL("Update CustomerClue set ccStatusID='58',ccCustomerID= " + id + " where ccID=" + ClueID);
                    AddOperatorLog("线索转化为客户，线索ID：" + ClueID, int.Parse(id), CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", int.Parse(id), 0, "线索转化为客户", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                }

                return id;

            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static int DeleteCustomer(int id, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForCustomer(id.ToString(), CurrentOperatorID.ToString()))
                {
                    string cusName = DbHelperSQL.GetSHSL("select cusName from Customer where cusID = " + id);
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhRelatedID in (" + id + ") and cfhSource='Customer'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpRelatedID in (" + id + ") and cfpSource='Customer'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CoWorker where cwRelatedID in (" + id + ") and cwSource='Customer'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerLinkMan where clmCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerBusiness where cbCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerReceipt where crCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFile where cfCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFeedback where cfbCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerOpptunity where coCustomerID in (" + id + ")");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerOperatorHistory where coCustomerID in (" + id + ")");

                    DbHelperSQL.ExecuteSQL("Delete from Customer where cusID = " + id);
                    AddOperatorLog("删除客户：" + cusName, id, CurrentOperatorID.ToString());
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int CheckInCustomerPool(int ID)
        {
            try
            {
                if (DbHelperSQL.Exists("select * from [XCustomerPool] where cusID = " + ID))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int PutInOrGetFromCustomerPool(int ID, int CurrentOperatorID)
        {
            try
            {
                //已经在公海里了，则从公海里领出来
                if (DbHelperSQL.Exists("select * from [XCustomerPool] where cusID = " + ID))
                {
                    string cusDepartmentID = DbHelperSQL.GetSHSL("select ISNULL(opeDepartmentID,0) from Operators where opeID = " + CurrentOperatorID);
                    DbHelperSQL.ExecuteSQL("Update Customer set cusOperatorID=" + CurrentOperatorID + ", cusDepartmentID=" + cusDepartmentID + " where cusID = " + ID);
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerOperatorHistory(coCustomerID,coOperatorID,coAddOperatorID,coRemark,coAddDate) values({0},{1},{1},'{2}',getdate())", ID, CurrentOperatorID, "从公海中认领"));
                    DbHelperSQL.ExecuteSQL("delete from coworker where cwSource='Customer' and cwRelatedID = " + ID);
                    AddOperatorLog("从公海中认领", ID, CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", ID, 0, "从公海里领取了客户", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                }
                else
                {
                    //负责人才能转
                    if (CheckPermissionForCustomer(ID.ToString(), CurrentOperatorID.ToString()))
                    {
                        DbHelperSQL.ExecuteSQL("Update Customer set cusOperatorID=NULL, cusDepartmentID=NULL where cusID = " + ID);
                        AddOperatorLog("转入公海", ID, CurrentOperatorID.ToString());
                        SaveCustomerFollowHistory("Customer", ID, 0, "转入公海", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                return 1;

                
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ChangeCustomerOperator(int cusID, int cusOperatorID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForCustomer(cusID.ToString(), CurrentOperatorID.ToString()))
                {
                    string cusDepartmentID = DbHelperSQL.GetSHSL("select ISNULL(opeDepartmentID,0) from Operators where opeID = " + cusOperatorID);
                    DbHelperSQL.ExecuteSQL("Update Customer set cusOperatorID=" + cusOperatorID + ", cusDepartmentID=" + cusDepartmentID + " where cusID = " + cusID);
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerOperatorHistory(coCustomerID,coOperatorID,coAddOperatorID,coRemark,coAddDate) values({0},{1},{2},'{3}',getdate())", cusID, cusOperatorID, CurrentOperatorID, "从手机端转给他人"));

                    DbHelperSQL.ExecuteSQL("delete from coworker where cwSource='Customer' and cwRelatedID = " + cusID);
                    string cusName = DbHelperSQL.GetSHSL("select cusName from Customer where cusID=" + cusID);
                    string newOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + cusOperatorID);
                    AddOperatorLog("转移客户：" + cusName, cusID, CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", cusID, 0, "转移客户给" + newOperatorName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    string url = "Data/CustomerEditForm.aspx?ID=" + cusID;
                    AddMessages(CurrentOperatorID, cusOperatorID, "客户转移提醒", GetOpeName(CurrentOperatorID) + "把客户[" + cusName + "]转到了你名下，快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    return cusID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static IList<CustomerCheck> SearchCustomer(string KeyWord)
        {
            try
            {
                if (KeyWord != "")
                {
                    string sql = string.Format(@"select top 10 * from ( select cusName as Name, opeName as OperatorName, depName as DepartmentName, cusAddDate as AddDate, '来自：客户' as SourceName
 from Customer A INNER JOIN Operators B On A.cusOperatorID = B.opeID
 INNER JOIN Department D ON B.opeDepartmentID = D.depID
 where (cusName like '%{0}%' or cusTel like '%{0}%' or cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmMobile like '%{0}%' OR clmTel like '%{0}%'))
 union all
 select ccCustomerName + '-' +  ccName as Name, opeName as OperatorName, depName as DepartmentName, ccAddDate as AddDate, '来自：线索' as SourceName
 from CustomerClue A INNER JOIN Operators B On A.ccOperatorID = B.opeID
 INNER JOIN Department D ON B.opeDepartmentID = D.depID
 where (ccName like '%{0}%' OR ccCustomerName like '%{0}%' OR ccTel like '%{0}%' OR ccMobile like '%{0}%')
 union all
 select pjCompanyName + '-' +  pjName as Name, opeName as OperatorName, depName as DepartmentName, pjAddDate as AddDate, '来自：项目报备' as SourceName
 from Project A INNER JOIN Operators B On A.pjOperatorID = B.opeID
 INNER JOIN Department D ON B.opeDepartmentID = D.depID
 where (pjName like '%{0}%' OR pjCompanyName like '%{0}%' OR pjDetail like '%{0}%' OR pjProduct like '%{0}%' OR pjNO like '%{0}%')) A ", KeyWord);
                    DataTable dt = DbHelperSQL.GetDataTable(sql);
                    List<CustomerCheck> cclist = new List<CustomerCheck>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerCheck cc = new CustomerCheck();
                        cc.AddDate = dr["AddDate"] + "";
                        cc.DepartmentName = dr["DepartmentName"] + "";
                        cc.Name = dr["Name"] + "";
                        cc.OperatorName = dr["OperatorName"] + "";
                        cc.SourceName = dr["SourceName"] + "";
                        cclist.Add(cc);
                    }
                    return cclist;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        
        #region CustomerLinkMan
        public static CustomerLinkManResult GetLinkMan(int clmID)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from CustomerLinkMan where clmID = " + clmID);
                CustomerLinkManResult clm = new CustomerLinkManResult();
                clm.clmID = clmID;
                clm.clmName = dr["clmName"] + "";
                clm.clmSex = dr["clmSex"] + "";
                clm.clmTel = dr["clmTel"] + "";
                clm.clmMobile = dr["clmMobile"] + "";
                clm.clmEmail = dr["clmEmail"] + "";
                clm.clmQQ = dr["clmQQ"] + "";
                clm.clmWeChat = dr["clmWeChat"] + "";
                clm.clmRemark = dr["clmRemark"] + "";
                return clm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string SaveLinkMan(int cusID, int clmID, string clmName, string clmSex, string clmTel, string clmMobile, string clmEmail, string clmQQ, string clmWeChat, string clmRemark, int clmAddOperatorID,string clmTypeID)
        {
            try
            {
                if (CheckPermissionForCustomer(cusID.ToString(), clmAddOperatorID.ToString()))
                {
                    if (clmID > 0)
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update CustomerLinkMan set clmName = '{0}',clmSex='{1}',clmTel='{2}',clmMobile='{3}',clmEmail='{4}',clmQQ='{5}',clmWechat='{6}',clmRemark='{7}',clmModifyDate=getdate(),clmModifyOperatorID={8},clmTypeID='{10}' where clmID={9}", clmName, clmSex, clmTel, clmMobile, clmEmail, clmQQ, clmWeChat, clmRemark, clmAddOperatorID, clmID,clmTypeID));
                        AddOperatorLog("修改联系人信息,ID:" + clmID, cusID, clmAddOperatorID.ToString());
                        SaveCustomerFollowHistory("Customer", cusID, 0, "修改联系人信息[" + clmName + "]", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", clmAddOperatorID.ToString(), "", "", clmAddOperatorID.ToString());
                    }
                    else
                    {
                        string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerLinkMan'");
                        DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerLinkMan(clmID,clmName,clmSex,clmTel,clmMobile,clmEmail,clmQQ,clmWeChat,clmRemark,clmAddOperatorID,clmCustomerID,clmAddDate,clmModifyDate,clmTypeID)values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},getdate(),getdate(),'{11}')", id, clmName, clmSex, clmTel, clmMobile, clmEmail, clmQQ, clmWeChat, clmRemark, clmAddOperatorID, cusID,clmTypeID));
                        AddOperatorLog("录入联系人信息,ID:" + id, cusID, clmAddOperatorID.ToString());
                        SaveCustomerFollowHistory("Customer", cusID, 0, "录入联系人信息[" + clmName + "]", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", clmAddOperatorID.ToString(), "", "", clmAddOperatorID.ToString());
                        return id;
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            return clmID.ToString();
        }

        public static string DeleteLinkMan(int ID, int CurrentOperatorID)
        {
            try
            {
                DataRow row = DbHelperSQL.GetDataRow("select clmName, clmCustomerID from CustomerLinkMan where clmID = " + ID);
                if (CheckPermissionForCustomer(row["clmCustomerID"] + "", CurrentOperatorID.ToString()))
                {
                    DbHelperSQL.ExecuteSQL("Delete from CustomerLinkMan where clmID = " + ID);
                    AddOperatorLog("删除联系人:" + row["clmName"], int.Parse(row["clmCustomerID"] + ""), CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", int.Parse(row["clmCustomerID"] + ""), 0, "删除联系人[" + row["clmName"] + "]", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    return ID.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        #endregion

        #region CustomerOpptunity
        public static CustomerOpptunityResult GetOpptunity(int coID)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from CustomerOpptunity where coID = " + coID);
                CustomerOpptunityResult co = new CustomerOpptunityResult();
                co.coID = coID;
                co.coName = dr["coName"] + "";
                co.coOperatorID = dr["coOperatorID"] + "";
                co.coOperatorName = GetOperatorName(dr["coOperatorID"] + "");
                co.coOpptunitySourceID = dr["coOpptunitySourceID"] + "";
                co.coOpptunitySourceName = GetSourceName(dr["coOpptunitySourceID"] + "");
                co.coPhaseID = dr["coPhaseID"] + "";
                co.coPhaseName = GetPhaseName(dr["coPhaseID"] + "");
                co.coPredictAmount = string.Format("{0:N0}", dr["coPredictAmount"]);
                if (dr["coPredictFinishDate"] + "" != "")
                {
                    co.coPredictFinishDate = DateTime.Parse(dr["coPredictFinishDate"] + "").ToString("yyyy-MM-dd");
                }
                if (dr["coDate"] + "" != "")
                {
                    co.coDate = DateTime.Parse(dr["coDate"] + "").ToString("yyyy-MM-dd");
                }
                co.coStatusID = dr["coStatusID"] + "";
                co.coStatusName = GetStatusName(dr["coStatusID"] + "");

                return co;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string SaveOpptunity(int cusID, int coID, string coName, string coPhaseID, string coPredictAmount, string coPredictFinishDate, string coStatusID, string coOpptunitySourceID, string coOperatorID, string coDate, string CurrentOperatorID)
        {
            try
            {
                if (coID > 0)
                {
                    if (coPredictAmount != "")
                    {
                        if (CheckPermissionForOpptunity(coID.ToString(), CurrentOperatorID))
                        {
                            coPredictAmount = coPredictAmount.Replace(",", "");
                            DbHelperSQL.ExecuteSQL(string.Format("Update CustomerOpptunity set coName = '{0}',coPhaseID='{1}',coPredictAmount='{2}',coPredictFinishDate='{3}',coStatusID='{4}',coOpptunitySourceID='{5}',coOperatorID='{6}',coModifyDate=getdate(),coDate='{8}' where coID={7}", coName, coPhaseID, coPredictAmount, coPredictFinishDate, coStatusID, coOpptunitySourceID, coOperatorID, coID, coDate));
                            AddOperatorLog("修改商机 ID:" + coID, cusID, CurrentOperatorID);
                            SaveCustomerFollowHistory("Customer", cusID, 0, "修改了商机 " + coName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
                else
                {
                    coPredictAmount = "0";
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerOpptunity'");
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerOpptunity(coID,coName,coPhaseID,coPredictAmount,coPredictFinishDate,coStatusID,coOpptunitySourceID,coOperatorID,coCustomerID,coAddDate,coModifyDate,coDate)values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),getdate(),'{9}')", id, coName, coPhaseID, coPredictAmount, coPredictFinishDate, coStatusID, coOpptunitySourceID, coOperatorID, cusID, coDate));

                    AddOperatorLog("新增商机 ID:" + id, cusID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", cusID, 0, "录入了商机商机 " + coName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    return id;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            return coID.ToString();
        }

        public static string DeleteOpptunity(int ID, int CurrentOperatorID)
        {
            try
            {
                DataRow row = DbHelperSQL.GetDataRow("select coName, coCustomerID from CustomerOpptunity where coID = " + ID);
                if (CheckPermissionForOpptunity(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhRelatedID in (" + ID + ") and cfhSource='Opptunity'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpRelatedID in (" + ID + ") and cfpSource='Opptunity'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CoWorker where cwRelatedID in (" + ID + ") and cwSource='Opptunity'");

                    AddOperatorLog("删除商机:" + row["coName"], int.Parse(row["coCustomerID"] + ""), CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerOpptunity where coID = " + ID);

                    SaveCustomerFollowHistory("Customer", int.Parse(row["coCustomerID"] + ""), 0, "删除商机 " + row["coName"], "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string ChangeOpptunityStatus(int ID, string coStatusID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForOpptunity(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DataRow row = DbHelperSQL.GetDataRow("select coName, coCustomerID from CustomerOpptunity where coID = " + ID);
                    DbHelperSQL.ExecuteSQL("update CustomerOpptunity set coStatusID = " + coStatusID + " where coID = " + ID);
                    string statusName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName = 'OpptunityStatus' and sID = " + coStatusID);
                    AddOperatorLog("调整商机状态:" + statusName + " " + row["coName"], int.Parse(row["coCustomerID"] + ""), CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Opptunity", ID, 0, "调整商机状态:" + statusName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        #endregion

        #region CustomerFollowHistory
        public static string DeleteFollowHistory(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForFollowHistory(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DataRow row = DbHelperSQL.GetDataRow("select cfhContent from CustomerFollowHistory where cfhID = " + ID);
                    AddOperatorLog("删除跟进:" + row["cfhContent"], 0, CurrentOperatorID.ToString());
                    string url = "Data/CustomerFollowHistoryEditForm.aspx?ID=" + ID;
                    DeleteMessageByURL(url);
                    DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhID = " + ID);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string SaveCustomerFollowHistory(string cfhSource, int cfhRelatedID, int cfhID, string cfhContent, string cfhTypeID, string cfhStatusID, string cfhDate, string cfhAddress, string cfhAddOperatorID, string cfhNextFollowTime, string cfhFilePath, string CurrentOperatorID)
        {
            try
            {
                string result = "0";
                if (cfhTypeID == "")
                    cfhTypeID = "0";
                if (cfhID > 0)
                {
                    if (CheckPermissionForFollowHistory(cfhID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(@"update CustomerFollowHistory set
cfhContent='" + cfhContent + @"',
cfhFilePath='" + cfhFilePath + @"',
cfhTypeID='" + cfhTypeID + @"',
cfhDate='" + cfhDate + @"',
cfhNextFollowTime='" + cfhNextFollowTime + @"',
cfhAddress='" + cfhAddress + @"',
cfhModifyOperatorID='" + CurrentOperatorID + @"',
cfhModofyDate='getDate()' where cfhID='" + cfhID + "'");
                        AddOperatorLog("修改跟进记录 ID：" + cfhID, 0, CurrentOperatorID);
                        result = cfhID.ToString();
                    }
                    else
                    {
                        result = "0";
                    }

                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                    DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, cfhContent, cfhFilePath, cfhTypeID, cfhDate, cfhNextFollowTime, cfhAddress, CurrentOperatorID, CurrentOperatorID, cfhSource));
                    AddOperatorLog("新增跟进记录 ID：" + id, 0, CurrentOperatorID);
                    result = id;
                    if (cfhSource == "Customer")
                    {
                        SendMessageToCoWorkerForCustomer(cfhRelatedID.ToString(), CurrentOperatorID, "客户新动态", "客户有了新动态，快去看看吧", "Data/CustomerEditForm.aspx?ID=" + cfhRelatedID);
                    }
                    else if (cfhSource == "Opptunity")
                    {
                        SendMessageToCoWorkerForOpptunity(cfhRelatedID.ToString(), CurrentOperatorID, "商机新动态", "商机有了新动态，快去看看吧", "Data/CustomerOpptunityEditForm.aspx?ID=" + cfhRelatedID);
                    }
                    else if (cfhSource == "Business" || cfhSource == "AfterSales")
                    {
                        SendMessageToCoWorkerForBusiness(cfhRelatedID.ToString(), CurrentOperatorID, "合同新动态", "合同有了新动态，快去看看吧", "Data/CustomerBusinessEditForm.aspx?ID=" + cfhRelatedID);
                    }
                    else if (cfhSource == "Project")
                    {
                        SendMessageToCoWorkerForProject(cfhRelatedID.ToString(), CurrentOperatorID, "报备新动态", "项目报备有了新动态，快去看看吧", "Data/ProjectEditForm.aspx?ID=" + cfhRelatedID);
                    }
                    else if (cfhSource == "Feedback")
                    {
                        SendMessageToCoWorkerForFeedback(cfhRelatedID.ToString(), CurrentOperatorID, "反馈新动态", "客户反馈有了新动态，快去看看吧", "Data/CustomerFeedbackEditForm.aspx?ID=" + cfhRelatedID);
                    }
                    else if (cfhSource == "MarketingActivity") 
                    {
                        SendMessageToCoWorkerForMarketing(cfhRelatedID.ToString(), CurrentOperatorID, "市场管理新动态", "市场管理有了新动态，快去看看吧", "Data/MarketingActivityEditForm.aspx?ID=" + cfhRelatedID);
                    }
                }

                if (cfhStatusID != "")
                {
                    switch (cfhSource)
                    {
                        case "Clue":
                            if (!DbHelperSQL.Exists("select * from CustomerClue where ccStatusID='" + cfhStatusID + "' and ccID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update CustomerClue Set ccStatusID='" + cfhStatusID + "' where ccID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='CustomerClueStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            break;
                        case "Customer":
                            if (!DbHelperSQL.Exists("select * from Customer where cusStatusID='" + cfhStatusID + "' and cusID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update Customer Set cusStatusID='" + cfhStatusID + "' where cusID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='defCustomerStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            break;
                        case "Opptunity":
                            if (!DbHelperSQL.Exists("select * from CustomerOpptunity where coPhaseID='" + cfhStatusID + "' and coID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update CustomerOpptunity Set coPhaseID='" + cfhStatusID + "' where coID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select copName from defCustomerOpptunityPhase where copID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "阶段变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            
                            break;
                        case "Business":
                            if (!DbHelperSQL.Exists("select * from CustomerBusiness where cbStatus='" + cfhStatusID + "' and cbID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update CustomerBusiness set cbStatus ='" + cfhStatusID + "' where cbID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='defBusinessStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "业务状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            
                            break;
                        case "AfterSales":
                            if (!DbHelperSQL.Exists("select * from CustomerBusiness where cbAfterID='" + cfhStatusID + "' and cbID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update CustomerBusiness set cbAfterID ='" + cfhStatusID + "' where cbID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='defAfterStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "售后状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            
                            break;

                        case "Project":
                            if (!DbHelperSQL.Exists("select * from Project where pjStatusID='" + cfhStatusID + "' and pjID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update Project set pjStatusID ='" + cfhStatusID + "' where pjID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='ProjectStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }

                            break;
                        case "Feedback":
                            if (!DbHelperSQL.Exists("select * from CustomerFeedback where cfbStatusID='" + cfhStatusID + "' and cfbID=" + cfhRelatedID))
                            {
                                DbHelperSQL.ExecuteSQL("Update CustomerFeedback set cfbStatusID ='" + cfhStatusID + "' where cfbID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='defCustomerFeedbackStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            break;

                        case "MarketingActivity":
                            if (!DbHelperSQL.Exists("select * from MarketingActivity where maStatusID='" + cfhStatusID + "' and maID=" + cfhRelatedID)) 
                            {
                                DbHelperSQL.ExecuteSQL("Update MarketingActivity set maStatusID='" + cfhStatusID + "' where maID=" + cfhRelatedID);
                                string statusNewName = DbHelperSQL.GetSHSL("select sName from sdefCommon where sTypeName='defMarketingActivityStatus' and sID=" + cfhStatusID);
                                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowHistory'");
                                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerFollowHistory 
(cfhID,cfhRelatedID,cfhContent,cfhFilePath,cfhTypeID,cfhDate,cfhNextFollowTime,cfhAddress,cfhAddOperatorID,cfhAddDate,cfhOperatorID,cfhSource) values 
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',getdate(),'{9}','{10}')", id, cfhRelatedID, "状态变为：" + statusNewName, "", cfhTypeID, cfhDate, "", "", CurrentOperatorID, CurrentOperatorID, cfhSource));
                            }
                            break;
                    }
                }

                if (cfhNextFollowTime != "")
                {
                    string content = "";
                    if (cfhSource == "Customer")
                    {
                        content += "客户[" + DbHelperSQL.GetSHSL("select cusName from Customer where cusID = " + cfhRelatedID) + "]";
                    }
                    else if (cfhSource == "Clue")
                    {
                        content += "线索[" + DbHelperSQL.GetSHSL("select ccName from CustomerClue where ccID = " + cfhRelatedID) + "]";
                    }
                    else if (cfhSource == "Opptunity")
                    {
                        content += "商机[" + DbHelperSQL.GetSHSL("select coName from CustomerOpptunity where coID = " + cfhRelatedID) + "]";
                    }
                    else if (cfhSource == "Business")
                    {
                        content += "合同[" + DbHelperSQL.GetSHSL("select cbName from CustomerBusiness where cbID = " + cfhRelatedID) + "]";
                    }
                    else if (cfhSource == "Project")
                    {
                        content += "项目[" + DbHelperSQL.GetSHSL("select pjName from Project where pjID = " + cfhRelatedID) + "]";
                    }
                    else if (cfhSource == "Feedback")
                    {
                        content += "反馈[" + DbHelperSQL.GetSHSL("select cfbContent from CustomerFeedback where cfbID = " + cfhRelatedID) + "]";
                    }
                    content += "到了跟进时间了，快去看看吧";
                    string url = "Data/CustomerFollowHistoryEditForm.aspx?ID=" + result;
                    AddMessages(int.Parse(cfhAddOperatorID), int.Parse(cfhAddOperatorID), "跟进提醒", content, url, cfhNextFollowTime);
                }

                return result;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string DeleteBillComment(string id)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Delete from BillComment where bcID=" + id);
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string DeleteBillCommentForRelatedID(string id)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Delete from BillComment where bcRelatedID=" + id);
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string SavaBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string cfhOperatorID, string bcSourceID)
        {
            try
            {
                //bcSourceID（1为跟进，2为日志）
                //bcTypeID(1为评论，2为点赞)    
                string bcID = DbHelperSQL.GetSHSLInt("usp_GetID 'BillComment'");
                DbHelperSQL.ExecuteSQL(@"Insert into BillComment (bcID,bcSourceID,bcContent,bcTypeID,bcOperatorID,bcDate,bcRelatedID) 
values ('" + bcID + "','" + bcSourceID + "','" + bcContent + "','1','" + bcOperatorID + "',getdate(),'" + bcRelatedID + "')");
                string sourceName = "";
                string url = "";
                if (bcSourceID == "1")
                {
                    sourceName = "动态";
                    url = "data/CustomerFollowHistoryEditForm.aspx?ID=" + bcRelatedID;
                }
                else
                {
                    sourceName = "日志";
                    url = "data/CustomerWorkReportEditForm.aspx?ID=" + bcRelatedID;
                }

                string opeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + bcOperatorID);
                AddMessages(int.Parse(bcOperatorID), int.Parse(cfhOperatorID), sourceName + "评论提醒", opeName + "评论了你的" + sourceName + "，快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                return bcID;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string ReplyBillComment(string bcOperatorID, string bcContent, string bcRelatedID, string replyOperatorID, string bcSourceID)
        {
            try
            {
                //bcSourceID（1为跟进，2为日志）
                //bcTypeID(1为评论，2为点赞)    
                string bcID = DbHelperSQL.GetSHSLInt("usp_GetID 'BillComment'");
                DbHelperSQL.ExecuteSQL(@"Insert into BillComment (bcID,bcSourceID,bcContent,bcTypeID,bcOperatorID,bcDate,bcRelatedID) 
values ('" + bcID + "','" + bcSourceID + "','" + bcContent + "','1','" + bcOperatorID + "',getdate(),'" + bcRelatedID + "')");


                string sourceName = "";
                string url = "";
                if (bcSourceID == "1")
                {
                    sourceName = "动态";
                    url = "data/CustomerFollowHistoryEditForm.aspx?ID=" + bcRelatedID;
                }
                else
                {
                    sourceName = "日志";
                    url = "data/CustomerWorkReportEditForm.aspx?ID=" + bcRelatedID;
                }

                string opeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + bcOperatorID);
                AddMessages(int.Parse(bcOperatorID), int.Parse(replyOperatorID), sourceName + "评论回复提醒", opeName + "回复了你的" + sourceName + "评论，快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                return bcID;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string GetClickLikeInfoForHistory(string currentOperatorID, string cfhID, string cfhOperatorID)
        {
            try
            {
                string result = "";

                string bcID = DbHelperSQL.GetSHSLInt("usp_GetID 'BillComment'");
                DbHelperSQL.ExecuteSQL(string.Format(@"if exists(select * from BillComment where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = {0} and bcOperatorID = {1})
	delete from BillComment where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = {0} and bcOperatorID = {1}
else
	insert into BillComment(bcID,bcSourceID,bcTypeID,bcOperatorID,bcDate,bcRelatedID) values({3},1,2,{1},'{2}',{0})", cfhID, currentOperatorID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bcID));

                DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from BillComment A INNER JOIN Operators B ON A.bcOperatorID = B.opeID where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = {0}", cfhID));

                bool send = false;
                foreach (DataRow row in dt.Rows)
                {
                    result += row["opeName"] + "|";

                    if (currentOperatorID == row["opeID"] + "")
                    {
                        send = true;
                    }
                }
                if (result != string.Empty)
                {
                    result = result.Substring(0, result.Length - 1);
                }
                //如果是点赞而不是反点赞，并且当前人不是负责人，就发消息
                if (send && currentOperatorID != cfhOperatorID)
                {
                    string opeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + currentOperatorID);
                    string url = "data/CustomerFollowHistoryEditForm.aspx?ID=" + cfhID;
                    AddMessages(int.Parse(currentOperatorID), int.Parse(cfhOperatorID), "动态点赞提醒" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), opeName + "赞了你的动态，快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetClickLikeInfoForWorkReport(string currentOperatorID, string cfhID, string cfhOperatorID)
        {
            try
            {
                string result = "";

                string bcID = DbHelperSQL.GetSHSLInt("usp_GetID 'BillComment'");
                DbHelperSQL.ExecuteSQL(string.Format(@"if exists(select * from BillComment where bcSourceID = 2 and bcTypeID = 2 and bcRelatedID = {0} and bcOperatorID = {1})
	delete from BillComment where bcSourceID = 2 and bcTypeID = 2 and bcRelatedID = {0} and bcOperatorID = {1}
else
	insert into BillComment(bcID,bcSourceID,bcTypeID,bcOperatorID,bcDate,bcRelatedID) values({3},2,2,{1},'{2}',{0})", cfhID, currentOperatorID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bcID));

                DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from BillComment A INNER JOIN Operators B ON A.bcOperatorID = B.opeID where bcSourceID = 2 and bcTypeID = 2 and bcRelatedID = {0}", cfhID));

                bool send = false;
                foreach (DataRow row in dt.Rows)
                {
                    result += row["opeName"] + "|";

                    if (currentOperatorID == row["opeID"] + "")
                    {
                        send = true;
                    }
                }
                if (result != string.Empty)
                {
                    result = result.Substring(0, result.Length - 1);
                }
                //如果是点赞而不是反点赞，并且当前人不是负责人，就发消息
                if (send && currentOperatorID != cfhOperatorID)
                {
                    string opeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + currentOperatorID);
                    string url = "data/CustomerWorkReportEditForm.aspx?ID=" + cfhID;
                    AddMessages(int.Parse(currentOperatorID), int.Parse(cfhOperatorID), "日志点赞提醒" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), opeName + "赞了你的日志，快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CustomerFollowHistiryDetailData GetFollowHistoryDetailData(string cfhID, string CurrentOperatorID)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow(@"select A.*,B.opeName as cfhOperatorName,ISNULL(C.Name,'系统跟进') as cfhTypeName from customerFollowHistory A 
left outer join Operators B on A.cfhOperatorID =B.opeID 
left outer join defCommon C on A.cfhTypeID =C.ID where cfhID=" + cfhID);
                CustomerFollowHistiryDetailData cfhdd = new CustomerFollowHistiryDetailData();
                cfhdd.cfhAddDate = dr["cfhAddDate"] + "";
                cfhdd.cfhAddress = dr["cfhAddress"] + "";
                cfhdd.cfhContent = dr["cfhContent"] + "";
                cfhdd.cfhFilePath = GetEachFileForWeb(dr["cfhFilePath"] + "");
                cfhdd.cfhOperatorID = dr["cfhOperatorID"] + "";
                cfhdd.cfhOperatorName = dr["cfhOperatorName"] + "";
                cfhdd.cfhTypeName = dr["cfhTypeName"] + "";
                cfhdd.cfhLikeAndComment = GetLikeAndCommentCountHtml(cfhID, dr["cfhOperatorID"] + "", CurrentOperatorID);

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + cfhID);
                string html = "";
                foreach (DataRow item in dt.Rows)
                {
                    html += "<li id='liBillCommenet" + item["bcID"] + "' style=' border-bottom:1px solid #ccc; width:96%;'>";
                    html += "<table style='width:100%;'><tr><td><label>" + item["bcOperator"] + "" + "</label></td>";
                    html += "<td style='text-align:right;'><a data-role='none' style='cursor:pointer;' onclick='javascript:DeleteBillComment(" + item["bcID"] + "" + ")'>删除</a> | <a  data-role='none' style='cursor:pointer;'  onclick=\"javascript:ShowThisReplyDiv('DivForReply" + item["bcID"] + "','" + item["bcOperator"] + "'," + item["bcID"] + ")\">回复</a></td></tr>";
                    html += "<tr><td colspan='2'><label>" + item["bcDate"] + "" + "</label></td></tr>";
                    html += "<tr><td colspan='2'><label>" + item["bcContent"] + "" + "</label></td></tr>";
                    html += "<tr><td colspan='2'><div style='line-height: 40px;text-align: right; display:none;' id='DivForReply" + item["bcID"] + "'>";
                    html += "<input type='text' id='txtReply" + item["bcID"] + "' style='width: 100%;outline: none;height: 24px;' />";
                    html += "<a  data-role='none' class='MeunButton' onclick=\"javascript:ReplyContent('" + item["bcOperator"] + "'," + item["bcOperatorID"] + "," + item["bcID"] + ",'Reply')\">回复</a><a  data-role='none' class='CancelBtn' onclick=\"javascript:CloseThisDiv('DivForReply" + item["bcID"] + "')\">取消</a>";
                    html += "</div></td></tr></table></li>";
                }
                cfhdd.cfhBillComment = html;

                DataTable dt2 = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + cfhID);
                string likePersonsHtml = "";
                foreach (DataRow r in dt2.Rows)
                {
                    likePersonsHtml += "<div class='cssLikePerson'>" + r["bcOperator"] + "</div>";
                }
                if (likePersonsHtml != "")
                {
                    likePersonsHtml += "<div class='cssTxt'>觉得很赞哦</div>";
                }
                cfhdd.litLikePersons = likePersonsHtml;

                return cfhdd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static CustomerFollowPlanResult GetFollowPlanDetailData(string cfpID, string CurrentOperatorID)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from CustomerFollowPlan where cfpID=" + cfpID);
                CustomerFollowPlanResult cfpr = new CustomerFollowPlanResult();
                cfpr.cfpDate = dr["cfpDate"] + "";
                cfpr.cfpContent = dr["cfpContent"] + "";
                cfpr.cfpFilePath = GetEachFileForWeb(dr["cfpFilePath"] + "");
                return cfpr;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }


        #endregion

        #region CustomerFollowPlan
        public static CustomerFollowPlanResult GetFollowPlan(int id)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select A.*,C.Name AS cfpFollowTypeName,D.opeName AS cfpOperatorName from CustomerFollowPlan A LEFT OUTER JOIN Operators D ON A.cfpOperatorID = D.opeID where cfpID = " + id);
                CustomerFollowPlanResult entity = new CustomerFollowPlanResult();
                entity.cfpDate = DateTime.Parse(dr["cfpDate"] + "").ToString("yyyy-MM-dd HH:mm");
                entity.cfpID = id;
                entity.cfpOperatorID = dr["cfpOperatorID"] + "";
                entity.cfpOperatorName = dr["cfpOperatorName"] + "";
                entity.cfpContent = dr["cfpContent"] + "";

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string DeleteFollowPlan(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForFollowPlan(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DataRow row = DbHelperSQL.GetDataRow("select cfpSource, cfpRelatedID, cfpContent from CustomerFollowPlan where cfpID = " + ID);
                    AddOperatorLog("删除提醒:" + row["cfpContent"], 0, CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpID = " + ID);
                    string url = "Data/CustomerFollowPlanEditForm.aspx?ID=" + ID;
                    DeleteMessageByURL(url);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string SaveFollowPlan(int cfpID, string cfpRelatedID, string cfpSource, string cfpContent, string cfpFilePath, string cfpDate, string cfpOperatorID)
        {
            try
            {
                string result = "-1";
                if (cfpID > 0)
                {
                    if (CheckPermissionForFollowPlan(cfpID.ToString(), cfpOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL("Update CustomerFollowPlan set cfpContent='" + cfpContent + "',cfpFilePath='" + cfpFilePath + "',cfpDate='" + cfpDate + "',cfpOperatorID='" + cfpOperatorID + "',cfpModifyDate='getdate()',cfpModifyOperatorID='" + cfpOperatorID + "' where cfpID='" + cfpID + "' ");
                        AddOperatorLog("修改提醒:" + cfpID, 0, cfpOperatorID.ToString());
                        result = cfpID.ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowPlan'");
                    DbHelperSQL.ExecuteSQL(@"Insert into CustomerFollowPlan (cfpID,cfpRelatedID,cfpContent,cfpFilePath,cfpDate,cfpOperatorID,cfpAddOperatorID,cfpAddDate,cfpSource) 
values ('" + id + "','" + cfpRelatedID + "','" + cfpContent + "','" + cfpFilePath + "','" + cfpDate + "','" + cfpOperatorID + "','" + cfpOperatorID + "',getdate(),'" + cfpSource + "')");
                    AddOperatorLog("录入提醒:" + id, 0, cfpOperatorID.ToString());
                    result = id;
                }

                string content = "";
                if (cfpSource == "Customer")
                {
                    content += "客户[" + DbHelperSQL.GetSHSL("select cusName from Customer where cusID = " + cfpRelatedID) + "]";
                }
                else if (cfpSource == "Clue")
                {
                    content += "线索[" + DbHelperSQL.GetSHSL("select ccName from CustomerClue where ccID = " + cfpRelatedID) + "]";
                }
                else if (cfpSource == "Opptunity")
                {
                    content += "商机[" + DbHelperSQL.GetSHSL("select coName from CustomerOpptunity where coID = " + cfpRelatedID) + "]";
                }
                else if (cfpSource == "Business")
                {
                    content += "合同[" + DbHelperSQL.GetSHSL("select cbName from CustomerBusiness where cbID = " + cfpRelatedID) + "]";
                }
                else if (cfpSource == "Project")
                {
                    content += "项目[" + DbHelperSQL.GetSHSL("select pjName from Project where pjID = " + cfpRelatedID) + "]";
                }
                else if (cfpSource == "Feedback")
                {
                    content += "反馈[" + DbHelperSQL.GetSHSL("select cfbContent from CustomerFeedback where cfbID = " + cfpRelatedID) + "]";
                }
                content += "跟进提醒";
                string url = "Data/CustomerFollowPlanEditForm.aspx?ID=" + result;
                AddMessages(int.Parse(cfpOperatorID), int.Parse(cfpOperatorID), "跟进提醒", content + ":" + cfpContent, url, cfpDate);

                return result;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        #endregion

        #region CustomerBusiness
        public static CustomerBusinessResult GetBusiness(int id)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select A.*,D.opeName AS cbOperatorName,E.opeName as cbNotifyOperatorName from CustomerBusiness A  LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID LEFT OUTER JOIN Operators E ON A.cbNotifyOperatorID = E.opeID where cbID = " + id);
                CustomerBusinessResult entity = new CustomerBusinessResult();
                entity.cbDate = DateTime.Parse(dr["cbDate"] + "").ToString("yyyy-MM-dd");
                entity.cbGotAmount = dr["cbGotAmount"] + "";
                entity.cbID = id.ToString();
                entity.cbName = dr["cbName"] + "";
                entity.cbNotGotAmount = dr["cbNotGotAmount"] + "";
                entity.cbOperatorID = dr["cbOperatorID"] + "";
                entity.cbOperatorName = dr["cbOperatorName"] + "";
                entity.cbNotifyOperatorID = dr["cbNotifyOperatorID"] + "";
                entity.cbNotifyOperatorName = dr["cbNotifyOperatorName"] + "";
                entity.cbTotalAmount = dr["cbTotalAmount"] + "";
                entity.cbBusinessType = dr["cbBusinessType"] + "";
                entity.cbRemark = dr["cbRemark"] + "";
                entity.cbExpiredDate = dr["cbExpiredDate"] + "" == "" ? "" : DateTime.Parse(dr["cbExpiredDate"] + "").ToString("yyyy-MM-dd");
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string DeleteBusiness(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForBusiness(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhRelatedID in (" + ID + ") and cfhSource='Business'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpRelatedID in (" + ID + ") and cfpSource='Business'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CoWorker where cwRelatedID in (" + ID + ") and cwSource='Business'");

                    DataRow row = DbHelperSQL.GetDataRow("select cbName, cbCustomerID from CustomerBusiness where cbID = " + ID);
                    AddOperatorLog("删除合同:" + row["cbName"], int.Parse(row["cbCustomerID"] + ""), CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", int.Parse(row["cbCustomerID"] + ""), 0, "删除合同:" + row["cbName"], "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerBusiness where cbID = " + ID);
                    string url = "Data/CustomerBusinessEditForm.aspx?ID=" + ID;
                    DeleteMessageByURL(url);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string SaveBusiness(int cbCustomerID, int cbID, string cbName, string cbTotalAmount, string cbOperatorID, string cbDate, string cbRemark, string CurrentOperatorID, string cbBusinessType, string cbExpiredDate, string cbNotifyOperatorID)
        {
            try
            {
                string result = "-1";
                cbDate = cbDate.Replace("年", "-").Replace("月", "-").Replace("日", "");
                cbExpiredDate = cbExpiredDate.Replace("年", "-").Replace("月", "-").Replace("日", "");
                if (cbExpiredDate != "")
                {
                    cbExpiredDate = DateTime.Parse(cbExpiredDate).ToString("yyyy-MM-dd 09:00:00");
                }
                if (cbID > 0)
                {
                    if (CheckPermissionForBusiness(cbID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update CustomerBusiness set cbName = '{0}',cbTotalAmount={1},cbOperatorID={2},cbDate='{3}',cbRemark='{4}',cbModifyDate=getdate(),cbBusinessType='{6}' where cbID={5}", cbName, cbTotalAmount, cbOperatorID, cbDate, cbRemark, cbID, cbBusinessType));
                        AddOperatorLog("修改合同 ID:" + cbID, cbCustomerID, CurrentOperatorID);
                        SaveCustomerFollowHistory("Customer", cbCustomerID, 0, "修改合同 " + cbName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                        result = cbID.ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerBusiness'");
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerBusiness(cbID,cbName,cbTotalAmount,cbOperatorID,cbDate,cbRemark,cbCustomerID,cbAddDate,cbModifyDate,cbBusinessType,cbAddOperatorID,cbNotGotAmount,cbGotAmount)values({0},'{1}',{2},{3},'{4}','{5}',{6},getdate(),getdate(),'{7}',{3},{2},0.0)", id, cbName, cbTotalAmount, cbOperatorID, cbDate, cbRemark, cbCustomerID, cbBusinessType));
                    AddOperatorLog("新增业务 ID:" + id, cbCustomerID, CurrentOperatorID);
                    result = id;
                    SaveCustomerFollowHistory("Customer", cbCustomerID, 0, "录入合同 " + cbName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                }

                string url = "Data/CustomerBusinessEditForm.aspx?ID=" + result;

                if (cbNotifyOperatorID != "")
                {
                    DbHelperSQL.ExecuteSQL(string.Format("Update CustomerBusiness set cbNotifyOperatorID = '{0}' where cbID = {1}", cbNotifyOperatorID, result));
                    string title = "新合同[" + cbName + "]";
                    DeleteMessageByURL(url, int.Parse(cbNotifyOperatorID), "新合同提醒", "有新合同,快去看看吧");
                    AddMessages(int.Parse(cbOperatorID), int.Parse(cbNotifyOperatorID), "新合同提醒", "有新合同,快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                
                if (cbExpiredDate != "")
                {
                    DbHelperSQL.ExecuteSQL(string.Format("Update CustomerBusiness set cbExpiredDate = '{0}' where cbID = {1}", cbExpiredDate, result));
                    string title = "合同[" + cbName + "]到期提醒";
                    DeleteMessageByURL(url, int.Parse(cbOperatorID), "合同到期提醒", "合同[" + cbName + "]到期了,快去看看吧");
                    AddMessages(int.Parse(cbOperatorID), int.Parse(cbOperatorID), "合同到期提醒", "合同[" + cbName + "]到期了,快去看看吧", url, cbExpiredDate);
                }

                return result;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        private static string GetNumber(Object n)
        {
            if (n == null)
            {
                return "0";
            }
            double number = 0.0;
            if (double.TryParse(n + "", out number))
            {
                return number.ToString("N0");
            }
            return "0";
        }

        private static string GetOpeName(Object n)
        {
            string name = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + n);
            return name;
        }

        public static string AddCustomerBusinessInfo(string cbName, string cbBusinessTypeID, string cbDate, string cbTotalAmount, string cbOperatorID, string cbRemark, string CurrentOperatorID, string OpptunityID, string cbExpiredDate, string cbNotifyOperatorID)
        {
            try
            {
                if (cbBusinessTypeID == "")
                    cbBusinessTypeID = "0";
                string cusID = DbHelperSQL.GetSHSLInt("select coCustomerID from CustomerOpptunity where coID = " + OpptunityID);
                DbHelperSQL.ExecuteSQL("update CustomerOpptunity set coStatusID = '2' where coID = " + OpptunityID);//商机状态改为2，表示已经成功
                return SaveBusiness(int.Parse(cusID), 0, cbName, cbTotalAmount, cbOperatorID, cbDate, cbRemark, CurrentOperatorID, cbBusinessTypeID, cbExpiredDate, cbNotifyOperatorID);
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        #endregion

        #region CustomerReceipt
        public static CustomerReceiptResult GetReceipt(int id)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select A.*,B.cbName AS crBusinessName,D.opeName AS crOperatorName from CustomerReceipt A  LEFT OUTER JOIN CustomerBusiness B ON A.crBusinessID = B.cbID LEFT OUTER JOIN Operators D ON A.crOperatorID = D.opeID where crID = " + id);
                CustomerReceiptResult entity = new CustomerReceiptResult();
                entity.crAmount = dr["crAmount"] + "";
                entity.crBusinessID = dr["crBusinessID"] + "";
                entity.crBusinessName = dr["crBusinessName"] + "";
                entity.crDate = DateTime.Parse(dr["crDate"] + "").ToString("yyyy-MM-dd");
                entity.crOperatorID = dr["crOperatorID"] + "";
                entity.crOperatorName = dr["crOperatorName"] + "";
                entity.crRemark = dr["crRemark"] + "";
                entity.crBatchID = dr["crBatchID"] + "";
                entity.crTypeID = dr["crTypeID"] + "";
                entity.crID = id.ToString();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string DeleteReceipt(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForReceipt(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DataRow row = DbHelperSQL.GetDataRow("select crCustomerID, crBusinessID from CustomerReceipt where crID=" + ID);
                    AddOperatorLog("删除收款:" + ID, 0, CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", int.Parse(row["crCustomerID"] + ""), 0, "删除收款信息", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerReceipt where crID = " + ID);
                    return row["crBusinessID"] + "";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string SaveReceipt(int crCustomerID, int crID, string crBusinessID, string crAmount, string crOperatorID, string crDate, string CurrentOperatorID, string crTypeID, string crBatchID, string crRemark)
        {
            try
            {
                if (crBusinessID == "")
                {
                    crBusinessID = "NULL";
                }
                if (crAmount == "")
                {
                    return "0";
                }
                crDate = crDate.Replace("年", "-").Replace("月", "-").Replace("日", "");
                if (crID > 0)
                {
                    if (CheckPermissionForReceipt(crID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update CustomerReceipt set crBusinessID = {0},crAmount={1},crOperatorID={2},crDate='{3}',crModifyDate=getdate(),crTypeID='{5}',crBatchID='{6}', crRemark='{7}' where crID={4}", crBusinessID, crAmount, crOperatorID, crDate, crID, crTypeID, crBatchID, crRemark));
                        AddOperatorLog("修改收款 ID:" + crID, crCustomerID, CurrentOperatorID);
                        SaveCustomerFollowHistory("Customer", crCustomerID, 0, "修改收款信息", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerReceipt'");
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerReceipt(crID,crBusinessID,crAmount,crOperatorID,crDate,crCustomerID,crAddDate,crModifyDate,crTypeID,crAddOperatorID,crBatchID, crRemark)values({0},{1},{2},{3},'{4}',{5},getdate(),getdate(),'{6}',{3},'{7}','{8}')", id, crBusinessID, crAmount, crOperatorID, crDate, crCustomerID, crTypeID,crBatchID, crRemark));
                    AddOperatorLog("新增收款 ID:" + crID, crCustomerID, CurrentOperatorID);
                    //SendMessageToCoWorkerForCustomer(crCustomerID.ToString(), CurrentOperatorID, "客户新动态", "客户收款了，快去看看吧", "Data/CustomerEditForm.aspx?ID=" + crCustomerID);
                    SaveCustomerFollowHistory("Customer", crCustomerID, 0, "录入收款信息", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    SendMessageToCoWorkerForBusiness(crBusinessID, CurrentOperatorID, "合同新动态", "合同收款了，快去看看吧", "Data/CustomerBusinessEditForm.aspx?ID=" + crBusinessID);
                    return id;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            return crID.ToString();
        }

        public static string GetBusinessReceiptAmount(string crBusinessID, string crAmount, string crID)
        {
            try
            {
                string BusinessReceiptAmount = DbHelperSQL.GetSHSL("select SUM(crAmount) from CustomerReceipt where crBusinessID=" + crBusinessID);
                string CurrentAmount = DbHelperSQL.GetSHSL("select crAmount from CustomerReceipt where crID=" + crID);
                int SubAmount = int.Parse(BusinessReceiptAmount) - int.Parse(CurrentAmount);
                int sumAmount = SubAmount + int.Parse(crAmount);
                return sumAmount.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        #endregion

        #region CustomerReceiptPlan
        public static CustomerReceiptPlanResult GetReceiptPlan(int id)
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select A.*,B.cbName AS crpBusinessName,D.opeName AS crpOperatorName from CustomerReceiptPlan A  LEFT OUTER JOIN CustomerBusiness B ON A.crpBusinessID = B.cbID LEFT OUTER JOIN Operators D ON A.crpOperatorID = D.opeID where crpID = " + id);
                CustomerReceiptPlanResult entity = new CustomerReceiptPlanResult();
                entity.crpAmount = dr["crpAmount"] + "";
                entity.crpBusinessID = dr["crpBusinessID"] + "";
                entity.crpBusinessName = dr["crpBusinessName"] + "";
                entity.crpDate = DateTime.Parse(dr["crpDate"] + "").ToString("yyyy-MM-dd");
                entity.crpOperatorID = dr["crpOperatorID"] + "";
                entity.crpOperatorName = dr["crpOperatorName"] + "";
                entity.crpRemark = dr["crpRemark"] + "";
                entity.crpTypeID = dr["crpTypeID"] + "";
                entity.crpBatchID = dr["crpBatchID"] + "";
                entity.crpID = id.ToString();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string DeleteReceiptPlan(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForReceiptPlan(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DataRow row = DbHelperSQL.GetDataRow("select crpCustomerID, crpBusinessID from CustomerReceiptPlan where crpID=" + ID);
                    AddOperatorLog("删除收款计划:" + ID, 0, CurrentOperatorID.ToString());
                    SaveCustomerFollowHistory("Customer", int.Parse(row["crpCustomerID"] + ""), 0, "删除收款计划", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerReceiptPlan where crpID = " + ID);
                    return row["crpBusinessID"] + "";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string SaveReceiptPlan(int crpCustomerID, int crpID, string crpBusinessID, string crpAmount, string crpOperatorID, string crpDate, string CurrentOperatorID, string crpTypeID, string crpBatchID, string crpRemark)
        {
            try
            {
                if (crpBusinessID == "")
                {
                    crpBusinessID = "NULL";
                }
                if (crpAmount == "")
                {
                    return "0";
                }
                crpDate = crpDate.Replace("年", "-").Replace("月", "-").Replace("日", "");
                if (crpID > 0)
                {
                    if (CheckPermissionForReceiptPlan(crpID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update CustomerReceiptPlan set crpBusinessID = {0},crpAmount={1},crpOperatorID={2},crpDate='{3}',crpModifyDate=getdate(),crpTypeID='{5}',crpBatchID='{6}',crpRemark='{7}' where crpID={4}", crpBusinessID, crpAmount, crpOperatorID, crpDate, crpID, crpTypeID,crpBatchID, crpRemark));
                        AddMessages(int.Parse(CurrentOperatorID), int.Parse(CurrentOperatorID), "收款计划到期提醒", "收款计划到期了，快去看看吧", "Data/CustomerBusinessEditForm.aspx?ID=" + crpBusinessID, DateTime.Parse(crpDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        AddOperatorLog("修改收款 ID:" + crpID, crpCustomerID, CurrentOperatorID);
                        
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerReceiptPlan'");
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerReceiptPlan(crpID,crpBusinessID,crpAmount,crpOperatorID,crpDate,crpCustomerID,crpAddDate,crpModifyDate,crpTypeID,crpAddOperatorID,crpBatchID,crpRemark,crpStatus)values({0},{1},{2},{3},'{4}',{5},getdate(),getdate(),'{6}',{3},'{7}','{8}','未收款')", id, crpBusinessID, crpAmount, crpOperatorID, crpDate, crpCustomerID, crpTypeID,crpBatchID, crpRemark));
                    AddOperatorLog("新增收款计划 ID:" + crpID, crpCustomerID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", crpCustomerID, 0, "录入收款计划", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    //SendMessageToCoWorkerForCustomer(crpCustomerID.ToString(), CurrentOperatorID, "客户新动态", "客户做了收款计划了，快去看看吧", "Data/CustomerEditForm.aspx?ID=" + crpCustomerID);
                    SendMessageToCoWorkerForBusiness(crpBusinessID, CurrentOperatorID, "合同新动态", "合同做了收款计划了，快去看看吧", "Data/CustomerBusinessEditForm.aspx?ID=" + crpBusinessID);
                    AddMessages(int.Parse(CurrentOperatorID), int.Parse(CurrentOperatorID), "收款计划到期提醒", "收款计划到期了，快去看看吧", "Data/CustomerBusinessEditForm.aspx?ID=" + crpBusinessID, DateTime.Parse(crpDate).ToString("yyyy-MM-dd HH:mm:ss"));
                    return id;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            return crpID.ToString();
        }

        public static string GetBusinessReceiptPlanAmount(string crpBusinessID, string crpAmount, string crpID)
        {
            try
            {
                string BusinessReceiptAmount = DbHelperSQL.GetSHSL("select SUM(crpAmount) from CustomerReceiptPlan where crpBusinessID=" + crpBusinessID);
                string CurrentAmount = DbHelperSQL.GetSHSL("select crpAmount from CustomerReceiptPlan where crpID=" + crpID);
                int SubAmount = int.Parse(BusinessReceiptAmount) - int.Parse(CurrentAmount);
                int sumAmount = SubAmount + int.Parse(crpAmount);
                return sumAmount.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        #endregion

        #region CustomerFeedback
        public static string HandleFeedback(int cfbID, string cfbResult, string cfbStatus)
        {
            try
            {
                DbHelperSQL.ExecuteSQL(string.Format("Update CustomerFeedback set cfbResult = '{0}',cfbStatusID={1},cfbHandleDate='{2}' where cfbID={3}", cfbResult, cfbStatus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cfbID));
                string url = "Data/CustomerFeedbackEditForm.aspx?Action=View&ID=" + cfbID;
                DataRow row = DbHelperSQL.GetDataRow("select cfbHandleOperatorID, cfbCustomerID, cfbOperatorID from CustomerFeedback where cfbID=" + cfbID);
                AddMessages(int.Parse(row["cfbOperatorID"] + ""), int.Parse(row["cfbOperatorID"] + ""), "客户反馈提醒", "客户反馈处理有更新了，快去看看吧", url, DateTime.Now.ToString());
                SaveCustomerFollowHistory("Customer", int.Parse(row["cfbCustomerID"] + ""), 0, "客户反馈处理更新", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", row["cfbHandleOperatorID"] + "", "", "", row["cfbHandleOperatorID"] + "");
                return "OK";
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static FeedbackList GetFeedback(int id)
        {
            try
            {
                string sql = @"select A.*,B.Name as cfbTypeName,C.opeName as AddOperatorName,D.opeName as cfbHandleOperator,E.sname as cfbStatus from CustomerFeedback A 
left outer join defCommon B on A.cfbFeedbackTypeID=B.ID 
left outer join Operators C on A.cfbAddOperatorID=C.opeID 
left outer join Operators D on A.cfbHandleOperatorID=D.opeID 
left outer join sdefCommon E on A.cfbStatusID=E.sID where cfbID=" + id;
                DataRow dr = DbHelperSQL.GetDataRow(sql);
                FeedbackList fb = new FeedbackList();
                fb.cfbContent = dr["cfbContent"] + "";
                fb.cfbEmail = dr["cfbEmail"] + "";
                fb.cfbHandleOperatorID = dr["cfbHandleOperatorID"] + "";
                fb.cfbHandleOperator = dr["cfbHandleOperator"] + "";
                fb.cfbID = dr["cfbID"] + "";
                fb.cfbLinkman = dr["cfbLinkman"] + "";
                fb.cfbOrderRelated = dr["cfbOrderRelated"] + "";
                fb.cfbResult = dr["cfbResult"] + "";
                fb.cfbStatusID = dr["cfbStatusID"] + "";
                fb.cfbStatus = dr["cfbStatus"] + "";
                fb.cfbTelephone = dr["cfbTelephone"] + "";
                fb.cfbFeedbackTypeID = dr["cfbFeedbackTypeID"] + "";
                fb.cfbTypeName = dr["cfbTypeName"] + "";
                return fb;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string SaveFeedback(int cfbCustomerID, int cfbID, string cfbFeedbackTypeID, string cfbLinkman, string cfbTelephone, string cfbEmail, string cfbOrderRelated, string cfbContent, string cfbHandleOperatorID, string CurrentOperatorID, string cfbResult, string cfbStatusID)
        {
            try
            {
                string result = "-1";
                if (cfbID > 0)
                {
                    if (CheckPermissionForFeedback(cfbID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(string.Format("Update CustomerFeedback set cfbFeedbackTypeID = {0},cfbLinkman='{1}',cfbTelephone='{2}',cfbEmail='{3}',cfbOrderRelated='{4}',cfbContent='{5}',cfbHandleOperatorID={6},cfbModifyOperatorID={7},cfbModifyDate=getdate(),cfbResult='{8}',cfbStatusID='{9}' where cfbID={10}", cfbFeedbackTypeID, cfbLinkman, cfbTelephone, cfbEmail, cfbOrderRelated, cfbContent, cfbHandleOperatorID, CurrentOperatorID, cfbResult, cfbStatusID, cfbID));
                        AddOperatorLog("修改客户反馈 ID：" + cfbID, cfbCustomerID, CurrentOperatorID);
                        SaveCustomerFollowHistory("Customer", cfbCustomerID, 0, "修改了客户反馈", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                        result = cfbID.ToString();
                        if (cfbHandleOperatorID != "")
                        {
                            string cusName = DbHelperSQL.GetSHSL("select cusName from customer where cusID=" + cfbCustomerID);
                            string content = "客户[" + cusName + "] 反馈了问题，快去看看吧";
                            string url = "Data/CustomerFeedbackEditForm.aspx?ID=" + cfbID;
                            AddMessages(int.Parse(CurrentOperatorID), int.Parse(cfbHandleOperatorID.ToString()), "客户反馈提醒", content, url, DateTime.Now.ToString());
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFeedback'");
                    DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerFeedback(cfbID,cfbCustomerID,cfbFeedbackTypeID,cfbLinkman,cfbTelephone,cfbEmail,cfbOrderRelated,cfbContent,cfbHandleOperatorID,cfbAddOperatorID,cfbOperatorID,cfbStatusID,cfbAddDate,cfbModifyDate) values ({0},{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},23,getdate(),getdate())", id, cfbCustomerID, cfbFeedbackTypeID, cfbLinkman, cfbTelephone, cfbEmail, cfbOrderRelated, cfbContent, cfbHandleOperatorID, CurrentOperatorID, CurrentOperatorID));
                    AddOperatorLog("新增客户反馈 ID：" + id, cfbCustomerID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Customer", cfbCustomerID, 0, "录入了客户反馈", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                    result = id;
                    if (cfbHandleOperatorID != "")
                    {
                        string cusName = DbHelperSQL.GetSHSL("select cusName from customer where cusID=" + cfbCustomerID);
                        string content = "客户[" + cusName + "] 反馈了问题，快去看看吧";
                        string url = "Data/CustomerFeedbackEditForm.aspx?ID=" + id;
                        AddMessages(int.Parse(CurrentOperatorID), int.Parse(cfbHandleOperatorID.ToString()), "客户反馈提醒", content, url, DateTime.Now.ToString());
                    }
                }

                

                return result;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string DeleteFeedback(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForFeedback(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    DbHelperSQL.ExecuteSQL("Delete from CustomerFeedback where cfbID = " + ID);
                    AddOperatorLog("删除反馈:" + ID, 0, CurrentOperatorID.ToString());
                    //SaveCustomerFollowHistory("Customer", cfbCustomerID, 0, "录入了客户反馈", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                    string url = "Data/CustomerFeedbackEditForm.aspx?ID=" + ID;
                    DeleteMessageByURL(url);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }


        public static string SaveHelpCenterFeedBackInfo(string fdOperatorID, string fdContent, string fdOperatorEmail, string fdOperatorPhone)
        {
            try
            {
                DateTime fdAddDate = DateTime.Now;
                int fdTypeID = 0;
                if (fdOperatorID != "")
                {
                    DbHelperSQL.ExecuteSQL(string.Format(@"INSERT INTO FeedBack (fdOperatorID, fdAddDate,fdTypeID,fdContent,fdOperatorEmail, fdOperatorPhone) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", fdOperatorID, fdAddDate, fdTypeID, fdContent, fdOperatorEmail, fdOperatorPhone));
                    int toOperatorID = 171;
                    string adminId = DbHelperSQL.GetSHSLInt("select opeID from Operators where opeMobile like '%18823310469%'");
                    int.TryParse(adminId, out toOperatorID);
                    string opeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID = " + fdOperatorID);
                    AddMessages(int.Parse(fdOperatorID), toOperatorID, "系统反馈建议提醒", opeName + "反馈:" + fdContent, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region WorkDiary
        public static string SaveWorkDiary(string wdDate, string wdTitle, string wdContent, string wdNotifierID, string wdAddOperatorID, string wdTypeID, string wdFile)
        {
            try
            {
                string id = DbHelperSQL.GetSHSLInt("usp_GetID 'WorkDiary'");
                DbHelperSQL.ExecuteSQL(string.Format("insert into WorkDiary (wdID,wdDate,wdTitle,wdContent,wdNotifierID,wdAddOperatorID,wdAddDate,wdTypeID,wdFile) values ('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}','{7}')", id, wdDate, wdTitle, wdContent, wdNotifierID, wdAddOperatorID, wdTypeID, wdFile));
                AddOperatorLog("新增工作日报 ID:" + id, wdAddOperatorID);
                string url = "data/CustomerWorkReportEditForm.aspx?Action=View&ID=" + id;
                string addOperatorID = DbHelperSQL.GetSHSL("select wdAddOperatorID from WorkDiary where wdID=" + id);
                string Name = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + addOperatorID);

                if (!string.IsNullOrEmpty(wdNotifierID))
                {
                    AddMessages(int.Parse(addOperatorID), int.Parse(wdNotifierID), "" + Name + "工作日报提交提醒", "" + Name + "工作日报提交提醒", url, DateTime.Now.ToString());
                }
                return id;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string DeleteWorkDiary(string wdID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForWorkDiary(wdID, CurrentOperatorID.ToString()))
                {
                    int i = DbHelperSQL.ExecuteSQL("Delete from WorkDiary where wdID=" + wdID);
                    AddOperatorLog("删除工作报告:" + wdID, 0, CurrentOperatorID.ToString());
                    return wdID;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        #endregion

        #region CustomerClue
        public static string SaveCustomerClue(string ccActivityID,int ccID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            try
            {
                string newDepartmentID = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID = " + ccOperatorID);
                if (CheckPermissionForClue(ccID.ToString(), CurrentOperatorID))
                {
                    DbHelperSQL.ExecuteSQL(string.Format(@"Update CustomerClue set 
ccName = '{0}',
ccCustomerName='{1}',
ccTel='{2}',
ccMobile='{3}',
ccAddress='{4}',
ccRemark='{5}',
ccOperatorID={6},
ccDepartmentID={7},
ccModifyDate=getdate(),
ccModifyOperatorID='{8}',
ccActivityID='{10}' where ccID={9}", ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, newDepartmentID, CurrentOperatorID, ccID, ccActivityID));

                    AddOperatorLog("修改线索资料 ID:" + ccID, ccID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Clue", ccID, 0, "修改了客户线索 " + ccName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                    return ccID.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static string AddCustomerClue(string ccActivityID, string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            try
            {
                string ccID = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerClue'");
                string newDepartmentID = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID = " + ccOperatorID);
                DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerClue 
(ccID,ccName,ccCustomerName,ccTel,ccMobile,ccAddress,ccRemark,ccOperatorID,ccDepartmentID,ccAddOperatorID,ccAddDate,ccStatusID,ccActivityID) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},getdate(),'54','{10}')", ccID, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, newDepartmentID, CurrentOperatorID, ccActivityID));

                AddOperatorLog("新增线索资料 ID:" + ccID, int.Parse(ccID), CurrentOperatorID);
                SaveCustomerFollowHistory("Clue", int.Parse(ccID), 0, "录入了客户线索 " + ccName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                string currentOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID = " + CurrentOperatorID);
                if (int.Parse(CurrentOperatorID) != ccOperatorID)
                {
                    AddMessages(int.Parse(CurrentOperatorID), ccOperatorID, "线索分配提醒", currentOperatorName + "分配了一条线索给你，快去看看吧", "Data/CustomerClueEditForm.aspx?ID=" + ccID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return ccID;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string SaveMarketingActivityClue(int ccID,string ccActivityID,string ccName, string ccCustomerName, string ccTel, string ccMobile, string ccAddress, string ccRemark, int ccOperatorID, int ccDepartmentID, string CurrentOperatorID)
        {
            try
            {
                if (ccID > 0)
                {
                    DbHelperSQL.ExecuteSQL(string.Format(@"Update CustomerClue set 
ccName = '{0}',
ccCustomerName='{1}',
ccTel='{2}',
ccMobile='{3}',
ccAddress='{4}',
ccRemark='{5}',
ccOperatorID={6},
ccDepartmentID={7},
ccModifyDate=getdate(),
ccModifyOperatorID='{8}' where ccID={9}", ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID, ccID));

                    AddOperatorLog("修改线索资料 ID:" + ccID, ccID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Clue", ccID, 0, "修改了客户线索 " + ccName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                    return ccID.ToString();
                }
                else
                {
                    string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerClue'");
                    DbHelperSQL.ExecuteSQL(string.Format(@"insert into CustomerClue 
(ccID,ccName,ccCustomerName,ccTel,ccMobile,ccAddress,ccRemark,ccOperatorID,ccDepartmentID,ccAddOperatorID,ccAddDate,ccStatusID,ccActivityID) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},getdate(),'54','{10}')", id, ccName, ccCustomerName, ccTel, ccMobile, ccAddress, ccRemark, ccOperatorID, ccDepartmentID, CurrentOperatorID, ccActivityID));

                    AddOperatorLog("新增线索资料 ID:" + ccID, int.Parse(id), CurrentOperatorID);
                    SaveCustomerFollowHistory("Clue", int.Parse(id), 0, "录入了客户线索 " + ccName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID, "", "", CurrentOperatorID);
                    return id;
                }
                
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static CustomerCLueInfo GetMarketActivityClue(int ID) 
        {
            try
            {
                DataRow dr = DbHelperSQL.GetDataRow("select * from CustomerClue where ccID=" + ID);
                CustomerCLueInfo cc = new CustomerCLueInfo();
                cc.ccName = dr["ccName"] + "";
                cc.ccCustomerName = dr["ccCustomerName"] + "";
                cc.ccTel = dr["ccTel"] + "";
                cc.ccMobile = dr["ccMobile"] + "";
                cc.ccRemark = dr["ccRemark"] + "";
                cc.ccAddress = dr["ccAddress"] + "";
                cc.ccOperatorID = dr["ccOperatorID"] + "";
                cc.ccDepartmentID = dr["ccDepartmentID"] + "";
                return cc;

            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public static int DeleteClue(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForClue(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhRelatedID in (" + ID + ") and cfhSource='Clue'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpRelatedID in (" + ID + ") and cfpSource='Clue'");

                    DataRow row = DbHelperSQL.GetDataRow("select ccCustomerName,ccName from CustomerClue where ccID = " + ID);
                    AddOperatorLog("删除线索:" + row["ccCustomerName"] + "-" + row["ccName"], 0, CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from CustomerClue where ccID=" + ID);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ChangeClueOperator(int ccID, string ccOperatorID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForClue(ccID.ToString(), CurrentOperatorID.ToString()))
                {
                    int i = DbHelperSQL.ExecuteSQL("Update CustomerClue set ccOperatorID='" + ccOperatorID + "' where ccID=" + ccID);
                    if (i > 0)
                    {
                        string ccName = DbHelperSQL.GetSHSL("select ccName from CustomerClue where ccID=" + ccID);
                        string newOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID = " + ccOperatorID);
                        string url = "Data/CustomerClueEditForm.aspx?ID=" + ccID;
                        AddMessages(CurrentOperatorID, int.Parse(ccOperatorID), "线索转移通知", GetOpeName(CurrentOperatorID) + "把线索[" + ccName + "]已转到你名下,快去看看吧", url, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        SaveCustomerFollowHistory("Clue", ccID, 0, GetOpeName(CurrentOperatorID) + "把客户线索转给" + newOperatorName, "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    }

                    return ccID;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static string SetFollowPlanMessageRead(int cfpID, string messageID, string CurrentOperatorID)
        {
            try
            {
                if (messageID != "")
                {
                    SetMessageReaded(messageID, CurrentOperatorID);
                    return "1";
                }
                else
                {
                    string mID = DbHelperSQL.GetSHSL("select MessageID from sysMessage where URL like '%data/CustomerFollowPlanEditForm.aspx?ID=" + cfpID + "&MessageID%'");
                    SetMessageReaded(mID, CurrentOperatorID);
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        #endregion

        #region Project
        public static string SaveProject(int pjID, string pjNO, string pjName, string pjCompanyName, string pjDetail, string pjProduct, string pjAmount, string pjContactor, string pjPrice, string pjRemark, string pjOperatorID, string pjToApproveOperatorID, string pjExpiredDate, string CurrentOperatorID)
        {
            try
            {
                if (pjID > 0)
                {
                    if (CheckPermissionForProject(pjID.ToString(), CurrentOperatorID))
                    {
                        DbHelperSQL.ExecuteSQL(string.Format(@"Update Project set 
pjNO = '{0}',
pjName='{1}',
pjCompanyName='{2}',
pjDetail='{3}',
pjProduct='{4}',
pjAmount='{5}',
pjContactor='{6}',
pjPrice='{7}',
pjRemark='{8}',
pjModifyDate=getdate(),
pjModifyOperatorID='{9}',pjOperatorID='{11}',pjExpiredDate='{12}' where pjID={10}", pjNO, pjName, pjCompanyName, pjDetail, pjProduct, pjAmount, pjContactor, pjPrice, pjRemark, CurrentOperatorID, pjID, pjOperatorID,pjExpiredDate));

                        AddOperatorLog("修改项目资料 ID:" + pjID, 0, CurrentOperatorID);
                        SaveCustomerFollowHistory("Project", pjID, 0, "修改了项目报备", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                        return pjID.ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                     pjID = int.Parse(DbHelperSQL.GetSHSLInt("usp_GetID 'Project'"));
                    DbHelperSQL.ExecuteSQL(string.Format(@"insert into Project 
(pjID,pjNO,pjName,pjCompanyName,pjDetail,pjProduct,pjAmount,pjContactor,pjPrice,pjRemark, pjOperatorID, pjAddOperatorID,pjAddDate,pjToApproveOperatorID,pjExpiredDate) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate(),'{12}','{13}')", pjID, pjNO, pjName, pjCompanyName, pjDetail, pjProduct, pjAmount, pjContactor, pjPrice, pjRemark, pjOperatorID, CurrentOperatorID, pjToApproveOperatorID, pjExpiredDate));

                    AddOperatorLog("新增项目资料 ID:" + pjID, pjID, CurrentOperatorID);
                    SaveCustomerFollowHistory("Project", pjID, 0, "录入了项目报备", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                    if (pjToApproveOperatorID != "" && pjToApproveOperatorID != "0")
                    {
                        DbHelperSQL.Exists("select * from Operators where opeID=" + pjToApproveOperatorID);
                        string Url = "Data/ProjectEditForm.aspx?Action=View&ID=" + pjID;
                        AddMessages(int.Parse(CurrentOperatorID), int.Parse(pjToApproveOperatorID), "项目审核提醒", "有新项目需要你审核，快去看看吧", Url, DateTime.Now.ToString());
                    }

                    if (pjExpiredDate != "")
                    {
                        string Url = "Data/ProjectEditForm.aspx?Action=View&ID=" + pjID;
                        DeleteMessageByURL(Url, int.Parse(CurrentOperatorID), "项目提醒", "项目到期了");
                        AddMessages(int.Parse(CurrentOperatorID), int.Parse(pjOperatorID), "项目提醒", "项目到期了", Url, pjExpiredDate);
                    }
                    return pjID.ToString();
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static int DeleteProject(int ID, int CurrentOperatorID)
        {
            try
            {
                if (CheckPermissionForProject(ID.ToString(), CurrentOperatorID.ToString()))
                {
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhRelatedID in (" + ID + ") and cfhSource='Project'");
                    SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpRelatedID in (" + ID + ") and cfpSource='Project'");

                    DataRow row = DbHelperSQL.GetDataRow("select pjCompanyName,pjName from Project where pjID = " + ID);
                    string url = "Data/ProjectEditForm.aspx?ID=" + ID;
                    DeleteMessageByURL(url);
                    AddOperatorLog("删除项目:" + row["pjCompanyName"] + "-" + row["pjName"], 0, CurrentOperatorID.ToString());
                    DbHelperSQL.ExecuteSQL("Delete from Project where pjID=" + ID);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ApproveProject(int ID, int CurrentOperatorID)
        {
            try
            {
                if (DbHelperSQL.Exists("select * from Project where pjID = " + ID + " and pjToApproveOperatorID=" + CurrentOperatorID))
                {
                    string Url = "Data/ProjectEditForm.aspx?Action=View&ID=" + ID;
                    string pjOperatorID = DbHelperSQL.GetSHSL("select pjOperatorID from Project where pjID = " + ID);
                    //已经审核
                    if (DbHelperSQL.Exists("select * from Project where pjApproveTag = '已审核' and pjID = " + ID))
                    {
                        DbHelperSQL.ExecuteSQL("Update Project set pjApproveOperatorID=null, pjApproveDate=null, pjApproveTag=null where pjID = " + ID);
                        AddOperatorLog("反审核项目", ID, CurrentOperatorID.ToString());
                        SaveCustomerFollowHistory("Project", ID, 0, "反审核项目", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                        AddMessages(CurrentOperatorID, int.Parse(pjOperatorID), "项目审核提醒", "反审核了你的项目，快去看看吧", Url, DateTime.Now.ToString());
                    }
                    else
                    {
                        DbHelperSQL.ExecuteSQL("Update Project set pjApproveOperatorID=" + CurrentOperatorID + ", pjApproveDate=getdate(), pjApproveTag='已审核' where pjID = " + ID);
                        AddOperatorLog("审核通过项目", ID, CurrentOperatorID.ToString());
                        SaveCustomerFollowHistory("Project", ID, 0, "审核通过项目", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                        AddMessages(CurrentOperatorID, int.Parse(pjOperatorID), "项目审核提醒", "审核通过了你的项目，快去看看吧", Url, DateTime.Now.ToString());
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region CoWorker
        public static string SaveCoWorker(string cwRelatedID, string cwOperatorID, string CurrentOperatorID, string cwSource)
        {
            try
            {
                //typeID为1为客户协作人
                string cwID = DbHelperSQL.GetSHSLInt("usp_GetID 'CoWorker'");
                DbHelperSQL.ExecuteSQL(@"Insert into CoWorker (cwID,cwSource,cwRelatedID,cwOperatorID,cwAddOperatorID,cwAddDate) 
values ('" + cwID + "','" + cwSource + "','" + cwRelatedID + "','" + cwOperatorID + "','" + CurrentOperatorID + "',getdate())");

                string CurrentopeName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID=" + CurrentOperatorID);
                string opeDingDingUserID = DbHelperSQL.GetSHSL("select opeDingDingUserID from Operators where opeID=" + CurrentOperatorID);
                string messcusName = DbHelperSQL.GetSHSL("select cusName from Customer where cusID=" + cwRelatedID);

                messcusName = "客户[" + messcusName + "]";
                string Url = "";
                if (cwSource == "Customer")
                {
                    messcusName = "客户[" + messcusName + "]";
                    Url = "Data/CustomerEditForm.aspx?Action=View&ID=" + cwRelatedID;
                }
                else if (cwSource == "Opptunity")
                {
                    messcusName = DbHelperSQL.GetSHSL("select coName from CustomerOpptunity where coID=" + cwRelatedID);
                    messcusName = "商机[" + messcusName + "]";
                    Url = "Data/CustomerOpptunityEditForm.aspx?Action=View&ID=" + cwRelatedID;
                }
                else if (cwSource == "Business")
                {
                    messcusName = DbHelperSQL.GetSHSL("select cbName from CustomerBusiness where cbID=" + cwRelatedID);
                    messcusName = "合同[" + messcusName + "]";
                    Url = "Data/CustomerBusinessEditForm.aspx?Action=View&ID=" + cwRelatedID;
                }
                SaveCustomerFollowHistory(cwSource, int.Parse(cwRelatedID), 0, "添加协作人", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                AddMessages(int.Parse(CurrentOperatorID), int.Parse(cwOperatorID), "协作消息提醒", CurrentopeName + "添加你为" + messcusName + "的协作人，快去看看吧", Url, DateTime.Now.ToString());

                return cwID;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetDepartment(string cwOperatorID)
        {
            try
            {
                return DbHelperSQL.GetSHSL("select depName from Department A left outer join Operators B on A.depID=B.opeDepartmentID where opeID=" + cwOperatorID);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static int DeleteCoWorkerView(int ID, int CurrentOperatorID)
        {
            try
            {
                DataRow row = DbHelperSQL.GetDataRow("select cwRelatedID, cwSource from CoWorker where cwID = " + ID);
                string source = row["cwSource"] + "";
                string relatedID = row["cwRelatedID"] + "";
                if (source == "Customer")
                {
                    if (!CheckPermissionForCustomer(relatedID, CurrentOperatorID.ToString()))
                    {
                        return 0;
                    }
                }
                if (source == "Opptunity")
                {
                    if (!CheckPermissionForOpptunity(relatedID, CurrentOperatorID.ToString()))
                    {
                        return 0;
                    }
                }
                if (source == "Business")
                {
                    if (!CheckPermissionForBusiness(relatedID, CurrentOperatorID.ToString()))
                    {
                        return 0;
                    }
                }

                DbHelperSQL.ExecuteSQL("Delete from CoWorker where cwID=" + ID);
                SaveCustomerFollowHistory(source, int.Parse(relatedID), 0, "删除协作人", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", CurrentOperatorID.ToString(), "", "", CurrentOperatorID.ToString());
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region GetData

        public static IList<CustomerCLueInfo> GetCustomerClueDate(int count, int EachCount, string ccStatusID, string keyword, string ccOperatorID, string ccDepartmentID, string ccAddOperatorID, string ccDateBegin, string ccDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("ccOperatorID", currentOperatorID, "ccAddOperatorID");
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (ccName like '%{0}%' or ccCustomerName like '%{0}%' or ccTel like '%{0}%' or ccMobile like '%{0}%')", keyword);
                }
                if (ccStatusID != "")
                {
                    if (ccStatusID == "1")
                    {
                        whereCondition += " AND ccStatusID in (54,55)";
                    }
                    else if (ccStatusID == "2")
                    {
                        whereCondition += " AND ccStatusID in (56,58)";
                    }
                }
                if (ccDepartmentID != "")
                {
                    whereCondition += " AND ccDepartmentID = " + ccDepartmentID;
                }
                if (ccOperatorID != "")
                {
                    whereCondition += " AND ccOperatorID = " + ccOperatorID;
                }
                if (ccDateBegin != "")
                {
                    whereCondition += " AND ccAddDate >= '" + ccDateBegin + "'";
                }
                if (ccDateEnd != "")
                {
                    whereCondition += " AND ccAddDate <= '" + ccDateEnd + "'";
                }

                string orderByStr = " ORDER BY ccID DESC";
                if (cusColumn != "")
                {
                    orderByStr = " order by " + cusColumn + " " + orderby + "";
                }

                string sql = string.Format(@"SELECT top {0} w1.ccID,ccName,ccAddress,ccAddDate,ccCustomerName,opeName,ISNULL(C.sName,'未处理') as StatusName from CustomerClue w1 
LEFT OUTER JOIN Operators B ON w1.ccOperatorID = B.opeID 
LEFT OUTER JOIN sdefCommon C ON w1.ccStatusID=C.sID,(SELECT TOP {0} row_number() OVER ({3}) n, ccID FROM CustomerClue where (1=1) {2}) w2 WHERE w1.ccID = w2.ccID AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, orderByStr);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"SELECT top {0} w1.ccID,ccName,ccMobile,ccAddress,ccAddDate,ccCustomerName,opeName,ISNULL(C.sName,'未处理') as StatusName from CustomerClue w1 
INNER JOIN Operators B ON w1.ccOperatorID = B.opeID 
LEFT OUTER JOIN sdefCommon C ON w1.ccStatusID=C.sID WHERE (1=1) {1} {2}", count, whereCondition, orderByStr);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerCLueInfo> list = new List<CustomerCLueInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerCLueInfo cc = new CustomerCLueInfo();
                    cc.ccID = row["ccID"] + "";
                    cc.ccCustomerName = row["ccCustomerName"] + "";
                    cc.ccName = row["ccName"] + "";
                    cc.opeName = row["opeName"] + "";
                    cc.ccStatus = row["StatusName"] + "";
                    cc.ccAddress = row["ccAddress"] + "";
                    cc.ccMobile = row["ccMobile"] + "";
                    cc.ccAddDate = DateTime.Parse(row["ccAddDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(cc);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from CustomerClue where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerInfoResult> GetCustomerDate(int count, int EachCount, string cusKindID, string keyword, string cusSource, string cusDepartment, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            try
            {
                string whereCondition = "";
                if (conditionType != "")
                {
                    switch (conditionType)
                    {
                        case "NDayNotFollowCustomer":
                            whereCondition += " AND  dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + currentOperatorID;
                            break;
                        case "NDayNotFollowNotSuccessCustomer":
                            whereCondition += " AND  cusStatusID in (61,62,63) and dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + currentOperatorID;
                            break;
                        case "NDayNotSuccessCustomer":
                            whereCondition += " AND  cusStatusID in (61,62,63) and dateadd(day," + conditionValue + ",cusAddDate) < getdate() and cusOperatorID=" + currentOperatorID;
                            break;
                        case "NDayNotFollowSuccessCustomer":
                            whereCondition += " AND cusStatusID = 64 and dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + currentOperatorID;
                            break;
                        default:
                            whereCondition += " AND cusID not in (select cusID from XCustomerPool) ";
                            break;
                    }
                }
                else
                {
                    whereCondition += " AND cusID not in (select cusID from XCustomerPool) ";
                }

                if (keyword != "")
                {
                    if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
                    {
                        string cusCNs = "'kkk'";
                        DataTable dt2 = DbHelperSQL.GetDataTable(string.Format("select CustNO from v_SellOrderItem where FBillNoDD like '%{0}%' or FItemIDNumber like '%{0}%' or FItemIDName like '%{0}%' or Fmodel like  '%{0}%' or FItemIDName like '%{0}%'",keyword));
                        foreach (DataRow r in dt2.Rows)
                        {
                            cusCNs += ",'" + r["CustNO"] + "'";
                        }
                        whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%' or cbName in (select FBillNoDD from v_SellOrderItem where FItemIDNumber like '%{0}%' or FItemIDName like '%{0}%' or Fmodel like  '%{0}%' or FItemIDName like '%{0}%')) OR cusCN in ({1}))", keyword, cusCNs);
                    }
                    else
                    {
                        whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%'))", keyword);
                    }
                }
                if (cusKindID != "")
                {
                    whereCondition += " AND cusKindID = " + cusKindID;
                }
                if (cusDepartment != "")
                {
                    whereCondition += " AND cusDepartmentID = " + cusDepartment;
                }
                if (cusOperator != "")
                {
                    whereCondition += " AND cusOperatorID = " + cusOperator;
                }
                if (cusDateBegin != "")
                {
                    whereCondition += " AND cusAddDate >= '" + cusDateBegin + "'";
                }
                if (cusDateEnd != "")
                {
                    whereCondition += " AND cusAddDate <= '" + cusDateEnd + "'";
                }

                string permission = GetPermissionWhereCondition("cusOperatorID", currentOperatorID);
                //如果不是公司全部都可以看到，则加入协同人
                if (!string.IsNullOrEmpty(permission))
                {
                    whereCondition = whereCondition + permission + " OR (1=1 " + whereCondition + " and cusID in (select cwRelatedID from CoWorker where cwSource='Customer' and cwOperatorID=" + currentOperatorID + "))";
                }

                string orderByStr = " ORDER BY cusID DESC";
                if (cusColumn != "")
                {
                    orderByStr = " order by " + cusColumn + " " + orderby + "";
                }

                string sql = string.Format(@"SELECT w1.cusID,cusName,cusAddress,cusAddDate,cusContactor,opeName,C.ctName as TypeName from Customer w1 
LEFT OUTER JOIN Operators B ON w1.cusOperatorID = B.opeID 
LEFT OUTER JOIN defCustomerType C ON w1.cusKindID=C.ctID,(SELECT TOP {0} row_number() OVER ({3}) n, cusID as cusID2 FROM Customer where (1=1) {2}) w2 WHERE w1.cusID = w2.cusID2 AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, orderByStr);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"SELECT top {0} w1.cusID,cusName,cusAddress,cusAddDate,cusContactor,opeName,C.ctName as TypeName from Customer w1 
LEFT OUTER JOIN Operators B ON w1.cusOperatorID = B.opeID 
LEFT OUTER JOIN defCustomerType C ON w1.cusKindID=C.ctID WHERE (1=1) {1} {2}", count, whereCondition, orderByStr);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerInfoResult> list = new List<CustomerInfoResult>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerInfoResult cus = new CustomerInfoResult();
                    cus.cusID = row["cusID"] + "";
                    cus.cusName = row["cusName"] + "";
                    cus.cusAddress = row["cusAddress"] + "";
                    cus.opeName = row["opeName"] + "";
                    cus.cusContactor = row["cusContactor"] + "";
                    cus.TypeName = row["TypeName"] + "";
                    cus.cusAddDate = DateTime.Parse(row["cusAddDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(cus);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from Customer where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerInfoResult> GetCustomerPublicPoolDate(int count, int EachCount, string cusKindID, string keyword, string cusSource, string cusDepartment, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = " AND cusID in (select cusID from XCustomerPool) ";
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (cusName like '%{0}%' or cusAddress like '%{0}%' or cusTel like '%{0}%' or cusCertificationNO like '%{0}%'  or cusEnglishName like '%{0}%' or cusID in (select clmCustomerID from CustomerLinkMan where clmTel like '%{0}%' or clmMobile like '%{0}%' or clmName like '%{0}%'))", keyword);
                }
                if (cusKindID != "")
                {
                    whereCondition += " AND cusKindID = " + cusKindID;
                }
                if (cusDepartment != "")
                {
                    whereCondition += " AND cusDepartmentID = " + cusDepartment;
                }
                if (cusOperator != "")
                {
                    whereCondition += " AND cusOperatorID = " + cusOperator;
                }
                if (cusDateBegin != "")
                {
                    whereCondition += " AND cusAddDate >= '" + cusDateBegin + "'";
                }
                if (cusDateEnd != "")
                {
                    whereCondition += " AND cusAddDate <= '" + cusDateEnd + "'";
                }

                string orderByStr = " ORDER BY cusID DESC";
                if (cusColumn != "")
                {
                    orderByStr = " order by " + cusColumn + " " + orderby + "";
                }

                string sql = string.Format(@"SELECT w1.cusID,cusName,cusAddress,cusAddDate,cusContactor,opeName,C.ctName as TypeName from Customer w1 
LEFT OUTER JOIN Operators B ON w1.cusOperatorID = B.opeID 
LEFT OUTER JOIN defCustomerType C ON w1.cusKindID=C.ctID,(SELECT TOP {0} row_number() OVER ({3}) n, cusID FROM Customer where (1=1) {2}) w2 WHERE w1.cusID = w2.cusID AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, orderByStr);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"SELECT top {0} w1.cusID,cusName,cusAddress,cusAddDate,cusContactor,opeName,C.ctName as TypeName from Customer w1 
LEFT OUTER JOIN Operators B ON w1.cusOperatorID = B.opeID 
LEFT OUTER JOIN defCustomerType C ON w1.cusKindID=C.ctID WHERE (1=1) {1} {2}", count, whereCondition, orderByStr);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerInfoResult> list = new List<CustomerInfoResult>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerInfoResult cus = new CustomerInfoResult();
                    cus.cusID = row["cusID"] + "";
                    cus.cusName = row["cusName"] + "";
                    cus.cusAddress = row["cusAddress"] + "";
                    cus.opeName = row["opeName"] + "";
                    cus.cusContactor = row["cusContactor"] + "";
                    cus.TypeName = row["TypeName"] + "";
                    cus.cusAddDate = DateTime.Parse(row["cusAddDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(cus);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from Customer where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerLinkManResult> GetCustomerLinkManData(int count, int EachCount, string clmTypeID, string keyword, string clmAddOperatorID, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("cusOperatorID", currentOperatorID);
                if (conditionType != "")
                {
                    switch (conditionType)
                    {
                        case "NDayBirthdayLinkMan":
                            string startBirthMonth = DateTime.Now.Month.ToString();
                            string startBirthDay = DateTime.Now.Day.ToString();
                            string endBirthMonth = DateTime.Now.AddDays(int.Parse(conditionValue) - 1).Month.ToString();
                            string endBirthDay = DateTime.Now.AddDays(int.Parse(conditionValue) - 1).Day.ToString();
                            whereCondition += string.Format(" AND month(clmBirthDay) >= '{0}' and day(clmBirthDay) >= '{1}' and month(clmBirthday) <= '{2}' and day(clmBirthday) <='{3}' and clmCustomerID in (select cusID from Customer where cusOperatorID = {4})", startBirthMonth, startBirthDay, endBirthMonth, endBirthDay, currentOperatorID);
                            break;

                    }
                }

                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (clmName like '%{0}%' or clmMobile like '%{0}%' )", keyword);
                }
                if (clmTypeID != "")
                {
                    whereCondition += " AND clmTypeID = " + clmTypeID;
                }
                if (clmAddOperatorID != "")
                {
                    whereCondition += " AND clmAddOperatorID = " + clmAddOperatorID;
                }
                if (cusDateBegin != "")
                {
                    whereCondition += " AND clmAddDate >= '" + cusDateBegin + "'";
                }
                if (cusDateEnd != "")
                {
                    whereCondition += " AND clmAddDate <= '" + cusDateEnd + "'";
                }
                string OrderBy = "";
                if (cusColumn != "")
                {
                    OrderBy = " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    OrderBy = " order by clmAddDate desc";
                }

                string sql = string.Format(@"select A.*,B.Name as TypeName,C.cusName as cusName,D.opeName as opeName from CustomerLinkMan A 
left outer join defCommon B on A.clmTypeID=B.ID
left outer join Customer C on A.clmCustomerID=C.cusID
left outer join Operators D on A.clmAddOperatorID=D.opeID,
(SELECT TOP {0} row_number() OVER ({3}) n, clmID FROM CustomerLinkMan A inner join Customer B on A.clmCustomerID=B.cusID where (1=1) {2}) w2 WHERE A.clmID = w2.clmID AND w2.n >{1} {2}  ORDER BY w2.n ASC", count + EachCount, count, whereCondition, OrderBy);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.Name as TypeName,C.cusName as cusName,D.opeName as opeName from CustomerLinkMan A 
left outer join defCommon B on A.clmTypeID=B.ID
left outer join Customer C on A.clmCustomerID=C.cusID
left outer join Operators D on A.clmAddOperatorID=D.opeID  where 1=1 {1} {2} ", count, whereCondition, OrderBy);

                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerLinkManResult> list = new List<CustomerLinkManResult>();
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerLinkManResult clm = new CustomerLinkManResult();
                    clm.cusID = dr["clmCustomerID"] + "";
                    clm.clmMobile = dr["clmMobile"] + "";
                    clm.clmName = dr["clmName"] + "";
                    clm.cusName = dr["cusName"] + "";
                    clm.TypeName = dr["TypeName"] + "";
                    clm.opeName = dr["opeName"] + "";
                    clm.clmAddDate = dr["clmAddDate"] + "";
                    list.Add(clm);
                }
                if (list.Count > 0)
                {
                    sql = @"select COUNT(*) from CustomerLinkMan A 
left outer join defCommon B on A.clmTypeID=B.ID
left outer join Customer C on A.clmCustomerID=C.cusID
left outer join Operators D on A.clmAddOperatorID=D.opeID where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerOpptunityResult> GetOpptunityData(int count, int EachCount, string coPhaseID, string keyword, string coOpptunitySourceID, string coStatusID, string coDateBegin, string coDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            try
            {
                string whereCondition = "";
                if (conditionType != "")
                {
                    switch (conditionType)
                    {
                        case "NDaySuccessOpptunity":
                            whereCondition += " AND coStatusID = 1 and dateadd(day," + conditionValue + ",coPredictFinishDate) > getdate() and coOperatorID=" + currentOperatorID;
                            break;
                    }
                }

                if (keyword != "")
                {
                    whereCondition += string.Format("AND (cusName like'%{0}%' or coName like '%{0}%' or coPhaseID like '%{0}%' or coPredictAmount like '%{0}%' or coStatusID like '%{0}%' or coOpptunitySourceID like '%{0}%')", keyword);
                }
                if (coPhaseID != "")
                {
                    whereCondition += "AND coPhaseID=" + coPhaseID;
                }
                if (coOpptunitySourceID != "")
                {
                    whereCondition += "AND coOpptunitySourceID=" + coOpptunitySourceID;
                }
                if (coStatusID != "")
                {
                    whereCondition += "AND coStatusID=" + coStatusID;
                }
                if (coDateBegin != "")
                {
                    whereCondition += " AND coAddDate >= '" + coDateBegin + "'";
                }
                if (coDateEnd != "")
                {
                    whereCondition += " AND coAddDate <= '" + coDateEnd + "'";
                }

                string permission = GetPermissionWhereCondition("coOperatorID", currentOperatorID);
                //如果不是全部都可以看到，则加入协同人
                if (!string.IsNullOrEmpty(permission))
                {
                    whereCondition = whereCondition + permission + " OR (1=1 " + whereCondition + " and coID in (select cwRelatedID from CoWorker where cwSource='Clue' and cwOperatorID=" + currentOperatorID + "))";
                }

                string Order = "";
                if (cusColumn != "")
                {
                    Order += " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    Order += " order by coAddDate desc";
                }

                string sql = string.Format(@"select A.*,B.cusName as cusName,C.opeName as coOperatorName
from CustomerOpptunity A 
inner join Customer B on A.coCustomerID=B.cusID 
left outer join Operators C on A.coOperatorID =C.opeID,
(SELECT TOP {0} row_number() OVER ({3}) n, coID FROM CustomerOpptunity where (1=1) {2}) w2 WHERE A.coID = w2.coID AND w2.n > {1} {2} ORDER BY w2.n ASC
", count + EachCount, EachCount, whereCondition, Order);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.cusName as cusName,C.opeName as coOperatorName from CustomerOpptunity A inner join Customer B on A.coCustomerID=B.cusID
 left outer join Operators C on A.coOperatorID=C.opeID
where (1=1) {1} {2} ", count, whereCondition, Order);


                }
                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerOpptunityResult> list = new List<CustomerOpptunityResult>();
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerOpptunityResult co = new CustomerOpptunityResult();
                    co.coID = int.Parse(dr["coID"] + "");
                    co.cusName = dr["cusName"] + "";
                    co.coCustomerID = dr["coCustomerID"] + "";
                    co.coName = dr["coName"] + "";
                    co.coPhaseID = GetPhaseName(dr["coPhaseID"] + "");
                    co.coOpptunitySourceID = GetSourceName(dr["coOpptunitySourceID"] + "");
                    co.coStatusID = GetStatusName(dr["coStatusID"] + "");
                    co.coPredictAmount = dr["coPredictAmount"] + "";
                    co.coAddDate = DateTime.Parse(dr["coAddDate"] + "").ToString("yyyy-MM-dd");
                    co.coOperatorName = dr["coOperatorName"] + "";
                    list.Add(co);
                }

                if (list.Count > 0)
                {
                    sql = "select count(*) from CustomerOpptunity where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerFollowHistoryResult> GetCustomerFollowDate(int count, int EachCount, string cusKindID, string keyword, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("cfhOperatorID", currentOperatorID);
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (cfhContent like '%{0}%')", keyword);
                }
                if (cusKindID != "")
                {
                    whereCondition += " AND cfhTypeID = " + cusKindID;
                }
                if (cusOperator != "")
                {
                    whereCondition += " AND cfhOperatorID = " + cusOperator;
                }
                if (cusDateBegin != "")
                {
                    whereCondition += " AND cfhAddDate >= '" + cusDateBegin + "'";
                }
                if (cusDateEnd != "")
                {
                    whereCondition += " AND cfhAddDate <= '" + cusDateEnd + "'";
                }
                string OrderBy = "";
                if (cusColumn != "")
                {
                    OrderBy = " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    OrderBy = " order by cfhAddDate desc";
                }

                string sql = string.Format(@"select A.*,B.cusName as cusName,B1.ccName, B2.coName, B3.cbName, C.opeName as opeName,ISNULL(E.Name,'系统跟进') as cfhTypeName from CustomerFollowHistory A  
left outer join Customer B on A.cfhRelatedID=B.cusID and A.cfhSource='Customer' 
left outer join CustomerClue B1 on A.cfhRelatedID=B1.ccID and A.cfhSource='Clue' 
left outer join CustomerOpptunity B2 on A.cfhRelatedID=B2.coID and A.cfhSource='Opptunity' 
left outer join CustomerBusiness B3 on A.cfhRelatedID=B3.cbID and A.cfhSource='Business' 
left outer join Operators C on A.cfhOperatorID=C.opeID 
left outer join defCommon E on A.cfhTypeID=E.ID,
(SELECT TOP {0} row_number() OVER ({3}) n, cfhID FROM CustomerFollowHistory A where (1=1) {2}) w2 WHERE A.cfhID = w2.cfhID AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, OrderBy);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.cusName as cusName,B1.ccName, B2.coName, B3.cbName,C.opeName as opeName,ISNULL(E.Name,'系统跟进') as cfhTypeName from CustomerFollowHistory A  
left outer join Customer B on A.cfhRelatedID=B.cusID and A.cfhSource='Customer' 
left outer join CustomerClue B1 on A.cfhRelatedID=B1.ccID and A.cfhSource='Clue' 
left outer join CustomerOpptunity B2 on A.cfhRelatedID=B2.coID and A.cfhSource='Opptunity' 
left outer join CustomerBusiness B3 on A.cfhRelatedID=B3.cbID and A.cfhSource='Business' 
left outer join Operators C on A.cfhOperatorID=C.opeID 
left outer join defCommon E on A.cfhTypeID=E.ID  where 1=1 {1} {2} ", count, whereCondition, OrderBy);

                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerFollowHistoryResult> list = new List<CustomerFollowHistoryResult>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerFollowHistoryResult cfh = new CustomerFollowHistoryResult();
                    cfh.cfhID = row["cfhID"] + "";
                    cfh.opeName = row["opeName"] + "";
                    cfh.cfhTypeName = row["cfhTypeName"] + "";
                    cfh.cfhContent = row["cfhContent"] + "";
                    cfh.cfhFile = GetEachFile(row["cfhFilePath"] + "");
                    cfh.cfhAddress = row["cfhAddress"] + "";
                    cfh.cfhLikeAndComment = GetLikeAndCommentCountHtml(row["cfhID"] + "", row["cfhOperatorID"] + "", currentOperatorID);
                    cfh.cfhAddDate = DateTime.Parse(row["cfhAddDate"] + "").ToString("yyyy-MM-dd");

                    string cfhSource = row["cfhSource"] + "";
                    if (cfhSource == "Customer")
                    {
                        cfh.cfhFromName = "客户-" + row["cusName"];
                    }
                    else if (cfhSource == "Opptunity")
                    {
                        cfh.cfhFromName = "商机-" + row["coName"];
                    }
                    else if (cfhSource == "Business")
                    {
                        cfh.cfhFromName = "合同-" + row["cbName"];
                    }
                    else if (cfhSource == "Clue")
                    {
                        cfh.cfhFromName = "线索-" + row["ccName"];
                    }
                    else
                    {
                        cfh.cfhFromName = "未知";
                    }

                    list.Add(cfh);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from CustomerFollowHistory A where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerBusinessResult> GetCustomerBusinessData(int count, int EachCount, string keyWord, string cbBusinessType, string ddlReceipt, string cbDepartmentID, string cbOperatorID, string cbDateBegin, string cbDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode, string conditionType, string conditionValue)
        {
            try
            {
                string whereCondition = "";
                if (conditionType != "")
                {
                    switch (conditionType)
                    {
                        case "NDayExpireBusiness":
                            whereCondition += " AND cbStatus in (66,67,68) and dateadd(day," + conditionValue + ",getdate()) > cbExpiredDate and cbExpiredDate > getdate() and cbOperatorID=" + CurrentOperatorID;
                            break;
                    }
                }
                if (keyWord != "")
                {
                    whereCondition += string.Format(" AND (cusName like '%{0}%' or cusAddress like '%{0}%' or cusTel like '%{0}%' or cbName like '%{0}%')", keyWord);
                }

                if (cbBusinessType != "")
                {
                    whereCondition += " AND cbBusinessType = " + cbBusinessType;
                }

                if (cbDepartmentID != "")
                {
                    whereCondition += " AND cbDepartmentID = " + cbDepartmentID;
                }

                if (cbOperatorID != "")
                {
                    whereCondition += " AND cbOperatorID = " + cbOperatorID;
                }

                if (ddlReceipt == "已收完款")
                {
                    whereCondition += " AND cbTotalAmount = ISNULL(cbGotAmount,0.0)";
                }
                else if (ddlReceipt == "未收完款")
                {
                    whereCondition += " AND cbTotalAmount > ISNULL(cbGotAmount,0.0)";
                }

                if (cbDateBegin != "")
                {
                    whereCondition += " AND cbDate >= '" + cbDateBegin + "'";
                }

                if (cbDateEnd != "")
                {
                    whereCondition += " AND cbDate <= '" + cbDateEnd + "'";
                }

                string permission = GetPermissionWhereCondition("cbOperatorID", CurrentOperatorID);
                //如果不是全部都可以看到，则加入协同人
                if (!string.IsNullOrEmpty(permission))
                {
                    whereCondition = whereCondition + permission + " OR (1=1 " + whereCondition + " and cbID in (select cwRelatedID from CoWorker where cwSource='Business' and cwOperatorID=" + CurrentOperatorID + "))";
                }

                string OrderBy = " order by cbAddDate Desc";
                if (cusColumn != "")
                {
                    OrderBy = " order by " + cusColumn + " " + orderby + "";
                }

                string sql = string.Format(@"select cusName,cusID,C.Name AS TypeName,opeName,A.*
from CustomerBusiness A 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID,(select top {0} row_number() over ({3}) n,cbID from CustomerBusiness A INNER JOIN Customer B ON A.cbCustomerID = B.cusID  where (1=1) {2}) w2 where A.cbID=w2.cbID and w2.n>{1} {2} order by w2.n ASC", count + EachCount, count, whereCondition, OrderBy);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} cusName,cusID,C.Name AS TypeName,opeName,A.*
from CustomerBusiness A 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID where 1=1 {1} {2} ", count, whereCondition, OrderBy);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerBusinessResult> list = new List<CustomerBusinessResult>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerBusinessResult item = new CustomerBusinessResult();
                    item.cbID = row["cbID"] + "";
                    item.cusID = row["cusID"] + "";
                    item.cusName = row["cusName"] + "";
                    item.cbName = row["cbName"] + "";
                    item.cbTypeName = row["TypeName"] + "";
                    item.cbTotalAmount = row["cbTotalAmount"] + "";
                    item.cbGotAmount = row["cbGotAmount"] + "";
                    item.cbNotGotAmount = GetNumber(row["cbNotGotAmount"] + "");
                    item.cbOperatorID = row["cbOperatorID"] + "";
                    item.cbOperatorName = GetOpeName(row["cbOperatorID"] + "");
                    item.cbStatus = row["cbStatus"] + "";
                    item.cbDate = DateTime.Parse(row["cbDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(item);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from CustomerBusiness A left outer join Customer B on A.cbCustomerID =B.cusID where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<CustomerReceiptResult> GetCustomerReceiptData(int count, int EachCount, string KeyWord, string crDepartmentID, string crOperatorID, string crDateBegin, string crDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("cusOperatorID", CurrentOperatorID);
                string OrderBy = "";
                if (KeyWord != "")
                {
                    whereCondition += string.Format(" AND (cusName like '%{0}%' or cusAddress like '%{0}%' or cusTel like '%{0}%' or cbName like '%{0}%')", KeyWord);
                }

                if (crDepartmentID != "")
                {
                    whereCondition += " AND cbDepartmentID = " + crDepartmentID;
                }

                if (crOperatorID != "")
                {
                    whereCondition += " AND crOperatorID = " + crOperatorID;
                }

                if (crDateBegin != "")
                {
                    whereCondition += " AND crDate >= '" + crDateBegin + "'";
                }

                if (crDateEnd != "")
                {
                    whereCondition += " AND crDate <= '" + crDateEnd + "'";
                }

                if (cusColumn != "")
                {
                    OrderBy = " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    OrderBy = " order by crDate desc";
                }
                string sql = string.Format(@"select
    cusName,cusID,C.Name AS TypeName,opeName,
A.*,K.* from CustomerReceipt K 
LEFT OUTER JOIN CustomerBusiness A ON K.crBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID,
(select top {0} row_number() over ({3}) n,crID from CustomerReceipt A INNER JOIN Customer B ON A.cbCustomerID = B.cusID where (1=1) {2}) w2 where A.crID=w2.crID and w2.n>{1} {2} order by w2.n ASC", count + EachCount, count, whereCondition, OrderBy);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0}
    cusName,cusID,C.Name AS TypeName,opeName,
A.*,K.* from CustomerReceipt K 
LEFT OUTER JOIN CustomerBusiness A ON K.crBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID where 1=1 {1} {2}", count, whereCondition, OrderBy);
                }
                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerReceiptResult> list = new List<CustomerReceiptResult>();
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerReceiptResult item = new CustomerReceiptResult();
                    item.crID = dr["crID"] + "";
                    item.cbCustomerID = dr["cbCustomerID"] + "";
                    item.crBusinessID = dr["crBusinessID"] + "";
                    item.cusName = dr["cusName"] + "";
                    item.cbName = dr["cbName"] + "";
                    item.crAmount = dr["crAmount"] + "";
                    item.crDate = DateTime.Parse(dr["crDate"] + "").ToString("yyyy-MM-dd");
                    item.crOperatorName = GetOpeName(dr["crOperatorID"] + "");
                    list.Add(item);
                }
                if (list.Count > 0)
                {
                    sql = @"select COUNT(*) from CustomerReceipt K 
LEFT OUTER JOIN CustomerBusiness A ON K.crBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<CustomerReceiptPlanResult> GetCustomerReceiptPlanData(int count, int EachCount, string KeyWord, string crpDepartmentID, string crpOperatorID, string crpDateBegin, string crpDateEnd, string CurrentOperatorID, string cusColumn, string orderby, string mode, string crpStatus)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("cusOperatorID", CurrentOperatorID);
                string OrderBy = "";
                if (KeyWord != "")
                {
                    whereCondition += string.Format(" AND (cusName like '%{0}%' or cusAddress like '%{0}%' or cusTel like '%{0}%' or cbName like '%{0}%')", KeyWord);
                }

                if (crpDepartmentID != "")
                {
                    whereCondition += " AND cbDepartmentID = " + crpDepartmentID;
                }

                if (crpOperatorID != "")
                {
                    whereCondition += " AND crpOperatorID = " + crpOperatorID;
                }

                if (crpStatus != "")
                {
                    if (crpStatus == "未完成")
                    {
                        whereCondition += " AND crpStatus in('未收款','部分收款')";
                    }
                    else
                    {
                        whereCondition += " AND crpStatus = '" + crpStatus + "'";
                    }
                }

                if (crpDateBegin != "")
                {
                    whereCondition += " AND crpDate >= '" + crpDateBegin + "'";
                }

                if (crpDateEnd != "")
                {
                    whereCondition += " AND crpDate <= '" + crpDateEnd + "'";
                }

                if (cusColumn != "")
                {
                    OrderBy = " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    OrderBy = " order by crpDate desc";
                }
                string sql = string.Format(@"select
    cusName,cusID,C.Name AS TypeName,opeName,
A.*,K.* from CustomerReceiptPlan K 
LEFT OUTER JOIN CustomerBusiness A ON K.crpBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID,
(select top {0} row_number() over ({3}) n,crpID from CustomerReceiptPlan A INNER JOIN Customer B ON A.cbCustomerID = B.cusID where (1=1) {2}) w2 where A.crpID=w2.crpID and w2.n>{1} {2} order by w2.n ASC", count + EachCount, count, whereCondition, OrderBy);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0}
    cusName,cusID,C.Name AS TypeName,opeName,
A.*,K.* from CustomerReceiptPlan K 
LEFT OUTER JOIN CustomerBusiness A ON K.crpBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID 
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID where 1=1 {1} {2}", count, whereCondition, OrderBy);
                }
                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerReceiptPlanResult> list = new List<CustomerReceiptPlanResult>();
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerReceiptPlanResult item = new CustomerReceiptPlanResult();
                    item.crpID = dr["crpID"] + "";
                    item.cbCustomerID = dr["cbCustomerID"] + "";
                    item.crpBusinessID = dr["crpBusinessID"] + "";
                    item.cusName = dr["cusName"] + "";
                    item.cbName = dr["cbName"] + "";
                    item.crpAmount = dr["crpAmount"] + "";
                    item.crpGotAmount = dr["crpGotAmount"] + "";
                    item.crpStatus = dr["crpStatus"] + "";
                    item.crpDate = DateTime.Parse(dr["crpDate"] + "").ToString("yyyy-MM-dd");
                    item.crpOperatorName = GetOpeName(dr["crpOperatorID"] + "");
                    list.Add(item);
                }
                if (list.Count > 0)
                {
                    sql = @"select COUNT(*) from CustomerReceiptPlan K 
LEFT OUTER JOIN CustomerBusiness A ON K.crpBusinessID = A.cbID 
INNER JOIN Customer B ON A.cbCustomerID = B.cusID 
LEFT OUTER JOIN defCommon C ON A.cbBusinessType = C.ID
LEFT OUTER JOIN Operators D ON A.cbOperatorID = D.opeID where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<WorkDiaryList> GetWorkDiaryDate(int count, int EachCount, string keyword, string wdDateBegin, string wdDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("wdAddOperatorID", currentOperatorID);
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND wdAddOperatorID in (select opeID from Operators where opeName like '%{0}%')", keyword);
                }
                if (wdDateBegin != "")
                {
                    whereCondition += " AND wdAddDate >= '" + wdDateBegin + "'";
                }
                if (wdDateEnd != "")
                {
                    whereCondition += " AND wdAddDate <= '" + wdDateEnd + "'";
                }
                string Order = "";
                if (cusColumn != "")
                {
                    Order += " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    Order += " order by wdAddDate desc";
                }

                string sql = string.Format(@"select A.*,B.opeName as opeName from WorkDiary A 
left outer join Operators B on A.wdAddOperatorID=B.opeID,
(select top {0} row_number() over ({3}) n,wdID from WorkDiary where (1=1) {2}) w2 where A.wdID=w2.wdID and w2.n>{1} {2} order by w2.n ASC", count + EachCount, count, whereCondition, Order);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.opeName as opeName from WorkDiary A 
left outer join Operators B on A.wdAddOperatorID=B.opeID  where (1=1) {1} {2} ", count, whereCondition, Order);

                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<WorkDiaryList> list = new List<WorkDiaryList>();
                foreach (DataRow row in dt.Rows)
                {
                    WorkDiaryList wd = new WorkDiaryList();
                    wd.wdID = row["wdID"] + "";
                    wd.wdAddOperatorName = row["opeName"] + "";
                    wd.wdAddDate = DateTime.Parse(row["wdAddDate"] + "").ToString("yyyy-MM-dd");
                    string Type = "";
                    string t = row["wdTypeID"] + "";
                    switch (t)
                    {
                        case "1":
                            Type = "日报";
                            break;
                        case "2":
                            Type = "周报";
                            break;
                        case "3":
                            Type = "月报";
                            break;
                    }
                    wd.wdType = Type;
                    list.Add(wd);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from WorkDiary  where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<CustomerFeedbackResult> GetCustomerFeedbackDate(int count, int EachCount, string cusKindID, string keyword, string cusOperator, string cusDateBegin, string cusDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = string.Empty;
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (cusName like '%{0}%' or cfbLinkMan like '%{0}%' or cfbOrderRelated like '%{0}%' )", keyword);
                }
                if (cusKindID != "")
                {
                    whereCondition += " AND cfbFeedbackTypeID = " + cusKindID;
                }
                if (cusOperator != "")
                {
                    whereCondition += " AND cfbOperatorID = " + cusOperator;
                }
                if (cusDateBegin != "")
                {
                    whereCondition += " AND cfbAddDate >= '" + cusDateBegin + "'";
                }
                if (cusDateEnd != "")
                {
                    whereCondition += " AND cfbAddDate <= '" + cusDateEnd + "'";
                }

                string Order = "";
                if (cusColumn != "")
                {
                    Order += " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    Order += " order by cfbAddDate desc";
                }


                string sql = string.Format(@"select A.*,B.cusName as cusName,C.Name as TypeName,D.opeName as opeName from CustomerFeedback A 
left Outer join Customer B on A.cfbCustomerID=B.cusID 
left outer join defCommon C on A.cfbFeedbackTypeID =C.ID 
left outer join Operators D on A.cfbAddOperatorID=D.opeID 
(select top {0} row_number() over ({3}) n,cfbID from CustomerFeedback A inner join Customer B on A.cfbCustomerID=B.cusID where (1=1) {2}) w2 where A.cfbID=w2.cfbID and w2.n>{1} {2} order by w2.n ASC
", count + EachCount, count, whereCondition, Order);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.cusName as cusName,C.Name as TypeName,D.opeName as opeName from CustomerFeedback A 
left Outer join Customer B on A.cfbCustomerID=B.cusID 
left outer join defCommon C on A.cfbFeedbackTypeID =C.ID 
left outer join Operators D on A.cfbAddOperatorID=D.opeID where (1=1) {1} {2} ", count, whereCondition, Order);

                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<CustomerFeedbackResult> list = new List<CustomerFeedbackResult>();
                foreach (DataRow row in dt.Rows)
                {
                    CustomerFeedbackResult cfb = new CustomerFeedbackResult();
                    cfb.cfbID = row["cfbID"] + "";
                    cfb.cfbCustomerID = row["cfbCustomerID"] + "";
                    cfb.cusName = row["cusName"] + "";
                    cfb.cfbLinkMan = row["cfbLinkMan"] + "";
                    cfb.opeName = row["opeName"] + "";
                    cfb.cfbOrderRelated = row["cfbOrderRelated"] + "";
                    cfb.TypeName = row["TypeName"] + "";
                    cfb.cfbAddDate = DateTime.Parse(row["cfbAddDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(cfb);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from CustomerFeedback A left outer join Customer B on A.cfbCustomerID =B.cusID where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<KnowList> GetKnowDate(int count, int EachCount, string keyword, string knowOperateDateBegin, string knowOperateDateEnd, string KnowType, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = "";
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND knowTheme like '%{0}%'", keyword);
                }
                if (KnowType != "")
                {
                    whereCondition += " AND knowType = " + KnowType;
                }
                if (knowOperateDateBegin != "")
                {
                    whereCondition += " AND KnowOperateDate >= '" + knowOperateDateBegin + "'";
                }
                if (knowOperateDateEnd != "")
                {
                    whereCondition += " AND KnowOperateDate <= '" + knowOperateDateEnd + "'";
                }

                string Order = "";
                if (cusColumn != "")
                {
                    Order += " order by " + cusColumn + " " + orderby + "";
                }
                else
                {
                    Order += " order by knowOperateDate desc";
                }

                string sql = string.Format(@"select A.*,B.Name as knowTypeName from knowledge A 
left outer join defCommon B on A.knowType=B.ID,
(select top {0} row_number() over ({3}) n,knowID from knowledge where (1=1) {2}) w2 where A.knowID=w2.knowID and w2.n>{1} {2} order by w2.n ASC", count + EachCount, count, whereCondition, Order);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.Name as knowTypeName from knowledge A 
left outer join defCommon B on A.knowType=B.ID where 1=1 {1} {2} ", count, whereCondition, Order);

                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<KnowList> list = new List<KnowList>();
                foreach (DataRow row in dt.Rows)
                {
                    KnowList kl = new KnowList();
                    kl.knowID = row["knowID"] + "";
                    kl.knowTypeName = row["knowTypeName"] + "";
                    kl.knowTheme = row["knowTheme"] + "";
                    kl.knowOperateDate = DateTime.Parse(row["knowOperateDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(kl);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from knowledge  where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<ProjectDetail> GetProjectDate(int count, int EachCount, string pjStatusID, string keyword, string pjOperatorID, string pjAddOperatorID, string pjDateBegin, string pjDateEnd, string pjApproveTag, string currentOperatorID, string column, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("pjOperatorID", currentOperatorID);
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (pjName like '%{0}%' or pjCompanyName like '%{0}%' or pjProduct like '%{0}%' or pjContactor like '%{0}%')", keyword);
                }
                if (pjStatusID != "")
                {
                    whereCondition += " AND pjStatusID = " + pjStatusID;
                }
               
                if (pjOperatorID != "")
                {
                    whereCondition += " AND pjOperatorID = " + pjOperatorID;
                }
                if (pjDateBegin != "")
                {
                    whereCondition += " AND pjAddDate >= '" + pjDateBegin + "'";
                }
                if (pjDateEnd != "")
                {
                    whereCondition += " AND pjAddDate <= '" + pjDateEnd + "'";
                }
                if (pjApproveTag != "")
                {
                    if (pjApproveTag == "已审核")
                    {
                        whereCondition += " AND pjApproveTag = '已审核'";
                    }
                    else
                    {
                        whereCondition += " AND ISNULL(pjApproveTag,'') = ''";
                    }
                }

                string orderByStr = " ORDER BY pjAddDate DESC";
                if (column != "")
                {
                    orderByStr = " order by " + column + " " + orderby + "";
                }

                string sql = string.Format(@"select top {0} A.*,B.opeName as pjAddOperatorName,ISNULL(A.pjApproveTag,'未审核') as pjStatusName from Project A 
left outer join Operators B on A.pjAddOperatorID=B.opeID
left outer join sdefCommon C on A.pjStatusID=C.sID and sTypeName='ProjectStatus',(SELECT TOP {0} row_number() OVER ({3}) n, pjID FROM Project where (1=1) {2}) w2 WHERE A.pjID = w2.pjID AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, orderByStr);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"select top {0} A.*,B.opeName as pjAddOperatorName,ISNULL(A.pjApproveTag,'未审核') as pjStatusName from Project A 
left outer join Operators B on A.pjAddOperatorID=B.opeID
left outer join sdefCommon C on A.pjStatusID=C.sID and sTypeName='ProjectStatus' WHERE (1=1) {1} {2}", count, whereCondition, orderByStr);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<ProjectDetail> list = new List<ProjectDetail>();
                foreach (DataRow row in dt.Rows)
                {
                    ProjectDetail pj = new ProjectDetail();
                    pj.pjID = row["pjID"] + "";
                    pj.pjAddOperatorName = row["pjAddOperatorName"] + "";
                    pj.pjCompanyName = row["pjCompanyName"] + "";
                    pj.pjName = row["pjName"] + "";
                    pj.pjProduct = row["pjProduct"] + "";
                    pj.pjStatusName = row["pjStatusName"] + "";
                    pj.pjAddDate = DateTime.Parse(row["pjAddDate"] + "").ToString("yyyy-MM-dd");
                    list.Add(pj);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from Project where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IList<MarketingActivityDate> GetMarketingActivityDate(int count, int EachCount, string maTypeID, string keyword, string maStatusID, string maDepartmentID, string maOperatorID, string maDateBegin, string maDateEnd, string currentOperatorID, string cusColumn, string orderby, string mode)
        {
            try
            {
                string whereCondition = GetPermissionWhereCondition("maOperatorID", currentOperatorID);
                if (keyword != "")
                {
                    whereCondition += string.Format(" AND (maName like '%{0}%' or maDescription like '%{0}%' or maRemark like '%{0}%')", keyword);
                }
                if (maStatusID != "")
                {
                    whereCondition += " AND maStatusID = " + maStatusID;
                }
                if (maDepartmentID != "")
                {
                    whereCondition += " AND maDepartmentID = " + maDepartmentID;
                }
                if (maOperatorID != "")
                {
                    whereCondition += " AND maOperatorID = " + maOperatorID;
                }
                if (maDateBegin != "")
                {
                    whereCondition += " AND maAddDate >= '" + maDateBegin + "'";
                }
                if (maDateEnd != "")
                {
                    whereCondition += " AND maAddDate <= '" + maDateEnd + "'";
                }

                string orderByStr = " ORDER BY maID DESC";
                if (cusColumn != "")
                {
                    orderByStr = " order by " + cusColumn + " " + orderby + "";
                }

                string sql = string.Format(@"SELECT top {0} w1.*,B.opeName as opeName,C.sName as StatusName,D.Name as TypeName from MarketingActivity w1 
INNER JOIN Operators B ON w1.maOperatorID = B.opeID 
LEFT OUTER JOIN sdefCommon C ON w1.maStatusID=C.sID and sTypeName='defMarketingActivityStatus'
left outer join defCommon D on w1.maTypeID=D.ID,(SELECT TOP {0} row_number() OVER ({3}) n, maID FROM MarketingActivity where (1=1) {2}) w2 WHERE w1.maID = w2.maID AND w2.n > {1} {2} ORDER BY w2.n ASC", count + EachCount, count, whereCondition, orderByStr);

                if (mode == "Search" || mode == "FirstLoad")
                {
                    sql = string.Format(@"SELECT top {0} w1.*,B.opeName as opeName,C.sName as StatusName,D.Name as TypeName from MarketingActivity w1 
INNER JOIN Operators B ON w1.maOperatorID = B.opeID 
LEFT OUTER JOIN sdefCommon C ON w1.maStatusID=C.sID and sTypeName='defMarketingActivityStatus'
left outer join defCommon D on w1.maTypeID=D.ID WHERE (1=1) {1} {2}", count, whereCondition, orderByStr);
                }

                DataTable dt = DbHelperSQL.GetDataTable(sql);
                List<MarketingActivityDate> list = new List<MarketingActivityDate>();
                foreach (DataRow row in dt.Rows)
                {
                    MarketingActivityDate ma = new MarketingActivityDate();
                    ma.maAddDate = DateTime.Parse(row["maAddDate"] + "").ToString("yyyy-MM-dd HH:mm:ss");
                    ma.maDescription = row["maDescription"] + "";
                    ma.maID = row["maID"] + "";
                    ma.maName = row["maName"] + "";
                    ma.maTypeName = row["TypeName"] + "";
                    ma.opeName = row["opeName"] + "";
                    ma.maDate = DateTime.Parse(row["maStartDate"]+"").ToString("yyyy-MM-dd HH:mm:ss") + "-" + DateTime.Parse(row["maEndDate"] + "").ToString("yyyy-MM-dd HH:mm:ss");
                    list.Add(ma);
                }
                if (list.Count > 0)
                {
                    sql = "select count(*) from MarketingActivity where (1=1) " + whereCondition;
                    list[0].SumCount = DbHelperSQL.GetSHSLInt(sql);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetEachFile(string filePath)
        {
            if (filePath != "")
            {
                if (filePath.Contains("|"))
                {
                    filePath = filePath.Substring(0, filePath.Length - 1);
                }
                string cfp = "";
                string path = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/CustomerFile/";
                string path2 = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "@images/NewDocImg/";
                string[] fp = filePath.Split('|');
                foreach (string cfpFilePath in fp)
                {
                    if (cfpFilePath != "")
                    {
                        string[] items = cfpFilePath.Split('.');
                        if (items.Length == 2)
                        {
                            if (IsValidImage(items[1]))
                            {
                                cfp += "<a target=\"_blank\" onclick=\"javascript:showPictures('" + filePath + "','" + path + cfpFilePath + "','" + path + "');\">";
                                cfp += "<img class=\"picView\" src=\"" + path + "small" + cfpFilePath + "\" /></a>";
                            }
                            else
                            {
                                cfp += "<a target=\"_blank\" data-role='none' href='" + path + cfpFilePath + "'>";
                                cfp += "<img class=\"picView\" src=\"" + path2 + items[1] + ".gif" + "\" /></a>";
                            }
                        }
                    }
                }
                return cfp;
            }
            else
            {
                return "";
            }

        }

        public static string GetEachFileForWeb(string filePath)
        {
            if (filePath != "")
            {
                if (filePath.Contains("|"))
                {
                    filePath = filePath.Substring(0, filePath.Length - 1);
                }
                string cfp = "";
                string path = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/CustomerFile/";
                string path2 = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "@images/NewDocImg/";
                string[] fp = filePath.Split('|');
                foreach (string cfpFilePath in fp)
                {
                    string[] items = cfpFilePath.Split('.');
                    if (items.Length == 2)
                    {
                        if (IsValidImage(items[1]))
                        {
                            cfp += "<a target=\"_blank\" data-role='none' href='" + path + cfpFilePath + "'>";
                            cfp += "<img class=\"picView\" src=\"" + path + "small" + cfpFilePath + "\" /></a>";
                        }
                        else
                        {
                            cfp += "<a target=\"_blank\" data-role='none' href='" + path + cfpFilePath + "'>";
                            cfp += "<img class=\"picView\" src=\"" + path2 + items[1] + ".gif" + "\" /></a>";
                        }
                    }
                }
                return cfp;
            }
            else
            {
                return "";
            }
        }

        public static string GetSuccessFile(string filePath)
        {
            if (filePath != "")
            {
                if (filePath.Contains("|"))
                {
                    filePath = filePath.Substring(0, filePath.Length - 1);
                }
                string cfp = "";
                string path = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/CustomerFile/";
                string path2 = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "@images/NewDocImg/";
                string[] fp = filePath.Split('|');
                foreach (string cfpFilePath in fp)
                {
                    if (cfpFilePath != "")
                    {
                        string[] items = cfpFilePath.Split('.');
                        if (items.Length == 2)
                        {
                            if (IsValidImage(items[1]))
                            {
                                cfp += "<div style='float:left;' id='pic" + items[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + items[0] + "','" + items[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src='" + path + cfpFilePath + "' /></div></div>";
                                //cfp += "<a target=\"_blank\" data-role='none' href='" + path + cfpFilePath + "'>";
                                //cfp += "<img class=\"picView\" src=\"" + path + "small" + cfpFilePath + "\" /></a>";
                            }
                            else
                            {
                                cfp += "<div style='float:left;' id='pic" + items[0] + "'><div style='position: relative;top: -10px;left: 50px;width: 20px;height: 0px;' onclick=javascript:DelectFile('" + items[0] + "','" + items[1] + "') ><img style='width:20px;height:20px;' src='../@images/img_close.gif' /></div><div><img style='width:60px;height:60px;margin-right:5px;' src=\"" + path2 + items[1] + ".gif" + "\"  /></div></div>";
                                //cfp += "<a target=\"_blank\" data-role='none' href='" + path + cfpFilePath + "'>";
                                //cfp += "<img class=\"picView\" src=\"" + path2 + items[1] + ".gif" + "\" /></a>";
                            }
                        }
                    }
                }
                return cfp;
            }
            else
            {
                return "";
            }

        }

        public static string GetCommentCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 1 and bcRelatedID = " + cfhID);
        }

        public static string GetLikeCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = " + cfhID);
        }

        public static string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID, string CurrentOperatorID)
        {
            string likeCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + CurrentOperatorID);
            string html = "";
            string contentCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + CurrentOperatorID);
            string cssLikeName = "cssNotLike";
            string cssContentName = "cssNotLike";
            string imageLike = "../@images/like.png";
            string imageContent = "../@images/content.png";

            if (int.Parse(likeCount) > 0)
            {
                cssLikeName = "cssLike";
                imageLike = "../@images/likeClick.png";
            }
            if (int.Parse(contentCount) > 0)
            {
                cssContentName = "cssLike";
                imageContent = "../@images/HasContent.png";
            }
            html = "<div style='width:50%; float:left; text-align:center'  class='" + cssContentName + "' onclick=\"javascript:GotoFollowHistory(" + cfhID + ",'HistoryList');\">";
            html += "<img src=\"" + imageContent + "\" style=\"width:18px;\" id=\"imgContent" + cfhID + "\"/>";
            html += " <label style='cursor:pointer' id=\"lblCommentCount" + cfhID + "\">评论(" + GetCommentCount(cfhID) + ")</label>";
            html += "</div><div onclick=\"javascript:handleClickLike(" + cfhID + "," + cfhOperatorID + ");\" id=\"divLike" + cfhID + "\" class='" + cssLikeName + "' style=\" width:50%; float:left; text-align:center\">";
            html += "<img src=\"" + imageLike + "\" style=\" width:18px;\" id='imgLike" + cfhID + "'/>";
            html += "<label style='cursor:pointer' id=\"lblLikeCount" + cfhID + "\">点赞(" + GetLikeCount(cfhID) + ")</label>";
            html += "</div>";
            return html;
        }
        #endregion

        #region document
        public static string SaveDocument(string cfNane,string cfCN,string cfUserID,string cfCustomerID)
        {
            try
            {
                string id= DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFile'");
                DbHelperSQL.ExecuteSQL(@"Insert into CustomerFile (cfID,cfName,cfCN,cfCustomerID,cfUserID,cfUploadTime,cfLength,cfBusinessID) 
values ('" + id + "','" + cfNane + "','" + cfCN + "','" + cfCustomerID + "','" + cfUserID + "',getdate(),0,0)");
                return id;
            }
            catch (Exception ex) 
            {
                return "0";
            }
        }

        public static string DeleteDocument(int ID,int CurrentOperatorID) 
        {
            try
            {
                string cusID = DbHelperSQL.GetSHSL("select cfCustomerID from CustomerFile where cfID = " + ID);
                if (CheckPermissionForCustomer(cusID, CurrentOperatorID.ToString()))
                {
                    DbHelperSQL.ExecuteSQL("Delete from CustomerFile where cfID=" + ID);
                    return ID.ToString();
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex) 
            {
                return "-1";
            }
        }
        #endregion

        #region MarketingActivity
        public static string AddMarketingActivityInfo(string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID) 
        {
            try
            {
                if (maTypeID == "")
                    maTypeID = "0";
                if (maPredictNum == "")
                    maPredictNum = "0";
                if (maActualNum == "")
                    maActualNum = "0";
                if (maPredictCost == "")
                    maPredictCost = "0";
                if (maPredictAmount == "")
                    maPredictAmount = "0";
                if (maActualCost == "")
                    maActualCost = "0";
                string maID= DbHelperSQL.GetSHSLInt("usp_GetID 'MarketingActivity'");
                DbHelperSQL.ExecuteSQL(@"Insert into MarketingActivity 
(maID,maName,maStartDate,maEndDate,maTypeID,maDescription,maPredictCost,maPredictAmount,maActualCost,maPredictNum,maActualNum,maRemark,maDepartmentID,maOperatorID,maAddOperatorID,maAddDate) values 
('" + maID + "','" + maName + "','" + maStartDate + "','" + maEndDate + "','" + maTypeID + "','" + maDescription + "','" + maPredictCost + "','" + maPredictAmount + "','" + maActualCost + "','" + maPredictNum + "','" + maActualNum + "','" + maRemark + "','" + maDepartmentID + "','" + maOperatorID + "','" + CurrentOperatorID + "',getdate())");
                return maID;
            }
            catch (Exception ex) 
            {
                return "0";
            }
        }

        public static string SaveMarketingActivity(string maID, string maName, string maStartDate, string maEndDate, string maTypeID, string maDescription, string maPredictCost, string maPredictAmount, string maActualCost, string maPredictNum, string maActualNum, string maRemark, string maDepartmentID, string maOperatorID, string CurrentOperatorID) 
        {
            try
            {
                if (maTypeID == "")
                    maTypeID = "0";
                if (maPredictNum == "")
                    maPredictNum = "0";
                if (maActualNum == "")
                    maActualNum = "0";
                DbHelperSQL.ExecuteSQL(@"Update MarketingActivity set maName='" + maName + "',maStartDate='" + maStartDate + @"',
maEndDate='" + maEndDate + "',maTypeID='" + maTypeID + "',maDescription='" + maDescription + "',maPredictCost='" + maPredictCost + @"',
maPredictAmount='" + maPredictAmount + "',maActualCost='" + maActualCost + "',maPredictNum='" + maPredictNum + @"',
maActualNum='" + maActualNum + "',maRemark='" + maRemark + "',maDepartmentID='" + maDepartmentID + "',maOperatorID='" + maOperatorID + @"',
maModifyDate=getDate(),maModifyOperatorID='" + CurrentOperatorID + "'");
                return maID;
            }
            catch (Exception ex) 
            {
                return "0";
            }
        }
        
        #endregion
    }

    public class sysMessageResult
    {
        public string MessageID;
        public string SendTime;
        public string MessageContent;
        public string Title;
        public string SendOperatorID;
        public string MessageReaded;
        public string MessageUnread;
    }

    public class CoWorkerList
    {
        public string opeDeprtment;
        public string cwID;
    }

    public class CustomerLinkManResult
    {
        public int clmID;
        public string clmName;
        public string clmSex;
        public string clmTel;
        public string clmMobile;
        public string clmEmail;
        public string clmQQ;
        public string clmWeChat;
        public string clmRemark;
        public string TypeName;
        public string opeName;
        public string cusID;
        public string cusName;
        public string clmAddDate;
        public string SumCount;
    }

    public class CustomerOpptunityResult
    {
        public string cusName;

        public int coID;
        public string coDate;
        public string coName;
        public string coPhaseID;
        public string coPhaseName;
        public string coPredictAmount;
        public string coPredictFinishDate;
        public string coStatusID;
        public string coStatusName;
        public string coOpptunitySourceID;
        public string coOpptunitySourceName;
        public string coOperatorID;
        public string coOperatorName;
        public string coCustomerID;
        public string coCustomerName;
        public string coAddDate;
        public string ProdictAmount;
        public string TrueAmount;
        public string coSumQuantity;
        public string SumCount;
    }

    public class AchievementReportResult
    {
        public string Title;
        public string Amount;
    }

    public class CustomerFollowHistoryResult
    {
        public string cfhID;
        public string cfhRelatedID;
        public string cusName;
        public string cfhTitle;
        public string cfhTypeName;
        public string clmName;
        public string opeName;
        public string cfhAddDate;
        public string SumQuantity;
        public string cfhContent;
        public string cfhFile;
        public string cfhAddress;
        public string cfhLikeAndComment;
        public string cfhFromName;

        public string SumCount;
    }

    public class CustomerFollowPlanResult
    {
        public int cfpID;
        public string cfpContent;
        public string cfpOperatorID;
        public string cfpOperatorName;
        public string cfpFilePath;
        public string cfpDate;
    }

    public class CustomerBusinessResult
    {
        public string cbID;
        public string cbName;
        public string cbTotalAmount;
        public string cbGotAmount;
        public string cbNotGotAmount;
        public string cbOperatorID;
        public string cbOperatorName;
        public string cbDate;
        public string cbNotifyOperatorID;
        public string cbNotifyOperatorName;

        public string cbStatus;
        public string cbDepartmentName;
        public string cbTypeName;
        public string cbBusinessType;
        public string cusID;
        public string cusName;

        public string cbExpiredDate;
        public string cbRemark;

        public string SumTotalAmount;
        public string SumGotAmount;
        public string SumNotGotAmount;
        public string SumQuantity;

        public string SumCount;

    }

    public class CustomerReceiptResult
    {
        public string crID;
        public string crBusinessID;
        public string crBusinessName;
        public string crAmount;
        public string crOperatorID;
        public string crOperatorName;
        public string crDate;
        public string crTypeID;
        public string crBatchID;
        public string crRemark;

        public string cbCustomerID;
        public string cusName;
        public string cbName;

        public string SumTotalAmount;
        public string SumQuantity;

        public string SumCount;
    }

    public class CustomerReceiptPlanResult
    {
        public string crpID;
        public string crpBusinessID;
        public string crpBusinessName;
        public string crpAmount;
        public string crpOperatorID;
        public string crpOperatorName;
        public string crpDate;
        public string crpGotAmount;
        public string crpStatus;
        public string crpTypeID;
        public string crpBatchID;
        public string crpRemark;

        public string cbCustomerID;
        public string cusName;
        public string cbName;

        public string SumTotalAmount;
        public string SumQuantity;
        public string SumCount;
    }

    public class CustomerInfo
    {
        public string cusID;
        public string cusName;
        public string cusAddress;
        public string cusLongtitude;
        public string cusLatitude;
        public string cusContactor;
        public string cusTel;
    }

    public class CustomerInfoResult
    {
        public string cusID;
        public string cusName;
        public string cusAddress;
        public string cusContactor;
        public string opeName;
        public string TypeName;
        public string cusAddDate;
        public string cusSumQuantity;
        public string SumCount;
    }

    public class CustomerFeedbackResult
    {
        public string cfbID;
        public string cfbCustomerID;
        public string cusName;
        public string cfbOrderRelated;
        public string TypeName;
        public string cfbLinkMan;
        public string opeName;
        public string cfbAddDate;
        public string SumQuantity;

        public string SumCount;
    }

    public class WorkDiaryList
    {
        public string wdID;
        public string wdDate;
        public string wdAddDate;
        public string wdAddOperatorID;
        public string wdAddOperatorName;
        public string SumQuantity;
        public string wdType;

        public string SumCount;
    }

    public class FeedbackList
    {
        public string cfbID;
        public string cfbFeedbackTypeID;
        public string cfbTypeName;
        public string cfbOrderRelated;
        public string cfbContent;
        public string cfbLinkman;
        public string cfbTelephone;
        public string cfbEmail;
        public string cfbStatusID;
        public string cfbStatus;
        public string cfbResult;
        public string cfbHandleOperatorID;
        public string cfbHandleOperator;
    }

    public class KnowList
    {
        public string knowTheme;
        public string knowTypeName;
        public string knowOperateDate;
        public string knowID;
        public string SumQuantity;

        public string SumCount;
    }

    public class WorkbenchList
    {
        //荣誉榜
        public string MaxReceiptAmountOperatorName;
        public string MaxReceiptAmount;
        public string MaxReceiptAmountOperatorFace;

        public string MaxBusinessAmountOperatorName;
        public string MaxBusinessAmount;
        public string MaxBusinessAmountOperatorFace;

        public string MaxOpptunityAmountOperatorName;
        public string MaxOpptunityAmount;
        public string MaxOpptunityAmountOperatorFace;

        public string MaxCustomerCountOperatorName;
        public string MaxCustomerCount;
        public string MaxCustomerCountOperatorFace;

        public string MaxFollowHistoryCountOperatorName;
        public string MaxFollowHistoryCount;
        public string MaxFollowHistoryCountOperatorFace;

        //实际业绩
        public string SumOpptunityAmount;
        public string SumBusinessAmount;
        public string SumReceiptAmount;

        //目标
        public string SumBusinessTargetAmount;
        public string SumReceiptTargetAmount;

        //绩效
        public string SumAddNewFollowCount;
        public string SumAddNewReceiptCount;
        public string SumAddNewCustomerCount;
        public string SumAddNewOpptunityCount;
        public string SumAddNewBusinessCount;
        public string SumAddNewClueCount;

        //销售漏斗数据
        //初期沟通
        public string Phase6SumCount = "0";
        public string Phase6SumAmount = "0.00";
        public string Phase6SumPredictAmount = "0.00";
        //立项评估
        public string Phase4SumCount = "0";
        public string Phase4SumAmount = "0.00";
        public string Phase4SumPredictAmount = "0.00";
        //需求调研
        public string Phase5SumCount = "0";
        public string Phase5SumAmount = "0.00";
        public string Phase5SumPredictAmount = "0.00";
        //方案论证
        public string Phase2SumCount = "0";
        public string Phase2SumAmount = "0.00";
        public string Phase2SumPredictAmount = "0.00";
        //商务谈判
        public string Phase3SumCount = "0";
        public string Phase3SumAmount = "0.00";
        public string Phase3SumPredictAmount = "0.00";
        //签订合同
        public string Phase7SumCount = "0";
        public string Phase7SumAmount = "0.00";
        public string Phase7SumPredictAmount = "0.00";
    }

    public class TempTable
    {
        public string hfCurrentOperatorName;
        public string cfpID;
        public string cfpFollowTypeName;
        public string cfhID;

        public string bID;
        public string getAmount;
    }

    public class CustomerCLueInfo
    {
        public string ccID;
        public string ccStatus;
        public string ccCustomerName;
        public string ccName;
        public string opeName;
        public string ccAddDate;
        public string ccAddress;
        public string SumCount;
        public string ccMobile;
        public string ccTel;
        public string ccRemark;
        public string ccOperatorID;
        public string ccDepartmentID;
    }

    public class ClueDynamic
    {
        public string cfhContent;
        public string cfhFilePath;
        public string cfhTypeName;
        public string cfhTypeID;
        public string cfhDate;
        public string cfhNextWarnTime;
    }

    public class CustomerCheck
    {
        public string Name;
        public string OperatorName;
        public string DepartmentName;
        public string AddDate;
        public string SourceName;
    }

    public class CustomerFollowHistiryDetailData
    {
        public string cfhOperatorName;
        public string cfhAddDate;
        public string cfhContent;
        public string cfhAddress;
        public string cfhFilePath;
        public string cfhTypeName;
        public string cfhLikeAndComment;
        public string cfhBillComment;
        public string cfhOperatorID;
        public string litLikePersons;
    }

    public class ProjectDetail 
    {
        public string pjID;
        public string pjName;
        public string pjCompanyName;
        public string pjAddOperatorName;
        public string pjAddDate;
        public string pjStatusName;
        public string pjProduct;
        public string SumCount;
    }

    public class MarketingActivityDate 
    {
        public string maID;
        public string maTypeName;
        public string maName;
        public string maDescription;
        public string opeName;
        public string maAddDate;
        public string maDate;
        public string SumCount;
    }

}
