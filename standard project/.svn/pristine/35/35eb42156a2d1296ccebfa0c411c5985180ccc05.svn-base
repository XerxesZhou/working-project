/***************************************************************************
 * 
 *       功能：     通用定义信息录入/修改
 *       作者：     肖秋李
 *       日期：     2007/02/01
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/

namespace SmartSoft.Web.SystemConfig
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

    using SmartSoft.Component;
    using SmartSoft.Service.Interface.Data;
    using SmartSoft.Domain.Data;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Presentation;

    using UDEF.Service;

    public partial class DefineCommonEdit : WebForm<defCommon>
    {
        private IDefCommonService _defcommon;
        public IDefCommonService defcommon
        {
            set
            { _defcommon = value; }
        }

        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        /// <summary>
        /// 使用URL"ID"传值
        /// </summary>
        private int defID
        {
            get
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["ID"]);
                }
                return 0;
            }
        }


        /// <summary>
        /// action
        /// </summary>
        private string action
        {
            get
            {
                if (Request.QueryString["Action"] != null && Request.QueryString["Action"] != string.Empty)
                {
                    return Request.QueryString["Action"];
                }
                return "";
            }
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

        private string TableText
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

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initPage();
                bindPage();
            }
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                defCommon defCom = new defCommon();
                defCom.ID = int.Parse(this.lb_ID.Text);
                defCom.Name = tb_Name.Text;
                defCom.TableName = TableName;
                defCom.Remark = tb_Remark.Text;
                if (!string.IsNullOrEmpty(tb_OrderBy.Text))
                {
                    defCom.OrderBy = int.Parse(tb_OrderBy.Text);
                }
                if (this.action == "Update")
                {
                    _defcommon.UpdateDefCommon(TableName, defCom);
                }
                else
                {
                    defCom.UseTag = true;
                    _defcommon.AddDefCommon(TableName, defCom);
                }

                //清除缓存
                _userdefinebase.ClearCache(TableName);

                string strScripts = "<script language='javascript'>alert('保存成功！');window.opener.Refresh();window.close();</script>";
                WebFunc.ExecuteJScript(strScripts, Page);
            }
            catch (Exception ex)
            {
                WebFunc.Alert(ex.Message, Page);
            }
        }
        #endregion

        #region Mothed
        private void initPage()
        {
            if (this.action == "Update")
            {
                lt_title.Text = "编辑定义信息";
            }
            else
            {
                lt_title.Text = "录入定义信息";
            }
        }

        private void bindPage()
        {
            if (this.action == "Update")
            {
                #region 绑定值
                defCommon defCom = _defcommon.GetDefCommon(this.TableName, this.defID);
                this.lb_ID.Text = defCom.ID.ToString();
                this.tb_Name.Text = defCom.Name;
                this.tb_Remark.Text = defCom.Remark;
                this.tb_OrderBy.Text = defCom.OrderBy.ToString();

                #endregion
            }
        }
        #endregion
    }
}
