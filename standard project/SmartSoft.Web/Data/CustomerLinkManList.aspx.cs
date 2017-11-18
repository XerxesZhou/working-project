/***************************************************************************
 * 
 *       功能：     客户联系人资料
 *       作者：     朱少军
 *       日期：     2012-07-19
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

    public partial class CustomerLinkManList : PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "客户联系人列表";

        private int CustomerID
        {
            get
            {
                if (Request.QueryString["CustomerID"] != null && Request.QueryString["CustomerID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["CustomerID"]);
                }
                return 0;
            }
        }
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                base.Page_Load(sender, e);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "alert", "$(function(){ConvertToLink();})", true);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
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
                string message = _datacontroller.Delete("CustomerLinkMan", this.hfSelectCheck.Value, CurrentOperatorID);
                this.AspNetPager1.CurrentPageIndex = 0;
                base.bindGrid();
                WebFunc.Alert(message, Page);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Delete_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
                base.bindGrid();
            }
        }


        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.bindGrid();
        }

        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                //添加Email主记录
                string maxID = DbHelperSQL.GetSHSL("Select ISNULL(MAX(ID),0) + 1 AS C from EmailMain");
                string sql = string.Format("insert into EmailMain(ID,Subject,Content,AddOperatorID,AddDate) values({0},'{1}','{2}',{3},'{4}')",
                   maxID, txtSubject.Text, TxtContent.Text, this.Operator.opeID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DbHelperSQL.ExecuteSQL(sql);

                sql = "insert into EmailItem(EmailID, CustomerID,LinkManID,LinkManName,EmailAddress,Status,TryTotalTime) select "
                    + maxID
                    + ", clmCustomerID,clmID, clmName,clmEmail, 0, 0 from CustomerLinkMan where len(ltrim(rtrim(clmEmail))) > 0 and clmID in ("
                    + this.hfSelectCheck.Value + ")";

                DbHelperSQL.ExecuteSQL(sql);

                this.AspNetPager1.CurrentPageIndex = 0;
                base.bindGrid();
                WebFunc.ExecuteAjaxJScript("alert('操作成功！已经放入邮件待发送队列。!');var divfade = document.getElementById('fade');divfade.style.display = 'none'; var divSendEmail = document.getElementById('divSendEmail'); divSendEmail.style.display = 'none';__doPostBack('ToolBar1$lb_Search','');", Page);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "btn_Confirm_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
                base.bindGrid();
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
                    + ", clmCustomerID,clmID, clmName,clmMobile, 0, 0 from CustomerLinkMan where len(ltrim(rtrim(clmMobile))) > 0 and clmID in ("
                    + this.hfSelectCheck.Value + ")";

                DbHelperSQL.ExecuteSQL(sql);

                this.AspNetPager1.CurrentPageIndex = 0;
                base.bindGrid();
                WebFunc.ExecuteAjaxJScript("alert('操作成功！已经放入短信待发送队列。!'); var divfade = document.getElementById('fade');divfade.style.display = 'none'; var divSendMobileMessage = document.getElementById('divSendMobileMessage'); divSendMobileMessage.style.display = 'none';__doPostBack('ToolBar1$lb_Search','');", Page);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "btn_ConfirmSendMobileMessage_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
                base.bindGrid();
            }
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

            //发邮件
            this.ToolBar1.lb_SendEmail.Visible = true;
            this.ToolBar1.lb_SendEmail.Attributes.Add("href", "javascript:ShowSendEmail();");

            //发短信
            this.ToolBar1.lb_SendMobileMessage.Visible = true;
            this.ToolBar1.lb_SendMobileMessage.Attributes.Add("href", "javascript:ShowSendMobileMessage();");

            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;

            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;

            this.ToolBar1.lb_Close.Visible = true;
            this.ToolBar1.lb_Close.Attributes.Add("href", "javascript:window.close();");


        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {

            base.requiredColumn = "clmID";

            base.Grid = this.GridCustomerLinkMan;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();

            if (CustomerID > 0)
            {
                this.whereCondition += " AND clmCustomerID = " + CustomerID;
            }
            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                base.whereCondition += string.Format(" AND (cusCN like '%{0}%' OR cusName like  '%{0}%' OR cusEnglishName like  '%{0}%' OR cusTel  like  '%{0}%' OR cusFax like  '%{0}%' OR cusEmail like '%{0}%' OR cusWebsite like '%{0}%' OR cusAddress  like '%{0}%' OR clmName like '%{0}%' OR clmTel like '%{0}%' OR clmMobile like '%{0}%' OR clmEmail like '%{0}%' OR clmQQ like '%{0}%')", k);
            }

            if (!string.IsNullOrEmpty(this.ddlStartMonth.SelectedValue))
            {
                base.whereCondition += " AND MONTH(clmBirthday) >= " + this.ddlStartMonth.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.ddlStartDay.SelectedValue))
            {
                base.whereCondition += " AND Day(clmBirthday) >= " + this.ddlStartDay.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.ddlEndMonth.SelectedValue))
            {
                base.whereCondition += " AND MONTH(clmBirthday) <= " + this.ddlEndMonth.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.ddlEndDay.SelectedValue))
            {
                base.whereCondition += " AND Day(clmBirthday) <= " + this.ddlEndDay.SelectedValue;
            }

            base.whereCondition += _datacontroller.GetWhereConditionForUser("cusOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "");

            string conditionType = Request["ConditionType"] + "";
            string conditionValue = Request["ConditionValue"] + "";
            if (conditionType != "")
            {
                this.TableSearch.Visible = false;
                switch (conditionType)
                {
                    case "NDayBirthdayLinkMan":
                        string startBirthMonth = DateTime.Now.Month.ToString();
                        string startBirthDay = DateTime.Now.Day.ToString();
                        string endBirthMonth = DateTime.Now.AddDays(int.Parse(conditionValue) - 1).Month.ToString();
                        string endBirthDay = DateTime.Now.AddDays(int.Parse(conditionValue) - 1).Day.ToString();
                        whereCondition += string.Format(" AND month(clmBirthDay) >= '{0}' and day(clmBirthDay) >= '{1}' and month(clmBirthday) <= '{2}' and day(clmBirthday) <='{3}' and clmCustomerID in (select cusID from Customer where cusOperatorID = {4})", startBirthMonth, startBirthDay, endBirthMonth, endBirthDay, CurrentOperatorID);
                        break;

                }
            }
        }
        #endregion

    }
}


