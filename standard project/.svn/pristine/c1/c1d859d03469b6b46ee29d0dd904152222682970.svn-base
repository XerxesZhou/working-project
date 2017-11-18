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
    public partial class KnowledgeEditForm : MobilePageBase
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
                bindData();
            }
        }

        private void bindData()
        {
            if (rowID.HasValue)
            {
                hfwdID.Value = rowID.ToString();

                string sql = @"select A.*,B.Name as knowTypeName,C.opeName as opeName from Knowledge A left outer join defCommon B on A.knowType=B.ID left outer join Operators C on A.knowOperator=C.opeID where knowID=" + rowID;
                DataRow dr = DbHelperSQL.GetDataRow(sql);
                if (dr != null)
                {
                    lblknowType.InnerText = dr["knowTypeName"] + "";
                    lblopeName.InnerText = "知识录入人：" + dr["opeName"] + "";
                    lblknowTheme.InnerText = dr["knowTheme"] + "";
                    lblknowContent.Text = dr["knowContent"] + "";
                    string filepath = dr["knowFilePath"] + "";
                    if (filepath != "")
                    {
                        lblknowFilePath.HRef = "http://112.74.86.224:8888/UploadFile/CustomerFile/" + filepath + "";
                        lblURL.InnerText = filepath;
                    }
                    else 
                    {
                        lblURL.InnerText = "没有附件";
                    }
                    lblknowOperateDate.InnerText = DateTime.Parse(dr["knowOperateDate"] + "").ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }
    }
}