using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.Data;
using System.Configuration;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using SmartSoft.Domain.Common;
using SmartSoft.Component;
using UDEF.Component;
using SmartSoft.Service.Interface.Common;

using System.IO;
using SmartSoft.Service.Implement.Common;

namespace SmartSoft.Presentation
{
    /// <summary>
    /// 为将页面类添加到Castle容器中而建立的类,所有页面必须继承此类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        protected BindingFlags BINDING_FLAGS_SET
                    = BindingFlags.Public
                    | BindingFlags.SetProperty
                    | BindingFlags.Instance
                    | BindingFlags.SetField
                    ;
        public TopvisionContainer container = ObtainContainer();

        private BaseController _basecontroller;
        /// <summary>
        /// 表现层基类对象
        /// </summary>
        public BaseController basecontroller
        {
            set { _basecontroller = value; }
            get { return _basecontroller; }
        }

        protected override void OnInit(EventArgs e)
        {

            Type type = this.GetType();

            PropertyInfo[] properties = type.GetProperties(BINDING_FLAGS_SET);

            foreach (PropertyInfo propertie in properties)
            {
                string pname = propertie.Name;

                if (container.Kernel.HasComponent(pname))
                {
                    propertie.SetValue(this, container[pname], null);
                }
            }

            base.OnInit(e);

        }

        //当不需要检测时，可在外面关掉
        public bool CheckPermissionRequired = true;
        public void CheckPagePermission()
        {
            if (this.Operator != null && CheckPermissionRequired && !this.Operator.opeIsAdmin)
            {
                _basecontroller.CheckPagePermission(HttpContext.Current, this.Operator.Ids);
            }
        }

        public void AddOperatorLog(string dosth, int customerID)
        {
            CommonFunction.AddOperatorLog(dosth, customerID, CurrentOperatorID);
        }

        public void AddOperatorLog(string dosth)
        {
            AddOperatorLog(dosth, 0);
        }

        public static TopvisionContainer ObtainContainer()
        {

            IContainerAccessor containerAccessor =

            HttpContext.Current.ApplicationInstance as IContainerAccessor;
            if (containerAccessor == null)
            {
                throw new ApplicationException("你必须在HttpApplication中实现接口 IContainerAccessor 暴露容器的属性");
            }

            TopvisionContainer container = containerAccessor.Container as TopvisionContainer;
            if (container == null)
            {
                throw new ApplicationException("HttpApplication 得不到容器的实例");
            }
            return container;
        }

        #region ConfigValue

        public string Amount
        {
            get
            {
                return StringHelper.GetConfigValue("Amount");
            }
        }

        public string Currency
        {
            get
            {
                return StringHelper.GetConfigValue("Currency");
            }
        }

        public string Price
        {
            get
            {
                return StringHelper.GetConfigValue("Price");
            }
        }

        public string Qty
        {
            get
            {
                return StringHelper.GetConfigValue("Qty");
            }
        }

        #endregion

        #region Session

        /// <summary>
        /// 操作员信息
        /// </summary>
        public Operators Operator
        {
            get
            {
                return this.GetOperator();
            }
            set
            {
                HttpContext.Current.Session["Operator"] = this;
            }
        }

        /// <summary>
        /// 员工拥有的ID字符串
        /// </summary>
        public string IDS
        {
            get
            {
                return this.Operator.Ids;
            }
        }

        public string CurrentOperatorID
        {
            get
            {
                return this.Operator.opeID.ToString();
            }
        }

        public string CurrentOperatorName 
        {
            get 
            {
                return this.Operator.opeName.ToString();
            }
        }

        /// <summary>
        /// 从Session中得到操作员信息
        /// </summary>
        /// <returns></returns>
        private Operators GetOperator()
        {
            Operators op = new Operators();
            if (HttpContext.Current.Session["Operator"] != null)
            {
                op = HttpContext.Current.Session["Operator"] as Operators;
            }
            else
            {
                if (Request.Cookies["Operators"] != null)
                {
                    HttpCookie cookies = Request.Cookies["Operators"];

                    NameValueCollection nvc = cookies.Values;                    

                    op.opeID = int.Parse(nvc["opeID"]);

                    IOrganizationService _org = container["org"] as IOrganizationService;

                    op = _org.GetOperatorsDetail(op.opeID);

                    op.Ids = nvc["Ids"];
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/login.aspx");
                }
            }
                return op;
        }

