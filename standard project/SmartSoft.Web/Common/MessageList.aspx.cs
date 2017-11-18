/***************************************************************************
 * 
 *       功能：     消息列表
 *       作者：     彭伟
 *       日期：     2007/05/09
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Common
{
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

    using System.Collections.Generic;
    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;
    using Wuqi.Webdiyer;

    public partial class MessageList : PageBaseList
    {
        #region Fields
        private IMessageService _message;
        public IMessageService message
        {
            set { _message = value; }
        }

        protected int titleIndex = 3;
        #endregion

        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }
        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //检测页面权限
                CheckPagePermission();

                this.initPage();
                base.Page_Load(sender, e);
                titleIndex = 3;
                hfCurrentOperatorID.Value = this.Operator.opeID.ToString();
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("初始化错误:" + ex.Message, this.Page);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string keyValue = this.hfSelectCheck.Value;
                string[] keyArray = keyValue.Split(",".ToCharArray());
                for (int j = 0; j < keyArray.Length; j++)
                {
                    int key = int.Parse(keyArray[j]);
                    _message.DeleteMessage(key);
                }
                //string message = _message.DeleteMessage(
                base.bindGrid();
                WebFunc.Alert("删除成功！", Page);
            }
            catch (Exception ex)
            {
                base.bindGrid();
                WebFunc.AlertError("删除成功！" + ex.Message, Page);
            }
        }

        protected void GridMessage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string url = GridMessage.DataKeys[e.Row.RowIndex].Values["URL"].ToString();
                string title = GridMessage.DataKeys[e.Row.RowIndex].Values["Title"].ToString();
                if (url != "")
                {
                    e.Row.Cells[titleIndex].Attributes.Add("onclick", "OpenEditForm('../" + url + "',800,600);");
                    e.Row.Cells[titleIndex].Attributes.Add("class", "LinkRow");

                    //判断是否已读
                    int messageID = int.Parse(GridMessage.DataKeys[e.Row.RowIndex].Values["MessageID"].ToString());
                    bool isread = _message.SelectMessageIsRead(messageID, this.Operator.opeID);
                    if (!isread)
                    {
                        e.Row.Cells[titleIndex].Attributes.Add("class", "LinkRow2");
                    }
                }
            }
        }


        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.bindGrid();
        }

        protected void btn_ConfirmSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string content = this.txtContent.Value;
                string users = this.txtUsers.Value;

                sysMessage meg = new sysMessage();
                meg.SendTime = DateTime.Now;
                meg.AwokeTime = DateTime.Now;
                meg.MessageContent = content;
                meg.Title = "内部群消息[" + this.Operator.opeName + "]";
                meg.SendOperatorID = this.Operator.opeID;
                meg.MessageTypeID = 1;
                meg.URL = "";
                ArrayList a = new ArrayList();
                DataTable dt = DbHelperSQL.GetDataTable(string.Format("select opeID from Operators where  ',{0},' like ',%' + opeName + ',%'", users));

                foreach (DataRow row in dt.Rows)
                {
                    a.Add(int.Parse(row["opeID"] + ""));
                }

                _datacontroller.message.InsertMessage(meg, a);

                base.bindGrid();
                WebFunc.Alert("操作成功！", Page);
            }
            catch (Exception ex)
            {
                base.bindGrid();
                WebFunc.AlertError("操作失败！" + ex.Message, Page);
            }
        }

        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += base.lb_Search_Click;
            //删除
            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "javascript:return Delete();");
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;
            //导出
            this.ToolBar1.lb_Export.Visible = true;
            this.ToolBar1.lb_Export.Click += base.lb_Export_Click;
            //设计视图
            this.ToolBar1.lb_DesignView.Visible = true;
        }

        protected void ddl_SelectedChange(object sender, EventArgs e)
        {
                string value = this.ddlDepartment.SelectedValue;
                string id = ddlDepartment.SelectedItem.Text;
                if (value != "")
                {
                   
                    SmartSoft.Component.DbHelperSQL.BindItemList("select * from Operators where opeDepartmentID=" + value, this.ListBox1, "opeName", "opeID");
                }
                else
                {
                   
                    SmartSoft.Component.DbHelperSQL.BindItemList("select * from Operators", this.ListBox1, "opeName", "opeID");
                }
            }
    


        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            //不需要合计行
            base.isSum = false;
            base.Grid = this.GridMessage;
            base.AnPager = this.AspNetPager1;
            base.isQueryPage = true;

            //视图权限
            this.ctrlColumnViewList1 = this.ToolBar1.ddl_ColumnView;
            this.ctrlDesignView1 = this.ToolBar1.lb_DesignView;
            base.requiredColumn = "MessageID,URL";
            base.pageSize = int.Parse(this.ddlPageSize.SelectedValue);
            base.isSort = true;
            base.checkBoxCss = "";

            this.initToolBar();
            if (!IsPostBack)
            {
                SmartSoft.Component.DbHelperSQL.BindDropDownListAddEmpty("select * from Department", ddlDepartment, "depName", "depID");
            }
            SmartSoft.Component.DbHelperSQL.BindDropDownListAddEmpty("select * from Operators where opeLastActiveTime > '" + DateTime.Now.AddSeconds(60) + "'", ActiveOperator, "opeName", "opeID");
            if (!IsPostBack)
            {
                SmartSoft.Component.DbHelperSQL.BindItemList("select * from Operators", this.ListBox1, "opeName", "opeID");
            }
            SmartSoft.Component.DbHelperSQL.BindItemList("select * from Operators where opeLastActiveTime > '" + DateTime.Now.AddSeconds(-60) + "'", this.ListBox2, "opeName", "opeID");
            this.whereCondition += " AND  messageid in (Select messageid from sysMessage_Looker Where objectid=" + this.Operator.opeID + ")" +
                                    " AND messageid not in (Select messageid from sysMessage_Deleted Where operatorid=" + this.Operator.opeID + ")";

            if (!Page.IsPostBack)
            {
                this.SearchSendTimeBegin.Text = DateTime.Now.ToShortDateString();
                this.SearchSendTimeEnd.Text = DateTime.Now.ToShortDateString();
            }
            if (this.ddlStatus.SelectedValue == "已处理")
            {
                this.whereCondition += " AND messageid in (select messageid from sysMessage_Readed where operatorid = " + this.Operator.opeID + ")";
            }
            else if (this.ddlStatus.SelectedValue == "未处理")
            {
                this.whereCondition += " AND messageid not in (select messageid from sysMessage_Readed where operatorid = " + this.Operator.opeID + ")";
            }
        }

        #endregion

       
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ListBox3.Items.Count == 0)
                return;
            for (int i = 0; i < ListBox3.Items.Count; i++)
            {
                int  a= int.Parse(ListBox3.Items[i].Value);
                //string sql = "select * from Operators where opeID" + a;
                _datacontroller.AddMessages(a,"标题","内容","",DateTime.Now);
            }
        }
    }
}
