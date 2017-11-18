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
    public partial class EditCustomField_First : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

        private CustomColumnService _customcolumn;
        public CustomColumnService customcolumn
        {
            set { _customcolumn = value; }
        }

        private ColumnService _column;
        public ColumnService column
        {
            set { _column = value; }
        }

        private SelectTypeService _selecttype;
        public SelectTypeService selecttype
        {
            set { _selecttype = value; }
        }

        //系统数据表
        private SystemTableService _systemtable;
        public SystemTableService systemtable
        {
            set { _systemtable = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxService));

            if (!IsPostBack)
            {
                initPage();
                FieldDataType_SelectedIndexChanged(sender, e);
            }
        }

        private void initPage()
        {

            //数据类型
            ListControlDataInit.Init(
                this.FieldDataType,
                DataType.SelectDataTypeAll(),
                "TypeText",
                "TypeID",
                false
            );

            //数据类型
            ListControlDataInit.Init(
                this.FieldSelectType,
                _selecttype.SelectSelectTypeDynamic("", "SelectTypeText ASC"),
                "SelectTypeText",
                "SelectTypeID",
                true
            );
            
            if (Request.QueryString["ColumnID"] != null)
            {
                dataBind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            if (Request.QueryString["ColumnID"] != null)
            {
                int nColumnID = int.Parse(Request.QueryString["ColumnID"].ToString());
                CustomColumn de = _customcolumn.Select(nColumnID);
                FormBindingHelper.BindObjectToControls(de, this);

                //this.FieldIsFormula.Checked = _column.IsFormula(nColumnID);
                
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //参数表
            Hashtable ht = new Hashtable();
            ht.Add("DefaultValue", FieldDefaultValue.Text);
            string editMode = StringHelper.IsValidDBInt(Request.QueryString["ColumnID"]) ? "UPDATE" : "INSERT";
            ht.Add("EditType", editMode);
            string columnID = string.Empty;
            if (editMode == "UPDATE")
            {
                columnID = Request.QueryString["ColumnID"].ToString();
                ht.Add("ColumnID", columnID);
            }

            string tableID = "";
            if (Request.QueryString["TableID"] != null)
            {
                tableID = Request.QueryString["TableID"].ToString();
                ht.Add("TableID", tableID);

                //得到TableName
                string tableName = _systemtable.Select(int.Parse(tableID)).TableName;
                ht.Add("TableName", tableName);

            }
            string columnName = FieldColumnName.Text;
            ht.Add("ColumnName", columnName);
            if (columnName.ToLower().IndexOf(" as ") == -1)
            {
                ht.Add("IsFormula", "0");
            }
            

            ht.Add("ColumnText", FieldColumnText.Text);
            ht.Add("DataType", FieldDataType.SelectedValue);
            ht.Add("DataTypeName", DataType.GetDataTypeName(int.Parse(FieldDataType.SelectedValue)));
            if (FieldSelectType.SelectedValue != string.Empty)
            {
                ht.Add("SelectType", FieldSelectType.SelectedValue);
            }
            ht.Add("AddDate", DateTime.Now.ToShortDateString());
            ht.Add("ModifyDate", DateTime.Now.ToShortDateString());
            ht.Add("Remark", FieldRemark.Text);

            int nTableID = 0;
            //修改
            bool bSuccess = true;
            if (editMode == "UPDATE")
            {
                if (StringHelper.IsValidDBInt(Request.QueryString["ColumnID"]))
                {
                    int nColumnID = int.Parse(Request.QueryString["ColumnID"].ToString());
                    //CustomColumn de = _customcolumn.Select(nColumnID);
                    //FormBindingHelper.BindControlsToObject(de, this);
                    _customcolumn.Update(ht);

                    nTableID = _column.GetTableID(nColumnID); //返回列表时指定的参数
                }
            }
            else
            {
                CustomColumn de = new CustomColumn();
                //FormBindingHelper.BindControlsToObject(de, this);
                //de.TableID = int.Parse(Request.QueryString["TableID"].ToString());
                _customcolumn.Add(ht);

                nTableID = int.Parse(Request.QueryString["TableID"].ToString()); //返回列表时指定的参数
            }
            string sHtml = string.Empty;
            if (bSuccess)
            {
                sHtml = "alert('操作成功!');";
                sHtml += "backToList(" + nTableID + ");";
            }
            else
            {
                sHtml = "alert('操作失败!');";
            }

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sHtml", sHtml, true);
        }

        protected void FieldDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            short typeID = 0;
            if (StringHelper.IsValidDBInt(this.FieldDataType.SelectedValue))
            {
                typeID = short.Parse(this.FieldDataType.SelectedValue);
            }
            DataTypeEntity deType = DataType.SelectDataType(typeID);
            this.txtDetails.Text = deType.Remark;
        }
    }
}
