using System;
using System.Globalization;
using System.Web.UI;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;
using Jayrock.Json;

namespace OpenSignals.Frontend
{
    public partial class SignalDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();
            GetCurrentCity();

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

                JsonObject currentMarker = new JsonObject();
                currentMarker["id"] = s.SignalID.ToString();
                currentMarker["lat"] = s.Latitude;//.ToString(new CultureInfo("en-US"));
                currentMarker["lng"] = s.Longitude;//.ToString(new CultureInfo("en-US"));
                currentMarker["zoom"] = s.Zoom;//.ToString();
                currentMarker["image"] = new OpenSignals.Framework.Core.Utility.JsUtils.JsConstant(markerImage);
                currentMarker["zip"] = s.Zip;
                currentMarker["status"] = s.Status;

                ClientScript.RegisterClientScriptBlock(this.GetType(), "currentMarker", "currentMarker=" + currentMarker.ToString() + ";", true);
                //RegisterDocumentReadyFunction("init", "initDetailPage");

                if (!s.Attachment.Equals(string.Empty))
                {
                    divPhoto.Visible = true;
                    lnkPhoto.HRef = WebUtils.GetImageUrl( UploadPaths.Big, s.Attachment);
                    imgPhoto.ImageUrl = WebUtils.GetImageUrl(UploadPaths.Small, s.Attachment);
                    ogImage.Attributes["content"] = WebUtils.GetImageUrl(UploadPaths.Small, s.Attachment);
                }

                if (s.Status == Signal.SignalStatus.Resolved)
                {
                    ddlStatus.Items.FindByValue(Signal.SignalStatus.Resolved.ToString()).Selected = true;
                    ddlStatus.Enabled = false;
                    divResolved.Visible = true;
                }
            }
        }
    }
}