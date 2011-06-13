using System.Web;
using System.Web.SessionState;
using Jayrock.JsonRpc.Web;
using Elmah;
using System;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseGenericHandler : IHttpHandler
    {
        protected HttpContext currentContext = null;

        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        ///   </returns>
        public virtual bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public virtual void ProcessRequest(HttpContext context)
        {
            currentContext = context;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 
        /// </summary>
        protected HttpContext currentContext = null;

        /// <summary>
        /// Checks the request.
        /// </summary>
        /// <param name="key">The key.</param>
        protected void CheckRequest(string key)
        {
            if (key != null)
            {
                if (currentContext.Request.UrlReferrer != null)
                {
                    if (currentContext.Request.UrlReferrer.Host.Equals(currentContext.Request.Url.Host))
                    {
                        if (currentContext.Session["AjaxSessionKey"] != null)
                        {
                            if (!key.Equals(currentContext.Session["AjaxSessionKey"].ToString()))
                                throw new Exception("Richiesta AJAX non autorizzata");
                        }
                        else
                            throw new Exception("Richiesta AJAX non autorizzata");
                    }
                    else
                        throw new Exception("Richiesta AJAX negata");
                }
                else
                    throw new Exception("No referer");
            }
            else
                throw new Exception("No key");
        }

        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        ///   </returns>
        public virtual bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public virtual void ProcessRequest(HttpContext context)
        {
            this.currentContext = context; 
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseJSONHandler : JsonRpcHandler, IRequiresSessionState 
    {
        /// <summary>
        /// Checks the request.
        /// </summary>
        /// <param name="key">The key.</param>
        protected void CheckRequest(string key)
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
    }
}