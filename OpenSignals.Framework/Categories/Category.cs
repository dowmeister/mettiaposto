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

namespace OpenSignals.Framework.Categories
{
    /// <summary>
    /// 
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 
        /// </summary>
        public Category() { }

        #region Private variables

        private int _categoryID;
        private string _name;
        private bool _status;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public virtual int CategoryID { get { return _categoryID; } set { _categoryID = value; } }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get { return _name; } set { _name = value; } }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool Status { get { return _status; } set { _status = value; } }

        #endregion
    }

}
