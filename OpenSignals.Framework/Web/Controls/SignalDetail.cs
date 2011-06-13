using System;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Web.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SignalDetail : System.Web.UI.UserControl
    {
        /// <summary>
        /// title control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl title;

        /// <summary>
        /// timeframe control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Literal timeframe;

        /// <summary>
        /// category control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Literal category;

        /// <summary>
        /// address control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Literal address;

        /// <summary>
        /// divImage control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl divImage;

        /// <summary>
        /// imgImage control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Image imgImage;

        /// <summary>
        /// lnkDetail control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlAnchor lnkDetail;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Builds the signal description.
        /// </summary>
        /// <param name="currentSignal">The current signal.</param>
        public void BuildSignalDescription(Signal currentSignal)
        {
            title.InnerText = currentSignal.Subject;
            address.Text = currentSignal.Address;
            CategoryManager cm = new CategoryManager();
            category.Text = cm.Load(currentSignal.CategoryID).Name;
            lnkDetail.HRef = currentSignal.Link;
            timeframe.Text = SignalUtils.GetTimeframe(currentSignal.CreationDate);
            if (!currentSignal.Attachment.Equals(string.Empty))
            {
                divImage.Visible = true;
                imgImage.ImageUrl = WebUtils.GetImageUrl(UploadPaths.Comments, currentSignal.Attachment);
            }
        }
    }
}