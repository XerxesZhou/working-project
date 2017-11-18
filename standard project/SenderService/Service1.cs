using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
 
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SmartSoft.Component;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace SenderService
{
    public partial class Service1 : ServiceBase
    {
        //Email sender
        private System.Timers.Timer _emailtimer;
        private bool _isemailworking = false;
        private string emailAcount = "";
        private string emailPWD = "";
        private string emailSmtp = "";
        private int emailFailedMax = 5;
        private bool emailAddExtra = false;

        //Sms sender
        private System.Timers.Timer _smstimer;
        private bool _issmsworking = false;
        private int corpID = -1;
        private string serverName = "";
        private string loginName = "";
        private string password = "";
        private int smsFailedMax = 5;
        private string suffix = "";
        private bool smsAddExtra = false;

        //DingDing sender
        private System.Timers.Timer _ddtimer;
        private bool _isddworking = false;
        private string ddCorpID = "";
        private string ddSecrect = "";
        private string ddDomain = "";
        private string ddAgentID = "";
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Email sender
                if (ConfigurationManager.AppSettings["EmailActive"] + "" == "1")
                {
                    int intervalToCheck = 30000;
                    int.TryParse(ConfigurationManager.AppSettings["EmailIntervalToCheck"], out intervalToCheck);
                    int.TryParse(ConfigurationManager.AppSettings["EmailFailedMax"], out emailFailedMax);
                    _emailtimer = new System.Timers.Timer(intervalToCheck);
                    _emailtimer.Elapsed += new System.Timers.ElapsedEventHandler(emailtimer_Elapsed);
                    emailAcount = ConfigurationManager.AppSettings["EmailAccount"];
                    emailPWD = ConfigurationManager.AppSettings["EmailPassword"];
                    emailSmtp = ConfigurationManager.AppSettings["EmailSMTP"];
                    emailAddExtra = ConfigurationManager.AppSettings["EmailAddExtra"] + "" == "1";
                    this._emailtimer.Enabled = true;
                    this._emailtimer.Start();
                    LogEmailMessage(0, "Email Sending Service started!");
                }
                else
                {
                    LogEmailMessage(0, "Email Service not active!");
                }

                //Sms sender
                if (ConfigurationManager.AppSettings["SmsActive"] + "" == "1")
                {
                    int intervalToCheckSms = 30000;
                    int.TryParse(ConfigurationManager.AppSettings["SmsIntervalToCheck"], out intervalToCheckSms);
                    int.TryParse(ConfigurationManager.AppSettings["SmsFailedMax"], out smsFailedMax);
                    _smstimer = new System.Timers.Timer(intervalToCheckSms);
                    _smstimer.Elapsed += new System.Timers.ElapsedEventHandler(smstimer_Elapsed);
                    int.TryParse(ConfigurationManager.AppSettings["SmsCorpID"], out corpID);
                    serverName = ConfigurationManager.AppSettings["SmsServer"];
                    loginName = ConfigurationManager.AppSettings["SmsLoginName"];
                    password = ConfigurationManager.AppSettings["SmsPassword"];
                    suffix = ConfigurationManager.AppSettings["SmsSuffix"];
                    smsAddExtra = ConfigurationManager.AppSettings["SmsAddExtra"] + "" == "1";
                    this._smstimer.Enabled = true;
                    this._smstimer.Start();
                    LogSmsMessage(0, "Sms Sending Service started!");
                }
                else
                {
                    LogSmsMessage(0, "Sms Service not active!");
                }

                //DingDing sender
                if (ConfigurationManager.AppSettings["DingDingActive"] + "" == "1")
                {
                    int intervalToCheckDingDing = 30000;
                    int.TryParse(ConfigurationManager.AppSettings["DingDingIntervalToCheck"], out intervalToCheckDingDing);
                    _ddtimer = new System.Timers.Timer(intervalToCheckDingDing);
                    _ddtimer.Elapsed += new System.Timers.ElapsedEventHandler(ddtimer_Elapsed);
                    ddCorpID = ConfigurationManager.AppSettings["DingDingCorpID"] + "";
                    ddSecrect = ConfigurationManager.AppSettings["DingDingSecrect"] + "";
                    ddDomain = ConfigurationManager.AppSettings["DingDingDomain"] + "";
                    ddAgentID = ConfigurationManager.AppSettings["DingDingAgentID"] + "";
                    this._ddtimer.Enabled = true;
                    this._ddtimer.Start();
                    LogSmsMessage(0, "DingDing Sending Service started!");
                }
                else
                {
                    LogSmsMessage(0, "DingDing Service not active!");
                }
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at Application_Start:" + ex.ToString());
                LogSmsMessage(0, "Error occurred at Application_Start:" + ex.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                //Email sender
                LogEmailMessage(0, "Email Sending Service stopped!");
                this._emailtimer.Stop();
                this._emailtimer.Enabled = false;

                //Sms sender
                LogSmsMessage(0, "Sms Sending Service stopped!");
                this._smstimer.Stop();
                this._smstimer.Enabled = false;

                this.emailthread.Abort();
                this.smsthread.Abort();
            }
            catch (ThreadAbortException eexx)
            {
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at Application_End:" + ex.ToString());
                LogSmsMessage(0, "Error occurred at Application_End:" + ex.ToString());
            }
        }

        Thread emailthread;
        private void emailtimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (IsConnectInternet())
                {
                    if (!_isemailworking)
                    {
                        _isemailworking = true;
                        emailthread = new Thread(new ThreadStart(SendEmail));
                        emailthread.Start();
                    }
                }
                else
                {
                    LogEmailMessage(0, "网络不通！");
                }
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at emailtimer_Elapsed:" + ex.ToString());
            }
        }

        Thread smsthread;
        private void smstimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //网络通才工作
                if (IsConnectInternet())
                {
                    if (!_issmsworking)
                    {
                        _issmsworking = true;
                        smsthread = new Thread(new ThreadStart(SendMobileMessage));
                        smsthread.Start();
                    }
                }
                else
                {
                    LogSmsMessage(0, "网络不通");
                }
            }
            catch (Exception ex)
            {
                LogSmsMessage(0, "Error occurred at smstimer_Elapsed:" + ex.ToString());
            }
        }

        Thread ddthread;
        private void ddtimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //网络通才工作
                if (IsConnectInternet())
                {
                    if (!_isddworking)
                    {
                        _isddworking = true;
                        ddthread = new Thread(new ThreadStart(SendDingDingMessage));
                        ddthread.Start();
                    }
                }
                else
                {
                    LogSmsMessage(0, "网络不通");
                }
            }
            catch (Exception ex)
            {
                LogSmsMessage(0, "Error occurred at ddtimer_Elapsed:" + ex.ToString());
            }
        }

        #region Email Sender
        public void LogEmailMessage(int ID, string message)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("insert into EmailLog values(" + ID + ",'" + message + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            catch { }
        }

        private EmailItem currentEmailItem;
        private IList<EmailItem> listEmailItem = new List<EmailItem>();
        public void SendEmail()
        {
            try
            {
                DataTable dt = DbHelperSQL.GetDataTable(@"select E.EmailAccount,E.EmailPWD,A.*,B.Subject, B.Content,C.cusName from EmailItem A inner join EmailMain B ON A.EmailID = B.ID INNER JOIN Customer C ON A.CustomerID = C.cusID
	INNER JOIN Operators D ON B.AddOperatorID = D.opeID LEFT OUTER JOIN Department E ON D.opeDepartmentID = E.depID
 where Status = 0");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        EmailItem item = new EmailItem();
                        item.ID = int.Parse(r["ID"].ToString());
                        item.EmailID = int.Parse(r["EmailID"].ToString());
                        item.CustomerID = int.Parse(r["CustomerID"].ToString());
                        item.LinkManID = int.Parse(r["LinkManID"].ToString());
                        item.LinkManName = r["LinkManName"].ToString();
                        item.EmailAddress = r["EmailAddress"].ToString();
                        item.EmailAccount = r["EmailAccount"].ToString();
                        item.EmailPWD = r["EmailPWD"].ToString();

                        if (emailAddExtra)
                        {
                            item.Subject = r["cusName"] + " " + r["Subject"].ToString();
                        }
                        else
                        {
                            item.Subject = r["Subject"].ToString();
                        }

                        item.Content = r["Content"].ToString();
                        listEmailItem.Add(item);
                    }

                }
                if (listEmailItem.Count > 0)
                {
                    _isemailworking = true;
                    DoSendEmailWork();
                }
                else
                {
                    UpdateEmailActiveTime();
                    _isemailworking = false;
                    emailthread.Abort();
                }
            }
            catch (ThreadAbortException exxx)
            {
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at SendEmail:" + ex.ToString());
            }
        }

        bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public void DoSendEmailWork()
        {
            try
            {
                if (listEmailItem.Count > 0)
                {
                    currentEmailItem = listEmailItem[0];

                    string from = emailAcount;
                    MailMessage newEmail = new MailMessage();
                    newEmail.From = new MailAddress(from, from);
                    string[] addrs = currentEmailItem.EmailAddress.Split(@"|/,、;；，".ToCharArray());
                    foreach (string ad in addrs)
                    {
                        if (!string.IsNullOrEmpty(ad) && IsValidEmail(ad))
                        {
                            newEmail.To.Add(new MailAddress(ad));
                        }
                    }
                    newEmail.Subject = currentEmailItem.Subject;
                    newEmail.Body = currentEmailItem.Content;
                    newEmail.IsBodyHtml = true;                //是否支持html
                    newEmail.Priority = MailPriority.High;  //优先级

                    //发送方服务器信息
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.UseDefaultCredentials = true;
                    if (currentEmailItem.EmailAccount.Length == 0)
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential(emailAcount, emailPWD);
                    }
                    else
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential(currentEmailItem.EmailAccount, currentEmailItem.EmailPWD);
                    }
                    smtpClient.Host = emailSmtp; //主机

                    #region 异步发送, 会进入回调函数SendCompletedCallback，来判断发送是否成功
                    smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);//回调函数
                    string userState = "测试";
                    smtpClient.SendAsync(newEmail, userState);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at DoSendEmailWork:" + ex.ToString());
            }
        }

        private void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled || e.Error != null)  //邮件发送不成功
                {
                    LogEmailMessage(currentEmailItem.ID, "发送被取消或失败！");
                    IncreaseCurrentEmailFailedTime();
                }
                else   //发送成功
                {
                    LogEmailMessage(currentEmailItem.ID, "发送成功！");
                    UpdateCurrentEmailStatus(1);
                }

                if (listEmailItem.Count > 0)
                {
                    listEmailItem.RemoveAt(0);
                }

                if (listEmailItem.Count > 0)
                {
                    DoSendEmailWork();
                }
                else
                {
                    UpdateEmailItemToFailedStatus();
                    _isemailworking = false;
                    emailthread.Abort();
                }
            }
            catch (ThreadAbortException exxx)
            {
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at SendCompletedCallback:" + ex.ToString());
            }
        }

        private void UpdateCurrentEmailStatus(int status)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Update EmailItem set Status = " + status + ", HandledDate = getdate() where ID = " + currentEmailItem.ID);
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at UpdateCurrentEmailStatus:" + ex.ToString());
            }
        }

        private void IncreaseCurrentEmailFailedTime()
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Update EmailItem set TryTotalTime = TryTotalTime + 1, HandledDate = getdate() where ID = " + currentEmailItem.ID);
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at IncreaseCurrentEmailFailedTime:" + ex.ToString());
            }
        }

        private void UpdateEmailItemToFailedStatus()
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Update EmailItem set Status = 2 where TryTotalTime >= " + emailFailedMax);
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at UpdateEmailItemToFailedStatus:" + ex.ToString());
            }
        }

        private void UpdateEmailActiveTime()
        {
            try
            {
                string c = DbHelperSQL.GetSHSL("select ISNULL(count(*),0) from EmailLog where Message = '没有需要发送的邮件'");

                if (int.Parse(c) > 0)
                {
                    DbHelperSQL.ExecuteSQL("Update EmailLog set AddTime = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Message = '没有需要发送的邮件'");
                }
                else
                {
                    LogEmailMessage(0, "没有需要发送的邮件");
                }
            }
            catch (Exception ex)
            {
                LogEmailMessage(0, "Error occurred at UpdateEmailActiveTime:" + ex.ToString());
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        /// <summary>
        /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败 
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }
        #endregion

        #region Sms sender
        public void SendMobileMessage()
        {
            try
            {

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.Content,C.clmName,C.clmSex from SmsItem A inner join SmsMain B ON A.SmsID = B.ID INNER JOIN CustomerLinkMan C ON A.LinkManID = C.clmID where Status = 0 and SendDate <= '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "'");
                if (dt.Rows.Count > 0)
                {
                    int connectResult = -99;
                    string strText = "";
                    try
                    {
                        IntPtr hWnd = include.FindWindow(null, "测试");
                        connectResult = include.Sms_Connect(serverName, corpID, loginName, password, 30, hWnd);
                        
                        strText = "";
                        switch (connectResult)
                        {
                            case 0:
                                strText = "初始化成功!";
                                break;
                            case -1:
                                strText = "连接服务器失败!";
                                break;
                            case -2:
                                strText = "连接服务器超时!";
                                break;
                            case -3:
                                strText = "登录失败，帐号有误!";
                                break;
                            case -4:
                                strText = "登录失败，相同帐号已在别处登录!";
                                break;
                            default:
                                strText = "连接失败,无相关失败信息!";
                                break;
                        }

                    }
                    catch (Exception ex1)
                    {
                        _issmsworking = false;
                        strText = "连接Sms服务器发生异常:  " + ex1.ToString();
                        LogSmsMessage(0, strText);
                        return;
                    }

                    LogSmsMessage(0, strText);

                    if (connectResult == 0)
                    {
                        int iKYSms = include.Sms_KYSms();
                        LogSmsMessage(0, "可用短信数为:" + iKYSms);
                        string strContent = "";
                        foreach (DataRow r in dt.Rows)
                        {
                            int lSmsID = -1;
                            if (r["MobilePhone"] + "" != "")
                            {
                                string[] ms = r["MobilePhone"].ToString().Split(@"|/,".ToCharArray());
                                foreach (string m in ms)
                                {
                                    if (smsAddExtra)
                                    {
                                        strContent = r["clmName"].ToString();
                                        if (r["clmSex"].ToString().Trim() == "男" && !strContent.Contains("先生"))
                                        {
                                            strContent += "先生";
                                        }
                                        else if (r["clmSex"].ToString().Trim() == "女" && !strContent.Contains("小姐"))
                                        {
                                            strContent += "小姐";
                                        }

                                        strContent += "，您好 " + r["Content"].ToString().Trim();
                                    }
                                    else
                                    {
                                        strContent = r["Content"].ToString().Trim();
                                    }
                                    int sendStatus = include.Sms_Send(m, strContent + suffix, out lSmsID);
                                    UpdateCurrentSmsReturnedID(int.Parse(r["ID"].ToString()), lSmsID);
                                    if (sendStatus > 0)
                                    {
                                        strText = "发送成功 ! 此次操作所发送的短信数量为 " + sendStatus + " 条 手机号为:" + m;
                                    }
                                    else
                                    {
                                        switch (sendStatus)
                                        {
                                            case -1:
                                                strText = "接口未初始化!!";
                                                break;
                                            case -2:
                                                strText = "发送超时!!";
                                                break;
                                            case -3:
                                            case 0:
                                                strText = "发送失败，可能是帐号过期或余额不足!!";
                                                break;
                                            case -4:
                                                strText = "发送失败，发送的信息内容中含有敏感关键字，禁止发送!!";
                                                break;
                                            case -5:
                                                strText = "发送失败，发送的目标号码为黑名单用户，禁止发送。!!";
                                                break;
                                            default:
                                                strText = "发送失败,无相关失败信息!!";
                                                break;
                                        }
                                    }

                                    LogSmsMessage(int.Parse(r["ID"].ToString()), strText);

                                    //为0时可能是账户余额不足
                                    if (sendStatus == 0)
                                        sendStatus = -3;

                                    UpdateCurrentSmsStatus(int.Parse(r["ID"].ToString()), sendStatus);
                                }
                            }
                        }
                    }

                    include.Sms_DisConnect();
                }
                else
                {
                    UpdateSmsActiveTime();
                }
                _issmsworking = false;
                smsthread.Abort();
            }
            catch (ThreadAbortException eexx)
            {
            }
            catch (Exception e)
            {
                _issmsworking = false;
                LogSmsMessage(0, "Error occurred at SendMobileMessage:" + e.ToString());
            }
            LogSmsMessage(0, "SendMobileMessage结束了");
        }

        private void UpdateCurrentSmsStatus(int ItemSmsID, int status)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Update SmsItem set Status = " + status + ",HandledDate = getdate() where ID = " + ItemSmsID);
            }
            catch (Exception ex)
            {
                LogSmsMessage(0, "Error occurred at UpdateCurrentSmsStatus:" + ex.ToString());
            }
        }

        private void UpdateCurrentSmsReturnedID(int ItemSmsID, int ReturnedID)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("Update SmsItem set ReturnedSmsID = " + ReturnedID + " where ID = " + ItemSmsID);
            }
            catch (Exception ex)
            {
                LogSmsMessage(0, "Error occurred at UpdateCurrentSmsReturnedID:" + ex.ToString());
            }
        }

        public void LogSmsMessage(int ID, string message)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("insert into SmsLog values(" + ID + ",'" + message + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateSmsActiveTime()
        {
            try
            {
                string c = DbHelperSQL.GetSHSL("select ISNULL(count(*),0) from SmsLog where Message = '没有需要发送的短信'");

                if (int.Parse(c) > 0)
                {
                    DbHelperSQL.ExecuteSQL("Update SmsLog set AddTime = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Message = '没有需要发送的短信'");
                }
                else
                {
                    LogSmsMessage(0, "没有需要发送的短信");
                }
            }
            catch (Exception ex)
            {
                LogSmsMessage(0, "Error occurred at UpdateSmsActiveTime:" + ex.ToString());
            }
        }
        #endregion

        #region DingDing sender
        public void SendDingDingMessage()
        {
            try
            {

                DataTable dt = DbHelperSQL.GetDataTable(@"select A.*,U.* from sysMessage A INNER JOIN sysMessage_Looker B ON A.MessageID = B.MessageID
INNER JOIN Operators U ON B.ObjectID = U.opeID
LEFT OUTER JOIN sysMessage_Readed C ON B.MessageID = C.MessageID and B.ObjectID = C.OperatorID
LEFT OUTER JOIN sysMessage_DingDingSent D ON B.MessageID = D.MessageID and B.ObjectID = D.ObjectID
where C.MessageID is null and D.MessageID is null and opeDingDingUserID is not null and SendTime <= getdate()");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["opeDingDingUserID"] + "" != "")
                        {
                            bool result = DDHelper.SendDingDingMessage(ddCorpID, ddSecrect, r["opeDingDingUserID"] + "", ddAgentID, r["Title"] + "", r["MessageContent"] + "", ddDomain + r["URL"]);
                        }
                        AddDingDingSentRecord(r["MessageID"] + "", r["opeID"] + "");
                    }
                }
                _isddworking = false;
                ddthread.Abort();
            }
            catch (ThreadAbortException eexx)
            {
            }
            catch (Exception e)
            {
                _isddworking = false;
            }
        }

        

        public void AddDingDingSentRecord(string messageid, string objectid)
        {
            try
            {
                DbHelperSQL.ExecuteSQL("insert into sysMessage_DingDingSent values(" + messageid + ",'" + objectid + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }

    public class EmailItem
    {
        public int ID;
        public int EmailID;
        public int CustomerID;
        public int LinkManID;
        public string LinkManName;
        public string EmailAddress;
        public string Subject;
        public string Content;
        public int Status;
        public int TryTotalTime;
        public string EmailAccount;
        public string EmailPWD;
    }
}
