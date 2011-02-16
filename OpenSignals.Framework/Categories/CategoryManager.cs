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
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Categories
{
    /// <summary>
    /// This class manage Categories.
    /// </summary>
    public class CategoryManager : NHibernateSessionManager
    {
        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <returns>Category collection</returns>
        public List<Category> GetActive()
        {
            try
            {
                OpenSession();
                List<Category> cats = (List<Category>)session.CreateCriteria(typeof(Category))
                    .AddOrder(new NHibernate.Criterion.Order("Name", true)).List<Category>();
                return cats;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading active categories", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// Loads the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Category object</returns>
        public Category Load(int id)
        {
            try
            {
                OpenSession();
                Category ret = (Category)session.Load(typeof(Category), id);
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading single category", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
        }
    }
}
