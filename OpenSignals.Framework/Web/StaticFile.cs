using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OpenSignals.Framework.Web
{
    /// <summary>
    /// 
    /// </summary>
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
}
