/***************************************************************************
 * 
 *       ���ܣ�     ϵͳ��ɫ�б�
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007/01/27
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
 * 
 * *************************************************************************/
namespace SmartSoft.Web.Common
{
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

    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Common;
    using SmartSoft.Component;
    using SmartSoft.Presentation;


    public partial class SysRoleList : WebList<sysRole>
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }
        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            //���ҳ��Ȩ��
            CheckPagePermission();

            initToolBar();
            this.ScriptManager1.RegisterAsyncPostBackControl(this.ToolBar1.lb_Delete);
            this.ListGrid = GridRole;
            this.SetDataSource();
            base.Page_Load(sender, e);
        }

        protected void GridRole_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    _authorization.DeleteSysRole(id);
                }

                this.SetDataSource();
                this.ReBindGrid();
            }
            catch (Exception ex)
            {
                WebFunc.AlertError("ɾ��ʱ����������Ϣ��" + ex.Message, Page);
            }
        }
        #endregion

        #region Mothed
        private void initToolBar()
        {
            this.ToolBar1.lb_Add.Visible = true;
            this.ToolBar1.lb_Add.Attributes.Add("href", "javascript:Add();");

            this.ToolBar1.lb_Edit.Visible = true;
            this.ToolBar1.lb_Edit.Attributes.Add("href", "javascript:Edit();");

            //this.ToolBar1.lb_Resume.Visible = true;
            //this.ToolBar1.lb_Resume.Click += lb_Resume_Click;
            //this.ToolBar1.lb_Resume.Attributes.Add("onclick", "return Resume();");

            this.ToolBar1.lb_Delete.Visible = true;
            this.ToolBar1.lb_Delete.Click += lb_Delete_Click;
            this.ToolBar1.lb_Delete.Attributes.Add("onclick", "return Delete();");
        }

        private void SetDataSource()
        {
            IList<sysRole> roleList = _authorization.GetSysRoleList();
            if (roleList == null || roleList.Count == 0)
            {
                roleList.Add(new sysRole());
            }
            this.DataSource = roleList;
        }

        protected void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.SetDataSource();
            this.ReBindGrid();
        }
        #endregion
    }
}
