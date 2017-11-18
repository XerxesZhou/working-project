/***************************************************************************
 * 
 *       功能：     客户跟进历史项目编辑
 *       作者：     朱少军
 *       日期：     2009-08-07
 * 
 *       修改日期： 
 *       修改人：   
 *       修改内容： 
 * 
 * *************************************************************************/
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

    public partial class CustomerFollowHistoryEditForm : PageBaseEdit
    {
        #region Fields
        private int rowID
        {
            get
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["ID"]);
                }
                return 0;
            }
        }

        private FormAction action
        {
            get
            {
                string sAction = string.Empty;
                if (Request.QueryString["Action"] != null && Request.QueryString["Action"] != string.Empty)
                {
                    sAction = Request.QueryString["Action"].ToString();
                }
                return FormActionClass.GetFormAction(sAction);
            }
        }

        public int? MessageID
        {
            get
            {
                if (Request.QueryString["MessageID"] != null && Request.QueryString["MessageID"] != string.Empty)
                {
                    return int.Parse(Request.QueryString["MessageID"]);
                }
                else
                {
                    return null;
                }
            }

        }
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }
        #endregion

        #region Event

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.MessageID.HasValue)
                {
                    _datacontroller.SetMessageIsRead(this.MessageID.Value, this.Operator.opeID);
                }
                base.Page_Load(sender, e);
                if (!IsPostBack) 
                {
                    hfCurrentOperatorID.Value = this.Operator.opeID.ToString();
                    hfCurrentOperatorName.Value = this.Operator.opeName;
                    BindSpeData();
                }
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        public void BindSpeData() 
        {
            if (rowID != 0) 
            {
                DataRow dr = DbHelperSQL.GetDataRow(@"select *,B.opeName as cfhOperatorName,ISNULL(C.Name,'系统跟进') as cfhTypeName from customerFollowHistory A 
left outer join Operators B on A.cfhOperatorID =B.opeID 
left outer join CustomerClue B4 on A.cfhRelatedID =B4.ccID and cfhSource='Clue'
left outer join Customer B1 on A.cfhRelatedID =B1.cusID and cfhSource='Customer'
left outer join CustomerOpptunity B2 on A.cfhRelatedID =B2.coID and cfhSource='Opptunity'
left outer join CustomerBusiness B3 on A.cfhRelatedID =B3.cbID and cfhSource='Business' 
left outer join defCommon C on A.cfhTypeID =C.ID where cfhID=" + rowID);
                cfhAddDate.InnerText = dr["cfhAddDate"] + "";
                cfhAddress.InnerText = dr["cfhAddress"] + "";
                cfhContent.InnerText = dr["cfhContent"] + "";
                cfhFilePath.InnerHtml = GetEachFile(dr["cfhFilePath"] + "");
                hfcfhOperatorID.Value = dr["cfhOperatorID"] + "";
                cfhOperatorName.InnerText = dr["cfhOperatorName"] + "";
                cfhTypeName.InnerText = dr["cfhTypeName"] + "";
                cfhLikeAndComment.InnerHtml = GetLikeAndCommentCountHtml(rowID.ToString(), dr["cfhOperatorID"] + "");
                hfCfhID.Value = rowID.ToString();

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

                DataTable dt = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=2 AND bcRelatedID=" + rowID);
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

                DataTable dt2 = DbHelperSQL.GetDataTable("select A.*,B.opeName as bcOperator from BillComment A left outer join Operators B on A.bcOperatorID=B.opeID where bcSourceID=1 AND bcTypeID=1 AND bcRelatedID=" + rowID);
                repBillComment.DataSource = dt2;
                repBillComment.DataBind();
            }
        }

        public string GetEachFile(string filePath)
        {
            return CommonFunction.GetEachFileForWeb(filePath);
        }

        #endregion

    }
}
