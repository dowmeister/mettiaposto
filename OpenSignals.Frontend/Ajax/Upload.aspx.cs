using System;
using System.IO;
using System.Web;
using Jayrock.Json;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Frontend.Ajax
{
    public partial class Upload : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestContainsFile("fuFile"))
            {
                JsonObject ret = new JsonObject();

                try
                {
                    SignalManager sm = new SignalManager();
                    ret["fileName"] = sm.UploadAttachment(GetFileFromRequest("fuFile"));
                }
                catch (Exception ex)
                {
                    ret["error"] = ex.Message;
                }

                Response.Write(ret.ToString());
            }
        }
    }
}