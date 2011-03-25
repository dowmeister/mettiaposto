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
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Places
{
    /// <summary>
    /// This class manage Places
    /// </summary>
    public class PlaceManager : NHibernateSessionManager
    {
        /// <summary>
        /// Gets the active places.
        /// </summary>
        /// <returns></returns>
        public List<Place> GetActivePlaces()
        {
            try
            {
                OpenSession();
                List<Place> ret = (List<Place>)Session.CreateCriteria(typeof(Place))
                    .AddOrder(new NHibernate.Criterion.Order("Name", true))    
                    .Add(Restrictions.Eq("Status", true))
                    .List<Place>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading active places", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// Gets the active places.
        /// </summary>
        /// <returns></returns>
        public List<Place> GetPlaces()
        {
            try
            {
                OpenSession();
                List<Place> ret = (List<Place>)Session.CreateCriteria(typeof(Place))
                     .AddOrder(new NHibernate.Criterion.Order("Name", true))    
                    .List<Place>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading active places", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// Loads the place.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Place LoadPlace(string name)
        {
            try
            {
                OpenSession();
                Place ret = Session.CreateCriteria(typeof(Place))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Place>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading place by name", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// Creates the place.
        /// </summary>
        /// <param name="p">The p.</param>
        public void CreatePlace(Place p)
        {
            try
            {
                OpenSession();
                OpenTransaction();
                Session.Save(p);
                CommitTransaction();
                CloseSession();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }
    }
}
