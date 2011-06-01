using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elmah;
using System.Web;

namespace OpenSignals.Framework.Core.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class LogUtils
    {
        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void Log(Exception ex)
        {
            if (ex is Exception)
                ErrorSignal.FromCurrentContext().Raise(new OpenSignalsException(ex));
            else
                ErrorSignal.FromCurrentContext().Raise(ex);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Log(string message, Exception ex)
        {
            Log(new OpenSignalsException(message, ex));
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        public static void Log(string message, HttpContext context, Exception ex)
        {
            Log(context, new OpenSignalsException(message, ex));
        }

        /// <summary>
        /// Logs the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        public static void Log(HttpContext context, Exception ex)
        {
            if (ex is Exception)
                ErrorSignal.FromContext(context).Raise(new OpenSignalsException(ex));
            else
                ErrorSignal.FromContext(context).Raise(ex);
        }
    }
}
