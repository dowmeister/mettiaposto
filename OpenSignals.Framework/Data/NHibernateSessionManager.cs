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

using System.Web;
using NHibernate;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Web;

namespace OpenSignals.Framework.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class NHibernateSessionManager : BaseManager
    {
        /// <summary>
        /// 
        /// </summary>
        protected ISessionFactory factory = null;
        /// <summary>
        /// 
        /// </summary>
        protected ISession session = null;
        /// <summary>
        /// 
        /// </summary>
        protected ITransaction transaction = null;

        /// <summary>
        /// Creates the session.
        /// </summary>
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

        /// <summary>
        /// Opens the session.
        /// </summary>
        public void OpenSession()
        {
            if (factory == null)
                CreateSession();

            if (session == null)
                session = factory.OpenSession();
            else if (!session.IsOpen)
                session = factory.OpenSession();
        }

        /// <summary>
        /// Closes the session.
        /// </summary>
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

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        protected void Flush()
        {
            session.Flush();
            CloseSession();
        }

        /// <summary>
        /// Opens the transaction.
        /// </summary>
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

        /// <summary>
        /// Commits the transaction.
        /// </summary>
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

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
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