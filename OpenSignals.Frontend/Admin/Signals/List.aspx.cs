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
    public partial class List : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblList.Rows.Add(this.CreateListHeader(true, new string[] { "ID", "Oggetto", "Categoria", "Stato" }));

            SignalManager sm = new SignalManager();
            int totalRecords = 0;
            List<Signal> items = sm.Search(string.Empty, string.Empty, string.Empty, -1, -1, 0, out totalRecords);
        }
    }
}