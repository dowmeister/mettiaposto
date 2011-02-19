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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// Class representation of Open311 Service
    /// </summary>
    [XmlRoot("service")]
    [XmlType("service")]
    public class Service
    {
        private string _serviceCode = string.Empty;

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [XmlElement("service_code")]
        public string ServiceCode
        {
            get { return _serviceCode; }
            set { _serviceCode = value; }
        }
        private string _serviceName = string.Empty;

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        [XmlElement("service_name")]
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        private string _serviceDescription = string.Empty;

        /// <summary>
        /// Gets or sets the service description.
        /// </summary>
        /// <value>
        /// The service description.
        /// </value>
        [XmlElement("service_description")]
        public string Description
        {
            get { return _serviceDescription; }
            set { _serviceDescription = value; }
        }

        private string _metaData = string.Empty;

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        [XmlElement("metadata")]
        public string MetaData
        {
            get { return _metaData; }
            set { _metaData = value; }
        }
        private string _type = string.Empty;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlElement("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _keywords = string.Empty;

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        [XmlElement("keywords")]
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
        private string _group = string.Empty;

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        [XmlElement("group")]
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
    }

    /// <summary>
    /// Collection of Service class
    /// </summary>
    [XmlRoot("services")]
    public class ServiceResponse
    {
        private List<Service> _services = new List<Service>();

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        [XmlElement("service")]
        public List<Service> Services
        {
            get { return _services; }
            set { _services = value; }
        }
    }
}
