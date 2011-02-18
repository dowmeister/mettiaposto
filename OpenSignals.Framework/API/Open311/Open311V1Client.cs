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
    /// Open311 V1 Client implementation
    /// </summary>
    public class Open311V1Client : Open311Base
    {
        private struct Methods
        {
            public const string ServiceList = "service_list";
            public const string CreateRequest = "create_request";
            public const string StatusUpdate = "status_update";
        }

        private XmlDocument MakeAPIRequest(string methodName)
        {
            if (ApiKey.Equals(string.Empty))
                throw new ArgumentException("ApiKey not provided");

            if (CityID.Equals(string.Empty))
                throw new ArgumentException("CityID or JuristicionID not provided");

            if (BaseURL.Equals(string.Empty))
                throw new ArgumentException("BaseUrl not provided");

            try
            {            
                InitializeAPIRequest(BaseURL + "/" + methodName + "?" + this.BuildRequestQueryString());

                SetRequestMethod("GET");

                XmlDocument xmlResponse = new XmlDocument();
                
                xmlResponse.LoadXml(this.GetAPIResponse());
                
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
        public ServiceResponse SelectService()
        {
            try
            {
                Parameters.Add(ParametersKey.ApiKey, ApiKey);
                Parameters.Add(ParametersKey.CityID, CityID);

                MakeAPIRequest(Methods.ServiceList);

                return new ServiceResponse();
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
            Parameters.Add(ParametersKey.ApiKey, ApiKey);
            Parameters.Add(ParametersKey.CityID, CityID);

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
                Parameters.Add(ParametersKey.CustomerEmail, customer_email);
                Parameters.Add(ParametersKey.Description, description);
                Parameters.Add(ParametersKey.MediaURL, media_url);

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
            Parameters.Add(ParametersKey.ApiKey, ApiKey);
            Parameters.Add(ParametersKey.CityID, CityID);

            if (serviceRequestID == null || serviceRequestID.Equals(string.Empty))
                throw new ArgumentException("Parameter serviceRequestID cannot be null or empty");

            try
            {
                Parameters.Add(ParametersKey.ServiceRequestID, serviceRequestID);

                MakeAPIRequest(Methods.StatusUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
