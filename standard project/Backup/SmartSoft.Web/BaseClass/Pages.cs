using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SmartSoft.Web.BaseClass
{
    public class Pages
    {
        private string _id;
        private string _name;
        private string _src;
        private string _align;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Src
        {
            get { return _src; }
            set { _src = value; }
        }

        public string Align
        {
            get { return _align; }
            set { _align = value; }
        }

    }
}
