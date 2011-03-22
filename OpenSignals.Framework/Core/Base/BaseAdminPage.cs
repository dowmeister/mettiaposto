using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// Base class for Admin pages
    /// </summary>
    public class BaseAdminPage : BasePage
    {
        /// <summary>
        /// Creates the list header.
        /// </summary>
        /// <param name="addOperationsColumn">if set to <c>true</c> [add operations column].</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        protected TableHeaderRow CreateListHeader(bool addOperationsColumn, params string[] cols)
        {
            TableHeaderRow tr = new TableHeaderRow();
            foreach (string s in cols)
            {
                TableHeaderCell th = new TableHeaderCell();
                th.Text = s;
                tr.Cells.Add(th);
            }

            if (addOperationsColumn)
            {
                TableHeaderCell th = new TableHeaderCell();
                th.CssClass = "commands";
                tr.Cells.Add(th);
            }

            return tr;
        }
    }
}
