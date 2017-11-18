/***************************************************************************
 * 
 *       功能：     系统参数定义
 *       作者：     彭伟
 *       日期：     2007/02/02
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

    public partial class SysParameter : SmartSoft.Presentation.PageBase
    {
        private IAuthorizationService _authorization;
        public IAuthorizationService authorization
        {
            set { _authorization = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BuildGrid();
                this.Grid1.DataBind();
            }
        }

        private void BuildGrid()
        {
            System.Threading.Thread.Sleep(500);

            IList<sysParameter> paramlist = _authorization.GetAllSysParameter();

            this.Grid1.DataSource = paramlist;
        }

        protected void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            sysParameter param = new sysParameter();
            param.ParameterID = int.Parse(e.Item["ParameterID"].ToString());
            param.ParameterName = e.Item["ParameterName"].ToString();
            param.ParameterValue = e.Item["ParameterValue"].ToString();
            param.ParameterDataType = 1;
            _authorization.UpdateSysParameter(param);
        }

        protected void Grid1_NeedDataSource(object sender, EventArgs e)
        {
            this.BuildGrid();
        }

        protected void Grid1_NeedRebind(object sender, EventArgs e)
        {
            this.Grid1.DataBind();
        }

        protected void Grid1_SortCommand(object sender, ComponentArt.Web.UI.GridSortCommandEventArgs e)
        {
            Grid1.Sort = e.SortExpression;
        }

        protected void Grid1_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            Grid1.CurrentPageIndex = e.NewIndex;
        }
    }
}
