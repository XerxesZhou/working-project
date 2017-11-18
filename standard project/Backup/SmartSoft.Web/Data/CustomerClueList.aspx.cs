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



    public partial class CustomerClueList : PageBaseList
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

        protected string title = "线索资料列表";
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
                string message = _datacontroller.Delete("CustomerClue", this.hfSelectCheck.Value, CurrentOperatorID); 
                this.AspNetPager1.CurrentPageIndex = 0;
                bindGrid();
                AddOperatorLog("删除线索资料:" + this.hfSelectCheck.Value, 0);
                this.Show(message);
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
                
                sqls[0] = "insert into CustomerOperatorHistory(coCustomerID,coOperatorID,coAddOperatorID, coAddDate) Select cusID, cusOperatorID, " + this.Operator.opeID + ", getdate() from Customer where cusID in (" + this.hfSelectCheck.Value + ")";
                sqls[1] = "Update Customer set cusOperatorID = " + changeTOOperator + ", cusDepartmentID =" + department + " where cusID in (" + this.hfSelectCheck.Value + ")";
                AddOperatorLog("改变客户负责人至" + ddlOperators.SelectedValue + ":" + this.hfSelectCheck.Value, 0);
                bool success = DbHelperSQL.ExecuteSQL(sqls);
                if (success)
                {
                    bindGrid();
                    this.ExecuteJavascript("alert('操作成功!');var divfade = document.getElementById('fade');divfade.style.display = 'none';var divChange = document.getElementById('divChange'); divChange.style.display = 'none';__doPostBack('ToolBar1$lb_Search','');");
                   //this.Show("操作成功!");
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

            base.requiredColumn = "ccID";
            base.isSum = false;
            base.Grid = this.GridCustomerClue;
            base.AnPager = this.AspNetPager1;
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;

            base.whereCondition += _datacontroller.GetWhereConditionForUser("ccOperatorID", this.Operator.opeID, this.Operator.opeDepartmentID + "", "ccAddOperatorID");
            
            base.isSort = true;
            base.checkBoxCss = "";

            if (txtKeyWord.Text != "") 
            {
                base.whereCondition += " AND ccCustomerName like '%" + txtKeyWord.Text + "%' OR ccName like '%" + txtKeyWord.Text + "%' OR ccTel like '%" + txtKeyWord + "%' OR ccMobile like '%" + txtKeyWord.Text + "%'";
            }
            if (ddlccStatusID.SelectedValue == "1")
            {
                whereCondition += " AND ccStatusID in (54,55)";
            }
            else if (ddlccStatusID.SelectedValue == "2")
            {
                whereCondition += " AND ccStatusID in (56,58)";
            }
            this.initToolBar();
        }


        private void BindControls()
        {
            //绑定DropDownList
            IList<Operators> odlist = _org.GetOperatorsList();
            (new WebFunc()).BindListControl<Operators>(odlist, ddlOperators, "opeName", "opeID", "选择业务员", "");

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
                msg = _datacontroller.Import(fullFilePath, this.Operator.opeID, "CustomerClue","cc");
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


