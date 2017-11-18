using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using SmartSoft.Component;

namespace SmartSoft.Presentation
{
    /// <summary>
    /// 表单页面的基类,Web层所有表单应继承此类
    /// </summary>
    /// <typeparam name="T">表单处理的类型</typeparam>
    public class WebForm<T> : PageBase
    {
        #region Properties

        public Type FormType
        {
            get
            {
                return typeof(T);
            }
        }

        public T FormData
        {
            get
            {
                this.GetFormData();
                return (T)ViewState["FormData"]; 
            }
            set 
            { 
                ViewState["FormData"] = value;
                this.SetCtrlValue(value);
            }
        }

        #endregion

        #region 得到页面上所有控件

        protected List<Control> GetCtrlList(Control ctrlObject)
        {
            List<Control> al = new List<Control>();
            this.GetCtrlMethod(ctrlObject, al);
            return al;
        }

        private void GetCtrlMethod(Control ctrlObject,List<Control> ctrls)
        {
            foreach (Control ctrl in ctrlObject.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is DropDownList)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is Label)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is Literal)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is RadioButtonList)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is HtmlInputHidden)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl is CheckBox)
                {
                    ctrls.Add(ctrl);
                }
                if (ctrl.HasControls())
                {
                    this.GetCtrlMethod(ctrl, ctrls);
                }
            }
        }

        #endregion

        #region 得到控件对应的字段名
        /// <summary>
        /// 得到控件对应的字段名
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public string GetColumnName(Control ctrl)
        {
            string strColumnName = string.Empty;
            string strControlId = ctrl.ID;

            if (strControlId.IndexOf("_") != -1)
            {
                strColumnName = strControlId.Substring(strControlId.IndexOf("_") + 1);
            }

            return strColumnName;
        }
        #endregion

        #region 得到控件值

        /// <summary>
        /// 得到控件值
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public string GetCtrlValue(Control ctrl)
        {
            string strCtrlValue = string.Empty;

            if (ctrl is TextBox)
            {
                strCtrlValue = ((TextBox)ctrl).Text.Trim();
            }
            if (ctrl is Label)
            {
                strCtrlValue = ((Label)ctrl).Text.Trim();
            }
            if (ctrl is Literal)
            {
                strCtrlValue = ((Literal)ctrl).Text.Trim();
            }
            if (ctrl is ListControl)
            {
                strCtrlValue = ((ListControl)ctrl).SelectedValue.Trim();
            }
            if (ctrl is HtmlInputHidden)
            {
                strCtrlValue = ((HtmlInputHidden)ctrl).Value.Trim();
            }
            if (ctrl is CheckBox)
            {
                if (((CheckBox)ctrl).Checked)
                {
                    strCtrlValue = "true";
                }
                else
                {
                    strCtrlValue = "false";
                }
            }

            return strCtrlValue;
        }

        #endregion

        #region 设置控件的值
        /// <summary>
        /// 设置控件的值
        /// </summary>
        /// <param name="formData"></param>
        public void SetCtrlValue(T formData)
        {
            Type type = this.FormType;
            PropertyInfo[] pis = type.GetProperties(BINDING_FLAGS_SET);

            IList<Control> al = this.GetCtrlList(this);

            foreach (PropertyInfo pi in pis)
            {
                foreach (Control ctrl in al)
                {
                    string strColumnName = this.GetColumnName(ctrl);
                    if (pi.Name == strColumnName)
                    {
                        object propertyValue = this.GetPropertyValue(formData,pi.Name);
                        string value = (propertyValue == null) ? string.Empty : propertyValue.ToString();
                        if (propertyValue != null)
                        {
                            this.SetCtrlValue(value, ctrl);
                        }
                    }
                }
            }
        }
        #endregion

        #region 得到某对象的某属性的值
        /// <summary>
        /// 得到某对象的属性值
        /// </summary>
        private object GetPropertyValue(object data, string propertyName)
        {
            Type type = data.GetType();

            PropertyInfo pi = type.GetProperty(propertyName);

            object propertyValue = pi.GetValue(data, null);

            return propertyValue;
        }
        #endregion

        #region 设置某对象的属性值
        /// <summary>
        /// 将数据赋给某对象的属性
        /// </summary>
        private void SetPropertyValue(object data, string propertyName, object propertyValue)
        {
            Type type = data.GetType();

            PropertyInfo pi = type.GetProperty(propertyName);


            if (pi != null)
            {
                propertyValue = DataTypeConverter.ChangeType(pi.PropertyType, propertyValue);
                pi.SetValue(data, propertyValue, null);
            }

        }
        #endregion

        #region 将数值赋给控件
        /// <summary>
        /// 将数值赋给控件
        /// </summary>
        /// <param name="strValueField">要选中的值</param>
        /// <param name="listctrl">DropDownList，CheckBoxList，RadioButtonList，ListBox控件Id值</param>
        public void SetCtrlValue(string strValueField, Control ctrl)
        {
            if (ctrl != null)
            {
                strValueField = strValueField.Trim();
                try
                {
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = strValueField.ToString().Trim();
                    }

                    if (ctrl is Label)
                    {
                        ((Label)ctrl).Text = strValueField.ToString().Trim();

                    }

                    if (ctrl is HtmlInputHidden)
                    {
                        ((HtmlInputHidden)ctrl).Value = strValueField.ToString().Trim();
                    }

                    if (ctrl is HtmlInputFile)
                    {
                        ((HtmlInputFile)ctrl).Value = strValueField.ToString().Trim();
                    }

                    if (ctrl is HtmlInputText)
                    {
                        ((HtmlInputText)ctrl).Value = strValueField.ToString().Trim();
                    }

                    //DropDownList|CheckBoxList|RadioButtonList|ListBox
                    if ((ctrl is DropDownList) || (ctrl is CheckBoxList) || (ctrl is RadioButtonList) || (ctrl is ListBox))
                    {
                        //＝＝＝DropDownList＝＝＝
                        if (((ListControl)ctrl).SelectedItem != null)
                            ((ListControl)ctrl).SelectedItem.Selected = false;
                        for (int i = 0; i < ((ListControl)ctrl).Items.Count; i++)
                        {
                            if (strValueField == ((ListControl)ctrl).Items[i].Value.Trim())
                            {
                                ((ListControl)ctrl).Items[i].Selected = false;
                                ((ListControl)ctrl).Items[i].Selected = true;
                            }
                        }
                    }

                    if (ctrl is CheckBox)
                    {
                        ((CheckBox)ctrl).Checked = (strValueField.Trim().ToLower() == "true" || strValueField.Trim().ToLower() == "1");
                    }
                }
                catch { throw; }
            }

        }

        #endregion

        #region 得到页面控件的值

        public void GetFormData()
        {
            //得到模型的类型
            Type type = this.FormType;

            T instance;

            if (ViewState["formData"] != null)
            {
                instance = (T)ViewState["formData"];
            }
            else
            {
                instance = (T)Activator.CreateInstance(type);
            }

            List<Control> al = this.GetCtrlList(this);
            PropertyInfo[] pis = type.GetProperties(BINDING_FLAGS_SET);

            foreach (PropertyInfo pi in pis)
            {
                foreach (Control ctrl in al)
                {
                    string strColumnName = this.GetColumnName(ctrl);
                    if (pi.Name == strColumnName)
                    {
                        this.SetPropertyValue(instance, pi.Name, this.GetCtrlValue(ctrl));
                    }
                    
                }
            }

            ViewState["FormData"] = instance;
        }

        #endregion

        #region 文件上传

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="filePath"></param>
        /// <param name="myfileName"></param>
        /// <param name="isRandom"></param>
        /// <returns></returns>
        public FileUpLoad UpLoadFile(HtmlInputFile InputFile, string filePath,bool isVitural, double currentSpace, double maxSpace)
        {

            FileUpLoad fp = new FileUpLoad();

            string fileName, fileExtension;

            //
            //建立上传对象
            //
            HttpPostedFile postedFile = InputFile.PostedFile;

            if (postedFile.ContentLength + currentSpace <= maxSpace * 1024 * 1024)
            {

                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);
                ////
                ////根据类型确定文件格式
                ////
                //AppConfig app = new AppConfig();
                //string format = app.GetPath("FileUpLoad/Format");


                ////
                ////如果格式都不符合则返回
                ////
                //if (format.IndexOf(fileExtension) == -1)
                //{
                //    throw new ApplicationException("上传数据格式不合法");
                //}

                //
                //根据日期和随机数生成随机的文件名
                //
                string phyPath = string.Empty;
                if (isVitural)
                {
                    phyPath = HttpContext.Current.Request.MapPath(filePath);
                }
                else
                {
                    phyPath = filePath;
                }



                //判断路径是否存在,若不存在则创建路径
                DirectoryInfo upDir = new DirectoryInfo(phyPath);
                if (!upDir.Exists)
                {
                    upDir.Create();
                }

                //
                //保存文件
                //
                try
                {
                    FileInfo fi = new FileInfo(phyPath + fileName);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    postedFile.SaveAs(phyPath + fileName);

                    fp.FilePath = filePath + fileName;
                    fp.FileExtension = fileExtension;
                    fp.FileName = fileName;
                    fp.ContentType = postedFile.ContentType;
                    fp.FileSize = postedFile.ContentLength;
                }
                catch
                {
                    throw new ApplicationException("上传失败!");
                }


                //返回上传文件的信息
                return fp;
            }
            else
            {
                throw new ApplicationException("上传空间不足！");
            }
        }

        /// <summary>
        /// 个人文档库的文件上传
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="currentUsedSpace"></param>
        /// <param name="maxSpace"></param>
        /// <param name="dirID"></param>
        /// <returns></returns>
        public SmartSoft.Component.FileUpLoad UpLoadFile(HttpPostedFile postedFile, string filePath, bool isVirtual, double currentUsedSpace, double maxSpace, double totalCurrentUsedSpace, double totalMaxSpace, int dirID)
        {
            if (postedFile.ContentLength + currentUsedSpace <= maxSpace * 1024 * 1024
                && postedFile.ContentLength + totalCurrentUsedSpace <= totalMaxSpace * 1024 * 1024)
            {

                SmartSoft.Component.FileUpLoad fp = new SmartSoft.Component.FileUpLoad();

                string fileName, fileExtension;

                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);



                //
                //根据目录ID和文件名构造生成物理文件名
                //
                string phyPath = string.Empty;
                if (isVirtual)
                {
                    phyPath = HttpContext.Current.Request.MapPath(filePath);
                }
                else
                {
                    phyPath = filePath;
                }

                //
                phyPath += this.Operator.opeID.ToString() + @"\";

                //判断路径是否存在,若不存在则创建路径
                DirectoryInfo upDir = new DirectoryInfo(phyPath);
                if (!upDir.Exists)
                {
                    upDir.Create();
                }

                //
                //保存文件
                //
                try
                {
                    //构造物理名称
                    phyPath += "[" + dirID + "]" + fileName;
                    FileInfo fi = new FileInfo(phyPath);
                    if (fi.Exists)
                    {
                        //fi.Delete();
                        throw new ApplicationException("上传失败!原因：该目录下已经存在相同名称的文件！请重命名后再上传!");
                    }

                    postedFile.SaveAs(phyPath);

                    fp.FilePath = phyPath;
                    fp.FileExtension = fileExtension;
                    fp.FileName = fileName;
                    fp.ContentType = postedFile.ContentType;
                    fp.FileSize = postedFile.ContentLength;
                }
                catch (ApplicationException e)
                {
                    throw;
                }

                catch
                {
                    throw new ApplicationException("上传失败!");
                }


                //返回上传文件的信息
                return fp;
            }
            else
            {
                throw new ApplicationException("上传空间不足！");
            }
        }

        public SmartSoft.Component.FileUpLoad UpLoadFile(HttpPostedFile postedFile, string filePath, bool isVirtual, double currentUsedSpace, double maxSpace, int dirID, int maxFileLength)
        {
            if (postedFile.ContentLength + currentUsedSpace <= maxSpace * 1024 * 1024)
            {

                SmartSoft.Component.FileUpLoad fp = new SmartSoft.Component.FileUpLoad();

                string fileName, fileExtension;

                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);



                //
                //根据目录ID和文件名构造生成物理文件名
                //
                string phyPath = string.Empty;
                if (isVirtual)
                {
                    phyPath = HttpContext.Current.Request.MapPath(filePath);
                }
                else
                {
                    phyPath = filePath;
                }

                //
                phyPath += this.Operator.opeID.ToString() + @"\";

                //判断路径是否存在,若不存在则创建路径
                DirectoryInfo upDir = new DirectoryInfo(phyPath);
                if (!upDir.Exists)
                {
                    upDir.Create();
                }

                //
                //保存文件
                //
                try
                {
                    //构造物理名称
                    phyPath += "[" + dirID + "]" + fileName;
                    FileInfo fi = new FileInfo(phyPath);
                    if (fi.Exists)
                    {
                        //fi.Delete();
                        throw new ApplicationException("上传失败!原因：该目录下已经存在相同名称的文件！请重命名后再上传!");
                    }

                    postedFile.SaveAs(phyPath);

                    fp.FilePath = phyPath;
                    fp.FileExtension = fileExtension;
                    fp.FileName = fileName;
                    fp.ContentType = postedFile.ContentType;
                    fp.FileSize = postedFile.ContentLength;
                }
                catch (ApplicationException e)
                {
                    throw;
                }

                catch
                {
                    throw new ApplicationException("上传失败!");
                }


                //返回上传文件的信息
                return fp;
            }
            else
            {
                throw new ApplicationException("上传空间不足！");
            }
        }
        #endregion
    }
}
