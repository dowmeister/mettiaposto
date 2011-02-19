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
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// Base class for Open311 client API
    /// </summary>
    public class Open311Base
    {
        private NetworkCredential _credentials = null;
        private Hashtable _parameters = new Hashtable();
        private WebProxy _proxy = null;
        private string _baseURL = string.Empty;
        private string _apiKey = string.Empty;
        private string _cityID = string.Empty;
        private string _responseFormat = Formats.XML;
        private WebRequest webClient = null;

        /// <summary>
        /// Format type defintions
        /// </summary>
        protected struct Formats
        {
            /// <summary>
            /// XML format
            /// </summary>
            public const string XML = ".xml";
            /// <summary>
            /// JSON format
            /// </summary>
            public const string JSON = ".json";
        }

        /// <summary>
        /// Specify with constants parameters key for querystring and post calls
        /// </summary>
        protected struct ParametersKey
        {
            /// <summary>
            /// 
            /// </summary>
            public const string ServiceCode = "service_code";
            /// <summary>
            /// 
            /// </summary>
            public const string Latitude = "lat";
            /// <summary>
            /// 
            /// </summary>
            public const string Longitude = "lon";
            /// <summary>
            /// 
            /// </summary>
            public const string AddressString = "address_string";
            /// <summary>
            /// 
            /// </summary>
            public const string CustomerEmail = "customer_email";
            /// <summary>
            /// 
            /// </summary>
            public const string DeviceID = "device_id";
            /// <summary>
            /// 
            /// </summary>
            public const string AccountID = "account_id";
            /// <summary>
            /// 
            /// </summary>
            public const string FirstName = "first_name";
            /// <summary>
            /// 
            /// </summary>
            public const string LastName = "last_name";
            /// <summary>
            /// 
            /// </summary>
            public const string PhoneNumber = "phone_number";
            /// <summary>
            /// 
            /// </summary>
            public const string ServiceRequestID = "service_request_id";
            /// <summary>
            /// 
            /// </summary>
            public const string Description = "description";
            /// <summary>
            /// 
            /// </summary>
            public const string MediaURL = "media_url";
            /// <summary>
            /// 
            /// </summary>
            public const string ApiKey = "api_key";
            /// <summary>
            /// 
            /// </summary>
            public const string CityID = "city_id";
            /// <summary>
            /// 
            /// </summary>
            public const string Token = "token";
            /// <summary>
            /// 
            /// </summary>
            public const string JuristicionID = "jurisdiction_id";
            /// <summary>
            /// 
            /// </summary>
            public const string Attributes = "attribute";
            /// <summary>
            /// 
            /// </summary>
            public const string StartDate = "start_date";
            /// <summary>
            /// 
            /// </summary>
            public const string EndDate = "end_date";
            /// <summary>
            /// 
            /// </summary>
            public const string Status = "status";
        }

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

        /// <summary>
        /// Gets or sets the city ID.
        /// </summary>
        /// <value>
        /// The city ID.
        /// </value>
        protected string CityID
        {
            get { return _cityID; }
            set { _cityID = value; }
        }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        protected Hashtable Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        /// <summary>
        /// Gets or sets the response format.
        /// </summary>
        /// <value>
        /// The response format.
        /// </value>
        /// <remarks>Default is XML</remarks>
        public string ResponseFormat
        {
            get { return _responseFormat; }
            set { _responseFormat = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Open311Base"/> class.
        /// </summary>
        public Open311Base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Open311Base"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="cityID">The city ID.</param>
        public Open311Base(string baseUrl, string apiKey, string cityID)
        {
            _baseURL = baseUrl;
            _apiKey = apiKey;
            _cityID = cityID;
        }

        /// <summary>
        /// Initializes the API request.
        /// </summary>
        protected void InitializeAPIRequest(string url)
        {
            webClient = WebRequest.Create(url);

            if (_proxy != null)
                webClient.Proxy = _proxy;

            if (_credentials != null && _proxy != null)
                webClient.Proxy.Credentials = Credentials;
        }

        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <returns></returns>
        protected string GetAPIResponse()
        {
            string responseString = string.Empty;
            WebResponse response = webClient.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                }
            }

            return responseString;
        }

        /// <summary>
        /// Builds the request query string.
        /// </summary>
        /// <returns></returns>
        protected string BuildRequestQueryString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DictionaryEntry param in Parameters)
            {
                sb.Append(param.Key);
                sb.Append("=");
                sb.Append(param.Value);
                sb.Append("&");
            }

            return sb.ToString().TrimEnd('&');
        }

        /// <summary>
        /// Sets the request method.
        /// </summary>
        /// <param name="method">The method.</param>
        protected void SetRequestMethod(string method)
        {
            webClient.Method = method;

            if (method.Equals("POST"))
                webClient.ContentType = "application/x-www-form-urlencoded";
        }

        /// <summary>
        /// Sets the post data.
        /// </summary>
        protected void SetPostData()
        {
            byte[] data = Encoding.UTF8.GetBytes(BuildRequestQueryString());
            webClient.ContentLength = data.Length;
            Stream stream = webClient.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
        }

        /// <summary>
        /// Deserializes the response.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="castResponse">The cast response.</param>
        /// <returns></returns>
        protected object DeserializeResponse(XmlDocument doc, Type castResponse)
        {
            XmlSerializer xSerializer = new XmlSerializer(castResponse);
            XmlNodeReader nReader = new XmlNodeReader(doc);
            return xSerializer.Deserialize(nReader);
        }

        /// <summary>
        /// Clears the parameters.
        /// </summary>
        protected void ClearParameters()
        {
            _parameters.Clear();
        }

        /// <summary>
        /// Checks the error.
        /// </summary>
        /// <param name="doc">The doc.</param>
        protected void CheckError(XmlDocument doc)
        {
            if (doc.DocumentElement.Name == "open311_error")
            {
                Open311Exception ex = new Open311Exception(doc.SelectSingleNode("/open311_error/errorDescription").InnerXml);
                ex.ErrorCode = doc.SelectSingleNode("/open311_error/errorCode").InnerXml;
                throw ex;
            }
        }
    }
}
