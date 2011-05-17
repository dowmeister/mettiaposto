using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Globalization;

namespace OpenSignals.Framework.Web.RSS
{
    [XmlRoot("feed")]
    public class GeoRSS
    {
//        <?xml version="1.0" encoding="utf-8"?>
//<feed xmlns="http://www.w3.org/2005/Atom" 
//  xmlns:georss="http://www.georss.org/georss">
//  <title>Earthquakes</title>
//  <subtitle>International earthquake observation labs</subtitle>
//  <link href="http://example.org/"/>
//  <updated>2005-12-13T18:30:02Z</updated>
//  <author>
//    <name>Dr. Thaddeus Remor</name>
//    <email>tremor@quakelab.edu</email>
//  </author>
//  <id>urn:uuid:60a76c80-d399-11d9-b93C-0003939e0af6</id>
//  <entry>
//    <title>M 3.2, Mona Passage</title>
//    <link href="http://example.org/2005/09/09/atom01"/>
//    <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
//    <updated>2005-08-17T07:02:32Z</updated>
//    <summary>We just had a big one.</summary>
//    <georss:point>45.256 -71.92</georss:point>
//  </entry>
//</feed>

        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("subtitle")]
        public string SubTitle { get; set; }
        [XmlElement("link")]
        public GeoRSSLink Link { get; set; }
        [XmlElement("updated")]
        public string Updated { get { return DateTime.Now.ToString("s"); } }
        [XmlElement("id")]
        public Guid ID { get { return Guid.NewGuid(); } }
        [XmlElement("author")]
        public GeoRSSAuthor Author { get; set; }
        [XmlElement(ElementName="entry", Type = typeof(GeoRSSEntry))]
        public List<GeoRSSEntry> Entries { get; set; }
    }

    public class GeoRSSLink
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        public GeoRSSLink() { }

        public GeoRSSLink(string href)
        {
            this.Href = href;
        }
    }

    public class GeoRSSAuthor
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }

        public GeoRSSAuthor() { }

        public GeoRSSAuthor(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
    }

    public class GeoRSSEntry
    {
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("link")]
        public GeoRSSLink Link { get; set; }
        [XmlElement("id")]
        public string ID { get; set; }
        [XmlElement("summary")]
        public string Summary { get; set; }
        [XmlElement(ElementName = "point", Namespace = "http://www.georss.org/georss")]
        public string PointString { get; set; }

        [XmlIgnore]
        public GeoRSSPoint Point
        {
            get
            {
                string[] latLng = this.PointString.Split(' ');
                return new GeoRSSPoint(Convert.ToDecimal(latLng[0]), Convert.ToDecimal(latLng[1]));
            }
            set
            {
                this.PointString = value.ToString();
            }
        }
    }

    public class GeoRSSPoint
    {
        public GeoRSSPoint(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return this.Latitude.ToString("N3", new CultureInfo("en-US")) + " " + this.Longitude.ToString("N3", new CultureInfo("en-US"));
        }
    }    
}
