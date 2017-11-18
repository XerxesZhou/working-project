using System;


namespace SmartSoft.MobileWeb
{
    public partial class CustomerClueList : MobilePageBase
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
            InitDataSourceAddEmpty(ddlccDepartment, "CD_Department");
            InitDataSourceAddEmpty(ddlccOperatorID, "CD_Operators");
            //InitDataSourceAddEmpty(ddlccStatusID, "CD_CustomerClueStatus");
            InitDataSourceAddEmpty(ddlccAddOperatorID, "CD_Operators");
            //ddlccStatusID.SelectedIndex = 1;
        }
    }
}