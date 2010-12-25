using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Comments;
using FixMi.Framework.Core.Utility;
using System.Web.UI.HtmlControls;
using FixMi.Framework.Signals;

namespace FixMi.Frontend.Includes
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
                ((HtmlGenericControl)e.Item.FindControl("comment")).InnerHtml = ((Comment)e.Item.DataItem).Text;
                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(((Comment)e.Item.DataItem).CreationDate);
            }
        }
    }
}