using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Framework.API.Google
{
    /// <summary>
    /// 
    /// </summary>
    public class Geocoder
    {
        /// <summary>
        /// Geocodes the by address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public GeocodeResponse GeocodeByAddress(string address)
        {
            WebClient wc = new WebClient();
            //wc.Proxy = new WebProxy("http://proxy.reply.it:8080", true, null, new NetworkCredential("f.bramato", "Reply.05!", "replynet"));
            string r = wc.DownloadString("https://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=true&region=it&language=it");
            return (GeocodeResponse)XmlUtils.Deserialize(r, typeof(GeocodeResponse));
        }
    }
}
