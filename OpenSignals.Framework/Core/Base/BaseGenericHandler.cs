using System.Web;
using System.Web.SessionState;
using Jayrock.JsonRpc.Web;
using log4net;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseGenericHandler : IHttpHandler
    {
        /// <summary>
        /// Logger class
        /// </summary>
        protected ILog _logger = null;

        /// <summary>
        /// Gets the log.
        /// </summary>
        protected ILog log
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger("System");
                return _logger;
            }
        }

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
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseJSONHandler : JsonRpcHandler, IRequiresSessionState
    {
        /// <summary>
        /// Logger class
        /// </summary>
        protected ILog _logger = null;

        /// <summary>
        /// Gets the log.
        /// </summary>
        protected ILog log
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger("System");
                return _logger;
            }
        }
    }
}