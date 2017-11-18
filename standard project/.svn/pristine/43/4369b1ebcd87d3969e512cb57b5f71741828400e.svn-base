using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartSoft.Domain.Enumerate;
using SmartSoft.Component;

namespace SmartSoft.MobileWeb.data
{
    public partial class CustomerFollowPlanEditForm : MobilePageBase
    {
        public int? MessageID
        {
            get
            {
                if (Request.QueryString["MessageID"] + "" != "")
                {
                    return int.Parse(Request.QueryString["MessageID"] + "");
                }
                return null;
            }
        }

        public int? rowID
        {
            get
            {
                if (Request.QueryString["ID"] + "" != "")
                {
                    return int.Parse(Request.QueryString["ID"] + "");
                }
                return null;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.Page_Load(sender, e);
                if (MessageID.HasValue)
                {
                    hfMessageID.Value = MessageID.ToString();
                    SetMessageReaded(int.Parse(CurrentOperatorID), MessageID.Value);
                    divSetMessage.Visible = false;
                    divWriteFollow.Attributes.Add("style", "display:block;float: left; width: 100%; text-align: center;");
                }
                else
                {
                    string mID = DbHelperSQL.GetSHSL("select MessageID from sysMessage where URL like '%CustomerFollowPlanEditForm.aspx?ID=" + rowID + "&MessageID%'");
                    DataTable dt = DbHelperSQL.GetDataTable("select * from sysMessage_Readed where MessageID='" + mID + "' and OperatorID='" + CurrentOperatorID + "'");
                    if (dt.Rows.Count > 0)
                    {
                        divSetMessage.Attributes.Add("style", "display:none;float:left; width: 100%; text-align: center;");
                        divWriteFollow.Attributes.Add("style", "display:none;float:left; width: 100%; text-align: center;");
                    }
                    else
                    {
                        divSetMessage.Attributes.Add("style", "display:block;float: left; width: 100%; text-align: center;");
                        divWriteFollow.Visible = false;
                    }
                    hfMessageID.Value = mID;
                }
                if (CurrentOperatorID != "") 
                {
                    hfCurrentOperatorID.Value = CurrentOperatorID;
                }
                bindData();
            }
        }

        private void bindData()
        {
            if (rowID.HasValue)
            {
                DataRow dr = DbHelperSQL.GetDataRow(@"select * from CustomerFollowPlan A 
left outer join CustomerClue B on A.cfpRelatedID =B.ccID and cfpSource='Clue'
left outer join Customer B1 on A.cfpRelatedID =B1.cusID and cfpSource='Customer'
left outer join CustomerOpptunity B2 on A.cfpRelatedID =B2.coID and cfpSource='Opptunity'
left outer join CustomerBusiness B3 on A.cfpRelatedID =B3.cbID and cfpSource='Business' where cfpID=" + rowID);
                lblcfpDate.InnerText = DateTime.Parse(dr["cfpDate"] + "").ToString("yyyy-MM-dd HH:mm");
                lblcfpContent.InnerText = dr["cfpContent"] + "";
                lblcfpFilePath.Text = GetEachFile(dr["cfpFilePath"] + "", dr["cfpID"] + "");
                string cfpSource = dr["cfpSource"] + "";
                if (cfpSource == "Clue")
                {
                    lblcfpFromName.InnerHtml = "关联线索：<a data-ajax='false' href='CustomerClueEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["ccName"] + "</a>";
                }
                else if (cfpSource == "Customer")
                {
                    lblcfpFromName.InnerHtml = "关联客户：<a data-ajax='false' href='CustomerEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["cusName"] + "</a>";
                }
                else if (cfpSource == "Opptunity")
                {
                    lblcfpFromName.InnerHtml = "关联商机：<a data-ajax='false' href='CustomerOpptunityEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["coName"] + "</a>";
                }
                else if (cfpSource == "Business")
                {
                    lblcfpFromName.InnerHtml = "关联合同：<a data-ajax='false' href='CustomerBusinessEditForm.aspx?ID=" + dr["cfpRelatedID"] + "'>" + dr["cbName"] + "</a>";
                }
                hfcfpID.Value = rowID.ToString();
                hfcfpRelatedID.Value = dr["cfpRelatedID"] + "";
                hfcfpSource.Value = dr["cfpSource"] + "";
            }
        }

        public string GetEachFile(string filePath, string ID)
        {
            return CommonFunction.GetEachFile(filePath);
        }

        public string GetCommentCount()
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 1 and bcRelatedID = " + rowID);
        }

        public string GetLikeCount()
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 1 and bcTypeID = 2 and bcRelatedID = " + rowID);
        }
    }
}