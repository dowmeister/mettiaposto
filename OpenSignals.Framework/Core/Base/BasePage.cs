// Copyright (C) 2010-2011 Francesco 'ShArDiCk' Bramato

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Threading;
using System.Web;
using log4net;
using OpenSignals.Framework.Places;
using Jayrock.Json;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// This class represents the base class for Web Pages, implements common methods and utilities to be used in asp.net pages
    /// </summary>
    public class BasePage : System.Web.UI.Page
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

        private Place _currentCity = null;

        /// <summary>
        /// Gets the current city.
        /// </summary>
        /// <value>
        /// The current city.
        /// </value>
        public Place CurrentCity
        {
            get { return _currentCity; }
        }        

        /// <summary>
        /// Gets from request.
        /// </summary>
        /// <param name="szControl">The sz control.</param>
        /// <returns></returns>
        protected virtual string GetFromRequest(string szControl)
        {
            if (Request[szControl] != null)
            {
                return Request[szControl];
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Check if request the contains file.
        /// </summary>
        /// <param name="szControl">The control.</param>
        /// <returns>True if request contains file otherwise false</returns>
        protected bool RequestContainsFile(string szControl)
        {
            if (Request.Files[szControl] != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the file from request.
        /// </summary>
        /// <param name="szControl">The html control.</param>
        /// <returns>HttpPostedFile object</returns>
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

        /// <summary>
        /// Gets the check value from request.
        /// </summary>
        /// <param name="szControl">The sz control.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sessions the contains.
        /// </summary>
        /// <param name="szName">Name of the sz.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets from cookie.
        /// </summary>
        /// <param name="szCookieName">Name of the sz cookie.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds to cookie.
        /// </summary>
        /// <param name="szName">Name of the sz.</param>
        /// <param name="objObject">The obj object.</param>
        /// <param name="dtExpire">The dt expire.</param>
        protected void AddToCookie(string szName, object objObject, DateTime dtExpire)
        {
            HttpCookie objCookie = new HttpCookie(szName, objObject.ToString());
            objCookie.Expires = dtExpire;
            Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// Adds to cookie.
        /// </summary>
        /// <param name="szName">Name of the sz.</param>
        /// <param name="objObject">The obj object.</param>
        /// <param name="dtExpire">The dt expire.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        protected void AddToCookie(string szName, object objObject, DateTime dtExpire, bool isSecure)
        {
            HttpCookie objCookie = new HttpCookie(szName, objObject.ToString());
            objCookie.Expires = dtExpire;
            objCookie.Secure = isSecure;
            Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// Removes from cookie.
        /// </summary>
        /// <param name="szName">Name of the sz.</param>
        protected void RemoveFromCookie(string szName)
        {
            Response.Cookies.Remove(szName);
        }

        /// <summary>
        /// Cookies the contains.
        /// </summary>
        /// <param name="szName">Name of the sz.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Alerts the specified sz message.
        /// </summary>
        /// <param name="szMessage">The sz message.</param>
        /// <param name="bGoBack">if set to <c>true</c> [b go back].</param>
        /// <returns></returns>
        protected string Alert(string szMessage, bool bGoBack)
        {
            return "<script language='javascript'>alert('" + szMessage.Replace("'", "\\'") + "');history.back();</script>";
        }

        /// <summary>
        /// Registers the document ready function.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="function">The function.</param>
        public void RegisterDocumentReadyFunction(string key, string function)
        {
            string docReady = "$(document).ready(function() { " + function + " });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "docReady_" + key, docReady, true);
        }

        /// <summary>
        /// Registers the startup script.
        /// </summary>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="script">The script.</param>
        protected new void RegisterStartupScript(string scriptName, string script)
        {
            ClientScript.RegisterStartupScript(this.GetType(), scriptName, script, true);
        }

        /// <summary>
        /// Gets the option value from request.
        /// </summary>
        /// <param name="szGroupName">Name of the sz group.</param>
        /// <param name="szOptionId">The sz option id.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the option value from request.
        /// </summary>
        /// <param name="szGroupName">Name of the sz group.</param>
        /// <returns></returns>
        protected virtual string GetOptionValueFromRequest(string szGroupName)
        {
            if (Request[szGroupName] != null)
                return GetFromRequest(szGroupName);
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns></returns>
        protected string GetVersion()
        {
            //return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision.ToString();
            return Session.SessionID;
        }

        /// <summary>
        /// Determines whether [is thread abort exception] [the specified ex].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>
        ///   <c>true</c> if [is thread abort exception] [the specified ex]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsThreadAbortException(Exception ex)
        {
            return ex is ThreadAbortException;
        }

        /// <summary>
        /// Gets the event arg.
        /// </summary>
        /// <returns></returns>
        protected object GetEventArg()
        {
            return Request.Form["__EVENTARGUMENT"];
        }

        /// <summary>
        /// Registers the ajax session key.
        /// </summary>
        protected void RegisterAjaxSessionKey()
        {
            string ajaxSessionKey = Guid.NewGuid().ToString();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ajaxSessionKey", "ajaxSessionKey='" + ajaxSessionKey + "';", true);
            AddToSession("AjaxSessionKey", ajaxSessionKey);
        }

        /// <summary>
        /// Gets the current city.
        /// </summary>
        protected void GetCurrentCity()
        {
            if (QueryStringContains("city"))
            {
                PlaceManager pm = new PlaceManager();
                _currentCity = pm.LoadPlace(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city")));
                JsonObject o = new JsonObject();
                o["name"] = _currentCity.Name;
                o["link"] = _currentCity.Link;
                o["id"] = _currentCity.ID;
                o["lat"] = _currentCity.Latitude;
                o["lng"] = _currentCity.Longitude;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "currentCity", "currentCity = " + o.ToString(), true);
            }
        }
    }
}
