using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.API.Open311;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend
{
    public partial class Open311Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service s = new Service();
            s.Description = "XXXX";
            ServiceResponse ss = new ServiceResponse();
            ss.Services.Add(s);
            Response.Write(XmlUtils.Serialize(ss).InnerXml);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Open311V2Client client = new Open311V2Client("https://open311.sfgov.org/dev/v2/", string.Empty, "sfgov.org");
            client.Proxy = new System.Net.WebProxy("proxy.reply.it", 8080);
            client.Credentials = new System.Net.NetworkCredential("f.bramato", "sticazzi1!", "REPLYNET");
            client.GetServices();
        }
    }
}