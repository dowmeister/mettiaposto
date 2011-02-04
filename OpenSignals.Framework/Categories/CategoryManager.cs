using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Categories
{
    public class CategoryManager : NHibernateSessionManager
    {
        public List<Category> GetActive()
        {
            OpenSession();
            List<Category> cats = (List<Category>)session.CreateCriteria(typeof(Category))
                .AddOrder(new NHibernate.Criterion.Order("Name",true)).List<Category>();
            CloseSession();
            return cats;
        }

        public Category Load(int id)
        {
            OpenSession();
            Category ret = (Category)session.Load(typeof(Category), id);
            CloseSession();
            return ret;
        }
    }
}
