using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Web;

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
            public const string CreateRequest = "create_request";
            /// <summary>
            /// 
            /// </summary>
            public const string StatusUpdate = "status_update";
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
        /// <param name="castResponse">The cast response.</param>
        /// <returns></returns>
        private object MakeAPIRequest(string method, bool ensureApiKey, string requestMethod, Type castResponse)
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
                    url += "?" + this.BuildRequestQueryString();

                InitializeAPIRequest(url);

                SetRequestMethod(requestMethod);

                if (ResponseFormat == Formats.XML)
                {                   
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(this.GetAPIResponse());
                    //ClearNamespaces(doc);
                    XmlSerializer xSerializer = new XmlSerializer(castResponse);
                    XmlNodeReader nReader = new XmlNodeReader(doc);
                    retObject = xSerializer.Deserialize(nReader);
                }

                return retObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns></returns>
        public ServiceResponse GetServices()
        {
            Parameters.Add(ParametersKey.JuristicionID, CityID);

            MakeAPIRequest(Methods.ServiceList, false, "GET", typeof(ServiceResponse));

            return new ServiceResponse();
        }
    }
}
