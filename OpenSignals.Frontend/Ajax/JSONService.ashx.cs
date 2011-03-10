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
            s.Description = s.Description.Replace("\n", "<br/>");
            s.CreationDate = DateTime.Now;
            s.UpdateDate = DateTime.Now;

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
        public string GetComments(JsonObject pars)
        {
            CheckRequest(pars["ajaxSessionKey"].ToString());

            string ret = string.Empty;
            CommentManager cm = new CommentManager();
            int totalRecords = 0;
            List<Comment> comments = cm.GetComments(Convert.ToInt32(pars["signalID"]), Convert.ToInt32(pars["offset"]),
                out totalRecords);

            CommentsList s = (CommentsList)new UserControl().LoadControl("/Includes/CommentsList.ascx");
            s.Populate(comments, totalRecords);
            ret = WebUtils.RenderControlToString(s);
            return ret;
        }

        [JsonRpcMethod("addComment")]
        public int AddComment(Comment c, string ajaxSessionKey)
        {
            CheckRequest(ajaxSessionKey);

            CommentManager cm = new CommentManager();
            c.CreationDate = DateTime.Now;
            c.Text = c.Text.Replace("\n", "<br/>");
            c.Status = Comment.CommentStatus.Approved;
            int ret = cm.AddComment(c);

            if (c.SetSignalResolved)
            {
                SignalManager sm = new SignalManager();
                sm.ResolveSignal(c.SignalID, c.Text);
            }

            SignalAlertEmail sae = new SignalAlertEmail();
            sae.Send(c);
            
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

            if (sm.CheckIfSubscribed(ss))
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

            return p;
        }
    }
}