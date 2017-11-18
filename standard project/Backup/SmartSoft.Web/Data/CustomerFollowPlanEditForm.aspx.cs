/***************************************************************************
 * 
 *       功能：     客户跟进历史项目编辑
 *       作者：     朱少军
 *       日期：     2009-08-07
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

    public partial class CustomerFollowPlanEditForm : PageBaseEdit
    {
        #region Fields
        private int rowID
        {
            get
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["ID"]);
                }
                return 0;
            }
        }

        private int FollowType
        {
            get
            {
                if (Request.QueryString["FollowType"] != null && Request.QueryString["FollowType"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["FollowType"]);
                }
                return 0;
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

        public int? MessageID
        {
            get
            {
                if (Request.QueryString["MessageID"] != null && Request.QueryString["MessageID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["MessageID"]);
                }
                else
                {
                    return null;
                }
            }

        }
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.MessageID.HasValue)
                {
                    int count = int.Parse(DbHelperSQL.GetSHSLInt("select count(*) from CustomerFollowPlan where cfpID = " + this.rowID));
                    if (count == 0)
                    {
                        _datacontroller.SetMessageIsRead(this.MessageID.Value, this.Operator.opeID);
                        WebFunc.AlertClose("跟进计划已被删除.", this.Page);
                        return;
                    }
                }

                AjaxPro.Utility.RegisterTypeForAjax(typeof(SmartSoft.Presentation.BaseController));
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                    bindSpecialData();
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        protected void btn_NoAlert_Click(object sender, EventArgs e)
        {
            try
            {
                _datacontroller.SetMessageIsRead(this.MessageID.Value, this.Operator.opeID);
                if (rowID > 0)
                {
                    WebFunc.SaveClose("操作成功！", Page);
                    AddOperatorLog("确认跟进计划已完成,不再提醒", rowID);
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "btn_NoAlert_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
            }
        }

        protected void btn_FollowNow_Click(object sender, EventArgs e)
        {
            try
            {
                _datacontroller.SetMessageIsRead(this.MessageID.Value, this.Operator.opeID);
                if (rowID > 0)
                {
                    WebFunc.ToPage("/Data/CustomerEditForm.aspx?Action=View&ID=" + rowID, this.Page);
                    AddOperatorLog("确认跟进计划已完成,不再提醒", rowID);
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "btn_FollowNow_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
            }
        }

        protected void lb_AlertLater_Click(object sender, EventArgs e)
        {
            try
            {
                int days = 0;
                int.TryParse(txtDay.Text, out days);
                int hours = 1;
                int.TryParse(txtHour.Text, out hours);

                DbHelperSQL.ExecuteSQL(string.Format("Update sysMessage set SendTime = '{0}',AwokeTime='{0}' where MessageID = {1}", DateTime.Now.AddDays(days).AddHours(hours).ToString("yyyy-MM-dd HH:mm"), this.MessageID.Value));

                if (rowID > 0)
                {
                    WebFunc.SaveClose("操作成功！", Page);
                    AddOperatorLog("跟进计划下次提示", rowID);
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_AlertLater_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
            }
        }
        
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void bindSpecialData()
        {
            if (rowID > 0) 
            {
                DataRow dr = DbHelperSQL.GetDataRow(@"select * from CustomerFollowPlan A 
left outer join CustomerClue B on A.cfpRelatedID =B.ccID and cfpSource='Clue'
left outer join Customer B1 on A.cfpRelatedID =B1.cusID and cfpSource='Customer'
left outer join CustomerOpptunity B2 on A.cfpRelatedID =B2.coID and cfpSource='Opptunity'
left outer join CustomerBusiness B3 on A.cfpRelatedID =B3.cbID and cfpSource='Business' where cfpID=" + rowID);
                lblcfpDate.InnerText = DateTime.Parse(dr["cfpDate"] + "").ToString("yyyy-MM-dd HH:mm");
                lblcfpContent.InnerText = dr["cfpContent"] + "";
                lblcfpFilePath.Text = GetEachFile(dr["cfpFilePath"] + "");
                string cfpSource = dr["cfpSource"] + "";
                if (cfpSource == "Clue")
                {
                    lblcfpFromName.InnerHtml = "关联线索：<a data-ajax='false' href='CustomerClueEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["ccName"] + "</a>";
                }
                else if (cfpSource == "Customer")
                {
                    lblcfpFromName.InnerHtml = "关联客户：<a data-ajax='false' href='CustomerEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["cusName"] + "</a>";
                }
                else if (cfpSource == "Opptunity")
                {
                    lblcfpFromName.InnerHtml = "关联商机：<a data-ajax='false' href='CustomerOpptunityEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["coName"] + "</a>";
                }
                else if (cfpSource == "Business")
                {
                    lblcfpFromName.InnerHtml = "关联合同：<a data-ajax='false' href='CustomerBusinessEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["cbName"] + "</a>";
                }
                hfcfpID.Value = rowID.ToString();
            }
        }

        public string GetEachFile(string filePath)
        {
            return CommonFunction.GetEachFileForWeb(filePath);
        }
        #endregion
    }
}
