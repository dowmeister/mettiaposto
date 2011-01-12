using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jayrock.JsonRpc.Web;
using System.Web.SessionState;
using Jayrock.Json;
using Jayrock.JsonRpc;
using FixMi.Framework.Signals;
using FixMi.Framework.Categories;
using System.Text;
using FixMi.Frontend.Includes;
using FixMi.Framework.Core.Utility;
using System.Web.UI;
using FixMi.Framework.Comments;
using System.Web.UI.WebControls;

namespace FixMi.Frontend.Ajax
{
    /// <summary>
    /// Summary description for JSON
    /// </summary>
    public class JSONService : JsonRpcHandler, IRequiresSessionState
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
            s.Status = Signal.SignalStatus.Approved; // FIX!!!!
            s.UpdateDate = DateTime.Now;
            s.ResolutionDescription = string.Empty;
            sm.CreateSignal(s);

            return s;
        }

        [JsonRpcMethod("getSignalsNearby")]
        public JsonArray GetSignalsNearby(JsonObject param)
        {
            CheckRequest(param["ajaxSessionKey"].ToString());

            JsonArray ar = new JsonArray();

            SignalManager sm = new SignalManager();
            List<Signal> ret = sm.SearchNearZip(param["zip"].ToString());

            for (int i = 0; i < ret.Count; i++)
            {
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
            //CategoryManager cm = new CategoryManager();
            //string categoryName = cm.Load(s.CategoryID).Name;
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<div id=\"");
            //sb.Append("infoWindowContainer");
            //sb.Append("\"");
            //sb.Append(s.Subject);
            //sb.Append("<br/>");
            //sb.Append("<br/>");
            //sb.Append("Inviato ");
            //sb.Append(SignalUtils.GetTimeframe(s.CreationDate));
            //sb.Append(" ");
            //sb.Append("nella categoria '");
            //sb.Append(categoryName);
            //sb.Append("'");
            //sb.Append(" - ");
            //sb.Append(s.Address);
            //sb.Append("</div>");

            //return sb.ToString();
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
    }
}