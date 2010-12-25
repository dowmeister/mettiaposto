using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Web;
using System.Threading;

namespace FixMi.Framework.Core.Base
{
    public class BasePage : System.Web.UI.Page
    {
        protected ILog _logger = null;

        protected ILog log
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger("System");
                return _logger;
            }
        }

        protected virtual string GetFromRequest(string szControl)
        {
            if (Request[szControl] != null)
            {
                return Request[szControl];
            }
            else
                return string.Empty;
        }

        protected bool RequestContainsFile(string szControl)
        {
            if (Request.Files[szControl] != null)
                return true;
            else
                return false;
        }

        protected HttpPostedFile GetFileFromRequest(string szControl)
        {
            if (RequestContainsFile(szControl))
            {
                if (Request.Files[szControl].FileName.Equals(string.Empty))
                    return null;
                else
                    return Request.Files[szControl];
            }
            else
                return null;
        }

        protected virtual bool GetCheckValueFromRequest(string szControl)
        {
            if (Request[szControl] != null)
            {
                if (Request[szControl].Equals("on"))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Check if a parameters is contained into query string
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <returns>True if contained false if not</returns>
        protected bool QueryStringContains(string szName)
        {
            if (Request.QueryString[szName] != null)
            {
                if (!Request.QueryString[szName].Equals(string.Empty))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get a parameter value from query string
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <returns></returns>
        protected string GetFromQueryString(string szName)
        {
            if (QueryStringContains(szName))
                return HttpUtility.UrlDecode(Request.QueryString[szName]);
            else
                return string.Empty;
        }

        protected bool SessionContains(string szName)
        {
            if (Session != null)
            {
                if (Session[szName] != null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get a value from session
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <returns></returns>
        protected object GetFromSession(string szName)
        {
            if (SessionContains(szName))
                return Session[szName];
            else
                return null;
        }

        /// <summary>
        /// Add a object to session, and if already exists remove it before adding
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <param name="objObject">Param value</param>
        protected void AddToSession(string szName, object objObject)
        {
            if (SessionContains(szName))
            {
                Session.Remove(szName);
                Session.Add(szName, objObject);
            }
            else
                Session.Add(szName, objObject);
        }

        protected string GetFromCookie(string szCookieName)
        {
            if (CookieContains(szCookieName))
                return Request.Cookies[szCookieName].Value;
            else
                return string.Empty;
        }

        /// <summary>
        /// Remove a object from session
        /// </summary>
        /// <param name="szName">Object name</param>
        protected void RemoveFromSession(string szName)
        {
            if (SessionContains(szName))
                Session.Remove(szName);
        }

        /// <summary>
        /// Check if context object contains the input key
        /// </summary>
        /// <param name="szName">Key name</param>
        /// <returns>True if exists, false if not</returns>
        protected bool ContextContains(string szName)
        {
            if (Context != null)
            {
                if (Context.Items.Contains(szName))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check if request object contains the input key
        /// </summary>
        /// <param name="szName">Key name</param>
        /// <returns>True if exists, false if not</returns>
        protected bool RequestContains(string szName)
        {
            if (!GetFromRequest(szName).Equals(string.Empty))
                return true;
            return false;
        }

        /// <summary>
        /// Get a parameter from context object
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <returns></returns>
        protected object GetFromContext(string szName)
        {
            if (ContextContains(szName))
                return Context.Items[szName];
            else
                return null;
        }

        /// <summary>
        /// Add a parameter into context object
        /// </summary>
        /// <param name="szName">Param name</param>
        /// <param name="objObject">Param value</param>
        protected void AddToContext(string szName, object objObject)
        {
            if (ContextContains(szName))
            {
                Context.Items.Remove(szName);
                Context.Items.Add(szName, objObject);
            }
            else
                Context.Items.Add(szName, objObject);
        }

        protected void AddToCookie(string szName, object objObject, DateTime dtExpire)
        {
            HttpCookie objCookie = new HttpCookie(szName, objObject.ToString());
            objCookie.Expires = dtExpire;
            Response.Cookies.Add(objCookie);
        }

        protected void AddToCookie(string szName, object objObject, DateTime dtExpire, bool isSecure)
        {
            HttpCookie objCookie = new HttpCookie(szName, objObject.ToString());
            objCookie.Expires = dtExpire;
            objCookie.Secure = isSecure;
            Response.Cookies.Add(objCookie);
        }

        protected void RemoveFromCookie(string szName)
        {
            Response.Cookies.Remove(szName);
        }

        protected bool CookieContains(string szName)
        {
            if (Request.Cookies[szName] != null)
            {
                if (Request.Cookies[szName].Value.Equals(string.Empty))
                    return false;
                else
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Remove the input parameter from context
        /// </summary>
        /// <param name="szName">Param name</param>
        protected void RemoveFromContext(string szName)
        {
            if (ContextContains(szName))
                Context.Items.Remove(szName);
        }

        /// <summary>
        /// Alert a message
        /// </summary>
        /// <param name="szMessage">Message to show</param>
        protected void Alert(string szMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + szMessage.Replace("'", "\\'") + "');", true);
        }

        protected string Alert(string szMessage, bool bGoBack)
        {
            return "<script language='javascript'>alert('" + szMessage.Replace("'", "\\'") + "');history.back();</script>";
        }

        public void RegisterDocumentReadyFunction(string key, string function)
        {
            string docReady = "$(document).ready(function() { " + function + " });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "docReady_" + key, docReady, true);
        }

        protected new void RegisterStartupScript(string scriptName, string script)
        {
            ClientScript.RegisterStartupScript(this.GetType(), scriptName, script, true);
        }

        protected virtual bool GetOptionValueFromRequest(string szGroupName, string szOptionId)
        {
            if (Request[szGroupName] != null)
            {
                string szCheckedOption = GetFromRequest(szGroupName);
                if (szCheckedOption.Equals(szOptionId))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected virtual string GetOptionValueFromRequest(string szGroupName)
        {
            if (Request[szGroupName] != null)
                return GetFromRequest(szGroupName);
            else
                return string.Empty;
        }

        protected string GetVersion()
        {
            //return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision.ToString();
            return Session.SessionID;
        }

        protected bool IsThreadAbortException(Exception ex)
        {
            return ex is ThreadAbortException;
        }

        protected object GetEventArg()
        {
            return Request.Form["__EVENTARGUMENT"];
        }

        protected void RegisterAjaxSessionKey()
        {
            string ajaxSessionKey = Guid.NewGuid().ToString();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ajaxSessionKey", "ajaxSessionKey='" + ajaxSessionKey + "';", true);
            AddToSession("AjaxSessionKey", ajaxSessionKey);
        }
    }
}
