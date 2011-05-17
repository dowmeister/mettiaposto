using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NHibernate.UserTypes;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Framework.API.Google
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot(Namespace = "", ElementName = "GeocodeResponse")]
    public class GeocodeResponse
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [XmlElement("status")]
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [XmlElement("result")]
        public GeocodeResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddressComponent
    {
        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        [XmlElement("short_name")]
        public string ShortName { get; set; }
        /// <summary>
        /// Gets or sets the long name.
        /// </summary>
        /// <value>
        /// The long name.
        /// </value>
        [XmlElement("long_name")]
        public string LongName { get; set; }
        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        [XmlElement("type")]
        public List<string> Types { get; set; } 
    }

    /// <summary>
    /// 
    /// </summary>
    public class GeocodeResult
    {
        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>
        /// The geometry.
        /// </value>
        [XmlElement("geometry")]
        public Geometry Geometry { get; set; }
        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        [XmlElement("type")]
        public List<string> Types { get; set; }
        /// <summary>
        /// Gets or sets the address components.
        /// </summary>
        /// <value>
        /// The address components.
        /// </value>
        [XmlElement("address_component")]
        public AddressComponentCollection AddressComponents { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddressComponentCollection : List<AddressComponent>
    {
        /// <summary>
        /// Finds the type of the by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public AddressComponent FindByType(string type)
        {
            return this.Find(delegate(AddressComponent ac) { return ac.Types.Contains(type); });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot(Namespace = "", ElementName = "geometry")]
    public class Geometry : IUserType
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [XmlElement("location")]
        public Point Location { get; set; }
        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        [XmlElement("location_type")]
        public string LocationType { get; set; }
        /// <summary>
        /// Gets or sets the viewport.
        /// </summary>
        /// <value>
        /// The viewport.
        /// </value>
        [XmlElement("viewport")]
        public Bounds Viewport { get; set; }
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [XmlElement("bounds")]
        public Bounds Bounds { get; set; }

        #region IUserType Members

        /// <summary>
        /// Gets a value indicating whether this instance is mutable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mutable; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsMutable
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the type of the returned.
        /// </summary>
        /// <value>
        /// The type of the returned.
        /// </value>
        [XmlIgnore]
        public Type ReturnedType
        {
            get { return typeof(Geometry); }
        }

        /// <summary>
        /// Gets the SQL types.
        /// </summary>
        [XmlIgnore]
        public SqlType[] SqlTypes
        {
            get { return new[] { NHibernateUtil.String.SqlType }; }
        }

        /// <summary>
        /// Nulls the safe get.
        /// </summary>
        /// <param name="rs">The rs.</param>
        /// <param name="names">The names.</param>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);

            if (obj == null) return null;

            if (!obj.ToString().Equals(string.Empty))
                return XmlUtils.Deserialize(obj.ToString(), typeof(Geometry));
            else
                return null;
        }

        /// <summary>
        /// Nulls the safe set.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((IDataParameter)cmd.Parameters[index]).Value = XmlUtils.Serialize(value).InnerXml;
            }
        }

        /// <summary>
        /// Deeps the copy.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object DeepCopy(object value)
        {
            return value;
        }

        /// <summary>
        /// Replaces the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="target">The target.</param>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        /// <summary>
        /// Assembles the specified cached.
        /// </summary>
        /// <param name="cached">The cached.</param>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        /// <summary>
        /// Disassembles the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Disassemble(object value)
        {
            return value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="x">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x == null || y == null) return false;

            return x.Equals(y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(object x)
        {
            return x == null ? typeof(object).GetHashCode() + 473 : x.GetHashCode();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class Bounds
    {
        /// <summary>
        /// Gets or sets the northeast.
        /// </summary>
        /// <value>
        /// The northeast.
        /// </value>
        [XmlElement("northeast")]
        public Point Northeast { get; set; }
        /// <summary>
        /// Gets or sets the southwest.
        /// </summary>
        /// <value>
        /// The southwest.
        /// </value>
        [XmlElement("southwest")]
        public Point Southwest { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [XmlElement("lat")]
        public decimal Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [XmlElement("lng")]
        public decimal Longitude { get; set; }
    }
}
