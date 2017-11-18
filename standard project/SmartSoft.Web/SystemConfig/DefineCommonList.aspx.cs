/***************************************************************************
 * 
 *       功能：     通用定义列表
 *       作者：     肖秋李
 *       日期：     2007/01/31
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SmartSoft.Presentation;


namespace SmartSoft.Web.SystemConfig 
{
    using SmartSoft.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Service.Interface.Data;

    using UDEF.Service;

    public partial class DefineCommon : WebList<defCommon>
    {
        private IDefCommonService _defcommon;
        public IDefCommonService defcommon
        {
            set { _defcommon = value; }
        }

        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        private string TableName
        {
            get
            {
                if (Request.QueryString["TableName"] != null && Request.QueryString["TableName"] != string.Empty)
                {
                    return Request.QueryString["TableName"] + "";
                }
                return "";
            }
        }

        public string TableText
        {
            get
            {
                if (Request.QueryString["TableText"] != null && Request.QueryString["TableText"] != string.Empty)
                {
                    return Request.QueryString["TableText"] + "";
                }
                return "";
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                initToolBar();
                this.ListGrid = this.GridDefCommon;
                //this.bindGrid();
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("读取时出错！错误信息：" + ex.Message, Page);
            }
        }

        #region Event
        protected void GridDefCommon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.dg_list_RowDataBound(sender, e);
        }

        protected void lb_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_del = this.GetCheckedValues();
                foreach (string key in str_list_del)
                {
                    int id = int.Parse(key);
                    if (!_defcommon.GetDefCommon(TableName, id).UseTag)
                    {
                        try
                        {
                            _defcommon.DeleteDefCommon(TableName, id);
                        }
                        catch
                        {
                            _defcommon.StopDefCommon(TableName, id);
                        }
                    }
                    else
                    {
                        _defcommon.StopDefCommon(TableName, id);
                    }
                }

                this.bindGrid();
                this.ReBindGrid();

                //清除缓存
                _userdefinebase.ClearCache(TableName);
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("删除时出错！错误信息：" + ex.Message, Page);
            }
        }

        protected void lb_Search_Click(object sender, EventArgs e)
        {
            this.bindGrid();
            this.ReBindGrid();
        }

        protected void lb_Resume_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str_list_open = this.GetCheckedValues();
                foreach (string key in str_list_open)
                {
                    int id = int.Parse(key);
                    _defcommon.OpenDefCommon(TableName, id);
                }

                this.bindGrid();
                this.ReBindGrid();

                //清除缓存
                _userdefinebase.ClearCache(TableName);
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("启用时出错！错误信息：" + ex.Message, Page);
            }
        }

        #endregion

        #region Mothed
        private void initToolBar()
        {
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.CssClass = "hidden";
            this.ToolBar1.lb_Search.Click += lb_Search_Click;

            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Edit();");

            this.ToolBar1.lb_Resume.Visible = true;
            this.ToolBar1.lb_Resume.Click += lb_Resume_Click;
            this.ToolBar1.lb_Resume.Attributes.Add("onclick", "return Resume();");

            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "return Delete();");
        }

        private void bindGrid()
        {
            IList<defCommon> defCommonlist = _defcommon.GetDefCommonList(TableName);

            this.DataSource = defCommonlist;
        }
        #endregion
    }
}
