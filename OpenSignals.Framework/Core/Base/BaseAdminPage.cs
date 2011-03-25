using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using OpenSignals.Framework.Core.Utility;
using System.Web.UI.HtmlControls;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// Base class for Admin pages
    /// </summary>
    public class BaseAdminPage : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        public struct ImageButtons
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Delete = "/Admin/images/delete.png";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "/Admin/images/edit.png";
            /// <summary>
            /// 
            /// </summary>
            public const string Approve = "/Admin/images/approve.png";
            /// <summary>
            /// 
            /// </summary>
            public const string Reject = "/Admin/images/reject.png";
        }

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

        /// <summary>
        /// Creates the commands cell.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <returns></returns>
        protected TableCell CreateCommandsCell(List<Control> controls)
        {
            TableCell td = new TableCell();
            td.CssClass = "commands";
            for (int i = 0; i < controls.Count; i++)
            {
                td.Controls.Add(controls[i]);
            }
            return td;
        }

        /// <summary>
        /// Creates the image button.
        /// </summary>
        /// <param name="onclick">The onclick.</param>
        /// <param name="title">The title.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        protected HtmlAnchor CreateImageButton(string onclick, string title, string image)
        {
            HtmlAnchor a = new HtmlAnchor();
            a.HRef = "javascript:;";
            a.Title = title;
            a.Attributes.Add("onclick", onclick);
            Image img = new Image();
            img.ImageUrl = image;
            img.AlternateText = title;
            img.ToolTip = title;
            a.Controls.Add(img);
            return a;
        }
    }
}
