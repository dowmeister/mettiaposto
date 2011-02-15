using System.Collections.Generic;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Places
{
    public class PlaceManager : NHibernateSessionManager
    {
        public List<Place> GetActivePlaces()
        {
            OpenSession();
            List<Place> ret = (List<Place>)session.CreateCriteria(typeof(Place))
                .Add(Restrictions.Eq("Status", true))
                .List<Place>();
            CloseSession();
            return ret;
        }
    }
}
