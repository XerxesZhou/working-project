
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
    using SmartSoft.Domain.Common;
    using System.IO;

    public partial class KnowledgeEditForm : SmartSoft.Presentation.PageBaseEdit
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
                this.initToolBar();
                initPage();

                //base.Page_Load(sender, e);
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

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Knowledge entity = new Knowledge();

                if (action == FormAction.Update)
                {
                    entity = _datacontroller.GetDetail<Knowledge>(rowID);
                }

                entity.knowTheme = this.tb_Theme.Text;
                entity.knowType = int.Parse(this.tb_Type.SelectedValue);

                entity.knowContent = this.txtDefaultHtmlArea.Value;
                entity.knowOperator = this.Operator.opeID;

                entity.knowOperateDate = DateTime.Now;

                int rid = rowID;
                if (action == FormAction.Insert)
                {
                    rid = _datacontroller.Add<Knowledge>(entity);
                                 
                }   
                else
                {
                    _datacontroller.Update<Knowledge>(entity);
                }

                saveFile(rid);
                CScript.execute(this.Page, "SaveBackToList();");
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "lbt_Save_Click", ex);
                WebFunc.Alert("操作失败！\n请检查后再重试，如仍有问题，请联系管理员.", this.Page);
            }
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            if (action == FormAction.Insert || action == FormAction.Update)
            {
                this.ToolBar1.lb_Save.Visible = true;
                this.ToolBar1.lb_Save.Click += lb_Save_Click;
            }

            this.ToolBar1.lb_Close.Visible = true;
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            //布局权限
            base.itemAction = this.action;
            base.ctrlLayoutList1 = this.ToolBar1.ddl_Layout;
            base.ctrlLayoutPanel1 = this.MainPanel;
            base.ctrlDesignLayout1 = this.ToolBar1.lb_DesignLayout;
            base.rowID = this.rowID;

            this.title = "知识资料";

        }

        private void bindSpecialData()
        {
            if (action == FormAction.Update)
            {
                Knowledge entity = _datacontroller.GetDetail<Knowledge>(rowID);
                tb_OperateDate.Text = entity.knowOperateDate.Value.ToString("yyyy-MM-dd");
                tb_Theme.Text = entity.knowTheme;
                tb_Type.SelectedValue = entity.knowType.ToString();
                txtDefaultHtmlArea.Value = entity.knowContent;
                //ltl_wendang.Text = entity.knowFilePath;
            }

            DbHelperSQL.BindDropDownList("select * from defCommon where TableName = 'defKnowledgeType'", this.tb_Type, "Name", "ID");
        }

        //上传文件
        private void saveFile(int rID) 
        {
            if (Fup.PostedFile != null && !string.IsNullOrEmpty(Fup.PostedFile.FileName))
            {
                string ramStr = DateTime.Now.Ticks.ToString();
                string extension = System.IO.Path.GetExtension(Fup.PostedFile.FileName);
                string filePath = ramStr + extension;
                
                Knowledge entity = _datacontroller.GetDetail<Knowledge>(rID);

                if (!string.IsNullOrEmpty(entity.knowFilePath))
                {
                    File.Delete(System.Web.HttpContext.Current.Request.MapPath("../UploadFile/CustomerFile/") + entity.knowFilePath);
                }
                this.Fup.SaveAs(System.Web.HttpContext.Current.Request.MapPath("../UploadFile/CustomerFile/") + filePath);
                entity.knowFilePath = filePath;
                _datacontroller.Update<Knowledge>(entity);
            }
        }
        
        #endregion
    }
}
