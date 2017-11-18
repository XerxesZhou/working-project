/***************************************************************************
 * 
 *       功能：     对象列表，用于权限设置
 *       作者：     彭伟
 *       日期：     2007/01/29
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
    using SmartSoft.Service.Implement.Common;

    public partial class ObjectList : SmartSoft.Presentation.PageBase
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            IList<sysRole> rolelist = _authorization.GetSysRoleList();
            IList<Department> deplist = _org.GetDepartmentList();
            IList<Operators> oplist = _org.GetOperatorsList();

            GridRole.DataSource = rolelist;
            GridRole.DataBind();

            GridOperator.DataSource = oplist;
            GridOperator.DataBind();
        }

        protected void GridRole_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.cursor='hand';this.style.backgroundColor='#B9D1F3';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';this.style.cursor='hand';");

                //添加"授权"按钮
                int ObjectId = int.Parse(GridRole.DataKeys[e.Row.RowIndex].Value.ToString());
                string html = "<a href='SetObjectPurview.aspx?objectId=" + ObjectId + "'>设置权限</a>";
                e.Row.Cells[e.Row.Cells.Count - 1].Text = html;
            }
        }

        //protected void GridDepartment_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.cursor='hand';this.style.backgroundColor='#B9D1F3';");
        //        //当鼠标离开的时候 将背景颜色还原的以前的颜色
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';this.style.cursor='hand';");

        //        //添加"授权"按钮
        //        int ObjectId = int.Parse(GridDepartment.DataKeys[e.Row.RowIndex].Value.ToString());
        //        string html = "<a href='SetObjectPurview.aspx?objectId=" + ObjectId + "'>设置权限</a>";
        //        e.Row.Cells[e.Row.Cells.Count - 1].Text = html;
        //    }
        //}

        protected void GridOperator_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.cursor='hand';this.style.backgroundColor='#B9D1F3';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';this.style.cursor='hand';");

                //添加"授权"按钮
                int ObjectId = int.Parse(GridOperator.DataKeys[e.Row.RowIndex].Value.ToString());
                string html = "<a href='SetObjectPurview.aspx?objectId=" + ObjectId + "&isoperator=true'>设置权限</a>";
                e.Row.Cells[e.Row.Cells.Count - 1].Text = html;
            }
        }
    }
}
