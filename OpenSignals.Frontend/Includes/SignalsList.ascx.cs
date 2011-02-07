using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Categories;
using System.Web.UI.HtmlControls;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend.Includes
{
    public partial class SignalsList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Populate(List<Signal> Signals, int totalRecords, int recordsPerPage)
        {
            rptList.DataSource = Signals;
            rptList.DataBind();

            pagination.Controls.Add(WebUtils.CreatePagination(totalRecords, recordsPerPage, "searchSignals"));
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((HtmlAnchor)e.Item.FindControl("title")).InnerText = ((Signal)e.Item.DataItem).Subject;
                ((HtmlAnchor)e.Item.FindControl("title")).HRef = "/" + ((Signal)e.Item.DataItem).City + "/" + ((Signal)e.Item.DataItem).SignalID + "/segnalazione.aspx";

                if (((Signal)e.Item.DataItem).Status == 1)
                    ((Image)e.Item.FindControl("status")).ImageUrl = "/images/alert.png";
                else
                    ((Image)e.Item.FindControl("status")).ImageUrl = "/images/check.png";

                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(((Signal)e.Item.DataItem).CreationDate);

                CategoryManager cm = new CategoryManager();
                ((Label)e.Item.FindControl("category")).Text = cm.Load(((Signal)e.Item.DataItem).CategoryID).Name;
            }
        }
    }
}