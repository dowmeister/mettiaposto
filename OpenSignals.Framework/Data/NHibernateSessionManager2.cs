using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernate;
using NHibernate.Cache;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernateConfiguration = NHibernate.Cfg;

namespace OpenSignals.Framework.Data2
{
    /// <summary>
    /// Handles creation and management of sessions and transactions.  It is a singleton because
    /// building the initial session factory is very expensive. Inspiration for this class came
    /// from Chapter 8 of Hibernate in Action by Bauer and King.  Although it is a sealed singleton
    /// you can use TypeMock (http://www.typemock.com) for more flexible testing.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        private ISessionFactory sessionFactory;
        private ITransaction threadTransaction;
        private ISession threadSession;

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSessionManager Instance
        {
            get { return Nested.nHibernateNHibernateSessionManager; }
        }

        /// <summary>
        /// Initializes the NHibernate session factory upon instantiation.
        /// </summary>
        private NHibernateSessionManager()
        {
            InitSessionFactory();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly NHibernateSessionManager nHibernateNHibernateSessionManager = new NHibernateSessionManager();
        }

        #endregion

        private void InitSessionFactory()
        {
            Configuration cfg = new Configuration();
            sessionFactory = cfg.BuildSessionFactory();
        }

        /// <summary>
        /// Allows you to register an interceptor on a new session.  This may not be called if there is already
        /// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        /// the HttpModule to call this before calling BeginTransaction().
        /// </summary>
        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = threadSession;

            if (session != null && session.IsOpen)
            {
                throw new CacheException("You cannot register an interceptor once a session has already been opened");
            }

            GetSession(interceptor);
        }

        public ISession GetSession()
        {
            return GetSession(null);
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession GetSession(IInterceptor interceptor)
        {
            ISession session = threadSession;

            if (session == null)
            {
                if (interceptor != null)
                {
                    session = sessionFactory.OpenSession(interceptor);
                }
                else
                {
                    session = sessionFactory.OpenSession();
                }

                threadSession = session;
            }

            return session;
        }

        public void CloseSession()
        {
            ISession session = threadSession;
            threadSession = null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }
        }

        public void BeginTransaction()
        {
            ITransaction transaction = threadTransaction;

            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                threadTransaction = transaction;
            }
        }

        public void CommitTransaction()
        {
            ITransaction transaction = threadTransaction;

            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Commit();
                    threadTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        public void RollbackTransaction()
        {
            ITransaction transaction = threadTransaction;

            try
            {
                threadTransaction = null;

                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                {
                    transaction.Rollback();
                }
            }
            catch (HibernateException ex)
            {
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// If within a web context, this uses <see cref=”HttpContext” /> instead of the WinForms
        /// specific <see cref=”CallContext” />.  Discussion concerning this found at
        /// http://forum.springframework.net/showthread.php?t=572.
        /// </summary>
        private ITransaction ThreadTransaction
        {
            get
            {
                if (IsInWebContext())
                {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTION_KEY];
                }
                else
                {
                    return (ITransaction)CallContext.GetData(TRANSACTION_KEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[TRANSACTION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(TRANSACTION_KEY, value);
                }
            }
        }

        /// <summary>
        /// If within a web context, this uses <see cref=”HttpContext” /> instead of the WinForms
        /// specific <see cref=”CallContext” />.  Discussion concerning this found at
        /// http://forum.springframework.net/showthread.php?t=572.
        /// </summary>
        private ISession ThreadSession
        {
            get
            {
                if (IsInWebContext())
                {
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                }
                else
                {
                    return (ISession)CallContext.GetData(SESSION_KEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(SESSION_KEY, value);
                }
            }
        }

        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";
    }
}