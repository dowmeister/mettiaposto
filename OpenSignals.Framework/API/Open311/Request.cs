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
using System;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("request")]
    [XmlType("request")]
    public class ProblemRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public struct RequestStatus
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Open = "Open";
            /// <summary>
            /// 
            /// </summary>
            public const string Closed = "Closed";
        }

        string serviceRequestID = string.Empty;

        /// <summary>
        /// Gets or sets the service request ID.
        /// </summary>
        /// <value>
        /// The service request ID.
        /// </value>
        [XmlElement("service_request_id")]
        public string ServiceRequestID
        {
            get { return serviceRequestID; }
            set { serviceRequestID = value; }
        }
        string status = string.Empty;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [XmlElement("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string statusNotes = string.Empty;

        /// <summary>
        /// Gets or sets the status notes.
        /// </summary>
        /// <value>
        /// The status notes.
        /// </value>
        [XmlElement("status_notes")]
        public string StatusNotes
        {
            get { return statusNotes; }
            set { statusNotes = value; }
        }
        string serviceName = string.Empty;

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        [XmlElement("service_name")]
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }
        string serviceCode = string.Empty;

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [XmlElement("service_code")]
        public string ServiceCode
        {
            get { return serviceCode; }
            set { serviceCode = value; }
        }
        string description = string.Empty;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [XmlElement("description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string agencyResponsible = string.Empty;

        /// <summary>
        /// Gets or sets the agency responsible.
        /// </summary>
        /// <value>
        /// The agency responsible.
        /// </value>
        [XmlElement("agency_responsible")]
        public string AgencyResponsible
        {
            get { return agencyResponsible; }
            set { agencyResponsible = value; }
        }
        string serviceNotice = string.Empty;

        /// <summary>
        /// Gets or sets the service notice.
        /// </summary>
        /// <value>
        /// The service notice.
        /// </value>
        [XmlElement("service_notice")]
        public string ServiceNotice
        {
            get { return serviceNotice; }
            set { serviceNotice = value; }
        }
        DateTime requestedDatetime = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the requested datetime.
        /// </summary>
        /// <value>
        /// The requested datetime.
        /// </value>
        [XmlElement("request_date_time")]
        public DateTime RequestedDatetime
        {
            get { return requestedDatetime; }
            set { requestedDatetime = value; }
        }
        Nullable<System.DateTime> updatedDatetime = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the updated datetime.
        /// </summary>
        /// <value>
        /// The updated datetime.
        /// </value>
        [XmlElement("update_datetime",IsNullable=true)]
        public Nullable<System.DateTime> UpdatedDatetime
        {
            get { return updatedDatetime; }
            set { updatedDatetime = value; }
        }
        Nullable<System.DateTime> expectedDatetime = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the expected datetime.
        /// </summary>
        /// <value>
        /// The expected datetime.
        /// </value>
        [XmlElement("expected_datetime",IsNullable=true)]
        public Nullable<System.DateTime> ExpectedDatetime
        {
            get { return expectedDatetime; }
            set { expectedDatetime = value; }
        }
        string address = string.Empty;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [XmlElement("address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        string addressID = string.Empty;

        /// <summary>
        /// Gets or sets the address ID.
        /// </summary>
        /// <value>
        /// The address ID.
        /// </value>
        [XmlElement("address_id")]
        public string AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }
        string zipCode = string.Empty;

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [XmlElement("zipcode")]
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
        decimal latitude = 0.0M;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [XmlElement("lat")]
        public decimal Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        decimal longitude = 0.0M;

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [XmlElement("long")]
        public decimal Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        string mediaURL = string.Empty;

        /// <summary>
        /// Gets or sets the media URL.
        /// </summary>
        /// <value>
        /// The media URL.
        /// </value>
        [XmlElement("media_url")]
        public string MediaURL
        {
            get { return mediaURL; }
            set { mediaURL = value; }
        }

        string token = string.Empty;

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [XmlElement("token")]
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        string accountID = string.Empty;

        /// <summary>
        /// Gets or sets the account ID.
        /// </summary>
        /// <value>
        /// The account ID.
        /// </value>
        [XmlElement("account_id")]
        public string AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("service_requests")]
    public class RequestResponse
    {
        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        [XmlElement("request")]
        public List<ProblemRequest> Requests { get; set; }
    }
}
