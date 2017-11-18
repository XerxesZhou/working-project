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
using UDEF.Domain;
using UDEF.Domain.Enumerate;
using UDEF.Service;

namespace UDEF.Web.UserDefine
{
    public partial class EditTable : BasePage
    {
        private AjaxService _ajax;
        public AjaxService ajax
        {
            set { _ajax = value; }
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

        private ColumnService _column;
        public ColumnService column
        {
            set { _column = value; }
        }

        private SystemColumnService _systemcolumn;
        public SystemColumnService systemcolumn
        {
            set { _systemcolumn = value; }
        }
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
            ListControlDataInit.Init(
                this.dropdownlistTableName,
                _databasetable.SelectAll(),
                "TableName",
                "TableName",
                false
            );

        }
        /// <summary>
        /// ������
        /// </summary>
        private void dataBind()
        {
            string tableName = this.dropdownlistTableName.SelectedValue;
            if (StringHelper.IsValidDBInt(Request.QueryString["TableID"]))
            {
                SystemTable de = _systemtable.Select(int.Parse(Request.QueryString["TableID"].ToString()));
                FormBindingHelper.BindObjectToControls(de, this);
                this.dropdownlistTableName.SelectedValue = de.TableName;
            }
            else
            {
                bindDataBaseTable(tableName);
            }

            tableName = this.dropdownlistTableName.SelectedValue;
            bindFromTableName(tableName);
        }

        protected void FieldTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = this.dropdownlistTableName.SelectedValue;
            bindFromTableName(tableName);
        }

        private void bindDataBaseTable(string tableName)
        {
            DataBaseTable objDataBaseTable = _databasetable.SelectByName(tableName);
            if (objDataBaseTable != null)
            {
                this.FieldTableText.Text = objDataBaseTable.Description;
                this.FieldRemark.Text = objDataBaseTable.Description;
            }
        }

        private void bindFromTableName(string tableName)
        {
            SystemTable de = _systemtable.SelectByName(tableName);
            if (de == null)
            {
                this.FieldPrimaryColumn.Text = "";
                this.FieldRemark.Text = "";
                this.FieldTableID.Text = "";
                this.FieldTableText.Text = "";
                IList<DataBaseColumn> listDataBaseColumn = _systemtable.SelectPrimaryKeyName(
                    tableName);
                //�������������ʾ�����ݿ�����,����Ϊ���ݿ���ͼ����
                if (listDataBaseColumn.Count > 0)
                {
                    this.FieldPrimaryColumn.Text = (listDataBaseColumn[0] as DataBaseColumn).ColumnName;
                    this.FieldPrimaryColumn.CssClass = "OnlyBottom";
                }
                else
                {
                    this.FieldPrimaryColumn.CssClass = "";
                }
                bindDataBaseTable(tableName);
            }
            else
            {
                FormBindingHelper.BindObjectToControls(de, this);
            }

            string sortExpression = this.GetSortExpression();
            this.GridView1.DataSource = _systemtable.SelectDataBaseColumnAllForTable(tableName, sortExpression);
            this.GridView1.DataBind();
            initGridViewCheckBox();
        }

