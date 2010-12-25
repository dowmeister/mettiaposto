using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FixMi.Frontend.Includes
{
    public partial class Head : System.Web.UI.UserControl
    {
        private List<String> _files = new List<string>();

        public List<String> Files
        {
            get { return _files; }
            set { _files = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _files.Add("/js/json.js");
            _files.Add("/js/functions.js");
            _files.Add("/js/validations.js");
        }
    }
}