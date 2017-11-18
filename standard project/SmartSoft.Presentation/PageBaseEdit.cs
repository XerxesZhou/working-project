/***************************************************************************
 * 
 *       ���ܣ�     ���б༭ҳ��Ļ���
 *       ���ߣ�     ���پ�
 *       ���ڣ�     2007-4-6
 *              
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�   
 *       �޸����ݣ� 
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
        /// �Զ������
        /// </summary>
        public UserDefineBaseService userdefinebase
        {
            set { _userDefine = value; }
        }

        //protected BaseController _basecontroller;
        ///// <summary>
        ///// ���ֲ���࣬�����ⲿָ��������Ӷ�ʵ������
        ///// </summary>
        //public BaseController basecontroller
        //{
        //    set { _basecontroller = value; }
        //}

        private Control _ctrlLayoutList1;
        /// <summary>
        /// ���������б�ؼ����г��˿�ѡ��Ĳ���
        /// </summary>
        public Control ctrlLayoutList1
        {
            set { _ctrlLayoutList1 = value; }
        }

        private Control _ctrlLayoutPanel1;
        /// <summary>
        /// ��������
        /// </summary>
        public Control ctrlLayoutPanel1
        {
            set { _ctrlLayoutPanel1 = value; }
        }

        private Control _ctrlDesignLayout1;
        /// <summary>
        /// ��Ʋ��ֵİ�ť�ؼ�����
        /// </summary>
        public Control ctrlDesignLayout1
        {
            set { _ctrlDesignLayout1 = value; }
        }

        private FormAction _itemAction;
        /// <summary>
        /// ����
        /// </summary>
        public FormAction itemAction
        {
            get { return _itemAction; }
            set { _itemAction = value; }
        }

        /// <summary>
        /// ID��
        /// </summary>
        protected int rowID = 0;

        /// <summary>
        /// ����ID
        /// </summary>
        protected int layoutID = 0;

        /// <summary>
        /// �Ƿ�ֻ��ģʽ
        /// </summary>
        protected bool? isReadOnly = null;

        private string _title = string.Empty;
        /// <summary>
        /// ����
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
        /// �����˻���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            //���ҳ��Ȩ��,�ر� Jack 20170626
            //CheckPagePermission();

            //����Ȩ�޳�ʼ������
            initLayout();

            bool bReadOnly = false;
            //���ģʽ�Ѿ�������ָ�������������Ϊ׼
            if (isReadOnly.HasValue)
            {
                bReadOnly = isReadOnly.Value;
            }
            else    //�������Ĭ�ϵĴ���ʽ���鿴ģʽΪֻ���������Ϊ�ɱ༭
            {
                if (this.itemAction == FormAction.View)
                {
                    bReadOnly = true;
                }
            }

            //����ҳ���ϵĿؼ�
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

            //��ҳ���ϵĿؼ�������1ӵ����Ч����;2��ЧID��;3��һ������;4������ģʽ
            if (this.layoutID > 0 
                && this.rowID >= 0 
                && !IsPostBack)
            {
                BindDataForLayout();
            }

            //�ڿͻ��˱������С����ʽ���ı���λ��
            saveUnitToClient(nCurrency, nPrice, nQty, nAmount);
            //�ڿͻ��˱��浱ǰ����Ա�͵�ǰ������Ϣ
            saveOperatorIDAndLayoutIDToClient(this.Operator.opeID);

            if (this._ctrlDesignLayout1 != null)
            {
                //�ر� Jack 20170626
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
        /// �ڿͻ��˱��浱ǰ����Ա��Ϣ
        /// </summary>
        /// <param name="opeID">����ԱID</param>
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
            html += " <label id='lblCommentCount'>����(" + GetCommentCount(cfhID) + ")</label>";
            html += "</div><div onclick=\"javascript:handleClickLike(" + cfhID + "," + cfhOperatorID + ");\" id=\"divLike" + cfhID + "\" class='" + cssLikeName + "' style=\" width:50%; float:left; text-align:center\">";
            html += "<img src=\"" + imageLike + "\" style=\" width:18px;\" id='imgLike" + cfhID + "'/>";
            html += "<label id=\"lblLikeCount" + cfhID + "\">����(" + GetLikeCount(cfhID) + ")</label>";
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
        /// ��ʼ����������������Դ
        /// �ı�layoutIDΪ��ǰѡ��Ĳ���
        /// ������Ʋ��ֹ��ܿͻ����¼�
        /// </summary>
        private void initLayout()
        {
            //����������
            if (this._ctrlLayoutList1 != null)
            {
                this._ctrlLayoutList1.Visible = false;
                basecontroller.GetLayoutSource(this._ctrlLayoutList1, this._ctrlLayoutPanel1.ID, out this.layoutID, this.Operator.opeID, (int)this.itemAction);
            }
            //��Ʋ���
            if (this._ctrlDesignLayout1 != null)
            {
                LinkButton lb = this._ctrlDesignLayout1 as LinkButton;
                lb.Attributes.Add("href",
                    "javascript:DesignLayout(" + this.layoutID + "," + basecontroller.GetTableIDbyLayoutID(this.layoutID) + ");");
            }
        }

        /// <summary>
        /// �󶨱༭ҳ���Ͽؼ���ֵ,����������Ҫ�����������ṩ����
        /// </summary>
        protected virtual void BindDataForLayout()
        {
            //��ʼ��GridView��������
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
        /// ����״̬���ر���
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        protected string getTitleFromFormAction(string title)
        {
            if (itemAction == FormAction.Update)
            {
                if (!title.Contains("�༭"))
                {
                    title = "�༭" + title;
                }
            }
            else if (itemAction == FormAction.Insert)
            {
                if (!title.Contains("¼��"))
                {
                    title = "¼��" + title;
                }
            }
            else if (itemAction == FormAction.View)
            {
                if (!title.Contains("�鿴"))
                {
                    title = "�鿴" + title;
                }
            }

            return title;
        }

        #endregion

        #region ��ֹ���û���ε�����水ť�����µ��ظ��ύ�¼�

        private void DonotCommit(LinkButton ctrl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }}");    //��֤��֤������ִ��
            //sb.Append("if(window.confirm('are you sure?')==false) return false;");        //�Զ���ͻ��˽ű�
            sb.Append("disableOtherSubmit();");        // disable����submit��ť
            sb.Append(";");
            ctrl.Attributes.Add("onclick", sb.ToString());
        }

        #endregion

    }
}
