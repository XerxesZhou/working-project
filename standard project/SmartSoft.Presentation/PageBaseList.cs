/***************************************************************************
 * 
 *       功能：     List页面的基类
 *                  将List页共公的按钮，Grid事件及函数放到PageBaseList类中。
 *                  List页面继承PageBaseList类
 *       作者：     肖秋李
 *       日期：     2007-2-2
 *              
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/

namespace SmartSoft.Presentation
{
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
    using System.Data;

    using Wuqi.Webdiyer;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;
    using UDEF.Service;
    using UDEF.Domain;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Component;
    using SmartSoft.Domain.Common;

    public class PageBaseList : PageBase
    {
        #region Fields
        #region private
        protected UserDefineBaseService _userDefine;
        /// <summary>
        /// 自定义服务
        /// </summary>
        public UserDefineBaseService userdefinebase
        {
            set { _userDefine = value; }
        }

        private GridView grid;
        /// <summary>
        /// 显示数据的GridView对象
        /// </summary>
        public GridView Grid
        {
            set { this.grid = value; }
            get { return this.grid; }
        }

        protected AspNetPager anPager;
        /// <summary>
        /// 分页控件
        /// </summary>
        public AspNetPager AnPager
        {
            set { this.anPager = value; }
        }

        private ColumnViewService _columnview;
        /// <summary>
        /// 自定义中的视图服务对象
        /// </summary>
        public ColumnViewService columnview
        {
            set { _columnview = value; }
        }

        private Control _ctrlColumnViewList1;
        /// <summary>
        /// 视图下拉列表控件
        /// </summary>
        public Control ctrlColumnViewList1
        {
            set { _ctrlColumnViewList1 = value; }
        }

        private Control _ctrlDesignView1;
        /// <summary>
        /// 视图设计控件
        /// </summary>
        public Control ctrlDesignView1
        {
            set { _ctrlDesignView1 = value; }
        }

        private Control _ctrlBranchList1;
        /// <summary>
        /// 分公司下拉列表控件对象,位于查询框中，以Search开头
        /// </summary>
        public Control ctrlBranchList1
        {
            set { _ctrlBranchList1 = value; }
        }
        /// <summary>
        /// 是否列出全部分公司而不仅仅所属公司
        /// 默认为否，在出入库里为真，即出入库时可选择所有分公司
        /// </summary>
        protected bool isBranchAll = false;

        //private BaseController _basecontroller;
        ///// <summary>
        ///// 表现层基类对象
        ///// </summary>
        //public BaseController basecontroller
        //{
        //    set { _basecontroller = value; }
        //    get { return _basecontroller; }
        //}
        #endregion

        #region protected
        /// <summary>
        /// 视图ID
        /// </summary>
        protected int viewID = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        protected int totalRecord = 0;

        /// <summary>
        /// 普通页记录数
        /// </summary>
        private int _pageSize = int.Parse(StringHelper.GetConfigValue("PageSize"));
        public int pageSize
        {
            set { _pageSize = value; }
            get { return _pageSize; }
        }

        /// <summary>
        /// 查询所有时的页记录数
        /// </summary>
        protected int pageSizeAll = int.Parse(StringHelper.GetConfigValue("PageSizeAll"));
        
        /// <summary>
        /// 必须列
        /// </summary>
        protected string requiredColumn = string.Empty;

        /// <summary>
        /// 选择条件
        /// </summary>
        protected string whereCondition = "";

        /// <summary>
        /// 查询条件列集合
        /// </summary>
        protected string whereConditionColumn = "";

        /// <summary>
        /// 排序条件
        /// </summary>
        protected string sortExpression = "";

        /// <summary>
        /// 导入Excel中的标题
        /// </summary>
        protected string exportToExcelTitle = string.Empty;
        protected string[] rowToExcelText = null;

        /// <summary>
        /// 是否排序,可在子类指定
        /// </summary>
        protected bool isSort = false;

        /// <summary>
        /// 复选框列的样式
        /// </summary>
        protected string checkBoxCss = "";

        /// <summary>
        /// 复选框列的html文本
        /// </summary>
        protected string checkBoxHtml = "<input type=\"checkbox\" id=\"checkboxname\" value='{0}' onclick='OnCheck(this);doNothing(event);' />";
       
        /// <summary>
        /// 复选框列头的html文本
        /// </summary>
        protected string checkBoxHeaderText = "<input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /> ";

        /// <summary>
        /// 双击时的客户端响应事件
        /// </summary>
        protected string dbClickJs = "dbclick(this, '" + FormAction.View.ToString() + "')";

        /// <summary>
        /// GridView是否生成特殊列（复选框列)
        /// </summary>
        protected bool isHasSpecialColumn = true; 

        /// <summary>
        /// GridView及数据源里是否生成统计行
        /// 是：在数据源及数据源中同时生成汇总行
        /// 否：不生成汇总行
        /// </summary>
        protected bool isSum = true;  
        
        /// <summary>
        /// 是否查询页面，默认为否
        /// 是：GridView中的行不生成响应单双击的事件
        /// 否：GridView中的行生成响应单双击的事件
        /// </summary>
        protected bool isQueryPage = false;

        /// <summary>
        /// 是否加序号列
        /// </summary>
        protected bool isAddIndexColumn = true;

        protected bool isRowBound = true;
   
        /// <summary>
        /// 是否按列数设定GirdView的宽度
        /// 列数很多时,如统计报表时,可考虑设定为计算生成宽度
        /// </summary>
        protected bool isSetGridViewWidth = true;

        /// <summary>
        /// 是否让列表中有关客户、供应商、货品的下拉框具有查询特效
        /// </summary>
        protected bool hasSpecialClientDffect = false;
        #endregion
        #endregion

        #region Event
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            //检测页面权限
            CheckPagePermission();

            //根据权限，初始化视图ID及其数据源
            initColumnView();

            if (this.viewID > 0)
            {
                //根据视图初始化GridView的列框架
                initGrid();
                
                if (!IsPostBack)
                {
                    //初始化查询框中的所有下拉列表项条件数据源
                    basecontroller.InitDropDownList(this.Page, this.viewID, this.Operator.opeID);


                    this.sortExpression = this.getSort(this.viewID);
                }
                else
                {
                    this.sortExpression = this.getSortExpression();
                }

                if (this.grid == null)
                    return;

                //从查询条件框的特定控件中，按规则生成查询条件
                this.whereCondition += basecontroller.GetSearchCondition(this.Page, this.viewID);
                
                this.grid.Sorting += this.grid_Sorting;

                if (isRowBound)
                {
                    //如果是只供查询的页面如报表,则不生成单双击事件
                    if (isQueryPage)
                    {
                        this.grid.RowDataBound += this.gridQuery_RowDataBound;
                    }
                    else
                    {
                        this.grid.RowDataBound += this.grid_RowDataBound;
                    }
                }
                //换页事件
                this.anPager.PageChanged += this.anPager_PageChanged;
            }

            basecontroller.CheckPurview(this.Page, HttpContext.Current, this.Operator.Ids, this._ctrlDesignView1.Parent);

            //在客户端保存当前操作员信息
            saveOperatorIDAndLayoutIDToClient(this.Operator.opeID);


            if (hasSpecialClientDffect)
            {
                //为列表页面加客户脚本文件的链接
                addJavascript();
                //为列表查询框特效创建Div和ListBox
                createListBoxesAndDivs();
            }

            if (!IsPostBack)
            {
                lb_Search_Click(sender, e);
            }

            if (CurrentOperatorID != "")
            {
                CommonFunction.UpdateOperatorLastActiveTime(CurrentOperatorID);
            }

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "handleFixedTitle", "$(function(){handleFixedTitle();});", true);
        }

        #region 为列表页面加客户脚本文件的链接
        private void addJavascript()
        {
            this.ClientScript.RegisterClientScriptInclude("AjaxMainList", "../@Scripts/AjaxMainList.js");
        }
        #endregion

        #region 在客户端保存当前操作员信息
        /// <summary>
        /// 在客户端保存当前操作员信息
        /// </summary>
        /// <param name="opeID">操作员ID</param>
        private void saveOperatorIDAndLayoutIDToClient(int opeID)
        {
            string script = "SetCurrentOperatorAndLayout(" + opeID + ", " + 0 + ");";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SetOperatorAndLayout", script, true);
        }
        #endregion

        #region 为列表查询框特效创建Div和ListBox
        private void createListBoxesAndDivs()
        {
            HtmlGenericControl div1 = new HtmlGenericControl();
            div1.ID = "divProductSN";
            div1.Attributes["style"] = "display:none";
            ListBox list1 = new ListBox();
            list1.ID = "lbProductSN";
            list1.CssClass = "ListBox";
            div1.Controls.Add(list1);
            this.Page.Controls.Add(div1);

            HtmlGenericControl div2 = new HtmlGenericControl();
            div2.ID = "divProductPartNumber";
            div2.Attributes["style"] = "display:none";
            ListBox list2 = new ListBox();
            list2.ID = "lbProductPartNumber";
            list2.CssClass = "ListBox";
            div2.Controls.Add(list2);
            this.Page.Controls.Add(div2);

            HtmlGenericControl div3 = new HtmlGenericControl();
            div3.ID = "divCustomerSN";
            div3.Attributes["style"] = "display:none";
            ListBox list3 = new ListBox();
            list3.ID = "lbCustomerSN";
            list3.CssClass = "ListBox";
            div3.Controls.Add(list3);
            this.Page.Controls.Add(div3);

            HtmlGenericControl div4 = new HtmlGenericControl();
            div4.ID = "divCustomerName";
            div4.Attributes["style"] = "display:none";
            ListBox list4 = new ListBox();
            list4.ID = "lbCustomerName";
            list4.CssClass = "ListBox";
            div4.Controls.Add(list4);
            this.Page.Controls.Add(div4);

            HtmlGenericControl div5 = new HtmlGenericControl();
            div5.ID = "divSupplierSN";
            div5.Attributes["style"] = "display:none";
            ListBox list5 = new ListBox();
            list5.ID = "lbSupplierSN";
            list5.CssClass = "ListBox";
            div5.Controls.Add(list5);
            this.Page.Controls.Add(div5);

            HtmlGenericControl div6 = new HtmlGenericControl();
            div6.ID = "divSupplierName";
            div6.Attributes["style"] = "display:none";
            ListBox list6 = new ListBox();
            list6.ID = "lbSupplierName";
            list6.CssClass = "ListBox";
            div6.Controls.Add(list6);
            this.Page.Controls.Add(div6);
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// 1.初始化视图下拉框数据源
        /// 2.改变viewID为当前选择的视图
        /// 3.附加设计视图客户端功能
        /// </summary>
        private void initColumnView()
        {
            //视图数据源下拉框
            if (this._ctrlColumnViewList1 != null)
            {
                this._ctrlColumnViewList1.Visible = true;
                basecontroller.GetViewSource(this._ctrlColumnViewList1, this.grid.ID, out this.viewID, this.Operator.opeID);
            }
            //设计视图
            if (this._ctrlDesignView1 != null)
            {
                LinkButton lb = this._ctrlDesignView1 as LinkButton;
                lb.Attributes.Add("href",
                   "javascript:DesignView(" + this.viewID + "," + basecontroller.GetTableIDByViewID(this.viewID) + ");");
            }
        }


        /// <summary>
        /// 移动到行时改变颜色及增加点击脚本及单击双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                e.Row.Attributes["onmouseover"] = "ItemOver(this)";
                e.Row.Attributes["onmouseout"] = "ItemOut(this)";
                e.Row.Attributes["onclick"] = "Onclick(this)";
                e.Row.Attributes["ondblclick"] = dbClickJs;
            }
        }

        /// <summary>
        /// 移动到行时改变颜色及增加点击脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridQuery_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                e.Row.Attributes["onclick"] = "Onclick(this)";
                e.Row.Attributes["onmouseover"] = "ItemOver(this)";
                e.Row.Attributes["onmouseout"] = "ItemOut(this)";
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.getSorting(e.SortExpression);
            this.sortExpression = this.getSortExpression();
            if (!GetIsSearchAll())
            {
                this.bindGrid();
            }
            else
            {
                this.bindGridAll();
            }
        }

        /// <summary>
        /// 换页
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected virtual void anPager_PageChanged(object src, Wuqi.Webdiyer.PageChangedEventArgs e)
        {
            try
            {
                this.anPager.CurrentPageIndex = e.NewPageIndex;
                if (!GetIsSearchAll())
                {

                    this.bindGrid();
                }
                else
                {
                    this.bindGridAll();
                }

                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "k", "ClearSelected();", true);
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "anPager_PageChanged", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void lb_Search_Click(object sender, EventArgs e)
        {
            try
            {
                SetIsSearchAll(false);
                this.bindGrid();
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "lb_Search_Click", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        /// <summary>
        /// 不分页查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void lb_SearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                SetIsSearchAll(true);
                //this.anPager.CurrentPageIndex = 1;
                bindGridAll();
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "lb_SearchAll_Click", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Export_Click(object sender, EventArgs e)
        {
            try
            {
                //如果有复选框
                if (this.isHasSpecialColumn)
                {
                    //导出时删除复选框列
                    this.grid.Columns.Remove(this.grid.Columns[0]);
                }
                //初始化为不分页，不排序
                ExportToExcel.InitGridView(this.grid);


                //格式化
                //this.rowToExcelText = getRowToExcelText(this.viewID);
                //this.grid.RowDataBound += GridExport_RowDataBound;

                //绑定数据
                this.bindGridAll();
                //this.DisableControls(this.grid);

                //将GridView导出
                //ExportToExcel.ToExcel(
                //    this.grid,
                //    exportToExcelTitle + "(" + DateTime.Now.ToShortDateString() + ")",
                //    this.Page);

                //this.ToExcel(this.grid, "(" + DateTime.Now.ToShortDateString() + ")");

                this.ToExcelNew(this.grid, "(" + DateTime.Now.ToShortDateString() + ")");
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "lb_Export_Click", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        /// <summary>
        /// 导出到Excel,针对身份证号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Export2_Click(object sender, EventArgs e)
        {
            try
            {
                //如果有复选框
                if (this.isHasSpecialColumn)
                {
                    //导出时删除复选框列
                    this.grid.Columns.Remove(this.grid.Columns[0]);
                }
                //初始化为不分页，不排序
                ExportToExcel.InitGridView(this.grid);

                //绑定数据
                this.bindGridAll();

                this.ToExcelNew2(this.grid, "(" + DateTime.Now.ToShortDateString() + ")");
            }
            catch (Exception ex)
            {
                basecontroller.LogException("PageBaseList", this.Operator.opeName, "lb_Export_Click", ex);
                WebFunc.Alert("操作失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        /// <summary>
        /// 格式化Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        private void DisableControls(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(HyperLink))
                {
                    l.Text = (gv.Controls[i] as HyperLink).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    DisableControls(gv.Controls[i]);
                }
            }
        }

        #endregion

        #region Mothed
        #region public
        /// <summary>
        /// 设置是否查看所有
        /// </summary>
        /// <param name="IsSearchAll"></param>
        protected void SetIsSearchAll(bool IsSearchAll)
        {
            if (ViewState["IsSearchAll"] == null)
            {
                ViewState.Add("IsSearchAll", IsSearchAll.ToString());
            }
            else
            {
                ViewState["IsSearchAll"] = IsSearchAll.ToString();
            }
        }

        /// <summary>
        /// 读取是否查看所有
        /// </summary>
        /// <returns></returns>
        protected bool GetIsSearchAll()
        {
            if (ViewState["IsSearchAll"] != null)
            {
                return bool.Parse(ViewState["IsSearchAll"].ToString());
            }
            return false;
        }

        /// <summary>
        /// 初始化生成GridView
        /// </summary>
        protected void initGrid()
        {
            //已改为在客户端判断
            //if (GetIsSearchAll())
            //{
            //    this.checkBoxHeaderText = "";
            //}
            //清空GridView的列
            if (this.grid != null)
            {
                this.grid.Columns.Clear();

                //视图增加序号列（待完成）

                if (isHasSpecialColumn)
                {
                    //初始化GridView的复选框列
                    _userDefine.InitGridViewSpecial(
                        this.grid,
                        this.requiredColumn,
                        this.checkBoxHtml,
                        this.checkBoxHeaderText,
                        this.checkBoxCss);
                }

                if (isAddIndexColumn)
                {
                    //增加序号列
                    TemplateField tf = new TemplateField();
                    tf.HeaderText = "序号";
                    tf.ItemTemplate = new GridViewTemplateService(DataControlRowType.DataRow, "Index", 6, 0);
                    this.grid.Columns.Add(tf);
                }

                //初始化GridView的所有列
                Hashtable htDataTypeFormatText = new Hashtable();
                string unitCurrency = StringHelper.GetConfigValue("Currency");
                string unitPrice = StringHelper.GetConfigValue("Price");
                string unitQty = StringHelper.GetConfigValue("Qty");
                string unitAmount = StringHelper.GetConfigValue("Amount");
                htDataTypeFormatText[((int)EnumDataType.Currency).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitCurrency) ? "2" : unitCurrency) + "}";
                htDataTypeFormatText[((int)EnumDataType.Price).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitPrice) ? "6" : unitPrice) + "}";
                htDataTypeFormatText[((int)EnumDataType.Qty).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitQty) ? "2" : unitQty) + "}";
                htDataTypeFormatText[((int)EnumDataType.Amount).ToString()] = "{0:N" + (string.IsNullOrEmpty(unitAmount) ? "4" : unitAmount) + "}";
                _userDefine.InitGridViewColumn(this.viewID, this.grid, this.isSort, htDataTypeFormatText);

                //设置宽度
                if (isSetGridViewWidth)
                {
                    setGridViewWidth();
                }
            }
        }

        /// <summary>
        /// 设置GridView的宽度
        /// </summary>
        private void setGridViewWidth()
        {
            int gridWidth = this.grid.Columns.Count * 80;
            if (this.grid.Columns.Count > 12)
            {
                this.grid.Width = gridWidth;
            }
        }

        /// <summary>
        /// 得到排序条件
        /// </summary>
        /// <param name="viewID"></param>
        /// <returns></returns>
        protected string getSort(int viewID)
        {
            return _columnview.Select(viewID).Sort;
        }

        /// <summary>
        /// 查询排序
        /// </summary>
        /// <param name="SortExpression"></param>
        protected void getSorting(string SortExpression)
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState.Add("SortExpression", "");
            }

            if (ViewState["SortDirection"] == null)
            {
                ViewState.Add("SortDirection", "");
            }

            if (ViewState["SortExpression"].ToString().Equals(SortExpression))
            {
                if (ViewState["SortDirection"].ToString().Equals("DESC"))
                    ViewState["SortDirection"] = "ASC";
                else
                    ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortExpression"] = SortExpression;
                ViewState["SortDirection"] = "ASC";
            }
        }

        /// <summary>
        /// 得到排序
        /// </summary>
        /// <returns></returns>
        protected string getSortExpression()
        {
            if (ViewState["SortExpression"] == null ||
                ViewState["SortExpression"].ToString().Equals(""))
            {
                return "";
            }
            else
            {
                return ViewState["SortExpression"] + " " + ViewState["SortDirection"];
            }
        }

        /// <summary>
        /// 设置分页控件
        /// </summary>
        /// <param name="pager">分页控件</param>
        /// <param name="totalRecord">总数</param>
        /// <param name="pageSize">每页数量</param>
        protected void setCustomInfo(AspNetPager pager, int totalRecord, int pageSize)
        {
            pager.PageSize = pageSize;
            pager.RecordCount = totalRecord;
            pager.CustomInfoText = "第" + pager.CurrentPageIndex +"/" + Math.Ceiling((double)((double)totalRecord/(double)pageSize)) + "页 | 共" + totalRecord.ToString() + "条";
        }

        /// <summary>
        /// 获得分页数据源
        /// </summary>
        /// <returns></returns>
        protected virtual DataTable getDataSource()
        {
            DataTable dt = 
            _userDefine.GetDataSource(
                this.viewID,
                this.requiredColumn,
                this.whereCondition,
                this.whereConditionColumn,
                this.sortExpression,
                this.pageSize,
                this.anPager.CurrentPageIndex,
                ref this.totalRecord,
                this.isSum
                );
            dt.Columns.Add("Index");
            int i = (this.anPager.CurrentPageIndex - 1) * this.pageSize + 1;
            
            foreach (DataRow r in dt.Rows)
            {
                r["Index"] = i++;
            }
            if (this.isSum)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[dt.Rows.Count - 1]["Index"] = "";
                }
            }
            return dt;
        }

        public DataTable getDataSourceForChart()
        {
            DataTable dt =
            _userDefine.GetDataSource(
                this.viewID,
                this.requiredColumn,
                this.whereCondition,
                this.whereConditionColumn,
                this.sortExpression,
                this.pageSize,
                this.anPager.CurrentPageIndex,
                ref this.totalRecord,
                false
                );
            return dt;
        }

        /// <summary>
        /// 获得不分页数据源
        /// </summary>
        /// <returns>数据源</returns>
        protected virtual DataTable getGridAllDataSource()
        {
            DataTable dt = _userDefine.GetDataSource(
                this.viewID,
                this.requiredColumn,
                this.whereCondition,
                this.whereConditionColumn,
                this.sortExpression,
                this.pageSizeAll,
                this.anPager.CurrentPageIndex,
                ref this.totalRecord,
                this.isSum
                );
            dt.Columns.Add("Index");
            int i = (this.anPager.CurrentPageIndex - 1) * this.pageSize + 1;

            foreach (DataRow r in dt.Rows)
            {
                r["Index"] = i++;
            }
            if (this.isSum)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[dt.Rows.Count - 1]["Index"] = "";
                }
            }
            return dt;
        }
        #endregion

        #region protected
        /// <summary>
        /// 绑定grid并设置anPager的页数
        /// </summary>
        protected virtual void bindGrid()
        {
            if (this.viewID > 0)
            {

                DataTable dt = this.getDataSource();

                //如果有符合要求的记录但当前页不是第一页,则返回数据库再取数据
                if (this.anPager.CurrentPageIndex != 1 && dt.Rows.Count == 0)
                {
                    this.anPager.CurrentPageIndex = 1;
                    dt = this.getDataSource();
                }

                this.grid.DataSource = dt;
                this.grid.DataBind();

                if (this.isSum)
                {
                    setSumRowStyle();
                }
                this.setCustomInfo(this.anPager, this.totalRecord, this.pageSize);
            }

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "clear", "this.selectCheck=new Array();SetValue(GetElementById('hfSelectCheck'),'');", true);
        }

        /// <summary>
        /// 绑定全部记录
        /// </summary>
        protected void bindGridAll()
        {
            DataTable dt = this.getGridAllDataSource();

            //如果有符合要求的记录但当前页不是第一页,则返回数据库再取数据
            if (this.anPager.CurrentPageIndex != 1 && dt.Rows.Count == 0)
            {
                this.anPager.CurrentPageIndex = 1;
                dt = this.getDataSource();
            }

            this.grid.DataSource = dt;
            this.grid.DataBind();

            if (this.isSum)
            {
                setSumRowStyle();
            }
            this.setCustomInfo(this.anPager, this.totalRecord, this.pageSizeAll);

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "clear", "this.selectCheck=new Array();SetValue(GetElementById('hfSelectCheck'),'');", true);
        }

        /// <summary>
        /// 设置GridView中的汇总行
        /// </summary>
        protected void setSumRowStyle()
        {
            int rowCount = this.grid.Rows.Count;
            if (rowCount > 0)
            {
                this.grid.Rows[rowCount - 1].CssClass = "SumRow";
                this.grid.Rows[rowCount - 1].Cells[0].Text = "合计";

                this.grid.Rows[rowCount - 1].Attributes["onmouseover"] = null;
                this.grid.Rows[rowCount - 1].Attributes["onmouseout"] = null;
                this.grid.Rows[rowCount - 1].Attributes["onclick"] = null;
                this.grid.Rows[rowCount - 1].Attributes["ondblclick"] = null;
            }
            
        }

       

        /// <summary>
        /// 设置HashTable保存查询值
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        protected Hashtable GetSearchControl(Control container)
        {
            string prefix = "Search";
            Hashtable ht = new Hashtable();
            for (int i = 0; i < container.Controls.Count; i++)
            {
                string controlID = (container.Controls[i].ID != null)
                    ? container.Controls[i].ID : "";
                if (controlID.IndexOf(prefix) == 0)
                {
                    if (controlID.Substring(0, prefix.Length) == prefix)
                    {
                        string value = GetCtrlValue(container.Controls[i]);
                        if (value != "")
                        {
                            ht[controlID] = value;
                        }
                    }
                }
            }
            return ht;
        }

        #region 绑定控件
        /// <summary>
        /// 得到控件值
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public string GetCtrlValue(Control ctrl)
        {
            string strCtrlValue = string.Empty;

            if (ctrl is TextBox)
            {
                strCtrlValue = ((TextBox)ctrl).Text.Trim();
            }
            if (ctrl is Label)
            {
                strCtrlValue = ((Label)ctrl).Text.Trim();
            }
            if (ctrl is Literal)
            {
                strCtrlValue = ((Literal)ctrl).Text.Trim();
            }
            if (ctrl is ListControl)
            {
                strCtrlValue = ((ListControl)ctrl).SelectedValue.Trim();
            }
            if (ctrl is HtmlInputHidden)
            {
                strCtrlValue = ((HtmlInputHidden)ctrl).Value.Trim();
            }

            return strCtrlValue;
        }

        /// <summary>
        /// 将数值赋给控件
        /// </summary>
        /// <param name="strValueField">要选中的值</param>
        /// <param name="listctrl">DropDownList，CheckBoxList，RadioButtonList，ListBox控件Id值</param>
        public void SetCtrlValue(string strValueField, Control ctrl)
        {
            if (ctrl != null)
            {
                strValueField = strValueField.Trim();
                try
                {
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = strValueField.ToString().Trim();
                    }

                    if (ctrl is Label)
                    {
                        ((Label)ctrl).Text = strValueField.ToString().Trim();

                    }

                    if (ctrl is HtmlInputHidden)
                    {
                        ((HtmlInputHidden)ctrl).Value = strValueField.ToString().Trim();
                    }

                    if (ctrl is HtmlInputFile)
                    {
                        ((HtmlInputFile)ctrl).Value = strValueField.ToString().Trim();
                    }

                    if (ctrl is HtmlInputText)
                    {
                        ((HtmlInputText)ctrl).Value = strValueField.ToString().Trim();
                    }

                    //DropDownList|CheckBoxList|RadioButtonList|ListBox
                    if ((ctrl is DropDownList) || (ctrl is CheckBoxList) || (ctrl is RadioButtonList) || (ctrl is ListBox))
                    {
                        //＝＝＝DropDownList＝＝＝
                        if (((ListControl)ctrl).SelectedItem != null)
                            ((ListControl)ctrl).SelectedItem.Selected = false;
                        for (int i = 0; i < ((ListControl)ctrl).Items.Count; i++)
                        {
                            if (strValueField == ((ListControl)ctrl).Items[i].Value.Trim())
                            {
                                ((ListControl)ctrl).Items[i].Selected = false;
                                ((ListControl)ctrl).Items[i].Selected = true;
                            }
                        }
                    }

                }
                catch { throw; }
            }

        }
        #endregion


        #endregion
        #endregion

        /// <summary>
        /// 导出Excel中必须重写的
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        public string GetWhereConditionForUser(string userField, int operatorID, string department)
        {
            string result = basecontroller.GetWhereConditionForUser(userField, operatorID, department);
            return result;
        }
    }
}
