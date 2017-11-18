/***************************************************************************
 * 
 *       功能：     操作的枚举
 *       作者：     Leo
 *       日期：     2007-02-01
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/


namespace SmartSoft.Domain.Enumerate
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Reflection;
    using System.Security;

    public enum FormAction
    {
        //[EnumItemDescription("查看")]
        View = 1,
        //[EnumItemDescription("增加")]
        Insert = 2,
        //[EnumItemDescription("编辑")]
        Update = 3,
        //[EnumItemDescription("删除")]
        Delete = 4,
        //[EnumItemDescription("审批")]
        Approve = 5,
        //[EnumItemDescription("提交审批")]
        PutInApprove = 6,
        //[EnumItemDescription("复制")]
        Copy = 7
    }

    public class FormActionClass
    {
        public static FormAction GetFormAction(string sAction)
        {

            switch(sAction.ToLower())
            {
                case "view":
                    return FormAction.View;
                case "insert":
                    return FormAction.Insert;
                case "update":
                    return FormAction.Update;
                case "delete":
                    return FormAction.Delete;
                case "approve":
                    return FormAction.Approve;
                case "putinApprove":
                    return FormAction.PutInApprove;
                case "copy":
                    return FormAction.Copy;
                default:
                    return FormAction.View;
            }
        }
    }
}
