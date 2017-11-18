/***************************************************************************
 * 
 *       功能：     表现层基类
 *       作者：     朱少军
 *       日期：     2007-2-5
 * 
 *       修改日期： 
 *       修改人：   朱少军
 *       修改内容： 
 
 * *************************************************************************/

namespace SmartSoft.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using System.IO;


    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;

    using SmartSoft.Service.Interface.Data;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Common;
    using SmartSoft.Component;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Service;
    using SmartSoft.Service.Implement.Common;

    public class BaseController
    {
        #region Fields
        private static UserDefineBaseService _userdefinebase;
        private static IOrganizationService _org;
        private static IMessageService _message;
        private static IAuthorizationService _authorization;
        private static IDefCommonService _defcommon;

        public IDefCommonService defcommon
        {
            set { _defcommon = value; }
        }

        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        public IMessageService message
        {
            set { _message = value; }
            get { return _message;}
        }
        
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        public IOrganizationService org
        {
            set { _org = value; }
            get { return _org;}
        }

        public BaseController()
        {
        }

        private static ColumnViewService _columnview;
        public ColumnViewService columnview
        {
            set { _columnview = value; }
            get { return _columnview; }
        }

        private static ColumnViewOrderService _columnvieworder;
        public ColumnViewOrderService columnvieworder
        {
            set { _columnvieworder = value; }
            get { return _columnvieworder; }
        }

        private static CustomColumnService _customcolumn;
        public CustomColumnService customcolumn
        {
            set { _customcolumn = value; }
        }

        private static CustomColumnValueService _customcolumnvalue;
        public CustomColumnValueService customcolumnvalue
        {
            set { _customcolumnvalue = value; }
        }
        private static ColumnService _column;
        public ColumnService column
        {
            set { _column = value; }
        }

        private static LayoutDetailViewService _layoutdetailview;
        public LayoutDetailViewService layoutdetailview
        {
            set { _layoutdetailview = value; }
        }

        private static LayoutService _layout;
        public LayoutService layout
        {
            set { _layout = value; }
        }

        private static SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        private static SelectDataDynamicService _selectdatadynamic;
        public SelectDataDynamicService selectdatadynamic
        {
            set { _selectdatadynamic = value; }
        }

        
        public string Extend = "Field";


        private static BaseService _baseservice;
        public BaseService baseservice
        {
            set { _baseservice = value; }
            get { return _baseservice; }
        }
        #endregion

        #region Ajax
        #region Customer
        /// <summary>
        /// 从客户名称模糊搜索出客户资料集
        /// </summary>
        /// <param name="custName">客户名称</param>
        /// <returns>cusID|cusName字符串集</returns>
        [AjaxPro.AjaxMethod]
        public string[] GetCustIDAndCustNameList(string custName, int opeID)
        {
            custName = custName.Replace("'", "").Replace(";", "").Replace("--", "");
            if (custName == " ")
            {
                custName = "";
            }

            string[] result = null;
            string whereCondition = string.Empty;
            whereCondition = GetCondition(opeID);
            whereCondition += string.Format(" AND ([cusName] like '%{0}%')", custName);
            IList<Customer> listCustomer = _baseservice.SelectDynamic<Customer>(whereCondition);
            if (listCustomer.Count > 0)
            {
                int nLength = 0;
                nLength = listCustomer.Count > MAX_LENGTH ? MAX_LENGTH : listCustomer.Count;
                result = new string[nLength];
                for (int i = 0; i < nLength; i++)
                {
                    Customer cus = listCustomer[i] as Customer;
                    result[i] = cus.cusID + "|" + cus.cusName;
                    ;
                }
            }

            return result;
        }

        private readonly int MAX_LENGTH = 100; //返回的最大限制数

        [AjaxPro.AjaxMethod]
        public string[] GetColumnNameDataTypeByLayoutID(int layoutID)
        {
            string[] result = null;
            IList<LayoutDetailView> listLayoutDetailView = _layoutdetailview.SelectByLayoutID(layoutID);
            if (listLayoutDetailView.Count > 0)
            {
                result = new string[listLayoutDetailView.Count];
                for (int i = 0; i < listLayoutDetailView.Count; i++)
                {
                    LayoutDetailView de = listLayoutDetailView[i] as LayoutDetailView;
                    result[i] = de.ColumnName + "|" + de.DataType;
                }
            }

            return result;

        }

        /// <summary>
        /// 根据表名获得某表某记录的所有信息，包括外键，且如是ID，则替换成相应文本,格式化
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="rowID">记录编号</param>
        /// <returns>ColumnName|FormatValue|RawValue形式的字符串数据集</returns>
        [AjaxPro.AjaxMethod]
        public string[] GetRecordAllByTableName(string tableName, int rowID)
        {
            string[] result = null;
            IList<SystemTable> listSystemTable = _systemtable.SelectDynamic(" AND TableName = '" + tableName + "'");

            if (listSystemTable.Count > 0)
            {
                SystemTable objSystemTable = listSystemTable[0];
                int tableID = objSystemTable.TableID;
                result = GetRecordAllByTableID(tableID, rowID);
            }

            return result;
        }

        /// <summary>
        /// 根据表ID获得某表某记录的所有信息，包括外键，且如是ID，则替换成相应文本,格式化
        /// </summary>
        /// <param name="tableID">表ID</param>
        /// <param name="rowID">记录编号</param>
        /// <returns>ColumnName|FormatValue|RawValue形式的字符串数据集</returns>
        [AjaxPro.AjaxMethod]
        public string[] GetRecordAllByTableID(int tableID, int rowID)
        {
            //初始化GridView的所有列
            Hashtable htDataTypeFormatText = new Hashtable();
            string unitCurrency = StringHelper.GetConfigValue("Currency");
            string unitPrice = StringHelper.GetConfigValue("Price");
            string unitQty = StringHelper.GetConfigValue("Qty");
            string unitAmount = StringHelper.GetConfigValue("Amount");
            htDataTypeFormatText[((int)EnumDataType.Currency).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitCurrency) ? "2" : unitCurrency) + "}";
            htDataTypeFormatText[((int)EnumDataType.Price).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitPrice) ? "6" : unitPrice) + "}";
            htDataTypeFormatText[((int)EnumDataType.Qty).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitQty) ? "2" : unitQty) + "}";
            htDataTypeFormatText[((int)EnumDataType.Amount).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitAmount) ? "4" : unitAmount) + "}";

            string[] result = null;
            DataTable dtRecord = _selectdatadynamic.GetRecordDetail(tableID, rowID, "", "");
            if (dtRecord.Rows.Count == 1 && dtRecord.Columns.Count > 0)
            {
                DataRow rowRecord = dtRecord.Rows[0];
                result = new string[dtRecord.Columns.Count];

                IList<Column> listColumn = _column.SelectColumnByTableID(tableID);
                Hashtable htColumn = new Hashtable();
                //把布局字段的所有信息放入Hashtable里供后面绑定时格式化和换成下拉框文本时使用
                foreach (Column c in listColumn)
                {
                    htColumn[c.ColumnName] = c;
                }

                Column column = null;

                for (int i = 0; i < dtRecord.Columns.Count; i++)
                {
                    DataColumn c = dtRecord.Columns[i];
                    string columnName = c.ColumnName;
                    string rawValue = rowRecord[columnName].ToString();
                    string formatValue = rawValue;
                    column = htColumn[columnName] as Column;

                    if (column != null)
                    {
                        int nDataType = column.DataType;
                        EnumDataType enumDataType = (EnumDataType)Enum.Parse(typeof(EnumDataType), nDataType.ToString());

                        switch (enumDataType)
                        {
                            //下拉框列表处理：从主键值获得相应的文本值
                            case EnumDataType.Select:
                                if (StringHelper.IsValidDBInt(formatValue))
                                {
                                    formatValue = _userdefinebase.GetNameByID(column.SelectType,
                                     int.Parse(formatValue), false);
                                }
                                break;

                            //复选框处理：选择时为√否则为空白
                            case EnumDataType.CheckBox:
                                formatValue = formatValue.ToLower().Trim();
                                formatValue = formatValue == "1" || formatValue == "true" ? "√" : "";
                                break;

                            //其它类型的处理：格式化文本
                            default:
                                if (isDataTypeValid(enumDataType, formatValue))
                                {
                                    string sFormatText = DataType.GetFormatText(enumDataType);
                                    if (htDataTypeFormatText.ContainsKey(((int)enumDataType).ToString()))
                                    {
                                        sFormatText = htDataTypeFormatText[((int)enumDataType).ToString()].ToString();
                                    }
                                    formatValue = String.Format(sFormatText,
                                         SmartSoft.Component.DataTypeConverter.ChangeType(Type.GetType(DataType.GetTypeText(enumDataType)), formatValue.ToString()));
                                }

                                break;
                        }

                        result[i] = columnName + "|" + formatValue + "|" + rawValue;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 判断是否合法的数据类型对应的值
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="rawValue">值</param>
        /// <returns>是否合法</returns>
        private bool isDataTypeValid(EnumDataType type, string rawValue)
        {
            bool bValid = true;
            rawValue = rawValue.ToLower().Trim();
            switch (type)
            {
                case EnumDataType.CheckBox:
                    bValid = (rawValue == "1" || rawValue == "0" || rawValue == "true" || rawValue == "false");
                    break;

                case EnumDataType.Currency:
                    double currency;
                    bValid = Double.TryParse(rawValue, out currency);
                    break;

                case EnumDataType.Date:
                case EnumDataType.DateTime:
                    DateTime datatime;
                    bValid = DateTime.TryParse(rawValue, out datatime);
                    break;

                case EnumDataType.IntType:
                case EnumDataType.Select:
                    int i;
                    bValid = int.TryParse(rawValue, out i);
                    break;

                default:
                    bValid = !String.IsNullOrEmpty(rawValue);
                    break;
            }

            return bValid;
        }

        private int getCustomerID(int rowID, string fromType)
        {
            int r = rowID;
            int customerid = 0;
            if (!string.IsNullOrEmpty(fromType))
            {
                switch (fromType)
                {
                    case "followhistory":
                        CustomerFollowHistory cfh = baseservice.GetDetail<CustomerFollowHistory>(r);
                        //customerid = cfh.cfhCustomerID;
                        customerid = cfh.cfhRelatedID.Value;
                        break;

                    case "followplan":
                        CustomerFollowPlan cfp = baseservice.GetDetail<CustomerFollowPlan>(r);
                        customerid = cfp.cfpRelatedID.Value;
                        break;

                    case "linkman":
                        CustomerLinkMan clm = baseservice.GetDetail<CustomerLinkMan>(r);
                        customerid = clm.clmCustomerID;
                        break;

                    case "feedback":
                        CustomerFeedback cfb = baseservice.GetDetail<CustomerFeedback>(r);
                        customerid = cfb.cfbCustomerID;
                        break;

                    case "business":
                        CustomerBusiness cb = baseservice.GetDetail<CustomerBusiness>(r);
                        customerid = cb.cbCustomerID.Value;
                        break;

                    case "receipt":
                        CustomerReceipt cr = baseservice.GetDetail<CustomerReceipt>(r);
                        customerid = cr.crCustomerID.Value;
                        break;
                    case "opptunity":
                        CustomerOpptunity co = baseservice.GetDetail<CustomerOpptunity>(r);
                        customerid = co.coCustomerID.Value;
                        break;
                }
            }
            else
            {
                customerid = r;
            }

            return customerid;
        }

        string splitor = "$|@";

        public void DeleteMessageByURL(string url)
        {
            DbHelperSQL.ExecuteSQL("Delete from sysMessage where URL like '" + url + "&MessageID%'  and sendtime > getdate() ");
        }

        public void AddMessages(int operatorID, string title, string content, string url, DateTime sendTime)
        {
            //Add messages
            sysMessage meg = new sysMessage();
            meg.SendTime = sendTime;
            meg.AwokeTime = sendTime;
            meg.MessageContent = content;
            meg.Title = title;
            meg.SendOperatorID = operatorID;
            meg.MessageTypeID = 1;
            meg.URL = url;
            ArrayList a = new ArrayList();
            a.Add(operatorID);
            int messageid = message.InsertMessage(meg, a);
            meg.URL += "&MessageID=" + messageid;
            message.UpdateMessage(meg);
        }

        public void AddMessages(int operatorID, string title, string content, string url)
        {
            //Add messages
            sysMessage meg = new sysMessage();
            meg.SendTime = DateTime.Now;
            meg.MessageContent = content;
            meg.Title = title;
            meg.SendOperatorID = operatorID;
            meg.MessageTypeID = 1;
            meg.URL = url;
            ArrayList a = new ArrayList();
            IList<Operators> operators = org.GetOperatorsList();
            foreach (Operators o in operators)
            {
                if (o.opeID != operatorID)
                {
                    a.Add(o.opeID);
                }
            }
            int messageid = message.InsertMessage(meg, a);
            meg.URL += "&MessageID=" + messageid;
            message.UpdateMessage(meg);
        }

        public void AddCustomerLog(string dosth, int customerID)
        {
            OperatorLog entity = new OperatorLog();
            int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
            entity.olCustomerID = customerID;
            entity.olOperatorID = operatorID;
            entity.olContent = dosth;
            entity.olFromIP = HttpContext.Current.Request.UserHostAddress.ToString();
            entity.olDate = DateTime.Now;
            entity.olOperateSource = "PC";
            _baseservice.Add<OperatorLog>(entity);
        }

        public void AddOperatorLog(string dosth)
        {
            AddCustomerLog(dosth, 0);
        }

        [AjaxPro.AjaxMethod]
        public string[] GetCustIDAndCustNameListAll(int opeID)
        {
            string[] result = null;
            string whereCondition = string.Empty;
            whereCondition = GetCondition(opeID);
            IList<Customer> listCustomer = _baseservice.SelectDynamic<Customer>(whereCondition);
            if (listCustomer.Count > 0)
            {
                int nLength = 0;
                nLength = listCustomer.Count;
                result = new string[nLength];
                for (int i = 0; i < nLength; i++)
                {
                    Customer cus = listCustomer[i] as Customer;
                    result[i] = cus.cusID + "|" + cus.cusName;
                    ;
                }
            }

            return result;
        }

        [AjaxPro.AjaxMethod]
        public string[] GetCustIDAndCustNameListAll2(int opeID)
        {
            string[] result = null;
            string whereCondition = string.Empty;
            IList<Customer> listCustomer = _baseservice.SelectDynamic<Customer>(whereCondition);
            if (listCustomer.Count > 0)
            {
                int nLength = 0;
                nLength = listCustomer.Count;
                result = new string[nLength];
                for (int i = 0; i < nLength; i++)
                {
                    Customer cus = listCustomer[i] as Customer;
                    result[i] = cus.cusID + "|" + cus.cusName;
                    ;
                }
            }

            return result;
        }
        #endregion

        #region CustomerFollowHistory
        [AjaxPro.AjaxMethod]
        public int SaveCustomerFollowHistoryForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerFollowHistory entity = new CustomerFollowHistory();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    DateTime d;
                    int i;
                    if (DateTime.TryParse(items[1], out d))
                    {
                        entity.cfhDate = d;
                    }
                    if (int.TryParse(items[2], out i))
                    {
                        entity.cfhTypeID = i;
                    }
                  
                    if (int.TryParse(items[4], out i))
                    {
                        entity.cfhOperatorID = i;
                    }

                    entity.cfhContent = items[5];

                    int cfhCustomerID=getCustomerID(int.Parse(items[13]), items[14]);

                    //entity.cfhCustomerID = getCustomerID(int.Parse(items[14]), items[15]);


                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        CustomerFollowHistory oldItem = baseservice.GetDetail<CustomerFollowHistory>(rowID);
                        entity.cfhAddDate = oldItem.cfhAddDate;
                        entity.cfhAddOperatorID = oldItem.cfhAddOperatorID;
                        entity.cfhID = rowID;
                        entity.cfhModifyOperatorID = operatorID;
                        entity.cfhModifyDate = DateTime.Now;
                        baseservice.Update<CustomerFollowHistory>(entity);
                        //AddCustomerLog("修改客户跟进 ID:" + rowID, entity.cfhRelatedID);

                        if (items[12] != "" && items[13] != "")
                        {
                            string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowPlan'");
                            DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerFollowPlan (cfpID,cfpCustomerID,cfpDate,cfpFollowTypeID,cfpPurpose,cfpOperatorID,cfpSummary,cfpAddOperatorID,cfpAddDate,cfpLinkManID) values ({0},{1},'{2}',1,'跟进',{3},'{4}',{5},getdate(),{6})", id, cfhCustomerID, items[13], operatorID, items[12], operatorID, items[3]));

                            string url = "../Data/CustomerFollowPlanEditForm.aspx?showPanel=divFollowPlanList&ctr=FollowPlan&Action=View&ID=" + id;
                            DeleteMessageByURL(url);
                            AddMessages(operatorID, operatorID, "跟进计划提醒", "跟进计划提醒", url, items[13]);
                        }

                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.cfhAddOperatorID = operatorID;
                        entity.cfhAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerFollowHistory>(entity);
                        //AddCustomerLog("添加客户跟进 ID:" + rowID, entity.cfhCustomerID);

                        if (items[12] != "" && items[13] != "")
                        {
                            string id = DbHelperSQL.GetSHSLInt("usp_GetID 'CustomerFollowPlan'");
                            DbHelperSQL.ExecuteSQL(string.Format("Insert into CustomerFollowPlan (cfpID,cfpCustomerID,cfpDate,cfpFollowTypeID,cfpPurpose,cfpOperatorID,cfpSummary,cfpAddOperatorID,cfpAddDate,cfpLinkManID) values ({0},{1},'{2}',1,'跟进',{3},'{4}',{5},getdate(),{6})", id, cfhCustomerID, items[13], operatorID, items[12], operatorID, items[3]));

                            string url = "../Data/CustomerFollowPlanEditForm.aspx?showPanel=divFollowPlanList&ctr=FollowPlan&Action=View&ID=" + id;
                            DeleteMessageByURL(url);
                            AddMessages(operatorID, operatorID, "跟进计划提醒", "跟进计划提醒", url, items[13]);
                        }

                    }

                    
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        public void AddMessages(int sendOperatorID, int toOperatorID, string title, string content, string url, string d)
        {
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
        }


        [AjaxPro.AjaxMethod]
        public string AddFollowHistoryCommentForClient(int cfhID, string comment)
        {
            try
            {
                CustomerFollowHistory entity = new CustomerFollowHistory();
                CustomerFollowHistory oldItem = baseservice.GetDetail<CustomerFollowHistory>(cfhID);
                entity.cfhAddDate = oldItem.cfhAddDate;
                entity.cfhAddOperatorID = oldItem.cfhAddOperatorID;
                entity.cfhID = cfhID;
                entity.cfhModifyOperatorID = oldItem.cfhModifyOperatorID;
                entity.cfhModifyDate = oldItem.cfhModifyDate;
                entity.cfhOperatorID = oldItem.cfhOperatorID;
                entity.cfhDate = oldItem.cfhDate;
                entity.cfhTypeID = oldItem.cfhTypeID;

                string username = HttpContext.Current.Session["UserName"] + "";
                baseservice.Update<CustomerFollowHistory>(entity);
                //AddCustomerLog("修改客户跟进 ID:" + cfhID, entity.cfhCustomerID);

                //Add messages
                string title = "[" + DbHelperSQL.GetSHSL("select cusName from Customer where cusID=" + entity.cfhRelatedID) + "] 跟进记录点评";
                string url = "Data/CustomerFollowHistoryEditForm.aspx?Action=View&ID=" + cfhID;
                DeleteMessageByURL(url);
                //AddMessages(entity.cfhOperatorID.Value, title, entity.cfhSubject,url, DateTime.Now);

                return "OK";
            }
            catch
            {
                return "NO";
            }
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerFollowHistory(int rID)
        {
            CustomerFollowHistory entity = _baseservice.GetDetail<CustomerFollowHistory>(rID);
            _baseservice.Delete<CustomerFollowHistory>(rID);
            //AddCustomerLog("删除客户跟进 ID:" + rID, entity.cfhCustomerID);
        }
        #endregion

        #region CustomerBusiness
        [AjaxPro.AjaxMethod]
        public int SaveCustomerBusinessForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerBusiness entity = new CustomerBusiness();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    DateTime d;
                    int i;
                    decimal de;
                    if (DateTime.TryParse(items[1], out d))
                    {
                        entity.cbDate = d;
                    }
                    if (int.TryParse(items[2], out i))
                    {
                        entity.cbBusinessType = i;
                    }
                    entity.cbName = items[3];
                    if (decimal.TryParse(items[4], out de))
                    {
                        entity.cbTotalAmount = de;
                    }
                    if (decimal.TryParse(items[5], out de))
                    {
                        entity.cbGotAmount = de;
                    }
                    if (decimal.TryParse(items[6], out de))
                    {
                        entity.cbNotGotAmount = de;
                    }

                    entity.cbRemark = items[7];

                    if (int.TryParse(items[8], out i))
                    {
                        entity.cbBranchID = i;
                    }

                    if (int.TryParse(items[9], out i))
                    {
                        entity.cbOperatorID = i;
                    }

                    entity.cbCustomerID = getCustomerID(int.Parse(items[10]), items[11]);

                    if (!entity.cbGotAmount.HasValue)
                        entity.cbGotAmount = 0.0m;
                    entity.cbNotGotAmount = entity.cbTotalAmount - entity.cbGotAmount;
                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        CustomerBusiness oldItem = baseservice.GetDetail<CustomerBusiness>(rowID);
                        entity.cbAddDate = oldItem.cbAddDate;
                        entity.cbAddOperatorID = oldItem.cbAddOperatorID;
                        entity.cbID = rowID;
                        entity.cbModifyOperatorID = operatorID;
                        entity.cbModifyDate = DateTime.Now;
                        baseservice.Update<CustomerBusiness>(entity);
                        AddCustomerLog("修改客户业务 ID:" + rowID, entity.cbCustomerID.Value);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.cbAddOperatorID = operatorID;
                        entity.cbAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerBusiness>(entity);
                        AddCustomerLog("添加客户业务 ID:" + rowID, entity.cbCustomerID.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerBusiness(int rID)
        {
            CustomerBusiness entity = _baseservice.GetDetail<CustomerBusiness>(rID);
            _baseservice.Delete<CustomerBusiness>(rID);

            AddCustomerLog("删除客户业务 ID:" + rID, entity.cbCustomerID.Value);
        }
        #endregion

        #region CustomerOpptunity
        [AjaxPro.AjaxMethod]
        public int SaveCustomerOpptunityForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerOpptunity entity = new CustomerOpptunity();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    DateTime d;
                    int i;
                    decimal de;
                    if (DateTime.TryParse(items[1], out d))
                    {
                        entity.coDate = d;
                    }
                    entity.coName = items[2];

                    if (int.TryParse(items[3], out i))
                    {
                        entity.coPhaseID = i;
                    }
                    
                    if (decimal.TryParse(items[4], out de))
                    {
                        entity.coPredictAmount = de;
                    }
                    if (DateTime.TryParse(items[5], out d))
                    {
                        entity.coPredictFinishDate = d;
                    }
                    if (int.TryParse(items[6], out i))
                    {
                        entity.coOperatorID = i;
                    }
                    if (int.TryParse(items[7], out i))
                    {
                        entity.coOpptunitySourceID = i;
                    }
                    
                    entity.coDecisionFlow = items[8];
                    entity.coCompetitors = items[9];

                    //if (int.TryParse(items[10], out i))
                    //{
                    //    entity.coStatusID = i;
                    //}

                    entity.coRequirement = items[10];

                    entity.coCustomerID = getCustomerID(int.Parse(items[11]), items[12]);

                   
                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        CustomerOpptunity oldItem = baseservice.GetDetail<CustomerOpptunity>(rowID);
                        entity.coAddDate = oldItem.coAddDate;
                        entity.coAddOperatorID = oldItem.coAddOperatorID;
                        entity.coID = rowID;
                        entity.coModifyOperatorID = operatorID;
                        entity.coModifyDate = DateTime.Now;
                        baseservice.Update<CustomerOpptunity>(entity);
                        AddCustomerLog("修改客户商机 ID:" + rowID, entity.coCustomerID.Value);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.coAddOperatorID = operatorID;
                        entity.coAddDate = DateTime.Now;
                        entity.coStatusID = 1;//活动状态
                        rowID = baseservice.Add<CustomerOpptunity>(entity);
                        AddCustomerLog("添加客户商机 ID:" + rowID, entity.coCustomerID.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerOpptunity(int rID)
        {
            CustomerOpptunity entity = _baseservice.GetDetail<CustomerOpptunity>(rID);
            _baseservice.Delete<CustomerOpptunity>(rID);

            AddCustomerLog("删除客户商机 ID:" + rID, entity.coCustomerID.Value);
        }
        #endregion

        #region CustomerReceipt
        [AjaxPro.AjaxMethod]
        public int SaveCustomerReceiptForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerReceipt entity = new CustomerReceipt();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    DateTime d;
                    int i;
                    decimal de;

                    if (int.TryParse(items[1], out i))
                    {
                        entity.crBusinessID = i;
                    }
                    if (int.TryParse(items[2], out i))
                    {
                        entity.crTypeID = i;
                    }
                    if (DateTime.TryParse(items[3], out d))
                    {
                        entity.crDate = d;
                    }
                    if (decimal.TryParse(items[4], out de))
                    {
                        entity.crAmount = de;
                    }
                    entity.crRemark = items[5];
                    if (int.TryParse(items[6], out i))
                    {
                        entity.crOperatorID = i;
                    }
                    entity.crCustomerID = getCustomerID(int.Parse(items[7]), items[8]);

                    if (items[1] != "")
                    {
                        string sql = "select ISNULL(sum(crAmount),0) from CustomerReceipt where crID != " + items[0] + " and crBusinessID = " + items[1];
                        string totalAmount = DbHelperSQL.GetSHSL(sql);
                        sql = "select ISNULL(cbTotalAmount, 0) from CustomerBusiness where cbID = " + items[1];
                        string billAmount = DbHelperSQL.GetSHSL(sql);
                        if (decimal.Parse(totalAmount) + entity.crAmount > decimal.Parse(billAmount))
                        {
                            return -1;
                        }
                    }

                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        CustomerReceipt oldItem = baseservice.GetDetail<CustomerReceipt>(rowID);
                        entity.crAddDate = oldItem.crAddDate;
                        entity.crAddOperatorID = oldItem.crAddOperatorID;
                        entity.crID = rowID;
                        entity.crModifyOperatorID = operatorID;
                        entity.crModifyDate = DateTime.Now;
                        baseservice.Update<CustomerReceipt>(entity);
                        AddCustomerLog("修改客户业务 ID:" + rowID, entity.crCustomerID.Value);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.crAddOperatorID = operatorID;
                        entity.crAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerReceipt>(entity);
                        AddCustomerLog("添加客户收款 ID:" + rowID, entity.crCustomerID.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerReceipt(int rID)
        {
            CustomerReceipt entity = _baseservice.GetDetail<CustomerReceipt>(rID);
            _baseservice.Delete<CustomerReceipt>(rID);

            AddCustomerLog("删除客户收款 ID:" + rID, entity.crCustomerID.Value);
        }

        #endregion

        #region CustomerLinkMan
        [AjaxPro.AjaxMethod]
        public int SaveCustomerLinkManForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerLinkMan entity = new CustomerLinkMan();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    entity.clmName = items[1];
                    entity.clmSex = items[2];
                    entity.clmPost = items[3];
                    entity.clmTel = items[4];
                    entity.clmMobile = items[5];
                    entity.clmWeChat = items[6];
                    entity.clmEmail = items[7];
                    DateTime d;
                    int i;
                    if (DateTime.TryParse(items[8], out d))
                    {
                        entity.clmBirthday = d;
                    }
                    //if (int.TryParse(items[9], out i))
                    //{
                    //    entity.clmImportantDayType = i;
                    //}
                    //if (DateTime.TryParse(items[10], out d))
                    //{
                    //    entity.clmImportantDay = d;
                    //}
                    entity.clmQQ = items[11];
                    entity.clmRemark = items[12];

                    if (int.TryParse(items[13], out i))
                    {
                        entity.clmTypeID = i;
                    }
                    entity.clmHobby = items[14];
                    entity.clmHometown = items[15];

                    entity.clmSkype = items[16];
                    entity.clmCustomerID = getCustomerID(int.Parse(items[17]), items[18]);
                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        entity.clmID = rowID;
                        CustomerLinkMan oldItem = baseservice.GetDetail<CustomerLinkMan>(rowID);
                        entity.clmAddDate = oldItem.clmAddDate;
                        entity.clmAddOperatorID = oldItem.clmAddOperatorID;
                        AddCustomerLog("修改客户联系人 ID:" + rowID, entity.clmCustomerID);

                        entity.clmModifyOperatorID = operatorID;
                        entity.clmModifyDate = DateTime.Now;
                        baseservice.Update<CustomerLinkMan>(entity);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.clmAddOperatorID = operatorID;
                        entity.clmAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerLinkMan>(entity);
                        AddCustomerLog("添加客户联系人 ID:" + rowID, entity.clmCustomerID);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerLinkMan(int rID)
        {
            CustomerLinkMan entity = _baseservice.GetDetail<CustomerLinkMan>(rID);
            _baseservice.Delete<CustomerLinkMan>(rID);
            AddCustomerLog("删除客户联系人 ID:" + rID, entity.clmCustomerID);
        }
        #endregion

        #region CustomerFeedback
        [AjaxPro.AjaxMethod]
        public int SaveCustomerFeedbackForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerFeedback entity = new CustomerFeedback();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    int i;
                    if (int.TryParse(items[1], out i))
                    {
                        entity.cfbFeedbackTypeID = i;
                    }

                    entity.cfbLinkman = items[2];
                    entity.cfbTelephone = items[3];
                    entity.cfbEmail = items[4];
                    entity.cfbOrderRelated = items[5];

                    if (int.TryParse(items[6], out i))
                    {
                        entity.cfbHandleOperatorID = i;
                    }

                    entity.cfbContent = items[7];
                    entity.cfbCustomerID = getCustomerID(int.Parse(items[8]), items[9]);
                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        entity.cfbID = rowID;
                        CustomerFeedback oldItem = baseservice.GetDetail<CustomerFeedback>(rowID);
                        entity.cfbAddDate = oldItem.cfbAddDate;
                        entity.cfbAddOperatorID = oldItem.cfbAddOperatorID;

                        entity.cfbModifyOperatorID = operatorID;
                        entity.cfbModifyDate = DateTime.Now;
                        baseservice.Update<CustomerFeedback>(entity);

                        if (entity.cfbHandleOperatorID.HasValue && entity.cfbHandleOperatorID != oldItem.cfbHandleOperatorID)
                        {
                            string title = "[" + baseservice.GetDetail<Customer>(entity.cfbCustomerID).cusName + "] 客户反馈";
                            string url = "Data/CustomerFeedbackEditForm.aspx?Action=View&ID=" + entity.cfbID;
                            DeleteMessageByURL(url);
                            AddMessages(entity.cfbHandleOperatorID.Value, title, entity.cfbContent, url + "&HandleFeedback=1", System.DateTime.Now);
                        }
                        AddCustomerLog("修改客户反馈 ID:" + rowID, entity.cfbCustomerID);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.cfbAddOperatorID = operatorID;
                        entity.cfbAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerFeedback>(entity);

                        //Add messages
                        if (entity.cfbHandleOperatorID.HasValue)
                        {
                            string title = "[" + baseservice.GetDetail<Customer>(entity.cfbCustomerID).cusName + "] 客户反馈";
                            string url = "Data/CustomerFeedbackEditForm.aspx?Action=View&ID=" + rowID;
                            DeleteMessageByURL(url);
                            AddMessages(entity.cfbHandleOperatorID.Value, title, entity.cfbContent, url + "&HandleFeedback=1", DateTime.Now);
                        }
                        AddCustomerLog("添加客户反馈 ID:" + rowID, entity.cfbCustomerID);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerFeedback(int rID)
        {
            CustomerFeedback entity = _baseservice.GetDetail<CustomerFeedback>(rID);
            _baseservice.Delete<CustomerFeedback>(rID);
            string url = "Data/CustomerFeedbackEditForm.aspx?Action=View&ID=" + rID;
            DbHelperSQL.ExecuteSQL("Delete from sysMessage where URL like '" + url + "%'");
            AddCustomerLog("删除客户反馈 ID:" + rID, entity.cfbCustomerID);
        }
        #endregion

        #region CustomerFollowPlan
        [AjaxPro.AjaxMethod]
        public int SaveCustomerFollowPlanForClient(string details)
        {
            int rowID = 0;
            try
            {
                if (!string.IsNullOrEmpty(details))
                {
                    CustomerFollowPlan entity = new CustomerFollowPlan();

                    int operatorID = int.Parse(HttpContext.Current.Session["UserID"] + "");
                    string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                    DateTime d;
                    int i;
                    if (DateTime.TryParse(items[1], out d))
                    {
                        entity.cfpDate = d;
                    }
                    if (int.TryParse(items[5], out i))
                    {
                        entity.cfpOperatorID = i;
                    }
                    entity.cfpContent = items[7];
                    entity.cfpRelatedID = 0;
                    if (items[0] != "0") //修改
                    {
                        rowID = int.Parse(items[0]);
                        entity.cfpID = rowID;
                        CustomerFollowPlan oldItem = baseservice.GetDetail<CustomerFollowPlan>(rowID);
                        entity.cfpAddDate = oldItem.cfpAddDate;
                        entity.cfpAddOperatorID = oldItem.cfpAddOperatorID;

                        entity.cfpModifyOperatorID = operatorID;
                        entity.cfpModifyDate = DateTime.Now;
                        baseservice.Update<CustomerFollowPlan>(entity);

                        string title = "跟进计划";
                        string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + entity.cfpID;
                        DeleteMessageByURL(url);
                        AddMessages(entity.cfpOperatorID.Value, title, entity.cfpContent, url, entity.cfpDate.Value);
                        
                        AddCustomerLog("修改跟进计划 ID:" + rowID, 0);
                    }
                    else if (items[0] == "0") //增加
                    {
                        entity.cfpAddOperatorID = operatorID;
                        entity.cfpAddDate = DateTime.Now;
                        rowID = baseservice.Add<CustomerFollowPlan>(entity);

                        //Add messages
                        string title = "跟进计划";
                        string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + rowID;
                        DeleteMessageByURL(url);
                        AddMessages(entity.cfpOperatorID.Value, title, entity.cfpContent, url, entity.cfpDate.Value);
                        AddCustomerLog("添加跟进计划 ID:" + rowID, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return rowID;
        }

        [AjaxPro.AjaxMethod]
        public void DeleteCustomerFollowPlan(int rID)
        {
            CustomerFollowPlan entity = _baseservice.GetDetail<CustomerFollowPlan>(rID);
            _baseservice.Delete<CustomerFollowPlan>(rID);
            string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + rID;
            DbHelperSQL.ExecuteSQL("Delete from sysMessage where URL like '" + url + "%'");
            AddCustomerLog("删除跟进计划 ID:" + rID, 0);
        }

 
        #endregion

        #region CustomerFile
        [AjaxPro.AjaxMethod]
        public void DeleteCustomerFile(int rID)
        {
            string cfCN = DbHelperSQL.GetSHSL("select cfCN from CustomerFile where cfID = " + rID);
            File.Delete(System.Web.HttpContext.Current.Request.MapPath("../UploadFile/CustomerFile/") + cfCN);
            DbHelperSQL.ExecuteSQL("delete from CustomerFile where cfID = " + rID);
            AddCustomerLog("删除客户文档 ID:" + rID, 0);
        }
        #endregion
        #endregion

        #region 通用方法
        public virtual void InitEditPage(Control ctrl, int layoutID, bool bReadOnly, string tableCSS, string TableHeaderCss, Hashtable htDataTypeFormat, int operatorID)
        {
            _userdefinebase.InitEditPage(ctrl, layoutID, bReadOnly, tableCSS, TableHeaderCss, true, htDataTypeFormat);
        }

        public virtual void BindAllToControls(Control ctrl, int layoutID, int rowID, Hashtable htDataTypeFormatText)
        {
            _userdefinebase.BindAllToControls(ctrl, layoutID, rowID, htDataTypeFormatText);
        }
        public void InitDropDownList(Control ctrl, int viewID, int operatorID)
        {
            _userdefinebase.InitQueryDropdownlist(ctrl, viewID, "Search", operatorID);
        }

        public string GetSearchCondition(Control ctrl, int viewID)
        {
            return _userdefinebase.GetSearchCondition(ctrl, viewID);
        }

        public void GetViewSource(Control ctrl, string ctrGridViewID, out int viewID, int opID)
        {
            DropDownList ddl = ctrl as DropDownList;

            string strUrl = ddl.Page.Request.Url.PathAndQuery.Substring(1);
            IList<sysRoleLayoutView> sysRoleLayoutViewList = _authorization.SelectRoleLayoutViewByRoleControlAction(strUrl, ctrGridViewID, opID, null);
            if (sysRoleLayoutViewList == null)
            {
                strUrl = ddl.Page.Request.Url.AbsolutePath.Substring(1);
                sysRoleLayoutViewList = _authorization.SelectRoleLayoutViewByRoleControlAction(strUrl, ctrGridViewID, opID, null);
            }
            
            string columnViewIds = "0";
            if (sysRoleLayoutViewList != null)
            {
                foreach (sysRoleLayoutView sys in sysRoleLayoutViewList)
                {
                    columnViewIds += "," + sys.LayoutOrViewID.ToString();
                }
            }

            string whereCondition = " AND ViewID IN (" + columnViewIds + ")";

            viewID = 0;
            //非第一次初始化
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                int.TryParse(ddl.SelectedValue, out viewID);
            }

            IList<ColumnView> listColumnView = _columnview.SelectDynamic(whereCondition);

            //有视图时
            if (listColumnView.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = listColumnView;
                ddl.DataTextField = "ViewName";
                ddl.DataValueField = "ViewID";
                ddl.DataBind();
                //第一次初始化时
                if (viewID == 0)
                {
                    int.TryParse(ddl.SelectedValue, out viewID);
                }

                ddl.Visible = true;
            }

            //只有一个视图时，隐藏视图下拉框
            if (listColumnView.Count <= 1)
            {
                ddl.Visible = false;
            }

            //没有视图时
            if (listColumnView.Count == 0)
            {
                ddl.Visible = false;
                string v = GetDefaultViewID(strUrl, ctrGridViewID);
                int.TryParse(v, out viewID);
               
            }

            if (viewID <= 0)
            {
                WebFunc.AjaxAlert("您没有获得视图权限！请联系管理员！", ddl.Page);
            }
        }

        public string GetDefaultViewID(string url, string gridViewID)
        {
            return DbHelperSQL.GetSHSLInt(string.Format("select ViewID from udefColumnView A INNER JOIN sysViewLayoutControl B ON A.TableID = B.TableID where ControlName = '{0}' and PageID in (select PageID from sysPage where PageFilePath like '%{1}%')", gridViewID, url));
        }

        public void GetLayoutSource(Control ctrl, string ctrLayoutID, out int layoutID, int opID, int actionID)
        {
            //复制即新增
            if (actionID == (int)FormAction.Copy)
            {
                actionID = (int)FormAction.Insert;
            }

            DropDownList ddl = ctrl as DropDownList;
            string strUrl = ddl.Page.Request.Url.PathAndQuery.Substring(1);
            IList<sysRoleLayoutView> sysRoleLayoutViewList = _authorization.SelectRoleLayoutViewByRoleControlAction(strUrl, ctrLayoutID, opID, actionID);
            if (sysRoleLayoutViewList == null)
            {
                strUrl = ddl.Page.Request.Url.AbsolutePath.Substring(1);
                sysRoleLayoutViewList = _authorization.SelectRoleLayoutViewByRoleControlAction(strUrl, ctrLayoutID, opID, actionID);
            }
            string layoutIDs = "0";
            layoutID = 0;
            if (sysRoleLayoutViewList != null)
            {
                foreach (sysRoleLayoutView sys in sysRoleLayoutViewList)
                {
                    layoutIDs += "," + sys.LayoutOrViewID.ToString();
                }
                string whereCondition = " AND LayoutID IN (" + layoutIDs + ")";
                IList<Layout> listLayout = _layout.SelectDynamic(whereCondition);

                
                //非第一次初始化
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                {
                    int.TryParse(ddl.SelectedValue, out layoutID);
                }



                //有视图时
                if (listLayout.Count > 0)
                {
                    ddl.Items.Clear();
                    ddl.DataSource = listLayout;
                    ddl.DataTextField = "LayoutName";
                    ddl.DataValueField = "LayoutID";
                    ddl.DataBind();
                    //第一次初始化时
                    if (layoutID == 0)
                    {
                        int.TryParse(ddl.SelectedValue, out layoutID);
                    }
                    ddl.Visible = true;
                }

                //只有一个布局时，隐藏布局下拉框
                if (listLayout.Count <= 1)
                {
                    ddl.Visible = false;
                }

                //没有布局时
                if (listLayout.Count == 0)
                {
                    ddl.Visible = false;
                    string l = GetDefaultLayout(strUrl, ctrLayoutID);
                    int.TryParse(l, out layoutID);
                }

                if (layoutID <= 0)
                {
                    WebFunc.AjaxAlert("您没有获得布局权限！请联系管理员！", ddl.Page);
                }
            }
        }

        public string GetDefaultLayout(string url, string ctrLayoutID)
        {
            return DbHelperSQL.GetSHSLInt(string.Format("Select LayoutID from udefLayout A INNER JOIN sysViewLayoutControl B ON A.TableID = B.TableID where ControlName = '{0}' and PageID in (select PageID from sysPage where PageFilePath like '%{1}%')", ctrLayoutID, url));
        }

        /// <summary>
        /// 加入隐藏域
        /// </summary>
        /// <param name="ctrl">布局控件</param>
        /// <param name="ID">要加入的隐藏域ID</param>
        public void AddHiddenField(Control ctrl, string ID)
        {
            string cotrolID = Extend + ID;
            if (ctrl.FindControl(cotrolID) != null)
            {

                Control c = ctrl.FindControl(cotrolID);
                if (c is Label)
                {
                    (c as Label).Attributes.Add("style", "display:none;");
                }
                else if (c is TextBox)
                {
                    (c as TextBox).Attributes.Add("style", "display:none;");
                }

            }
            else
            {
                HiddenField hf = new HiddenField();
                hf.ID = cotrolID;
                ctrl.Controls.Add(hf);
            }
        }

        public Control GetControlByFieldName(Control container, string columnName)
        {
            return _userdefinebase.GetControlByFieldName(container, columnName);
        }

        /// <summary>
        /// 得到查找控件的值
        /// </summary>
        /// <param name="ctrl">布局控件</param>
        /// <param name="columnName">查找的控件名</param>
        /// <returns>控件值</returns>
        public string GetControlValue(Control ctrl, string columnName)
        {
            string controlValue = string.Empty;
            Control c = _userdefinebase.GetControlByFieldName(ctrl, columnName);
            if (c != null)
            {
                if (c is Label)
                {
                    controlValue = (c as Label).Text;
                }
                else if (c is TextBox)
                {
                    controlValue = (c as TextBox).Text;
                }
                else if (c is ListControl)
                {
                    controlValue = (c as ListControl).SelectedValue;
                }
                else if (c is HiddenField)
                {
                    controlValue = (c as HiddenField).Value;
                }
                else if (c is CheckBox)
                {
                    controlValue = (c as CheckBox).Checked ? "1" : "0";
                }
            }
            return controlValue;
        }

        /// <summary>
        /// 向查询到的控件赋值
        /// </summary>
        /// <param name="ctrl">布局控件</param>
        /// <param name="columnName">查找的控件名</param>
        /// <param name="controlValue">要设置的值</param>
        public void SetControlValue(Control ctrl, string columnName, string controlValue)
        {
            try
            {
                Control c = _userdefinebase.GetControlByFieldName(ctrl, columnName);
                if (c != null)
                {
                    if (c is Label)
                    {
                        (c as Label).Text = controlValue;
                    }
                    else if (c is TextBox)
                    {
                        (c as TextBox).Text = controlValue;
                    }
                    else if (c is ListControl)
                    {
                        (c as ListControl).SelectedValue = controlValue;
                    }
                    else if (c is HiddenField)
                    {
                        (c as HiddenField).Value = controlValue;
                    }
                    else if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = bool.Parse(controlValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据视图ID得到TableID
        /// </summary>
        /// <param name="viewID">视图ID</param>
        /// <returns></returns>
        public int GetTableIDByViewID(int viewID)
        {
            ColumnView columnView = _columnview.Select(viewID);

            return columnView == null ? 0 : columnView.TableID;
        }

        /// <summary>
        /// 根据布局ID得到TableID
        /// </summary>
        /// <param name="layoutID"></param>
        /// <returns></returns>
        public int GetTableIDbyLayoutID(int layoutID)
        {
            Layout layout = _layout.Select(layoutID);

            return layout == null ? 0 : layout.TableID;
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        public void SetMessageIsRead(int messageId, int operatorId)
        {
            _message.InsertMessageReaded(messageId, operatorId);
        }

        public void LogException(string page, string operatorName, string functionName, Exception ex)
        {
            string errorMessage = ex == null ? "" : ex.ToString();
            string message = string.Format("[{0}] Error occurred in page {1} at function {2} by {3} Details:{4}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), page, functionName, operatorName, errorMessage);
            DbHelperSQL.ExecuteSQL("Insert into sysLog(Message) values('" + message.Replace("'","") + "');");
        }
        #endregion

        #region 权限管理
        /// <summary>
        /// 服务器端设置用户页面的按钮权限
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="context">HttpContext</param>
        /// <param name="objectIDArray">操作员权限集合</param>
        /// <param name="toolBar">ToolBar</param>
        /// <returns></returns>
        public virtual string CheckPurview(Page page, HttpContext context, string objectIDArray, Control toolBar)
        {
            int pageID = this.GetPageID(context);
            string sHtml = string.Empty;
            if (!StringHelper.IsValidDBInt(pageID) || objectIDArray == "")
                return null;

            IList<sysObjectPurview> objectPurviewList = _authorization.GetObjectSTRPagePurview(objectIDArray, pageID);
            if (objectPurviewList.Count <= 0)
            {
                objectPurviewList = new List<sysObjectPurview>();
                sysObjectPurview p = new sysObjectPurview();
                p.FunctionCount = 0;
                p.PurviewCode = 0;
                objectPurviewList.Add(p);
            }
            
            IList<sysFunction> sysFunctionList = _authorization.GetAllSysFunctionList();

            IDictionary<long, long> htFunctionPurviewCode = new Dictionary<long, long>();

            //权限组合
            foreach (sysObjectPurview objectPurview in objectPurviewList)
            {
                //操作权限码
                foreach (sysFunction func in sysFunctionList)
                {
                    //如果操作员具有这个操作
                    if ((objectPurview.PurviewCode & func.FunctionPurviewCode) > 0)
                    {
                        //如果之前还没有包含进来，则包含进去
                        if (!htFunctionPurviewCode.ContainsKey(func.FunctionPurviewCode))
                        {
                            htFunctionPurviewCode[func.FunctionPurviewCode] = func.FunctionPurviewCode;
                        }
                    }
                }
            }

            foreach (sysFunction func in sysFunctionList)
            {
                //如果在操作员的所有按钮集中不包含该按钮，则屏蔽掉
                if (!htFunctionPurviewCode.ContainsKey(func.FunctionPurviewCode))
                {
                    Control c = toolBar.FindControl("lb_" + func.FunctionCode);
                    if (c != null)
                    {
                        c.Visible = false;
                    }
                }
            }


            string[] ids = objectIDArray.Split(',');
            if (ids.Length > 0)
            {
                Operators op = org.GetOperatorsDetail(int.Parse(ids[0]));
                Control c1 = toolBar.FindControl("lb_DesignLayout");
                Control c2 = toolBar.FindControl("lb_DesignView");
                if (c1 != null)
                {
                    //如果不是管理员（开发员有），没有设计布局和视图的权限
                    if (!op.opeIsAdmin && !op.opeIsDeveloper)
                    {
                        c1.Visible = false;
                        c2.Visible = false;
                    }
                }
            }

            return sHtml;
        }

        public virtual string CheckPurview2(Page page, HttpContext context, string objectIDArray, Control toolBar)
        {
            int pageID = this.GetPageID(context);
            string sHtml = string.Empty;
            string[] ids = objectIDArray.Split(',');
            if (ids.Length > 0)
            {
                Operators op = org.GetOperatorsDetail(int.Parse(ids[0]));
                Control c1 = toolBar.FindControl("lb_DesignLayout");
                Control c2 = toolBar.FindControl("lb_DesignView");
                if (c1 != null)
                {
                    //如果不是管理员（开发员有），没有设计布局和视图的权限
                    if (!op.opeIsAdmin && !op.opeIsDeveloper)
                    {
                        c1.Visible = false;
                        c2.Visible = false;
                    }
                }
            }

            return sHtml;
        }

        public bool HasViewAllCustomer(string operatorID)
        {
            Operators op = org.GetOperatorsDetail(int.Parse(operatorID));
            if (op.opeDataRange.HasValue && op.opeDataRange.Value == 3)
            {
                return true;
            }
            return false;
        }

        public bool HasViewDepartmentCustomer(string operatorID)
        {
            Operators op = org.GetOperatorsDetail(int.Parse(operatorID));
            if (op.opeDataRange.HasValue && op.opeDataRange.Value >= 2)
            {
                return true;
            }
            return false;
        }

        public bool HasViewSpecial(Page page, HttpContext context, string objectIDArray, long functionCode)
        {
            int pageID = this.GetPageID(context);
            string sHtml = string.Empty;
            if (!StringHelper.IsValidDBInt(pageID) || objectIDArray == "")
                return false;

            IList<sysObjectPurview> objectPurviewList = _authorization.GetObjectSTRPagePurview(objectIDArray, pageID);
            if (objectPurviewList.Count <= 0)
            {
                throw new ApplicationException("您无权访问该页！");
            }
            else
            {
                //权限组合
                foreach (sysObjectPurview objectPurview in objectPurviewList)
                {
                    //如果操作员具有该操作
                    if ((objectPurview.PurviewCode & functionCode) > 0)
                    {
                        return true;
                    }
                }


            }

            return false;
        }

        public bool CanViewCustomer(int operatorID, int customerID, string Ids)
        {
            //先不控制
            return true;
            Operators op = org.GetOperatorsDetail(operatorID);
            if (op.opeIsAdmin)
            {
                return true;
            }

            bool hasViewAll = this.HasViewAllCustomer(operatorID.ToString());
            if (hasViewAll)
            {
                return true;
            }

            //检测是否在公共池中，公共池中的客户，任何人都可见
            IList<Customer> listCustomer = _baseservice.SelectDynamic<Customer>(" AND cusID in (select cusID from XCustomerPool)");
            if (listCustomer != null && listCustomer.Count > 0)
                return true;

            string result = "";
            if (!hasViewAll)
            {
                bool hasViewDepartment = this.HasViewDepartmentCustomer(operatorID.ToString());
                string condition = "";
                condition = " OR opeID in (select SubordinateID from sysOperatorSubordinate where OperatorID = " + operatorID + ")";

                if (hasViewDepartment)
                {
                    result += "AND cusID = " + customerID + " AND (cusID IN " +
                                 " (select cusID from Customer where cusOperatorID in (select opeID from Operators where opeDepartmentID = '" + op.opeDepartmentID + "'" + condition + "))"
                                 + ")";
                }
                else
                {
                    result += "AND cusID = " + customerID + " AND (cusID IN " +
                                 " (select cusID from Customer where cusOperatorID  in(select opeID from Operators where opeID = " + operatorID.ToString() + condition + "))"
                                 + ")";
                }
            }

            listCustomer = _baseservice.SelectDynamic<Customer>(result);
            if (listCustomer != null && listCustomer.Count > 0)
                return true;
            else
                return false;
        }

        public string GetCondition(int operatorID)
        {
            return GetCondition("cusID", operatorID);
        }

        public string GetCondition(string customerField, int operatorID)
        {
            string result = string.Empty;
            Operators op = org.GetOperatorsDetail(operatorID);
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + operatorID + ")";

                if (hasViewDepartment)
                {
                    result += " AND (" + customerField + " IN " +
                                 " (select cusID from Customer where cusOperatorID in (select opeID from Operators where opeDepartmentID = '" + op.opeDepartmentID + "'" + condition + "))"
                                 + ")";
                }
                else
                {
                    result += " AND (" + customerField + " IN " +
                                 " (select cusID from Customer where cusOperatorID  in(select opeID from Operators where opeID = " + operatorID.ToString() + condition + "))"
                                 + ")";
                }
            }
            return result;
        }

        public string GetWhereConditionForUser(string userField, int operatorID, string department)
        {
            string result = "";
            Operators op = org.GetOperatorsDetail(operatorID);
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + operatorID + ")";
                if (hasViewDepartment)
                {
                    result = " AND (" + userField + " in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + "))";
                }
                else
                {
                    result = " AND (" + userField + " in (select opeID from Operators where opeID = " + operatorID + condition + "))";
                }
            }
            return result;
        }

        public string GetWhereConditionForUser(string userField, int operatorID, string department, string addUserField)
        {
            string result = "";
            Operators op = org.GetOperatorsDetail(operatorID);
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + operatorID + ")";
                if (hasViewDepartment)
                {
                    result = " AND (" + userField + " in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + "))";
                }
                else
                {
                    result = " AND (" + userField + " in (select opeID from Operators where opeID = " + operatorID + condition + ") OR " + addUserField + "=" + operatorID + ")";
                }
            }
            return result;
        }

        public string GetWhereConditionForCustomer(Operators op)
        {
            string result = "";
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + op.opeID + ")";
                if (hasViewDepartment)
                {
                    result = " AND (cusID in (select cwRelatedID from CoWorker where cwSource='Customer' and cwOperatorID = " + op.opeID + " ) or (cusOperatorID in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + ")))";
                }
                else
                {
                    result = " AND (cusID in (select cwRelatedID from CoWorker where cwSource='Customer' and cwOperatorID = " + op.opeID + " ) or (cusOperatorID in (select opeID from Operators where opeID = " + op.opeID + condition + ")))";
                }
            }
            return result;
        }

        public string GetWhereConditionForFeedback(Operators op)
        {
            string result = "";
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + op.opeID + ")";
                if (hasViewDepartment)
                {
                    result = " AND ((cfbHandleOperatorID = " + op.opeID + ") or (cfbOperatorID in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + ")))";
                }
                else
                {
                    result = " AND ((cfbHandleOperatorID = " + op.opeID + ") or (cfbOperatorID in (select opeID from Operators where opeID = " + op.opeID + condition + ")))";
                }
            }
            return result;
        }

        public string GetWhereConditionForBusiness(Operators op)
        {
            string result = "";
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + op.opeID + ")";
                if (hasViewDepartment)
                {
                    result = " AND (cbID in (select cwRelatedID from CoWorker where cwSource='Business' and cwOperatorID = " + op.opeID + " ) or (cbOperatorID in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + ")))";
                }
                else
                {
                    result = " AND (cbID in (select cwRelatedID from CoWorker where cwSource='Business' and cwOperatorID = " + op.opeID + " ) or (cbOperatorID in (select opeID from Operators where opeID = " + op.opeID + condition + ")))";
                }
            }
            return result;
        }

        public string GetWhereConditionForOpptunity(Operators op)
        {
            string result = "";
            bool hasViewAll = (op.opeDataRange.HasValue && op.opeDataRange.Value == 3);
            if (!hasViewAll)
            {
                bool hasViewDepartment = (op.opeDataRange.HasValue && op.opeDataRange.Value == 2);
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + op.opeID + ")";
                if (hasViewDepartment)
                {
                    result = " AND (coID in (select cwRelatedID from CoWorker where cwSource='Opptunity' and cwOperatorID = " + op.opeID + " ) or (coOperatorID in (select opeID from Operators where opeDepartmentID = " + op.opeDepartmentID + condition + ")))";
                }
                else
                {
                    result = " AND (coID in (select cwRelatedID from CoWorker where cwSource='Opptunity' and cwOperatorID = " + op.opeID + " ) or (coOperatorID in (select opeID from Operators where opeID = " + op.opeID + condition + ")))";
                }
            }
            return result;
        }
        /// <summary>
        /// 根据页面得到PageID，用于页面权限验证时
        /// </summary>
        /// <param name="Container"></param>
        /// <returns>PageID</returns>
        private int GetPageID(HttpContext context)
        {
            String strUrl;
            string applicationPath = context.Request.ApplicationPath;

            if (applicationPath == "/")
            {
                strUrl = context.Request.Url.AbsolutePath.Substring(1);
            }
            else
            {
                strUrl = context.Request.Url.AbsolutePath.Substring(applicationPath.Length + 1);
            }
            int pageID = _authorization.GetsysPagesByPageFilePath(strUrl);
            if (pageID > 0)
            {
                return pageID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 是否有进入本页的权限
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Ids"></param>
        public void CheckPagePermission(HttpContext context, string Ids)
        {

            IList<sysObjectPurview> soplist = _authorization.GetObjectSTRPagePurview(Ids, this.GetPageID(context));
            if (soplist != null && soplist.Count > 0)
            {
                long functionCount = 0;
                foreach (sysObjectPurview sop in soplist)
                {
                    functionCount += sop.FunctionCount;
                }

                if (functionCount <= 0)
                {
                    throw new ApplicationException("你无权操作！");
                }
            }
            else
            {
                throw new ApplicationException("你无权操作！");
            }
        }
        #endregion
    }
}
