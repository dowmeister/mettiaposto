using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using System.Resources;
using System.Globalization;

namespace FixMi.Frontend
{
    public partial class Search : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower());
        }
    }
}