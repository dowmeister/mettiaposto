using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Core
{
    public class Option
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }

    public class OptionCollection : List<Option>
    {
        public OptionCollection(IList<Option> x)
        {
            this.AddRange(x);
        }

        public bool Contains(string key)
        {
            return this.Find(delegate(Option o) { return o.Key.Equals(key); }) != null;
        }

        public Option this[string key]
        {
            get
            {
                return this.Find(delegate(Option o) { return o.Key.Equals(key); });
            }
        }
    }

    public class ConfigurationOptions : NHibernateSessionManager
    {
        #region Private Properties

        private OptionCollection _htOptions = null;

        #endregion

        #region Singleton Access

        private static ConfigurationOptions _objOptions = new ConfigurationOptions();
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

        public string GetString(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return _htOptions[szKey].ToString();
            else
                return string.Empty;
        }

        public bool GetBool(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return Convert.ToBoolean(_htOptions[szKey]);
            else
                return false;
        }

        public int GetInt32(string szKey)
        {
            if (_htOptions.Contains(szKey))
                return Convert.ToInt32(_htOptions[szKey]);
            else
                return -1;
        }

        public void Reset()
        {
            _htOptions = null;
            Load();
        }

        public object GetCustom(string key)
        {
            if (_htOptions.Contains(key))
                return _htOptions[key];
            else
                return null;
        }

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
