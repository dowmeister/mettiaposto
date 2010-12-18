using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jayrock.JsonRpc.Web;
using System.Web.SessionState;
using Jayrock.Json;
using Jayrock.JsonRpc;
using FixMi.Framework.Signals;

namespace FixMi.Frontend.Ajax
{
    /// <summary>
    /// Summary description for JSON
    /// </summary>
    public class JSONService : JsonRpcHandler, IRequiresSessionState
    {
        [JsonRpcMethod("addSignal")]
        public int AddSignal(Signal s)
        {
            SignalManager sm = new SignalManager();
            s.CreationDate = DateTime.Now;
            s.UpdateDate = DateTime.Now;
            s.Status = Signal.SignalStatus.NotApproved;
            s.UpdateDate = DateTime.Now;
            s.ResolutionDescription = string.Empty;
            sm.CreateSignal(s);

            return s.SignalID;
        }
    }
}