using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using SmartSoft.Component;

namespace SmartSoft.Presentation
{
    /// <summary>
    /// 列表页面的基类,所有列表页面应继承此类
    /// 此类目前只是一个简单的处理,尚需完善
    /// </summary>
    /// <typeparam name="T">列表处理的类型</typeparam>
    public class WebList<T> : PageBase
    {
        #region Fields
        
        private GridView dg_list;
        //private bool manualPage;
        //private DeleGetCount getcount;
        //private DeleGetList getlist;
        private IList<T> dataSource = null;

        #endregion

        #region Delegate

        //public delegate int DeleGetCount(string condition);
        //public delegate IList<T> DeleGetList(int pageSize, int pageNo, string keyField, string condition);

        #endregion

        #region Properties

        public IList<T> DataSource
        {
            get { return this.dataSource; }
            set { this.dataSource = value; }
        }

        //#region Condition
        //public string Condition
        //{
        //    get
        //    {
        //        if (this.ViewState["condition"] != null)
        //        {
        //            return this.ViewState["condition"].ToString();
        //        }
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        this.ViewState["condition"] = value;
        //    }
        //}
        //#endregion

        //#region KeyField
        //public string KeyField
        //{
        //    get
        //    {
        //        if (this.ViewState["keyfield"] != null)
        //        {
        //            return this.ViewState["keyfield"].ToString();
        //        }
        //        return "id";
        //    }
        //    set
        //    {
        //        this.ViewState["keyfield"] = value;
        //    }
        //}
        //#endregion

        //#region ListCount
        //public int ListCount
        //{
        //    get
        //    {
        //        if (this.ViewState["listcount"] != null)
        //        {
        //            return int.Parse(this.ViewState["listcount"].ToString());
        //        }
        //        return 0;
        //    }
        //    set
        //    {
        //        this.ViewState["listcount"] = value;
        //    }
        //}
        //#endregion

        #region ListGrid
        public GridView ListGrid
        {
            set { this.dg_list = value; }
        }
        #endregion

        //#region ManualPage

        //public bool ManualPage
        //{
        //    get { return this.manualPage; }
        //    set { this.manualPage = value; }
        //}

        //#endregion

        //#region GetCount

        //public DeleGetCount GetCount
        //{
        //    get { return this.getcount; }
        //    set { this.getcount = value; }
        //}

        //#endregion

        //#region GetList

        //public DeleGetList GetList
        //{
        //    get { return this.getlist; }
        //    set { this.getlist = value; }
        //}

        //#endregion

        #endregion

        #region Methods
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (this.dg_list != null)
            {
                if (!this.Page.IsPostBack)
                {
                    this.dg_list.DataSource = this.DataSource;
                    this.dg_list.DataBind();
                }

                this.dg_list.RowDataBound += new GridViewRowEventHandler(dg_list_RowDataBound);
            }
        }

        //RowDataBound Event
        protected void dg_list_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "ItemOver(this)";
                e.Row.Attributes["onmouseout"] = "ItemOut(this)";


                e.Row.Attributes["onclick"] = "Onclick(this)";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public void ReBindGrid()
        {
            this.dg_list.PageIndex = 0;
            this.dg_list.DataSource = this.DataSource;
            this.dg_list.DataBind();
        }

        public string[] GetCheckedValues()
        {
            string str = "";
            string[] ckb = null;

            str = Request.Form.Get("checkboxname");
            ckb = str.Split(new char[] { ',' });

            return ckb;
        }

        #endregion
    }
}
