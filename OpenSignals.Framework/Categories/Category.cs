using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSignals.Framework.Categories
{
    public class Category
    {
        public Category() { }

        #region Private variables

        private int _categoryID;
        private string _name;
        private bool _status;

        #endregion

        #region Public Properties

        public virtual int CategoryID { get { return _categoryID; } set { _categoryID = value; } }
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual bool Status { get { return _status; } set { _status = value; } }

        #endregion
    }

}
