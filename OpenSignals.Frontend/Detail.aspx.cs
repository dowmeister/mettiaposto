using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Categories;
using System.Globalization;
using OpenSignals.Framework;
using System.IO;
using System.Configuration;
using OpenSignals.Framework.Core;

namespace OpenSignals.Frontend
{
    public partial class SignalDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();

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

                this.Title = String.Format(this.Title, s.Subject, s.Address, s.City);
                metaOgDescription.Attributes["content"] = s.Excerpt;
                ogTitle.Attributes["content"] = String.Format(ogTitle.Attributes["content"], s.Subject, s.Address, s.City);
                
                if (!s.ShowName)
                    ltAuthor.Text = "Anonimo";
                else
                    ltAuthor.Text = s.Name;

                lblAddress.Text = s.Address;

                ltTimeFrame.Text = SignalUtils.GetTimeframe(s.CreationDate);

                CategoryManager cm = new CategoryManager();
                ltCategory.Text = cm.Load(s.CategoryID).Name;

                string markerImage = "MARKERIMAGE_ALERT";
                if (s.Status == Signal.SignalStatus.Resolved)
                    markerImage = "MARKERIMAGE_OK";

                string func = JsUtils.CreateJsFunction("setMarker", false, "signalMarker" + GetFromQueryString("id"),
                    new JsUtils.JsFunction("new google.maps.LatLng(" + s.Latitude.ToString(new CultureInfo("en-US")) + "," + s.Longitude.ToString(new CultureInfo("en-US")) + ")"),
                    false, "map_canvas", true, true, new JsUtils.JsConstant(markerImage)) + "getMap('map_canvas').obj.setZoom(" + s.Zoom.ToString() + "); fbInit(); getComments(0); ";

                RegisterDocumentReadyFunction("setmarker", func);

                nearby.Attributes.Add("zip", s.Zip);

                if (!s.Attachment.Equals(string.Empty))
                {
                    divPhoto.Visible = true;
                    lnkPhoto.HRef = s.GetImageUrl( UploadPaths.Big);
                    imgPhoto.ImageUrl = s.GetImageUrl(UploadPaths.Small);

                    RegisterDocumentReadyFunction("fancybox", "$('#lnkPhoto').fancybox(); ");

                    ogImage.Attributes["content"] = s.GetImageUrl(UploadPaths.Small);
                }

                if (s.Status == Signal.SignalStatus.Resolved)
                {
                    ddlStatus.Items.FindByValue(Signal.SignalStatus.Resolved.ToString()).Selected = true;
                    ddlStatus.Enabled = false;
                    divResolved.Visible = true;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "currentSignalID", "currentSignalID=" + GetFromQueryString("id"), true);
            }
        }
    }
}