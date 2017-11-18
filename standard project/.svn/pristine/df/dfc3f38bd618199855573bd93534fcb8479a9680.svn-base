using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SmartSoft.Presentation;
using SmartSoft.Domain.Common;
using System.Collections.Generic;
using SmartSoft.Component;

public partial class MyDesk : PageBase
{
    private DataController _datacontroller;
    public DataController datacontroller
    {
        set { _datacontroller = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hfCurrentOperatorID.Value = this.Operator.opeID.ToString();
        if (!Page.IsPostBack)
        {
            IList<OperatorDesktop> listDesktop = _datacontroller.SelectDynamic<OperatorDesktop>(" AND odtModelName not like '%快捷方式%' AND odtOperatorID = " + this.Operator.opeID, " odtOrderBy asc");

            if (listDesktop.Count > 0)
            {
                string StartStr = "<table width=\"100%\"><tr><td valign=\"top\" width=\"50%\">";                    
                string EndStr = "</td></tr></table>";
                string MidStr ="";
                for (int i = 0; i < listDesktop.Count; i++)
                {
                    //对当前次数求余，为1时为一行结束，否则0代表还有一个td
                    int TdInt = i % 2;                    
                    //按照设置显示桌面
                    string TTStr = "<div class=\"bigtype\"  style=\"background-color:#0174CF;\"><table width=\"98%\"  cellpadding=\"0\" cellspacing=\"0\" ><tr ><td height=\"30px\" style=\"background-color:#0174CF;border-bottom:1px solid #0174CF;\" class=\"DashboardHeader\"><img src=\"../images/img_158.png\" style=\" float:left\" width=\"15\" height=\"16\" align=\"absmiddle\"><a class=\"heiBold\" style=\"color:#FFF\" href=\"javascript:void(0);\" onclick=\"SwitchMenu('Model" + i.ToString() + "')\">" + listDesktop[i].odtModelName + "</a></td><td class=\"DashboardHeader\" style=\"background-color:#0174CF;border-bottom:1px solid #0174CF;\"><div align=\"right\"><a href=\"MyDesk.aspx?ModelName=" + listDesktop[i].odtModelName + "\"><img onclick=\"_delmodel('" + listDesktop[i].odtModelName + "')\" src=\"../images/img_newclose.png\"  width=\"15\" style=\"float:right\" title=\"关闭此模块\" height=\"15\" border=\"0\" align=\"absmiddle\"></a>&nbsp; <img onclick=\"_editmodel('" + listDesktop[i].odtModelName + "')\" class=\"HerCss\" src=\"../images/img_update(1).png\" width=\"15\" height=\"15\" border=\"0\" align=\"absmiddle\">&nbsp; </div></td></tr></table></div><div style=\"background-color:#FFF;\" class=\"Model" + i.ToString() + "\">" + GetDeskLink(listDesktop[i].odtModelName, listDesktop[i].odtLookNum.Value) + "</div>";
                    if (i != 0)
                    {
                        if (TdInt != 0)
                        {
                            MidStr = MidStr + TTStr + "</td></tr><tr><td valign=\"top\" width=\"50%\">";
                        }
                        else
                        {
                            MidStr = MidStr + TTStr + "</td><td valign=\"top\" width=\"50%\">";
                        }
                    }
                    else
                    {
                        MidStr = MidStr + TTStr + "</td><td valign=\"top\" width=\"50%\">";
                    }
                }
                 this.Label1.Text = StartStr + MidStr + EndStr;
            }

            listDesktop = _datacontroller.SelectDynamic<OperatorDesktop>(" AND odtModelName like '%快捷方式%' AND odtOperatorID = " + this.Operator.opeID, " odtOrderBy asc");
            if (listDesktop.Count > 0)
            {
                string html = "<ul>";
                for (int i = 0; i < listDesktop.Count; i++)
                {
                    if (listDesktop[i].odtModelName.Contains("客户管理"))
                    {
                        html += @"<li>
                                
                                    <a href='../Data/CustomerList.aspx' title='客户管理'><img src='../@images/img/kehuguanlis.png' title='客户管理' />客户管理</a> </li>";
                    }
                    else if (listDesktop[i].odtModelName.Contains("商机管理"))
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["CloseOpptunity"] + "" != "1")
                        {
                            html += @"<li>
                                
                                    <a href='../Data/CustomerOpptunityList.aspx' title='商机管理'><img src='../@images/img/shoukuang.png' title='商机管理' />商机管理</a> </li>";
                        }
                    }
                    else if (listDesktop[i].odtModelName.Contains("跟进管理"))
                    {
                        html += @"<li>
                                
                                    <a href='../Data/CustomerFollowHistoryList.aspx' title='跟进管理'><img src='../@images/img/yewugenjin.png' title='跟进管理' />跟进管理</a> </li>";
                    }
                    else if (listDesktop[i].odtModelName.Contains("公共池"))
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["CloseCustomerPool"] + "" != "1")
                        {
                            html += @"<li>
                                
                                    <a href='../Data/CustomerPoolList.aspx' title='公共池'><img src='../@images/img/gonggongci.png' title='公共池' />公共池</a> </li>";
                        }
                    }
                    else if (listDesktop[i].odtModelName.Contains("计划管理"))
                    {
                        html += @"<li>
                               
                                    <a href='../Data/CustomerFollowPlanList.aspx' title='计划管理'> <img src='../@images/img/PlanManage.png' title='计划管理' />计划管理</a> </li>";
                    }
                    if (listDesktop[i].odtModelName.Contains("业务管理"))
                    {
                        html += @"<li>
                                
                                    <a href='../Data/CustomerBusinessList.aspx' title='业务管理'><img src='../@images/img/SellOrder.png' title='业务管理' />业务管理</a> </li>";
                    }
                    if (listDesktop[i].odtModelName.Contains("收款管理"))
                    {
                        html += @"<li>
                                
                                    <a href='../Data/CustomerReceiptList.aspx' title='收款管理'><img src='../@images/img/shoukuang.png' title='收款管理' />收款管理</a> </li>";
                    }
                    if (listDesktop[i].odtModelName.Contains("客户统计"))
                    {
                        html += @"<li>
                                
                                    <a href='../Data/CustomerReport.aspx' title='客户统计'><img src='../@images/img/kehutongji54.png' title='客户统计' />客户统计</a> </li>";
                    }
                    
                    if (listDesktop[i].odtModelName.Contains("业务统计"))
                    {
                        html += @"<li>
                               
                                    <a href='../Data/CustomerBusinessReport.aspx' title='业务统计'> <img src='../@images/img/yewutongji2.png' title='业务统计' />业务统计</a> </li>";
                    }
                    if (listDesktop[i].odtModelName.Contains("收款统计"))
                    {
                        html += @"<li>
                               
                                    <a href='../Data/CustomerReceiptReport.aspx' title='收款统计'><img src='../@images/img/yejitongji.png' title='收款统计' />收款统计</a> </li>";
                    }
                }
                html += "</ul>";
                litLinks.Text = html;
            }
        }
    }

    private string GetDeskLink(string ModelName, int ModelNum)
    {
        string StartStr = "<table width=\"98%\">";
        string EndStr = "</table>";
        string MidStr = "";
        string OKStr = "";

        if (ModelName == "最近新增客户")
        {
            DataSet MyDataSet = DbHelperSQL.GetDataSet("select top " + ModelNum.ToString() + " cusID, cusName, cusAddDate, opeName AS TrueName from Customer A LEFT OUTER JOIN Operators B ON A.cusOperatorID = B.opeID where 1=1 " + _datacontroller.GetCondition("cusID", this.Operator.opeID) + " order by cusAddDate desc");
            //记录集行数>0
            if (MyDataSet.Tables[0].Rows.Count > 0)
            {
                string cusName = "";
                for (int i = 0; i < MyDataSet.Tables[0].Rows.Count; i++)
                {
                    cusName = MyDataSet.Tables[0].Rows[i]["cusName"].ToString();
                    if (cusName.Length > 30)
                    {
                        cusName = cusName.Substring(0, 30) + "...";
                    }
                    MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left\" ><a href=\"javascript:OpenEditForm('../Data/CustomerEditForm.aspx?Action=View&ID=" + MyDataSet.Tables[0].Rows[i]["cusID"].ToString() + "',1000,700)\">" + cusName + "</td><td>" + getDateString(MyDataSet.Tables[0].Rows[i]["cusAddDate"].ToString()) + "</td><td>" + MyDataSet.Tables[0].Rows[i]["TrueName"] + "</td></tr>";
                }
            }
        }
        else if (ModelName == "最近已跟进客户")
        {
            DataSet MyDataSet = DbHelperSQL.GetDataSet("select top " + ModelNum.ToString() + " cusID, cusName, cfhAddDate, opeName AS TrueName from CustomerFollowHistory A INNER JOIN Customer C ON A.cfhCustomerID = C.cusID LEFT OUTER JOIN Operators B ON A.cfhOperatorID = B.opeID where 1=1 " + _datacontroller.GetCondition("cfhCustomerID", this.Operator.opeID) + " order by cfhAddDate desc");
            //记录集行数>0
            if (MyDataSet.Tables[0].Rows.Count > 0)
            {
                string cusName = "";
                for (int i = 0; i < MyDataSet.Tables[0].Rows.Count; i++)
                {
                    cusName = MyDataSet.Tables[0].Rows[i]["cusName"].ToString();
                    if (cusName.Length > 30)
                    {
                        cusName = cusName.Substring(0, 30) + "...";
                    }
                    MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left\" ><a href=\"javascript:OpenEditForm('../Data/CustomerEditForm.aspx?Action=View&ID=" + MyDataSet.Tables[0].Rows[i]["cusID"].ToString() + "',1000,700)\">" + cusName + "</td><td>" + getDateString(MyDataSet.Tables[0].Rows[i]["cfhAddDate"].ToString()) + "</td><td>" + MyDataSet.Tables[0].Rows[i]["TrueName"] + "</td></tr>";
                }
            }
        }
        else if (ModelName == "个人业绩")
        {
            string monthlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and Month(cbDate)={1} and cbOperatorID={2}", DateTime.Now.Year, DateTime.Now.Month, this.Operator.opeID));
            string monthlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and Month(crDate)={1} and crOperatorID={2}", DateTime.Now.Year, DateTime.Now.Month, this.Operator.opeID));
            string yearlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and cbOperatorID={1}", DateTime.Now.Year, this.Operator.opeID));
            string yearlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and crOperatorID={1}", DateTime.Now.Year, this.Operator.opeID));
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月销售总额：</td><td>" + double.Parse(monthlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月收款总额：</td><td>" + double.Parse(monthlyReceiptAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left ;margin-top: -2px\" >本年销售总额：</td><td>" + double.Parse(yearlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本年收款总额：</td><td>" + double.Parse(yearlyReceiptAmount).ToString("N0") + "</td></tr>";
        }
        else if (ModelName == "部门业绩" && _datacontroller.HasViewDepartmentCustomer(this.Operator.opeID.ToString()))
        {
            string monthlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and Month(cbDate)={1} and cbOperatorID in (select opeID from Operators where opeDepartmentID = {2})", DateTime.Now.Year, DateTime.Now.Month, (this.Operator.opeDepartmentID.HasValue ? this.Operator.opeDepartmentID.Value : 0)));
            string monthlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and Month(crDate)={1} and crOperatorID in (select opeID from Operators where opeDepartmentID = {2})", DateTime.Now.Year, DateTime.Now.Month, (this.Operator.opeDepartmentID.HasValue ? this.Operator.opeDepartmentID.Value : 0)));
            string yearlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and cbOperatorID in (select opeID from Operators where opeDepartmentID = {1})", DateTime.Now.Year, (this.Operator.opeDepartmentID.HasValue ? this.Operator.opeDepartmentID.Value : 0)));
            string yearlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and crOperatorID in (select opeID from Operators where opeDepartmentID = {1})", DateTime.Now.Year, (this.Operator.opeDepartmentID.HasValue ? this.Operator.opeDepartmentID.Value : 0)));
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月销售总额：</td><td>" + double.Parse(monthlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月收款总额：</td><td>" + double.Parse(monthlyReceiptAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本年销售总额：</td><td>" + double.Parse(yearlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本年收款总额：</td><td>" + double.Parse(yearlyReceiptAmount).ToString("N0") + "</td></tr>";
        }
        else if (ModelName == "公司业绩" && _datacontroller.HasViewAllCustomer(this.Operator.opeID.ToString()))
        {
            string monthlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and Month(cbDate)={1}", DateTime.Now.Year, DateTime.Now.Month));
            string monthlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and Month(crDate)={1}", DateTime.Now.Year, DateTime.Now.Month));
            string yearlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0}", DateTime.Now.Year));
            string yearlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0}", DateTime.Now.Year));
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月销售总额：</td><td>" + double.Parse(monthlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月收款总额：</td><td>" + double.Parse(monthlyReceiptAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本年销售总额：</td><td>" + double.Parse(yearlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本年收款总额：</td><td>" + double.Parse(yearlyReceiptAmount).ToString("N0") + "</td></tr>";
        }
        else if (ModelName == "销售达成率")
        {
            decimal orderAmount = 0.0m;
            if (this.Operator.opeOrderAmount.HasValue)
            {
                orderAmount = this.Operator.opeOrderAmount.Value;
            }
            string monthlySellOrderAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(cbTotalAmount),0.0) from CustomerBusiness where Year(cbDate)={0} and Month(cbDate)={1} and cbOperatorID = {2}", DateTime.Now.Year, DateTime.Now.Month, this.Operator.opeID));
            this.hfOrderFinishRate.Value = "0";
            
            if (orderAmount > 0.0m)
            {
                this.hfOrderFinishRate.Value = (decimal.Parse(monthlySellOrderAmount) * 100 / orderAmount).ToString("N0");
            }

            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月销售总额：</td><td>" + double.Parse(monthlySellOrderAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月目标总额：</td><td>" + orderAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td colspan='2' style='text-align:center;'><div id='divOrderFinishRate'></div></td></tr>";
        }
        else if (ModelName == "回款达成率")
        {
            decimal receiptAmount = 0.0m;
            if (this.Operator.opeReceiptAmount.HasValue)
            {
                receiptAmount = this.Operator.opeReceiptAmount.Value;
            }
            string monthlyReceiptAmount = DbHelperSQL.GetSHSL(string.Format("select ISNULL(sum(crAmount),0.0) from CustomerReceipt where Year(crDate)={0} and Month(crDate)={1} and crOperatorID={2}", DateTime.Now.Year, DateTime.Now.Month, this.Operator.opeID));
            this.hfReceiptFinishRate.Value = "0";

            if (receiptAmount > 0.0m)
            {
                this.hfReceiptFinishRate.Value = (decimal.Parse(monthlyReceiptAmount) * 100 / receiptAmount).ToString("N0");
            }

            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月收款总额：</td><td>" + double.Parse(monthlyReceiptAmount).ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >本月目标总额：</td><td>" + receiptAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td colspan='2' style='text-align:center;'><div id='divReceiptFinishRate'></div></td></tr>";
        }
        else if (ModelName == "我的销售漏斗")
        {
            decimal totalAmount = 2100000;
            decimal predictAmount = 570000;
            
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >漏斗总额：</td><td>" + totalAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >预测总额：</td><td>" + predictAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td colspan='2'><div id='divMyFunnel'></div></td></tr>";
        }
        else if (ModelName == "公司销售漏斗")
        {
            decimal totalAmount = 2100000;
            decimal predictAmount = 570000;

            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >漏斗总额：</td><td>" + totalAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td><img src=\"../images/right.png\" width=\"20px\" height=\" 20px\" style=\" float:left;margin-top: -2px\" >预测总额：</td><td>" + predictAmount.ToString("N0") + "</td></tr>";
            MidStr = MidStr + "<tr><td colspan='2'><div id='divAllFunnel'></div></td></tr>";
        }
        OKStr = StartStr + MidStr + EndStr;
        return OKStr;
    }

    private string getDateString(string datestr)
    {
        DateTime d;
        if (DateTime.TryParse(datestr, out d))
        {
            return d.ToString("yyyy-MM-dd HH:mm");
        }
        return datestr;
    }
}
