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

using UDEF.Component;
using UDEF.Domain.Enumerate;
using UDEF.Domain;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class EditSystemSelectType : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
        }

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

        private DataBaseTableService _databasetable;
        public DataBaseTableService databasetable
        {
            set { _databasetable = value; }
        }

        private UserDefineBaseService _userdefinebase;
        public UserDefineBaseService userdefinebase
        {
            set { _userdefinebase = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
                if (Request.QueryString["SelectTypeID"] + "" == "")
                {
                    this.liTitle.Text = "新增";
                }
                else
                {
                    this.liTitle.Text = "修改";
                    dataBind2();
                }
            }
        }

        private void dataBind2()
        {
            string tableName = this.FieldTableName.SelectedValue;
            IList<DataBaseColumn> list = _systemtable.SelectPrimaryKeyName(tableName);
            if (list.Count > 0)
            {
                this.FieldPrimaryID.Text = list[0].ColumnName;

                ListControlDataInit.Init(
                this.FieldPrimaryName,
                _systemtable.SelectDataBaseColumnAllForTable(tableName),
                "ColumnName",
                "ColumnName",
                false
                );

                ListControlDataInit.Init(
                this.FieldStopTagName,
                _systemtable.SelectBoolTypeDataBaseColumnForTable(tableName),
                "ColumnName",
                "ColumnName",
                true
                );
            }

            SystemSelectType objSystemSelectType = _systemselecttype.Select(int.Parse(Request.QueryString["SelectTypeID"] + ""));
            if (objSystemSelectType != null)
            {
                _userdefinebase.BindObjectToControls(this.Page, objSystemSelectType);
            }
            else
            {
                DataBaseTable objDataBaseTable = _databasetable.SelectByName(tableName);
                //this.FieldSelectTypeID.Text = "";
                this.FieldSelectTypeName.Text = objDataBaseTable != null
                    ? objDataBaseTable.TableName : "";
                this.FieldSelectTypeText.Text = objDataBaseTable != null
                    ? objDataBaseTable.Description : "";
                this.FieldRemark.Text = "";
            }
        }
        private void initPage()
        {
            ListControlDataInit.Init(
                this.FieldTableName,
                _databasetable.SelectAll(),
                "TableName",
                "TableName",
                true
                );

            if (StringHelper.IsValidDBInt(Request.QueryString["SelectTypeID"]))
            {
                int selectTypeID = int.Parse(Request.QueryString["SelectTypeID"].ToString());
                SystemSelectType objSystemSelectType = _systemselecttype.Select(selectTypeID);
                this.FieldTableName.SelectedValue = objSystemSelectType.TableName;
            }
        }

        protected void FieldTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataBind();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void dataBind()
        {
            string tableName = this.FieldTableName.SelectedValue;
            if (string.IsNullOrEmpty(tableName))
            {
                this.FieldPrimaryID.Text = "";
                this.FieldPrimaryName.Items.Clear();
                this.FieldStopTagName.Items.Clear();
            }
            else
            {
                IList<DataBaseColumn> list = _systemtable.SelectPrimaryKeyName(tableName);
                if (list.Count > 0)
                {
                    this.FieldPrimaryID.Text = list[0].ColumnName;

                    ListControlDataInit.Init(
                    this.FieldPrimaryName,
                    _systemtable.SelectDataBaseColumnAllForTable(tableName),
                    "ColumnName",
                    "ColumnName",
                    false
                    );

                    ListControlDataInit.Init(
                    this.FieldStopTagName,
                    _systemtable.SelectBoolTypeDataBaseColumnForTable(tableName),
                    "ColumnName",
                    "ColumnName",
                    true
                    );
                }

                /*SystemSelectType objSystemSelectType = _systemselecttype.SelectByTableName(tableName);
                if (objSystemSelectType != null)
                {
                    _userdefinebase.BindObjectToControls(this.Page, objSystemSelectType);
                }
                else
                {
                    DataBaseTable objDataBaseTable = _databasetable.SelectByName(tableName);
                    //this.FieldSelectTypeID.Text = "";
                    this.FieldSelectTypeName.Text = objDataBaseTable != null
                        ? objDataBaseTable.TableName : "";
                    this.FieldSelectTypeText.Text = objDataBaseTable != null
                        ? objDataBaseTable.Description : "";
                    this.FieldRemark.Text = "";
                }*/
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sHtml = string.Empty;
                string tableName = this.FieldTableName.SelectedValue;
                SystemSelectType objSystemSelectType = null;
                if (Request.QueryString["SelectTypeID"] + "" != "")
                {
                    objSystemSelectType = _systemselecttype.Select(int.Parse(Request.QueryString["SelectTypeID"].ToString()));
                    FormBindingHelper.BindControlsToObject(objSystemSelectType, this);
                    _systemselecttype.Update(objSystemSelectType);
                }
                else
                {
                    objSystemSelectType = new SystemSelectType();
                    FormBindingHelper.BindControlsToObject(objSystemSelectType, this);
                    objSystemSelectType.TableName = this.FieldTableName.SelectedValue;
                    objSystemSelectType.PrimaryName = this.FieldPrimaryName.SelectedValue;
                    _systemselecttype.Add(objSystemSelectType);
                }

                //清除所有的
                _userdefinebase.ClearCache(objSystemSelectType.SelectTypeID);
                CScript.alertAndGoto(this, "操作成功!", "ListSystemSelectType.aspx");
            }
            catch
            {
                CScript.alert(this, "操作失败!");
            }
        }
    }
}
