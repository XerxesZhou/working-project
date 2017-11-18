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

using UDEF.Component;
using UDEF.Domain.Enumerate;
using UDEF.Domain;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class EditCustomSelectType : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private CustomSelectTypeService _customselecttype;
        public CustomSelectTypeService customselecttype
        {
            set { _customselecttype = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxService));

            if (!IsPostBack)
            {
                dataBind();
            }
        }

        /// <summary>
        /// °ó¶¨Êý¾Ý
        /// </summary>
        private void dataBind()
        {
            if (StringHelper.IsValidDBInt(Request.QueryString["SelectTypeID"]))
            {
                int selectTypeID = int.Parse(Request.QueryString["SelectTypeID"].ToString());
                CustomSelectType objCustomSelectType = _customselecttype.Select(selectTypeID);
                FormBindingHelper.BindObjectToControls(objCustomSelectType, this);
            }
            else
            {
                string selectTypeText = Request.QueryString["SelectTypeText"] == null ? 
                    "" : Request.QueryString["SelectTypeText"].ToString();
                string remark = Request.QueryString["Remark"] == null ? 
                    "" : Request.QueryString["Remark"].ToString();
                this.FieldSelectTypeText.Text = selectTypeText;
                this.FieldRemark.Text = remark;
            }
        }
    }
}

