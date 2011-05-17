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
    [XmlRoot(Namespace = "", ElementName = "GeocodeResponse")]
    public class GeocodeResponse
    {
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlElement("result")]
        public GeocodeResult Result { get; set; }
    }

    public class AddressComponent
    {
        [XmlElement("short_name")]
        public string ShortName { get; set; }
        [XmlElement("long_name")]
        public string LongName { get; set; }
        [XmlElement("type")]
        public List<string> Types { get; set; } 
    }

    public class GeocodeResult
    {
        [XmlElement("geometry")]
        public Geometry Geometry { get; set; }
        [XmlElement("type")]
        public List<string> Types { get; set; }
        [XmlElement("address_component")]
        public AddressComponentCollection AddressComponents { get; set; }
    }

    public class AddressComponentCollection : List<AddressComponent>
    {
        public AddressComponent FindByType(string type)
        {
            return this.Find(delegate(AddressComponent ac) { return ac.Types.Contains(type); });
        }
    }

    [XmlRoot(Namespace = "", ElementName = "geometry")]
    public class Geometry : IUserType
    {
        [XmlElement("location")]
        public Point Location { get; set; }
        [XmlElement("location_type")]
        public string LocationType { get; set; }
        [XmlElement("viewport")]
        public Bounds Viewport { get; set; }
        [XmlElement("bounds")]
        public Bounds Bounds { get; set; }

        #region IUserType Members

        [XmlIgnore]
        public bool IsMutable
        {
            get { return false; }
        }

        [XmlIgnore]
        public Type ReturnedType
        {
            get { return typeof(Geometry); }
        }

        [XmlIgnore]
        public SqlType[] SqlTypes
        {
            get { return new[] { NHibernateUtil.String.SqlType }; }
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);

            if (obj == null) return null;

            if (!obj.ToString().Equals(string.Empty))
                return XmlUtils.Deserialize(obj.ToString(), typeof(Geometry));
            else
                return null;
        }

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

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x == null || y == null) return false;

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x == null ? typeof(object).GetHashCode() + 473 : x.GetHashCode();
        }

        #endregion
    }

    public class Bounds
    {
        [XmlElement("northeast")]
        public Point Northeast { get; set; }
        [XmlElement("southwest")]
        public Point Southwest { get; set; }
    }

    public class Point
    {
        [XmlElement("lat")]
        public decimal Latitude { get; set; }
        [XmlElement("lng")]
        public decimal Longitude { get; set; }
    }
}
