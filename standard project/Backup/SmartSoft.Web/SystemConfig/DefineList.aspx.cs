
namespace SmartSoft.Web.SystemConfig
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

    using SmartSoft.Component;
    using SmartSoft.Domain.Data;
    using SmartSoft.Service.Interface.Data;
    using SmartSoft.Domain.Enumerate;

    public partial class DefineList : SmartSoft.Presentation.PageBase
    {
        private IDefCommonService _defcommon;
        public IDefCommonService defcommon
        {
            set { _defcommon = value; }
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
            IList<defCommon> defCommonlist = _defcommon.GetDefCommonList(TableName);
            this.Grid1.DataSource = defCommonlist;
        }

        protected void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            //sysParameter param = new sysParameter();
            //param.ParameterID = int.Parse(e.Item["ParameterID"].ToString());
            //param.ParameterName = e.Item["ParameterName"].ToString();
            //param.ParameterValue = e.Item["ParameterValue"].ToString();
            //param.ParameterDataType = 1;
            //_authorization.UpdateSysParameter(param);

            defCommon defCom = new defCommon();
            defCom.ID = int.Parse(e.Item["ID"].ToString());
            defCom.Name = e.Item["Name"].ToString();
            defCom.Remark = e.Item["Remark"].ToString();
            defCom.UseTag = bool.Parse(e.Item["UseTag"].ToString());
            defCom.TableName = e.Item["TableName"].ToString();

            _defcommon.UpdateDefCommon(TableName, defCom);
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
