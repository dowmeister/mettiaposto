using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMi.Framework.Core.Base;
using FixMi.Framework.Data;

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
    }
}
