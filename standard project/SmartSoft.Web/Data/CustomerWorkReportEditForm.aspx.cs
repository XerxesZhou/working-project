namespace SmartSoft.Web.Data
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


    using UDEF.Service;
    using UDEF.Domain;
    using UDEF.Domain.Enumerate;
    using UDEF.Component;

    using SmartSoft.Service.Interface.Common;
    using SmartSoft.Domain.Data;
    using SmartSoft.Component;
    using SmartSoft.Domain.Enumerate;
    using SmartSoft.Presentation;
    using SmartSoft.Service;

    public partial class CustomerWorkReportEditForm : PageBaseEdit
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
                    cfhLikeAndComment.InnerHtml = GetLikeAndCommentCountHtml(rowID.ToString(), dr["wdAddOperatorID"] + "");
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


                    DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=2 AND bcTypeID=2 AND bcRelatedID=" + rowID);
                    string likePersonsHtml = "";
                    foreach (DataRow r in dt.Rows)
                    {
                        likePersonsHtml += "<div class='cssLikePerson'>" + r["bcOperator"] + "</div>";
                    }
                    if (likePersonsHtml != "")
                    {
                        likePersonsHtml += "<div class='cssTxt'>觉得很赞哦</div>";
                    }
                    litLikePersons.InnerHtml = likePersonsHtml;

                    DataTable dt2 = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=2 AND bcTypeID=1 AND bcRelatedID=" + rowID);
                    repBillComment.DataSource = dt2;
                    repBillComment.DataBind();

                    
                }
            }
        }

        public string GetLikeAndCommentCountHtml(string cfhID, string cfhOperatorID)
        {
            string likeCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=2 AND bcTypeID=2 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + this.Operator.opeID);
            string html = "";
            string contentCount = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=2 AND bcTypeID=1 AND bcRelatedID=" + cfhID + " and bcOperatorID=" + this.Operator.opeID);
            string cssLikeName = "cssNotLike";
            string cssContentName = "cssNotLike";
            string imageLike = "../@images/like.png";
            string imageContent = "../@images/content.png";

            if (int.Parse(likeCount) > 0)
            {
                cssLikeName = "cssLike";
                imageLike = "../@images/likeClick.png";
            }
            if (int.Parse(contentCount) > 0)
            {
                cssContentName = "cssLike";
                imageContent = "../@images/HasContent.png";
            }
            html = "<div style='width:50%; float:left; text-align:center' id=\"divComment" + cfhID + "\" class='" + cssContentName + "' onclick=\"javascript:GotoFollowHistory(" + cfhID + ",'Clue');\">";
            html += "<img src=\"" + imageContent + "\" style=\"width:18px;\" id=\"imgContent" + cfhID + "\"/>";
            html += " <label id='lblCommentCount'>评论(" + GetCommentCount(cfhID) + ")</label>";
            html += "</div><div onclick=\"javascript:handleClickLike(" + cfhID + "," + cfhOperatorID + ");\" id=\"divLike" + cfhID + "\" class='" + cssLikeName + "' style=\" width:50%; float:left; text-align:center\">";
            html += "<img src=\"" + imageLike + "\" style=\" width:18px;\" id='imgLike" + cfhID + "'/>";
            html += "<label id=\"lblLikeCount" + cfhID + "\">点赞(" + GetLikeCount(cfhID) + ")</label>";
            html += "</div>";
            return html;
        }

        public string GetCommentCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID =2 and bcTypeID = 1 and bcRelatedID = " + cfhID);
        }

        public string GetLikeCount(string cfhID)
        {
            return DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID = 2 and bcTypeID = 2 and bcRelatedID = " + cfhID);
        }
    }
}
