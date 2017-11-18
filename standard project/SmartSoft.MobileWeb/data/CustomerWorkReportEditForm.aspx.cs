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
    public partial class CustomerWorkReportEditForm : MobilePageBase
    {
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
                hfHasLike.Value = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=2 AND bcTypeID=2 AND bcRelatedID=" + rowID + " and bcOperatorID=" + CurrentOperatorID);
                hfHasComment.Value = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=2 AND bcTypeID=1 AND bcRelatedID=" + rowID + " and bcOperatorID=" + CurrentOperatorID);
                hfCurrentOperatorID.Value = CurrentOperatorID;
                hfCurrentOperatorName.Value = CurrentOperatorName;
                bindData();
            }
        }

        private void bindData()
        {
            if (rowID.HasValue)
            {

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=2 AND bcTypeID=1 AND bcRelatedID=" + rowID);
                this.repComment.DataSource = dt;
                this.repComment.DataBind();

                hfwdID.Value = rowID.ToString();

                string sql = @"select A.*,B.opeName as wdNotifier,C.opeName as opeName from WorkDiary A left outer join Operators B on A.wdNotifierID =B.opeID left outer join Operators C on A.wdAddOperatorID=C.opeID where wdID=" + rowID;
                DataRow dr = DbHelperSQL.GetDataRow(sql);
                if (dr != null)
                {
                    string Type = "";
                    string t = dr["wdTypeID"] + "";
                    switch (t) 
                    {
                        case "1":
                            Type = "日报";
                            break;
                        case "2":
                            Type = "周报";
                            break;
                        case "3":
                            Type = "月报";
                            break;
                    }
                    lblopeName.InnerText = dr["opeName"] + "" + "的" + Type + "" + DateTime.Parse(dr["wdAddDate"] + "").ToString("yyyy-MM-dd");
                    lblwdAddDateDetails.InnerText = DateTime.Parse(dr["wdAddDate"] + "").ToString("yyyy-MM-dd HH:mm");
                    lblwdContent.InnerText = dr["wdContent"] + "";
                    lblwdNotifierID.InnerText = dr["wdNotifier"] + "";
                    lblwdTitle.InnerText = dr["wdTitle"] + "";
                    hfwdOperatorID.Value = dr["wdAddOperatorID"] + "";
                    string filePath = dr["wdFile"] + "";
                    if (filePath != "")
                    {
                        if (filePath.Contains("|"))
                        {
                            filePath = filePath.Substring(0, filePath.Length - 1);
                        }
                        string cfp = "";
                        string path = System.Configuration.ConfigurationManager.AppSettings["PCWebDomain"] + "UploadFile/CustomerFile/";
                        string[] fp = filePath.Split('|');
                        foreach (string cfpFilePath in fp)
                        {
                            if (cfpFilePath != "")
                            {
                                cfp += "<a target=\"_blank\" onclick=\"javascript:showPictures('" + filePath + "','" + path + cfpFilePath + "','" + path + "');\">";
                                cfp += "<img class=\"picView\" src=\"" + path + "small" + cfpFilePath + "\" /></a>";
                            }
                        }
                        lblwdFile.InnerHtml = cfp;
                    }

                    DataTable dt2 = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=2 AND bcTypeID=2 AND bcRelatedID=" + rowID);
                    string likePersonsHtml = "";
                    foreach (DataRow r in dt2.Rows)
                    {
                        likePersonsHtml += "<label class='cssLikePerson'>" + r["bcOperator"] + "</label>";
                    }
                    litLikePersons.Text = likePersonsHtml;

                }
            }
        }

        public string GetCommentCount()
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 2 and bcTypeID = 1 and bcRelatedID = " + rowID);
        }

        public string GetLikeCount()
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 2 and bcTypeID = 2 and bcRelatedID = " + rowID);
        }
    }
}