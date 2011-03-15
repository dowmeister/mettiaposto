using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using System.Xml;

namespace OpenSignals.Framework.Communications.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class CitySubmissionEmail : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitySubmissionEmail"/> class.
        /// </summary>
        public CitySubmissionEmail()
        {
            this.Sender = new System.Net.Mail.MailAddress(ConfigurationOptions.Current.GetString("email_sender_address"));
            this.Receivers.Add(ConfigurationOptions.Current.GetString("email_sender_address"));
            xslFileName = "CitySubmissionBody";
            this.Subject = "Mettiaposto.it: Città richiesta";
        }

        /// <summary>
        /// Sends the specified city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="email">The email.</param>
        public void Send(string city, string email)
        {
            try
            {
                this.CreateXML(city, email);
                base.Transform();
                base.Send();
            }
            catch (Exception ex)
            {
                log.Error("Error sending alert communication", ex);
                throw ex;
            }
        }

        private void CreateXML(string city, string email)
        {
            base.CreateXML();
            XmlNode root = XmlUtils.CreateNode("CityRequest", xmlDocument, xmlDocument);
            XmlUtils.CreateNode("City", city, xmlDocument, root);
            XmlUtils.CreateNode("Email", email, xmlDocument, root);
        }
    }
}
