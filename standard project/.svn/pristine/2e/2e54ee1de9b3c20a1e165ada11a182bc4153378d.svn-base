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
using SmartSoft.Presentation;
using SmartSoft.Domain.Common;
using System.Collections.Generic;
using SmartSoft.Service;
using System.IO;
using SmartSoft.Service.Implement.Common;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Component;

public partial class Main_MyDeskSetting : PageBase
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cpw_uname.Text = this.Operator.opeName;
        this.xx_uname.Text = this.Operator.opeName;
        
        initPage();
    }


    private void initPage()
    {
        if (!string.IsNullOrEmpty(this.Operator.opeUrl))
        {
           // face.Src = "UploadFile/Face/" + this.Operator.opeUrl;
            face2.Src = "../UploadFile/Face/" + this.Operator.opeUrl;
        }


    }

    //修改密码——保存
    protected void lbt_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (tb_newpassword1.Text.Trim() == tb_newpassword2.Text.Trim())
            {
                string newpassword = Security.EncryptDES(tb_newpassword1.Text);
                string oldpassword = Security.EncryptDES(tb_oldpassword.Text);

                _org.UpdatePassword(this.Operator.opeID, newpassword, oldpassword);

                WebFunc.AlertClose("密码修改成功！", Page);
                Response.Redirect("~/Login.aspx", true);
               
            }
            else
            {
                WebFunc.AjaxAlertError("两次密码输入不一致！", Page);
            }
        }
        catch (Exception ex)
        {
            WebFunc.AjaxAlertError(ex.Message, Page);
        }

    }

    //保存上传的文件 
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
                File.Delete(System.Web.HttpContext.Current.Request.MapPath("~/UploadFile/Face/") + ope.opeUrl);
            }
            this.FileUpload1.SaveAs(System.Web.HttpContext.Current.Request.MapPath("~/UploadFile/Face/") + filePath);

            ope.opeUrl = filePath;
            _org.UpdateOperators(ope);
        }
    }

    //个人信息修改——保存
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
            this.ShowAndRedirect("保存成功！", "MyDeskSetting.aspx");
        }
        catch (Exception ex)
        {
            WebFunc.AjaxAlertError(ex.Message, Page);
        }
    } 
}
