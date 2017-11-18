namespace SmartSoft.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using Castle.Facilities.AutomaticTransactionManagement;
    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;

    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Common;
    using SmartSoft.Component;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Persistence.Data;
    using SmartSoft.Service;
    using System.Data;
    using System.Collections;

    [Transactional]
    [UsesAutomaticSessionCreation]
    public class CommonController : BaseController
    {
        #region Fields
        private static BaseService _baseservice;
        private static UserDefineBaseService _userdefinebase;
        public BaseService baseservice
        {
            set { _baseservice = value; }
            get { return _baseservice; }
        }
        public new UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
            get { return _userdefinebase; }
        }
        public CommonController()
        {
        }
        #endregion

        #region EditForm
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="ctrl">布局控件</param>
        /// <param name="layoutID">布局ID</param>
        /// <param name="rowID">数据表ID</param>
        [Transaction(TransactionMode.Requires)]
        public virtual void Save<T>(FormAction action, Control ctrl, int layoutID, int rowID, int operatorID) where T : new()
        {
            if (action == FormAction.Update) //修改
            {
                T entity = _baseservice.GetDetail<T>(rowID);
                //从页面中得到系统字段
                _userdefinebase.BindControlsToObject(ctrl, entity);

                //增加系统字段的值
                _baseservice.Update<T>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                T entity = new T();
                //得到控件中的系统字段的值
                _userdefinebase.BindControlsToObject(ctrl, entity);

                //增加系统字段的值
                rowID = _baseservice.Add<T>(entity);
            }
            //处理自定义字段的值
            //string updateSql = _userdefinebase.GetCustomValueStringFromPage(ctrl, layoutID, rowID);
            //_userdefinebase.UpdateCustomValue(updateSql);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="ctrl">布局控件</param>
        /// <param name="rowID">数据表ID</param>
        /// <param name="operatorID"></param>
        [Transaction(TransactionMode.Requires)]
        public virtual void Save<T>(FormAction action, Control ctrl, int rowID, int operatorID) where T : new()
        {
            if (action == FormAction.Update) //修改
            {
                T entity = _baseservice.GetDetail<T>(rowID);
                //从页面中得到系统字段
                _userdefinebase.BindControlsToObject(ctrl, entity);

                //增加系统字段的值
                _baseservice.Update<T>(entity);
            }
            else if (action == FormAction.Insert) //增加
            {
                T entity = new T();
                //得到控件中的系统字段的值
                _userdefinebase.BindControlsToObject(ctrl, entity);

                //增加系统字段的值
                rowID = _baseservice.Add<T>(entity);
            }
        }
        #endregion

        #region List
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strListDelete">ID数组</param>
        [Transaction(TransactionMode.Requires)]
        public virtual string Delete<T>(string strListDelete, int viewID)
        {
            string message = string.Empty;
            string deleteID = string.Empty;
            string[] RowIDs = strListDelete.Split(",".ToCharArray());
            foreach (string key in RowIDs)
            {
                int id = int.Parse(key);
                T entity = _baseservice.GetDetail<T>(id);
                if (entity != null)
                {
                    _baseservice.Delete<T>(id);
                    string url = "Data/CustomerFollowPlanEditForm.aspx?Action=View&ID=" + id;
                    this.DeleteMessageByURL(url);
                    _userdefinebase.DeleteCustomValuesForRecord(viewID, id);
                    deleteID += "[" + id + "]";
                }
            }
            if (deleteID != "")
            {
                message += "成功删除：" + deleteID + "\r\n";
            }
            return message;
        }

        public virtual string Delete<T>(string strListDelete, int viewID, string url)
        {
            string message = string.Empty;
            string deleteID = string.Empty;
            string[] RowIDs = strListDelete.Split(",".ToCharArray());
            foreach (string key in RowIDs)
            {
                int id = int.Parse(key);
                T entity = _baseservice.GetDetail<T>(id);
                if (entity != null)
                {
                    _baseservice.Delete<T>(id);
                    this.DeleteMessageByURL(url + id);
                    _userdefinebase.DeleteCustomValuesForRecord(viewID, id);
                    deleteID += "[" + id + "]";
                }
            }
            if (deleteID != "")
            {
                message += "成功删除：" + deleteID + "\r\n";
            }
            return message;
        }

        public virtual string Delete<T>(int rowID)
        {
            string message = string.Empty;
            string deleteID = string.Empty;
            _baseservice.Delete<T>(rowID);
            if (deleteID != "")
            {
                message += "成功删除\r\n";
            }
            return message;
        }

        public virtual string Delete(string tableName, string strListDelete, string CurrentOperatorID)
        {
            string message = string.Empty;
            string deleteID = string.Empty;
            string[] RowIDs = strListDelete.Split(",".ToCharArray());
            bool hasPermission = false;
            string primaryKey = "";
            foreach (string key in RowIDs)
            {
                switch (tableName)
                {
                    case "CustomerBusiness":
                        hasPermission = CommonFunction.CheckPermissionForBusiness(key, CurrentOperatorID);
                        primaryKey = "cbID";
                        break;
                    case "CustomerClue":
                        hasPermission = CommonFunction.CheckPermissionForClue(key, CurrentOperatorID);
                        primaryKey = "ccID";
                        break;
                    case "CustomerFeedback":
                        hasPermission = CommonFunction.CheckPermissionForFeedback(key, CurrentOperatorID);
                        primaryKey = "cfbID";
                        break;
                    case "CustomerFollowHistory":
                        hasPermission = CommonFunction.CheckPermissionForFollowHistory(key, CurrentOperatorID);
                        primaryKey = "cfhID";
                        break;
                    case "CustomerFollowPlan":
                        hasPermission = CommonFunction.CheckPermissionForFollowPlan(key, CurrentOperatorID);
                        primaryKey = "cfpID";
                        break;
                    case "CustomerLinkMan":
                        string kk = DbHelperSQL.GetSHSL("select clmCustomerID from CustomerLinkMan where clmID = " + key);
                        hasPermission = CommonFunction.CheckPermissionForCustomer(kk, CurrentOperatorID);
                        primaryKey = "clmID";
                        break;
                    case "Customer":
                        hasPermission = CommonFunction.CheckPermissionForCustomer(key, CurrentOperatorID);
                        primaryKey = "cusID";
                        break;
                    case "CustomerOpptunity":
                        hasPermission = CommonFunction.CheckPermissionForOpptunity(key, CurrentOperatorID);
                        primaryKey = "coID";
                        break;
                    case "CustomerReceipt":
                        hasPermission = CommonFunction.CheckPermissionForReceipt(key, CurrentOperatorID);
                        primaryKey = "crID";
                        break;
                    case "CustomerReceiptPlan":
                        hasPermission = CommonFunction.CheckPermissionForReceiptPlan(key, CurrentOperatorID);
                        primaryKey = "crpID";
                        break;
                    case "Project":
                        hasPermission = CommonFunction.CheckPermissionForProject(key, CurrentOperatorID);
                        primaryKey = "pjID";
                        break;
                    case "WorkDiary":
                        hasPermission = CommonFunction.CheckPermissionForWorkDiary(key, CurrentOperatorID);
                        primaryKey = "wdID";
                        break;
                    case "MarketingActivity":
                        hasPermission = CommonFunction.CheckPermissionForMarketingActivity(key, CurrentOperatorID);
                        primaryKey = "maID";
                        break;
                }
                if (hasPermission)
                {
                    DbHelperSQL.ExecuteSQL("delete from " + tableName + " where " + primaryKey + "=" + key);
                    deleteID += "[" + key + "]";

                    if (tableName == "Customer")
                    {
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhSource = 'Customer' and cfhRelatedID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpSource='Customer' and cfpRelatedID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerLinkMan where clmCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerBusiness where cbCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerReceipt where crCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerReceiptPlan where crpCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFile where cfCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFeedback where cfbCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerOpptunity where coCustomerID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerOperatorHistory where coCustomerID in (" + key + ")");
                    }
                    else if (tableName == "CustomerOpptunity")
                    {
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhSource = 'Opptunity' and cfhRelatedID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpSource='Opptunity' and cfpRelatedID in (" + key + ")");
                    }
                    else if (tableName == "CustomerClue")
                    {
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhSource = 'Clue' and cfhRelatedID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpSource='Clue' and cfpRelatedID in (" + key + ")");
                    }
                    else if (tableName == "CustomerBusiness")
                    {
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhSource in('Business','AfterSales') and cfhRelatedID in (" + key + ")");
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowPlan where cfpSource in('Business','AfterSales') and cfpRelatedID in (" + key + ")");
                    }
                    else if (tableName == "MarketingActivity") 
                    {
                        SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from CustomerFollowHistory where cfhSource = 'MarketingActivity' and cfhRelatedID in (" + key + ")");
                    }
                }
            }
            if (deleteID != "")
            {
                message += "成功删除：" + deleteID + "\r\n";
                AddOperatorLog("成功删除" + tableName + "：" + deleteID);
            }
            return message;
        }
        #endregion

        #region Common

        /// <summary>
        /// 获得所有记录
        /// </summary>
        /// <returns></returns>
        public IList<T> SelectAll<T>()
        {
            return _baseservice.SelectAll<T>();
        }

        public IList<T> SelectDynamic<T>(string whereCondition)
        {
            return SelectDynamic<T>(whereCondition, "");
        }

        /// <summary>
        /// 动态获得符合条件的记录并排序
        /// </summary>
        /// <param name="whereCondition">选择条件如：" AND ID > 20"</param>
        /// <param name="orderByExpression">排序条件如："ID DESC"</param>
        /// <returns>实体记录集</returns>
        public IList<T> SelectDynamic<T>(string whereCondition, string orderByExpression)
        {
            return _baseservice.SelectDynamic<T>(whereCondition, orderByExpression);
        }

        public DataTable SelectForDataTable(string statementName, object objectParameter, Hashtable htOutPutParameters)
        {
            return _baseservice.SelectForDataTable(statementName, objectParameter, htOutPutParameters);
        }

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="objUser">新增的记录实体</param>
        /// <returns>新增的记录编号</returns>
        public int Add<T>(T obj)
        {
            return _baseservice.Add<T>(obj);
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="objUser">修改的记录实体</param>
        /// <returns>修改是否成功</returns>
        public void Update<T>(T obj)
        {
            _baseservice.Update<T>(obj);
        }


        /// <summary>
        /// 选择记录
        /// </summary>
        /// <param name="rowID">记录编号</param>
        /// <returns>记录实体对象</returns>
        public T GetDetail<T>(int rowID)
        {
            return _baseservice.GetDetail<T>(rowID);
        }

        public bool BatchDelete<T>(string ids)
        {
            return _baseservice.BatchDelete<T>(ids);
        } 
        #endregion
    }
}

