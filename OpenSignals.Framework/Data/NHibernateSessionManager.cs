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
using NHibernate.Cfg;

namespace OpenSignals.Framework.Data
{
    /// <summary>
    /// This class manage NHibernate session, db access, transactions
    /// </summary>
    public class NHibernateSessionManager : BaseManager
    {
        private ISessionFactory _sessionFactory =  NHibernateSession.Current.Factory;
        /// <summary>
        /// 
        /// </summary>
        protected ISession _session = NHibernateSession.Current.Session;
        /// <summary>
        /// Gets the session.
        /// </summary>
        public ISession Session { get { return _session; } }

        /// <summary>
        /// Transaction object
        /// </summary>
        protected ITransaction _transaction = null;

        /// <summary>
        /// Opens the NHibernate session.
        /// </summary>
        public void OpenSession()
        {
            _session = _sessionFactory.OpenSession();
        }

        /// <summary>
        /// Closes the NHibernate session.
        /// </summary>
        public void CloseSession()
        {
            CheckSession();

            _session.Close();
        }

        /// <summary>
        /// Flushes the NHibernate session.
        /// </summary>
        protected void FlushSession()
        {
            _session.Flush();
            _session.Close();
        }

        /// <summary>
        /// Opens the NHibernate transaction.
        /// </summary>
        public void OpenTransaction()
        {
            this.CheckSession();

            if (_transaction == null)
                _session.BeginTransaction();
        }

        private void CheckSession()
        {
            if (_session == null)
                this.OpenSession();

            if (_session != null && !_session.IsOpen)
                this.OpenSession();
        }

        /// <summary>
        /// Commits the NHibernate transaction.
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction != null && !_transaction.WasCommitted && !_transaction.WasRolledBack)
            {
                _transaction.Commit();
            }

            _transaction = null;
        }

        /// <summary>
        /// Rollbacks the NHibernate transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction != null && !_transaction.WasCommitted && !_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }

            _transaction = null;
        }
    }
}