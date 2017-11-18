/***************************************************************************
 * 
 *       功能：     操作员信息录入/修改
 *       作者：     彭伟
 *       日期：     2007/01/28
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
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    using SmartSoft.Component;
    using SmartSoft.Domain.Common;
    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Presentation;

    using UDEF.Service;
    using SmartSoft.Service.Implement.Common;


    public partial class OperatorEdit : WebForm<Operators>
    {
        #region Define
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

        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        /// <summary>
        /// 记录编号
        /// </summary>
        protected int rowID
        {
            get
            {
                if (Request.QueryString["opeID"] != null && Request.QueryString["opeID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["opeID"]);
                }
                return -1;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //检测页面权限
            CheckPagePermission();

            if (!Page.IsPostBack)
            {
                try
                {
                    this.BindControls();

                    lt_title.Text = "操作员信息编辑";
                    Operators op = _org.GetOperatorsDetail(this.rowID);
                    
                    //修改
                    if (op != null)
                    {
                        
                        IList<sysOperatorInRole> roleList = _authorization.GetOperatorRoles(this.rowID);
                        IList<sysOperatorSubordinate> subOrdinateList = _authorization.GetOperatorSubordinates(this.rowID);
                        //解密
                        op.opePassword = Security.DecryptDES(op.opePassword);

                        this.FormData = op;
                        if (string.IsNullOrEmpty(this.tb_opeName.Text))
                        {
                            this.tb_opeName.Text = op.opeName;
                        }
                        tb_password.Text = op.opePassword;
                        //tb_UsbKey.Text = key.Text;

                        //所属系统角色
                        foreach (ListItem item in cbl_Role.Items)
                        {
                            foreach (sysOperatorInRole sysrole in roleList)
                            {
                                if (item.Value == sysrole.RoleID.ToString())
                                {
                                    item.Selected = true;
                                }
                            }
                        }

                        foreach (ListItem item in cbl_Subordinate.Items)
                        {
                            foreach (sysOperatorSubordinate syssubordinate in subOrdinateList)
                            {
                                if (item.Value == syssubordinate.SubordinateID.ToString())
                                {
                                    item.Selected = true;
                                }
                            }
                        }

                        //屏蔽密码栏
                        RequiredFieldValidator2.EnableClientScript = false;
                        RequiredFieldValidator2.Enabled = false;

                        
                    }
                    else //新增
                    {

                        lt_title.Text = "操作员信息录入";
                    }
                }
                catch(Exception ex)
                {
                    WebFunc.AjaxAlertClose(ex.Message, Page);
                }
            }
        }

        private void BindControls()
        {
            //绑定CheckBoxList
            IList<sysRole> roleList = _authorization.GetSysRoleList();
            (new WebFunc()).BindListControl<sysRole>(roleList, cbl_Role, "RoleName", "RoleID","","");

            IList<Operators> opeList = _org.GetOperatorsList();
            (new WebFunc()).BindListControl<Operators>(opeList, cbl_Subordinate, "opeName", "opeID", "", "");

            IList<Department> deplist = _org.GetDepartmentList();
            (new WebFunc()).BindListControl<Department>(deplist, ddl_opeDepartmentID, "depName", "depID", "选择部门", "");
        }

        protected void lbt_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Operators op = this.FormData;

                op.opeAddDate = DateTime.Now;
                op.opeAddOperatorID = this.Operator.opeID;
                op.opeModifyDate = DateTime.Now;
                op.opeModifyOperatorID = this.Operator.opeID;
                op.opeUrl = "denglu.png";

                string strScripts = "";
                Operators opOld = _org.GetOperatorsDetail(this.rowID);
                bool isUpdate = opOld != null;
                

                //判断用户名是否重复
                IList<Operators> opList = _org.GetOperatorListByName(op.opeName);
                if (isUpdate)
                {
                    //如果该用户名已经存在并且不是本人
                    if (opList.Count >= 1 && opList[0].opeID != this.rowID)
                    {
                        strScripts = "<script language='javascript'>alert('该用户名已经存在！请修改！');</script>"; ;
                        WebFunc.ExecuteJScript(strScripts, Page);
                        return;
                    }
                }
                else
                {
                    if (opList.Count >= 1)
                    {
                        strScripts = "<script language='javascript'>alert('该用户名已经存在！请修改！');</script>"; ;
                        WebFunc.ExecuteJScript(strScripts, Page);
                        return;
                    }
                }

                ArrayList al_subordinate = new ArrayList();
                ArrayList al_role = new ArrayList();

                //添加所属角色信息
                foreach (ListItem item in cbl_Role.Items)
                {
                    if (item.Selected == true)
                    {
                        sysOperatorInRole sysrole = new sysOperatorInRole();
                        sysrole.RoleID = int.Parse(item.Value);
                        al_role.Add(sysrole);
                    }
                }

                foreach (ListItem item in cbl_Subordinate.Items)
                {
                    if (item.Selected == true)
                    {
                        sysOperatorSubordinate sysSubordinate = new sysOperatorSubordinate();
                        sysSubordinate.SubordinateID = int.Parse(item.Value);
                        al_subordinate.Add(sysSubordinate);
                    }
                }

                string ddCorpID = ConfigurationManager.AppSettings["CorpId"] + "";
                string ddSecrect = ConfigurationManager.AppSettings["AppSecret"] + "";
                string ddDepartmentID = ConfigurationManager.AppSettings["DepartmentID"] + "";
                int uid = 0;
                if (isUpdate)
                {
                    //修改
                    op.opePassword = (tb_opePassword.Text == string.Empty) ? tb_password.Text : tb_opePassword.Text;
                    //加密
                    op.opePassword = Security.EncryptDES(op.opePassword);
                    op.opeWeChatUserID = opOld.opeWeChatUserID;
                    _org.UpdateOperators(op, al_role, al_subordinate);
                    uid = op.opeID;
                }
                else
                {
                    //加密
                    op.opePassword = Security.EncryptDES(op.opePassword);
                    //新增
                    uid = _org.AddOperators(op, al_role, al_subordinate);
                }

                string dingdinguserid = DDHelper.GetUserIDByMobile(ddCorpID, ddSecrect, op.opeMobile, "1");
                if (dingdinguserid == "")
                {
                    string res = DDHelper.AddUserForDingDing(ddCorpID, ddSecrect, uid.ToString(), op.opeName, ddDepartmentID, op.opeMobile);
                    if (res == uid.ToString())
                    {
                        op.opeDingDingUserID = res;
                        _org.UpdateOperators(op);
                    }
                }
                else
                {
                    DDHelper.ModifyUserForDingDing(ddCorpID, ddSecrect, dingdinguserid, op.opeName, op.opeMobile);
                    op.opeDingDingUserID = dingdinguserid;
                    _org.UpdateOperators(op);
                }
                strScripts = "<script language='javascript'>alert('保存成功！');window.opener.document.forms[0].btn_Refresh.click();window.close();</script>";

                //清除缓存
                _userdefinebase.ClearCache("Operators");

                //保存成功
                WebFunc.ExecuteJScript(strScripts, Page);
            }
            catch(Exception ex)
            {
                WebFunc.AlertError(ex.Message, Page);
            }
        }

    }
}
