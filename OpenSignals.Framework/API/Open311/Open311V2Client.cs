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
using System.Xml;
using System.Collections.Generic;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// Open311 V2 client implementation
    /// </summary>
    public class Open311V2Client : Open311Base
    {
        private struct Methods
        {
            /// <summary>
            /// 
            /// </summary>
            public const string ServiceList = "services";
            /// <summary>
            /// 
            /// </summary>
            public const string Requests = "requests";
            /// <summary>
            /// 
            /// </summary>
            public const string Tokens = "tokens/{0}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Open311V2Client"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="cityID">The city ID.</param>
        public Open311V2Client(string baseUrl, string apiKey, string cityID) : base(baseUrl, apiKey, cityID) { }

        /// <summary>
        /// Makes the API request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="ensureApiKey">if set to <c>true</c> [ensure API key].</param>
        /// <param name="requestMethod">The request method.</param>
        /// <returns></returns>
        private object MakeAPIRequest(string method, bool ensureApiKey, string requestMethod)
        {
            if (ensureApiKey)
            {
                if (ApiKey.Equals(string.Empty))
                    throw new ArgumentException("ApiKey not provided");
            }

            if (CityID.Equals(string.Empty))
                throw new ArgumentException("CityID or JuristicionID not provided");

            if (BaseURL.Equals(string.Empty))
                throw new ArgumentException("BaseUrl not provided");

            try
            {
                object retObject = null;

                string url = BaseURL + method + ResponseFormat;

                if (requestMethod.Equals("GET"))
                {
                    url += "?" + this.BuildRequestQueryString();
                    InitializeAPIRequest(url);
                    SetRequestMethod(requestMethod);
                }
                else
                {
                    InitializeAPIRequest(url);
                    SetRequestMethod(requestMethod);
                    SetPostData();
                }

                if (ResponseFormat == Formats.XML)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(this.GetAPIResponse());

                    CheckError(doc);

                    retObject = doc;
                }

                return retObject;
            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <remarks>http://wiki.open311.org/GeoReport_v2#GET_Service_List</remarks>
        /// <returns>Object containing Services collection</returns>
        public List<Service> GetServices()
        {
            try
            {
                ClearParameters();

                Parameters.Add(ParametersKey.JuristicionID, CityID);

                if (ResponseFormat == Formats.XML)
                {
                    XmlDocument response = (XmlDocument)MakeAPIRequest(Methods.ServiceList, false, "GET");
                    return ((ServiceResponse)DeserializeResponse(response, typeof(ServiceResponse))).Services;
                }
                else
                    throw new NotImplementedException("JSON response not supported");
            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates the request.
        /// </summary>
        /// <param name="service_code">The service_code.</param>
        /// <param name="lat">The lat.</param>
        /// <param name="lon">The lon.</param>
        /// <param name="address_string">The address_string.</param>
        /// <param name="customer_email">The customer_email.</param>
        /// <param name="device_id">The device_id.</param>
        /// <param name="acccount_id">The acccount_id.</param>
        /// <param name="first_name">The first_name.</param>
        /// <param name="last_name">The last_name.</param>
        /// <param name="phone_number">The phone_number.</param>
        /// <param name="description">The description.</param>
        /// <param name="media_url">The media_url.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://wiki.open311.org/GeoReport_v2#POST_Service_Request"/&gt;
        /// </remarks>
        public ProblemRequest CreateRequest(string service_code, decimal lat, decimal lon, string address_string, string customer_email,
            string device_id, string acccount_id, string first_name, string last_name, string phone_number, string description, string media_url)
        {
            ClearParameters();

            Parameters.Add(ParametersKey.ApiKey, ApiKey);
            Parameters.Add(ParametersKey.JuristicionID, CityID);

            if (service_code == null || service_code.Equals(string.Empty))
                throw new ArgumentException("Parameter service_code cannot be null or empty");

            try
            {
                Parameters.Add(ParametersKey.ServiceCode, service_code);
                Parameters.Add(ParametersKey.Latitude, lat);
                Parameters.Add(ParametersKey.Longitude, lon);
                Parameters.Add(ParametersKey.AddressString, address_string);
                Parameters.Add(ParametersKey.CustomerEmail, customer_email);
                Parameters.Add(ParametersKey.DeviceID, device_id);
                Parameters.Add(ParametersKey.AccountID, acccount_id);
                Parameters.Add(ParametersKey.FirstName, first_name);
                Parameters.Add(ParametersKey.LastName, last_name);
                Parameters.Add(ParametersKey.PhoneNumber, phone_number);
                Parameters.Add(ParametersKey.Description, description);
                Parameters.Add(ParametersKey.MediaURL, media_url);

                if (ResponseFormat == Formats.XML)
                {
                    XmlDocument response = (XmlDocument)MakeAPIRequest(Methods.ServiceList, false, "POST");

                    ProblemRequest pr = ((RequestResponse)DeserializeResponse(response, typeof(RequestResponse))).Requests[0];

                    if (pr.ServiceRequestID.Equals(string.Empty))
                    {
                        ProblemRequest prFromToken = this.GetRequestByToken(pr.Token);
                        if (prFromToken == null)
                            throw new Open311Exception("Token not found");
                        else
                        {
                            pr.ServiceRequestID = prFromToken.ServiceRequestID;
                            return pr;
                        }
                    }
                    else
                        return pr;
                }   
                else
                    throw new NotImplementedException("JSON response not supported");
            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the request by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public ProblemRequest GetRequestByToken(string token)
        {
            try
            {
                ClearParameters();

                Parameters.Add(ParametersKey.JuristicionID, CityID);

                if (ResponseFormat == Formats.XML)
                {
                    XmlDocument response = (XmlDocument)MakeAPIRequest(String.Format(Methods.Tokens, token), false, "GET");

                    return ((RequestResponse)DeserializeResponse(response, typeof(RequestResponse))).Requests[0];
                }
                else
                    throw new NotImplementedException("JSON response not supported");
                
            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Searches the requests.
        /// </summary>
        /// <param name="serviceRequestID">The service request ID.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public List<ProblemRequest> SearchRequests(string serviceRequestID, string serviceCode, string startDate, string endDate, string status)
        {
            try
            {
                ClearParameters();

                Parameters.Add(ParametersKey.JuristicionID, CityID);
                Parameters.Add(ParametersKey.ServiceRequestID, serviceRequestID);
                Parameters.Add(ParametersKey.ServiceCode, serviceCode);
                Parameters.Add(ParametersKey.StartDate, startDate);
                Parameters.Add(ParametersKey.EndDate, endDate);
                Parameters.Add(ParametersKey.Status, status);

                if (ResponseFormat == Formats.XML)
                {
                    XmlDocument response = (XmlDocument)MakeAPIRequest(Methods.Requests, false, "GET");
                    return ((RequestResponse)DeserializeResponse(response, typeof(RequestResponse))).Requests;
                }
                else
                    throw new NotImplementedException("JSON response not supported");

            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads the request.
        /// </summary>
        /// <param name="serviceRequestID">The service request ID.</param>
        /// <returns></returns>
        public ProblemRequest LoadRequest(string serviceRequestID)
        {
            try
            {
                return SearchRequests(serviceRequestID, string.Empty, string.Empty, string.Empty, string.Empty)[0];
            }
            catch (Open311Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
