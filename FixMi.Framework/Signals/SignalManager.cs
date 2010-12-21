using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMi.Framework.Core.Base;
using FixMi.Framework.Data;
using NHibernate;
using NHibernate.Criterion;

namespace FixMi.Framework.Signals
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

        public List<Signal> SearchNearZip(string zipCode)
        {
            try
            {
                OpenSession();
                List<Signal> ret = (List<Signal>)session.CreateCriteria(typeof(Signal))
                        .Add(Restrictions.Between("Zip", int.Parse(zipCode) - 2, int.Parse(zipCode) + 2))
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

        public List<Signal> Search(string city, string address, string zip, int categoryID, int status, int offset)
        {
            try
            {
                OpenSession();
                ICriteria criteria = session.CreateCriteria(typeof(Signal))
                        .Add(Restrictions.Eq("City", city))
                        .SetMaxResults(offset + 20)
                        .SetFirstResult(offset);

                if (!address.Equals(string.Empty))
                    criteria.Add(Restrictions.Like("Address", address));

                if (!address.Equals(string.Empty))
                    criteria.Add(Restrictions.Like("Address", address));

                if (!address.Equals(string.Empty))
                    criteria.Add(Restrictions.Like("Address", address));

                if (!address.Equals(string.Empty))
                    criteria.Add(Restrictions.Like("Address", address));

                if (!zip.Equals(string.Empty))
                    criteria.Add(Restrictions.Eq("Zip", zip));

                if (categoryID != -1)
                    criteria.Add(Restrictions.Eq("CategoryID", categoryID));

                if (status != -1)
                    criteria.Add(Restrictions.Eq("Status", status));

                List<Signal> ret = (List<Signal>)criteria.List<Signal>();

                CloseSession();

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
