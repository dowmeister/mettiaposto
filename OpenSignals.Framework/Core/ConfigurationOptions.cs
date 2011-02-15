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

namespace OpenSignals.Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public virtual string Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public virtual string Value { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OptionCollection : List<Option>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionCollection"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        public OptionCollection(IList<Option> x)
        {
            this.AddRange(x);
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return this.Find(delegate(Option o) { return o.Key.Equals(key); }) != null;
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        ///   </returns>
        ///   
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.
        /// -or-
        ///   <paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"/>.
        ///   </exception>
        public Option this[string key]
        {
            get
            {
                return this.Find(delegate(Option o) { return o.Key.Equals(key); });
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationOptions : NHibernateSessionManager
    {
        #region Private Properties

        private OptionCollection _htOptions = null;

        #endregion

        #region Singleton Access

        private static ConfigurationOptions _objOptions = new ConfigurationOptions();
        /// <summary>
        /// Gets the current.
        /// </summary>
        public static ConfigurationOptions Current { get { return _objOptions; } }

        #endregion

        #region Constructor

        private ConfigurationOptions()
        {
            Load();
        }

        #endregion

        #region Private Methods

        private void Load()
        {
            OpenSession();
            _htOptions = new OptionCollection(session.CreateCriteria(typeof(Option)).List<Option>());
            CloseSession();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="szKey">The sz key.</param>
        /// <returns></returns>
        public string GetString(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return _htOptions[szKey].ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="szKey">The sz key.</param>
        /// <returns></returns>
        public bool GetBool(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return Convert.ToBoolean(_htOptions[szKey]);
            else
                return false;
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="szKey">The sz key.</param>
        /// <returns></returns>
        public int GetInt32(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return Convert.ToInt32(_htOptions[szKey]);
            else
                return -1;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            _htOptions = null;
            Load();
        }

        /// <summary>
        /// Gets the custom.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetCustom(string key)
        {
            if (_htOptions.Contains(key))
                return _htOptions[key];
            else
                return null;
        }

        /// <summary>
        /// Gets the array.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sep">The sep.</param>
        /// <returns></returns>
        public string[] GetArray(string key, char sep)
        {
            if (_htOptions.Contains(key))
                return _htOptions[key].ToString().Split(sep);
            else
                return new string[0];
        }

        #endregion
    }
}
