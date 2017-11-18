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

    public partial class OperatorPlanEditForm :PageBaseList
    {
        #region Fields
        private DataController _datacontroller;
        public DataController datacontroller
        {
            set { _datacontroller = value; }
        }

        protected string title = "业务员签约回款收款设置";
        #endregion

        #region Event
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.initPage();
                //base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                basecontroller.LogException(Request.Path, this.Operator.opeName, "Page_Load", ex);
                WebFunc.Alert("加载失败!\n请检查后再重试,如仍有问题，请联系管理员.", Page);
            }
        }

        protected void lb_Search2_Click(object sender, EventArgs e)
        {
            DataBindToGridview();
        }
        protected void lb_SetBudget_Click(object sender, EventArgs e)
        {
            int year = int.Parse(ddlYear.SelectedValue);
            string sql = "";
            string opeid = "0";
            for (int i = 0; i < GridOperatorEdit.Rows.Count; i++)
            {
                Label LabVis = (Label)GridOperatorEdit.Rows[i].FindControl("LabVisible");
                opeid = LabVis.Text;
                if (DbHelperSQL.Exists("select * from OperatorPlan where opOperatorID = " + opeid + " AND opYear= " + year + " and opTypeID=" + this.SearchopTypeID.SelectedValue))
                {
                    sql = "update OperatorPlan set opModifyDate=getdate(),opModifyOperatorID=" + this.Operator.opeID.ToString() + ",opTypeID=" + this.SearchopTypeID.SelectedValue;
                    for (int j = 1; j <= 12; j++)
                    {
                        float m = 0.0f;
                        TextBox txtM = (TextBox)GridOperatorEdit.Rows[i].FindControl("txtM" + j.ToString());
                        float.TryParse(txtM.Text.ToString(), out m);
                        sql += ", opM" + j.ToString() + "=" + m.ToString();
                    }
                    sql += " where opOperatorID = " + opeid + " AND opYear= " + year + " and opTypeID = " + this.SearchopTypeID.SelectedValue;

                    DbHelperSQL.ExecuteSQL(sql);
                }
                else
                {
                    sql = "insert into OperatorPlan(opAddDate,opAddOperatorID,opOperatorID,opYear,opM1,opM2,opM3,opM4,opM5,opM6,opM7,opM8,opM9,opM10,opM11,opM12,opTypeID) values(getdate()," + this.Operator.opeID + "," + opeid + "," + year;
                    for (int j = 1; j <= 12; j++)
                    {
                        float m = 0.0f;
                        TextBox txtM = (TextBox)GridOperatorEdit.Rows[i].FindControl("txtM" + j.ToString());
                        float.TryParse(txtM.Text.ToString(), out m);
                        sql += "," + m.ToString();
                    }
                    sql += "," + this.SearchopTypeID.SelectedValue + ")";
                    DbHelperSQL.ExecuteSQL(sql);
                }
                
            }

            DbHelperSQL.ExecuteSQL("update OperatorPlan set opTotal = opM1 + opM2+opM3+opM4+opM5+opM6+opM7+opM8+opM9+opM10+opM11+opM12 ");
            Response.Write("<script>alert('操作成功！');</script>");

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindToGridview();
        }

        private void DataBindToGridview()
        {
            string sql = string.Format("select * from Operators A left outer join defYear B on 1=1 left outer join (select * from sdefCommon where sTypeName='OperatorPlanType') D on 1=1 left outer join OperatorPlan C ON A.opeID = C.opOperatorID and B.yID = C.opYear and C.opTypeID = D.sID where opeName like '%{0}%'", this.txtopeName.Text);
            if (this.SearchopTypeID.SelectedValue != "")
            {
                sql += " AND sID = " + this.SearchopTypeID.SelectedValue;
            }

            sql += this.GetWhereConditionForUser("opeID", this.Operator.opeID, this.Operator.opeDepartmentID + "");
            sql += " AND yID = " + this.ddlYear.SelectedValue;
            GridOperatorEdit.DataSource =DbHelperSQL.GetDataTable(sql);
            GridOperatorEdit.DataBind();
        }
        #endregion

        #region Mothed
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void initToolBar()
        {
            //查询
            this.ToolBar1.lb_Search.Visible = true;
            this.ToolBar1.lb_Search.Click += this.lb_Search2_Click;

            this.ToolBar1.lb_Close.Visible = true;
            this.ToolBar1.lb_Close.Attributes.Add("href", "javascript:window.close();");

            this.ToolBar1.lb_Save.Visible = true;
            this.ToolBar1.lb_Save.Click += this.lb_SetBudget_Click;
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            this.initToolBar();
            if (!IsPostBack)
            {
                this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                DbHelperSQL.BindDropDownList2("select * from sdefCommon where sTypeName='OperatorPlanType'", SearchopTypeID, "sName", "sID");
                DataBindToGridview(); 
            }
        }
        #endregion
    }
}
