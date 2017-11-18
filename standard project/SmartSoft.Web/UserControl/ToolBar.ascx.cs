/***************************************************************************
 * 
 *       ���ܣ�     ������
 *       ���ߣ�     Ф����
 *       ���ڣ�     2007-2-2
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�   
 *       �޸����ݣ� 
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
            this.lb_SubmitApprove.Attributes.Add("onclick", "javascript:return confirm('ȷ���ύ������');");
            this.lb_ApprovePass.Attributes.Add("onclick", "javascript:return confirm('ȷ������ͨ����');");
            this.lb_ApproveNoPass.Attributes.Add("onclick", "javascript:return confirm('ȷ��������ͨ����');");
            this.lb_CancelApprove.Attributes.Add("onclick", "javascript:return confirm('ȷ��ȡ��������');");
        }

        protected void methodCauseValid(object sender, EventArgs e)
        {
            ;
        }

      
    
    }
}