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
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Xml;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// 
    /// </summary>
    public class Open311Client
    {
        private Hashtable _parameters = new Hashtable();

        private WebProxy _proxy = null;

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>
        /// The proxy.
        /// </value>
        public WebProxy Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }

        private NetworkCredential _credentials = null;

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public NetworkCredential Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
        }

        private string _baseURL = string.Empty;

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseURL
        {
            get { return _baseURL; }
            set { _baseURL = value; }
        }

        private string _apiKey = string.Empty;

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        private string _cityID = string.Empty;

        /// <summary>
        /// Gets or sets the city ID.
        /// </summary>
        /// <value>
        /// The city ID.
        /// </value>
        public string CityID
        {
            get { return _cityID; }
            set { _cityID = value; }
        }

        private struct Parameters
        {
            public const string ServiceCode = "service_code";
            public const string Latitude = "lat";
            public const string Longitude = "lon";
            public const string AddressString = "address_string";
            public const string CustomerEmail = "customer_email";
            public const string DeviceID = "device_id";
            public const string AccountID = "account_id";
            public const string FirstName = "first_name";
            public const string LastName = "last_name";
            public const string PhoneNumber = "phone_number";
            public const string ServiceRequestID = "service_request_id";
            public const string Description = "description";
            public const string MediaURL = "media_url";
            public const string ApiKey = "api_key";
            public const string CityID = "city_id";
        }

        private struct Methods
        {
            public const string ServiceList = "service_list";
            public const string CreateRequest = "create_request";
            public const string StatusUpdate = "status_update";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Open311Client"/> class.
        /// </summary>
        public Open311Client() { } 

        /// <summary>
        /// Initializes a new instance of the <see cref="Open311Client"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="cityID">The city ID.</param>
        public Open311Client(string baseUrl, string apiKey, string cityID)
        {
            _baseURL = baseUrl;
            _apiKey = apiKey;
            _cityID = cityID;
        }

        private XmlDocument MakeAPIRequest(string methodName)
        {
            if (_apiKey.Equals(string.Empty))
                throw new ArgumentException("ApiKey not provided");

            if (_cityID.Equals(string.Empty))
                throw new ArgumentException("CityID not provided");

            if (_baseURL.Equals(string.Empty))
                throw new ArgumentException("BaseUrl not provided");

            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (DictionaryEntry param in _parameters)
                {
                    sb.Append(param.Key);
                    sb.Append("=");
                    sb.Append(param.Value);
                    sb.Append("&");
                }

                WebClient wb = new WebClient();

                if (_proxy != null)
                    wb.Proxy = _proxy;

                if (_credentials != null && _proxy != null)
                    wb.Proxy.Credentials = _credentials;

                string response = wb.DownloadString(_baseURL + "/" + methodName + "?" + sb.ToString().TrimEnd('&'));
                XmlDocument xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(response);
                return xmlResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects the service.
        /// </summary>
        /// <returns></returns>
        public List<ServiceType> SelectService()
        {
            try
            {
                _parameters.Add(Parameters.ApiKey, _apiKey);
                _parameters.Add(Parameters.CityID, _cityID);

                MakeAPIRequest(Methods.ServiceList);

                return new List<ServiceType>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates the request.
        /// </summary>
        public void CreateRequest(string service_code, decimal lat, decimal lon, string address_string, string customer_email,
            string device_id, string acccount_id, string first_name, string last_name, string phone_number, string description, string media_url)
        {
            _parameters.Add(Parameters.ApiKey, _apiKey);
            _parameters.Add(Parameters.CityID, _cityID);

            if (service_code == null || service_code.Equals(string.Empty))
                throw new ArgumentException("Parameter service_code cannot be null or empty");

            try
            {
                _parameters.Add(Parameters.ServiceCode, service_code);

                _parameters.Add(Parameters.Latitude, lat);
                _parameters.Add(Parameters.Longitude, lon);
                _parameters.Add(Parameters.AddressString, address_string);
                _parameters.Add(Parameters.CustomerEmail, customer_email);
                _parameters.Add(Parameters.DeviceID, device_id);
                _parameters.Add(Parameters.AccountID, acccount_id);
                _parameters.Add(Parameters.FirstName, first_name);
                _parameters.Add(Parameters.LastName, last_name);
                _parameters.Add(Parameters.PhoneNumber, phone_number);
                _parameters.Add(Parameters.CustomerEmail, customer_email);
                _parameters.Add(Parameters.Description, description);
                _parameters.Add(Parameters.MediaURL, media_url);

                MakeAPIRequest(Methods.CreateRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Requests a status update for given service request ID
        /// </summary>
        /// <param name="serviceRequestID">The service request ID.</param>
        public void StatusUpdate(string serviceRequestID)
        {
            _parameters.Add(Parameters.ApiKey, _apiKey);
            _parameters.Add(Parameters.CityID, _cityID);

            if (serviceRequestID == null || serviceRequestID.Equals(string.Empty))
                throw new ArgumentException("Parameter serviceRequestID cannot be null or empty");

            try
            {
                _parameters.Add(Parameters.ServiceRequestID, serviceRequestID);

                MakeAPIRequest(Methods.StatusUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
