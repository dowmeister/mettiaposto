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
        
        private string _metaDescription = string.Empty;

        public string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = value; }
        }

        private string _metaKeywords = string.Empty;

        public string MetaKeywords
        {
            get { return _metaKeywords; }
            set { _metaKeywords = value; }
        }

        public List<String> Files
        {
            get { return _files; }
            set { _files = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TO-DO
            //_files.Add("/js/json.js");
            //_files.Add("/js/functions.js");
            //_files.Add("/js/validations.js");

            if (!_metaDescription.Equals(string.Empty))
            {
                metaDescription.Attributes["content"] = _metaDescription;
            }

            if (!_metaKeywords.Equals(string.Empty))
            {
                metaKeywords.Attributes["content"] = _metaKeywords;
            }
        }
    }
}