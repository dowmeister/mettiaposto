using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jayrock.Json;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Core.Base;
using System.IO;

namespace OpenSignals.Framework.Web.Services
{
    /// <summary>
    /// Summary description for Upload1
    /// </summary>
    public class UploadService : BaseHandler
    {
        string[] permittedExts = new string[] { ".jpg", ".png", ".gif", ".bmp" };

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            CheckRequest(context.Request.QueryString["token"]);

            context.Response.ContentType = "text/plain";

            if (context.Request.Files["fuFile"] != null)
            {
                JsonObject ret = new JsonObject();

                try
                {
                    HttpPostedFile file = context.Request.Files["fuFile"];

                    if (file != null)
                    {
                        if (permittedExts.Contains(Path.GetExtension(file.FileName).ToLower()))
                        {
                            SignalManager sm = new SignalManager();
                            ret["fileName"] = sm.UploadAttachment(file);
                        }
                        else
                        {
                            ret["error"] = "WRONG_EXT";
                        }
                    }
                    else
                        ret["error"] = "NO_FILE";
                }
                catch (Exception ex)
                {
                    ret["error"] = "ERROR";
                    ret["errorMessage"] = ex.Message;
                    ret["stackTrace"] = ex.StackTrace;
                }

                context.Response.Write(ret.ToString());
            }
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        ///   </returns>
        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}