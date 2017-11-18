/***************************************************************************
 * 
 *       功能：     所有编辑页面的基类
 *       作者：     朱少军
 *       日期：     2007-4-6
 *              
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/

namespace SmartSoft.Presentation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Web.Security;
    using System.Data;

    using Wuqi.Webdiyer;
    using UDEF.Component;
    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Component;
    using SmartSoft.Domain.Common;

    public class PageBaseEdit : PageBase
    {
        #region Fields
        protected UserDefineBaseService _userDefine;
        /// <summary>
        /// 自定义服务
        /// </summary>
        public UserDefineBaseService userdefinebase
        {
            set { _userDefine = value; }
        }

        //protected BaseController _basecontroller;
        ///// <summary>
        ///// 表现层基类，可在外部指向其子类从而实现重载
        ///// </summary>
        //public BaseController basecontroller
        //{
        //    set { _basecontroller = value; }
        //}

        private Control _ctrlLayoutList1;
        /// <summary>
        /// 布局下拉列表控件，列出了可选择的布局
        /// </summary>
        public Control ctrlLayoutList1
        {
            set { _ctrlLayoutList1 = value; }
        }

        private Control _ctrlLayoutPanel1;
        /// <summary>
        /// 布局容器
        /// </summary>
        public Control ctrlLayoutPanel1
        {
            set { _ctrlLayoutPanel1 = value; }
        }

        private Control _ctrlDesignLayout1;
        /// <summary>
        /// 设计布局的按钮控件对象
        /// </summary>
        public Control ctrlDesignLayout1
        {
            set { _ctrlDesignLayout1 = value; }
        }

        private FormAction _itemAction;
        /// <summary>
        /// 动作
        /// </summary>
        public FormAction itemAction
        {
            get { return _itemAction; }
            set { _itemAction = value; }
        }

        /// <summary>
        /// ID号
        /// </summary>
        protected int rowID = 0;

        /// <summary>
        /// 布局ID
        /// </summary>
        protected int layoutID = 0;

        /// <summary>
        /// 是否只读模式
        /// </summary>
        protected bool? isReadOnly = null;

        private string _title = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        protected string title
        {
            set { _title = value; }
            get { return getTitleFromFormAction(_title); }
        }

        private LinkButton ctrl;
        public LinkButton Ctrl
        {
            get { return this.ctrl; }
            set { this.ctrl = value; }
        }

      
        public int currentOperatorID
        {
            get { return this.Operator.opeID; }
        }

        public int uCurrency
        {
            get
            {
                int nCurrency = 4;
                string unitCurrency = StringHelper.GetConfigValue("Currency");
                int.TryParse(unitCurrency, out nCurrency);
                return nCurrency;
            }
        }

        public int uPrice
        {
            get
            {
                int nPrice = 6;
                string unitPrice = StringHelper.GetConfigValue("Price");
                int.TryParse(unitPrice, out nPrice);
                return nPrice;
            }
        }

        public int uQty
        {
            get
            {
                int nQty = 0;
                string unitQty = StringHelper.GetConfigValue("Qty");
                int.TryParse(unitQty, out nQty);
                return nQty;
            }
        }

        public int uAmount
        {
            get
            {
                int nAmount = 0;
                string unitAmount = StringHelper.GetConfigValue("Amount");
                int.TryParse(unitAmount, out nAmount);
                return nAmount;
            }
        }
        
        #endregion

        #region Event
        /// <summary>
        /// 重载了基类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            //检测页面权限,关闭 Jack 20170626
            //CheckPagePermission();

            //根据权限初始化布局
            initLayout();

            bool bReadOnly = false;
            //如果模式已经在外面指定，则以外面的为准
            if (isReadOnly.HasValue)
            {
                bReadOnly = isReadOnly.Value;
            }
            else    //否则采用默认的处理方式即查看模式为只读，其余的为可编辑
            {
                if (this.itemAction == FormAction.View)
                {
                    bReadOnly = true;
                }
            }

            //生成页面上的控件
            Hashtable htDataTypeFormat = new Hashtable();

            string unitCurrency = StringHelper.GetConfigValue("Currency");
            string unitPrice = StringHelper.GetConfigValue("Price");
            string unitQty = StringHelper.GetConfigValue("Qty");
            string unitAmount = StringHelper.GetConfigValue("Amount");

            int nCurrency = 4;
            int nPrice = 6;
            int nQty = 0;
            int nAmount = 4;
            int.TryParse(unitCurrency, out nCurrency);
            int.TryParse(unitPrice, out nPrice);
            int.TryParse(unitQty, out nQty);
            int.TryParse(unitAmount, out nAmount);

            htDataTypeFormat[((int)EnumDataType.Currency).ToString()] = nCurrency;
            htDataTypeFormat[((int)EnumDataType.Price).ToString()] = nPrice;
            htDataTypeFormat[((int)EnumDataType.Qty).ToString()] = nQty;
            htDataTypeFormat[((int)EnumDataType.Amount).ToString()] = nAmount;

            basecontroller.InitEditPage(this._ctrlLayoutPanel1, this.layoutID, bReadOnly, "TablePanel", "TableHeader", htDataTypeFormat, this.Operator.opeID);

            //绑定页面上的控件条件：1拥有有效布局;2有效ID号;3第一次下载;4非新增模式
            if (this.layoutID > 0 
                && this.rowID >= 0 
                && !IsPostBack)
            {
                BindDataForLayout();
            }

            //在客户端保存各种小数格式化的保留位数
            saveUnitToClient(nCurrency, nPrice, nQty, nAmount);
            //在客户端保存当前操作员和当前布局信息
            saveOperatorIDAndLayoutIDToClient(this.Operator.opeID);

            if (this._ctrlDesignLayout1 != null)
            {
                //关闭 Jack 20170626
                basecontroller.CheckPurview2(this.Page, HttpContext.Current, this.Operator.Ids, this._ctrlDesignLayout1.Parent);
            }

            if (this.ctrl != null)
            {
                if (!Page.IsPostBack)
                {
                    this.DonotCommit(this.ctrl);
                }
            }

            if (CurrentOperatorID != "" && Request.QueryString["MessageID"] + "" != "")
            {
                SetMessageReaded(int.Parse(CurrentOperatorID), int.Parse(Request.QueryString["MessageID"] + ""));
            }

            if (CurrentOperatorID != "")
            {
                CommonFunction.UpdateOperatorLastActiveTime(CurrentOperatorID);
            }
        }

        public void SetMessageReaded(int operatorID, int messageID)
        {
            DbHelperSQL.ExecuteSQL(string.Format("if not exists(select * from sysMessage_Readed where MessageID={0} and OperatorID = {1}) begin Insert into sysMessage_Readed (MessageID,OperatorID) values({0},{1}); end", messageID, operatorID));
        }


        /// <summary>
        /// 在客户端保存当前操作员信息
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        private void saveOperatorIDAndLayoutIDToClient(int opeID)
        {
            string script = "SetCurrentOperatorAndLayout(" + opeID + ", " + this.layoutID +");";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SetOperatorAndLayout", script, true);
        }

        private void saveUnitToClient(int common, int price, int qty, int amount)
        {
            string script = "SetUnit(" + common + ", " + price + ", " + qty + ", " + amount + ");";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SetUnit", script, true);
        }

        public string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID)
        {
            string likeCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + this.Operator.opeID);
            string html = "";
            string contentCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + this.Operator.opeID);
            string cssLikeName = "cssNotLike";
            string cssContentName = "cssNotLike";
            string imageLike = "../@images/like.png";
            string imageContent = "../@images/content.png";

            if (int.Parse(likeCount) > 0)
            {
                cssLikeName = "cssLike";
                imageLike = "../@images/likeClick.png";
            }
            if (int.Parse(contentCount) > 0)
            {
                cssContentName = "cssLike";
                imageContent = "../@images/HasContent.png";
            }
            html = "<div style='width:50%; float:left; text-align:center' id=\"divComment" + cfhID + "\" class='" + cssContentName + "' onclick=\"javascript:GotoFollowHistory(" + cfhID + ",'Clue');\">";
            html += "<img src=\"" + imageContent + "\" style=\"width:18px;\" id=\"imgContent" + cfhID + "\"/>";
            html += " <label id='lblCommentCount'>评论(" + GetCommentCount(cfhID) + ")</label>";
            html += "</div><div onclick=\"javascript:handleClickLike(" + cfhID + "," + cfhOperatorID + ");\" id=\"divLike" + cfhID + "\" class='" + cssLikeName + "' style=\" width:50%; float:left; text-align:center\">";
            html += "<img src=\"" + imageLike + "\" style=\" width:18px;\" id='imgLike" + cfhID + "'/>";
            html += "<label id=\"lblLikeCount" + cfhID + "\">点赞(" + GetLikeCount(cfhID) + ")</label>";
            html += "</div>";
            return html;
        }

        public string GetCommentCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 1 and bcRelatedID = " + cfhID);
        }

        public string GetLikeCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = " + cfhID);
        }
        #endregion

        #region Method
        /// <summary>
        /// 初始化布局下拉框数据源
        /// 改变layoutID为当前选择的布局
        /// 附加设计布局功能客户端事件
        /// </summary>
        private void initLayout()
        {
            //布局下拉框
            if (this._ctrlLayoutList1 != null)
            {
                this._ctrlLayoutList1.Visible = false;
                basecontroller.GetLayoutSource(this._ctrlLayoutList1, this._ctrlLayoutPanel1.ID, out this.layoutID, this.Operator.opeID, (int)this.itemAction);
            }
            //设计布局
            if (this._ctrlDesignLayout1 != null)
            {
                LinkButton lb = this._ctrlDesignLayout1 as LinkButton;
                lb.Attributes.Add("href",
                    "javascript:DesignLayout(" + this.layoutID + "," + basecontroller.GetTableIDbyLayoutID(this.layoutID) + ");");
            }
        }

        /// <summary>
        /// 绑定编辑页面上控件的值,如有特殊需要，可在子类提供重载
        /// </summary>
        protected virtual void BindDataForLayout()
        {
            //初始化GridView的所有列
            Hashtable htDataTypeFormatText = new Hashtable();
            string unitCurrency = StringHelper.GetConfigValue("Currency");
            string unitPrice = StringHelper.GetConfigValue("Price");
            string unitQty = StringHelper.GetConfigValue("Qty");
            string unitAmount = StringHelper.GetConfigValue("Amount");
            htDataTypeFormatText[((int)EnumDataType.Currency).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitCurrency) ? "4" : unitCurrency) + "}";
            htDataTypeFormatText[((int)EnumDataType.Price).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitPrice) ? "6" : unitPrice) + "}";
            htDataTypeFormatText[((int)EnumDataType.Qty).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitQty) ? "2" : unitQty) + "}";
            htDataTypeFormatText[((int)EnumDataType.Amount).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitAmount) ? "4" : unitAmount) + "}";
            basecontroller.BindAllToControls(this._ctrlLayoutPanel1.Page, this.layoutID, this.rowID, htDataTypeFormatText);
        }

        /// <summary>
        /// 根据状态返回标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        protected string getTitleFromFormAction(string title)
        {
            if (itemAction == FormAction.Update)
            {
                if (!title.Contains("编辑"))
                {
                    title = "编辑" + title;
                }
            }
            else if (itemAction == FormAction.Insert)
            {
                if (!title.Contains("录入"))
                {
                    title = "录入" + title;
                }
            }
            else if (itemAction == FormAction.View)
            {
                if (!title.Contains("查看"))
                {
                    title = "查看" + title;
                }
            }

            return title;
        }

        #endregion

        #region 防止因用户多次点击保存按钮而导致的重复提交事件

        private void DonotCommit(LinkButton ctrl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }}");    //保证验证函数的执行
            //sb.Append("if(window.confirm('are you sure?')==false) return false;");        //自定义客户端脚本
            sb.Append("disableOtherSubmit();");        // disable所有submit按钮
            sb.Append(";");
            ctrl.Attributes.Add("onclick", sb.ToString());
        }

        #endregion

    }
}
