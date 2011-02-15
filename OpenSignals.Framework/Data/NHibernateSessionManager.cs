using System.Web;
using NHibernate;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Web;

namespace OpenSignals.Framework.Data
{
    public class NHibernateSessionManager : BaseManager
    {
        protected ISessionFactory factory = null;
        protected ISession session = null;
        protected ITransaction transaction = null;

        protected void CreateSession()
        {
            if (factory == null)
            {
                if (HttpContext.Current == null)
                {
                    NHibernate.Cfg.Configuration conf = new NHibernate.Cfg.Configuration();
                    factory = conf.Configure().BuildSessionFactory();
                }
                else
                    factory = Global.SessionFactory;
            }
        }

        public void OpenSession()
        {
            if (factory == null)
                CreateSession();

            if (session == null)
                session = factory.OpenSession();
            else if (!session.IsOpen)
                session = factory.OpenSession();
        }

        public void CloseSession()
        {
            if (factory != null)
            {
                if (session.IsDirty())
                    session.Flush();

                if (session.IsOpen)
                {
                    factory.Close();
                    factory.Dispose();
                }
            }
        }

        protected void Flush()
        {
            session.Flush();
            CloseSession();
        }

        public void OpenTransaction()
        {
            if (session != null)
            {
                if (session.IsOpen)
                {
                    if (transaction != null)
                    {
                        if (!transaction.IsActive)
                            transaction = session.BeginTransaction();
                    }
                    else
                        transaction = session.BeginTransaction();
                }
            }
        }

        public void CommitTransaction()
        {
            if (session != null)
            {
                if (session.IsOpen)
                {
                    if (transaction != null)
                    {
                        if (transaction.IsActive)
                            transaction.Commit();
                    }
                }
            }
        }

        public void RollbackTransaction()
        {
            if (session != null)
            {
                if (session.IsOpen)
                {
                    if (transaction != null)
                    {
                        if (transaction.IsActive)
                            transaction.Rollback();
                    }
                }
            }
        }
    }
}