        private void initGridViewCheckBox()
        {
            string tableName = this.dropdownlistTableName.SelectedValue;
            SystemTable objSystemTable = _systemtable.SelectByName(tableName);
            if (objSystemTable != null)
            {
                IList<Column> listColumn = _column.SelectColumnByTableID(objSystemTable.TableID);

                Hashtable htColumnName = new Hashtable();
                foreach (Column column in listColumn)
                {
                    htColumnName[column.ColumnName] = column.ColumnName;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    string columnName = row.Cells[2].Text;
                    if (htColumnName[columnName] != null)
                    {
                        CheckBox c = row.Cells[0].FindControl("checkboxname") as CheckBox;
                        c.Checked = true;
                        c.Enabled = false;
                    }
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
            bool isSuccess = true;
            int tableID = 0;
            string tableName = this.dropdownlistTableName.SelectedValue;
            SystemTable objSystemTable = _systemtable.SelectByName(tableName);

            //�޸ı���Ϣ������Ӧ��ϵͳ�ֶ���Ϣ
            if (objSystemTable != null)
            {
                FormBindingHelper.BindControlsToObject(objSystemTable, this);
                objSystemTable.TableName = tableName;
                isSuccess = _systemtable.Update(objSystemTable);
                tableID = objSystemTable.TableID;
                if (isSuccess)
                {
                    isSuccess = this.addDataBaseColumnToSystemColumn(tableID, false);
                }
                if (isSuccess)
                {
                    CScript.alertAndGoto(this.Page, "�޸ĳɹ���", "ListTable.aspx");
                }
                else
                {
                    CScript.alert(this.Page, "�޸�ʧ�ܣ�");
                }

            }
            else    //��������Ϣ������Ӧ��������ѡϵͳ�ֶ���Ϣ
            {
                objSystemTable = new SystemTable();
                FormBindingHelper.BindControlsToObject(objSystemTable, this);
                objSystemTable.TableName = tableName;
                tableID = _systemtable.Add(objSystemTable);
                if (tableID > 0)
                {
                    isSuccess = this.addDataBaseColumnToSystemColumn(tableID, true);
                }
                else
                {
                    isSuccess = false;
                }

                if (isSuccess)
                {
                    CScript.alertAndGoto(this.Page, "�����ɹ���", "ListTable.aspx");
                }
                else
                {
                    CScript.alert(this.Page, "����ʧ�ܣ�");
                }
            }
        }

        /// <summary>
        /// �Ѹñ�����ݿ��ֶμ���ϵͳ�ֶα���
        /// </summary>
        /// <param name="talbeID">��ID</param>
        /// <returns></returns>
        private bool addDataBaseColumnToSystemColumn(int tableID, bool isNew)
        {
            bool ischecked = true;
            bool isSuccess = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string columnName = row.Cells[2].Text;
                Control c = row.Cells[0].FindControl("checkboxname");
                if (c != null)
                {
                    ischecked = ((CheckBox)c).Checked;
                }

                 int columnLength = 0;
                 string sColumnLenght = row.Cells[7].Text;
                 int.TryParse(sColumnLenght, out columnLength);

                //�Ѿ�ѡ�У����������ݿ��ϵͳ�ֶα��в�����
                if (ischecked && (isNew || !_systemtable.IsSystemColumnExist(tableID, columnName)))
                {
                    SystemColumn objSystemColumn = new SystemColumn();
                    string sDataType = row.Cells[4].Text.Trim().ToLower();
                    string description = row.Cells[3].Text;
                    bool isPrimary = row.Cells[5].Text.ToLower() == "true";
                    bool isNullable = row.Cells[6].Text.ToLower() == "true";
                   

                    objSystemColumn.TableID = tableID;
                    objSystemColumn.ColumnName = columnName;
                    objSystemColumn.ColumnText = description;
                    objSystemColumn.Remark = description;
                    objSystemColumn.IsAlwaysRequired = !isNullable;
                    objSystemColumn.IsAlwaysReadonly = isPrimary;
                    objSystemColumn.ColumnLength = columnLength;

                    if (isPrimary)  //���������,���Ϊ�Ǳ�����
                    {
                        objSystemColumn.IsAlwaysRequired = false;
                    }

                    switch (sDataType)
                    {
                        case "smallint":
                        case "int":
                            objSystemColumn.DataType = (int)EnumDataType.IntType;
                            //Jack Added 2016-05-18 
                            if (objSystemColumn.ColumnName.ToLower().Contains("operatorid"))
                            {
                                objSystemColumn.DataType = (int)EnumDataType.Select;
                                objSystemColumn.SelectType = 42;//����Ա�б�
                            }
                            break;

                        case "bit":
                            objSystemColumn.DataType = (int)EnumDataType.CheckBox;
                            break;

                        case "smalldatetime":
                        case "datetime":
                            objSystemColumn.DataType = (int)EnumDataType.Date;
                            break;

                        case "float":
                        case "double":
                            objSystemColumn.DataType = (int)EnumDataType.Currency;
                            break;

                        default:
                            objSystemColumn.DataType = (int)EnumDataType.SingleText;
                            break;
                    }
                    _systemcolumn.Add(objSystemColumn);
                }

                //�����ֶε��ֽ���
                IList<SystemColumn> listSystemColumn = _systemcolumn.SelectDynamic(" AND TableID = " + tableID + " AND ColumnName = '" + columnName + "'");
                if (listSystemColumn != null && listSystemColumn.Count == 1)
                {
                    SystemColumn sColumn = listSystemColumn[0];
                    sColumn.ColumnLength = columnLength;
                    _systemcolumn.Update(sColumn);
                }
            }

            return isSuccess;
        }

        private string[] getAllChecked()
        {
            string str = Request.Form.Get("checkboxname");
            string[] ckb = str.Split(new char[] { ',' });

            return ckb;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState.Add("SortExpression", "");
            }

            if (ViewState["SortDirection"] == null)
            {
                ViewState.Add("SortDirection", "");
            }

            if (ViewState["SortExpression"].ToString().Equals(e.SortExpression))
            {
                if (ViewState["SortDirection"].ToString().Equals("DESC"))
                    ViewState["SortDirection"] = "ASC";
                else
                    ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression;
                ViewState["SortDirection"] = "ASC";
            }
            dataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "ItemOver(this)";
                e.Row.Attributes["onmouseout"] = "ItemOut(this)";
            }
        }
    }
}