        public void SetControlCausesValidation(string ctrID, bool bCausesValidation)
        {
            Control ctrObj = this.FindControl(ctrID);
            if (ctrObj != null)
            {
                if (ctrObj is TextBox)
                {
                    (ctrObj as TextBox).CausesValidation = bCausesValidation;
                }
                else if (ctrObj is ListControl)
                {
                    (ctrObj as ListControl).CausesValidation = bCausesValidation;
                }
            }
        }


        //连接字符串
        public string ConnectString
        {
            get 
            {
                string result = string.Empty;
                if (Session["ConnectString"] != null)
                {
                    result = Session["ConnectString"].ToString();
                }
                return result;
            }
        }
        #endregion

        #region 导出EXCEL

        public void ToExcel(DataTable dt, string fileName)
        {
            HttpResponse resp;
            resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            string colHeaders = "", ls_item = "";
            int i = 0;

            //定义表对象与行对像，同时用DataSet对其值进行初始化
            DataRow[] myRow = dt.Select("");

            //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符
            for (i = 0; i < dt.Columns.Count; i++)
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                //向HTTP输出流中写入取得的数据信息
                resp.Write(colHeaders);

            }
            //逐行处理数据 
            foreach (DataRow row in myRow)
            {
                //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    ls_item += row[i].ToString() + "\t";
                    ls_item += row[i].ToString() + "\n";
                    //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据
                    resp.Write(ls_item);
                    ls_item = "";
                }
            }

            resp.End();
        }

        public void ToExcel(ComponentArt.Web.UI.Grid grid, string fileName)
        {
            HttpResponse resp;
            resp = HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //resp.ContentEncoding = System.Text.Encoding.UTF8;
            resp.ContentType = "application/vnd.ms-excel";
            
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            //resp.Write("<base target=_blank>");


            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            //grid.RenderControl(hw);

            //resp.Write(tw.ToString().Replace("&nbsp;", ""));
            //resp.End();


            string colHeaders = "", ls_item = "";
            int i = 0;

            //取得Grid各列标题，各标题之间以\t分割，最后一个列标题后加回车符
            for (i = 0; i < grid.Levels[0].Columns.Count; i++)
            {
                if (i == grid.Levels[0].Columns.Count - 1)
                {
                    colHeaders += grid.Levels[0].Columns[i].HeadingText.ToString() + "\n";
                }
                else
                {
                    colHeaders += grid.Levels[0].Columns[i].HeadingText.ToString() + "\t";
                }
            }
            //向HTTP输出流中写入取得的数据信息
            resp.Write(colHeaders);

            //逐行处理数据 
            foreach (ComponentArt.Web.UI.GridItem item in grid.Items)
            {
                //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n
                for (i = 0; i < grid.Levels[0].Columns.Count; i++)
                {
                    if (i == grid.Levels[0].Columns.Count - 1)
                    {

                        ls_item += item.ToArray()[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += item.ToArray()[i].ToString() + "\t";
                    }
                }
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据    
                resp.Write(ls_item);
                ls_item = "";
            }


            resp.End();
        }

        public void ToExcel(GridView grid, string fileName)
        {
            HttpResponse resp;
            resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //resp.ContentEncoding = System.Text.Encoding.UTF8;
            resp.ContentType = "application/vnd.ms-excel";
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            
            
            
            string colHeaders = "", ls_item = "";
            int i = 0;

            //取得Grid各列标题，各标题之间以\t分割，最后一个列标题后加回车符
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (i == grid.Columns.Count - 1)
                {
                    colHeaders += grid.Columns[i].HeaderText.ToString() + "\n";
                }
                else
                {
                    colHeaders += grid.Columns[i].HeaderText.ToString() + "\t";
                }
            }
            //向HTTP输出流中写入取得的数据信息
            resp.Write(colHeaders);

            //逐行处理数据 
            foreach (GridViewRow row in grid.Rows)
            {
                //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n
                for (i = 0; i < grid.Columns.Count; i++)
                {
                    if (i == grid.Columns.Count - 1)
                    {

                        ls_item += row.Cells[i].Text.ToString() +"\n";
                    }
                    else
                    {
                        ls_item += row.Cells[i].Text.ToString() + "\t";
                    }
                }
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据    
                resp.Write(ls_item);
                ls_item = "";
            }

            resp.End();
        }

        public void ToExcelNew2(GridView ctrl, string fileName)
        {
            foreach (GridViewRow dg in ctrl.Rows)
            {
                foreach (TableCell cell in dg.Cells)
                {
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
                }
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName.Trim() + ".xls");
            //g经测试如果设置为 GetEncoding("GB2312"),导出的文件将会出现乱码。
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            //设置输出文件类型为excel文件。 
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            ctrl.RenderControl(oHtmlTextWriter);
            Response.Output.Write(oStringWriter.ToString());

            Response.Flush();
            Response.End();
        }

        public void ToExcelNew(Control ctrl, string fileName)
        {
            //对于ie，用流的方式导出
            if (Session["IsWebClient"] == null)
            {
                System.Diagnostics.Trace.WriteLine("ie导出");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName.Trim() + ".xls");
                //g经测试如果设置为 GetEncoding("GB2312"),导出的文件将会出现乱码。
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                //设置输出文件类型为excel文件。 
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                ctrl.RenderControl(oHtmlTextWriter);
                Response.Output.Write(oStringWriter.ToString());

                Response.Flush();
                Response.End();
            }
            //对于webclient，通过文件连接的方式导出
            else
            {
                System.Diagnostics.Trace.WriteLine("webclient导出");
                DateTime curTime = DateTime.Now;
                string phyRootPath = Request.PhysicalApplicationPath;
                string virRootPath = Request.ApplicationPath;

                string tempFolderPath = "downloadTempFile";

                StringBuilder curFileName = new StringBuilder();
                curFileName.Append(Operator.opeName);
                curFileName.Append(curTime.Year.ToString());
                curFileName.Append(curTime.Month.ToString());
                curFileName.Append(curTime.Day.ToString());
                curFileName.Append(curTime.Hour.ToString());
                curFileName.Append(curTime.Minute.ToString());
                curFileName.Append(curTime.Second.ToString());
                curFileName.Append(".xls");

                Directory.CreateDirectory(phyRootPath + "\\" + tempFolderPath + "\\");

                string curUrl = Request.Url.ToString();
                string serverUrl = curUrl.Substring(0, curUrl.IndexOf('/', 10));

                string phyPath = phyRootPath + "\\" + tempFolderPath + "\\" + curFileName.ToString();
                string virPath = serverUrl + virRootPath + tempFolderPath + "/" + curFileName.ToString();

                //System.Diagnostics.Trace.WriteLine("物理路径为" + phyPath);
                //System.Diagnostics.Trace.WriteLine("虚拟路径为" + virPath);

                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                ctrl.RenderControl(oHtmlTextWriter);

                StreamWriter ab = new StreamWriter(phyPath);
                ab.Write(oStringWriter.ToString());
                ab.Flush();
                ab.Close();


                String scriptString = "<script language='JavaScript'>OpenEditForm('" + virPath + "','800','600');<";
                scriptString += "/";
                scriptString += "script>";
                ClientScript.RegisterStartupScript(this.GetType(), "", scriptString);
            }

        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        public int GetGridViewColumnIndex(GridView grid, string sortExpression)
        {
            int result = -1;
            foreach (DataControlField column in grid.Columns)
            {
                if (column.SortExpression == sortExpression)
                {
                    result = grid.Columns.IndexOf(column);
                }
            }
            return result;
        }

        public void Show(Control up, string msg)
        {
            ScriptManager.RegisterClientScriptBlock(up, this.GetType(), "alert", "alert('" + msg + "');", true);
        }

        public void Show(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "');", true);
        }

        public void ShowAndRedirect(string msg, string url)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "'); document.location.href='" + url + "';", true);
        }

        public void ShowAndClose(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "'); window.close();", true);
        }

        public void ShowRefreshAndClose(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "'); window.opener.location.reload();window.close();", true);
        }

        public void ExecuteJavascript(string script)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "execute", script, true);
        }

        public void SaveToPageAndRefresh(string pagehref, Page page)
        {
            pagehref += (pagehref.IndexOf("?") > -1 ? "&" : "?") + "stamp=" + DateTime.Now.Ticks.ToString();
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>RefreshListPage();document.location.href='" + pagehref + "';</script>");
        }

        public void SaveAndRefresh(string pagehref, Page page)
        {
            pagehref += (pagehref.IndexOf("?") > -1 ? "&" : "?") + "stamp=" + DateTime.Now.Ticks.ToString();
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>document.location.href='" + pagehref + "';</script>");
        }

        public void SaveAlertToPage(string pagehref, string message, Page page)
        {
            pagehref += (pagehref.IndexOf("?") > -1 ? "&" : "?") + "stamp=" + DateTime.Now.Ticks.ToString();
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), " ", "<script language=javascript>alert('" + message + "');document.location.href='" + pagehref + "';</script>");
        }

        public void InitDataSource(DropDownList ddl, string dataName)
        {
            CommonFunction.InitDataSource(ddl, dataName);
        }

        public void InitDataSourceAddEmpty(DropDownList ddl, string dataName)
        {
            CommonFunction.InitDataSourceAddEmpty(ddl, dataName);
        }

    }
}
