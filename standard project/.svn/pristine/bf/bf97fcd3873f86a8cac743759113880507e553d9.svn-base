/***************************************************************************
 * 
 *       功能：     客户公共池
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

    public partial class CustomerPoolList : PageBaseList
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

        protected string title = "客户公共池";
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
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){ConvertToLink();})", true);
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
            this.ToolBar1.lb_CustomerPickup.Visible = true;
            this.ToolBar1.lb_CustomerPickup.Click += lb_CustomerPickup_Click;

            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += base.lb_Search_Click;

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;

            //转移
            this.ToolBar1.lb_Change.Visible = true;
            this.ToolBar1.lb_Change.Attributes.Add("href", "javascript:ShowChangeOperator();");

        }

      

        /// <summary>
        /// 从公共池获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_CustomerPickup_Click(object sender, EventArgs e)
        {
            try
            {
                IList<Customer> listCustomer = _datacontroller.SelectDynamic<Customer>(" AND cusID in (" + this.hfSelectCheck.Value + ")");
                foreach (Customer cus in listCustomer)
                {
                    cus.cusOperatorID = this.Operator.opeID;
                    cus.cusDepartmentID = this.Operator.opeDepartmentID;
                    _datacontroller.Update<Customer>(cus);
                }

                base.bindGrid();
                AddOperatorLog("把客户放入公共池:" + this.hfSelectCheck.Value, 0);
                WebFunc.Alert("操作成功！", Page);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_CustomerRelease_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
                base.bindGrid();
            }
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


        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "cusID";
            base.isSum = false;
            base.Grid = this.GridCustomerPool;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            //公共池中的才显示
            base.whereCondition += " AND cusID in (select cusID from XCustomerPool)";

            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                base.whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR cusContactor  like '%{0}%' OR cusExtText1  like '%{0}%' OR cusExtText2  like '%{0}%' OR cusExtText3  like '%{0}%' OR cusID in (select clmCustomerID from CustomerLinkMan where clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%') OR cusID in (select cfCustomerID from CustomerFile where cfName like '%{0}%') OR cusID in (select cbCustomerID from CustomerBusiness where cbName like '%{0}%') OR cusID in (select cfhRelatedID from CustomerFollowHistory where cfhContent like '%{0}%') OR cusID in (select cfpRelatedID from CustomerFollowPlan where cfpContent like '%{0}%'))", k);
            }
            if (!string.IsNullOrEmpty(this.ddlcusAreaID.SelectedValue))
            {
                base.whereCondition += " AND cusAreaID in (select caID from defCustomerArea where caCode like '" + this.ddlcusAreaID.SelectedValue + "%')";
            }
            base.isSort = true;
            base.checkBoxCss = "";
             
            this.initToolBar();
        }


        private void BindControls()
        {
            //绑定DropDownList
            IList<Operators> odlist = _org.GetOperatorsList();
            (new WebFunc()).BindListControl<Operators>(odlist, ddlOperators, "opeName", "opeID", "选择业务员", "");
            DbHelperSQL.BindDropDownListAddEmpty("select caCode, caName from defCustomerArea order by caCode asc", this.ddlcusAreaID, "caName", "caCode");
        }
        #endregion

    }
}


