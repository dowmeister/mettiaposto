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
using System.IO;
using System.Configuration;
using FixMi.Framework.Core;

namespace FixMi.Frontend
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
                    false, "map_canvas", true, true, new JsUtils.JsConstant(markerImage)) + "getMap('map_canvas').obj.setZoom(" + s.Zoom.ToString() + ");";

                RegisterDocumentReadyFunction("setmarker", func);

                nearby.Attributes.Add("zip", s.Zip);

                if (!s.Attachment.Equals(string.Empty))
                {
                    divPhoto.Visible = true;
                    lnkPhoto.HRef = Path.Combine(ConfigurationManager.AppSettings["UploadPath"], UploadPaths.Big + s.Attachment);
                    imgPhoto.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["UploadPath"], UploadPaths.Small + s.Attachment);

                    RegisterDocumentReadyFunction("fancybox", "$('#lnkPhoto').fancybox(); ");
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "currentSignalID", "currentSignalID=" + GetFromQueryString("id"), true);
            }
        }
    }
}