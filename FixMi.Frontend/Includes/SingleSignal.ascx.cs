using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Signals;
using FixMi.Framework.Categories;
using System.Web.UI.HtmlControls;

namespace FixMi.Frontend.Includes
{
    public partial class SingleSignal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Populate(List<Signal> Signals)
        {
            rptList.DataSource = Signals;
            rptList.DataBind();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((HtmlAnchor)e.Item.FindControl("title")).InnerText = ((Signal)e.Item.DataItem).Subject;

                if (((Signal)e.Item.DataItem).Status == 0)
                    ((Image)e.Item.FindControl("status")).ImageUrl = "/images/alert.png";

                ((Label)e.Item.FindControl("timeframe")).Text = SignalUtils.GetTimeframe(((Signal)e.Item.DataItem).CreationDate);

                CategoryManager cm = new CategoryManager();
                ((Label)e.Item.FindControl("category")).Text = cm.Load(((Signal)e.Item.DataItem).CategoryID).Name;
            }
        }
    }
}