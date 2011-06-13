using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Web.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SignalsList : System.Web.UI.UserControl
    {
        /// <summary>
        /// rptList control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Repeater rptList;

        /// <summary>
        /// pagination control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl pagination;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Populates the specified signals.
        /// </summary>
        /// <param name="Signals">The signals.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="recordsPerPage">The records per page.</param>
        public void Populate(List<Signal> Signals, int totalRecords, int recordsPerPage)
        {
            rptList.DataSource = Signals;
            rptList.DataBind();

            pagination.Controls.Add(WebUtils.CreatePagination(totalRecords, recordsPerPage, "searchSignals"));
        }

        /// <summary>
        /// Handles the ItemDataBound event of the rptList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((HtmlAnchor)e.Item.FindControl("title")).InnerText = ((Signal)e.Item.DataItem).Subject;
                ((HtmlAnchor)e.Item.FindControl("title")).HRef = ((Signal)e.Item.DataItem).Link;

                if (((Signal)e.Item.DataItem).Status == 1)
                    ((System.Web.UI.WebControls.Image)e.Item.FindControl("status")).ImageUrl = "/images/alert.png";
                else
                    ((System.Web.UI.WebControls.Image)e.Item.FindControl("status")).ImageUrl = "/images/check.png";

                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(((Signal)e.Item.DataItem).CreationDate);

                CategoryManager cm = new CategoryManager();
                ((Label)e.Item.FindControl("category")).Text = ((Signal)e.Item.DataItem).CategoryName;
            }
        }
    }
}