using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;

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

        public static HtmlGenericControl CreatePagination(int totalRecords, int recordsPerPage, string func)
        {
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)recordsPerPage));

            if (totalRecords <= 1)
                return new HtmlGenericControl("ul");
            else
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");

                for (int i = 0; i < totalPages; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HtmlAnchor a = new HtmlAnchor();
                    a.InnerHtml = (i + 1).ToString();
                    a.HRef = "#";
                    a.Attributes.Add("onclick", JsUtils.CreateJsFunction(func, true, (i * recordsPerPage)));
                    li.Controls.Add(a);
                    ul.Controls.Add(li);
                }

                return ul;
            }
        }
    }
}
