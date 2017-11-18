using System;
using System.Runtime.InteropServices;
using System.Text;


namespace SmartSoft.Web
{
    public class include
    {
        public include()
        {
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct SmsSend
        {
            public int lSmsID;
            public int reserve;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string szMobile;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 168)]
            public string szMsg;
        }

        public struct SmsSend2
        {
            public int lSmsID; //发送成功后返回的ID号
            public int reserve; //保留，暂未用到;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string szSendExNum; //发送扩展号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string szMobile; //目标手机号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 168)]
            public string szMsg; //短信内容
        }

        public struct SmsSend3
        {
            public int lSmsID; //发送成功后返回的ID号
            public int reserve; //保留，暂未用到;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string szSendExNum; //发送扩展号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string szMobile; //目标手机号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 168)]
            public string szMsg; //短信内容
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szTimer; //时间的格式:2005-08-01 
        }

        [DllImport("user32.dll")]//获得窗口句柄的函数,第一个参数为NULL，指定对当前所有窗口都进行搜索。第二个参数就是待搜寻的窗口标题
        public static extern IntPtr FindWindow(string className, string WindowsName);

        [DllImport("SmsSdk.dll")]//连接短信平台服务器并登录
        public static extern int Sms_Connect(string pServer, int lCorpID, string pLoginName, string pPasswd, int lTimeOut, IntPtr hWnd);

        [DllImport("SmsSdk.dll")]//断开短信平台服务器的连接
        public static extern int Sms_DisConnect();

        [DllImport("SmsSdk.dll")]//修改密码
        public static extern int Sms_ChangePasswd(string pOldPasswd, string pNewPasswd);

        [DllImport("SmsSdk.dll")]//发送指定的短信，它能自动分段发送,每段信息最大长度为70个字
        public static extern int Sms_Send(string pMobile, string pMsg, out int lSmsID);

        [DllImport("SmsSdk.dll")]//发送指定的短信，它能自动分段发送,每段信息最大长度为70个字
        public static extern int Sms_Send2(string pSendExNum, string pMobile, string pMsg, out int lSmsID);

        [DllImport("SmsSdk.dll")]//发送指定的短信，它能自动分段发送,每段信息最大长度为70个字
        public static extern int Sms_Send3(string pSendExNum, string pMobile, string pMsg, string pTimer, out int lSmsID);

        [DllImport("SmsSdk.dll")]//发送指定的短信，它不能自动分段发送,每段信息最大长度为70个字(如带签名，则少于70个字)，超长短信请自行分割
        public static extern int Sms_SendEx(IntPtr pSmsSend, int lCount, int bWaitRet);

        [DllImport("SmsSdk.dll")]
        public static extern int Sms_SendEx2(IntPtr pSmsSend2, int lCount, int bWaitRet);

        [DllImport("SmsSdk.dll")]
        public static extern int Sms_SendEx3(IntPtr pSmsSend3, int lCount, int bWaitRet);

        [DllImport("SmsSdk.dll")]//查询指定短信发送后的状态
        public static extern int Sms_Status(int lSmsID);

        [DllImport("SmsSdk.dll")]//查询本帐号能够使用的剩余短信数
        public static extern int Sms_KYSms();

        [DllImport("SmsSdk.dll")]//接收短信函数
        public static extern int Sms_Get(StringBuilder pNo, StringBuilder pMsg, StringBuilder pTime);

        [DllImport("SmsSdk.dll")]//接收短信函数
        public static extern int Sms_Get2(StringBuilder pSendNo, StringBuilder pRecvNo, StringBuilder pMsg, StringBuilder pTime);

        [DllImport("SmsSdk.dll")]//使用超透网关
        public static extern int Sms_UseGateway(int bUse);
    }
}