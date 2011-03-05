using System.Xml;
using System.Xml.Serialization;

namespace OpenSignals.Framework.Web
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRootAttribute(ElementName = "rss")] 
    public class RssGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssGenerator"/> class.
        /// </summary>
        public RssGenerator() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RssGenerator"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public RssGenerator(string version)
        {
            Version = version;
        }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        [XmlElement("channel")]
        public Channel Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlAttribute("version")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private Channel _channel;
        private string _version = "2.0";
    }
}
