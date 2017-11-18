/***************************************************************************
 * 
 *       功能：     工具栏
 *       作者：     肖秋李
 *       日期：     2007-2-2
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/

namespace SmartSoft.Web.UserControl
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Component;

    public partial class ToolBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initToolBar();
        }

        private void initToolBar()
        {
            this.lb_GoBack.Attributes.Add("href", "javascript:BackToList();");
            this.lb_Close.Attributes.Add("href", "javascript:window.close();");
            this.lb_Close.CausesValidation = false;
            this.lb_Search.Attributes.Add("onclick", "javascript:SetSearchFlag('Search');ClearSelected();");
            this.lb_SubmitApprove.Attributes.Add("onclick", "javascript:return confirm('确认提交审批吗？');");
            this.lb_ApprovePass.Attributes.Add("onclick", "javascript:return confirm('确认审批通过？');");
            this.lb_ApproveNoPass.Attributes.Add("onclick", "javascript:return confirm('确认审批不通过？');");
            this.lb_CancelApprove.Attributes.Add("onclick", "javascript:return confirm('确认取消审批？');");
        }

        protected void methodCauseValid(object sender, EventArgs e)
        {
            ;
        }

      
    
    }
}