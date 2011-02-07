using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using OpenSignals.Framework.Core;

namespace OpenSignals.Framework.Web
{
    public class Global : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            var config = new Configuration();
            config.Configure();
            SessionFactory = config.BuildSessionFactory();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            RewriteManager.RewriteUrl();
        }
    }
}
