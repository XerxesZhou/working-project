using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace SmartSoft.Component
{
    public class WebFunc 
    {
        protected BindingFlags BINDING_FLAGS_SET
                    = BindingFlags.Public
                    | BindingFlags.SetProperty
                    | BindingFlags.Instance
                    | BindingFlags.SetField
                    ;
        
        public WebFunc()
        {

        }

        #region 在页面上执行JScript脚本
        public static void Alert(string strMessageInfo, Page page)
        {
            if (strMessageInfo != "")
            {
                strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alertnormal", "<script type='text/javascript'>alert('" + strMessageInfo + "');</script>");
            }

            WebFunc.AjaxAlert(strMessageInfo, page);
            
        }

        public static void AlertError(string strMessageInfo, Page page)
        {
            //strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            //page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script language='javascript'>alert('" + strMessageInfo + "');</script>");
            WebFunc.AjaxAlertError(strMessageInfo, page);
        }

        public static void ExecuteJScript(string jscript, Page page)
        {
            //jscript = jscript.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            page.ClientScript.RegisterClientScriptBlock(page.GetType()," ",jscript, false);
        }

        public static void Execute(string jscript, Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", jscript, true);
        }

        public static void AlertClose(string strMessageInfo,Page page)
        {
            strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alertclose", "<script language='javascript'>alert('" + strMessageInfo + "');window.close();</script>");
        }

        public static void SaveClose(Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "saveclose", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close()</script>");
        }

        public static void SaveClose(string message, Page page)
        {
            message = message.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "saveclose", "<script language=javascript>alert('" + message + "');window.opener.location.reload();window.close()</script>");
        }

        public static void SaveToPage(string message, string pagehref, Page page)
        {
            message = message.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>alert('" + message + "');document.location.href='" + pagehref + "';</script>");
        }

        public static void ToPage(string pagehref, Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>document.location.href='" + pagehref + "';</script>");
        }

        public static void SaveToGoBack(string message, Page page)
        {
            message = message.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>alert('" + message + "');history.back(-3);</script>");
        }

        public static void SaveCloseToPage(string pagehref, Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>window.close();document.location.href='" + pagehref + "';</script>");
        }

        public static void SaveCloseToPage(string message, string pagehref, Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>alert('" + message + "');window.close();window.opener.location.href='" + pagehref + "';</script>");
        }
        #endregion

        #region 绑定ListControl
        public void BindListControl<T>(IList<T> list,ListControl ctrl,string dataTextField,string dataValueField)
        {
            this.BindListControl<T>(list, ctrl, dataTextField, dataValueField, "请选择...", "");
        }

        public void BindListControl<T>(IList<T> list, ListControl ctrl, string dataTextField, string dataValueField, string defaultText, string defaultValue)
        {
            ctrl.Items.Clear();
            if (defaultText != string.Empty || defaultValue != string.Empty)
            {
                ListItem defaultItem = new ListItem(defaultText, defaultValue);
                defaultItem.Selected = true;
                ctrl.Items.Add(defaultItem);
            }

            Type type = typeof(T);
            PropertyInfo[] pis = type.GetProperties(BINDING_FLAGS_SET);

            foreach (T t in list)
            {
                ListItem li = new ListItem();
                foreach (PropertyInfo pi in pis)
                {
                    if (pi.Name == dataTextField)
                    {
                        object propertyValue = this.GetPropertyValue(t, pi.Name);
                        li.Text = (propertyValue != null) ? propertyValue.ToString() : "";
                    }

                    if (pi.Name == dataValueField)
                    {
                        object propertyValue = this.GetPropertyValue(t, pi.Name);
                        li.Value = (propertyValue != null) ? propertyValue.ToString() : "";
                    }
                }
                ctrl.Items.Add(li);
            }
        }

        #endregion

        #region 得到某对象的某属性的值
        /// <summary>
        /// 得到某对象的属性值
        /// </summary>
        private object GetPropertyValue(object data, string propertyName)
        {
            Type type = data.GetType();

            PropertyInfo pi = type.GetProperty(propertyName);
            object propertyValue = pi.GetValue(data, null);

            return propertyValue;
        }
        #endregion

        #region AJAX JScript
        public static void AjaxAlert(string strMessageInfo, Page page)
        {
            if (strMessageInfo != "")
            {
                strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alert", "alert('" + strMessageInfo + "');", true);
            }
        }

        public static void AjaxAlert(string strMessageInfo, bool isClear, Page page)
        {
            if (strMessageInfo != "")
            {
                strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alert", "alert('" + strMessageInfo + "'); this.selectCheck=new Array();", true);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alert", "this.selectCheck=new Array();", true);
            }
        }

        public static void AjaxAlertClose(string strMessageInfo, Page page)
        {
            if (strMessageInfo != "")
            {
                strMessageInfo = strMessageInfo.Replace("\r", "").Replace("\n", "").Replace("\"", "\"\"").Replace("\'", "");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alert", "alert('" + strMessageInfo + "');window.close();", true);
            }
        }

        public static void AjaxAlertError(string strMessageInfo, Page page)
        {
            if (strMessageInfo != "")
            {
                AjaxAlert("错误信息为:" + strMessageInfo, page);
            }
        }

        public static void ExecuteAjaxJScript(string jscript, Page page)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "execute", jscript, true);
        }
        #endregion

        #region
        public static void SetListControl(ListControl lc, string strValue, bool isMutiable)
        {
            foreach (ListItem li in lc.Items)
            {
                if (!isMutiable)
                {
                    li.Selected = false;
                }

                if (li.Value == strValue)
                {
                    li.Selected = false;
                    li.Selected = true;
                }
            }
        }

        public static void SetListControlValue(ListControl lc, string strValue, bool isText)
        {
            foreach (ListItem li in lc.Items)
            {
                li.Selected = false;

                if (isText)
                {
                    if (li.Text == strValue)
                    {
                        li.Selected = false;
                        li.Selected = true;
                    }
                }
                else
                {
                    if (li.Value == strValue)
                    {
                        li.Selected = false;
                        li.Selected = true;
                    }
                }
            }
        }
        #endregion

        #region IP

        /// <summary>
        /// 得到客户端IP
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        /// <summary>
        /// 判断IP是否为内网IP
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static bool IsValidIP(string IP)
        {
            bool result = false;
            //本机IP
            if (IP == "127.0.0.1")
            {
                result = true;
            }

            string[] array_IP = IP.Split(".".ToCharArray());

            //判断10.*.*.*
            if (array_IP[0] == "10")
            {
                result = true;
            }
            else if (array_IP[0] == "192")
            {
                if (array_IP[1] == "168")
                {
                    result = true;
                }
            }
            else if (array_IP[0] == "172")
            {
                int temp = int.Parse(array_IP[1]);
                if (temp >= 16 && temp <= 31)
                {
                    result = true;
                }
            }
            else if (array_IP[0] == "186")
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
