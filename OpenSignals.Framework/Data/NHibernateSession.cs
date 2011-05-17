using NHibernate;
using NHibernate.Cfg;

namespace OpenSignals.Framework.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class NHibernateSession
    {
        private static NHibernateSession _istance = new NHibernateSession();
        private ISessionFactory factory = null;
        private ISession session = null;

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public ISessionFactory Factory
        {
            get { return factory; }
            set { factory = value; }
        }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public ISession Session
        {
            get { return session; }
            set { session = value; }
        }

        /// <summary>
        /// Gets the istance
        /// </summary>
        public static NHibernateSession Current
        {
            get
            {
                if (_istance == null)
                    _istance = new NHibernateSession();

                return _istance;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="NHibernateSession"/> class from being created.
        /// </summary>
        private NHibernateSession()
        {
            Configuration config = new Configuration().Configure();
            factory = config.BuildSessionFactory();
        }
    }
}
