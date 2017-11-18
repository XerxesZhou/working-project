using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using System.Data;
using SmartSoft.Component;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Security.Cryptography;
using System.Web.UI.WebControls;

namespace SmartSoft.MobileWeb
{
    public class MobilePageBase : System.Web.UI.Page
    {
        HttpHelper httpx = new HttpHelper();
        public string rtuserinfo;
        Hashtable signPackage = new Hashtable();

        public string RtUserInfo()
        {
            return rtuserinfo;
        }
        public Hashtable RtHashtable()
        {
            return signPackage;
        }
        public string RtToken()
        {
            return ZToken.GetToken();
        }

        public static string GetSwcSH1(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        private static string createNonceStr()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>double</returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = Convert.ToInt32((time - startTime).TotalSeconds);
            return intResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl">post地址</param>
        /// <param name="paramData">参数</param>
        /// <param name="dataEncode">数据格式</param>
        /// <returns></returns>
        public static string HttpPost(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {
                Encoding dataEncode = Encoding.GetEncoding("utf-8");
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json; charset=utf-8";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        public bool IsDebug
        {
            get
            {
                var isDebug = ConfigurationManager.AppSettings["IsDebug"] + "";
                if (!string.IsNullOrEmpty(isDebug))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string CurrentOperatorID
        {
            get
            {
                if (IsDebug)
                {
                    return "1";
                }

                return Session["CurrentOperatorID"] + "";
            }

            set
            {
                Session["CurrentOperatorID"] = value;
            }
        }

        public string CurrentOperatorName
        {
            get
            {
                if (IsDebug)
                {
                    return "管理员";
                }
                return Session["CurrentOperatorName"] + "";
            }
            set
            {
                Session["CurrentOperatorName"] = value;
            }
        }

        public string AccessToken
        {
            get
            {
                if (Cache["AccessToken"] + "" == "")
                {
                    AccessTokenResponse accesstoken = DDHelper.GetAccessToken(CorpID, AppSecret);
                    Cache.Insert("AccessToken", accesstoken.access_token, null, System.DateTime.UtcNow.AddSeconds(7000), System.Web.Caching.Cache.NoSlidingExpiration);
                    AddsysLog(accesstoken.access_token);
                }

                return Cache["AccessToken"] + "";
            }
        }

        public void AddsysLog(string message)
        {
            DbHelperSQL.ExecuteSQL("insert into sysLog(LogTime, Message) values(getdate(), '" + message + "')");
        }

        public string CorpID
        {
            get
            {
                return ConfigurationManager.AppSettings["CorpId"];
            }
        }

        public string AppSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["AppSecret"];
            }
        }

        public string AgentID
        {
            get
            {
                return ConfigurationManager.AppSettings["AgentID"];
            }
        }

        public bool HasViewAllCustomer(string opeID)
        {
            if (IsDebug)
            {
                return true;
            }
            DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from Operators where opeID = {0} and opeDataRange = 3", opeID));
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HasViewDepartment(string opeID)
        {
            if (IsDebug)
            {
                return true;
            }
            DataTable dt = DbHelperSQL.GetDataTable(string.Format("select * from Operators where opeID = {0} and opeDataRange = 2", opeID));
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HasViewSepcialPage(string url, string opeID)
        {
            if (IsDebug)
            {
                return true;
            }
            DataTable dt = DbHelperSQL.GetDataTable(string.Format(@"select * from sysObjectPurview A INNER JOIN sysPage B ON A.PageID = B.PageID 
                where PageFilePath like '%" + url + @"'  and PurviewCode > 0 and (ObjectID = {0} OR ObjectID in (select RoleID from sysOperatorInRole where OperatorID = {0}))", CurrentOperatorID));
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string PCWebFilePath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/CustomerFile/";
            }
        }

        public string PCWebDomain
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "";
            }
        }

        //从手机端录入操作信息
        public void AddOperatorLog(string dosth, int customerID)
        {
            CommonFunction.AddOperatorLog(dosth, customerID, CurrentOperatorID);
        }

        public void AddOperatorLog(string dosth)
        {
            AddOperatorLog(dosth, 0);
        }


        public void SetMessageReaded(int operatorID, int messageID)
        {
            DbHelperSQL.ExecuteSQL(string.Format("if not exists(select * from sysMessage_Readed where MessageID={0} and OperatorID = {1}) begin Insert into sysMessage_Readed (MessageID,OperatorID) values({0},{1}); end", messageID, operatorID));
        }

        public string GetPermissionWhereCondition(string operatorField)
        {
            string whereCondition = "";
            bool hasViewAllCustomer = HasViewAllCustomer(CurrentOperatorID);
            if (!hasViewAllCustomer)
            {
                string condition = " OR opeID in (select SubOrdinateID from sysOperatorSubordinate where OperatorID = " + CurrentOperatorID + ")";
                bool hasViewDepartment = HasViewDepartment(CurrentOperatorID);
                if (hasViewDepartment)
                {
                    string departmentID = DbHelperSQL.GetSHSL("Select opeDepartmentID from Operators where opeID = " + CurrentOperatorID);
                    whereCondition += " AND " + operatorField + " in (select opeID from Operators where opeDepartmentID = " + departmentID + condition + ")";
                }
                else
                {
                    whereCondition += " AND " + operatorField + " in (select opeID from Operators where opeID = " + CurrentOperatorID + condition + ")";
                }
            }
            return whereCondition;
        }

        public void GetParmaters()
        {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("access_token", AccessToken);
            var info1 = httpx.GetJson("https://oapi.dingtalk.com/get_jsapi_ticket", myDictionary);

            var jser = new JavaScriptSerializer();
            DTServerClass.GetTicket getTicket = jser.Deserialize<DTServerClass.GetTicket>(info1);
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
            string nonceStr = createNonceStr();

            // 这里参数的顺序要按照 key 值 ASCII 码升序排序 
            string rawstring = "jsapi_ticket=" + getTicket.ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url;
            string signature = GetSwcSH1(rawstring).ToLower();

            signPackage.Add("agentId", AgentID);
            signPackage.Add("corpId", CorpID);
            signPackage.Add("timeStamp", timestamp);
            signPackage.Add("nonceStr", nonceStr);
            signPackage.Add("signature", signature);
        }

        public void ConfigDingDing()
        {
            GetParmaters();
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "ddconfig", "$(function(){configDingDing('" + RtHashtable()["agentId"] + "','" + RtHashtable()["corpId"] + "','" + RtHashtable()["timeStamp"] + "','" + RtHashtable()["nonceStr"] + "','" + RtHashtable()["signature"] + "','" + CurrentOperatorID + "');})", true);
            //避免没有考虑的地方直接使用而报错
            if (CurrentOperatorID == "")
            {
                CurrentOperatorID = "-1";
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (IsDebug)
            {
                return;
            }
            
            string party = ConfigurationManager.AppSettings["Party"];
            if (party.ToLower() == "dingding")
            {
                #region 钉钉
                ConfigDingDing();

                #endregion
            }
            else if (party.ToLower() == "wechat")
            {
                #region 微信
                if (string.IsNullOrEmpty(CurrentOperatorID))
                {
                    if (HttpContext.Current.Request.QueryString["code"] + "" == "")
                    {
                        string encodeUrl = Server.UrlEncode(Request.Url.ToString());
                        string redirect = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", CorpID, encodeUrl);
                        Response.Redirect(redirect);
                    }
                    else
                    {
                        string code = HttpContext.Current.Request.QueryString["code"] + "";
                        if (!SessionManager.Contains(code))
                        {
                            SessionManager.RemoveNotInUsed();
                            if (AccessToken == null)
                            {
                                string accToken = AccessTokenContainer.TryGetToken(CorpID, AppSecret, true);
                                //AccessToken = accToken;
                            }
                            GetUserInfoResult userInfo = null;
                            userInfo = OAuth2Api.GetUserId(AccessToken, HttpContext.Current.Request.QueryString["code"] + "", 5);
                            string operatorID = DbHelperSQL.GetSHSL(string.Format("select top 1 [opeID] from Operators where opeName = '{0}' OR opeWeChatUserID='{0}'", userInfo.UserId));

                            SessionItem sItem = new SessionItem();
                            sItem.Code = code;
                            sItem.FromUserName = userInfo.UserId;
                            sItem.DeviceID = userInfo.DeviceId;
                            sItem.ID = operatorID;
                            sItem.UpdateTime = DateTime.Now;
                            SessionManager.Set(code, sItem);
                        }

                        //填入你的实际corpid，必填
                        CorpInfo._ZCorpID = CorpID;
                        //填入你的实际corpsecret，必填
                        CorpInfo._ZCorpSecret = AppSecret;
                        if (ZToken.IsTimeOut())
                        {
                            ZToken.GetNewToken();
                        }
                        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                        myDictionary.Add("access_token", ZToken.GetToken());
                        var info1 = httpx.GetJson("https://oapi.dingtalk.com/get_jsapi_ticket", myDictionary);

                        var jser = new JavaScriptSerializer();
                        DTServerClass.GetTicket getTicket = jser.Deserialize<DTServerClass.GetTicket>(info1);
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
                        string nonceStr = createNonceStr();

                        // 这里参数的顺序要按照 key 值 ASCII 码升序排序 
                        string rawstring = "jsapi_ticket=" + getTicket.ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url;
                        string signature = GetSwcSH1(rawstring).ToLower();

                        signPackage.Add("agentId", AgentID);
                        signPackage.Add("corpId", CorpInfo._ZCorpID);
                        signPackage.Add("timeStamp", timestamp);
                        signPackage.Add("nonceStr", nonceStr);
                        signPackage.Add("signature", signature);
                    }
                }
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "ddconfig", "$(function(){configDingDing('" + RtHashtable()["agentId"] + "','" + RtHashtable()["corpId"] + "','" + RtHashtable()["timeStamp"] + "','" + RtHashtable()["nonceStr"] + "','" + RtHashtable()["signature"] + "');})", true);
                #endregion
            }
            if (!IsPostBack)
            {
                handleMessageRead();
            }
        }


        private void handleMessageRead()
        {
            if (CurrentOperatorID != "" && Request.QueryString["MessageID"] + "" != "")
            {
                SetMessageReaded(int.Parse(CurrentOperatorID), int.Parse(Request.QueryString["MessageID"] + ""));
            }
        }

        public void InitDataSourceAddEmpty(DropDownList ddl, string dataName)
        {
            CommonFunction.InitDataSourceAddEmpty(ddl, dataName);
        }

        public void InitDataSource(DropDownList ddl, string dataName)
        {
            CommonFunction.InitDataSource(ddl, dataName);
        }

        public string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID)
        {
            return CommonFunction.GetLikeAndCommentCountHtml(cfhID, cfhOperatorID, CurrentOperatorID);
        }
    }

 

    public class SessionManager
    {
        /// <summary>
        /// 缓存hashtable
        /// </summary>
        private static IDictionary<string, SessionItem> mDic = new Dictionary<string, SessionItem>();

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">key</param>
        public static void Remove(string key)
        {
            if (Contains(key))
            {
                mDic.Remove(key);
            }
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, SessionItem value)
        {
            mDic[key] = value;
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SessionItem Get(string key)
        {
            return mDic[key];
        }
        /// <summary>
        /// 是否含有
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>bool</returns>
        public static bool Contains(string key)
        {
            return mDic.ContainsKey(key);
        }
        /// <summary>
        /// 清空所有项
        /// </summary>
        public static void Clear()
        {
            mDic.Clear();
        }

        public static void RemoveNotInUsed()
        {
            foreach (SessionItem item in mDic.Values)
            {
                if (item.UpdateTime.AddSeconds(7000) < DateTime.Now)
                {
                    Remove(item.Code);
                }
            }
        }
    }

    public class SessionItem
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoRemove
        {
            get;
            set;
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public class ZToken
    {
        private static HttpHelper http = new HttpHelper();
        public static ZToken _Token;
        public string access_token { get; set; }
        public int expires_in = 7200;//正常情况下有效期为7200秒
        private DateTime createTokenTimme = DateTime.Now;

        /// <summary>
        /// 到期时间(防止时间差，提前1分钟到期)
        /// </summary>
        public DateTime TookenOverdueTime
        {
            get { return createTokenTimme.AddSeconds(expires_in - 60); }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static void Renovate()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            ZToken._Token.createTokenTimme = DateTime.Now;
        }

        public static bool IsTimeOut()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            return DateTime.Now >= ZToken._Token.TookenOverdueTime;
        }

        public static ZToken GetNewToken()
        {
            string strulr = "https://oapi.dingtalk.com/gettoken";
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("corpid", CorpInfo._ZCorpID);
            myDictionary.Add("corpsecret", CorpInfo._ZCorpSecret);

            string respone = http.GetJson(strulr, myDictionary);

            var jser = new JavaScriptSerializer();
            var token = jser.Deserialize<ZToken>(respone);
            ZToken._Token = token;
            return token;
        }
        public static string GetToken()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            return _Token.access_token;
        }
    }

    public class CorpInfo
    {
        public static string _ZCorpID { get; set; }

        public static string _ZCorpSecret { get; set; }

    }

    public class DTServerClass
    {
        public class CodeGetUserInfo
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string userid { get; set; }
            public string deviceId { get; set; }
            public string is_sys { get; set; }
            public string sys_level { get; set; }
        }

        public class GetUserDetail
        {
            public int errocde { get; set; }
            public string errmsg { get; set; }
            public string userid { get; set; }
            public string name { get; set; }
            public string tel { get; set; }
            public string workPlace { get; set; }
            public string remark { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public Boolean active { get; set; }
            public string orderInDepts { get; set; }
            public Boolean isAdmin { get; set; }
            public Boolean isBoss { get; set; }
            public string dingId { get; set; }
            public string isLeaderInDepts { get; set; }
            public Boolean isHide { get; set; }
            public int[] department { get; set; }
            public string position { get; set; }
            public string avatar { get; set; }
            public string jobnumber { get; set; }

        }

        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        public class GetTicket
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
        }
    }

    public class HttpHelper
    {
        public string GetJson(string url, Dictionary<string, string> headers = null)
        {
            string _headers = string.Empty;
            string retString = string.Empty;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _headers = _headers + item.Key + "=" + item.Value + "&";
                }
                _headers = _headers.Substring(0, _headers.Length - 1);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (headers == null ? "" : "?") + _headers);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Timeout = 300 * 1000;//超时300秒
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
            {
                retString = myStreamReader.ReadToEnd();
            }
            return retString;
        }
    }
}