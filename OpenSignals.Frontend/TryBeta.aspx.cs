using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Frontend
{
    public partial class TryBeta : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddToCookie("OSBETA", true, DateTime.Now.AddDays(60));
            Response.Redirect("/milano/index.aspx");
        }
    }
}