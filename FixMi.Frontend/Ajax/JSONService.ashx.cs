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

namespace FixMi.Frontend.Ajax
{
    /// <summary>
    /// Summary description for JSON
    /// </summary>
    public class JSONService : JsonRpcHandler, IRequiresSessionState
    {
        [JsonRpcMethod("addSignal")]
        public Signal AddSignal(Signal s)
        {
            SignalManager sm = new SignalManager();
            s.CreationDate = DateTime.Now;
            s.UpdateDate = DateTime.Now;
            s.Status = Signal.SignalStatus.Approved; // FIX!!!!
            s.UpdateDate = DateTime.Now;
            s.ResolutionDescription = string.Empty;
            sm.CreateSignal(s);

            return s;
        }

        [JsonRpcMethod("getSignalsNearby")]
        public JsonArray GetSignalsNearby(string zip)
        {
            JsonArray ar = new JsonArray();

            SignalManager sm = new SignalManager();
            List<Signal> ret = sm.SearchNearZip(zip);

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

            SingleSignal s = (SingleSignal)new UserControl().LoadControl("/Includes/SingleSignal.ascx");
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
            CategoryManager cm = new CategoryManager();
            string categoryName = cm.Load(s.CategoryID).Name;
            StringBuilder sb = new StringBuilder();
            sb.Append(s.Subject);
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("Inviato ");
            sb.Append(SignalUtils.GetTimeframe(s.CreationDate));
            sb.Append(" ");
            sb.Append("nella categoria '");
            sb.Append(categoryName);
            sb.Append("'");
            sb.Append(" - ");
            sb.Append(s.Address);

            return sb.ToString();
        }
    }
}