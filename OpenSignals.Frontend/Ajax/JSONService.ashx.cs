using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Jayrock.Json;
using Jayrock.JsonRpc;
using OpenSignals.Framework.Comments;
using OpenSignals.Framework.Communications.Messages;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;
using OpenSignals.Frontend.Includes;
using OpenSignals.Framework.Places;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Newsletter;

namespace OpenSignals.Frontend.Ajax
{
    /// <summary>
    /// Summary description for JSON
    /// </summary>
    public class JSONService : BaseJSONHandler
    {
        private void CheckRequest(string key)
        {
            if (HttpContext.Current.Request.UrlReferrer.Host.Equals(HttpContext.Current.Request.Url.Host))
            {
                if (HttpContext.Current.Session["AjaxSessionKey"] != null)
                {
                    if (!key.Equals(HttpContext.Current.Session["AjaxSessionKey"].ToString()))
                        throw new Exception("Richiesta AJAX non autorizzata");
                }
                else
                    throw new Exception("Richiesta AJAX non autorizzata");
            }
            else
                throw new Exception("Richiesta AJAX negata");
        }

        [JsonRpcMethod("addSignal")]
        public Signal AddSignal(Signal s, string ajaxSessionKey)
        {
            CheckRequest(ajaxSessionKey);

            SignalManager sm = new SignalManager();
            s.CategoryName = null;
            s.Description = WebUtils.FormatHtml(s.Description);
            s.CreationDate = DateTime.Now;
            s.UpdateDate = DateTime.Now;
            s.ReopenDescription = string.Empty;

            if (ConfigurationOptions.Current.GetBool("signal_approve_on_submission"))
                s.Status = Signal.SignalStatus.Approved;
            else
                s.Status = Signal.SignalStatus.NotApproved;

            s.UpdateDate = DateTime.Now;
            s.ResolutionDescription = string.Empty;
            sm.CreateSignal(s);

            SignalSubmissionEmail sse = new SignalSubmissionEmail();
            sse.Send(s);

            return s;
        }

        [JsonRpcMethod("getSignalsNearby")]
        public JsonArray GetSignalsNearby(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            JsonArray ar = new JsonArray();

            SignalManager sm = new SignalManager();
            List<Signal> ret = sm.SearchNearZip(param["zip"].ToString(), Convert.ToInt32(param["signalID"]));

            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].Email = string.Empty;

                JsonObject obj = new JsonObject();
                obj["signal"] = ret[i];
                obj["description"] = GetSignalDescription(ret[i]);
                ar.Push(obj);
            }

