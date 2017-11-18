namespace SmartSoft.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Collections;

    using Castle.Facilities.AutomaticTransactionManagement;
    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;

    using SmartSoft.Domain.Data;
    using SmartSoft.Component;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Persistence.Data;
    using SmartSoft.Service;
    using SmartSoft.Domain.Common;
    using System.Data;
    using System.IO;
    using System.Diagnostics;

    [Transactional]
    [UsesAutomaticSessionCreation]
    public class DataController : CommonController
    {
        #region Customer
        public readonly string AreaColumn = "areaid";
        public IList<Customer> SelectCustomerByAssistantID(int assistantID, int? employeeID)
        {
            return baseservice.SelectDynamic<Customer>(string.Format(" AND (empid <> {1} OR empid is null) AND cusID in (select carCustomerID from CustomerAssistantRelation where carAssistantID = {0})", assistantID, (employeeID.HasValue ? employeeID.Value.ToString() : "-1")), "cusName asc");
        }


        [Transaction(TransactionMode.Requires)]
        public virtual int SaveCustomer(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID, int? employeeID, ref string message)
        {
            if (action == FormAction.Update) //修改
            {
                
                Customer entity = baseservice.GetDetail<Customer>(rowID);
              
                entity.cusModifyDate = DateTime.Now;
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);

                bool isExist = DbHelperSQL.Exists("select * from Customer where cusID != " + rowID + " and cusName = '" + entity.cusName + "'");
                if (isExist)
                {
                    message = "相同客户已经存在，请检查后重试！";
                    rowID = -1;
                }
                else
                {
                    //增加系统字段的值
                    baseservice.Update<Customer>(entity);
                }
            }
            else if (action == FormAction.Insert) //增加
            {
                Customer entity = new Customer();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);
                if (string.IsNullOrEmpty(entity.cusCN))
                {
                    string cusCN = DbHelperSQL.GetSHSL("select top 1 cusCN from Customer where cusCN like '" + DateTime.Now.ToString("yyyyMMdd") + "[0-9][0-9][0-9]' order by cusCN desc");
                    if (string.IsNullOrEmpty(cusCN))
                    {
                        entity.cusCN = DateTime.Now.ToString("yyyyMMdd") + "001";
                    }
                    else
                    {
                        int maxCN = int.Parse(cusCN.Substring(8));
                        entity.cusCN = DateTime.Now.ToString("yyyyMMdd") + (maxCN + 1).ToString("D3");
                    }
                }
                entity.cusAddOperatorID = operatorID;
                entity.cusAddDate = DateTime.Now;
                entity.cusModifyOperatorID = operatorID;
                entity.cusModifyDate = DateTime.Now;

                string sql = "select * from Customer where cusName = '" + entity.cusName + "'";

                bool isExist = DbHelperSQL.Exists(sql);
                if (isExist)
                {
                    message = "相同客户已经存在，请检查后重试！";
                    rowID = -1;
                }
                else
                {
                    string protectDays = DbHelperSQL.GetSHSL("select ctProtectDays from defCustomerType where ctID = " + entity.cusKindID);
                    if (protectDays != "" && StringHelper.IsValidDBInt(protectDays))
                    {
                        entity.cusExpiredDate = DateTime.Now.AddDays(int.Parse(protectDays));
                    }
                    rowID = baseservice.Add<Customer>(entity);
                }
            }
            return rowID;
        }


        public DataTable SelectRecentReturnBillForCustomer(int cusid)
        {
            return baseservice.ExecuteQueryForDataTable("SelectRecentReturnBillForCustomer", cusid);
        }
        #endregion

        #region CustomerFollowHistory
        [Transaction(TransactionMode.Requires)]
        public virtual void SaveCustomerFollowHistory(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID)
        {
            CustomerFollowHistory entity = null;
            if (action == FormAction.Update) //修改
            {
                entity = baseservice.GetDetail<CustomerFollowHistory>(rowID);
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);
                //entity.cfhOperatorID = operatorID;
                entity.cfhModifyOperatorID = operatorID;
                entity.cfhModifyDate = DateTime.Now;

                //增加系统字段的值
                baseservice.Update<CustomerFollowHistory>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                entity = new CustomerFollowHistory();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);

                entity.cfhAddOperatorID = operatorID;
                entity.cfhAddDate = DateTime.Now;
                //entity.cfhOperatorID = operatorID;
                entity.cfhModifyDate = DateTime.Now;

                //增加系统字段的值
                rowID = baseservice.Add<CustomerFollowHistory>(entity);
                entity.cfhID = rowID;
                
                //Add messages
                //string title = "[" + baseservice.GetDetail<Customer>(entity.cfhCustomerID).cusName + "] 跟进信息";
                string title = "[" + DbHelperSQL.GetSHSL("select cusName from Customer where cusID=" + entity.cfhRelatedID) + "] 跟进信息";
                string url = "Data/CustomerFollowHistoryEditForm.aspx?Action=View&ID=" + entity.cfhID;
                DeleteMessageByURL(url);
                AddMessages(operatorID, title, entity.cfhContent, url);
            }
        }
        #endregion

        #region CustomerFollowPlan
        [Transaction(TransactionMode.Requires)]
        public virtual void SaveCustomerFollowPlan(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID)
        {
            SmartSoft.Domain.Data.CustomerFollowPlan entity = null;
            if (action == FormAction.Update) //修改
            {
                entity = baseservice.GetDetail<SmartSoft.Domain.Data.CustomerFollowPlan>(rowID);
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);
                entity.cfpOperatorID = operatorID;
                entity.cfpModifyOperatorID = operatorID;
                entity.cfpModifyDate = DateTime.Now;

                //增加系统字段的值
                baseservice.Update<SmartSoft.Domain.Data.CustomerFollowPlan>(entity);

                string title = "跟进提醒";
                string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + entity.cfpID;
                DeleteMessageByURL(url);
                AddMessages(entity.cfpOperatorID.Value, title, entity.cfpContent, url, entity.cfpDate.Value);
              
            }
            else if (action == FormAction.Insert) //增加
            {
                entity = new SmartSoft.Domain.Data.CustomerFollowPlan();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);

                entity.cfpAddOperatorID = operatorID;
                entity.cfpAddDate = DateTime.Now;
                entity.cfpModifyDate = DateTime.Now;
                entity.cfpOperatorID = operatorID;

                //增加系统字段的值
                rowID = baseservice.Add<SmartSoft.Domain.Data.CustomerFollowPlan>(entity);
                entity.cfpID = rowID;
                //Add messages
                string title = "跟进计划";
                string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + entity.cfpID;
                DeleteMessageByURL(url);
                AddMessages(entity.cfpOperatorID.Value, title, entity.cfpContent, url, entity.cfpDate.Value);
                
            }

        }

        public virtual void ConfirmComplete(int rowID, int operatorID, DateTime nextDate)
        {
            CustomerFollowPlan entity = null;
            entity = baseservice.GetDetail<CustomerFollowPlan>(rowID);

            entity.cfpAddOperatorID = operatorID;
            entity.cfpAddDate = DateTime.Now;
            entity.cfpModifyDate = DateTime.Now;
            entity.cfpOperatorID = operatorID;
            entity.cfpDate = nextDate;

            //增加系统字段的值
            rowID = baseservice.Add<CustomerFollowPlan>(entity);
            entity.cfpID = rowID;
            //Add messages
            string title = "跟进提醒";
            string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + entity.cfpID;
            DeleteMessageByURL(url);
            url += "&FollowType=16";
            AddMessages(operatorID, title, entity.cfpContent, url, entity.cfpDate.Value);

        }
        #endregion

        #region CustomerLinkMan
        [Transaction(TransactionMode.Requires)]
        public virtual void SaveCustomerLinkMan(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID)
        {
            CustomerLinkMan entity = null;
            if (action == FormAction.Update) //修改
            {
                entity = baseservice.GetDetail<CustomerLinkMan>(rowID);
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);
                entity.clmModifyOperatorID = operatorID;
                entity.clmModifyDate = DateTime.Now;

                //增加系统字段的值
                baseservice.Update<CustomerLinkMan>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                entity = new CustomerLinkMan();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);

                entity.clmAddOperatorID = operatorID;
                entity.clmAddDate = DateTime.Now;
 
                entity.clmModifyDate = DateTime.Now;

                //增加系统字段的值
                rowID = baseservice.Add<CustomerLinkMan>(entity);
                entity.clmID = rowID;
            }

        }
        #endregion

        #region CustomerFeedback
        [Transaction(TransactionMode.Requires)]
        public virtual void SaveCustomerFeedback(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID)
        {
            CustomerFeedback entity = null;
            if (action == FormAction.Update) //修改
            {
                entity = baseservice.GetDetail<CustomerFeedback>(rowID);
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);
                entity.cfbOperatorID = operatorID;
                entity.cfbModifyOperatorID = operatorID;
                entity.cfbModifyDate = DateTime.Now;

                //增加系统字段的值
                baseservice.Update<CustomerFeedback>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                entity = new CustomerFeedback();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);

                entity.cfbAddOperatorID = operatorID;
                entity.cfbAddDate = DateTime.Now;
                entity.cfbModifyOperatorID = operatorID;
                entity.cfbModifyDate = DateTime.Now;
                entity.cfbOperatorID = operatorID;
                //增加系统字段的值
                rowID = baseservice.Add<CustomerFeedback>(entity);


                //Add messages
                string title = "[" + baseservice.GetDetail<Customer>(entity.cfbCustomerID).cusName + "] 反馈信息";
                string url = "Data/CustomerFeedbackEditForm.aspx?Action=View&ID=" + rowID;
                DeleteMessageByURL(url);
                AddMessages(operatorID, title, entity.cfbContent, url);
            }
            //处理自定义字段的值
            //string updateSql = userdefinebase.GetCustomValueStringFromPage(ctrl, layoutID, rowID);
            //userdefinebase.UpdateCustomValue(updateSql);

        }
        #endregion


        #region Knowledge
        [Transaction(TransactionMode.Requires)]
        public virtual void SaveKnowledge(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID)
        {
            Knowledge entity = null;
            if (action == FormAction.Update) //修改
            {
                entity = baseservice.GetDetail<Knowledge>(rowID);
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);
                entity.knowOperator = operatorID;
                entity.knowOperateDate = DateTime.Now;

                //增加系统字段的值
                baseservice.Update<Knowledge>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                entity = new Knowledge();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);
                entity.knowOperateDate = DateTime.Now;
                entity.knowOperator = operatorID;
                //增加系统字段的值
                baseservice.Add<Knowledge>(entity);
            }
            
        }



        public string Import(string fullPath, int operatorid, string tableName, string prefix)
        {
            try
            {
                string msg = string.Empty;
                DataTable dt = new tpExcel().ExcelToDT(fullPath);
                string tableID = DbHelperSQL.GetSHSLInt("select TableID from udefSystemTable where TableName='" + tableName + "'");
                IList<SystemColumn> listColumn = baseservice.SelectDynamic<SystemColumn>(" AND TableID = " + tableID);
                IList<Customer> listCustomer = new List<Customer>();
                IDictionary<string, string> htColumnTextToColumnName = new Dictionary<string, string>();
                IDictionary<string, SystemColumn> htColumnTextToSystemColumn = new Dictionary<string, SystemColumn>();
                IDictionary<string, string> htColumnNameToColumnText = new Dictionary<string, string>();
                IDictionary<string, int> htColumnTextToCellIndex = new Dictionary<string, int>();
                IDictionary<int, string> htCellIndexToColumnName = new Dictionary<int, string>();
                Dictionary<string, int> htDataContentToDataType = new Dictionary<string, int>();
                foreach (SystemColumn col in listColumn)
                {
                    htColumnTextToColumnName[col.ColumnText] = col.ColumnName;
                    htColumnTextToSystemColumn[col.ColumnText] = col;
                }


                //检测Excel表中的列名跟系统客户资料列名是否匹配
                int currentIndex = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (!htColumnTextToColumnName.ContainsKey(dc.ColumnName.Trim()))
                    {
                        msg += " 客户信息里不包含列：" + dc.ColumnName;
                    }
                    else
                    {
                        htColumnTextToCellIndex[dc.ColumnName.Trim()] = currentIndex;
                        htCellIndexToColumnName[currentIndex] = htColumnTextToColumnName[dc.ColumnName.Trim()];
                        currentIndex++;
                    }
                }

                IList<SystemSelectType> listSystemSelectType = baseservice.SelectDynamic<SystemSelectType>(" AND SelectTypeID in (select SelectType from udefSystemColumn where TableID = " + tableID + " and DataType = 5)");
                foreach (SystemSelectType st in listSystemSelectType)
                {
                    IList<BaseIDName> listIDName = userdefinebase.GetIDNameList(st.SelectTypeID);
                    if (listIDName != null)
                    {
                        foreach (BaseIDName idname in listIDName)
                        {
                            htDataContentToDataType[st.SelectTypeID.ToString() + ":" + idname.Name] = idname.ID;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }

                //检测内容是否合理
                string[] sqls = new string[dt.Rows.Count];
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    DataRow row = dt.Rows[r];
                    int selectType = 0;
                    int dataType = 0;
                    string sql = "";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        selectType = htColumnTextToSystemColumn[dt.Columns[i].ColumnName.Trim()].SelectType;
                        dataType = htColumnTextToSystemColumn[dt.Columns[i].ColumnName.Trim()].DataType;

                        if (dataType == 5)//下拉框数据
                        {
                            if (row[i] + "" == "")
                            {
                                sql += ",null";
                            }
                            else if (!htDataContentToDataType.ContainsKey(selectType + ":" + row[i] + ""))
                            {
                                msg += "第" + (r + 1).ToString() + "行第" + (i + 1).ToString() + "列数据系统里不存在此数据源,请检查!";
                                return msg;
                            }
                            else
                            {
                                sql += "," + htDataContentToDataType[selectType + ":" + row[i] + ""];
                            }
                        }
                        else if (dataType == 2 || dataType == 4)//日期
                        {
                            if (row[i] + "" == "")
                            {
                                sql += ",null";
                            }
                            else if (!StringHelper.IsValidDBDate(row[i] + ""))
                            {
                                msg += "第" + (r + 1).ToString() + "行第" + (i + 1).ToString() + "列数据不是正确的日期,请检查!";
                                return msg;
                            }
                            else
                            {
                                sql += ",'" + row[i] + "'";
                            }
                        }
                        else//文本及数字
                        {
                            if (string.IsNullOrEmpty(row[i] + ""))
                            {
                                sql += ",NULL";
                            }
                            else
                            {
                                sql += ",'" + row[i] + "'";
                            }
                        }
                    }
                    sqls[r] = sql;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }

                string maxid = SmartSoft.Component.DbHelperSQL.GetSHSLInt("select MaxID from sysStream where TableName = '" + tableName + "'");
                int mid = int.Parse(maxid);
                string exsql = string.Format("insert into {0}({1}ID,{1}AddDate,{1}AddOperatorID", tableName, prefix);
                foreach (DataColumn c in dt.Columns)
                {
                    exsql += "," + htColumnTextToColumnName[c.ColumnName.Trim()];
                }
                exsql += ") values(";
                string finalSQL = "";
                int result = -1;
                foreach (string sql in sqls)
                {
                    finalSQL = exsql + mid.ToString() + ",getdate()," + operatorid + sql + ")";
                    result = SmartSoft.Component.DbHelperSQL.ExecuteSQL(finalSQL);
                    if (result < 0)
                    {
                        return "执行插入语句出错了，可能部分成功，请处理后再执行！";
                    }
                    else
                    {
                        mid++;
                    }
                }

                SmartSoft.Component.DbHelperSQL.ExecuteSQL("update sysStream set MAXID = " + mid + " where  TableName = '" + tableName + "'");

                return "";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region WorkDiary

        public virtual int SaveWorkDiary(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID, int? employeeID, ref string message)
        {
            if (action == FormAction.Update) //修改
            {

                WorkDiary entity = baseservice.GetDetail<WorkDiary>(rowID);

                entity.wdModifyDate = DateTime.Now;
                entity.wdModifyOperatorID = operatorID;
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);

                baseservice.Update<WorkDiary>(entity);

            }
            else if (action == FormAction.Insert) //增加
            {
                WorkDiary entity = new WorkDiary();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);
               
                entity.wdAddOperatorID = operatorID;
                entity.wdAddDate = DateTime.Now;

                rowID = baseservice.Add<WorkDiary>(entity);
            }
            return rowID;
        }

        #endregion

        #region CustomerClue
        public virtual int SaveCustomerClue(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID, int? employeeID, ref string message)
        {
            if (action == FormAction.Update) //修改
            {

                CustomerClue entity = baseservice.GetDetail<CustomerClue>(rowID);

                entity.ccModifyDate = DateTime.Now;
                entity.ccModifyOperatorID = operatorID;
                //从页面中得到系统字段
                userdefinebase.BindControlsToObject(ctrl, entity);

                baseservice.Update<CustomerClue>(entity);

            }
            else if (action == FormAction.Insert) //增加
            {
                CustomerClue entity = new CustomerClue();
                //得到控件中的系统字段的值
                userdefinebase.BindControlsToObject(ctrl, entity);

                entity.ccAddOperatorID = operatorID;
                entity.ccAddDate = DateTime.Now;

                rowID = baseservice.Add<CustomerClue>(entity);
            }
            return rowID;
        }
        #endregion
    }
}
