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
using Microsoft.Win32;
using System.Management;
using SmartSoft.Component;
using SmartSoft.Presentation;

public partial class ssss : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.TextBox1.Text = SmartSoft.Component.Security.EncryptDES(GetMoAddress());
        }
    }

    //获得网卡序列号----MAc地址
    public string GetMoAddress()
    {
        string MoAddress = " ";
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc2 = mc.GetInstances();
        foreach (ManagementObject mo in moc2)
        {
            if ((bool)mo["IPEnabled"] == true)
                MoAddress = mo["MacAddress"].ToString();
            mo.Dispose();
        }
        return MoAddress.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string SqlStr = "update sysConfig set USBKEY='" + this.TextBox2.Text.Trim() + "'";
            DbHelperSQL.ExecuteSQL(SqlStr);

            string information = DbHelperSQL.GetSHSL("select top 1 USBKEY from sysConfig");
            if (!string.IsNullOrEmpty(information))
            {
                string splitor = "$|@";
                string details = Security.DecryptDES(information);
                string[] items = details.Split(new string[] { splitor }, StringSplitOptions.None);
                Application["key"] = information;
                if (items.Length == 3)
                {
                    Application["UserUnique"] = items[0];
                    Application["AllowedUserCount"] = items[1];
                    Application["ExpireDate"] = items[2];
                }
            }

            this.ShowAndRedirect("序列号已经写入！", "Login.aspx");
        }
        catch
        {
            this.ShowAndRedirect("序列号写入时发生错误！请重新获取！", "ssss.aspx");
        }
    }
}
