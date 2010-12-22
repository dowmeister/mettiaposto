using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace FixMi.Framework.Core.Utility
{
    public class WebUtils
    {
        public static string RenderControlToString(Control c)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            c.RenderControl(htmlWriter);
            return sb.ToString();
        }
    }
}
