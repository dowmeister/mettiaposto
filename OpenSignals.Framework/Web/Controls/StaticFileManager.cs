// QuickContacts.cs
using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenSignals.Framework.Core.Base;
using System.Web.SessionState;

namespace OpenSignals.Framework.Web.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), DefaultProperty("Files"),
    ParseChildren(true, "Files"), ToolboxData("<{0}:StaticFileManager runat=\"server\"> </{0}:StaticFileManager>")]
    public class StaticFileManager : WebControl
    {
        private string _contextKey = string.Empty;

        /// <summary>
        /// Gets or sets the context key.
        /// </summary>
        /// <value>
        /// The context key.
        /// </value>
        public string ContextKey
        {
            get { return _contextKey; }
            set { _contextKey = value; }
        }

        private bool _renderFiles = false;

        /// <summary>
        /// Gets or sets a value indicating whether [render files].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [render files]; otherwise, <c>false</c>.
        /// </value>
        public bool RenderFiles
        {
            get { return _renderFiles; }
            set { _renderFiles = value; }
        }

        private List<StaticFile> _files;

        /// <summary>
        /// Gets the files.
        /// </summary>
        [Category("Behavior"), Description("The contacts collection"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public List<StaticFile> Files
        {
            get
            {
                if (_files == null)
                {
                    _files = new List<StaticFile>();
                }
                return _files;
            }
        }


        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(
            HtmlTextWriter writer)
        {
            List<StaticFile> files = new List<StaticFile>();

            if (HttpContext.Current.Session[_contextKey] != null)
                files = (List<StaticFile>)HttpContext.Current.Session[_contextKey];

            files.AddRange(_files);

            HttpContext.Current.Session.Remove(_contextKey);
            HttpContext.Current.Session.Add(_contextKey, files);
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
    }

    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class StaticFile
    {
        private string url;
        private StaticFileType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFile"/> class.
        /// </summary>
        public StaticFile()
            : this(String.Empty, StaticFileType.Javascript)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFile"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="type">The type.</param>
        public StaticFile(string url, StaticFileType type)
        {
            this.url = url;
            this.type = type;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Category("Behavior"), DefaultValue(""), Description("Name of contact"), NotifyParentProperty(true)]
        public String Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Category("Behavior"), DefaultValue(""), Description("Email address of contact"), NotifyParentProperty(true)]
        public StaticFileType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum StaticFileType : int
    {
        /// <summary>
        /// 
        /// </summary>
        Css = 1,
        /// <summary>
        /// 
        /// </summary>
        Javascript = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public class StaticFileIncluder : BaseGenericHandler, IRequiresSessionState
    {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        ///   </returns>
        public override bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public override void ProcessRequest(HttpContext context)
        {
            string[] keys = context.Request.QueryString["key"].Split(',');

            StringBuilder sb = new StringBuilder();

            foreach (string k in keys)
            {
                List<StaticFile> files = (List<StaticFile>)context.Session[k];

                foreach (StaticFile f in files)
                {
                    using (StreamReader sr = File.OpenText(context.Server.MapPath(f.Url)))
                    {
                        sb.Append(sr.ReadToEnd());
                    }
                }
            }

            context.Response.ContentType = "application/javascript";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
}