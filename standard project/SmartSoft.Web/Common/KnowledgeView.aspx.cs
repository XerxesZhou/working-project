
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

    public partial class KnowledgeView : SmartSoft.Presentation.PageBaseEdit
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
                    bindFile();
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

     
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {

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
            Knowledge entity = _datacontroller.GetDetail<Knowledge>(rowID);
            lbl_OperateDate.Text = entity.knowOperateDate.Value.ToString("yyyy-MM-dd");
            lbl_Theme.Text = entity.knowTheme;
            lbl_Type.Text = DbHelperSQL.GetSHSL("select Name from defCommon where ID = " + entity.knowType);
            lit_Content.Text = entity.knowContent;
            
        }

        private void bindFile()
        {
            if (this.rowID > 0)
            {
                Knowledge entity = _datacontroller.GetDetail<Knowledge>(this.rowID);
                if (!string.IsNullOrEmpty(entity.knowFilePath))
                {
                    ltl_wendang.Text = " <a id='HyperLink1' href='../UploadFile/CustomerFile/" + entity.knowFilePath + "' target='_blank' style='text-decoration:underline;'><p>下载</p></a>";
                }
                else
                {
                    ltl_wendang.Text = "没有附件";
                }
            }
        }



        #endregion
    }
}
