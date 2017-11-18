using System;


namespace SmartSoft.MobileWeb
{
    public partial class ProjectList : MobilePageBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (IsDebug)
            {
            }
            else
            {
                base.Page_Load(sender, e);
            }
            if (!IsPostBack)
            {
                this.hfCurrentOperatorID.Value = CurrentOperatorID;
                initControls();
            }
        }


        protected void initControls()
        {
            InitDataSourceAddEmpty(ddlpjStatusID, "CD_ProjectStatus");
            InitDataSourceAddEmpty(ddlpjAddOperatorID, "CD_Operators");
            InitDataSourceAddEmpty(ddlpjOperatorID, "CD_Operators");
        }
    }
}