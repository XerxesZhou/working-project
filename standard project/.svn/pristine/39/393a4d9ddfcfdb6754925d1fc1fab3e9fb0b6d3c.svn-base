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
using System.Text.RegularExpressions;

public partial class eWebEditor_upload_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.uploadfile.PostedFile.FileName.Equals(""))
        {
            return;
        }
        else
        {
            string _flag = Request["flag"];
            string _regex = "";

            switch (_flag)
            {
                case "img":
                    _regex = @"^.+\.(jpe?g|gif)$";
                    break;
                case "flash":
                    _regex = @"^.+\.(swf)$";
                    break;
                case "media":
                    _regex = @"^.+\.(wmv|avi|rmvb|mpeg|mov)$";
                    break;
                case "file":
                    _regex = @"^.+\.(rar|zip|doc|pdf|txt)$";
                    break;
                default:
                    _regex = @"^.+\.(jpe?g|gif|swf|wmv|avi|rmvb|mpeg|mov|rar|zip|doc|pdf|txt)$";
                    break;
            }

            Regex test = new Regex(_regex);

            if (test.IsMatch(uploadfile.PostedFile.FileName) == true)
            {
                ClientScriptManager cs = Page.ClientScript;

                string imagesfolder = ConfigurationManager.AppSettings["imagesfolder"].ToString();
                string filename = "";// Common.UpLoadFile(uploadfile, imagesfolder);
                string apurl = Request.Url.ToString();
                string apurl2 = Request.CurrentExecutionFilePath;
                string tempurl = apurl.Substring(0, apurl.IndexOf(apurl2));
                string appurl = Request.ApplicationPath;
                string hurl;
                if (appurl.Length == 1)
                    hurl = appurl + filename.Replace("~/", "");
                else
                    hurl = appurl + "/" + filename.Replace("~/", "");
                string imgpreview = tempurl + hurl;

                cs.RegisterClientScriptBlock(this.GetType(), "tt", "parent.document.all('divProcessing').style.display='none';parent.document.all('imgPreview').src='" + imgpreview + "';parent.document.all('Hurl').value='" + hurl + "';parent.document.all('d_fromurl').value='';", true);

                lblinfo.Text = "文件上传成功！<a href=\"\">重新上传</a>";
            }
            else
            {
                lblinfo.Text = "您上传的文件类型不正确！<a href=\"\">重新上传</a><script>parent.document.all('divProcessing').style.display='none';</script>";
            }
        }
    }
}
