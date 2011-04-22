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

using NHibernate;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Framework.Data
{
    /// <summary>
    /// This class manage NHibernate session, db access, transactions
    /// </summary>
    public class NHibernateSessionManager : BaseManager
    {
        /// <summary>
        /// Gets the session.
        /// </summary>
        public ISession Session { get { return NHibernateSession.Current.Session; } }

        /// <summary>
        /// Transaction object
        /// </summary>
        protected ITransaction transaction = null;

        /// <summary>
        /// Opens the NHibernate session.
        /// </summary>
        public void OpenSession()
        {
            if (NHibernateSession.Current.Session == null)
                NHibernateSession.Current.Session = NHibernateSession.Current.Factory.OpenSession();
            else if (!NHibernateSession.Current.Session.IsOpen)
                NHibernateSession.Current.Session = NHibernateSession.Current.Factory.OpenSession();
        }

        /// <summary>
        /// Closes the NHibernate session.
        /// </summary>
        public void CloseSession()
        {
            NHibernateSession.Current.Session.Flush();

            if (NHibernateSession.Current.Session.IsOpen)
            {
                NHibernateSession.Current.Factory.Close();
                NHibernateSession.Current.Factory.Dispose();
            }
        }

        /// <summary>
        /// Flushes the NHibernate session.
        /// </summary>
        protected void Flush()
        {
            if (NHibernateSession.Current.Factory != null)
                NHibernateSession.Current.Session.Flush();

            CloseSession();
        }

        /// <summary>
        /// Opens the NHibernate transaction.
        /// </summary>
        public void OpenTransaction()
        {
            if (NHibernateSession.Current.Session != null)
            {
                if (NHibernateSession.Current.Session.IsOpen)
                {
                    if (transaction != null)
                    {
                        if (!transaction.IsActive)
                            transaction = NHibernateSession.Current.Session.BeginTransaction();
                    }
                    else
                        transaction = NHibernateSession.Current.Session.BeginTransaction();
                }
            }
        }

        /// <summary>
        /// Commits the NHibernate transaction.
        /// </summary>
        public void CommitTransaction()
        {
            if (NHibernateSession.Current.Session != null)
            {
                if (NHibernateSession.Current.Session.IsOpen)
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
        /// Rollbacks the NHibernate transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (NHibernateSession.Current.Session != null)
            {
                if (NHibernateSession.Current.Session.IsOpen)
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