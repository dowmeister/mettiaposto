using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using System.IO;

namespace FixMi.Frontend
{
    public partial class Content : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringContains("page"))
            {
                using(StreamReader page = File.OpenText(Server.MapPath(Path.Combine("/Contents", GetFromQueryString("page") + ".htm"))))
                {
                    divContent.InnerHtml = page.ReadToEnd();
                }
            }
        }
    }
}