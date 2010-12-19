using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using FixMi.Framework.Signals;
using FixMi.Framework.Categories;
using System.Globalization;
using FixMi.Framework;

namespace FixMi.Frontend
{
    public partial class SignalDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RenderPage();
            }
        }

        private void RenderPage()
        {
            if (QueryStringContains("id"))
            {
                SignalManager sm = new SignalManager();
                Signal s = sm.LoadSingnal(int.Parse(GetFromQueryString("id")));
                divTitle.InnerHtml = s.Subject;
                divDescription.InnerHtml = s.Description;

                if (s.ShowName)
                    ltAuthor.Text = "Anonimo";
                else
                    ltAuthor.Text = s.Name;

                lblAddress.Text = s.Address;

                GetTimeFrame(s.CreationDate);

                CategoryManager cm = new CategoryManager();
                ltCategory.Text = cm.Load(s.CategoryID).Name;

                string func = JsUtils.CreateJsFunction("setMarker", false, "signalMarker" + GetFromQueryString("id"),
                    new JsUtils.JsFunction("new google.maps.LatLng(" + s.Latitude.ToString(new CultureInfo("en-US")) + "," + s.Longitude.ToString(new CultureInfo("en-US")) + ")"),
                    false, "map_canvas", true, true) + "getMap('map_canvas').obj.setZoom(" + s.Zoom.ToString() + ");";

                RegisterDocumentReadyFunction("setmarker", func);

                nearby.Attributes.Add("zip", s.Zip);
            }
        }

        private void GetTimeFrame(DateTime creationDate)
        {
            TimeSpan ts = DateTime.Now.Subtract(creationDate);

            if (ts.Days > 60)
            {
                ltTimeFrame.Text = "circa 1 mese fa (il " + creationDate.ToShortDateString() + ")";
                return;
            }

            if (ts.Days > 30)
            {
                ltTimeFrame.Text = "circa " + (ts.Days / 30).ToString() + " mesi fa (il " + creationDate.ToShortDateString() + ")";
                return;
            }

            if (ts.Days > 1)
            {
                ltTimeFrame.Text = ts.Days.ToString() + " giorni fa alle " + creationDate.ToShortTimeString();
                return;
            }

            if (ts.Days == 1)
            {
                ltTimeFrame.Text = "ieri alle " + creationDate.ToShortTimeString();
                return;
            }
            
            if (ts.Days == 0 && ts.Hours > 1)
            {
                ltTimeFrame.Text = ts.Hours.ToString() + " ore fa e " + ts.Minutes.ToString() + " fa";
                return;
            }

            ltAuthor.Text = ts.Minutes.ToString() + " minuti fa";
        }
    }
}