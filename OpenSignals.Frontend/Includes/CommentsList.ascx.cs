using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Comments;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Frontend.Includes
{
    public partial class CommentsList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Populate(List<Comment> comments, int totalRecords)
        {
            pagination.Controls.Add(WebUtils.CreatePagination(totalRecords, 5, "getComments"));
            rptList.DataSource = comments;
            rptList.DataBind();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Comment c = (Comment)e.Item.DataItem;

                ((HtmlGenericControl)e.Item.FindControl("comment")).InnerHtml = c.Text;
                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(c.CreationDate);

                if (c.ShowAuthorName)
                    ((Label)e.Item.FindControl("author")).Text = c.AuthorName;
                else
                    ((Label)e.Item.FindControl("author")).Text = "Anonimo";

                if (!c.Attachment.Equals(string.Empty))
                {
                    ((HtmlGenericControl)e.Item.FindControl("divPhoto")).Visible = true;
                    ((HtmlAnchor)e.Item.FindControl("lnkPhoto")).HRef = WebUtils.GetImageUrl(UploadPaths.Big, c.Attachment);
                    ((Image)e.Item.FindControl("imgPhoto")).ImageUrl = WebUtils.GetImageUrl(UploadPaths.Comments, c.Attachment);
                }
            }
        }
    }
}