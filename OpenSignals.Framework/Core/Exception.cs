using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;

namespace OpenSignals.Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenSignalsException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSignalsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public OpenSignalsException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSignalsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public OpenSignalsException(string message, Exception ex) : base(message, ex) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSignalsException"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public OpenSignalsException(Exception ex) : base(ex.Message, ex) { }
    }

    public class NHibernateElmahException : IUserType
    {
        #region IUserType Members

        public object Assemble(object cached, object owner)
        {
            throw new NotImplementedException();
        }

        public object DeepCopy(object value)
        {
            throw new NotImplementedException();
        }

        public object Disassemble(object value)
        {
            throw new NotImplementedException();
        }

        public bool Equals(object x, object y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object x)
        {
            throw new NotImplementedException();
        }

        public bool IsMutable
        {
            get { throw new NotImplementedException(); }
        }

        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            throw new NotImplementedException();
        }

        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            throw new NotImplementedException();
        }

        public object Replace(object original, object target, object owner)
        {
            throw new NotImplementedException();
        }

        public Type ReturnedType
        {
            get { throw new NotImplementedException(); }
        }

        public NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
