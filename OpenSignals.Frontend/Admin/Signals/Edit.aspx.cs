using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Frontend.Admin.Signals
{
    public partial class Edit : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BuildPage();
            }
        }

        private void BuildPage()
        {
            SignalManager sm = new SignalManager();
            Signal s = sm.LoadSingnal(int.Parse(GetFromQueryString("ID")));

            
        }
    }
}