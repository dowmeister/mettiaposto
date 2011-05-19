using System;
using System.Web.UI;
using Jayrock.Json;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Frontend
{
    public partial class SignalDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();
            InitClientObjects();

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
                plhDescription.Controls.Add(new LiteralControl(s.Description));

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

                if (!s.Attachment.Equals(string.Empty))
                {
                    divPhoto.Visible = true;
                    lnkPhoto.HRef = WebUtils.GetImageUrl( UploadPaths.Big, s.Attachment);
                    imgPhoto.ImageUrl = WebUtils.GetImageUrl(UploadPaths.Small, s.Attachment);
                    ogImage.Attributes["content"] = WebUtils.GetImageUrl(UploadPaths.Small, s.Attachment);
                }

                switch (s.Status)
                {
                    case Signal.SignalStatus.ReOpened:
                        divStatusReopened.Visible = true;
                        divStatusReopened.InnerHtml = String.Format(divStatusReopened.InnerHtml, s.ReopenDate.ToShortDateString(), s.ReopenDescription);
                        break;
                    case Signal.SignalStatus.Approved:
                        divStatusNotResolved.Visible = true;
                        break;
                    case Signal.SignalStatus.Resolved:
                        divStatusResolved.Visible = true;
                        divStatusReopened.InnerHtml = String.Format(divStatusResolved.InnerHtml, s.ResolutionDate, s.ResolutionDescription);
                        break;
                    case Signal.SignalStatus.Expired:
                        divStatusExpired.Visible = true;
                        break;
                }

                if (sm.SearchNearZip(s.Zip, s.SignalID).Count == 0)
                {
                    liMapNearby.Visible = false;
                    mapNearby.Visible = false;
                }
            }
        }
    }
}