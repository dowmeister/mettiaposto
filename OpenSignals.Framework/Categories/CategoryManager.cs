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
using NHibernate.Criterion;

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
                List<Category> cats = (List<Category>)Session.CreateCriteria(typeof(Category))
                    .Add(Restrictions.Eq("Status", true))
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
                 
            }
        }

        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <returns>Category collection</returns>
        public List<Category> GetCategories()
        {
            try
            {
                OpenSession();
                List<Category> cats = (List<Category>)Session.CreateCriteria(typeof(Category))
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
                Category ret = (Category)Session.Load(typeof(Category), id);
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal("Error loading single category", ex);
                throw ex;
            }
            finally
            {
                 
            }
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
                Category ret = this.Load(id);
                Session.Delete(ret);
            }
            catch (Exception ex)
            {
                log.Fatal("Error deleting single category", ex);
                throw ex;
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Updates the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        public void Update(Category c)
        {
            try
            {
                OpenSession();
                Session.Evict(c);
                Session.Update(c);
            }
            catch (Exception ex)
            {
                log.Fatal("Error updating single category", ex);
                throw ex;
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Creates the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        public void Create(Category c)
        {
            try
            {
                OpenSession();
                Session.Save(c);
            }
            catch (Exception ex)
            {
                log.Fatal("Error updating single category", ex);
                throw ex;
            }
            finally
            {
                 
            }
        }
    }
}
