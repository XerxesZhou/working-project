using System;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace SmartSoft.Component
{
    public class DDHelper
    {
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


        public static string GetRedirectURL(string corpID, string encodeUrl)
        {
            return string.Format("https://oapi.dingtalk.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE", corpID, encodeUrl);
        }

        public static AccessTokenResponse GetAccessToken(string corpID, string appSecrect)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}", corpID, appSecrect);
            wr = WebRequest.Create(url);

            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            AccessTokenResponse accesstoken = jser.Deserialize<AccessTokenResponse>(responseData);
            return accesstoken;
        }

        public static UserInfoResponse GetUserInfo(string accessToken, string code)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/user/getuserinfo?access_token={0}&code={1}", accessToken, code);
            wr = WebRequest.Create(url);
            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            UserInfoResponse user = jser.Deserialize<UserInfoResponse>(responseData);
            return user;
        }

        public static UserDetailResponse GetUserDetailInfo(string accessToken, string userid)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/user/get?access_token={0}&userid={1}", accessToken, userid);
            wr = WebRequest.Create(url);
            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            UserDetailResponse userDetail = jser.Deserialize<UserDetailResponse>(responseData);
            
            return userDetail;
        }

        public static string GetUserIDByMobile(string ddCorpID, string ddSecrect, string mobile, string departmentid)
        {
            string userid = "";
            DepartmentItem[] deptList = GetDepartmentList(ddCorpID, ddSecrect, departmentid);
            if (deptList != null && deptList.Length > 0)
            {
                foreach (DepartmentItem dept in deptList)
                {
                    if (userid == "")
                    {
                        userid = GetUserIDByMobile(ddCorpID, ddSecrect, mobile, dept.id);
                    }
                    else
                    {
                        return userid;
                    }
                }
            }
            else
            {
                UserDetailItem[] userList = GetUserDetailList(ddCorpID, ddSecrect, departmentid);
                foreach (UserDetailItem user in userList)
                {
                    if (user.mobile == mobile)
                    {
                        userid = user.userid;
                        return userid;
                    }
                }
            }
            return userid;
        }

        public static UserDetailItem[] GetUserDetailList(string ddCorpID, string ddSecrect, string departmentid)
        {
            AccessTokenResponse access = GetAccessToken(ddCorpID, ddSecrect);
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/user/list?access_token={0}&department_id={1}", access.access_token, departmentid);
            wr = WebRequest.Create(url);
            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            UserDetailListResponse response = jser.Deserialize<UserDetailListResponse>(responseData);
            if (response.errcode == 0)
            {
                return response.userlist;
            }
            return null;
        }

        public static DepartmentItem[] GetDepartmentList(string ddCorpID, string ddSecrect, string parentid)
        {
            AccessTokenResponse access = GetAccessToken(ddCorpID, ddSecrect);
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/department/list?access_token={0}&id={1}", access.access_token, parentid);
            wr = WebRequest.Create(url);
            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            DepartmentListResponse response = jser.Deserialize<DepartmentListResponse>(responseData);
            if (response.errcode == 0)
            {
                return response.department;
            }
            return null;
        }

        public static string AddUserForDingDing(string ddCorpID, string ddSecrect, string userid, string name, string departmentid, string mobile)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";

            var jser = new JavaScriptSerializer();
            try
            {
                url = string.Format("https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}", ddCorpID, ddSecrect);
                wr = WebRequest.Create(url);

                responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
                AccessTokenResponse accesstoken = jser.Deserialize<AccessTokenResponse>(responseData);

                string postUrl = "https://oapi.dingtalk.com/user/create?access_token=" + accesstoken.access_token;
                string paramData = "{\"userid\":\"" + userid + "\",";
                paramData += "\"name\":\"" + name + "\",";
                paramData += "\"department\":[" + departmentid + "],";
                paramData += "\"mobile\":\"" + mobile + "\"";
                paramData += "}";


                responseData = HttpPost(postUrl, paramData);
                UserResponse megResp = jser.Deserialize<UserResponse>(responseData);
                if (megResp.errcode == 0)
                {
                    return megResp.userid;
                }
                return "";
            }
            catch
            {
            }

            return "";
        }

        public static bool ModifyUserForDingDing(string ddCorpID, string ddSecrect, string userid, string name, string mobile)
        {
            string responseData = "";

            var jser = new JavaScriptSerializer();
            try
            {
                AccessTokenResponse accesstoken = GetAccessToken(ddCorpID, ddSecrect);

                string postUrl = "https://oapi.dingtalk.com/user/update?access_token=" + accesstoken.access_token;
                string paramData = "{\"userid\":\"" + userid + "\",";
                paramData += "\"name\":\"" + name + "\",";
                paramData += "\"mobile\":\"" + mobile + "\"";
                paramData += "}";


                responseData = HttpPost(postUrl, paramData);
                BaseResponse megResp = jser.Deserialize<BaseResponse>(responseData);
                if (megResp.errcode == 0)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public static bool DeleteUser(string accessToken, string userid)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";
            url = string.Format("https://oapi.dingtalk.com/user/delete?access_token={0}&userid={1}", accessToken, userid);
            wr = WebRequest.Create(url);

            responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
            responseData = responseReader.ReadToEnd();
            var jser = new JavaScriptSerializer();
            BaseResponse result = jser.Deserialize<BaseResponse>(responseData);
            if (result.errcode == 0)
            {
                return true;
            }
            return false;
        }

        public static bool SendDingDingMessage(string ddCorpID, string ddSecrect, string touser, string agentid, string title, string content, string messageurl)
        {
            string url = "";
            WebRequest wr = null;
            StreamReader responseReader = null;
            string responseData = "";

            var jser = new JavaScriptSerializer();
            try
            {
                url = string.Format("https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}", ddCorpID, ddSecrect);
                wr = WebRequest.Create(url);

                responseReader = new StreamReader(wr.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
                AccessTokenResponse accesstoken = jser.Deserialize<AccessTokenResponse>(responseData);

                string postUrl = "https://oapi.dingtalk.com/message/send?access_token=" + accesstoken.access_token;
                string paramData = "{\"touser\":\"" + touser + "\",";
                paramData += "\"toparty\":\"\",";
                paramData += "\"agentid\":\"" + agentid + "\",";
                paramData += "\"msgtype\":\"link\",";
                paramData += "\"link\":{";
                paramData += "\"messageUrl\":\"" + messageurl + "\",";
                paramData += "\"picUrl\":\"\",";
                paramData += "\"title\":\"" + title + "\",";
                paramData += "\"text\":\"" + content + "\"";
                paramData += "}";
                paramData += "}";


                responseData = HttpPost(postUrl, paramData);
                MessageResponse megResp = jser.Deserialize<MessageResponse>(responseData);
                if (megResp.errcode == 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
            }

            return false;
        }
    }

    public class BaseResponse
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    public class AccessTokenResponse : BaseResponse
    {
        public AccessTokenResponse() { }
        public string access_token { get; set; }

    }

    public class UserResponse : BaseResponse
    {
        public UserResponse() { }
        public string userid { get; set; }
    }


    public class UserDetailResponse : BaseResponse
    {
        public UserDetailResponse() { }
        public string userid { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public object workPlace { get; set; }
        public string remark { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public object orderInDepts { get; set; }
        public bool isAdmin { get; set; }
        public bool isBoss { get; set; }
        public string dingId { get; set; }
        public object isLeaderInDepts { get; set; }
        public bool isHide { get; set; }
        public object department { get; set; }
        public string position { get; set; }
        public string avatar { get; set; }
        public string jobnumber { get; set; }
        public object extattr { get; set; }
    }

    public class UserDetailItem
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public object workPlace { get; set; }
        public string remark { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public object orderInDepts { get; set; }
        public bool isAdmin { get; set; }
        public bool isBoss { get; set; }
        public string dingId { get; set; }
        public object isLeaderInDepts { get; set; }
        public bool isHide { get; set; }
        public object department { get; set; }
        public string position { get; set; }
        public string avatar { get; set; }
        public string jobnumber { get; set; }
        public object extattr { get; set; }
    }

    public class UserInfoResponse : BaseResponse
    {
        public UserInfoResponse() { }
        public string deviceId { get; set; }
        public bool is_sys { get; set; }
        public int sys_level { get; set; }
        public string userid { get; set; }
    }

    public class MessageResponse : BaseResponse
    {
        public string invaliduser;
        public string invalidparty;
        public string messageId;
    }

    public class DepartmentListResponse : BaseResponse
    {
        public DepartmentItem[] department {get;set;}
    }

    public class UserDetailListResponse : BaseResponse
    {
        public bool hasMore { get; set; }
        public UserDetailItem[] userlist { get; set; }
    }

    public class DepartmentItem
    {
        public DepartmentItem() { }
        public string id{get;set;}
        public string name { get; set; }
        public int parentid {get;set;}
        public bool createDeptGroup {get;set;}
        public bool autoAddUser {get;set;}
    }
}
