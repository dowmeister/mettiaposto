using System;
using System.Collections.Generic;

namespace OpenSignals.Frontend.Includes
{
    public partial class Head : System.Web.UI.UserControl
    {       
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

        protected void Page_Load(object sender, EventArgs e)
        {
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