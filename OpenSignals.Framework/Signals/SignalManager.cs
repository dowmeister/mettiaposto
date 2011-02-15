using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Signals
{
    public class SignalManager : NHibernateSessionManager
    {
        public Signal LoadSingnal(int id)
        {
            OpenSession();
            Signal ret = (Signal)session.Load(typeof(Signal), id);
            CloseSession();
            return ret;
        }

        public void CreateSignal(Signal s)
        {
            try
            {
                OpenSession();
                OpenTransaction();
                session.Save(s);
                CommitTransaction();
                CloseSession();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            finally
            {

            }
        }

        public List<Signal> SearchNearZip(string zipCode, int signalID)
        {
            try
            {
                OpenSession();
                List<Signal> ret = (List<Signal>)session.CreateCriteria(typeof(Signal))
                        .Add(Restrictions.Between("Zip", int.Parse(zipCode) - 2, int.Parse(zipCode) + 2))
                        .Add(Expression.Not(Expression.Eq("SignalID", signalID)))
                        .SetMaxResults(20)
                        .List<Signal>();
                CloseSession();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                CloseSession();

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ICriteria BuildCriteria(string city, string address, string zip, int categoryID, int status, int offset)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Signal))
                        .Add(Restrictions.Eq("City", city))
                        .AddOrder(Order.Desc("CreationDate"));

            if (!address.Equals(string.Empty))
                criteria.Add(Restrictions.Like("Address", "%" + address + "%"));

            if (!zip.Equals(string.Empty))
                criteria.Add(Restrictions.Eq("Zip", zip));

            if (categoryID != -1)
                criteria.Add(Restrictions.Eq("CategoryID", categoryID));

            if (status == -1)
                criteria.Add(Restrictions.Ge("Status", 1));
            else
                criteria.Add(Restrictions.Eq("Status", status));

            return criteria;
        }

        public int GetCountByStatus(int status)
        {
            OpenSession();
            int count = session.CreateCriteria(typeof(Signal))
                .Add(Restrictions.Eq("Status", status))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();    
            CloseSession();
            return count;
        }

        public int GetCountAll()
        {
            OpenSession();
            int count = session.CreateCriteria(typeof(Signal))
                .Add(Restrictions.Ge("Status", 1))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();
            CloseSession();
            return count;
        }

        public bool CheckIfSubscribed(SignalSubscription ss)
        {
            OpenSession();

            int rowcount = session.CreateCriteria(typeof(SignalSubscription))
                .Add(Restrictions.Eq("SignalID", ss.SignalID))
                .Add(Restrictions.Eq("Email", ss.Email))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();
            
            CloseSession();

            return (rowcount == 0);
        }

        public List<SignalSubscription> GetSubscriptions(int signalID)
        {
            OpenSession();
            List<SignalSubscription> ret = (List<SignalSubscription>)session.CreateCriteria(typeof(SignalSubscription))
                .Add(Restrictions.Eq("SignalID", signalID))
                .List<SignalSubscription>();
            CloseSession();
            return ret;
        }

        public void SubscribeSignal(SignalSubscription ss)
        {
            OpenSession();
            session.Save(ss);
            CloseSession();            
        }

        public void ResolveSignal(int signalID, string comment)
        {
            OpenSession();
            Signal s = this.LoadSingnal(signalID);
            s.Status = Signal.SignalStatus.Resolved;
            s.ResolutionDate = DateTime.Now;
            s.ResolutionDescription = comment;
            s.UpdateDate = DateTime.Now;
            session.SaveOrUpdate(s);
            CloseSession();
        }
    }
}
