using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Xml;

namespace ZWL.Common
{
    /// <summary>
    /// PublicMethod 的摘要说明
    /// </summary>
    public class PublicMethod
    {

        public PublicMethod()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //修改web.config节点的值
        public static void EditAppValue(string KeyNameStr,string SetValueStr)
        {           
            //修改web.config
            XmlDocument xDoc = new XmlDocument();
            try
            {
                //打开web.config
                xDoc.Load(System.Web.HttpContext.Current.Request.MapPath("../Web.config"));
                //string key;
                XmlNode app;
                app = xDoc.SelectSingleNode("/configuration/appSettings/add[@key='" + KeyNameStr + "']");
                app.Attributes["value"].Value = SetValueStr;
                //关闭
                xDoc.Save(System.Web.HttpContext.Current.Request.MapPath("../web.config"));
                System.Web.HttpContext.Current.Response.Write("<script>alert('配置数据修改完成！');</script>");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('"+ex.Message.ToString()+"');</script>");
            }
            finally
            {
                xDoc = null;
            }
        }        
        //得到文件列表
        public static string GetWenJian(string WenJianList, string DirStr)
        {
            string[] MyRange = WenJianList.Split('|');
            string MyReturn = string.Empty;
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    if (MyReturn.Trim().Length > 0)
                    {
                        if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                        {
                            MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                        }
                        else
                        {
                            string OldNameStr = ZWL.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                            if (OldNameStr.Trim().Length <= 0)
                            {
                                OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                            }
                            MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../DsoFramer/ReadFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                        }
                    }
                    else
                    {
                        if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                        {
                            MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                        }
                        else
                        {
                            string OldNameStr = ZWL.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                            if (OldNameStr.Trim().Length <= 0)
                            {
                                OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                            }
                            MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../DsoFramer/ReadFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                        }
                    }
                }
            }
            if (MyReturn.ToString().Trim().Length <= 0)
            {
                MyReturn = MyReturn + "无文件！";
            }
            return MyReturn;
        }        
        //将ListItem1中的选中项加入ListItem2中，或者从ListItem2中减去选中项,CanShu1代表是添加，或者去除，CanShu2代表是全部选中项
        public static string GetListStr(ListBox List1, ListBox List2, string CanShu1, string CanShu2)
        {
            if (CanShu1 == "添加")
            {
                if (CanShu2 == "全部")
                {
                    //全部添加
                    for (int i = 0; i < List1.Items.Count; i++)
                    {
                        if (List2.Items.IndexOf(List1.Items[i]) < 0)
                        {
                            List2.Items.Add(List1.Items[i]);
                        }
                    }
                }
                else
                {
                    //部分添加
                    for (int i = 0; i < List1.Items.Count; i++)
                    {
                        if (List1.Items[i].Selected == true)
                        {
                            if (List2.Items.IndexOf(List1.Items[i]) < 0)
                            {
                                List2.Items.Add(List1.Items[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (CanShu2 == "全部")
                {
                    //全部去除
                    List2.Items.Clear();
                }
                else
                {
                    //部分去除
                    for (int i = 0; i < List2.Items.Count; i++)
                    {
                        if (List2.Items[i].Selected == true)
                        {                            
                            List2.Items.Remove(List2.Items[i]);
                            i = i - 1;
                        }
                    }
                }
            }
            //返回选中项的构建字符串
            string ReturnStr = string.Empty;
            for (int j = 0; j < List2.Items.Count; j++)
            {
                if (ReturnStr.Trim().Length > 0)
                {
                    ReturnStr = ReturnStr + "," + List2.Items[j].Text.Trim();
                }
                else
                {
                    ReturnStr = List2.Items[j].Text.Trim();
                }
            }
            return ReturnStr;
        }       
        //从checkBoxList里面读取选中的值
        public static string GetStringFromCheckList(CheckBoxList MyChk)
        {
            string ReturnStr = string.Empty;
            for (int i = 0; i < MyChk.Items.Count; i++)
            {
                if (MyChk.Items[i].Selected == true)
                {
                    ReturnStr = ReturnStr + "|" + MyChk.Items[i].Value.ToString() + "|";
                }
            }
            return ReturnStr;
        }
        //从checkBoxList里面读中字符串中有的值
        public static void GetCheckList(CheckBoxList MyChk, string PerStr)
        {
            for (int i = 0; i < MyChk.Items.Count; i++)
            {
                if (StrIFIn("|" + MyChk.Items[i].Value.ToString() + "|", PerStr) == true)
                {
                    MyChk.Items[i].Selected = true;
                }
                else
                {
                    MyChk.Items[i].Selected = false;
                }
            }
        }
        //绑定字符串分隔开的到CheckBoxList
        public static void BindDDL(CheckBoxList MyDDL, string FenGeStr)
        {
            MyDDL.Items.Clear();
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = MyRange[i].ToString();
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }
        //绑定字符串分隔开的到dropdownlist
        public static void BindDDLForEmPty(DropDownList MyDDL, string FenGeStr)
        {
            ListItem MyListItem1 = new ListItem();
            MyListItem1.Text = "";
            MyListItem1.Value = "";
            MyDDL.Items.Add(MyListItem1);
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = MyRange[i].ToString();
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }
        public static void BindDDL(DropDownList MyDDL, string FenGeStr)
        {
            MyDDL.Items.Clear();
            ListItem MyListItem1 = new ListItem();            
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = MyRange[i].ToString();
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }       
        //在RowDataBound事件时使用
        public static void GridViewRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E4F4FF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
            }
        }
        //判断GridView里面被选中的ID
        public static string CheckCbx(GridView GVData, string CheckBoxName, string LabID)
        {
            string str = "";
            for (int i = 0; i < GVData.Rows.Count; i++)
            {
                GridViewRow row = GVData.Rows[i];
                CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
                Label LabVis = (Label)row.FindControl(LabID);
                if (Chk.Checked == true)
                {
                    if (str == "")
                    {
                        str = LabVis.Text.ToString();
                    }
                    else
                    {
                        str = str + "," + LabVis.Text.ToString();
                    }
                }
            }
            return str;
        }
        //判断Str1是否是在Str2这个长的字符串中
        public static bool StrIFIn(string Str1, string Str2)
        {
            if (Str2.IndexOf(Str1) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //将长字符串取前面250个，然后返回
        public static string LongToShortStr(string LongStr, int StrNum)
        {
            try
            {
                return LongStr.Substring(0, StrNum - 2) + "……";
            }
            catch
            {
                return LongStr;
            }
        }
        //提取Html中的文字信息
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={ @"<script[^>]*?>.*?</script>", @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);", @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n" };
            string[] aryRep = { "", "", "", "\"", "&", "<", ">", " ", "\xa1", "\xa2", "\xa3", "\xa9", "", "\r\n", "" };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }
        //判断文件是否在允许的范围内
        public static bool IfOkFile(string DirName)
        {
            bool ReturnIF = true;
            try
            {
                string FileExd = DirName.Split('.')[1].ToString();
                string JKL = ZWL.DBUtility.DbHelperSQL.GetSHSL("select FileType from ERPSystemSetting where FileType like '%|" + FileExd + "|%'");
                if (JKL.Length < 1)
                {
                    ReturnIF = false;
                }
            }
            catch
            {
                ReturnIF = false;
            }
            return ReturnIF;
        }
        //上传文件
        public static string UploadFileIntoDir(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    MyFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath("../UploadFile/") + DirName);


                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    ZWL.DBUtility.DbHelperSQL.ExecuteSQL(SqlTempStr);


                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length>0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {                    
                    return "";
                }
            }
        }        
        //判断Session是否有效
        public static void CheckSession()
        {
            //测试的时候注释掉
            //try
            //{
            //    if (System.Web.HttpContext.Current.Session["UserName"] == null)
            //    {
            //        System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
            //    }
            //}
            //catch
            //{
            //    System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
            //}
        }
        //获得Session中的值
        public static string GetSessionValue(string SessionKey)
        {
            //测试时候使用，不掉线
            try
            {
                return System.Web.HttpContext.Current.Session[SessionKey].ToString();
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
                return "NoLogin";

                //System.Web.HttpContext.Current.Session["UserID"] = "32";
                //System.Web.HttpContext.Current.Session["UserName"] = "admin";
                //System.Web.HttpContext.Current.Session["JiaoSe"] = "OA综合版角色";
                //System.Web.HttpContext.Current.Session["Department"] = "开发部";
                //System.Web.HttpContext.Current.Session["TrueName"] = "admin";
                //System.Web.HttpContext.Current.Session["ZhiWei"] = "部门主管";
                //System.Web.HttpContext.Current.Session["QuanXian"] = "|001||001a||001d||001e||||||||||002||002a||002s||002m||002e||||||||003||003e||||||||||||||004||004a||004r||004d||004e||||||||005||005a||005d||005e||||||||||006||006a||006s||006m||006e||||||||007||007d||007e||||||||||||008||008a||008r||008d||008e||||||||009||009a||009s||009d||009e||||||||010||010a||010m||010d||010e||||||||011||011a||011m||011d||011e||||||||012||012a||012m||012d||012e||||||||013||013a||013m||013d||013e||||||||R013||R013e||||||||||||||014||014a||014m||014d||014e||||||||015||015a||015m||015d||015e||||||||016||016e||||||||||||||017||017a||017m||017d||017e||||||||020||||||||||||||||021||021m||||||||||||||022||||||||||||||023||||||||||||||024||024a||024m||024d||024e||||||||025||025a||025m||025d||025e||||||||026||||||||||||||||074||074a||074d||074e||||||||||075||075e||||||||||||||076||076e||||||||||||||077||077d||077e||||||||||||078||078d||078e||||||||||||079||079a||079m||079d||079e||||||||080||080m||||||||||||||081||081a||081m||081d||081e||||||||082||082a||082m||082d||082e||||||||083||083a||083m||083d||083e||||||||084||084d||084e||||||||||||085||085d||085e||||||||||||057||057e||||||||||||||058||058a||058d||058e||||||||||F001||F001e||||||||||||||F002||F002e||||||||||||||F003||F003a||F003m||F003d||F003e||||||||F004||F004a||F004m||F004d||F004e||||||||027||027a||027m||027d||027e||||||||028||028e||||||||||||||A001||A001a||A001m||A001d||A001e||||||||A002||A002e||||||||||||||A003||||||||||||||||A004||A004a||A004m||A004d||A004e||||||||A005||A005a||A005m||A005d||A005e||||||||A006||A006a||A006m||A006d||A006e||||||||A007||A007a||A007m||A007d||A007e||||||||A008||A008a||A008m||A008d||A008e||||||||029||||||||||||||||030||030a||030d||030e||||||||||031||031a||031d||031e||||||||||032||032a||032d||032e||||||||||033||033a||033d||033e||||||||||034||034a||034d||034e||||||||||035||035p||035n||035e||||||||||036||036p||036n||036e||||||||||037||037p||037n||037e||||||||||038||038p||038n||038e||||||||||039||039p||039n||039e||||||||||040||040m||||||||||||||041||041e||||||||||||||042||042e||||||||||||||C001||C001a||C001m||C001d||C001e||||||||C002||C002a||C002m||C002d||C002e||||||||C003||C003a||C003m||C003d||C003e||||||||C004||C004a||C004m||C004d||C004e||||||||C005||C005a||C005m||C005d||C005e||||||||C006||C006a||C006m||C006d||C006e||||||||C007||C007a||C007m||C007d||C007e||||||||C008||C008a||C008m||C008d||C008e||||||||C018||C018a||C018m||C018d||C018e||||||||C009||C009d||C009e||C009m||||||||||C010||C010d||C010e||||||||||||C011||C011d||C011e||||||||||||C012||C012d||C012e||||||||||||C013||C013d||C013e||||||||||||C014||C014d||C014e||||||||||||C015||C015d||C015e||||||||||||C016||C016d||C016e||||||||||||C019||C019d||C019e||C019m||||||||||C017||C017e||||||||||||||C020||||||||||||||||C021||||||||||||||||X001||X001a||X001m||X001d||X001e||||||||X002||X002a||X002m||X002d||X002e||||||||X003||X003a||X003m||X003d||X003e||||||||X004||X004a||X004m||X004d||X004e||||||||X005||X005a||X005m||X005d||X005e||||||||X006||X006a||X006d||X006e||||||||||X007||X007a||X007d||X007e||||||||||X008||X008a||X008m||X008d||X008e||||||||X009||||||||||||||||A009||A009a||A009m||A009d||A009e||||||||A010||A010a||A010m||A010d||A010e||||||||A011||A011a||A011m||A011d||A011e||||||||A012||A012a||A012m||A012d||A012e||||||||A013||A013a||A013m||A013d||A013e||||||||A014||A014a||A014m||A014d||A014e||||||||A015||A015a||A015m||A015d||A015e||||||||A016||A016a||A016m||A016d||A016e||||||||A017||A017a||A017m||A017d||A017e||||||||A018||A018a||A018m||A018d||A018e||||||||A019||A019a||A019m||A019d||A019e||||||||A020||A020a||A020m||A020d||A020e||||||||A021||A021a||A021m||A021d||A021e||||||||A022||A022a||A022m||A022d||A022e||||||||A023||A023a||A023m||A023d||A023e||||||||A024||A024b||A024rb||A024d||A024e||||||||A025||A025a||A025m||A025d||A025e||||||||A026||A026a||A026m||A026d||A026e||||||||A027||A027a||A027d||A027e||||||||||A028||A028a||A028m||A028d||A028e||||||||A029||A029a||A029m||A029d||A029e||||||||A030||A030a||A030m||A030d||A030e||||||||018||018e||||||||||||||F005||F005a||F005m||F005d||F005e||||||||019||019ap||019anp||019d||019e||||||||F006||F006e||||||||||||||A031||A031a||A031m||A031d||A031e||||||||A032||A032ap||A032anp||A032d||A032e||||||||F007||F007a||F007m||F007d||F007e||||||||F008||F008ap||F008anp||F008d||F008e||||||||F009||F009a||F009m||F009d||F009e||||||||F010||F010ap||F010anp||F010d||F010e||||||||F011||F011a||F011m||F011d||F011e||||||||043||||||||||||||||044||||||||||||||||045||||||||||||||||046||046n||046a||046m||046d||046e||||||047||047n||047a||047m||047d||047e||||||048||048n||048a||048m||048d||048e||||||049||||||||||||||||050||050n||050a||050m||050d||050e||||||051||||||||||||||||052||052d||052r||052e||||||||||053||||||||||||||||054||054a||054m||054d||054e||||||||055||||||||||||||||056||056m||||||||||||||059||||||||||||||||060||||||||||||||||061||||||||||||||||062||||||||||||||||063||||||||||||||||064||||||||||||||||065||||||||||||||||066||||||||||||||||067||||||||||||||||068||||||||||||||069||069e||||||||070||070e||||||||071||071e||||||||086||086e||||||||072||072e||||||||||||073||||||||||||| ";
                //return System.Web.HttpContext.Current.Session[SessionKey].ToString();                
            }
        }
        //设置Session中的值
        public static void SetSessionValue(string SessionKey,string ValueStr)
        {
            System.Web.HttpContext.Current.Session[SessionKey] = ValueStr;
        }
        //根据投票的选项和的分数，生成界面的Table
        public static string GetVoteTable(string ContentStr, string ScoreStr,string IDStr,bool IFTouGuo)
        {
            string StartStr ="<table>";
            string EndStr = "</table>";
            string MidStr = string.Empty;
            string[] ContentList = ContentStr.Split('|');
            string[] ScoreList = ScoreStr.Split('|');

            //总票数
            double TotalPiao = 0;
            for (int j = 0; j < ScoreList.Length; j++)
            {
                double MaxTouPiao=double.Parse(ScoreList[j]);
                if (MaxTouPiao > TotalPiao)
                {
                    TotalPiao = MaxTouPiao;
                }
            }
            if (TotalPiao == 0)
            {
                TotalPiao = 1;
            }

            for (int i = 0; i < ContentList.Length; i++)
            {
                double PicWidth = (double.Parse(ScoreList[i]) / TotalPiao)*250;
                if (IFTouGuo == true)
                {
                    MidStr = MidStr + "<tr><td><img src=\"../images/ShiWuSmall.jpg\" /></td><td>" + (i + 1).ToString() + "：" + ContentList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td>得票：<img src=\"../images/vote_bg.gif\" height=10 width=" + PicWidth.ToString() + "  />&nbsp;&nbsp;" + ScoreList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td></td></tr>";
                }
                else
                {
                    MidStr = MidStr + "<tr><td><img src=\"../images/ShiWuSmall.jpg\" /></td><td>" + (i + 1).ToString() + "：" + ContentList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td>得票：<img src=\"../images/vote_bg.gif\" height=10 width=" + PicWidth.ToString() + "  />&nbsp;&nbsp;" + ScoreList[i] + "&nbsp;&nbsp;&nbsp;&nbsp;</td><td><input type='checkbox' value=" + i.ToString() + "/></td></tr>";
                }
            }
            return StartStr + MidStr + EndStr;   
        }
        //判断是否已经存在该项 列名称，表名称，去除的ID名称
        public static bool IFExists(string LieName,string TableName,int ExceptID,string TextStr)
        {
            bool ReturnIF = false;
            try
            {
                string JKL = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select count(*) from " + TableName + " where " + LieName + "='" + TextStr + "' and ID !=" + ExceptID.ToString());
                if (int.Parse(JKL) < 1)
                {
                    ReturnIF = true;
                }
            }
            catch
            {
                ReturnIF = true;
            }
            return ReturnIF;
        }

        public static string ProcessSqlStr(string Str)
        {
            string SqlStr = "exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare";
            string ReturnValue = Str;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue = "";
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = "";
            }
            if (Str.Length > 20)
            {
                ReturnValue = "";
            }
            return ReturnValue;
        }
    }
}
