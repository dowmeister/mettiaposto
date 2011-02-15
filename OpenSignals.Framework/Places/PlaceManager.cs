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

using System.Collections.Generic;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Places
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceManager : NHibernateSessionManager
    {
        /// <summary>
        /// Gets the active places.
        /// </summary>
        /// <returns></returns>
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
