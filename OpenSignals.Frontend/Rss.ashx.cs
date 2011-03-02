using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Web;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Frontend
{
    /// <summary>
    /// Summary description for Rss
    /// </summary>
    public class Rss : BaseGenericHandler
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            try
            {
                context.Response.ContentType = "text/xml";
                RssGenerator rss = new RssGenerator();
                rss.Channel = new Channel();
                rss.Channel.Title = "Mettiaposto.it - Feed " + context.Request.QueryString["city"];
                rss.Channel.Link = "http://" + context.Request.Url.Host;
                rss.Channel.PubDate = DateTime.Now.ToString("s");
                rss.Channel.Description = "Elenco delle segnalazioni inviate tramite Mettiaposto.it per la città di " + context.Request.QueryString["city"];
                rss.Channel.Image = new Image();
                rss.Channel.Image.Url = "http://" + context.Request.Url.Host + "/images/logo.png";
                rss.Channel.Items = new List<Item>();

                SignalManager sm = new SignalManager();
                int tot;
                List<Signal> signals = sm.Search(context.Request.QueryString["city"], string.Empty, string.Empty, -1, -1, 0, out tot);

                foreach (Signal s in signals)
                {
                    Item item = new Item();
                    item.Title = s.Subject;
                    item.Description = s.Description;
                    item.PubDate = s.CreationDate.ToString("s");
                    item.Link = "http://" + context.Request.Url.Host + s.Link;
                    item.Guid = "http://" + context.Request.Url.Host + s.Link;
                    item.Commments = "http://" + context.Request.Url.Host + s.Link + "#comments";
                    if (s.HasImage)
                    {
                        item.Image = new Image();
                        item.Image.Url = WebUtils.GetImageUrl(UploadPaths.Comments, s.Attachment);
                    }

                    rss.Channel.Items.Add(item);
                }

                context.Response.Write(XmlUtils.Serialize(rss).OuterXml);
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading or creating RSS", ex);
            }
        }
    }
}