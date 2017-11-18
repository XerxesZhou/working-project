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
using UDEF.Domain.Enumerate;
using UDEF.Domain;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class EditColumnViewExpression : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private ColumnViewExpressionService _columnviewexpression;
        public ColumnViewExpressionService columnviewexpression
        {
            set { _columnviewexpression = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
                dataBind();
            }
        }

        private void initPage()
        {
            if (StringHelper.IsValidDBInt(Request.QueryString["ExpressionID"]))
            {
                this.FieldExpressionID.CssClass = "OnlyBottom";
                this.FieldExpressionID.ReadOnly = true;
            }

            ListControlDataInit.Init(
                this.FieldUseDataType,
                DataType.GetListItemCollection(),
                "NAME",
                "ID",
                false);

            ListControlDataInit.Init(
                this.FieldExpressionName,
                CustomExpressionService.GetListItemCollection(),
                "NAME",
                "ID", 
                false
            );
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            if (StringHelper.IsValidDBInt(Request.QueryString["ExpressionID"]))
            {
                ColumnViewExpression de = _columnviewexpression.Select(int.Parse(Request.QueryString["ExpressionID"].ToString()));
                FormBindingHelper.BindObjectToControls(de, this);
                this.FieldExpressionName.SelectedValue = CustomExpressionService.GetIndexByText(de.ExpressionName).ToString();
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sHtml = string.Empty;

            try
            {
                if (StringHelper.IsValidDBInt(Request.QueryString["ExpressionID"]))
                {
                    int operatorID = int.Parse(Request.QueryString["ExpressionID"].ToString());
                    ColumnViewExpression de = _columnviewexpression.Select(operatorID);
                    FormBindingHelper.BindControlsToObject(de, this);
                    de.ExpressionName = CustomExpressionService.GetText(int.Parse(this.FieldExpressionName.SelectedValue.ToString()));
                    de.ExpressionValue = CustomExpressionService.GetValue(int.Parse(this.FieldExpressionName.SelectedValue.ToString()));
                    
                    _columnviewexpression.Update(de);

                }
                else
                {
                    ColumnViewExpression de = new ColumnViewExpression();
                    FormBindingHelper.BindControlsToObject(de, this);
                    de.ExpressionName = CustomExpressionService.GetText(int.Parse(this.FieldExpressionName.SelectedValue.ToString()));
                    de.ExpressionValue = CustomExpressionService.GetValue(int.Parse(this.FieldExpressionName.SelectedValue.ToString()));
                    _columnviewexpression.Add(de);

                }

                sHtml += "window.returnValue = true;";
                sHtml += "window.close();";
            }
            catch
            {
                sHtml += "window.returnValue = false;";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Replace", sHtml, true);
        }
    }
}