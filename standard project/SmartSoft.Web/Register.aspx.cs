using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartSoft.Component;
using SmartSoft.Domain.Common;
using System.Collections;
using SmartSoft.Service.Interface.Common;
using SmartSoft.Presentation;
using System.Web.Security;
using System.Configuration;

namespace SmartSoft.Web
{
    public partial class Register : WebForm<Operators>
    {
        private IOrganizationService _org;
        public IOrganizationService org
        {
            set { _org = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                string mobile = this.username.Value;
                string code = tb_code.Text.Trim();
                if (DbHelperSQL.Exists("select * from Operators where opeMobile = '" + mobile.Trim() + "'"))
                {
                    this.lblMsg.Text = "该手机号码已经注册！请直接登录或者重试。";
                    return;
                }

                string message = DbHelperSQL.GetSHSL(string.Format("select top 1 Message from SmsLog where Message like '验证码:{0}:%' and AddTime >='{1}' and AddTime < '{2}' order by AddTime desc" , mobile, DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss")));
                if (!string.IsNullOrEmpty(message))
                {
                    string code2 = message.Replace("验证码:" + mobile + ":", "");
                    if (code == code2)
                    {
                        Operators op = new Operators();
                        op.opeMobile = mobile;
                        op.opeName = this.contactor.Value;
                        op.opeDataRange = 3;
                        op.opeOrderAmount = 100000;
                        op.opeReceiptAmount = 100000;
                        op.opeAddDate = DateTime.Now;
                        op.opeDepartmentID = 8;
                        op.opeIsAdmin = true;
                        op.opeIsCanLogin = true;
                        op.opeLastActiveTime = DateTime.Now;
                        op.opeLastLoginTime = DateTime.Now;
                        //加密
                        string password = Security.EncryptDES(mobile);
                        op.opePassword = password;
                        ArrayList al_subordinate = new ArrayList();
                        ArrayList al_role = new ArrayList();

                        //管理员角色
                        sysOperatorInRole sysrole = new sysOperatorInRole();
                        sysrole.RoleID = 100006;
                        al_role.Add(sysrole);

                        //新增
                        int uid = 0;
                        uid = _org.AddOperators(op, al_role, al_subordinate);

                        string ddCorpID = ConfigurationManager.AppSettings["CorpId"] + "";
                        string ddSecrect = ConfigurationManager.AppSettings["AppSecret"] + "";
                        string ddDepartmentID = ConfigurationManager.AppSettings["DepartmentID"] + "";
                        string adminMobile = ConfigurationManager.AppSettings["AdminMobile"] + "";
                        string ddAgentID = ConfigurationManager.AppSettings["AgentID"] + "";
                        string res = DDHelper.AddUserForDingDing(ddCorpID, ddSecrect, uid.ToString(), op.opeName, ddDepartmentID, op.opeMobile);
                        op.opeDingDingUserID = res;
                        _org.UpdateOperators(op);

                        string adminDingDingUserID = DbHelperSQL.GetSHSL("select opeDingDingUserId from Operators where opeMobile ='" + adminMobile + "'");
                        if (!string.IsNullOrEmpty(adminDingDingUserID))
                        {
                            DDHelper.SendDingDingMessage(ddCorpID, ddSecrect, adminDingDingUserID, ddAgentID, "客户注册通知", op.opeName + "(" + op.opeMobile + ")通过PC注册成功，请及时联系！", "");
                        }

                        bool result = _org.OperatorLogIn(mobile, password, 1000, ref op);
                        if (result)
                        {
                            string Ids = _org.GetOperatorHasIDs(op.opeID);

                            //将相关信息存入Session
                            op.Ids = Ids;
                            Session["Operator"] = op;
                            Session["DepartmentID"] = op.opeDepartmentID.Value;
                            Session["UserID"] = op.opeID;
                            FormsAuthentication.SetAuthCookie(op.opeName, true);
                            this.AddOperatorLog("用户登录系统.");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>OpenMainWindow();</script>");
                        }
                        else
                        {
                            this.lblMsg.Text = "注册失败";
                        }
                    }
                    else
                    {
                        this.lblMsg.Text = "验证码错误，请重新获取后再试。";
                    }
                }
                else
                {
                    this.lblMsg.Text = "验证码错误，请重新获取后再试。";
                }
            }
            catch (Exception exxx)
            {
                this.lblMsg.Text = "注册失败";
            }
        }
    }
}