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
    }
}
