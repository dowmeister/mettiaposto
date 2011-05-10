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
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Signals
{
    /// <summary>
    /// This class manage signal operations and signal subscriptions
    /// </summary>
    public class SignalManager : NHibernateSessionManager
    {
        /// <summary>
        /// Loads the singnal.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Signal LoadSingnal(int id)
        {
            OpenSession();
            Signal ret = (Signal)Session.Load(typeof(Signal), id);
            CloseSession();
            return ret;
        }

        /// <summary>
        /// Creates the signal.
        /// </summary>
        /// <param name="s">The s.</param>
        public void CreateSignal(Signal s)
        {
            try
            {
                OpenSession();
                OpenTransaction();
                Session.Save(s);
                CommitTransaction();
                CloseSession();
            }
            catch (Exception ex)
            {
                log.Error("Error creating signal", ex);
                RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Searches signals nearby using the zip code
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="signalID">The signal ID.</param>
        /// <returns>Signal collection</returns>
        public List<Signal> SearchNearZip(string zipCode, int signalID)
        {
            try
            {
                OpenSession();
                List<Signal> ret = (List<Signal>)Session.CreateCriteria(typeof(Signal))
                        .Add(Restrictions.Between("Zip", int.Parse(zipCode) - 2, int.Parse(zipCode) + 2))
                        .Add(Expression.Not(Expression.Eq("SignalID", signalID)))
                        .SetMaxResults(20)
                        .List<Signal>();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// Searches for signals by given parameters
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="address">The address.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="categoryID">The category ID.</param>
        /// <param name="status">The status.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns>Signal collection</returns>
        public List<Signal> Search(string city, string address, string zip, int categoryID, int status, int offset, out int totalRecords)
        {
            try
            {
                OpenSession();

                totalRecords = BuildCriteria(city, address, zip, categoryID, status, offset)
                    .SetProjection(Projections.RowCount()).FutureValue<int>().Value;

                List<Signal> ret = BuildCriteria(city, address, zip, categoryID, status, offset)
                    .SetMaxResults(offset + 10)
                    .SetFirstResult(offset)
                    .Future<Signal>().ToList();

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        private ICriteria BuildCriteria(string city, string address, string zip, int categoryID, int status, int offset)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(Signal))
                        .AddOrder(Order.Desc("CreationDate"));

            if (!city.Equals(string.Empty))
                criteria.Add(Restrictions.Eq("City", city));

            if (!address.Equals(string.Empty))
                criteria.Add(Restrictions.Like("Address", "%" + address + "%"));

            if (!zip.Equals(string.Empty))
                criteria.Add(Restrictions.Eq("Zip", zip));

            if (categoryID != -1)
                criteria.Add(Restrictions.Eq("CategoryID", categoryID));

            if (status != -2)
            {
                if (status == -1)
                    criteria.Add(Restrictions.Ge("Status", 1));
                else
                    criteria.Add(Restrictions.Eq("Status", status));
            }

            return criteria;
        }

        /// <summary>
        /// Gets the signal count by status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public int GetCountByStatus(int status)
        {
            OpenSession();
            int count = Session.CreateCriteria(typeof(Signal))
                .Add(Restrictions.Eq("Status", status))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();
            CloseSession();
            return count;
        }

        /// <summary>
        /// Gets the count of all approved (not resolved) signals.
        /// </summary>
        /// <returns></returns>
        public int GetCountAll()
        {
            OpenSession();
            int count = Session.CreateCriteria(typeof(Signal))
                .Add(Restrictions.Ge("Status", 1))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();
            CloseSession();
            return count;
        }

        /// <summary>
        /// Checks if email is subscribed to the signal
        /// </summary>
        /// <param name="ss">The signal subscription information</param>
        /// <returns>True if already subscribed otherwise false</returns>
        public bool CheckIfSubscribed(SignalSubscription ss)
        {
            OpenSession();

            int rowcount = Session.CreateCriteria(typeof(SignalSubscription))
                .Add(Restrictions.Eq("SignalID", ss.SignalID))
                .Add(Restrictions.Eq("Email", ss.Email))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            CloseSession();

            return (rowcount == 0);
        }

        /// <summary>
        /// Gets the signal subscriptions.
        /// </summary>
        /// <param name="signalID">The signal ID.</param>
        /// <returns>Subscription collection</returns>
        public List<SignalSubscription> GetSubscriptions(int signalID)
        {
            OpenSession();
            List<SignalSubscription> ret = (List<SignalSubscription>)Session.CreateCriteria(typeof(SignalSubscription))
                .Add(Restrictions.Eq("SignalID", signalID))
                .List<SignalSubscription>();
            CloseSession();
            return ret;
        }

        /// <summary>
        /// Subscribes email to the signal.
        /// </summary>
        /// <param name="ss">The ss.</param>
        public void SubscribeSignal(SignalSubscription ss)
        {
            OpenSession();
            Session.Save(ss);
            CloseSession();
        }

        /// <summary>
        /// Set the signal as resolved
        /// </summary>
        /// <param name="signalID">The signal ID.</param>
        /// <param name="comment">The comment.</param>
        public void ResolveSignal(int signalID, string comment)
        {
            this.ChangeSignalStatus(Signal.SignalStatus.Approved, signalID, comment);
        }

        /// <summary>
        /// Rejects the signal.
        /// </summary>
        /// <param name="signalID">The signal ID.</param>
        /// <param name="comment">The comment.</param>
        public void RejectSignal(int signalID, string comment)
        {
            this.ChangeSignalStatus(Signal.SignalStatus.NotApproved, signalID, comment);
        }

        private void ChangeSignalStatus(int newStatus, int signalID, string comment)
        {
            OpenSession();
            Signal s = this.LoadSingnal(signalID);
            s.Status = newStatus;
            s.ResolutionDate = DateTime.Now;
            s.ResolutionDescription = comment;
            s.UpdateDate = DateTime.Now;
            Session.SaveOrUpdate(s);
            CloseSession();
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            try
            {
                OpenSession();
                OpenTransaction();
                Signal s = this.LoadSingnal(id);
                Session.Delete(s);
                CommitTransaction();
                CloseSession();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                log.Error("Error deleting signal " + id, ex);
            }
            finally
            {
                CloseSession();
            }
        }
    }
}
