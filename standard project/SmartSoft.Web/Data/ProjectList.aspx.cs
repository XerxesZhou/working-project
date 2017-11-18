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

    

    public partial class ProjectList : PageBaseList
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

        protected string title = "项目报备列表";
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
                //SmartSoft.Component.DbHelperSQL.ExecuteSQL("Delete from Project where ISNULL(pjApproveTag,'未审核') <> '已审核' and pjID in (" + this.hfSelectCheck.Value + ")");

                string message = _datacontroller.Delete("Project", this.hfSelectCheck.Value, CurrentOperatorID);
                this.AspNetPager1.CurrentPageIndex = 0;
                bindGrid();
                //this.Show(message);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lb_Delete_Click", ex);
                bindGrid();
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

            //发短信
            this.ToolBar1.lb_SendMobileMessage.Visible = true;
            this.ToolBar1.lb_SendMobileMessage.Attributes.Add("href", "javascript:ShowSendMobileMessage();");

            //导入
            //this.ToolBar1.lb_Import.Click += lb_Import_Click;
            this.ToolBar1.lb_Import.Visible = true;
            this.ToolBar1.lb_Import.Attributes.Add("href", "javascript:ShowFileUpload();");
           
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

            base.requiredColumn = "pjID";
            base.isSum = false;
            base.Grid = this.GridProject;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            //base.whereCondition += _datacontroller.GetWhereConditionForUser("pjOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "");
           
            if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
            {
                string k = this.txtKeyWord.Text;
                base.whereCondition += string.Format(" AND (pjNO like '%{0}%' OR pjName like  '%{0}%' OR pjCompanyName like  '%{0}%' OR pjDetail  like  '%{0}%' OR pjProduct like  '%{0}%' OR pjContactor like '%{0}%')", k);
            }
            if (!string.IsNullOrEmpty(ddlpjApproveTag.SelectedValue))
            {
                if (ddlpjApproveTag.SelectedValue == "已审核")
                {
                    whereCondition += " AND pjApproveTag = '已审核'";
                }
                else
                {
                    whereCondition += " AND ISNULL(pjApproveTag,'') = ''";
                }
            }
            base.isSort = true;
            base.checkBoxCss = "";
             
            this.initToolBar();
        }


        private void BindControls()
        {
            //绑定DropDownList
            IList<Operators> odlist = _org.GetOperatorsList();
            //(new WebFunc()).BindListControl<Operators>(odlist, ddlOperators, "opeName", "opeID", "选择业务员", "");
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
                msg = _datacontroller.Import(fullFilePath, this.Operator.opeID, "Project","pj");
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