            return ar;
        }

        [JsonRpcMethod("searchSignals")]
        public JsonObject SearchSignals(JsonObject searchParams)
        {
            JsonArray ar = new JsonArray();
            JsonObject container = new JsonObject();
 
            int totalRecords = 0;

            SignalManager sm = new SignalManager();
            List<Signal> ret = (List<Signal>)sm.Search(searchParams["city"].ToString(), searchParams["address"].ToString(), searchParams["zip"].ToString(),
                Convert.ToInt32(searchParams["categoryID"]), Convert.ToInt32(searchParams["status"]), Convert.ToInt32(searchParams["start"]), out totalRecords);

            SignalsList s = (SignalsList)new UserControl().LoadControl("/Includes/SignalsList.ascx");
            s.Populate(ret, totalRecords, 10);
            container["html"] = WebUtils.RenderControlToString(s);
            
            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].Email = string.Empty;

                JsonObject obj = new JsonObject();
                obj["signal"] = ret[i];
                obj["description"] = GetSignalDescription(ret[i]);
                ar.Push(obj);
            }

            container["signals"] = ar;

            return container;
        }

        private string GetSignalDescription(Signal s)
        {
            Includes.SignalDetail sd = (Includes.SignalDetail)new UserControl().LoadControl("/Includes/SignalDetail.ascx");
            sd.BuildSignalDescription(s);
            return WebUtils.RenderControlToString(sd);
        }

        [JsonRpcMethod("getComments")]
        public JsonObject GetComments(JsonObject pars)
        {
            CheckRequest(pars["ajaxSessionKey"].ToString());

            JsonObject ret = new JsonObject();
            CommentManager cm = new CommentManager();
            int totalRecords = 0;
            List<Comment> comments = cm.GetComments(Convert.ToInt32(pars["signalID"]), Convert.ToInt32(pars["offset"]),
                out totalRecords);

            if (comments.Count > 0)
            {
                CommentsList s = (CommentsList)new UserControl().LoadControl("/Includes/CommentsList.ascx");
                s.Populate(comments, totalRecords);
                ret["count"] = totalRecords;
                ret["html"] = WebUtils.RenderControlToString(s);
            }
            else
                ret["count"] = 0;

            return ret;
        }

        [JsonRpcMethod("addComment")]
        public int AddComment(Comment c, string ajaxSessionKey)
        {
            CheckRequest(ajaxSessionKey);

            CommentManager cm = new CommentManager();
            c.CreationDate = DateTime.Now;
            c.Text = WebUtils.FormatHtml(c.Text);
            c.Status = Comment.CommentStatus.Approved;
            int ret = cm.AddComment(c);

            SignalAlertEmail sae = new SignalAlertEmail();
            sae.Send(c, c.SignalID);
            
            return ret;
        }

        [JsonRpcMethod("subscribeSignal")]
        public void SubscribeSignal(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            SignalManager sm = new SignalManager();
            SignalSubscription ss = new SignalSubscription();
            ss.Email = param["email"].ToString();
            ss.SignalID = Convert.ToInt32(param["signalID"]);

            if (!sm.CheckIfSubscribed(ss))
                sm.SubscribeSignal(ss);
            else
                throw new Exception("Sei già iscritto a questa segnalazione");
        }

        [JsonRpcMethod("addPlace")]
        public Place AddPlace(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            Place p = new Place();
            p.Open311ApiKey = string.Empty;
            p.Open311CityID = string.Empty;
            p.Open311URL = string.Empty;

            p.Latitude = Convert.ToDecimal(param["lat"]);
            p.Longitude = Convert.ToDecimal(param["lng"]);
            p.MapZoom = Convert.ToInt32(param["zoom"]);
            p.Name = param["name"].ToString();
            p.Status = false;
            PlaceManager pm = new PlaceManager();
            pm.CreatePlace(p);

            CitySubmissionEmail cse = new CitySubmissionEmail();
            cse.Send(param["name"].ToString(), param["email"].ToString());

            return p;
        }

        [JsonRpcMethod("subscribeToNewsletter")]
        public void SubscribeToNewsletter(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            NewsletterEntry user = new NewsletterEntry();
            user.Email = param["email"].ToString();
            NewsletterManager mg = new NewsletterManager();
            mg.SubscribeUser(user);
        }

        [JsonRpcMethod("sendFeedback")]
        public void SendFeedback(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            FeedbackEmail f = new FeedbackEmail();
            f.Send(param["name"].ToString(), param["email"].ToString(), param["message"].ToString());
        }

        [JsonRpcMethod("checkPlace")]
        public Place CheckPlace(string placeName, string sessionKey)
        {
            CheckRequest(sessionKey);

            PlaceManager pm = new PlaceManager();
            return pm.CheckPlace(placeName);
        }

        [JsonRpcMethod("changeSignalStatus")]
        public void ChangeSignalStatus(int signalID, int newStatus, string description, string sessionKey)
        {
            CheckRequest(sessionKey);

            SignalManager sm = new SignalManager();
            Signal s = sm.LoadSingnal(signalID);
            s.Status = newStatus;

            switch (newStatus)
            {
                case Signal.SignalStatus.ReOpened:
                    s.ReopenDate = DateTime.Now;
                    s.ReopenDescription = WebUtils.FormatHtml(description);
                    break;
                case Signal.SignalStatus.Resolved:
                    s.ResolutionDate = DateTime.Now;
                    s.ResolutionDescription = WebUtils.FormatHtml(description);
                    break;
            }

            sm.ChangeSignalStatus(s);

            SignalAlertEmail email = new SignalAlertEmail();
            email.Send(signalID);
        }

        [JsonRpcMethod("reportAbuse")]
        public void ReportAbuse(string message, string ajaxSessionKey)
        {
            CheckRequest(ajaxSessionKey);

            AbuseReportEmail email = new AbuseReportEmail();
            email.Send(this.Context.Request.UrlReferrer.AbsoluteUri.ToString(), WebUtils.FormatHtml(message));
        }
    }
}