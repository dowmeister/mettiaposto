using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace OpenSignals.Frontend.Includes
{
    [DefaultProperty("Include")]
    public class StaticFile
    {
        private string _url = string.Empty;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }

    [ParseChildren(true, "Files")]
    [PersistChildren(false)] 
    public partial class StaticFileManager : System.Web.UI.UserControl
    {
        private List<StaticFile> _Files = new List<StaticFile>();

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] 
        public List<StaticFile> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}