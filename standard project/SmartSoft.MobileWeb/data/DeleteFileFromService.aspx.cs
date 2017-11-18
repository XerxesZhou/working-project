using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace SmartSoft.MobileWeb
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
                string path = System.Web.HttpContext.Current.Request.MapPath("../../publish/UploadFile/CustomerFile/");
                try
                {
                    File.Delete(path + Request["picname"].ToString());
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