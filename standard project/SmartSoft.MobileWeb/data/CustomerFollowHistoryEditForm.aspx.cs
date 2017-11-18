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
    public partial class CustomerFollowHistoryEditForm : MobilePageBase
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
                this.hfCurrentOperatorID.Value = CurrentOperatorID;
                hfCurrentOperatorName.Value = CurrentOperatorName;
                hfHasLike.Value = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + rowID + " and bcOperatorID=" + CurrentOperatorID);
                hfHasComment.Value = DbHelperSQL.GetSHSLInt("select count(*) from BillComment where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + rowID + " and bcOperatorID=" + CurrentOperatorID);
                bindData();
            }
        }

        private void bindData()
        {
            if (rowID.HasValue)
            {

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + rowID);
                this.repComment.DataSource = dt;
                this.repComment.DataBind();

                DataRow dr = DbHelperSQL.GetDataRow(@"select 
*,B.opeName as cfhOperator,
ISNULL(C.Name,'系统跟进') as cfhType 
from CustomerFollowHistory A 
left outer join Operators B on A.cfhOperatorID=B.opeID 
left outer join defCommon C on A.cfhTypeID=C.ID 
left outer join CustomerClue B4 on A.cfhRelatedID =B4.ccID and cfhSource='Clue'
left outer join Customer B1 on A.cfhRelatedID =B1.cusID and cfhSource='Customer'
left outer join CustomerOpptunity B2 on A.cfhRelatedID =B2.coID and cfhSource='Opptunity'
left outer join CustomerBusiness B3 on A.cfhRelatedID =B3.cbID and cfhSource='Business' where cfhID=" + rowID);
                lblcfhContent.InnerText = dr["cfhContent"] + "";
                lblcfhOperator.InnerText = dr["cfhOperator"] + "";
                hfcfhOperatorID.Value = dr["cfhOperatorID"] + "";
                lblcfhAddDate.InnerText = DateTime.Parse(dr["cfhAddDate"] + "").ToString("yyyy-MM-dd HH:mm");
                GetFile.Text = GetEachFile(dr["cfhFilePath"] + "", dr["cfhID"] + "");
                lblcfhAddress.InnerText = dr["cfhAddress"] + "";
                lblcfhType.InnerText = dr["cfhType"] + "";
                lblcfhDate.InnerText = DateTime.Parse(dr["cfhDate"] + "").ToString("MM-dd HH:mm");
                //权限只有本人能删除 1为本人，可以删除；0不是本人，不能删除
                hfPermissionCode.Value = CurrentOperatorID == dr["cfhOperatorID"] + "" ? "1" : "0";
                hfcfhID.Value = rowID + "";
                hfFromSource.Value = Request.QueryString["FromSource"] + "";
                hfRelatedID.Value = dr["cfhRelatedID"] + "";
                string cfhSource = dr["cfhSource"] + "";
                if (cfhSource == "Clue")
                {
                    lblcfhFromName.InnerHtml = "关联线索：<a data-ajax='false' href='CustomerClueEditForm.aspx?ID=" + dr["cfhRelatedID"] + "'>" + dr["ccName"] + "</a>";
                }
                else if (cfhSource == "Customer")
                {
                    lblcfhFromName.InnerHtml = "关联客户：<a data-ajax='false' href='CustomerEditForm.aspx?ID=" + dr["cfhRelatedID"] + "'>" + dr["cusName"] + "</a>";
                }
                else if (cfhSource == "Opptunity")
                {
                    lblcfhFromName.InnerHtml = "关联商机：<a data-ajax='false' href='CustomerOpptunityEditForm.aspx?ID=" + dr["cfhRelatedID"] + "'>" + dr["coName"] + "</a>";
                }
                else if (cfhSource == "Business")
                {
                    lblcfhFromName.InnerHtml = "关联合同：<a data-ajax='false' href='CustomerBusinessEditForm.aspx?ID=" + dr["cfhRelatedID"] + "'>" + dr["cbName"] + "</a>";
                }

                DataTable dt2 = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + rowID);
                string likePersonsHtml = "";
                foreach (DataRow r in dt2.Rows)
                {
                    likePersonsHtml += "<label class='cssLikePerson'>" + r["bcOperator"] + "</label>";
                }
                litLikePersons.Text = likePersonsHtml;
            }
        }

        public string GetEachFile(string filePath, string cfhID)
        {
            return CommonFunction.GetEachFile(filePath);
        }

        public string GetCommentCount()
        {
            return CommonFunction.GetCommentCount(rowID.Value.ToString());
        }

        public string GetLikeCount()
        {
            return CommonFunction.GetLikeCount(rowID.Value.ToString());
        }
    }
}