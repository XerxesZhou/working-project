namespace SmartSoft.Web
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

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;
    using SmartSoft.Service.Implement.Common;
    using System.Data.SqlClient;
    using System.IO;


    public partial class main : WebForm<sysPage>
    {


        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
            this.li_username.Text = this.Operator.opeName;
            ltlCompanyName.Text = ConfigurationManager.AppSettings["CompanyName"].ToString() + "����ƽ̨";
            lbCompanyName.Text = ConfigurationManager.AppSettings["CompanyName"].ToString();
        }

        private void initPage()
        {
            if (!string.IsNullOrEmpty(this.Operator.opeUrl))
            {
                face.Src = "UploadFile/Face/" + this.Operator.opeUrl;
                face2.Src = "UploadFile/Face/" + this.Operator.opeUrl;
            }
            if (!IsPostBack)
            {
                this.initMenu(0);
                this.litMenu.Text = htmlString;
                this.hfCurrentOperatorID.Value = this.Operator.opeID.ToString();
            }
        }

        private string htmlString = "";
        private void initMenu(int menuParentID)
        {
            htmlString = "";
            IList<sysPage> Pagelist = _authorization.GetSysPageMenu(this.IDS, menuParentID);
            foreach (sysPage page in Pagelist)
            {
                //1.�û�������Զ����Ҫչʾ����߲˵����Ѿ���ʾ��ϵͳ����������
                //2.ֻ�п���Ա��չʾ�Զ���ģ�飬�����κ��ˣ������Է�ϵͳ����Ա������Ҫ�Զ������ù���
                if (page.PageID != 11208 && (page.PageID != 11206 || this.Operator.opeIsDeveloper))
                {
                    htmlString += string.Format(@"<h3 class='menu_head'>{0}<img src='{1}'/></h3>", page.PageName, page.MenuImagePath);
                    initChildMenu(page.PageID);
                }
            }
        }

        private void initChildMenu(int ParentID)
        {
            htmlString += @"<div style='display:none;' class='menu_body'>";
            IList<sysPage> Pagelist = _authorization.GetSysPageMenu(this.IDS, ParentID);
            if (Pagelist.Count > 0)
            {
                foreach (sysPage page in Pagelist)
                {
                    htmlString += string.Format(@"<a href='{0}' target='rform'>{1}</a>", page.PageFilePath, page.PageName);
                }
            }
            htmlString += @"</div>";
        }
 
        //�޸����롪������
         protected void lbt_save_Click(object sender, EventArgs e)
         {
             try
             {
                 if (tb_newpassword1.Text.Trim() == tb_newpassword2.Text.Trim())
                 {
                     string newpassword = Security.EncryptDES(tb_newpassword1.Text);
                     string oldpassword = Security.EncryptDES(tb_oldpassword.Text);

                     _org.UpdatePassword(this.Operator.opeID, newpassword, oldpassword);

                     WebFunc.AlertClose("�����޸ĳɹ���", Page);
                     Response.Redirect("Login.aspx", true);
                 }
                 else
                 {
                     WebFunc.AjaxAlertError("�����������벻һ�£�", Page);
                 }
             }
             catch (Exception ex)
             {
                 WebFunc.AjaxAlertError(ex.Message, Page);
             }

         }

        //�����ϴ����ļ� 
         private void saveFile()
         {
             if (FileUpload1.PostedFile != null && !string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
             {
                 string ramStr = DateTime.Now.Ticks.ToString();
                 string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                 string filePath = ramStr + extension;

                 Operators ope = this.Operator;

                 if (!string.IsNullOrEmpty(ope.opeUrl))
                 {
                     File.Delete(System.Web.HttpContext.Current.Request.MapPath("UploadFile/Face/") + ope.opeUrl);
                 }
                 this.FileUpload1.SaveAs(System.Web.HttpContext.Current.Request.MapPath("UploadFile/Face/") + filePath);

                 ope.opeUrl = filePath;
                 _org.UpdateOperators(ope);
             }
         }

        //������Ϣ�޸ġ�������
         protected void lbt_saveOperator_Click(object sender, EventArgs e)
         {
             try
             {
                 Operators ope = this.Operator;
                 ope.opeMobile = this.txtMobile.Text;
                 decimal de = 0.0m;
                 if (decimal.TryParse(this.txtOrderAmount.Text, out de))
                 {
                     ope.opeOrderAmount = de;
                 }
                 if (decimal.TryParse(this.txtReceiptAmount.Text, out de))
                 {
                     ope.opeReceiptAmount = de;
                 }
                 ope.opeEmail = this.txtEmail.Text;
                 DateTime d = DateTime.Now;
                 if (DateTime.TryParse(this.txtEnterDate.Text, out d))
                 {
                     ope.opeEnterDate = d;
                 }
                 _org.UpdateOperators(ope);
                 saveFile();
                 this.ShowAndRedirect("����ɹ��������µ�¼��", "Login.aspx");
             }
             catch (Exception ex)
             {
                 WebFunc.AjaxAlertError(ex.Message, Page);
             }
         } 
    }
}
