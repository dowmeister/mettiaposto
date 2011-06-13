using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Comments;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Web.Controls
{
    public partial class CommentsList : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Populate(List<Comment> comments, int totalRecords)
        {
            if (comments.Count > 0)
            {
                pagination.Controls.Add(WebUtils.CreatePagination(totalRecords, 5, "getComments"));
                rptList.DataSource = comments;
                rptList.DataBind();
            }
            else
                this.Visible = false;
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Comment c = (Comment)e.Item.DataItem;

                ((HtmlGenericControl)e.Item.FindControl("comment")).InnerHtml = c.Text;
                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(c.CreationDate);

                if (c.ShowAuthorName)
                {
                    //if (c.AuthorReferenceType == Comment.AuthorType.Facebook)
                    //    ((Literal)e.Item.FindControl("ltAuthor")).Text = "<fb:name uid=\"" + c.AuthorReferenceKey + "\" use-you=\"no\"></fb:name>";
                    //else
                    ((Literal)e.Item.FindControl("ltAuthor")).Text = c.AuthorName;
                }
                else
                    ((Literal)e.Item.FindControl("ltAuthor")).Text = "Anonimo";

                if (!c.Attachment.Equals(string.Empty))
                {
                    ((HtmlGenericControl)e.Item.FindControl("divPhoto")).Visible = true;
                    ((HtmlAnchor)e.Item.FindControl("lnkPhoto")).HRef = WebUtils.GetImageUrl(UploadPaths.Big, c.Attachment);
                    ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgPhoto")).ImageUrl = WebUtils.GetImageUrl(UploadPaths.Comments, c.Attachment);
                }

                if (c.AuthorReferenceType == Comment.AuthorType.Facebook)
                    ((System.Web.UI.WebControls.Image)e.Item.FindControl("avatar")).ImageUrl = "http://graph.facebook.com/" + c.AuthorReferenceKey + "/picture?type=square";
            }
        }
    }
}