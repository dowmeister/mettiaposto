using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Newsletter
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsletterManager : NHibernateSessionManager
    {
        /// <summary>
        /// Subscribes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void SubscribeUser(NewsletterEntry user)
        {
            try
            {
                OpenSession();
                OpenTransaction();
                Session.Save(user);
                 
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                 
                throw ex;
            }
        }
    }
}
