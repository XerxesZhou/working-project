using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace SmartSoft.Web
{
    public partial class DeleteFileFromService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["picname"] != null)
            {
                Response.Clear();
                Response.ContentType = "application/json";
                String result = "success";
                string smallFileName = "small" + Request["picName"] + "";
                string path = System.Web.HttpContext.Current.Request.MapPath("UploadFile/CustomerFile/");
                try
                {
                    File.Delete(path + Request["picname"].ToString());
                    File.Delete(path + smallFileName);
                }
                catch (Exception ee)
                {
                    result = ee.Message;
                }
                Response.Write("{\"result\":\"" + result + "\"}");
                Response.End();
            }  
        }
    }
}