using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using OpenSignals.Framework.Core.Utility;

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

        /// <summary>
        /// Creates the table cell.
        /// </summary>
        /// <param name="ctrl">The CTRL.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        protected TableCell CreateTableCell(Control ctrl, string cssClass)
        {
            TableCell td = new TableCell();
            td.Controls.Add(ctrl);

            if (!cssClass.Equals(string.Empty))
                td.CssClass = cssClass;

            return td;
        }

        /// <summary>
        /// Creates the table cell.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        protected TableCell CreateTableCell(string text)
        {
            return this.CreateTableCell(new LiteralControl(text), string.Empty);
        }

        /// <summary>
        /// Creates the table cell.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        protected TableCell CreateTableCell(string text, string cssClass)
        {
            return this.CreateTableCell(new LiteralControl(text), cssClass);
        }

        /// <summary>
        /// Creates the table row.
        /// </summary>
        /// <returns></returns>
        protected TableRow CreateTableRow()
        {
            return new TableRow();
        }

        /// <summary>
        /// Creates the pagination row.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="colspan">The colspan.</param>
        /// <returns></returns>
        protected TableRow CreatePaginationRow(int totalRecords, int perPage, int colspan)
        {
            if (totalRecords < perPage)
            {
                TableRow tr = new TableRow();
                TableCell td = CreateTableCell(WebUtils.CreatePagination(totalRecords, perPage, string.Empty), "pagination");
                td.ColumnSpan = colspan;
                tr.Cells.Add(td);
                return tr;
            }
            else return new TableRow();
        }
    }
}
