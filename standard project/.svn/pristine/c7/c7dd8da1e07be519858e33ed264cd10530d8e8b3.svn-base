/***************************************************************************
 * 
 *       功能：     客户资料
 *       作者：     朱少军
 *       日期：     2009-7-17
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
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using UDEF.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Component;
    using Wuqi.Webdiyer;
    using SmartSoft.Presentation;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Common;
    using System.Collections.Generic;
    using SmartSoft.Service.Implement.Common;
    using System.IO;

    

    public partial class CustomerList : PageBaseList
    {
        #region Fields
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "客户资料列表";
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                
                if (!IsPostBack)
                {
                    BindControls();
                }
                
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                this.Show("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.");
            }
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){ConvertToLink();})", true);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string message = _datacontroller.Delete("Customer", this.hfSelectCheck.Value, CurrentOperatorID);
                this.AspNetPager1.CurrentPageIndex = 0;
                bindGrid();
                this.Show(message);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Delete_Click", ex);
                bindGrid();
                this.Show("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.");
            }
        }

        /// <summary>
        /// 放入公共池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_CustomerRelease_Click(object sender, EventArgs e)
        {
            try
            {
                IList<Customer> listCustomer = _datacontroller.SelectDynamic<Customer>(" AND cusID in (" + this.hfSelectCheck.Value + ")");
                foreach (Customer cus in listCustomer)
                {
                    cus.cusOperatorID = null;
                    cus.cusDepartmentID = null;
                    cus.cusExpiredDate = null;
                    _datacontroller.Update<Customer>(cus);

                    CustomerFollowHistory entity = new CustomerFollowHistory();
                    entity.cfhAddDate = DateTime.Now;
                    entity.cfhAddOperatorID = this.Operator.opeID;
                    entity.cfhContent = "从客户列表放入公海";
                    entity.cfhDate = DateTime.Now;
                    entity.cfhOperatorID = this.Operator.opeID;
                    entity.cfhRelatedID = cus.cusID;
                    entity.cfhSource = "Customer";
                    _datacontroller.Add<CustomerFollowHistory>(entity);
                }

                bindGrid();
                AddOperatorLog("把客户放入公共池:" + this.hfSelectCheck.Value, 0);
                this.Show("操作成功！");
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_CustomerRelease_Click", ex);
                this.Show("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.");
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.bindGrid();
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //增加
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Insert();");
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += base.lb_Search_Click;
            //删除
            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "javascript:return Delete();");
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export2_Click;

            //转移
            this.ToolBar1.lb_Change.Visible = true;
            this.ToolBar1.lb_Change.Attributes.Add("href", "javascript:ShowChangeOperator();");

            //放入公共池
            this.ToolBar1.lb_CustomerRelease.Visible = true;
            //this.ToolBar1.lb_CustomerRelease.Attributes.Add("onclick", "javascript:return window.confirm('确定放入客户公共池?');");
            this.ToolBar1.lb_CustomerRelease.Attributes.Add("onclick", "javascript:IsPutCustomerToPool();");
            this.ToolBar1.lb_CustomerRelease.Click += lb_CustomerRelease_Click;

            //发短信
            this.ToolBar1.lb_SendMobileMessage.Visible = true;
            this.ToolBar1.lb_SendMobileMessage.Attributes.Add("href", "javascript:ShowSendMobileMessage();");

            //导入
            //this.ToolBar1.lb_Import.Click += lb_Import_Click;
            this.ToolBar1.lb_Import.Visible = true;
            this.ToolBar1.lb_Import.Attributes.Add("href", "javascript:ShowFileUpload();");
           
        }

        /// <summary>
        /// 转移客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ConfirmChangeOperator_Click(object sender, EventArgs e)
        {
            try
            {
                string changeTOOperator = ddlOperators.SelectedValue;
                string[] sqls = new string[2];
                string department = DbHelperSQL.GetSHSL("select opeDepartmentID from Operators where opeID=" + ddlOperators.SelectedValue);

                sqls[0] = "insert into CustomerOperatorHistory(coCustomerID,coOperatorID,coAddOperatorID, coAddDate,coRemark) Select cusID, cusOperatorID, " + this.Operator.opeID + ", getdate(),'从客户列表转移' from Customer where cusID in (" + this.hfSelectCheck.Value + ") and cusOperatorID <> " + ddlOperators.SelectedValue;
                sqls[1] = "Update Customer set cusOperatorID = " + changeTOOperator + ", cusDepartmentID =" + department + " where cusID in (" + this.hfSelectCheck.Value + ") and cusOperatorID <> " + ddlOperators.SelectedValue;
                AddOperatorLog("转移客户至" + ddlOperators.SelectedValue + ":" + this.hfSelectCheck.Value, 0);
                IList<Customer> listCustomer = _datacontroller.SelectDynamic<Customer>(" AND cusID in (" + this.hfSelectCheck.Value + ") and cusOperatorID <> " + ddlOperators.SelectedValue);
                bool success = DbHelperSQL.ExecuteSQL(sqls);
                if (success)
                {
                    if (listCustomer.Count > 0)
                    {
                        string newOperatorName = DbHelperSQL.GetSHSL("select opeName from Operators where opeID = " + ddlOperators.SelectedValue);
                        foreach (Customer cus in listCustomer)
                        {
                            CustomerFollowHistory entity = new CustomerFollowHistory();
                            entity.cfhAddDate = DateTime.Now;
                            entity.cfhAddOperatorID = this.Operator.opeID;
                            entity.cfhContent = this.Operator.opeName + "从客户列表把客户转移给了" + newOperatorName;
                            entity.cfhDate = DateTime.Now;
                            entity.cfhOperatorID = this.Operator.opeID;
                            entity.cfhRelatedID = cus.cusID;
                            entity.cfhSource = "Customer";
                            _datacontroller.Add<CustomerFollowHistory>(entity);
                        }

                        bindGrid();
                        this.ExecuteJavascript("alert('操作成功!');var divfade = document.getElementById('fade');divfade.style.display = 'none';var divChange = document.getElementById('divChange'); divChange.style.display = 'none';__doPostBack('ToolBar1$lb_Search','');");
                        _datacontroller.AddMessages(this.Operator.opeID, int.Parse(ddlOperators.SelectedValue), "客户转移通知", this.Operator.opeName + "从客户列表把客户转移给了你，快去看看吧", "Data/CustomerList.aspx?mytype=1", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                else
                {
                    this.Show("操作失败！");
                }
            }
            catch (Exception ex)
            {
                this.Show("操作失败！");
            }
        }

        protected void btn_ConfirmSendMobileMessage_Click(object sender, EventArgs e)
        {
            try
            {
                //添加主记录
                string maxID = DbHelperSQL.GetSHSL("Select ISNULL(MAX(ID),0) + 1 AS C from SmsMain");
                string sql = string.Format("insert into SmsMain(ID,Content,AddOperatorID,AddDate,SendDate) values({0},'{1}',{2},'{3}','{4}')",
                   maxID, txtMobileMessage.Text, this.Operator.opeID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), this.txtSendDate.Text);
                DbHelperSQL.ExecuteSQL(sql);

                sql = "insert into SmsItem(SmsID, CustomerID,LinkManID,LinkManName,MobilePhone,Status,TryTotalTime) select "
                    + maxID
                    + ", clmCustomerID,clmID, clmName,clmMobile, 0, 0 from CustomerLinkMan where len(ltrim(rtrim(clmMobile))) > 0 and clmCustomerID in ("
                    + this.hfSelectCheck.Value + ")";

                DbHelperSQL.ExecuteSQL(sql);
                bindGrid();
                this.ExecuteJavascript("alert('操作成功！已经放入短信待发送队列。!');var divfade = document.getElementById('fade');divfade.style.display = 'none';var divSendMobileMessage = document.getElementById('divSendMobileMessage'); divSendMobileMessage.style.display = 'none';__doPostBack('ToolBar1$lb_Search','');");
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "btn_ConfirmSendMobileMessage_Click", ex);
                this.Show("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.");
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "cusID";
            base.isSum = false;
            base.Grid = this.GridCustomer;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.whereCondition += _datacontroller.GetWhereConditionForCustomer(this.Operator);
           
            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;

                if (ConfigurationManager.AppSettings["OpenExtOrder"] + "" == "1")
                {
                    string cusCNs = "'kkk'";
                    DataTable dt2 = DbHelperSQL.GetDataTable(string.Format("select CustNO from v_SellOrderItem where FBillNoDD like '%{0}%' or FItemIDNumber like '%{0}%' or FItemIDName like '%{0}%' or Fmodel like  '%{0}%' or FItemIDName like '%{0}%'", k));
                    foreach (DataRow r in dt2.Rows)
                    {
                        cusCNs += ",'" + r["CustNO"] + "'";
                    }
                    whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%' or cbName in (select FBillNoDD from v_SellOrderItem where FItemIDNumber like '%{0}%' or FItemIDName like '%{0}%' or Fmodel like  '%{0}%' or FItemIDName like '%{0}%')) OR cusCN in ({1}))", k, cusCNs);
                }
                else
                {
                    whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%'))", k);
                }
            }

            if (!string.IsNullOrEmpty(this.ddlcusAreaID.SelectedValue))
            {
                base.whereCondition += " AND cusAreaID in (select caID from defCustomerArea where caCode like '" + this.ddlcusAreaID.SelectedValue + "%')";
            }

            if (!string.IsNullOrEmpty(this.ddl_FollowStatus.SelectedValue))
            {
                base.whereCondition += "AND dateadd(day, " + this.ddl_FollowStatus.SelectedValue + ", ISNULL(cusLastFollowDate,cusAddDate)) < getdate()";
            }

           

            base.isSort = true;
            base.checkBoxCss = "";
             
            this.initToolBar();

            string conditionType = Request["ConditionType"] + "";
            string conditionValue = Request["ConditionValue"] + "";
            if (conditionType != "")
            {
                this.TableSearch.Visible = false;
                switch (conditionType)
                {
                    case "NDayNotFollowCustomer":
                        whereCondition += " AND  dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID;
                        break;
                    case "NDayNotFollowNotSuccessCustomer":
                        whereCondition += " AND  cusStatusID in (61,62,63) and dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID;
                        break;
                    case "NDayNotSuccessCustomer":
                        whereCondition += " AND  cusStatusID in (61,62,63) and dateadd(day," + conditionValue + ",cusAddDate) < getdate() and cusOperatorID=" + CurrentOperatorID;
                        break;
                    case "NDayNotFollowSuccessCustomer":
                        whereCondition += " AND cusStatusID = 64 and dateadd(day," + conditionValue + ",isnull(cusLastFollowDate,cusAddDate)) < getdate() and cusOperatorID=" + CurrentOperatorID;
                        break;
                }
            }
            else
            {
                //公共池中的不显示
                base.whereCondition += " AND cusID not in (select cusID from XCustomerPool)";
            }
        }


        private void BindControls()
        {
            //绑定DropDownList
            IList<Operators> odlist = _org.GetOperatorsList();
            (new WebFunc()).BindListControl<Operators>(odlist, ddlOperators, "opeName", "opeID", "选择业务员", "");
            DbHelperSQL.BindDropDownListAddEmpty("select caCode, caName from defCustomerArea order by caCode asc", this.ddlcusAreaID, "caName", "caCode");

        }
        #endregion

        protected void btn_FiUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                string fileExtName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.') + 1);
                if (fileExtName.ToLower() != "xls")
                {
                    bindGrid();
                    this.Show("请选择导入的Excel文件!");
                    return;

                }
                string fullFilePath = System.Web.HttpContext.Current.Request.MapPath("../UploadFile/CustomerFile/") + FileUpload1.PostedFile.FileName;
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
                FileUpload1.PostedFile.SaveAs(fullFilePath);
                msg = _datacontroller.Import(fullFilePath, this.Operator.opeID, "Customer","cus");
                if (msg == "")
                {
                    bindGrid();
                    ScriptManager.RegisterStartupScript(up1, this.GetType(), "ShowMe", "alert('导入成功.');CloseDiv('divFileUpload');$(function(){ConvertToLink();});", true);
                }
                else
                {
                    bindGrid();
                    ScriptManager.RegisterStartupScript(up1, typeof(UpdatePanel), "ShowMe", "alert('导入失败原因：" + msg + "');CloseDiv('divFileUpload');$(function(){ConvertToLink();});", true);
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Import_Click", ex);
                ScriptManager.RegisterStartupScript(up1, typeof(UpdatePanel), "ShowMe", "alert('导入失败原因：" + ex.ToString() + "');CloseDiv('divFileUpload');", true);
            }
        }

    }
}


