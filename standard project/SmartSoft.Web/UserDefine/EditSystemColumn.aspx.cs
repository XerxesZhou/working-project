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

using UDEF.Component;
using UDEF.Service;
using UDEF.Domain;
using UDEF.Domain.Enumerate;

namespace UDEF.Web.UserDefine
{
    public partial class EditSystemColumn : BasePage
    {
        private SystemSelectTypeService _systemselecttype;
        public SystemSelectTypeService systemselecttype
        {
            set { _systemselecttype = value; }
        }

        private SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        private SystemColumnService _systemcolumn;
        public SystemColumnService systemcolumn
        {
            set { _systemcolumn = value; }
        }

        private ColumnService _column;
        public ColumnService column
        {
            set { _column = value; }
        }

        public int TableID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
                dataBind();
            }
        }

        private void initPage()
        {
            //��������
            ListControlDataInit.Init(
                this.FieldDataType,
                DataType.SelectDataTypeAll(),
                "TypeText",
                "TypeID",
                false
            );

            //����������Դ����
            ListControlDataInit.Init(
                this.FieldSelectType,
                _systemselecttype.SelectDynamic("", "SelectTypeText ASC"),
                "SelectTypeText",
                "SelectTypeID",
                true
            );

            //���������
            ListControlDataInit.Init(
                this.ddlForeignTableID,
                _systemtable.SelectAll(),
                "TableText",
                "TableID",
                true
            );
        }
        /// <summary>
        /// ������
        /// </summary>
        private void dataBind()
        {
            if (StringHelper.IsValidDBInt(Request.QueryString["ColumnID"]))
            {
                int nColumnID = int.Parse(Request.QueryString["ColumnID"].ToString());
                SystemColumn de = _systemcolumn.Select(nColumnID);
                FormBindingHelper.BindObjectToControls(de, this);

                TableID = _column.GetTableID(nColumnID);
            }
            else
            {
                if (StringHelper.IsValidDBInt(Request.QueryString["TableID"]))
                {
                    this.FieldTableID.Text = Request.QueryString["TableID"].ToString();

                    int.TryParse(Request.QueryString["TableID"].ToString(), out TableID);
                }
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sHtml = string.Empty;
            
            try
            {
                if (StringHelper.IsValidDBInt(Request.QueryString["ColumnID"]))
                {
                    int nColumnID = int.Parse(Request.QueryString["ColumnID"].ToString());
                    SystemColumn de = _systemcolumn.Select(nColumnID);
                    FormBindingHelper.BindControlsToObject(de, this);

                    TableID = _column.GetTableID(nColumnID);

                    _systemcolumn.Update(de);
                    
                }
                else
                {
                    SystemColumn de = new SystemColumn();
                    FormBindingHelper.BindControlsToObject(de, this);

                    _systemcolumn.Add(de);

                    int.TryParse(Request.QueryString["TableID"].ToString(), out TableID);
                }

                sHtml += "alert('�޸ĳɹ���');";
                sHtml += "backToList(" + TableID + ");";
            }
            catch
            {
                sHtml += "alert('�޸�ʧ�ܣ�');";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Replace", sHtml, true);
        }
    }
}
