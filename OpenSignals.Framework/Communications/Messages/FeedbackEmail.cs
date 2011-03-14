using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Framework.Communications.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class FeedbackEmail : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackEmail"/> class.
        /// </summary>
        public FeedbackEmail()
        {
            this.Sender = new System.Net.Mail.MailAddress(ConfigurationOptions.Current.GetString("email_sender_address"));
            this.Receivers.Add(ConfigurationOptions.Current.GetString("email_sender_address"));
            xslFileName = "FeedbackBody";
            this.Subject = "Mettiaposto.it: Feedback";
        }

        /// <summary>
        /// Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="message">The message.</param>
        public void Send(string name, string email, string message)
        {
            try
            {
                this.CreateXML(name, email, message);
                base.Transform();
                base.Send();
            }
            catch (Exception ex)
            {
                log.Error("Error sending alert communication", ex);
            }
        }

        private void CreateXML(string name, string email, string message)
        {
            base.CreateXML();
            XmlUtils.CreateXmlNode("Name", name, xmlDocument);
            XmlUtils.CreateXmlNode("Email", email, xmlDocument);
            XmlUtils.CreateXmlNode("Message", message, xmlDocument);
        }
    }
}
